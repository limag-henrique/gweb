using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Win32.Penimc
{
	// Token: 0x0200064E RID: 1614
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SuppressUnmanagedCodeSecurity]
	[Guid("215B68E5-0E78-4505-BE40-962EE3A0C379")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IPimcManager2
	{
		// Token: 0x06004874 RID: 18548
		void GetTabletCount(out uint count);

		// Token: 0x06004875 RID: 18549
		void GetTablet(uint tablet, out IPimcTablet2 IPimcTablet);
	}
}
