using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000627 RID: 1575
	[Flags]
	internal enum MilRenderOptionFlags
	{
		// Token: 0x04001A92 RID: 6802
		BitmapScalingMode = 1,
		// Token: 0x04001A93 RID: 6803
		EdgeMode = 2,
		// Token: 0x04001A94 RID: 6804
		CompositingMode = 4,
		// Token: 0x04001A95 RID: 6805
		ClearTypeHint = 8,
		// Token: 0x04001A96 RID: 6806
		TextRenderingMode = 16,
		// Token: 0x04001A97 RID: 6807
		TextHintingMode = 32,
		// Token: 0x04001A98 RID: 6808
		Last = 33,
		// Token: 0x04001A99 RID: 6809
		FORCE_DWORD = -1
	}
}
