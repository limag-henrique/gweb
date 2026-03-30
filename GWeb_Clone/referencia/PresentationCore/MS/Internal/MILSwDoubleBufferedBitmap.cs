using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MS.Internal
{
	// Token: 0x0200066A RID: 1642
	internal static class MILSwDoubleBufferedBitmap
	{
		// Token: 0x060048AB RID: 18603
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILSwDoubleBufferedBitmapCreate")]
		internal static extern int Create(uint width, uint height, double dpiX, double dpiY, ref Guid pixelFormatGuid, SafeMILHandle pPalette, out SafeMILHandle ppSwDoubleBufferedBitmap);

		// Token: 0x060048AC RID: 18604
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILSwDoubleBufferedBitmapGetBackBuffer", PreserveSig = false)]
		internal static extern void GetBackBuffer(SafeMILHandle THIS_PTR, out BitmapSourceSafeMILHandle pBackBuffer, out uint pBackBufferSize);

		// Token: 0x060048AD RID: 18605
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILSwDoubleBufferedBitmapAddDirtyRect", PreserveSig = false)]
		internal static extern void AddDirtyRect(SafeMILHandle THIS_PTR, ref Int32Rect dirtyRect);

		// Token: 0x060048AE RID: 18606
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILSwDoubleBufferedBitmapProtectBackBuffer")]
		internal static extern int ProtectBackBuffer(SafeMILHandle THIS_PTR);
	}
}
