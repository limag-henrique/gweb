using System;

namespace System.Windows.Input
{
	// Token: 0x02000292 RID: 658
	[Flags]
	internal enum RawKeyboardActions
	{
		// Token: 0x04000A6D RID: 2669
		None = 0,
		// Token: 0x04000A6E RID: 2670
		AttributesChanged = 1,
		// Token: 0x04000A6F RID: 2671
		Activate = 2,
		// Token: 0x04000A70 RID: 2672
		Deactivate = 4,
		// Token: 0x04000A71 RID: 2673
		KeyDown = 8,
		// Token: 0x04000A72 RID: 2674
		KeyUp = 16
	}
}
