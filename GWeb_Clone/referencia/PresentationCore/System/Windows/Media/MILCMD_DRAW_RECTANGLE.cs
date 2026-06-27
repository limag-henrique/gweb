using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D3 RID: 979
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_RECTANGLE
	{
		// Token: 0x060026DA RID: 9946 RVA: 0x0009B2D4 File Offset: 0x0009A6D4
		public MILCMD_DRAW_RECTANGLE(uint hBrush, uint hPen, Rect rectangle)
		{
			this.hBrush = hBrush;
			this.hPen = hPen;
			this.rectangle = rectangle;
		}

		// Token: 0x040011F1 RID: 4593
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x040011F2 RID: 4594
		[FieldOffset(32)]
		public uint hBrush;

		// Token: 0x040011F3 RID: 4595
		[FieldOffset(36)]
		public uint hPen;
	}
}
