using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D6 RID: 982
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE
	{
		// Token: 0x060026DD RID: 9949 RVA: 0x0009B360 File Offset: 0x0009A760
		public MILCMD_DRAW_ROUNDED_RECTANGLE_ANIMATE(uint hBrush, uint hPen, Rect rectangle, uint hRectangleAnimations, double radiusX, uint hRadiusXAnimations, double radiusY, uint hRadiusYAnimations)
		{
			this.hBrush = hBrush;
			this.hPen = hPen;
			this.rectangle = rectangle;
			this.hRectangleAnimations = hRectangleAnimations;
			this.radiusX = radiusX;
			this.hRadiusXAnimations = hRadiusXAnimations;
			this.radiusY = radiusY;
			this.hRadiusYAnimations = hRadiusYAnimations;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x040011FE RID: 4606
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x040011FF RID: 4607
		[FieldOffset(32)]
		public double radiusX;

		// Token: 0x04001200 RID: 4608
		[FieldOffset(40)]
		public double radiusY;

		// Token: 0x04001201 RID: 4609
		[FieldOffset(48)]
		public uint hBrush;

		// Token: 0x04001202 RID: 4610
		[FieldOffset(52)]
		public uint hPen;

		// Token: 0x04001203 RID: 4611
		[FieldOffset(56)]
		public uint hRectangleAnimations;

		// Token: 0x04001204 RID: 4612
		[FieldOffset(60)]
		public uint hRadiusXAnimations;

		// Token: 0x04001205 RID: 4613
		[FieldOffset(64)]
		public uint hRadiusYAnimations;

		// Token: 0x04001206 RID: 4614
		[FieldOffset(68)]
		private uint QuadWordPad0;
	}
}
