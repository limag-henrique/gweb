using System;
using System.Runtime.InteropServices;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000008 RID: 8
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct GlyphMetrics
	{
		// Token: 0x04000303 RID: 771
		[FieldOffset(0)]
		public int LeftSideBearing;

		// Token: 0x04000304 RID: 772
		[FieldOffset(4)]
		public uint AdvanceWidth;

		// Token: 0x04000305 RID: 773
		[FieldOffset(8)]
		public int RightSideBearing;

		// Token: 0x04000306 RID: 774
		[FieldOffset(12)]
		public int TopSideBearing;

		// Token: 0x04000307 RID: 775
		[FieldOffset(16)]
		public uint AdvanceHeight;

		// Token: 0x04000308 RID: 776
		[FieldOffset(20)]
		public int BottomSideBearing;

		// Token: 0x04000309 RID: 777
		[FieldOffset(24)]
		public int VerticalOriginY;
	}
}
