using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000705 RID: 1797
	internal struct TextMetrics : ITextMetrics
	{
		// Token: 0x06004D4C RID: 19788 RVA: 0x001323B8 File Offset: 0x001317B8
		[SecurityCritical]
		internal unsafe void Compute(FullTextState fullText, int firstCharIndex, int paragraphWidth, FormattedTextSymbols collapsingSymbol, ref LsLineWidths lineWidths, LsLInfo* plsLineInfo)
		{
			this._formatter = fullText.Formatter;
			TextStore textStore = fullText.TextStore;
			this._pixelsPerDip = textStore.Settings.TextSource.PixelsPerDip;
			this._textStart = lineWidths.upStartMainText;
			this._textWidthAtTrailing = lineWidths.upStartTrailing;
			this._textWidth = lineWidths.upLimLine;
			if (collapsingSymbol != null)
			{
				this.AppendCollapsingSymbolWidth(TextFormatterImp.RealToIdeal(collapsingSymbol.Width));
			}
			this._textWidth -= this._textStart;
			this._textWidthAtTrailing -= this._textStart;
			this._cchNewline = textStore.CchEol;
			this._lscpLim = plsLineInfo->cpLimToContinue;
			this._lastRun = fullText.CountText(this._lscpLim, firstCharIndex, out this._cchLength);
			if (plsLineInfo->endr != LsEndRes.endrEndPara && plsLineInfo->endr != LsEndRes.endrSoftCR)
			{
				this._cchNewline = 0;
				if (plsLineInfo->dcpDepend >= 0)
				{
					int lscpLim = Math.Max(plsLineInfo->cpLimToContinue + plsLineInfo->dcpDepend, fullText.LscpHyphenationLookAhead);
					fullText.CountText(lscpLim, firstCharIndex, out this._cchDepend);
					this._cchDepend -= this._cchLength;
				}
			}
			ParaProp pap = textStore.Pap;
			if (this._height <= 0)
			{
				if (pap.LineHeight > 0)
				{
					this._height = pap.LineHeight;
					this._baselineOffset = (int)Math.Round((double)this._height * pap.DefaultTypeface.Baseline((double)pap.EmSize, 0.0033333333333333335, this._pixelsPerDip, fullText.TextFormattingMode) / pap.DefaultTypeface.LineSpacing((double)pap.EmSize, 0.0033333333333333335, this._pixelsPerDip, fullText.TextFormattingMode));
				}
				if (plsLineInfo->dvrMultiLineHeight == 2147483647)
				{
					this._textAscent = (int)Math.Round(pap.DefaultTypeface.Baseline((double)pap.EmSize, 0.0033333333333333335, this._pixelsPerDip, fullText.TextFormattingMode));
					this._textHeight = (int)Math.Round(pap.DefaultTypeface.LineSpacing((double)pap.EmSize, 0.0033333333333333335, this._pixelsPerDip, fullText.TextFormattingMode));
				}
				else
				{
					this._textAscent = plsLineInfo->dvrAscent;
					this._textHeight = this._textAscent + plsLineInfo->dvrDescent;
					if (fullText.VerticalAdjust)
					{
						textStore.AdjustRunsVerticalOffset(plsLineInfo->cpLimToContinue - firstCharIndex, this._height, this._baselineOffset, out this._textHeight, out this._textAscent);
					}
				}
				if (this._height <= 0)
				{
					this._height = this._textHeight;
					this._baselineOffset = this._textAscent;
				}
			}
			TextAlignment align = pap.Align;
			if (align == TextAlignment.Right)
			{
				this._paragraphToText = paragraphWidth - this._textWidthAtTrailing;
				return;
			}
			if (align != TextAlignment.Center)
			{
				this._paragraphToText = pap.ParagraphIndent + this._textStart;
				return;
			}
			this._paragraphToText = (int)Math.Round((double)(paragraphWidth + this._textStart - this._textWidthAtTrailing) * 0.5);
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x001326B4 File Offset: 0x00131AB4
		[SecurityCritical]
		internal TextLineBreak GetTextLineBreak(IntPtr ploline)
		{
			IntPtr zero = IntPtr.Zero;
			if (ploline != IntPtr.Zero)
			{
				LsErr lsErr = UnsafeNativeMethods.LoAcquireBreakRecord(ploline, out zero);
				if (lsErr != LsErr.None)
				{
					TextFormatterContext.ThrowExceptionFromLsError(SR.Get("AcquireBreakRecordFailure", new object[]
					{
						lsErr
					}), lsErr);
				}
			}
			if (this._lastRun != null && this._lastRun.TextModifierScope != null && !(this._lastRun.TextRun is TextEndOfParagraph))
			{
				return new TextLineBreak(this._lastRun.TextModifierScope, new SecurityCriticalDataForSet<IntPtr>(zero));
			}
			if (!(zero != IntPtr.Zero))
			{
				return null;
			}
			return new TextLineBreak(null, new SecurityCriticalDataForSet<IntPtr>(zero));
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x00132758 File Offset: 0x00131B58
		private void AppendCollapsingSymbolWidth(int symbolIdealWidth)
		{
			this._textWidth += symbolIdealWidth;
			this._textWidthAtTrailing += symbolIdealWidth;
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06004D4F RID: 19791 RVA: 0x00132784 File Offset: 0x00131B84
		public int Length
		{
			get
			{
				return this._cchLength;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06004D50 RID: 19792 RVA: 0x00132798 File Offset: 0x00131B98
		public int DependentLength
		{
			get
			{
				return this._cchDepend;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06004D51 RID: 19793 RVA: 0x001327AC File Offset: 0x00131BAC
		public int NewlineLength
		{
			get
			{
				return this._cchNewline;
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06004D52 RID: 19794 RVA: 0x001327C0 File Offset: 0x00131BC0
		public double Start
		{
			get
			{
				return this._formatter.IdealToReal((double)(this._paragraphToText - this._textStart), this._pixelsPerDip);
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x001327EC File Offset: 0x00131BEC
		public double Width
		{
			get
			{
				return this._formatter.IdealToReal((double)(this._textWidthAtTrailing + this._textStart), this._pixelsPerDip);
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x00132818 File Offset: 0x00131C18
		public double WidthIncludingTrailingWhitespace
		{
			get
			{
				return this._formatter.IdealToReal((double)(this._textWidth + this._textStart), this._pixelsPerDip);
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06004D55 RID: 19797 RVA: 0x00132844 File Offset: 0x00131C44
		public double Height
		{
			get
			{
				return this._formatter.IdealToReal((double)this._height, this._pixelsPerDip);
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x0013286C File Offset: 0x00131C6C
		public double TextHeight
		{
			get
			{
				return this._formatter.IdealToReal((double)this._textHeight, this._pixelsPerDip);
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06004D57 RID: 19799 RVA: 0x00132894 File Offset: 0x00131C94
		public double Baseline
		{
			get
			{
				return this._formatter.IdealToReal((double)this._baselineOffset, this._pixelsPerDip);
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06004D58 RID: 19800 RVA: 0x001328BC File Offset: 0x00131CBC
		public double TextBaseline
		{
			get
			{
				return this._formatter.IdealToReal((double)this._textAscent, this._pixelsPerDip);
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06004D59 RID: 19801 RVA: 0x001328E4 File Offset: 0x00131CE4
		public double MarkerBaseline
		{
			get
			{
				return this.Baseline;
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x001328F8 File Offset: 0x00131CF8
		public double MarkerHeight
		{
			get
			{
				return this.Height;
			}
		}

		// Token: 0x0400219B RID: 8603
		private TextFormatterImp _formatter;

		// Token: 0x0400219C RID: 8604
		private int _lscpLim;

		// Token: 0x0400219D RID: 8605
		private int _cchLength;

		// Token: 0x0400219E RID: 8606
		private int _cchDepend;

		// Token: 0x0400219F RID: 8607
		private int _cchNewline;

		// Token: 0x040021A0 RID: 8608
		private int _height;

		// Token: 0x040021A1 RID: 8609
		private int _textHeight;

		// Token: 0x040021A2 RID: 8610
		private int _baselineOffset;

		// Token: 0x040021A3 RID: 8611
		private int _textAscent;

		// Token: 0x040021A4 RID: 8612
		private int _textStart;

		// Token: 0x040021A5 RID: 8613
		private int _textWidth;

		// Token: 0x040021A6 RID: 8614
		private int _textWidthAtTrailing;

		// Token: 0x040021A7 RID: 8615
		private int _paragraphToText;

		// Token: 0x040021A8 RID: 8616
		private LSRun _lastRun;

		// Token: 0x040021A9 RID: 8617
		private double _pixelsPerDip;

		// Token: 0x020009DA RID: 2522
		internal class FullTextLine : TextLine
		{
			// Token: 0x06005B2F RID: 23343 RVA: 0x0016D7BC File Offset: 0x0016CBBC
			internal FullTextLine(FormatSettings settings, int cpFirst, int lineLength, int paragraphWidth, LineFlags lineFlags) : this(settings.TextFormattingMode, settings.Pap.Justify, settings.TextSource.PixelsPerDip)
			{
				if ((lineFlags & LineFlags.KeepState) != LineFlags.None || settings.Pap.AlwaysCollapsible)
				{
					this._statusFlags |= TextMetrics.FullTextLine.StatusFlags.KeepState;
				}
				int finiteFormatWidth = settings.GetFiniteFormatWidth(paragraphWidth);
				FullTextState fullTextState = FullTextState.Create(settings, cpFirst, finiteFormatWidth);
				this.FormatLine(fullTextState, cpFirst, lineLength, fullTextState.FormatWidth, finiteFormatWidth, paragraphWidth, lineFlags, null);
			}

			// Token: 0x06005B30 RID: 23344 RVA: 0x0016D838 File Offset: 0x0016CC38
			~FullTextLine()
			{
				this.DisposeInternal(true);
			}

			// Token: 0x06005B31 RID: 23345 RVA: 0x0016D874 File Offset: 0x0016CC74
			public override void Dispose()
			{
				this.DisposeInternal(false);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06005B32 RID: 23346 RVA: 0x0016D890 File Offset: 0x0016CC90
			[SecurityTreatAsSafe]
			[SecurityCritical]
			private void DisposeInternal(bool finalizing)
			{
				if (this._ploline.Value != IntPtr.Zero)
				{
					UnsafeNativeMethods.LoDisposeLine(this._ploline.Value, finalizing);
					this._ploline.Value = IntPtr.Zero;
					GC.KeepAlive(this);
				}
			}

			// Token: 0x06005B33 RID: 23347 RVA: 0x0016D8DC File Offset: 0x0016CCDC
			[SecurityTreatAsSafe]
			[SecurityCritical]
			private FullTextLine(TextFormattingMode textFormattingMode, bool justify, double pixelsPerDip) : base(pixelsPerDip)
			{
				this._textFormattingMode = textFormattingMode;
				if (justify)
				{
					this._statusFlags |= TextMetrics.FullTextLine.StatusFlags.IsJustified;
				}
				this._metrics = default(TextMetrics);
				this._metrics._pixelsPerDip = pixelsPerDip;
				this._ploline = new SecurityCriticalDataForSet<IntPtr>(IntPtr.Zero);
			}

			// Token: 0x06005B34 RID: 23348 RVA: 0x0016D934 File Offset: 0x0016CD34
			[SecurityCritical]
			[SecurityTreatAsSafe]
			private unsafe void FormatLine(FullTextState fullText, int cpFirst, int lineLength, int formatWidth, int finiteFormatWidth, int paragraphWidth, LineFlags lineFlags, FormattedTextSymbols collapsingSymbol)
			{
				this._metrics._formatter = fullText.Formatter;
				TextStore textStore = fullText.TextStore;
				TextStore textMarkerStore = fullText.TextMarkerStore;
				FormatSettings settings = textStore.Settings;
				ParaProp pap = settings.Pap;
				this._paragraphTextDecorations = pap.TextDecorations;
				if (this._paragraphTextDecorations != null)
				{
					if (this._paragraphTextDecorations.Count != 0)
					{
						this._defaultTextDecorationsBrush = pap.DefaultTextDecorationsBrush;
					}
					else
					{
						this._paragraphTextDecorations = null;
					}
				}
				TextFormatterContext textFormatterContext = this._metrics._formatter.AcquireContext(fullText, IntPtr.Zero);
				LsLInfo lsLInfo = default(LsLInfo);
				LsLineWidths lsLineWidths = default(LsLineWidths);
				fullText.SetTabs(textFormatterContext);
				int lineLength2 = 0;
				if (lineLength > 0)
				{
					lineLength2 = this.PrefetchLSRuns(textStore, cpFirst, lineLength);
				}
				IntPtr value;
				LsErr lsErr = textFormatterContext.CreateLine(cpFirst, lineLength2, formatWidth, lineFlags, IntPtr.Zero, out value, out lsLInfo, out this._depthQueryMax, out lsLineWidths);
				if (lsErr == LsErr.TooLongParagraph)
				{
					int num = fullText.CpMeasured;
					int num2 = 1;
					for (;;)
					{
						if (num < 1)
						{
							num = 1;
						}
						textStore.InsertFakeLineBreak(num);
						lsErr = textFormatterContext.CreateLine(cpFirst, lineLength2, formatWidth, lineFlags, IntPtr.Zero, out value, out lsLInfo, out this._depthQueryMax, out lsLineWidths);
						if (lsErr != LsErr.TooLongParagraph || num == 1)
						{
							break;
						}
						num = fullText.CpMeasured - num2;
						num2 *= 2;
					}
				}
				this._ploline.Value = value;
				Exception callbackException = textFormatterContext.CallbackException;
				textFormatterContext.Release();
				if (lsErr != LsErr.None)
				{
					GC.SuppressFinalize(this);
					if (callbackException != null)
					{
						throw TextMetrics.FullTextLine.WrapException(callbackException);
					}
					TextFormatterContext.ThrowExceptionFromLsError(SR.Get("CreateLineFailure", new object[]
					{
						lsErr
					}), lsErr);
				}
				GC.KeepAlive(textFormatterContext);
				this._metrics.Compute(fullText, cpFirst, paragraphWidth, collapsingSymbol, ref lsLineWidths, &lsLInfo);
				this._textMinWidthAtTrailing = lsLineWidths.upMinStartTrailing - this._metrics._textStart;
				if (collapsingSymbol != null)
				{
					this._collapsingSymbol = collapsingSymbol;
					this._textMinWidthAtTrailing += TextFormatterImp.RealToIdeal(collapsingSymbol.Width);
				}
				else if (this._metrics._textStart + this._metrics._textWidthAtTrailing > finiteFormatWidth)
				{
					bool flag = true;
					if (this._textFormattingMode == TextFormattingMode.Display)
					{
						double width = this.Width;
						double y = this._metrics._formatter.IdealToReal((double)finiteFormatWidth, base.PixelsPerDip);
						flag = (TextFormatterImp.CompareReal(width, y, base.PixelsPerDip, this._textFormattingMode) > 0);
					}
					if (flag)
					{
						this._statusFlags |= TextMetrics.FullTextLine.StatusFlags.HasOverflowed;
						this._fullText = fullText;
					}
				}
				if (fullText != null && (fullText.KeepState || (this._statusFlags & TextMetrics.FullTextLine.StatusFlags.KeepState) != TextMetrics.FullTextLine.StatusFlags.None))
				{
					this._fullText = fullText;
				}
				this._ploc = textFormatterContext.Ploc;
				this._cpFirst = cpFirst;
				this._paragraphWidth = paragraphWidth;
				if (pap.RightToLeft)
				{
					this._statusFlags |= TextMetrics.FullTextLine.StatusFlags.RightToLeft;
				}
				if (lsLInfo.fForcedBreak != 0)
				{
					this._statusFlags |= TextMetrics.FullTextLine.StatusFlags.IsTruncated;
				}
				this._plsrunVector = textStore.PlsrunVector;
				this._lsrunsMainText = textStore.LsrunList;
				if (textMarkerStore != null)
				{
					this._lsrunsMarkerText = textMarkerStore.LsrunList;
				}
				this._textSource = settings.TextSource;
			}

			// Token: 0x06005B35 RID: 23349 RVA: 0x0016DC28 File Offset: 0x0016D028
			private static Exception WrapException(Exception caughtException)
			{
				Type type = caughtException.GetType();
				if (type.IsPublic)
				{
					ConstructorInfo constructor = type.GetConstructor(new Type[]
					{
						typeof(Exception)
					});
					if (constructor != null)
					{
						return (Exception)constructor.Invoke(new object[]
						{
							caughtException
						});
					}
					constructor = type.GetConstructor(new Type[]
					{
						typeof(string),
						typeof(Exception)
					});
					if (constructor != null)
					{
						return (Exception)constructor.Invoke(new object[]
						{
							caughtException.Message,
							caughtException
						});
					}
				}
				return caughtException;
			}

			// Token: 0x06005B36 RID: 23350 RVA: 0x0016DCD0 File Offset: 0x0016D0D0
			private void AppendCollapsingSymbol(FormattedTextSymbols symbol)
			{
				this._collapsingSymbol = symbol;
				int num = TextFormatterImp.RealToIdeal(symbol.Width);
				this._metrics.AppendCollapsingSymbolWidth(num);
				this._textMinWidthAtTrailing += num;
			}

			// Token: 0x06005B37 RID: 23351 RVA: 0x0016DD0C File Offset: 0x0016D10C
			private int PrefetchLSRuns(TextStore store, int cpFirst, int lineLength)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				LSRun lsrun;
				do
				{
					Plsrun plsrun;
					int num5;
					int num6;
					lsrun = store.FetchLSRun(cpFirst + num2, this._textFormattingMode, false, out plsrun, out num5, out num6);
					if (lineLength == num && lsrun.Type == Plsrun.Reverse)
					{
						break;
					}
					num3 = num6;
					num4 = lsrun.Length;
					num2 += num3;
					num += num4;
				}
				while (!TextStore.IsNewline(lsrun.Type) && lineLength >= num);
				if (num == lineLength || num3 == num4)
				{
					return num2 - num + lineLength;
				}
				Invariant.Assert(num - lineLength == num4);
				return num2 - num3;
			}

			// Token: 0x06005B38 RID: 23352 RVA: 0x0016DD88 File Offset: 0x0016D188
			public override void Draw(DrawingContext drawingContext, Point origin, InvertAxes inversion)
			{
				if (drawingContext == null)
				{
					throw new ArgumentNullException("drawingContext");
				}
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				MatrixTransform matrixTransform = TextFormatterImp.CreateAntiInversionTransform(inversion, this._metrics._formatter.IdealToReal((double)this._paragraphWidth, base.PixelsPerDip), this._metrics._formatter.IdealToReal((double)this._metrics._height, base.PixelsPerDip));
				if (matrixTransform == null)
				{
					this.DrawTextLine(drawingContext, origin, null);
					return;
				}
				drawingContext.PushTransform(matrixTransform);
				try
				{
					this.DrawTextLine(drawingContext, origin, matrixTransform);
				}
				finally
				{
					drawingContext.Pop();
				}
			}

			// Token: 0x06005B39 RID: 23353 RVA: 0x0016DE48 File Offset: 0x0016D248
			[SecurityCritical]
			[SecurityTreatAsSafe]
			private void DrawTextLine(DrawingContext drawingContext, Point origin, MatrixTransform antiInversion)
			{
				Rect boundingBox = Rect.Empty;
				if (this._ploline.Value != IntPtr.Zero)
				{
					LsErr lsErr = LsErr.None;
					LSRECT lsrect = new LSRECT(0, 0, this._metrics._textWidthAtTrailing, this._metrics._height);
					TextFormatterContext textFormatterContext;
					using (DrawingState drawingState = new DrawingState(drawingContext, origin, antiInversion, this))
					{
						textFormatterContext = this._metrics._formatter.AcquireContext(drawingState, this._ploc.Value);
						textFormatterContext.EmptyBoundingBox();
						LSPOINT lspoint = new LSPOINT(0, this._metrics._baselineOffset);
						lsErr = UnsafeNativeMethods.LoDisplayLine(this._ploline.Value, ref lspoint, 1U, ref lsrect);
					}
					boundingBox = textFormatterContext.BoundingBox;
					Exception callbackException = textFormatterContext.CallbackException;
					textFormatterContext.Release();
					if (lsErr != LsErr.None)
					{
						if (callbackException != null)
						{
							throw callbackException;
						}
						TextFormatterContext.ThrowExceptionFromLsError(SR.Get("CreateLineFailure", new object[]
						{
							lsErr
						}), lsErr);
					}
					GC.KeepAlive(textFormatterContext);
				}
				if (this._collapsingSymbol != null)
				{
					Point vectorToLineOrigin = default(Point);
					if (antiInversion != null)
					{
						vectorToLineOrigin = origin;
						origin.X = (origin.Y = 0.0);
					}
					boundingBox.Union(this.DrawCollapsingSymbol(drawingContext, origin, vectorToLineOrigin));
				}
				this.BuildOverhang(origin, boundingBox);
				this._statusFlags |= TextMetrics.FullTextLine.StatusFlags.BoundingBoxComputed;
			}

			// Token: 0x06005B3A RID: 23354 RVA: 0x0016DFB4 File Offset: 0x0016D3B4
			private Rect DrawCollapsingSymbol(DrawingContext drawingContext, Point lineOrigin, Point vectorToLineOrigin)
			{
				int num = TextFormatterImp.RealToIdeal(this._collapsingSymbol.Width);
				Point currentOrigin = LSRun.UVToXY(lineOrigin, vectorToLineOrigin, this.LSLineUToParagraphU(this._metrics._textStart + this._metrics._textWidthAtTrailing - num), this._metrics._baselineOffset, this);
				return this._collapsingSymbol.Draw(drawingContext, currentOrigin);
			}

			// Token: 0x06005B3B RID: 23355 RVA: 0x0016E014 File Offset: 0x0016D414
			private void CheckBoundingBox()
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.BoundingBoxComputed) == TextMetrics.FullTextLine.StatusFlags.None)
				{
					this.DrawTextLine(null, new Point(0.0, 0.0), null);
				}
			}

			// Token: 0x06005B3C RID: 23356 RVA: 0x0016E04C File Offset: 0x0016D44C
			public override TextLine Collapse(params TextCollapsingProperties[] collapsingPropertiesList)
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				if (!this.HasOverflowed && (this._statusFlags & TextMetrics.FullTextLine.StatusFlags.KeepState) == TextMetrics.FullTextLine.StatusFlags.None)
				{
					return this;
				}
				if (collapsingPropertiesList == null || collapsingPropertiesList.Length == 0)
				{
					throw new ArgumentNullException("collapsingPropertiesList");
				}
				TextCollapsingProperties textCollapsingProperties = collapsingPropertiesList[0];
				double num = textCollapsingProperties.Width;
				if (TextFormatterImp.CompareReal(num, this.Width, base.PixelsPerDip, this._textFormattingMode) > 0)
				{
					return this;
				}
				FormattedTextSymbols formattedTextSymbols = null;
				if (textCollapsingProperties.Symbol != null)
				{
					formattedTextSymbols = new FormattedTextSymbols(this._metrics._formatter.GlyphingCache, textCollapsingProperties.Symbol, this.RightToLeft, TextFormatterImp.ToIdeal, (float)base.PixelsPerDip, this._textFormattingMode, false);
					num -= formattedTextSymbols.Width;
				}
				TextMetrics.FullTextLine fullTextLine = new TextMetrics.FullTextLine(this._textFormattingMode, this.IsJustified, base.PixelsPerDip);
				fullTextLine._metrics._formatter = this._metrics._formatter;
				fullTextLine._metrics._height = this._metrics._height;
				fullTextLine._metrics._baselineOffset = this._metrics._baselineOffset;
				if (num > 0.0)
				{
					int finiteFormatWidth = this._fullText.TextStore.Settings.GetFiniteFormatWidth(TextFormatterImp.RealToIdeal(num));
					bool forceWrap = this._fullText.ForceWrap;
					this._fullText.ForceWrap = true;
					if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.KeepState) != TextMetrics.FullTextLine.StatusFlags.None)
					{
						fullTextLine._statusFlags |= TextMetrics.FullTextLine.StatusFlags.KeepState;
					}
					fullTextLine.FormatLine(this._fullText, this._cpFirst, 0, finiteFormatWidth, finiteFormatWidth, this._paragraphWidth, (textCollapsingProperties.Style == TextCollapsingStyle.TrailingCharacter) ? LineFlags.BreakAlways : LineFlags.None, formattedTextSymbols);
					this._fullText.ForceWrap = forceWrap;
					fullTextLine._metrics._cchDepend = 0;
				}
				else if (formattedTextSymbols != null)
				{
					fullTextLine.AppendCollapsingSymbol(formattedTextSymbols);
				}
				if (fullTextLine._metrics._cchLength < this.Length)
				{
					fullTextLine._collapsedRange = new TextCollapsedRange(this._cpFirst + fullTextLine._metrics._cchLength, this.Length - fullTextLine._metrics._cchLength, this.Width - fullTextLine.Width);
					fullTextLine._metrics._cchLength = this.Length;
				}
				fullTextLine._statusFlags |= TextMetrics.FullTextLine.StatusFlags.HasCollapsed;
				fullTextLine._statusFlags &= ~TextMetrics.FullTextLine.StatusFlags.HasOverflowed;
				return fullTextLine;
			}

			// Token: 0x06005B3D RID: 23357 RVA: 0x0016E294 File Offset: 0x0016D694
			public override IList<TextCollapsedRange> GetTextCollapsedRanges()
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				if (this._collapsedRange == null)
				{
					return null;
				}
				return new TextCollapsedRange[]
				{
					this._collapsedRange
				};
			}

			// Token: 0x06005B3E RID: 23358 RVA: 0x0016E2D4 File Offset: 0x0016D6D4
			public override CharacterHit GetCharacterHitFromDistance(double distance)
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				return this.CharacterHitFromDistance(this.ParagraphUToLSLineU(TextFormatterImp.RealToIdeal(distance)));
			}

			// Token: 0x06005B3F RID: 23359 RVA: 0x0016E310 File Offset: 0x0016D710
			private CharacterHit CharacterHitFromDistance(int hitTestDistance)
			{
				CharacterHit result = new CharacterHit(this._cpFirst, 0);
				if (this._ploline.Value == IntPtr.Zero)
				{
					return result;
				}
				if (this.HasCollapsed && this._collapsedRange != null && this._collapsingSymbol != null)
				{
					int num = this._metrics._textStart + this._metrics._textWidthAtTrailing;
					int num2 = TextFormatterImp.RealToIdeal(this._collapsingSymbol.Width);
					if (hitTestDistance >= num - num2)
					{
						if (num - hitTestDistance < num2 / 2)
						{
							return new CharacterHit(this._collapsedRange.TextSourceCharacterIndex, this._collapsedRange.Length);
						}
						return new CharacterHit(this._collapsedRange.TextSourceCharacterIndex, 0);
					}
				}
				LsQSubInfo[] array = new LsQSubInfo[this._depthQueryMax];
				int num3;
				LsTextCell lsTextCell;
				this.QueryLinePointPcp(new Point((double)hitTestDistance, 0.0), array, out num3, out lsTextCell);
				if (num3 > 0 && lsTextCell.dupCell > 0)
				{
					LSRun run = this.GetRun((Plsrun)((int)array[num3 - 1].plsrun));
					int num4 = lsTextCell.lscpEndCell + 1 - lsTextCell.lscpStartCell;
					int trailingLength = run.IsHitTestable ? 1 : run.Length;
					if (run.IsHitTestable && (run.HasExtendedCharacter || run.NeedsCaretInfo))
					{
						trailingLength = num4;
						num4 = 1;
					}
					int num5 = (array[num3 - 1].lstflowSubLine == array[0].lstflowSubLine) ? 1 : -1;
					hitTestDistance = (hitTestDistance - lsTextCell.pointUvStartCell.x) * num5;
					Invariant.Assert(num4 > 0);
					int num6 = lsTextCell.dupCell / num4;
					int num7 = lsTextCell.dupCell % num4;
					int i = 0;
					while (i < num4)
					{
						int num8 = num6;
						if (num7 > 0)
						{
							num8++;
							num7--;
						}
						if (hitTestDistance <= num8)
						{
							if (hitTestDistance > num8 / 2)
							{
								return new CharacterHit(this.GetExternalCp(lsTextCell.lscpStartCell) + i, trailingLength);
							}
							return new CharacterHit(this.GetExternalCp(lsTextCell.lscpStartCell) + i, 0);
						}
						else
						{
							hitTestDistance -= num8;
							i++;
						}
					}
					return new CharacterHit(this.GetExternalCp(lsTextCell.lscpStartCell) + num4 - 1, trailingLength);
				}
				return result;
			}

			// Token: 0x06005B40 RID: 23360 RVA: 0x0016E530 File Offset: 0x0016D930
			public override double GetDistanceFromCharacterHit(CharacterHit characterHit)
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				TextFormatterImp.VerifyCaretCharacterHit(characterHit, this._cpFirst, this._metrics._cchLength);
				return this._metrics._formatter.IdealToReal((double)this.LSLineUToParagraphU(this.DistanceFromCharacterHit(characterHit)), base.PixelsPerDip);
			}

			// Token: 0x06005B41 RID: 23361 RVA: 0x0016E594 File Offset: 0x0016D994
			private int DistanceFromCharacterHit(CharacterHit characterHit)
			{
				int result = 0;
				if (this._ploline.Value == IntPtr.Zero)
				{
					return result;
				}
				if (characterHit.FirstCharacterIndex >= this._cpFirst + this._metrics._cchLength)
				{
					return this._metrics._textStart + this._metrics._textWidthAtTrailing;
				}
				if (this.HasCollapsed && this._collapsedRange != null && characterHit.FirstCharacterIndex >= this._collapsedRange.TextSourceCharacterIndex)
				{
					int num = this._metrics._textStart + this._metrics._textWidthAtTrailing;
					if (characterHit.FirstCharacterIndex >= this._collapsedRange.TextSourceCharacterIndex + this._collapsedRange.Length || characterHit.TrailingLength != 0 || this._collapsingSymbol == null)
					{
						return num;
					}
					return num - TextFormatterImp.RealToIdeal(this._collapsingSymbol.Width);
				}
				else
				{
					LsQSubInfo[] array = new LsQSubInfo[this._depthQueryMax];
					int internalCp = this.GetInternalCp(characterHit.FirstCharacterIndex);
					int num2;
					LsTextCell lsTextCell;
					this.QueryLineCpPpoint(internalCp, array, out num2, out lsTextCell);
					if (num2 > 0)
					{
						return lsTextCell.pointUvStartCell.x + this.GetDistanceInsideTextCell(internalCp, characterHit.TrailingLength != 0, array, num2, ref lsTextCell);
					}
					return result;
				}
			}

			// Token: 0x06005B42 RID: 23362 RVA: 0x0016E6C8 File Offset: 0x0016DAC8
			private int GetDistanceInsideTextCell(int lscpCurrent, bool isTrailing, LsQSubInfo[] sublineInfo, int actualSublineCount, ref LsTextCell lsTextCell)
			{
				int num = 0;
				int num2 = (sublineInfo[actualSublineCount - 1].lstflowSubLine == sublineInfo[0].lstflowSubLine) ? 1 : -1;
				int num3 = lsTextCell.lscpEndCell + 1 - lsTextCell.lscpStartCell;
				int num4 = lscpCurrent - lsTextCell.lscpStartCell;
				LSRun run = this.GetRun((Plsrun)((int)sublineInfo[actualSublineCount - 1].plsrun));
				if (run.IsHitTestable && (run.HasExtendedCharacter || run.NeedsCaretInfo))
				{
					num3 = 1;
				}
				Invariant.Assert(num3 > 0);
				int num5 = lsTextCell.dupCell / num3;
				int num6 = lsTextCell.dupCell % num3;
				int i = 1;
				while (i <= num3)
				{
					int num7 = num5;
					if (num6 > 0)
					{
						num7++;
						num6--;
					}
					if (num4 < i)
					{
						if (isTrailing)
						{
							return (num + num7) * num2;
						}
						return num * num2;
					}
					else
					{
						num += num7;
						i++;
					}
				}
				return num * num2;
			}

			// Token: 0x06005B43 RID: 23363 RVA: 0x0016E7AC File Offset: 0x0016DBAC
			public override CharacterHit GetNextCaretCharacterHit(CharacterHit characterHit)
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				TextFormatterImp.VerifyCaretCharacterHit(characterHit, this._cpFirst, this._metrics._cchLength);
				if (this._ploline.Value == IntPtr.Zero)
				{
					return characterHit;
				}
				int num;
				int num2;
				if (!this.GetNextOrPreviousCaretStop(characterHit.FirstCharacterIndex, TextMetrics.FullTextLine.CaretDirection.Forward, out num, out num2))
				{
					return characterHit;
				}
				if (num > characterHit.FirstCharacterIndex || characterHit.TrailingLength == 0)
				{
					return new CharacterHit(num, num2);
				}
				if (!this.GetNextOrPreviousCaretStop(num + num2, TextMetrics.FullTextLine.CaretDirection.Forward, out num, out num2))
				{
					return characterHit;
				}
				return new CharacterHit(num, num2);
			}

			// Token: 0x06005B44 RID: 23364 RVA: 0x0016E854 File Offset: 0x0016DC54
			public override CharacterHit GetPreviousCaretCharacterHit(CharacterHit characterHit)
			{
				return this.GetPreviousCaretCharacterHitByBehavior(characterHit, TextMetrics.FullTextLine.CaretDirection.Backward);
			}

			// Token: 0x06005B45 RID: 23365 RVA: 0x0016E86C File Offset: 0x0016DC6C
			public override CharacterHit GetBackspaceCaretCharacterHit(CharacterHit characterHit)
			{
				return this.GetPreviousCaretCharacterHitByBehavior(characterHit, TextMetrics.FullTextLine.CaretDirection.Backspace);
			}

			// Token: 0x06005B46 RID: 23366 RVA: 0x0016E884 File Offset: 0x0016DC84
			private CharacterHit GetPreviousCaretCharacterHitByBehavior(CharacterHit characterHit, TextMetrics.FullTextLine.CaretDirection direction)
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				TextFormatterImp.VerifyCaretCharacterHit(characterHit, this._cpFirst, this._metrics._cchLength);
				if (this._ploline.Value == IntPtr.Zero)
				{
					return characterHit;
				}
				if (characterHit.FirstCharacterIndex == this._cpFirst && characterHit.TrailingLength == 0)
				{
					return characterHit;
				}
				int num;
				int num2;
				if (!this.GetNextOrPreviousCaretStop(characterHit.FirstCharacterIndex, direction, out num, out num2))
				{
					return characterHit;
				}
				if (num2 != 0 && characterHit.TrailingLength == 0 && num != this._cpFirst && num >= characterHit.FirstCharacterIndex && !this.GetNextOrPreviousCaretStop(num - 1, direction, out num, out num2))
				{
					return characterHit;
				}
				return new CharacterHit(num, 0);
			}

			// Token: 0x06005B47 RID: 23367 RVA: 0x0016E948 File Offset: 0x0016DD48
			private bool GetNextOrPreviousCaretStop(int currentIndex, TextMetrics.FullTextLine.CaretDirection direction, out int caretStopIndex, out int offsetToNextCaretStopIndex)
			{
				caretStopIndex = currentIndex;
				offsetToNextCaretStopIndex = 0;
				if (this.HasCollapsed && this._collapsedRange != null && currentIndex >= this._collapsedRange.TextSourceCharacterIndex)
				{
					caretStopIndex = this._collapsedRange.TextSourceCharacterIndex;
					if (currentIndex < this._collapsedRange.TextSourceCharacterIndex + this._collapsedRange.Length)
					{
						offsetToNextCaretStopIndex = this._collapsedRange.Length;
					}
					return true;
				}
				LsQSubInfo[] array = new LsQSubInfo[this._depthQueryMax];
				LsTextCell lsTextCell = default(LsTextCell);
				int internalCp = this.GetInternalCp(currentIndex);
				if (!this.FindNextOrPreviousVisibleCp(internalCp, direction, out internalCp))
				{
					return false;
				}
				int num;
				this.QueryLineCpPpoint(internalCp, array, out num, out lsTextCell);
				caretStopIndex = this.GetExternalCp(lsTextCell.lscpStartCell);
				if (num > 0 && internalCp >= lsTextCell.lscpStartCell && internalCp <= lsTextCell.lscpEndCell)
				{
					LSRun run = this.GetRun((Plsrun)((int)array[num - 1].plsrun));
					if (run.IsHitTestable)
					{
						if (run.HasExtendedCharacter || (direction != TextMetrics.FullTextLine.CaretDirection.Backspace && run.NeedsCaretInfo))
						{
							offsetToNextCaretStopIndex = lsTextCell.lscpEndCell + 1 - lsTextCell.lscpStartCell;
						}
						else
						{
							caretStopIndex = this.GetExternalCp(internalCp);
							offsetToNextCaretStopIndex = 1;
						}
					}
					else
					{
						offsetToNextCaretStopIndex = Math.Min(this.Length, run.Length - caretStopIndex + run.OffsetToFirstCp + this._cpFirst);
					}
				}
				return true;
			}

			// Token: 0x06005B48 RID: 23368 RVA: 0x0016EA9C File Offset: 0x0016DE9C
			private bool FindNextOrPreviousVisibleCp(int lscp, TextMetrics.FullTextLine.CaretDirection direction, out int lscpVisisble)
			{
				lscpVisisble = lscp;
				SpanRider spanRider = new SpanRider(this._plsrunVector);
				if (direction == TextMetrics.FullTextLine.CaretDirection.Forward)
				{
					while (lscpVisisble < this._metrics._lscpLim)
					{
						spanRider.At(lscpVisisble - this._cpFirst);
						LSRun run = this.GetRun((Plsrun)spanRider.CurrentElement);
						if (run.IsVisible)
						{
							return true;
						}
						lscpVisisble += spanRider.Length;
					}
				}
				else
				{
					for (lscpVisisble = Math.Min(lscpVisisble, this._metrics._lscpLim - 1); lscpVisisble >= this._cpFirst; lscpVisisble = this._cpFirst + spanRider.CurrentSpanStart - 1)
					{
						spanRider.At(lscpVisisble - this._cpFirst);
						LSRun run2 = this.GetRun((Plsrun)spanRider.CurrentElement);
						if (run2.IsVisible)
						{
							return true;
						}
						if (run2.IsNewline)
						{
							lscpVisisble = this._cpFirst + spanRider.CurrentSpanStart;
							return true;
						}
					}
				}
				lscpVisisble = lscp;
				return false;
			}

			// Token: 0x06005B49 RID: 23369 RVA: 0x0016EB8C File Offset: 0x0016DF8C
			private TextBounds[] CreateDegenerateBounds()
			{
				return new TextBounds[]
				{
					new TextBounds(new Rect(0.0, 0.0, 0.0, this.Height), this.RightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight, null)
				};
			}

			// Token: 0x06005B4A RID: 23370 RVA: 0x0016EBDC File Offset: 0x0016DFDC
			private TextBounds CreateCollapsingSymbolBounds()
			{
				return new TextBounds(new Rect(this.Start + this.Width - this._collapsingSymbol.Width, 0.0, this._collapsingSymbol.Width, this.Height), this.RightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight, null);
			}

			// Token: 0x06005B4B RID: 23371 RVA: 0x0016EC34 File Offset: 0x0016E034
			public override IList<TextBounds> GetTextBounds(int firstTextSourceCharacterIndex, int textLength)
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				if (textLength == 0)
				{
					throw new ArgumentOutOfRangeException("textLength", SR.Get("ParameterMustBeGreaterThanZero"));
				}
				if (textLength < 0)
				{
					firstTextSourceCharacterIndex += textLength;
					textLength = -textLength;
				}
				if (firstTextSourceCharacterIndex < this._cpFirst)
				{
					textLength += firstTextSourceCharacterIndex - this._cpFirst;
					firstTextSourceCharacterIndex = this._cpFirst;
				}
				if (firstTextSourceCharacterIndex > this._cpFirst + this._metrics._cchLength - textLength)
				{
					textLength = this._cpFirst + this._metrics._cchLength - firstTextSourceCharacterIndex;
				}
				if (this._ploline.Value == IntPtr.Zero)
				{
					return this.CreateDegenerateBounds();
				}
				Point origin = new Point(0.0, 0.0);
				LsQSubInfo[] array = new LsQSubInfo[this._depthQueryMax];
				int internalCp = this.GetInternalCp(firstTextSourceCharacterIndex);
				int num;
				LsTextCell lsTextCell;
				this.QueryLineCpPpoint(internalCp, array, out num, out lsTextCell);
				if (num <= 0)
				{
					return this.CreateDegenerateBounds();
				}
				LsQSubInfo[] array2 = new LsQSubInfo[this._depthQueryMax];
				int internalCp2 = this.GetInternalCp(firstTextSourceCharacterIndex + textLength - 1);
				int num2;
				LsTextCell lsTextCell2;
				this.QueryLineCpPpoint(internalCp2, array2, out num2, out lsTextCell2);
				if (num2 <= 0)
				{
					return this.CreateDegenerateBounds();
				}
				bool flag = this._collapsingSymbol != null && this._collapsedRange != null && firstTextSourceCharacterIndex < this._collapsedRange.TextSourceCharacterIndex && firstTextSourceCharacterIndex + textLength - this._collapsedRange.TextSourceCharacterIndex > this._collapsedRange.Length / 2;
				TextBounds[] array3 = null;
				ArrayList arrayList = null;
				bool isTrailing = false;
				bool isTrailing2 = true;
				if (internalCp > lsTextCell.lscpEndCell)
				{
					isTrailing = true;
				}
				if (internalCp2 < lsTextCell2.lscpStartCell)
				{
					isTrailing2 = false;
				}
				if (num == num2 && array[num - 1].lscpFirstSubLine == array2[num2 - 1].lscpFirstSubLine)
				{
					int num3 = flag ? 2 : 1;
					array3 = new TextBounds[num3];
					array3[0] = new TextBounds(LSRun.RectUV(origin, new LSPOINT(this.LSLineUToParagraphU(this.GetDistanceInsideTextCell(internalCp, isTrailing, array, num, ref lsTextCell) + lsTextCell.pointUvStartCell.x), 0), new LSPOINT(this.LSLineUToParagraphU(this.GetDistanceInsideTextCell(internalCp2, isTrailing2, array2, num2, ref lsTextCell2) + lsTextCell2.pointUvStartCell.x), this._metrics._height), this), Convert.LsTFlowToFlowDirection(array[num - 1].lstflowSubLine), this.CalculateTextRunBounds(internalCp, internalCp2 + 1));
					if (num3 > 1)
					{
						array3[1] = this.CreateCollapsingSymbolBounds();
					}
				}
				else
				{
					arrayList = new ArrayList(2);
					int lscpFirst = internalCp;
					int lscpEnd = Math.Min(internalCp2, array2[num2 - 1].lscpFirstSubLine + array2[num2 - 1].lsdcpSubLine - 1);
					int u = this.GetDistanceInsideTextCell(internalCp, isTrailing, array, num, ref lsTextCell) + lsTextCell.pointUvStartCell.x;
					int num4;
					this.CollectTextBoundsToBaseLevel(arrayList, ref lscpFirst, ref u, array, num, lscpEnd, out num4);
					if (num4 < num2)
					{
						this.CollectTextBoundsFromBaseLevel(arrayList, ref lscpFirst, ref u, array2, num2, num4);
					}
					this.AddValidTextBounds(arrayList, new TextBounds(LSRun.RectUV(origin, new LSPOINT(this.LSLineUToParagraphU(u), 0), new LSPOINT(this.LSLineUToParagraphU(this.GetDistanceInsideTextCell(internalCp2, isTrailing2, array2, num2, ref lsTextCell2) + lsTextCell2.pointUvStartCell.x), this._metrics._height), this), Convert.LsTFlowToFlowDirection(array2[num2 - 1].lstflowSubLine), this.CalculateTextRunBounds(lscpFirst, internalCp2 + 1)));
				}
				if (array3 == null)
				{
					if (arrayList.Count > 0)
					{
						if (flag)
						{
							this.AddValidTextBounds(arrayList, this.CreateCollapsingSymbolBounds());
						}
						array3 = new TextBounds[arrayList.Count];
						for (int i = 0; i < arrayList.Count; i++)
						{
							array3[i] = (TextBounds)arrayList[i];
						}
					}
					else
					{
						int horizontalPosition = this.LSLineUToParagraphU(this.GetDistanceInsideTextCell(internalCp, isTrailing, array, num, ref lsTextCell) + lsTextCell.pointUvStartCell.x);
						array3 = new TextBounds[]
						{
							new TextBounds(LSRun.RectUV(origin, new LSPOINT(horizontalPosition, 0), new LSPOINT(horizontalPosition, this._metrics._height), this), Convert.LsTFlowToFlowDirection(array[num - 1].lstflowSubLine), null)
						};
					}
				}
				return array3;
			}

			// Token: 0x06005B4C RID: 23372 RVA: 0x0016F054 File Offset: 0x0016E454
			private void CollectTextBoundsToBaseLevel(ArrayList boundsList, ref int lscpCurrent, ref int currentDistance, LsQSubInfo[] sublines, int sublineDepth, int lscpEnd, out int baseLevelDepth)
			{
				baseLevelDepth = sublineDepth;
				if (lscpEnd < sublines[sublineDepth - 1].lscpFirstSubLine + sublines[sublineDepth - 1].lsdcpSubLine)
				{
					return;
				}
				this.AddValidTextBounds(boundsList, new TextBounds(LSRun.RectUV(new Point(0.0, 0.0), new LSPOINT(this.LSLineUToParagraphU(currentDistance), 0), new LSPOINT(this.LSLineUToParagraphU(this.GetEndOfSublineDistance(sublines, sublineDepth - 1)), this._metrics._height), this), Convert.LsTFlowToFlowDirection(sublines[sublineDepth - 1].lstflowSubLine), this.CalculateTextRunBounds(lscpCurrent, sublines[sublineDepth - 1].lscpFirstSubLine + sublines[sublineDepth - 1].lsdcpSubLine)));
				baseLevelDepth = sublineDepth - 1;
				while (baseLevelDepth > 0 && lscpEnd >= sublines[baseLevelDepth - 1].lscpFirstSubLine + sublines[baseLevelDepth - 1].lsdcpSubLine)
				{
					int num = baseLevelDepth - 1;
					this.AddValidTextBounds(boundsList, new TextBounds(LSRun.RectUV(new Point(0.0, 0.0), new LSPOINT(this.LSLineUToParagraphU(this.GetEndOfRunDistance(sublines, num)), 0), new LSPOINT(this.LSLineUToParagraphU(this.GetEndOfSublineDistance(sublines, num)), this._metrics._height), this), Convert.LsTFlowToFlowDirection(sublines[num].lstflowSubLine), this.CalculateTextRunBounds(sublines[num].lscpFirstRun + sublines[num].lsdcpRun, sublines[num].lscpFirstSubLine + sublines[num].lsdcpSubLine)));
					baseLevelDepth--;
				}
				Invariant.Assert(baseLevelDepth >= 1);
				lscpCurrent = sublines[baseLevelDepth - 1].lscpFirstRun + sublines[baseLevelDepth - 1].lsdcpRun;
				currentDistance = this.GetEndOfRunDistance(sublines, baseLevelDepth - 1);
			}

			// Token: 0x06005B4D RID: 23373 RVA: 0x0016F25C File Offset: 0x0016E65C
			private void CollectTextBoundsFromBaseLevel(ArrayList boundsList, ref int lscpCurrent, ref int currentDistance, LsQSubInfo[] sublines, int sublineDepth, int baseLevelDepth)
			{
				Invariant.Assert(lscpCurrent <= sublines[baseLevelDepth - 1].lscpFirstRun);
				this.AddValidTextBounds(boundsList, new TextBounds(LSRun.RectUV(new Point(0.0, 0.0), new LSPOINT(this.LSLineUToParagraphU(currentDistance), 0), new LSPOINT(this.LSLineUToParagraphU(sublines[baseLevelDepth - 1].pointUvStartRun.x), this._metrics._height), this), Convert.LsTFlowToFlowDirection(sublines[baseLevelDepth - 1].lstflowSubLine), this.CalculateTextRunBounds(lscpCurrent, sublines[baseLevelDepth - 1].lscpFirstRun)));
				for (int i = baseLevelDepth; i < sublineDepth - 1; i++)
				{
					this.AddValidTextBounds(boundsList, new TextBounds(LSRun.RectUV(new Point(0.0, 0.0), new LSPOINT(this.LSLineUToParagraphU(sublines[i].pointUvStartSubLine.x), 0), new LSPOINT(this.LSLineUToParagraphU(sublines[i].pointUvStartRun.x), this._metrics._height), this), Convert.LsTFlowToFlowDirection(sublines[i].lstflowSubLine), this.CalculateTextRunBounds(sublines[i].lscpFirstSubLine, sublines[i].lscpFirstRun)));
				}
				lscpCurrent = sublines[sublineDepth - 1].lscpFirstSubLine;
				currentDistance = sublines[sublineDepth - 1].pointUvStartSubLine.x;
			}

			// Token: 0x06005B4E RID: 23374 RVA: 0x0016F3F4 File Offset: 0x0016E7F4
			private int GetEndOfSublineDistance(LsQSubInfo[] sublines, int index)
			{
				return sublines[index].pointUvStartSubLine.x + ((sublines[index].lstflowSubLine == sublines[0].lstflowSubLine) ? sublines[index].dupSubLine : (-sublines[index].dupSubLine));
			}

			// Token: 0x06005B4F RID: 23375 RVA: 0x0016F448 File Offset: 0x0016E848
			private int GetEndOfRunDistance(LsQSubInfo[] sublines, int index)
			{
				return sublines[index].pointUvStartRun.x + ((sublines[index].lstflowSubLine == sublines[0].lstflowSubLine) ? sublines[index].dupRun : (-sublines[index].dupRun));
			}

			// Token: 0x06005B50 RID: 23376 RVA: 0x0016F49C File Offset: 0x0016E89C
			private void AddValidTextBounds(ArrayList boundsList, TextBounds bounds)
			{
				if (bounds.Rectangle.Width != 0.0 && bounds.Rectangle.Height != 0.0)
				{
					boundsList.Add(bounds);
				}
			}

			// Token: 0x06005B51 RID: 23377 RVA: 0x0016F4E4 File Offset: 0x0016E8E4
			private IList<TextRunBounds> CalculateTextRunBounds(int lscpFirst, int lscpEnd)
			{
				if (lscpEnd <= lscpFirst)
				{
					return null;
				}
				int num = lscpFirst;
				int i = lscpEnd - lscpFirst;
				SpanRider spanRider = new SpanRider(this._plsrunVector);
				Point origin = new Point(0.0, 0.0);
				IList<TextRunBounds> list = new List<TextRunBounds>(2);
				while (i > 0)
				{
					spanRider.At(num - this._cpFirst);
					Plsrun plsrun = (Plsrun)spanRider.CurrentElement;
					int num2 = Math.Min(spanRider.Length, i);
					if (TextStore.IsContent(plsrun))
					{
						LSRun run = this.GetRun(plsrun);
						if (run.Type == Plsrun.Text || run.Type == Plsrun.InlineObject)
						{
							int externalCp = this.GetExternalCp(num);
							int num3 = num2;
							if (this.HasCollapsed && this._collapsedRange != null && externalCp <= this._collapsedRange.TextSourceCharacterIndex && externalCp + num3 >= this._collapsedRange.TextSourceCharacterIndex && externalCp + num3 < this._collapsedRange.TextSourceCharacterIndex + this._collapsedRange.Length)
							{
								num3 = this._collapsedRange.TextSourceCharacterIndex - externalCp;
							}
							if (num3 > 0)
							{
								TextRunBounds item = new TextRunBounds(LSRun.RectUV(origin, new LSPOINT(this.LSLineUToParagraphU(this.DistanceFromCharacterHit(new CharacterHit(externalCp, 0))), this._metrics._baselineOffset - run.BaselineOffset + run.BaselineMoveOffset), new LSPOINT(this.LSLineUToParagraphU(this.DistanceFromCharacterHit(new CharacterHit(externalCp + num3 - 1, 1))), this._metrics._baselineOffset - run.BaselineOffset + run.BaselineMoveOffset + run.Height), this), externalCp, externalCp + num3, run.TextRun);
								list.Add(item);
							}
						}
					}
					i -= num2;
					num += num2;
				}
				if (list.Count <= 0)
				{
					return null;
				}
				return list;
			}

			// Token: 0x06005B52 RID: 23378 RVA: 0x0016F6A4 File Offset: 0x0016EAA4
			public override IList<TextSpan<TextRun>> GetTextRunSpans()
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				if (this._plsrunVector == null)
				{
					return new TextSpan<TextRun>[0];
				}
				IList<TextSpan<TextRun>> list = new List<TextSpan<TextRun>>(2);
				TextRun textRun = null;
				int num = 0;
				int num2 = this._metrics._cchLength;
				int num3 = 0;
				while (num3 < this._plsrunVector.Count && num2 > 0)
				{
					Span span = this._plsrunVector[num3];
					int num4 = this.CpCount(span);
					num4 = Math.Min(num4, num2);
					if (num4 > 0)
					{
						TextRun textRun2 = this.GetRun((Plsrun)span.element).TextRun;
						if (textRun != null && textRun2 != textRun)
						{
							list.Add(new TextSpan<TextRun>(num, textRun));
							num = 0;
						}
						textRun = textRun2;
						num += num4;
						num2 -= num4;
					}
					num3++;
				}
				if (textRun != null)
				{
					list.Add(new TextSpan<TextRun>(num, textRun));
				}
				return list;
			}

			// Token: 0x06005B53 RID: 23379 RVA: 0x0016F784 File Offset: 0x0016EB84
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public override IEnumerable<IndexedGlyphRun> GetIndexedGlyphRuns()
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				IEnumerable<IndexedGlyphRun> result = null;
				if (this._ploline.Value != IntPtr.Zero)
				{
					TextFormatterContext textFormatterContext = this._metrics._formatter.AcquireContext(new DrawingState(null, new Point(0.0, 0.0), null, this), this._ploc.Value);
					LSPOINT lspoint = new LSPOINT(0, 0);
					LsErr lsErr = UnsafeNativeMethods.LoEnumLine(this._ploline.Value, false, false, ref lspoint);
					result = textFormatterContext.IndexedGlyphRuns;
					Exception callbackException = textFormatterContext.CallbackException;
					textFormatterContext.ClearIndexedGlyphRuns();
					textFormatterContext.Release();
					if (lsErr != LsErr.None)
					{
						if (callbackException != null)
						{
							throw callbackException;
						}
						TextFormatterContext.ThrowExceptionFromLsError(SR.Get("EnumLineFailure", new object[]
						{
							lsErr
						}), lsErr);
					}
				}
				return result;
			}

			// Token: 0x06005B54 RID: 23380 RVA: 0x0016F864 File Offset: 0x0016EC64
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public override TextLineBreak GetTextLineBreak()
			{
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsDisposed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					throw new ObjectDisposedException(SR.Get("TextLineHasBeenDisposed"));
				}
				if ((this._statusFlags & TextMetrics.FullTextLine.StatusFlags.HasCollapsed) != TextMetrics.FullTextLine.StatusFlags.None)
				{
					return null;
				}
				return this._metrics.GetTextLineBreak(IntPtr.Zero);
			}

			// Token: 0x17001293 RID: 4755
			// (get) Token: 0x06005B55 RID: 23381 RVA: 0x0016F8A8 File Offset: 0x0016ECA8
			public override int TrailingWhitespaceLength
			{
				get
				{
					if (this._metrics._textWidth == this._metrics._textWidthAtTrailing)
					{
						return this._metrics._cchNewline;
					}
					CharacterHit characterHit = this.CharacterHitFromDistance(this._metrics._textWidthAtTrailing + this._metrics._textStart);
					return this._cpFirst + this._metrics._cchLength - characterHit.FirstCharacterIndex - characterHit.TrailingLength;
				}
			}

			// Token: 0x17001294 RID: 4756
			// (get) Token: 0x06005B56 RID: 23382 RVA: 0x0016F91C File Offset: 0x0016ED1C
			public override int Length
			{
				get
				{
					return this._metrics.Length;
				}
			}

			// Token: 0x17001295 RID: 4757
			// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0016F934 File Offset: 0x0016ED34
			public override int DependentLength
			{
				get
				{
					return this._metrics.DependentLength;
				}
			}

			// Token: 0x17001296 RID: 4758
			// (get) Token: 0x06005B58 RID: 23384 RVA: 0x0016F94C File Offset: 0x0016ED4C
			public override int NewlineLength
			{
				get
				{
					return this._metrics.NewlineLength;
				}
			}

			// Token: 0x17001297 RID: 4759
			// (get) Token: 0x06005B59 RID: 23385 RVA: 0x0016F964 File Offset: 0x0016ED64
			public override double Start
			{
				get
				{
					return this._metrics.Start;
				}
			}

			// Token: 0x17001298 RID: 4760
			// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0016F97C File Offset: 0x0016ED7C
			public override double Width
			{
				get
				{
					return this._metrics.Width;
				}
			}

			// Token: 0x17001299 RID: 4761
			// (get) Token: 0x06005B5B RID: 23387 RVA: 0x0016F994 File Offset: 0x0016ED94
			public override double WidthIncludingTrailingWhitespace
			{
				get
				{
					return this._metrics.WidthIncludingTrailingWhitespace;
				}
			}

			// Token: 0x1700129A RID: 4762
			// (get) Token: 0x06005B5C RID: 23388 RVA: 0x0016F9AC File Offset: 0x0016EDAC
			public override double Height
			{
				get
				{
					return this._metrics.Height;
				}
			}

			// Token: 0x1700129B RID: 4763
			// (get) Token: 0x06005B5D RID: 23389 RVA: 0x0016F9C4 File Offset: 0x0016EDC4
			public override double TextHeight
			{
				get
				{
					return this._metrics.TextHeight;
				}
			}

			// Token: 0x1700129C RID: 4764
			// (get) Token: 0x06005B5E RID: 23390 RVA: 0x0016F9DC File Offset: 0x0016EDDC
			public override double Baseline
			{
				get
				{
					return this._metrics.Baseline;
				}
			}

			// Token: 0x1700129D RID: 4765
			// (get) Token: 0x06005B5F RID: 23391 RVA: 0x0016F9F4 File Offset: 0x0016EDF4
			public override double TextBaseline
			{
				get
				{
					return this._metrics.TextBaseline;
				}
			}

			// Token: 0x1700129E RID: 4766
			// (get) Token: 0x06005B60 RID: 23392 RVA: 0x0016FA0C File Offset: 0x0016EE0C
			public override double MarkerBaseline
			{
				get
				{
					return this._metrics.MarkerBaseline;
				}
			}

			// Token: 0x1700129F RID: 4767
			// (get) Token: 0x06005B61 RID: 23393 RVA: 0x0016FA24 File Offset: 0x0016EE24
			public override double MarkerHeight
			{
				get
				{
					return this._metrics.MarkerHeight;
				}
			}

			// Token: 0x170012A0 RID: 4768
			// (get) Token: 0x06005B62 RID: 23394 RVA: 0x0016FA3C File Offset: 0x0016EE3C
			public override double Extent
			{
				get
				{
					this.CheckBoundingBox();
					return this._overhang.Extent;
				}
			}

			// Token: 0x170012A1 RID: 4769
			// (get) Token: 0x06005B63 RID: 23395 RVA: 0x0016FA5C File Offset: 0x0016EE5C
			public override double OverhangLeading
			{
				get
				{
					this.CheckBoundingBox();
					return this._overhang.Leading;
				}
			}

			// Token: 0x170012A2 RID: 4770
			// (get) Token: 0x06005B64 RID: 23396 RVA: 0x0016FA7C File Offset: 0x0016EE7C
			public override double OverhangTrailing
			{
				get
				{
					this.CheckBoundingBox();
					return this._overhang.Trailing;
				}
			}

			// Token: 0x170012A3 RID: 4771
			// (get) Token: 0x06005B65 RID: 23397 RVA: 0x0016FA9C File Offset: 0x0016EE9C
			public override double OverhangAfter
			{
				get
				{
					this.CheckBoundingBox();
					return this._overhang.Extent - this.Height - this._overhang.Before;
				}
			}

			// Token: 0x170012A4 RID: 4772
			// (get) Token: 0x06005B66 RID: 23398 RVA: 0x0016FAD0 File Offset: 0x0016EED0
			public override bool HasOverflowed
			{
				get
				{
					return (this._statusFlags & TextMetrics.FullTextLine.StatusFlags.HasOverflowed) > TextMetrics.FullTextLine.StatusFlags.None;
				}
			}

			// Token: 0x170012A5 RID: 4773
			// (get) Token: 0x06005B67 RID: 23399 RVA: 0x0016FAE8 File Offset: 0x0016EEE8
			public override bool HasCollapsed
			{
				get
				{
					return (this._statusFlags & TextMetrics.FullTextLine.StatusFlags.HasCollapsed) > TextMetrics.FullTextLine.StatusFlags.None;
				}
			}

			// Token: 0x170012A6 RID: 4774
			// (get) Token: 0x06005B68 RID: 23400 RVA: 0x0016FB04 File Offset: 0x0016EF04
			public override bool IsTruncated
			{
				get
				{
					return (this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsTruncated) > TextMetrics.FullTextLine.StatusFlags.None;
				}
			}

			// Token: 0x170012A7 RID: 4775
			// (get) Token: 0x06005B69 RID: 23401 RVA: 0x0016FB20 File Offset: 0x0016EF20
			public int CpFirst
			{
				get
				{
					return this._cpFirst;
				}
			}

			// Token: 0x170012A8 RID: 4776
			// (get) Token: 0x06005B6A RID: 23402 RVA: 0x0016FB34 File Offset: 0x0016EF34
			public TextSource TextSource
			{
				get
				{
					return this._textSource;
				}
			}

			// Token: 0x06005B6B RID: 23403 RVA: 0x0016FB48 File Offset: 0x0016EF48
			[SecurityCritical]
			[SecurityTreatAsSafe]
			private unsafe void QueryLinePointPcp(Point ptQuery, LsQSubInfo[] subLineInfo, out int actualDepthQuery, out LsTextCell lsTextCell)
			{
				lsTextCell = default(LsTextCell);
				LsErr lsErr;
				fixed (LsQSubInfo[] array = subLineInfo)
				{
					LsQSubInfo* value;
					if (subLineInfo == null || array.Length == 0)
					{
						value = null;
					}
					else
					{
						value = &array[0];
					}
					LSPOINT lspoint = new LSPOINT((int)ptQuery.X, (int)ptQuery.Y);
					lsErr = UnsafeNativeMethods.LoQueryLinePointPcp(this._ploline.Value, ref lspoint, subLineInfo.Length, (IntPtr)((void*)value), out actualDepthQuery, out lsTextCell);
				}
				if (lsErr != LsErr.None)
				{
					TextFormatterContext.ThrowExceptionFromLsError(SR.Get("QueryLineFailure", new object[]
					{
						lsErr
					}), lsErr);
				}
				if (lsTextCell.lscpEndCell < lsTextCell.lscpStartCell)
				{
					lsTextCell.lscpEndCell = lsTextCell.lscpStartCell;
				}
			}

			// Token: 0x06005B6C RID: 23404 RVA: 0x0016FBF0 File Offset: 0x0016EFF0
			[SecurityTreatAsSafe]
			[SecurityCritical]
			private unsafe void QueryLineCpPpoint(int lscpQuery, LsQSubInfo[] subLineInfo, out int actualDepthQuery, out LsTextCell lsTextCell)
			{
				lsTextCell = default(LsTextCell);
				int lscpQuery2 = (lscpQuery < this._metrics._lscpLim) ? lscpQuery : (this._metrics._lscpLim - 1);
				LsErr lsErr;
				fixed (LsQSubInfo[] array = subLineInfo)
				{
					LsQSubInfo* value;
					if (subLineInfo == null || array.Length == 0)
					{
						value = null;
					}
					else
					{
						value = &array[0];
					}
					lsErr = UnsafeNativeMethods.LoQueryLineCpPpoint(this._ploline.Value, lscpQuery2, subLineInfo.Length, (IntPtr)((void*)value), out actualDepthQuery, out lsTextCell);
				}
				if (lsErr != LsErr.None)
				{
					TextFormatterContext.ThrowExceptionFromLsError(SR.Get("QueryLineFailure", new object[]
					{
						lsErr
					}), lsErr);
				}
				if (lsTextCell.lscpEndCell < lsTextCell.lscpStartCell)
				{
					lsTextCell.lscpEndCell = lsTextCell.lscpStartCell;
				}
			}

			// Token: 0x06005B6D RID: 23405 RVA: 0x0016FCA0 File Offset: 0x0016F0A0
			internal int LSLineUToParagraphU(int u)
			{
				return u + this._metrics._paragraphToText - this._metrics._textStart;
			}

			// Token: 0x06005B6E RID: 23406 RVA: 0x0016FCC8 File Offset: 0x0016F0C8
			internal int ParagraphUToLSLineU(int u)
			{
				return u - this._metrics._paragraphToText + this._metrics._textStart;
			}

			// Token: 0x170012A9 RID: 4777
			// (get) Token: 0x06005B6F RID: 23407 RVA: 0x0016FCF0 File Offset: 0x0016F0F0
			internal int BaselineOffset
			{
				get
				{
					return this._metrics._baselineOffset;
				}
			}

			// Token: 0x170012AA RID: 4778
			// (get) Token: 0x06005B70 RID: 23408 RVA: 0x0016FD08 File Offset: 0x0016F108
			internal int ParagraphWidth
			{
				get
				{
					return this._paragraphWidth;
				}
			}

			// Token: 0x170012AB RID: 4779
			// (get) Token: 0x06005B71 RID: 23409 RVA: 0x0016FD1C File Offset: 0x0016F11C
			internal double MinWidth
			{
				get
				{
					return this._metrics._formatter.IdealToReal((double)(this._textMinWidthAtTrailing + this._metrics._textStart), base.PixelsPerDip);
				}
			}

			// Token: 0x170012AC RID: 4780
			// (get) Token: 0x06005B72 RID: 23410 RVA: 0x0016FD54 File Offset: 0x0016F154
			internal bool RightToLeft
			{
				get
				{
					return (this._statusFlags & TextMetrics.FullTextLine.StatusFlags.RightToLeft) > TextMetrics.FullTextLine.StatusFlags.None;
				}
			}

			// Token: 0x170012AD RID: 4781
			// (get) Token: 0x06005B73 RID: 23411 RVA: 0x0016FD6C File Offset: 0x0016F16C
			internal TextFormatterImp Formatter
			{
				get
				{
					return this._metrics._formatter;
				}
			}

			// Token: 0x170012AE RID: 4782
			// (get) Token: 0x06005B74 RID: 23412 RVA: 0x0016FD84 File Offset: 0x0016F184
			internal bool IsJustified
			{
				get
				{
					return (this._statusFlags & TextMetrics.FullTextLine.StatusFlags.IsJustified) > TextMetrics.FullTextLine.StatusFlags.None;
				}
			}

			// Token: 0x170012AF RID: 4783
			// (get) Token: 0x06005B75 RID: 23413 RVA: 0x0016FDA0 File Offset: 0x0016F1A0
			internal TextDecorationCollection TextDecorations
			{
				get
				{
					return this._paragraphTextDecorations;
				}
			}

			// Token: 0x170012B0 RID: 4784
			// (get) Token: 0x06005B76 RID: 23414 RVA: 0x0016FDB4 File Offset: 0x0016F1B4
			internal Brush DefaultTextDecorationsBrush
			{
				get
				{
					return this._defaultTextDecorationsBrush;
				}
			}

			// Token: 0x06005B77 RID: 23415 RVA: 0x0016FDC8 File Offset: 0x0016F1C8
			private void BuildOverhang(Point origin, Rect boundingBox)
			{
				if (boundingBox.IsEmpty)
				{
					this._overhang.Leading = (this._overhang.Trailing = 0.0);
					this._overhang.Before = 0.0;
					this._overhang.Extent = 0.0;
					return;
				}
				boundingBox.X -= origin.X;
				boundingBox.Y -= origin.Y;
				if (this.RightToLeft)
				{
					double num = this._metrics._formatter.IdealToReal((double)this._paragraphWidth, base.PixelsPerDip);
					this._overhang.Leading = num - this.Start - boundingBox.Right;
					this._overhang.Trailing = boundingBox.Left - (num - this.Start - this.Width);
				}
				else
				{
					this._overhang.Leading = boundingBox.Left - this.Start;
					this._overhang.Trailing = this.Start + this.Width - boundingBox.Right;
				}
				this._overhang.Extent = boundingBox.Bottom - boundingBox.Top;
				this._overhang.Before = -boundingBox.Top;
			}

			// Token: 0x06005B78 RID: 23416 RVA: 0x0016FF20 File Offset: 0x0016F320
			internal int GetInternalCp(int cp)
			{
				int num = this._cpFirst;
				int num2 = cp;
				cp = num;
				foreach (object obj in this._plsrunVector)
				{
					Span span = (Span)obj;
					int num3 = this.CpCount(span);
					if (num3 > 0)
					{
						if (cp + num3 > num2)
						{
							num += ((num3 == span.length) ? (num2 - cp) : 0);
							break;
						}
						cp += num3;
					}
					num += span.length;
				}
				return num;
			}

			// Token: 0x06005B79 RID: 23417 RVA: 0x0016FFC8 File Offset: 0x0016F3C8
			internal int GetExternalCp(int lscp)
			{
				if (lscp < this._metrics._lscpLim)
				{
					SpanRider spanRider = new SpanRider(this._plsrunVector);
					int offsetToFirstCp;
					do
					{
						spanRider.At(lscp - this._cpFirst);
						offsetToFirstCp = this.GetRun((Plsrun)spanRider.CurrentElement).OffsetToFirstCp;
					}
					while (offsetToFirstCp < 0 && ++lscp < this._metrics._lscpLim);
					return offsetToFirstCp + lscp - spanRider.CurrentSpanStart;
				}
				if (this._collapsedRange != null)
				{
					return this._collapsedRange.TextSourceCharacterIndex;
				}
				return this._cpFirst + this._metrics._cchLength;
			}

			// Token: 0x06005B7A RID: 23418 RVA: 0x00170064 File Offset: 0x0016F464
			internal int CpCount(Span plsrunSpan)
			{
				Plsrun plsrun = (Plsrun)plsrunSpan.element;
				plsrun = TextStore.ToIndex(plsrun);
				if (plsrun >= Plsrun.FormatAnchor)
				{
					LSRun run = this.GetRun(plsrun);
					return run.Length;
				}
				return 0;
			}

			// Token: 0x06005B7B RID: 23419 RVA: 0x00170098 File Offset: 0x0016F498
			internal LSRun GetRun(Plsrun plsrun)
			{
				ArrayList arrayList = this._lsrunsMainText;
				if (TextStore.IsMarker(plsrun))
				{
					arrayList = this._lsrunsMarkerText;
				}
				plsrun = TextStore.ToIndex(plsrun);
				return (LSRun)(TextStore.IsContent(plsrun) ? arrayList[(int)(plsrun - Plsrun.FormatAnchor)] : TextStore.ControlRuns[(int)plsrun]);
			}

			// Token: 0x04002E5A RID: 11866
			private TextMetrics _metrics;

			// Token: 0x04002E5B RID: 11867
			private int _cpFirst;

			// Token: 0x04002E5C RID: 11868
			private int _depthQueryMax;

			// Token: 0x04002E5D RID: 11869
			private int _paragraphWidth;

			// Token: 0x04002E5E RID: 11870
			private int _textMinWidthAtTrailing;

			// Token: 0x04002E5F RID: 11871
			private SecurityCriticalDataForSet<IntPtr> _ploline;

			// Token: 0x04002E60 RID: 11872
			private SecurityCriticalDataForSet<IntPtr> _ploc;

			// Token: 0x04002E61 RID: 11873
			private TextMetrics.FullTextLine.Overhang _overhang;

			// Token: 0x04002E62 RID: 11874
			private TextMetrics.FullTextLine.StatusFlags _statusFlags;

			// Token: 0x04002E63 RID: 11875
			private SpanVector _plsrunVector;

			// Token: 0x04002E64 RID: 11876
			private ArrayList _lsrunsMainText;

			// Token: 0x04002E65 RID: 11877
			private ArrayList _lsrunsMarkerText;

			// Token: 0x04002E66 RID: 11878
			private FullTextState _fullText;

			// Token: 0x04002E67 RID: 11879
			private FormattedTextSymbols _collapsingSymbol;

			// Token: 0x04002E68 RID: 11880
			private TextCollapsedRange _collapsedRange;

			// Token: 0x04002E69 RID: 11881
			private TextSource _textSource;

			// Token: 0x04002E6A RID: 11882
			private TextDecorationCollection _paragraphTextDecorations;

			// Token: 0x04002E6B RID: 11883
			private Brush _defaultTextDecorationsBrush;

			// Token: 0x04002E6C RID: 11884
			private TextFormattingMode _textFormattingMode;

			// Token: 0x02000A30 RID: 2608
			[Flags]
			private enum StatusFlags
			{
				// Token: 0x04002FBE RID: 12222
				None = 0,
				// Token: 0x04002FBF RID: 12223
				IsDisposed = 1,
				// Token: 0x04002FC0 RID: 12224
				HasOverflowed = 2,
				// Token: 0x04002FC1 RID: 12225
				BoundingBoxComputed = 4,
				// Token: 0x04002FC2 RID: 12226
				RightToLeft = 8,
				// Token: 0x04002FC3 RID: 12227
				HasCollapsed = 16,
				// Token: 0x04002FC4 RID: 12228
				KeepState = 32,
				// Token: 0x04002FC5 RID: 12229
				IsTruncated = 64,
				// Token: 0x04002FC6 RID: 12230
				IsJustified = 128
			}

			// Token: 0x02000A31 RID: 2609
			private enum CaretDirection
			{
				// Token: 0x04002FC8 RID: 12232
				Forward,
				// Token: 0x04002FC9 RID: 12233
				Backward,
				// Token: 0x04002FCA RID: 12234
				Backspace
			}

			// Token: 0x02000A32 RID: 2610
			private struct Overhang
			{
				// Token: 0x04002FCB RID: 12235
				internal double Leading;

				// Token: 0x04002FCC RID: 12236
				internal double Trailing;

				// Token: 0x04002FCD RID: 12237
				internal double Extent;

				// Token: 0x04002FCE RID: 12238
				internal double Before;
			}
		}
	}
}
