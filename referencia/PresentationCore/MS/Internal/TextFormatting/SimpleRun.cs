using System;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000759 RID: 1881
	internal sealed class SimpleRun
	{
		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x00139B6C File Offset: 0x00138F6C
		internal bool EOT
		{
			get
			{
				return (this.RunFlags & SimpleRun.Flags.EOT) > SimpleRun.Flags.None;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x00139B84 File Offset: 0x00138F84
		internal bool Ghost
		{
			get
			{
				return (this.RunFlags & SimpleRun.Flags.Ghost) > SimpleRun.Flags.None;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x00139B9C File Offset: 0x00138F9C
		internal bool Tab
		{
			get
			{
				return (this.RunFlags & SimpleRun.Flags.Tab) > SimpleRun.Flags.None;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x00139BB4 File Offset: 0x00138FB4
		// (set) Token: 0x06004EF6 RID: 20214 RVA: 0x00139BCC File Offset: 0x00138FCC
		internal bool TrimTrailingUnderline
		{
			get
			{
				return (this.RunFlags & SimpleRun.Flags.TrimTrailingUnderline) > SimpleRun.Flags.None;
			}
			set
			{
				if (value)
				{
					this.RunFlags |= SimpleRun.Flags.TrimTrailingUnderline;
					return;
				}
				this.RunFlags &= ~SimpleRun.Flags.TrimTrailingUnderline;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x00139C00 File Offset: 0x00139000
		internal double Baseline
		{
			get
			{
				if (this.Ghost || this.EOT)
				{
					return 0.0;
				}
				return this.TextRun.Properties.Typeface.Baseline(this.TextRun.Properties.FontRenderingEmSize, 1.0, this._pixelsPerDip, this._textFormatterImp.TextFormattingMode);
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x00139C68 File Offset: 0x00139068
		internal double Height
		{
			get
			{
				if (this.Ghost || this.EOT)
				{
					return 0.0;
				}
				return this.TextRun.Properties.Typeface.LineSpacing(this.TextRun.Properties.FontRenderingEmSize, 1.0, this._pixelsPerDip, this._textFormatterImp.TextFormattingMode);
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x00139CD0 File Offset: 0x001390D0
		internal Typeface Typeface
		{
			get
			{
				return this.TextRun.Properties.Typeface;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x00139CF0 File Offset: 0x001390F0
		internal double EmSize
		{
			get
			{
				return this.TextRun.Properties.FontRenderingEmSize;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x00139D10 File Offset: 0x00139110
		internal bool IsVisible
		{
			get
			{
				return this.TextRun is TextCharacters;
			}
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x00139D2C File Offset: 0x0013912C
		internal SimpleRun(TextFormatterImp textFormatterImp, double pixelsPerDip)
		{
			this._textFormatterImp = textFormatterImp;
			this._pixelsPerDip = pixelsPerDip;
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x00139D50 File Offset: 0x00139150
		public static SimpleRun Create(FormatSettings settings, int cp, int cpFirst, int widthLeft, int widthMax, int idealRunOffsetUnRounded, double pixelsPerDip)
		{
			TextRun textRun;
			int runLength;
			CharacterBufferRange charString = settings.FetchTextRun(cp, cpFirst, out textRun, out runLength);
			return SimpleRun.Create(settings, charString, textRun, cp, cpFirst, runLength, widthLeft, idealRunOffsetUnRounded, pixelsPerDip);
		}

		// Token: 0x06004EFE RID: 20222 RVA: 0x00139D7C File Offset: 0x0013917C
		public static SimpleRun Create(FormatSettings settings, CharacterBufferRange charString, TextRun textRun, int cp, int cpFirst, int runLength, int widthLeft, int idealRunOffsetUnRounded, double pixelsPerDip)
		{
			SimpleRun simpleRun = null;
			if (textRun is TextCharacters)
			{
				if (textRun.Properties.BaselineAlignment != BaselineAlignment.Baseline || (textRun.Properties.TextEffects != null && textRun.Properties.TextEffects.Count != 0))
				{
					return null;
				}
				TextDecorationCollection textDecorations = textRun.Properties.TextDecorations;
				if (textDecorations != null && textDecorations.Count != 0 && !textDecorations.ValueEquals(TextDecorations.Underline))
				{
					return null;
				}
				settings.DigitState.SetTextRunProperties(textRun.Properties);
				if (settings.DigitState.RequiresNumberSubstitution)
				{
					return null;
				}
				bool flag = SimpleRun.CanProcessTabsInSimpleShapingPath(settings.Pap, settings.Formatter.TextFormattingMode);
				if (charString[0] == '\r')
				{
					runLength = 1;
					if (charString.Length > 1 && charString[1] == '\n')
					{
						runLength = 2;
					}
					else if (charString.Length == 1)
					{
						TextRun textRun2;
						int num;
						CharacterBufferRange characterBufferRange = settings.FetchTextRun(cp + 1, cpFirst, out textRun2, out num);
						if (characterBufferRange.Length > 0 && characterBufferRange[0] == '\n')
						{
							int num2 = 2;
							char[] array = new char[num2];
							array[0] = '\r';
							array[1] = '\n';
							TextRun textRun3 = new TextCharacters(array, 0, num2, textRun.Properties);
							return new SimpleRun(num2, textRun3, SimpleRun.Flags.EOT | SimpleRun.Flags.Ghost, settings.Formatter, pixelsPerDip);
						}
					}
					return new SimpleRun(runLength, textRun, SimpleRun.Flags.EOT | SimpleRun.Flags.Ghost, settings.Formatter, pixelsPerDip);
				}
				if (charString[0] == '\n')
				{
					runLength = 1;
					return new SimpleRun(runLength, textRun, SimpleRun.Flags.EOT | SimpleRun.Flags.Ghost, settings.Formatter, pixelsPerDip);
				}
				if (flag && charString[0] == '\t')
				{
					return SimpleRun.CreateSimpleRunForTab(settings, textRun, idealRunOffsetUnRounded, pixelsPerDip);
				}
				simpleRun = SimpleRun.CreateSimpleTextRun(charString, textRun, settings.Formatter, widthLeft, settings.Pap.EmergencyWrap, flag, pixelsPerDip);
				if (simpleRun == null)
				{
					return null;
				}
				if (textDecorations != null && textDecorations.Count == 1)
				{
					simpleRun.Underline = textDecorations[0];
				}
			}
			else if (textRun is TextEndOfLine)
			{
				simpleRun = new SimpleRun(runLength, textRun, SimpleRun.Flags.EOT | SimpleRun.Flags.Ghost, settings.Formatter, pixelsPerDip);
			}
			else if (textRun is TextHidden)
			{
				simpleRun = new SimpleRun(runLength, textRun, SimpleRun.Flags.Ghost, settings.Formatter, pixelsPerDip);
			}
			return simpleRun;
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x00139F7C File Offset: 0x0013937C
		private static SimpleRun CreateSimpleRunForTab(FormatSettings settings, TextRun textRun, int idealRunOffsetUnRounded, double pixelsPerDip)
		{
			if (settings == null || textRun == null || textRun.Properties == null || textRun.Properties.Typeface == null)
			{
				return null;
			}
			GlyphTypeface glyphTypeface = textRun.Properties.Typeface.TryGetGlyphTypeface();
			if (glyphTypeface == null || !glyphTypeface.HasCharacter(32U))
			{
				return null;
			}
			TextRun textRun2 = new TextCharacters(" ", textRun.Properties);
			CharacterBufferRange charBufferRange = new CharacterBufferRange(textRun2);
			SimpleRun simpleRun = new SimpleRun(1, textRun2, SimpleRun.Flags.Tab, settings.Formatter, pixelsPerDip);
			simpleRun.CharBufferReference = charBufferRange.CharacterBufferReference;
			simpleRun.TextRun.Properties.Typeface.GetCharacterNominalWidthsAndIdealWidth(charBufferRange, simpleRun.EmSize, (float)pixelsPerDip, TextFormatterImp.ToIdeal, settings.Formatter.TextFormattingMode, false, out simpleRun.NominalAdvances);
			int num = TextFormatterImp.RealToIdeal(settings.Pap.DefaultIncrementalTab);
			int num2 = (idealRunOffsetUnRounded / num + 1) * num;
			simpleRun.IdealWidth = (simpleRun.NominalAdvances[0] = num2 - idealRunOffsetUnRounded);
			return simpleRun;
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0013A064 File Offset: 0x00139464
		private static bool CanProcessTabsInSimpleShapingPath(ParaProp textParagraphProperties, TextFormattingMode textFormattingMode)
		{
			return textParagraphProperties.Tabs == null && textParagraphProperties.DefaultIncrementalTab > 0.0;
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0013A08C File Offset: 0x0013948C
		internal static SimpleRun CreateSimpleTextRun(CharacterBufferRange charBufferRange, TextRun textRun, TextFormatterImp formatter, int widthLeft, bool emergencyWrap, bool breakOnTabs, double pixelsPerDip)
		{
			Invariant.Assert(textRun is TextCharacters);
			SimpleRun simpleRun = new SimpleRun(formatter, pixelsPerDip);
			simpleRun.CharBufferReference = charBufferRange.CharacterBufferReference;
			simpleRun.TextRun = textRun;
			if (!simpleRun.TextRun.Properties.Typeface.CheckFastPathNominalGlyphs(charBufferRange, simpleRun.EmSize, (float)pixelsPerDip, 1.0, formatter.IdealToReal((double)widthLeft, pixelsPerDip), !emergencyWrap, false, CultureMapper.GetSpecificCulture(simpleRun.TextRun.Properties.CultureInfo), formatter.TextFormattingMode, false, breakOnTabs, out simpleRun.Length))
			{
				return null;
			}
			simpleRun.TextRun.Properties.Typeface.GetCharacterNominalWidthsAndIdealWidth(new CharacterBufferRange(simpleRun.CharBufferReference, simpleRun.Length), simpleRun.EmSize, (float)pixelsPerDip, TextFormatterImp.ToIdeal, formatter.TextFormattingMode, false, out simpleRun.NominalAdvances, out simpleRun.IdealWidth);
			return simpleRun;
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x0013A16C File Offset: 0x0013956C
		private SimpleRun(int length, TextRun textRun, SimpleRun.Flags flags, TextFormatterImp textFormatterImp, double pixelsPerDip)
		{
			this.Length = length;
			this.TextRun = textRun;
			this.RunFlags = flags;
			this._textFormatterImp = textFormatterImp;
			this._pixelsPerDip = pixelsPerDip;
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x0013A1A4 File Offset: 0x001395A4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal Rect Draw(DrawingContext drawingContext, double x, double y, bool visiCodePath)
		{
			if (this.Length <= 0 || this.Ghost)
			{
				return Rect.Empty;
			}
			Brush brush = this.TextRun.Properties.ForegroundBrush;
			if (visiCodePath && brush is SolidColorBrush)
			{
				Color color = ((SolidColorBrush)brush).Color;
				brush = new SolidColorBrush(Color.FromArgb((byte)(color.A >> 2), color.R, color.G, color.B));
			}
			IList<double> list;
			if (this._textFormatterImp.TextFormattingMode == TextFormattingMode.Ideal)
			{
				list = new ThousandthOfEmRealDoubles(this.EmSize, this.NominalAdvances.Length);
				for (int i = 0; i < list.Count; i++)
				{
					list[i] = this._textFormatterImp.IdealToReal((double)this.NominalAdvances[i], this._pixelsPerDip);
				}
			}
			else
			{
				list = new List<double>(this.NominalAdvances.Length);
				for (int j = 0; j < this.NominalAdvances.Length; j++)
				{
					list.Add(this._textFormatterImp.IdealToReal((double)this.NominalAdvances[j], this._pixelsPerDip));
				}
			}
			CharacterBufferRange charBufferRange = new CharacterBufferRange(this.CharBufferReference, this.Length);
			GlyphTypeface glyphTypeface = this.Typeface.TryGetGlyphTypeface();
			Invariant.Assert(glyphTypeface != null);
			GlyphRun glyphRun = glyphTypeface.ComputeUnshapedGlyphRun(new Point(x, y), charBufferRange, list, this.EmSize, (float)this._pixelsPerDip, this.TextRun.Properties.FontHintingEmSize, this.Typeface.NullFont, CultureMapper.GetSpecificCulture(this.TextRun.Properties.CultureInfo), null, this._textFormatterImp.TextFormattingMode);
			Rect result;
			if (glyphRun != null)
			{
				result = glyphRun.ComputeInkBoundingBox();
			}
			else
			{
				result = Rect.Empty;
			}
			if (!result.IsEmpty)
			{
				result.X += glyphRun.BaselineOrigin.X;
				result.Y += glyphRun.BaselineOrigin.Y;
			}
			if (drawingContext != null)
			{
				if (glyphRun != null)
				{
					glyphRun.EmitBackground(drawingContext, this.TextRun.Properties.BackgroundBrush);
					drawingContext.DrawGlyphRun(brush, glyphRun);
				}
				if (this.Underline != null)
				{
					int num = this.Length;
					if (this.TrimTrailingUnderline)
					{
						while (num > 0 && SimpleRun.IsSpace(charBufferRange[num - 1]))
						{
							num--;
						}
					}
					double num2 = 0.0;
					for (int k = 0; k < num; k++)
					{
						num2 += this._textFormatterImp.IdealToReal((double)this.NominalAdvances[k], this._pixelsPerDip);
					}
					double num3 = -this.Typeface.UnderlinePosition * this.EmSize;
					double num4 = this.Typeface.UnderlineThickness * this.EmSize;
					Point point = new Point(x, y + num3);
					Rect rect = new Rect(point.X, point.Y - num4 * 0.5, num2, num4);
					drawingContext.PushGuidelineY2(y, point.Y - num4 * 0.5 - y);
					try
					{
						drawingContext.DrawRectangle(brush, null, rect);
					}
					finally
					{
						drawingContext.Pop();
					}
					result.Union(rect);
				}
			}
			return result;
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x0013A4E4 File Offset: 0x001398E4
		internal bool CollectTrailingSpaces(TextFormatterImp formatter, ref int trailing, ref int trailingSpaceWidth)
		{
			if (this.Ghost)
			{
				if (!this.EOT)
				{
					trailing += this.Length;
					trailingSpaceWidth += this.IdealWidth;
				}
				return true;
			}
			if (this.Tab)
			{
				return false;
			}
			int offsetToFirstChar = this.CharBufferReference.OffsetToFirstChar;
			CharacterBuffer characterBuffer = this.CharBufferReference.CharacterBuffer;
			int num = this.Length;
			if (num > 0 && SimpleRun.IsSpace(characterBuffer[offsetToFirstChar + num - 1]))
			{
				while (num > 0 && SimpleRun.IsSpace(characterBuffer[offsetToFirstChar + num - 1]))
				{
					trailingSpaceWidth += this.NominalAdvances[num - 1];
					num--;
					trailing++;
				}
				return num == 0;
			}
			return false;
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x0013A590 File Offset: 0x00139990
		private static bool IsSpace(char ch)
		{
			if (TextStore.IsSpace(ch))
			{
				return true;
			}
			int unicodeClassUTF = (int)Classification.GetUnicodeClassUTF16(ch);
			return Classification.CharAttributeOf(unicodeClassUTF).BiDi == DirectionClass.WhiteSpace;
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0013A5C0 File Offset: 0x001399C0
		internal bool IsUnderlineCompatible(SimpleRun nextRun)
		{
			return this.Typeface.Equals(nextRun.Typeface) && this.EmSize == nextRun.EmSize && this.Baseline == nextRun.Baseline;
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0013A600 File Offset: 0x00139A00
		internal int DistanceFromDcp(int dcp)
		{
			if (!this.Ghost && !this.Tab)
			{
				if (dcp > this.Length)
				{
					dcp = this.Length;
				}
				int num = 0;
				for (int i = 0; i < dcp; i++)
				{
					num += this.NominalAdvances[i];
				}
				return num;
			}
			if (dcp > 0)
			{
				return this.IdealWidth;
			}
			return 0;
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x0013A658 File Offset: 0x00139A58
		internal CharacterHit DcpFromDistance(int idealDistance)
		{
			if (this.Ghost)
			{
				if (!this.EOT && idealDistance > 0)
				{
					return new CharacterHit(this.Length, 0);
				}
				return default(CharacterHit);
			}
			else
			{
				if (this.Length <= 0)
				{
					return default(CharacterHit);
				}
				int num = 0;
				int num2 = 0;
				while (num < this.Length && idealDistance >= (this.Tab ? (num2 = this.IdealWidth / this.Length) : (num2 = this.NominalAdvances[num])))
				{
					idealDistance -= num2;
					num++;
				}
				if (num < this.Length)
				{
					return new CharacterHit(num, (idealDistance > num2 / 2) ? 1 : 0);
				}
				return new CharacterHit(this.Length - 1, 1);
			}
		}

		// Token: 0x040023D5 RID: 9173
		public CharacterBufferReference CharBufferReference;

		// Token: 0x040023D6 RID: 9174
		public int Length;

		// Token: 0x040023D7 RID: 9175
		public int[] NominalAdvances;

		// Token: 0x040023D8 RID: 9176
		public int IdealWidth;

		// Token: 0x040023D9 RID: 9177
		public TextRun TextRun;

		// Token: 0x040023DA RID: 9178
		public TextDecoration Underline;

		// Token: 0x040023DB RID: 9179
		public SimpleRun.Flags RunFlags;

		// Token: 0x040023DC RID: 9180
		private TextFormatterImp _textFormatterImp;

		// Token: 0x040023DD RID: 9181
		private double _pixelsPerDip;

		// Token: 0x020009E0 RID: 2528
		[Flags]
		internal enum Flags : ushort
		{
			// Token: 0x04002EB1 RID: 11953
			None = 0,
			// Token: 0x04002EB2 RID: 11954
			EOT = 1,
			// Token: 0x04002EB3 RID: 11955
			Ghost = 2,
			// Token: 0x04002EB4 RID: 11956
			TrimTrailingUnderline = 4,
			// Token: 0x04002EB5 RID: 11957
			Tab = 8
		}
	}
}
