using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E7 RID: 1767
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct MultipleSubstitutionSubtable
	{
		// Token: 0x06004C3F RID: 19519 RVA: 0x0012AA80 File Offset: 0x00129E80
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0012AA9C File Offset: 0x00129E9C
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0012AAC4 File Offset: 0x00129EC4
		private MultipleSubstitutionSequenceTable Sequence(FontTable Table, int Index)
		{
			return new MultipleSubstitutionSequenceTable(this.offset + (int)Table.GetUShort(this.offset + 6 + Index * 2));
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0012AAF0 File Offset: 0x00129EF0
		public bool Apply(IOpenTypeFont Font, FontTable Table, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, out int NextGlyph)
		{
			NextGlyph = FirstGlyph + 1;
			if (this.Format(Table) != 1)
			{
				return false;
			}
			int length = GlyphInfo.Length;
			ushort glyph = GlyphInfo.Glyphs[FirstGlyph];
			int glyphIndex = this.Coverage(Table).GetGlyphIndex(Table, glyph);
			if (glyphIndex == -1)
			{
				return false;
			}
			MultipleSubstitutionSequenceTable multipleSubstitutionSequenceTable = this.Sequence(Table, glyphIndex);
			ushort num = multipleSubstitutionSequenceTable.GlyphCount(Table);
			int num2 = (int)(num - 1);
			if (num == 0)
			{
				GlyphInfo.Remove(FirstGlyph, 1);
			}
			else
			{
				ushort value = GlyphInfo.FirstChars[FirstGlyph];
				ushort value2 = GlyphInfo.LigatureCounts[FirstGlyph];
				if (num2 > 0)
				{
					GlyphInfo.Insert(FirstGlyph, num2);
				}
				for (ushort num3 = 0; num3 < num; num3 += 1)
				{
					GlyphInfo.Glyphs[FirstGlyph + (int)num3] = multipleSubstitutionSequenceTable.Glyph(Table, num3);
					GlyphInfo.GlyphFlags[FirstGlyph + (int)num3] = 23;
					GlyphInfo.FirstChars[FirstGlyph + (int)num3] = value;
					GlyphInfo.LigatureCounts[FirstGlyph + (int)num3] = value2;
				}
			}
			for (int i = 0; i < CharCount; i++)
			{
				if ((int)Charmap[i] > FirstGlyph)
				{
					Charmap[i] = (ushort)((int)Charmap[i] + num2);
				}
			}
			NextGlyph = FirstGlyph + num2 + 1;
			return true;
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0012AC30 File Offset: 0x0012A030
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return this.Coverage(table).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId);
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0012AC54 File Offset: 0x0012A054
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0012AC68 File Offset: 0x0012A068
		public MultipleSubstitutionSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x0400211C RID: 8476
		private const int offsetFormat = 0;

		// Token: 0x0400211D RID: 8477
		private const int offsetCoverage = 2;

		// Token: 0x0400211E RID: 8478
		private const int offsetSequenceCount = 4;

		// Token: 0x0400211F RID: 8479
		private const int offsetSequenceArray = 6;

		// Token: 0x04002120 RID: 8480
		private const int sizeSequenceOffset = 2;

		// Token: 0x04002121 RID: 8481
		private int offset;
	}
}
