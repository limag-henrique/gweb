using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E6 RID: 1766
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct MultipleSubstitutionSequenceTable
	{
		// Token: 0x06004C3C RID: 19516 RVA: 0x0012AA30 File Offset: 0x00129E30
		public ushort GlyphCount(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004C3D RID: 19517 RVA: 0x0012AA4C File Offset: 0x00129E4C
		public ushort Glyph(FontTable Table, ushort index)
		{
			return Table.GetUShort(this.offset + 2 + (int)(index * 2));
		}

		// Token: 0x06004C3E RID: 19518 RVA: 0x0012AA6C File Offset: 0x00129E6C
		public MultipleSubstitutionSequenceTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002118 RID: 8472
		private const int offsetGlyphCount = 0;

		// Token: 0x04002119 RID: 8473
		private const int offsetGlyphArray = 2;

		// Token: 0x0400211A RID: 8474
		private const int sizeGlyphId = 2;

		// Token: 0x0400211B RID: 8475
		private int offset;
	}
}
