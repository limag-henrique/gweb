using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D7 RID: 983
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_ELLIPSE
	{
		// Token: 0x060026DE RID: 9950 RVA: 0x0009B3B4 File Offset: 0x0009A7B4
		public MILCMD_DRAW_ELLIPSE(uint hBrush, uint hPen, Point center, double radiusX, double radiusY)
		{
			this.hBrush = hBrush;
			this.hPen = hPen;
			this.center = center;
			this.radiusX = radiusX;
			this.radiusY = radiusY;
		}

		// Token: 0x04001207 RID: 4615
		[FieldOffset(0)]
		public Point center;

		// Token: 0x04001208 RID: 4616
		[FieldOffset(16)]
		public double radiusX;

		// Token: 0x04001209 RID: 4617
		[FieldOffset(24)]
		public double radiusY;

		// Token: 0x0400120A RID: 4618
		[FieldOffset(32)]
		public uint hBrush;

		// Token: 0x0400120B RID: 4619
		[FieldOffset(36)]
		public uint hPen;
	}
}
