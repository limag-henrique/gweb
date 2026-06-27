using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200063A RID: 1594
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_SEGMENT
	{
		// Token: 0x04001BA1 RID: 7073
		[FieldOffset(0)]
		internal MIL_SEGMENT_TYPE Type;

		// Token: 0x04001BA2 RID: 7074
		[FieldOffset(4)]
		internal MILCoreSegFlags Flags;

		// Token: 0x04001BA3 RID: 7075
		[FieldOffset(8)]
		internal uint BackSize;
	}
}
