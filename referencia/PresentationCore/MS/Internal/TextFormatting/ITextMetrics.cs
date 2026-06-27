using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000709 RID: 1801
	internal interface ITextMetrics
	{
		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06004D91 RID: 19857
		int Length { get; }

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06004D92 RID: 19858
		int DependentLength { get; }

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06004D93 RID: 19859
		int NewlineLength { get; }

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06004D94 RID: 19860
		double Start { get; }

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06004D95 RID: 19861
		double Width { get; }

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06004D96 RID: 19862
		double WidthIncludingTrailingWhitespace { get; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06004D97 RID: 19863
		double Height { get; }

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06004D98 RID: 19864
		double MarkerHeight { get; }

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06004D99 RID: 19865
		double Baseline { get; }

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06004D9A RID: 19866
		double MarkerBaseline { get; }
	}
}
