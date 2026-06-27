using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D4 RID: 980
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_RECTANGLE_ANIMATE
	{
		// Token: 0x060026DB RID: 9947 RVA: 0x0009B2F8 File Offset: 0x0009A6F8
		public MILCMD_DRAW_RECTANGLE_ANIMATE(uint hBrush, uint hPen, Rect rectangle, uint hRectangleAnimations)
		{
			this.hBrush = hBrush;
			this.hPen = hPen;
			this.rectangle = rectangle;
			this.hRectangleAnimations = hRectangleAnimations;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x040011F4 RID: 4596
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x040011F5 RID: 4597
		[FieldOffset(32)]
		public uint hBrush;

		// Token: 0x040011F6 RID: 4598
		[FieldOffset(36)]
		public uint hPen;

		// Token: 0x040011F7 RID: 4599
		[FieldOffset(40)]
		public uint hRectangleAnimations;

		// Token: 0x040011F8 RID: 4600
		[FieldOffset(44)]
		private uint QuadWordPad0;
	}
}
