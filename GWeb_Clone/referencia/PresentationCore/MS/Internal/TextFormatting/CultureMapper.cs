using System;
using System.Globalization;
using System.Windows.Markup;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000700 RID: 1792
	internal static class CultureMapper
	{
		// Token: 0x06004D1C RID: 19740 RVA: 0x00131708 File Offset: 0x00130B08
		public static CultureInfo GetSpecificCulture(CultureInfo runCulture)
		{
			CultureInfo cultureInfo = TypeConverterHelper.InvariantEnglishUS;
			if (runCulture != null)
			{
				CultureMapper.CachedCultureMap cachedCultureMap = CultureMapper._cachedCultureMap;
				if (cachedCultureMap != null && cachedCultureMap.OriginalCulture == runCulture)
				{
					return cachedCultureMap.SpecificCulture;
				}
				if (runCulture != CultureInfo.InvariantCulture)
				{
					if (!runCulture.IsNeutralCulture)
					{
						cultureInfo = runCulture;
					}
					else
					{
						string name = runCulture.Name;
						if (!string.IsNullOrEmpty(name))
						{
							try
							{
								CultureInfo cultureInfo2 = CultureInfo.CreateSpecificCulture(name);
								cultureInfo = SafeSecurityHelper.GetCultureInfoByIetfLanguageTag(cultureInfo2.IetfLanguageTag);
							}
							catch (ArgumentException)
							{
								cultureInfo = TypeConverterHelper.InvariantEnglishUS;
							}
						}
					}
				}
				CultureMapper._cachedCultureMap = new CultureMapper.CachedCultureMap(runCulture, cultureInfo);
			}
			return cultureInfo;
		}

		// Token: 0x04002180 RID: 8576
		private static CultureMapper.CachedCultureMap _cachedCultureMap;

		// Token: 0x020009D8 RID: 2520
		private class CachedCultureMap
		{
			// Token: 0x06005B26 RID: 23334 RVA: 0x0016D58C File Offset: 0x0016C98C
			public CachedCultureMap(CultureInfo originalCulture, CultureInfo specificCulture)
			{
				this._originalCulture = originalCulture;
				this._specificCulture = specificCulture;
			}

			// Token: 0x1700128E RID: 4750
			// (get) Token: 0x06005B27 RID: 23335 RVA: 0x0016D5B0 File Offset: 0x0016C9B0
			public CultureInfo OriginalCulture
			{
				get
				{
					return this._originalCulture;
				}
			}

			// Token: 0x1700128F RID: 4751
			// (get) Token: 0x06005B28 RID: 23336 RVA: 0x0016D5C4 File Offset: 0x0016C9C4
			public CultureInfo SpecificCulture
			{
				get
				{
					return this._specificCulture;
				}
			}

			// Token: 0x04002E51 RID: 11857
			private CultureInfo _originalCulture;

			// Token: 0x04002E52 RID: 11858
			private CultureInfo _specificCulture;
		}
	}
}
