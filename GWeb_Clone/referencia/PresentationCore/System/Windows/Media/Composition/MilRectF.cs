using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000636 RID: 1590
	[StructLayout(LayoutKind.Explicit)]
	internal struct MilRectF
	{
		// Token: 0x04001B81 RID: 7041
		[FieldOffset(0)]
		internal float _left;

		// Token: 0x04001B82 RID: 7042
		[FieldOffset(4)]
		internal float _top;

		// Token: 0x04001B83 RID: 7043
		[FieldOffset(8)]
		internal float _right;

		// Token: 0x04001B84 RID: 7044
		[FieldOffset(12)]
		internal float _bottom;
	}
}
