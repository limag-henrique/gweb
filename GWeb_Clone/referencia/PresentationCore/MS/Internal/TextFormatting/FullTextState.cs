using System;
using System.Globalization;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.Generic;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000706 RID: 1798
	internal sealed class FullTextState
	{
		// Token: 0x06004D5B RID: 19803 RVA: 0x0013290C File Offset: 0x00131D0C
		internal static FullTextState Create(FormatSettings settings, int cpFirst, int finiteFormatWidth)
		{
			TextStore store = new TextStore(settings, cpFirst, 0, settings.GetFormatWidth(finiteFormatWidth));
			ParaProp pap = settings.Pap;
			TextStore markerStore = null;
			if (pap.FirstLineInParagraph && pap.TextMarkerProperties != null && pap.TextMarkerProperties.TextSource != null)
			{
				markerStore = new TextStore(new FormatSettings(settings.Formatter, pap.TextMarkerProperties.TextSource, new TextRunCacheImp(), pap, null, true, settings.TextFormattingMode, settings.IsSideways), 0, -2147483647, 1073741822);
			}
			return new FullTextState(store, markerStore, settings.IsSideways);
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x00132998 File Offset: 0x00131D98
		private FullTextState(TextStore store, TextStore markerStore, bool isSideways)
		{
			this._isSideways = isSideways;
			this._store = store;
			this._markerStore = markerStore;
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06004D5D RID: 19805 RVA: 0x001329C0 File Offset: 0x00131DC0
		// (set) Token: 0x06004D5E RID: 19806 RVA: 0x001329D4 File Offset: 0x00131DD4
		internal int CpMeasured
		{
			get
			{
				return this._cpMeasured;
			}
			set
			{
				this._cpMeasured = value;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06004D5F RID: 19807 RVA: 0x001329E8 File Offset: 0x00131DE8
		internal int LscpHyphenationLookAhead
		{
			get
			{
				return this._lscpHyphenationLookAhead;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x001329FC File Offset: 0x00131DFC
		internal TextFormattingMode TextFormattingMode
		{
			get
			{
				return this.Formatter.TextFormattingMode;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06004D61 RID: 19809 RVA: 0x00132A14 File Offset: 0x00131E14
		internal bool IsSideways
		{
			get
			{
				return this._isSideways;
			}
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x00132A28 File Offset: 0x00131E28
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe void SetTabs(TextFormatterContext context)
		{
			ParaProp pap = this._store.Pap;
			FormatSettings settings = this._store.Settings;
			int incrementalTab = TextFormatterImp.RealToIdeal(pap.DefaultIncrementalTab);
			int num = (pap.Tabs != null) ? pap.Tabs.Count : 0;
			if (this._markerStore != null)
			{
				if (pap.Tabs != null && pap.Tabs.Count > 0)
				{
					num = pap.Tabs.Count + 1;
					LsTbd[] array = new LsTbd[num];
					array[0].ur = settings.TextIndent;
					fixed (LsTbd* ptr = &array[1])
					{
						LsTbd* ptr2 = ptr;
						this.CreateLsTbds(pap, ptr2, num - 1);
						context.SetTabs(incrementalTab, ptr2 - 1, num);
					}
					return;
				}
				LsTbd lsTbd = default(LsTbd);
				lsTbd.ur = settings.TextIndent;
				context.SetTabs(incrementalTab, &lsTbd, 1);
				return;
			}
			else
			{
				if (pap.Tabs != null && pap.Tabs.Count > 0)
				{
					LsTbd[] array = new LsTbd[num];
					fixed (LsTbd* ptr3 = &array[0])
					{
						LsTbd* ptr4 = ptr3;
						this.CreateLsTbds(pap, ptr4, num);
						context.SetTabs(incrementalTab, ptr4, num);
					}
					return;
				}
				context.SetTabs(incrementalTab, null, 0);
				return;
			}
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x00132B5C File Offset: 0x00131F5C
		[SecurityCritical]
		private unsafe void CreateLsTbds(ParaProp pap, LsTbd* plsTbds, int lsTbdCount)
		{
			for (int i = 0; i < lsTbdCount; i++)
			{
				TextTabProperties textTabProperties = pap.Tabs[i];
				plsTbds[i].lskt = Convert.LsKTabFromTabAlignment(textTabProperties.Alignment);
				plsTbds[i].ur = TextFormatterImp.RealToIdeal(textTabProperties.Location);
				if (textTabProperties.TabLeader != 0)
				{
					plsTbds[i].wchTabLeader = (char)textTabProperties.TabLeader;
					this._statusFlags |= FullTextState.StatusFlags.KeepState;
				}
				plsTbds[i].wchCharTab = (char)textTabProperties.AligningCharacter;
			}
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x00132C04 File Offset: 0x00132004
		internal int GetMainTextToMarkerIdealDistance()
		{
			if (this._markerStore != null)
			{
				return Math.Min(0, TextFormatterImp.RealToIdeal(this._markerStore.Pap.TextMarkerProperties.Offset) - this._store.Settings.TextIndent);
			}
			return 0;
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x00132C4C File Offset: 0x0013204C
		internal LSRun CountText(int lscpLim, int cpFirst, out int count)
		{
			LSRun lsrun = null;
			count = 0;
			int num = lscpLim - this._store.CpFirst;
			foreach (object obj in this._store.PlsrunVector)
			{
				Span span = (Span)obj;
				if (num <= 0)
				{
					break;
				}
				Plsrun plsrun = (Plsrun)span.element;
				if (plsrun >= Plsrun.FormatAnchor)
				{
					lsrun = this._store.GetRun(plsrun);
					int length = lsrun.Length;
					if (length > 0)
					{
						if (num < span.length && length == span.length)
						{
							count += num;
							break;
						}
						count += length;
					}
				}
				num -= span.length;
			}
			count = count - cpFirst + this._store.CpFirst;
			return lsrun;
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x00132D3C File Offset: 0x0013213C
		internal int GetBreakpointInternalCp(int cp)
		{
			int num = cp - this._store.CpFirst;
			int num2 = this._store.CpFirst;
			int num3 = 0;
			SpanVector plsrunVector = this._store.PlsrunVector;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			LSRun run;
			do
			{
				Span span = plsrunVector[num4];
				Plsrun plsrun = (Plsrun)span.element;
				run = this._store.GetRun(plsrun);
				if (num == num3 && run.Type == Plsrun.Reverse)
				{
					break;
				}
				num5 = span.length;
				num6 = ((plsrun >= Plsrun.FormatAnchor) ? run.Length : 0);
				num2 += num5;
				num3 += num6;
			}
			while (++num4 < plsrunVector.Count && run.Type != Plsrun.ParaBreak && num >= num3);
			if (num3 == num || num5 == num6)
			{
				return num2 - num3 + num;
			}
			Invariant.Assert(num3 - num == num6);
			return num2 - num5;
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x00132E10 File Offset: 0x00132210
		internal bool FindNextHyphenBreak(int lscpCurrent, int lscchLim, bool isCurrentAtWordStart, ref int lscpHyphen, ref LsHyph lshyph)
		{
			lshyph = default(LsHyph);
			if (this._store.Pap.Hyphenator != null)
			{
				int num;
				int num2;
				LexicalChunk chunk = this.GetChunk(this._store.Pap.Hyphenator, lscpCurrent, lscchLim, isCurrentAtWordStart, out num, out num2);
				this._lscpHyphenationLookAhead = num + num2;
				if (!chunk.IsNoBreak)
				{
					int num3 = chunk.LSCPToCharacterIndex(lscpCurrent - num);
					int num4 = chunk.LSCPToCharacterIndex(lscpCurrent + lscchLim - num);
					if (lscchLim >= 0)
					{
						int nextBreak = chunk.Breaks.GetNextBreak(num3);
						if (nextBreak >= 0 && nextBreak > num3 && nextBreak <= num4)
						{
							lscpHyphen = chunk.CharacterIndexToLSCP(nextBreak - 1) + num;
							return true;
						}
					}
					else
					{
						int previousBreak = chunk.Breaks.GetPreviousBreak(num3);
						if (previousBreak >= 0 && previousBreak <= num3 && previousBreak > num4)
						{
							lscpHyphen = chunk.CharacterIndexToLSCP(previousBreak - 1) + num;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x00132EE8 File Offset: 0x001322E8
		private LexicalChunk GetChunk(TextLexicalService lexicalService, int lscpCurrent, int lscchLim, bool isCurrentAtWordStart, out int lscpChunk, out int lscchChunk)
		{
			int num = lscpCurrent;
			int num2 = num + lscchLim;
			int cpFirst = this._store.CpFirst;
			if (num > num2)
			{
				num = num2;
				num2 = lscpCurrent;
			}
			LexicalChunk result = default(LexicalChunk);
			CultureInfo cultureInfo;
			int num3;
			SpanVector<int> ichVector;
			char[] array = this._store.CollectRawWord(num, isCurrentAtWordStart, this._isSideways, out lscpChunk, out lscchChunk, out cultureInfo, out num3, out ichVector);
			if (array != null && num3 >= 7 && num2 < lscpChunk + lscchChunk && cultureInfo != null && lexicalService != null && lexicalService.IsCultureSupported(cultureInfo))
			{
				TextLexicalBreaks textLexicalBreaks = lexicalService.AnalyzeText(array, array.Length, cultureInfo);
				if (textLexicalBreaks != null)
				{
					result = new LexicalChunk(textLexicalBreaks, ichVector);
				}
			}
			return result;
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x00132F78 File Offset: 0x00132378
		internal TextStore StoreFrom(Plsrun plsrun)
		{
			if (!TextStore.IsMarker(plsrun))
			{
				return this._store;
			}
			return this._markerStore;
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x00132F9C File Offset: 0x0013239C
		internal TextStore StoreFrom(int lscp)
		{
			if (lscp >= 0)
			{
				return this._store;
			}
			return this._markerStore;
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06004D6B RID: 19819 RVA: 0x00132FBC File Offset: 0x001323BC
		// (set) Token: 0x06004D6C RID: 19820 RVA: 0x00132FD4 File Offset: 0x001323D4
		internal bool VerticalAdjust
		{
			get
			{
				return (this._statusFlags & FullTextState.StatusFlags.VerticalAdjust) > FullTextState.StatusFlags.None;
			}
			set
			{
				if (value)
				{
					this._statusFlags |= FullTextState.StatusFlags.VerticalAdjust;
					return;
				}
				this._statusFlags &= ~FullTextState.StatusFlags.VerticalAdjust;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06004D6D RID: 19821 RVA: 0x00133004 File Offset: 0x00132404
		// (set) Token: 0x06004D6E RID: 19822 RVA: 0x0013301C File Offset: 0x0013241C
		internal bool ForceWrap
		{
			get
			{
				return (this._statusFlags & FullTextState.StatusFlags.ForceWrap) > FullTextState.StatusFlags.None;
			}
			set
			{
				if (value)
				{
					this._statusFlags |= FullTextState.StatusFlags.ForceWrap;
					return;
				}
				this._statusFlags &= ~FullTextState.StatusFlags.ForceWrap;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x0013304C File Offset: 0x0013244C
		internal bool KeepState
		{
			get
			{
				return (this._statusFlags & FullTextState.StatusFlags.KeepState) > FullTextState.StatusFlags.None;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x00133068 File Offset: 0x00132468
		internal TextStore TextStore
		{
			get
			{
				return this._store;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06004D71 RID: 19825 RVA: 0x0013307C File Offset: 0x0013247C
		internal TextStore TextMarkerStore
		{
			get
			{
				return this._markerStore;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x00133090 File Offset: 0x00132490
		internal TextFormatterImp Formatter
		{
			get
			{
				return this._store.Settings.Formatter;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x001330B0 File Offset: 0x001324B0
		internal int FormatWidth
		{
			get
			{
				return this._store.FormatWidth;
			}
		}

		// Token: 0x040021AA RID: 8618
		private TextStore _store;

		// Token: 0x040021AB RID: 8619
		private TextStore _markerStore;

		// Token: 0x040021AC RID: 8620
		private FullTextState.StatusFlags _statusFlags;

		// Token: 0x040021AD RID: 8621
		private int _cpMeasured;

		// Token: 0x040021AE RID: 8622
		private int _lscpHyphenationLookAhead;

		// Token: 0x040021AF RID: 8623
		private bool _isSideways;

		// Token: 0x040021B0 RID: 8624
		private const int MinCchWordToHyphenate = 7;

		// Token: 0x020009DB RID: 2523
		[Flags]
		private enum StatusFlags
		{
			// Token: 0x04002E6E RID: 11886
			None = 0,
			// Token: 0x04002E6F RID: 11887
			VerticalAdjust = 1,
			// Token: 0x04002E70 RID: 11888
			ForceWrap = 2,
			// Token: 0x04002E71 RID: 11889
			KeepState = 64
		}
	}
}
