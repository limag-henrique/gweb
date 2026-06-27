using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D7 RID: 1751
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct AnchorTable
	{
		// Token: 0x06004BBD RID: 19389 RVA: 0x00128870 File Offset: 0x00127C70
		private short XCoordinate(FontTable Table)
		{
			return Table.GetShort(this.offset + 2);
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0012888C File Offset: 0x00127C8C
		private short YCoordinate(FontTable Table)
		{
			return Table.GetShort(this.offset + 4);
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x001288A8 File Offset: 0x00127CA8
		private ushort Format2AnchorPoint(FontTable Table)
		{
			Invariant.Assert(this.format == 2);
			return Table.GetUShort(this.offset + 6);
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x001288D4 File Offset: 0x00127CD4
		private DeviceTable Format3XDeviceTable(FontTable Table)
		{
			Invariant.Assert(this.format == 3);
			int @ushort = (int)Table.GetUShort(this.offset + 6);
			if (@ushort != 0)
			{
				return new DeviceTable(this.offset + @ushort);
			}
			return new DeviceTable(0);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x00128918 File Offset: 0x00127D18
		private DeviceTable Format3YDeviceTable(FontTable Table)
		{
			Invariant.Assert(this.format == 3);
			int @ushort = (int)Table.GetUShort(this.offset + 8);
			if (@ushort != 0)
			{
				return new DeviceTable(this.offset + @ushort);
			}
			return new DeviceTable(0);
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0012895C File Offset: 0x00127D5C
		public bool NeedContourPoint(FontTable Table)
		{
			return this.format == 2;
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x00128974 File Offset: 0x00127D74
		public ushort ContourPointIndex(FontTable Table)
		{
			Invariant.Assert(this.NeedContourPoint(Table));
			return this.Format2AnchorPoint(Table);
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x00128994 File Offset: 0x00127D94
		public LayoutOffset AnchorCoordinates(FontTable Table, LayoutMetrics Metrics, LayoutOffset ContourPoint)
		{
			LayoutOffset result = default(LayoutOffset);
			switch (this.format)
			{
			case 1:
				result.dx = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmWidth, (int)this.XCoordinate(Table));
				result.dy = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmHeight, (int)this.YCoordinate(Table));
				break;
			case 2:
				if (ContourPoint.dx == -2147483648)
				{
					result.dx = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmWidth, (int)this.XCoordinate(Table));
					result.dy = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmHeight, (int)this.YCoordinate(Table));
				}
				else
				{
					result.dx = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmWidth, ContourPoint.dx);
					result.dy = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmWidth, ContourPoint.dy);
				}
				break;
			case 3:
				result.dx = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmWidth, (int)this.XCoordinate(Table)) + this.Format3XDeviceTable(Table).Value(Table, Metrics.PixelsEmWidth);
				result.dy = Positioning.DesignToPixels(Metrics.DesignEmHeight, Metrics.PixelsEmHeight, (int)this.YCoordinate(Table)) + this.Format3YDeviceTable(Table).Value(Table, Metrics.PixelsEmHeight);
				break;
			default:
				result.dx = 0;
				result.dx = 0;
				break;
			}
			return result;
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x00128B1C File Offset: 0x00127F1C
		public AnchorTable(FontTable Table, int Offset)
		{
			this.offset = Offset;
			if (this.offset != 0)
			{
				this.format = Table.GetUShort(this.offset);
				return;
			}
			this.format = 0;
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x00128B54 File Offset: 0x00127F54
		public bool IsNull()
		{
			return this.offset == 0;
		}

		// Token: 0x040020BB RID: 8379
		private const int offsetFormat = 0;

		// Token: 0x040020BC RID: 8380
		private const int offsetXCoordinate = 2;

		// Token: 0x040020BD RID: 8381
		private const int offsetYCoordinate = 4;

		// Token: 0x040020BE RID: 8382
		private const int offsetFormat2AnchorPoint = 6;

		// Token: 0x040020BF RID: 8383
		private const int offsetFormat3XDeviceTable = 6;

		// Token: 0x040020C0 RID: 8384
		private const int offsetFormat3YDeviceTable = 8;

		// Token: 0x040020C1 RID: 8385
		private int offset;

		// Token: 0x040020C2 RID: 8386
		private ushort format;
	}
}
