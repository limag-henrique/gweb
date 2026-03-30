using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006AC RID: 1708
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ContextualLookupRecords
	{
		// Token: 0x06004ACA RID: 19146 RVA: 0x00123C60 File Offset: 0x00123060
		private ushort SequenceIndex(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + (int)(Index * 4));
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x00123C80 File Offset: 0x00123080
		private ushort LookupIndex(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + (int)(Index * 4) + 2);
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00123CA0 File Offset: 0x001230A0
		public unsafe void ApplyContextualLookups(IOpenTypeFont Font, OpenTypeTags TableTag, FontTable Table, LayoutMetrics Metrics, int CharCount, UshortList Charmap, GlyphInfoList GlyphInfo, int* Advances, LayoutOffset* Offsets, ushort LookupFlags, int FirstGlyph, int AfterLastGlyph, uint Parameter, int nestingLevel, out int nextGlyph)
		{
			if (nestingLevel >= 16)
			{
				nextGlyph = AfterLastGlyph;
				return;
			}
			LookupList lookupList;
			if (TableTag == OpenTypeTags.GSUB)
			{
				lookupList = new GSUBHeader(0).GetLookupList(Table);
			}
			else
			{
				lookupList = new GPOSHeader(0).GetLookupList(Table);
			}
			int num = -1;
			int num2 = -1;
			for (;;)
			{
				ushort num3 = ushort.MaxValue;
				ushort num4 = ushort.MaxValue;
				for (ushort num5 = 0; num5 < this.recordCount; num5 += 1)
				{
					ushort num6 = this.LookupIndex(Table, num5);
					ushort num7 = this.SequenceIndex(Table, num5);
					if ((int)num6 >= num && ((int)num6 != num || (int)num7 > num2) && (num6 < num3 || (num6 == num3 && num7 < num4)))
					{
						num3 = num6;
						num4 = num7;
					}
				}
				if (num3 == 65535)
				{
					break;
				}
				num = (int)num3;
				num2 = (int)num4;
				int num8 = FirstGlyph;
				int num9 = 0;
				while (num9 < (int)num4 && num8 < AfterLastGlyph)
				{
					num8 = LayoutEngine.GetNextGlyphInLookup(Font, GlyphInfo, num8 + 1, LookupFlags, 1);
					num9++;
				}
				if (num8 < AfterLastGlyph)
				{
					int length = GlyphInfo.Length;
					int num10;
					LayoutEngine.ApplyLookup(Font, TableTag, Table, Metrics, lookupList.Lookup(Table, num3), CharCount, Charmap, GlyphInfo, Advances, Offsets, num8, AfterLastGlyph, Parameter, nestingLevel + 1, out num10);
					AfterLastGlyph += GlyphInfo.Length - length;
				}
			}
			nextGlyph = AfterLastGlyph;
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x00123DD0 File Offset: 0x001231D0
		public ContextualLookupRecords(int Offset, ushort RecordCount)
		{
			this.offset = Offset;
			this.recordCount = RecordCount;
		}

		// Token: 0x04001FAE RID: 8110
		private const int offsetSequenceIndex = 0;

		// Token: 0x04001FAF RID: 8111
		private const int offsetLookupIndex = 2;

		// Token: 0x04001FB0 RID: 8112
		private const int sizeLookupRecord = 4;

		// Token: 0x04001FB1 RID: 8113
		private const int MaximumContextualLookupNestingLevel = 16;

		// Token: 0x04001FB2 RID: 8114
		private int offset;

		// Token: 0x04001FB3 RID: 8115
		private ushort recordCount;
	}
}
