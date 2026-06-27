using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006DC RID: 1756
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct MarkToMarkPositioningSubtable
	{
		// Token: 0x06004BEC RID: 19436 RVA: 0x0012943C File Offset: 0x0012883C
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00129458 File Offset: 0x00128858
		private CoverageTable Mark1Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00129480 File Offset: 0x00128880
		private CoverageTable Mark2Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 4));
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x001294A8 File Offset: 0x001288A8
		private ushort Mark1ClassCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 6);
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x001294C4 File Offset: 0x001288C4
		private MarkArray Mark1Array(FontTable Table)
		{
			return new MarkArray(this.offset + (int)Table.GetUShort(this.offset + 8));
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x001294EC File Offset: 0x001288EC
		private MarkToMarkPositioningSubtable.Mark2Array Marks2(FontTable Table)
		{
			return new MarkToMarkPositioningSubtable.Mark2Array(this.offset + (int)Table.GetUShort(this.offset + 10));
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x00129514 File Offset: 0x00128914
		public unsafe bool Apply(IOpenTypeFont Font, FontTable Table, LayoutMetrics Metrics, GlyphInfoList GlyphInfo, ushort LookupFlags, int* Advances, LayoutOffset* Offsets, int FirstGlyph, int AfterLastGlyph, out int NextGlyph)
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
			int glyphIndex = this.Mark1Coverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[FirstGlyph]);
			if (glyphIndex == -1)
			{
				return false;
			}
			int nextGlyphInLookup = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, FirstGlyph - 1, LookupFlags & 65280, -1);
			if (nextGlyphInLookup < 0)
			{
				return false;
			}
			int glyphIndex2 = this.Mark2Coverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[nextGlyphInLookup]);
			if (glyphIndex2 == -1)
			{
				return false;
			}
			ushort num = this.Mark1ClassCount(Table);
			MarkArray markArray = this.Mark1Array(Table);
			ushort num2 = markArray.Class(Table, (ushort)glyphIndex);
			if (num2 >= num)
			{
				return false;
			}
			AnchorTable mobileAnchor = markArray.MarkAnchor(Table, (ushort)glyphIndex);
			if (mobileAnchor.IsNull())
			{
				return false;
			}
			AnchorTable staticAnchor = this.Marks2(Table).Anchor(Table, (ushort)glyphIndex2, num, num2);
			if (staticAnchor.IsNull())
			{
				return false;
			}
			Positioning.AlignAnchors(Font, Table, Metrics, GlyphInfo, Advances, Offsets, nextGlyphInLookup, FirstGlyph, staticAnchor, mobileAnchor, false);
			return true;
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00129654 File Offset: 0x00128A54
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return false;
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x00129664 File Offset: 0x00128A64
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Mark1Coverage(table);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x00129678 File Offset: 0x00128A78
		public MarkToMarkPositioningSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x040020E3 RID: 8419
		private const int offsetFormat = 0;

		// Token: 0x040020E4 RID: 8420
		private const int offsetCoverage = 2;

		// Token: 0x040020E5 RID: 8421
		private const int offsetMark2Coverage = 4;

		// Token: 0x040020E6 RID: 8422
		private const int offsetClassCount = 6;

		// Token: 0x040020E7 RID: 8423
		private const int offsetMark1Array = 8;

		// Token: 0x040020E8 RID: 8424
		private const int offsetMark2Array = 10;

		// Token: 0x040020E9 RID: 8425
		private int offset;

		// Token: 0x020009C2 RID: 2498
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private struct Mark2Array
		{
			// Token: 0x06005AE1 RID: 23265 RVA: 0x0016CB34 File Offset: 0x0016BF34
			public AnchorTable Anchor(FontTable Table, ushort Mark2Index, ushort Mark1ClassCount, ushort Mark1Class)
			{
				int @ushort = (int)Table.GetUShort(this.offset + 2 + (int)((Mark2Index * Mark1ClassCount + Mark1Class) * 2));
				if (@ushort == 0)
				{
					return new AnchorTable(Table, 0);
				}
				return new AnchorTable(Table, this.offset + @ushort);
			}

			// Token: 0x06005AE2 RID: 23266 RVA: 0x0016CB74 File Offset: 0x0016BF74
			public Mark2Array(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DDB RID: 11739
			private const int offsetCount = 0;

			// Token: 0x04002DDC RID: 11740
			private const int offsetAnchors = 2;

			// Token: 0x04002DDD RID: 11741
			private const int sizeAnchorOffset = 2;

			// Token: 0x04002DDE RID: 11742
			private int offset;
		}
	}
}
