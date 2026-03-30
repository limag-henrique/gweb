using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006BC RID: 1724
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct GDEFHeader
	{
		// Token: 0x06004B38 RID: 19256 RVA: 0x00126328 File Offset: 0x00125728
		public ClassDefTable GetGlyphClassDef(FontTable Table)
		{
			Invariant.Assert(Table.IsPresent);
			return new ClassDefTable(this.offset + (int)Table.GetOffset(this.offset + 4));
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x0012635C File Offset: 0x0012575C
		public ClassDefTable GetMarkAttachClassDef(FontTable Table)
		{
			Invariant.Assert(Table.IsPresent);
			return new ClassDefTable(this.offset + (int)Table.GetOffset(this.offset + 10));
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00126390 File Offset: 0x00125790
		public GDEFHeader(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x0400200F RID: 8207
		private const int offsetGlyphClassDef = 4;

		// Token: 0x04002010 RID: 8208
		private const int offsetGlyphAttachList = 6;

		// Token: 0x04002011 RID: 8209
		private const int offsetLigaCaretList = 8;

		// Token: 0x04002012 RID: 8210
		private const int offsetMarkAttachClassDef = 10;

		// Token: 0x04002013 RID: 8211
		private int offset;
	}
}
