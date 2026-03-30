using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200063B RID: 1595
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_SEGMENT_LINE
	{
		// Token: 0x04001BA4 RID: 7076
		[FieldOffset(0)]
		internal MIL_SEGMENT_TYPE Type;

		// Token: 0x04001BA5 RID: 7077
		[FieldOffset(4)]
		internal MILCoreSegFlags Flags;

		// Token: 0x04001BA6 RID: 7078
		[FieldOffset(8)]
		internal uint BackSize;

		// Token: 0x04001BA7 RID: 7079
		[FieldOffset(12)]
		internal uint ForcePacking;

		// Token: 0x04001BA8 RID: 7080
		[FieldOffset(16)]
		internal Point Point;
	}
}
