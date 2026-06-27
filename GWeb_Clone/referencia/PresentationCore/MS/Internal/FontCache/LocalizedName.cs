using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace MS.Internal.FontCache
{
	// Token: 0x0200077D RID: 1917
	internal class LocalizedName
	{
		// Token: 0x060050B9 RID: 20665 RVA: 0x001434C8 File Offset: 0x001428C8
		internal LocalizedName(XmlLanguage language, string name) : this(language, name, language.GetEquivalentCulture().LCID)
		{
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x001434E8 File Offset: 0x001428E8
		internal LocalizedName(XmlLanguage language, string name, int originalLCID)
		{
			this._language = language;
			this._name = name;
			this._originalLCID = originalLCID;
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x00143510 File Offset: 0x00142910
		internal XmlLanguage Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x00143524 File Offset: 0x00142924
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x00143538 File Offset: 0x00142938
		internal int OriginalLCID
		{
			get
			{
				return this._originalLCID;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x0014354C File Offset: 0x0014294C
		internal static IComparer<LocalizedName> NameComparer
		{
			get
			{
				return LocalizedName._nameComparer;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x00143560 File Offset: 0x00142960
		internal static IComparer<LocalizedName> LanguageComparer
		{
			get
			{
				return LocalizedName._languageComparer;
			}
		}

		// Token: 0x040024CA RID: 9418
		private XmlLanguage _language;

		// Token: 0x040024CB RID: 9419
		private string _name;

		// Token: 0x040024CC RID: 9420
		private int _originalLCID;

		// Token: 0x040024CD RID: 9421
		private static LocalizedName.NameComparerClass _nameComparer = new LocalizedName.NameComparerClass();

		// Token: 0x040024CE RID: 9422
		private static LocalizedName.LanguageComparerClass _languageComparer = new LocalizedName.LanguageComparerClass();

		// Token: 0x020009F4 RID: 2548
		private class NameComparerClass : IComparer<LocalizedName>
		{
			// Token: 0x06005BD5 RID: 23509 RVA: 0x001710C0 File Offset: 0x001704C0
			int IComparer<LocalizedName>.Compare(LocalizedName x, LocalizedName y)
			{
				return Util.CompareOrdinalIgnoreCase(x._name, y._name);
			}
		}

		// Token: 0x020009F5 RID: 2549
		private class LanguageComparerClass : IComparer<LocalizedName>
		{
			// Token: 0x06005BD7 RID: 23511 RVA: 0x001710F4 File Offset: 0x001704F4
			int IComparer<LocalizedName>.Compare(LocalizedName x, LocalizedName y)
			{
				return string.Compare(x._language.IetfLanguageTag, y._language.IetfLanguageTag, StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
