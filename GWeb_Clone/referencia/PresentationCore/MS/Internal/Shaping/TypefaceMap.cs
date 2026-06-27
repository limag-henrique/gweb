using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.FontCache;
using MS.Internal.FontFace;
using MS.Internal.Generic;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;
using MS.Utility;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E9 RID: 1769
	internal class TypefaceMap
	{
		// Token: 0x06004C4D RID: 19533 RVA: 0x0012ADC0 File Offset: 0x0012A1C0
		internal TypefaceMap(System.Windows.Media.FontFamily fontFamily, System.Windows.Media.FontFamily fallbackFontFamily, System.Windows.FontStyle canonicalStyle, System.Windows.FontWeight canonicalWeight, System.Windows.FontStretch canonicalStretch, bool nullFont)
		{
			Invariant.Assert(fontFamily != null);
			System.Windows.Media.FontFamily[] fontFamilies;
			if (fallbackFontFamily != null)
			{
				System.Windows.Media.FontFamily[] array = new System.Windows.Media.FontFamily[2];
				array[0] = fontFamily;
				fontFamilies = array;
				array[1] = fallbackFontFamily;
			}
			else
			{
				(fontFamilies = new System.Windows.Media.FontFamily[1])[0] = fontFamily;
			}
			this._fontFamilies = fontFamilies;
			this._canonicalStyle = canonicalStyle;
			this._canonicalWeight = canonicalWeight;
			this._canonicalStretch = canonicalStretch;
			this._nullFont = nullFont;
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0012AE38 File Offset: 0x0012A238
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe void GetShapeableText(CharacterBufferReference characterBufferReference, int stringLength, TextRunProperties textRunProperties, CultureInfo digitCulture, bool isRightToLeftParagraph, IList<TextShapeableSymbols> shapeableList, IShapeableTextCollector collector, TextFormattingMode textFormattingMode)
		{
			int num = 0;
			CharacterBufferRange characterBufferRange = new CharacterBufferRange(characterBufferReference, stringLength);
			CultureInfo cultureInfo = textRunProperties.CultureInfo;
			GCHandle gcHandle;
			IntPtr intPtr = characterBufferReference.CharacterBuffer.PinAndGetCharacterPointer(characterBufferReference.OffsetToFirstChar, out gcHandle);
			bool ignoreUserOverride;
			NumberSubstitutionMethod resolvedSubstitutionMethod = DigitState.GetResolvedSubstitutionMethod(textRunProperties, digitCulture, out ignoreUserOverride);
			IList<Span> collection = checked(TextAnalyzer.Itemize((ushort*)intPtr.ToPointer(), (uint)stringLength, cultureInfo, DWriteFactory.Instance, isRightToLeftParagraph, digitCulture, ignoreUserOverride, (uint)resolvedSubstitutionMethod, ClassificationUtility.Instance, new CreateTextAnalysisSink(UnsafeNativeMethods.CreateTextAnalysisSink), new GetScriptAnalysisList(UnsafeNativeMethods.GetScriptAnalysisList), new GetNumberSubstitutionList(UnsafeNativeMethods.GetNumberSubstitutionList), new CreateTextAnalysisSource(UnsafeNativeMethods.CreateTextAnalysisSource)));
			characterBufferReference.CharacterBuffer.UnpinCharacterPointer(gcHandle);
			SpanVector spanVector = new SpanVector(null, new FrugalStructList<Span>(collection));
			SpanVector<int> vector = new SpanVector<int>(-1);
			foreach (object obj in spanVector)
			{
				Span span = (Span)obj;
				this.MapItem(new CharacterBufferRange(characterBufferRange, num, span.length), cultureInfo, span, ref vector, num);
				num += span.length;
			}
			int i = 0;
			SpanRider spanRider = new SpanRider(spanVector);
			SpanRider<int> spanRider2 = new SpanRider<int>(vector);
			while (i < characterBufferRange.Length)
			{
				spanRider.At(i);
				spanRider2.At(i);
				int currentValue = spanRider2.CurrentValue;
				int num2 = characterBufferRange.Length - i;
				num2 = Math.Min(num2, spanRider.Length);
				num2 = Math.Min(num2, spanRider2.Length);
				ScaledShapeTypeface scaledShapeTypeface = this._cachedScaledTypefaces[currentValue];
				collector.Add(shapeableList, new CharacterBufferRange(characterBufferRange, i, num2), textRunProperties, (ItemProps)spanRider.CurrentElement, scaledShapeTypeface.ShapeTypeface, scaledShapeTypeface.ScaleInEm, scaledShapeTypeface.NullShape, textFormattingMode);
				i += num2;
			}
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0012B01C File Offset: 0x0012A41C
		private void MapItem(CharacterBufferRange unicodeString, CultureInfo culture, Span itemSpan, ref SpanVector<int> cachedScaledTypefaceIndexSpans, int ichItem)
		{
			CultureInfo digitCulture = ((ItemProps)itemSpan.element).DigitCulture;
			if (!this.GetCachedScaledTypefaceMap(unicodeString, culture, digitCulture, ref cachedScaledTypefaceIndexSpans, ichItem))
			{
				SpanVector scaledTypefaceSpans = new SpanVector(null);
				PhysicalFontFamily firstValidFamily = null;
				int num = 0;
				if (!this._nullFont)
				{
					int num2;
					this.MapByFontFamilyList(unicodeString, culture, digitCulture, this._fontFamilies, ref firstValidFamily, ref num, null, 1.0, 0, scaledTypefaceSpans, 0, out num2);
				}
				else
				{
					int num2;
					this.MapUnresolvedCharacters(unicodeString, culture, digitCulture, firstValidFamily, ref num, scaledTypefaceSpans, 0, out num2);
				}
				this.CacheScaledTypefaceMap(unicodeString, culture, digitCulture, scaledTypefaceSpans, ref cachedScaledTypefaceIndexSpans, ichItem);
			}
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0012B0A4 File Offset: 0x0012A4A4
		private bool GetCachedScaledTypefaceMap(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, ref SpanVector<int> cachedScaledTypefaceIndexSpans, int ichItem)
		{
			TypefaceMap.IntMap intMap;
			if (!this._intMaps.TryGetValue(culture, out intMap))
			{
				return false;
			}
			DigitMap digitMap = new DigitMap(digitCulture);
			int num4;
			for (int i = 0; i < unicodeString.Length; i += num4)
			{
				int num2;
				int num = digitMap[Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, i, unicodeString.Length - i), out num2)];
				ushort num3 = intMap[num];
				if (num3 == 0)
				{
					return false;
				}
				num4 = num2;
				while (i + num4 < unicodeString.Length)
				{
					num = digitMap[Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, i + num4, unicodeString.Length - i - num4), out num2)];
					if (intMap[num] != num3 && !Classification.IsCombining(num) && !Classification.IsJoiner(num))
					{
						break;
					}
					num4 += num2;
				}
				cachedScaledTypefaceIndexSpans.Set(ichItem + i, num4, (int)(num3 - 1));
			}
			return true;
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0012B174 File Offset: 0x0012A574
		private void CacheScaledTypefaceMap(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, SpanVector scaledTypefaceSpans, ref SpanVector<int> cachedScaledTypefaceIndexSpans, int ichItem)
		{
			TypefaceMap.IntMap intMap;
			if (!this._intMaps.TryGetValue(culture, out intMap))
			{
				intMap = new TypefaceMap.IntMap();
				this._intMaps.Add(culture, intMap);
			}
			DigitMap digitMap = new DigitMap(digitCulture);
			SpanRider spanRider = new SpanRider(scaledTypefaceSpans);
			int num;
			for (int i = 0; i < unicodeString.Length; i += num)
			{
				spanRider.At(i);
				num = Math.Min(unicodeString.Length - i, spanRider.Length);
				int num2 = this.IndexOfScaledTypeface((ScaledShapeTypeface)spanRider.CurrentElement);
				cachedScaledTypefaceIndexSpans.Set(ichItem + i, num, num2);
				num2++;
				int num4;
				for (int j = 0; j < num; j += num4)
				{
					int num3 = digitMap[Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, i + j, unicodeString.Length - i - j), out num4)];
					if (!Classification.IsCombining(num3) && !Classification.IsJoiner(num3))
					{
						if (intMap[num3] != 0 && (int)intMap[num3] != num2)
						{
							Invariant.Assert(false, string.Format(CultureInfo.InvariantCulture, "shapeable cache stores conflicting info, ch = {0}, map[ch] = {1}, index = {2}", new object[]
							{
								num3,
								intMap[num3],
								num2
							}));
						}
						intMap[num3] = (ushort)num2;
					}
				}
			}
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x0012B2B8 File Offset: 0x0012A6B8
		private int IndexOfScaledTypeface(ScaledShapeTypeface scaledTypeface)
		{
			int num = 0;
			while (num < this._cachedScaledTypefaces.Count && !scaledTypeface.Equals(this._cachedScaledTypefaces[num]))
			{
				num++;
			}
			if (num == this._cachedScaledTypefaces.Count)
			{
				num = this._cachedScaledTypefaces.Count;
				this._cachedScaledTypefaces.Add(scaledTypeface);
			}
			return num;
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0012B318 File Offset: 0x0012A718
		private int MapByFontFamily(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, IFontFamily fontFamily, CanonicalFontFamilyReference canonicalFamilyReference, System.Windows.FontStyle canonicalStyle, System.Windows.FontWeight canonicalWeight, System.Windows.FontStretch canonicalStretch, ref PhysicalFontFamily firstValidFamily, ref int firstValidLength, IDeviceFont deviceFont, double scaleInEm, int recursionDepth, SpanVector scaledTypefaceSpans, int firstCharIndex, out int nextValid)
		{
			if (recursionDepth >= 32)
			{
				nextValid = 0;
				return 0;
			}
			if (deviceFont == null)
			{
				deviceFont = fontFamily.GetDeviceFont(this._canonicalStyle, this._canonicalWeight, this._canonicalStretch);
			}
			DigitMap digitMap = new DigitMap(digitCulture);
			int num = 0;
			int num2 = 0;
			nextValid = 0;
			bool flag = false;
			while (num2 < unicodeString.Length && !flag)
			{
				int num3 = unicodeString.Length - num2;
				bool flag2 = false;
				if (deviceFont != null)
				{
					flag2 = deviceFont.ContainsCharacter(digitMap[(int)unicodeString[num2]]);
					int num4 = num2 + 1;
					while (num4 < unicodeString.Length && flag2 == deviceFont.ContainsCharacter(digitMap[(int)unicodeString[num4]]))
					{
						num4++;
					}
					num3 = num4 - num2;
				}
				string text;
				double scaleInEm2;
				bool mapTargetFamilyNameAndScale = fontFamily.GetMapTargetFamilyNameAndScale(new CharacterBufferRange(unicodeString, num2, num3), culture, digitCulture, scaleInEm, out num3, out text, out scaleInEm2);
				CharacterBufferRange unicodeString2 = new CharacterBufferRange(unicodeString, num2, num3);
				int num6;
				int num5;
				if (!mapTargetFamilyNameAndScale)
				{
					num5 = this.MapByFontFaceFamily(unicodeString2, culture, digitCulture, fontFamily, canonicalStyle, canonicalWeight, canonicalStretch, ref firstValidFamily, ref firstValidLength, flag2 ? deviceFont : null, false, scaleInEm2, scaledTypefaceSpans, firstCharIndex + num2, false, out num6);
				}
				else if (!string.IsNullOrEmpty(text))
				{
					Uri baseUri = (canonicalFamilyReference != null) ? canonicalFamilyReference.LocationUri : null;
					num5 = this.MapByFontFamilyName(unicodeString2, culture, digitCulture, text, baseUri, ref firstValidFamily, ref firstValidLength, flag2 ? deviceFont : null, scaleInEm2, recursionDepth + 1, scaledTypefaceSpans, firstCharIndex + num2, out num6);
				}
				else
				{
					num5 = 0;
					num6 = num3;
				}
				int num7 = num5;
				int num8 = num6;
				if (num7 < num3)
				{
					flag = true;
				}
				num += num7;
				nextValid = num2 + num8;
				num2 += num7;
			}
			return num;
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x0012B4A0 File Offset: 0x0012A8A0
		private int MapUnresolvedCharacters(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, PhysicalFontFamily firstValidFamily, ref int firstValidLength, SpanVector scaledTypefaceSpans, int firstCharIndex, out int nextValid)
		{
			IFontFamily fontFamily = firstValidFamily;
			bool nullFont = false;
			if (firstValidLength <= 0)
			{
				fontFamily = System.Windows.Media.FontFamily.LookupFontFamily(System.Windows.Media.FontFamily.NullFontFamilyCanonicalName);
				Invariant.Assert(fontFamily != null);
				nullFont = true;
			}
			return this.MapByFontFaceFamily(unicodeString, culture, digitCulture, fontFamily, this._canonicalStyle, this._canonicalWeight, this._canonicalStretch, ref firstValidFamily, ref firstValidLength, null, nullFont, 1.0, scaledTypefaceSpans, firstCharIndex, true, out nextValid);
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x0012B500 File Offset: 0x0012A900
		private int MapByFontFamilyName(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, string familyName, Uri baseUri, ref PhysicalFontFamily firstValidFamily, ref int firstValidLength, IDeviceFont deviceFont, double scaleInEm, int fontMappingDepth, SpanVector scaledTypefaceSpans, int firstCharIndex, out int nextValid)
		{
			if (familyName == null)
			{
				return this.MapUnresolvedCharacters(unicodeString, culture, digitCulture, firstValidFamily, ref firstValidLength, scaledTypefaceSpans, firstCharIndex, out nextValid);
			}
			return this.MapByFontFamilyList(unicodeString, culture, digitCulture, new System.Windows.Media.FontFamily[]
			{
				new System.Windows.Media.FontFamily(baseUri, familyName)
			}, ref firstValidFamily, ref firstValidLength, deviceFont, scaleInEm, fontMappingDepth, scaledTypefaceSpans, firstCharIndex, out nextValid);
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x0012B554 File Offset: 0x0012A954
		private int MapByFontFamilyList(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, System.Windows.Media.FontFamily[] familyList, ref PhysicalFontFamily firstValidFamily, ref int firstValidLength, IDeviceFont deviceFont, double scaleInEm, int recursionDepth, SpanVector scaledTypefaceSpans, int firstCharIndex, out int nextValid)
		{
			int num = 0;
			int num2 = 0;
			int i = 0;
			nextValid = 0;
			while (i < unicodeString.Length)
			{
				int num3 = this.MapOnceByFontFamilyList(new CharacterBufferRange(unicodeString, i, unicodeString.Length - i), culture, digitCulture, familyList, ref firstValidFamily, ref firstValidLength, deviceFont, scaleInEm, recursionDepth, scaledTypefaceSpans, firstCharIndex + i, out num2);
				if (num3 <= 0)
				{
					if (recursionDepth > 0)
					{
						break;
					}
					num3 = this.MapUnresolvedCharacters(new CharacterBufferRange(unicodeString, i, num2), culture, digitCulture, firstValidFamily, ref firstValidLength, scaledTypefaceSpans, firstCharIndex + i, out num2);
				}
				i += num3;
			}
			num += i;
			nextValid = i + num2;
			return num;
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0012B5DC File Offset: 0x0012A9DC
		private int MapOnceByFontFamilyList(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, System.Windows.Media.FontFamily[] familyList, ref PhysicalFontFamily firstValidFamily, ref int firstValidLength, IDeviceFont deviceFont, double scaleInEm, int recursionDepth, SpanVector scaledTypefaceSpans, int firstCharIndex, out int nextValid)
		{
			Invariant.Assert(familyList != null);
			int num = 0;
			nextValid = 0;
			CharacterBufferRange unicodeString2 = unicodeString;
			System.Windows.FontStyle canonicalStyle = this._canonicalStyle;
			System.Windows.FontWeight canonicalWeight = this._canonicalWeight;
			System.Windows.FontStretch canonicalStretch = this._canonicalStretch;
			int i = 0;
			while (i < familyList.Length)
			{
				FontFamilyIdentifier familyIdentifier = familyList[i].FamilyIdentifier;
				CanonicalFontFamilyReference canonicalFamilyReference = null;
				IFontFamily fontFamily;
				if (familyIdentifier.Count != 0)
				{
					canonicalFamilyReference = familyIdentifier[0];
					fontFamily = System.Windows.Media.FontFamily.LookupFontFamilyAndFace(canonicalFamilyReference, ref canonicalStyle, ref canonicalWeight, ref canonicalStretch);
				}
				else
				{
					fontFamily = familyList[i].FirstFontFamily;
				}
				int num2 = 0;
				for (;;)
				{
					if (fontFamily != null)
					{
						num = this.MapByFontFamily(unicodeString2, culture, digitCulture, fontFamily, canonicalFamilyReference, canonicalStyle, canonicalWeight, canonicalStretch, ref firstValidFamily, ref firstValidLength, deviceFont, scaleInEm, recursionDepth, scaledTypefaceSpans, firstCharIndex, out nextValid);
						if (nextValid < unicodeString2.Length)
						{
							unicodeString2 = new CharacterBufferRange(unicodeString.CharacterBuffer, unicodeString.OffsetToFirstChar, nextValid);
						}
						if (num > 0)
						{
							goto Block_4;
						}
					}
					else
					{
						nextValid = unicodeString2.Length;
					}
					if (++num2 >= familyIdentifier.Count)
					{
						break;
					}
					canonicalFamilyReference = familyIdentifier[num2];
					fontFamily = System.Windows.Media.FontFamily.LookupFontFamilyAndFace(canonicalFamilyReference, ref canonicalStyle, ref canonicalWeight, ref canonicalStretch);
				}
				IL_F7:
				i++;
				continue;
				Block_4:
				i = familyList.Length;
				goto IL_F7;
			}
			nextValid = unicodeString2.Length;
			return num;
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x0012B6FC File Offset: 0x0012AAFC
		private int MapByFontFaceFamily(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, IFontFamily fontFamily, System.Windows.FontStyle canonicalStyle, System.Windows.FontWeight canonicalWeight, System.Windows.FontStretch canonicalStretch, ref PhysicalFontFamily firstValidFamily, ref int firstValidLength, IDeviceFont deviceFont, bool nullFont, double scaleInEm, SpanVector scaledTypefaceSpans, int firstCharIndex, bool ignoreMissing, out int nextValid)
		{
			Invariant.Assert(fontFamily != null);
			PhysicalFontFamily physicalFontFamily = fontFamily as PhysicalFontFamily;
			Invariant.Assert(physicalFontFamily != null);
			int num = unicodeString.Length;
			nextValid = 0;
			GlyphTypeface glyphTypeface;
			if (ignoreMissing)
			{
				glyphTypeface = physicalFontFamily.GetGlyphTypeface(canonicalStyle, canonicalWeight, canonicalStretch);
			}
			else if (nullFont)
			{
				glyphTypeface = physicalFontFamily.GetGlyphTypeface(canonicalStyle, canonicalWeight, canonicalStretch);
				num = 0;
				nextValid = unicodeString.Length;
			}
			else
			{
				glyphTypeface = physicalFontFamily.MapGlyphTypeface(canonicalStyle, canonicalWeight, canonicalStretch, unicodeString, digitCulture, ref num, ref nextValid);
			}
			Invariant.Assert(glyphTypeface != null);
			int length = unicodeString.Length;
			if (!ignoreMissing && num > 0)
			{
				length = num;
			}
			if (firstValidLength <= 0)
			{
				firstValidFamily = physicalFontFamily;
				firstValidLength = unicodeString.Length;
			}
			firstValidLength -= num;
			scaledTypefaceSpans.SetValue(firstCharIndex, length, new ScaledShapeTypeface(glyphTypeface, deviceFont, scaleInEm, nullFont));
			return num;
		}

		// Token: 0x04002129 RID: 8489
		private System.Windows.Media.FontFamily[] _fontFamilies;

		// Token: 0x0400212A RID: 8490
		private System.Windows.FontStyle _canonicalStyle;

		// Token: 0x0400212B RID: 8491
		private System.Windows.FontWeight _canonicalWeight;

		// Token: 0x0400212C RID: 8492
		private System.Windows.FontStretch _canonicalStretch;

		// Token: 0x0400212D RID: 8493
		private bool _nullFont;

		// Token: 0x0400212E RID: 8494
		private IList<ScaledShapeTypeface> _cachedScaledTypefaces = new List<ScaledShapeTypeface>(2);

		// Token: 0x0400212F RID: 8495
		private IDictionary<CultureInfo, TypefaceMap.IntMap> _intMaps = new Dictionary<CultureInfo, TypefaceMap.IntMap>();

		// Token: 0x04002130 RID: 8496
		private const int InitialScaledGlyphableTypefaceCount = 2;

		// Token: 0x04002131 RID: 8497
		private const int MaxTypefaceMapDepths = 32;

		// Token: 0x020009C6 RID: 2502
		internal class IntMap
		{
			// Token: 0x06005AED RID: 23277 RVA: 0x0016CCC4 File Offset: 0x0016C0C4
			public IntMap()
			{
				this._planes = new TypefaceMap.Plane[17];
				for (int i = 0; i < 17; i++)
				{
					this._planes[i] = TypefaceMap.IntMap.EmptyPlane;
				}
			}

			// Token: 0x06005AEE RID: 23278 RVA: 0x0016CD00 File Offset: 0x0016C100
			private void CreatePlane(int i)
			{
				Invariant.Assert(i < 17);
				if (this._planes[i] == TypefaceMap.IntMap.EmptyPlane)
				{
					TypefaceMap.Plane plane = new TypefaceMap.Plane();
					this._planes[i] = plane;
				}
			}

			// Token: 0x17001286 RID: 4742
			public ushort this[int i]
			{
				get
				{
					return this._planes[i >> 16][i >> 8 & 255][i & 255];
				}
				set
				{
					this.CreatePlane(i >> 16);
					this._planes[i >> 16].CreatePage(i >> 8 & 255, this);
					this._planes[i >> 16][i >> 8 & 255][i & 255] = value;
				}
			}

			// Token: 0x04002DEC RID: 11756
			private const byte NumberOfPlanes = 17;

			// Token: 0x04002DED RID: 11757
			private TypefaceMap.Plane[] _planes;

			// Token: 0x04002DEE RID: 11758
			private static TypefaceMap.Plane EmptyPlane = new TypefaceMap.Plane();
		}

		// Token: 0x020009C7 RID: 2503
		internal class Plane
		{
			// Token: 0x06005AF2 RID: 23282 RVA: 0x0016CDDC File Offset: 0x0016C1DC
			public Plane()
			{
				this._data = new TypefaceMap.Page[256];
				for (int i = 0; i < 256; i++)
				{
					this._data[i] = TypefaceMap.Plane.EmptyPage;
				}
			}

			// Token: 0x17001287 RID: 4743
			public TypefaceMap.Page this[int index]
			{
				get
				{
					return this._data[index];
				}
				set
				{
					this._data[index] = value;
				}
			}

			// Token: 0x06005AF5 RID: 23285 RVA: 0x0016CE4C File Offset: 0x0016C24C
			internal void CreatePage(int i, TypefaceMap.IntMap intMap)
			{
				if (this[i] == TypefaceMap.Plane.EmptyPage)
				{
					TypefaceMap.Page value = new TypefaceMap.Page();
					this[i] = value;
				}
			}

			// Token: 0x04002DEF RID: 11759
			private TypefaceMap.Page[] _data;

			// Token: 0x04002DF0 RID: 11760
			private static TypefaceMap.Page EmptyPage = new TypefaceMap.Page();
		}

		// Token: 0x020009C8 RID: 2504
		internal class Page
		{
			// Token: 0x06005AF7 RID: 23287 RVA: 0x0016CE90 File Offset: 0x0016C290
			public Page()
			{
				this._data = new ushort[256];
			}

			// Token: 0x17001288 RID: 4744
			public ushort this[int index]
			{
				get
				{
					return this._data[index];
				}
				set
				{
					this._data[index] = value;
				}
			}

			// Token: 0x04002DF1 RID: 11761
			private ushort[] _data;
		}
	}
}
