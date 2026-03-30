using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal.FontCache
{
	// Token: 0x0200077C RID: 1916
	[FriendAccessAllowed]
	internal class FileMapping : UnmanagedMemoryStream
	{
		// Token: 0x060050B5 RID: 20661 RVA: 0x00143298 File Offset: 0x00142698
		~FileMapping()
		{
			this.Dispose(false);
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x001432D4 File Offset: 0x001426D4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!this._disposed)
			{
				if (disposing)
				{
					if (this._viewHandle != null)
					{
						this._viewHandle.Dispose();
					}
					if (this._mappingHandle != null)
					{
						this._mappingHandle.Dispose();
					}
				}
				Invariant.Assert(!this.CanWrite);
			}
			this._disposed = true;
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x00143330 File Offset: 0x00142730
		[SecurityCritical]
		internal unsafe void OpenFile(string fileName)
		{
			NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES();
			try
			{
				long quadPart;
				using (SafeFileHandle safeFileHandle = UnsafeNativeMethods.CreateFile(fileName, 2147483648U, 1U, null, 3, 0, IntPtr.Zero))
				{
					if (safeFileHandle.IsInvalid)
					{
						Util.ThrowWin32Exception(Marshal.GetLastWin32Error(), fileName);
					}
					UnsafeNativeMethods.LARGE_INTEGER large_INTEGER = default(UnsafeNativeMethods.LARGE_INTEGER);
					if (!UnsafeNativeMethods.GetFileSizeEx(safeFileHandle, ref large_INTEGER))
					{
						throw new IOException(SR.Get("IOExceptionWithFileName", new object[]
						{
							fileName
						}));
					}
					quadPart = large_INTEGER.QuadPart;
					if (quadPart == 0L)
					{
						throw new FileFormatException(new Uri(fileName));
					}
					this._mappingHandle = UnsafeNativeMethods.CreateFileMapping(safeFileHandle, security_ATTRIBUTES, 2, 0U, 0U, null);
				}
				if (this._mappingHandle.IsInvalid)
				{
					throw new IOException(SR.Get("IOExceptionWithFileName", new object[]
					{
						fileName
					}));
				}
				this._viewHandle = UnsafeNativeMethods.MapViewOfFileEx(this._mappingHandle, 4, 0, 0, IntPtr.Zero, IntPtr.Zero);
				if (this._viewHandle.IsInvalid)
				{
					throw new IOException(SR.Get("IOExceptionWithFileName", new object[]
					{
						fileName
					}));
				}
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					base.Initialize((byte*)this._viewHandle.Memory, quadPart, quadPart, FileAccess.Read);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			finally
			{
				security_ATTRIBUTES.Release();
				security_ATTRIBUTES = null;
			}
		}

		// Token: 0x040024C7 RID: 9415
		[SecurityCritical]
		private UnsafeNativeMethods.SafeViewOfFileHandle _viewHandle;

		// Token: 0x040024C8 RID: 9416
		[SecurityCritical]
		private UnsafeNativeMethods.SafeFileMappingHandle _mappingHandle;

		// Token: 0x040024C9 RID: 9417
		private bool _disposed;
	}
}
