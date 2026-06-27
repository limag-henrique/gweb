using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000738 RID: 1848
	internal struct LSRECT
	{
		// Token: 0x06004E12 RID: 19986 RVA: 0x001336B0 File Offset: 0x00132AB0
		internal LSRECT(int x1, int y1, int x2, int y2)
		{
			this.left = x1;
			this.top = y1;
			this.right = x2;
			this.bottom = y2;
		}

		// Token: 0x04002272 RID: 8818
		public int left;

		// Token: 0x04002273 RID: 8819
		public int top;

		// Token: 0x04002274 RID: 8820
		public int right;

		// Token: 0x04002275 RID: 8821
		public int bottom;
	}
}
