using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using MS.Internal.PresentationCore;

namespace System.Windows.Markup
{
	/// <summary>Representa uma marca de idioma para uso na marcação XAML.</summary>
	// Token: 0x02000202 RID: 514
	[TypeConverter(typeof(XmlLanguageConverter))]
	public class XmlLanguage
	{
		// Token: 0x06000D47 RID: 3399 RVA: 0x000324D0 File Offset: 0x000318D0
		private XmlLanguage(string lowercase)
		{
			this._lowerCaseTag = lowercase;
			this._equivalentCulture = null;
			this._specificCulture = null;
			this._compatibleCulture = null;
			this._specificity = -1;
			this._equivalentCultureFailed = false;
		}

		/// <summary>Obtém uma instância <see cref="T:System.Windows.Markup.XmlLanguage" /> estática como seria criada por <see cref="M:System.Windows.Markup.XmlLanguage.GetLanguage(System.String)" /> com a marca de linguagem como uma cadeia de caracteres de atributo vazia.</summary>
		/// <returns>A versão de marca de idioma vazio do <see cref="T:System.Windows.Markup.XmlLanguage" />, para uso em operações de comparação.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00032510 File Offset: 0x00031910
		public static XmlLanguage Empty
		{
			get
			{
				if (XmlLanguage._empty == null)
				{
					XmlLanguage._empty = XmlLanguage.GetLanguage(string.Empty);
				}
				return XmlLanguage._empty;
			}
		}

		/// <summary>Retorna uma instância de <see cref="T:System.Windows.Markup.XmlLanguage" /> com base em uma cadeia de caracteres que representa o idioma por RFC 3066.</summary>
		/// <param name="ietfLanguageTag">Uma cadeia de caracteres de idioma de RFC 3066 ou cadeia de caracteres vazia.</param>
		/// <returns>Um novo <see cref="T:System.Windows.Markup.XmlLanguage" /> com a cadeia de caracteres fornecida como seu valor <see cref="P:System.Windows.Markup.XmlLanguage.IetfLanguageTag" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">O parâmetro <paramref name="ietfLanguageTag" /> não pode ser nulo.</exception>
		/// <exception cref="T:System.ArgumentException">O parâmetro <paramref name="ietfLanguageTag" /> era não vazio, mas não estava em conformidade com a sintaxe especificada em RFC 3066.</exception>
		// Token: 0x06000D49 RID: 3401 RVA: 0x00032538 File Offset: 0x00031938
		public static XmlLanguage GetLanguage(string ietfLanguageTag)
		{
			if (ietfLanguageTag == null)
			{
				throw new ArgumentNullException("ietfLanguageTag");
			}
			string text = XmlLanguage.AsciiToLower(ietfLanguageTag);
			XmlLanguage xmlLanguage = (XmlLanguage)XmlLanguage._cache[text];
			if (xmlLanguage == null)
			{
				XmlLanguage.ValidateLowerCaseTag(text);
				object syncRoot = XmlLanguage._cache.SyncRoot;
				lock (syncRoot)
				{
					xmlLanguage = (XmlLanguage)XmlLanguage._cache[text];
					if (xmlLanguage == null)
					{
						xmlLanguage = (XmlLanguage._cache[text] = new XmlLanguage(text));
					}
				}
			}
			return xmlLanguage;
		}

		/// <summary>Obtém a representação de cadeia de caracteres da marca de linguagem.</summary>
		/// <returns>A representação de cadeia de caracteres da marca de linguagem.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x000325DC File Offset: 0x000319DC
		public string IetfLanguageTag
		{
			get
			{
				return this._lowerCaseTag;
			}
		}

		/// <summary>Retorna um <see cref="T:System.String" /> que representa o <see cref="T:System.Windows.Markup.XmlLanguage" /> atual.</summary>
		/// <returns>Um <see cref="T:System.String" /> que representa o <see cref="T:System.Windows.Markup.XmlLanguage" /> atual.</returns>
		// Token: 0x06000D4B RID: 3403 RVA: 0x000325F0 File Offset: 0x000319F0
		public override string ToString()
		{
			return this.IetfLanguageTag;
		}

