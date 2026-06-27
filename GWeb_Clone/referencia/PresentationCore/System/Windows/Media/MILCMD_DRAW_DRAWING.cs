using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003DD RID: 989
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_DRAWING
	{
		// Token: 0x060026E4 RID: 9956 RVA: 0x0009B4CC File Offset: 0x0009A8CC
		public MILCMD_DRAW_DRAWING(uint hDrawing)
		{
			this.hDrawing = hDrawing;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x04001221 RID: 4641
		[FieldOffset(0)]
		public uint hDrawing;

		// Token: 0x04001222 RID: 4642
		[FieldOffset(4)]
		private uint QuadWordPad0;
	}
}
