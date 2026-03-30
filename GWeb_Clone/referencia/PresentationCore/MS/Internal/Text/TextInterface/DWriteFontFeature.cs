using System;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000023 RID: 35
	internal struct DWriteFontFeature
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0000B88C File Offset: 0x0000AC8C
		public DWriteFontFeature(DWriteFontFeatureTag dwriteNameTag, uint dwriteParameter)
		{
			this.nameTag = dwriteNameTag;
			this.parameter = dwriteParameter;
		}

		// Token: 0x04000338 RID: 824
		public DWriteFontFeatureTag nameTag;

		// Token: 0x04000339 RID: 825
		public uint parameter;
	}
}
