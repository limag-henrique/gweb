using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using MS.Internal.PresentationCore;

namespace MS.Win32.Compile
{
	// Token: 0x0200065D RID: 1629
	[FriendAccessAllowed]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600488D RID: 18573
		[DllImport("shfolder.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

		// Token: 0x0600488E RID: 18574
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("urlmon.dll", CharSet = CharSet.Unicode)]
		internal static extern int FindMimeFromData(IBindCtx pBC, string wszUrl, IntPtr Buffer, int cbSize, string wzMimeProposed, int dwMimeFlags, out string wzMimeOut, int dwReserved);
	}
}
