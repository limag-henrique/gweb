using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D9 RID: 985
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_GEOMETRY
	{
		// Token: 0x060026E0 RID: 9952 RVA: 0x0009B43C File Offset: 0x0009A83C
		public MILCMD_DRAW_GEOMETRY(uint hBrush, uint hPen, uint hGeometry)
		{
			this.hBrush = hBrush;
			this.hPen = hPen;
			this.hGeometry = hGeometry;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x04001215 RID: 4629
		[FieldOffset(0)]
		public uint hBrush;

		// Token: 0x04001216 RID: 4630
		[FieldOffset(4)]
		public uint hPen;

		// Token: 0x04001217 RID: 4631
		[FieldOffset(8)]
		public uint hGeometry;

		// Token: 0x04001218 RID: 4632
		[FieldOffset(12)]
		private uint QuadWordPad0;
	}
}
