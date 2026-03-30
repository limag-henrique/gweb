using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Win32.Penimc
{
	// Token: 0x0200064C RID: 1612
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1868091E-AB5A-415F-A02F-5C4DD0CF901D")]
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[ComImport]
	internal interface IPimcContext2
	{
		// Token: 0x0600485E RID: 18526
		void ShutdownComm();

		// Token: 0x0600485F RID: 18527
		void GetPacketDescriptionInfo(out int cProps, out int cButtons);

		// Token: 0x06004860 RID: 18528
		void GetPacketPropertyInfo(int iProp, out Guid guid, out int iMin, out int iMax, out int iUnits, out float flResolution);

		// Token: 0x06004861 RID: 18529
		void GetPacketButtonInfo(int iButton, out Guid guid);

		// Token: 0x06004862 RID: 18530
		void GetLastSystemEventData(out int evt, out int modifier, out int character, out int x, out int y, out int stylusMode, out int buttonState);
	}
}
