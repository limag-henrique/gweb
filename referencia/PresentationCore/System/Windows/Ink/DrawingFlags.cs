using System;

namespace System.Windows.Ink
{
	// Token: 0x02000337 RID: 823
	[Flags]
	internal enum DrawingFlags
	{
		// Token: 0x04000F24 RID: 3876
		Polyline = 0,
		// Token: 0x04000F25 RID: 3877
		FitToCurve = 1,
		// Token: 0x04000F26 RID: 3878
		SubtractiveTransparency = 2,
		// Token: 0x04000F27 RID: 3879
		IgnorePressure = 4,
		// Token: 0x04000F28 RID: 3880
		AntiAliased = 16,
		// Token: 0x04000F29 RID: 3881
		IgnoreRotation = 32,
		// Token: 0x04000F2A RID: 3882
		IgnoreAngle = 64
	}
}
