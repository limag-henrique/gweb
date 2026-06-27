using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C5 RID: 1733
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ClassDefTable
	{
		// Token: 0x06004B6A RID: 19306 RVA: 0x00126BB8 File Offset: 0x00125FB8
		private ushort Format(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x00126BD4 File Offset: 0x00125FD4
		private ushort Format1StartGlyph(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x00126BF0 File Offset: 0x00125FF0
		private ushort Format1GlyphCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 4);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00126C0C File Offset: 0x0012600C
		private ushort Format1ClassValue(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 6 + (int)(Index * 2));
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x00126C2C File Offset: 0x0012602C
		private ushort Format2RangeCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00126C48 File Offset: 0x00126048
		private ushort Format2RangeStartGlyph(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 6));
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x00126C68 File Offset: 0x00126068
		private ushort Format2RangeEndGlyph(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 6) + 2);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00126C8C File Offset: 0x0012608C
		private ushort Format2RangeClassValue(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 6) + 4);
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00126CB0 File Offset: 0x001260B0
		public ushort GetClass(FontTable Table, ushort glyph)
		{
			ushort num = this.Format(Table);
			if (num != 1)
			{
				if (num != 2)
				{
					return 0;
				}
				ushort num2 = 0;
				ushort num3 = this.Format2RangeCount(Table);
				while (num2 < num3)
				{
					ushort num4 = (ushort)(num2 + num3 >> 1);
					if (glyph < this.Format2RangeStartGlyph(Table, num4))
					{
						num3 = num4;
					}
					else
					{
						if (glyph <= this.Format2RangeEndGlyph(Table, num4))
						{
							return this.Format2RangeClassValue(Table, num4);
						}
						num2 = num4 + 1;
					}
				}
				return 0;
			}
			else
			{
				ushort num5 = this.Format1StartGlyph(Table);
				ushort num6 = this.Format1GlyphCount(Table);
				if (glyph >= num5 && glyph - num5 < num6)
				{
					return this.Format1ClassValue(Table, glyph - num5);
				}
				return 0;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06004B73 RID: 19315 RVA: 0x00126D40 File Offset: 0x00126140
		public static ClassDefTable InvalidClassDef
		{
			get
			{
				return new ClassDefTable(-1);
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06004B74 RID: 19316 RVA: 0x00126D54 File Offset: 0x00126154
		public bool IsInvalid
		{
			get
			{
				return this.offset == -1;
			}
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x00126D6C File Offset: 0x0012616C
		public ClassDefTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002048 RID: 8264
		private const int offsetFormat = 0;

		// Token: 0x04002049 RID: 8265
		private const int offsetFormat1StartGlyph = 2;

		// Token: 0x0400204A RID: 8266
		private const int offsetFormat1GlyphCount = 4;

		// Token: 0x0400204B RID: 8267
		private const int offsetFormat1ClassValueArray = 6;

		// Token: 0x0400204C RID: 8268
		private const int sizeFormat1ClassValue = 2;

		// Token: 0x0400204D RID: 8269
		private const int offsetFormat2RangeCount = 2;

		// Token: 0x0400204E RID: 8270
		private const int offsetFormat2RangeRecordArray = 4;

		// Token: 0x0400204F RID: 8271
		private const int sizeFormat2RangeRecord = 6;

		// Token: 0x04002050 RID: 8272
		private const int offsetFormat2RangeRecordStart = 0;

		// Token: 0x04002051 RID: 8273
		private const int offsetFormat2RangeRecordEnd = 2;

		// Token: 0x04002052 RID: 8274
		private const int offsetFormat2RangeRecordClass = 4;

		// Token: 0x04002053 RID: 8275
		private int offset;
	}
}
