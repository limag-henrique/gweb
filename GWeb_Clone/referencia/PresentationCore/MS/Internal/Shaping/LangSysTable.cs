using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006BF RID: 1727
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct LangSysTable
	{
		// Token: 0x06004B48 RID: 19272 RVA: 0x001265E0 File Offset: 0x001259E0
		public FeatureTable FindFeature(FontTable Table, FeatureList Features, uint FeatureTag)
		{
			ushort num = this.FeatureCount(Table);
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				ushort featureIndex = this.GetFeatureIndex(Table, num2);
				if (Features.FeatureTag(Table, featureIndex) == FeatureTag)
				{
					return Features.FeatureTable(Table, featureIndex);
				}
			}
			return new FeatureTable(int.MaxValue);
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x0012662C File Offset: 0x00125A2C
		public FeatureTable RequiredFeature(FontTable Table, FeatureList Features)
		{
			ushort @ushort = Table.GetUShort(this.offset + 2);
			if (@ushort != 65535)
			{
				return Features.FeatureTable(Table, @ushort);
			}
			return new FeatureTable(int.MaxValue);
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x00126664 File Offset: 0x00125A64
		public ushort FeatureCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 4);
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x00126680 File Offset: 0x00125A80
		public ushort GetFeatureIndex(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 6 + (int)(Index * 2));
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x001266A0 File Offset: 0x00125AA0
		public LangSysTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06004B4D RID: 19277 RVA: 0x001266B4 File Offset: 0x00125AB4
		public bool IsNull
		{
			get
			{
				return this.offset == int.MaxValue;
			}
		}

		// Token: 0x04002021 RID: 8225
		private const int offsetRequiredFeature = 2;

		// Token: 0x04002022 RID: 8226
		private const int offsetFeatureCount = 4;

		// Token: 0x04002023 RID: 8227
		private const int offsetFeatureIndexArray = 6;

		// Token: 0x04002024 RID: 8228
		private const int sizeFeatureIndex = 2;

		// Token: 0x04002025 RID: 8229
		private int offset;
	}
}
