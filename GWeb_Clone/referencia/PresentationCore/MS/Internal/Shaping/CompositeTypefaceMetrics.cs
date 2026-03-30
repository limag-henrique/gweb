using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using MS.Internal.FontFace;

namespace MS.Internal.Shaping
{
	// Token: 0x020006AB RID: 1707
	internal class CompositeTypefaceMetrics : ITypefaceMetrics
	{
		// Token: 0x06004ABF RID: 19135 RVA: 0x00123A64 File Offset: 0x00122E64
		internal CompositeTypefaceMetrics(double underlinePosition, double underlineThickness, double strikethroughPosition, double strikethroughThickness, double capsHeight, double xHeight, FontStyle style, FontWeight weight, FontStretch stretch)
		{
			this._underlinePosition = ((underlinePosition != 0.0) ? underlinePosition : -0.15625);
			this._underlineThickness = ((underlineThickness > 0.0) ? underlineThickness : 0.078125);
			this._strikethroughPosition = ((strikethroughPosition != 0.0) ? strikethroughPosition : 0.3125);
			this._strikethroughThickenss = ((strikethroughThickness > 0.0) ? strikethroughThickness : 0.078125);
			this._capsHeight = ((capsHeight > 0.0) ? capsHeight : 1.0);
			this._xHeight = ((xHeight > 0.0) ? xHeight : 0.671875);
			this._style = style;
			this._weight = weight;
			this._stretch = stretch;
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x00123B4C File Offset: 0x00122F4C
		internal CompositeTypefaceMetrics() : this(-0.15625, 0.078125, 0.3125, 0.078125, 1.0, 0.671875, FontStyles.Normal, FontWeights.Regular, FontStretches.Normal)
		{
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06004AC1 RID: 19137 RVA: 0x00123BA4 File Offset: 0x00122FA4
		public double XHeight
		{
			get
			{
				return this._xHeight;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x00123BB8 File Offset: 0x00122FB8
		public double CapsHeight
		{
			get
			{
				return this._capsHeight;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06004AC3 RID: 19139 RVA: 0x00123BCC File Offset: 0x00122FCC
		public double UnderlinePosition
		{
			get
			{
				return this._underlinePosition;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06004AC4 RID: 19140 RVA: 0x00123BE0 File Offset: 0x00122FE0
		public double UnderlineThickness
		{
			get
			{
				return this._underlineThickness;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06004AC5 RID: 19141 RVA: 0x00123BF4 File Offset: 0x00122FF4
		public double StrikethroughPosition
		{
			get
			{
				return this._strikethroughPosition;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x00123C08 File Offset: 0x00123008
		public double StrikethroughThickness
		{
			get
			{
				return this._strikethroughThickenss;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06004AC7 RID: 19143 RVA: 0x00123C1C File Offset: 0x0012301C
		public bool Symbol
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x00123C2C File Offset: 0x0012302C
		public StyleSimulations StyleSimulations
		{
			get
			{
				return StyleSimulations.None;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x00123C3C File Offset: 0x0012303C
		public IDictionary<XmlLanguage, string> AdjustedFaceNames
		{
			get
			{
				return FontDifferentiator.ConstructFaceNamesByStyleWeightStretch(this._style, this._weight, this._stretch);
			}
		}

		// Token: 0x04001F9F RID: 8095
		private double _underlinePosition;

		// Token: 0x04001FA0 RID: 8096
		private double _underlineThickness;

		// Token: 0x04001FA1 RID: 8097
		private double _strikethroughPosition;

		// Token: 0x04001FA2 RID: 8098
		private double _strikethroughThickenss;

		// Token: 0x04001FA3 RID: 8099
		private double _capsHeight;

		// Token: 0x04001FA4 RID: 8100
		private double _xHeight;

		// Token: 0x04001FA5 RID: 8101
		private FontStyle _style;

		// Token: 0x04001FA6 RID: 8102
		private FontWeight _weight;

		// Token: 0x04001FA7 RID: 8103
		private FontStretch _stretch;

		// Token: 0x04001FA8 RID: 8104
		private const double UnderlineOffsetDefaultInEm = -0.15625;

		// Token: 0x04001FA9 RID: 8105
		private const double UnderlineSizeDefaultInEm = 0.078125;

		// Token: 0x04001FAA RID: 8106
		private const double StrikethroughOffsetDefaultInEm = 0.3125;

		// Token: 0x04001FAB RID: 8107
		private const double StrikethroughSizeDefaultInEm = 0.078125;

		// Token: 0x04001FAC RID: 8108
		private const double CapsHeightDefaultInEm = 1.0;

		// Token: 0x04001FAD RID: 8109
		private const double XHeightDefaultInEm = 0.671875;
	}
}
