using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B0 RID: 1712
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ChainingSubtable
	{
		// Token: 0x06004AEC RID: 19180 RVA: 0x00124560 File Offset: 0x00123960
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x0012457C File Offset: 0x0012397C
		public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
		{
			NextGlyph = FirstGlyph + 1;
			switch (this.Format(Table))
			{
			case 1:
			{
				GlyphChainingSubtable glyphChainingSubtable = new GlyphChainingSubtable(this.offset);
				return glyphChainingSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
			}
			case 2:
			{
				ClassChainingSubtable classChainingSubtable = new ClassChainingSubtable(this.offset);
				return classChainingSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
			}
			case 3:
			{
				CoverageChainingSubtable coverageChainingSubtable = new CoverageChainingSubtable(Table, this.offset);
				return coverageChainingSubtable.Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
			}
			default:
				return false;
			}
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00124644 File Offset: 0x00123A44
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			switch (this.Format(table))
			{
			case 1:
			{
				GlyphChainingSubtable glyphChainingSubtable = new GlyphChainingSubtable(this.offset);
				return glyphChainingSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
			}
			case 2:
			{
				ClassChainingSubtable classChainingSubtable = new ClassChainingSubtable(this.offset);
				return classChainingSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
			}
			case 3:
			{
				CoverageChainingSubtable coverageChainingSubtable = new CoverageChainingSubtable(table, this.offset);
				return coverageChainingSubtable.IsLookupCovered(table, glyphBits, minGlyphId, maxGlyphId);
			}
			default:
				return true;
			}
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x001246C0 File Offset: 0x00123AC0
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			switch (this.Format(table))
			{
			case 1:
			{
				GlyphChainingSubtable glyphChainingSubtable = new GlyphChainingSubtable(this.offset);
				return glyphChainingSubtable.GetPrimaryCoverage(table);
			}
			case 2:
			{
				ClassChainingSubtable classChainingSubtable = new ClassChainingSubtable(this.offset);
				return classChainingSubtable.GetPrimaryCoverage(table);
			}
			case 3:
			{
				CoverageChainingSubtable coverageChainingSubtable = new CoverageChainingSubtable(table, this.offset);
				return coverageChainingSubtable.GetPrimaryCoverage(table);
			}
			default:
				return CoverageTable.InvalidCoverage;
			}
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00124734 File Offset: 0x00123B34
		public ChainingSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FCB RID: 8139
		private const int offsetFormat = 0;

		// Token: 0x04001FCC RID: 8140
		private int offset;
	}
}
