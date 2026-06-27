using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200061F RID: 1567
	[Flags]
	internal enum MILCoreSegFlags
	{
		// Token: 0x04001A47 RID: 6727
		SegTypeLine = 1,
		// Token: 0x04001A48 RID: 6728
		SegTypeBezier = 2,
		// Token: 0x04001A49 RID: 6729
		SegTypeMask = 3,
		// Token: 0x04001A4A RID: 6730
		SegIsAGap = 4,
		// Token: 0x04001A4B RID: 6731
		SegSmoothJoin = 8,
		// Token: 0x04001A4C RID: 6732
		SegClosed = 16,
		// Token: 0x04001A4D RID: 6733
		SegIsCurved = 32,
		// Token: 0x04001A4E RID: 6734
		FORCE_DWORD = -1
	}
}
