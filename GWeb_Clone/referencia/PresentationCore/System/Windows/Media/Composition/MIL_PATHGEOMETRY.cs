using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000638 RID: 1592
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_PATHGEOMETRY
	{
		// Token: 0x04001B95 RID: 7061
		[FieldOffset(0)]
		internal uint Size;

		// Token: 0x04001B96 RID: 7062
		[FieldOffset(4)]
		internal MilPathGeometryFlags Flags;

		// Token: 0x04001B97 RID: 7063
		[FieldOffset(8)]
		internal MilRectD Bounds;

		// Token: 0x04001B98 RID: 7064
		[FieldOffset(40)]
		internal uint FigureCount;

		// Token: 0x04001B99 RID: 7065
		[FieldOffset(44)]
		internal uint ForcePacking;
	}
}
