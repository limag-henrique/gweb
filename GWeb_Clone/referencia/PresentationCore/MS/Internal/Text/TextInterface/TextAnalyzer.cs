using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Media;
using MS.Internal.Text.TextInterface.Generics;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x0200002C RID: 44
	internal sealed class TextAnalyzer
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00010FB4 File Offset: 0x000103B4
		[SecurityCritical]
		private unsafe void GetBlankGlyphsForControlCharacters(ushort* pTextString, uint textLength, FontFace fontFace, ushort blankGlyphIndex, uint maxGlyphCount, ushort* clusterMap, ushort* glyphIndices, int* pfCanGlyphAlone, out uint actualGlyphCount)
		{
			actualGlyphCount = textLength;
			if (maxGlyphCount >= textLength)
			{
				if (textLength > 65535U)
				{
					Util.ConvertHresultToException(-2147024809);
				}
				ushort num = (ushort)textLength;
				uint num2 = 45U;
				ushort num3 = 0;
				ushort num4 = 0;
				if (0 < num)
				{
					do
					{
						long num5 = (long)((ulong)num4);
						long num6 = num5 * 2L;
						if (*(ushort*)(num6 / (long)sizeof(ushort) + pTextString) == 45)
						{
							if (num3 == 0)
							{
								IDWriteFontFace* dwriteFontFaceNoAddRef = fontFace.DWriteFontFaceNoAddRef;
								int hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.UInt16*), dwriteFontFaceNoAddRef, ref num2, 1, ref num3, *(*(long*)dwriteFontFaceNoAddRef + 88L));
								GC.KeepAlive(fontFace);
								Util.ConvertHresultToException(hr);
							}
							(num6 / 2L)[glyphIndices] = num3;
						}
						else
						{
							(num6 / 2L)[glyphIndices] = blankGlyphIndex;
						}
						(num6 / 2L)[clusterMap] = num4;
						(num5 * 4L / 4L)[pfCanGlyphAlone] = 1;
						num4 += 1;
					}
					while (num4 < num);
				}
				GC.KeepAlive(this);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000124CC File Offset: 0x000118CC
		[SecurityCritical]
		private unsafe void GetGlyphPlacementsForControlCharacters(ushort* pTextString, uint textLength, Font font, TextFormattingMode textFormattingMode, double fontEmSize, double scalingFactor, [MarshalAs(UnmanagedType.U1)] bool isSideways, float pixelsPerDip, uint glyphCount, ushort* pGlyphIndices, int* glyphAdvances, out GlyphOffset[] glyphOffsets)
		{
			if (glyphCount != textLength)
			{
				Util.ConvertHresultToException(-2147024809);
			}
			glyphOffsets = new GlyphOffset[textLength];
			FontFace fontFace = font.GetFontFace();
			try
			{
				int num = -1;
				for (uint num2 = 0U; num2 < textLength; num2 += 1U)
				{
					long num3 = (long)((ulong)num2);
					long num4 = num3 * 2L;
					if (*(ushort*)(num4 / (long)sizeof(ushort) + pTextString) == 45)
					{
						if (num == -1)
						{
							DWRITE_GLYPH_METRICS dwrite_GLYPH_METRICS;
							int hr;
							if (textFormattingMode == TextFormattingMode.Ideal)
							{
								IDWriteFontFace* dwriteFontFaceNoAddRef = fontFace.DWriteFontFaceNoAddRef;
								hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_METRICS*,System.Int32), dwriteFontFaceNoAddRef, num4 / (long)sizeof(ushort) + pGlyphIndices, 1, ref dwrite_GLYPH_METRICS, 0, *(*(long*)dwriteFontFaceNoAddRef + 80L));
							}
							else
							{
								int num5 = (textFormattingMode != TextFormattingMode.Display) ? 1 : 0;
								IDWriteFontFace* dwriteFontFaceNoAddRef2 = fontFace.DWriteFontFaceNoAddRef;
								hr = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Single,System.Single,MS.Internal.Text.TextInterface.Native.DWRITE_MATRIX modopt(System.Runtime.CompilerServices.IsConst)*,System.Int32,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_METRICS*,System.Int32), dwriteFontFaceNoAddRef2, (float)fontEmSize, pixelsPerDip, 0L, num5, num4 / (long)sizeof(ushort) + pGlyphIndices, 1, ref dwrite_GLYPH_METRICS, isSideways, *(*(long*)dwriteFontFaceNoAddRef2 + 136L));
							}
							GC.KeepAlive(fontFace);
							Util.ConvertHresultToException(hr);
							double num6 = *(ref dwrite_GLYPH_METRICS + 4) * fontEmSize;
							double num7 = Math.Round(num6 / font.Metrics.DesignUnitsPerEm * (double)pixelsPerDip) / (double)pixelsPerDip;
							num = (int)Math.Round(num7 * scalingFactor);
						}
						(num3 * 4L / 4L)[glyphAdvances] = num;
					}
					else
					{
						(num3 * 4L / 4L)[glyphAdvances] = 0;
					}
					glyphOffsets[(int)num2].du = 0;
					glyphOffsets[(int)num2].dv = 0;
				}
			}
			finally
			{
				fontFace.Release();
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B8C8 File Offset: 0x0000ACC8
		[SecurityCritical]
		private unsafe static void ReleaseItemizationNativeResources(IDWriteFactory** ppFactory, IDWriteTextAnalyzer** ppTextAnalyzer, IDWriteTextAnalysisSource** ppTextAnalysisSource, IDWriteTextAnalysisSink** ppTextAnalysisSink)
		{
			if (ppFactory != null)
			{
				ulong num = (ulong)(*(long*)ppFactory);
				if (num != 0UL)
				{
					ulong num2 = num;
					object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), num2, *(*num2 + 16L));
					*(long*)ppFactory = 0L;
				}
			}
			if (ppTextAnalyzer != null)
			{
				ulong num3 = (ulong)(*(long*)ppTextAnalyzer);
				if (num3 != 0UL)
				{
					ulong num4 = num3;
					object obj2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), num4, *(*num4 + 16L));
					*(long*)ppTextAnalyzer = 0L;
				}
			}
			if (ppTextAnalysisSource != null)
			{
				ulong num5 = (ulong)(*(long*)ppTextAnalysisSource);
				if (num5 != 0UL)
				{
					ulong num6 = num5;
					object obj3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), num6, *(*num6 + 16L));
					*(long*)ppTextAnalysisSource = 0L;
				}
			}
			if (ppTextAnalysisSink != null)
			{
				ulong num7 = (ulong)(*(long*)ppTextAnalysisSink);
				if (num7 != 0UL)
				{
					ulong num8 = num7;
					object obj4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), num8, *(*num8 + 16L));
					*(long*)ppTextAnalysisSink = 0L;
				}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00011E14 File Offset: 0x00011214
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private unsafe static IList<Span> AnalyzeExtendedAndItemize(TextItemizer textItemizer, ushort* text, uint length, CultureInfo numberCulture, IClassification classification)
		{
			Invariant.Assert(true);
			byte* ptr = (byte*)<Module>.@new((ulong)length);
			IList<Span> result;
			try
			{
				TextAnalyzer.AnalyzeExtendedCharactersAndDigits(text, length, textItemizer, ptr, numberCulture, classification);
				result = textItemizer.Itemize(numberCulture, ptr, length);
			}
			finally
			{
				<Module>.delete((void*)ptr);
			}
			return result;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00011058 File Offset: 0x00010458
		[SecuritySafeCritical]
		private unsafe static DWRITE_SCRIPT_SHAPES GetScriptShapes(ItemProps itemProps)
		{
			return (DWRITE_SCRIPT_SHAPES)(*(int*)((byte*)itemProps.ScriptAnalysis + 4L));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00010F94 File Offset: 0x00010394
		[SecurityCritical]
		internal unsafe TextAnalyzer(IDWriteTextAnalyzer* textAnalyzer)
		{
			this._textAnalyzer = new NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteTextAnalyzer>((IUnknown*)textAnalyzer);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000120D8 File Offset: 0x000114D8
		[SecurityCritical]
		internal unsafe static IList<Span> Itemize(ushort* text, uint length, CultureInfo culture, Factory factory, [MarshalAs(UnmanagedType.U1)] bool isRightToLeftParagraph, CultureInfo numberCulture, [MarshalAs(UnmanagedType.U1)] bool ignoreUserOverride, uint numberSubstitutionMethod, IClassification classificationUtility, CreateTextAnalysisSink pfnCreateTextAnalysisSink, GetScriptAnalysisList pfnGetScriptAnalysisList, GetNumberSubstitutionList pfnGetNumberSubstitutionList, CreateTextAnalysisSource pfnCreateTextAnalysisSource)
		{
			if (length > 0U)
			{
				IDWriteTextAnalyzer* ptr = null;
				IDWriteTextAnalysisSink* ptr2 = null;
				IDWriteTextAnalysisSource* ptr3 = null;
				IDWriteFactory* dwriteFactoryAddRef = factory.DWriteFactoryAddRef;
				try
				{
					Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteTextAnalyzer**), dwriteFactoryAddRef, ref ptr, *(*(long*)dwriteFactoryAddRef + 168L)));
					ushort* numberCulture2 = null;
					if (numberCulture != null)
					{
						ref ushort ptrToStringChars = ref Util.GetPtrToStringChars(numberCulture.IetfLanguageTag);
						numberCulture2 = ref ptrToStringChars;
					}
					ref ushort ptrToStringChars2 = ref Util.GetPtrToStringChars(culture.IetfLanguageTag);
					Util.ConvertHresultToException(pfnCreateTextAnalysisSource(text, length, ref ptrToStringChars2, (void*)dwriteFactoryAddRef, isRightToLeftParagraph, numberCulture2, ignoreUserOverride, numberSubstitutionMethod, (void**)(&ptr3)));
					ptr2 = (IDWriteTextAnalysisSink*)pfnCreateTextAnalysisSink();
					Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteTextAnalysisSource*,System.UInt32,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteTextAnalysisSink*), ptr, ptr3, 0, length, ptr2, *(*(long*)ptr + 24L)));
					Util.ConvertHresultToException(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,MS.Internal.Text.TextInterface.Native.IDWriteTextAnalysisSource*,System.UInt32,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteTextAnalysisSink*), ptr, ptr3, 0, length, ptr2, *(*(long*)ptr + 40L)));
					DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>* pScriptAnalysisListHead = (DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>*)pfnGetScriptAnalysisList((void*)ptr2);
					DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>* pNumberSubstitutionListHead = (DWriteTextAnalysisNode<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution\u0020*>*)pfnGetNumberSubstitutionList((void*)ptr2);
					return TextAnalyzer.AnalyzeExtendedAndItemize(new TextItemizer(pScriptAnalysisListHead, pNumberSubstitutionListHead), text, length, numberCulture, classificationUtility);
				}
				finally
				{
					TextAnalyzer.ReleaseItemizationNativeResources(&dwriteFactoryAddRef, &ptr, &ptr3, &ptr2);
				}
			}
			return null;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C0A0 File Offset: 0x0000B4A0
		[SecurityCritical]
		internal unsafe static void AnalyzeExtendedCharactersAndDigits(ushort* text, uint length, TextItemizer textItemizer, byte* pCharAttribute, CultureInfo numberCulture, IClassification classificationUtility)
		{
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			bool flag5;
			bool flag6;
			classificationUtility.GetCharAttribute((int)(*(ushort*)text), out flag, out flag2, out flag3, out flag4, out flag5, out flag6);
			bool flag7 = ItemizerHelper.IsExtendedCharacter(*(ushort*)text);
			uint num = 0U;
			bool flag8 = ((numberCulture != null && flag4) ? 1 : 0) != 0;
			CharAttribute.Enum @enum = flag ? ((CharAttribute.Enum)1) : ((CharAttribute.Enum)0);
			CharAttribute.Enum enum2 = flag2 ? ((CharAttribute.Enum)2) : ((CharAttribute.Enum)0);
			CharAttribute.Enum enum3 = flag5 ? ((CharAttribute.Enum)8) : ((CharAttribute.Enum)0);
			CharAttribute.Enum enum4 = flag3 ? ((CharAttribute.Enum)4) : ((CharAttribute.Enum)0);
			CharAttribute.Enum enum5 = flag6 ? ((CharAttribute.Enum)16) : ((CharAttribute.Enum)0);
			CharAttribute.Enum enum6 = flag7 ? ((CharAttribute.Enum)32) : ((CharAttribute.Enum)0);
			*pCharAttribute = ((byte)enum6 | (byte)enum5 | (byte)enum4 | (byte)enum3 | (byte)enum2 | (byte)@enum);
			uint num2 = 1U;
			if (1U < length)
			{
				byte* ptr = pCharAttribute + 1L;
				ushort* ptr2 = text + 2L / (long)sizeof(ushort);
				do
				{
					classificationUtility.GetCharAttribute((int)(*(ushort*)ptr2), out flag, out flag2, out flag3, out flag4, out flag5, out flag6);
					flag7 = ItemizerHelper.IsExtendedCharacter(*(ushort*)ptr2);
					CharAttribute.Enum enum7 = flag ? ((CharAttribute.Enum)1) : ((CharAttribute.Enum)0);
					CharAttribute.Enum enum8 = flag2 ? ((CharAttribute.Enum)2) : ((CharAttribute.Enum)0);
					CharAttribute.Enum enum9 = flag5 ? ((CharAttribute.Enum)8) : ((CharAttribute.Enum)0);
					CharAttribute.Enum enum10 = flag3 ? ((CharAttribute.Enum)4) : ((CharAttribute.Enum)0);
					CharAttribute.Enum enum11 = flag6 ? ((CharAttribute.Enum)16) : ((CharAttribute.Enum)0);
					CharAttribute.Enum enum12 = flag7 ? ((CharAttribute.Enum)32) : ((CharAttribute.Enum)0);
					*ptr = ((byte)enum12 | (byte)enum11 | (byte)enum10 | (byte)enum9 | (byte)enum8 | (byte)enum7);
					bool flag9 = ((numberCulture != null && flag4) ? 1 : 0) != 0;
					if (flag8 != flag9)
					{
						textItemizer.SetIsDigit(num, num2 - num, flag8);
						num = num2;
						flag8 = flag9;
					}
					num2 += 1U;
					ptr2 += 2L / (long)sizeof(ushort);
					ptr += 1L;
				}
				while (num2 < length);
			}
			textItemizer.SetIsDigit(num, num2 - num, flag8);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00012AAC File Offset: 0x00011EAC
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe void GetGlyphsAndTheirPlacements(ushort* textString, uint textLength, Font font, ushort blankGlyphIndex, [MarshalAs(UnmanagedType.U1)] bool isSideways, [MarshalAs(UnmanagedType.U1)] bool isRightToLeft, CultureInfo cultureInfo, DWriteFontFeature[][] features, uint[] featureRangeLengths, double fontEmSize, double scalingFactor, float pixelsPerDip, TextFormattingMode textFormattingMode, ItemProps itemProps, out ushort[] clusterMap, out ushort[] glyphIndices, out int[] glyphAdvances, out GlyphOffset[] glyphOffsets)
		{
			uint num = textLength * 3U;
			ushort[] array = new ushort[textLength];
			clusterMap = array;
			ref ushort clusterMap2 = ref array[0];
			DWRITE_SHAPING_TEXT_PROPERTIES* ptr = (DWRITE_SHAPING_TEXT_PROPERTIES*)<Module>.@new((ulong)textLength * 2UL);
			DWRITE_SHAPING_GLYPH_PROPERTIES* ptr2 = null;
			ushort* ptr3 = null;
			try
			{
				uint num2 = num + 1U;
				while (num2 > num)
				{
					num = num2;
					if (ptr2 != null)
					{
						<Module>.delete((void*)ptr2);
						ptr2 = null;
					}
					ulong size = (ulong)num * 2UL;
					DWRITE_SHAPING_GLYPH_PROPERTIES* ptr4 = (DWRITE_SHAPING_GLYPH_PROPERTIES*)<Module>.@new(size);
					ptr2 = ptr4;
					if (ptr3 != null)
					{
						<Module>.delete((void*)ptr3);
						ptr3 = null;
					}
					ushort* ptr5 = (ushort*)<Module>.@new(size);
					ptr3 = ptr5;
					this.GetGlyphs(textString, textLength, font, blankGlyphIndex, isSideways, isRightToLeft, cultureInfo, features, featureRangeLengths, num, textFormattingMode, itemProps, ref clusterMap2, (ushort*)ptr, ptr5, (uint*)ptr4, null, out num2);
				}
				glyphIndices = new ushort[num2];
				IntPtr source = new IntPtr((void*)ptr3);
				Marshal.Copy(source, (short[])glyphIndices, 0, (int)num2);
				int[] array2 = new int[num2];
				glyphAdvances = array2;
				ref int glyphAdvances2 = ref array2[0];
				glyphOffsets = new GlyphOffset[num2];
				this.GetGlyphPlacements(textString, ref clusterMap2, (ushort*)ptr, textLength, (ushort*)ptr3, (uint*)ptr2, num2, font, fontEmSize, scalingFactor, isSideways, isRightToLeft, cultureInfo, features, featureRangeLengths, textFormattingMode, itemProps, pixelsPerDip, ref glyphAdvances2, out glyphOffsets);
			}
			finally
			{
				<Module>.delete((void*)ptr);
				if (ptr2 != null)
				{
					<Module>.delete((void*)ptr2);
				}
				if (ptr3 != null)
				{
					<Module>.delete((void*)ptr3);
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000121E4 File Offset: 0x000115E4
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe void GetGlyphs(ushort* textString, uint textLength, Font font, ushort blankGlyphIndex, [MarshalAs(UnmanagedType.U1)] bool isSideways, [MarshalAs(UnmanagedType.U1)] bool isRightToLeft, CultureInfo cultureInfo, DWriteFontFeature[][] features, uint[] featureRangeLengths, uint maxGlyphCount, TextFormattingMode textFormattingMode, ItemProps itemProps, ushort* clusterMap, ushort* textProps, ushort* glyphIndices, uint* glyphProps, int* pfCanGlyphAlone, out uint actualGlyphCount)
		{
			byte condition = (itemProps.ScriptAnalysis != null) ? 1 : 0;
			Invariant.Assert(condition != 0);
			if (TextAnalyzer.GetScriptShapes(itemProps) != (DWRITE_SCRIPT_SHAPES)0)
			{
				FontFace fontFace = font.GetFontFace();
				try
				{
					this.GetBlankGlyphsForControlCharacters(textString, textLength, fontFace, blankGlyphIndex, maxGlyphCount, clusterMap, glyphIndices, pfCanGlyphAlone, out actualGlyphCount);
					goto IL_29E;
				}
				finally
				{
					fontFace.Release();
				}
			}
			ref ushort ptrToStringChars = ref Util.GetPtrToStringChars(cultureInfo.IetfLanguageTag);
			uint* ptr = null;
			uint num = 0U;
			GCHandle[] array = null;
			DWRITE_TYPOGRAPHIC_FEATURES** ptr2 = null;
			if (features != null)
			{
				ref uint uint32& = ref featureRangeLengths[0];
				ptr = ref uint32&;
				num = (uint)featureRangeLengths.Length;
				array = new GCHandle[num];
				ptr2 = (DWRITE_TYPOGRAPHIC_FEATURES**)<Module>.@new((ulong)num * 8UL);
			}
			FontFace fontFace2 = font.GetFontFace();
			try
			{
				if (features != null)
				{
					for (uint num2 = 0U; num2 < num; num2 += 1U)
					{
						GCHandle gchandle = GCHandle.Alloc(features[(int)num2], GCHandleType.Pinned);
						array[(int)num2] = gchandle;
						DWRITE_TYPOGRAPHIC_FEATURES* ptr3 = (DWRITE_TYPOGRAPHIC_FEATURES*)<Module>.@new(16UL);
						DWRITE_TYPOGRAPHIC_FEATURES* ptr4;
						if (ptr3 != null)
						{
							initblk(ptr3, 0, 16L);
							ptr4 = ptr3;
						}
						else
						{
							ptr4 = null;
						}
						DWRITE_TYPOGRAPHIC_FEATURES** ptr5 = num2 * 8L + ptr2 / sizeof(DWRITE_TYPOGRAPHIC_FEATURES*);
						*(long*)ptr5 = ptr4;
						IntPtr intPtr = array[(int)num2].AddrOfPinnedObject();
						*(*(long*)ptr5) = intPtr.ToPointer();
						*(*(long*)ptr5 + 8L) = features[(int)num2].Length;
					}
				}
				uint num3 = 0U;
				int num4 = isRightToLeft ? 1 : 0;
				int num5 = isSideways ? 1 : 0;
				IDWriteTextAnalyzer* value = this._textAnalyzer.Value;
				int num6 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFace*,System.Int32,System.Int32,MS.Internal.Text.TextInterface.Native.DWRITE_SCRIPT_ANALYSIS modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.IDWriteNumberSubstitution*,MS.Internal.Text.TextInterface.Native.DWRITE_TYPOGRAPHIC_FEATURES modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.UInt32,System.UInt16*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_TEXT_PROPERTIES*,System.UInt16*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_GLYPH_PROPERTIES*,System.UInt32*), value, textString, textLength, fontFace2.DWriteFontFaceNoAddRef, num5, num4, itemProps.ScriptAnalysis, ref ptrToStringChars, itemProps.NumberSubstitutionNoAddRef, ptr2, ptr, num, maxGlyphCount, clusterMap, textProps, glyphIndices, glyphProps, ref num3, *(*(long*)value + 56L));
				if (-2147024809 == num6)
				{
					int num7 = isRightToLeft ? 1 : 0;
					int num8 = isSideways ? 1 : 0;
					IDWriteTextAnalyzer* value2 = this._textAnalyzer.Value;
					num6 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFace*,System.Int32,System.Int32,MS.Internal.Text.TextInterface.Native.DWRITE_SCRIPT_ANALYSIS modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.IDWriteNumberSubstitution*,MS.Internal.Text.TextInterface.Native.DWRITE_TYPOGRAPHIC_FEATURES modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.UInt32,System.UInt16*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_TEXT_PROPERTIES*,System.UInt16*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_GLYPH_PROPERTIES*,System.UInt32*), value2, textString, textLength, fontFace2.DWriteFontFaceNoAddRef, num8, num7, itemProps.ScriptAnalysis, 0L, itemProps.NumberSubstitutionNoAddRef, ptr2, ptr, num, maxGlyphCount, clusterMap, textProps, glyphIndices, glyphProps, ref num3, *(*(long*)value2 + 56L));
				}
				GC.KeepAlive(fontFace2);
				GC.KeepAlive(itemProps);
				GC.KeepAlive(this._textAnalyzer);
				if (features != null)
				{
					for (uint num9 = 0U; num9 < num; num9 += 1U)
					{
						array[(int)num9].Free();
						<Module>.delete((num9 * 8L)[ptr2 / 8]);
					}
				}
				if (num6 == <Module>.HRESULT_FROM_WIN32(122))
				{
					actualGlyphCount = (maxGlyphCount * 27U >> 3) + 76U;
				}
				else
				{
					Util.ConvertHresultToException(num6);
					if (pfCanGlyphAlone != null)
					{
						for (uint num10 = 0U; num10 < textLength; num10 += 1U)
						{
							long num11 = (long)((ulong)num10);
							int num12 = (((num11 * 2L / 2L)[textProps] & 1) > 0) ? 1 : 0;
							(num11 * 4L / 4L)[pfCanGlyphAlone] = num12;
						}
					}
					actualGlyphCount = num3;
				}
			}
			finally
			{
				fontFace2.Release();
				if (ptr2 != null)
				{
					<Module>.delete((void*)ptr2);
				}
			}
			IL_29E:
			GC.KeepAlive(this);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00012630 File Offset: 0x00011A30
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal unsafe void GetGlyphPlacements(ushort* textString, ushort* clusterMap, ushort* textProps, uint textLength, ushort* glyphIndices, uint* glyphProps, uint glyphCount, Font font, double fontEmSize, double scalingFactor, [MarshalAs(UnmanagedType.U1)] bool isSideways, [MarshalAs(UnmanagedType.U1)] bool isRightToLeft, CultureInfo cultureInfo, DWriteFontFeature[][] features, uint[] featureRangeLengths, TextFormattingMode textFormattingMode, ItemProps itemProps, float pixelsPerDip, int* glyphAdvances, out GlyphOffset[] glyphOffsets)
		{
			byte condition = (itemProps.ScriptAnalysis != null) ? 1 : 0;
			Invariant.Assert(condition != 0);
			if (TextAnalyzer.GetScriptShapes(itemProps) != (DWRITE_SCRIPT_SHAPES)0)
			{
				this.GetGlyphPlacementsForControlCharacters(textString, textLength, font, textFormattingMode, fontEmSize, scalingFactor, isSideways, pixelsPerDip, glyphCount, glyphIndices, glyphAdvances, out glyphOffsets);
			}
			else
			{
				ulong num = (ulong)glyphCount;
				float* ptr = (float*)<Module>.@new(num * 4UL);
				DWRITE_GLYPH_OFFSET* ptr2 = (DWRITE_GLYPH_OFFSET*)<Module>.@new(num * 8UL);
				GCHandle[] array = null;
				uint num2 = 0U;
				DWRITE_TYPOGRAPHIC_FEATURES** ptr3 = null;
				uint* ptr4 = null;
				if (features != null)
				{
					num2 = (uint)featureRangeLengths.Length;
					ptr3 = (DWRITE_TYPOGRAPHIC_FEATURES**)<Module>.@new((ulong)num2 * 8UL);
					ref uint uint32& = ref featureRangeLengths[0];
					ptr4 = ref uint32&;
					array = new GCHandle[num2];
				}
				FontFace fontFace = font.GetFontFace();
				try
				{
					ref ushort ptrToStringChars = ref Util.GetPtrToStringChars(cultureInfo.IetfLanguageTag);
					DWRITE_MATRIX identityTransform = Factory.GetIdentityTransform();
					if (features != null)
					{
						for (uint num3 = 0U; num3 < num2; num3 += 1U)
						{
							GCHandle gchandle = GCHandle.Alloc(features[(int)num3], GCHandleType.Pinned);
							array[(int)num3] = gchandle;
							DWRITE_TYPOGRAPHIC_FEATURES* ptr5 = (DWRITE_TYPOGRAPHIC_FEATURES*)<Module>.@new(16UL);
							DWRITE_TYPOGRAPHIC_FEATURES* ptr6;
							if (ptr5 != null)
							{
								initblk(ptr5, 0, 16L);
								ptr6 = ptr5;
							}
							else
							{
								ptr6 = null;
							}
							DWRITE_TYPOGRAPHIC_FEATURES** ptr7 = num3 * 8L + ptr3 / sizeof(DWRITE_TYPOGRAPHIC_FEATURES*);
							*(long*)ptr7 = ptr6;
							IntPtr intPtr = array[(int)num3].AddrOfPinnedObject();
							*(*(long*)ptr7) = intPtr.ToPointer();
							*(*(long*)ptr7 + 8L) = features[(int)num3].Length;
						}
					}
					float num4 = (float)fontEmSize;
					int num7;
					if (textFormattingMode == TextFormattingMode.Ideal)
					{
						int num5 = isRightToLeft ? 1 : 0;
						int num6 = isSideways ? 1 : 0;
						IDWriteTextAnalyzer* value = this._textAnalyzer.Value;
						num7 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_TEXT_PROPERTIES*,System.UInt32,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_GLYPH_PROPERTIES modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFace*,System.Single,System.Int32,System.Int32,MS.Internal.Text.TextInterface.Native.DWRITE_SCRIPT_ANALYSIS modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_TYPOGRAPHIC_FEATURES modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.Single*,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_OFFSET*), value, textString, clusterMap, textProps, textLength, glyphIndices, glyphProps, glyphCount, fontFace.DWriteFontFaceNoAddRef, num4, num6, num5, itemProps.ScriptAnalysis, ref ptrToStringChars, ptr3, ptr4, num2, ptr, ptr2, *(*(long*)value + 64L));
						if (-2147024809 == num7)
						{
							int num8 = isRightToLeft ? 1 : 0;
							int num9 = isSideways ? 1 : 0;
							IDWriteTextAnalyzer* value2 = this._textAnalyzer.Value;
							num7 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_TEXT_PROPERTIES*,System.UInt32,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_GLYPH_PROPERTIES modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFace*,System.Single,System.Int32,System.Int32,MS.Internal.Text.TextInterface.Native.DWRITE_SCRIPT_ANALYSIS modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_TYPOGRAPHIC_FEATURES modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.Single*,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_OFFSET*), value2, textString, clusterMap, textProps, textLength, glyphIndices, glyphProps, glyphCount, fontFace.DWriteFontFaceNoAddRef, num4, num9, num8, itemProps.ScriptAnalysis, 0L, ptr3, ptr4, num2, ptr, ptr2, *(*(long*)value2 + 64L));
						}
					}
					else
					{
						int num10 = isRightToLeft ? 1 : 0;
						int num11 = isSideways ? 1 : 0;
						IDWriteTextAnalyzer* value3 = this._textAnalyzer.Value;
						num7 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_TEXT_PROPERTIES*,System.UInt32,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_GLYPH_PROPERTIES modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFace*,System.Single,System.Single,MS.Internal.Text.TextInterface.Native.DWRITE_MATRIX modopt(System.Runtime.CompilerServices.IsConst)*,System.Int32,System.Int32,System.Int32,MS.Internal.Text.TextInterface.Native.DWRITE_SCRIPT_ANALYSIS modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_TYPOGRAPHIC_FEATURES modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.Single*,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_OFFSET*), value3, textString, clusterMap, textProps, textLength, glyphIndices, glyphProps, glyphCount, fontFace.DWriteFontFaceNoAddRef, num4, pixelsPerDip, ref identityTransform, 0, num11, num10, itemProps.ScriptAnalysis, ref ptrToStringChars, ptr3, ptr4, num2, ptr, ptr2, *(*(long*)value3 + 72L));
						if (-2147024809 == num7)
						{
							int num12 = isRightToLeft ? 1 : 0;
							int num13 = isSideways ? 1 : 0;
							IDWriteTextAnalyzer* value4 = this._textAnalyzer.Value;
							num7 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_TEXT_PROPERTIES*,System.UInt32,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_SHAPING_GLYPH_PROPERTIES modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,MS.Internal.Text.TextInterface.Native.IDWriteFontFace*,System.Single,System.Single,MS.Internal.Text.TextInterface.Native.DWRITE_MATRIX modopt(System.Runtime.CompilerServices.IsConst)*,System.Int32,System.Int32,System.Int32,MS.Internal.Text.TextInterface.Native.DWRITE_SCRIPT_ANALYSIS modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt16 modopt(System.Runtime.CompilerServices.IsConst)*,MS.Internal.Text.TextInterface.Native.DWRITE_TYPOGRAPHIC_FEATURES modopt(System.Runtime.CompilerServices.IsConst)**,System.UInt32 modopt(System.Runtime.CompilerServices.IsConst)*,System.UInt32,System.Single*,MS.Internal.Text.TextInterface.Native.DWRITE_GLYPH_OFFSET*), value4, textString, clusterMap, textProps, textLength, glyphIndices, glyphProps, glyphCount, fontFace.DWriteFontFaceNoAddRef, num4, pixelsPerDip, ref identityTransform, 0, num13, num12, itemProps.ScriptAnalysis, 0L, ptr3, ptr4, num2, ptr, ptr2, *(*(long*)value4 + 72L));
						}
					}
					GC.KeepAlive(fontFace);
					GC.KeepAlive(itemProps);
					GC.KeepAlive(this._textAnalyzer);
					if (features != null)
					{
						for (uint num14 = 0U; num14 < num2; num14 += 1U)
						{
							array[(int)num14].Free();
							<Module>.delete((num14 * 8L)[ptr3 / 8]);
						}
					}
					glyphOffsets = new GlyphOffset[glyphCount];
					if (textFormattingMode == TextFormattingMode.Ideal)
					{
						for (uint num15 = 0U; num15 < glyphCount; num15 += 1U)
						{
							long num16 = (long)((ulong)num15);
							long num17 = num16 * 4L;
							(num17 / 4L)[glyphAdvances] = (int)Math.Round((double)(num17 / 4L)[ptr] * fontEmSize * scalingFactor / (double)num4);
							DWRITE_GLYPH_OFFSET* ptr8 = num16 * 8L / (long)sizeof(DWRITE_GLYPH_OFFSET) + ptr2;
							glyphOffsets[(int)num15].du = (int)((double)(*(float*)ptr8) * scalingFactor);
							glyphOffsets[(int)num15].dv = (int)((double)(*(float*)(ptr8 + 4L / (long)sizeof(DWRITE_GLYPH_OFFSET))) * scalingFactor);
						}
					}
					else
					{
						for (uint num18 = 0U; num18 < glyphCount; num18 += 1U)
						{
							long num19 = (long)((ulong)num18);
							long num20 = num19 * 4L;
							(num20 / 4L)[glyphAdvances] = (int)Math.Round((double)(num20 / 4L)[ptr] * scalingFactor);
							DWRITE_GLYPH_OFFSET* ptr9 = num19 * 8L / (long)sizeof(DWRITE_GLYPH_OFFSET) + ptr2;
							glyphOffsets[(int)num18].du = (int)((double)(*(float*)ptr9) * scalingFactor);
							glyphOffsets[(int)num18].dv = (int)((double)(*(float*)(ptr9 + 4L / (long)sizeof(DWRITE_GLYPH_OFFSET))) * scalingFactor);
						}
					}
					Util.ConvertHresultToException(num7);
				}
				finally
				{
					fontFace.Release();
					if (ptr != null)
					{
						<Module>.delete((void*)ptr);
					}
					if (ptr2 != null)
					{
						<Module>.delete((void*)ptr2);
					}
					if (ptr3 != null)
					{
						<Module>.delete((void*)ptr3);
					}
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x04000344 RID: 836
		[SecurityCritical]
		private NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteTextAnalyzer> _textAnalyzer;

		// Token: 0x04000345 RID: 837
		internal static char CharHyphen = '-';
	}
}
