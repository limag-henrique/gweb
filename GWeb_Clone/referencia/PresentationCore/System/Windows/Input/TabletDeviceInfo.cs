using System;
using System.Collections.ObjectModel;
using System.Security;
using MS.Internal;
using MS.Win32.Penimc;

namespace System.Windows.Input
{
	// Token: 0x020002CD RID: 717
	internal class TabletDeviceInfo
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0004FCE4 File Offset: 0x0004F0E4
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x0004FCF8 File Offset: 0x0004F0F8
		public uint WispTabletKey { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x04000BAE RID: 2990
		[SecurityCritical]
		public SecurityCriticalDataClass<IPimcTablet2> PimcTablet;

		// Token: 0x04000BAF RID: 2991
		public int Id;

		// Token: 0x04000BB0 RID: 2992
		public string Name;

		// Token: 0x04000BB1 RID: 2993
		public string PlugAndPlayId;

		// Token: 0x04000BB2 RID: 2994
		public TabletDeviceSizeInfo SizeInfo;

		// Token: 0x04000BB3 RID: 2995
		public TabletHardwareCapabilities HardwareCapabilities;

		// Token: 0x04000BB4 RID: 2996
		public TabletDeviceType DeviceType;

		// Token: 0x04000BB5 RID: 2997
		public ReadOnlyCollection<StylusPointProperty> StylusPointProperties;

		// Token: 0x04000BB6 RID: 2998
		public int PressureIndex;

		// Token: 0x04000BB7 RID: 2999
		public StylusDeviceInfo[] StylusDevicesInfo;
	}
}
