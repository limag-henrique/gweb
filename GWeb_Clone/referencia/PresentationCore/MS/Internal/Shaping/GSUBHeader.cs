using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006BA RID: 1722
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct GSUBHeader
	{
		// Token: 0x06004B30 RID: 19248 RVA: 0x00126210 File Offset: 0x00125610
		public ScriptList GetScriptList(FontTable Table)
		{
			return new ScriptList(this.offset + (int)Table.GetOffset(this.offset + 4));
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00126238 File Offset: 0x00125638
		public FeatureList GetFeatureList(FontTable Table)
		{
			return new FeatureList(this.offset + (int)Table.GetOffset(this.offset + 6));
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00126260 File Offset: 0x00125660
		public LookupList GetLookupList(FontTable Table)
		{
			return new LookupList(this.offset + (int)Table.GetOffset(this.offset + 8));
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x00126288 File Offset: 0x00125688
		public GSUBHeader(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002007 RID: 8199
		private const int offsetScriptList = 4;

		// Token: 0x04002008 RID: 8200
		private const int offsetFeatureList = 6;

		// Token: 0x04002009 RID: 8201
		private const int offsetLookupList = 8;

		// Token: 0x0400200A RID: 8202
		private int offset;
	}
}
