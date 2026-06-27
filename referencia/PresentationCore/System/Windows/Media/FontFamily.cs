using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Windows.Markup;
using MS.Internal;
using MS.Internal.FontCache;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;
using MS.Internal.Shaping;

namespace System.Windows.Media
{
	/// <summary>Representa uma família de fontes relacionadas.</summary>
	// Token: 0x02000396 RID: 918
	[Localizability(LocalizationCategory.Font)]
	[TypeConverter(typeof(FontFamilyConverter))]
	[ValueSerializer(typeof(FontFamilyValueSerializer))]
	public class FontFamily
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FontFamily" /> do nome da família de fontes especificado.</summary>
		/// <param name="familyName">O nome ou nomes da família que compõem o novo <see cref="T:System.Windows.Media.FontFamily" />. Vários nomes de família devem ser separados por vírgulas.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="familyName" /> não pode ser <see langword="null" />.</exception>
		// Token: 0x0600224B RID: 8779 RVA: 0x0008A548 File Offset: 0x00089948
		public FontFamily(string familyName) : this(null, familyName)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.FontFamily" /> de um valor URI (Uniform Resource Identifier) base opcional e do nome da família de fontes especificado.</summary>
		/// <param name="baseUri">Especifica a base de URI usada para resolver <paramref name="familyName" />.</param>
		/// <param name="familyName">O nome ou nomes da família que compõem o novo <see cref="T:System.Windows.Media.FontFamily" />. Vários nomes de família devem ser separados por vírgulas.</param>
		// Token: 0x0600224C RID: 8780 RVA: 0x0008A560 File Offset: 0x00089960
		public FontFamily(Uri baseUri, string familyName)
		{
			if (familyName == null)
			{
				throw new ArgumentNullException("familyName");
			}
			if (baseUri != null && !baseUri.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.Get("UriNotAbsolute"), "baseUri");
			}
			this._familyIdentifier = new FontFamilyIdentifier(familyName, baseUri);
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0008A5B4 File Offset: 0x000899B4
		internal FontFamily(FontFamilyIdentifier familyIdentifier)
		{
			this._familyIdentifier = familyIdentifier;
		}

		/// <summary>Inicializa uma nova instância de uma classe <see cref="T:System.Windows.Media.FontFamily" /> anônima.</summary>
		// Token: 0x0600224E RID: 8782 RVA: 0x0008A5D0 File Offset: 0x000899D0
		public FontFamily()
		{
			this._familyIdentifier = new FontFamilyIdentifier(null, null);
			this._firstFontFamily = new CompositeFontFamily();
		}

		/// <summary>Obtém uma coleção de cadeias de caracteres e valores <see cref="T:System.Globalization.CultureInfo" /> que representam os nomes das famílias de fontes do objeto <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.LanguageSpecificStringDictionary" /> que representa os nomes de família de fontes.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0008A5FC File Offset: 0x000899FC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public LanguageSpecificStringDictionary FamilyNames
		{
			get
			{
				CompositeFontFamily compositeFontFamily = this.FirstFontFamily as CompositeFontFamily;
				if (compositeFontFamily != null)
				{
					return compositeFontFamily.FamilyNames;
				}
				return new LanguageSpecificStringDictionary(this.FirstFontFamily.Names);
			}
		}

		/// <summary>Obtém uma coleção de fates de tipo para o objeto <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.FamilyTypefaceCollection" /> que representa uma coleção de fates de tipo para o <see cref="T:System.Windows.Media.FontFamily" /> objeto.</returns>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0008A630 File Offset: 0x00089A30
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FamilyTypefaceCollection FamilyTypefaces
		{
			get
			{
				CompositeFontFamily compositeFontFamily = this.FirstFontFamily as CompositeFontFamily;
				if (compositeFontFamily != null)
				{
					return compositeFontFamily.FamilyTypefaces;
				}
				return new FamilyTypefaceCollection(this.FirstFontFamily.GetTypefaces(this._familyIdentifier));
			}
		}

