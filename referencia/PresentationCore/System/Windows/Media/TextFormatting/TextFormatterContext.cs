using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	// Token: 0x020005A5 RID: 1445
	[FriendAccessAllowed]
	internal class TextFormatterContext
	{
		// Token: 0x06004235 RID: 16949 RVA: 0x00102ECC File Offset: 0x001022CC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public TextFormatterContext()
		{
			this._ploc = new SecurityCriticalDataForSet<IntPtr>(IntPtr.Zero);
			this.Init();
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x00102EF8 File Offset: 0x001022F8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void Init()
		{
			if (this._ploc.Value == IntPtr.Zero)
			{
				LsContextInfo lsContextInfo = default(LsContextInfo);
				LscbkRedefined lscbkRedefined = default(LscbkRedefined);
				this._callbacks = new LineServicesCallbacks();
				this._callbacks.PopulateContextInfo(ref lsContextInfo, ref lscbkRedefined);
				lsContextInfo.version = 4U;
				lsContextInfo.pols = IntPtr.Zero;
				lsContextInfo.cEstimatedCharsPerLine = 100;
				lsContextInfo.fDontReleaseRuns = 1;
				lsContextInfo.cJustPriorityLim = 3;
				lsContextInfo.wchNull = '\0';
				lsContextInfo.wchUndef = '\u0001';
				lsContextInfo.wchTab = '\t';
				lsContextInfo.wchPosTab = lsContextInfo.wchUndef;
				lsContextInfo.wchEndPara1 = '\u2029';
				lsContextInfo.wchEndPara2 = lsContextInfo.wchUndef;
				lsContextInfo.wchSpace = ' ';
				lsContextInfo.wchHyphen = TextAnalyzer.CharHyphen;
				lsContextInfo.wchNonReqHyphen = '­';
				lsContextInfo.wchNonBreakHyphen = '‑';
				lsContextInfo.wchEnDash = '–';
				lsContextInfo.wchEmDash = '—';
				lsContextInfo.wchEnSpace = '\u2002';
				lsContextInfo.wchEmSpace = '\u2003';
				lsContextInfo.wchNarrowSpace = '\u2009';
				lsContextInfo.wchJoiner = '‍';
				lsContextInfo.wchNonJoiner = '‌';
				lsContextInfo.wchVisiNull = '⁐';
				lsContextInfo.wchVisiAltEndPara = '⁑';
				lsContextInfo.wchVisiEndLineInPara = '⁒';
				lsContextInfo.wchVisiEndPara = '⁓';
				lsContextInfo.wchVisiSpace = '⁔';
				lsContextInfo.wchVisiNonBreakSpace = '⁕';
				lsContextInfo.wchVisiNonBreakHyphen = '⁖';
				lsContextInfo.wchVisiNonReqHyphen = '⁗';
				lsContextInfo.wchVisiTab = '⁘';
				lsContextInfo.wchVisiPosTab = lsContextInfo.wchUndef;
				lsContextInfo.wchVisiEmSpace = '⁙';
				lsContextInfo.wchVisiEnSpace = '⁚';
				lsContextInfo.wchVisiNarrowSpace = '⁛';
				lsContextInfo.wchVisiOptBreak = '⁜';
				lsContextInfo.wchVisiNoBreak = '⁝';
				lsContextInfo.wchVisiFESpace = '⁞';
				lsContextInfo.wchFESpace = '\u3000';
				lsContextInfo.wchEscAnmRun = '\u2029';
				lsContextInfo.wchAltEndPara = lsContextInfo.wchUndef;
				lsContextInfo.wchEndLineInPara = '\u2028';
				lsContextInfo.wchSectionBreak = lsContextInfo.wchUndef;
				lsContextInfo.wchNonBreakSpace = '\u00a0';
				lsContextInfo.wchNoBreak = lsContextInfo.wchUndef;
				lsContextInfo.wchColumnBreak = lsContextInfo.wchUndef;
				lsContextInfo.wchPageBreak = lsContextInfo.wchUndef;
				lsContextInfo.wchOptBreak = lsContextInfo.wchUndef;
				lsContextInfo.wchToReplace = lsContextInfo.wchUndef;
				lsContextInfo.wchReplace = lsContextInfo.wchUndef;
				IntPtr zero = IntPtr.Zero;
				IntPtr zero2 = IntPtr.Zero;
				LsErr lsErr = UnsafeNativeMethods.LoCreateContext(ref lsContextInfo, ref lscbkRedefined, out zero);
				if (lsErr != LsErr.None)
				{
					TextFormatterContext.ThrowExceptionFromLsError(SR.Get("CreateContextFailure", new object[]
					{
						lsErr
					}), lsErr);
				}
				if (TextFormatterContext._specialCharacters == null)
				{
					TextFormatterContext.SetSpecialCharacters(ref lsContextInfo);
				}
				this._ploc.Value = zero;
				GC.KeepAlive(lsContextInfo);
				LsDevRes lsDevRes;
				lsDevRes.dxpInch = (lsDevRes.dxrInch = 1440U);
				lsDevRes.dypInch = (lsDevRes.dyrInch = 1440U);
				this.SetDoc(true, true, ref lsDevRes);
				this.SetBreaking(BreakStrategies.BreakCJK);
			}
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x00103230 File Offset: 0x00102630
		internal TextPenaltyModule GetTextPenaltyModule()
		{
			Invariant.Assert(this._ploc.Value != IntPtr.Zero);
			return new TextPenaltyModule(this._ploc);
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x00103264 File Offset: 0x00102664
		[SecurityCritical]
		internal void Release()
		{
			this.CallbackException = null;
			this.Owner = null;
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x00103280 File Offset: 0x00102680
		// (set) Token: 0x0600423A RID: 16954 RVA: 0x00103298 File Offset: 0x00102698
		internal object Owner
		{
			[SecurityCritical]
			get
			{
				return this._callbacks.Owner;
			}
			[SecurityCritical]
			set
			{
				this._callbacks.Owner = value;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x001032B4 File Offset: 0x001026B4
		// (set) Token: 0x0600423C RID: 16956 RVA: 0x001032CC File Offset: 0x001026CC
		internal Exception CallbackException
		{
			[SecurityCritical]
			get
			{
				return this._callbacks.Exception;
			}
			[SecurityCritical]
			set
			{
				this._callbacks.Exception = value;
			}
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x001032E8 File Offset: 0x001026E8
		internal void EmptyBoundingBox()
		{
			this._callbacks.EmptyBoundingBox();
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x00103300 File Offset: 0x00102700
		internal Rect BoundingBox
		{
			get
			{
				return this._callbacks.BoundingBox;
			}
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x00103318 File Offset: 0x00102718
		internal void ClearIndexedGlyphRuns()
		{
			this._callbacks.ClearIndexedGlyphRuns();
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x00103330 File Offset: 0x00102730
		internal ICollection<IndexedGlyphRun> IndexedGlyphRuns
		{
			get
			{
				return this._callbacks.IndexedGlyphRuns;
			}
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x00103348 File Offset: 0x00102748
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void Destroy()
		{
			if (this._ploc.Value != IntPtr.Zero)
			{
				UnsafeNativeMethods.LoDestroyContext(this._ploc.Value);
				this._ploc.Value = IntPtr.Zero;
			}
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x00103390 File Offset: 0x00102790
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void SetBreaking(BreakStrategies breaking)
		{
			if (this._state == TextFormatterContext.State.Uninitialized || breaking != this._breaking)
			{
				Invariant.Assert(this._ploc.Value != IntPtr.Zero);
				LsErr lsErr = UnsafeNativeMethods.LoSetBreaking(this._ploc.Value, (int)breaking);
				if (lsErr != LsErr.None)
				{
					TextFormatterContext.ThrowExceptionFromLsError(SR.Get("SetBreakingFailure", new object[]
					{
						lsErr
					}), lsErr);
				}
				this._breaking = breaking;
			}
			this._state = TextFormatterContext.State.Initialized;
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x0010340C File Offset: 0x0010280C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal LsErr CreateLine(int cpFirst, int lineLength, int maxWidth, LineFlags lineFlags, IntPtr previousLineBreakRecord, out IntPtr ploline, out LsLInfo plslineInfo, out int maxDepth, out LsLineWidths lineWidths)
		{
			Invariant.Assert(this._ploc.Value != IntPtr.Zero);
			return UnsafeNativeMethods.LoCreateLine(this._ploc.Value, cpFirst, lineLength, maxWidth, (uint)lineFlags, previousLineBreakRecord, out plslineInfo, out ploline, out maxDepth, out lineWidths);
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00103454 File Offset: 0x00102854
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal LsErr CreateBreaks(int cpFirst, IntPtr previousLineBreakRecord, IntPtr ploparabreak, IntPtr ptslinevariantRestriction, ref LsBreaks lsbreaks, out int bestFitIndex)
		{
			Invariant.Assert(this._ploc.Value != IntPtr.Zero);
			return UnsafeNativeMethods.LoCreateBreaks(this._ploc.Value, cpFirst, previousLineBreakRecord, ploparabreak, ptslinevariantRestriction, ref lsbreaks, out bestFitIndex);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00103494 File Offset: 0x00102894
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal LsErr CreateParaBreakingSession(int cpFirst, int maxWidth, IntPtr previousLineBreakRecord, ref IntPtr ploparabreak, ref bool penalizedAsJustified)
		{
			Invariant.Assert(this._ploc.Value != IntPtr.Zero);
			return UnsafeNativeMethods.LoCreateParaBreakingSession(this._ploc.Value, cpFirst, maxWidth, previousLineBreakRecord, ref ploparabreak, ref penalizedAsJustified);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x001034D4 File Offset: 0x001028D4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void SetDoc(bool isDisplay, bool isReferencePresentationEqual, ref LsDevRes deviceInfo)
		{
			Invariant.Assert(this._ploc.Value != IntPtr.Zero);
			LsErr lsErr = UnsafeNativeMethods.LoSetDoc(this._ploc.Value, isDisplay ? 1 : 0, isReferencePresentationEqual ? 1 : 0, ref deviceInfo);
			if (lsErr != LsErr.None)
			{
				TextFormatterContext.ThrowExceptionFromLsError(SR.Get("SetDocFailure", new object[]
				{
					lsErr
				}), lsErr);
			}
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00103540 File Offset: 0x00102940
		[SecurityCritical]
		internal unsafe void SetTabs(int incrementalTab, LsTbd* tabStops, int tabStopCount)
		{
			Invariant.Assert(this._ploc.Value != IntPtr.Zero);
			LsErr lsErr = UnsafeNativeMethods.LoSetTabs(this._ploc.Value, incrementalTab, tabStopCount, tabStops);
			if (lsErr != LsErr.None)
			{
				TextFormatterContext.ThrowExceptionFromLsError(SR.Get("SetTabsFailure", new object[]
				{
					lsErr
				}), lsErr);
			}
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x001035A0 File Offset: 0x001029A0
		internal static void ThrowExceptionFromLsError(string message, LsErr lserr)
		{
			if (lserr == LsErr.OutOfMemory)
			{
				throw new OutOfMemoryException(message);
			}
			throw new Exception(message);
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x001035C0 File Offset: 0x001029C0
		internal static bool IsSpecialCharacter(char c)
		{
			return TextFormatterContext._specialCharacters.ContainsKey(c);
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x001035D8 File Offset: 0x001029D8
		private static void SetSpecialCharacters(ref LsContextInfo contextInfo)
		{
			Dictionary<char, bool> dictionary = new Dictionary<char, bool>();
			dictionary[contextInfo.wchHyphen] = true;
			dictionary[contextInfo.wchTab] = true;
			dictionary[contextInfo.wchPosTab] = true;
			dictionary[contextInfo.wchEndPara1] = true;
			dictionary[contextInfo.wchEndPara2] = true;
			dictionary[contextInfo.wchAltEndPara] = true;
			dictionary[contextInfo.wchEndLineInPara] = true;
			dictionary[contextInfo.wchColumnBreak] = true;
			dictionary[contextInfo.wchSectionBreak] = true;
			dictionary[contextInfo.wchPageBreak] = true;
			dictionary[contextInfo.wchNonBreakSpace] = true;
			dictionary[contextInfo.wchNonBreakHyphen] = true;
			dictionary[contextInfo.wchNonReqHyphen] = true;
			dictionary[contextInfo.wchEmDash] = true;
			dictionary[contextInfo.wchEnDash] = true;
			dictionary[contextInfo.wchEmSpace] = true;
			dictionary[contextInfo.wchEnSpace] = true;
			dictionary[contextInfo.wchNarrowSpace] = true;
			dictionary[contextInfo.wchOptBreak] = true;
			dictionary[contextInfo.wchNoBreak] = true;
			dictionary[contextInfo.wchFESpace] = true;
			dictionary[contextInfo.wchJoiner] = true;
			dictionary[contextInfo.wchNonJoiner] = true;
			dictionary[contextInfo.wchToReplace] = true;
			dictionary[contextInfo.wchReplace] = true;
			dictionary[contextInfo.wchVisiNull] = true;
			dictionary[contextInfo.wchVisiAltEndPara] = true;
			dictionary[contextInfo.wchVisiEndLineInPara] = true;
			dictionary[contextInfo.wchVisiEndPara] = true;
			dictionary[contextInfo.wchVisiSpace] = true;
			dictionary[contextInfo.wchVisiNonBreakSpace] = true;
			dictionary[contextInfo.wchVisiNonBreakHyphen] = true;
			dictionary[contextInfo.wchVisiNonReqHyphen] = true;
			dictionary[contextInfo.wchVisiTab] = true;
			dictionary[contextInfo.wchVisiPosTab] = true;
			dictionary[contextInfo.wchVisiEmSpace] = true;
			dictionary[contextInfo.wchVisiEnSpace] = true;
			dictionary[contextInfo.wchVisiNarrowSpace] = true;
			dictionary[contextInfo.wchVisiOptBreak] = true;
			dictionary[contextInfo.wchVisiNoBreak] = true;
			dictionary[contextInfo.wchVisiFESpace] = true;
			dictionary[contextInfo.wchEscAnmRun] = true;
			dictionary[contextInfo.wchPad] = true;
			dictionary.Remove(contextInfo.wchUndef);
			Interlocked.CompareExchange<Dictionary<char, bool>>(ref TextFormatterContext._specialCharacters, dictionary, null);
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x00103834 File Offset: 0x00102C34
		internal SecurityCriticalDataForSet<IntPtr> Ploc
		{
			get
			{
				return this._ploc;
			}
		}

		// Token: 0x04001824 RID: 6180
		private SecurityCriticalDataForSet<IntPtr> _ploc;

		// Token: 0x04001825 RID: 6181
		private LineServicesCallbacks _callbacks;

		// Token: 0x04001826 RID: 6182
		private TextFormatterContext.State _state;

		// Token: 0x04001827 RID: 6183
		private BreakStrategies _breaking;

		// Token: 0x04001828 RID: 6184
		private static Dictionary<char, bool> _specialCharacters;

		// Token: 0x04001829 RID: 6185
		private const uint TwipsPerInch = 1440U;

		// Token: 0x020008CF RID: 2255
		private enum State : byte
		{
			// Token: 0x04002979 RID: 10617
			Uninitialized,
			// Token: 0x0400297A RID: 10618
			Initialized
		}
	}
}
