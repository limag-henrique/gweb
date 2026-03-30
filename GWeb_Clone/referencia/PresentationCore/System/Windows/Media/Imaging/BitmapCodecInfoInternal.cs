using System;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005D0 RID: 1488
	internal class BitmapCodecInfoInternal : BitmapCodecInfo
	{
		// Token: 0x0600435D RID: 17245 RVA: 0x00105250 File Offset: 0x00104650
		private BitmapCodecInfoInternal()
		{
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x00105264 File Offset: 0x00104664
		internal BitmapCodecInfoInternal(SafeMILHandle codecInfoHandle) : base(codecInfoHandle)
		{
		}
	}
}
