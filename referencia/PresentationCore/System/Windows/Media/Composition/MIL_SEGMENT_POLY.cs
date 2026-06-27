using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200063F RID: 1599
	[StructLayout(LayoutKind.Explicit)]
	internal struct MIL_SEGMENT_POLY
	{
		// Token: 0x04001BBF RID: 7103
		[FieldOffset(0)]
		internal MIL_SEGMENT_TYPE Type;

		// Token: 0x04001BC0 RID: 7104
		[FieldOffset(4)]
		internal MILCoreSegFlags Flags;

		// Token: 0x04001BC1 RID: 7105
		[FieldOffset(8)]
		internal uint BackSize;

		// Token: 0x04001BC2 RID: 7106
		[FieldOffset(12)]
		internal uint Count;
	}
}
