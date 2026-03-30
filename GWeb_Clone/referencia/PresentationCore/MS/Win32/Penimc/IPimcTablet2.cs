using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Win32.Penimc
{
	// Token: 0x0200064D RID: 1613
	[Guid("0EF507FF-0B48-40AD-84DB-E4C7AB81B74A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IPimcTablet2
	{
		// Token: 0x06004863 RID: 18531
		void GetKey(out int key);

		// Token: 0x06004864 RID: 18532
		void GetName([MarshalAs(UnmanagedType.LPWStr)] out string name);

		// Token: 0x06004865 RID: 18533
		void GetPlugAndPlayId([MarshalAs(UnmanagedType.LPWStr)] out string plugAndPlayId);

		// Token: 0x06004866 RID: 18534
		void GetTabletAndDisplaySize(out int tabletWidth, out int tabletHeight, out int displayWidth, out int displayHeight);

		// Token: 0x06004867 RID: 18535
		void GetHardwareCaps(out int caps);

		// Token: 0x06004868 RID: 18536
		void GetDeviceType(out int devType);

		// Token: 0x06004869 RID: 18537
		void RefreshCursorInfo();

		// Token: 0x0600486A RID: 18538
		void GetCursorCount(out int cCursors);

		// Token: 0x0600486B RID: 18539
		void GetCursorInfo(int iCursor, [MarshalAs(UnmanagedType.LPWStr)] out string sName, out int id, [MarshalAs(UnmanagedType.Bool)] out bool fInverted);

		// Token: 0x0600486C RID: 18540
		void GetCursorButtonCount(int iCursor, out int cButtons);

		// Token: 0x0600486D RID: 18541
		void GetCursorButtonInfo(int iCursor, int iButton, [MarshalAs(UnmanagedType.LPWStr)] out string sName, out Guid guid);

		// Token: 0x0600486E RID: 18542
		void IsPropertySupported(Guid guid, [MarshalAs(UnmanagedType.Bool)] out bool fSupported);

		// Token: 0x0600486F RID: 18543
		void GetPropertyInfo(Guid guid, out int min, out int max, out int units, out float resolution);

		// Token: 0x06004870 RID: 18544
		void CreateContext(IntPtr handle, [MarshalAs(UnmanagedType.Bool)] bool fEnable, uint timeout, out IPimcContext2 IPimcContext, out int key, out long commHandle);

		// Token: 0x06004871 RID: 18545
		void GetPacketDescriptionInfo(out int cProps, out int cButtons);

		// Token: 0x06004872 RID: 18546
		void GetPacketPropertyInfo(int iProp, out Guid guid, out int iMin, out int iMax, out int iUnits, out float flResolution);

		// Token: 0x06004873 RID: 18547
		void GetPacketButtonInfo(int iButton, out Guid guid);
	}
}
