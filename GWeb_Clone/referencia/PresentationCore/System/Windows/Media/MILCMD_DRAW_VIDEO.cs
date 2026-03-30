using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003DE RID: 990
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_VIDEO
	{
		// Token: 0x060026E5 RID: 9957 RVA: 0x0009B4E8 File Offset: 0x0009A8E8
		public MILCMD_DRAW_VIDEO(uint hPlayer, Rect rectangle)
		{
			this.hPlayer = hPlayer;
			this.rectangle = rectangle;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x04001223 RID: 4643
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x04001224 RID: 4644
		[FieldOffset(32)]
		public uint hPlayer;

		// Token: 0x04001225 RID: 4645
		[FieldOffset(36)]
		private uint QuadWordPad0;
	}
}
