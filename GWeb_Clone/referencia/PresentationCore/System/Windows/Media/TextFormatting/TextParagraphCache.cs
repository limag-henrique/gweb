using System;
using System.Collections.Generic;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	// Token: 0x020005B0 RID: 1456
	[FriendAccessAllowed]
	internal sealed class TextParagraphCache : IDisposable
	{
		// Token: 0x06004292 RID: 17042 RVA: 0x00103C60 File Offset: 0x00103060
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal TextParagraphCache(FormatSettings settings, int firstCharIndex, int paragraphWidth)
		{
			Invariant.Assert(settings != null);
			this._finiteFormatWidth = settings.GetFiniteFormatWidth(paragraphWidth);
			this._fullText = FullTextState.Create(settings, firstCharIndex, this._finiteFormatWidth);
			TextFormatterContext textFormatterContext = settings.Formatter.AcquireContext(this._fullText, IntPtr.Zero);
			this._fullText.SetTabs(textFormatterContext);
			IntPtr zero = IntPtr.Zero;
			LsErr lsErr = textFormatterContext.CreateParaBreakingSession(firstCharIndex, this._finiteFormatWidth, IntPtr.Zero, ref zero, ref this._penalizedAsJustified);
			Exception callbackException = textFormatterContext.CallbackException;
			textFormatterContext.Release();
			if (lsErr != LsErr.None)
			{
				GC.SuppressFinalize(this);
				if (callbackException != null)
				{
					throw new InvalidOperationException(SR.Get("CreateParaBreakingSessionFailure", new object[]
					{
						lsErr
					}), callbackException);
				}
				TextFormatterContext.ThrowExceptionFromLsError(SR.Get("CreateParaBreakingSessionFailure", new object[]
				{
					lsErr
				}), lsErr);
			}
			this._ploparabreak.Value = zero;
			GC.KeepAlive(textFormatterContext);
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x00103D4C File Offset: 0x0010314C
		~TextParagraphCache()
		{
			this.Dispose(false);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x00103D88 File Offset: 0x00103188
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x00103DA4 File Offset: 0x001031A4
		internal IList<TextBreakpoint> FormatBreakpoints(int firstCharIndex, TextLineBreak previousLineBreak, IntPtr breakpointRestrictionHandle, double maxLineWidth, out int bestFitIndex)
		{
			return FullTextBreakpoint.CreateMultiple(this, firstCharIndex, this.VerifyMaxLineWidth(maxLineWidth), previousLineBreak, breakpointRestrictionHandle, out bestFitIndex);
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x00103DC4 File Offset: 0x001031C4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void Dispose(bool disposing)
		{
			if (this._ploparabreak.Value != IntPtr.Zero)
			{
				UnsafeNativeMethods.LoDisposeParaBreakingSession(this._ploparabreak.Value, !disposing);
				this._ploparabreak.Value = IntPtr.Zero;
				GC.KeepAlive(this);
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x00103E14 File Offset: 0x00103214
		private int VerifyMaxLineWidth(double maxLineWidth)
		{
			if (DoubleUtil.IsNaN(maxLineWidth))
			{
				throw new ArgumentOutOfRangeException("maxLineWidth", SR.Get("ParameterValueCannotBeNaN"));
			}
			if (maxLineWidth == 0.0 || double.IsPositiveInfinity(maxLineWidth))
			{
				return 1073741822;
			}
			if (maxLineWidth < 0.0 || maxLineWidth > 3579139.4066666667)
			{
				throw new ArgumentOutOfRangeException("maxLineWidth", SR.Get("ParameterMustBeBetween", new object[]
				{
					0,
					3579139.4066666667
				}));
			}
			return TextFormatterImp.RealToIdeal(maxLineWidth);
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x00103EAC File Offset: 0x001032AC
		internal FullTextState FullText
		{
			get
			{
				return this._fullText;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06004299 RID: 17049 RVA: 0x00103EC0 File Offset: 0x001032C0
		internal SecurityCriticalDataForSet<IntPtr> Ploparabreak
		{
			get
			{
				return this._ploparabreak;
			}
		}

		// Token: 0x0400183A RID: 6202
		private FullTextState _fullText;

		// Token: 0x0400183B RID: 6203
		private SecurityCriticalDataForSet<IntPtr> _ploparabreak;

		// Token: 0x0400183C RID: 6204
		private int _finiteFormatWidth;

		// Token: 0x0400183D RID: 6205
		private bool _penalizedAsJustified;
	}
}
