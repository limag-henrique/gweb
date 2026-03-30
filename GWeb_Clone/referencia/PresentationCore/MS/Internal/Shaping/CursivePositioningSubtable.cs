using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006DD RID: 1757
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct CursivePositioningSubtable
	{
		// Token: 0x06004BF6 RID: 19446 RVA: 0x0012968C File Offset: 0x00128A8C
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x001296A8 File Offset: 0x00128AA8
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x001296D0 File Offset: 0x00128AD0
		private AnchorTable EntryAnchor(FontTable Table, int Index)
		{
			int @ushort = (int)Table.GetUShort(this.offset + 6 + 4 * Index);
			if (@ushort == 0)
			{
				return new AnchorTable(Table, 0);
			}
			return new AnchorTable(Table, this.offset + @ushort);
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0012970C File Offset: 0x00128B0C
		private AnchorTable ExitAnchor(FontTable Table, int Index)
		{
			int @ushort = (int)Table.GetUShort(this.offset + 6 + 4 * Index + 2);
			if (@ushort == 0)
			{
				return new AnchorTable(Table, 0);
			}
			return new AnchorTable(Table, this.offset + @ushort);
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x00129748 File Offset: 0x00128B48
		public unsafe bool Apply(IOpenTypeFont Font, FontTable Table, LayoutMetrics Metrics, GlyphInfoList GlyphInfo, ushort LookupFlags, int* Advances, LayoutOffset* Offsets, int FirstGlyph, int AfterLastGlyph, out int NextGlyph)
		{
			Invariant.Assert(FirstGlyph >= 0);
			Invariant.Assert(AfterLastGlyph <= GlyphInfo.Length);
			NextGlyph = FirstGlyph + 1;
			if (this.Format(Table) != 1)
			{
				return false;
			}
			bool flag = (LookupFlags & 1) > 0;
			ushort num = 64;
			int nextGlyphInLookup = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, FirstGlyph, LookupFlags, 1);
			if (flag)
			{
				UshortList ushortList = GlyphInfo.GlyphFlags;
				int index = nextGlyphInLookup;
				ushortList[index] &= ~num;
			}
			if (nextGlyphInLookup >= AfterLastGlyph)
			{
				return false;
			}
			int nextGlyphInLookup2 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, FirstGlyph - 1, LookupFlags, -1);
			if (nextGlyphInLookup2 < 0)
			{
				return false;
			}
			CoverageTable coverageTable = this.Coverage(Table);
			int glyphIndex = coverageTable.GetGlyphIndex(Table, GlyphInfo.Glyphs[nextGlyphInLookup]);
			if (glyphIndex == -1)
			{
				return false;
			}
			int glyphIndex2 = coverageTable.GetGlyphIndex(Table, GlyphInfo.Glyphs[nextGlyphInLookup2]);
			if (glyphIndex2 == -1)
			{
				return false;
			}
			AnchorTable staticAnchor = this.ExitAnchor(Table, glyphIndex2);
			if (staticAnchor.IsNull())
			{
				return false;
			}
			AnchorTable mobileAnchor = this.EntryAnchor(Table, glyphIndex);
			if (mobileAnchor.IsNull())
			{
				return false;
			}
			Positioning.AlignAnchors(Font, Table, Metrics, GlyphInfo, Advances, Offsets, nextGlyphInLookup2, nextGlyphInLookup, staticAnchor, mobileAnchor, true);
			if (flag)
			{
				UshortList glyphFlags = GlyphInfo.GlyphFlags;
				int i;
				for (i = nextGlyphInLookup; i > nextGlyphInLookup2; i--)
				{
					UshortList ushortList = glyphFlags;
					int index = i;
					ushortList[index] |= num;
				}
				int dy = Offsets[nextGlyphInLookup].dy;
				i = nextGlyphInLookup;
				while ((glyphFlags[i] & num) != 0)
				{
					Offsets[i].dy -= dy;
					i--;
				}
				Invariant.Assert(nextGlyphInLookup >= 0);
				Offsets[i].dy -= dy;
			}
			return true;
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x00129904 File Offset: 0x00128D04
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return true;
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x00129914 File Offset: 0x00128D14
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x00129928 File Offset: 0x00128D28
		public CursivePositioningSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x040020EA RID: 8426
		private const ushort offsetFormat = 0;

		// Token: 0x040020EB RID: 8427
		private const ushort offsetCoverage = 2;

		// Token: 0x040020EC RID: 8428
		private const ushort offsetEntryExitCount = 4;

		// Token: 0x040020ED RID: 8429
		private const ushort offsetEntryExitArray = 6;

		// Token: 0x040020EE RID: 8430
		private const ushort sizeEntryExitRecord = 4;

		// Token: 0x040020EF RID: 8431
		private const ushort offsetEntryAnchor = 0;

		// Token: 0x040020F0 RID: 8432
		private const ushort offsetExitAnchor = 2;

		// Token: 0x040020F1 RID: 8433
		private int offset;
	}
}
