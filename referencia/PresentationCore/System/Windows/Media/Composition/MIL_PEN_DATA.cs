using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000624 RID: 1572
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MIL_PEN_DATA
	{
		// Token: 0x04001A7E RID: 6782
		internal double Thickness;

		// Token: 0x04001A7F RID: 6783
		internal double MiterLimit;

		// Token: 0x04001A80 RID: 6784
		internal double DashOffset;

		// Token: 0x04001A81 RID: 6785
		internal MIL_PEN_CAP StartLineCap;

		// Token: 0x04001A82 RID: 6786
		internal MIL_PEN_CAP EndLineCap;

		// Token: 0x04001A83 RID: 6787
		internal MIL_PEN_CAP DashCap;

		// Token: 0x04001A84 RID: 6788
		internal MIL_PEN_JOIN LineJoin;

		// Token: 0x04001A85 RID: 6789
		internal uint DashArraySize;
	}
}
