using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D5 RID: 981
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_ROUNDED_RECTANGLE
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x0009B32C File Offset: 0x0009A72C
		public MILCMD_DRAW_ROUNDED_RECTANGLE(uint hBrush, uint hPen, Rect rectangle, double radiusX, double radiusY)
		{
			this.hBrush = hBrush;
			this.hPen = hPen;
			this.rectangle = rectangle;
			this.radiusX = radiusX;
			this.radiusY = radiusY;
		}

		// Token: 0x040011F9 RID: 4601
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x040011FA RID: 4602
		[FieldOffset(32)]
		public double radiusX;

		// Token: 0x040011FB RID: 4603
		[FieldOffset(40)]
		public double radiusY;

		// Token: 0x040011FC RID: 4604
		[FieldOffset(48)]
		public uint hBrush;

		// Token: 0x040011FD RID: 4605
		[FieldOffset(52)]
		public uint hPen;
	}
}
