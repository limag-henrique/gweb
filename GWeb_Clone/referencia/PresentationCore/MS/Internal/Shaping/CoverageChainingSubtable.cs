using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006AF RID: 1711
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct CoverageChainingSubtable
	{
		// Token: 0x06004AE0 RID: 19168 RVA: 0x00124178 File Offset: 0x00123578
		public ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x00124194 File Offset: 0x00123594
		public ushort BacktrackGlyphCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x001241B0 File Offset: 0x001235B0
		public CoverageTable BacktrackCoverage(FontTable Table, ushort Index)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2 + 2 + (int)(Index * 2)));
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x001241E0 File Offset: 0x001235E0
		public ushort InputGlyphCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + this.offsetInputGlyphCount);
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x00124200 File Offset: 0x00123600
		public CoverageTable InputCoverage(FontTable Table, ushort Index)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + this.offsetInputGlyphCount + 2 + (int)(Index * 2)));
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x00124234 File Offset: 0x00123634
		public ushort LookaheadGlyphCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + this.offsetLookaheadGlyphCount);
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x00124254 File Offset: 0x00123654
		public CoverageTable LookaheadCoverage(FontTable Table, ushort Index)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + this.offsetLookaheadGlyphCount + 2 + (int)(Index * 2)));
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x00124288 File Offset: 0x00123688
		public ContextualLookupRecords ContextualLookups(FontTable Table)
		{
			int num = this.offset + this.offsetLookaheadGlyphCount + 2 + (int)(this.LookaheadGlyphCount(Table) * 2);
			return new ContextualLookupRecords(num + 2, Table.GetUShort(num));
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x001242C0 File Offset: 0x001236C0
		public CoverageChainingSubtable(FontTable Table, int Offset)
		{
			this.offset = Offset;
			this.offsetInputGlyphCount = (int)(4 + Table.GetUShort(this.offset + 2) * 2);
			this.offsetLookaheadGlyphCount = this.offsetInputGlyphCount + 2 + (int)(Table.GetUShort(this.offset + this.offsetInputGlyphCount) * 2);
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x00124310 File Offset: 0x00123710
		public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
		{
			Invariant.Assert(this.Format(Table) == 3);
			NextGlyph = FirstGlyph + 1;
			int length = GlyphInfo.Length;
			ushort num = this.BacktrackGlyphCount(Table);
			ushort num2 = this.InputGlyphCount(Table);
			ushort num3 = this.LookaheadGlyphCount(Table);
			if (FirstGlyph < (int)num || FirstGlyph + (int)num2 > AfterLastGlyph)
			{
				return false;
			}
			bool flag = true;
			int num4 = FirstGlyph;
			ushort num5 = 0;
			while (num5 < num && flag)
			{
				num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 - 1, LookupFlags, -1);
				if (num4 < 0 || this.BacktrackCoverage(Table, num5).GetGlyphIndex(Table, GlyphInfo.Glyphs[num4]) < 0)
				{
					flag = false;
				}
				num5 += 1;
			}
			if (!flag)
			{
				return false;
			}
			num4 = FirstGlyph;
			ushort num6 = 0;
			while (num6 < num2 && flag)
			{
				if (num4 >= AfterLastGlyph || this.InputCoverage(Table, num6).GetGlyphIndex(Table, GlyphInfo.Glyphs[num4]) < 0)
				{
					flag = false;
				}
				else
				{
					num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 + 1, LookupFlags, 1);
				}
				num6 += 1;
			}
			if (!flag)
			{
				return false;
			}
			int afterLastGlyph = num4;
			ushort num7 = 0;
			while (num7 < num3 && flag)
			{
				if (num4 >= GlyphInfo.Length || this.LookaheadCoverage(Table, num7).GetGlyphIndex(Table, GlyphInfo.Glyphs[num4]) < 0)
				{
					flag = false;
				}
				else
				{
					num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 + 1, LookupFlags, 1);
				}
				num7 += 1;
			}
			if (flag)
			{
				this.ContextualLookups(Table).ApplyContextualLookups(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, afterLastGlyph, Parameter, nestingLevel, out NextGlyph);
			}
			return flag;
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x00124498 File Offset: 0x00123898
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			ushort num = this.BacktrackGlyphCount(table);
			ushort num2 = this.InputGlyphCount(table);
			ushort num3 = this.LookaheadGlyphCount(table);
			for (ushort num4 = 0; num4 < num; num4 += 1)
			{
				if (!this.BacktrackCoverage(table, num4).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId))
				{
					return false;
				}
			}
			for (ushort num5 = 0; num5 < num2; num5 += 1)
			{
				if (!this.InputCoverage(table, num5).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId))
				{
					return false;
				}
			}
			for (ushort num6 = 0; num6 < num3; num6 += 1)
			{
				if (!this.LookaheadCoverage(table, num6).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x00124538 File Offset: 0x00123938
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			if (this.InputGlyphCount(table) > 0)
			{
				return this.InputCoverage(table, 0);
			}
			return CoverageTable.InvalidCoverage;
		}

		// Token: 0x04001FC3 RID: 8131
		private const int offsetFormat = 0;

		// Token: 0x04001FC4 RID: 8132
		private const int offsetBacktrackGlyphCount = 2;

		// Token: 0x04001FC5 RID: 8133
		private const int offsetBacktrackCoverageArray = 4;

		// Token: 0x04001FC6 RID: 8134
		private const int sizeGlyphCount = 2;

		// Token: 0x04001FC7 RID: 8135
		private const int sizeCoverageOffset = 2;

		// Token: 0x04001FC8 RID: 8136
		private int offset;

		// Token: 0x04001FC9 RID: 8137
		private int offsetInputGlyphCount;

		// Token: 0x04001FCA RID: 8138
		private int offsetLookaheadGlyphCount;
	}
}
