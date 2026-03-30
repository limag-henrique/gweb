using System;
using System.Security;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000702 RID: 1794
	internal sealed class FormatSettings
	{
		// Token: 0x06004D26 RID: 19750 RVA: 0x001318FC File Offset: 0x00130CFC
		internal FormatSettings(TextFormatterImp formatter, TextSource textSource, TextRunCacheImp runCache, ParaProp pap, TextLineBreak previousLineBreak, bool isSingleLineFormatting, TextFormattingMode textFormattingMode, bool isSideways)
		{
			this._isSideways = isSideways;
			this._textFormattingMode = textFormattingMode;
			this._formatter = formatter;
			this._textSource = textSource;
			this._runCache = runCache;
			this._pap = pap;
			this._digitState = new DigitState();
			this._previousLineBreak = previousLineBreak;
			this._maxLineWidth = 1073741822;
			if (isSingleLineFormatting)
			{
				this._textIndent = this._pap.Indent;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x00131970 File Offset: 0x00130D70
		internal TextFormattingMode TextFormattingMode
		{
			get
			{
				return this._textFormattingMode;
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x00131984 File Offset: 0x00130D84
		internal bool IsSideways
		{
			get
			{
				return this._isSideways;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06004D29 RID: 19753 RVA: 0x00131998 File Offset: 0x00130D98
		internal TextFormatterImp Formatter
		{
			get
			{
				return this._formatter;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x001319AC File Offset: 0x00130DAC
		internal TextSource TextSource
		{
			get
			{
				return this._textSource;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06004D2B RID: 19755 RVA: 0x001319C0 File Offset: 0x00130DC0
		internal TextLineBreak PreviousLineBreak
		{
			get
			{
				return this._previousLineBreak;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x001319D4 File Offset: 0x00130DD4
		internal ParaProp Pap
		{
			get
			{
				return this._pap;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06004D2D RID: 19757 RVA: 0x001319E8 File Offset: 0x00130DE8
		internal int MaxLineWidth
		{
			get
			{
				return this._maxLineWidth;
			}
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x001319FC File Offset: 0x00130DFC
		internal void UpdateSettingsForCurrentLine(int maxLineWidth, TextLineBreak previousLineBreak, bool isFirstLineInPara)
		{
			this._previousLineBreak = previousLineBreak;
			this._digitState = new DigitState();
			this._maxLineWidth = Math.Max(maxLineWidth, 0);
			if (isFirstLineInPara)
			{
				this._textIndent = this._pap.Indent;
				return;
			}
			this._textIndent = 0;
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x00131A44 File Offset: 0x00130E44
		internal int GetFormatWidth(int finiteFormatWidth)
		{
			if (!this._pap.Wrap)
			{
				return 1073741822;
			}
			return finiteFormatWidth;
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x00131A68 File Offset: 0x00130E68
		internal int GetFiniteFormatWidth(int paragraphWidth)
		{
			int num = (paragraphWidth <= 0) ? 1073741822 : paragraphWidth;
			num -= this._pap.ParagraphIndent;
			num = Math.Max(num, 0);
			return Math.Min(num, 1073741822);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x00131AA8 File Offset: 0x00130EA8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe CharacterBufferRange FetchTextRun(int cpFetch, int cpFirst, out TextRun textRun, out int runLength)
		{
			int num;
			textRun = this._runCache.FetchTextRun(this, cpFetch, cpFirst, out num, out runLength);
			CharacterBufferRange empty;
			switch (TextRunInfo.GetRunType(textRun))
			{
			case Plsrun.Hidden:
				empty = new CharacterBufferRange((char*)((void*)TextStore.PwchHidden), 1);
				break;
			case Plsrun.Text:
			{
				CharacterBufferReference characterBufferReference = textRun.CharacterBufferReference;
				empty = new CharacterBufferRange(characterBufferReference.CharacterBuffer, characterBufferReference.OffsetToFirstChar + num, runLength);
				break;
			}
			case Plsrun.InlineObject:
				empty = new CharacterBufferRange((char*)((void*)TextStore.PwchObjectReplacement), 1);
				break;
			case Plsrun.LineBreak:
				empty = new CharacterBufferRange((char*)((void*)TextStore.PwchLineSeparator), 1);
				break;
			case Plsrun.ParaBreak:
				empty = new CharacterBufferRange((char*)((void*)TextStore.PwchParaSeparator), 1);
				break;
			default:
				empty = CharacterBufferRange.Empty;
				break;
			}
			return empty;
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x00131B70 File Offset: 0x00130F70
		internal TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int cpLimit)
		{
			return this._runCache.GetPrecedingText(this._textSource, cpLimit);
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06004D33 RID: 19763 RVA: 0x00131B90 File Offset: 0x00130F90
		internal DigitState DigitState
		{
			get
			{
				return this._digitState;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06004D34 RID: 19764 RVA: 0x00131BA4 File Offset: 0x00130FA4
		internal int TextIndent
		{
			get
			{
				return this._textIndent;
			}
		}

		// Token: 0x04002188 RID: 8584
		private TextFormatterImp _formatter;

		// Token: 0x04002189 RID: 8585
		private TextSource _textSource;

		// Token: 0x0400218A RID: 8586
		private TextRunCacheImp _runCache;

		// Token: 0x0400218B RID: 8587
		private ParaProp _pap;

		// Token: 0x0400218C RID: 8588
		private DigitState _digitState;

		// Token: 0x0400218D RID: 8589
		private TextLineBreak _previousLineBreak;

		// Token: 0x0400218E RID: 8590
		private int _maxLineWidth;

		// Token: 0x0400218F RID: 8591
		private int _textIndent;

		// Token: 0x04002190 RID: 8592
		private TextFormattingMode _textFormattingMode;

		// Token: 0x04002191 RID: 8593
		private bool _isSideways;
	}
}
