using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000022 RID: 34
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	internal class TextItemizer
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000BF90 File Offset: 0x0000B390
		private unsafe uint GetNextSmallestPos(DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>** ppScriptAnalysisCurrent, uint* scriptAnalysisRangeIndex, DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>** ppNumberSubstitutionCurrent, uint* numberSubstitutionRangeIndex, uint* isDigitIndex, uint* isDigitRangeIndex)
		{
			ulong num = (ulong)(*(long*)ppScriptAnalysisCurrent);
			uint num2;
			if (num != 0UL)
			{
				num2 = (uint)(*(((ulong)(*scriptAnalysisRangeIndex) + 2UL) * 4UL + num));
			}
			else
			{
				num2 = uint.MaxValue;
			}
			ulong num3 = (ulong)(*(long*)ppNumberSubstitutionCurrent);
			uint num4;
			if (num3 != 0UL)
			{
				num4 = (uint)(*(((ulong)(*numberSubstitutionRangeIndex) + 2UL) * 4UL + num3));
			}
			else
			{
				num4 = uint.MaxValue;
			}
			uint num5 = (uint)(*isDigitIndex);
			uint val;
			if (num5 < (uint)this._isDigitListRanges.Count)
			{
				val = this._isDigitListRanges[(int)num5][*isDigitRangeIndex];
			}
			else
			{
				val = uint.MaxValue;
			}
			uint num6 = Math.Min(num2, num4);
			num6 = Math.Min(num6, val);
			if (num6 == num2)
			{
				if ((*scriptAnalysisRangeIndex + 1 & -2) == 2)
				{
					*(long*)ppScriptAnalysisCurrent = *(*(long*)ppScriptAnalysisCurrent + 16L);
				}
				*scriptAnalysisRangeIndex = (*scriptAnalysisRangeIndex - 1 & 1);
			}
			else if (num6 == num4)
			{
				if ((*numberSubstitutionRangeIndex + 1 & -2) == 2)
				{
					*(long*)ppNumberSubstitutionCurrent = *(*(long*)ppNumberSubstitutionCurrent + 16L);
				}
				*numberSubstitutionRangeIndex = (*numberSubstitutionRangeIndex - 1 & 1);
			}
			else
			{
				*isDigitIndex += (int)((uint)(*isDigitRangeIndex + 1) >> 1);
				*isDigitRangeIndex = (*isDigitRangeIndex - 1 & 1);
			}
			return num6;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000BF58 File Offset: 0x0000B358
		public unsafe TextItemizer(DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>* pScriptAnalysisListHead, DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>* pNumberSubstitutionListHead)
		{
			this._pScriptAnalysisListHead = pScriptAnalysisListHead;
			this._pNumberSubstitutionListHead = pNumberSubstitutionListHead;
			this._isDigitList = new List<bool>();
			this._isDigitListRanges = new List<uint[]>();
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011C2C File Offset: 0x0001102C
		[SecurityCritical]
		public unsafe IList<Span> Itemize(CultureInfo numberCulture, byte* pCharAttribute, uint textLength)
		{
			DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>* pScriptAnalysisListHead = this._pScriptAnalysisListHead;
			uint num = 0U;
			DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>* pNumberSubstitutionListHead = this._pNumberSubstitutionListHead;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			uint nextSmallestPos = this.GetNextSmallestPos(&pScriptAnalysisListHead, ref num, &pNumberSubstitutionListHead, ref num2, ref num3, ref num4);
			List<Span> list = new List<Span>();
			if (nextSmallestPos != textLength)
			{
				while (pScriptAnalysisListHead != null || pNumberSubstitutionListHead != null || num3 < (uint)this._isDigitList.Count)
				{
					uint num5 = nextSmallestPos;
					DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>* scriptAnalysis;
					DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>* ptr;
					uint index;
					do
					{
						scriptAnalysis = pScriptAnalysisListHead;
						ptr = pNumberSubstitutionListHead;
						index = num3;
						nextSmallestPos = this.GetNextSmallestPos(&pScriptAnalysisListHead, ref num, &pNumberSubstitutionListHead, ref num2, ref num3, ref num4);
					}
					while (nextSmallestPos == num5);
					IDWriteNumberSubstitution* numberSubstitution = null;
					if (ptr != null && nextSmallestPos > (uint)(*(int*)(ptr + 8L / (long)sizeof(DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>))) && nextSmallestPos <= (uint)(*(int*)(ptr + 12L / (long)sizeof(DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>))))
					{
						numberSubstitution = *(long*)ptr;
					}
					bool hasCombiningMark = false;
					uint num6 = num5;
					if (num5 < nextSmallestPos)
					{
						byte* ptr2 = num5 + pCharAttribute;
						while ((*ptr2 & 1) == 0)
						{
							num6 += 1U;
							ptr2 += 1L;
							if (num6 >= nextSmallestPos)
							{
								goto IL_CB;
							}
						}
						hasCombiningMark = true;
					}
					IL_CB:
					bool needsCaretInfo = true;
					uint num7 = num5;
					if (num5 < nextSmallestPos)
					{
						byte* ptr3 = num5 + pCharAttribute;
						for (;;)
						{
							byte b = *ptr3;
							if ((b & 16) != 0 && (b & 2) == 0)
							{
								break;
							}
							num7 += 1U;
							ptr3 += 1L;
							if (num7 >= nextSmallestPos)
							{
								goto IL_106;
							}
						}
						needsCaretInfo = false;
					}
					IL_106:
					int num8 = 0;
					int num9 = 0;
					int num10 = 0;
					bool flag = false;
					if (num5 >= nextSmallestPos)
					{
						goto IL_174;
					}
					byte* ptr4 = num5 + pCharAttribute;
					uint num11 = nextSmallestPos - num5;
					do
					{
						byte b2 = *ptr4;
						flag = ((b2 & 32) != 0 || flag);
						if ((b2 & 16) != 0)
						{
							num8++;
							if ((b2 & 8) != 0)
							{
								num9++;
							}
							else if ((b2 & 4) != 0)
							{
								num10++;
							}
						}
						ptr4 += 1L;
						num11 += uint.MaxValue;
					}
					while (num11 > 0U);
					if (num10 <= 0)
					{
						goto IL_174;
					}
					int num12 = 1;
					IL_177:
					bool isIndic = (byte)num12 != 0;
					int num13;
					if (num8 > 0 && num9 == num8)
					{
						num13 = 1;
					}
					else
					{
						num13 = 0;
					}
					bool isLatin = (byte)num13 != 0;
					CultureInfo digitCulture;
					if (this._isDigitList[(int)index])
					{
						digitCulture = numberCulture;
					}
					else
					{
						digitCulture = null;
					}
					ItemProps element = ItemProps.Create((void*)scriptAnalysis, (void*)numberSubstitution, digitCulture, hasCombiningMark, needsCaretInfo, flag, isIndic, isLatin);
					list.Add(new Span(element, (int)(nextSmallestPos - num5)));
					if (nextSmallestPos == textLength)
					{
						break;
					}
					continue;
					IL_174:
					num12 = 0;
					goto IL_177;
				}
			}
			return list;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C068 File Offset: 0x0000B468
		public void SetIsDigit(uint textPosition, uint textLength, [MarshalAs(UnmanagedType.U1)] bool isDigit)
		{
			this._isDigitList.Add(isDigit);
			uint[] item = new uint[]
			{
				textPosition,
				textPosition + textLength
			};
			this._isDigitListRanges.Add(item);
		}

		// Token: 0x04000334 RID: 820
		private unsafe DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>* _pScriptAnalysisListHead;

		// Token: 0x04000335 RID: 821
		private unsafe DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>* _pNumberSubstitutionListHead;

		// Token: 0x04000336 RID: 822
		private List<bool> _isDigitList;

		// Token: 0x04000337 RID: 823
		private List<uint[]> _isDigitListRanges;
	}
}
