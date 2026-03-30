using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200063C RID: 1596
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_SEGMENT_BEZIER
	{
		// Token: 0x04001BA9 RID: 7081
		[FieldOffset(0)]
		internal MIL_SEGMENT_TYPE Type;

		// Token: 0x04001BAA RID: 7082
		[FieldOffset(4)]
		internal MILCoreSegFlags Flags;

		// Token: 0x04001BAB RID: 7083
		[FieldOffset(8)]
		internal uint BackSize;

		// Token: 0x04001BAC RID: 7084
		[FieldOffset(12)]
		internal uint ForcePacking;

		// Token: 0x04001BAD RID: 7085
		[FieldOffset(16)]
		internal Point Point1;

		// Token: 0x04001BAE RID: 7086
		[FieldOffset(32)]
		internal Point Point2;

		// Token: 0x04001BAF RID: 7087
		[FieldOffset(48)]
		internal Point Point3;
	}
}