		/// <summary>Retorna a <see cref="T:System.Globalization.CultureInfo" /> equivalente apropriada para esta <see cref="T:System.Windows.Markup.XmlLanguage" />, se e somente se uma <see cref="T:System.Globalization.CultureInfo" /> desse tipo for registrada para o valor de <see cref="P:System.Windows.Markup.XmlLanguage.IetfLanguageTag" /> dessa <see cref="T:System.Windows.Markup.XmlLanguage" /></summary>
		/// <returns>Uma <see cref="T:System.Globalization.CultureInfo" /> que pode ser usada por chamadas à API de globalização de localização que usam esse tipo como um argumento.</returns>
		/// <exception cref="T:System.InvalidOperationException">Não existe nenhuma <see cref="T:System.Globalization.CultureInfo" /> registrada para a <see cref="T:System.Windows.Markup.XmlLanguage" /> fornecida, conforme determinado por uma chamada ao <see cref="M:System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag(System.String)" />.</exception>
		// Token: 0x06000D4C RID: 3404 RVA: 0x00032604 File Offset: 0x00031A04
		public CultureInfo GetEquivalentCulture()
		{
			if (this._equivalentCulture == null)
			{
				string text = this._lowerCaseTag;
				if (string.CompareOrdinal(text, "und") == 0)
				{
					text = string.Empty;
				}
				try
				{
					this._equivalentCulture = SafeSecurityHelper.GetCultureInfoByIetfLanguageTag(text);
				}
				catch (ArgumentException innerException)
				{
					this._equivalentCultureFailed = true;
					throw new InvalidOperationException(SR.Get("XmlLangGetCultureFailure", new object[]
					{
						text
					}), innerException);
				}
			}
			return this._equivalentCulture;
		}

