using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000729 RID: 1833
	[Flags]
	internal enum LineFlags
	{
		// Token: 0x040021D4 RID: 8660
		None = 0,
		// Token: 0x040021D5 RID: 8661
		BreakClassWide = 1,
		// Token: 0x040021D6 RID: 8662
		BreakClassStrict = 2,
		// Token: 0x040021D7 RID: 8663
		BreakAlways = 4,
		// Token: 0x040021D8 RID: 8664
		MinMax = 8,
		// Token: 0x040021D9 RID: 8665
		KeepState = 16
	}
}
