using System;

namespace System.Windows.Media
{
	// Token: 0x0200042A RID: 1066
	[Flags]
	internal enum PathGeometryInternalFlags
	{
		// Token: 0x040013E2 RID: 5090
		None = 0,
		// Token: 0x040013E3 RID: 5091
		Invalid = 1,
		// Token: 0x040013E4 RID: 5092
		Dirty = 2,
		// Token: 0x040013E5 RID: 5093
		BoundsValid = 4
	}
}
