using System;
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.IO.Packaging;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x02000780 RID: 1920
	internal class FontSource : IFontSource
	{
		// Token: 0x060050C8 RID: 20680 RVA: 0x00143838 File Offset: 0x00142C38
		[SecurityCritical]
		public FontSource(Uri fontUri, bool skipDemand)
		{
			this.Initialize(fontUri, skipDemand, false);
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x00143854 File Offset: 0x00142C54
		[SecurityCritical]
		public FontSource(Uri fontUri, bool skipDemand, bool isComposite)
		{
			this.Initialize(fontUri, skipDemand, isComposite);
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00143870 File Offset: 0x00142C70
		[SecurityCritical]
		private void Initialize(Uri fontUri, bool skipDemand, bool isComposite)
		{
			this._fontUri = fontUri;
			this._skipDemand = skipDemand;
			this._isComposite = isComposite;
			Invariant.Assert(this._fontUri.IsAbsoluteUri);
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060050CB RID: 20683 RVA: 0x001438A4 File Offset: 0x00142CA4
		public bool IsComposite
		{
			get
			{
				return this._isComposite;
			}
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x001438B8 File Offset: 0x00142CB8
		[SecurityCritical]
		public string GetUriString()
		{
			return this._fontUri.GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x001438D4 File Offset: 0x00142CD4
		[SecurityCritical]
		public string ToStringUpperInvariant()
		{
			return this.GetUriString().ToUpperInvariant();
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x001438EC File Offset: 0x00142CEC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override int GetHashCode()
		{
			return HashFn.HashString(this.ToStringUpperInvariant(), 0);
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060050CF RID: 20687 RVA: 0x00143908 File Offset: 0x00142D08
		public Uri Uri
		{
			[SecurityCritical]
			get
			{
				return this._fontUri;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060050D0 RID: 20688 RVA: 0x0014391C File Offset: 0x00142D1C
		public bool IsAppSpecific
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return Util.IsAppSpecificUri(this._fontUri);
			}
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00143934 File Offset: 0x00142D34
		internal long SkipLastWriteTime()
		{
			return -1L;
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00143944 File Offset: 0x00142D44
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public DateTime GetLastWriteTimeUtc()
		{
			if (this._fontUri.IsFile)
			{
				bool flag = false;
				if (this._skipDemand)
				{
					new FileIOPermission(FileIOPermissionAccess.Read, this._fontUri.LocalPath).Assert();
					flag = true;
				}
				try
				{
					return Directory.GetLastWriteTimeUtc(this._fontUri.LocalPath);
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			return DateTime.MaxValue;
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x001439C0 File Offset: 0x00142DC0
		[SecurityCritical]
		public UnmanagedMemoryStream GetUnmanagedStream()
		{
			if (this._fontUri.IsFile)
			{
				FileMapping fileMapping = new FileMapping();
				this.DemandFileIOPermission();
				fileMapping.OpenFile(this._fontUri.LocalPath);
				return fileMapping;
			}
			SizeLimitedCache<Uri, byte[]> resourceCache = FontSource._resourceCache;
			byte[] array;
			lock (resourceCache)
			{
				array = FontSource._resourceCache.Get(this._fontUri);
			}
			if (array == null)
			{
				WebResponse webResponse = WpfWebRequestHelper.CreateRequestAndGetResponse(this._fontUri);
				Stream stream = webResponse.GetResponseStream();
				if (string.Equals(webResponse.ContentType, "application/vnd.ms-package.obfuscated-opentype", StringComparison.Ordinal))
				{
					stream = new DeobfuscatingStream(stream, this._fontUri, false);
				}
				UnmanagedMemoryStream unmanagedMemoryStream = stream as UnmanagedMemoryStream;
				if (unmanagedMemoryStream != null)
				{
					return unmanagedMemoryStream;
				}
				array = FontSource.StreamToByteArray(stream);
				stream.Close();
				SizeLimitedCache<Uri, byte[]> resourceCache2 = FontSource._resourceCache;
				lock (resourceCache2)
				{
					FontSource._resourceCache.Add(this._fontUri, array, false);
				}
			}
			return FontSource.ByteArrayToUnmanagedStream(array);
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x00143AF0 File Offset: 0x00142EF0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void TestFileOpenable()
		{
			if (this._fontUri.IsFile)
			{
				FileMapping fileMapping = new FileMapping();
				this.DemandFileIOPermission();
				fileMapping.OpenFile(this._fontUri.LocalPath);
				fileMapping.Close();
			}
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x00143B30 File Offset: 0x00142F30
		[SecurityCritical]
		public Stream GetStream()
		{
			if (this._fontUri.IsFile)
			{
				FileMapping fileMapping = new FileMapping();
				this.DemandFileIOPermission();
				fileMapping.OpenFile(this._fontUri.LocalPath);
				return fileMapping;
			}
			SizeLimitedCache<Uri, byte[]> resourceCache = FontSource._resourceCache;
			byte[] array;
			lock (resourceCache)
			{
				array = FontSource._resourceCache.Get(this._fontUri);
			}
			if (array != null)
			{
				return new MemoryStream(array);
			}
			WebRequest webRequest = PackWebRequestFactory.CreateWebRequest(this._fontUri);
			WebResponse response = webRequest.GetResponse();
			Stream stream = response.GetResponseStream();
			if (string.Equals(response.ContentType, "application/vnd.ms-package.obfuscated-opentype", StringComparison.Ordinal))
			{
				stream = new DeobfuscatingStream(stream, this._fontUri, false);
			}
			return stream;
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00143C00 File Offset: 0x00143000
		private static UnmanagedMemoryStream ByteArrayToUnmanagedStream(byte[] bits)
		{
			return new FontSource.PinnedByteArrayStream(bits);
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x00143C14 File Offset: 0x00143014
		private static byte[] StreamToByteArray(Stream fontStream)
		{
			byte[] array;
			if (fontStream.CanSeek)
			{
				checked
				{
					array = new byte[(int)fontStream.Length];
					PackagingUtilities.ReliableRead(fontStream, array, 0, (int)fontStream.Length);
				}
			}
			else
			{
				int num = 1048576;
				byte[] array2 = new byte[num];
				int num2 = 0;
				for (;;)
				{
					int num3 = num - num2;
					if (num3 < num / 3)
					{
						num *= 2;
						byte[] array3 = new byte[num];
						Array.Copy(array2, array3, num2);
						array2 = array3;
						num3 = num - num2;
					}
					int num4 = fontStream.Read(array2, num2, num3);
					if (num4 == 0)
					{
						break;
					}
					num2 += num4;
				}
				if (num2 == num)
				{
					array = array2;
				}
				else
				{
					array = new byte[num2];
					Array.Copy(array2, array, num2);
				}
			}
			return array;
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x00143CB0 File Offset: 0x001430B0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void DemandFileIOPermission()
		{
			if (!this._skipDemand)
			{
				SecurityHelper.DemandUriReadPermission(this._fontUri);
			}
		}

		// Token: 0x040024D1 RID: 9425
		private bool _isComposite;

		// Token: 0x040024D2 RID: 9426
		[SecurityCritical]
		private Uri _fontUri;

		// Token: 0x040024D3 RID: 9427
		[SecurityCritical]
		private bool _skipDemand;

		// Token: 0x040024D4 RID: 9428
		private static SizeLimitedCache<Uri, byte[]> _resourceCache = new SizeLimitedCache<Uri, byte[]>(10);

		// Token: 0x040024D5 RID: 9429
		private const int MaximumCacheItems = 10;

		// Token: 0x040024D6 RID: 9430
		private const string ObfuscatedContentType = "application/vnd.ms-package.obfuscated-opentype";

		// Token: 0x020009F6 RID: 2550
		private class PinnedByteArrayStream : UnmanagedMemoryStream
		{
			// Token: 0x06005BD9 RID: 23513 RVA: 0x00171134 File Offset: 0x00170534
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal unsafe PinnedByteArrayStream(byte[] bits)
			{
				this._memoryHandle = GCHandle.Alloc(bits, GCHandleType.Pinned);
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					base.Initialize((byte*)((void*)this._memoryHandle.AddrOfPinnedObject()), (long)bits.Length, (long)bits.Length, FileAccess.Read);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}

			// Token: 0x06005BDA RID: 23514 RVA: 0x001711A4 File Offset: 0x001705A4
			~PinnedByteArrayStream()
			{
				this.Dispose(false);
			}

			// Token: 0x06005BDB RID: 23515 RVA: 0x001711E0 File Offset: 0x001705E0
			[SecurityCritical]
			[SecurityTreatAsSafe]
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				this._memoryHandle.Free();
			}

			// Token: 0x04002F0E RID: 12046
			private GCHandle _memoryHandle;
		}
	}
}
