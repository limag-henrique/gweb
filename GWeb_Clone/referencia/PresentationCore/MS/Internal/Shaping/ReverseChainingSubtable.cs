using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B5 RID: 1717
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ReverseChainingSubtable
	{
		// Token: 0x06004B0F RID: 19215 RVA: 0x00124DF8 File Offset: 0x001241F8
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x00124E14 File Offset: 0x00124214
		private CoverageTable InputCoverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00124E3C File Offset: 0x0012423C
		private CoverageTable Coverage(FontTable Table, int Offset)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(Offset));
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00124E5C File Offset: 0x0012425C
		private ushort GlyphCount(FontTable Table, int Offset)
		{
			return Table.GetUShort(Offset);
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x00124E70 File Offset: 0x00124270
		private static ushort Glyph(FontTable Table, int Offset)
		{
			return Table.GetUShort(Offset);
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x00124E84 File Offset: 0x00124284
		public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, out int NextGlyph)
		{
			NextGlyph = AfterLastGlyph - 1;
			if (this.Format(Table) != 1)
			{
				return false;
			}
			bool flag = true;
			int num = AfterLastGlyph - 1;
			int glyphIndex = this.InputCoverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[num]);
			if (glyphIndex < 0)
			{
				return false;
			}
			int num2 = this.offset + 4;
			ushort num3 = this.GlyphCount(Table, num2);
			num2 += 2;
			int num4 = num;
			ushort num5 = 0;
			while (num5 < num3 && flag)
			{
				num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 - 1, LookupFlags, -1);
				if (num4 < 0)
				{
					flag = false;
				}
				else
				{
					flag = (this.Coverage(Table, num2).GetGlyphIndex(Table, GlyphInfo.Glyphs[num4]) >= 0);
					num2 += 2;
				}
				num5 += 1;
			}
			ushort num6 = this.GlyphCount(Table, num2);
			num2 += 2;
			num4 = num;
			ushort num7 = 0;
			while (num7 < num6 && flag)
			{
				num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 + 1, LookupFlags, 1);
				if (num4 >= GlyphInfo.Length)
				{
					flag = false;
				}
				else
				{
					flag = (this.Coverage(Table, num2).GetGlyphIndex(Table, GlyphInfo.Glyphs[num4]) >= 0);
					num2 += 2;
				}
				num7 += 1;
			}
			if (flag)
			{
				num2 += 2 + 2 * glyphIndex;
				GlyphInfo.Glyphs[num] = ReverseChainingSubtable.Glyph(Table, num2);
				GlyphInfo.GlyphFlags[num] = 23;
			}
			return flag;
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x00124FE0 File Offset: 0x001243E0
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return true;
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x00124FF0 File Offset: 0x001243F0
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.InputCoverage(table);
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x00125004 File Offset: 0x00124404
		public ReverseChainingSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FE2 RID: 8162
		private const int offsetFormat = 0;

		// Token: 0x04001FE3 RID: 8163
		private const int offsetCoverage = 2;

		// Token: 0x04001FE4 RID: 8164
		private const int offsetBacktrackGlyphCount = 4;

		// Token: 0x04001FE5 RID: 8165
		private const int sizeCount = 2;

		// Token: 0x04001FE6 RID: 8166
		private const int sizeOffset = 2;

		// Token: 0x04001FE7 RID: 8167
		private const int sizeGlyphId = 2;

		// Token: 0x04001FE8 RID: 8168
		private int offset;
	}
}
