using System;

namespace System.Windows.Input
{
	// Token: 0x020002CC RID: 716
	internal struct TabletDeviceSizeInfo
	{
		// Token: 0x0600158E RID: 5518 RVA: 0x0004FCC8 File Offset: 0x0004F0C8
		internal TabletDeviceSizeInfo(Size tabletSize, Size screenSize)
		{
			this.TabletSize = tabletSize;
			this.ScreenSize = screenSize;
		}

		// Token: 0x04000BAC RID: 2988
		public Size TabletSize;

		// Token: 0x04000BAD RID: 2989
		public Size ScreenSize;
	}
}
