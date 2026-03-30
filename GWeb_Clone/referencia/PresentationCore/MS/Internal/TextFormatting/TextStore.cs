using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.Generic;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200075F RID: 1887
	internal class TextStore
	{
		// Token: 0x06004F5F RID: 20319 RVA: 0x0013BE38 File Offset: 0x0013B238
		static TextStore()
		{
			EscStringInfo escStringInfo = default(EscStringInfo);
			UnsafeNativeMethods.LoGetEscString(ref escStringInfo);
			TextStore.ControlRuns = new LSRun[3];
			TextStore.ControlRuns[0] = new LSRun(Plsrun.CloseAnchor, escStringInfo.szObjectTerminator);
			TextStore.ControlRuns[1] = new LSRun(Plsrun.Reverse, escStringInfo.szObjectReplacement);
			TextStore.ControlRuns[2] = new LSRun(Plsrun.FakeLineBreak, escStringInfo.szLineSeparator);
			TextStore.PwchNbsp = escStringInfo.szNbsp;
			TextStore.PwchHidden = escStringInfo.szHidden;
			TextStore.PwchParaSeparator = escStringInfo.szParaSeparator;
			TextStore.PwchLineSeparator = escStringInfo.szLineSeparator;
			TextStore.PwchObjectReplacement = escStringInfo.szObjectReplacement;
			TextStore.PwchObjectTerminator = escStringInfo.szObjectTerminator;
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0013BEDC File Offset: 0x0013B2DC
		public TextStore(FormatSettings settings, int cpFirst, int lscpFirstValue, int formatWidth)
		{
			this._settings = settings;
			this._formatWidth = formatWidth;
			this._cpFirst = cpFirst;
			this._lscpFirstValue = lscpFirstValue;
			this._lsrunList = new ArrayList(2);
			this._plsrunVector = new SpanVector(null);
			this._plsrunVectorLatestPosition = default(SpanPosition);
			TextLineBreak previousLineBreak = settings.PreviousLineBreak;
			if (previousLineBreak != null && previousLineBreak.TextModifierScope != null)
			{
				this._modifierScope = previousLineBreak.TextModifierScope.CloneStack();
				this._bidiState = new BidiState(this._settings, this._cpFirst, this._modifierScope);
			}
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0013BF70 File Offset: 0x0013B370
		internal LSRun FetchLSRun(int lscpFetch, TextFormattingMode textFormattingMode, bool isSideways, out Plsrun plsrun, out int lsrunOffset, out int lsrunLength)
		{
			lscpFetch -= this._lscpFirstValue;
			Invariant.Assert(lscpFetch >= this._cpFirst);
			if (this._cpFirst + this._lscchUpTo <= lscpFetch)
			{
				ushort num = 0;
				ushort num2 = 0;
				int num3 = 0;
				int num4 = this._cchUpTo;
				int num5 = 0;
				int num6 = 0;
				SpanVector spanVector = new SpanVector(null);
				SpanVector textEffectsVector = new SpanVector(null);
				byte[] array = null;
				int num7 = this.GetLastLevel();
				do
				{
					TextRunInfo textRunInfo = this.FetchTextRun(this._cpFirst + num4);
					if (textRunInfo != null)
					{
						if (this._bidiState == null && (this.IsDirectionalModifier(textRunInfo.TextRun as TextModifier) || this.IsEndOfDirectionalModifier(textRunInfo)))
						{
							this._bidiState = new BidiState(this._settings, this._cpFirst);
						}
						int num8 = textRunInfo.StringLength;
						if (textRunInfo.TextRun is ITextSymbols)
						{
							ushort mask;
							ushort num9;
							if (!textRunInfo.IsSymbol)
							{
								mask = 524;
								num9 = 2;
							}
							else
							{
								mask = 1032;
								num9 = 0;
							}
							ushort num10;
							num8 = Classification.AdvanceUntilUTF16(textRunInfo.CharacterBuffer, textRunInfo.OffsetToFirstChar, textRunInfo.Length, mask, out num10);
							num10 &= 65019;
							if (num8 <= 0)
							{
								textRunInfo = this.CreateSpecialRunFromTextContent(textRunInfo, num4);
								num8 = textRunInfo.StringLength;
								num10 = textRunInfo.CharacterAttributeFlags;
							}
							else if (num8 != textRunInfo.Length)
							{
								textRunInfo.Length = num8;
							}
							TextRunInfo textRunInfo2 = textRunInfo;
							textRunInfo2.CharacterAttributeFlags |= num10;
							num |= num10;
							num2 |= (num10 & num9);
							num6 += num8;
						}
						this._accNominalWidthSoFar += textRunInfo.GetRoughWidth(TextFormatterImp.ToIdeal);
						spanVector.SetReference(num5, num8, textRunInfo);
						TextEffectCollection textEffectCollection = (textRunInfo.Properties != null) ? textRunInfo.Properties.TextEffects : null;
						if (textEffectCollection != null && textEffectCollection.Count != 0)
						{
							this.SetTextEffectsVector(textEffectsVector, num5, textRunInfo, textEffectCollection);
						}
						num5 += num8;
						num4 += textRunInfo.Length;
						if (this._accNominalWidthSoFar < this._formatWidth && !textRunInfo.IsEndOfLine && !TextStore.IsNewline(num) && this._accTextLengthSoFar + num6 <= 9600)
						{
							continue;
						}
					}
					if (num7 > 0 || num2 != 0 || this._bidiState != null)
					{
						num3 = this.BidiAnalyze(spanVector, num5, out array);
						if (num3 == 0 && this._accTextLengthSoFar + num6 >= 9600)
						{
							num3 = num5;
							array = null;
						}
					}
					else
					{
						num3 = num5;
					}
				}
				while (num3 <= 0);
				bool flag = this.IsForceBreakRequired(spanVector, ref num3);
				if (array == null)
				{
					this.CreateLSRunsUniformBidiLevel(spanVector, textEffectsVector, this._cchUpTo, 0, num3, 0, textFormattingMode, isSideways, ref num7);
				}
				else
				{
					int cchUpTo = this._cchUpTo;
					int num11;
					for (int i = 0; i < num3; i += num11)
					{
						num11 = 1;
						int num12 = (int)array[i];
						while (i + num11 < num3 && (int)array[i + num11] == num12)
						{
							num11++;
						}
						this.CreateLSRunsUniformBidiLevel(spanVector, textEffectsVector, cchUpTo, i, num11, num12, textFormattingMode, isSideways, ref num7);
					}
				}
				if (flag)
				{
					if (num7 != 0)
					{
						num7 = this.CreateReverseLSRuns(this.BaseBidiLevel, num7);
					}
					this._plsrunVectorLatestPosition = this._plsrunVector.SetValue(this._lscchUpTo, 1, Plsrun.FakeLineBreak, this._plsrunVectorLatestPosition);
					this._lscchUpTo++;
				}
			}
			return this.GrabLSRun(lscpFetch, out plsrun, out lsrunOffset, out lsrunLength);
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x0013C28C File Offset: 0x0013B68C
		internal TextRunInfo FetchTextRun(int cpFetch)
		{
			TextRun textRun;
			int textRunLength;
			CharacterBufferRange charBufferRange = this._settings.FetchTextRun(cpFetch, this._cpFirst, out textRun, out textRunLength);
			CultureInfo digitCulture = null;
			bool contextualSubstitution = false;
			bool flag = false;
			Plsrun runType = TextRunInfo.GetRunType(textRun);
			if (runType == Plsrun.Text)
			{
				TextRunProperties properties = textRun.Properties;
				flag = properties.Typeface.Symbol;
				if (!flag)
				{
					this._settings.DigitState.SetTextRunProperties(properties);
					digitCulture = this._settings.DigitState.DigitCulture;
					contextualSubstitution = this._settings.DigitState.Contextual;
				}
			}
			TextModifierScope modifierScope = this._modifierScope;
			TextModifier textModifier = textRun as TextModifier;
			if (textModifier != null)
			{
				this._modifierScope = new TextModifierScope(this._modifierScope, textModifier, cpFetch);
				modifierScope = this._modifierScope;
			}
			else if (this._modifierScope != null && textRun is TextEndOfSegment)
			{
				this._modifierScope = this._modifierScope.ParentScope;
			}
			return new TextRunInfo(charBufferRange, textRunLength, cpFetch - this._cpFirst, textRun, runType, 0, digitCulture, contextualSubstitution, flag, modifierScope);
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x0013C380 File Offset: 0x0013B780
		private void SetTextEffectsVector(SpanVector textEffectsVector, int ich, TextRunInfo runInfo, TextEffectCollection textEffects)
		{
			int num = this._cpFirst + this._cchUpTo + ich;
			int num2 = num - this._settings.TextSource.GetTextEffectCharacterIndexFromTextSourceCharacterIndex(num);
			int count = textEffects.Count;
			TextStore.TextEffectBoundary[] array = new TextStore.TextEffectBoundary[count * 2];
			for (int i = 0; i < count; i++)
			{
				TextEffect textEffect = textEffects[i];
				array[2 * i] = new TextStore.TextEffectBoundary(textEffect.PositionStart, true);
				array[2 * i + 1] = new TextStore.TextEffectBoundary(textEffect.PositionStart + textEffect.PositionCount, false);
			}
			Array.Sort<TextStore.TextEffectBoundary>(array);
			int num3 = Math.Max(num - num2, array[0].Position);
			int num4 = Math.Min(num - num2 + runInfo.Length, array[array.Length - 1].Position);
			int num5 = 0;
			int num6 = num3;
			int num7 = 0;
			while (num7 < array.Length && num6 < num4)
			{
				if (num6 < array[num7].Position && num5 > 0)
				{
					int num8 = Math.Min(array[num7].Position, num4);
					IList<TextEffect> list = new TextEffect[num5];
					int num9 = 0;
					for (int j = 0; j < count; j++)
					{
						TextEffect textEffect2 = textEffects[j];
						if (num6 >= textEffect2.PositionStart && num6 < textEffect2.PositionStart + textEffect2.PositionCount)
						{
							list[num9++] = textEffect2;
						}
					}
					Invariant.Assert(num9 == num5);
					textEffectsVector.SetReference(num6 + num2 - this._cchUpTo - this._cpFirst, num8 - num6, list);
					num6 = num8;
				}
				num5 += (array[num7].IsStart ? 1 : -1);
				if (num5 == 0 && num7 < array.Length - 1)
				{
					Invariant.Assert(array[num7 + 1].IsStart);
					num6 = Math.Max(num6, array[num7 + 1].Position);
				}
				num7++;
			}
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x0013C570 File Offset: 0x0013B970
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe TextRunInfo CreateSpecialRunFromTextContent(TextRunInfo runInfo, int cchFetched)
		{
			CharacterBuffer characterBuffer = runInfo.CharacterBuffer;
			int offsetToFirstChar = runInfo.OffsetToFirstChar;
			char c = characterBuffer[offsetToFirstChar];
			ushort flags = Classification.CharAttributeOf((int)Classification.GetUnicodeClassUTF16(c)).Flags;
			if ((flags & 4) != 0)
			{
				int num = 1;
				if (c == '\r')
				{
					if (runInfo.Length > 1)
					{
						num += ((characterBuffer[offsetToFirstChar + 1] == '\n') ? 1 : 0);
					}
					else
					{
						TextRunInfo textRunInfo = this.FetchTextRun(this._cpFirst + cchFetched + 1);
						if (textRunInfo != null && textRunInfo.TextRun is ITextSymbols)
						{
							num += ((textRunInfo.CharacterBuffer[textRunInfo.OffsetToFirstChar] == '\n') ? 1 : 0);
						}
					}
				}
				runInfo = new TextRunInfo(new CharacterBufferRange((char*)((void*)TextStore.PwchLineSeparator), 1), num, runInfo.OffsetToFirstCp, runInfo.TextRun, Plsrun.LineBreak, flags, null, false, false, runInfo.TextModifierScope);
			}
			else if ((flags & 512) != 0)
			{
				runInfo = new TextRunInfo(new CharacterBufferRange((char*)((void*)TextStore.PwchParaSeparator), 1), 1, runInfo.OffsetToFirstCp, runInfo.TextRun, Plsrun.ParaBreak, flags, null, false, false, runInfo.TextModifierScope);
			}
			else
			{
				Invariant.Assert((flags & 8) > 0);
				runInfo = new TextRunInfo(new CharacterBufferRange((char*)((void*)TextStore.PwchNbsp), 1), 1, runInfo.OffsetToFirstCp, runInfo.TextRun, runInfo.Plsrun, flags, null, false, false, runInfo.TextModifierScope);
			}
			return runInfo;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x0013C6C4 File Offset: 0x0013BAC4
		private LSRun GrabLSRun(int lscpFetch, out Plsrun plsrun, out int lsrunOffset, out int lsrunLength)
		{
			int num = lscpFetch - this._cpFirst;
			SpanRider spanRider = new SpanRider(this._plsrunVector, this._plsrunVectorLatestPosition, num);
			this._plsrunVectorLatestPosition = spanRider.SpanPosition;
			plsrun = (Plsrun)spanRider.CurrentElement;
			LSRun result;
			if (plsrun < Plsrun.FormatAnchor)
			{
				result = TextStore.ControlRuns[(int)plsrun];
				lsrunOffset = 0;
			}
			else
			{
				result = (LSRun)this._lsrunList[(int)(TextStore.ToIndex(plsrun) - Plsrun.FormatAnchor)];
				lsrunOffset = num - spanRider.CurrentSpanStart;
			}
			if (this._lscpFirstValue != 0)
			{
				plsrun = TextStore.MakePlsrunMarker(plsrun);
			}
			lsrunLength = spanRider.Length;
			return result;
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x0013C760 File Offset: 0x0013BB60
		private int GetLastLevel()
		{
			if (this._lscchUpTo > 0)
			{
				SpanRider spanRider = new SpanRider(this._plsrunVector, this._plsrunVectorLatestPosition, this._lscchUpTo - 1);
				this._plsrunVectorLatestPosition = spanRider.SpanPosition;
				return (int)this.GetRun((Plsrun)spanRider.CurrentElement).BidiLevel;
			}
			return this.BaseBidiLevel;
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x0013C7BC File Offset: 0x0013BBBC
		private int BaseBidiLevel
		{
			get
			{
				if (!this._settings.Pap.RightToLeft)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x0013C7E0 File Offset: 0x0013BBE0
		private int BidiAnalyze(SpanVector runInfoVector, int stringLength, out byte[] bidiLevels)
		{
			SpanRider spanRider = new SpanRider(runInfoVector);
			CharacterBuffer charBuffer;
			int num;
			if (spanRider.Length >= stringLength)
			{
				TextRunInfo textRunInfo = (TextRunInfo)spanRider.CurrentElement;
				if (!textRunInfo.IsSymbol)
				{
					charBuffer = textRunInfo.CharacterBuffer;
					num = textRunInfo.OffsetToFirstChar;
				}
				else
				{
					charBuffer = new StringCharacterBuffer(new string('A', stringLength));
					num = 0;
				}
			}
			else
			{
				int i = 0;
				StringBuilder stringBuilder = new StringBuilder(stringLength);
				while (i < stringLength)
				{
					spanRider.At(i);
					int length = spanRider.Length;
					TextRunInfo textRunInfo2 = (TextRunInfo)spanRider.CurrentElement;
					if (!textRunInfo2.IsSymbol)
					{
						textRunInfo2.CharacterBuffer.AppendToStringBuilder(stringBuilder, textRunInfo2.OffsetToFirstChar, length);
					}
					else
					{
						stringBuilder.Append('A', length);
					}
					i += length;
				}
				charBuffer = new StringCharacterBuffer(stringBuilder.ToString());
				num = 0;
			}
			if (this._bidiState == null)
			{
				this._bidiState = new BidiState(this._settings, this._cpFirst);
			}
			bidiLevels = new byte[stringLength];
			DirectionClass[] array = new DirectionClass[stringLength];
			int num2 = 0;
			for (int j = 0; j < runInfoVector.Count; j++)
			{
				int num3 = 0;
				TextRunInfo textRunInfo3 = (TextRunInfo)runInfoVector[j].element;
				TextModifier textModifier = textRunInfo3.TextRun as TextModifier;
				if (this.IsDirectionalModifier(textModifier))
				{
					bidiLevels[num2] = this.AnalyzeDirectionalModifier(this._bidiState, textModifier.FlowDirection);
					num3 = 1;
				}
				else
				{
					if (!this.IsEndOfDirectionalModifier(textRunInfo3))
					{
						int num4 = num2;
						for (;;)
						{
							CultureInfo specificCulture = CultureMapper.GetSpecificCulture((textRunInfo3.Properties == null) ? null : textRunInfo3.Properties.CultureInfo);
							DirectionClass europeanNumberClassOverride = this._bidiState.GetEuropeanNumberClassOverride(specificCulture);
							for (int k = 0; k < runInfoVector[j].length; k++)
							{
								array[num4 + k] = europeanNumberClassOverride;
							}
							num4 += runInfoVector[j].length;
							if (++j >= runInfoVector.Count)
							{
								break;
							}
							textRunInfo3 = (TextRunInfo)runInfoVector[j].element;
							if (textRunInfo3.Plsrun == Plsrun.Hidden && (this.IsDirectionalModifier(textRunInfo3.TextRun as TextModifier) || this.IsEndOfDirectionalModifier(textRunInfo3)))
							{
								goto IL_222;
							}
						}
						IL_226:
						Bidi.Flags flags = (j < runInfoVector.Count) ? ((Bidi.Flags)200U) : ((Bidi.Flags)216U);
						Bidi.BidiAnalyzeInternal(charBuffer, num + num2, num4 - num2, 0, flags, this._bidiState, new PartialArray<byte>(bidiLevels, num2, num4 - num2), new PartialArray<DirectionClass>(array, num2, num4 - num2), out num3);
						Invariant.Assert(num3 == num4 - num2 || (flags & Bidi.Flags.IncompleteText) > Bidi.Flags.DirectionLeftToRight);
						goto IL_28E;
						IL_222:
						j--;
						goto IL_226;
					}
					bidiLevels[num2] = this.AnalyzeEndOfDirectionalModifier(this._bidiState);
					num3 = 1;
				}
				IL_28E:
				num2 += num3;
			}
			Invariant.Assert(num2 <= bidiLevels.Length);
			return num2;
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0013CAA0 File Offset: 0x0013BEA0
		private byte AnalyzeDirectionalModifier(BidiState state, FlowDirection flowDirection)
		{
			bool pushToGreaterEven = flowDirection == FlowDirection.LeftToRight;
			ulong levelStack = state.LevelStack;
			byte maximumLevel = Bidi.BidiStack.GetMaximumLevel(levelStack);
			byte b;
			if (!Bidi.BidiStack.Push(ref levelStack, pushToGreaterEven, out b))
			{
				ushort overflow = state.Overflow;
				state.Overflow = overflow + 1;
			}
			state.LevelStack = levelStack;
			state.SetLastDirectionClassesAtLevelChange();
			return maximumLevel;
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0013CAEC File Offset: 0x0013BEEC
		private byte AnalyzeEndOfDirectionalModifier(BidiState state)
		{
			if (state.Overflow > 0)
			{
				ushort overflow = state.Overflow;
				state.Overflow = overflow - 1;
				return state.CurrentLevel;
			}
			ulong levelStack = state.LevelStack;
			byte result;
			bool condition = Bidi.BidiStack.Pop(ref levelStack, out result);
			Invariant.Assert(condition);
			state.LevelStack = levelStack;
			state.SetLastDirectionClassesAtLevelChange();
			return result;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0013CB40 File Offset: 0x0013BF40
		private bool IsEndOfDirectionalModifier(TextRunInfo runInfo)
		{
			return runInfo.TextModifierScope != null && runInfo.TextModifierScope.TextModifier.HasDirectionalEmbedding && runInfo.TextRun is TextEndOfSegment;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x0013CB78 File Offset: 0x0013BF78
		private bool IsDirectionalModifier(TextModifier modifier)
		{
			return modifier != null && modifier.HasDirectionalEmbedding;
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0013CB90 File Offset: 0x0013BF90
		internal bool InsertFakeLineBreak(int cpLimit)
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < this._plsrunVector.Count)
			{
				Span span = this._plsrunVector[i];
				Plsrun plsrun = (Plsrun)span.element;
				if (plsrun >= Plsrun.FormatAnchor)
				{
					LSRun run = this.GetRun(plsrun);
					if (num + run.Length >= cpLimit)
					{
						this._plsrunVector.Delete(i + 1, this._plsrunVector.Count - (i + 1), ref this._plsrunVectorLatestPosition);
						if (run.Type == Plsrun.Text && num + run.Length > cpLimit)
						{
							run.Truncate(cpLimit - num);
							span.length = run.Length;
						}
						this._lscchUpTo = num2 + run.Length;
						this.CreateReverseLSRuns(this.BaseBidiLevel, (int)run.BidiLevel);
						this._plsrunVectorLatestPosition = this._plsrunVector.SetValue(this._lscchUpTo, 1, Plsrun.FakeLineBreak, this._plsrunVectorLatestPosition);
						this._lscchUpTo++;
						return true;
					}
					num += run.Length;
				}
				num2 += span.length;
				i++;
			}
			return false;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x0013CCB0 File Offset: 0x0013C0B0
		private bool IsForceBreakRequired(SpanVector runInfoVector, ref int cchToAdd)
		{
			bool result = false;
			int num = 0;
			int num2 = 0;
			while (num2 < runInfoVector.Count && num < cchToAdd)
			{
				Span span = runInfoVector[num2];
				TextRunInfo textRunInfo = (TextRunInfo)span.element;
				int num3 = Math.Min(span.length, cchToAdd - num);
				if (textRunInfo.Plsrun == Plsrun.Text && !TextStore.IsNewline(textRunInfo.CharacterAttributeFlags))
				{
					if (this._accTextLengthSoFar + num3 <= 9600)
					{
						this._accTextLengthSoFar += num3;
					}
					else
					{
						num3 = 9600 - this._accTextLengthSoFar;
						this._accTextLengthSoFar = 9600;
						cchToAdd = num + num3;
						result = true;
					}
				}
				num += num3;
				num2++;
			}
			return result;
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0013CD60 File Offset: 0x0013C160
		private TextStore.NumberContext GetNumberContext(TextModifierScope scope)
		{
			int i = this._cpFirst + this._cchUpTo;
			int num = this._cpNumberContext;
			TextStore.NumberContext numberContext = this._numberContext;
			while (scope != null)
			{
				if (scope.TextModifier.HasDirectionalEmbedding)
				{
					int textSourceCharacterIndex = scope.TextSourceCharacterIndex;
					if (textSourceCharacterIndex >= this._cpNumberContext)
					{
						num = textSourceCharacterIndex;
						numberContext = TextStore.NumberContext.Unknown;
						break;
					}
					break;
				}
				else
				{
					scope = scope.ParentScope;
				}
			}
			bool flag = (scope != null) ? (scope.TextModifier.FlowDirection == FlowDirection.RightToLeft) : this.Pap.RightToLeft;
			while (i > num)
			{
				TextSpan<CultureSpecificCharacterBufferRange> precedingText = this._settings.GetPrecedingText(i);
				if (precedingText.Length <= 0)
				{
					break;
				}
				CharacterBufferRange characterBufferRange = precedingText.Value.CharacterBufferRange;
				if (!characterBufferRange.IsEmpty)
				{
					CharacterBuffer characterBuffer = characterBufferRange.CharacterBuffer;
					int num2 = characterBufferRange.OffsetToFirstChar + characterBufferRange.Length;
					int num3 = num2 - Math.Min(characterBufferRange.Length, i - num);
					int j = num2 - 1;
					while (j >= num3)
					{
						char codepoint = characterBuffer[j];
						CharacterAttribute characterAttribute = Classification.CharAttributeOf((int)Classification.GetUnicodeClassUTF16(codepoint));
						ushort num4 = characterAttribute.Flags & 2052;
						if (num4 != 0)
						{
							if ((num4 & 2048) != 0)
							{
								if (characterAttribute.Script != 1 && characterAttribute.Script != 49)
								{
									return TextStore.NumberContext.European | TextStore.NumberContext.FromLetter;
								}
								return TextStore.NumberContext.Arabic | TextStore.NumberContext.FromLetter;
							}
							else
							{
								if (!flag)
								{
									return TextStore.NumberContext.European | TextStore.NumberContext.FromFlowDirection;
								}
								return TextStore.NumberContext.Arabic | TextStore.NumberContext.FromFlowDirection;
							}
						}
						else
						{
							j--;
						}
					}
				}
				i -= precedingText.Length;
			}
			if (i <= num && (numberContext & TextStore.NumberContext.FromLetter) != TextStore.NumberContext.Unknown)
			{
				return numberContext;
			}
			if (!flag)
			{
				return TextStore.NumberContext.European | TextStore.NumberContext.FromFlowDirection;
			}
			return TextStore.NumberContext.Arabic | TextStore.NumberContext.FromFlowDirection;
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x0013CED4 File Offset: 0x0013C2D4
		private void CreateLSRunsUniformBidiLevel(SpanVector runInfoVector, SpanVector textEffectsVector, int runInfoFirstCp, int ichUniform, int cchUniform, int uniformBidiLevel, TextFormattingMode textFormattingMode, bool isSideways, ref int lastBidiLevel)
		{
			int i = 0;
			SpanRider spanRider = new SpanRider(runInfoVector);
			SpanRider spanRider2 = new SpanRider(textEffectsVector);
			while (i < cchUniform)
			{
				spanRider.At(ichUniform + i);
				spanRider2.At(ichUniform + i);
				int num = Math.Min(spanRider.Length, spanRider2.Length);
				int num2 = Math.Min(i + num, cchUniform);
				TextRunInfo textRunInfo = (TextRunInfo)spanRider.CurrentElement;
				IList<TextEffect> textEffects = (IList<TextEffect>)spanRider2.CurrentElement;
				CultureInfo digitCulture = null;
				TextStore.NumberContext numberContext = TextStore.NumberContext.Unknown;
				int num4;
				if ((textRunInfo.CharacterAttributeFlags & 256) != 0)
				{
					if (!textRunInfo.ContextualSubstitution)
					{
						digitCulture = textRunInfo.DigitCulture;
					}
					else
					{
						TextStore.NumberContext numberContext2 = TextStore.NumberContext.Unknown;
						CharacterBuffer characterBuffer = textRunInfo.CharacterBuffer;
						int num3 = ichUniform - spanRider.CurrentSpanStart + textRunInfo.OffsetToFirstChar;
						for (int j = i; j < num2; j++)
						{
							char codepoint = characterBuffer[j + num3];
							CharacterAttribute characterAttribute = Classification.CharAttributeOf((int)Classification.GetUnicodeClassUTF16(codepoint));
							if ((characterAttribute.Flags & 256) != 0)
							{
								if (numberContext == TextStore.NumberContext.Unknown)
								{
									numberContext = this.GetNumberContext(textRunInfo.TextModifierScope);
								}
								if ((numberContext2 & TextStore.NumberContext.Mask) != (numberContext & TextStore.NumberContext.Mask))
								{
									if (numberContext2 != TextStore.NumberContext.Unknown)
									{
										this.CreateLSRuns(textRunInfo, textEffects, digitCulture, ichUniform + i - spanRider.CurrentSpanStart, j - i, uniformBidiLevel, textFormattingMode, isSideways, ref lastBidiLevel, out num4);
										this._cchUpTo += num4;
										i = j;
									}
									digitCulture = (((numberContext & TextStore.NumberContext.Mask) == TextStore.NumberContext.Arabic) ? textRunInfo.DigitCulture : null);
									numberContext2 = numberContext;
								}
							}
							else if ((characterAttribute.Flags & 2048) != 0)
							{
								numberContext = ((characterAttribute.Script == 1 || characterAttribute.Script == 49) ? (TextStore.NumberContext.Arabic | TextStore.NumberContext.FromLetter) : (TextStore.NumberContext.European | TextStore.NumberContext.FromLetter));
							}
						}
					}
				}
				this.CreateLSRuns(textRunInfo, textEffects, digitCulture, ichUniform + i - spanRider.CurrentSpanStart, num2 - i, uniformBidiLevel, textFormattingMode, isSideways, ref lastBidiLevel, out num4);
				this._cchUpTo += num4;
				i = num2;
				if (numberContext != TextStore.NumberContext.Unknown)
				{
					this._numberContext = numberContext;
					this._cpNumberContext = this._cpFirst + this._cchUpTo;
				}
			}
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x0013D0C4 File Offset: 0x0013C4C4
		private int CreateReverseLSRuns(int currentBidiLevel, int lastBidiLevel)
		{
			int num = currentBidiLevel - lastBidiLevel;
			Plsrun plsrun;
			if (num > 0)
			{
				plsrun = Plsrun.Reverse;
			}
			else
			{
				plsrun = Plsrun.CloseAnchor;
				num = -num;
			}
			for (int i = 0; i < num; i++)
			{
				this._plsrunVectorLatestPosition = this._plsrunVector.SetValue(this._lscchUpTo, 1, plsrun, this._plsrunVectorLatestPosition);
				this._lscchUpTo++;
			}
			return currentBidiLevel;
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0013D124 File Offset: 0x0013C524
		private void CreateLSRuns(TextRunInfo runInfo, IList<TextEffect> textEffects, CultureInfo digitCulture, int offsetToFirstChar, int stringLength, int uniformBidiLevel, TextFormattingMode textFormattingMode, bool isSideways, ref int lastBidiLevel, out int textRunLength)
		{
			LSRun lsrun = null;
			int num = 0;
			textRunLength = 0;
			switch (runInfo.Plsrun)
			{
			case Plsrun.Hidden:
				num = runInfo.Length - offsetToFirstChar;
				textRunLength = num;
				lsrun = new LSRun(runInfo, Plsrun.Hidden, TextStore.PwchHidden, textRunLength, runInfo.OffsetToFirstCp, (byte)uniformBidiLevel);
				break;
			case Plsrun.Text:
			{
				ushort characterAttributeFlags = runInfo.CharacterAttributeFlags;
				Invariant.Assert(!TextStore.IsNewline(characterAttributeFlags));
				if ((characterAttributeFlags & 8) != 0)
				{
					lsrun = new LSRun(runInfo, Plsrun.FormatAnchor, TextStore.PwchNbsp, 1, runInfo.OffsetToFirstCp, (byte)uniformBidiLevel);
					num = (textRunLength = lsrun.StringLength);
				}
				else
				{
					num = stringLength;
					textRunLength = stringLength;
					this.CreateTextLSRuns(runInfo, textEffects, digitCulture, offsetToFirstChar, stringLength, uniformBidiLevel, textFormattingMode, isSideways, ref lastBidiLevel);
				}
				break;
			}
			case Plsrun.InlineObject:
			{
				double toIdeal = TextFormatterImp.ToIdeal;
				lsrun = new LSRun(runInfo, textEffects, Plsrun.InlineObject, runInfo.OffsetToFirstCp, runInfo.Length, (int)Math.Round(toIdeal * runInfo.TextRun.Properties.FontRenderingEmSize), 0, new CharacterBufferRange(runInfo.CharacterBuffer, 0, stringLength), null, toIdeal, (byte)uniformBidiLevel);
				num = stringLength;
				textRunLength = runInfo.Length;
				break;
			}
			case Plsrun.LineBreak:
				uniformBidiLevel = (this.Pap.RightToLeft ? 1 : 0);
				lsrun = this.CreateLineBreakLSRun(runInfo, stringLength, out num, out textRunLength);
				break;
			case Plsrun.ParaBreak:
				lsrun = this.CreateLineBreakLSRun(runInfo, stringLength, out num, out textRunLength);
				break;
			}
			if (lsrun != null)
			{
				if (lastBidiLevel != uniformBidiLevel)
				{
					lastBidiLevel = this.CreateReverseLSRuns(uniformBidiLevel, lastBidiLevel);
				}
				this._plsrunVectorLatestPosition = this._plsrunVector.SetValue(this._lscchUpTo, num, this.AddLSRun(lsrun), this._plsrunVectorLatestPosition);
				this._lscchUpTo += num;
			}
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x0013D2C8 File Offset: 0x0013C6C8
		private void CreateTextLSRuns(TextRunInfo runInfo, IList<TextEffect> textEffects, CultureInfo digitCulture, int offsetToFirstChar, int stringLength, int uniformBidiLevel, TextFormattingMode textFormattingMode, bool isSideways, ref int lastBidiLevel)
		{
			ICollection<TextShapeableSymbols> collection = null;
			ITextSymbols textSymbols = runInfo.TextRun as ITextSymbols;
			if (textSymbols != null)
			{
				bool isRightToLeftParagraph = (runInfo.TextModifierScope != null) ? (runInfo.TextModifierScope.TextModifier.FlowDirection == FlowDirection.RightToLeft) : this._settings.Pap.RightToLeft;
				collection = textSymbols.GetTextShapeableSymbols(this._settings.Formatter.GlyphingCache, new CharacterBufferReference(runInfo.CharacterBuffer, runInfo.OffsetToFirstChar + offsetToFirstChar), stringLength, (uniformBidiLevel & 1) != 0, isRightToLeftParagraph, digitCulture, runInfo.TextModifierScope, textFormattingMode, isSideways);
			}
			else
			{
				TextShapeableSymbols textShapeableSymbols = runInfo.TextRun as TextShapeableSymbols;
				if (textShapeableSymbols != null)
				{
					collection = new TextShapeableSymbols[]
					{
						textShapeableSymbols
					};
				}
			}
			if (collection == null)
			{
				throw new NotSupportedException();
			}
			double toIdeal = TextFormatterImp.ToIdeal;
			int num = 0;
			foreach (TextShapeableSymbols textShapeableSymbols2 in collection)
			{
				int length = textShapeableSymbols2.Length;
				LSRun lsrun = new LSRun(runInfo, textEffects, Plsrun.Text, runInfo.OffsetToFirstCp + offsetToFirstChar + num, length, (int)Math.Round(toIdeal * runInfo.TextRun.Properties.FontRenderingEmSize), runInfo.CharacterAttributeFlags, new CharacterBufferRange(runInfo.CharacterBuffer, runInfo.OffsetToFirstChar + offsetToFirstChar + num, length), textShapeableSymbols2, toIdeal, (byte)uniformBidiLevel);
				if (uniformBidiLevel != lastBidiLevel)
				{
					lastBidiLevel = this.CreateReverseLSRuns(uniformBidiLevel, lastBidiLevel);
				}
				this._plsrunVectorLatestPosition = this._plsrunVector.SetValue(this._lscchUpTo, length, this.AddLSRun(lsrun), this._plsrunVectorLatestPosition);
				this._lscchUpTo += length;
				num += length;
			}
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x0013D484 File Offset: 0x0013C884
		private LSRun CreateLineBreakLSRun(TextRunInfo runInfo, int stringLength, out int lsrunLength, out int textRunLength)
		{
			lsrunLength = stringLength;
			textRunLength = runInfo.Length;
			return new LSRun(runInfo, null, runInfo.Plsrun, runInfo.OffsetToFirstCp, runInfo.Length, 0, runInfo.CharacterAttributeFlags, new CharacterBufferRange(runInfo.CharacterBuffer, runInfo.OffsetToFirstChar, stringLength), null, TextFormatterImp.ToIdeal, this.Pap.RightToLeft ? 1 : 0);
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x0013D4E8 File Offset: 0x0013C8E8
		private Plsrun AddLSRun(LSRun lsrun)
		{
			if (lsrun.Type < Plsrun.FormatAnchor)
			{
				return lsrun.Type;
			}
			Plsrun plsrun = (uint)this._lsrunList.Count + Plsrun.FormatAnchor;
			if (lsrun.IsSymbol)
			{
				plsrun = TextStore.MakePlsrunSymbol(plsrun);
			}
			this._lsrunList.Add(lsrun);
			return plsrun;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x0013D530 File Offset: 0x0013C930
		internal int GetExternalCp(int lscp)
		{
			lscp -= this._lscpFirstValue;
			SpanRider spanRider = new SpanRider(this._plsrunVector, this._plsrunVectorLatestPosition, lscp - this._cpFirst);
			this._plsrunVectorLatestPosition = spanRider.SpanPosition;
			return this.GetRun((Plsrun)spanRider.CurrentElement).OffsetToFirstCp + lscp - spanRider.CurrentSpanStart;
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0013D590 File Offset: 0x0013C990
		internal LSRun GetRun(Plsrun plsrun)
		{
			plsrun = TextStore.ToIndex(plsrun);
			return (LSRun)(TextStore.IsContent(plsrun) ? this._lsrunList[(int)(plsrun - Plsrun.FormatAnchor)] : TextStore.ControlRuns[(int)plsrun]);
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x0013D5CC File Offset: 0x0013C9CC
		internal static bool IsMarker(Plsrun plsrun)
		{
			return (plsrun & Plsrun.IsMarker) > Plsrun.CloseAnchor;
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0013D5E4 File Offset: 0x0013C9E4
		internal static Plsrun MakePlsrunMarker(Plsrun plsrun)
		{
			return plsrun | Plsrun.IsMarker;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x0013D5F8 File Offset: 0x0013C9F8
		internal static Plsrun MakePlsrunSymbol(Plsrun plsrun)
		{
			return plsrun | Plsrun.IsSymbol;
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x0013D60C File Offset: 0x0013CA0C
		internal static Plsrun ToIndex(Plsrun plsrun)
		{
			return plsrun & Plsrun.UnmaskAll;
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x0013D620 File Offset: 0x0013CA20
		internal static bool IsContent(Plsrun plsrun)
		{
			plsrun = TextStore.ToIndex(plsrun);
			return plsrun >= Plsrun.FormatAnchor;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x0013D63C File Offset: 0x0013CA3C
		internal static bool IsSpace(char ch)
		{
			return ch == ' ' || ch == '\u00a0';
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x0013D658 File Offset: 0x0013CA58
		internal static bool IsStrong(char ch)
		{
			int unicodeClass = (int)Classification.GetUnicodeClass((int)ch);
			ItemClass itemClass = (ItemClass)Classification.CharAttributeOf(unicodeClass).ItemClass;
			return itemClass == ItemClass.StrongClass;
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x0013D67C File Offset: 0x0013CA7C
		internal static bool IsNewline(Plsrun plsrun)
		{
			return plsrun == Plsrun.LineBreak || plsrun == Plsrun.ParaBreak;
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0013D694 File Offset: 0x0013CA94
		internal static bool IsNewline(ushort flags)
		{
			return (flags & 4) != 0 || (flags & 512) > 0;
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x0013D6B4 File Offset: 0x0013CAB4
		internal void AdjustRunsVerticalOffset(int dcpLimit, int height, int baselineOffset, out int cellHeight, out int cellAscent)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			ArrayList arrayList = new ArrayList(3);
			int i = 0;
			int num4 = 0;
			while (i < dcpLimit)
			{
				Span span = this._plsrunVector[num4++];
				LSRun run = this.GetRun((Plsrun)span.element);
				if (run.Type == Plsrun.Text || run.Type == Plsrun.InlineObject)
				{
					if (run.RunProp.BaselineAlignment == BaselineAlignment.Baseline)
					{
						num2 = Math.Max(num2, run.BaselineOffset);
						num3 = Math.Max(num3, run.Descent);
					}
					arrayList.Add(run);
				}
				i += span.length;
			}
			num2 = -num2;
			num = ((height > 0) ? (-baselineOffset) : num2);
			foreach (object obj in arrayList)
			{
				LSRun lsrun = (LSRun)obj;
				BaselineAlignment baselineAlignment = lsrun.RunProp.BaselineAlignment;
				if (baselineAlignment != BaselineAlignment.Top)
				{
					if (baselineAlignment == BaselineAlignment.TextTop)
					{
						num3 = Math.Max(num3, lsrun.Height + num2);
					}
				}
				else
				{
					num3 = Math.Max(num3, lsrun.Height + num);
				}
			}
			int num5 = (height > 0) ? (height - baselineOffset) : num3;
			int num6 = (num + num5) / 2;
			int num7 = num5 / 2;
			int num8 = num * 2 / 3;
			cellAscent = 0;
			int num9 = 0;
			foreach (object obj2 in arrayList)
			{
				LSRun lsrun2 = (LSRun)obj2;
				int num10 = 0;
				switch (lsrun2.RunProp.BaselineAlignment)
				{
				case BaselineAlignment.Top:
					num10 = num + lsrun2.BaselineOffset;
					break;
				case BaselineAlignment.Center:
					num10 = num6 - lsrun2.Height / 2 + lsrun2.BaselineOffset;
					break;
				case BaselineAlignment.Bottom:
					num10 = num5 - lsrun2.Height + lsrun2.BaselineOffset;
					break;
				case BaselineAlignment.TextTop:
					num10 = num2 + lsrun2.BaselineOffset;
					break;
				case BaselineAlignment.TextBottom:
					num10 = num3 - lsrun2.Height + lsrun2.BaselineOffset;
					break;
				case BaselineAlignment.Subscript:
					num10 = num7;
					break;
				case BaselineAlignment.Superscript:
					num10 = num8;
					break;
				}
				lsrun2.Move(num10);
				cellAscent = Math.Max(cellAscent, lsrun2.BaselineOffset - num10);
				num9 = Math.Max(num9, lsrun2.Descent + num10);
			}
			cellHeight = cellAscent + num9;
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x0013D950 File Offset: 0x0013CD50
		internal char[] CollectRawWord(int lscpCurrent, bool isCurrentAtWordStart, bool isSideways, out int lscpChunk, out int lscchChunk, out CultureInfo textCulture, out int cchWordMax, out SpanVector<int> textVector)
		{
			textVector = default(SpanVector<int>);
			textCulture = null;
			lscpChunk = lscpCurrent;
			lscchChunk = 0;
			cchWordMax = 0;
			Plsrun plsrun;
			int num;
			int num2;
			LSRun lsrun = this.FetchLSRun(lscpCurrent, this.Settings.Formatter.TextFormattingMode, isSideways, out plsrun, out num, out num2);
			if (lsrun == null)
			{
				return null;
			}
			textCulture = lsrun.TextCulture;
			int num3 = 0;
			if (!isCurrentAtWordStart && lscpChunk > this._cpFirst)
			{
				SpanRider spanRider = new SpanRider(this._plsrunVector, this._plsrunVectorLatestPosition);
				int num7;
				for (;;)
				{
					spanRider.At(lscpChunk - this._cpFirst - 1);
					int num4 = spanRider.CurrentSpanStart + this._cpFirst;
					lsrun = this.GetRun((Plsrun)spanRider.CurrentElement);
					if (TextStore.IsNewline(lsrun.Type) || lsrun.Type == Plsrun.InlineObject)
					{
						goto IL_16B;
					}
					if (lsrun.Type == Plsrun.Text)
					{
						if (!lsrun.TextCulture.Equals(textCulture))
						{
							goto IL_16B;
						}
						int num5 = lscpChunk - num4;
						int num6 = lsrun.OffsetToFirstChar + lscpChunk - this._cpFirst - spanRider.CurrentSpanStart;
						num7 = 0;
						while (num7 < num5 && !TextStore.IsSpace(lsrun.CharacterBuffer[num6 - num7 - 1]))
						{
							num7++;
						}
						num3 += num7;
						if (num7 < num5)
						{
							break;
						}
					}
					Invariant.Assert(num4 < lscpChunk);
					lscpChunk = num4;
					if (lscpChunk <= this._cpFirst || num3 > 80)
					{
						goto IL_16B;
					}
				}
				lscpChunk -= num7;
				IL_16B:
				this._plsrunVectorLatestPosition = spanRider.SpanPosition;
			}
			if (num3 > 80)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num8 = lscpChunk;
			int num9 = 0;
			int num10 = 0;
			int num13;
			for (;;)
			{
				lsrun = this.FetchLSRun(num8, this.Settings.Formatter.TextFormattingMode, isSideways, out plsrun, out num, out num2);
				if (lsrun == null)
				{
					break;
				}
				int num4 = num8 + num2;
				if (TextStore.IsNewline(lsrun.Type) || lsrun.Type == Plsrun.InlineObject)
				{
					goto IL_2DB;
				}
				if (lsrun.Type == Plsrun.Text)
				{
					if (!lsrun.TextCulture.Equals(textCulture))
					{
						goto IL_2DB;
					}
					int num11 = num4 - num8;
					int num12 = lsrun.OffsetToFirstChar + lsrun.Length - num2;
					num13 = 0;
					if (num9 == 0)
					{
						while (num13 < num11 && TextStore.IsSpace(lsrun.CharacterBuffer[num12 + num13]))
						{
							num13++;
						}
					}
					int num14 = num10;
					char ch;
					while (num13 < num11 && num9 + num13 < 80 && !TextStore.IsSpace(ch = lsrun.CharacterBuffer[num12 + num13]))
					{
						num13++;
						if (TextStore.IsStrong(ch))
						{
							num14++;
						}
						else
						{
							if (num14 > cchWordMax)
							{
								cchWordMax = num14;
							}
							num14 = 0;
						}
					}
					num10 = num14;
					if (num10 > cchWordMax)
					{
						cchWordMax = num10;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					lsrun.CharacterBuffer.AppendToStringBuilder(stringBuilder, num12, num13);
					textVector.Set(num9, num13, num8 - lscpChunk);
					num9 += num13;
					if (num13 < num11)
					{
						goto Block_25;
					}
				}
				Invariant.Assert(num4 > num8);
				num8 = num4;
				if (num9 >= 80)
				{
					goto IL_2DB;
				}
			}
			return null;
			Block_25:
			num8 += num13;
			IL_2DB:
			if (stringBuilder == null)
			{
				return null;
			}
			lscchChunk = num8 - lscpChunk;
			Invariant.Assert(stringBuilder.Length == num9);
			char[] array = new char[stringBuilder.Length];
			stringBuilder.CopyTo(0, array, 0, array.Length);
			return array;
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x0013DC74 File Offset: 0x0013D074
		internal TextEmbeddedObjectMetrics FormatTextObject(TextEmbeddedObject textObject, int cpFirst, int currentPosition, int rightMargin)
		{
			if (this._textObjectMetricsVector == null)
			{
				this._textObjectMetricsVector = new SpanVector(null);
			}
			SpanRider spanRider = new SpanRider(this._textObjectMetricsVector);
			spanRider.At(cpFirst);
			TextEmbeddedObjectMetrics textEmbeddedObjectMetrics = (TextEmbeddedObjectMetrics)spanRider.CurrentElement;
			if (textEmbeddedObjectMetrics == null)
			{
				int num = this._formatWidth - currentPosition;
				if (num <= 0)
				{
					num = rightMargin - this._formatWidth;
				}
				textEmbeddedObjectMetrics = textObject.Format(this._settings.Formatter.IdealToReal((double)num, this._settings.TextSource.PixelsPerDip));
				if (double.IsPositiveInfinity(textEmbeddedObjectMetrics.Width))
				{
					textEmbeddedObjectMetrics = new TextEmbeddedObjectMetrics(this._settings.Formatter.IdealToReal((double)(1073741822 - currentPosition), this._settings.TextSource.PixelsPerDip), textEmbeddedObjectMetrics.Height, textEmbeddedObjectMetrics.Baseline);
				}
				else if (textEmbeddedObjectMetrics.Width > this._settings.Formatter.IdealToReal((double)(1073741822 - currentPosition), this._settings.TextSource.PixelsPerDip))
				{
					throw new ArgumentException(SR.Get("TextObjectMetrics_WidthOutOfRange"));
				}
				this._textObjectMetricsVector.SetReference(cpFirst, textObject.Length, textEmbeddedObjectMetrics);
			}
			return textEmbeddedObjectMetrics;
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x0013DD9C File Offset: 0x0013D19C
		internal FormatSettings Settings
		{
			get
			{
				return this._settings;
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x0013DDB0 File Offset: 0x0013D1B0
		internal ParaProp Pap
		{
			get
			{
				return this._settings.Pap;
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x0013DDC8 File Offset: 0x0013D1C8
		internal int CpFirst
		{
			get
			{
				return this._cpFirst;
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x0013DDDC File Offset: 0x0013D1DC
		internal SpanVector PlsrunVector
		{
			get
			{
				return this._plsrunVector;
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x0013DDF0 File Offset: 0x0013D1F0
		internal ArrayList LsrunList
		{
			get
			{
				return this._lsrunList;
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x0013DE04 File Offset: 0x0013D204
		internal int FormatWidth
		{
			get
			{
				return this._formatWidth;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x0013DE18 File Offset: 0x0013D218
		// (set) Token: 0x06004F8B RID: 20363 RVA: 0x0013DE2C File Offset: 0x0013D22C
		internal int CchEol
		{
			get
			{
				return this._cchEol;
			}
			set
			{
				this._cchEol = value;
			}
		}

		// Token: 0x040023FE RID: 9214
		private FormatSettings _settings;

		// Token: 0x040023FF RID: 9215
		private int _lscpFirstValue;

		// Token: 0x04002400 RID: 9216
		private int _cpFirst;

		// Token: 0x04002401 RID: 9217
		private int _lscchUpTo;

		// Token: 0x04002402 RID: 9218
		private int _cchUpTo;

		// Token: 0x04002403 RID: 9219
		private int _cchEol;

		// Token: 0x04002404 RID: 9220
		private int _accNominalWidthSoFar;

		// Token: 0x04002405 RID: 9221
		private int _accTextLengthSoFar;

		// Token: 0x04002406 RID: 9222
		private TextStore.NumberContext _numberContext;

		// Token: 0x04002407 RID: 9223
		private int _cpNumberContext;

		// Token: 0x04002408 RID: 9224
		private SpanVector _plsrunVector;

		// Token: 0x04002409 RID: 9225
		private SpanPosition _plsrunVectorLatestPosition;

		// Token: 0x0400240A RID: 9226
		private ArrayList _lsrunList;

		// Token: 0x0400240B RID: 9227
		private BidiState _bidiState;

		// Token: 0x0400240C RID: 9228
		private TextModifierScope _modifierScope;

		// Token: 0x0400240D RID: 9229
		private int _formatWidth;

		// Token: 0x0400240E RID: 9230
		private SpanVector _textObjectMetricsVector;

		// Token: 0x0400240F RID: 9231
		internal static LSRun[] ControlRuns;

		// Token: 0x04002410 RID: 9232
		internal const int LscpFirstMarker = -2147483647;

		// Token: 0x04002411 RID: 9233
		internal const int TypicalCharactersPerLine = 100;

		// Token: 0x04002412 RID: 9234
		internal const char CharLineSeparator = '\u2028';

		// Token: 0x04002413 RID: 9235
		internal const char CharParaSeparator = '\u2029';

		// Token: 0x04002414 RID: 9236
		internal const char CharLineFeed = '\n';

		// Token: 0x04002415 RID: 9237
		internal const char CharCarriageReturn = '\r';

		// Token: 0x04002416 RID: 9238
		internal const char CharTab = '\t';

		// Token: 0x04002417 RID: 9239
		internal static IntPtr PwchParaSeparator;

		// Token: 0x04002418 RID: 9240
		internal static IntPtr PwchLineSeparator;

		// Token: 0x04002419 RID: 9241
		internal static IntPtr PwchNbsp;

		// Token: 0x0400241A RID: 9242
		internal static IntPtr PwchHidden;

		// Token: 0x0400241B RID: 9243
		internal static IntPtr PwchObjectTerminator;

		// Token: 0x0400241C RID: 9244
		internal static IntPtr PwchObjectReplacement;

		// Token: 0x0400241D RID: 9245
		internal const int MaxCharactersPerLine = 9600;

		// Token: 0x0400241E RID: 9246
		private const int MaxCchWordToHyphenate = 80;

		// Token: 0x020009E2 RID: 2530
		private struct TextEffectBoundary : IComparable<TextStore.TextEffectBoundary>
		{
			// Token: 0x06005B7E RID: 23422 RVA: 0x00170128 File Offset: 0x0016F528
			internal TextEffectBoundary(int position, bool isStart)
			{
				this._position = position;
				this._isStart = isStart;
			}

			// Token: 0x170012B1 RID: 4785
			// (get) Token: 0x06005B7F RID: 23423 RVA: 0x00170144 File Offset: 0x0016F544
			internal int Position
			{
				get
				{
					return this._position;
				}
			}

			// Token: 0x170012B2 RID: 4786
			// (get) Token: 0x06005B80 RID: 23424 RVA: 0x00170158 File Offset: 0x0016F558
			internal bool IsStart
			{
				get
				{
					return this._isStart;
				}
			}

			// Token: 0x06005B81 RID: 23425 RVA: 0x0017016C File Offset: 0x0016F56C
			public int CompareTo(TextStore.TextEffectBoundary other)
			{
				if (this.Position != other.Position)
				{
					return this.Position - other.Position;
				}
				if (this.IsStart == other.IsStart)
				{
					return 0;
				}
				if (!this.IsStart)
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x04002EB9 RID: 11961
			private readonly int _position;

			// Token: 0x04002EBA RID: 11962
			private readonly bool _isStart;
		}

		// Token: 0x020009E3 RID: 2531
		[Flags]
		private enum NumberContext
		{
			// Token: 0x04002EBC RID: 11964
			Unknown = 0,
			// Token: 0x04002EBD RID: 11965
			Arabic = 1,
			// Token: 0x04002EBE RID: 11966
			European = 2,
			// Token: 0x04002EBF RID: 11967
			Mask = 3,
			// Token: 0x04002EC0 RID: 11968
			FromLetter = 4,
			// Token: 0x04002EC1 RID: 11969
			FromFlowDirection = 8
		}
	}
}
