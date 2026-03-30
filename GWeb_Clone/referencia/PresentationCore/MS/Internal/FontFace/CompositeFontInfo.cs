using System;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal.FontFace
{
	// Token: 0x02000763 RID: 1891
	internal sealed class CompositeFontInfo
	{
		// Token: 0x06004FBB RID: 20411 RVA: 0x0013E880 File Offset: 0x0013DC80
		internal CompositeFontInfo()
		{
			this._familyNames = new LanguageSpecificStringDictionary(new Dictionary<XmlLanguage, string>(1));
			this._familyMaps = new FontFamilyMapCollection(this);
			this._defaultFamilyMapRanges = CompositeFontInfo.EmptyFamilyMapRanges;
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0013E8BC File Offset: 0x0013DCBC
		internal void PrepareToAddFamilyMap(FontFamilyMap familyMap)
		{
			if (familyMap == null)
			{
				throw new ArgumentNullException("familyMap");
			}
			if (string.IsNullOrEmpty(familyMap.Target))
			{
				throw new ArgumentException(SR.Get("FamilyMap_TargetNotSet"));
			}
			if (familyMap.Language != null)
			{
				if (this._familyMapRangesByLanguage == null)
				{
					this._familyMapRangesByLanguage = new Dictionary<XmlLanguage, ushort[]>(1);
					this._familyMapRangesByLanguage.Add(familyMap.Language, CompositeFontInfo.EmptyFamilyMapRanges);
					return;
				}
				if (!this._familyMapRangesByLanguage.ContainsKey(familyMap.Language))
				{
					this._familyMapRangesByLanguage.Add(familyMap.Language, CompositeFontInfo.EmptyFamilyMapRanges);
				}
			}
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0013E950 File Offset: 0x0013DD50
		internal void InvalidateFamilyMapRanges()
		{
			this._defaultFamilyMapRanges = CompositeFontInfo.EmptyFamilyMapRanges;
			if (this._familyMapRangesByLanguage != null)
			{
				Dictionary<XmlLanguage, ushort[]> dictionary = new Dictionary<XmlLanguage, ushort[]>(this._familyMapRangesByLanguage.Count);
				foreach (XmlLanguage key in this._familyMapRangesByLanguage.Keys)
				{
					dictionary.Add(key, CompositeFontInfo.EmptyFamilyMapRanges);
				}
				this._familyMapRangesByLanguage = dictionary;
			}
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x0013E9E4 File Offset: 0x0013DDE4
		internal ushort[] GetFamilyMapsOfLanguage(XmlLanguage language)
		{
			ushort[] array = null;
			if (this._familyMapRangesByLanguage != null && language != null)
			{
				foreach (XmlLanguage xmlLanguage in language.MatchingLanguages)
				{
					if (xmlLanguage.IetfLanguageTag.Length == 0)
					{
						break;
					}
					if (this._familyMapRangesByLanguage.TryGetValue(xmlLanguage, out array))
					{
						if (!this.IsFamilyMapRangesValid(array))
						{
							array = this.CreateFamilyMapRanges(xmlLanguage);
							this._familyMapRangesByLanguage[xmlLanguage] = array;
						}
						return array;
					}
				}
			}
			if (!this.IsFamilyMapRangesValid(this._defaultFamilyMapRanges))
			{
				this._defaultFamilyMapRanges = this.CreateFamilyMapRanges(null);
			}
			return this._defaultFamilyMapRanges;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0013EAB8 File Offset: 0x0013DEB8
		internal FontFamilyMap GetFamilyMapOfChar(ushort[] familyMapRanges, int ch)
		{
			for (int i = 1; i < familyMapRanges.Length; i += 2)
			{
				int num = (int)familyMapRanges[i];
				int num2 = (int)familyMapRanges[i + 1];
				for (int j = num; j < num2; j++)
				{
					FontFamilyMap fontFamilyMap = this._familyMaps[j];
					Invariant.Assert(fontFamilyMap != null);
					if (fontFamilyMap.InRange(ch))
					{
						return fontFamilyMap;
					}
				}
			}
			return FontFamilyMap.Default;
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0013EB14 File Offset: 0x0013DF14
		private bool IsFamilyMapRangesValid(ushort[] familyMapRanges)
		{
			return (int)familyMapRanges[0] == this._familyMaps.Count;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0013EB34 File Offset: 0x0013DF34
		private ushort[] CreateFamilyMapRanges(XmlLanguage language)
		{
			ushort[] array = new ushort[7];
			array[0] = (ushort)this._familyMaps.Count;
			int num = 1;
			for (int i = 0; i < this._familyMaps.Count; i++)
			{
				if (FontFamilyMap.MatchLanguage(this._familyMaps[i].Language, language))
				{
					if (num + 2 > array.Length)
					{
						ushort[] array2 = new ushort[array.Length * 2 - 1];
						array.CopyTo(array2, 0);
						array = array2;
					}
					array[num++] = (ushort)i;
					i++;
					while (i < this._familyMaps.Count && FontFamilyMap.MatchLanguage(this._familyMaps[i].Language, language))
					{
						i++;
					}
					array[num++] = (ushort)i;
				}
			}
			if (num < array.Length)
			{
				ushort[] array3 = new ushort[num];
				Array.Copy(array, array3, num);
				array = array3;
			}
			return array;
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06004FC2 RID: 20418 RVA: 0x0013EC0C File Offset: 0x0013E00C
		internal FamilyTypefaceCollection FamilyTypefaces
		{
			get
			{
				return this._familyTypefaces;
			}
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0013EC20 File Offset: 0x0013E020
		internal FamilyTypefaceCollection GetFamilyTypefaceList()
		{
			if (this._familyTypefaces == null)
			{
				this._familyTypefaces = new FamilyTypefaceCollection();
			}
			return this._familyTypefaces;
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06004FC4 RID: 20420 RVA: 0x0013EC48 File Offset: 0x0013E048
		// (set) Token: 0x06004FC5 RID: 20421 RVA: 0x0013EC5C File Offset: 0x0013E05C
		internal double Baseline
		{
			get
			{
				return this._baseline;
			}
			set
			{
				CompositeFontParser.VerifyNonNegativeMultiplierOfEm("Baseline", ref value);
				this._baseline = value;
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x0013EC7C File Offset: 0x0013E07C
		// (set) Token: 0x06004FC7 RID: 20423 RVA: 0x0013EC90 File Offset: 0x0013E090
		internal double LineSpacing
		{
			get
			{
				return this._lineSpacing;
			}
			set
			{
				CompositeFontParser.VerifyPositiveMultiplierOfEm("LineSpacing", ref value);
				this._lineSpacing = value;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x0013ECB0 File Offset: 0x0013E0B0
		internal LanguageSpecificStringDictionary FamilyNames
		{
			get
			{
				return this._familyNames;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x0013ECC4 File Offset: 0x0013E0C4
		internal FontFamilyMapCollection FamilyMaps
		{
			get
			{
				return this._familyMaps;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06004FCA RID: 20426 RVA: 0x0013ECD8 File Offset: 0x0013E0D8
		internal ICollection<XmlLanguage> FamilyMapLanguages
		{
			get
			{
				if (this._familyMapRangesByLanguage != null)
				{
					return this._familyMapRangesByLanguage.Keys;
				}
				return null;
			}
		}

		// Token: 0x0400242A RID: 9258
		private LanguageSpecificStringDictionary _familyNames;

		// Token: 0x0400242B RID: 9259
		private double _baseline;

		// Token: 0x0400242C RID: 9260
		private double _lineSpacing;

		// Token: 0x0400242D RID: 9261
		private FamilyTypefaceCollection _familyTypefaces;

		// Token: 0x0400242E RID: 9262
		private FontFamilyMapCollection _familyMaps;

		// Token: 0x0400242F RID: 9263
		private ushort[] _defaultFamilyMapRanges;

		// Token: 0x04002430 RID: 9264
		private Dictionary<XmlLanguage, ushort[]> _familyMapRangesByLanguage;

		// Token: 0x04002431 RID: 9265
		private const int InitialCultureCount = 1;

		// Token: 0x04002432 RID: 9266
		private const int InitialTargetFamilyCount = 1;

		// Token: 0x04002433 RID: 9267
		private static readonly ushort[] EmptyFamilyMapRanges = new ushort[1];

		// Token: 0x04002434 RID: 9268
		private const int InitialFamilyMapRangesCapacity = 7;

		// Token: 0x04002435 RID: 9269
		internal const int FirstFamilyMapRange = 1;
	}
}
