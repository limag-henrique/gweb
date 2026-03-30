using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006DA RID: 1754
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct MarkArray
	{
		// Token: 0x06004BDF RID: 19423 RVA: 0x00129184 File Offset: 0x00128584
		public ushort Class(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 2 + (int)(Index * 4));
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x001291A4 File Offset: 0x001285A4
		public AnchorTable MarkAnchor(FontTable Table, ushort Index)
		{
			int @ushort = (int)Table.GetUShort(this.offset + 2 + (int)(Index * 4) + 2);
			if (@ushort == 0)
			{
				return new AnchorTable(Table, 0);
			}
			return new AnchorTable(Table, this.offset + @ushort);
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x001291E0 File Offset: 0x001285E0
		public MarkArray(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x040020D7 RID: 8407
		private const int offsetClassArray = 2;

		// Token: 0x040020D8 RID: 8408
		private const int sizeClassRecord = 4;

		// Token: 0x040020D9 RID: 8409
		private const int offsetClassRecordClass = 0;

		// Token: 0x040020DA RID: 8410
		private const int offsetClassRecordAnchor = 2;

		// Token: 0x040020DB RID: 8411
		private int offset;
	}
}
