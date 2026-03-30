using System;
using System.Windows;

namespace MS.Internal.Ink
{
	// Token: 0x020007C1 RID: 1985
	internal struct StrokeNodeData
	{
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x001510E0 File Offset: 0x001504E0
		internal static StrokeNodeData Empty
		{
			get
			{
				return StrokeNodeData.s_empty;
			}
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x001510F4 File Offset: 0x001504F4
		internal StrokeNodeData(Point position)
		{
			this._position = position;
			this._pressure = 1f;
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00151114 File Offset: 0x00150514
		internal StrokeNodeData(Point position, float pressure)
		{
			this._position = position;
			this._pressure = pressure;
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060053B0 RID: 21424 RVA: 0x00151130 File Offset: 0x00150530
		internal bool IsEmpty
		{
			get
			{
				return DoubleUtil.AreClose((double)this._pressure, (double)StrokeNodeData.s_empty._pressure);
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060053B1 RID: 21425 RVA: 0x00151154 File Offset: 0x00150554
		internal Point Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x00151168 File Offset: 0x00150568
		internal float PressureFactor
		{
			get
			{
				return this._pressure;
			}
		}

		// Token: 0x040025C8 RID: 9672
		private static StrokeNodeData s_empty;

		// Token: 0x040025C9 RID: 9673
		private Point _position;

		// Token: 0x040025CA RID: 9674
		private float _pressure;
	}
}
