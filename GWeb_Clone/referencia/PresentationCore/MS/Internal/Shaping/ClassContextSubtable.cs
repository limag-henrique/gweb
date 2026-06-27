using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B2 RID: 1714
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ClassContextSubtable
	{
		// Token: 0x06004AF8 RID: 19192 RVA: 0x0012489C File Offset: 0x00123C9C
		public ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x001248B8 File Offset: 0x00123CB8
		private CoverageTable Coverage(FontTable Table)
		{
			return new CoverageTable(this.offset + (int)Table.GetUShort(this.offset + 2));
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x001248E0 File Offset: 0x00123CE0
		private ClassDefTable ClassDef(FontTable Table)
		{
			return new ClassDefTable(this.offset + (int)Table.GetUShort(this.offset + 4));
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00124908 File Offset: 0x00123D08
		private ushort ClassSetCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 6);
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00124924 File Offset: 0x00123D24
		private ClassContextSubtable.SubClassSet ClassSet(FontTable Table, ushort Index)
		{
			int @ushort = (int)Table.GetUShort(this.offset + 8 + (int)(Index * 2));
			if (@ushort == 0)
			{
				return new ClassContextSubtable.SubClassSet(int.MaxValue);
			}
			return new ClassContextSubtable.SubClassSet(this.offset + @ushort);
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00124960 File Offset: 0x00123D60
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
			ClassDefTable classDef = this.ClassDef(Table);
			ushort @class = classDef.GetClass(Table, glyph);
			if (@class >= this.ClassSetCount(Table))
			{
				return false;
			}
			ClassContextSubtable.SubClassSet subClassSet = this.ClassSet(Table, @class);
			if (subClassSet.IsNull)
			{
				return false;
			}
			ushort num = subClassSet.RuleCount(Table);
			bool flag = false;
			ushort num2 = 0;
			while (!flag && num2 < num)
			{
				flag = subClassSet.Rule(Table, num2).Apply(Font, TableTag, Table, Metrics, classDef, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, AfterLastGlyph, Parameter, nestingLevel, out NextGlyph);
				num2 += 1;
			}
			return flag;
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00124A34 File Offset: 0x00123E34
		public bool IsLookupCovered(FontTable table, uint[] glyphBits, ushort minGlyphId, ushort maxGlyphId)
		{
			return true;
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00124A44 File Offset: 0x00123E44
		public CoverageTable GetPrimaryCoverage(FontTable table)
		{
			return this.Coverage(table);
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x00124A58 File Offset: 0x00123E58
		public ClassContextSubtable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04001FD3 RID: 8147
		private const int offsetFormat = 0;

		// Token: 0x04001FD4 RID: 8148
		private const int offsetCoverage = 2;

		// Token: 0x04001FD5 RID: 8149
		private const int offsetClassDef = 4;

		// Token: 0x04001FD6 RID: 8150
		private const int offsetSubClassSetCount = 6;

		// Token: 0x04001FD7 RID: 8151
		private const int offsetSubClassSetArray = 8;

		// Token: 0x04001FD8 RID: 8152
		private const int sizeClassSetOffset = 2;

		// Token: 0x04001FD9 RID: 8153
		private int offset;

		// Token: 0x020009BD RID: 2493
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubClassSet
		{
			// Token: 0x06005AC6 RID: 23238 RVA: 0x0016C6DC File Offset: 0x0016BADC
			public ushort RuleCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005AC7 RID: 23239 RVA: 0x0016C6F8 File Offset: 0x0016BAF8
			public ClassContextSubtable.SubClassRule Rule(FontTable Table, ushort Index)
			{
				return new ClassContextSubtable.SubClassRule(this.offset + (int)Table.GetUShort(this.offset + 2 + (int)(Index * 2)));
			}

			// Token: 0x17001283 RID: 4739
			// (get) Token: 0x06005AC8 RID: 23240 RVA: 0x0016C724 File Offset: 0x0016BB24
			public bool IsNull
			{
				get
				{
					return this.offset == int.MaxValue;
				}
			}

			// Token: 0x06005AC9 RID: 23241 RVA: 0x0016C740 File Offset: 0x0016BB40
			public SubClassSet(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DC5 RID: 11717
			private const int offsetRuleCount = 0;

			// Token: 0x04002DC6 RID: 11718
			private const int offsetRuleArray = 2;

			// Token: 0x04002DC7 RID: 11719
			private const int sizeRuleOffset = 2;

			// Token: 0x04002DC8 RID: 11720
			private int offset;
		}

		// Token: 0x020009BE RID: 2494
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private class SubClassRule
		{
			// Token: 0x06005ACA RID: 23242 RVA: 0x0016C75C File Offset: 0x0016BB5C
			public ushort GlyphCount(FontTable Table)
			{
				return Table.GetUShort(this.offset);
			}

			// Token: 0x06005ACB RID: 23243 RVA: 0x0016C778 File Offset: 0x0016BB78
			public ushort ClassId(FontTable Table, int Index)
			{
				return Table.GetUShort(this.offset + 4 + (Index - 1) * 2);
			}

			// Token: 0x06005ACC RID: 23244 RVA: 0x0016C79C File Offset: 0x0016BB9C
			public ushort SubstCount(FontTable Table)
			{
				return Table.GetUShort(this.offset + 2);
			}

			// Token: 0x06005ACD RID: 23245 RVA: 0x0016C7B8 File Offset: 0x0016BBB8
			public ContextualLookupRecords ContextualLookups(FontTable Table)
			{
				return new ContextualLookupRecords(this.offset + 4 + (int)((this.GlyphCount(Table) - 1) * 2), this.SubstCount(Table));
			}

			// Token: 0x06005ACE RID: 23246 RVA: 0x0016C7E8 File Offset: 0x0016BBE8
			public unsafe bool Apply(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, ClassDefTable ClassDef, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int NextGlyph)
			{
				NextGlyph = FirstGlyph + 1;
				bool flag = true;
				int num = FirstGlyph;
				int num2 = (int)this.GlyphCount(Table);
				ushort num3 = 1;
				while ((int)num3 < num2 && flag)
				{
					num = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num + 1, LookupFlags, 1);
					if (num >= AfterLastGlyph)
					{
						flag = false;
					}
					else
					{
						ushort num4 = this.ClassId(Table, (int)num3);
						ushort @class = ClassDef.GetClass(Table, GlyphInfo.Glyphs[num]);
						flag = (@class == num4);
					}
					num3 += 1;
				}
				if (flag)
				{
					this.ContextualLookups(Table).ApplyContextualLookups(Font, TableTag, Table, Metrics, CharCount, Charmap, GlyphInfo, Advances, Offsets, LookupFlags, FirstGlyph, num + 1, Parameter, nestingLevel, out NextGlyph);
				}
				return flag;
			}

			// Token: 0x06005ACF RID: 23247 RVA: 0x0016C88C File Offset: 0x0016BC8C
			public SubClassRule(int Offset)
			{
				this.offset = Offset;
			}

			// Token: 0x04002DC9 RID: 11721
			private const int offsetGlyphCount = 0;

			// Token: 0x04002DCA RID: 11722
			private const int offsetSubstCount = 2;

			// Token: 0x04002DCB RID: 11723
			private const int offsetInputSequence = 4;

			// Token: 0x04002DCC RID: 11724
			private const int sizeCount = 2;

			// Token: 0x04002DCD RID: 11725
			private const int sizeClassId = 2;

			// Token: 0x04002DCE RID: 11726
			private int offset;
		}
	}
}
