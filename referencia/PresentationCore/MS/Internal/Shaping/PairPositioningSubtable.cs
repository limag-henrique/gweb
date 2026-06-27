using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D9 RID: 1753
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct PairPositioningSubtable
	{
		// Token: 0x06004BD0 RID: 19408 RVA: 0x00128D44 File Offset: 0x00128144
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x00128D60 File Offset: 0x00128160
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetOffset(this.offset + 2));
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x00128D88 File Offset: 0x00128188
		private ushort FirstValueFormat(FontTable Table)
		{
			return Table.GetUShort(this.offset + 4);
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x00128DA4 File Offset: 0x001281A4
		private ushort SecondValueFormat(FontTable Table)
		{
			return Table.GetUShort(this.offset + 6);
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x00128DC0 File Offset: 0x001281C0
		private PairPositioningSubtable.PairSetTable Format1PairSet(FontTable Table, ushort Index)
		{
			Invariant.Assert(this.Format(Table) == 1);
			return new PairPositioningSubtable.PairSetTable(this.offset + (int)Table.GetUShort(this.offset + 10 + (int)(Index * 2)), this.FirstValueFormat(Table), this.SecondValueFormat(Table));
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00128E0C File Offset: 0x0012820C
		private ClassDefTable Format2Class1Table(FontTable Table)
		{
			Invariant.Assert(this.Format(Table) == 2);
			return new ClassDefTable(this.offset + (int)Table.GetUShort(this.offset + 8));
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00128E44 File Offset: 0x00128244
		private ClassDefTable Format2Class2Table(FontTable Table)
		{
			Invariant.Assert(this.Format(Table) == 2);
			return new ClassDefTable(this.offset + (int)Table.GetUShort(this.offset + 10));
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00128E7C File Offset: 0x0012827C
		private ushort Format2Class1Count(FontTable Table)
		{
			Invariant.Assert(this.Format(Table) == 2);
			return Table.GetUShort(this.offset + 12);
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x00128EA8 File Offset: 0x001282A8
		private ushort Format2Class2Count(FontTable Table)
		{
			Invariant.Assert(this.Format(Table) == 2);
			return Table.GetUShort(this.offset + 14);
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x00128ED4 File Offset: 0x001282D4
		private ValueRecordTable Format2FirstValueRecord(FontTable Table, ushort Class2Count, ushort Class1Index, ushort Class2Index)
		{
			Invariant.Assert(this.Format(Table) == 2);
			ushort format = this.FirstValueFormat(Table);
			ushort format2 = this.SecondValueFormat(Table);
			int num = (int)(ValueRecordTable.Size(format) + ValueRecordTable.Size(format2));
			return new ValueRecordTable(this.offset + 16 + (int)(Class1Index * Class2Count + Class2Index) * num, this.offset, format);
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x00128F2C File Offset: 0x0012832C
		private ValueRecordTable Format2SecondValueRecord(FontTable Table, ushort Class2Count, ushort Class1Index, ushort Class2Index)
		{
			Invariant.Assert(this.Format(Table) == 2);
			ushort format = this.FirstValueFormat(Table);
			ushort format2 = this.SecondValueFormat(Table);
			int num = (int)ValueRecordTable.Size(format);
			int num2 = num + (int)ValueRecordTable.Size(format2);
			return new ValueRecordTable(this.offset + 16 + (int)(Class1Index * Class2Count + Class2Index) * num2 + num, this.offset, format2);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x00128F88 File Offset: 0x00128388
		public unsafe bool Apply(IOpenTypeFont Font, FontTable Table, LayoutMetrics Metrics, GlyphInfoList GlyphInfo, ushort LookupFlags, int* Advances, LayoutOffset* Offsets, int FirstGlyph, int AfterLastGlyph, out int NextGlyph)
		{
			Invariant.Assert(FirstGlyph >= 0);
			Invariant.Assert(AfterLastGlyph <= GlyphInfo.Length);
			NextGlyph = FirstGlyph + 1;
			int length = GlyphInfo.Length;
			ushort glyph = GlyphInfo.Glyphs[FirstGlyph];
			int nextGlyphInLookup = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, FirstGlyph + 1, LookupFlags, 1);
			if (nextGlyphInLookup >= AfterLastGlyph)
			{
				return false;
			}
			ushort glyph2 = GlyphInfo.Glyphs[nextGlyphInLookup];
			ushort num = this.Format(Table);
			ValueRecordTable valueRecordTable;
			ValueRecordTable valueRecordTable2;
			if (num != 1)
			{
				if (num != 2)
				{
					return false;
				}
				int glyphIndex = this.Coverage(Table).GetGlyphIndex(Table, glyph);
				if (glyphIndex == -1)
				{
					return false;
				}
				ushort @class = this.Format2Class1Table(Table).GetClass(Table, glyph);
				if (@class >= this.Format2Class1Count(Table))
				{
					return false;
				}
				ushort class2 = this.Format2Class2Table(Table).GetClass(Table, glyph2);
				if (class2 >= this.Format2Class2Count(Table))
				{
					return false;
				}
				ushort class2Count = this.Format2Class2Count(Table);
				valueRecordTable = this.Format2FirstValueRecord(Table, class2Count, @class, class2);
				valueRecordTable2 = this.Format2SecondValueRecord(Table, class2Count, @class, class2);
			}
			else
			{
				int glyphIndex2 = this.Coverage(Table).GetGlyphIndex(Table, glyph);
				if (glyphIndex2 == -1)
				{
					return false;
				}
				PairPositioningSubtable.PairSetTable pairSetTable = this.Format1PairSet(Table, (ushort)glyphIndex2);
				int num2 = pairSetTable.FindPairValue(Table, glyph2);
				if (num2 == -1)
				{
					return false;
				}
				valueRecordTable = pairSetTable.FirstValueRecord(Table, (ushort)num2, this.FirstValueFormat(Table));
				valueRecordTable2 = pairSetTable.SecondValueRecord(Table, (ushort)num2, this.SecondValueFormat(Table));
			}
			valueRecordTable.AdjustPos(Table, Metrics, ref Offsets[FirstGlyph], ref Advances[FirstGlyph]);
			valueRecordTable2.AdjustPos(Table, Metrics, ref Offsets[nextGlyphInLookup], ref Advances[nextGlyphInLookup]);
			return true;
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x00129138 File Offset: 0x00128538
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return this.Coverage(table).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId);
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0012915C File Offset: 0x0012855C
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x00129170 File Offset: 0x00128570
		public PairPositioningSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x040020CA RID: 8394
		private const int offsetFormat = 0;

		// Token: 0x040020CB RID: 8395
		private const int offsetCoverage = 2;

		// Token: 0x040020CC RID: 8396
		private const int offsetValueFormat1 = 4;

		// Token: 0x040020CD RID: 8397
		private const int offsetValueFormat2 = 6;

		// Token: 0x040020CE RID: 8398
		private const int offsetFormat1PairSetCount = 8;

		// Token: 0x040020CF RID: 8399
		private const int offsetFormat1PairSetArray = 10;

		// Token: 0x040020D0 RID: 8400
		private const int sizeFormat1PairSetOffset = 2;

		// Token: 0x040020D1 RID: 8401
		private const int offsetFormat2ClassDef1 = 8;

		// Token: 0x040020D2 RID: 8402
		private const int offsetFormat2ClassDef2 = 10;

		// Token: 0x040020D3 RID: 8403
		private const int offsetFormat2Class1Count = 12;

		// Token: 0x040020D4 RID: 8404
		private const int offsetFormat2Class2Count = 14;

		// Token: 0x040020D5 RID: 8405
		private const int offsetFormat2ValueRecordArray = 16;

		// Token: 0x040020D6 RID: 8406
		private int offset;

		// Token: 0x020009C0 RID: 2496
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private struct PairSetTable
		{
			// Token: 0x06005AD9 RID: 23257 RVA: 0x0016C9D8 File Offset: 0x0016BDD8
			public ushort PairValueCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005ADA RID: 23258 RVA: 0x0016C9F4 File Offset: 0x0016BDF4
			public ushort PairValueGlyph(FontTable Table, ushort Index)
			{
				return Table.GetUShort(this.offset + 2 + (int)(Index * this.pairValueRecordSize));
			}

			// Token: 0x06005ADB RID: 23259 RVA: 0x0016CA18 File Offset: 0x0016BE18
			public ValueRecordTable FirstValueRecord(FontTable Table, ushort Index, ushort Format)
			{
				return new ValueRecordTable(this.offset + 2 + (int)(Index * this.pairValueRecordSize) + 2, this.offset, Format);
			}

			// Token: 0x06005ADC RID: 23260 RVA: 0x0016CA44 File Offset: 0x0016BE44
			public ValueRecordTable SecondValueRecord(FontTable Table, ushort Index, ushort Format)
			{
				return new ValueRecordTable(this.offset + 2 + (int)(Index * this.pairValueRecordSize) + (int)this.secondValueRecordOffset, this.offset, Format);
			}

			// Token: 0x06005ADD RID: 23261 RVA: 0x0016CA78 File Offset: 0x0016BE78
			public int FindPairValue(FontTable Table, ushort Glyph)
			{
				ushort num = this.PairValueCount(Table);
				for (ushort num2 = 0; num2 < num; num2 += 1)
				{
					if (this.PairValueGlyph(Table, num2) == Glyph)
					{
						return (int)num2;
					}
				}
				return -1;
			}

			// Token: 0x06005ADE RID: 23262 RVA: 0x0016CAA8 File Offset: 0x0016BEA8
			public PairSetTable(int Offset, ushort firstValueRecordSize, ushort secondValueRecordSize)
			{
				this.secondValueRecordOffset = 2 + ValueRecordTable.Size(firstValueRecordSize);
				this.pairValueRecordSize = this.secondValueRecordOffset + ValueRecordTable.Size(secondValueRecordSize);
				this.offset = Offset;
			}

			// Token: 0x04002DD1 RID: 11729
			private const int offsetPairValueCount = 0;

			// Token: 0x04002DD2 RID: 11730
			private const int offsetPairValueArray = 2;

			// Token: 0x04002DD3 RID: 11731
			private const int offsetPairValueSecondGlyph = 0;

			// Token: 0x04002DD4 RID: 11732
			private const int offsetPairValueValue1 = 2;

			// Token: 0x04002DD5 RID: 11733
			private int offset;

			// Token: 0x04002DD6 RID: 11734
			private ushort pairValueRecordSize;

			// Token: 0x04002DD7 RID: 11735
			private ushort secondValueRecordOffset;
		}
	}
}
