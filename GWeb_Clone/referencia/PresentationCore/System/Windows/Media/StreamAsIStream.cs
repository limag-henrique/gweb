using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x0200043F RID: 1087
	internal class StreamAsIStream
	{
		// Token: 0x06002C47 RID: 11335 RVA: 0x000B0E10 File Offset: 0x000B0210
		private StreamAsIStream(Stream dataStream)
		{
			this.dataStream = dataStream;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000B0E34 File Offset: 0x000B0234
		private void ActualizeVirtualPosition()
		{
			if (this.virtualPosition == -1L)
			{
				return;
			}
			if (this.virtualPosition > this.dataStream.Length)
			{
				this.dataStream.SetLength(this.virtualPosition);
			}
			this.dataStream.Position = this.virtualPosition;
			this.virtualPosition = -1L;
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000B0E8C File Offset: 0x000B028C
		public int Clone(out IntPtr stream)
		{
			stream = IntPtr.Zero;
			try
			{
				this.Verify();
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return -2147467263;
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000B0EDC File Offset: 0x000B02DC
		public int Commit(uint grfCommitFlags)
		{
			try
			{
				this.Verify();
				this.dataStream.Flush();
				this.ActualizeVirtualPosition();
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000B0F34 File Offset: 0x000B0334
		[SecurityCritical]
		public int CopyTo(IntPtr pstm, long cb, out long cbRead, out long cbWritten)
		{
			int result = 0;
			uint num = 4096U;
			byte[] buffer = new byte[num];
			cbWritten = 0L;
			cbRead = 0L;
			try
			{
				this.Verify();
				while (cbWritten < cb)
				{
					uint num2 = num;
					if (cbWritten + (long)((ulong)num2) > cb)
					{
						num2 = (uint)(cb - cbWritten);
					}
					uint num3 = 0U;
					result = this.Read(buffer, num2, out num3);
					if (num3 == 0U)
					{
						break;
					}
					cbRead += (long)((ulong)num3);
					uint num4 = 0U;
					result = StreamAsIStream.MILIStreamWrite(pstm, buffer, num3, out num4);
					if (num4 != num3)
					{
						return result;
					}
					cbWritten += (long)((ulong)num3);
				}
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return result;
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000B0FEC File Offset: 0x000B03EC
		public int LockRegion(long libOffset, long cb, uint dwLockType)
		{
			try
			{
				this.Verify();
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return -2147467263;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000B1038 File Offset: 0x000B0438
		public int Read(byte[] buffer, uint cb, out uint cbRead)
		{
			cbRead = 0U;
			try
			{
				this.Verify();
				this.ActualizeVirtualPosition();
				cbRead = (uint)this.dataStream.Read(buffer, 0, (int)cb);
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000B1098 File Offset: 0x000B0498
		public int Revert()
		{
			try
			{
				this.Verify();
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return -2147467263;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000B10E4 File Offset: 0x000B04E4
		[SecurityCritical]
		public unsafe int Seek(long offset, uint origin, long* plibNewPostion)
		{
			try
			{
				this.Verify();
				long position = this.virtualPosition;
				if (this.virtualPosition == -1L)
				{
					position = this.dataStream.Position;
				}
				long length = this.dataStream.Length;
				switch (origin)
				{
				case 0U:
					if (offset <= length)
					{
						this.dataStream.Position = offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = offset;
					}
					break;
				case 1U:
					if (offset + position <= length)
					{
						this.dataStream.Position = position + offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = offset + position;
					}
					break;
				case 2U:
					if (offset <= 0L)
					{
						this.dataStream.Position = length + offset;
						this.virtualPosition = -1L;
					}
					else
					{
						this.virtualPosition = length + offset;
					}
					break;
				}
				if (plibNewPostion != null)
				{
					if (this.virtualPosition != -1L)
					{
						*plibNewPostion = this.virtualPosition;
					}
					else
					{
						*plibNewPostion = this.dataStream.Position;
					}
				}
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000B1200 File Offset: 0x000B0600
		public int SetSize(long value)
		{
			try
			{
				this.Verify();
				this.dataStream.SetLength(value);
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000B1254 File Offset: 0x000B0654
		public int Stat(out System.Runtime.InteropServices.ComTypes.STATSTG statstg, uint grfStatFlag)
		{
			System.Runtime.InteropServices.ComTypes.STATSTG statstg2 = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			statstg = statstg2;
			try
			{
				this.Verify();
				statstg2.type = 2;
				statstg2.cbSize = this.dataStream.Length;
				statstg2.grfLocksSupported = 2;
				statstg2.pwcsName = null;
				statstg = statstg2;
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000B12DC File Offset: 0x000B06DC
		public int UnlockRegion(long libOffset, long cb, uint dwLockType)
		{
			try
			{
				this.Verify();
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return -2147467263;
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000B1328 File Offset: 0x000B0728
		public int Write(byte[] buffer, uint cb, out uint cbWritten)
		{
			cbWritten = 0U;
			try
			{
				this.Verify();
				this.ActualizeVirtualPosition();
				this.dataStream.Write(buffer, 0, (int)cb);
				cbWritten = cb;
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000B1388 File Offset: 0x000B0788
		public int CanWrite(out bool canWrite)
		{
			canWrite = false;
			try
			{
				this.Verify();
				canWrite = this.dataStream.CanWrite;
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000B13E0 File Offset: 0x000B07E0
		public int CanSeek(out bool canSeek)
		{
			canSeek = false;
			try
			{
				this.Verify();
				canSeek = this.dataStream.CanSeek;
			}
			catch (Exception ex)
			{
				this._lastException = ex;
				return SecurityHelper.GetHRForException(ex);
			}
			return 0;
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000B1438 File Offset: 0x000B0838
		private void Verify()
		{
			if (this.dataStream == null)
			{
				throw new ObjectDisposedException(SR.Get("Media_StreamClosed"));
			}
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000B1460 File Offset: 0x000B0860
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static StreamAsIStream FromSD(ref StreamDescriptor sd)
		{
			GCHandle handle = sd.m_handle;
			return (StreamAsIStream)handle.Target;
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000B1480 File Offset: 0x000B0880
		internal static int Clone(ref StreamDescriptor pSD, out IntPtr stream)
		{
			return StreamAsIStream.FromSD(ref pSD).Clone(out stream);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000B149C File Offset: 0x000B089C
		internal static int Commit(ref StreamDescriptor pSD, uint grfCommitFlags)
		{
			return StreamAsIStream.FromSD(ref pSD).Commit(grfCommitFlags);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000B14B8 File Offset: 0x000B08B8
		[SecurityCritical]
		internal static int CopyTo(ref StreamDescriptor pSD, IntPtr pstm, long cb, out long cbRead, out long cbWritten)
		{
			return StreamAsIStream.FromSD(ref pSD).CopyTo(pstm, cb, out cbRead, out cbWritten);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000B14D8 File Offset: 0x000B08D8
		internal static int LockRegion(ref StreamDescriptor pSD, long libOffset, long cb, uint dwLockType)
		{
			return StreamAsIStream.FromSD(ref pSD).LockRegion(libOffset, cb, dwLockType);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000B14F4 File Offset: 0x000B08F4
		internal static int Read(ref StreamDescriptor pSD, byte[] buffer, uint cb, out uint cbRead)
		{
			return StreamAsIStream.FromSD(ref pSD).Read(buffer, cb, out cbRead);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000B1510 File Offset: 0x000B0910
		internal static int Revert(ref StreamDescriptor pSD)
		{
			return StreamAsIStream.FromSD(ref pSD).Revert();
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000B1528 File Offset: 0x000B0928
		[SecurityCritical]
		internal unsafe static int Seek(ref StreamDescriptor pSD, long offset, uint origin, long* plibNewPostion)
		{
			return StreamAsIStream.FromSD(ref pSD).Seek(offset, origin, plibNewPostion);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000B1544 File Offset: 0x000B0944
		internal static int SetSize(ref StreamDescriptor pSD, long value)
		{
			return StreamAsIStream.FromSD(ref pSD).SetSize(value);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000B1560 File Offset: 0x000B0960
		internal static int Stat(ref StreamDescriptor pSD, out System.Runtime.InteropServices.ComTypes.STATSTG statstg, uint grfStatFlag)
		{
			return StreamAsIStream.FromSD(ref pSD).Stat(out statstg, grfStatFlag);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000B157C File Offset: 0x000B097C
		internal static int UnlockRegion(ref StreamDescriptor pSD, long libOffset, long cb, uint dwLockType)
		{
			return StreamAsIStream.FromSD(ref pSD).UnlockRegion(libOffset, cb, dwLockType);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000B1598 File Offset: 0x000B0998
		internal static int Write(ref StreamDescriptor pSD, byte[] buffer, uint cb, out uint cbWritten)
		{
			return StreamAsIStream.FromSD(ref pSD).Write(buffer, cb, out cbWritten);
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000B15B4 File Offset: 0x000B09B4
		internal static int CanWrite(ref StreamDescriptor pSD, out bool canWrite)
		{
			return StreamAsIStream.FromSD(ref pSD).CanWrite(out canWrite);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000B15D0 File Offset: 0x000B09D0
		internal static int CanSeek(ref StreamDescriptor pSD, out bool canSeek)
		{
			return StreamAsIStream.FromSD(ref pSD).CanSeek(out canSeek);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000B15EC File Offset: 0x000B09EC
		[SecurityCritical]
		internal static IntPtr IStreamMemoryFrom(IntPtr comStream)
		{
			IntPtr zero = IntPtr.Zero;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				if (HRESULT.Failed(UnsafeNativeMethods.WICImagingFactory.CreateStream(factoryMaker.ImagingFactoryPtr, out zero)))
				{
					return IntPtr.Zero;
				}
				if (HRESULT.Failed(UnsafeNativeMethods.WICStream.InitializeFromIStream(zero, comStream)))
				{
					UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref zero);
					return IntPtr.Zero;
				}
			}
			return zero;
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000B166C File Offset: 0x000B0A6C
		[SecurityCritical]
		internal static IntPtr IStreamFrom(IntPtr memoryBuffer, int bufferSize)
		{
			IntPtr zero = IntPtr.Zero;
			using (FactoryMaker factoryMaker = new FactoryMaker())
			{
				if (HRESULT.Failed(UnsafeNativeMethods.WICImagingFactory.CreateStream(factoryMaker.ImagingFactoryPtr, out zero)))
				{
					return IntPtr.Zero;
				}
				if (HRESULT.Failed(UnsafeNativeMethods.WICStream.InitializeFromMemory(zero, memoryBuffer, (uint)bufferSize)))
				{
					UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref zero);
					return IntPtr.Zero;
				}
			}
			return zero;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000B16EC File Offset: 0x000B0AEC
		[SecurityCritical]
		internal static IntPtr IStreamFrom(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			IntPtr zero = IntPtr.Zero;
			StreamAsIStream value = new StreamAsIStream(stream);
			StreamDescriptor streamDescriptor = default(StreamDescriptor);
			streamDescriptor.pfnDispose = StaticPtrs.pfnDispose;
			streamDescriptor.pfnClone = StaticPtrs.pfnClone;
			streamDescriptor.pfnCommit = StaticPtrs.pfnCommit;
			streamDescriptor.pfnCopyTo = StaticPtrs.pfnCopyTo;
			streamDescriptor.pfnLockRegion = StaticPtrs.pfnLockRegion;
			streamDescriptor.pfnRead = StaticPtrs.pfnRead;
			streamDescriptor.pfnRevert = StaticPtrs.pfnRevert;
			streamDescriptor.pfnSeek = StaticPtrs.pfnSeek;
			streamDescriptor.pfnSetSize = StaticPtrs.pfnSetSize;
			streamDescriptor.pfnStat = StaticPtrs.pfnStat;
			streamDescriptor.pfnUnlockRegion = StaticPtrs.pfnUnlockRegion;
			streamDescriptor.pfnWrite = StaticPtrs.pfnWrite;
			streamDescriptor.pfnCanWrite = StaticPtrs.pfnCanWrite;
			streamDescriptor.pfnCanSeek = StaticPtrs.pfnCanSeek;
			streamDescriptor.m_handle = GCHandle.Alloc(value, GCHandleType.Normal);
			HRESULT.Check(UnsafeNativeMethods.MilCoreApi.MILCreateStreamFromStreamDescriptor(ref streamDescriptor, out zero));
			return zero;
		}

		// Token: 0x06002C68 RID: 11368
		[DllImport("wpfgfx_v0400.dll")]
		private static extern int MILIStreamWrite(IntPtr pStream, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, uint cb, out uint cbWritten);

		// Token: 0x04001446 RID: 5190
		private const int STREAM_SEEK_SET = 0;

		// Token: 0x04001447 RID: 5191
		private const int STREAM_SEEK_CUR = 1;

		// Token: 0x04001448 RID: 5192
		private const int STREAM_SEEK_END = 2;

		// Token: 0x04001449 RID: 5193
		protected Stream dataStream;

		// Token: 0x0400144A RID: 5194
		private Exception _lastException;

		// Token: 0x0400144B RID: 5195
		private long virtualPosition = -1L;
	}
}
