using System;
using System.Globalization;

namespace MS.Internal.FontCache
{
	// Token: 0x02000777 RID: 1911
	internal static class MajorLanguages
	{
		// Token: 0x06005085 RID: 20613 RVA: 0x001426BC File Offset: 0x00141ABC
		internal static bool Contains(ScriptTags script, LanguageTags langSys)
		{
			for (int i = 0; i < MajorLanguages.majorLanguages.Length; i++)
			{
				if (script == MajorLanguages.majorLanguages[i].Script && (langSys == LanguageTags.Default || langSys == MajorLanguages.majorLanguages[i].LangSys))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x0014270C File Offset: 0x00141B0C
		internal static bool Contains(CultureInfo culture)
		{
			if (culture == null)
			{
				return false;
			}
			if (culture == CultureInfo.InvariantCulture)
			{
				return true;
			}
			for (int i = 0; i < MajorLanguages.majorLanguages.Length; i++)
			{
				if (MajorLanguages.majorLanguages[i].Culture.Equals(culture) || MajorLanguages.majorLanguages[i].Culture.Equals(culture.Parent))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040024AF RID: 9391
		private static readonly MajorLanguages.MajorLanguageDesc[] majorLanguages = new MajorLanguages.MajorLanguageDesc[]
		{
			new MajorLanguages.MajorLanguageDesc(new CultureInfo("en"), ScriptTags.Latin, LanguageTags.English),
			new MajorLanguages.MajorLanguageDesc(new CultureInfo("de"), ScriptTags.Latin, LanguageTags.German),
			new MajorLanguages.MajorLanguageDesc(new CultureInfo("ja"), ScriptTags.CJKIdeographic, LanguageTags.Japanese),
			new MajorLanguages.MajorLanguageDesc(new CultureInfo("ja"), ScriptTags.Hiragana, LanguageTags.Japanese)
		};

		// Token: 0x020009F3 RID: 2547
		private struct MajorLanguageDesc
		{
			// Token: 0x06005BD4 RID: 23508 RVA: 0x0017109C File Offset: 0x0017049C
			internal MajorLanguageDesc(CultureInfo culture, ScriptTags script, LanguageTags langSys)
			{
				this.Culture = culture;
				this.Script = script;
				this.LangSys = langSys;
			}

			// Token: 0x04002F0B RID: 12043
			internal readonly CultureInfo Culture;

			// Token: 0x04002F0C RID: 12044
			internal readonly ScriptTags Script;

			// Token: 0x04002F0D RID: 12045
			internal readonly LanguageTags LangSys;
		}
	}
}
