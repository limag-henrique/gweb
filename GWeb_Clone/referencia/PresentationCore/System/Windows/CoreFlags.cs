using System;

namespace System.Windows
{
	// Token: 0x020001F5 RID: 501
	[Flags]
	internal enum CoreFlags : uint
	{
		// Token: 0x040007CF RID: 1999
		None = 0U,
		// Token: 0x040007D0 RID: 2000
		SnapsToDevicePixelsCache = 1U,
		// Token: 0x040007D1 RID: 2001
		ClipToBoundsCache = 2U,
		// Token: 0x040007D2 RID: 2002
		MeasureDirty = 4U,
		// Token: 0x040007D3 RID: 2003
		ArrangeDirty = 8U,
		// Token: 0x040007D4 RID: 2004
		MeasureInProgress = 16U,
		// Token: 0x040007D5 RID: 2005
		ArrangeInProgress = 32U,
		// Token: 0x040007D6 RID: 2006
		NeverMeasured = 64U,
		// Token: 0x040007D7 RID: 2007
		NeverArranged = 128U,
		// Token: 0x040007D8 RID: 2008
		MeasureDuringArrange = 256U,
		// Token: 0x040007D9 RID: 2009
		IsCollapsed = 512U,
		// Token: 0x040007DA RID: 2010
		IsKeyboardFocusWithinCache = 1024U,
		// Token: 0x040007DB RID: 2011
		IsKeyboardFocusWithinChanged = 2048U,
		// Token: 0x040007DC RID: 2012
		IsMouseOverCache = 4096U,
		// Token: 0x040007DD RID: 2013
		IsMouseOverChanged = 8192U,
		// Token: 0x040007DE RID: 2014
		IsMouseCaptureWithinCache = 16384U,
		// Token: 0x040007DF RID: 2015
		IsMouseCaptureWithinChanged = 32768U,
		// Token: 0x040007E0 RID: 2016
		IsStylusOverCache = 65536U,
		// Token: 0x040007E1 RID: 2017
		IsStylusOverChanged = 131072U,
		// Token: 0x040007E2 RID: 2018
		IsStylusCaptureWithinCache = 262144U,
		// Token: 0x040007E3 RID: 2019
		IsStylusCaptureWithinChanged = 524288U,
		// Token: 0x040007E4 RID: 2020
		HasAutomationPeer = 1048576U,
		// Token: 0x040007E5 RID: 2021
		RenderingInvalidated = 2097152U,
		// Token: 0x040007E6 RID: 2022
		IsVisibleCache = 4194304U,
		// Token: 0x040007E7 RID: 2023
		AreTransformsClean = 8388608U,
		// Token: 0x040007E8 RID: 2024
		IsOpacitySuppressed = 16777216U,
		// Token: 0x040007E9 RID: 2025
		ExistsEventHandlersStore = 33554432U,
		// Token: 0x040007EA RID: 2026
		TouchesOverCache = 67108864U,
		// Token: 0x040007EB RID: 2027
		TouchesOverChanged = 134217728U,
		// Token: 0x040007EC RID: 2028
		TouchesCapturedWithinCache = 268435456U,
		// Token: 0x040007ED RID: 2029
		TouchesCapturedWithinChanged = 536870912U,
		// Token: 0x040007EE RID: 2030
		TouchLeaveCache = 1073741824U,
		// Token: 0x040007EF RID: 2031
		TouchEnterCache = 2147483648U
	}
}
