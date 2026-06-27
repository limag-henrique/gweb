using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D5 RID: 1749
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct DeviceTable
	{
		// Token: 0x06004BB2 RID: 19378 RVA: 0x001284B0 File Offset: 0x001278B0
		private ushort StartSize(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x001284CC File Offset: 0x001278CC
		private ushort EndSize(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x001284E8 File Offset: 0x001278E8
		private ushort DeltaFormat(FontTable Table)
		{
			return Table.GetUShort(this.offset + 4);
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x00128504 File Offset: 0x00127904
		private ushort DeltaValue(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 6 + (int)(Index * 2));
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x00128524 File Offset: 0x00127924
		public int Value(FontTable Table, ushort PixelsPerEm)
		{
			if (this.IsNull())
			{
				return 0;
			}
			ushort num = this.StartSize(Table);
			ushort num2 = this.EndSize(Table);
			if (PixelsPerEm < num || PixelsPerEm > num2)
			{
				return 0;
			}
			ushort num3 = PixelsPerEm - num;
			ushort index;
			ushort num4;
			ushort num5;
			switch (this.DeltaFormat(Table))
			{
			case 1:
				index = (ushort)(num3 >> 3);
				num4 = 16 + 2 * (num3 & 7);
				num5 = 30;
				break;
			case 2:
				index = (ushort)(num3 >> 2);
				num4 = 16 + 4 * (num3 & 3);
				num5 = 28;
				break;
			case 3:
				index = (ushort)(num3 >> 1);
				num4 = 16 + 8 * (num3 & 1);
				num5 = 24;
				break;
			default:
				return 0;
			}
			int num6 = (int)this.DeltaValue(Table, index);
			num6 <<= (int)num4;
			return num6 >> (int)num5;
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x001285DC File Offset: 0x001279DC
		public DeviceTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x001285F0 File Offset: 0x001279F0
		private bool IsNull()
		{
			return this.offset == 0;
		}

		// Token: 0x040020A9 RID: 8361
		private const int offsetStartSize = 0;

		// Token: 0x040020AA RID: 8362
		private const int offsetEndSize = 2;

		// Token: 0x040020AB RID: 8363
		private const int offsetDeltaFormat = 4;

		// Token: 0x040020AC RID: 8364
		private const int offsetDeltaValueArray = 6;

		// Token: 0x040020AD RID: 8365
		private const int sizeDeltaValue = 2;

		// Token: 0x040020AE RID: 8366
		private int offset;
	}
}
