using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MS.Internal.FontFace;
using MS.Internal.TextFormatting;

namespace MS.Internal.Shaping
{
	// Token: 0x020006AA RID: 1706
	internal sealed class CompositeFontFamily : IFontFamily
	{
		// Token: 0x06004AA8 RID: 19112 RVA: 0x0012340C File Offset: 0x0012280C
		internal CompositeFontFamily() : this(new CompositeFontInfo())
		{
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00123424 File Offset: 0x00122824
		internal CompositeFontFamily(CompositeFontInfo fontInfo)
		{
			this._fontInfo = fontInfo;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00123440 File Offset: 0x00122840
		internal CompositeFontFamily(string friendlyName) : this(friendlyName, null)
		{
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00123458 File Offset: 0x00122858
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal CompositeFontFamily(string friendlyName, IFontFamily firstFontFamily) : this()
		{
			this.FamilyMaps.Add(new FontFamilyMap(0, 1114111, null, friendlyName, 1.0));
			this._firstFontFamily = firstFontFamily;
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004AAC RID: 19116 RVA: 0x00123494 File Offset: 0x00122894
		IDictionary<XmlLanguage, string> IFontFamily.Names
		{
			get
			{
				return this._fontInfo.FamilyNames;
			}
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x001234AC File Offset: 0x001228AC
		public double Baseline(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode)
		{
			if (textFormattingMode == TextFormattingMode.Ideal)
			{
				return ((IFontFamily)this).BaselineDesign * emSize;
			}
			if (this._fontInfo.Baseline != 0.0)
			{
				return Math.Round(this._fontInfo.Baseline * emSize);
			}
			return this.GetFirstFontFamily().Baseline(emSize, toReal, pixelsPerDip, textFormattingMode);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00123500 File Offset: 0x00122900
		public void SetBaseline(double value)
		{
			this._fontInfo.Baseline = value;
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0012351C File Offset: 0x0012291C
		public double LineSpacing(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode)
		{
			if (textFormattingMode == TextFormattingMode.Ideal)
			{
				return ((IFontFamily)this).LineSpacingDesign * emSize;
			}
			if (this._fontInfo.LineSpacing != 0.0)
			{
				return Math.Round(this._fontInfo.LineSpacing * emSize);
			}
			return this.GetFirstFontFamily().LineSpacing(emSize, toReal, pixelsPerDip, textFormattingMode);
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x00123570 File Offset: 0x00122970
		double IFontFamily.BaselineDesign
		{
			get
			{
				if (this._fontInfo.Baseline == 0.0)
				{
					this._fontInfo.Baseline = this.GetFirstFontFamily().BaselineDesign;
				}
				return this._fontInfo.Baseline;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x001235B4 File Offset: 0x001229B4
		double IFontFamily.LineSpacingDesign
		{
			get
			{
				if (this._fontInfo.LineSpacing == 0.0)
				{
					this._fontInfo.LineSpacing = this.GetFirstFontFamily().LineSpacingDesign;
				}
				return this._fontInfo.LineSpacing;
			}
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x001235F8 File Offset: 0x001229F8
		public void SetLineSpacing(double value)
		{
			this._fontInfo.LineSpacing = value;
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x00123614 File Offset: 0x00122A14
		ITypefaceMetrics IFontFamily.GetTypefaceMetrics(FontStyle style, FontWeight weight, FontStretch stretch)
		{
			if (this._fontInfo.FamilyTypefaces == null && this._fontInfo.FamilyMaps.Count == 1 && this._fontInfo.FamilyMaps[0].IsSimpleFamilyMap)
			{
				return this.GetFirstFontFamily().GetTypefaceMetrics(style, weight, stretch);
			}
			return this.FindTypefaceMetrics(style, weight, stretch);
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00123674 File Offset: 0x00122A74
		IDeviceFont IFontFamily.GetDeviceFont(FontStyle style, FontWeight weight, FontStretch stretch)
		{
			FamilyTypeface familyTypeface = this.FindExactFamilyTypeface(style, weight, stretch);
			if (familyTypeface != null && familyTypeface.DeviceFontName != null)
			{
				return familyTypeface;
			}
			return null;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0012369C File Offset: 0x00122A9C
		bool IFontFamily.GetMapTargetFamilyNameAndScale(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, double defaultSizeInEm, out int cchAdvance, out string targetFamilyName, out double scaleInEm)
		{
			Invariant.Assert(unicodeString.CharacterBuffer != null && unicodeString.Length > 0);
			Invariant.Assert(culture != null);
			FontFamilyMap targetFamilyMap = this.GetTargetFamilyMap(unicodeString, culture, digitCulture, out cchAdvance);
			targetFamilyName = targetFamilyMap.Target;
			scaleInEm = targetFamilyMap.Scale;
			return true;
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x001236EC File Offset: 0x00122AEC
		ICollection<Typeface> IFontFamily.GetTypefaces(FontFamilyIdentifier familyIdentifier)
		{
			return new TypefaceCollection(new FontFamily(familyIdentifier), this.FamilyTypefaces);
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x00123710 File Offset: 0x00122B10
		internal LanguageSpecificStringDictionary FamilyNames
		{
			get
			{
				return this._fontInfo.FamilyNames;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x00123728 File Offset: 0x00122B28
		internal FamilyTypefaceCollection FamilyTypefaces
		{
			get
			{
				return this._fontInfo.GetFamilyTypefaceList();
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x00123740 File Offset: 0x00122B40
		internal FontFamilyMapCollection FamilyMaps
		{
			get
			{
				return this._fontInfo.FamilyMaps;
			}
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00123758 File Offset: 0x00122B58
		private FontFamilyMap GetTargetFamilyMap(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, out int cchAdvance)
		{
			DigitMap digitMap = new DigitMap(digitCulture);
			ushort[] familyMapsOfLanguage = this._fontInfo.GetFamilyMapsOfLanguage(XmlLanguage.GetLanguage(culture.IetfLanguageTag));
			int num = 0;
			cchAdvance = Classification.AdvanceWhile(unicodeString, ItemClass.JoinerClass);
			if (cchAdvance >= unicodeString.Length)
			{
				return this._fontInfo.GetFamilyMapOfChar(familyMapsOfLanguage, Classification.UnicodeScalar(unicodeString, out num));
			}
			int num2 = Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, cchAdvance, unicodeString.Length - cchAdvance), out num);
			bool flag = !Classification.IsCombining(num2);
			num2 = digitMap[num2];
			FontFamilyMap familyMapOfChar = this._fontInfo.GetFamilyMapOfChar(familyMapsOfLanguage, num2);
			Invariant.Assert(familyMapOfChar != null);
			for (cchAdvance += num; cchAdvance < unicodeString.Length; cchAdvance += num)
			{
				num2 = Classification.UnicodeScalar(new CharacterBufferRange(unicodeString, cchAdvance, unicodeString.Length - cchAdvance), out num);
				if (!Classification.IsJoiner(num2))
				{
					if (!Classification.IsCombining(num2))
					{
						flag = true;
					}
					else if (flag)
					{
						goto IL_ED;
					}
					num2 = digitMap[num2];
					if (this._fontInfo.GetFamilyMapOfChar(familyMapsOfLanguage, num2) != familyMapOfChar)
					{
						break;
					}
				}
				IL_ED:;
			}
			return familyMapOfChar;
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00123868 File Offset: 0x00122C68
		private IFontFamily GetFirstFontFamily()
		{
			if (this._firstFontFamily == null)
			{
				if (this._fontInfo.FamilyMaps.Count != 0)
				{
					this._firstFontFamily = FontFamily.FindFontFamilyFromFriendlyNameList(this._fontInfo.FamilyMaps[0].Target);
				}
				else
				{
					this._firstFontFamily = FontFamily.LookupFontFamily(FontFamily.NullFontFamilyCanonicalName);
				}
				Invariant.Assert(this._firstFontFamily != null);
			}
			return this._firstFontFamily;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x001238D8 File Offset: 0x00122CD8
		private ITypefaceMetrics FindTypefaceMetrics(FontStyle style, FontWeight weight, FontStretch stretch)
		{
			FamilyTypeface familyTypeface = this.FindNearestFamilyTypeface(style, weight, stretch);
			if (familyTypeface == null)
			{
				return new CompositeTypefaceMetrics();
			}
			return familyTypeface;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x001238FC File Offset: 0x00122CFC
		private FamilyTypeface FindNearestFamilyTypeface(FontStyle style, FontWeight weight, FontStretch stretch)
		{
			if (this._fontInfo.FamilyTypefaces == null || this._fontInfo.FamilyTypefaces.Count == 0)
			{
				return null;
			}
			FamilyTypeface familyTypeface = this._fontInfo.FamilyTypefaces[0];
			MatchingStyle best = new MatchingStyle(familyTypeface.Style, familyTypeface.Weight, familyTypeface.Stretch);
			MatchingStyle target = new MatchingStyle(style, weight, stretch);
			for (int i = 1; i < this._fontInfo.FamilyTypefaces.Count; i++)
			{
				FamilyTypeface familyTypeface2 = this._fontInfo.FamilyTypefaces[i];
				MatchingStyle matchingStyle = new MatchingStyle(familyTypeface2.Style, familyTypeface2.Weight, familyTypeface2.Stretch);
				if (MatchingStyle.IsBetterMatch(target, best, ref matchingStyle))
				{
					familyTypeface = familyTypeface2;
					best = matchingStyle;
				}
			}
			return familyTypeface;
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x001239B8 File Offset: 0x00122DB8
		private FamilyTypeface FindExactFamilyTypeface(FontStyle style, FontWeight weight, FontStretch stretch)
		{
			if (this._fontInfo.FamilyTypefaces == null || this._fontInfo.FamilyTypefaces.Count == 0)
			{
				return null;
			}
			MatchingStyle r = new MatchingStyle(style, weight, stretch);
			foreach (FamilyTypeface familyTypeface in this._fontInfo.FamilyTypefaces)
			{
				MatchingStyle l = new MatchingStyle(familyTypeface.Style, familyTypeface.Weight, familyTypeface.Stretch);
				if (l == r)
				{
					return familyTypeface;
				}
			}
			return null;
		}

		// Token: 0x04001F9D RID: 8093
		private readonly CompositeFontInfo _fontInfo;

		// Token: 0x04001F9E RID: 8094
		private IFontFamily _firstFontFamily;
	}
}
