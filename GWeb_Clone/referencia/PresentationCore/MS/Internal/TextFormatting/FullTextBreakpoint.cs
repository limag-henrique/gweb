using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Media.TextFormatting;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000704 RID: 1796
	internal sealed class FullTextBreakpoint : TextBreakpoint
	{
		// Token: 0x06004D38 RID: 19768 RVA: 0x00131F04 File Offset: 0x00131304
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static IList<TextBreakpoint> CreateMultiple(TextParagraphCache paragraphCache, int firstCharIndex, int maxLineWidth, TextLineBreak previousLineBreak, IntPtr penaltyRestriction, out int bestFitIndex)
		{
			Invariant.Assert(paragraphCache != null);
			FullTextState fullText = paragraphCache.FullText;
			Invariant.Assert(fullText != null);
			FormatSettings settings = fullText.TextStore.Settings;
			Invariant.Assert(settings != null);
			settings.UpdateSettingsForCurrentLine(maxLineWidth, previousLineBreak, firstCharIndex == fullText.TextStore.CpFirst);
			Invariant.Assert(settings.Formatter != null);
			TextFormatterContext textFormatterContext = settings.Formatter.AcquireContext(fullText, IntPtr.Zero);
			IntPtr previousLineBreakRecord = IntPtr.Zero;
			if (settings.PreviousLineBreak != null)
			{
				previousLineBreakRecord = settings.PreviousLineBreak.BreakRecord.Value;
			}
			fullText.SetTabs(textFormatterContext);
			LsBreaks lsBreaks = default(LsBreaks);
			LsErr lsErr = textFormatterContext.CreateBreaks(fullText.GetBreakpointInternalCp(firstCharIndex), previousLineBreakRecord, paragraphCache.Ploparabreak.Value, penaltyRestriction, ref lsBreaks, out bestFitIndex);
			Exception callbackException = textFormatterContext.CallbackException;
			textFormatterContext.Release();
			if (lsErr != LsErr.None)
			{
				if (callbackException != null)
				{
					throw callbackException;
				}
				TextFormatterContext.ThrowExceptionFromLsError(SR.Get("CreateBreaksFailure", new object[]
				{
					lsErr
				}), lsErr);
			}
			GC.KeepAlive(textFormatterContext);
			TextBreakpoint[] array = new TextBreakpoint[lsBreaks.cBreaks];
			for (int i = 0; i < lsBreaks.cBreaks; i++)
			{
				array[i] = new FullTextBreakpoint(fullText, firstCharIndex, maxLineWidth, ref lsBreaks, i);
			}
			return array;
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x00132040 File Offset: 0x00131440
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe FullTextBreakpoint(FullTextState fullText, int firstCharIndex, int maxLineWidth, ref LsBreaks lsbreaks, int breakIndex) : this()
		{
			LsLineWidths lsLineWidths = default(LsLineWidths);
			lsLineWidths.upLimLine = maxLineWidth;
			lsLineWidths.upStartMainText = fullText.TextStore.Settings.TextIndent;
			lsLineWidths.upStartMarker = lsLineWidths.upStartMainText;
			lsLineWidths.upStartTrailing = lsLineWidths.upLimLine;
			lsLineWidths.upMinStartTrailing = lsLineWidths.upStartTrailing;
			this._metrics.Compute(fullText, firstCharIndex, maxLineWidth, null, ref lsLineWidths, lsbreaks.plslinfoArray + breakIndex);
			this._ploline = new SecurityCriticalDataForSet<IntPtr>(lsbreaks.pplolineArray[(IntPtr)breakIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]);
			this._penaltyResource = new SecurityCriticalDataForSet<IntPtr>(lsbreaks.plinepenaltyArray[(IntPtr)breakIndex * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]);
			if (lsbreaks.plslinfoArray[breakIndex].fForcedBreak != 0)
			{
				this._isLineTruncated = true;
			}
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x00132124 File Offset: 0x00131524
		private FullTextBreakpoint()
		{
			this._metrics = default(TextMetrics);
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x00132144 File Offset: 0x00131544
		~FullTextBreakpoint()
		{
			this.Dispose(false);
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x00132180 File Offset: 0x00131580
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._ploline.Value != IntPtr.Zero)
			{
				UnsafeNativeMethods.LoDisposeLine(this._ploline.Value, !disposing);
				this._ploline.Value = IntPtr.Zero;
				this._penaltyResource.Value = IntPtr.Zero;
				this._isDisposed = true;
				GC.KeepAlive(this);
			}
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x001321E8 File Offset: 0x001315E8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override TextLineBreak GetTextLineBreak()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(SR.Get("TextBreakpointHasBeenDisposed"));
			}
			return this._metrics.GetTextLineBreak(this._ploline.Value);
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x00132224 File Offset: 0x00131624
		[SecurityCritical]
		internal override SecurityCriticalDataForSet<IntPtr> GetTextPenaltyResource()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(SR.Get("TextBreakpointHasBeenDisposed"));
			}
			LsErr lsErr = UnsafeNativeMethods.LoRelievePenaltyResource(this._ploline.Value);
			if (lsErr != LsErr.None)
			{
				TextFormatterContext.ThrowExceptionFromLsError(SR.Get("RelievePenaltyResourceFailure", new object[]
				{
					lsErr
				}), lsErr);
			}
			return this._penaltyResource;
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x00132284 File Offset: 0x00131684
		public override bool IsTruncated
		{
			get
			{
				return this._isLineTruncated;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06004D40 RID: 19776 RVA: 0x00132298 File Offset: 0x00131698
		public override int Length
		{
			get
			{
				return this._metrics.Length;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x001322B0 File Offset: 0x001316B0
		public override int DependentLength
		{
			get
			{
				return this._metrics.DependentLength;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06004D42 RID: 19778 RVA: 0x001322C8 File Offset: 0x001316C8
		public override int NewlineLength
		{
			get
			{
				return this._metrics.NewlineLength;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06004D43 RID: 19779 RVA: 0x001322E0 File Offset: 0x001316E0
		public override double Start
		{
			get
			{
				return this._metrics.Start;
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06004D44 RID: 19780 RVA: 0x001322F8 File Offset: 0x001316F8
		public override double Width
		{
			get
			{
				return this._metrics.Width;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06004D45 RID: 19781 RVA: 0x00132310 File Offset: 0x00131710
		public override double WidthIncludingTrailingWhitespace
		{
			get
			{
				return this._metrics.WidthIncludingTrailingWhitespace;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06004D46 RID: 19782 RVA: 0x00132328 File Offset: 0x00131728
		public override double Height
		{
			get
			{
				return this._metrics.Height;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06004D47 RID: 19783 RVA: 0x00132340 File Offset: 0x00131740
		public override double TextHeight
		{
			get
			{
				return this._metrics.TextHeight;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06004D48 RID: 19784 RVA: 0x00132358 File Offset: 0x00131758
		public override double Baseline
		{
			get
			{
				return this._metrics.Baseline;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06004D49 RID: 19785 RVA: 0x00132370 File Offset: 0x00131770
		public override double TextBaseline
		{
			get
			{
				return this._metrics.TextBaseline;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06004D4A RID: 19786 RVA: 0x00132388 File Offset: 0x00131788
		public override double MarkerBaseline
		{
			get
			{
				return this._metrics.MarkerBaseline;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06004D4B RID: 19787 RVA: 0x001323A0 File Offset: 0x001317A0
		public override double MarkerHeight
		{
			get
			{
				return this._metrics.MarkerHeight;
			}
		}

		// Token: 0x04002196 RID: 8598
		private TextMetrics _metrics;

		// Token: 0x04002197 RID: 8599
		private SecurityCriticalDataForSet<IntPtr> _ploline;

		// Token: 0x04002198 RID: 8600
		private SecurityCriticalDataForSet<IntPtr> _penaltyResource;

		// Token: 0x04002199 RID: 8601
		private bool _isDisposed;

		// Token: 0x0400219A RID: 8602
		private bool _isLineTruncated;
	}
}
