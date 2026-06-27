using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200063D RID: 1597
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_SEGMENT_QUADRATICBEZIER
	{
		// Token: 0x04001BB0 RID: 7088
		[FieldOffset(0)]
		internal MIL_SEGMENT_TYPE Type;

		// Token: 0x04001BB1 RID: 7089
		[FieldOffset(4)]
		internal MILCoreSegFlags Flags;

		// Token: 0x04001BB2 RID: 7090
		[FieldOffset(8)]
		internal uint BackSize;

		// Token: 0x04001BB3 RID: 7091
		[FieldOffset(12)]
		internal uint ForcePacking;

		// Token: 0x04001BB4 RID: 7092
		[FieldOffset(16)]
		internal Point Point1;

		// Token: 0x04001BB5 RID: 7093
		[FieldOffset(32)]
		internal Point Point2;
	}
}
