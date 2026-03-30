using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E8 RID: 1768
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct AlternateSubstitutionSubtable
	{
		// Token: 0x06004C46 RID: 19526 RVA: 0x0012AC7C File Offset: 0x0012A07C
		public ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0012AC98 File Offset: 0x0012A098
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0012ACC0 File Offset: 0x0012A0C0
		private AlternateSubstitutionSubtable.AlternateSetTable AlternateSet(FontTable Table, int index)
		{
			return new AlternateSubstitutionSubtable.AlternateSetTable(this.offset + (int)Table.GetUShort(this.offset + 6 + index * 2));
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0012ACEC File Offset: 0x0012A0EC
		public bool Apply(FontTable Table, GlyphInfoList GlyphInfo, uint FeatureParam, int FirstGlyph, out int NextGlyph)
		{
			NextGlyph = FirstGlyph + 1;
			if (this.Format(Table) != 1)
			{
				return false;
			}
			int length = GlyphInfo.Length;
			int glyphIndex = this.Coverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[FirstGlyph]);
			if (glyphIndex == -1)
			{
				return false;
			}
			ushort num = this.AlternateSet(Table, glyphIndex).Alternate(Table, FeatureParam);
			if (num != 65535)
			{
				GlyphInfo.Glyphs[FirstGlyph] = num;
				GlyphInfo.GlyphFlags[FirstGlyph] = 23;
				return true;
			}
			return false;
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0012AD74 File Offset: 0x0012A174
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return this.Coverage(table).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId);
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0012AD98 File Offset: 0x0012A198
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0012ADAC File Offset: 0x0012A1AC
		public AlternateSubstitutionSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002122 RID: 8482
		private const int offsetFormat = 0;

		// Token: 0x04002123 RID: 8483
		private const int offsetCoverage = 2;

		// Token: 0x04002124 RID: 8484
		private const int offsetAlternateSetCount = 4;

		// Token: 0x04002125 RID: 8485
		private const int offsetAlternateSets = 6;

		// Token: 0x04002126 RID: 8486
		private const int sizeAlternateSetOffset = 2;

		// Token: 0x04002127 RID: 8487
		private const ushort InvalidAlternateGlyph = 65535;

		// Token: 0x04002128 RID: 8488
		private int offset;

		// Token: 0x020009C5 RID: 2501
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private struct AlternateSetTable
		{
			// Token: 0x06005AEA RID: 23274 RVA: 0x0016CC54 File Offset: 0x0016C054
			public ushort GlyphCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005AEB RID: 23275 RVA: 0x0016CC70 File Offset: 0x0016C070
			public ushort Alternate(FontTable Table, uint FeatureParam)
			{
				Invariant.Assert(FeatureParam > 0U);
				uint num = FeatureParam - 1U;
				if (num >= (uint)this.GlyphCount(Table))
				{
					return ushort.MaxValue;
				}
				return Table.GetUShort(this.offset + 2 + (int)((ushort)num * 2));
			}

			// Token: 0x06005AEC RID: 23276 RVA: 0x0016CCB0 File Offset: 0x0016C0B0
			public AlternateSetTable(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DE8 RID: 11752
			private const int offsetGlyphCount = 0;

			// Token: 0x04002DE9 RID: 11753
			private const int offsetGlyphs = 2;

			// Token: 0x04002DEA RID: 11754
			private const int sizeGlyph = 2;

			// Token: 0x04002DEB RID: 11755
			private int offset;
		}
	}
}
