using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C2 RID: 1730
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct LookupList
	{
		// Token: 0x06004B56 RID: 19286 RVA: 0x001267BC File Offset: 0x00125BBC
		public ushort LookupCount(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x001267D8 File Offset: 0x00125BD8
		public LookupTable Lookup(FontTable Table, ushort Index)
		{
			return new LookupTable(Table, this.offset + (int)Table.GetUShort(this.offset + 2 + (int)(Index * 2)));
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00126804 File Offset: 0x00125C04
		public LookupList(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002030 RID: 8240
		private const int offsetLookupCount = 0;

		// Token: 0x04002031 RID: 8241
		private const int LookupOffsetArray = 2;

		// Token: 0x04002032 RID: 8242
		private const int sizeLookupOffset = 2;

		// Token: 0x04002033 RID: 8243
		private int offset;
	}
}
