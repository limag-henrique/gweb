using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003DC RID: 988
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_GLYPH_RUN
	{
		// Token: 0x060026E3 RID: 9955 RVA: 0x0009B4B0 File Offset: 0x0009A8B0
		public MILCMD_DRAW_GLYPH_RUN(uint hForegroundBrush, uint hGlyphRun)
		{
			this.hForegroundBrush = hForegroundBrush;
			this.hGlyphRun = hGlyphRun;
		}

		// Token: 0x0400121F RID: 4639
		[FieldOffset(0)]
		public uint hForegroundBrush;

		// Token: 0x04001220 RID: 4640
		[FieldOffset(4)]
		public uint hGlyphRun;
	}
}
