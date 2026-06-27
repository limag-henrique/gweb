using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200062A RID: 1578
	internal enum MilCompositingMode
	{
		// Token: 0x04001AAB RID: 6827
		SourceOver,
		// Token: 0x04001AAC RID: 6828
		SourceCopy,
		// Token: 0x04001AAD RID: 6829
		SourceAdd,
		// Token: 0x04001AAE RID: 6830
		SourceAlphaMultiply,
		// Token: 0x04001AAF RID: 6831
		SourceInverseAlphaMultiply,
		// Token: 0x04001AB0 RID: 6832
		SourceUnder,
		// Token: 0x04001AB1 RID: 6833
		SourceOverNonPremultiplied,
		// Token: 0x04001AB2 RID: 6834
		SourceInverseAlphaOverNonPremultiplied,
		// Token: 0x04001AB3 RID: 6835
		DestInvert,
		// Token: 0x04001AB4 RID: 6836
		Last,
		// Token: 0x04001AB5 RID: 6837
		FORCE_DWORD = -1
	}
}
