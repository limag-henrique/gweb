using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C0 RID: 1728
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct FeatureList
	{
		// Token: 0x06004B4E RID: 19278 RVA: 0x001266D0 File Offset: 0x00125AD0
		public ushort FeatureCount(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x001266EC File Offset: 0x00125AEC
		public uint FeatureTag(FontTable Table, ushort Index)
		{
			return Table.GetUInt(this.offset + 2 + (int)(Index * 6));
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x0012670C File Offset: 0x00125B0C
		public FeatureTable FeatureTable(FontTable Table, ushort Index)
		{
			return new FeatureTable(this.offset + (int)Table.GetUShort(this.offset + 2 + (int)(Index * 6) + 4));
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x0012673C File Offset: 0x00125B3C
		public FeatureList(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002026 RID: 8230
		private const int offsetFeatureCount = 0;

		// Token: 0x04002027 RID: 8231
		private const int offsetFeatureRecordArray = 2;

		// Token: 0x04002028 RID: 8232
		private const int sizeFeatureRecord = 6;

		// Token: 0x04002029 RID: 8233
		private const int offsetFeatureRecordTag = 0;

		// Token: 0x0400202A RID: 8234
		private const int offsetFeatureRecordOffset = 4;

		// Token: 0x0400202B RID: 8235
		private int offset;
	}
}
