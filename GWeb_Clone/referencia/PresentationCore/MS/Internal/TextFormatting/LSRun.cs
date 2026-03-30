using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000753 RID: 1875
	internal sealed class LSRun
	{
		// Token: 0x06004E71 RID: 20081 RVA: 0x00136F40 File Offset: 0x00136340
		internal LSRun(TextRunInfo runInfo, IList<TextEffect> textEffects, Plsrun type, int offsetToFirstCp, int textRunLength, int emSize, ushort charFlags, CharacterBufferRange charBufferRange, TextShapeableSymbols shapeable, double realToIdeal, byte bidiLevel) : this(runInfo, textEffects, type, offsetToFirstCp, textRunLength, emSize, charFlags, charBufferRange, (shapeable != null) ? ((int)Math.Round(shapeable.Baseline * realToIdeal)) : 0, (shapeable != null) ? ((int)Math.Round(shapeable.Height * realToIdeal)) : 0, shapeable, bidiLevel)
		{
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x00136F94 File Offset: 0x00136394
		private LSRun(TextRunInfo runInfo, IList<TextEffect> textEffects, Plsrun type, int offsetToFirstCp, int textRunLength, int emSize, ushort charFlags, CharacterBufferRange charBufferRange, int baselineOffset, int height, TextShapeableSymbols shapeable, byte bidiLevel)
		{
			this._runInfo = runInfo;
			this._type = type;
			this._offsetToFirstCp = offsetToFirstCp;
			this._textRunLength = textRunLength;
			this._emSize = emSize;
			this._charFlags = charFlags;
			this._charBufferRange = charBufferRange;
			this._baselineOffset = baselineOffset;
			this._height = height;
			this._bidiLevel = bidiLevel;
			this._shapeable = shapeable;
			this._textEffects = textEffects;
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x00137004 File Offset: 0x00136404
		internal LSRun(Plsrun type, IntPtr controlChar) : this(null, type, controlChar, 0, -1, 0)
		{
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x00137020 File Offset: 0x00136420
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe LSRun(TextRunInfo runInfo, Plsrun type, IntPtr controlChar, int textRunLength, int offsetToFirstCp, byte bidiLevel)
		{
			this._runInfo = runInfo;
			this._type = type;
			this._charBufferRange = new CharacterBufferRange((char*)((void*)controlChar), 1);
			this._textRunLength = textRunLength;
			this._offsetToFirstCp = offsetToFirstCp;
			this._bidiLevel = bidiLevel;
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x0013706C File Offset: 0x0013646C
		internal void Truncate(int newLength)
		{
			this._charBufferRange = new CharacterBufferRange(this._charBufferRange.CharacterBufferReference, newLength);
			this._textRunLength = newLength;
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06004E76 RID: 20086 RVA: 0x00137098 File Offset: 0x00136498
		internal bool IsHitTestable
		{
			get
			{
				return this._type == Plsrun.Text;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06004E77 RID: 20087 RVA: 0x001370B0 File Offset: 0x001364B0
		internal bool IsVisible
		{
			get
			{
				return this._type == Plsrun.Text || this._type == Plsrun.InlineObject;
			}
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06004E78 RID: 20088 RVA: 0x001370D4 File Offset: 0x001364D4
		internal bool IsNewline
		{
			get
			{
				return this._type == Plsrun.LineBreak || this._type == Plsrun.ParaBreak;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x001370F8 File Offset: 0x001364F8
		internal bool NeedsCaretInfo
		{
			get
			{
				return this._shapeable != null && this._shapeable.NeedsCaretInfo;
			}
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x0013711C File Offset: 0x0013651C
		internal bool HasExtendedCharacter
		{
			get
			{
				return this._shapeable != null && this._shapeable.HasExtendedCharacter;
			}
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x00137140 File Offset: 0x00136540
		internal Rect DrawGlyphRun(DrawingContext drawingContext, Brush foregroundBrush, GlyphRun glyphRun)
		{
			Rect result = glyphRun.ComputeInkBoundingBox();
			if (!result.IsEmpty)
			{
				result.X += glyphRun.BaselineOrigin.X;
				result.Y += glyphRun.BaselineOrigin.Y;
			}
			if (drawingContext != null)
			{
				int num = 0;
				try
				{
					if (this._textEffects != null)
					{
						for (int i = 0; i < this._textEffects.Count; i++)
						{
							TextEffect textEffect = this._textEffects[i];
							if (textEffect.Transform != null && textEffect.Transform != Transform.Identity)
							{
								drawingContext.PushTransform(textEffect.Transform);
								num++;
							}
							if (textEffect.Clip != null)
							{
								drawingContext.PushClip(textEffect.Clip);
								num++;
							}
							if (textEffect.Foreground != null)
							{
								foregroundBrush = textEffect.Foreground;
							}
						}
					}
					this._shapeable.Draw(drawingContext, foregroundBrush, glyphRun);
				}
				finally
				{
					for (int j = 0; j < num; j++)
					{
						drawingContext.Pop();
					}
				}
			}
			return result;
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x0013725C File Offset: 0x0013665C
		internal static Point UVToXY(Point origin, Point vectorToOrigin, double u, double v, TextMetrics.FullTextLine line)
		{
			origin.Y += vectorToOrigin.Y;
			Point result;
			if (line.RightToLeft)
			{
				result = new Point(line.Formatter.IdealToReal((double)line.ParagraphWidth, line.PixelsPerDip) - vectorToOrigin.X - u + origin.X, v + origin.Y);
			}
			else
			{
				result = new Point(u + vectorToOrigin.X + origin.X, v + origin.Y);
			}
			return result;
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x001372E8 File Offset: 0x001366E8
		internal static Point UVToXY(Point origin, Point vectorToOrigin, int u, int v, TextMetrics.FullTextLine line)
		{
			origin.Y += vectorToOrigin.Y;
			Point result;
			if (line.RightToLeft)
			{
				result = new Point(line.Formatter.IdealToReal((double)(line.ParagraphWidth - u), line.PixelsPerDip) - vectorToOrigin.X + origin.X, line.Formatter.IdealToReal((double)v, line.PixelsPerDip) + origin.Y);
			}
			else
			{
				result = new Point(line.Formatter.IdealToReal((double)u, line.PixelsPerDip) + vectorToOrigin.X + origin.X, line.Formatter.IdealToReal((double)v, line.PixelsPerDip) + origin.Y);
			}
			return result;
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x001373B0 File Offset: 0x001367B0
		internal static void UVToNominalXY(Point origin, Point vectorToOrigin, int u, int v, TextMetrics.FullTextLine line, out int nominalX, out int nominalY)
		{
			origin.Y += vectorToOrigin.Y;
			if (line.RightToLeft)
			{
				nominalX = line.ParagraphWidth - u + TextFormatterImp.RealToIdeal(-vectorToOrigin.X + origin.X);
			}
			else
			{
				nominalX = u + TextFormatterImp.RealToIdeal(vectorToOrigin.X + origin.X);
			}
			nominalY = v + TextFormatterImp.RealToIdeal(origin.Y);
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x0013742C File Offset: 0x0013682C
		internal static Rect RectUV(Point origin, LSPOINT topLeft, LSPOINT bottomRight, TextMetrics.FullTextLine line)
		{
			int num = topLeft.x - bottomRight.x;
			if (num == 1 || num == -1)
			{
				bottomRight.x = topLeft.x;
			}
			Rect result = new Rect(new Point(line.Formatter.IdealToReal((double)topLeft.x, line.PixelsPerDip), line.Formatter.IdealToReal((double)topLeft.y, line.PixelsPerDip)), new Point(line.Formatter.IdealToReal((double)bottomRight.x, line.PixelsPerDip), line.Formatter.IdealToReal((double)bottomRight.y, line.PixelsPerDip)));
			if (DoubleUtil.AreClose(result.TopLeft.X, result.BottomRight.X))
			{
				result.Width = 0.0;
			}
			if (DoubleUtil.AreClose(result.TopLeft.Y, result.BottomRight.Y))
			{
				result.Height = 0.0;
			}
			return result;
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x00137538 File Offset: 0x00136938
		internal void Move(int baselineMoveOffset)
		{
			this._baselineMoveOffset += baselineMoveOffset;
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x00137554 File Offset: 0x00136954
		internal byte BidiLevel
		{
			get
			{
				return this._bidiLevel;
			}
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x00137568 File Offset: 0x00136968
		internal bool IsSymbol
		{
			get
			{
				TextShapeableCharacters textShapeableCharacters = this._shapeable as TextShapeableCharacters;
				return textShapeableCharacters != null && textShapeableCharacters.IsSymbol;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x0013758C File Offset: 0x0013698C
		internal int OffsetToFirstCp
		{
			get
			{
				return this._offsetToFirstCp;
			}
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x001375A0 File Offset: 0x001369A0
		internal int Length
		{
			get
			{
				return this._textRunLength;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x001375B4 File Offset: 0x001369B4
		internal TextModifierScope TextModifierScope
		{
			get
			{
				return this._runInfo.TextModifierScope;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x001375CC File Offset: 0x001369CC
		internal Plsrun Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x001375E0 File Offset: 0x001369E0
		internal ushort CharacterAttributeFlags
		{
			get
			{
				return this._charFlags;
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x001375F4 File Offset: 0x001369F4
		internal CharacterBuffer CharacterBuffer
		{
			get
			{
				return this._charBufferRange.CharacterBuffer;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06004E89 RID: 20105 RVA: 0x0013760C File Offset: 0x00136A0C
		internal int StringLength
		{
			get
			{
				return this._charBufferRange.Length;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x00137624 File Offset: 0x00136A24
		internal int OffsetToFirstChar
		{
			get
			{
				return this._charBufferRange.OffsetToFirstChar;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06004E8B RID: 20107 RVA: 0x0013763C File Offset: 0x00136A3C
		internal TextRun TextRun
		{
			get
			{
				return this._runInfo.TextRun;
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06004E8C RID: 20108 RVA: 0x00137654 File Offset: 0x00136A54
		internal TextShapeableSymbols Shapeable
		{
			get
			{
				return this._shapeable;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x00137668 File Offset: 0x00136A68
		// (set) Token: 0x06004E8E RID: 20110 RVA: 0x0013767C File Offset: 0x00136A7C
		internal int BaselineOffset
		{
			get
			{
				return this._baselineOffset;
			}
			set
			{
				this._baselineOffset = value;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x00137690 File Offset: 0x00136A90
		// (set) Token: 0x06004E90 RID: 20112 RVA: 0x001376A4 File Offset: 0x00136AA4
		internal int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x001376B8 File Offset: 0x00136AB8
		internal int Descent
		{
			get
			{
				return this.Height - this.BaselineOffset;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x001376D4 File Offset: 0x00136AD4
		internal TextRunProperties RunProp
		{
			get
			{
				return this._runInfo.Properties;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x001376EC File Offset: 0x00136AEC
		internal CultureInfo TextCulture
		{
			get
			{
				return CultureMapper.GetSpecificCulture((this.RunProp != null) ? this.RunProp.CultureInfo : null);
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x00137714 File Offset: 0x00136B14
		internal int EmSize
		{
			get
			{
				return this._emSize;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x00137728 File Offset: 0x00136B28
		internal int BaselineMoveOffset
		{
			get
			{
				return this._baselineMoveOffset;
			}
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0013773C File Offset: 0x00136B3C
		private static DWriteFontFeature[] CreateDWriteFontFeatures(TextRunTypographyProperties textRunTypographyProperties)
		{
			if (textRunTypographyProperties == null)
			{
				return null;
			}
			if (textRunTypographyProperties.CachedFeatureSet != null)
			{
				return textRunTypographyProperties.CachedFeatureSet;
			}
			List<DWriteFontFeature> list = new List<DWriteFontFeature>(73);
			if (textRunTypographyProperties.CapitalSpacing)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.CapitalSpacing, 1U));
			}
			if (textRunTypographyProperties.CaseSensitiveForms)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.CaseSensitiveForms, 1U));
			}
			if (textRunTypographyProperties.ContextualAlternates)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ContextualAlternates, 1U));
			}
			if (textRunTypographyProperties.ContextualLigatures)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ContextualLigatures, 1U));
			}
			if (textRunTypographyProperties.DiscretionaryLigatures)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.DiscretionaryLigatures, 1U));
			}
			if (textRunTypographyProperties.HistoricalForms)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.HistoricalForms, 1U));
			}
			if (textRunTypographyProperties.HistoricalLigatures)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.HistoricalLigatures, 1U));
			}
			if (textRunTypographyProperties.Kerning)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Kerning, 1U));
			}
			if (textRunTypographyProperties.MathematicalGreek)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.MathematicalGreek, 1U));
			}
			if (textRunTypographyProperties.SlashedZero)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.SlashedZero, 1U));
			}
			if (textRunTypographyProperties.StandardLigatures)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StandardLigatures, 1U));
			}
			if (textRunTypographyProperties.StylisticSet1)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet1, 1U));
			}
			if (textRunTypographyProperties.StylisticSet10)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet10, 1U));
			}
			if (textRunTypographyProperties.StylisticSet11)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet11, 1U));
			}
			if (textRunTypographyProperties.StylisticSet12)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet12, 1U));
			}
			if (textRunTypographyProperties.StylisticSet13)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet13, 1U));
			}
			if (textRunTypographyProperties.StylisticSet14)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet14, 1U));
			}
			if (textRunTypographyProperties.StylisticSet15)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet15, 1U));
			}
			if (textRunTypographyProperties.StylisticSet16)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet16, 1U));
			}
			if (textRunTypographyProperties.StylisticSet17)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet17, 1U));
			}
			if (textRunTypographyProperties.StylisticSet18)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet18, 1U));
			}
			if (textRunTypographyProperties.StylisticSet19)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet19, 1U));
			}
			if (textRunTypographyProperties.StylisticSet2)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet2, 1U));
			}
			if (textRunTypographyProperties.StylisticSet20)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet20, 1U));
			}
			if (textRunTypographyProperties.StylisticSet3)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet3, 1U));
			}
			if (textRunTypographyProperties.StylisticSet4)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet4, 1U));
			}
			if (textRunTypographyProperties.StylisticSet5)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet5, 1U));
			}
			if (textRunTypographyProperties.StylisticSet6)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet6, 1U));
			}
			if (textRunTypographyProperties.StylisticSet7)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet7, 1U));
			}
			if (textRunTypographyProperties.StylisticSet8)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet8, 1U));
			}
			if (textRunTypographyProperties.StylisticSet9)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticSet9, 1U));
			}
			if (textRunTypographyProperties.EastAsianExpertForms)
			{
				list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ExpertForms, 1U));
			}
			checked
			{
				if (textRunTypographyProperties.AnnotationAlternates > 0)
				{
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.AlternateAnnotationForms, (uint)textRunTypographyProperties.AnnotationAlternates));
				}
				if (textRunTypographyProperties.ContextualSwashes > 0)
				{
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ContextualSwash, (uint)textRunTypographyProperties.ContextualSwashes));
				}
				if (textRunTypographyProperties.StylisticAlternates > 0)
				{
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.StylisticAlternates, (uint)textRunTypographyProperties.StylisticAlternates));
				}
				if (textRunTypographyProperties.StandardSwashes > 0)
				{
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Swash, (uint)textRunTypographyProperties.StandardSwashes));
				}
				switch (textRunTypographyProperties.Capitals)
				{
				case FontCapitals.AllSmallCaps:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.SmallCapitals, 1U));
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.SmallCapitalsFromCapitals, 1U));
					break;
				case FontCapitals.SmallCaps:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.SmallCapitals, 1U));
					break;
				case FontCapitals.AllPetiteCaps:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.PetiteCapitals, 1U));
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.PetiteCapitalsFromCapitals, 1U));
					break;
				case FontCapitals.PetiteCaps:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.PetiteCapitals, 1U));
					break;
				case FontCapitals.Unicase:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Unicase, 1U));
					break;
				case FontCapitals.Titling:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Titling, 1U));
					break;
				}
				switch (textRunTypographyProperties.EastAsianLanguage)
				{
				case FontEastAsianLanguage.Jis78:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.JIS78Forms, 1U));
					break;
				case FontEastAsianLanguage.Jis83:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.JIS83Forms, 1U));
					break;
				case FontEastAsianLanguage.Jis90:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.JIS90Forms, 1U));
					break;
				case FontEastAsianLanguage.Jis04:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.JIS04Forms, 1U));
					break;
				case FontEastAsianLanguage.HojoKanji:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.HojoKanjiForms, 1U));
					break;
				case FontEastAsianLanguage.NlcKanji:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.NLCKanjiForms, 1U));
					break;
				case FontEastAsianLanguage.Simplified:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.SimplifiedForms, 1U));
					break;
				case FontEastAsianLanguage.Traditional:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.TraditionalForms, 1U));
					break;
				case FontEastAsianLanguage.TraditionalNames:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.TraditionalNameForms, 1U));
					break;
				}
				FontFraction fraction = textRunTypographyProperties.Fraction;
				if (fraction != FontFraction.Slashed)
				{
					if (fraction == FontFraction.Stacked)
					{
						list.Add(new DWriteFontFeature(DWriteFontFeatureTag.AlternativeFractions, 1U));
					}
				}
				else
				{
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Fractions, 1U));
				}
				FontNumeralAlignment numeralAlignment = textRunTypographyProperties.NumeralAlignment;
				if (numeralAlignment != FontNumeralAlignment.Proportional)
				{
					if (numeralAlignment == FontNumeralAlignment.Tabular)
					{
						list.Add(new DWriteFontFeature(DWriteFontFeatureTag.TabularFigures, 1U));
					}
				}
				else
				{
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ProportionalFigures, 1U));
				}
				FontNumeralStyle numeralStyle = textRunTypographyProperties.NumeralStyle;
				if (numeralStyle != FontNumeralStyle.Lining)
				{
					if (numeralStyle == FontNumeralStyle.OldStyle)
					{
						list.Add(new DWriteFontFeature(DWriteFontFeatureTag.OldStyleFigures, 1U));
					}
				}
				else
				{
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.LiningFigures, 1U));
				}
				switch (textRunTypographyProperties.Variants)
				{
				case FontVariants.Superscript:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Superscript, 1U));
					break;
				case FontVariants.Subscript:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Subscript, 1U));
					break;
				case FontVariants.Ordinal:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.Ordinals, 1U));
					break;
				case FontVariants.Inferior:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ScientificInferiors, 1U));
					break;
				case FontVariants.Ruby:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.RubyNotationForms, 1U));
					break;
				}
				switch (textRunTypographyProperties.EastAsianWidths)
				{
				case FontEastAsianWidths.Proportional:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ProportionalWidths, 1U));
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ProportionalAlternateWidth, 1U));
					break;
				case FontEastAsianWidths.Full:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.FullWidth, 1U));
					break;
				case FontEastAsianWidths.Half:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.HalfWidth, 1U));
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.AlternateHalfWidth, 1U));
					break;
				case FontEastAsianWidths.Third:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.ThirdWidths, 1U));
					break;
				case FontEastAsianWidths.Quarter:
					list.Add(new DWriteFontFeature(DWriteFontFeatureTag.QuarterWidths, 1U));
					break;
				}
				textRunTypographyProperties.CachedFeatureSet = list.ToArray();
				return textRunTypographyProperties.CachedFeatureSet;
			}
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00137E90 File Offset: 0x00137290
		[SecurityCritical]
		internal unsafe static void CompileFeatureSet(LSRun[] lsruns, int* pcchRuns, uint totalLength, out DWriteFontFeature[][] fontFeatures, out uint[] fontFeatureRanges)
		{
			if (lsruns[0].RunProp.TypographyProperties == null)
			{
				for (int i = 1; i < lsruns.Length; i++)
				{
					if (lsruns[i].RunProp.TypographyProperties != null)
					{
						throw new ArgumentException(SR.Get("CompileFeatureSet_InvalidTypographyProperties"));
					}
				}
				fontFeatures = null;
				fontFeatureRanges = null;
				return;
			}
			fontFeatures = new DWriteFontFeature[lsruns.Length][];
			fontFeatureRanges = new uint[lsruns.Length];
			for (int j = 0; j < lsruns.Length; j++)
			{
				TextRunTypographyProperties typographyProperties = lsruns[j].RunProp.TypographyProperties;
				fontFeatures[j] = LSRun.CreateDWriteFontFeatures(typographyProperties);
				fontFeatureRanges[j] = checked((uint)unchecked(pcchRuns[j]));
			}
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x00137F2C File Offset: 0x0013732C
		internal static void CompileFeatureSet(TextRunTypographyProperties textRunTypographyProperties, uint totalLength, out DWriteFontFeature[][] fontFeatures, out uint[] fontFeatureRanges)
		{
			if (textRunTypographyProperties == null)
			{
				fontFeatures = null;
				fontFeatureRanges = null;
				return;
			}
			fontFeatures = new DWriteFontFeature[1][];
			fontFeatureRanges = new uint[1];
			fontFeatures[0] = LSRun.CreateDWriteFontFeatures(textRunTypographyProperties);
			fontFeatureRanges[0] = totalLength;
		}

		// Token: 0x040023AF RID: 9135
		private TextRunInfo _runInfo;

		// Token: 0x040023B0 RID: 9136
		private Plsrun _type;

		// Token: 0x040023B1 RID: 9137
		private int _offsetToFirstCp;

		// Token: 0x040023B2 RID: 9138
		private int _textRunLength;

		// Token: 0x040023B3 RID: 9139
		private CharacterBufferRange _charBufferRange;

		// Token: 0x040023B4 RID: 9140
		private int _baselineOffset;

		// Token: 0x040023B5 RID: 9141
		private int _height;

		// Token: 0x040023B6 RID: 9142
		private int _baselineMoveOffset;

		// Token: 0x040023B7 RID: 9143
		private int _emSize;

		// Token: 0x040023B8 RID: 9144
		private TextShapeableSymbols _shapeable;

		// Token: 0x040023B9 RID: 9145
		private ushort _charFlags;

		// Token: 0x040023BA RID: 9146
		private byte _bidiLevel;

		// Token: 0x040023BB RID: 9147
		private IList<TextEffect> _textEffects;

		// Token: 0x040023BC RID: 9148
		private const ushort FeatureNotEnabled = 65535;
	}
}
