using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MS.Internal
{
	// Token: 0x02000668 RID: 1640
	internal static class MILRenderTargetBitmap
	{
		// Token: 0x06004894 RID: 18580
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILRenderTargetBitmapGetBitmap")]
		internal static extern int GetBitmap(SafeMILHandle THIS_PTR, out BitmapSourceSafeMILHandle ppIBitmap);

		// Token: 0x06004895 RID: 18581
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILRenderTargetBitmapClear")]
		internal static extern int Clear(SafeMILHandle THIS_PTR);
	}
}
