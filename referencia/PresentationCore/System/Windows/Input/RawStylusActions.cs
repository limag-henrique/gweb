using System;

namespace System.Windows.Input
{
	// Token: 0x020002A7 RID: 679
	[Flags]
	internal enum RawStylusActions
	{
		// Token: 0x04000AC1 RID: 2753
		None = 0,
		// Token: 0x04000AC2 RID: 2754
		Activate = 1,
		// Token: 0x04000AC3 RID: 2755
		Deactivate = 2,
		// Token: 0x04000AC4 RID: 2756
		Down = 4,
		// Token: 0x04000AC5 RID: 2757
		Up = 8,
		// Token: 0x04000AC6 RID: 2758
		Move = 16,
		// Token: 0x04000AC7 RID: 2759
		InAirMove = 32,
		// Token: 0x04000AC8 RID: 2760
		InRange = 64,
		// Token: 0x04000AC9 RID: 2761
		OutOfRange = 128,
		// Token: 0x04000ACA RID: 2762
		SystemGesture = 256
	}
}