		/// <summary>Obtém a coleção de objetos <see cref="T:System.Windows.Media.FontFamilyMap" />.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.FontFamilyMapCollection" /> que contém objetos de <see cref="T:System.Windows.Media.FontFamilyMap" /> .</returns>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x0008A66C File Offset: 0x00089A6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FontFamilyMapCollection FamilyMaps
		{
			get
			{
				CompositeFontFamily compositeFontFamily = this.FirstFontFamily as CompositeFontFamily;
				if (compositeFontFamily != null)
				{
					return compositeFontFamily.FamilyMaps;
				}
				if (FontFamily._emptyFamilyMaps == null)
				{
					FontFamily._emptyFamilyMaps = new FontFamilyMapCollection(null);
				}
				return FontFamily._emptyFamilyMaps;
			}
		}

		/// <summary>Obtém o nome da família de fontes que é usada para construir o objeto <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <returns>O nome da família de fonte de <see cref="T:System.Windows.Media.FontFamily" /> objeto.</returns>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0008A6A8 File Offset: 0x00089AA8
		public string Source
		{
			get
			{
				return this._familyIdentifier.Source;
			}
		}

		/// <summary>Obtém o URI (Uniform Resource Identifier) base que é usado para resolver um nome da família de fontes.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Uri" />.</returns>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0008A6C0 File Offset: 0x00089AC0
		public Uri BaseUri
		{
			get
			{
				return this._familyIdentifier.BaseUri;
			}
		}

