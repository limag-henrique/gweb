using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003DF RID: 991
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_VIDEO_ANIMATE
	{
		// Token: 0x060026E6 RID: 9958 RVA: 0x0009B50C File Offset: 0x0009A90C
		public MILCMD_DRAW_VIDEO_ANIMATE(uint hPlayer, Rect rectangle, uint hRectangleAnimations)
		{
			this.hPlayer = hPlayer;
			this.rectangle = rectangle;
			this.hRectangleAnimations = hRectangleAnimations;
		}

		// Token: 0x04001226 RID: 4646
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x04001227 RID: 4647
		[FieldOffset(32)]
		public uint hPlayer;

		// Token: 0x04001228 RID: 4648
		[FieldOffset(36)]
		public uint hRectangleAnimations;
	}
}
