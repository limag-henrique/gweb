using System;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.FontCache;
using MS.Internal.Shaping;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000703 RID: 1795
	internal sealed class FormattedTextSymbols
	{
		// Token: 0x06004D35 RID: 19765 RVA: 0x00131BB8 File Offset: 0x00130FB8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public unsafe FormattedTextSymbols(GlyphingCache glyphingCache, TextRun textSymbols, bool rightToLeft, double scalingFactor, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			this._textFormattingMode = textFormattingMode;
			this._isSideways = isSideways;
			ITextSymbols textSymbols2 = textSymbols as ITextSymbols;
			IList<TextShapeableSymbols> textShapeableSymbols = textSymbols2.GetTextShapeableSymbols(glyphingCache, textSymbols.CharacterBufferReference, textSymbols.Length, rightToLeft, rightToLeft, null, null, this._textFormattingMode, this._isSideways);
			this._rightToLeft = rightToLeft;
			this._glyphs = new FormattedTextSymbols.Glyphs[textShapeableSymbols.Count];
			CharacterBuffer characterBuffer = textSymbols.CharacterBufferReference.CharacterBuffer;
			int offsetToFirstChar = textSymbols.CharacterBufferReference.OffsetToFirstChar;
			int i = 0;
			int num = 0;
			while (i < textShapeableSymbols.Count)
			{
				TextShapeableSymbols textShapeableSymbols2 = textShapeableSymbols[i];
				int length = textShapeableSymbols2.Length;
				char[] array = new char[length];
				for (int j = 0; j < length; j++)
				{
					array[j] = characterBuffer[offsetToFirstChar + num + j];
				}
				if (textShapeableSymbols2.IsShapingRequired)
				{
					ushort[] clusterMap;
					ushort[] glyphIndices;
					int[] glyphAdvances;
					GlyphOffset[] glyphOffsets;
					fixed (char* ptr = &array[0])
					{
						char* textString = ptr;
						TextAnalyzer textAnalyzer = DWriteFactory.Instance.CreateTextAnalyzer();
						GlyphTypeface glyphTypeFace = textShapeableSymbols2.GlyphTypeFace;
						uint num2 = checked((uint)length);
						DWriteFontFeature[][] features;
						uint[] featureRangeLengths;
						LSRun.CompileFeatureSet(textShapeableSymbols2.Properties.TypographyProperties, num2, out features, out featureRangeLengths);
						textAnalyzer.GetGlyphsAndTheirPlacements((ushort*)textString, num2, glyphTypeFace.FontDWrite, glyphTypeFace.BlankGlyphIndex, false, rightToLeft, textShapeableSymbols2.Properties.CultureInfo, features, featureRangeLengths, textShapeableSymbols2.Properties.FontRenderingEmSize, scalingFactor, pixelsPerDip, this._textFormattingMode, textShapeableSymbols2.ItemProps, out clusterMap, out glyphIndices, out glyphAdvances, out glyphOffsets);
					}
					this._glyphs[i] = new FormattedTextSymbols.Glyphs(textShapeableSymbols2, array, glyphAdvances, clusterMap, glyphIndices, glyphOffsets, scalingFactor);
				}
				else
				{
					int[] array2 = new int[array.Length];
					fixed (char* ptr2 = &array[0])
					{
						char* characterString = ptr2;
						fixed (int* ptr3 = &array2[0])
						{
							int* advanceWidthsUnshaped = ptr3;
							textShapeableSymbols2.GetAdvanceWidthsUnshaped(characterString, length, scalingFactor, advanceWidthsUnshaped);
						}
					}
					this._glyphs[i] = new FormattedTextSymbols.Glyphs(textShapeableSymbols2, array, array2, scalingFactor);
				}
				i++;
				num += length;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06004D36 RID: 19766 RVA: 0x00131DA4 File Offset: 0x001311A4
		public double Width
		{
			get
			{
				double num = 0.0;
				foreach (FormattedTextSymbols.Glyphs glyphs2 in this._glyphs)
				{
					num += glyphs2.Width;
				}
				return num;
			}
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x00131DE0 File Offset: 0x001311E0
		public Rect Draw(DrawingContext drawingContext, Point currentOrigin)
		{
			Rect empty = Rect.Empty;
			FormattedTextSymbols.Glyphs[] glyphs = this._glyphs;
			int i = 0;
			while (i < glyphs.Length)
			{
				FormattedTextSymbols.Glyphs glyphs2 = glyphs[i];
				GlyphRun glyphRun = glyphs2.CreateGlyphRun(currentOrigin, this._rightToLeft);
				if (glyphRun == null)
				{
					goto IL_66;
				}
				Rect rect = glyphRun.ComputeInkBoundingBox();
				if (drawingContext != null)
				{
					glyphRun.EmitBackground(drawingContext, glyphs2.BackgroundBrush);
					drawingContext.PushGuidelineY1(currentOrigin.Y);
					try
					{
						drawingContext.DrawGlyphRun(glyphs2.ForegroundBrush, glyphRun);
						goto IL_6C;
					}
					finally
					{
						drawingContext.Pop();
					}
					goto IL_66;
				}
				IL_6C:
				if (!rect.IsEmpty)
				{
					rect.X += glyphRun.BaselineOrigin.X;
					rect.Y += glyphRun.BaselineOrigin.Y;
				}
				empty.Union(rect);
				if (this._rightToLeft)
				{
					currentOrigin.X -= glyphs2.Width;
				}
				else
				{
					currentOrigin.X += glyphs2.Width;
				}
				i++;
				continue;
				IL_66:
				rect = Rect.Empty;
				goto IL_6C;
			}
			return empty;
		}

		// Token: 0x04002192 RID: 8594
		private FormattedTextSymbols.Glyphs[] _glyphs;

		// Token: 0x04002193 RID: 8595
		private bool _rightToLeft;

		// Token: 0x04002194 RID: 8596
		private TextFormattingMode _textFormattingMode;

		// Token: 0x04002195 RID: 8597
		private bool _isSideways;

		// Token: 0x020009D9 RID: 2521
		private sealed class Glyphs
		{
			// Token: 0x06005B29 RID: 23337 RVA: 0x0016D5D8 File Offset: 0x0016C9D8
			internal Glyphs(TextShapeableSymbols shapeable, char[] charArray, int[] nominalAdvances, double scalingFactor) : this(shapeable, charArray, nominalAdvances, null, null, null, scalingFactor)
			{
			}

			// Token: 0x06005B2A RID: 23338 RVA: 0x0016D5F4 File Offset: 0x0016C9F4
			internal Glyphs(TextShapeableSymbols shapeable, char[] charArray, int[] glyphAdvances, ushort[] clusterMap, ushort[] glyphIndices, GlyphOffset[] glyphOffsets, double scalingFactor)
			{
				this._shapeable = shapeable;
				this._charArray = charArray;
				this._glyphAdvances = new double[glyphAdvances.Length];
				double num = 1.0 / scalingFactor;
				for (int i = 0; i < glyphAdvances.Length; i++)
				{
					this._glyphAdvances[i] = (double)glyphAdvances[i] * num;
					this._width += this._glyphAdvances[i];
				}
				if (glyphIndices != null)
				{
					this._clusterMap = clusterMap;
					if (glyphOffsets != null)
					{
						this._glyphOffsets = new PartialArray<Point>(new Point[glyphOffsets.Length]);
						for (int j = 0; j < glyphOffsets.Length; j++)
						{
							this._glyphOffsets[j] = new Point((double)glyphOffsets[j].du * num, (double)glyphOffsets[j].dv * num);
						}
					}
					if (glyphAdvances.Length != glyphIndices.Length)
					{
						this._glyphIndices = new ushort[glyphAdvances.Length];
						for (int k = 0; k < glyphAdvances.Length; k++)
						{
							this._glyphIndices[k] = glyphIndices[k];
						}
						return;
					}
					this._glyphIndices = glyphIndices;
				}
			}

			// Token: 0x17001290 RID: 4752
			// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0016D708 File Offset: 0x0016CB08
			public double Width
			{
				get
				{
					return this._width;
				}
			}

			// Token: 0x06005B2C RID: 23340 RVA: 0x0016D71C File Offset: 0x0016CB1C
			internal GlyphRun CreateGlyphRun(Point currentOrigin, bool rightToLeft)
			{
				if (!this._shapeable.IsShapingRequired)
				{
					return this._shapeable.ComputeUnshapedGlyphRun(currentOrigin, this._charArray, this._glyphAdvances);
				}
				return this._shapeable.ComputeShapedGlyphRun(currentOrigin, this._charArray, this._clusterMap, this._glyphIndices, this._glyphAdvances, this._glyphOffsets, rightToLeft, false);
			}

			// Token: 0x17001291 RID: 4753
			// (get) Token: 0x06005B2D RID: 23341 RVA: 0x0016D77C File Offset: 0x0016CB7C
			public Brush ForegroundBrush
			{
				get
				{
					return this._shapeable.Properties.ForegroundBrush;
				}
			}

			// Token: 0x17001292 RID: 4754
			// (get) Token: 0x06005B2E RID: 23342 RVA: 0x0016D79C File Offset: 0x0016CB9C
			public Brush BackgroundBrush
			{
				get
				{
					return this._shapeable.Properties.BackgroundBrush;
				}
			}

			// Token: 0x04002E53 RID: 11859
			private TextShapeableSymbols _shapeable;

			// Token: 0x04002E54 RID: 11860
			private char[] _charArray;

			// Token: 0x04002E55 RID: 11861
			private ushort[] _clusterMap;

			// Token: 0x04002E56 RID: 11862
			private ushort[] _glyphIndices;

			// Token: 0x04002E57 RID: 11863
			private double[] _glyphAdvances;

			// Token: 0x04002E58 RID: 11864
			private IList<Point> _glyphOffsets;

			// Token: 0x04002E59 RID: 11865
			private double _width;
		}
	}
}
