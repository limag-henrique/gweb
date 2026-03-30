using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal.Text.TextInterface.Interfaces;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200001D RID: 29
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	internal class FontFileStream : IDWriteFontFileStreamMirror, IDisposable
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000BD24 File Offset: 0x0000B124
		public FontFileStream(IFontSource fontSource)
		{
			this._fontSourceStream = fontSource.GetUnmanagedStream();
			try
			{
				this._lastWriteTime = fontSource.GetLastWriteTimeUtc().ToFileTimeUtc();
			}
			catch (ArgumentOutOfRangeException)
			{
				this._lastWriteTime = -1L;
			}
			this._fontSourceStreamLock = new object();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B870 File Offset: 0x0000AC70
		public FontFileStream()
		{
			Debug.Assert(false);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BD94 File Offset: 0x0000B194
		private void ~FontFileStream()
		{
			this._fontSourceStream.Close();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000BDAC File Offset: 0x0000B1AC
		[SecurityCritical]
		[ComVisible(true)]
		public unsafe virtual int ReadFileFragment(void** fragmentStart, ulong fileOffset, ulong fragmentSize, void** fragmentContext)
		{
			int result = 0;
			try
			{
				if (fragmentContext == null || fragmentStart == null || fileOffset > 2147483647UL || fragmentSize > 2147483647UL || fileOffset > 18446744073709551615UL - fragmentSize || fileOffset + fragmentSize > (ulong)this._fontSourceStream.Length)
				{
					return -2147024809;
				}
				int num = (int)fragmentSize;
				byte[] array = new byte[num];
				lock (this._fontSourceStreamLock)
				{
					this._fontSourceStream.Seek((long)fileOffset, SeekOrigin.Begin);
					this._fontSourceStream.Read(array, 0, num);
				}
				GCHandle value = GCHandle.Alloc(array, GCHandleType.Pinned);
				*(long*)fragmentStart = value.AddrOfPinnedObject().ToPointer();
				*(long*)fragmentContext = GCHandle.ToIntPtr(value).ToPointer();
			}
			catch (Exception e)
			{
				result = Marshal.GetHRForException(e);
			}
			return result;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000BEB8 File Offset: 0x0000B2B8
		[SecurityCritical]
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public unsafe virtual void ReleaseFileFragment(void* fragmentContext)
		{
			if (fragmentContext != null)
			{
				IntPtr value = new IntPtr(fragmentContext);
				GCHandle.FromIntPtr(value).Free();
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000BEE0 File Offset: 0x0000B2E0
		[SecurityCritical]
		[ComVisible(true)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public unsafe virtual int GetFileSize(ulong* fileSize)
		{
			if (fileSize == null)
			{
				return -2147024809;
			}
			int result = 0;
			try
			{
				*fileSize = (ulong)this._fontSourceStream.Length;
			}
			catch (Exception e)
			{
				result = Marshal.GetHRForException(e);
			}
			return result;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000BF2C File Offset: 0x0000B32C
		[ComVisible(true)]
		public unsafe virtual int GetLastWriteTime(ulong* lastWriteTime)
		{
			long lastWriteTime2 = this._lastWriteTime;
			if (lastWriteTime2 < 0L)
			{
				return -2147467259;
			}
			if (lastWriteTime == null)
			{
				return -2147024809;
			}
			*lastWriteTime = (ulong)lastWriteTime2;
			return 0;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000F490 File Offset: 0x0000E890
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~FontFileStream();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010580 File Offset: 0x0000F980
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000329 RID: 809
		private Stream _fontSourceStream;

		// Token: 0x0400032A RID: 810
		private long _lastWriteTime;

		// Token: 0x0400032B RID: 811
		private object _fontSourceStreamLock;
	}
}
