using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006BB RID: 1723
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct GPOSHeader
	{
		// Token: 0x06004B34 RID: 19252 RVA: 0x0012629C File Offset: 0x0012569C
		public ScriptList GetScriptList(FontTable Table)
		{
			return new ScriptList(this.offset + (int)Table.GetOffset(this.offset + 4));
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x001262C4 File Offset: 0x001256C4
		public FeatureList GetFeatureList(FontTable Table)
		{
			return new FeatureList(this.offset + (int)Table.GetOffset(this.offset + 6));
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x001262EC File Offset: 0x001256EC
		public LookupList GetLookupList(FontTable Table)
		{
			return new LookupList(this.offset + (int)Table.GetOffset(this.offset + 8));
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x00126314 File Offset: 0x00125714
		public GPOSHeader(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x0400200B RID: 8203
		private const int offsetScriptList = 4;

		// Token: 0x0400200C RID: 8204
		private const int offsetFeatureList = 6;

		// Token: 0x0400200D RID: 8205
		private const int offsetLookupList = 8;

		// Token: 0x0400200E RID: 8206
		private int offset;
	}
}
