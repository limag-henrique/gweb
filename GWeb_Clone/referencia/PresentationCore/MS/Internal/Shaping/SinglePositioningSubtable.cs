using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D8 RID: 1752
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct SinglePositioningSubtable
	{
		// Token: 0x06004BC7 RID: 19399 RVA: 0x00128B6C File Offset: 0x00127F6C
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x00128B88 File Offset: 0x00127F88
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetOffset(this.offset + 2));
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x00128BB0 File Offset: 0x00127FB0
		private ushort ValueFormat(FontTable Table)
		{
			return Table.GetUShort(this.offset + 4);
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x00128BCC File Offset: 0x00127FCC
		private ValueRecordTable Format1ValueRecord(FontTable Table)
		{
			Invariant.Assert(this.Format(Table) == 1);
			return new ValueRecordTable(this.offset + 6, this.offset, this.ValueFormat(Table));
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x00128C04 File Offset: 0x00128004
		private ValueRecordTable Format2ValueRecord(FontTable Table, ushort Index)
		{
			Invariant.Assert(this.Format(Table) == 2);
			return new ValueRecordTable(this.offset + 8 + (int)(Index * ValueRecordTable.Size(this.ValueFormat(Table))), this.offset, this.ValueFormat(Table));
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x00128C4C File Offset: 0x0012804C
		public unsafe bool Apply(FontTable Table, LayoutMetrics Metrics, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, int FirstGlyph, int AfterLastGlyph, out int NextGlyph)
		{
			Invariant.Assert(FirstGlyph >= 0);
			Invariant.Assert(AfterLastGlyph <= GlyphInfo.Length);
			NextGlyph = FirstGlyph + 1;
			int length = GlyphInfo.Length;
			ushort glyph = GlyphInfo.Glyphs[FirstGlyph];
			int glyphIndex = this.Coverage(Table).GetGlyphIndex(Table, glyph);
			if (glyphIndex == -1)
			{
				return false;
			}
			ushort num = this.Format(Table);
			ValueRecordTable valueRecordTable;
			if (num != 1)
			{
				if (num != 2)
				{
					return false;
				}
				valueRecordTable = this.Format2ValueRecord(Table, (ushort)glyphIndex);
			}
			else
			{
				valueRecordTable = this.Format1ValueRecord(Table);
			}
			valueRecordTable.AdjustPos(Table, Metrics, ref Offsets[FirstGlyph], ref Advances[FirstGlyph]);
			return true;
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x00128CF8 File Offset: 0x001280F8
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return this.Coverage(table).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId);
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x00128D1C File Offset: 0x0012811C
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x00128D30 File Offset: 0x00128130
		public SinglePositioningSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x040020C3 RID: 8387
		private const int offsetFormat = 0;

		// Token: 0x040020C4 RID: 8388
		private const int offsetCoverage = 2;

		// Token: 0x040020C5 RID: 8389
		private const int offsetValueFormat = 4;

		// Token: 0x040020C6 RID: 8390
		private const int offsetFormat1Value = 6;

		// Token: 0x040020C7 RID: 8391
		private const int offsetFormat2ValueCount = 6;

		// Token: 0x040020C8 RID: 8392
		private const int offsetFormat2ValueArray = 8;

		// Token: 0x040020C9 RID: 8393
		private int offset;
	}
}
