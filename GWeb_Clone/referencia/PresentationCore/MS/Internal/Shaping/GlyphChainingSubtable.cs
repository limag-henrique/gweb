using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006AD RID: 1709
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct GlyphChainingSubtable
	{
		// Token: 0x06004ACE RID: 19150 RVA: 0x00123DEC File Offset: 0x001231EC
		public ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x00123E08 File Offset: 0x00123208
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00123E30 File Offset: 0x00123230
		private GlyphChainingSubtable.SubRuleSet RuleSet(FontTable Table, int Index)
		{
			return new GlyphChainingSubtable.SubRuleSet(this.offset + (int)Table.GetUShort(this.offset + 6 + Index * 2));
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00123E5C File Offset: 0x0012325C
		public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
		{
			Invariant.Assert(this.Format(Table) == 1);
			NextGlyph = FirstGlyph + 1;
			int length = GlyphInfo.Length;
			ushort glyph = GlyphInfo.Glyphs[FirstGlyph];
			int glyphIndex = this.Coverage(Table).GetGlyphIndex(Table, glyph);
			if (glyphIndex < 0)
			{
				return false;
			}
			GlyphChainingSubtable.SubRuleSet subRuleSet = this.RuleSet(Table, glyphIndex);
			ushort num = subRuleSet.RuleCount(Table);
			bool flag = false;
			ushort num2 = 0;
			while (!flag && num2 < num)
			{
				flag = subRuleSet.Rule(Table, num2).Apply(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
				num2 += 1;
			}
			return flag;
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x00123F08 File Offset: 0x00123308
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return true;
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00123F18 File Offset: 0x00123318
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00123F2C File Offset: 0x0012332C
		public GlyphChainingSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FB4 RID: 8116
		private const int offsetFormat = 0;

		// Token: 0x04001FB5 RID: 8117
		private const int offsetCoverage = 2;

		// Token: 0x04001FB6 RID: 8118
		private const int offsetSubRuleSetCount = 4;

		// Token: 0x04001FB7 RID: 8119
		private const int offsetSubRuleSetArray = 6;

		// Token: 0x04001FB8 RID: 8120
		private const int sizeRuleSetOffset = 2;

		// Token: 0x04001FB9 RID: 8121
		private int offset;

		// Token: 0x020009B7 RID: 2487
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubRuleSet
		{
			// Token: 0x06005AAC RID: 23212 RVA: 0x0016C0A0 File Offset: 0x0016B4A0
			public ushort RuleCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005AAD RID: 23213 RVA: 0x0016C0BC File Offset: 0x0016B4BC
			public GlyphChainingSubtable.SubRule Rule(FontTable Table, ushort Index)
			{
				return new GlyphChainingSubtable.SubRule(this.offset + (int)Table.GetUShort(this.offset + 2 + (int)(Index * 2)));
			}

			// Token: 0x06005AAE RID: 23214 RVA: 0x0016C0E8 File Offset: 0x0016B4E8
			public SubRuleSet(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DAD RID: 11693
			private const int offsetRuleCount = 0;

			// Token: 0x04002DAE RID: 11694
			private const int offsetRuleArray = 2;

			// Token: 0x04002DAF RID: 11695
			private const int sizeRuleOffset = 2;

			// Token: 0x04002DB0 RID: 11696
			private int offset;
		}

		// Token: 0x020009B8 RID: 2488
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubRule
		{
			// Token: 0x06005AAF RID: 23215 RVA: 0x0016C104 File Offset: 0x0016B504
			public static ushort GlyphCount(FontTable Table, int Offset)
			{
				return Table.GetUShort(Offset);
			}

			// Token: 0x06005AB0 RID: 23216 RVA: 0x0016C118 File Offset: 0x0016B518
			public static ushort GlyphId(FontTable Table, int Offset)
			{
				return Table.GetUShort(Offset);
			}

			// Token: 0x06005AB1 RID: 23217 RVA: 0x0016C12C File Offset: 0x0016B52C
			public ContextualLookupRecords ContextualLookups(FontTable Table, int CurrentOffset)
			{
				return new ContextualLookupRecords(CurrentOffset + 2, Table.GetUShort(CurrentOffset));
			}

			// Token: 0x06005AB2 RID: 23218 RVA: 0x0016C148 File Offset: 0x0016B548
			public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
			{
				bool flag = true;
				NextGlyph = FirstGlyph + 1;
				int num = this.offset;
				int num2 = (int)GlyphChainingSubtable.SubRule.GlyphCount(Table, num);
				num += 2;
				int num3 = FirstGlyph;
				ushort num4 = 0;
				while ((int)num4 < num2 && flag)
				{
					num3 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num3 - 1, LookupFlags, -1);
					if (num3 < 0)
					{
						flag = false;
					}
					else
					{
						flag = (GlyphChainingSubtable.SubRule.GlyphId(Table, num) == GlyphInfo.Glyphs[num3]);
						num += 2;
					}
					num4 += 1;
				}
				if (!flag)
				{
					return false;
				}
				int num5 = (int)GlyphChainingSubtable.SubRule.GlyphCount(Table, num);
				num += 2;
				num3 = FirstGlyph;
				ushort num6 = 1;
				while ((int)num6 < num5 && flag)
				{
					num3 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num3 + 1, LookupFlags, 1);
					if (num3 >= AfterLastGlyph)
					{
						flag = false;
					}
					else
					{
						flag = (GlyphChainingSubtable.SubRule.GlyphId(Table, num) == GlyphInfo.Glyphs[num3]);
						num += 2;
					}
					num6 += 1;
				}
				if (!flag)
				{
					return false;
				}
				int afterLastGlyph = num3 + 1;
				int num7 = (int)GlyphChainingSubtable.SubRule.GlyphCount(Table, num);
				num += 2;
				ushort num8 = 0;
				while ((int)num8 < num7 && flag)
				{
					num3 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num3 + 1, LookupFlags, 1);
					if (num3 >= GlyphInfo.Length)
					{
						flag = false;
					}
					else
					{
						flag = (GlyphChainingSubtable.SubRule.GlyphId(Table, num) == GlyphInfo.Glyphs[num3]);
						num += 2;
					}
					num8 += 1;
				}
				if (flag)
				{
					this.ContextualLookups(Table, num).ApplyContextualLookups(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, afterLastGlyph, Parameter, nestingLevel, out NextGlyph);
				}
				return flag;
			}

			// Token: 0x06005AB3 RID: 23219 RVA: 0x0016C2AC File Offset: 0x0016B6AC
			public SubRule(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DB1 RID: 11697
			private const int sizeCount = 2;

			// Token: 0x04002DB2 RID: 11698
			private const int sizeGlyphId = 2;

			// Token: 0x04002DB3 RID: 11699
			private int offset;
		}
	}
}
