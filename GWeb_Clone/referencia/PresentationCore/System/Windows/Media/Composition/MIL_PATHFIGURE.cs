using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000639 RID: 1593
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_PATHFIGURE
	{
		// Token: 0x04001B9A RID: 7066
		[FieldOffset(0)]
		internal uint BackSize;

		// Token: 0x04001B9B RID: 7067
		[FieldOffset(4)]
		internal MilPathFigureFlags Flags;

		// Token: 0x04001B9C RID: 7068
		[FieldOffset(8)]
		internal uint Count;

		// Token: 0x04001B9D RID: 7069
		[FieldOffset(12)]
		internal uint Size;

		// Token: 0x04001B9E RID: 7070
		[FieldOffset(16)]
		internal Point StartPoint;

		// Token: 0x04001B9F RID: 7071
		[FieldOffset(32)]
		internal uint OffsetToLastSegment;

		// Token: 0x04001BA0 RID: 7072
		[FieldOffset(36)]
		internal uint ForcePacking;
	}
}
