using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003DA RID: 986
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_IMAGE
	{
		// Token: 0x060026E1 RID: 9953 RVA: 0x0009B468 File Offset: 0x0009A868
		public MILCMD_DRAW_IMAGE(uint hImageSource, Rect rectangle)
		{
			this.hImageSource = hImageSource;
			this.rectangle = rectangle;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x04001219 RID: 4633
		[FieldOffset(0)]
		public Rect rectangle;

		// Token: 0x0400121A RID: 4634
		[FieldOffset(32)]
		public uint hImageSource;

		// Token: 0x0400121B RID: 4635
		[FieldOffset(36)]
		private uint QuadWordPad0;
	}
}
