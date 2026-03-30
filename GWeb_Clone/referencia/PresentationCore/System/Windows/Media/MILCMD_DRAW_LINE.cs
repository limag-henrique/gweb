using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D1 RID: 977
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_LINE
	{
		// Token: 0x060026D8 RID: 9944 RVA: 0x0009B26C File Offset: 0x0009A66C
		public MILCMD_DRAW_LINE(uint hPen, Point point0, Point point1)
		{
			this.hPen = hPen;
			this.point0 = point0;
			this.point1 = point1;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x040011E7 RID: 4583
		[FieldOffset(0)]
		public Point point0;

		// Token: 0x040011E8 RID: 4584
		[FieldOffset(16)]
		public Point point1;

		// Token: 0x040011E9 RID: 4585
		[FieldOffset(32)]
		public uint hPen;

		// Token: 0x040011EA RID: 4586
		[FieldOffset(36)]
		private uint QuadWordPad0;
	}
}
