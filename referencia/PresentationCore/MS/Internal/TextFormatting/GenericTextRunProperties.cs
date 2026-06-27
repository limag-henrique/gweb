using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000707 RID: 1799
	internal sealed class GenericTextRunProperties : TextRunProperties
	{
		// Token: 0x06004D74 RID: 19828 RVA: 0x001330C8 File Offset: 0x001324C8
		public GenericTextRunProperties(Typeface typeface, double size, double hintingSize, double pixelsPerDip, TextDecorationCollection textDecorations, Brush foregroundBrush, Brush backgroundBrush, BaselineAlignment baselineAlignment, CultureInfo culture, NumberSubstitution substitution)
		{
			this._typeface = typeface;
			this._emSize = size;
			this._emHintingSize = hintingSize;
			this._textDecorations = textDecorations;
			this._foregroundBrush = foregroundBrush;
			this._backgroundBrush = backgroundBrush;
			this._baselineAlignment = baselineAlignment;
			this._culture = culture;
			this._numberSubstitution = substitution;
			base.PixelsPerDip = pixelsPerDip;
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x00133128 File Offset: 0x00132528
		public override int GetHashCode()
		{
			return this._typeface.GetHashCode() ^ this._emSize.GetHashCode() ^ this._emHintingSize.GetHashCode() ^ ((this._foregroundBrush == null) ? 0 : this._foregroundBrush.GetHashCode()) ^ ((this._backgroundBrush == null) ? 0 : this._backgroundBrush.GetHashCode()) ^ ((this._textDecorations == null) ? 0 : this._textDecorations.GetHashCode()) ^ (int)((int)this._baselineAlignment << 3) ^ this._culture.GetHashCode() << 6 ^ ((this._numberSubstitution == null) ? 0 : this._numberSubstitution.GetHashCode());
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x001331CC File Offset: 0x001325CC
		public override bool Equals(object o)
		{
			if (o == null || !(o is TextRunProperties))
			{
				return false;
			}
			TextRunProperties textRunProperties = (TextRunProperties)o;
			if (this._emSize != textRunProperties.FontRenderingEmSize || this._emHintingSize != textRunProperties.FontHintingEmSize || this._culture != textRunProperties.CultureInfo || !this._typeface.Equals(textRunProperties.Typeface) || !((this._textDecorations == null) ? (textRunProperties.TextDecorations == null) : this._textDecorations.ValueEquals(textRunProperties.TextDecorations)) || this._baselineAlignment != textRunProperties.BaselineAlignment || !((this._foregroundBrush == null) ? (textRunProperties.ForegroundBrush == null) : this._foregroundBrush.Equals(textRunProperties.ForegroundBrush)) || !((this._backgroundBrush == null) ? (textRunProperties.BackgroundBrush == null) : this._backgroundBrush.Equals(textRunProperties.BackgroundBrush)))
			{
				return false;
			}
			if (this._numberSubstitution != null)
			{
				return this._numberSubstitution.Equals(textRunProperties.NumberSubstitution);
			}
			return textRunProperties.NumberSubstitution == null;
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x001332E0 File Offset: 0x001326E0
		public override Typeface Typeface
		{
			get
			{
				return this._typeface;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x001332F4 File Offset: 0x001326F4
		public override double FontRenderingEmSize
		{
			get
			{
				return this._emSize;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x00133308 File Offset: 0x00132708
		public override double FontHintingEmSize
		{
			get
			{
				return this._emHintingSize;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x0013331C File Offset: 0x0013271C
		public override TextDecorationCollection TextDecorations
		{
			get
			{
				return this._textDecorations;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06004D7B RID: 19835 RVA: 0x00133330 File Offset: 0x00132730
		public override Brush ForegroundBrush
		{
			get
			{
				return this._foregroundBrush;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06004D7C RID: 19836 RVA: 0x00133344 File Offset: 0x00132744
		public override Brush BackgroundBrush
		{
			get
			{
				return this._backgroundBrush;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06004D7D RID: 19837 RVA: 0x00133358 File Offset: 0x00132758
		public override BaselineAlignment BaselineAlignment
		{
			get
			{
				return this._baselineAlignment;
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06004D7E RID: 19838 RVA: 0x0013336C File Offset: 0x0013276C
		public override CultureInfo CultureInfo
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06004D7F RID: 19839 RVA: 0x00133380 File Offset: 0x00132780
		public override TextRunTypographyProperties TypographyProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x00133390 File Offset: 0x00132790
		public override TextEffectCollection TextEffects
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06004D81 RID: 19841 RVA: 0x001333A0 File Offset: 0x001327A0
		public override NumberSubstitution NumberSubstitution
		{
			get
			{
				return this._numberSubstitution;
			}
		}

		// Token: 0x040021B1 RID: 8625
		private Typeface _typeface;

		// Token: 0x040021B2 RID: 8626
		private double _emSize;

		// Token: 0x040021B3 RID: 8627
		private double _emHintingSize;

		// Token: 0x040021B4 RID: 8628
		private TextDecorationCollection _textDecorations;

		// Token: 0x040021B5 RID: 8629
		private Brush _foregroundBrush;

		// Token: 0x040021B6 RID: 8630
		private Brush _backgroundBrush;

		// Token: 0x040021B7 RID: 8631
		private BaselineAlignment _baselineAlignment;

		// Token: 0x040021B8 RID: 8632
		private CultureInfo _culture;

		// Token: 0x040021B9 RID: 8633
		private NumberSubstitution _numberSubstitution;
	}
}
