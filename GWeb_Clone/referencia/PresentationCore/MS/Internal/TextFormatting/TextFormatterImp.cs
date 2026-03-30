using System;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;
using MS.Internal.Shaping;
using MS.Internal.Text.TextInterface;
using MS.Utility;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200075A RID: 1882
	internal sealed class TextFormatterImp : TextFormatter
	{
		// Token: 0x06004F09 RID: 20233 RVA: 0x0013A70C File Offset: 0x00139B0C
		internal TextFormatterImp(TextFormattingMode textFormattingMode) : this(null, textFormattingMode)
		{
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x0013A724 File Offset: 0x00139B24
		internal TextFormatterImp() : this(null, TextFormattingMode.Ideal)
		{
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x0013A73C File Offset: 0x00139B3C
		internal TextFormatterImp(TextFormatterContext soleContext, TextFormattingMode textFormattingMode)
		{
			this._textFormattingMode = textFormattingMode;
			if (soleContext != null)
			{
				this._contextList.Add(soleContext);
			}
			this._multipleContextProhibited = (this._contextList.Count != 0);
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0013A77C File Offset: 0x00139B7C
		~TextFormatterImp()
		{
			this.CleanupInternal();
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x0013A7B4 File Offset: 0x00139BB4
		public override void Dispose()
		{
			this.CleanupInternal();
			base.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x0013A7D4 File Offset: 0x00139BD4
		private void CleanupInternal()
		{
			for (int i = 0; i < this._contextList.Count; i++)
			{
				this._contextList[i].Destroy();
			}
			this._contextList.Clear();
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x0013A814 File Offset: 0x00139C14
		public override TextLine FormatLine(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak)
		{
			return this.FormatLineInternal(textSource, firstCharIndex, 0, paragraphWidth, paragraphProperties, previousLineBreak, new TextRunCache());
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x0013A834 File Offset: 0x00139C34
		public override TextLine FormatLine(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache)
		{
			return this.FormatLineInternal(textSource, firstCharIndex, 0, paragraphWidth, paragraphProperties, previousLineBreak, textRunCache);
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x0013A854 File Offset: 0x00139C54
		internal override TextLine RecreateLine(TextSource textSource, int firstCharIndex, int lineLength, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache)
		{
			return this.FormatLineInternal(textSource, firstCharIndex, lineLength, paragraphWidth, paragraphProperties, previousLineBreak, textRunCache);
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x0013A874 File Offset: 0x00139C74
		private TextLine FormatLineInternal(TextSource textSource, int firstCharIndex, int lineLength, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache)
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordText, EventTrace.Level.Verbose, EventTrace.Event.WClientStringBegin, "TextFormatterImp.FormatLineInternal Start");
			FormatSettings formatSettings = this.PrepareFormatSettings(textSource, firstCharIndex, paragraphWidth, paragraphProperties, previousLineBreak, textRunCache, lineLength != 0, true, this._textFormattingMode);
			TextLine textLine = null;
			if (!formatSettings.Pap.AlwaysCollapsible && previousLineBreak == null && lineLength <= 0)
			{
				textLine = SimpleTextLine.Create(formatSettings, firstCharIndex, TextFormatterImp.RealToIdealFloor(paragraphWidth), textSource.PixelsPerDip);
			}
			if (textLine == null)
			{
				textLine = new TextMetrics.FullTextLine(formatSettings, firstCharIndex, lineLength, TextFormatterImp.RealToIdealFloor(paragraphWidth), LineFlags.None);
			}
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordText, EventTrace.Level.Verbose, EventTrace.Event.WClientStringEnd, "TextFormatterImp.FormatLineInternal End");
			return textLine;
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x0013A8F8 File Offset: 0x00139CF8
		public override MinMaxParagraphWidth FormatMinMaxParagraphWidth(TextSource textSource, int firstCharIndex, TextParagraphProperties paragraphProperties)
		{
			return this.FormatMinMaxParagraphWidth(textSource, firstCharIndex, paragraphProperties, new TextRunCache());
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x0013A914 File Offset: 0x00139D14
		public override MinMaxParagraphWidth FormatMinMaxParagraphWidth(TextSource textSource, int firstCharIndex, TextParagraphProperties paragraphProperties, TextRunCache textRunCache)
		{
			FormatSettings settings = this.PrepareFormatSettings(textSource, firstCharIndex, 0.0, paragraphProperties, null, textRunCache, false, true, this._textFormattingMode);
			TextMetrics.FullTextLine fullTextLine = new TextMetrics.FullTextLine(settings, firstCharIndex, 0, 0, LineFlags.MinMax | LineFlags.KeepState);
			MinMaxParagraphWidth result = new MinMaxParagraphWidth(fullTextLine.MinWidth, fullTextLine.Width);
			fullTextLine.Dispose();
			return result;
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x0013A968 File Offset: 0x00139D68
		internal TextFormattingMode TextFormattingMode
		{
			get
			{
				return this._textFormattingMode;
			}
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x0013A97C File Offset: 0x00139D7C
		internal override TextParagraphCache CreateParagraphCache(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache)
		{
			FormatSettings formatSettings = this.PrepareFormatSettings(textSource, firstCharIndex, paragraphWidth, paragraphProperties, previousLineBreak, textRunCache, true, false, this._textFormattingMode);
			if (!formatSettings.Pap.Wrap && formatSettings.Pap.OptimalBreak)
			{
				throw new ArgumentException(SR.Get("OptimalParagraphMustWrap"));
			}
			return new TextParagraphCache(formatSettings, firstCharIndex, TextFormatterImp.RealToIdeal(paragraphWidth));
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x0013A9D8 File Offset: 0x00139DD8
		private FormatSettings PrepareFormatSettings(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextLineBreak previousLineBreak, TextRunCache textRunCache, bool useOptimalBreak, bool isSingleLineFormatting, TextFormattingMode textFormattingMode)
		{
			this.VerifyTextFormattingArguments(textSource, firstCharIndex, paragraphWidth, paragraphProperties, textRunCache);
			if (textRunCache.Imp == null)
			{
				textRunCache.Imp = new TextRunCacheImp();
			}
			return new FormatSettings(this, textSource, textRunCache.Imp, new ParaProp(this, paragraphProperties, useOptimalBreak), previousLineBreak, isSingleLineFormatting, textFormattingMode, false);
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x0013AA28 File Offset: 0x00139E28
		private void VerifyTextFormattingArguments(TextSource textSource, int firstCharIndex, double paragraphWidth, TextParagraphProperties paragraphProperties, TextRunCache textRunCache)
		{
			if (textSource == null)
			{
				throw new ArgumentNullException("textSource");
			}
			if (textRunCache == null)
			{
				throw new ArgumentNullException("textRunCache");
			}
			if (paragraphProperties == null)
			{
				throw new ArgumentNullException("paragraphProperties");
			}
			if (paragraphProperties.DefaultTextRunProperties == null)
			{
				throw new ArgumentNullException("paragraphProperties.DefaultTextRunProperties");
			}
			if (paragraphProperties.DefaultTextRunProperties.Typeface == null)
			{
				throw new ArgumentNullException("paragraphProperties.DefaultTextRunProperties.Typeface");
			}
			if (DoubleUtil.IsNaN(paragraphWidth))
			{
				throw new ArgumentOutOfRangeException("paragraphWidth", SR.Get("ParameterValueCannotBeNaN"));
			}
			if (double.IsInfinity(paragraphWidth))
			{
				throw new ArgumentOutOfRangeException("paragraphWidth", SR.Get("ParameterValueCannotBeInfinity"));
			}
			if (paragraphWidth < 0.0 || paragraphWidth > 3579139.4066666667)
			{
				throw new ArgumentOutOfRangeException("paragraphWidth", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					3579139.4066666667
				}));
			}
			double num = 35791.394066666668;
			if (paragraphProperties.DefaultTextRunProperties.FontRenderingEmSize < 0.0 || paragraphProperties.DefaultTextRunProperties.FontRenderingEmSize > num)
			{
				throw new ArgumentOutOfRangeException("paragraphProperties.DefaultTextRunProperties.FontRenderingEmSize", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					num
				}));
			}
			if (paragraphProperties.Indent > 3579139.4066666667)
			{
				throw new ArgumentOutOfRangeException("paragraphProperties.Indent", SR.Get("ParameterCannotBeGreaterThan", new object[]
				{
					3579139.4066666667
				}));
			}
			if (paragraphProperties.LineHeight > 3579139.4066666667)
			{
				throw new ArgumentOutOfRangeException("paragraphProperties.LineHeight", SR.Get("ParameterCannotBeGreaterThan", new object[]
				{
					3579139.4066666667
				}));
			}
			if (paragraphProperties.DefaultIncrementalTab < 0.0 || paragraphProperties.DefaultIncrementalTab > 3579139.4066666667)
			{
				throw new ArgumentOutOfRangeException("paragraphProperties.DefaultIncrementalTab", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					3579139.4066666667
				}));
			}
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x0013AC48 File Offset: 0x0013A048
		internal static void VerifyCaretCharacterHit(CharacterHit characterHit, int cpFirst, int cchLength)
		{
			if (characterHit.FirstCharacterIndex < cpFirst || characterHit.FirstCharacterIndex > cpFirst + cchLength)
			{
				throw new ArgumentOutOfRangeException("cpFirst", SR.Get("ParameterMustBeBetween", new object[]
				{
					cpFirst,
					cpFirst + cchLength
				}));
			}
			if (characterHit.TrailingLength < 0)
			{
				throw new ArgumentOutOfRangeException("cchLength", SR.Get("ParameterCannotBeNegative"));
			}
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x0013ACBC File Offset: 0x0013A0BC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal TextFormatterContext AcquireContext(object owner, IntPtr ploc)
		{
			Invariant.Assert(owner != null);
			TextFormatterContext textFormatterContext = null;
			int count = this._contextList.Count;
			int i;
			for (i = 0; i < count; i++)
			{
				textFormatterContext = this._contextList[i];
				if (ploc == IntPtr.Zero)
				{
					if (textFormatterContext.Owner == null)
					{
						break;
					}
				}
				else if (ploc == textFormatterContext.Ploc.Value)
				{
					break;
				}
			}
			if (i == count)
			{
				if (count != 0 && this._multipleContextProhibited)
				{
					throw new InvalidOperationException(SR.Get("TextFormatterReentranceProhibited"));
				}
				textFormatterContext = new TextFormatterContext();
				this._contextList.Add(textFormatterContext);
			}
			textFormatterContext.Owner = owner;
			return textFormatterContext;
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x0013AD64 File Offset: 0x0013A164
		internal static MatrixTransform CreateAntiInversionTransform(InvertAxes inversion, double paragraphWidth, double lineHeight)
		{
			if (inversion == InvertAxes.None)
			{
				return null;
			}
			double num = 1.0;
			double num2 = 1.0;
			double offsetX = 0.0;
			double offsetY = 0.0;
			if ((inversion & InvertAxes.Horizontal) != InvertAxes.None)
			{
				num = -num;
				offsetX = paragraphWidth;
			}
			if ((inversion & InvertAxes.Vertical) != InvertAxes.None)
			{
				num2 = -num2;
				offsetY = lineHeight;
			}
			return new MatrixTransform(num, 0.0, 0.0, num2, offsetX, offsetY);
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x0013ADD0 File Offset: 0x0013A1D0
		internal static int CompareReal(double x, double y, double pixelsPerDip, TextFormattingMode mode)
		{
			double num = x;
			double num2 = y;
			if (mode == TextFormattingMode.Display)
			{
				num = TextFormatterImp.RoundDipForDisplayMode(x, pixelsPerDip);
				num2 = TextFormatterImp.RoundDipForDisplayMode(y, pixelsPerDip);
			}
			if (num > num2)
			{
				return 1;
			}
			if (num < num2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x0013AE04 File Offset: 0x0013A204
		internal static double RoundDip(double value, double pixelsPerDip, TextFormattingMode textFormattingMode)
		{
			if (TextFormattingMode.Display == textFormattingMode)
			{
				return TextFormatterImp.RoundDipForDisplayMode(value, pixelsPerDip);
			}
			return value;
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x0013AE20 File Offset: 0x0013A220
		internal static double RoundDipForDisplayMode(double value, double pixelsPerDip)
		{
			return TextFormatterImp.RoundDipForDisplayMode(value, pixelsPerDip, MidpointRounding.ToEven);
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x0013AE38 File Offset: 0x0013A238
		private static double RoundDipForDisplayMode(double value, double pixelsPerDip, MidpointRounding midpointRounding)
		{
			return Math.Round(value * pixelsPerDip, midpointRounding) / pixelsPerDip;
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x0013AE50 File Offset: 0x0013A250
		internal static double RoundDipForDisplayModeJustifiedText(double value, double pixelsPerDip)
		{
			return TextFormatterImp.RoundDipForDisplayMode(value, pixelsPerDip, MidpointRounding.AwayFromZero);
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x0013AE68 File Offset: 0x0013A268
		internal static double IdealToRealWithNoRounding(double i)
		{
			return i * 0.0033333333333333335;
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x0013AE80 File Offset: 0x0013A280
		internal double IdealToReal(double i, double pixelsPerDip)
		{
			double num = TextFormatterImp.IdealToRealWithNoRounding(i);
			if (this._textFormattingMode == TextFormattingMode.Display)
			{
				num = TextFormatterImp.RoundDipForDisplayMode(num, pixelsPerDip);
			}
			if (i > 0.0)
			{
				num = Math.Max(num, 0.0033333333333333335);
			}
			return num;
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x0013AEC4 File Offset: 0x0013A2C4
		internal static int RealToIdeal(double i)
		{
			int num = (int)Math.Round(i * TextFormatterImp.ToIdeal);
			if (i > 0.0)
			{
				num = Math.Max(num, 1);
			}
			return num;
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x0013AEF4 File Offset: 0x0013A2F4
		internal static int RealToIdealFloor(double i)
		{
			int num = (int)Math.Floor(i * TextFormatterImp.ToIdeal);
			if (i > 0.0)
			{
				num = Math.Max(num, 1);
			}
			return num;
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x0013AF24 File Offset: 0x0013A324
		internal static double ToIdeal
		{
			get
			{
				return 300.0;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06004F26 RID: 20262 RVA: 0x0013AF3C File Offset: 0x0013A33C
		internal GlyphingCache GlyphingCache
		{
			get
			{
				if (this._glyphingCache == null)
				{
					this._glyphingCache = new GlyphingCache(16);
				}
				return this._glyphingCache;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06004F27 RID: 20263 RVA: 0x0013AF64 File Offset: 0x0013A364
		internal TextAnalyzer TextAnalyzer
		{
			[SecuritySafeCritical]
			get
			{
				if (this._textAnalyzer == null)
				{
					this._textAnalyzer = DWriteFactory.Instance.CreateTextAnalyzer();
				}
				return this._textAnalyzer;
			}
		}

		// Token: 0x040023DE RID: 9182
		private FrugalStructList<TextFormatterContext> _contextList;

		// Token: 0x040023DF RID: 9183
		private bool _multipleContextProhibited;

		// Token: 0x040023E0 RID: 9184
		private GlyphingCache _glyphingCache;

		// Token: 0x040023E1 RID: 9185
		private TextFormattingMode _textFormattingMode;

		// Token: 0x040023E2 RID: 9186
		private TextAnalyzer _textAnalyzer;

		// Token: 0x040023E3 RID: 9187
		private const int MaxGlyphingCacheCapacity = 16;
	}
}
