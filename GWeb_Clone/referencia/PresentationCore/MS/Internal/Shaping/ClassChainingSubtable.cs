using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006AE RID: 1710
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ClassChainingSubtable
	{
		// Token: 0x06004AD5 RID: 19157 RVA: 0x00123F40 File Offset: 0x00123340
		public ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00123F5C File Offset: 0x0012335C
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x00123F84 File Offset: 0x00123384
		private ClassDefTable BacktrackClassDef(FontTable Table)
		{
			return new ClassDefTable(this.offset + (int)Table.GetUShort(this.offset + 4));
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x00123FAC File Offset: 0x001233AC
		private ClassDefTable InputClassDef(FontTable Table)
		{
			return new ClassDefTable(this.offset + (int)Table.GetUShort(this.offset + 6));
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x00123FD4 File Offset: 0x001233D4
		private ClassDefTable LookaheadClassDef(FontTable Table)
		{
			return new ClassDefTable(this.offset + (int)Table.GetUShort(this.offset + 8));
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00123FFC File Offset: 0x001233FC
		private ushort ClassSetCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 10);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00124018 File Offset: 0x00123418
		private ClassChainingSubtable.SubClassSet ClassSet(FontTable Table, ushort Index)
		{
			int @ushort = (int)Table.GetUShort(this.offset + 12 + (int)(Index * 2));
			if (@ushort == 0)
			{
				return new ClassChainingSubtable.SubClassSet(int.MaxValue);
			}
			return new ClassChainingSubtable.SubClassSet(this.offset + @ushort);
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x00124054 File Offset: 0x00123454
		public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
		{
			Invariant.Assert(this.Format(Table) == 2);
			NextGlyph = FirstGlyph + 1;
			int length = GlyphInfo.Length;
			ushort glyph = GlyphInfo.Glyphs[FirstGlyph];
			if (this.Coverage(Table).GetGlyphIndex(Table, glyph) < 0)
			{
				return false;
			}
			ClassDefTable inputClassDef = this.InputClassDef(Table);
			ClassDefTable backtrackClassDef = this.BacktrackClassDef(Table);
			ClassDefTable lookaheadClassDef = this.LookaheadClassDef(Table);
			ushort @class = inputClassDef.GetClass(Table, glyph);
			if (@class >= this.ClassSetCount(Table))
			{
				return false;
			}
			ClassChainingSubtable.SubClassSet subClassSet = this.ClassSet(Table, @class);
			if (subClassSet.IsNull)
			{
				return false;
			}
			ushort num = subClassSet.RuleCount(Table);
			bool flag = false;
			ushort num2 = 0;
			while (!flag && num2 < num)
			{
				flag = subClassSet.Rule(Table, num2).Apply(Font, TableTag, Table, Metrics, inputClassDef, backtrackClassDef, lookaheadClassDef, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
				num2 += 1;
			}
			return flag;
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x00124140 File Offset: 0x00123540
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return true;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00124150 File Offset: 0x00123550
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x00124164 File Offset: 0x00123564
		public ClassChainingSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FBA RID: 8122
		private const int offsetFormat = 0;

		// Token: 0x04001FBB RID: 8123
		private const int offsetCoverage = 2;

		// Token: 0x04001FBC RID: 8124
		private const int offsetBacktrackClassDef = 4;

		// Token: 0x04001FBD RID: 8125
		private const int offsetInputClassDef = 6;

		// Token: 0x04001FBE RID: 8126
		private const int offsetLookaheadClassDef = 8;

		// Token: 0x04001FBF RID: 8127
		private const int offsetSubClassSetCount = 10;

		// Token: 0x04001FC0 RID: 8128
		private const int offsetSubClassSetArray = 12;

		// Token: 0x04001FC1 RID: 8129
		private const int sizeClassSetOffset = 2;

		// Token: 0x04001FC2 RID: 8130
		private int offset;

		// Token: 0x020009B9 RID: 2489
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubClassSet
		{
			// Token: 0x06005AB4 RID: 23220 RVA: 0x0016C2C8 File Offset: 0x0016B6C8
			public ushort RuleCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005AB5 RID: 23221 RVA: 0x0016C2E4 File Offset: 0x0016B6E4
			public ClassChainingSubtable.SubClassRule Rule(FontTable Table, ushort Index)
			{
				return new ClassChainingSubtable.SubClassRule(this.offset + (int)Table.GetUShort(this.offset + 2 + (int)(Index * 2)));
			}

			// Token: 0x17001282 RID: 4738
			// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x0016C310 File Offset: 0x0016B710
			public bool IsNull
			{
				get
				{
					return this.offset == int.MaxValue;
				}
			}

			// Token: 0x06005AB7 RID: 23223 RVA: 0x0016C32C File Offset: 0x0016B72C
			public SubClassSet(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DB4 RID: 11700
			private const int offsetRuleCount = 0;

			// Token: 0x04002DB5 RID: 11701
			private const int offsetRuleArray = 2;

			// Token: 0x04002DB6 RID: 11702
			private const int sizeRuleOffset = 2;

			// Token: 0x04002DB7 RID: 11703
			private int offset;
		}

		// Token: 0x020009BA RID: 2490
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubClassRule
		{
			// Token: 0x06005AB8 RID: 23224 RVA: 0x0016C348 File Offset: 0x0016B748
			public static ushort GlyphCount(FontTable Table, int Offset)
			{
				return Table.GetUShort(Offset);
			}

			// Token: 0x06005AB9 RID: 23225 RVA: 0x0016C35C File Offset: 0x0016B75C
			public static ushort ClassId(FontTable Table, int Offset)
			{
				return Table.GetUShort(Offset);
			}

			// Token: 0x06005ABA RID: 23226 RVA: 0x0016C370 File Offset: 0x0016B770
			public ContextualLookupRecords ContextualLookups(FontTable Table, int CurrentOffset)
			{
				return new ContextualLookupRecords(CurrentOffset + 2, Table.GetUShort(CurrentOffset));
			}

			// Token: 0x06005ABB RID: 23227 RVA: 0x0016C38C File Offset: 0x0016B78C
			public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, ClassDefTable inputClassDef, ClassDefTable backtrackClassDef, ClassDefTable lookaheadClassDef, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
			{
				bool flag = true;
				NextGlyph = FirstGlyph + 1;
				int num = this.offset;
				int num2 = (int)ClassChainingSubtable.SubClassRule.GlyphCount(Table, num);
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
						ushort num5 = ClassChainingSubtable.SubClassRule.ClassId(Table, num);
						num += 2;
						ushort @class = backtrackClassDef.GetClass(Table, GlyphInfo.Glyphs[num3]);
						flag = (@class == num5);
					}
					num4 += 1;
				}
				if (!flag)
				{
					return false;
				}
				int num6 = (int)ClassChainingSubtable.SubClassRule.GlyphCount(Table, num);
				num += 2;
				num3 = FirstGlyph;
				ushort num7 = 1;
				while ((int)num7 < num6 && flag)
				{
					num3 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num3 + 1, LookupFlags, 1);
					if (num3 >= AfterLastGlyph)
					{
						flag = false;
					}
					else
					{
						ushort num8 = ClassChainingSubtable.SubClassRule.ClassId(Table, num);
						num += 2;
						ushort class2 = inputClassDef.GetClass(Table, GlyphInfo.Glyphs[num3]);
						flag = (class2 == num8);
					}
					num7 += 1;
				}
				if (!flag)
				{
					return false;
				}
				int afterLastGlyph = num3 + 1;
				int num9 = (int)ClassChainingSubtable.SubClassRule.GlyphCount(Table, num);
				num += 2;
				ushort num10 = 0;
				while ((int)num10 < num9 && flag)
				{
					num3 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num3 + 1, LookupFlags, 1);
					if (num3 >= GlyphInfo.Length)
					{
						flag = false;
					}
					else
					{
						ushort num11 = ClassChainingSubtable.SubClassRule.ClassId(Table, num);
						num += 2;
						ushort class3 = lookaheadClassDef.GetClass(Table, GlyphInfo.Glyphs[num3]);
						flag = (class3 == num11);
					}
					num10 += 1;
				}
				if (flag)
				{
					this.ContextualLookups(Table, num).ApplyContextualLookups(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, afterLastGlyph, Parameter, nestingLevel, out NextGlyph);
				}
				return flag;
			}

			// Token: 0x06005ABC RID: 23228 RVA: 0x0016C520 File Offset: 0x0016B920
			public SubClassRule(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DB8 RID: 11704
			private const int sizeCount = 2;

			// Token: 0x04002DB9 RID: 11705
			private const int sizeClassId = 2;

			// Token: 0x04002DBA RID: 11706
			private int offset;
		}
	}
}
