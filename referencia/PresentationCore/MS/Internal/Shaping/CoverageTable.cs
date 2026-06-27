using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C4 RID: 1732
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct CoverageTable
	{
		// Token: 0x06004B5E RID: 19294 RVA: 0x001268CC File Offset: 0x00125CCC
		public ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x001268E8 File Offset: 0x00125CE8
		public ushort Format1GlyphCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00126904 File Offset: 0x00125D04
		public ushort Format1Glyph(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 2));
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x00126924 File Offset: 0x00125D24
		public ushort Format2RangeCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00126940 File Offset: 0x00125D40
		public ushort Format2RangeStartGlyph(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 6));
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x00126960 File Offset: 0x00125D60
		public ushort Format2RangeEndGlyph(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 6) + 2);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x00126984 File Offset: 0x00125D84
		public ushort Format2RangeStartCoverageIndex(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 6) + 4);
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x001269A8 File Offset: 0x00125DA8
		public int GetGlyphIndex(FontTable Table, ushort glyph)
		{
			ushort num = this.Format(Table);
			if (num == 1)
			{
				ushort num2 = 0;
				ushort num3 = this.Format1GlyphCount(Table);
				while (num2 < num3)
				{
					ushort num4 = (ushort)(num2 + num3 >> 1);
					ushort num5 = this.Format1Glyph(Table, num4);
					if (glyph < num5)
					{
						num3 = num4;
					}
					else
					{
						if (glyph <= num5)
						{
							return (int)num4;
						}
						num2 = num4 + 1;
					}
				}
				return -1;
			}
			if (num != 2)
			{
				return -1;
			}
			ushort num6 = 0;
			ushort num7 = this.Format2RangeCount(Table);
			while (num6 < num7)
			{
				ushort num8 = (ushort)(num6 + num7 >> 1);
				if (glyph < this.Format2RangeStartGlyph(Table, num8))
				{
					num7 = num8;
				}
				else
				{
					if (glyph <= this.Format2RangeEndGlyph(Table, num8))
					{
						return (int)(glyph - this.Format2RangeStartGlyph(Table, num8) + this.Format2RangeStartCoverageIndex(Table, num8));
					}
					num6 = num8 + 1;
				}
			}
			return -1;
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x00126A60 File Offset: 0x00125E60
		public bool IsAnyGlyphCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			ushort num = this.Format(table);
			if (num != 1)
			{
				if (num != 2)
				{
					return true;
				}
				ushort num2 = this.Format2RangeCount(table);
				if (num2 == 0)
				{
					return false;
				}
				ushort num3 = this.Format2RangeStartGlyph(table, 0);
				ushort num4 = this.Format2RangeEndGlyph(table, num2 - 1);
				if (maxGlyphId < num3 || minGlyphId > num4)
				{
					return false;
				}
				for (ushort num5 = 0; num5 < num2; num5 += 1)
				{
					ushort num6 = this.Format2RangeStartGlyph(table, num5);
					ushort num7 = this.Format2RangeEndGlyph(table, num5);
					for (ushort num8 = num6; num8 <= num7; num8 += 1)
					{
						if (num8 <= maxGlyphId && num8 >= minGlyphId && ((ulong)glyphBits[num8 >> 5] & (ulong)(1L << (int)(num8 % 32 & 31))) != 0UL)
						{
							return true;
						}
					}
				}
				return false;
			}
			else
			{
				ushort num9 = this.Format1GlyphCount(table);
				if (num9 == 0)
				{
					return false;
				}
				ushort num10 = this.Format1Glyph(table, 0);
				ushort num11 = this.Format1Glyph(table, num9 - 1);
				if (maxGlyphId < num10 || minGlyphId > num11)
				{
					return false;
				}
				for (ushort num12 = 0; num12 < num9; num12 += 1)
				{
					ushort num13 = this.Format1Glyph(table, num12);
					if (num13 <= maxGlyphId && num13 >= minGlyphId && ((ulong)glyphBits[num13 >> 5] & (ulong)(1L << (int)(num13 % 32 & 31))) != 0UL)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x00126B78 File Offset: 0x00125F78
		public static CoverageTable InvalidCoverage
		{
			get
			{
				return new CoverageTable(-1);
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06004B68 RID: 19304 RVA: 0x00126B8C File Offset: 0x00125F8C
		public bool IsInvalid
		{
			get
			{
				return this.offset == -1;
			}
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x00126BA4 File Offset: 0x00125FA4
		public CoverageTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x0400203D RID: 8253
		private const int offsetFormat = 0;

		// Token: 0x0400203E RID: 8254
		private const int offsetFormat1GlyphCount = 2;

		// Token: 0x0400203F RID: 8255
		private const int offsetFormat1GlyphArray = 4;

		// Token: 0x04002040 RID: 8256
		private const int sizeFormat1GlyphId = 2;

		// Token: 0x04002041 RID: 8257
		private const int offsetFormat2RangeCount = 2;

		// Token: 0x04002042 RID: 8258
		private const int offsetFormat2RangeRecordArray = 4;

		// Token: 0x04002043 RID: 8259
		private const int sizeFormat2RangeRecord = 6;

		// Token: 0x04002044 RID: 8260
		private const int offsetFormat2RangeRecordStart = 0;

		// Token: 0x04002045 RID: 8261
		private const int offsetFormat2RangeRecordEnd = 2;

		// Token: 0x04002046 RID: 8262
		private const int offsetFormat2RangeRecordStartIndex = 4;

		// Token: 0x04002047 RID: 8263
		private int offset;
	}
}
