using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B1 RID: 1713
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct GlyphContextSubtable
	{
		// Token: 0x06004AF1 RID: 19185 RVA: 0x00124748 File Offset: 0x00123B48
		public ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00124764 File Offset: 0x00123B64
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x0012478C File Offset: 0x00123B8C
		private GlyphContextSubtable.SubRuleSet RuleSet(FontTable Table, int Index)
		{
			return new GlyphContextSubtable.SubRuleSet(this.offset + (int)Table.GetUShort(this.offset + 6 + Index * 2));
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x001247B8 File Offset: 0x00123BB8
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
			GlyphContextSubtable.SubRuleSet subRuleSet = this.RuleSet(Table, glyphIndex);
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

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00124864 File Offset: 0x00123C64
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return true;
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00124874 File Offset: 0x00123C74
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00124888 File Offset: 0x00123C88
		public GlyphContextSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FCD RID: 8141
		private const int offsetFormat = 0;

		// Token: 0x04001FCE RID: 8142
		private const int offsetCoverage = 2;

		// Token: 0x04001FCF RID: 8143
		private const int offsetSubRuleSetCount = 4;

		// Token: 0x04001FD0 RID: 8144
		private const int offsetSubRuleSetArray = 6;

		// Token: 0x04001FD1 RID: 8145
		private const int sizeRuleSetOffset = 2;

		// Token: 0x04001FD2 RID: 8146
		private int offset;

		// Token: 0x020009BB RID: 2491
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubRuleSet
		{
			// Token: 0x06005ABD RID: 23229 RVA: 0x0016C53C File Offset: 0x0016B93C
			public ushort RuleCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005ABE RID: 23230 RVA: 0x0016C558 File Offset: 0x0016B958
			public GlyphContextSubtable.SubRule Rule(FontTable Table, ushort Index)
			{
				return new GlyphContextSubtable.SubRule(this.offset + (int)Table.GetUShort(this.offset + 2 + (int)(Index * 2)));
			}

			// Token: 0x06005ABF RID: 23231 RVA: 0x0016C584 File Offset: 0x0016B984
			public SubRuleSet(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DBB RID: 11707
			private const int offsetRuleCount = 0;

			// Token: 0x04002DBC RID: 11708
			private const int offsetRuleArray = 2;

			// Token: 0x04002DBD RID: 11709
			private const int sizeRuleOffset = 2;

			// Token: 0x04002DBE RID: 11710
			private int offset;
		}

		// Token: 0x020009BC RID: 2492
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubRule
		{
			// Token: 0x06005AC0 RID: 23232 RVA: 0x0016C5A0 File Offset: 0x0016B9A0
			public ushort GlyphCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005AC1 RID: 23233 RVA: 0x0016C5BC File Offset: 0x0016B9BC
			public ushort SubstCount(FontTable Table)
			{
				return Table.GetUShort(this.offset + 2);
			}

			// Token: 0x06005AC2 RID: 23234 RVA: 0x0016C5D8 File Offset: 0x0016B9D8
			public ushort GlyphId(FontTable Table, int Index)
			{
				return Table.GetUShort(this.offset + 4 + (Index - 1) * 2);
			}

			// Token: 0x06005AC3 RID: 23235 RVA: 0x0016C5FC File Offset: 0x0016B9FC
			public ContextualLookupRecords ContextualLookups(FontTable Table)
			{
				return new ContextualLookupRecords(this.offset + 4 + (int)((this.GlyphCount(Table) - 1) * 2), this.SubstCount(Table));
			}

			// Token: 0x06005AC4 RID: 23236 RVA: 0x0016C62C File Offset: 0x0016BA2C
			public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
			{
				bool flag = true;
				NextGlyph = FirstGlyph + 1;
				int num = (int)this.GlyphCount(Table);
				int num2 = FirstGlyph;
				ushort num3 = 1;
				while ((int)num3 < num && flag)
				{
					num2 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num2 + 1, LookupFlags, 1);
					flag = (num2 < AfterLastGlyph && this.GlyphId(Table, (int)num3) == GlyphInfo.Glyphs[num2]);
					num3 += 1;
				}
				if (flag)
				{
					this.ContextualLookups(Table).ApplyContextualLookups(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, num2 + 1, Parameter, nestingLevel, out NextGlyph);
				}
				return flag;
			}

			// Token: 0x06005AC5 RID: 23237 RVA: 0x0016C6C0 File Offset: 0x0016BAC0
			public SubRule(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DBF RID: 11711
			private const int offsetGlyphCount = 0;

			// Token: 0x04002DC0 RID: 11712
			private const int offsetSubstCount = 2;

			// Token: 0x04002DC1 RID: 11713
			private const int offsetInput = 4;

			// Token: 0x04002DC2 RID: 11714
			private const int sizeCount = 2;

			// Token: 0x04002DC3 RID: 11715
			private const int sizeGlyphId = 2;

			// Token: 0x04002DC4 RID: 11716
			private int offset;
		}
	}
}
