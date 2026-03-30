using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D2 RID: 978
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_LINE_ANIMATE
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x0009B298 File Offset: 0x0009A698
		public MILCMD_DRAW_LINE_ANIMATE(uint hPen, Point point0, uint hPoint0Animations, Point point1, uint hPoint1Animations)
		{
			this.hPen = hPen;
			this.point0 = point0;
			this.hPoint0Animations = hPoint0Animations;
			this.point1 = point1;
			this.hPoint1Animations = hPoint1Animations;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x040011EB RID: 4587
		[FieldOffset(0)]
		public Point point0;

		// Token: 0x040011EC RID: 4588
		[FieldOffset(16)]
		public Point point1;

		// Token: 0x040011ED RID: 4589
		[FieldOffset(32)]
		public uint hPen;

		// Token: 0x040011EE RID: 4590
		[FieldOffset(36)]
		public uint hPoint0Animations;

		// Token: 0x040011EF RID: 4591
		[FieldOffset(40)]
		public uint hPoint1Animations;

		// Token: 0x040011F0 RID: 4592
		[FieldOffset(44)]
		private uint QuadWordPad0;
	}
}
