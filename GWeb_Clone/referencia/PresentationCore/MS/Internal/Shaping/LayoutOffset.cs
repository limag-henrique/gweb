using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C7 RID: 1735
	internal struct LayoutOffset
	{
		// Token: 0x06004B79 RID: 19321 RVA: 0x00126DD4 File Offset: 0x001261D4
		public LayoutOffset(int dx, int dy)
		{
			this.dx = dx;
			this.dy = dy;
		}

		// Token: 0x04002058 RID: 8280
		public int dx;

		// Token: 0x04002059 RID: 8281
		public int dy;
	}
}
