using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000628 RID: 1576
	[Flags]
	internal enum MilPathGeometryFlags
	{
		// Token: 0x04001A9B RID: 6811
		HasCurves = 1,
		// Token: 0x04001A9C RID: 6812
		BoundsValid = 2,
		// Token: 0x04001A9D RID: 6813
		HasGaps = 4,
		// Token: 0x04001A9E RID: 6814
		HasHollows = 8,
		// Token: 0x04001A9F RID: 6815
		IsRegionData = 16,
		// Token: 0x04001AA0 RID: 6816
		Mask = 31,
		// Token: 0x04001AA1 RID: 6817
		FORCE_DWORD = -1
	}
}
