using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006DB RID: 1755
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct MarkToBasePositioningSubtable
	{
		// Token: 0x06004BE2 RID: 19426 RVA: 0x001291F4 File Offset: 0x001285F4
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x00129210 File Offset: 0x00128610
		private CoverageTable MarkCoverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x00129238 File Offset: 0x00128638
		private CoverageTable BaseCoverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 4));
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x00129260 File Offset: 0x00128660
		private ushort ClassCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 6);
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0012927C File Offset: 0x0012867C
		private MarkArray Marks(FontTable Table)
		{
			return new MarkArray(this.offset + (int)Table.GetUShort(this.offset + 8));
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x001292A4 File Offset: 0x001286A4
		private MarkToBasePositioningSubtable.BaseArray Bases(FontTable Table)
		{
			return new MarkToBasePositioningSubtable.BaseArray(this.offset + (int)Table.GetUShort(this.offset + 10));
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x001292CC File Offset: 0x001286CC
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
			int glyphIndex = this.MarkCoverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[FirstGlyph]);
			if (glyphIndex == -1)
			{
				return false;
			}
			int nextGlyphInLookup = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, FirstGlyph - 1, 8, -1);
			if (nextGlyphInLookup < 0)
			{
				return false;
			}
			int glyphIndex2 = this.BaseCoverage(Table).GetGlyphIndex(Table, GlyphInfo.Glyphs[nextGlyphInLookup]);
			if (glyphIndex2 == -1)
			{
				return false;
			}
			ushort num = this.ClassCount(Table);
			MarkArray markArray = this.Marks(Table);
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
			AnchorTable staticAnchor = this.Bases(Table).BaseAnchor(Table, (ushort)glyphIndex2, num, num2);
			if (staticAnchor.IsNull())
			{
				return false;
			}
			Positioning.AlignAnchors(Font, Table, Metrics, GlyphInfo, Advances, Offsets, nextGlyphInLookup, FirstGlyph, staticAnchor, mobileAnchor, false);
			return true;
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00129404 File Offset: 0x00128804
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return false;
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00129414 File Offset: 0x00128814
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.MarkCoverage(table);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x00129428 File Offset: 0x00128828
		public MarkToBasePositioningSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x040020DC RID: 8412
		private const int offsetFormat = 0;

		// Token: 0x040020DD RID: 8413
		private const int offsetCoverage = 2;

		// Token: 0x040020DE RID: 8414
		private const int offsetBaseCoverage = 4;

		// Token: 0x040020DF RID: 8415
		private const int offsetClassCount = 6;

		// Token: 0x040020E0 RID: 8416
		private const int offsetMarkArray = 8;

		// Token: 0x040020E1 RID: 8417
		private const int offsetBaseArray = 10;

		// Token: 0x040020E2 RID: 8418
		private int offset;

		// Token: 0x020009C1 RID: 2497
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private struct BaseArray
		{
			// Token: 0x06005ADF RID: 23263 RVA: 0x0016CAE0 File Offset: 0x0016BEE0
			public AnchorTable BaseAnchor(FontTable Table, ushort BaseIndex, ushort MarkClassCount, ushort MarkClass)
			{
				int @ushort = (int)Table.GetUShort(this.offset + 2 + (int)((BaseIndex * MarkClassCount + MarkClass) * 2));
				if (@ushort == 0)
				{
					return new AnchorTable(Table, 0);
				}
				return new AnchorTable(Table, this.offset + @ushort);
			}

			// Token: 0x06005AE0 RID: 23264 RVA: 0x0016CB20 File Offset: 0x0016BF20
			public BaseArray(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DD8 RID: 11736
			private const int offsetAnchorArray = 2;

			// Token: 0x04002DD9 RID: 11737
			private const int sizeAnchorOffset = 2;

			// Token: 0x04002DDA RID: 11738
			private int offset;
		}
	}
}