		/// <summary>Retorna a <see cref="T:System.Globalization.CultureInfo" /> não neutra mais próxima relacionada para esta <see cref="T:System.Windows.Markup.XmlLanguage" />.</summary>
		/// <returns>Uma <see cref="T:System.Globalization.CultureInfo" /> que pode ser usada para chamadas ao API de globalização de localização que usam esse tipo como um argumento.</returns>
		/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Globalization.CultureInfo" /> não neutra não relacionada está registrado para a <see cref="T:System.Windows.Markup.XmlLanguage" /><see cref="P:System.Windows.Markup.XmlLanguage.IetfLanguageTag" /> atual.</exception>
		// Token: 0x06000D4D RID: 3405 RVA: 0x0003268C File Offset: 0x00031A8C
		public CultureInfo GetSpecificCulture()
		{
			if (this._specificCulture == null)
			{
				if (this._lowerCaseTag.Length == 0 || string.CompareOrdinal(this._lowerCaseTag, "und") == 0)
				{
					this._specificCulture = this.GetEquivalentCulture();
				}
				else
				{
					CultureInfo cultureInfo = this.GetCompatibleCulture();
					if (cultureInfo.IetfLanguageTag.Length == 0)
					{
						throw new InvalidOperationException(SR.Get("XmlLangGetSpecificCulture", new object[]
						{
							this._lowerCaseTag
						}));
					}
					if (!cultureInfo.IsNeutralCulture)
					{
						this._specificCulture = cultureInfo;
					}
					else
					{
						try
						{
							cultureInfo = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
							this._specificCulture = SafeSecurityHelper.GetCultureInfoByIetfLanguageTag(cultureInfo.IetfLanguageTag);
						}
						catch (ArgumentException innerException)
						{
							throw new InvalidOperationException(SR.Get("XmlLangGetSpecificCulture", new object[]
							{
								this._lowerCaseTag
							}), innerException);
						}
					}
				}
			}
			return this._specificCulture;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00032780 File Offset: 0x00031B80
		[FriendAccessAllowed]
		internal CultureInfo GetCompatibleCulture()
		{
			if (this._compatibleCulture == null)
			{
				CultureInfo cultureInfo = null;
				if (!this.TryGetEquivalentCulture(out cultureInfo))
				{
					string text = this.IetfLanguageTag;
					do
					{
						text = XmlLanguage.Shorten(text);
						if (text == null)
						{
							cultureInfo = CultureInfo.InvariantCulture;
						}
						else
						{
							try
							{
								cultureInfo = SafeSecurityHelper.GetCultureInfoByIetfLanguageTag(text);
							}
							catch (ArgumentException)
							{
							}
						}
					}
					while (cultureInfo == null);
				}
				this._compatibleCulture = cultureInfo;
			}
			return this._compatibleCulture;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000327F4 File Offset: 0x00031BF4
		[FriendAccessAllowed]
		internal bool RangeIncludes(XmlLanguage language)
		{
			return this.IsPrefixOf(language.IetfLanguageTag) || this.RangeIncludes(language.GetCompatibleCulture());
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00032820 File Offset: 0x00031C20
		internal bool RangeIncludes(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			for (int i = 0; i < 32; i++)
			{
				if (this.IsPrefixOf(culture.IetfLanguageTag))
				{
					return true;
				}
				CultureInfo parent = culture.Parent;
				if (parent == null || parent.Equals(CultureInfo.InvariantCulture) || parent == culture)
				{
					break;
				}
				culture = parent;
			}
			return false;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00032878 File Offset: 0x00031C78
		internal int GetSpecificity()
		{
			if (this._specificity < 0)
			{
				CultureInfo compatibleCulture = this.GetCompatibleCulture();
				int num = XmlLanguage.GetSpecificity(compatibleCulture, 32);
				if (compatibleCulture != this._equivalentCulture)
				{
					num += XmlLanguage.GetSubtagCount(this._lowerCaseTag) - XmlLanguage.GetSubtagCount(compatibleCulture.IetfLanguageTag);
				}
				this._specificity = num;
			}
			return this._specificity;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000328D0 File Offset: 0x00031CD0
		private static int GetSpecificity(CultureInfo culture, int maxDepth)
		{
			int result = 0;
			if (maxDepth != 0 && culture != null)
			{
				string ietfLanguageTag = culture.IetfLanguageTag;
				if (ietfLanguageTag.Length > 0)
				{
					result = Math.Max(XmlLanguage.GetSubtagCount(ietfLanguageTag), 1 + XmlLanguage.GetSpecificity(culture.Parent, maxDepth - 1));
				}
			}
			return result;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00032914 File Offset: 0x00031D14
		private static int GetSubtagCount(string languageTag)
		{
			int length = languageTag.Length;
			int num = 0;
			if (length > 0)
			{
				num = 1;
				for (int i = 0; i < length; i++)
				{
					if (languageTag[i] == '-')
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0003294C File Offset: 0x00031D4C
		internal XmlLanguage.MatchingLanguageCollection MatchingLanguages
		{
			get
			{
				return new XmlLanguage.MatchingLanguageCollection(this);
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00032960 File Offset: 0x00031D60
		private bool IsPrefixOf(string longTag)
		{
			string ietfLanguageTag = this.IetfLanguageTag;
			return longTag.StartsWith(ietfLanguageTag, StringComparison.OrdinalIgnoreCase) && (ietfLanguageTag.Length == 0 || ietfLanguageTag.Length == longTag.Length || longTag[ietfLanguageTag.Length] == '-');
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000329A8 File Offset: 0x00031DA8
		private bool TryGetEquivalentCulture(out CultureInfo culture)
		{
			culture = null;
			if (this._equivalentCulture == null && !this._equivalentCultureFailed)
			{
				try
				{
					this.GetEquivalentCulture();
				}
				catch (InvalidOperationException)
				{
				}
			}
			culture = this._equivalentCulture;
			return culture != null;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00032A00 File Offset: 0x00031E00
		private XmlLanguage PrefixLanguage
		{
			get
			{
				string ietfLanguageTag = XmlLanguage.Shorten(this.IetfLanguageTag);
				return XmlLanguage.GetLanguage(ietfLanguageTag);
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00032A20 File Offset: 0x00031E20
		private static string Shorten(string languageTag)
		{
			if (languageTag.Length == 0)
			{
				return null;
			}
			int num = languageTag.Length - 1;
			while (languageTag[num] != '-' && num > 0)
			{
				num--;
			}
			return languageTag.Substring(0, num);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00032A60 File Offset: 0x00031E60
		private static void ValidateLowerCaseTag(string ietfLanguageTag)
		{
			if (ietfLanguageTag == null)
			{
				throw new ArgumentNullException("ietfLanguageTag");
			}
			if (ietfLanguageTag.Length > 0)
			{
				using (StringReader stringReader = new StringReader(ietfLanguageTag))
				{
					for (int num = XmlLanguage.ParseSubtag(ietfLanguageTag, stringReader, true); num != -1; num = XmlLanguage.ParseSubtag(ietfLanguageTag, stringReader, false))
					{
					}
				}
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00032ACC File Offset: 0x00031ECC
		private static int ParseSubtag(string ietfLanguageTag, StringReader reader, bool isPrimary)
		{
			int num = reader.Read();
			bool flag = XmlLanguage.IsLowerAlpha(num);
			if (!flag && !isPrimary)
			{
				flag = XmlLanguage.IsDigit(num);
			}
			if (!flag)
			{
				XmlLanguage.ThrowParseException(ietfLanguageTag);
			}
			int num2 = 1;
			for (;;)
			{
				num = reader.Read();
				num2++;
				flag = XmlLanguage.IsLowerAlpha(num);
				if (!flag && !isPrimary)
				{
					flag = XmlLanguage.IsDigit(num);
				}
				if (!flag)
				{
					if (num == -1 || num == 45)
					{
						break;
					}
					XmlLanguage.ThrowParseException(ietfLanguageTag);
				}
				else if (num2 > 8)
				{
					XmlLanguage.ThrowParseException(ietfLanguageTag);
				}
			}
			return num;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00032B40 File Offset: 0x00031F40
		private static bool IsLowerAlpha(int c)
		{
			return c >= 97 && c <= 122;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00032B5C File Offset: 0x00031F5C
		private static bool IsDigit(int c)
		{
			return c >= 48 && c <= 57;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00032B78 File Offset: 0x00031F78
		private static void ThrowParseException(string ietfLanguageTag)
		{
			throw new ArgumentException(SR.Get("XmlLangMalformed", new object[]
			{
				ietfLanguageTag
			}), "ietfLanguageTag");
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00032BA4 File Offset: 0x00031FA4
		private static string AsciiToLower(string tag)
		{
			int length = tag.Length;
			for (int i = 0; i < length; i++)
			{
				if (tag[i] > '\u007f')
				{
					XmlLanguage.ThrowParseException(tag);
				}
			}
			return tag.ToLowerInvariant();
		}

		// Token: 0x040007FC RID: 2044
		private static Hashtable _cache = new Hashtable(10);

		// Token: 0x040007FD RID: 2045
		private const int InitialDictionarySize = 10;

		// Token: 0x040007FE RID: 2046
		private const int MaxCultureDepth = 32;

		// Token: 0x040007FF RID: 2047
		private static XmlLanguage _empty = null;

		// Token: 0x04000800 RID: 2048
		private readonly string _lowerCaseTag;

		// Token: 0x04000801 RID: 2049
		private CultureInfo _equivalentCulture;

		// Token: 0x04000802 RID: 2050
		private CultureInfo _specificCulture;

		// Token: 0x04000803 RID: 2051
		private CultureInfo _compatibleCulture;

		// Token: 0x04000804 RID: 2052
		private int _specificity;

		// Token: 0x04000805 RID: 2053
		private bool _equivalentCultureFailed;

		// Token: 0x02000800 RID: 2048
		internal struct MatchingLanguageCollection : IEnumerable<XmlLanguage>, IEnumerable
		{
			// Token: 0x060055EA RID: 21994 RVA: 0x00161940 File Offset: 0x00160D40
			public MatchingLanguageCollection(XmlLanguage start)
			{
				this._start = start;
			}

			// Token: 0x060055EB RID: 21995 RVA: 0x00161954 File Offset: 0x00160D54
			public XmlLanguage.MatchingLanguageEnumerator GetEnumerator()
			{
				return new XmlLanguage.MatchingLanguageEnumerator(this._start);
			}

			// Token: 0x060055EC RID: 21996 RVA: 0x0016196C File Offset: 0x00160D6C
			IEnumerator<XmlLanguage> IEnumerable<XmlLanguage>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060055ED RID: 21997 RVA: 0x00161984 File Offset: 0x00160D84
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040026A6 RID: 9894
			private XmlLanguage _start;
		}

		// Token: 0x02000801 RID: 2049
		internal struct MatchingLanguageEnumerator : IEnumerator<XmlLanguage>, IDisposable, IEnumerator
		{
			// Token: 0x060055EE RID: 21998 RVA: 0x0016199C File Offset: 0x00160D9C
			public MatchingLanguageEnumerator(XmlLanguage start)
			{
				this._start = start;
				this._current = start;
				this._pastEnd = false;
				this._atStart = true;
				this._maxCultureDepth = 32;
			}

			// Token: 0x060055EF RID: 21999 RVA: 0x001619D0 File Offset: 0x00160DD0
			public void Reset()
			{
				this._current = this._start;
				this._pastEnd = false;
				this._atStart = true;
				this._maxCultureDepth = 32;
			}

			// Token: 0x1700119D RID: 4509
			// (get) Token: 0x060055F0 RID: 22000 RVA: 0x00161A00 File Offset: 0x00160E00
			public XmlLanguage Current
			{
				get
				{
					if (this._atStart)
					{
						throw new InvalidOperationException(SR.Get("Enumerator_NotStarted"));
					}
					if (this._pastEnd)
					{
						throw new InvalidOperationException(SR.Get("Enumerator_ReachedEnd"));
					}
					return this._current;
				}
			}

			// Token: 0x060055F1 RID: 22001 RVA: 0x00161A44 File Offset: 0x00160E44
			public bool MoveNext()
			{
				if (this._atStart)
				{
					this._atStart = false;
					return true;
				}
				if (this._current.IetfLanguageTag.Length == 0)
				{
					this._atStart = false;
					this._pastEnd = true;
					return false;
				}
				XmlLanguage prefixLanguage = this._current.PrefixLanguage;
				CultureInfo cultureInfo = null;
				if (this._maxCultureDepth > 0)
				{
					if (this._current.TryGetEquivalentCulture(out cultureInfo))
					{
						cultureInfo = cultureInfo.Parent;
					}
					else
					{
						cultureInfo = null;
					}
				}
				if (cultureInfo == null)
				{
					this._current = prefixLanguage;
					this._atStart = false;
					return true;
				}
				XmlLanguage language = XmlLanguage.GetLanguage(cultureInfo.IetfLanguageTag);
				if (language.IsPrefixOf(prefixLanguage.IetfLanguageTag))
				{
					this._current = prefixLanguage;
					this._atStart = false;
					return true;
				}
				this._maxCultureDepth--;
				this._current = language;
				this._atStart = false;
				return true;
			}

			// Token: 0x1700119E RID: 4510
			// (get) Token: 0x060055F2 RID: 22002 RVA: 0x00161B10 File Offset: 0x00160F10
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060055F3 RID: 22003 RVA: 0x00161B24 File Offset: 0x00160F24
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040026A7 RID: 9895
			private readonly XmlLanguage _start;

			// Token: 0x040026A8 RID: 9896
			private XmlLanguage _current;

			// Token: 0x040026A9 RID: 9897
			private bool _atStart;

			// Token: 0x040026AA RID: 9898
			private bool _pastEnd;

			// Token: 0x040026AB RID: 9899
			private int _maxCultureDepth;
		}
	}
}
