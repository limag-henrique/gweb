using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000737 RID: 1847
	internal struct LSPOINT
	{
		// Token: 0x06004E11 RID: 19985 RVA: 0x00133694 File Offset: 0x00132A94
		public LSPOINT(int horizontalPosition, int verticalPosition)
		{
			this.x = horizontalPosition;
			this.y = verticalPosition;
		}

		// Token: 0x04002270 RID: 8816
		public int x;

		// Token: 0x04002271 RID: 8817
		public int y;
	}
}
