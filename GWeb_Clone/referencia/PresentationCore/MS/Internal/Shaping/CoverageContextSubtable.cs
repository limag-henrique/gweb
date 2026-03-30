using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B3 RID: 1715
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct CoverageContextSubtable
	{
		// Token: 0x06004B01 RID: 19201 RVA: 0x00124A6C File Offset: 0x00123E6C
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x00124A88 File Offset: 0x00123E88
		private ushort GlyphCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x00124AA4 File Offset: 0x00123EA4
		private ushort SubstCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 4);
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00124AC0 File Offset: 0x00123EC0
		private CoverageTable InputCoverage(FontTable Table, ushort index)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 6 + (int)(index * 2)));
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00124AEC File Offset: 0x00123EEC
		public ContextualLookupRecords ContextualLookups(FontTable Table)
		{
			return new ContextualLookupRecords(this.offset + 6 + (int)(this.GlyphCount(Table) * 2), this.SubstCount(Table));
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00124B18 File Offset: 0x00123F18
		public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
		{
			Invariant.Assert(this.Format(Table) == 3);
			NextGlyph = FirstGlyph + 1;
			bool flag = true;
			int num = (int)this.GlyphCount(Table);
			int num2 = FirstGlyph;
			ushort num3 = 0;
			while ((int)num3 < num && flag)
			{
				if (num2 >= AfterLastGlyph || this.InputCoverage(Table, num3).GetGlyphIndex(Table, GlyphInfo.Glyphs[num2]) < 0)
				{
					flag = false;
				}
				else
				{
					num2 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num2 + 1, LookupFlags, 1);
				}
				num3 += 1;
			}
			if (flag)
			{
				this.ContextualLookups(Table).ApplyContextualLookups(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, num2, Parameter, nestingLevel, out NextGlyph);
			}
			return flag;
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00124BC4 File Offset: 0x00123FC4
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return true;
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00124BD4 File Offset: 0x00123FD4
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			if (this.GlyphCount(table) > 0)
			{
				return this.InputCoverage(table, 0);
			}
			return CoverageTable.InvalidCoverage;
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00124BFC File Offset: 0x00123FFC
		public CoverageContextSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FDA RID: 8154
		private const int offsetFormat = 0;

		// Token: 0x04001FDB RID: 8155
		private const int offsetGlyphCount = 2;

		// Token: 0x04001FDC RID: 8156
		private const int offsetSubstCount = 4;

		// Token: 0x04001FDD RID: 8157
		private const int offsetInputCoverage = 6;

		// Token: 0x04001FDE RID: 8158
		private const int sizeOffset = 2;

		// Token: 0x04001FDF RID: 8159
		private int offset;
	}
}
