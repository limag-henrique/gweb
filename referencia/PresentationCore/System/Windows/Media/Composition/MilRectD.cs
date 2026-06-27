using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000635 RID: 1589
	[StructLayout(LayoutKind.Explicit)]
	internal struct MilRectD
	{
		// Token: 0x060047F8 RID: 18424 RVA: 0x00119A74 File Offset: 0x00118E74
		internal MilRectD(double left, double top, double right, double bottom)
		{
			this._left = left;
			this._top = top;
			this._right = right;
			this._bottom = bottom;
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x00119AA0 File Offset: 0x00118EA0
		internal static MilRectD Empty
		{
			get
			{
				return new MilRectD(0.0, 0.0, 0.0, 0.0);
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x00119AD8 File Offset: 0x00118ED8
		internal static MilRectD NaN
		{
			get
			{
				return new MilRectD(double.NaN, double.NaN, double.NaN, double.NaN);
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x00119B10 File Offset: 0x00118F10
		internal Rect AsRect
		{
			get
			{
				if (this._right >= this._left && this._bottom >= this._top)
				{
					return new Rect(this._left, this._top, this._right - this._left, this._bottom - this._top);
				}
				return Rect.Empty;
			}
		}

		// Token: 0x04001B7D RID: 7037
		[FieldOffset(0)]
		internal double _left;

		// Token: 0x04001B7E RID: 7038
		[FieldOffset(8)]
		internal double _top;

		// Token: 0x04001B7F RID: 7039
		[FieldOffset(16)]
		internal double _right;

		// Token: 0x04001B80 RID: 7040
		[FieldOffset(24)]
		internal double _bottom;
	}
}
