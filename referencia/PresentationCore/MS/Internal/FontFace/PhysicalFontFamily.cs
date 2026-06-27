using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;

namespace MS.Internal.FontFace
{
	// Token: 0x0200076D RID: 1901
	internal sealed class PhysicalFontFamily : IFontFamily
	{
		// Token: 0x06005020 RID: 20512 RVA: 0x001408BC File Offset: 0x0013FCBC
		private static IDictionary<XmlLanguage, string> ConvertDictionary(IDictionary<CultureInfo, string> dictionary)
		{
			Dictionary<XmlLanguage, string> dictionary2 = new Dictionary<XmlLanguage, string>();
			foreach (KeyValuePair<CultureInfo, string> keyValuePair in dictionary)
			{
				XmlLanguage language = XmlLanguage.GetLanguage(keyValuePair.Key.Name);
				if (!dictionary2.ContainsKey(language))
				{
					dictionary2.Add(language, keyValuePair.Value);
				}
			}
			return dictionary2;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0014093C File Offset: 0x0013FD3C
		internal PhysicalFontFamily(MS.Internal.Text.TextInterface.FontFamily family)
		{
			Invariant.Assert(family != null);
			this._family = family;
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00140960 File Offset: 0x0013FD60
		ITypefaceMetrics IFontFamily.GetTypefaceMetrics(System.Windows.FontStyle style, System.Windows.FontWeight weight, System.Windows.FontStretch stretch)
		{
			return this.GetGlyphTypeface(style, weight, stretch);
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00140978 File Offset: 0x0013FD78
		IDeviceFont IFontFamily.GetDeviceFont(System.Windows.FontStyle style, System.Windows.FontWeight weight, System.Windows.FontStretch stretch)
		{
			return null;
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06005024 RID: 20516 RVA: 0x00140988 File Offset: 0x0013FD88
		IDictionary<XmlLanguage, string> IFontFamily.Names
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				if (this._familyNames == null)
				{
					this._familyNames = PhysicalFontFamily.ConvertDictionary(this._family.FamilyNames);
				}
				return this._familyNames;
			}
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x001409BC File Offset: 0x0013FDBC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal GlyphTypeface GetGlyphTypeface(System.Windows.FontStyle style, System.Windows.FontWeight weight, System.Windows.FontStretch stretch)
		{
			Font firstMatchingFont = this._family.GetFirstMatchingFont((MS.Internal.Text.TextInterface.FontWeight)weight.ToOpenTypeWeight(), (MS.Internal.Text.TextInterface.FontStretch)stretch.ToOpenTypeStretch(), (MS.Internal.Text.TextInterface.FontStyle)style.GetStyleForInternalConstruction());
			return new GlyphTypeface(firstMatchingFont);
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x001409F0 File Offset: 0x0013FDF0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal GlyphTypeface MapGlyphTypeface(System.Windows.FontStyle style, System.Windows.FontWeight weight, System.Windows.FontStretch stretch, CharacterBufferRange charString, CultureInfo digitCulture, ref int advance, ref int nextValid)
		{
			int num = charString.Length;
			MatchingStyle targetStyle = new MatchingStyle(style, weight, stretch);
			LegacyPriorityQueue<PhysicalFontFamily.MatchingFace> legacyPriorityQueue = new LegacyPriorityQueue<PhysicalFontFamily.MatchingFace>(checked((int)this._family.Count), new PhysicalFontFamily.MatchingFaceComparer(targetStyle));
			foreach (Font face in this._family)
			{
				legacyPriorityQueue.Push(new PhysicalFontFamily.MatchingFace(face));
			}
			Font font = null;
			while (legacyPriorityQueue.Count != 0)
			{
				int num2 = 0;
				Font fontFace = legacyPriorityQueue.Top.FontFace;
				int num3 = this.MapCharacters(fontFace, charString, digitCulture, ref num2);
				if (num3 > 0)
				{
					if (num > 0 && num < num3)
					{
						advance = num;
						nextValid = 0;
					}
					else
					{
						advance = num3;
						nextValid = num2;
					}
					return new GlyphTypeface(fontFace);
				}
				if (num2 < num)
				{
					num = num2;
				}
				if (font == null)
				{
					font = fontFace;
				}
				legacyPriorityQueue.Pop();
			}
			advance = 0;
			nextValid = num;
			return new GlyphTypeface(font);
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x00140AF8 File Offset: 0x0013FEF8
		private int MapCharacters(Font font, CharacterBufferRange unicodeString, CultureInfo digitCulture, ref int nextValid)
		{
			DigitMap digitMap = new DigitMap(digitCulture);
			int num = 0;
			int i = Classification.AdvanceWhile(unicodeString, ItemClass.JoinerClass);
			if (i >= unicodeString.Length)
			{
				return i;
			}
			bool flag = false;
			while (i < unicodeString.Length)
			{
				int num2 = Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, i, unicodeString.Length - i), out num);
				checked
				{
					if (!Classification.IsJoiner(num2))
					{
						if (!Classification.IsCombining(num2))
						{
							flag = true;
						}
						else if (flag)
						{
							goto IL_81;
						}
						int num3 = digitMap[num2];
						if (!font.HasCharacter((uint)num3))
						{
							if (num3 == num2)
							{
								break;
							}
							num3 = DigitMap.GetFallbackCharacter(num3);
							if (num3 == 0 || !font.HasCharacter((uint)num3))
							{
								break;
							}
						}
					}
					IL_81:;
				}
				i += num;
			}
			if (i < unicodeString.Length)
			{
				for (nextValid = i + num; nextValid < unicodeString.Length; nextValid += num)
				{
					int num4 = Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, nextValid, unicodeString.Length - nextValid), out num);
					int num5 = digitMap[num4];
					checked
					{
						if (!Classification.IsJoiner(num5) && (!flag || !Classification.IsCombining(num5)))
						{
							if (font.HasCharacter((uint)num5))
							{
								break;
							}
							if (num5 != num4)
							{
								num5 = DigitMap.GetFallbackCharacter(num5);
								if (num5 != 0 && font.HasCharacter((uint)num5))
								{
									break;
								}
							}
						}
					}
				}
			}
			return i;
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x00140C20 File Offset: 0x00140020
		[SecurityTreatAsSafe]
		[SecurityCritical]
		double IFontFamily.Baseline(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode)
		{
			if (textFormattingMode == TextFormattingMode.Ideal)
			{
				return emSize * this._family.Metrics.Baseline;
			}
			double num = emSize * toReal;
			return TextFormatterImp.RoundDipForDisplayMode(this._family.DisplayMetrics((float)num, (float)pixelsPerDip).Baseline * num, pixelsPerDip) / toReal;
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x00140C68 File Offset: 0x00140068
		double IFontFamily.BaselineDesign
		{
			get
			{
				return ((IFontFamily)this).Baseline(1.0, 1.0, 1.0, TextFormattingMode.Ideal);
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x00140C98 File Offset: 0x00140098
		double IFontFamily.LineSpacingDesign
		{
			get
			{
				return ((IFontFamily)this).LineSpacing(1.0, 1.0, 1.0, TextFormattingMode.Ideal);
			}
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x00140CC8 File Offset: 0x001400C8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		double IFontFamily.LineSpacing(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode)
		{
			if (textFormattingMode == TextFormattingMode.Ideal)
			{
				return emSize * this._family.Metrics.LineSpacing;
			}
			double num = emSize * toReal;
			return TextFormatterImp.RoundDipForDisplayMode(this._family.DisplayMetrics((float)num, (float)pixelsPerDip).LineSpacing * num, pixelsPerDip) / toReal;
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x00140D10 File Offset: 0x00140110
		ICollection<Typeface> IFontFamily.GetTypefaces(FontFamilyIdentifier familyIdentifier)
		{
			return new TypefaceCollection(new System.Windows.Media.FontFamily(familyIdentifier), this._family);
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x00140D34 File Offset: 0x00140134
		bool IFontFamily.GetMapTargetFamilyNameAndScale(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, double defaultSizeInEm, out int cchAdvance, out string targetFamilyName, out double scaleInEm)
		{
			cchAdvance = unicodeString.Length;
			targetFamilyName = null;
			scaleInEm = defaultSizeInEm;
			return false;
		}

		// Token: 0x04002476 RID: 9334
		private MS.Internal.Text.TextInterface.FontFamily _family;

		// Token: 0x04002477 RID: 9335
		private IDictionary<XmlLanguage, string> _familyNames;

		// Token: 0x020009EC RID: 2540
		private struct MatchingFace
		{
			// Token: 0x06005BA4 RID: 23460 RVA: 0x00170680 File Offset: 0x0016FA80
			[SecurityCritical]
			internal MatchingFace(Font face)
			{
				this._face = face;
				this._style = new MatchingStyle(new System.Windows.FontStyle((int)face.Style), new System.Windows.FontWeight((int)face.Weight), new System.Windows.FontStretch((int)face.Stretch));
			}

			// Token: 0x170012BB RID: 4795
			// (get) Token: 0x06005BA5 RID: 23461 RVA: 0x001706C0 File Offset: 0x0016FAC0
			internal Font FontFace
			{
				get
				{
					return this._face;
				}
			}

			// Token: 0x170012BC RID: 4796
			// (get) Token: 0x06005BA6 RID: 23462 RVA: 0x001706D4 File Offset: 0x0016FAD4
			internal MatchingStyle MatchingStyle
			{
				get
				{
					return this._style;
				}
			}

			// Token: 0x04002EF5 RID: 12021
			private Font _face;

			// Token: 0x04002EF6 RID: 12022
			private MatchingStyle _style;
		}

		// Token: 0x020009ED RID: 2541
		private class MatchingFaceComparer : IComparer<PhysicalFontFamily.MatchingFace>
		{
			// Token: 0x06005BA7 RID: 23463 RVA: 0x001706E8 File Offset: 0x0016FAE8
			internal MatchingFaceComparer(MatchingStyle targetStyle)
			{
				this._targetStyle = targetStyle;
			}

			// Token: 0x06005BA8 RID: 23464 RVA: 0x00170704 File Offset: 0x0016FB04
			int IComparer<PhysicalFontFamily.MatchingFace>.Compare(PhysicalFontFamily.MatchingFace a, PhysicalFontFamily.MatchingFace b)
			{
				if (!a.MatchingStyle.IsBetterMatch(this._targetStyle, b.MatchingStyle))
				{
					return 1;
				}
				return -1;
			}

			// Token: 0x04002EF7 RID: 12023
			private MatchingStyle _targetStyle;
		}
	}
}
