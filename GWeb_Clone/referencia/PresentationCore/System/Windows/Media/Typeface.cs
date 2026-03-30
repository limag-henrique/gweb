using System;
using System.Globalization;
using System.Windows.Media.TextFormatting;
using MS.Internal;
using MS.Internal.FontCache;
using MS.Internal.FontFace;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;

namespace System.Windows.Media
{
	/// <summary>Representa uma combinação de <see cref="T:System.Windows.Media.FontFamily" />, <see cref="T:System.Windows.FontWeight" />, <see cref="T:System.Windows.FontStyle" /> e <see cref="T:System.Windows.FontStretch" />.</summary>
	// Token: 0x02000446 RID: 1094
	public class Typeface
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Typeface" /> para o nome da face de tipos da família de fontes especificado.</summary>
		/// <param name="typefaceName">O nome da face de tipos da família de fontes especificada.</param>
		// Token: 0x06002C8C RID: 11404 RVA: 0x000B1C14 File Offset: 0x000B1014
		public Typeface(string typefaceName) : this(new FontFamily(typefaceName), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Typeface" /> do nome da família de fontes especificado e os valores <see cref="P:System.Windows.Media.Typeface.Style" />, <see cref="P:System.Windows.Media.Typeface.Weight" /> e <see cref="P:System.Windows.Media.Typeface.Stretch" />.</summary>
		/// <param name="fontFamily">A família de fontes da face de tipos.</param>
		/// <param name="style">O estilo da face de tipos.</param>
		/// <param name="weight">O peso relativo da face de tipos.</param>
		/// <param name="stretch">O grau ao qual a face de tipos é estendida.</param>
		// Token: 0x06002C8D RID: 11405 RVA: 0x000B1C3C File Offset: 0x000B103C
		public Typeface(FontFamily fontFamily, FontStyle style, FontWeight weight, FontStretch stretch) : this(fontFamily, style, weight, stretch, FontFamily.FontFamilyGlobalUI)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Typeface" /> do nome da família de fontes especificado e os valores <see cref="P:System.Windows.Media.Typeface.Style" />, <see cref="P:System.Windows.Media.Typeface.Weight" /> e <see cref="P:System.Windows.Media.Typeface.Stretch" />. Além disso, uma família de fontes de fallback é especificada.</summary>
		/// <param name="fontFamily">A família de fontes da face de tipos.</param>
		/// <param name="style">O estilo da face de tipos.</param>
		/// <param name="weight">O peso relativo da face de tipos.</param>
		/// <param name="stretch">O grau ao qual a face de tipos é estendida.</param>
		/// <param name="fallbackFontFamily">A família de fontes é usada quando um caractere é encontrado que a família de fontes principal (especificada pelo parâmetro <paramref name="fontFamily" />) não pode exibir.</param>
		// Token: 0x06002C8E RID: 11406 RVA: 0x000B1C5C File Offset: 0x000B105C
		public Typeface(FontFamily fontFamily, FontStyle style, FontWeight weight, FontStretch stretch, FontFamily fallbackFontFamily)
		{
			if (fontFamily == null)
			{
				throw new ArgumentNullException("fontFamily");
			}
			this._fontFamily = fontFamily;
			this._style = style;
			this._weight = weight;
			this._stretch = stretch;
			this._fallbackFontFamily = fallbackFontFamily;
		}

		/// <summary>Obtém o nome da família de fontes da qual a face de tipos foi construída.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.FontFamily" /> do qual a face de tipos foi construída.</returns>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000B1CA4 File Offset: 0x000B10A4
		public FontFamily FontFamily
		{
			get
			{
				return this._fontFamily;
			}
		}

		/// <summary>Obtém o peso relativo da face de tipos.</summary>
		/// <returns>Um <see cref="T:System.Windows.FontWeight" /> valor que representa o peso relativo da face de tipos.</returns>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000B1CB8 File Offset: 0x000B10B8
		public FontWeight Weight
		{
			get
			{
				return this._weight;
			}
		}

		/// <summary>Obtém o estilo da <see cref="T:System.Windows.Media.Typeface" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.FontStyle" /> valor que representa o valor de estilo para a face de tipos.</returns>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000B1CCC File Offset: 0x000B10CC
		public FontStyle Style
		{
			get
			{
				return this._style;
			}
		}

		/// <summary>O valor de ampliação para o <see cref="T:System.Windows.Media.Typeface" />. O valor de ampliação determina se uma face de tipos é expandida ou condensada quando exibida.</summary>
		/// <returns>Um <see cref="T:System.Windows.FontStretch" /> valor que representa o valor de ampliação para a face de tipos.</returns>
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000B1CE0 File Offset: 0x000B10E0
		public FontStretch Stretch
		{
			get
			{
				return this._stretch;
			}
		}

		/// <summary>Determina se é necessário simular um estilo de itálico para os glifos representados pelo <see cref="T:System.Windows.Media.Typeface" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se a simulação em itálico é usada para glifos; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000B1CF4 File Offset: 0x000B10F4
		public bool IsObliqueSimulated
		{
			get
			{
				return (this.CachedTypeface.TypefaceMetrics.StyleSimulations & StyleSimulations.ItalicSimulation) > StyleSimulations.None;
			}
		}

		/// <summary>Determina se é necessário simular um peso de negrito para os glifos representados pelo <see cref="T:System.Windows.Media.Typeface" />.</summary>
		/// <returns>
		///   <see langword="true" /> Se a simulação em negrito é usada para glifos; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000B1D18 File Offset: 0x000B1118
		public bool IsBoldSimulated
		{
			get
			{
				return (this.CachedTypeface.TypefaceMetrics.StyleSimulations & StyleSimulations.BoldSimulation) > StyleSimulations.None;
			}
		}

		/// <summary>Recupera o <see cref="T:System.Windows.Media.GlyphTypeface" /> que corresponde ao <see cref="T:System.Windows.Media.Typeface" />.</summary>
		/// <param name="glyphTypeface">O objeto <see cref="T:System.Windows.Media.GlyphTypeface" /> que corresponde a essa face de tipos ou <see langword="null" />, se a face de tipos foi construída com base em uma fonte de composição.</param>
		/// <returns>
		///   <see langword="true" /> se o parâmetro de saída estiver definido com um valor <see cref="T:System.Windows.Media.GlyphTypeface" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002C95 RID: 11413 RVA: 0x000B1D3C File Offset: 0x000B113C
		public bool TryGetGlyphTypeface(out GlyphTypeface glyphTypeface)
		{
			glyphTypeface = (this.CachedTypeface.TypefaceMetrics as GlyphTypeface);
			return glyphTypeface != null;
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000B1D60 File Offset: 0x000B1160
		internal FontFamily FallbackFontFamily
		{
			get
			{
				return this._fallbackFontFamily;
			}
		}

		/// <summary>Obtém a distância da linha de base até a parte superior de uma minúscula em inglês para a face de tipos. A distância exclui ascendentes.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a distância da linha de base para a parte superior de uma minúscula em inglês (exceto ascendentes), expressada como uma fração do tamanho em da fonte.</returns>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000B1D74 File Offset: 0x000B1174
		public double XHeight
		{
			get
			{
				return this.CachedTypeface.TypefaceMetrics.XHeight;
			}
		}

		/// <summary>Obtém a distância da linha de base até a parte superior de uma maiúscula em inglês para a face de tipos.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a distância da linha de base para a parte superior de uma letra maiuscula em inglês, expressada como uma fração do tamanho em da fonte.</returns>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000B1D94 File Offset: 0x000B1194
		public double CapsHeight
		{
			get
			{
				return this.CachedTypeface.TypefaceMetrics.CapsHeight;
			}
		}

		/// <summary>Obtém um valor que indica a distância do sublinhado da linha de base até a face de tipos.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a posição do sublinhado, medido a partir da linha de base e expresso como uma fração do tamanho em da fonte.</returns>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000B1DB4 File Offset: 0x000B11B4
		public double UnderlinePosition
		{
			get
			{
				return this.CachedTypeface.TypefaceMetrics.UnderlinePosition;
			}
		}

		/// <summary>Obtém um valor que indica a espessura do sublinhado em relação ao tamanho em da fonte para o face de tipos.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a espessura do sublinhado, expressada como uma fração do tamanho em da fonte.</returns>
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000B1DD4 File Offset: 0x000B11D4
		public double UnderlineThickness
		{
			get
			{
				return this.CachedTypeface.TypefaceMetrics.UnderlineThickness;
			}
		}

		/// <summary>Obtém um valor que indica a distância da linha de base para o tachado da face de tipos.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a posição de tachado, medido a partir da linha de base e expresso como uma fração do tamanho em da fonte.</returns>
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000B1DF4 File Offset: 0x000B11F4
		public double StrikethroughPosition
		{
			get
			{
				return this.CachedTypeface.TypefaceMetrics.StrikethroughPosition;
			}
		}

		/// <summary>Obtém um valor que indica a espessura do tachado em relação ao tamanho em da fonte.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a espessura do tachado, expressada como uma fração do tamanho em da fonte.</returns>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000B1E14 File Offset: 0x000B1214
		public double StrikethroughThickness
		{
			get
			{
				return this.CachedTypeface.TypefaceMetrics.StrikethroughThickness;
			}
		}

		/// <summary>Obtém uma coleção de nomes específicos da cultura para o <see cref="T:System.Windows.Media.Typeface" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> valor que representa os nomes de cultura específica para a face de tipos.</returns>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x000B1E34 File Offset: 0x000B1234
		public LanguageSpecificStringDictionary FaceNames
		{
			get
			{
				return new LanguageSpecificStringDictionary(this.CachedTypeface.TypefaceMetrics.AdjustedFaceNames);
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000B1E58 File Offset: 0x000B1258
		internal double Baseline(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode)
		{
			return this.CachedTypeface.FirstFontFamily.Baseline(emSize, toReal, pixelsPerDip, textFormattingMode);
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000B1E7C File Offset: 0x000B127C
		internal double LineSpacing(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode)
		{
			return this.CachedTypeface.FirstFontFamily.LineSpacing(emSize, toReal, pixelsPerDip, textFormattingMode);
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000B1EA0 File Offset: 0x000B12A0
		internal bool Symbol
		{
			get
			{
				return this.CachedTypeface.TypefaceMetrics.Symbol;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x000B1EC0 File Offset: 0x000B12C0
		internal bool NullFont
		{
			get
			{
				return this.CachedTypeface.NullFont;
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000B1ED8 File Offset: 0x000B12D8
		internal GlyphTypeface TryGetGlyphTypeface()
		{
			return this.CachedTypeface.TypefaceMetrics as GlyphTypeface;
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x000B1EF8 File Offset: 0x000B12F8
		internal FontStyle CanonicalStyle
		{
			get
			{
				return this.CachedTypeface.CanonicalStyle;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000B1F10 File Offset: 0x000B1310
		internal FontWeight CanonicalWeight
		{
			get
			{
				return this.CachedTypeface.CanonicalWeight;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x000B1F28 File Offset: 0x000B1328
		internal FontStretch CanonicalStretch
		{
			get
			{
				return this.CachedTypeface.CanonicalStretch;
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000B1F40 File Offset: 0x000B1340
		internal bool CheckFastPathNominalGlyphs(CharacterBufferRange charBufferRange, double emSize, float pixelsPerDip, double scalingFactor, double widthMax, bool keepAWord, bool numberSubstitution, CultureInfo cultureInfo, TextFormattingMode textFormattingMode, bool isSideways, bool breakOnTabs, out int stringLengthFit)
		{
			stringLengthFit = 0;
			if (this.CachedTypeface.NullFont)
			{
				return false;
			}
			GlyphTypeface glyphTypeface = this.TryGetGlyphTypeface();
			if (glyphTypeface == null)
			{
				return false;
			}
			double num = 0.0;
			int num2 = 0;
			ushort blankGlyphIndex = glyphTypeface.BlankGlyphIndex;
			ushort num3 = blankGlyphIndex;
			ushort num4 = numberSubstitution ? 257 : 1;
			ushort num5 = 0;
			ushort num6 = 48;
			bool symbol = glyphTypeface.Symbol;
			if (symbol)
			{
				num4 = 0;
			}
			bool flag = widthMax == double.MaxValue;
			ushort[] ushorts = BufferCache.GetUShorts(charBufferRange.Length);
			GlyphMetrics[] array = flag ? null : BufferCache.GetGlyphMetrics(charBufferRange.Length);
			glyphTypeface.GetGlyphMetricsOptimized(charBufferRange, emSize, pixelsPerDip, ushorts, array, textFormattingMode, isSideways);
			double num7 = emSize / (double)glyphTypeface.DesignEmHeight;
			if (keepAWord)
			{
				for (;;)
				{
					char c = charBufferRange[num2++];
					if (c == '\n' || c == '\r' || (breakOnTabs && c == '\t'))
					{
						break;
					}
					int unicodeClassUTF = (int)Classification.GetUnicodeClassUTF16(c);
					num5 = Classification.CharAttributeOf(unicodeClassUTF).Flags;
					num6 &= num5;
					num3 = ushorts[num2 - 1];
					if (!flag)
					{
						num += TextFormatterImp.RoundDip(array[num2 - 1].AdvanceWidth * num7, (double)pixelsPerDip, textFormattingMode) * scalingFactor;
					}
					if (num2 >= charBufferRange.Length || (num5 & num4) != 0 || (num3 <= 0 && !symbol))
					{
						goto IL_1D0;
					}
					if (num3 == blankGlyphIndex)
					{
						goto Block_13;
					}
				}
				num2--;
				Block_13:;
			}
			IL_1D0:
			while (num2 < charBufferRange.Length && (flag || num <= widthMax) && (num5 & num4) == 0 && (num3 > 0 || symbol))
			{
				char c2 = charBufferRange[num2++];
				if (c2 == '\n' || c2 == '\r' || (breakOnTabs && c2 == '\t'))
				{
					num2--;
					break;
				}
				int unicodeClassUTF2 = (int)Classification.GetUnicodeClassUTF16(c2);
				num5 = Classification.CharAttributeOf(unicodeClassUTF2).Flags;
				num6 &= num5;
				num3 = ushorts[num2 - 1];
				if (!flag)
				{
					num += TextFormatterImp.RoundDip(array[num2 - 1].AdvanceWidth * num7, (double)pixelsPerDip, textFormattingMode) * scalingFactor;
				}
			}
			BufferCache.ReleaseUShorts(ushorts);
			BufferCache.ReleaseGlyphMetrics(array);
			if (symbol)
			{
				stringLengthFit = num2;
				return true;
			}
			if (num3 == 0)
			{
				return false;
			}
			if ((num5 & num4) != 0 && --num2 <= 0)
			{
				return false;
			}
			stringLengthFit = num2;
			TypographyAvailabilities typographyAvailabilities = glyphTypeface.FontFaceLayoutInfo.TypographyAvailabilities;
			if ((num6 & 16) != 0)
			{
				return (typographyAvailabilities & (TypographyAvailabilities.FastTextTypographyAvailable | TypographyAvailabilities.FastTextMajorLanguageLocalizedFormAvailable)) == TypographyAvailabilities.None && ((typographyAvailabilities & TypographyAvailabilities.FastTextExtraLanguageLocalizedFormAvailable) == TypographyAvailabilities.None || MajorLanguages.Contains(cultureInfo));
			}
			if ((num6 & 32) != 0)
			{
				return (typographyAvailabilities & TypographyAvailabilities.IdeoTypographyAvailable) == TypographyAvailabilities.None;
			}
			return (typographyAvailabilities & TypographyAvailabilities.Available) == TypographyAvailabilities.None;
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000B21C0 File Offset: 0x000B15C0
		internal void GetCharacterNominalWidthsAndIdealWidth(CharacterBufferRange charBufferRange, double emSize, float pixelsPerDip, double toIdeal, TextFormattingMode textFormattingMode, bool isSideways, out int[] nominalWidths)
		{
			int num;
			this.GetCharacterNominalWidthsAndIdealWidth(charBufferRange, emSize, pixelsPerDip, toIdeal, textFormattingMode, isSideways, out nominalWidths, out num);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000B21E0 File Offset: 0x000B15E0
		internal void GetCharacterNominalWidthsAndIdealWidth(CharacterBufferRange charBufferRange, double emSize, float pixelsPerDip, double toIdeal, TextFormattingMode textFormattingMode, bool isSideways, out int[] nominalWidths, out int idealWidth)
		{
			GlyphTypeface glyphTypeface = this.TryGetGlyphTypeface();
			Invariant.Assert(glyphTypeface != null);
			GlyphMetrics[] glyphMetrics = BufferCache.GetGlyphMetrics(charBufferRange.Length);
			glyphTypeface.GetGlyphMetricsOptimized(charBufferRange, emSize, pixelsPerDip, textFormattingMode, isSideways, glyphMetrics);
			nominalWidths = new int[charBufferRange.Length];
			idealWidth = 0;
			if (TextFormattingMode.Display == textFormattingMode)
			{
				double num = emSize / (double)glyphTypeface.DesignEmHeight;
				for (int i = 0; i < charBufferRange.Length; i++)
				{
					nominalWidths[i] = (int)Math.Round(TextFormatterImp.RoundDipForDisplayMode(glyphMetrics[i].AdvanceWidth * num, (double)pixelsPerDip) * toIdeal);
					idealWidth += nominalWidths[i];
				}
			}
			else
			{
				double num2 = emSize * toIdeal / (double)glyphTypeface.DesignEmHeight;
				for (int j = 0; j < charBufferRange.Length; j++)
				{
					nominalWidths[j] = (int)Math.Round(glyphMetrics[j].AdvanceWidth * num2);
					idealWidth += nominalWidths[j];
				}
			}
			BufferCache.ReleaseGlyphMetrics(glyphMetrics);
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.Typeface" />. Ele é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash para o objeto atual.</returns>
		// Token: 0x06002CA9 RID: 11433 RVA: 0x000B22D0 File Offset: 0x000B16D0
		public override int GetHashCode()
		{
			int hash = this._fontFamily.GetHashCode();
			if (this._fallbackFontFamily != null)
			{
				hash = HashFn.HashMultiply(hash) + this._fallbackFontFamily.GetHashCode();
			}
			hash = HashFn.HashMultiply(hash) + this._style.GetHashCode();
			hash = HashFn.HashMultiply(hash) + this._weight.GetHashCode();
			hash = HashFn.HashMultiply(hash) + this._stretch.GetHashCode();
			return HashFn.HashScramble(hash);
		}

		/// <summary>Obtém um valor que indica se a face de tipos atual e a face de tipos especificada têm os mesmos <see cref="P:System.Windows.Media.Typeface.FontFamily" />, <see cref="P:System.Windows.Media.Typeface.Style" />, <see cref="P:System.Windows.Media.Typeface.Weight" />, <see cref="P:System.Windows.Media.Typeface.Stretch" /> e valores de fallback de fonte.</summary>
		/// <param name="o">O <see cref="T:System.Windows.Media.Typeface" /> de comparação.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="o" /> for igual ao objeto <see cref="T:System.Windows.Media.Typeface" /> atual; caso contrário, <see langword="false" />. Se <paramref name="o" /> não for um objeto <see cref="T:System.Windows.Media.Typeface" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x06002CAA RID: 11434 RVA: 0x000B2358 File Offset: 0x000B1758
		public override bool Equals(object o)
		{
			Typeface typeface = o as Typeface;
			return typeface != null && (this._style == typeface._style && this._weight == typeface._weight && this._stretch == typeface._stretch && this._fontFamily.Equals(typeface._fontFamily)) && this.CompareFallbackFontFamily(typeface._fallbackFontFamily);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000B23CC File Offset: 0x000B17CC
		internal bool CompareFallbackFontFamily(FontFamily fallbackFontFamily)
		{
			if (fallbackFontFamily == null || this._fallbackFontFamily == null)
			{
				return fallbackFontFamily == this._fallbackFontFamily;
			}
			return this._fallbackFontFamily.Equals(fallbackFontFamily);
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000B23FC File Offset: 0x000B17FC
		private CachedTypeface CachedTypeface
		{
			get
			{
				if (this._cachedTypeface == null)
				{
					CachedTypeface cachedTypeface = TypefaceMetricsCache.ReadonlyLookup(this) as CachedTypeface;
					if (cachedTypeface == null)
					{
						cachedTypeface = this.ConstructCachedTypeface();
						TypefaceMetricsCache.Add(this, cachedTypeface);
					}
					this._cachedTypeface = cachedTypeface;
				}
				return this._cachedTypeface;
			}
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000B243C File Offset: 0x000B183C
		private CachedTypeface ConstructCachedTypeface()
		{
			FontStyle style = this._style;
			FontWeight weight = this._weight;
			FontStretch stretch = this._stretch;
			FontFamily fontFamily = this.FontFamily;
			IFontFamily fontFamily2 = fontFamily.FindFirstFontFamilyAndFace(ref style, ref weight, ref stretch);
			if (fontFamily2 == null)
			{
				if (this.FallbackFontFamily != null)
				{
					fontFamily = this.FallbackFontFamily;
					fontFamily2 = fontFamily.FindFirstFontFamilyAndFace(ref style, ref weight, ref stretch);
				}
				if (fontFamily2 == null)
				{
					fontFamily = null;
					fontFamily2 = FontFamily.LookupFontFamily(FontFamily.NullFontFamilyCanonicalName);
				}
			}
			if (fontFamily != null && fontFamily.Source != null)
			{
				IFontFamily fontFamily3 = TypefaceMetricsCache.ReadonlyLookup(fontFamily.FamilyIdentifier) as IFontFamily;
				if (fontFamily3 != null)
				{
					fontFamily2 = fontFamily3;
				}
				else
				{
					TypefaceMetricsCache.Add(fontFamily.FamilyIdentifier, fontFamily2);
				}
			}
			ITypefaceMetrics typefaceMetrics = fontFamily2.GetTypefaceMetrics(style, weight, stretch);
			return new CachedTypeface(style, weight, stretch, fontFamily2, typefaceMetrics, fontFamily == null);
		}

		// Token: 0x04001462 RID: 5218
		private FontFamily _fontFamily;

		// Token: 0x04001463 RID: 5219
		private FontStyle _style;

		// Token: 0x04001464 RID: 5220
		private FontWeight _weight;

		// Token: 0x04001465 RID: 5221
		private FontStretch _stretch;

		// Token: 0x04001466 RID: 5222
		private FontFamily _fallbackFontFamily;

		// Token: 0x04001467 RID: 5223
		private CachedTypeface _cachedTypeface;
	}
}
