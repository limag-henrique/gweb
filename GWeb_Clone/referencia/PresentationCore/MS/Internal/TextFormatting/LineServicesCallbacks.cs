using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000752 RID: 1874
	internal sealed class LineServicesCallbacks
	{
		// Token: 0x06004E32 RID: 20018 RVA: 0x0013376C File Offset: 0x00132B6C
		[SecurityCritical]
		internal unsafe LsErr FetchRunRedefined(IntPtr pols, int lscpFetch, int fIsStyle, IntPtr pstyle, char* pwchTextBuffer, int cchTextBuffer, ref int fIsBufferUsed, out char* pwchText, ref int cchText, ref int fIsHidden, ref LsChp lschp, ref IntPtr lsplsrun)
		{
			LsErr result = LsErr.None;
			pwchText = (IntPtr)((UIntPtr)0);
			Plsrun plsrun = (Plsrun)2147483648U;
			LSRun lsrun = null;
			try
			{
				FullTextState fullText = this.FullText;
				TextStore textStore = fullText.StoreFrom(lscpFetch);
				int num;
				lsrun = textStore.FetchLSRun(lscpFetch, fullText.TextFormattingMode, fullText.IsSideways, out plsrun, out num, out cchText);
				fIsBufferUsed = 0;
				pwchText = lsrun.CharacterBuffer.GetCharacterPointer();
				if (pwchText == (IntPtr)((UIntPtr)0))
				{
					if (cchText > cchTextBuffer)
					{
						return LsErr.None;
					}
					Invariant.Assert(pwchTextBuffer != null);
					int num2 = lsrun.OffsetToFirstChar + num;
					int i = 0;
					while (i < cchText)
					{
						pwchTextBuffer[i] = lsrun.CharacterBuffer[num2];
						i++;
						num2++;
					}
					fIsBufferUsed = 1;
				}
				else
				{
					pwchText += (IntPtr)(lsrun.OffsetToFirstChar + num) * 2;
				}
				lschp = default(LsChp);
				fIsHidden = 0;
				switch (lsrun.Type)
				{
				case Plsrun.CloseAnchor:
				case Plsrun.FormatAnchor:
					lschp.idObj = ushort.MaxValue;
					goto IL_1D1;
				case Plsrun.Reverse:
					lschp.idObj = 0;
					goto IL_1D1;
				case Plsrun.Hidden:
					lschp.idObj = ushort.MaxValue;
					fIsHidden = 1;
					goto IL_1D1;
				case Plsrun.Text:
					lschp.idObj = ushort.MaxValue;
					if (lsrun.Shapeable != null && lsrun.Shapeable.IsShapingRequired)
					{
						lschp.flags |= LsChp.Flags.fGlyphBased;
						if (lsrun.Shapeable.NeedsMaxClusterSize)
						{
							lschp.dcpMaxContent = lsrun.Shapeable.MaxClusterSize;
						}
					}
					this.SetChpFormat(lsrun.RunProp, ref lschp);
					Invariant.Assert(!TextStore.IsNewline(lsrun.CharacterAttributeFlags));
					goto IL_1D1;
				case Plsrun.InlineObject:
					lschp.idObj = 1;
					this.SetChpFormat(lsrun.RunProp, ref lschp);
					goto IL_1D1;
				}
				lschp.idObj = ushort.MaxValue;
				textStore.CchEol = lsrun.Length;
				IL_1D1:
				if ((lsrun.Type == Plsrun.Text || lsrun.Type == Plsrun.InlineObject) && lsrun.RunProp != null && lsrun.RunProp.BaselineAlignment != BaselineAlignment.Baseline)
				{
					this.FullText.VerticalAdjust = true;
				}
				lsplsrun = (IntPtr)((long)((ulong)plsrun));
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("FetchRunRedefined", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x001339F4 File Offset: 0x00132DF4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void SetChpFormat(TextRunProperties runProp, ref LsChp lschp)
		{
			this.SetChpFormat(runProp.TextDecorations, ref lschp);
			this.SetChpFormat(this.FullText.TextStore.Pap.TextDecorations, ref lschp);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00133A2C File Offset: 0x00132E2C
		private void SetChpFormat(TextDecorationCollection textDecorations, ref LsChp lschp)
		{
			if (textDecorations != null)
			{
				for (int i = 0; i < textDecorations.Count; i++)
				{
					TextDecorationLocation location = textDecorations[i].Location;
					if (location != TextDecorationLocation.Underline)
					{
						if (location - TextDecorationLocation.OverLine <= 2)
						{
							lschp.flags |= LsChp.Flags.fStrike;
						}
					}
					else
					{
						lschp.flags |= LsChp.Flags.fUnderline;
					}
				}
			}
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x00133A88 File Offset: 0x00132E88
		[SecurityCritical]
		internal LsErr FetchPap(IntPtr pols, int lscpFetch, ref LsPap lspap)
		{
			LsErr result = LsErr.None;
			try
			{
				lspap = default(LsPap);
				TextStore textStore = this.FullText.StoreFrom(lscpFetch);
				lspap.cpFirstContent = lscpFetch;
				lspap.cpFirst = lscpFetch;
				lspap.lskeop = LsKEOP.lskeopEndPara1;
				lspap.grpf |= LsPap.Flags.fFmiTreatHyphenAsRegular;
				ParaProp pap = textStore.Pap;
				if (this.FullText.ForceWrap)
				{
					lspap.grpf |= LsPap.Flags.fFmiApplyBreakingRules;
				}
				else if (pap.Wrap)
				{
					lspap.grpf |= LsPap.Flags.fFmiApplyBreakingRules;
					if (!pap.EmergencyWrap)
					{
						lspap.grpf |= LsPap.Flags.fFmiForceBreakAsNext;
					}
					if (pap.Hyphenator != null)
					{
						lspap.grpf |= LsPap.Flags.fFmiAllowHyphenation;
					}
				}
				if (pap.FirstLineInParagraph)
				{
					lspap.cpFirstContent = textStore.CpFirst;
					lspap.cpFirst = lspap.cpFirstContent;
					if (this.FullText.TextMarkerStore != null)
					{
						lspap.grpf |= LsPap.Flags.fFmiAnm;
					}
				}
				lspap.fJustify = (pap.Justify ? 1 : 0);
				if (pap.Wrap && pap.OptimalBreak)
				{
					lspap.lsbrj = LsBreakJust.lsbrjBreakOptimal;
					lspap.lskj = LsKJust.lskjFullMixed;
				}
				else
				{
					lspap.lsbrj = LsBreakJust.lsbrjBreakJustify;
					if (pap.Justify)
					{
						lspap.lskj = LsKJust.lskjFullInterWord;
					}
				}
				lspap.lstflow = (pap.RightToLeft ? LsTFlow.lstflowWS : LsTFlow.lstflowDefault);
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("FetchPap", (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00133C34 File Offset: 0x00133034
		[SecurityCritical]
		internal LsErr FetchLineProps(IntPtr pols, int lscpFetch, int firstLineInPara, ref LsLineProps lsLineProps)
		{
			LsErr result = LsErr.None;
			try
			{
				TextStore textStore = this.FullText.TextStore;
				TextStore textMarkerStore = this.FullText.TextMarkerStore;
				ParaProp pap = textStore.Pap;
				FormatSettings settings = textStore.Settings;
				lsLineProps = default(LsLineProps);
				if (this.FullText.GetMainTextToMarkerIdealDistance() != 0)
				{
					lsLineProps.durLeft = TextFormatterImp.RealToIdeal(textMarkerStore.Pap.TextMarkerProperties.Offset);
				}
				else
				{
					lsLineProps.durLeft = settings.TextIndent;
				}
				if (pap.Wrap && pap.OptimalBreak && settings.MaxLineWidth < this.FullText.FormatWidth)
				{
					lsLineProps.durRightBreak = (lsLineProps.durRightJustify = this.FullText.FormatWidth - settings.MaxLineWidth);
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("FetchLineProps", (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00133D5C File Offset: 0x0013315C
		[SecurityCritical]
		internal LsErr GetRunTextMetrics(IntPtr pols, Plsrun plsrun, LsDevice lsDevice, LsTFlow lstFlow, ref LsTxM lstTextMetrics)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				FullTextState fullText = this.FullText;
				TextStore textStore = fullText.StoreFrom(plsrun);
				lsrun = textStore.GetRun(plsrun);
				if (lsrun.Height > 0)
				{
					lstTextMetrics.dvAscent = lsrun.BaselineOffset;
					lstTextMetrics.dvMultiLineHeight = lsrun.Height;
				}
				else
				{
					Typeface defaultTypeface = textStore.Pap.DefaultTypeface;
					lstTextMetrics.dvAscent = (int)Math.Round(defaultTypeface.Baseline((double)textStore.Pap.EmSize, 0.0033333333333333335, textStore.Settings.TextSource.PixelsPerDip, fullText.TextFormattingMode));
					lstTextMetrics.dvMultiLineHeight = (int)Math.Round(defaultTypeface.LineSpacing((double)textStore.Pap.EmSize, 0.0033333333333333335, textStore.Settings.TextSource.PixelsPerDip, fullText.TextFormattingMode));
				}
				lstTextMetrics.dvDescent = lstTextMetrics.dvMultiLineHeight - lstTextMetrics.dvAscent;
				lstTextMetrics.fMonospaced = 0;
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetRunTextMetrics", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00133EB8 File Offset: 0x001332B8
		[SecurityCritical]
		internal unsafe LsErr GetRunCharWidths(IntPtr pols, Plsrun plsrun, LsDevice device, char* charString, int stringLength, int maxWidth, LsTFlow textFlow, int* charWidths, ref int totalWidth, ref int stringLengthFitted)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				if (this.FullText != null)
				{
					lsrun = this.FullText.StoreFrom(plsrun).GetRun(plsrun);
					TextFormatterImp formatter = this.FullText.Formatter;
				}
				else
				{
					lsrun = this.Draw.CurrentLine.GetRun(plsrun);
					TextFormatterImp formatter = this.Draw.CurrentLine.Formatter;
				}
				if (lsrun.Type == Plsrun.Text)
				{
					lsrun.Shapeable.GetAdvanceWidthsUnshaped(charString, stringLength, TextFormatterImp.ToIdeal, charWidths);
					totalWidth = 0;
					stringLengthFitted = 0;
					int num;
					do
					{
						totalWidth += charWidths[stringLengthFitted];
						num = stringLengthFitted + 1;
						stringLengthFitted = num;
					}
					while (num < stringLength && totalWidth <= maxWidth);
					if (totalWidth <= maxWidth && this.FullText != null)
					{
						int num2 = lsrun.OffsetToFirstCp + stringLengthFitted;
						if (num2 > this.FullText.CpMeasured)
						{
							this.FullText.CpMeasured = num2;
						}
					}
				}
				else
				{
					*charWidths = 0;
					totalWidth = 0;
					stringLengthFitted = stringLength;
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetRunCharWidths", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x0013400C File Offset: 0x0013340C
		[SecurityCritical]
		internal LsErr GetDurMaxExpandRagged(IntPtr pols, Plsrun plsrun, LsTFlow lstFlow, ref int maxExpandRagged)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				lsrun = this.FullText.StoreFrom(plsrun).GetRun(plsrun);
				maxExpandRagged = lsrun.EmSize;
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetDurMaxExpandRagged", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00134098 File Offset: 0x00133498
		[SecurityCritical]
		internal LsErr GetAutoNumberInfo(IntPtr pols, ref LsKAlign alignment, ref LsChp lschp, ref IntPtr lsplsrun, ref ushort addedChar, ref LsChp lschpAddedChar, ref IntPtr lsplsrunAddedChar, ref int fWord95Model, ref int offset, ref int width)
		{
			LsErr result = LsErr.None;
			Plsrun plsrun = (Plsrun)2147483648U;
			LSRun lsrun = null;
			try
			{
				FullTextState fullText = this.FullText;
				TextStore textMarkerStore = fullText.TextMarkerStore;
				TextStore textStore = fullText.TextStore;
				int num = -2147483647;
				do
				{
					int num2;
					int num3;
					lsrun = textMarkerStore.FetchLSRun(num, fullText.TextFormattingMode, fullText.IsSideways, out plsrun, out num2, out num3);
					num += num3;
				}
				while (!TextStore.IsContent(plsrun));
				alignment = LsKAlign.lskalRight;
				lschp = default(LsChp);
				lschp.idObj = ushort.MaxValue;
				this.SetChpFormat(lsrun.RunProp, ref lschp);
				addedChar = ((this.FullText.GetMainTextToMarkerIdealDistance() != 0) ? 9 : 0);
				lschpAddedChar = lschp;
				fWord95Model = 0;
				offset = 0;
				width = 0;
				lsplsrun = (IntPtr)((long)((ulong)plsrun));
				lsplsrunAddedChar = lsplsrun;
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetAutoNumberInfo", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x001341BC File Offset: 0x001335BC
		[SecurityCritical]
		internal LsErr GetRunUnderlineInfo(IntPtr pols, Plsrun plsrun, ref LsHeights lsHeights, LsTFlow textFlow, ref LsULInfo ulInfo)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				lsrun = this.Draw.CurrentLine.GetRun(plsrun);
				ulInfo = default(LsULInfo);
				double underlinePosition;
				double underlineThickness;
				if (lsrun.Shapeable != null)
				{
					underlinePosition = lsrun.Shapeable.UnderlinePosition;
					underlineThickness = lsrun.Shapeable.UnderlineThickness;
				}
				else
				{
					underlinePosition = lsrun.RunProp.Typeface.UnderlinePosition;
					underlineThickness = lsrun.RunProp.Typeface.UnderlineThickness;
				}
				ulInfo.cNumberOfLines = 1;
				ulInfo.dvpFirstUnderlineOffset = (int)Math.Round((double)lsrun.EmSize * -underlinePosition);
				ulInfo.dvpFirstUnderlineSize = (int)Math.Round((double)lsrun.EmSize * underlineThickness);
				if (ulInfo.dvpFirstUnderlineSize <= 0)
				{
					ulInfo.dvpFirstUnderlineSize = 1;
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetAutoNumberInfo", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x001342D4 File Offset: 0x001336D4
		[SecurityCritical]
		internal LsErr GetRunStrikethroughInfo(IntPtr pols, Plsrun plsrun, ref LsHeights lsHeights, LsTFlow textFlow, ref LsStInfo stInfo)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				lsrun = this.Draw.CurrentLine.GetRun(plsrun);
				stInfo = default(LsStInfo);
				double num;
				double num2;
				this.GetLSRunStrikethroughMetrics(lsrun, out num, out num2);
				stInfo.cNumberOfLines = 1;
				stInfo.dvpLowerStrikethroughOffset = (int)Math.Round((double)lsrun.EmSize * num);
				stInfo.dvpLowerStrikethroughSize = (int)Math.Round((double)lsrun.EmSize * num2);
				if (stInfo.dvpLowerStrikethroughSize <= 0)
				{
					stInfo.dvpLowerStrikethroughSize = 1;
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetRunStrikethroughInfo", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x001343B0 File Offset: 0x001337B0
		private void GetLSRunStrikethroughMetrics(LSRun lsrun, out double strikeThroughPositionInEm, out double strikeThroughThicknessInEm)
		{
			if (lsrun.Shapeable != null)
			{
				strikeThroughPositionInEm = lsrun.Shapeable.StrikethroughPosition;
				strikeThroughThicknessInEm = lsrun.Shapeable.StrikethroughThickness;
				return;
			}
			strikeThroughPositionInEm = lsrun.RunProp.Typeface.StrikethroughPosition;
			strikeThroughThicknessInEm = lsrun.RunProp.Typeface.StrikethroughThickness;
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00134404 File Offset: 0x00133804
		[SecurityCritical]
		internal LsErr Hyphenate(IntPtr pols, int fLastHyphenFound, int lscpLastHyphen, ref LsHyph lastHyph, int lscpWordStart, int lscpExceed, ref int fHyphenFound, ref int lscpHyphen, ref LsHyph lsHyph)
		{
			LsErr result = LsErr.None;
			try
			{
				fHyphenFound = (this.FullText.FindNextHyphenBreak(lscpWordStart, lscpExceed - lscpWordStart, true, ref lscpHyphen, ref lsHyph) ? 1 : 0);
				Invariant.Assert(fHyphenFound == 0 || (lscpHyphen >= lscpWordStart && lscpHyphen < lscpExceed));
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("Hyphenate", (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x001344B8 File Offset: 0x001338B8
		[SecurityCritical]
		internal LsErr GetNextHyphenOpp(IntPtr pols, int lscpStartSearch, int lsdcpSearch, ref int fHyphenFound, ref int lscpHyphen, ref LsHyph lsHyph)
		{
			LsErr result = LsErr.None;
			try
			{
				fHyphenFound = (this.FullText.FindNextHyphenBreak(lscpStartSearch, lsdcpSearch, false, ref lscpHyphen, ref lsHyph) ? 1 : 0);
				Invariant.Assert(fHyphenFound == 0 || (lscpHyphen >= lscpStartSearch && lscpHyphen < lscpStartSearch + lsdcpSearch));
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetNextHyphenOpp", (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00134568 File Offset: 0x00133968
		[SecurityCritical]
		internal LsErr GetPrevHyphenOpp(IntPtr pols, int lscpStartSearch, int lsdcpSearch, ref int fHyphenFound, ref int lscpHyphen, ref LsHyph lsHyph)
		{
			LsErr result = LsErr.None;
			try
			{
				fHyphenFound = (this.FullText.FindNextHyphenBreak(lscpStartSearch + 1, -lsdcpSearch, false, ref lscpHyphen, ref lsHyph) ? 1 : 0);
				Invariant.Assert(fHyphenFound == 0 || (lscpHyphen > lscpStartSearch - lsdcpSearch && lscpHyphen <= lscpStartSearch));
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetPrevHyphenOpp", (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x00134620 File Offset: 0x00133A20
		[SecurityCritical]
		internal LsErr DrawStrikethrough(IntPtr pols, Plsrun plsrun, uint stType, ref LSPOINT ptOrigin, int stLength, int stThickness, LsTFlow textFlow, uint displayMode, ref LSRECT clipRect)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				if (!TextStore.IsContent(plsrun))
				{
					return LsErr.None;
				}
				TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
				lsrun = currentLine.GetRun(plsrun);
				double num;
				double num2;
				this.GetLSRunStrikethroughMetrics(lsrun, out num, out num2);
				int num3 = ptOrigin.y + (int)Math.Round((double)lsrun.EmSize * num);
				int overlineTop = num3 - (lsrun.BaselineOffset - (int)Math.Round((double)lsrun.EmSize * num2));
				this.DrawTextDecorations(lsrun, 14U, ptOrigin.x, 0, overlineTop, ptOrigin.y, num3, stLength, stThickness, textFlow);
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("DrawStrikethrough", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0013471C File Offset: 0x00133B1C
		[SecurityCritical]
		internal LsErr DrawUnderline(IntPtr pols, Plsrun plsrun, uint ulType, ref LSPOINT ptOrigin, int ulLength, int ulThickness, LsTFlow textFlow, uint displayMode, ref LSRECT clipRect)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				if (!TextStore.IsContent(plsrun))
				{
					return LsErr.None;
				}
				lsrun = this.Draw.CurrentLine.GetRun(plsrun);
				this.DrawTextDecorations(lsrun, 1U, ptOrigin.x, ptOrigin.y, 0, 0, 0, ulLength, ulThickness, textFlow);
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("DrawUnderline", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x001347CC File Offset: 0x00133BCC
		[SecurityCritical]
		private void DrawTextDecorations(LSRun lsrun, uint locationMask, int left, int underlineTop, int overlineTop, int strikethroughTop, int baselineTop, int length, int thickness, LsTFlow textFlow)
		{
			TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
			TextDecorationCollection textDecorations = currentLine.TextDecorations;
			if (textDecorations != null)
			{
				this.DrawTextDecorationCollection(lsrun, locationMask, textDecorations, currentLine.DefaultTextDecorationsBrush, left, underlineTop, overlineTop, strikethroughTop, baselineTop, length, thickness, textFlow);
			}
			textDecorations = lsrun.RunProp.TextDecorations;
			if (textDecorations != null)
			{
				this.DrawTextDecorationCollection(lsrun, locationMask, textDecorations, lsrun.RunProp.ForegroundBrush, left, underlineTop, overlineTop, strikethroughTop, baselineTop, length, thickness, textFlow);
			}
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x00134840 File Offset: 0x00133C40
		[SecurityCritical]
		private void DrawTextDecorationCollection(LSRun lsrun, uint locationMask, TextDecorationCollection textDecorations, Brush foregroundBrush, int left, int underlineTop, int overlineTop, int strikethroughTop, int baselineTop, int length, int thickness, LsTFlow textFlow)
		{
			Invariant.Assert(textDecorations != null);
			foreach (TextDecoration textDecoration in textDecorations)
			{
				if ((1U << (int)textDecoration.Location & locationMask) != 0U)
				{
					switch (textDecoration.Location)
					{
					case TextDecorationLocation.Underline:
						this._boundingBox.Union(this.DrawTextDecoration(lsrun, foregroundBrush, new LSPOINT(left, underlineTop), length, thickness, textFlow, textDecoration));
						break;
					case TextDecorationLocation.OverLine:
						this._boundingBox.Union(this.DrawTextDecoration(lsrun, foregroundBrush, new LSPOINT(left, overlineTop), length, thickness, textFlow, textDecoration));
						break;
					case TextDecorationLocation.Strikethrough:
						this._boundingBox.Union(this.DrawTextDecoration(lsrun, foregroundBrush, new LSPOINT(left, strikethroughTop), length, thickness, textFlow, textDecoration));
						break;
					case TextDecorationLocation.Baseline:
						this._boundingBox.Union(this.DrawTextDecoration(lsrun, foregroundBrush, new LSPOINT(left, baselineTop), length, thickness, textFlow, textDecoration));
						break;
					}
				}
			}
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x0013496C File Offset: 0x00133D6C
		[SecurityCritical]
		private Rect DrawTextDecoration(LSRun lsrun, Brush foregroundBrush, LSPOINT ptOrigin, int ulLength, int ulThickness, LsTFlow textFlow, TextDecoration textDecoration)
		{
			if (textFlow == LsTFlow.lstflowWS || textFlow - LsTFlow.lstflowNE <= 1)
			{
				ptOrigin.x -= ulLength;
			}
			TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
			if (currentLine.RightToLeft)
			{
				ptOrigin.x = -ptOrigin.x;
			}
			int u = currentLine.LSLineUToParagraphU(ptOrigin.x);
			Point point = LSRun.UVToXY(this.Draw.LineOrigin, this.Draw.VectorToLineOrigin, u, currentLine.BaselineOffset, currentLine);
			Point point2 = LSRun.UVToXY(this.Draw.LineOrigin, this.Draw.VectorToLineOrigin, u, ptOrigin.y + lsrun.BaselineMoveOffset, currentLine);
			double num = 1.0;
			if (textDecoration.Pen != null)
			{
				num = textDecoration.Pen.Thickness;
			}
			switch (textDecoration.PenThicknessUnit)
			{
			case TextDecorationUnit.FontRecommended:
				num = currentLine.Formatter.IdealToReal((double)ulThickness * num, currentLine.PixelsPerDip);
				break;
			case TextDecorationUnit.FontRenderingEmSize:
				num = currentLine.Formatter.IdealToReal(num * (double)lsrun.EmSize, currentLine.PixelsPerDip);
				break;
			}
			num = Math.Abs(num);
			double num2 = 1.0;
			switch (textDecoration.PenOffsetUnit)
			{
			case TextDecorationUnit.FontRecommended:
				num2 = point2.Y - point.Y;
				break;
			case TextDecorationUnit.FontRenderingEmSize:
				num2 = currentLine.Formatter.IdealToReal((double)lsrun.EmSize, currentLine.PixelsPerDip);
				break;
			case TextDecorationUnit.Pixel:
				num2 = 1.0;
				break;
			}
			double num3 = currentLine.Formatter.IdealToReal((double)ulLength, currentLine.PixelsPerDip);
			DrawingContext drawingContext = this.Draw.DrawingContext;
			if (drawingContext != null)
			{
				double num4 = num;
				Point point3 = point2;
				bool flag = !textDecoration.CanFreeze && num2 != 0.0;
				int num5 = 0;
				this.Draw.SetGuidelineY(point.Y);
				try
				{
					if (flag)
					{
						ScaleTransform transform = new ScaleTransform(1.0, num2, point3.X, point3.Y);
						TranslateTransform transform2 = new TranslateTransform(0.0, textDecoration.PenOffset);
						num4 /= Math.Abs(num2);
						drawingContext.PushTransform(transform);
						num5++;
						drawingContext.PushTransform(transform2);
						num5++;
					}
					else
					{
						point3.Y += num2 * textDecoration.PenOffset;
					}
					drawingContext.PushGuidelineY2(point.Y, point3.Y - num4 * 0.5 - point.Y);
					num5++;
					if (textDecoration.Pen == null)
					{
						drawingContext.DrawRectangle(foregroundBrush, null, new Rect(point3.X, point3.Y - num4 * 0.5, num3, num4));
					}
					else
					{
						Pen pen = textDecoration.Pen.CloneCurrentValue();
						if (textDecoration.Pen == pen)
						{
							pen = textDecoration.Pen.Clone();
						}
						pen.Thickness = num4;
						drawingContext.DrawLine(pen, point3, new Point(point3.X + num3, point3.Y));
					}
				}
				finally
				{
					for (int i = 0; i < num5; i++)
					{
						drawingContext.Pop();
					}
					this.Draw.UnsetGuidelineY();
				}
			}
			return new Rect(point2.X, point2.Y + num2 * textDecoration.PenOffset - num * 0.5, num3, num);
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x00134CF0 File Offset: 0x001340F0
		[SecurityCritical]
		internal unsafe LsErr DrawTextRun(IntPtr pols, Plsrun plsrun, ref LSPOINT ptText, char* pwchText, int* piCharAdvances, int cchText, LsTFlow textFlow, uint displayMode, ref LSPOINT ptRun, ref LsHeights lsHeights, int dupRun, ref LSRECT clipRect)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
				lsrun = currentLine.GetRun(plsrun);
				GlyphRun glyphRun = this.ComputeUnshapedGlyphRun(lsrun, textFlow, currentLine.Formatter, true, ptText, dupRun, cchText, pwchText, piCharAdvances, currentLine.IsJustified);
				if (glyphRun != null)
				{
					DrawingContext drawingContext = this.Draw.DrawingContext;
					this.Draw.SetGuidelineY(glyphRun.BaselineOrigin.Y);
					try
					{
						this._boundingBox.Union(lsrun.DrawGlyphRun(drawingContext, null, glyphRun));
					}
					finally
					{
						this.Draw.UnsetGuidelineY();
					}
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("DrawTextRun", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x00134E00 File Offset: 0x00134200
		[SecurityCritical]
		internal LsErr FInterruptShaping(IntPtr pols, LsTFlow textFlow, Plsrun plsrunFirst, Plsrun plsrunSecond, ref int fIsInterruptOk)
		{
			LsErr result = LsErr.None;
			try
			{
				TextStore textStore = this.FullText.StoreFrom(plsrunFirst);
				if (!TextStore.IsContent(plsrunFirst) || !TextStore.IsContent(plsrunSecond))
				{
					fIsInterruptOk = 1;
					return LsErr.None;
				}
				LSRun run = textStore.GetRun(plsrunFirst);
				LSRun run2 = textStore.GetRun(plsrunSecond);
				fIsInterruptOk = ((run.BidiLevel != run2.BidiLevel || run.Shapeable == null || run2.Shapeable == null || !run.Shapeable.CanShapeTogether(run2.Shapeable)) ? 1 : 0);
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("FInterruptShaping", (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x00134EF0 File Offset: 0x001342F0
		internal static CultureInfo GetNumberCulture(TextRunProperties properties, out NumberSubstitutionMethod method)
		{
			NumberSubstitution numberSubstitution = properties.NumberSubstitution;
			if (numberSubstitution == null)
			{
				method = NumberSubstitutionMethod.AsCulture;
				return CultureMapper.GetSpecificCulture(properties.CultureInfo);
			}
			method = numberSubstitution.Substitution;
			switch (numberSubstitution.CultureSource)
			{
			case NumberCultureSource.Text:
				return CultureMapper.GetSpecificCulture(properties.CultureInfo);
			case NumberCultureSource.User:
				return CultureInfo.CurrentCulture;
			case NumberCultureSource.Override:
				return numberSubstitution.CultureOverride;
			default:
				return null;
			}
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x00134F54 File Offset: 0x00134354
		[SecurityCritical]
		internal unsafe LsErr GetGlyphsRedefined(IntPtr pols, IntPtr* plsplsruns, int* pcchPlsrun, int plsrunCount, char* pwchText, int cchText, LsTFlow textFlow, ushort* puGlyphsBuffer, uint* piGlyphPropsBuffer, int cgiGlyphBuffers, ref int fIsGlyphBuffersUsed, ushort* puClusterMap, ushort* puCharProperties, int* pfCanGlyphAlone, ref int glyphCount)
		{
			Invariant.Assert(puGlyphsBuffer != null && piGlyphPropsBuffer != null);
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			checked
			{
				try
				{
					LSRun[] array = this.RemapLSRuns(plsplsruns, plsrunCount);
					lsrun = array[0];
					bool isRightToLeft = (lsrun.BidiLevel & 1) > 0;
					uint num = (uint)cchText;
					DWriteFontFeature[][] features;
					uint[] featureRangeLengths;
					LSRun.CompileFeatureSet(array, pcchPlsrun, num, out features, out featureRangeLengths);
					GlyphTypeface glyphTypeFace = lsrun.Shapeable.GlyphTypeFace;
					uint num2;
					this.FullText.Formatter.TextAnalyzer.GetGlyphs((ushort*)pwchText, num, glyphTypeFace.FontDWrite, glyphTypeFace.BlankGlyphIndex, false, isRightToLeft, lsrun.RunProp.CultureInfo, features, featureRangeLengths, (uint)cgiGlyphBuffers, this.FullText.TextFormattingMode, lsrun.Shapeable.ItemProps, puClusterMap, puCharProperties, puGlyphsBuffer, piGlyphPropsBuffer, pfCanGlyphAlone, out num2);
					glyphCount = (int)num2;
					if (glyphCount <= cgiGlyphBuffers)
					{
						fIsGlyphBuffersUsed = 1;
					}
					else
					{
						fIsGlyphBuffersUsed = 0;
					}
				}
				catch (Exception e)
				{
					this.SaveException(e, (Plsrun)((int)(*plsplsruns)), lsrun);
					result = LsErr.ClientAbort;
				}
				catch
				{
					this.SaveNonCLSException("GetGlyphsRedefined", (Plsrun)((int)(*plsplsruns)), lsrun);
					result = LsErr.ClientAbort;
				}
				return result;
			}
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00135098 File Offset: 0x00134498
		[SecurityCritical]
		internal unsafe LsErr GetGlyphPositions(IntPtr pols, IntPtr* plsplsruns, int* pcchPlsrun, int plsrunCount, LsDevice device, char* pwchText, ushort* puClusterMap, ushort* puCharProperties, int cchText, ushort* puGlyphs, uint* piGlyphProperties, int glyphCount, LsTFlow textFlow, int* piGlyphAdvances, GlyphOffset* piiGlyphOffsets)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				LSRun[] array = this.RemapLSRuns(plsplsruns, plsrunCount);
				lsrun = array[0];
				bool isRightToLeft = (lsrun.BidiLevel & 1) > 0;
				GlyphTypeface glyphTypeFace = lsrun.Shapeable.GlyphTypeFace;
				DWriteFontFeature[][] features;
				uint[] featureRangeLengths;
				LSRun.CompileFeatureSet(array, pcchPlsrun, checked((uint)cchText), out features, out featureRangeLengths);
				GlyphOffset[] array2;
				this.FullText.Formatter.TextAnalyzer.GetGlyphPlacements((ushort*)pwchText, (ushort*)puClusterMap, puCharProperties, (uint)cchText, (ushort*)puGlyphs, piGlyphProperties, (uint)glyphCount, glyphTypeFace.FontDWrite, lsrun.Shapeable.EmSize, TextFormatterImp.ToIdeal, false, isRightToLeft, lsrun.RunProp.CultureInfo, features, featureRangeLengths, this.FullText.TextFormattingMode, lsrun.Shapeable.ItemProps, (float)this.FullText.StoreFrom(lsrun.Type).Settings.TextSource.PixelsPerDip, piGlyphAdvances, out array2);
				for (int i = 0; i < glyphCount; i++)
				{
					piiGlyphOffsets[i].du = array2[i].du;
					piiGlyphOffsets[i].dv = array2[i].dv;
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)((int)(*plsplsruns)), lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetGlyphPositions", (Plsrun)((int)(*plsplsruns)), lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x00135220 File Offset: 0x00134620
		[SecurityCritical]
		private unsafe LSRun[] RemapLSRuns(IntPtr* plsplsruns, int plsrunCount)
		{
			LSRun[] array = new LSRun[plsrunCount];
			TextStore textStore = this.FullText.StoreFrom((Plsrun)((int)(*plsplsruns)));
			for (int i = 0; i < array.Length; i++)
			{
				Plsrun plsrun = (Plsrun)((int)plsplsruns[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]);
				array[i] = textStore.GetRun(plsrun);
			}
			return array;
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x00135274 File Offset: 0x00134674
		[SecurityCritical]
		internal unsafe LsErr DrawGlyphs(IntPtr pols, Plsrun plsrun, char* pwchText, ushort* puClusterMap, ushort* puCharProperties, int charCount, ushort* puGlyphs, int* piJustifiedGlyphAdvances, int* piGlyphAdvances, GlyphOffset* piiGlyphOffsets, uint* piGlyphProperties, LsExpType* plsExpType, int glyphCount, LsTFlow textFlow, uint displayMode, ref LSPOINT ptRun, ref LsHeights lsHeights, int runWidth, ref LSRECT clippingRect)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
				lsrun = currentLine.GetRun(plsrun);
				GlyphRun glyphRun = this.ComputeShapedGlyphRun(lsrun, currentLine.Formatter, true, ptRun, charCount, pwchText, puClusterMap, glyphCount, puGlyphs, piJustifiedGlyphAdvances, piiGlyphOffsets, currentLine.IsJustified);
				if (glyphRun != null)
				{
					DrawingContext drawingContext = this.Draw.DrawingContext;
					this.Draw.SetGuidelineY(glyphRun.BaselineOrigin.Y);
					try
					{
						this._boundingBox.Union(lsrun.DrawGlyphRun(drawingContext, null, glyphRun));
					}
					finally
					{
						this.Draw.UnsetGuidelineY();
					}
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("DrawGlyphs", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00135388 File Offset: 0x00134788
		[SecurityCritical]
		internal unsafe LsErr GetCharCompressionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsCharRunInfo* plscharrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplscompressionLeft, int** pplscompressionRight)
		{
			LsErr result = LsErr.None;
			Plsrun plsrun = (Plsrun)2147483648U;
			LSRun lsrun = null;
			try
			{
				Invariant.Assert(maxPriorityLevel == 3);
				plsrun = plscharrunInfo->plsrun;
				lsrun = this.FullText.StoreFrom(plsrun).GetRun(plsrun);
				return this.AdjustChars(plscharrunInfo, false, (int)((double)lsrun.EmSize * 0.2), pplscompressionLeft, pplscompressionRight);
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetCharCompressionInfoFullMixed", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x00135448 File Offset: 0x00134848
		[SecurityCritical]
		internal unsafe LsErr GetCharExpansionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsCharRunInfo* plscharrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplsexpansionLeft, int** pplsexpansionRight)
		{
			LsErr result = LsErr.None;
			Plsrun plsrun = (Plsrun)2147483648U;
			LSRun lsrun = null;
			try
			{
				Invariant.Assert(maxPriorityLevel == 3);
				plsrun = plscharrunInfo->plsrun;
				lsrun = this.FullText.StoreFrom(plsrun).GetRun(plsrun);
				return this.AdjustChars(plscharrunInfo, true, (int)((double)lsrun.EmSize * 0.5), pplsexpansionLeft, pplsexpansionRight);
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetCharExpansionInfoFullMixed", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00135508 File Offset: 0x00134908
		[SecurityCritical]
		private unsafe LsErr AdjustChars(LsCharRunInfo* plscharrunInfo, bool expanding, int interWordAdjustTo, int** pplsAdjustLeft, int** pplsAdjustRight)
		{
			char* pwch = plscharrunInfo->pwch;
			int cwch = plscharrunInfo->cwch;
			for (int i = 0; i < cwch; i++)
			{
				int num = plscharrunInfo->rgduNominalWidth[i] + plscharrunInfo->rgduChangeLeft[i] + plscharrunInfo->rgduChangeRight[i];
				*(*(IntPtr*)pplsAdjustLeft + (IntPtr)i * 4) = 0;
				*(*(IntPtr*)(pplsAdjustLeft + sizeof(int*) / sizeof(int*)) + (IntPtr)i * 4) = 0;
				*(*(IntPtr*)(pplsAdjustLeft + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)i * 4) = 0;
				*(*(IntPtr*)pplsAdjustRight + (IntPtr)i * 4) = 0;
				*(*(IntPtr*)(pplsAdjustRight + sizeof(int*) / sizeof(int*)) + (IntPtr)i * 4) = 0;
				*(*(IntPtr*)(pplsAdjustRight + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)i * 4) = 0;
				ushort flags = Classification.CharAttributeOf((int)Classification.GetUnicodeClassUTF16(pwch[i])).Flags;
				if ((flags & 128) != 0)
				{
					if (expanding)
					{
						int num2 = Math.Max(0, interWordAdjustTo - num);
						*(*(IntPtr*)pplsAdjustRight + (IntPtr)i * 4) = num2;
						*(*(IntPtr*)(pplsAdjustRight + sizeof(int*) / sizeof(int*)) + (IntPtr)i * 4) = num2 * 2;
						*(*(IntPtr*)(pplsAdjustRight + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)i * 4) = this.FullText.FormatWidth;
					}
					else
					{
						*(*(IntPtr*)pplsAdjustRight + (IntPtr)i * 4) = Math.Max(0, num - interWordAdjustTo);
					}
				}
				else if (expanding)
				{
					*(*(IntPtr*)(pplsAdjustRight + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)i * 4) = this.FullText.FormatWidth;
				}
			}
			return LsErr.None;
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00135660 File Offset: 0x00134A60
		[SecurityCritical]
		internal unsafe LsErr GetGlyphCompressionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsGlyphRunInfo* plsglyphrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplscompressionLeft, int** pplscompressionRight)
		{
			LsErr result = LsErr.None;
			Plsrun plsrun = (Plsrun)2147483648U;
			LSRun lsrun = null;
			try
			{
				Invariant.Assert(maxPriorityLevel == 3);
				plsrun = plsglyphrunInfo->plsrun;
				lsrun = this.FullText.StoreFrom(plsrun).GetRun(plsrun);
				int emSize = lsrun.EmSize;
				return this.CompressGlyphs(plsglyphrunInfo, (int)((double)emSize * 0.2), pplscompressionLeft, pplscompressionRight);
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetGlyphCompressionInfoFullMixed", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00135724 File Offset: 0x00134B24
		[SecurityCritical]
		private unsafe LsErr CompressGlyphs(LsGlyphRunInfo* plsglyphrunInfo, int interWordCompressTo, int** pplsCompressionLeft, int** pplsCompressionRight)
		{
			char* pwch = plsglyphrunInfo->pwch;
			ushort* rggmap = plsglyphrunInfo->rggmap;
			int cwch = plsglyphrunInfo->cwch;
			int cgindex = plsglyphrunInfo->cgindex;
			int i = 0;
			int num = (int)rggmap[i];
			while (i < cwch)
			{
				int num2 = 1;
				while (i + num2 < cwch && (int)rggmap[i + num2] == num)
				{
					num2++;
				}
				int num3 = (i + num2 == cwch) ? (cgindex - num) : ((int)rggmap[i + num2] - num);
				int j;
				for (j = 0; j < num2; j++)
				{
					ushort flags = Classification.CharAttributeOf((int)Classification.GetUnicodeClassUTF16(pwch[i + j])).Flags;
					if ((flags & 128) != 0)
					{
						break;
					}
				}
				int num4 = 0;
				for (int k = 0; k < num3; k++)
				{
					num4 += plsglyphrunInfo->rgduWidth[num + k];
					*(*(IntPtr*)pplsCompressionLeft + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsCompressionLeft + sizeof(int*) / sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsCompressionLeft + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)pplsCompressionRight + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsCompressionRight + sizeof(int*) / sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsCompressionRight + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					if (k == num3 - 1 && num2 == 1 && j < num2)
					{
						*(*(IntPtr*)pplsCompressionRight + (IntPtr)(num + k) * 4) = Math.Max(0, num4 - interWordCompressTo);
					}
				}
				i += num2;
				num += num3;
			}
			Invariant.Assert(num == cgindex);
			return LsErr.None;
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x001358A4 File Offset: 0x00134CA4
		[SecurityCritical]
		internal unsafe LsErr GetGlyphExpansionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsGlyphRunInfo* plsglyphrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplsexpansionLeft, int** pplsexpansionRight, LsExpType* plsexptype, int* pduMinInk)
		{
			LsErr result = LsErr.None;
			Plsrun plsrun = (Plsrun)2147483648U;
			LSRun lsrun = null;
			try
			{
				Invariant.Assert(maxPriorityLevel == 3);
				plsrun = plsglyphrunInfo->plsrun;
				lsrun = this.FullText.StoreFrom(plsrun).GetRun(plsrun);
				int emSize = lsrun.EmSize;
				return this.ExpandGlyphs(plsglyphrunInfo, (int)((double)emSize * 0.5), pplsexpansionLeft, pplsexpansionRight, plsexptype, LsExpType.AddWhiteSpace, ((lsrun.BidiLevel & 1) == 0) ? LsExpType.AddWhiteSpace : LsExpType.None);
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetGlyphExpansionInfoFullMixed", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x00135978 File Offset: 0x00134D78
		[SecurityCritical]
		private unsafe LsErr ExpandGlyphs(LsGlyphRunInfo* plsglyphrunInfo, int interWordExpandTo, int** pplsExpansionLeft, int** pplsExpansionRight, LsExpType* plsexptype, LsExpType interWordExpansionType, LsExpType interLetterExpansionType)
		{
			char* pwch = plsglyphrunInfo->pwch;
			ushort* rggmap = plsglyphrunInfo->rggmap;
			int cwch = plsglyphrunInfo->cwch;
			int cgindex = plsglyphrunInfo->cgindex;
			int i = 0;
			int num = (int)rggmap[i];
			while (i < cwch)
			{
				int num2 = 1;
				while (i + num2 < cwch && (int)rggmap[i + num2] == num)
				{
					num2++;
				}
				int num3 = (i + num2 == cwch) ? (cgindex - num) : ((int)rggmap[i + num2] - num);
				int j;
				for (j = 0; j < num2; j++)
				{
					ushort flags = Classification.CharAttributeOf((int)Classification.GetUnicodeClassUTF16(pwch[i + j])).Flags;
					if ((flags & 128) != 0)
					{
						break;
					}
				}
				int num4 = 0;
				for (int k = 0; k < num3; k++)
				{
					num4 += plsglyphrunInfo->rgduWidth[num + k];
					*(*(IntPtr*)pplsExpansionLeft + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsExpansionLeft + sizeof(int*) / sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsExpansionLeft + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)pplsExpansionRight + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsExpansionRight + sizeof(int*) / sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					*(*(IntPtr*)(pplsExpansionRight + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)(num + k) * 4) = 0;
					if (k == num3 - 1)
					{
						if (num2 == 1 && j < num2)
						{
							int num5 = Math.Max(0, interWordExpandTo - num4);
							*(*(IntPtr*)pplsExpansionRight + (IntPtr)(num + k) * 4) = num5;
							*(*(IntPtr*)(pplsExpansionRight + sizeof(int*) / sizeof(int*)) + (IntPtr)(num + k) * 4) = num5 * 2;
							*(*(IntPtr*)(pplsExpansionRight + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)(num + k) * 4) = this.FullText.FormatWidth;
							plsexptype[num + k] = interWordExpansionType;
						}
						else
						{
							*(*(IntPtr*)(pplsExpansionRight + (IntPtr)2 * (IntPtr)sizeof(int*) / (IntPtr)sizeof(int*)) + (IntPtr)(num + k) * 4) = this.FullText.FormatWidth;
							plsexptype[num + k] = interLetterExpansionType;
						}
					}
				}
				i += num2;
				num += num3;
			}
			Invariant.Assert(num == cgindex);
			return LsErr.None;
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x00135B68 File Offset: 0x00134F68
		[SecurityCritical]
		internal unsafe LsErr GetObjectHandlerInfo(IntPtr pols, uint objectId, void* objectInfo)
		{
			LsErr result = LsErr.None;
			try
			{
				if (objectId < 1U)
				{
					return UnsafeNativeMethods.LocbkGetObjectHandlerInfo(pols, objectId, objectInfo);
				}
				if (objectId == 1U)
				{
					Marshal.StructureToPtr(new InlineInit
					{
						pfnFormat = this.InlineFormatDelegate,
						pfnDraw = this.InlineDrawDelegate
					}, (IntPtr)objectInfo, false);
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("GetObjectHandlerInfo", (Plsrun)2147483648U, null);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x00135C28 File Offset: 0x00135028
		[SecurityCritical]
		internal LsErr InlineFormat(IntPtr pols, Plsrun plsrun, int lscpInline, int currentPosition, int rightMargin, ref ObjDim pobjDim, out int fFirstRealOnLine, out int fPenPositionUsed, out LsBrkCond breakBefore, out LsBrkCond breakAfter)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			fFirstRealOnLine = 0;
			fPenPositionUsed = 0;
			breakBefore = LsBrkCond.Please;
			breakAfter = LsBrkCond.Please;
			try
			{
				TextFormatterImp formatter = this.FullText.Formatter;
				TextStore textStore = this.FullText.StoreFrom(plsrun);
				lsrun = textStore.GetRun(plsrun);
				TextEmbeddedObject textEmbeddedObject = lsrun.TextRun as TextEmbeddedObject;
				int externalCp = textStore.GetExternalCp(lscpInline);
				fFirstRealOnLine = ((externalCp == textStore.CpFirst) ? 1 : 0);
				TextEmbeddedObjectMetrics textEmbeddedObjectMetrics = textStore.FormatTextObject(textEmbeddedObject, externalCp, currentPosition, rightMargin);
				pobjDim = default(ObjDim);
				pobjDim.dur = TextFormatterImp.RealToIdeal(textEmbeddedObjectMetrics.Width);
				pobjDim.heightsRef.dvMultiLineHeight = TextFormatterImp.RealToIdeal(textEmbeddedObjectMetrics.Height);
				pobjDim.heightsRef.dvAscent = TextFormatterImp.RealToIdeal(textEmbeddedObjectMetrics.Baseline);
				pobjDim.heightsRef.dvDescent = pobjDim.heightsRef.dvMultiLineHeight - pobjDim.heightsRef.dvAscent;
				pobjDim.heightsPres = pobjDim.heightsRef;
				breakBefore = this.BreakConditionToLsBrkCond(textEmbeddedObject.BreakBefore);
				breakAfter = this.BreakConditionToLsBrkCond(textEmbeddedObject.BreakAfter);
				fPenPositionUsed = ((!textEmbeddedObject.HasFixedSize) ? 1 : 0);
				lsrun.BaselineOffset = pobjDim.heightsRef.dvAscent;
				lsrun.Height = pobjDim.heightsRef.dvMultiLineHeight;
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("InlineFormat", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x00135DD0 File Offset: 0x001351D0
		private LsBrkCond BreakConditionToLsBrkCond(LineBreakCondition breakCondition)
		{
			switch (breakCondition)
			{
			case LineBreakCondition.BreakDesired:
				return LsBrkCond.Please;
			case LineBreakCondition.BreakPossible:
				return LsBrkCond.Can;
			case LineBreakCondition.BreakRestrained:
				return LsBrkCond.Never;
			case LineBreakCondition.BreakAlways:
				return LsBrkCond.Must;
			default:
				return LsBrkCond.Please;
			}
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x00135E00 File Offset: 0x00135200
		[SecurityCritical]
		internal LsErr InlineDraw(IntPtr pols, Plsrun plsrun, ref LSPOINT ptRun, LsTFlow textFlow, int runWidth)
		{
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
				lsrun = currentLine.GetRun(plsrun);
				LSPOINT lspoint = ptRun;
				int num = currentLine.RightToLeft ? 1 : 0;
				int num2 = (int)(lsrun.BidiLevel & 1);
				if (num != 0)
				{
					lspoint.x = -lspoint.x;
				}
				TextEmbeddedObject textEmbeddedObject = lsrun.TextRun as TextEmbeddedObject;
				if ((num ^ num2) != 0)
				{
					lspoint.x -= runWidth;
				}
				Point origin = new Point(currentLine.Formatter.IdealToReal((double)currentLine.LSLineUToParagraphU(lspoint.x), currentLine.PixelsPerDip) + this.Draw.VectorToLineOrigin.X, currentLine.Formatter.IdealToReal((double)(lspoint.y + lsrun.BaselineMoveOffset), currentLine.PixelsPerDip) + this.Draw.VectorToLineOrigin.Y);
				Rect rect = textEmbeddedObject.ComputeBoundingBox(num != 0, false);
				if (!rect.IsEmpty)
				{
					rect.X += origin.X;
					rect.Y += origin.Y;
				}
				this._boundingBox.Union(new Rect(LSRun.UVToXY(this.Draw.LineOrigin, default(Point), rect.Location.X, rect.Location.Y, currentLine), LSRun.UVToXY(this.Draw.LineOrigin, default(Point), rect.Location.X + rect.Size.Width, rect.Location.Y + rect.Size.Height, currentLine)));
				DrawingContext drawingContext = this.Draw.DrawingContext;
				if (drawingContext != null)
				{
					this.Draw.SetGuidelineY(origin.Y);
					try
					{
						if (this.Draw.AntiInversion == null)
						{
							textEmbeddedObject.Draw(drawingContext, LSRun.UVToXY(this.Draw.LineOrigin, default(Point), origin.X, origin.Y, currentLine), num != 0, false);
						}
						else
						{
							drawingContext.PushTransform(this.Draw.AntiInversion);
							try
							{
								textEmbeddedObject.Draw(drawingContext, origin, num != 0, false);
							}
							finally
							{
								drawingContext.Pop();
							}
						}
					}
					finally
					{
						this.Draw.UnsetGuidelineY();
					}
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("InlineDraw", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x00136104 File Offset: 0x00135504
		[SecurityCritical]
		internal unsafe LsErr EnumText(IntPtr pols, Plsrun plsrun, int cpFirst, int dcp, char* pwchText, int cchText, LsTFlow lstFlow, int fReverseOrder, int fGeometryProvided, ref LSPOINT pptStart, ref LsHeights pheights, int dupRun, int glyphBaseRun, int* piCharAdvances, ushort* puClusterMap, ushort* characterProperties, ushort* puGlyphs, int* piJustifiedGlyphAdvances, GlyphOffset* piiGlyphOffsets, uint* piGlyphProperties, int glyphCount)
		{
			if (cpFirst < 0)
			{
				return LsErr.None;
			}
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
				lsrun = currentLine.GetRun(plsrun);
				GlyphRun glyphRun = null;
				if (glyphBaseRun != 0)
				{
					if (glyphCount > 0)
					{
						glyphRun = this.ComputeShapedGlyphRun(lsrun, currentLine.Formatter, false, pptStart, cchText, pwchText, puClusterMap, glyphCount, puGlyphs, piJustifiedGlyphAdvances, piiGlyphOffsets, currentLine.IsJustified);
					}
				}
				else if (cchText > 0)
				{
					dupRun = 0;
					for (int i = 0; i < cchText; i++)
					{
						dupRun += piCharAdvances[i];
					}
					glyphRun = this.ComputeUnshapedGlyphRun(lsrun, lstFlow, currentLine.Formatter, false, pptStart, dupRun, cchText, pwchText, piCharAdvances, currentLine.IsJustified);
				}
				if (glyphRun != null)
				{
					this.IndexedGlyphRuns.Add(new IndexedGlyphRun(currentLine.GetExternalCp(cpFirst), dcp, glyphRun));
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("EnumText", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x00136234 File Offset: 0x00135634
		[SecurityCritical]
		internal unsafe LsErr EnumTab(IntPtr pols, Plsrun plsrun, int cpFirst, char* pwchText, char tabLeader, LsTFlow lstFlow, int fReverseOrder, int fGeometryProvided, ref LSPOINT pptStart, ref LsHeights heights, int dupRun)
		{
			if (cpFirst < 0)
			{
				return LsErr.None;
			}
			LsErr result = LsErr.None;
			LSRun lsrun = null;
			try
			{
				TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
				lsrun = currentLine.GetRun(plsrun);
				GlyphRun glyphRun = null;
				if (lsrun.Type == Plsrun.Text)
				{
					int dupRun2 = 0;
					lsrun.Shapeable.GetAdvanceWidthsUnshaped(&tabLeader, 1, TextFormatterImp.ToIdeal, &dupRun2);
					glyphRun = this.ComputeUnshapedGlyphRun(lsrun, lstFlow, currentLine.Formatter, false, pptStart, dupRun2, 1, &tabLeader, &dupRun2, currentLine.IsJustified);
				}
				if (glyphRun != null)
				{
					this.IndexedGlyphRuns.Add(new IndexedGlyphRun(currentLine.GetExternalCp(cpFirst), 1, glyphRun));
				}
			}
			catch (Exception e)
			{
				this.SaveException(e, plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			catch
			{
				this.SaveNonCLSException("EnumTab", plsrun, lsrun);
				result = LsErr.ClientAbort;
			}
			return result;
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x00136328 File Offset: 0x00135728
		private bool IsSpace(char ch)
		{
			return ch == '\t' || ch == ' ';
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x00136344 File Offset: 0x00135744
		private static int RealToIdeal(double i)
		{
			return TextFormatterImp.RealToIdeal(i);
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x00136358 File Offset: 0x00135758
		private static double RoundDipForDisplayModeJustifiedText(double value, double pixelsPerDip)
		{
			return TextFormatterImp.RoundDipForDisplayModeJustifiedText(value, pixelsPerDip);
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x0013636C File Offset: 0x0013576C
		private static double IdealToRealWithNoRounding(double i)
		{
			return TextFormatterImp.IdealToRealWithNoRounding(i);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x00136380 File Offset: 0x00135780
		[SecurityCritical]
		private unsafe void AdjustMetricsForDisplayModeJustifiedText(char* pwchText, int* piGlyphAdvances, int glyphCount, bool isRightToLeft, int idealBaselineOriginX, int idealBaselineOriginY, double pixelsPerDip, out Point baselineOrigin, out IList<double> adjustedAdvanceWidths)
		{
			adjustedAdvanceWidths = new double[glyphCount];
			baselineOrigin = new Point(LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(LineServicesCallbacks.IdealToRealWithNoRounding((double)idealBaselineOriginX), pixelsPerDip), LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(LineServicesCallbacks.IdealToRealWithNoRounding((double)idealBaselineOriginY), pixelsPerDip));
			int num = LineServicesCallbacks.RealToIdeal(baselineOrigin.X);
			int num2 = idealBaselineOriginX - num;
			if (isRightToLeft)
			{
				num2 *= -1;
			}
			if (glyphCount > 0)
			{
				double num3 = 0.0;
				int num4 = num2;
				double num5 = 0.0;
				int num6 = -1;
				for (int i = 0; i < glyphCount; i++)
				{
					if (this.IsSpace(pwchText[i]))
					{
						num6 = i;
					}
					num4 += piGlyphAdvances[i];
					double value = LineServicesCallbacks.IdealToRealWithNoRounding((double)num4);
					double value2 = LineServicesCallbacks.IdealToRealWithNoRounding((double)piGlyphAdvances[i]);
					double num7 = LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(value2, pixelsPerDip);
					num3 += num7;
					num5 += LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(num3 - LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(value, pixelsPerDip), pixelsPerDip);
					adjustedAdvanceWidths[i] = num7;
					if (num6 >= 0)
					{
						IList<double> list = adjustedAdvanceWidths;
						int index = num6;
						list[index] -= num5;
						num3 -= num5;
						num5 = 0.0;
					}
				}
				if (num6 < 0)
				{
					num3 = 0.0;
					num4 = num2;
					for (int j = 0; j < glyphCount; j++)
					{
						num4 += piGlyphAdvances[j];
						double value = LineServicesCallbacks.IdealToRealWithNoRounding((double)num4);
						double value2 = LineServicesCallbacks.IdealToRealWithNoRounding((double)piGlyphAdvances[j]);
						double num7 = LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(value2, pixelsPerDip);
						num3 += num7;
						num5 = LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(num3 - LineServicesCallbacks.RoundDipForDisplayModeJustifiedText(value, pixelsPerDip), pixelsPerDip);
						adjustedAdvanceWidths[j] = num7 - num5;
						num3 -= num5;
					}
				}
			}
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x00136570 File Offset: 0x00135970
		[SecurityCritical]
		private unsafe GlyphRun ComputeShapedGlyphRun(LSRun lsrun, TextFormatterImp textFormatterImp, bool originProvided, LSPOINT lsrunOrigin, int charCount, char* pwchText, ushort* puClusterMap, int glyphCount, ushort* puGlyphs, int* piJustifiedGlyphAdvances, GlyphOffset* piiGlyphOffsets, bool justify)
		{
			TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
			Point origin = default(Point);
			int idealBaselineOriginX = 0;
			int idealBaselineOriginY = 0;
			if (originProvided)
			{
				if (currentLine.RightToLeft)
				{
					lsrunOrigin.x = -lsrunOrigin.x;
				}
				if (textFormatterImp.TextFormattingMode == TextFormattingMode.Display && justify)
				{
					LSRun.UVToNominalXY(this.Draw.LineOrigin, this.Draw.VectorToLineOrigin, currentLine.LSLineUToParagraphU(lsrunOrigin.x), lsrunOrigin.y + lsrun.BaselineMoveOffset, currentLine, out idealBaselineOriginX, out idealBaselineOriginY);
				}
				else
				{
					origin = LSRun.UVToXY(this.Draw.LineOrigin, this.Draw.VectorToLineOrigin, currentLine.LSLineUToParagraphU(lsrunOrigin.x), lsrunOrigin.y + lsrun.BaselineMoveOffset, currentLine);
				}
			}
			char[] array = new char[charCount];
			ushort[] array2 = new ushort[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = pwchText[i];
				array2[i] = puClusterMap[i];
			}
			ushort[] array3 = new ushort[glyphCount];
			bool flag = (lsrun.BidiLevel & 1) > 0;
			IList<double> list;
			IList<Point> list2;
			if (textFormatterImp.TextFormattingMode == TextFormattingMode.Ideal)
			{
				list = new ThousandthOfEmRealDoubles(textFormatterImp.IdealToReal((double)lsrun.EmSize, currentLine.PixelsPerDip), glyphCount);
				list2 = new ThousandthOfEmRealPoints(textFormatterImp.IdealToReal((double)lsrun.EmSize, currentLine.PixelsPerDip), glyphCount);
				for (int j = 0; j < glyphCount; j++)
				{
					array3[j] = puGlyphs[j];
					list[j] = textFormatterImp.IdealToReal((double)piJustifiedGlyphAdvances[j], currentLine.PixelsPerDip);
					list2[j] = new Point(textFormatterImp.IdealToReal((double)piiGlyphOffsets[j].du, currentLine.PixelsPerDip), textFormatterImp.IdealToReal((double)piiGlyphOffsets[j].dv, currentLine.PixelsPerDip));
				}
			}
			else
			{
				if (justify)
				{
					this.AdjustMetricsForDisplayModeJustifiedText(pwchText, piJustifiedGlyphAdvances, glyphCount, flag, idealBaselineOriginX, idealBaselineOriginY, currentLine.PixelsPerDip, out origin, out list);
				}
				else
				{
					list = new List<double>(glyphCount);
					for (int k = 0; k < glyphCount; k++)
					{
						list.Add(textFormatterImp.IdealToReal((double)piJustifiedGlyphAdvances[k], currentLine.PixelsPerDip));
					}
				}
				list2 = new List<Point>(glyphCount);
				for (int l = 0; l < glyphCount; l++)
				{
					array3[l] = puGlyphs[l];
					list2.Add(new Point(textFormatterImp.IdealToReal((double)piiGlyphOffsets[l].du, currentLine.PixelsPerDip), textFormatterImp.IdealToReal((double)piiGlyphOffsets[l].dv, currentLine.PixelsPerDip)));
				}
			}
			return lsrun.Shapeable.ComputeShapedGlyphRun(origin, array, array2, array3, list, list2, flag, false);
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x00136830 File Offset: 0x00135C30
		[SecurityCritical]
		private unsafe GlyphRun ComputeUnshapedGlyphRun(LSRun lsrun, LsTFlow textFlow, TextFormatterImp textFormatterImp, bool originProvided, LSPOINT lsrunOrigin, int dupRun, int cchText, char* pwchText, int* piCharAdvances, bool justify)
		{
			GlyphRun result = null;
			if (lsrun.Type == Plsrun.Text)
			{
				Point origin = default(Point);
				int idealBaselineOriginX = 0;
				int idealBaselineOriginY = 0;
				if (originProvided)
				{
					TextMetrics.FullTextLine currentLine = this.Draw.CurrentLine;
					if (textFlow == LsTFlow.lstflowWS)
					{
						lsrunOrigin.x -= dupRun;
					}
					if (currentLine.RightToLeft)
					{
						lsrunOrigin.x = -lsrunOrigin.x;
					}
					if (textFormatterImp.TextFormattingMode == TextFormattingMode.Display && justify)
					{
						LSRun.UVToNominalXY(this.Draw.LineOrigin, this.Draw.VectorToLineOrigin, currentLine.LSLineUToParagraphU(lsrunOrigin.x), lsrunOrigin.y + lsrun.BaselineMoveOffset, currentLine, out idealBaselineOriginX, out idealBaselineOriginY);
					}
					else
					{
						origin = LSRun.UVToXY(this.Draw.LineOrigin, this.Draw.VectorToLineOrigin, currentLine.LSLineUToParagraphU(lsrunOrigin.x), lsrunOrigin.y + lsrun.BaselineMoveOffset, currentLine);
					}
				}
				char[] array = new char[cchText];
				bool isRightToLeft = (lsrun.BidiLevel & 1) > 0;
				IList<double> list;
				if (textFormatterImp.TextFormattingMode == TextFormattingMode.Ideal)
				{
					list = new ThousandthOfEmRealDoubles(textFormatterImp.IdealToReal((double)lsrun.EmSize, this.Draw.CurrentLine.PixelsPerDip), cchText);
					for (int i = 0; i < cchText; i++)
					{
						array[i] = pwchText[i];
						list[i] = textFormatterImp.IdealToReal((double)piCharAdvances[i], this.Draw.CurrentLine.PixelsPerDip);
					}
				}
				else
				{
					if (justify)
					{
						this.AdjustMetricsForDisplayModeJustifiedText(pwchText, piCharAdvances, cchText, isRightToLeft, idealBaselineOriginX, idealBaselineOriginY, this.Draw.CurrentLine.PixelsPerDip, out origin, out list);
					}
					else
					{
						list = new List<double>(cchText);
						for (int j = 0; j < cchText; j++)
						{
							list.Add(textFormatterImp.IdealToReal((double)piCharAdvances[j], this.Draw.CurrentLine.PixelsPerDip));
						}
					}
					for (int k = 0; k < cchText; k++)
					{
						array[k] = pwchText[k];
					}
				}
				result = lsrun.Shapeable.ComputeUnshapedGlyphRun(origin, array, list);
			}
			return result;
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x00136A38 File Offset: 0x00135E38
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal LineServicesCallbacks()
		{
			this._pfnFetchRunRedefined = new FetchRunRedefined(this.FetchRunRedefined);
			this._pfnFetchLineProps = new FetchLineProps(this.FetchLineProps);
			this._pfnFetchPap = new FetchPap(this.FetchPap);
			this._pfnGetRunTextMetrics = new GetRunTextMetrics(this.GetRunTextMetrics);
			this._pfnGetRunCharWidths = new GetRunCharWidths(this.GetRunCharWidths);
			this._pfnGetDurMaxExpandRagged = new GetDurMaxExpandRagged(this.GetDurMaxExpandRagged);
			this._pfnDrawTextRun = new DrawTextRun(this.DrawTextRun);
			this._pfnGetGlyphsRedefined = new GetGlyphsRedefined(this.GetGlyphsRedefined);
			this._pfnGetGlyphPositions = new GetGlyphPositions(this.GetGlyphPositions);
			this._pfnGetAutoNumberInfo = new GetAutoNumberInfo(this.GetAutoNumberInfo);
			this._pfnDrawGlyphs = new DrawGlyphs(this.DrawGlyphs);
			this._pfnGetObjectHandlerInfo = new GetObjectHandlerInfo(this.GetObjectHandlerInfo);
			this._pfnGetRunUnderlineInfo = new GetRunUnderlineInfo(this.GetRunUnderlineInfo);
			this._pfnGetRunStrikethroughInfo = new GetRunStrikethroughInfo(this.GetRunStrikethroughInfo);
			this._pfnHyphenate = new Hyphenate(this.Hyphenate);
			this._pfnGetNextHyphenOpp = new GetNextHyphenOpp(this.GetNextHyphenOpp);
			this._pfnGetPrevHyphenOpp = new GetPrevHyphenOpp(this.GetPrevHyphenOpp);
			this._pfnDrawUnderline = new DrawUnderline(this.DrawUnderline);
			this._pfnDrawStrikethrough = new DrawStrikethrough(this.DrawStrikethrough);
			this._pfnFInterruptShaping = new FInterruptShaping(this.FInterruptShaping);
			this._pfnGetCharCompressionInfoFullMixed = new GetCharCompressionInfoFullMixed(this.GetCharCompressionInfoFullMixed);
			this._pfnGetCharExpansionInfoFullMixed = new GetCharExpansionInfoFullMixed(this.GetCharExpansionInfoFullMixed);
			this._pfnGetGlyphCompressionInfoFullMixed = new GetGlyphCompressionInfoFullMixed(this.GetGlyphCompressionInfoFullMixed);
			this._pfnGetGlyphExpansionInfoFullMixed = new GetGlyphExpansionInfoFullMixed(this.GetGlyphExpansionInfoFullMixed);
			this._pfnEnumText = new EnumText(this.EnumText);
			this._pfnEnumTab = new EnumTab(this.EnumTab);
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x00136C20 File Offset: 0x00136020
		[SecurityCritical]
		internal void PopulateContextInfo(ref LsContextInfo contextInfo, ref LscbkRedefined lscbkRedef)
		{
			lscbkRedef.pfnFetchRunRedefined = this._pfnFetchRunRedefined;
			lscbkRedef.pfnGetGlyphsRedefined = this._pfnGetGlyphsRedefined;
			lscbkRedef.pfnFetchLineProps = this._pfnFetchLineProps;
			contextInfo.pfnFetchLineProps = this._pfnFetchLineProps;
			contextInfo.pfnFetchPap = this._pfnFetchPap;
			contextInfo.pfnGetRunTextMetrics = this._pfnGetRunTextMetrics;
			contextInfo.pfnGetRunCharWidths = this._pfnGetRunCharWidths;
			contextInfo.pfnGetDurMaxExpandRagged = this._pfnGetDurMaxExpandRagged;
			contextInfo.pfnDrawTextRun = this._pfnDrawTextRun;
			contextInfo.pfnGetGlyphPositions = this._pfnGetGlyphPositions;
			contextInfo.pfnGetAutoNumberInfo = this._pfnGetAutoNumberInfo;
			contextInfo.pfnDrawGlyphs = this._pfnDrawGlyphs;
			contextInfo.pfnGetObjectHandlerInfo = this._pfnGetObjectHandlerInfo;
			contextInfo.pfnGetRunUnderlineInfo = this._pfnGetRunUnderlineInfo;
			contextInfo.pfnGetRunStrikethroughInfo = this._pfnGetRunStrikethroughInfo;
			contextInfo.pfnHyphenate = this._pfnHyphenate;
			contextInfo.pfnGetNextHyphenOpp = this._pfnGetNextHyphenOpp;
			contextInfo.pfnGetPrevHyphenOpp = this._pfnGetPrevHyphenOpp;
			contextInfo.pfnDrawUnderline = this._pfnDrawUnderline;
			contextInfo.pfnDrawStrikethrough = this._pfnDrawStrikethrough;
			contextInfo.pfnFInterruptShaping = this._pfnFInterruptShaping;
			contextInfo.pfnGetCharCompressionInfoFullMixed = this._pfnGetCharCompressionInfoFullMixed;
			contextInfo.pfnGetCharExpansionInfoFullMixed = this._pfnGetCharExpansionInfoFullMixed;
			contextInfo.pfnGetGlyphCompressionInfoFullMixed = this._pfnGetGlyphCompressionInfoFullMixed;
			contextInfo.pfnGetGlyphExpansionInfoFullMixed = this._pfnGetGlyphExpansionInfoFullMixed;
			contextInfo.pfnEnumText = this._pfnEnumText;
			contextInfo.pfnEnumTab = this._pfnEnumTab;
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06004E63 RID: 20067 RVA: 0x00136D74 File Offset: 0x00136174
		internal InlineFormat InlineFormatDelegate
		{
			[SecurityCritical]
			get
			{
				if (this._pfnInlineFormat == null)
				{
					this._pfnInlineFormat = new InlineFormat(this.InlineFormat);
				}
				return this._pfnInlineFormat;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06004E64 RID: 20068 RVA: 0x00136DA4 File Offset: 0x001361A4
		internal InlineDraw InlineDrawDelegate
		{
			[SecurityCritical]
			get
			{
				if (this._pfnInlineDraw == null)
				{
					this._pfnInlineDraw = new InlineDraw(this.InlineDraw);
				}
				return this._pfnInlineDraw;
			}
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x00136DD4 File Offset: 0x001361D4
		[SecurityCritical]
		private void SaveException(Exception e, Plsrun plsrun, LSRun lsrun)
		{
			e.Data["ExceptionContext"] = new LineServicesCallbacks.ExceptionContext(e.Data["ExceptionContext"], e.StackTrace, plsrun, lsrun);
			this._exception = e;
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x00136E18 File Offset: 0x00136218
		[SecurityCritical]
		private void SaveNonCLSException(string methodName, Plsrun plsrun, LSRun lsrun)
		{
			Exception ex = new Exception(SR.Get("NonCLSException"));
			ex.Data["ExceptionContext"] = new LineServicesCallbacks.ExceptionContext(null, methodName, plsrun, lsrun);
			this._exception = ex;
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x00136E58 File Offset: 0x00136258
		// (set) Token: 0x06004E68 RID: 20072 RVA: 0x00136E6C File Offset: 0x0013626C
		internal Exception Exception
		{
			[SecurityCritical]
			get
			{
				return this._exception;
			}
			[SecurityCritical]
			set
			{
				this._exception = value;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x00136E80 File Offset: 0x00136280
		// (set) Token: 0x06004E6A RID: 20074 RVA: 0x00136E94 File Offset: 0x00136294
		internal object Owner
		{
			[SecurityCritical]
			get
			{
				return this._owner;
			}
			[SecurityCritical]
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x00136EA8 File Offset: 0x001362A8
		private FullTextState FullText
		{
			[SecurityCritical]
			get
			{
				return this._owner as FullTextState;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x00136EC0 File Offset: 0x001362C0
		private DrawingState Draw
		{
			[SecurityCritical]
			get
			{
				return this._owner as DrawingState;
			}
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x00136ED8 File Offset: 0x001362D8
		internal void EmptyBoundingBox()
		{
			this._boundingBox = Rect.Empty;
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x00136EF0 File Offset: 0x001362F0
		internal Rect BoundingBox
		{
			get
			{
				return this._boundingBox;
			}
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x00136F04 File Offset: 0x00136304
		internal void ClearIndexedGlyphRuns()
		{
			this._indexedGlyphRuns = null;
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x00136F18 File Offset: 0x00136318
		internal ICollection<IndexedGlyphRun> IndexedGlyphRuns
		{
			get
			{
				if (this._indexedGlyphRuns == null)
				{
					this._indexedGlyphRuns = new List<IndexedGlyphRun>(8);
				}
				return this._indexedGlyphRuns;
			}
		}

		// Token: 0x0400238F RID: 9103
		[SecurityCritical]
		private FetchRunRedefined _pfnFetchRunRedefined;

		// Token: 0x04002390 RID: 9104
		[SecurityCritical]
		private FetchLineProps _pfnFetchLineProps;

		// Token: 0x04002391 RID: 9105
		[SecurityCritical]
		private FetchPap _pfnFetchPap;

		// Token: 0x04002392 RID: 9106
		[SecurityCritical]
		private GetRunTextMetrics _pfnGetRunTextMetrics;

		// Token: 0x04002393 RID: 9107
		[SecurityCritical]
		private GetRunCharWidths _pfnGetRunCharWidths;

		// Token: 0x04002394 RID: 9108
		[SecurityCritical]
		private GetDurMaxExpandRagged _pfnGetDurMaxExpandRagged;

		// Token: 0x04002395 RID: 9109
		[SecurityCritical]
		private GetAutoNumberInfo _pfnGetAutoNumberInfo;

		// Token: 0x04002396 RID: 9110
		[SecurityCritical]
		private DrawTextRun _pfnDrawTextRun;

		// Token: 0x04002397 RID: 9111
		[SecurityCritical]
		private GetGlyphsRedefined _pfnGetGlyphsRedefined;

		// Token: 0x04002398 RID: 9112
		[SecurityCritical]
		private GetGlyphPositions _pfnGetGlyphPositions;

		// Token: 0x04002399 RID: 9113
		[SecurityCritical]
		private DrawGlyphs _pfnDrawGlyphs;

		// Token: 0x0400239A RID: 9114
		[SecurityCritical]
		private GetObjectHandlerInfo _pfnGetObjectHandlerInfo;

		// Token: 0x0400239B RID: 9115
		[SecurityCritical]
		private GetRunUnderlineInfo _pfnGetRunUnderlineInfo;

		// Token: 0x0400239C RID: 9116
		[SecurityCritical]
		private GetRunStrikethroughInfo _pfnGetRunStrikethroughInfo;

		// Token: 0x0400239D RID: 9117
		[SecurityCritical]
		private Hyphenate _pfnHyphenate;

		// Token: 0x0400239E RID: 9118
		[SecurityCritical]
		private GetNextHyphenOpp _pfnGetNextHyphenOpp;

		// Token: 0x0400239F RID: 9119
		[SecurityCritical]
		private GetPrevHyphenOpp _pfnGetPrevHyphenOpp;

		// Token: 0x040023A0 RID: 9120
		[SecurityCritical]
		private DrawUnderline _pfnDrawUnderline;

		// Token: 0x040023A1 RID: 9121
		[SecurityCritical]
		private DrawStrikethrough _pfnDrawStrikethrough;

		// Token: 0x040023A2 RID: 9122
		[SecurityCritical]
		private FInterruptShaping _pfnFInterruptShaping;

		// Token: 0x040023A3 RID: 9123
		[SecurityCritical]
		private GetCharCompressionInfoFullMixed _pfnGetCharCompressionInfoFullMixed;

		// Token: 0x040023A4 RID: 9124
		[SecurityCritical]
		private GetCharExpansionInfoFullMixed _pfnGetCharExpansionInfoFullMixed;

		// Token: 0x040023A5 RID: 9125
		[SecurityCritical]
		private GetGlyphCompressionInfoFullMixed _pfnGetGlyphCompressionInfoFullMixed;

		// Token: 0x040023A6 RID: 9126
		[SecurityCritical]
		private GetGlyphExpansionInfoFullMixed _pfnGetGlyphExpansionInfoFullMixed;

		// Token: 0x040023A7 RID: 9127
		[SecurityCritical]
		private EnumText _pfnEnumText;

		// Token: 0x040023A8 RID: 9128
		[SecurityCritical]
		private EnumTab _pfnEnumTab;

		// Token: 0x040023A9 RID: 9129
		[SecurityCritical]
		private InlineFormat _pfnInlineFormat;

		// Token: 0x040023AA RID: 9130
		[SecurityCritical]
		private InlineDraw _pfnInlineDraw;

		// Token: 0x040023AB RID: 9131
		[SecurityCritical]
		private Exception _exception;

		// Token: 0x040023AC RID: 9132
		[SecurityCritical]
		private object _owner;

		// Token: 0x040023AD RID: 9133
		private Rect _boundingBox;

		// Token: 0x040023AE RID: 9134
		private ICollection<IndexedGlyphRun> _indexedGlyphRuns;

		// Token: 0x020009DE RID: 2526
		[Serializable]
		private class ExceptionContext
		{
			// Token: 0x06005B7C RID: 23420 RVA: 0x001700E4 File Offset: 0x0016F4E4
			public ExceptionContext(object innerContext, string stackTraceOrMethodName, Plsrun plsrun, LSRun lsrun)
			{
				this._stackTraceOrMethodName = stackTraceOrMethodName;
				this._plsrun = (uint)plsrun;
				this._lsrun = lsrun;
				this._innerContext = innerContext;
			}

			// Token: 0x06005B7D RID: 23421 RVA: 0x00170114 File Offset: 0x0016F514
			public override string ToString()
			{
				return this._stackTraceOrMethodName;
			}

			// Token: 0x04002EA7 RID: 11943
			public const string Key = "ExceptionContext";

			// Token: 0x04002EA8 RID: 11944
			private object _innerContext;

			// Token: 0x04002EA9 RID: 11945
			private string _stackTraceOrMethodName;

			// Token: 0x04002EAA RID: 11946
			private uint _plsrun;

			// Token: 0x04002EAB RID: 11947
			[NonSerialized]
			private LSRun _lsrun;
		}
	}
}
