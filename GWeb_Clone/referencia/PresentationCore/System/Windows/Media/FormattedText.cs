using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media.TextFormatting;
using MS.Internal;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media
{
	/// <summary>Fornece controle de baixo nível para desenhar texto em aplicativos WPF (Windows Presentation Foundation).</summary>
	// Token: 0x0200039A RID: 922
	public class FormattedText
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FormattedText" /> com o texto, cultura, direção do fluxo, face de tipos, tamanho da fonte e pincel especificados.</summary>
		/// <param name="textToFormat">O texto a ser exibido.</param>
		/// <param name="culture">A cultura específica do texto.</param>
		/// <param name="flowDirection">A direção em que o texto é lido.</param>
		/// <param name="typeface">A família, espessura, estilo e ampliação da fonte com os quais o texto deve ser formatado.</param>
		/// <param name="emSize">O tamanho da fonte de formatação do texto.</param>
		/// <param name="foreground">O pincel usado para pintar cada glifo.</param>
		// Token: 0x0600227C RID: 8828 RVA: 0x0008B150 File Offset: 0x0008A550
		[Obsolete("Use the PixelsPerDip override", false)]
		public FormattedText(string textToFormat, CultureInfo culture, FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground) : this(textToFormat, culture, flowDirection, typeface, emSize, foreground, null, TextFormattingMode.Ideal)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FormattedText" /> com o texto, cultura, direção do fluxo, face de tipos, tamanho da fonte, pincel de primeiro plano e valor pixelsPerDip especificados.</summary>
		/// <param name="textToFormat">O texto a ser exibido.</param>
		/// <param name="culture">A cultura específica do texto.</param>
		/// <param name="flowDirection">A direção em que o texto é lido.</param>
		/// <param name="typeface">A família, espessura, estilo e ampliação da fonte com os quais o texto deve ser formatado.</param>
		/// <param name="emSize">O tamanho da fonte para a medida em do texto, fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		/// <param name="foreground">O pincel usado para pintar cada glifo.</param>
		/// <param name="pixelsPerDip">O valor de Pixels por Pixel Independente de Densidade, que é o equivalente do fator de escala. Por exemplo, se o DPI da tela for 120 (ou 1,25 porque 120/96 = 1,25), será desenhado 1,25 pixel por pixel independente de densidade. DIP é a unidade de medida usada pelo WPF para ser independente da resolução do dispositivo e DPIs.</param>
		// Token: 0x0600227D RID: 8829 RVA: 0x0008B170 File Offset: 0x0008A570
		public FormattedText(string textToFormat, CultureInfo culture, FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, double pixelsPerDip) : this(textToFormat, culture, flowDirection, typeface, emSize, foreground, null, TextFormattingMode.Ideal, pixelsPerDip)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FormattedText" /> com o texto, cultura, direção do fluxo, face de tipos, tamanho da fonte, pincel e comportamento de substituição de número especificados.</summary>
		/// <param name="textToFormat">O texto a ser exibido.</param>
		/// <param name="culture">A cultura específica do texto.</param>
		/// <param name="flowDirection">A direção em que o texto é lido.</param>
		/// <param name="typeface">A família, espessura, estilo e ampliação da fonte com os quais o texto deve ser formatado.</param>
		/// <param name="emSize">O tamanho da fonte para a medida em do texto, fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		/// <param name="foreground">O pincel usado para pintar cada glifo.</param>
		/// <param name="numberSubstitution">O comportamento de substituição de número a ser aplicado ao texto.</param>
		// Token: 0x0600227E RID: 8830 RVA: 0x0008B190 File Offset: 0x0008A590
		[Obsolete("Use the PixelsPerDip override", false)]
		public FormattedText(string textToFormat, CultureInfo culture, FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, NumberSubstitution numberSubstitution) : this(textToFormat, culture, flowDirection, typeface, emSize, foreground, numberSubstitution, TextFormattingMode.Ideal)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FormattedText" /> com o texto, cultura, direção do fluxo, face de tipos, tamanho da fonte, pincel de primeiro plano, comportamento de substituição de número e valor pixelsPerDip especificados.</summary>
		/// <param name="textToFormat">O texto a ser exibido.</param>
		/// <param name="culture">A cultura específica do texto.</param>
		/// <param name="flowDirection">A direção em que o texto é lido.</param>
		/// <param name="typeface">A família, espessura, estilo e ampliação da fonte com os quais o texto deve ser formatado.</param>
		/// <param name="emSize">O tamanho da fonte para a medida em do texto, fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		/// <param name="foreground">O pincel usado para pintar cada glifo.</param>
		/// <param name="numberSubstitution">Especifica como os números no texto são exibidos, com base em <see cref="T:System.Windows.Media.NumberSubstitution" />. Este valor pode ser <see langword="null" />.</param>
		/// <param name="pixelsPerDip">O valor de Pixels por Pixel Independente de Densidade, que é o equivalente do fator de escala. Por exemplo, se o DPI da tela for 120 (ou 1,25 porque 120/96 = 1,25), será desenhado 1,25 pixel por pixel independente de densidade. DIP é a unidade de medida usada pelo WPF para ser independente da resolução do dispositivo e DPIs.</param>
		// Token: 0x0600227F RID: 8831 RVA: 0x0008B1B0 File Offset: 0x0008A5B0
		public FormattedText(string textToFormat, CultureInfo culture, FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, NumberSubstitution numberSubstitution, double pixelsPerDip) : this(textToFormat, culture, flowDirection, typeface, emSize, foreground, numberSubstitution, TextFormattingMode.Ideal, pixelsPerDip)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FormattedText" /> com o texto, cultura, direção do fluxo, face de tipos, tamanho da fonte, pincel, comportamento de substituição do número e modo de formatação do texto especificados.</summary>
		/// <param name="textToFormat">O texto a ser exibido.</param>
		/// <param name="culture">A cultura específica do texto.</param>
		/// <param name="flowDirection">A direção em que o texto é lido.</param>
		/// <param name="typeface">A família, espessura, estilo e ampliação da fonte com os quais o texto deve ser formatado.</param>
		/// <param name="emSize">O tamanho da fonte para a medida em do texto, fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		/// <param name="foreground">O pincel usado para pintar cada glifo.</param>
		/// <param name="numberSubstitution">O comportamento de substituição de número a ser aplicado ao texto.</param>
		/// <param name="textFormattingMode">O <see cref="T:System.Windows.Media.TextFormattingMode" /> a ser aplicado ao texto.</param>
		// Token: 0x06002280 RID: 8832 RVA: 0x0008B1D4 File Offset: 0x0008A5D4
		[Obsolete("Use the PixelsPerDip override", false)]
		public FormattedText(string textToFormat, CultureInfo culture, FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, NumberSubstitution numberSubstitution, TextFormattingMode textFormattingMode)
		{
			this._pixelsPerDip = (double)Util.PixelsPerDip;
			this._formatRuns = new SpanVector(null);
			this._maxTextHeight = double.MaxValue;
			this._maxLineCount = int.MaxValue;
			this._trimming = TextTrimming.WordEllipsis;
			base..ctor();
			this.InitFormattedText(textToFormat, culture, flowDirection, typeface, emSize, foreground, numberSubstitution, textFormattingMode, this._pixelsPerDip);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FormattedText" /> com o texto, cultura, direção do fluxo, face de tipos, tamanho da fonte, pincel de primeiro plano, comportamento de substituição de número e valor pixelsPerDip especificados.</summary>
		/// <param name="textToFormat">O texto a ser exibido.</param>
		/// <param name="culture">A cultura específica do texto.</param>
		/// <param name="flowDirection">A direção em que o texto é lido.</param>
		/// <param name="typeface">A família, espessura, estilo e ampliação da fonte com os quais o texto deve ser formatado.</param>
		/// <param name="emSize">O tamanho da fonte para a medida em do texto, fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		/// <param name="foreground">O pincel usado para pintar o cada glifo.</param>
		/// <param name="numberSubstitution">Especifica como os números no texto são exibidos, com base em <see cref="T:System.Windows.Media.NumberSubstitution" />. Este valor pode ser <see langword="null" />.</param>
		/// <param name="textFormattingMode">O modo de formatação que afeta a maneira como o WPF exibe texto.</param>
		/// <param name="pixelsPerDip">O valor de Pixels por Pixel Independente de Densidade, que é o equivalente do fator de escala. Por exemplo, se o DPI da tela for 120 (ou 1,25 porque 120/96 = 1,25), será desenhado 1,25 pixel por pixel independente de densidade. DIP é a unidade de medida usada pelo WPF para ser independente da resolução do dispositivo e DPIs.</param>
		// Token: 0x06002281 RID: 8833 RVA: 0x0008B23C File Offset: 0x0008A63C
		public FormattedText(string textToFormat, CultureInfo culture, FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, NumberSubstitution numberSubstitution, TextFormattingMode textFormattingMode, double pixelsPerDip)
		{
			this._pixelsPerDip = (double)Util.PixelsPerDip;
			this._formatRuns = new SpanVector(null);
			this._maxTextHeight = double.MaxValue;
			this._maxLineCount = int.MaxValue;
			this._trimming = TextTrimming.WordEllipsis;
			base..ctor();
			this.InitFormattedText(textToFormat, culture, flowDirection, typeface, emSize, foreground, numberSubstitution, textFormattingMode, pixelsPerDip);
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0008B2A0 File Offset: 0x0008A6A0
		private void InitFormattedText(string textToFormat, CultureInfo culture, FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, NumberSubstitution numberSubstitution, TextFormattingMode textFormattingMode, double pixelsPerDip)
		{
			if (textToFormat == null)
			{
				throw new ArgumentNullException("textToFormat");
			}
			if (typeface == null)
			{
				throw new ArgumentNullException("typeface");
			}
			FormattedText.ValidateCulture(culture);
			FormattedText.ValidateFlowDirection(flowDirection, "flowDirection");
			FormattedText.ValidateFontSize(emSize);
			this._pixelsPerDip = pixelsPerDip;
			this._textFormattingMode = textFormattingMode;
			this._text = textToFormat;
			GenericTextRunProperties genericTextRunProperties = new GenericTextRunProperties(typeface, emSize, 12.0, this._pixelsPerDip, null, foreground, null, BaselineAlignment.Baseline, culture, numberSubstitution);
			this._latestPosition = this._formatRuns.SetValue(0, this._text.Length, genericTextRunProperties, this._latestPosition);
			this._defaultParaProps = new GenericTextParagraphProperties(flowDirection, TextAlignment.Left, false, false, genericTextRunProperties, TextWrapping.WrapWithOverflow, 0.0, 0.0);
			this.InvalidateMetrics();
		}

		/// <summary>Obtém a cadeia de caracteres do texto a ser exibido.</summary>
		/// <returns>A cadeia de caracteres de texto a ser exibido.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x0008B368 File Offset: 0x0008A768
		public string Text
		{
			get
			{
				return this._text;
			}
		}

		/// <summary>Obtém ou define o PixelsPerDip em que o texto deve ser renderizado.</summary>
		/// <returns>O valor <see cref="P:System.Windows.Media.FormattedText.PixelsPerDip" /> atual.</returns>
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0008B37C File Offset: 0x0008A77C
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x0008B390 File Offset: 0x0008A790
		public double PixelsPerDip
		{
			get
			{
				return this._pixelsPerDip;
			}
			set
			{
				this._pixelsPerDip = value;
				this._defaultParaProps.DefaultTextRunProperties.PixelsPerDip = this._pixelsPerDip;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0008B3BC File Offset: 0x0008A7BC
		private static void ValidateCulture(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x0008B3D8 File Offset: 0x0008A7D8
		private static void ValidateFontSize(double emSize)
		{
			if (emSize <= 0.0)
			{
				throw new ArgumentOutOfRangeException("emSize", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (emSize > 35791.394066666668)
			{
				throw new ArgumentOutOfRangeException("emSize", SR.Get("ParameterCannotBeGreaterThan", new object[]
				{
					35791.394066666668
				}));
			}
			if (DoubleUtil.IsNaN(emSize))
			{
				throw new ArgumentOutOfRangeException("emSize", SR.Get("ParameterValueCannotBeNaN"));
			}
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x0008B45C File Offset: 0x0008A85C
		private static void ValidateFlowDirection(FlowDirection flowDirection, string parameterName)
		{
			if (flowDirection < FlowDirection.LeftToRight || flowDirection > FlowDirection.RightToLeft)
			{
				throw new InvalidEnumArgumentException(parameterName, (int)flowDirection, typeof(FlowDirection));
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0008B484 File Offset: 0x0008A884
		private int ValidateRange(int startIndex, int count)
		{
			if (startIndex < 0 || startIndex > this._text.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			int num = startIndex + count;
			if (count < 0 || num < startIndex || num > this._text.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return num;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0008B4D4 File Offset: 0x0008A8D4
		private void InvalidateMetrics()
		{
			this._metrics = null;
			this._minWidth = double.MinValue;
		}

		/// <summary>Altera o <see cref="T:System.Windows.Media.Brush" /> de primeiro plano para um objeto <see cref="T:System.Windows.Media.FormattedText" /> inteiro.</summary>
		/// <param name="foregroundBrush">O pincel a ser usado para o primeiro plano do texto.</param>
		// Token: 0x0600228B RID: 8843 RVA: 0x0008B4F8 File Offset: 0x0008A8F8
		public void SetForegroundBrush(Brush foregroundBrush)
		{
			this.SetForegroundBrush(foregroundBrush, 0, this._text.Length);
		}

		/// <summary>Altera o <see cref="T:System.Windows.Media.Brush" /> de primeiro plano para o texto especificado dentro de um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="foregroundBrush">O pincel a ser usado para o primeiro plano do texto.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual aplicar o pincel de primeiro plano.</param>
		/// <param name="count">O número de caracteres ao qual aplicar o pincel de primeiro plano.</param>
		// Token: 0x0600228C RID: 8844 RVA: 0x0008B518 File Offset: 0x0008A918
		public void SetForegroundBrush(Brush foregroundBrush, int startIndex, int count)
		{
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				if (genericTextRunProperties.ForegroundBrush != foregroundBrush)
				{
					GenericTextRunProperties element = new GenericTextRunProperties(genericTextRunProperties.Typeface, genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, foregroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
				}
			}
		}

		/// <summary>Define a família de fontes de todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="fontFamily">Uma cadeia de caracteres que constrói o <see cref="T:System.Windows.Media.FontFamily" /> a ser usado para a formatação de texto. Fallbacks são permitidos; para obter detalhes, consulte <see cref="T:System.Windows.Media.FontFamily" />.</param>
		// Token: 0x0600228D RID: 8845 RVA: 0x0008B5E8 File Offset: 0x0008A9E8
		public void SetFontFamily(string fontFamily)
		{
			this.SetFontFamily(fontFamily, 0, this._text.Length);
		}

		/// <summary>Define a família de fontes para um subconjunto de caracteres especificado no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="fontFamily">Uma cadeia de caracteres que constrói o <see cref="T:System.Windows.Media.FontFamily" /> a ser usado para a formatação de texto. Fallbacks são permitidos; para obter detalhes, consulte <see cref="T:System.Windows.Media.FontFamily" />.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual a família de fontes será aplicada.</param>
		/// <param name="count">O número de caracteres aos quais a alteração deverá aplicar.</param>
		// Token: 0x0600228E RID: 8846 RVA: 0x0008B608 File Offset: 0x0008AA08
		public void SetFontFamily(string fontFamily, int startIndex, int count)
		{
			if (fontFamily == null)
			{
				throw new ArgumentNullException("fontFamily");
			}
			this.SetFontFamily(new FontFamily(fontFamily), startIndex, count);
		}

		/// <summary>Define a família de fontes para um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="fontFamily">O <see cref="T:System.Windows.Media.FontFamily" /> a ser usado para formatação de texto.</param>
		// Token: 0x0600228F RID: 8847 RVA: 0x0008B634 File Offset: 0x0008AA34
		public void SetFontFamily(FontFamily fontFamily)
		{
			this.SetFontFamily(fontFamily, 0, this._text.Length);
		}

		/// <summary>Define a família de fontes para um subconjunto de caracteres especificado no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="fontFamily">O <see cref="T:System.Windows.Media.FontFamily" /> a ser usado para formatação de texto.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual a família de fontes será aplicada.</param>
		/// <param name="count">O número de caracteres aos quais a alteração deverá aplicar.</param>
		// Token: 0x06002290 RID: 8848 RVA: 0x0008B654 File Offset: 0x0008AA54
		public void SetFontFamily(FontFamily fontFamily, int startIndex, int count)
		{
			if (fontFamily == null)
			{
				throw new ArgumentNullException("fontFamily");
			}
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				Typeface typeface = genericTextRunProperties.Typeface;
				if (!fontFamily.Equals(typeface.FontFamily))
				{
					GenericTextRunProperties element = new GenericTextRunProperties(new Typeface(fontFamily, typeface.Style, typeface.Weight, typeface.Stretch), genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
					this.InvalidateMetrics();
				}
			}
		}

		/// <summary>Define o tamanho da fonte de todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="emSize">O tamanho de medida “em” da fonte, fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		// Token: 0x06002291 RID: 8849 RVA: 0x0008B760 File Offset: 0x0008AB60
		public void SetFontSize(double emSize)
		{
			this.SetFontSize(emSize, 0, this._text.Length);
		}

		/// <summary>Define o tamanho da fonte para um subconjunto de caracteres especificado no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="emSize">O tamanho de medida “em” da fonte, fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual aplicar o tamanho da fonte.</param>
		/// <param name="count">O número de caracteres ao qual aplicar o tamanho da fonte.</param>
		// Token: 0x06002292 RID: 8850 RVA: 0x0008B780 File Offset: 0x0008AB80
		public void SetFontSize(double emSize, int startIndex, int count)
		{
			FormattedText.ValidateFontSize(emSize);
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				if (genericTextRunProperties.FontRenderingEmSize != emSize)
				{
					GenericTextRunProperties element = new GenericTextRunProperties(genericTextRunProperties.Typeface, emSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
					this.InvalidateMetrics();
				}
			}
		}

		/// <summary>Define o <see cref="T:System.Globalization.CultureInfo" /> para todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> a ser usado para formatação de texto.</param>
		// Token: 0x06002293 RID: 8851 RVA: 0x0008B85C File Offset: 0x0008AC5C
		public void SetCulture(CultureInfo culture)
		{
			this.SetCulture(culture, 0, this._text.Length);
		}

		/// <summary>Define o <see cref="T:System.Globalization.CultureInfo" /> para um subconjunto especificado de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> a ser usado para formatação de texto.</param>
		/// <param name="startIndex">O índice de início do caractere inicial ao qual aplicar a alteração.</param>
		/// <param name="count">O número de caracteres que devem ser aplicados à alteração.</param>
		// Token: 0x06002294 RID: 8852 RVA: 0x0008B87C File Offset: 0x0008AC7C
		public void SetCulture(CultureInfo culture, int startIndex, int count)
		{
			FormattedText.ValidateCulture(culture);
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				if (!genericTextRunProperties.CultureInfo.Equals(culture))
				{
					GenericTextRunProperties element = new GenericTextRunProperties(genericTextRunProperties.Typeface, genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, culture, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
					this.InvalidateMetrics();
				}
			}
		}

		/// <summary>Define o comportamento de substituição de número para todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="numberSubstitution">O comportamento de substituição de número a ser aplicado ao texto; pode ser <see langword="null" />, caso em que o método de substituição de número padrão para a cultura do texto é usado.</param>
		// Token: 0x06002295 RID: 8853 RVA: 0x0008B95C File Offset: 0x0008AD5C
		public void SetNumberSubstitution(NumberSubstitution numberSubstitution)
		{
			this.SetNumberSubstitution(numberSubstitution, 0, this._text.Length);
		}

		/// <summary>Define o comportamento de substituição de número para o texto especificado dentro de um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="numberSubstitution">O comportamento de substituição de número a ser aplicado ao texto; pode ser <see langword="null" />, caso em que o método de substituição de número padrão para a cultura do texto é usado.</param>
		/// <param name="startIndex">O índice de início do caractere inicial ao qual aplicar a alteração.</param>
		/// <param name="count">O número de caracteres que devem ser aplicados à alteração.</param>
		// Token: 0x06002296 RID: 8854 RVA: 0x0008B97C File Offset: 0x0008AD7C
		public void SetNumberSubstitution(NumberSubstitution numberSubstitution, int startIndex, int count)
		{
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				if (numberSubstitution != null)
				{
					if (numberSubstitution.Equals(genericTextRunProperties.NumberSubstitution))
					{
						continue;
					}
				}
				else if (genericTextRunProperties.NumberSubstitution == null)
				{
					continue;
				}
				GenericTextRunProperties element = new GenericTextRunProperties(genericTextRunProperties.Typeface, genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, numberSubstitution);
				this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
				this.InvalidateMetrics();
			}
		}

		/// <summary>Define a espessura da fonte para todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="weight">O <see cref="T:System.Windows.FontWeight" /> a ser usado para formatação de texto.</param>
		// Token: 0x06002297 RID: 8855 RVA: 0x0008BA64 File Offset: 0x0008AE64
		public void SetFontWeight(FontWeight weight)
		{
			this.SetFontWeight(weight, 0, this._text.Length);
		}

		/// <summary>Altera o <see cref="T:System.Windows.FontWeight" /> para o texto especificado dentro de um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="weight">A espessura da fonte a ser usada para formatação de texto.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual aplicar a espessura da fonte.</param>
		/// <param name="count">O número de caracteres ao qual aplicar a espessura da fonte.</param>
		// Token: 0x06002298 RID: 8856 RVA: 0x0008BA84 File Offset: 0x0008AE84
		public void SetFontWeight(FontWeight weight, int startIndex, int count)
		{
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				Typeface typeface = genericTextRunProperties.Typeface;
				if (!(typeface.Weight == weight))
				{
					GenericTextRunProperties element = new GenericTextRunProperties(new Typeface(typeface.FontFamily, typeface.Style, weight, typeface.Stretch), genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
					this.InvalidateMetrics();
				}
			}
		}

		/// <summary>Define o estilo da fonte de todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="style">O valor <see cref="T:System.Windows.FontStyle" /> a ser usado para formatação de texto.</param>
		// Token: 0x06002299 RID: 8857 RVA: 0x0008BB80 File Offset: 0x0008AF80
		public void SetFontStyle(FontStyle style)
		{
			this.SetFontStyle(style, 0, this._text.Length);
		}

		/// <summary>Define o estilo da fonte para um subconjunto de caracteres especificado no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="style">O valor <see cref="T:System.Windows.FontStyle" /> a ser usado para formatação de texto.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual aplicar o estilo da fonte.</param>
		/// <param name="count">O número de caracteres ao qual aplicar o estilo da fonte.</param>
		// Token: 0x0600229A RID: 8858 RVA: 0x0008BBA0 File Offset: 0x0008AFA0
		public void SetFontStyle(FontStyle style, int startIndex, int count)
		{
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				Typeface typeface = genericTextRunProperties.Typeface;
				if (!(typeface.Style == style))
				{
					GenericTextRunProperties element = new GenericTextRunProperties(new Typeface(typeface.FontFamily, style, typeface.Weight, typeface.Stretch), genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
					this.InvalidateMetrics();
				}
			}
		}

		/// <summary>Define o valor de ampliação da fonte para todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="stretch">O valor <see cref="T:System.Windows.FontStretch" /> desejado a ser usado para formatação de texto.</param>
		// Token: 0x0600229B RID: 8859 RVA: 0x0008BC9C File Offset: 0x0008B09C
		public void SetFontStretch(FontStretch stretch)
		{
			this.SetFontStretch(stretch, 0, this._text.Length);
		}

		/// <summary>Define o valor de ampliação da fonte para um subconjunto de caracteres especificado no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="stretch">O valor <see cref="T:System.Windows.FontStretch" /> desejado a ser usado para formatação de texto.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual aplicar a ampliação da fonte.</param>
		/// <param name="count">O número de caracteres ao qual aplicar a ampliação da fonte.</param>
		// Token: 0x0600229C RID: 8860 RVA: 0x0008BCBC File Offset: 0x0008B0BC
		public void SetFontStretch(FontStretch stretch, int startIndex, int count)
		{
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				Typeface typeface = genericTextRunProperties.Typeface;
				if (!(typeface.Stretch == stretch))
				{
					GenericTextRunProperties element = new GenericTextRunProperties(new Typeface(typeface.FontFamily, typeface.Style, typeface.Weight, stretch), genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
					this.InvalidateMetrics();
				}
			}
		}

		/// <summary>Define a face de tipos da fonte para todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="typeface">O <see cref="T:System.Windows.Media.Typeface" /> a ser usado para formatação de texto.</param>
		// Token: 0x0600229D RID: 8861 RVA: 0x0008BDB8 File Offset: 0x0008B1B8
		public void SetFontTypeface(Typeface typeface)
		{
			this.SetFontTypeface(typeface, 0, this._text.Length);
		}

		/// <summary>Define a face de tipos da fonte para um subconjunto de caracteres especificado no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="typeface">O <see cref="T:System.Windows.Media.Typeface" /> a ser usado para formatação de texto.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual aplicar a face de tipos.</param>
		/// <param name="count">O número de caracteres ao qual aplicar a face de tipos.</param>
		// Token: 0x0600229E RID: 8862 RVA: 0x0008BDD8 File Offset: 0x0008B1D8
		public void SetFontTypeface(Typeface typeface, int startIndex, int count)
		{
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				if (genericTextRunProperties.Typeface != typeface)
				{
					GenericTextRunProperties element = new GenericTextRunProperties(typeface, genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, genericTextRunProperties.TextDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
					this.InvalidateMetrics();
				}
			}
		}

		/// <summary>Define o <see cref="T:System.Windows.TextDecorationCollection" /> para todo o conjunto de caracteres no objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="textDecorations">O <see cref="T:System.Windows.TextDecorationCollection" /> a ser aplicado ao texto.</param>
		// Token: 0x0600229F RID: 8863 RVA: 0x0008BEAC File Offset: 0x0008B2AC
		public void SetTextDecorations(TextDecorationCollection textDecorations)
		{
			this.SetTextDecorations(textDecorations, 0, this._text.Length);
		}

		/// <summary>Define o <see cref="T:System.Windows.TextDecorationCollection" /> para o texto especificado dentro de um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <param name="textDecorations">O <see cref="T:System.Windows.TextDecorationCollection" /> a ser aplicado ao texto.</param>
		/// <param name="startIndex">O índice inicial do caractere inicial ao qual aplicar as decorações de texto.</param>
		/// <param name="count">O número de caracteres ao qual aplicar as decorações de texto.</param>
		// Token: 0x060022A0 RID: 8864 RVA: 0x0008BECC File Offset: 0x0008B2CC
		public void SetTextDecorations(TextDecorationCollection textDecorations, int startIndex, int count)
		{
			int num = this.ValidateRange(startIndex, count);
			int i = startIndex;
			while (i < num)
			{
				SpanRider spanRider = new SpanRider(this._formatRuns, this._latestPosition, i);
				i = Math.Min(num, i + spanRider.Length);
				GenericTextRunProperties genericTextRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				Invariant.Assert(genericTextRunProperties != null);
				if (genericTextRunProperties.TextDecorations != textDecorations)
				{
					GenericTextRunProperties element = new GenericTextRunProperties(genericTextRunProperties.Typeface, genericTextRunProperties.FontRenderingEmSize, genericTextRunProperties.FontHintingEmSize, this._pixelsPerDip, textDecorations, genericTextRunProperties.ForegroundBrush, genericTextRunProperties.BackgroundBrush, genericTextRunProperties.BaselineAlignment, genericTextRunProperties.CultureInfo, genericTextRunProperties.NumberSubstitution);
					this._latestPosition = this._formatRuns.SetValue(spanRider.CurrentPosition, i - spanRider.CurrentPosition, element, spanRider.SpanPosition);
				}
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0008BF9C File Offset: 0x0008B39C
		private FormattedText.LineEnumerator GetEnumerator()
		{
			return new FormattedText.LineEnumerator(this);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0008BFB0 File Offset: 0x0008B3B0
		private void AdvanceLineOrigin(ref Point lineOrigin, TextLine currentLine)
		{
			double height = currentLine.Height;
			FlowDirection flowDirection = this._defaultParaProps.FlowDirection;
			if (flowDirection <= FlowDirection.RightToLeft)
			{
				lineOrigin.Y += height;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.FlowDirection" /> de um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <returns>O <see cref="T:System.Windows.FlowDirection" /> do texto formatado.</returns>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0008C010 File Offset: 0x0008B410
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x0008BFE4 File Offset: 0x0008B3E4
		public FlowDirection FlowDirection
		{
			get
			{
				return this._defaultParaProps.FlowDirection;
			}
			set
			{
				FormattedText.ValidateFlowDirection(value, "value");
				this._defaultParaProps.SetFlowDirection(value);
				this.InvalidateMetrics();
			}
		}

		/// <summary>Obtém ou define o alinhamento do texto em um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <returns>Um dos valores <see cref="T:System.Windows.TextAlignment" /> que especifica o alinhamento do texto em um objeto <see cref="T:System.Windows.Media.FormattedText" />.</returns>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0008C048 File Offset: 0x0008B448
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x0008C028 File Offset: 0x0008B428
		public TextAlignment TextAlignment
		{
			get
			{
				return this._defaultParaProps.TextAlignment;
			}
			set
			{
				this._defaultParaProps.SetTextAlignment(value);
				this.InvalidateMetrics();
			}
		}

		/// <summary>Obtém a altura da linha ou o espaçamento entre as linhas do texto.</summary>
		/// <returns>O espaçamento entre linhas de texto, fornecida em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0008C0A0 File Offset: 0x0008B4A0
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0008C060 File Offset: 0x0008B460
		public double LineHeight
		{
			get
			{
				return this._defaultParaProps.LineHeight;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterCannotBeNegative"));
				}
				this._defaultParaProps.SetLineHeight(value);
				this.InvalidateMetrics();
			}
		}

		/// <summary>Obtém ou define a largura máxima do texto (comprimento) para uma linha de texto.</summary>
		/// <returns>A largura máxima do texto para uma linha de texto, fornecida em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0008C0F4 File Offset: 0x0008B4F4
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x0008C0B8 File Offset: 0x0008B4B8
		public double MaxTextWidth
		{
			get
			{
				return this._maxTextWidth;
			}
			set
			{
				if (value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterCannotBeNegative"));
				}
				this._maxTextWidth = value;
				this.InvalidateMetrics();
			}
		}

		/// <summary>Define uma matriz de larguras máximas do texto dentro do <see cref="T:System.Windows.Media.FormattedText" />, em uma base por linha. Cada elemento na matriz representa a largura máxima do texto de linhas sequenciais de texto.</summary>
		/// <param name="maxTextWidths">Uma matriz de larguras máximas do texto, cada largura fornecida no unidades independentes de dispositivo (1/96 polegada por unidade).</param>
		// Token: 0x060022AB RID: 8875 RVA: 0x0008C108 File Offset: 0x0008B508
		public void SetMaxTextWidths(double[] maxTextWidths)
		{
			if (maxTextWidths == null || maxTextWidths.Length == 0)
			{
				throw new ArgumentNullException("maxTextWidths");
			}
			this._maxTextWidths = maxTextWidths;
			this.InvalidateMetrics();
		}

		/// <summary>Recupera uma matriz de larguras de texto. Cada elemento na matriz representa a largura máxima do texto de linhas sequenciais de texto.</summary>
		/// <returns>Uma matriz de larguras máximas do texto, cada largura fornecida no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x060022AC RID: 8876 RVA: 0x0008C134 File Offset: 0x0008B534
		public double[] GetMaxTextWidths()
		{
			if (this._maxTextWidths != null)
			{
				return (double[])this._maxTextWidths.Clone();
			}
			return null;
		}

		/// <summary>Obtém ou define a altura máxima de uma coluna de texto.</summary>
		/// <returns>A altura máxima de uma coluna de texto, fornecida no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x0008C1D0 File Offset: 0x0008B5D0
		// (set) Token: 0x060022AD RID: 8877 RVA: 0x0008C15C File Offset: 0x0008B55C
		public double MaxTextHeight
		{
			get
			{
				return this._maxTextHeight;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("PropertyMustBeGreaterThanZero", new object[]
					{
						"MaxTextHeight"
					}));
				}
				if (DoubleUtil.IsNaN(value))
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("PropertyValueCannotBeNaN", new object[]
					{
						"MaxTextHeight"
					}));
				}
				this._maxTextHeight = value;
				this.InvalidateMetrics();
			}
		}

		/// <summary>Obtém ou define o número máximo de linhas a serem exibidas. O texto que excede o <see cref="P:System.Windows.Media.FormattedText.MaxLineCount" /> não será exibido.</summary>
		/// <returns>O número máximo de linhas a serem exibidas.</returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x0008C218 File Offset: 0x0008B618
		// (set) Token: 0x060022AF RID: 8879 RVA: 0x0008C1E4 File Offset: 0x0008B5E4
		public int MaxLineCount
		{
			get
			{
				return this._maxLineCount;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", SR.Get("ParameterMustBeGreaterThanZero"));
				}
				this._maxLineCount = value;
				this.InvalidateMetrics();
			}
		}

		/// <summary>Obtém ou define o meio pelo qual a omissão do texto é indicada.</summary>
		/// <returns>Um do <see cref="T:System.Windows.TextTrimming" /> valores que especifica como a omissão do texto é indicada. O padrão é <see cref="F:System.Windows.TextTrimming.WordEllipsis" />.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x0008C288 File Offset: 0x0008B688
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x0008C22C File Offset: 0x0008B62C
		public TextTrimming Trimming
		{
			get
			{
				return this._trimming;
			}
			set
			{
				if (value < TextTrimming.None || value > TextTrimming.WordEllipsis)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TextTrimming));
				}
				this._trimming = value;
				if (this._trimming == TextTrimming.None)
				{
					this._defaultParaProps.SetTextWrapping(TextWrapping.Wrap);
				}
				else
				{
					this._defaultParaProps.SetTextWrapping(TextWrapping.WrapWithOverflow);
				}
				this.InvalidateMetrics();
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x0008C29C File Offset: 0x0008B69C
		private FormattedText.CachedMetrics Metrics
		{
			get
			{
				if (this._metrics == null)
				{
					this._metrics = this.DrawAndCalculateMetrics(null, default(Point), false);
				}
				return this._metrics;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0008C2D0 File Offset: 0x0008B6D0
		private FormattedText.CachedMetrics BlackBoxMetrics
		{
			get
			{
				if (this._metrics == null || double.IsNaN(this._metrics.Extent))
				{
					this._metrics = this.DrawAndCalculateMetrics(null, default(Point), true);
				}
				return this._metrics;
			}
		}

		/// <summary>Obtém a distância da parte superior da primeira linha até a parte inferior da última linha do objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <returns>A distância da parte superior da primeira linha na parte inferior da última linha, fornecido no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x0008C314 File Offset: 0x0008B714
		public double Height
		{
			get
			{
				return this.Metrics.Height;
			}
		}

		/// <summary>Obtém a distância do pixel da extremidade superior desenhado da primeira linha até o pixel da extremidade inferior desenhado da última linha.</summary>
		/// <returns>A distância do pixel na extremidade superior desenhado da primeira linha até o pixel desenhado na extremidade inferior da última linha, fornecido no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0008C32C File Offset: 0x0008B72C
		public double Extent
		{
			get
			{
				return this.BlackBoxMetrics.Extent;
			}
		}

		/// <summary>Obtém a distância da parte superior da primeira linha até a linha de base da primeira linha de um objeto <see cref="T:System.Windows.Media.FormattedText" />.</summary>
		/// <returns>A distância da parte superior da primeira linha a linha de base da primeira linha, fornecido no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x0008C344 File Offset: 0x0008B744
		public double Baseline
		{
			get
			{
				return this.Metrics.Baseline;
			}
		}

		/// <summary>Obtém a distância da parte inferior da última linha do texto até o pixel da extremidade inferior desenhado.</summary>
		/// <returns>A distância da parte inferior da última linha até o pixel à tinta na extremidade inferior, fornecido no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x0008C35C File Offset: 0x0008B75C
		public double OverhangAfter
		{
			get
			{
				return this.BlackBoxMetrics.OverhangAfter;
			}
		}

		/// <summary>Obtém a distância máxima do ponto de alinhamento à esquerda até o pixel desenhado à esquerda de uma linha.</summary>
		/// <returns>A distância máxima do alinhamento à esquerda do ponto até o pixel desenhado à esquerda de uma linha, fornecido no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0008C374 File Offset: 0x0008B774
		public double OverhangLeading
		{
			get
			{
				return this.BlackBoxMetrics.OverhangLeading;
			}
		}

		/// <summary>Obtém a distância máxima do pixel à tinta à direita até o ponto de alinhamento à direita de uma linha.</summary>
		/// <returns>A distância máxima à direita recebem a tinta pixel para o ponto de alinhamento à direita de uma linha, fornecido no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x0008C38C File Offset: 0x0008B78C
		public double OverhangTrailing
		{
			get
			{
				return this.BlackBoxMetrics.OverhangTrailing;
			}
		}

		/// <summary>Obtém a largura entre os pontos de alinhamento à esquerda e à direita de uma linha, excluindo qualquer caractere de espaço em branco à direita.</summary>
		/// <returns>A largura entre os pontos de alinhamento à esquerda e à direita de uma linha, excluindo qualquer caractere de espaço em branco à direita. Fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x0008C3A4 File Offset: 0x0008B7A4
		public double Width
		{
			get
			{
				return this.Metrics.Width;
			}
		}

		/// <summary>Obtém a largura entre os pontos de alinhamento à esquerda e à direita de uma linha, incluindo caracteres de espaço em branco à direita.</summary>
		/// <returns>A largura entre os pontos de alinhamento à esquerda e à direita de uma linha, incluindo quaisquer caracteres de espaço em branco à direita. Fornecido em unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x0008C3BC File Offset: 0x0008B7BC
		public double WidthIncludingTrailingWhitespace
		{
			get
			{
				return this.Metrics.WidthIncludingTrailingWhitespace;
			}
		}

		/// <summary>Obtém a menor largura de texto possível que pode conter totalmente o conteúdo de texto especificado.</summary>
		/// <returns>A largura do texto mínimo da fonte de texto, fornecido no unidades independentes de dispositivo (1/96 polegada por unidade).</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0008C3D4 File Offset: 0x0008B7D4
		public double MinWidth
		{
			get
			{
				if (this._minWidth != -1.7976931348623157E+308)
				{
					return this._minWidth;
				}
				if (this._textSourceImpl == null)
				{
					this._textSourceImpl = new FormattedText.TextSourceImplementation(this);
				}
				this._minWidth = TextFormatter.FromCurrentDispatcher(this._textFormattingMode).FormatMinMaxParagraphWidth(this._textSourceImpl, 0, this._defaultParaProps).MinWidth;
				return this._minWidth;
			}
		}

		/// <summary>Retorna um objeto <see cref="T:System.Windows.Media.Geometry" /> que representa a caixa delimitadora de realce do texto formatado.</summary>
		/// <param name="origin">A origem da região de realce.</param>
		/// <returns>O objeto <see cref="T:System.Windows.Media.Geometry" /> que representa a caixa delimitadora de realce do texto formatado.</returns>
		// Token: 0x060022BE RID: 8894 RVA: 0x0008C440 File Offset: 0x0008B840
		public Geometry BuildHighlightGeometry(Point origin)
		{
			return this.BuildHighlightGeometry(origin, 0, this._text.Length);
		}

		/// <summary>Retorna um objeto <see cref="T:System.Windows.Media.Geometry" /> que representa o texto formatado, incluindo todos os glifos e decorações do texto.</summary>
		/// <param name="origin">A origem da parte superior esquerda da geometria resultante.</param>
		/// <returns>O representação de objeto <see cref="T:System.Windows.Media.Geometry" /> do texto formatado.</returns>
		// Token: 0x060022BF RID: 8895 RVA: 0x0008C460 File Offset: 0x0008B860
		public Geometry BuildGeometry(Point origin)
		{
			GeometryGroup geometryGroup = null;
			Point origin2 = origin;
			DrawingGroup drawingGroup = new DrawingGroup();
			DrawingContext drawingContext = drawingGroup.Open();
			foreach (TextLine textLine in this)
			{
				try
				{
					textLine.Draw(drawingContext, origin2, InvertAxes.None);
					this.AdvanceLineOrigin(ref origin2, textLine);
				}
				finally
				{
					if (textLine != null)
					{
						((IDisposable)textLine).Dispose();
					}
				}
			}
			drawingContext.Close();
			this.CombineGeometryRecursive(drawingGroup, ref geometryGroup);
			if (geometryGroup == null || geometryGroup.IsEmpty())
			{
				return Geometry.Empty;
			}
			return geometryGroup;
		}

		/// <summary>Retorna um objeto <see cref="T:System.Windows.Media.Geometry" /> que representa a caixa delimitadora de realce para uma subcadeia de caracteres especificada do texto formatado.</summary>
		/// <param name="origin">A origem da região de realce.</param>
		/// <param name="startIndex">O índice do caractere inicial para o qual os limites do realce devem ser obtidos.</param>
		/// <param name="count">O número de caracteres que os limites do realce devem conter.</param>
		/// <returns>O objeto <see cref="T:System.Windows.Media.Geometry" /> que representa a caixa delimitadora de realce da subcadeia de caracteres do texto formatado.</returns>
		// Token: 0x060022C0 RID: 8896 RVA: 0x0008C520 File Offset: 0x0008B920
		public Geometry BuildHighlightGeometry(Point origin, int startIndex, int count)
		{
			this.ValidateRange(startIndex, count);
			PathGeometry pathGeometry = null;
			using (FormattedText.LineEnumerator enumerator = this.GetEnumerator())
			{
				Point point = origin;
				while (enumerator.MoveNext())
				{
					using (TextLine textLine = enumerator.Current)
					{
						int num = Math.Max(enumerator.Position, startIndex);
						int num2 = Math.Min(enumerator.Position + enumerator.Length, startIndex + count);
						if (num < num2)
						{
							IList<TextBounds> textBounds = textLine.GetTextBounds(num, num2 - num);
							if (textBounds != null)
							{
								foreach (TextBounds textBounds2 in textBounds)
								{
									Rect rectangle = textBounds2.Rectangle;
									if (this.FlowDirection == FlowDirection.RightToLeft)
									{
										rectangle.X = enumerator.CurrentParagraphWidth - rectangle.Right;
									}
									rectangle.X += point.X;
									rectangle.Y += point.Y;
									RectangleGeometry rectangleGeometry = new RectangleGeometry(rectangle);
									if (pathGeometry == null)
									{
										pathGeometry = rectangleGeometry.GetAsPathGeometry();
									}
									else
									{
										pathGeometry = Geometry.Combine(pathGeometry, rectangleGeometry, GeometryCombineMode.Union, null);
									}
								}
							}
						}
						this.AdvanceLineOrigin(ref point, textLine);
					}
				}
			}
			if (pathGeometry == null || pathGeometry.IsEmpty())
			{
				return null;
			}
			return pathGeometry;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0008C6C0 File Offset: 0x0008BAC0
		internal void Draw(DrawingContext dc, Point origin)
		{
			Point origin2 = origin;
			if (this._metrics != null && !double.IsNaN(this._metrics.Extent))
			{
				using (FormattedText.LineEnumerator enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						using (TextLine textLine = enumerator.Current)
						{
							textLine.Draw(dc, origin2, InvertAxes.None);
							this.AdvanceLineOrigin(ref origin2, textLine);
						}
					}
					return;
				}
			}
			this._metrics = this.DrawAndCalculateMetrics(dc, origin, true);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0008C774 File Offset: 0x0008BB74
		private FormattedText.CachedMetrics DrawAndCalculateMetrics(DrawingContext dc, Point drawingOffset, bool getBlackBoxMetrics)
		{
			FormattedText.CachedMetrics cachedMetrics = new FormattedText.CachedMetrics();
			if (this._text.Length == 0)
			{
				return cachedMetrics;
			}
			using (FormattedText.LineEnumerator enumerator = this.GetEnumerator())
			{
				bool flag = true;
				double num2;
				double num = num2 = double.MaxValue;
				double num4;
				double num3 = num4 = double.MinValue;
				Point point = new Point(0.0, 0.0);
				double num5 = double.MaxValue;
				while (enumerator.MoveNext())
				{
					using (TextLine textLine = enumerator.Current)
					{
						if (dc != null)
						{
							textLine.Draw(dc, new Point(point.X + drawingOffset.X, point.Y + drawingOffset.Y), InvertAxes.None);
						}
						if (getBlackBoxMetrics)
						{
							double val = point.X + textLine.Start + textLine.OverhangLeading;
							double val2 = point.X + textLine.Start + textLine.Width - textLine.OverhangTrailing;
							double num6 = point.Y + textLine.Height + textLine.OverhangAfter;
							double val3 = num6 - textLine.Extent;
							num2 = Math.Min(num2, val);
							num4 = Math.Max(num4, val2);
							num3 = Math.Max(num3, num6);
							num = Math.Min(num, val3);
							cachedMetrics.OverhangAfter = textLine.OverhangAfter;
						}
						cachedMetrics.Height += textLine.Height;
						cachedMetrics.Width = Math.Max(cachedMetrics.Width, textLine.Width);
						cachedMetrics.WidthIncludingTrailingWhitespace = Math.Max(cachedMetrics.WidthIncludingTrailingWhitespace, textLine.WidthIncludingTrailingWhitespace);
						num5 = Math.Min(num5, textLine.Start);
						if (flag)
						{
							cachedMetrics.Baseline = textLine.Baseline;
							flag = false;
						}
						this.AdvanceLineOrigin(ref point, textLine);
					}
				}
				if (getBlackBoxMetrics)
				{
					cachedMetrics.Extent = num3 - num;
					cachedMetrics.OverhangLeading = num2 - num5;
					cachedMetrics.OverhangTrailing = cachedMetrics.Width - (num4 - num5);
				}
				else
				{
					cachedMetrics.Extent = double.NaN;
				}
			}
			return cachedMetrics;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0008C9B0 File Offset: 0x0008BDB0
		private void CombineGeometryRecursive(Drawing drawing, ref GeometryGroup accumulatedGeometry)
		{
			DrawingGroup drawingGroup = drawing as DrawingGroup;
			if (drawingGroup != null)
			{
				using (DrawingCollection.Enumerator enumerator = drawingGroup.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Drawing drawing2 = enumerator.Current;
						this.CombineGeometryRecursive(drawing2, ref accumulatedGeometry);
					}
					return;
				}
			}
			GlyphRunDrawing glyphRunDrawing = drawing as GlyphRunDrawing;
			if (glyphRunDrawing != null)
			{
				GlyphRun glyphRun = glyphRunDrawing.GlyphRun;
				if (glyphRun != null)
				{
					Geometry geometry = glyphRun.BuildGeometry();
					if (!geometry.IsEmpty())
					{
						if (accumulatedGeometry == null)
						{
							accumulatedGeometry = new GeometryGroup();
							accumulatedGeometry.FillRule = FillRule.Nonzero;
						}
						accumulatedGeometry.Children.Add(geometry);
						return;
					}
				}
			}
			else
			{
				GeometryDrawing geometryDrawing = drawing as GeometryDrawing;
				if (geometryDrawing != null)
				{
					Geometry geometry2 = geometryDrawing.Geometry;
					if (geometry2 != null)
					{
						LineGeometry lineGeometry = geometry2 as LineGeometry;
						if (lineGeometry != null)
						{
							Rect bounds = lineGeometry.Bounds;
							if (bounds.Height == 0.0)
							{
								bounds.Height = geometryDrawing.Pen.Thickness;
							}
							else if (bounds.Width == 0.0)
							{
								bounds.Width = geometryDrawing.Pen.Thickness;
							}
							geometry2 = new RectangleGeometry(bounds);
						}
						if (accumulatedGeometry == null)
						{
							accumulatedGeometry = new GeometryGroup();
							accumulatedGeometry.FillRule = FillRule.Nonzero;
						}
						accumulatedGeometry.Children.Add(geometry2);
					}
				}
			}
		}

		// Token: 0x04001109 RID: 4361
		private string _text;

		// Token: 0x0400110A RID: 4362
		private double _pixelsPerDip;

		// Token: 0x0400110B RID: 4363
		private SpanVector _formatRuns;

		// Token: 0x0400110C RID: 4364
		private SpanPosition _latestPosition;

		// Token: 0x0400110D RID: 4365
		private GenericTextParagraphProperties _defaultParaProps;

		// Token: 0x0400110E RID: 4366
		private double _maxTextWidth;

		// Token: 0x0400110F RID: 4367
		private double[] _maxTextWidths;

		// Token: 0x04001110 RID: 4368
		private double _maxTextHeight;

		// Token: 0x04001111 RID: 4369
		private int _maxLineCount;

		// Token: 0x04001112 RID: 4370
		private TextTrimming _trimming;

		// Token: 0x04001113 RID: 4371
		private TextFormattingMode _textFormattingMode;

		// Token: 0x04001114 RID: 4372
		private FormattedText.TextSourceImplementation _textSourceImpl;

		// Token: 0x04001115 RID: 4373
		private FormattedText.CachedMetrics _metrics;

		// Token: 0x04001116 RID: 4374
		private double _minWidth;

		// Token: 0x04001117 RID: 4375
		private const double MaxFontEmSize = 35791.394066666668;

		// Token: 0x0200086E RID: 2158
		private struct LineEnumerator : IEnumerator, IDisposable
		{
			// Token: 0x06005765 RID: 22373 RVA: 0x00165104 File Offset: 0x00164504
			internal LineEnumerator(FormattedText text)
			{
				this._previousHeight = 0.0;
				this._previousLength = 0;
				this._previousLineBreak = null;
				this._textStorePosition = 0;
				this._lineCount = 0;
				this._totalHeight = 0.0;
				this._currentLine = null;
				this._nextLine = null;
				this._formatter = TextFormatter.FromCurrentDispatcher(text._textFormattingMode);
				this._that = text;
				if (this._that._textSourceImpl == null)
				{
					this._that._textSourceImpl = new FormattedText.TextSourceImplementation(this._that);
				}
			}

			// Token: 0x06005766 RID: 22374 RVA: 0x00165194 File Offset: 0x00164594
			public void Dispose()
			{
				if (this._currentLine != null)
				{
					this._currentLine.Dispose();
					this._currentLine = null;
				}
				if (this._nextLine != null)
				{
					this._nextLine.Dispose();
					this._nextLine = null;
				}
			}

			// Token: 0x17001204 RID: 4612
			// (get) Token: 0x06005767 RID: 22375 RVA: 0x001651D8 File Offset: 0x001645D8
			internal int Position
			{
				get
				{
					return this._textStorePosition;
				}
			}

			// Token: 0x17001205 RID: 4613
			// (get) Token: 0x06005768 RID: 22376 RVA: 0x001651EC File Offset: 0x001645EC
			internal int Length
			{
				get
				{
					return this._previousLength;
				}
			}

			// Token: 0x17001206 RID: 4614
			// (get) Token: 0x06005769 RID: 22377 RVA: 0x00165200 File Offset: 0x00164600
			public TextLine Current
			{
				get
				{
					return this._currentLine;
				}
			}

			// Token: 0x17001207 RID: 4615
			// (get) Token: 0x0600576A RID: 22378 RVA: 0x00165214 File Offset: 0x00164614
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17001208 RID: 4616
			// (get) Token: 0x0600576B RID: 22379 RVA: 0x00165228 File Offset: 0x00164628
			internal double CurrentParagraphWidth
			{
				get
				{
					return this.MaxLineLength(this._lineCount);
				}
			}

			// Token: 0x0600576C RID: 22380 RVA: 0x00165244 File Offset: 0x00164644
			private double MaxLineLength(int line)
			{
				if (this._that._maxTextWidths == null)
				{
					return this._that._maxTextWidth;
				}
				return this._that._maxTextWidths[Math.Min(line, this._that._maxTextWidths.Length - 1)];
			}

			// Token: 0x0600576D RID: 22381 RVA: 0x0016528C File Offset: 0x0016468C
			public bool MoveNext()
			{
				if (this._currentLine == null)
				{
					if (this._that._text.Length == 0)
					{
						return false;
					}
					this._currentLine = this.FormatLine(this._that._textSourceImpl, this._textStorePosition, this.MaxLineLength(this._lineCount), this._that._defaultParaProps, null);
					if (this._totalHeight + this._currentLine.Height > this._that._maxTextHeight)
					{
						this._currentLine.Dispose();
						this._currentLine = null;
						return false;
					}
				}
				else
				{
					if (this._nextLine == null)
					{
						return false;
					}
					this._totalHeight += this._previousHeight;
					this._textStorePosition += this._previousLength;
					this._lineCount++;
					this._currentLine = this._nextLine;
					this._nextLine = null;
				}
				TextLineBreak textLineBreak = this._currentLine.GetTextLineBreak();
				if (this._textStorePosition + this._currentLine.Length < this._that._text.Length)
				{
					bool flag;
					if (this._lineCount + 1 >= this._that._maxLineCount)
					{
						flag = false;
					}
					else
					{
						this._nextLine = this.FormatLine(this._that._textSourceImpl, this._textStorePosition + this._currentLine.Length, this.MaxLineLength(this._lineCount + 1), this._that._defaultParaProps, textLineBreak);
						flag = (this._totalHeight + this._currentLine.Height + this._nextLine.Height <= this._that._maxTextHeight);
					}
					if (!flag)
					{
						if (this._nextLine != null)
						{
							this._nextLine.Dispose();
							this._nextLine = null;
						}
						if (this._that._trimming != TextTrimming.None && !this._currentLine.HasCollapsed)
						{
							TextWrapping textWrapping = this._that._defaultParaProps.TextWrapping;
							this._that._defaultParaProps.SetTextWrapping(TextWrapping.NoWrap);
							if (textLineBreak != null)
							{
								textLineBreak.Dispose();
							}
							this._currentLine.Dispose();
							this._currentLine = this.FormatLine(this._that._textSourceImpl, this._textStorePosition, this.MaxLineLength(this._lineCount), this._that._defaultParaProps, this._previousLineBreak);
							textLineBreak = this._currentLine.GetTextLineBreak();
							this._that._defaultParaProps.SetTextWrapping(textWrapping);
						}
					}
				}
				this._previousHeight = this._currentLine.Height;
				this._previousLength = this._currentLine.Length;
				if (this._previousLineBreak != null)
				{
					this._previousLineBreak.Dispose();
				}
				this._previousLineBreak = textLineBreak;
				return true;
			}

			// Token: 0x0600576E RID: 22382 RVA: 0x0016553C File Offset: 0x0016493C
			private TextLine FormatLine(TextSource textSource, int textSourcePosition, double maxLineLength, TextParagraphProperties paraProps, TextLineBreak lineBreak)
			{
				TextLine textLine = this._formatter.FormatLine(textSource, textSourcePosition, maxLineLength, paraProps, lineBreak);
				if (this._that._trimming != TextTrimming.None && textLine.HasOverflowed && textLine.Length > 0)
				{
					SpanRider spanRider = new SpanRider(this._that._formatRuns, this._that._latestPosition, Math.Min(textSourcePosition + textLine.Length - 1, this._that._text.Length - 1));
					GenericTextRunProperties textRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
					TextCollapsingProperties textCollapsingProperties;
					if (this._that._trimming == TextTrimming.CharacterEllipsis)
					{
						textCollapsingProperties = new TextTrailingCharacterEllipsis(maxLineLength, textRunProperties);
					}
					else
					{
						textCollapsingProperties = new TextTrailingWordEllipsis(maxLineLength, textRunProperties);
					}
					TextLine textLine2 = textLine.Collapse(new TextCollapsingProperties[]
					{
						textCollapsingProperties
					});
					if (textLine2 != textLine)
					{
						textLine.Dispose();
						textLine = textLine2;
					}
				}
				return textLine;
			}

			// Token: 0x0600576F RID: 22383 RVA: 0x0016560C File Offset: 0x00164A0C
			public void Reset()
			{
				this._textStorePosition = 0;
				this._lineCount = 0;
				this._totalHeight = 0.0;
				this._currentLine = null;
				this._nextLine = null;
			}

			// Token: 0x0400286F RID: 10351
			private int _textStorePosition;

			// Token: 0x04002870 RID: 10352
			private int _lineCount;

			// Token: 0x04002871 RID: 10353
			private double _totalHeight;

			// Token: 0x04002872 RID: 10354
			private TextLine _currentLine;

			// Token: 0x04002873 RID: 10355
			private TextLine _nextLine;

			// Token: 0x04002874 RID: 10356
			private TextFormatter _formatter;

			// Token: 0x04002875 RID: 10357
			private FormattedText _that;

			// Token: 0x04002876 RID: 10358
			private double _previousHeight;

			// Token: 0x04002877 RID: 10359
			private int _previousLength;

			// Token: 0x04002878 RID: 10360
			private TextLineBreak _previousLineBreak;
		}

		// Token: 0x0200086F RID: 2159
		private class CachedMetrics
		{
			// Token: 0x04002879 RID: 10361
			public double Height;

			// Token: 0x0400287A RID: 10362
			public double Baseline;

			// Token: 0x0400287B RID: 10363
			public double Width;

			// Token: 0x0400287C RID: 10364
			public double WidthIncludingTrailingWhitespace;

			// Token: 0x0400287D RID: 10365
			public double Extent;

			// Token: 0x0400287E RID: 10366
			public double OverhangAfter;

			// Token: 0x0400287F RID: 10367
			public double OverhangLeading;

			// Token: 0x04002880 RID: 10368
			public double OverhangTrailing;
		}

		// Token: 0x02000870 RID: 2160
		private class TextSourceImplementation : TextSource
		{
			// Token: 0x06005771 RID: 22385 RVA: 0x00165658 File Offset: 0x00164A58
			public TextSourceImplementation(FormattedText text)
			{
				this._that = text;
				base.PixelsPerDip = this._that.PixelsPerDip;
			}

			// Token: 0x06005772 RID: 22386 RVA: 0x00165684 File Offset: 0x00164A84
			public override TextRun GetTextRun(int textSourceCharacterIndex)
			{
				if (textSourceCharacterIndex >= this._that._text.Length)
				{
					return new TextEndOfParagraph(1);
				}
				SpanRider spanRider = new SpanRider(this._that._formatRuns, this._that._latestPosition, textSourceCharacterIndex);
				TextRunProperties textRunProperties = spanRider.CurrentElement as GenericTextRunProperties;
				TextCharacters result = new TextCharacters(this._that._text, textSourceCharacterIndex, spanRider.Length, textRunProperties);
				textRunProperties.PixelsPerDip = base.PixelsPerDip;
				return result;
			}

			// Token: 0x06005773 RID: 22387 RVA: 0x00165700 File Offset: 0x00164B00
			public override TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit)
			{
				CharacterBufferRange empty = CharacterBufferRange.Empty;
				CultureInfo culture = null;
				if (textSourceCharacterIndexLimit > 0)
				{
					SpanRider spanRider = new SpanRider(this._that._formatRuns, this._that._latestPosition, textSourceCharacterIndexLimit - 1);
					empty = new CharacterBufferRange(new CharacterBufferReference(this._that._text, spanRider.CurrentSpanStart), textSourceCharacterIndexLimit - spanRider.CurrentSpanStart);
					culture = ((TextRunProperties)spanRider.CurrentElement).CultureInfo;
				}
				return new TextSpan<CultureSpecificCharacterBufferRange>(empty.Length, new CultureSpecificCharacterBufferRange(culture, empty));
			}

			// Token: 0x06005774 RID: 22388 RVA: 0x00165788 File Offset: 0x00164B88
			public override int GetTextEffectCharacterIndexFromTextSourceCharacterIndex(int textSourceCharacterIndex)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04002881 RID: 10369
			private FormattedText _that;
		}
	}
}
