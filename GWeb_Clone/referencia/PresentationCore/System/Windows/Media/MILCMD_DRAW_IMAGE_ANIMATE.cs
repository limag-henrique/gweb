using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003DB RID: 987
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_IMAGE_ANIMATE
	{
		// Token: 0x060026E2 RID: 9954 RVA: 0x0009B48C File Offset: 0x0009A88C
		public MILCMD_DRAW_IMAGE_ANIMATE(uint hImageSource, Rect rectangle, uint hRectangleAnimations)
		{
			this.hImageSource = hImageSource;
			this.rectangle = rectangle;
			this.hRectangleAnimations = hRectangleAnimations;
		}

		// Token: 0x0400121C RID: 4636
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x0400121D RID: 4637
		[FieldOffset(32)]
		public uint hImageSource;

		// Token: 0x0400121E RID: 4638
		[FieldOffset(36)]
		public uint hRectangleAnimations;
	}
}
