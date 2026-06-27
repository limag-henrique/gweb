using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E4 RID: 1764
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct SingleSubstitutionSubtable
	{
		// Token: 0x06004C2C RID: 19500 RVA: 0x0012A3E4 File Offset: 0x001297E4
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0012A400 File Offset: 0x00129800
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x0012A428 File Offset: 0x00129828
		private short Format1DeltaGlyphId(FontTable Table)
		{
			Invariant.Assert(this.Format(Table) == 1);
			return Table.GetShort(this.offset + 4);
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0012A454 File Offset: 0x00129854
		private ushort Format2SubstituteGlyphId(FontTable Table, ushort Index)
		{
			Invariant.Assert(this.Format(Table) == 2);
			return Table.GetUShort(this.offset + 6 + (int)(Index * 2));
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0012A484 File Offset: 0x00129884
		public bool Apply(FontTable Table, GlyphInfoList GlyphInfo, int FirstGlyph, out int NextGlyph)
		{
			Invariant.Assert(FirstGlyph >= 0);
			NextGlyph = FirstGlyph + 1;
			ushort num = GlyphInfo.Glyphs[FirstGlyph];
			int glyphIndex = this.Coverage(Table).GetGlyphIndex(Table, num);
			if (glyphIndex == -1)
			{
				return false;
			}
			ushort num2 = this.Format(Table);
			if (num2 == 1)
			{
				GlyphInfo.Glyphs[FirstGlyph] = num + (ushort)this.Format1DeltaGlyphId(Table);
				GlyphInfo.GlyphFlags[FirstGlyph] = 23;
				NextGlyph = FirstGlyph + 1;
				return true;
			}
			if (num2 != 2)
			{
				NextGlyph = FirstGlyph + 1;
				return false;
			}
			GlyphInfo.Glyphs[FirstGlyph] = this.Format2SubstituteGlyphId(Table, (ushort)glyphIndex);
			GlyphInfo.GlyphFlags[FirstGlyph] = 23;
			NextGlyph = FirstGlyph + 1;
			return true;
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0012A538 File Offset: 0x00129938
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return this.Coverage(table).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x0012A55C File Offset: 0x0012995C
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0012A570 File Offset: 0x00129970
		public SingleSubstitutionSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x0400210B RID: 8459
		private const int offsetFormat = 0;

		// Token: 0x0400210C RID: 8460
		private const int offsetCoverage = 2;

		// Token: 0x0400210D RID: 8461
		private const int offsetFormat1DeltaGlyphId = 4;

		// Token: 0x0400210E RID: 8462
		private const int offsetFormat2GlyphCount = 4;

		// Token: 0x0400210F RID: 8463
		private const int offsetFormat2SubstitutehArray = 6;

		// Token: 0x04002110 RID: 8464
		private const int sizeFormat2SubstituteSize = 2;

		// Token: 0x04002111 RID: 8465
		private int offset;
	}
}
