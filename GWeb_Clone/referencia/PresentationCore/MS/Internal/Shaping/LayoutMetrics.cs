using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006CD RID: 1741
	internal struct LayoutMetrics
	{
		// Token: 0x06004B84 RID: 19332 RVA: 0x00126F64 File Offset: 0x00126364
		public LayoutMetrics(TextFlowDirection Direction, ushort DesignEmHeight, ushort PixelsEmWidth, ushort PixelsEmHeight)
		{
			this.Direction = Direction;
			this.DesignEmHeight = DesignEmHeight;
			this.PixelsEmWidth = PixelsEmWidth;
			this.PixelsEmHeight = PixelsEmHeight;
		}

		// Token: 0x04002090 RID: 8336
		public TextFlowDirection Direction;

		// Token: 0x04002091 RID: 8337
		public ushort DesignEmHeight;

		// Token: 0x04002092 RID: 8338
		public ushort PixelsEmWidth;

		// Token: 0x04002093 RID: 8339
		public ushort PixelsEmHeight;
	}
}
