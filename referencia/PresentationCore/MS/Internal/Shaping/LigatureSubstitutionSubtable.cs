using System;
using System.IO;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E5 RID: 1765
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct LigatureSubstitutionSubtable
	{
		// Token: 0x06004C34 RID: 19508 RVA: 0x0012A584 File Offset: 0x00129984
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x0012A5A0 File Offset: 0x001299A0
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x0012A5C8 File Offset: 0x001299C8
		private ushort LigatureSetCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 4);
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x0012A5E4 File Offset: 0x001299E4
		private LigatureSubstitutionSubtable.LigatureSetTable LigatureSet(FontTable Table, ushort Index)
		{
			return new LigatureSubstitutionSubtable.LigatureSetTable(this.offset + (int)Table.GetUShort(this.offset + 6 + (int)(Index * 2)));
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x0012A610 File Offset: 0x00129A10
		public bool Apply(IOpenTypeFont Font, FontTable Table, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, out int NextGlyph)
		{
			Invariant.Assert(FirstGlyph >= 0);
			Invariant.Assert(AfterLastGlyph <= GlyphInfo.Length);
			NextGlyph = FirstGlyph + 1;
			if (this.Format(Table) != 1)
			{
				return false;
			}
			int length = GlyphInfo.Length;
			ushort glyph = GlyphInfo.Glyphs[FirstGlyph];
			int glyphIndex = this.Coverage(Table).GetGlyphIndex(Table, glyph);
			if (glyphIndex == -1)
			{
				return false;
			}
			ushort value = 0;
			bool flag = false;
			ushort num = 0;
			LigatureSubstitutionSubtable.LigatureSetTable ligatureSetTable = this.LigatureSet(Table, (ushort)glyphIndex);
			ushort num2 = ligatureSetTable.LigatureCount(Table);
			for (ushort num3 = 0; num3 < num2; num3 += 1)
			{
				LigatureSubstitutionSubtable.LigatureTable ligatureTable = ligatureSetTable.Ligature(Table, num3);
				num = ligatureTable.ComponentCount(Table);
				if (num == 0)
				{
					throw new FileFormatException();
				}
				int num4 = FirstGlyph;
				ushort num5;
				for (num5 = 1; num5 < num; num5 += 1)
				{
					num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 + 1, LookupFlags, 1);
					if (num4 >= AfterLastGlyph || GlyphInfo.Glyphs[num4] != ligatureTable.Component(Table, num5))
					{
						break;
					}
				}
				if (num5 == num)
				{
					flag = true;
					value = ligatureTable.LigatureGlyph(Table);
					break;
				}
			}
			if (flag)
			{
				int num6 = 0;
				int num7 = int.MaxValue;
				int num4 = FirstGlyph;
				for (ushort num8 = 0; num8 < num; num8 += 1)
				{
					Invariant.Assert(num4 < AfterLastGlyph);
					int num9 = (int)GlyphInfo.FirstChars[num4];
					int num10 = (int)GlyphInfo.LigatureCounts[num4];
					num6 += num10;
					if (num9 < num7)
					{
						num7 = num9;
					}
					num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 + 1, LookupFlags, 1);
				}
				num4 = FirstGlyph;
				int num11 = FirstGlyph;
				ushort num12 = 0;
				for (ushort num13 = 1; num13 <= num; num13 += 1)
				{
					num11 = num4;
					if (num13 < num)
					{
						num4 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num4 + 1, LookupFlags, 1);
					}
					else
					{
						num4 = GlyphInfo.Length;
					}
					for (int i = 0; i < CharCount; i++)
					{
						if ((int)Charmap[i] == num11)
						{
							Charmap[i] = (ushort)FirstGlyph;
						}
					}
					if (num12 > 0)
					{
						for (int j = num11 + 1; j < num4; j++)
						{
							GlyphInfo.Glyphs[j - (int)num12] = GlyphInfo.Glyphs[j];
							GlyphInfo.GlyphFlags[j - (int)num12] = GlyphInfo.GlyphFlags[j];
							GlyphInfo.FirstChars[j - (int)num12] = GlyphInfo.FirstChars[j];
							GlyphInfo.LigatureCounts[j - (int)num12] = GlyphInfo.LigatureCounts[j];
						}
						if (num4 - num11 > 1)
						{
							for (int k = 0; k < CharCount; k++)
							{
								ushort num14 = Charmap[k];
								if ((int)num14 > num11 && (int)num14 < num4)
								{
									int index = k;
									Charmap[index] -= num12;
								}
							}
						}
					}
					num12 += 1;
				}
				GlyphInfo.Glyphs[FirstGlyph] = value;
				GlyphInfo.GlyphFlags[FirstGlyph] = 23;
				GlyphInfo.FirstChars[FirstGlyph] = (ushort)num7;
				GlyphInfo.LigatureCounts[FirstGlyph] = (ushort)num6;
				if (num > 1)
				{
					GlyphInfo.Remove(GlyphInfo.Length - (int)num + 1, (int)(num - 1));
				}
				NextGlyph = num11 - (int)(num - 1) + 1;
			}
			return flag;
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x0012A948 File Offset: 0x00129D48
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			if (!this.Coverage(table).IsAnyGlyphCovered(table, glyphBits, minGlyphId, maxGlyphId))
			{
				return false;
			}
			ushort num = this.LigatureSetCount(table);
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				LigatureSubstitutionSubtable.LigatureSetTable ligatureSetTable = this.LigatureSet(table, num2);
				ushort num3 = ligatureSetTable.LigatureCount(table);
				for (ushort num4 = 0; num4 < num3; num4 += 1)
				{
					LigatureSubstitutionSubtable.LigatureTable ligatureTable = ligatureSetTable.Ligature(table, num4);
					ushort num5 = ligatureTable.ComponentCount(table);
					bool flag = true;
					for (ushort num6 = 1; num6 < num5; num6 += 1)
					{
						ushort num7 = ligatureTable.Component(table, num6);
						if (num7 > maxGlyphId || num7 < minGlyphId || ((ulong)glyphBits[num7 >> 5] & (ulong)(1L << (int)(num7 % 32 & 31))) == 0UL)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x0012AA08 File Offset: 0x00129E08
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0012AA1C File Offset: 0x00129E1C
		public LigatureSubstitutionSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002112 RID: 8466
		private const int offsetFormat = 0;

		// Token: 0x04002113 RID: 8467
		private const int offsetCoverage = 2;

		// Token: 0x04002114 RID: 8468
		private const int offsetLigatureSetCount = 4;

		// Token: 0x04002115 RID: 8469
		private const int offsetLigatureSetArray = 6;

		// Token: 0x04002116 RID: 8470
		private const int sizeLigatureSet = 2;

		// Token: 0x04002117 RID: 8471
		private int offset;

		// Token: 0x020009C3 RID: 2499
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private struct LigatureSetTable
		{
			// Token: 0x06005AE3 RID: 23267 RVA: 0x0016CB88 File Offset: 0x0016BF88
			public ushort LigatureCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005AE4 RID: 23268 RVA: 0x0016CBA4 File Offset: 0x0016BFA4
			public LigatureSubstitutionSubtable.LigatureTable Ligature(FontTable Table, ushort Index)
			{
				return new LigatureSubstitutionSubtable.LigatureTable(this.offset + (int)Table.GetUShort(this.offset + 2 + (int)(Index * 2)));
			}

			// Token: 0x06005AE5 RID: 23269 RVA: 0x0016CBD0 File Offset: 0x0016BFD0
			public LigatureSetTable(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DDF RID: 11743
			private const int offsetLigatureCount = 0;

			// Token: 0x04002DE0 RID: 11744
			private const int offsetLigatureArray = 2;

			// Token: 0x04002DE1 RID: 11745
			private const int sizeLigatureOffset = 2;

			// Token: 0x04002DE2 RID: 11746
			private int offset;
		}

		// Token: 0x020009C4 RID: 2500
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private struct LigatureTable
		{
			// Token: 0x06005AE6 RID: 23270 RVA: 0x0016CBE4 File Offset: 0x0016BFE4
			public ushort LigatureGlyph(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005AE7 RID: 23271 RVA: 0x0016CC00 File Offset: 0x0016C000
			public ushort ComponentCount(FontTable Table)
			{
				return Table.GetUShort(this.offset + 2);
			}

			// Token: 0x06005AE8 RID: 23272 RVA: 0x0016CC1C File Offset: 0x0016C01C
			public ushort Component(FontTable Table, ushort Index)
			{
				return Table.GetUShort(this.offset + 4 + (int)((Index - 1) * 2));
			}

			// Token: 0x06005AE9 RID: 23273 RVA: 0x0016CC40 File Offset: 0x0016C040
			public LigatureTable(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DE3 RID: 11747
			private const int offsetLigatureGlyph = 0;

			// Token: 0x04002DE4 RID: 11748
			private const int offsetComponentCount = 2;

			// Token: 0x04002DE5 RID: 11749
			private const int offsetComponentArray = 4;

			// Token: 0x04002DE6 RID: 11750
			private const int sizeComponent = 2;

			// Token: 0x04002DE7 RID: 11751
			private int offset;
		}
	}
}