		/// <summary>Retorna o valor da propriedade <see cref="P:System.Windows.Media.FontFamily.Source" />.</summary>
		/// <returns>O local de origem do objeto <see cref="T:System.Windows.Media.FontFamily" />, incluindo o diretório ou a referência de local URI (Uniform Resource Identifier).</returns>
		// Token: 0x06002254 RID: 8788 RVA: 0x0008A6D8 File Offset: 0x00089AD8
		public override string ToString()
		{
			string source = this._familyIdentifier.Source;
			if (source == null)
			{
				return string.Empty;
			}
			return source;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0008A6FC File Offset: 0x00089AFC
		internal FontFamilyIdentifier FamilyIdentifier
		{
			get
			{
				return this._familyIdentifier;
			}
		}

		/// <summary>Obtém ou define a distância entre a linha de base e a parte superior da célula do caractere.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a distância entre a linha de base e a parte superior da célula de caractere, expresso como uma fração do tamanho em da fonte.</returns>
		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0008A710 File Offset: 0x00089B10
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x0008A728 File Offset: 0x00089B28
		public double Baseline
		{
			get
			{
				return this.FirstFontFamily.BaselineDesign;
			}
			set
			{
				this.VerifyMutable().SetBaseline(value);
			}
		}

		/// <summary>Obtém ou define o valor de espaçamento de linha para o objeto <see cref="T:System.Windows.Media.FontFamily" />. O espaçamento de linha é a distância da linha de base para linha de base recomendada para o texto nesta fonte com relação ao tamanho em.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa o espaçamento de linha do objeto <see cref="T:System.Windows.Media.FontFamily" />.</returns>
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x0008A744 File Offset: 0x00089B44
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x0008A75C File Offset: 0x00089B5C
		public double LineSpacing
		{
			get
			{
				return this.FirstFontFamily.LineSpacingDesign;
			}
			set
			{
				this.VerifyMutable().SetLineSpacing(value);
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0008A778 File Offset: 0x00089B78
		internal double GetLineSpacingForDisplayMode(double emSize, double pixelsPerDip)
		{
			return this.FirstFontFamily.LineSpacing(emSize, 1.0, pixelsPerDip, TextFormattingMode.Display);
		}

		/// <summary>Retorna uma coleção de objetos <see cref="T:System.Windows.Media.Typeface" /> que representam as faces de tipo no local de fontes padrão do sistema.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.ICollection`1" /> de <see cref="T:System.Windows.Media.Typeface" /> objetos.</returns>
		// Token: 0x0600225B RID: 8795 RVA: 0x0008A79C File Offset: 0x00089B9C
		[CLSCompliant(false)]
		public ICollection<Typeface> GetTypefaces()
		{
			return this.FirstFontFamily.GetTypefaces(this._familyIdentifier);
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.FontFamily" />. Ele é adequado para uso em algoritmos de hash e estruturas de dados como uma tabela de hash.</summary>
		/// <returns>Um valor <see cref="T:System.Int32" /> que representa o código hash para o objeto atual.</returns>
		// Token: 0x0600225C RID: 8796 RVA: 0x0008A7BC File Offset: 0x00089BBC
		public override int GetHashCode()
		{
			if (this._familyIdentifier.Source != null)
			{
				return this._familyIdentifier.GetHashCode();
			}
			return base.GetHashCode();
		}

		/// <summary>Obtém um valor que indica se o objeto de família de fontes atual e o especificado são os mesmos.</summary>
		/// <param name="o">O objeto <see cref="T:System.Windows.Media.FontFamily" /> a ser comparado.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="o" /> for igual ao objeto <see cref="T:System.Windows.Media.FontFamily" /> atual; caso contrário, <see langword="false" />. Se <paramref name="o" /> não for um objeto <see cref="T:System.Windows.Media.FontFamily" />, <see langword="false" /> será retornado.</returns>
		// Token: 0x0600225D RID: 8797 RVA: 0x0008A7F0 File Offset: 0x00089BF0
		public override bool Equals(object o)
		{
			FontFamily fontFamily = o as FontFamily;
			if (fontFamily == null)
			{
				return false;
			}
			if (this._familyIdentifier.Source != null)
			{
				return this._familyIdentifier.Equals(fontFamily._familyIdentifier);
			}
			return base.Equals(o);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0008A830 File Offset: 0x00089C30
		private CompositeFontFamily VerifyMutable()
		{
			CompositeFontFamily compositeFontFamily = this._firstFontFamily as CompositeFontFamily;
			if (compositeFontFamily == null)
			{
				throw new NotSupportedException(SR.Get("FontFamily_ReadOnly"));
			}
			return compositeFontFamily;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x0008A860 File Offset: 0x00089C60
		internal IFontFamily FirstFontFamily
		{
			get
			{
				IFontFamily fontFamily = this._firstFontFamily;
				if (fontFamily == null)
				{
					this._familyIdentifier.Canonicalize();
					fontFamily = (TypefaceMetricsCache.ReadonlyLookup(this.FamilyIdentifier) as IFontFamily);
					if (fontFamily == null)
					{
						FontStyle normal = FontStyles.Normal;
						FontWeight normal2 = FontWeights.Normal;
						FontStretch normal3 = FontStretches.Normal;
						fontFamily = this.FindFirstFontFamilyAndFace(ref normal, ref normal2, ref normal3);
						if (fontFamily == null)
						{
							fontFamily = FontFamily.LookupFontFamily(FontFamily.NullFontFamilyCanonicalName);
							Invariant.Assert(fontFamily != null);
						}
						TypefaceMetricsCache.Add(this.FamilyIdentifier, fontFamily);
					}
					this._firstFontFamily = fontFamily;
				}
				return fontFamily;
			}
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0008A8EC File Offset: 0x00089CEC
		internal static IFontFamily FindFontFamilyFromFriendlyNameList(string friendlyNameList)
		{
			IFontFamily fontFamily = null;
			FontFamilyIdentifier fontFamilyIdentifier = new FontFamilyIdentifier(friendlyNameList, null);
			int num = 0;
			int count = fontFamilyIdentifier.Count;
			while (fontFamily == null && num < count)
			{
				fontFamily = FontFamily.LookupFontFamily(fontFamilyIdentifier[num]);
				num++;
			}
			if (fontFamily == null)
			{
				fontFamily = FontFamily.LookupFontFamily(FontFamily.NullFontFamilyCanonicalName);
				Invariant.Assert(fontFamily != null);
			}
			return fontFamily;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0008A944 File Offset: 0x00089D44
		internal static IFontFamily SafeLookupFontFamily(CanonicalFontFamilyReference canonicalName, out bool nullFont)
		{
			nullFont = false;
			IFontFamily fontFamily = FontFamily.LookupFontFamily(canonicalName);
			if (fontFamily == null)
			{
				nullFont = true;
				fontFamily = FontFamily.LookupFontFamily(FontFamily.NullFontFamilyCanonicalName);
				Invariant.Assert(fontFamily != null, "Unable to create null font family");
			}
			return fontFamily;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0008A97C File Offset: 0x00089D7C
		internal static IFontFamily LookupFontFamily(CanonicalFontFamilyReference canonicalName)
		{
			FontStyle normal = FontStyles.Normal;
			FontWeight normal2 = FontWeights.Normal;
			FontStretch normal3 = FontStretches.Normal;
			return FontFamily.LookupFontFamilyAndFace(canonicalName, ref normal, ref normal2, ref normal3);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0008A9A8 File Offset: 0x00089DA8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static FamilyCollection PreCreateDefaultFamilyCollection()
		{
			return FamilyCollection.FromWindowsFonts(Util.WindowsFontsUriObject);
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0008A9C4 File Offset: 0x00089DC4
		internal IFontFamily FindFirstFontFamilyAndFace(ref FontStyle style, ref FontWeight weight, ref FontStretch stretch)
		{
			if (this._familyIdentifier.Source == null)
			{
				Invariant.Assert(this._firstFontFamily != null, "Unnamed FontFamily should have a non-null first font family");
				return this._firstFontFamily;
			}
			IFontFamily fontFamily = null;
			this._familyIdentifier.Canonicalize();
			int num = 0;
			int count = this._familyIdentifier.Count;
			while (fontFamily == null && num < count)
			{
				fontFamily = FontFamily.LookupFontFamilyAndFace(this._familyIdentifier[num], ref style, ref weight, ref stretch);
				num++;
			}
			return fontFamily;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0008AA38 File Offset: 0x00089E38
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static IFontFamily LookupFontFamilyAndFace(CanonicalFontFamilyReference canonicalFamilyReference, ref FontStyle style, ref FontWeight weight, ref FontStretch stretch)
		{
			if (canonicalFamilyReference == null || canonicalFamilyReference == CanonicalFontFamilyReference.Unresolved)
			{
				return null;
			}
			try
			{
				FamilyCollection familyCollection;
				if (canonicalFamilyReference.LocationUri == null && canonicalFamilyReference.EscapedFileName == null)
				{
					familyCollection = FontFamily._defaultFamilyCollection;
				}
				else if (canonicalFamilyReference.LocationUri != null)
				{
					familyCollection = FamilyCollection.FromUri(canonicalFamilyReference.LocationUri);
				}
				else
				{
					Uri folderUri = new Uri(Util.WindowsFontsUriObject, canonicalFamilyReference.EscapedFileName);
					familyCollection = FamilyCollection.FromWindowsFonts(folderUri);
				}
				return familyCollection.LookupFamily(canonicalFamilyReference.FamilyName, ref style, ref weight, ref stretch);
			}
			catch (FileFormatException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (UriFormatException)
			{
			}
			return null;
		}

		// Token: 0x04001101 RID: 4353
		private FontFamilyIdentifier _familyIdentifier;

		// Token: 0x04001102 RID: 4354
		private IFontFamily _firstFontFamily;

		// Token: 0x04001103 RID: 4355
		internal static readonly CanonicalFontFamilyReference NullFontFamilyCanonicalName = CanonicalFontFamilyReference.Create(null, "#ARIAL");

		// Token: 0x04001104 RID: 4356
		internal const string GlobalUI = "#GLOBAL USER INTERFACE";

		// Token: 0x04001105 RID: 4357
		internal static FontFamily FontFamilyGlobalUI = new FontFamily("#GLOBAL USER INTERFACE");

		// Token: 0x04001106 RID: 4358
		private static volatile FamilyCollection _defaultFamilyCollection = FontFamily.PreCreateDefaultFamilyCollection();

		// Token: 0x04001107 RID: 4359
		private static FontFamilyMapCollection _emptyFamilyMaps = null;
	}
}
