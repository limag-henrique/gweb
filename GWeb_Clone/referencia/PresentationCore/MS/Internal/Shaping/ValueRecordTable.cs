using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D6 RID: 1750
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ValueRecordTable
	{
		// Token: 0x06004BB9 RID: 19385 RVA: 0x00128608 File Offset: 0x00127A08
		public static ushort Size(ushort Format)
		{
			return ValueRecordTable.BitCount[(int)(Format & 15)] + ValueRecordTable.BitCount[Format >> 4 & 15];
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x00128630 File Offset: 0x00127A30
		public void AdjustPos(FontTable Table, LayoutMetrics Metrics, ref LayoutOffset GlyphOffset, ref int GlyphAdvance)
		{
			int num = this.offset;
			if ((this.format & 1) != 0)
			{
				GlyphOffset.dx += Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmWidth, (int)Table.GetShort(num));
				num += 2;
			}
			if ((this.format & 2) != 0)
			{
				GlyphOffset.dy += Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmHeight, (int)Table.GetShort(num));
				num += 2;
			}
			if ((this.format & 4) != 0)
			{
				GlyphAdvance += Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmWidth, (int)Table.GetShort(num));
				num += 2;
			}
			if ((this.format & 8) != 0)
			{
				GlyphAdvance += Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmHeight, (int)Table.GetShort(num));
				num += 2;
			}
			if ((this.format & 16) != 0)
			{
				int num2 = (int)Table.GetOffset(num);
				if (num2 != 0)
				{
					DeviceTable deviceTable = new DeviceTable(this.baseTableOffset + num2);
					GlyphOffset.dx += deviceTable.Value(Table, Metrics.PixelsEmWidth);
				}
				num += 2;
			}
			if ((this.format & 32) != 0)
			{
				int num3 = (int)Table.GetOffset(num);
				if (num3 != 0)
				{
					DeviceTable deviceTable2 = new DeviceTable(this.baseTableOffset + num3);
					GlyphOffset.dy += deviceTable2.Value(Table, Metrics.PixelsEmHeight);
				}
				num += 2;
			}
			if ((this.format & 64) != 0)
			{
				if (Metrics.Direction == TextFlowDirection.LTR || Metrics.Direction == TextFlowDirection.RTL)
				{
					int num4 = (int)Table.GetOffset(num);
					if (num4 != 0)
					{
						DeviceTable deviceTable3 = new DeviceTable(this.baseTableOffset + num4);
						GlyphAdvance += deviceTable3.Value(Table, Metrics.PixelsEmWidth);
					}
				}
				num += 2;
			}
			if ((this.format & 128) != 0)
			{
				if (Metrics.Direction == TextFlowDirection.TTB || Metrics.Direction == TextFlowDirection.BTT)
				{
					int num5 = (int)Table.GetOffset(num);
					if (num5 != 0)
					{
						DeviceTable deviceTable4 = new DeviceTable(this.baseTableOffset + num5);
						GlyphAdvance += deviceTable4.Value(Table, Metrics.PixelsEmHeight);
					}
				}
				num += 2;
			}
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x00128828 File Offset: 0x00127C28
		public ValueRecordTable(int Offset, int BaseTableOffset, ushort Format)
		{
			this.offset = Offset;
			this.baseTableOffset = BaseTableOffset;
			this.format = Format;
		}

		// Token: 0x040020AF RID: 8367
		private const ushort XPlacmentFlag = 1;

		// Token: 0x040020B0 RID: 8368
		private const ushort YPlacmentFlag = 2;

		// Token: 0x040020B1 RID: 8369
		private const ushort XAdvanceFlag = 4;

		// Token: 0x040020B2 RID: 8370
		private const ushort YAdvanceFlag = 8;

		// Token: 0x040020B3 RID: 8371
		private const ushort XPlacementDeviceFlag = 16;

		// Token: 0x040020B4 RID: 8372
		private const ushort YPlacementDeviceFlag = 32;

		// Token: 0x040020B5 RID: 8373
		private const ushort XAdvanceDeviceFlag = 64;

		// Token: 0x040020B6 RID: 8374
		private const ushort YAdvanceDeviceFlag = 128;

		// Token: 0x040020B7 RID: 8375
		private static ushort[] BitCount = new ushort[]
		{
			0,
			2,
			2,
			4,
			2,
			4,
			4,
			6,
			2,
			4,
			4,
			6,
			4,
			6,
			6,
			8
		};

		// Token: 0x040020B8 RID: 8376
		private ushort format;

		// Token: 0x040020B9 RID: 8377
		private int baseTableOffset;

		// Token: 0x040020BA RID: 8378
		private int offset;
	}
}
