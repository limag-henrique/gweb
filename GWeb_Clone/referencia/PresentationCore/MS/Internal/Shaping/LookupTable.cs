using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C3 RID: 1731
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct LookupTable
	{
		// Token: 0x06004B59 RID: 19289 RVA: 0x00126818 File Offset: 0x00125C18
		public ushort LookupType()
		{
			return this.lookupType;
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x0012682C File Offset: 0x00125C2C
		public ushort LookupFlags()
		{
			return this.lookupFlags;
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x00126840 File Offset: 0x00125C40
		public ushort SubTableCount()
		{
			return this.subtableCount;
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00126854 File Offset: 0x00125C54
		public int SubtableOffset(FontTable Table, ushort Index)
		{
			return this.offset + (int)Table.GetOffset(this.offset + 6 + (int)(Index * 2));
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x0012687C File Offset: 0x00125C7C
		public LookupTable(FontTable table, int Offset)
		{
			this.offset = Offset;
			this.lookupType = table.GetUShort(this.offset);
			this.lookupFlags = table.GetUShort(this.offset + 2);
			this.subtableCount = table.GetUShort(this.offset + 4);
		}

		// Token: 0x04002034 RID: 8244
		private const int offsetLookupType = 0;

		// Token: 0x04002035 RID: 8245
		private const int offsetLookupFlags = 2;

		// Token: 0x04002036 RID: 8246
		private const int offsetSubtableCount = 4;

		// Token: 0x04002037 RID: 8247
		private const int offsetSubtableArray = 6;

		// Token: 0x04002038 RID: 8248
		private const int sizeSubtableOffset = 2;

		// Token: 0x04002039 RID: 8249
		private int offset;

		// Token: 0x0400203A RID: 8250
		private ushort lookupType;

		// Token: 0x0400203B RID: 8251
		private ushort lookupFlags;

		// Token: 0x0400203C RID: 8252
		private ushort subtableCount;
	}
}
