using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200063E RID: 1598
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_SEGMENT_ARC
	{
		// Token: 0x04001BB6 RID: 7094
		[FieldOffset(0)]
		internal MIL_SEGMENT_TYPE Type;

		// Token: 0x04001BB7 RID: 7095
		[FieldOffset(4)]
		internal MILCoreSegFlags Flags;

		// Token: 0x04001BB8 RID: 7096
		[FieldOffset(8)]
		internal uint BackSize;

		// Token: 0x04001BB9 RID: 7097
		[FieldOffset(12)]
		internal uint LargeArc;

		// Token: 0x04001BBA RID: 7098
		[FieldOffset(16)]
		internal Point Point;

		// Token: 0x04001BBB RID: 7099
		[FieldOffset(32)]
		internal Size Size;

		// Token: 0x04001BBC RID: 7100
		[FieldOffset(48)]
		internal double XRotation;

		// Token: 0x04001BBD RID: 7101
		[FieldOffset(56)]
		internal uint Sweep;

		// Token: 0x04001BBE RID: 7102
		[FieldOffset(60)]
		internal uint ForcePacking;
	}
}
