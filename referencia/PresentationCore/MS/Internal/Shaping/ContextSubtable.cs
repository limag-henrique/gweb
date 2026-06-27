using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B4 RID: 1716
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ContextSubtable
	{
		// Token: 0x06004B0A RID: 19210 RVA: 0x00124C10 File Offset: 0x00124010
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00124C2C File Offset: 0x0012402C
		public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
		{
			switch (this.Format(Table))
			{
			case 1:
			{
				GlyphContextSubtable glyphContextSubtable = new GlyphContextSubtable(this.offset);
				return glyphContextSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
			}
			case 2:
			{
				ClassContextSubtable classContextSubtable = new ClassContextSubtable(this.offset);
				return classContextSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
			}
			case 3:
			{
				CoverageContextSubtable coverageContextSubtable = new CoverageContextSubtable(this.offset);
				return coverageContextSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
			}
			default:
				NextGlyph = FirstGlyph + 1;
				return false;
			}
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00124CF4 File Offset: 0x001240F4
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			switch (this.Format(table))
			{
			case 1:
			{
				GlyphContextSubtable glyphContextSubtable = new GlyphContextSubtable(this.offset);
				return glyphContextSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
			}
			case 2:
			{
				ClassContextSubtable classContextSubtable = new ClassContextSubtable(this.offset);
				return classContextSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
			}
			case 3:
			{
				CoverageContextSubtable coverageContextSubtable = new CoverageContextSubtable(this.offset);
				return coverageContextSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
			}
			default:
				return true;
			}
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00124D70 File Offset: 0x00124170
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			switch (this.Format(table))
			{
			case 1:
			{
				GlyphContextSubtable glyphContextSubtable = new GlyphContextSubtable(this.offset);
				return glyphContextSubtable.GetPrimaryCoverage(table);
			}
			case 2:
			{
				ClassContextSubtable classContextSubtable = new ClassContextSubtable(this.offset);
				return classContextSubtable.GetPrimaryCoverage(table);
			}
			case 3:
			{
				CoverageContextSubtable coverageContextSubtable = new CoverageContextSubtable(this.offset);
				return coverageContextSubtable.GetPrimaryCoverage(table);
			}
			default:
				return CoverageTable.InvalidCoverage;
			}
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00124DE4 File Offset: 0x001241E4
		public ContextSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FE0 RID: 8160
		private const int offsetFormat = 0;

		// Token: 0x04001FE1 RID: 8161
		private int offset;
	}
}
