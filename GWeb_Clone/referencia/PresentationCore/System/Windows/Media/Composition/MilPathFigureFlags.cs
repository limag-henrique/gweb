using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000629 RID: 1577
	[Flags]
	internal enum MilPathFigureFlags
	{
		// Token: 0x04001AA3 RID: 6819
		HasGaps = 1,
		// Token: 0x04001AA4 RID: 6820
		HasCurves = 2,
		// Token: 0x04001AA5 RID: 6821
		IsClosed = 4,
		// Token: 0x04001AA6 RID: 6822
		IsFillable = 8,
		// Token: 0x04001AA7 RID: 6823
		IsRectangleData = 16,
		// Token: 0x04001AA8 RID: 6824
		Mask = 31,
		// Token: 0x04001AA9 RID: 6825
		FORCE_DWORD = -1
	}
}
