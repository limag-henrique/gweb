using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Markup;
using MS.Internal;
using MS.Internal.FontCache;
using MS.Internal.Shaping;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	// Token: 0x02000591 RID: 1425
	internal sealed class TextShapeableCharacters : TextShapeableSymbols
	{
		// Token: 0x06004194 RID: 16788 RVA: 0x00101928 File Offset: 0x00100D28
		internal TextShapeableCharacters(CharacterBufferRange characterRange, TextRunProperties properties, double emSize, ItemProps textItem, ShapeTypeface shapeTypeface, bool nullShape, TextFormattingMode textFormattingMode, bool isSideways)
		{
			this._isSideways = isSideways;
			this._textFormattingMode = textFormattingMode;
			this._characterBufferRange = characterRange;
			this._properties = properties;
			this._emSize = emSize;
			this._textItem = textItem;
			this._shapeTypeface = shapeTypeface;
			this._nullShape = nullShape;
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x00101978 File Offset: 0x00100D78
		public sealed override CharacterBufferReference CharacterBufferReference
		{
			get
			{
				return this._characterBufferRange.CharacterBufferReference;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x00101990 File Offset: 0x00100D90
		public sealed override int Length
		{
			get
			{
				return this._characterBufferRange.Length;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x001019A8 File Offset: 0x00100DA8
		public sealed override TextRunProperties Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x001019BC File Offset: 0x00100DBC
		internal sealed override GlyphRun ComputeShapedGlyphRun(Point origin, char[] characterString, ushort[] clusterMap, ushort[] glyphIndices, IList<double> glyphAdvances, IList<Point> glyphOffsets, bool rightToLeft, bool sideways)
		{
			Invariant.Assert(this._shapeTypeface != null);
			Invariant.Assert(glyphIndices != null);
			Invariant.Assert(this._shapeTypeface.DeviceFont == null || this._textItem.DigitCulture != null);
			bool[] array = null;
			if (clusterMap != null && (this.HasExtendedCharacter || this.NeedsCaretInfo))
			{
				array = new bool[clusterMap.Length + 1];
				array[0] = true;
				array[clusterMap.Length] = true;
				ushort num = clusterMap[0];
				for (int i = 1; i < clusterMap.Length; i++)
				{
					ushort num2 = clusterMap[i];
					if (num2 != num)
					{
						array[i] = true;
						num = num2;
					}
				}
			}
			return GlyphRun.TryCreate(this._shapeTypeface.GlyphTypeface, rightToLeft ? 1 : 0, sideways, this._emSize, (float)this._properties.PixelsPerDip, glyphIndices, origin, glyphAdvances, glyphOffsets, characterString, null, clusterMap, array, XmlLanguage.GetLanguage(CultureMapper.GetSpecificCulture(this._properties.CultureInfo).IetfLanguageTag), this._textFormattingMode);
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x00101AA8 File Offset: 0x00100EA8
		private GlyphTypeface GetGlyphTypeface(out bool nullFont)
		{
			GlyphTypeface glyphTypeface;
			if (this._shapeTypeface == null)
			{
				Typeface typeface = this._properties.Typeface;
				glyphTypeface = typeface.TryGetGlyphTypeface();
				nullFont = typeface.NullFont;
			}
			else
			{
				glyphTypeface = this._shapeTypeface.GlyphTypeface;
				nullFont = this._nullShape;
			}
			Invariant.Assert(glyphTypeface != null);
			return glyphTypeface;
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x00101AF8 File Offset: 0x00100EF8
		internal sealed override GlyphRun ComputeUnshapedGlyphRun(Point origin, char[] characterString, IList<double> characterAdvances)
		{
			bool nullGlyph;
			GlyphTypeface glyphTypeface = this.GetGlyphTypeface(out nullGlyph);
			Invariant.Assert(glyphTypeface != null);
			return glyphTypeface.ComputeUnshapedGlyphRun(origin, new CharacterBufferRange(characterString, 0, characterString.Length), characterAdvances, this._emSize, (float)this._properties.PixelsPerDip, this._properties.FontHintingEmSize, nullGlyph, CultureMapper.GetSpecificCulture(this._properties.CultureInfo), (this._shapeTypeface == null || this._shapeTypeface.DeviceFont == null) ? null : this._shapeTypeface.DeviceFont.Name, this._textFormattingMode);
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x00101B88 File Offset: 0x00100F88
		internal sealed override void Draw(DrawingContext drawingContext, Brush foregroundBrush, GlyphRun glyphRun)
		{
			if (drawingContext == null)
			{
				throw new ArgumentNullException("drawingContext");
			}
			glyphRun.EmitBackground(drawingContext, this._properties.BackgroundBrush);
			drawingContext.DrawGlyphRun((foregroundBrush != null) ? foregroundBrush : this._properties.ForegroundBrush, glyphRun);
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x00101BD0 File Offset: 0x00100FD0
		internal override double EmSize
		{
			get
			{
				return this._emSize;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x00101BE4 File Offset: 0x00100FE4
		internal override ItemProps ItemProps
		{
			get
			{
				return this._textItem;
			}
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x00101BF8 File Offset: 0x00100FF8
		[SecurityCritical]
		internal unsafe sealed override void GetAdvanceWidthsUnshaped(char* characterString, int characterLength, double scalingFactor, int* advanceWidthsUnshaped)
		{
			if (this.IsShapingRequired)
			{
				GlyphTypeface glyphTypeface = this._shapeTypeface.GlyphTypeface;
				Invariant.Assert(glyphTypeface != null);
				Invariant.Assert(characterLength > 0);
				CharacterBufferRange characters = new CharacterBufferRange(characterString, characterLength);
				GlyphMetrics[] glyphMetrics = BufferCache.GetGlyphMetrics(characterLength);
				glyphTypeface.GetGlyphMetricsOptimized(characters, this._emSize, (float)this._properties.PixelsPerDip, this._textFormattingMode, this._isSideways, glyphMetrics);
				if (this._textFormattingMode == TextFormattingMode.Display && TextFormatterContext.IsSpecialCharacter(*characterString))
				{
					double num = this._emSize / (double)glyphTypeface.DesignEmHeight;
					double pixelsPerDip = this._properties.PixelsPerDip;
					for (int i = 0; i < characterLength; i++)
					{
						advanceWidthsUnshaped[i] = (int)Math.Round(TextFormatterImp.RoundDipForDisplayMode(glyphMetrics[i].AdvanceWidth * num, pixelsPerDip) * scalingFactor);
					}
				}
				else
				{
					double num2 = this._emSize * scalingFactor / (double)glyphTypeface.DesignEmHeight;
					for (int j = 0; j < characterLength; j++)
					{
						advanceWidthsUnshaped[j] = (int)Math.Round(glyphMetrics[j].AdvanceWidth * num2);
					}
				}
				BufferCache.ReleaseGlyphMetrics(glyphMetrics);
				return;
			}
			if (this._shapeTypeface != null && this._shapeTypeface.DeviceFont != null)
			{
				this._shapeTypeface.DeviceFont.GetAdvanceWidths(characterString, characterLength, this._emSize * scalingFactor, advanceWidthsUnshaped);
				return;
			}
			bool nullFont;
			GlyphTypeface glyphTypeface2 = this.GetGlyphTypeface(out nullFont);
			Invariant.Assert(glyphTypeface2 != null);
			glyphTypeface2.GetAdvanceWidthsUnshaped(characterString, characterLength, this._emSize, (float)this._properties.PixelsPerDip, scalingFactor, advanceWidthsUnshaped, nullFont, this._textFormattingMode, this._isSideways);
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x00101D80 File Offset: 0x00101180
		internal sealed override bool NeedsMaxClusterSize
		{
			get
			{
				return !this._textItem.IsLatin || this._textItem.HasCombiningMark || this._textItem.HasExtendedCharacter;
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x00101DB8 File Offset: 0x001011B8
		internal sealed override bool CanShapeTogether(TextShapeableSymbols shapeable)
		{
			TextShapeableCharacters textShapeableCharacters = shapeable as TextShapeableCharacters;
			return textShapeableCharacters != null && (this._shapeTypeface.Equals(textShapeableCharacters._shapeTypeface) && this._textItem.HasExtendedCharacter == textShapeableCharacters._textItem.HasExtendedCharacter && this._emSize == textShapeableCharacters._emSize && ((this._properties.CultureInfo == null) ? (textShapeableCharacters._properties.CultureInfo == null) : this._properties.CultureInfo.Equals(textShapeableCharacters._properties.CultureInfo)) && this._nullShape == textShapeableCharacters._nullShape) && this._textItem.CanShapeTogether(textShapeableCharacters._textItem);
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x00101E68 File Offset: 0x00101268
		internal sealed override bool IsShapingRequired
		{
			get
			{
				return this._shapeTypeface != null && (this._shapeTypeface.DeviceFont == null || this._textItem.DigitCulture != null) && !this.IsSymbol;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x00101EA4 File Offset: 0x001012A4
		internal sealed override bool NeedsCaretInfo
		{
			get
			{
				return this._textItem.HasCombiningMark || this._textItem.NeedsCaretInfo;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x00101ECC File Offset: 0x001012CC
		internal sealed override bool HasExtendedCharacter
		{
			get
			{
				return this._textItem.HasExtendedCharacter;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x00101EE4 File Offset: 0x001012E4
		internal sealed override double Height
		{
			get
			{
				return this._properties.Typeface.LineSpacing(this._properties.FontRenderingEmSize, 1.0, this._properties.PixelsPerDip, this._textFormattingMode);
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x00101F28 File Offset: 0x00101328
		internal sealed override double Baseline
		{
			get
			{
				return this._properties.Typeface.Baseline(this._properties.FontRenderingEmSize, 1.0, this._properties.PixelsPerDip, this._textFormattingMode);
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x00101F6C File Offset: 0x0010136C
		internal sealed override double UnderlinePosition
		{
			get
			{
				return this._properties.Typeface.UnderlinePosition;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x00101F8C File Offset: 0x0010138C
		internal sealed override double UnderlineThickness
		{
			get
			{
				return this._properties.Typeface.UnderlineThickness;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x00101FAC File Offset: 0x001013AC
		internal sealed override double StrikethroughPosition
		{
			get
			{
				return this._properties.Typeface.StrikethroughPosition;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x00101FCC File Offset: 0x001013CC
		internal sealed override double StrikethroughThickness
		{
			get
			{
				return this._properties.Typeface.StrikethroughThickness;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x00101FEC File Offset: 0x001013EC
		internal bool IsSymbol
		{
			get
			{
				if (this._shapeTypeface != null)
				{
					return this._shapeTypeface.GlyphTypeface.Symbol;
				}
				return this._properties.Typeface.Symbol;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x00102024 File Offset: 0x00101424
		internal override GlyphTypeface GlyphTypeFace
		{
			get
			{
				if (this._shapeTypeface != null)
				{
					return this._shapeTypeface.GlyphTypeface;
				}
				return this._properties.Typeface.TryGetGlyphTypeface();
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x00102058 File Offset: 0x00101458
		internal sealed override ushort MaxClusterSize
		{
			get
			{
				if (this._textItem.IsIndic)
				{
					return 15;
				}
				return 8;
			}
		}

		// Token: 0x040017F6 RID: 6134
		private CharacterBufferRange _characterBufferRange;

		// Token: 0x040017F7 RID: 6135
		private TextFormattingMode _textFormattingMode;

		// Token: 0x040017F8 RID: 6136
		private bool _isSideways;

		// Token: 0x040017F9 RID: 6137
		private TextRunProperties _properties;

		// Token: 0x040017FA RID: 6138
		private double _emSize;

		// Token: 0x040017FB RID: 6139
		private ItemProps _textItem;

		// Token: 0x040017FC RID: 6140
		private ShapeTypeface _shapeTypeface;

		// Token: 0x040017FD RID: 6141
		private bool _nullShape;

		// Token: 0x040017FE RID: 6142
		internal const ushort DefaultMaxClusterSize = 8;

		// Token: 0x040017FF RID: 6143
		private const ushort IndicMaxClusterSize = 15;
	}
}
