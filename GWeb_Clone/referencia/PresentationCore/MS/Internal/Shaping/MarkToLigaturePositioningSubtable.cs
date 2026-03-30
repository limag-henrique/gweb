using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006DF RID: 1759
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct MarkToLigaturePositioningSubtable
	{
		// Token: 0x06004C00 RID: 19456 RVA: 0x0012999C File Offset: 0x00128D9C
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x001299B8 File Offset: 0x00128DB8
		private CoverageTable MarkCoverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x001299E0 File Offset: 0x00128DE0
		private CoverageTable LigatureCoverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 4));
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x00129A08 File Offset: 0x00128E08
		private ushort ClassCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 6);
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x00129A24 File Offset: 0x00128E24
		private MarkArray Marks(FontTable Table)
		{
			return new MarkArray(this.offset + (int)Table.GetUShort(this.offset + 8));
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00129A4C File Offset: 0x00128E4C
		private LigatureAttachTable Ligatures(FontTable Table, int Index, ushort ClassCount)
		{
			int num = this.offset + (int)Table.GetUShort(this.offset + 10);
			return new LigatureAttachTable(num + (int)Table.GetUShort(num + 2 + Index * 2), ClassCount);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00129A88 File Offset: 0x00128E88
		private void FindBaseLigature(int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int markGlyph, out ushort component, out int ligatureGlyph)
		{
			int num = 0;
			ligatureGlyph = -1;
			component = 0;
			bool flag = false;
			int num2 = (int)GlyphInfo.FirstChars[markGlyph];
			while (num2 >= 0 && !flag)
			{
				ushort num3 = Charmap[num2];
				if ((GlyphInfo.GlyphFlags[(int)num3] & 7) != 3)
				{
					num = num2;
					ligatureGlyph = (int)num3;
					flag = true;
				}
				num2--;
			}
			if (!flag)
			{
				return;
			}
			ushort num4 = 0;
			ushort num5 = GlyphInfo.FirstChars[ligatureGlyph];
			while ((int)num5 < CharCount && (int)num5 != num)
			{
				if ((int)Charmap[(int)num5] == ligatureGlyph)
				{
					num4 += 1;
				}
				num5 += 1;
			}
			component = num4;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x00129B1C File Offset: 0x00128F1C
		public unsafe bool Apply(IOpenTypeFont Font, FontTable Table, LayoutMetrics Metrics, GlyphInfoList GlyphInfo, ushort LookupFlags, int CharCount, UshortList Charmap, int* Advances, LayoutOffset* Offsets, int FirstGlyph, int AfterLastGlyph, out int NextGlyph)
		{
			Invariant.Assert(FirstGlyph >= 0);
			Invariant.Assert(AfterLastGlyph <= GlyphInfo.Length);
			NextGlyph = FirstGlyph + 1;
			if (this.Format(Table) != 1)
			{
				return false;
			}
			int length = GlyphInfo.Length;
			if ((GlyphInfo.GlyphFlags[FirstGlyph] & 7) != 3)
			{
				return false;
			}
			int glyphIndex = this.MarkCoverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[FirstGlyph]);
			if (glyphIndex == -1)
			{
				return false;
			}
			ushort component;
			int num;
			this.FindBaseLigature(CharCount, Charmap, GlyphInfo, FirstGlyph, out component, out num);
			if (num < 0)
			{
				return false;
			}
			int glyphIndex2 = this.LigatureCoverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[num]);
			if (glyphIndex2 == -1)
			{
				return false;
			}
			ushort num2 = this.ClassCount(Table);
			MarkArray markArray = this.Marks(Table);
			ushort num3 = markArray.Class(Table, (ushort)glyphIndex);
			if (num3 >= num2)
			{
				return false;
			}
			AnchorTable staticAnchor = this.Ligatures(Table, glyphIndex2, num2).LigatureAnchor(Table, component, num3);
			if (staticAnchor.IsNull())
			{
				return false;
			}
			AnchorTable mobileAnchor = markArray.MarkAnchor(Table, (ushort)glyphIndex);
			if (mobileAnchor.IsNull())
			{
				return false;
			}
			Positioning.AlignAnchors(Font, Table, Metrics, GlyphInfo, Advances, Offsets, num, FirstGlyph, staticAnchor, mobileAnchor, false);
			return true;
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x00129C58 File Offset: 0x00129058
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return false;
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x00129C68 File Offset: 0x00129068
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.MarkCoverage(table);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x00129C7C File Offset: 0x0012907C
		public MarkToLigaturePositioningSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x040020F6 RID: 8438
		private const int offsetFormat = 0;

		// Token: 0x040020F7 RID: 8439
		private const int offsetMarkCoverage = 2;

		// Token: 0x040020F8 RID: 8440
		private const int offsetLigatureCoverage = 4;

		// Token: 0x040020F9 RID: 8441
		private const int offsetClassCount = 6;

		// Token: 0x040020FA RID: 8442
		private const int offsetMarkArray = 8;

		// Token: 0x040020FB RID: 8443
		private const int offsetLigatureArray = 10;

		// Token: 0x040020FC RID: 8444
		private const int offsetLigatureAttachArray = 2;

		// Token: 0x040020FD RID: 8445
		private const int sizeOffset = 2;

		// Token: 0x040020FE RID: 8446
		private int offset;
	}
}
