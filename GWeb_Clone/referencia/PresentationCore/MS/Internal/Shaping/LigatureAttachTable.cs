using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006DE RID: 1758
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct LigatureAttachTable
	{
		// Token: 0x06004BFE RID: 19454 RVA: 0x0012993C File Offset: 0x00128D3C
		public AnchorTable LigatureAnchor(FontTable Table, ushort Component, ushort MarkClass)
		{
			int @ushort = (int)Table.GetUShort(this.offset + 2 + ((int)Component * this.classCount + (int)MarkClass) * 2);
			if (@ushort == 0)
			{
				return new AnchorTable(Table, 0);
			}
			return new AnchorTable(Table, this.offset + @ushort);
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x00129980 File Offset: 0x00128D80
		public LigatureAttachTable(int Offset, ushort ClassCount)
		{
			this.offset = Offset;
			this.classCount = (int)ClassCount;
		}

		// Token: 0x040020F2 RID: 8434
		private const int offsetAnchorArray = 2;

		// Token: 0x040020F3 RID: 8435
		private const int sizeAnchorOffset = 2;

		// Token: 0x040020F4 RID: 8436
		private int offset;

		// Token: 0x040020F5 RID: 8437
		private int classCount;
	}
}
