using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Markup;
using System.Windows.Media.Composition;
using System.Windows.Media.TextFormatting;
using MS.Internal;
using MS.Internal.FontCache;
using MS.Internal.FontFace;
using MS.Internal.PresentationCore;
using MS.Internal.Text.TextInterface;
using MS.Internal.TextFormatting;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Especifica uma face de fonte física que corresponde a um arquivo de fonte no disco.</summary>
	// Token: 0x02000407 RID: 1031
	public class GlyphTypeface : ITypefaceMetrics, ISupportInitialize
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		// Token: 0x0600296F RID: 10607 RVA: 0x000A6BC4 File Offset: 0x000A5FC4
		public GlyphTypeface()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphTypeface" />, usando o local do arquivo de fonte especificado.</summary>
		/// <param name="typefaceSource">O URI que especifica o local do arquivo de fonte.</param>
		// Token: 0x06002970 RID: 10608 RVA: 0x000A6BD8 File Offset: 0x000A5FD8
		public GlyphTypeface(Uri typefaceSource) : this(typefaceSource, StyleSimulations.None)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GlyphTypeface" />, usando o local do arquivo de fonte especificado e o valor <see cref="T:System.Windows.Media.StyleSimulations" />.</summary>
		/// <param name="typefaceSource">O URI que especifica o local do arquivo de fonte.</param>
		/// <param name="styleSimulations">Um dos valores de <see cref="T:System.Windows.Media.StyleSimulations" />.</param>
		// Token: 0x06002971 RID: 10609 RVA: 0x000A6BF0 File Offset: 0x000A5FF0
		public GlyphTypeface(Uri typefaceSource, StyleSimulations styleSimulations)
		{
			this.Initialize(typefaceSource, styleSimulations);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000A6C0C File Offset: 0x000A600C
		[SecurityCritical]
		internal GlyphTypeface(Font font)
		{
			StyleSimulations simulationFlags = (StyleSimulations)font.SimulationFlags;
			this._font = font;
			FontFace fontFace = this._font.GetFontFace();
			string uriPath;
			try
			{
				using (FontFile fileZero = fontFace.GetFileZero())
				{
					uriPath = fileZero.GetUriPath();
				}
				this._originalUri = new SecurityCriticalDataClass<Uri>(Util.CombineUriWithFaceIndex(uriPath, checked((int)fontFace.Index)));
			}
			finally
			{
				fontFace.Release();
			}
			Uri uri = new Uri(uriPath);
			this._fileIOPermObj = new SecurityCriticalDataForSet<CodeAccessPermission>(SecurityHelper.CreateUriReadPermission(uri));
			this._fontFace = new FontFaceLayoutInfo(font);
			this._fontSource = new FontSource(uri, true);
			Invariant.Assert(simulationFlags == StyleSimulations.None || simulationFlags == StyleSimulations.ItalicSimulation || simulationFlags == StyleSimulations.BoldSimulation || simulationFlags == StyleSimulations.BoldItalicSimulation);
			this._styleSimulations = simulationFlags;
			this._initializationState = GlyphTypeface.InitializationState.IsInitialized;
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x000A6D04 File Offset: 0x000A6104
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void Initialize(Uri typefaceSource, StyleSimulations styleSimulations)
		{
			if (typefaceSource == null)
			{
				throw new ArgumentNullException("typefaceSource");
			}
			if (!typefaceSource.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.Get("UriNotAbsolute"), "typefaceSource");
			}
			this._originalUri = new SecurityCriticalDataClass<Uri>(typefaceSource);
			Uri uri;
			int faceIndex;
			Util.SplitFontFaceIndex(typefaceSource, out uri, out faceIndex);
			this._fileIOPermObj = new SecurityCriticalDataForSet<CodeAccessPermission>(SecurityHelper.CreateUriReadPermission(uri));
			this.DemandPermissionsForFontInformation();
			if (styleSimulations != StyleSimulations.None && styleSimulations != StyleSimulations.ItalicSimulation && styleSimulations != StyleSimulations.BoldSimulation && styleSimulations != StyleSimulations.BoldItalicSimulation)
			{
				throw new InvalidEnumArgumentException("styleSimulations", (int)styleSimulations, typeof(StyleSimulations));
			}
			this._styleSimulations = styleSimulations;
			FontCollection fontCollectionFromFile = DWriteFactory.GetFontCollectionFromFile(uri);
			using (FontFace fontFace = DWriteFactory.Instance.CreateFontFace(uri, (uint)faceIndex, (FontSimulations)styleSimulations))
			{
				if (fontFace == null)
				{
					try
					{
						SecurityHelper.DemandUriDiscoveryPermission(typefaceSource);
						throw new FileFormatException(typefaceSource);
					}
					catch (SecurityException)
					{
						throw new FileFormatException();
					}
				}
				this._font = fontCollectionFromFile.GetFontFromFontFace(fontFace);
			}
			this._fontFace = new FontFaceLayoutInfo(this._font);
			this._fontSource = new FontSource(uri, true);
			this._initializationState = GlyphTypeface.InitializationState.IsInitialized;
		}

		/// <summary>Serve como uma função de hash para <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um código hash do objeto atual.</returns>
		// Token: 0x06002974 RID: 10612 RVA: 0x000A6E40 File Offset: 0x000A6240
		[SecurityCritical]
		public override int GetHashCode()
		{
			this.CheckInitialized();
			return this._originalUri.Value.GetHashCode() ^ (int)this.StyleSimulations;
		}

		/// <summary>Determina se o objeto especificado é igual ao objeto <see cref="T:System.Windows.Media.GlyphTypeface" /> atual.</summary>
		/// <param name="o">O <see cref="T:System.Object" /> a ser comparado com o objeto <see cref="T:System.Windows.Media.GlyphTypeface" /> atual.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="o" /> é um <see cref="T:System.Windows.Media.GlyphTypeface" /> e é igual ao <see cref="T:System.Windows.Media.GlyphTypeface" /> atual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002975 RID: 10613 RVA: 0x000A6E6C File Offset: 0x000A626C
		[SecurityCritical]
		public override bool Equals(object o)
		{
			this.CheckInitialized();
			GlyphTypeface glyphTypeface = o as GlyphTypeface;
			return glyphTypeface != null && this.StyleSimulations == glyphTypeface.StyleSimulations && this._originalUri.Value == glyphTypeface._originalUri.Value;
		}

		/// <summary>Retorna um valor <see cref="T:System.Windows.Media.Geometry" /> que descreve o caminho para um único glifo na fonte.</summary>
		/// <param name="glyphIndex">O índice do glifo para o qual obter a estrutura de tópicos.</param>
		/// <param name="renderingEmSize">O tamanho da fonte em unidades de superfície de desenho.</param>
		/// <param name="hintingEmSize">O tamanho a sugerir em pontos.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.Geometry" /> que representa o caminho do glifo.</returns>
		// Token: 0x06002976 RID: 10614 RVA: 0x000A6EB8 File Offset: 0x000A62B8
		[CLSCompliant(false)]
		public Geometry GetGlyphOutline(ushort glyphIndex, double renderingEmSize, double hintingEmSize)
		{
			this.CheckInitialized();
			return this.ComputeGlyphOutline(glyphIndex, false, renderingEmSize);
		}

		/// <summary>Retorna a imagem binária do subconjunto fonte com base em uma coleção de glifos especificada.</summary>
		/// <param name="glyphs">A coleção de índices de glifo devem ser incluídos no subconjunto.</param>
		/// <returns>Uma matriz <see cref="T:System.Byte" /> que representa a imagem binária do subconjunto de fontes.</returns>
		// Token: 0x06002977 RID: 10615 RVA: 0x000A6ED4 File Offset: 0x000A62D4
		[SecurityCritical]
		[CLSCompliant(false)]
		public byte[] ComputeSubset(ICollection<ushort> glyphs)
		{
			SecurityHelper.DemandUnmanagedCode();
			this.DemandPermissionsForFontInformation();
			this.CheckInitialized();
			if (glyphs == null)
			{
				throw new ArgumentNullException("glyphs");
			}
			if (glyphs.Count <= 0)
			{
				throw new ArgumentException(SR.Get("CollectionNumberOfElementsMustBeGreaterThanZero"), "glyphs");
			}
			if (glyphs.Count > 65535)
			{
				throw new ArgumentException(SR.Get("CollectionNumberOfElementsMustBeLessOrEqualTo", new object[]
				{
					ushort.MaxValue
				}), "glyphs");
			}
			UnmanagedMemoryStream unmanagedStream = this.FontSource.GetUnmanagedStream();
			byte[] result;
			try
			{
				TrueTypeFontDriver trueTypeFontDriver = new TrueTypeFontDriver(unmanagedStream, this._originalUri.Value);
				trueTypeFontDriver.SetFace(this.FaceIndex);
				result = trueTypeFontDriver.ComputeFontSubset(glyphs);
			}
			catch (SEHException e)
			{
				throw Util.ConvertInPageException(this.FontSource, e);
			}
			finally
			{
				unmanagedStream.Close();
			}
			return result;
		}

		/// <summary>Retorna o fluxo de arquivos de fonte representado pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um valor <see cref="T:System.IO.Stream" /> que representa o arquivo de fonte.</returns>
		// Token: 0x06002978 RID: 10616 RVA: 0x000A6FD8 File Offset: 0x000A63D8
		[SecurityCritical]
		public Stream GetFontStream()
		{
			this.CheckInitialized();
			this.DemandPermissionsForFontInformation();
			return this.FontSource.GetStream();
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x000A6FFC File Offset: 0x000A63FC
		[FriendAccessAllowed]
		internal CodeAccessPermission CriticalFileReadPermission
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return this._fileIOPermObj.Value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000A701C File Offset: 0x000A641C
		[FriendAccessAllowed]
		internal CodeAccessPermission CriticalUriDiscoveryPermission
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return SecurityHelper.CreateUriDiscoveryPermission(this._originalUri.Value);
			}
		}

		/// <summary>Obtém ou define o URI para o objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>O URI para o <see cref="T:System.Windows.Media.GlyphTypeface" /> objeto.</returns>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000A7040 File Offset: 0x000A6440
		// (set) Token: 0x0600297C RID: 10620 RVA: 0x000A7070 File Offset: 0x000A6470
		public Uri FontUri
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				SecurityHelper.DemandUriDiscoveryPermission(this._originalUri.Value);
				return this._originalUri.Value;
			}
			[SecurityCritical]
			set
			{
				this.CheckInitializing();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!value.IsAbsoluteUri)
				{
					throw new ArgumentException(SR.Get("UriNotAbsolute"), "value");
				}
				this._originalUri = new SecurityCriticalDataClass<Uri>(value);
			}
		}

		/// <summary>Obtém o nome de família para o objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de nome de família representam pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que contém o nome da família.</returns>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000A70C0 File Offset: 0x000A64C0
		public IDictionary<CultureInfo, string> FamilyNames
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.CheckInitialized();
				LocalizedStrings result;
				if (this._font.GetInformationalStrings(InformationalStringID.PreferredFamilyNames, out result) || this._font.GetInformationalStrings(InformationalStringID.WIN32FamilyNames, out result))
				{
					return result;
				}
				return null;
			}
		}

		/// <summary>Obtém o nome de fonte para o objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de nome de face representam pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que contém o nome de face.</returns>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000A70F8 File Offset: 0x000A64F8
		public IDictionary<CultureInfo, string> FaceNames
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.CheckInitialized();
				LocalizedStrings result;
				if (this._font.GetInformationalStrings(InformationalStringID.PreferredSubFamilyNames, out result) || this._font.GetInformationalStrings(InformationalStringID.Win32SubFamilyNames, out result))
				{
					return result;
				}
				return null;
			}
		}

		/// <summary>Obtém o nome de família Win32 da fonte representada pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> objeto que contém pares chave/valor que representam informações de nome de família Win32. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que representa o nome de família Win32.</returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000A7130 File Offset: 0x000A6530
		public IDictionary<CultureInfo, string> Win32FamilyNames
		{
			get
			{
				this.CheckInitialized();
				return this.GetFontInfo(InformationalStringID.WIN32FamilyNames);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000A714C File Offset: 0x000A654C
		IDictionary<XmlLanguage, string> ITypefaceMetrics.AdjustedFaceNames
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				IDictionary<CultureInfo, string> faceNames = this._font.FaceNames;
				IDictionary<XmlLanguage, string> dictionary = new Dictionary<XmlLanguage, string>(faceNames.Count);
				foreach (KeyValuePair<CultureInfo, string> keyValuePair in faceNames)
				{
					dictionary[XmlLanguage.GetLanguage(keyValuePair.Key.IetfLanguageTag)] = keyValuePair.Value;
				}
				return dictionary;
			}
		}

		/// <summary>Obtém o nome de face Win32 da fonte representada pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> objeto que contém pares chave/valor que representam informações de nome de face Win32. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que representa o nome de face Win32.</returns>
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000A71D8 File Offset: 0x000A65D8
		public IDictionary<CultureInfo, string> Win32FaceNames
		{
			get
			{
				this.CheckInitialized();
				return this.GetFontInfo(InformationalStringID.Win32SubFamilyNames);
			}
		}

		/// <summary>Obtém as informações de cadeia de caracteres de versão do <see cref="T:System.Windows.Media.GlyphTypeface" /> objeto interpretadas da tabela 'NAME' da fonte.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de cadeia de caracteres de versão representam pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que representa a versão.</returns>
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000A71F4 File Offset: 0x000A65F4
		public IDictionary<CultureInfo, string> VersionStrings
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.VersionStrings);
			}
		}

		/// <summary>Obtém as informações de direitos autorais do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de direitos autorais representam pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres de informações de direitos autorais.</returns>
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000A7214 File Offset: 0x000A6614
		public IDictionary<CultureInfo, string> Copyrights
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.CopyrightNotice);
			}
		}

		/// <summary>Obtém as informações do fabricante de fonte do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> pares de objeto que contém a chave/valor para as informações do fabricante de fonte. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que descreve as informações do fabricante de fonte.</returns>
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000A7234 File Offset: 0x000A6634
		public IDictionary<CultureInfo, string> ManufacturerNames
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.Manufacturer);
			}
		}

		/// <summary>Obtém as informações de aviso de marca do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de aviso de marca comercial representam pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que a marca comercial notificação de informações.</returns>
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x000A7254 File Offset: 0x000A6654
		public IDictionary<CultureInfo, string> Trademarks
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.Trademark);
			}
		}

		/// <summary>Obtém as informações de designer do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> objeto que contém a chave/valor pares que representam informações do designer. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres de informações do designer.</returns>
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000A7274 File Offset: 0x000A6674
		public IDictionary<CultureInfo, string> DesignerNames
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.Designer);
			}
		}

		/// <summary>Obtém as informações de descrição do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Uma <see cref="T:System.Collections.Generic.IDictionary`2" /> objeto que contém pares chave/valor que a chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres de informações de descrição.</returns>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x000A7294 File Offset: 0x000A6694
		public IDictionary<CultureInfo, string> Descriptions
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.Description);
			}
		}

		/// <summary>Obtém as informações de URL do fornecedor do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de URL do fornecedor representam pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que faz referência a uma URL do fornecedor.</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000A72B4 File Offset: 0x000A66B4
		public IDictionary<CultureInfo, string> VendorUrls
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.FontVendorURL);
			}
		}

		/// <summary>Obtém as informações de URL de designer do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> objeto que contém a chave/valor pares que representam informações do designer. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que faz referência a um designer de URL.</returns>
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000A72D4 File Offset: 0x000A66D4
		public IDictionary<CultureInfo, string> DesignerUrls
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.DesignerURL);
			}
		}

		/// <summary>Obtém as informações de descrição de licença de fonte do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> informações de licença de fonte de pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que descreve as informações de licença de fonte.</returns>
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000A72F4 File Offset: 0x000A66F4
		public IDictionary<CultureInfo, string> LicenseDescriptions
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.LicenseDescription);
			}
		}

		/// <summary>Obtém as informações de amostra de texto do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> informações de texto de exemplo de pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.Globalization.CultureInfo" /> objeto que identifica a cultura. O valor é uma cadeia de caracteres que descreve as informações de texto de exemplo.</returns>
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x000A7318 File Offset: 0x000A6718
		public IDictionary<CultureInfo, string> SampleTexts
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this.GetFontInfo(InformationalStringID.SampleText);
			}
		}

		/// <summary>Obtém o estilo do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.FontStyle" /> valor que representa o valor de estilo.</returns>
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x000A733C File Offset: 0x000A673C
		public FontStyle Style
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return new FontStyle((int)this._font.Style);
			}
		}

		/// <summary>Obtém o peso projetado da fonte representada pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.FontWeight" /> que representa a espessura da fonte.</returns>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600298D RID: 10637 RVA: 0x000A7360 File Offset: 0x000A6760
		public FontWeight Weight
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return new FontWeight((int)this._font.Weight);
			}
		}

		/// <summary>Obtém o valor <see cref="T:System.Windows.FontStretch" /> do objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.FontStretch" /> valor que representa a ampliação da fonte.</returns>
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x000A7384 File Offset: 0x000A6784
		public FontStretch Stretch
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return new FontStretch((int)this._font.Stretch);
			}
		}

		/// <summary>Obtém a versão de face da fonte interpretada da tabela 'NAME' da fonte.</summary>
		/// <returns>Um <see cref="T:System.Double" /> valor que representa a versão.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600298F RID: 10639 RVA: 0x000A73A8 File Offset: 0x000A67A8
		public double Version
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this._font.Version;
			}
		}

		/// <summary>Obtém a altura da célula em relação ao tamanho em.</summary>
		/// <returns>Um <see cref="T:System.Double" /> valor que representa a altura da célula do caractere.</returns>
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x000A73CC File Offset: 0x000A67CC
		public double Height
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return (double)(this._font.Metrics.Ascent + this._font.Metrics.Descent) / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém o valor de linha de base do <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" /> que representa a linha de base.</returns>
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x000A7414 File Offset: 0x000A6814
		public double Baseline
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.CheckInitialized();
				return (double)this._font.Metrics.Ascent / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém ou define a distância da linha de base até a parte superior de uma maiúscula em inglês, com relação ao tamanho, para o objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a distância da linha de base para a parte superior de uma letra maiuscula em inglês, expressada como uma fração do tamanho em da fonte.</returns>
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x000A744C File Offset: 0x000A684C
		public double CapsHeight
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return (double)this._font.Metrics.CapHeight / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém a altura x Ocidental em relação ao tamanho em da fonte representada pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002993 RID: 10643 RVA: 0x000A7484 File Offset: 0x000A6884
		public double XHeight
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return (double)this._font.Metrics.XHeight / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém um valor que indica se a fonte <see cref="T:System.Windows.Media.GlyphTypeface" /> está em conformidade com a codificação Unicode.</summary>
		/// <returns>
		///   <see langword="true" /> Se a fonte está em conformidade com a codificação Unicode; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x000A74BC File Offset: 0x000A68BC
		public bool Symbol
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.CheckInitialized();
				return this._font.IsSymbolFont;
			}
		}

		/// <summary>Obtém a posição do sublinhado no <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Double" /> valor que representa a posição do sublinhado.</returns>
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x000A74DC File Offset: 0x000A68DC
		public double UnderlinePosition
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return (double)this._font.Metrics.UnderlinePosition / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém a espessura do sublinhado em relação ao tamanho em.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Double" />.</returns>
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x000A7514 File Offset: 0x000A6914
		public double UnderlineThickness
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return (double)this._font.Metrics.UnderlineThickness / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém um valor que indica a distância da linha de base para o tachado da face de tipos.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a posição de tachado.</returns>
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x000A754C File Offset: 0x000A694C
		public double StrikethroughPosition
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return (double)this._font.Metrics.StrikethroughPosition / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém um valor que indica a espessura do tachado em relação ao tamanho em da fonte.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que indica a espessura do tachado, expressada como uma fração do tamanho em da fonte.</returns>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000A7584 File Offset: 0x000A6984
		public double StrikethroughThickness
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.CheckInitialized();
				return (double)this._font.Metrics.StrikethroughThickness / (double)this._font.Metrics.DesignUnitsPerEm;
			}
		}

		/// <summary>Obtém a permissão para incorporação de fonte para o objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um do <see cref="T:System.Windows.Media.FontEmbeddingRight" /> valores que representa a permissão de incorporação de fonte</returns>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x000A75BC File Offset: 0x000A69BC
		public FontEmbeddingRight EmbeddingRights
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				this.DemandPermissionsForFontInformation();
				return this._fontFace.EmbeddingRights;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x000A75E0 File Offset: 0x000A69E0
		double ITypefaceMetrics.CapsHeight
		{
			get
			{
				return this.CapsHeight;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x000A75F4 File Offset: 0x000A69F4
		double ITypefaceMetrics.XHeight
		{
			get
			{
				return this.XHeight;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000A7608 File Offset: 0x000A6A08
		bool ITypefaceMetrics.Symbol
		{
			get
			{
				return this.Symbol;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600299D RID: 10653 RVA: 0x000A761C File Offset: 0x000A6A1C
		double ITypefaceMetrics.UnderlinePosition
		{
			get
			{
				return this.UnderlinePosition;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x000A7630 File Offset: 0x000A6A30
		double ITypefaceMetrics.UnderlineThickness
		{
			get
			{
				return this.UnderlineThickness;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x000A7644 File Offset: 0x000A6A44
		double ITypefaceMetrics.StrikethroughPosition
		{
			get
			{
				return this.StrikethroughPosition;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000A7658 File Offset: 0x000A6A58
		double ITypefaceMetrics.StrikethroughThickness
		{
			get
			{
				return this.StrikethroughThickness;
			}
		}

		/// <summary>Obtém as larguras de avanço para os glifos representados pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de largura de adiantamento representa para os glifos de pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.UInt16" /> que identifica o índice de glifo. O valor é um <see cref="T:System.Double" /> que representa a largura de avanço.</returns>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000A766C File Offset: 0x000A6A6C
		public IDictionary<ushort, double> AdvanceWidths
		{
			get
			{
				this.CheckInitialized();
				return this.CreateGlyphIndexer(new GlyphTypeface.GlyphAccessor(this.GetAdvanceWidth));
			}
		}

		/// <summary>Obtém as alturas de avanço para os glifos representados pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações de altura representa avanço para os glifos de pares de objeto que contém a chave-valor. A chave é um <see cref="T:System.UInt16" /> que identifica o índice de glifo. O valor é um <see cref="T:System.Double" /> que representa a altura de avanço.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000A7694 File Offset: 0x000A6A94
		public IDictionary<ushort, double> AdvanceHeights
		{
			get
			{
				this.CheckInitialized();
				return this.CreateGlyphIndexer(new GlyphTypeface.GlyphAccessor(this.GetAdvanceHeight));
			}
		}

		/// <summary>Obtém a distância da extremidade à esquerda do vetor de avanço até a extremidade esquerda da caixa preta para os glifos representados pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> essas informações a distância representam os glifos de pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.UInt16" /> que identifica o índice de glifo. O valor é um <see cref="T:System.Double" /> que representa a distância.</returns>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000A76BC File Offset: 0x000A6ABC
		public IDictionary<ushort, double> LeftSideBearings
		{
			get
			{
				this.CheckInitialized();
				return this.CreateGlyphIndexer(new GlyphTypeface.GlyphAccessor(this.GetLeftSidebearing));
			}
		}

		/// <summary>Obtém a distância da borda direita da caixa preta para a extremidade direita do vetor de avanço para os glifos representados pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> distância representam informações de pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.UInt16" /> que identifica o índice de glifo. O valor é um <see cref="T:System.Double" /> que representa a distância.</returns>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000A76E4 File Offset: 0x000A6AE4
		public IDictionary<ushort, double> RightSideBearings
		{
			get
			{
				this.CheckInitialized();
				return this.CreateGlyphIndexer(new GlyphTypeface.GlyphAccessor(this.GetRightSidebearing));
			}
		}

		/// <summary>Obtém a distância da extremidade superior do vetor de avanço vertical até a extremidade superior da caixa preta para os glifos representados pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> distância representam informações de pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.UInt16" /> que identifica o índice de glifo. O valor é um <see cref="T:System.Double" /> que representa a distância.</returns>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000A770C File Offset: 0x000A6B0C
		public IDictionary<ushort, double> TopSideBearings
		{
			get
			{
				this.CheckInitialized();
				return this.CreateGlyphIndexer(new GlyphTypeface.GlyphAccessor(this.GetTopSidebearing));
			}
		}

		/// <summary>Obtém a distância da borda inferior da caixa preta para a extremidade inferior do vetor de avanço para os glifos representados pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> distância representam informações de pares de objeto que contém a chave/valor. A chave é um <see cref="T:System.UInt16" /> que identifica o glifo. O valor é um <see cref="T:System.Double" /> que representa a distância.</returns>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000A7734 File Offset: 0x000A6B34
		public IDictionary<ushort, double> BottomSideBearings
		{
			get
			{
				this.CheckInitialized();
				return this.CreateGlyphIndexer(new GlyphTypeface.GlyphAccessor(this.GetBottomSidebearing));
			}
		}

		/// <summary>Obtém o valor de deslocamento da linha de base Ocidental horizontal para a parte inferior da caixa preta de glifo para os glifos representados pelo objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um <see cref="T:System.Collections.Generic.IDictionary`2" /> objeto que contém pares chave/valor que representam deslocamentos para os glifos. A chave é um <see cref="T:System.UInt16" /> que identifica o índice de glifo. O valor é um <see cref="T:System.Double" /> que representa o valor de deslocamento.</returns>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000A775C File Offset: 0x000A6B5C
		public IDictionary<ushort, double> DistancesFromHorizontalBaselineToBlackBoxBottom
		{
			get
			{
				this.CheckInitialized();
				return this.CreateGlyphIndexer(new GlyphTypeface.GlyphAccessor(this.GetBaseline));
			}
		}

		/// <summary>Obtém o mapeamento nominal de um ponto de código Unicode para um índice de glifo, conforme definido pela tabela “CMAP” da fonte.</summary>
		/// <returns>Um objeto <see cref="T:System.Collections.Generic.IDictionary`2" /> que contém o mapeamento de um código Unicode aponta para índices de glifo em todos os glifos no objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</returns>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000A7784 File Offset: 0x000A6B84
		public IDictionary<int, ushort> CharacterToGlyphMap
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return this._fontFace.CharacterMap;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.StyleSimulations" /> para o objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>Um do <see cref="T:System.Windows.Media.StyleSimulations" /> valores que representa a simulação de estilo da fonte.</returns>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x000A77A4 File Offset: 0x000A6BA4
		// (set) Token: 0x060029AA RID: 10666 RVA: 0x000A77C0 File Offset: 0x000A6BC0
		public StyleSimulations StyleSimulations
		{
			get
			{
				this.CheckInitialized();
				return this._styleSimulations;
			}
			set
			{
				this.CheckInitializing();
				this._styleSimulations = value;
			}
		}

		/// <summary>Obtém o número de glifos para o objeto <see cref="T:System.Windows.Media.GlyphTypeface" />.</summary>
		/// <returns>O número total de glifos.</returns>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x000A77DC File Offset: 0x000A6BDC
		public int GlyphCount
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.CheckInitialized();
				FontFace fontFace = this._font.GetFontFace();
				int glyphCount;
				try
				{
					glyphCount = (int)fontFace.GlyphCount;
				}
				finally
				{
					fontFace.Release();
				}
				return glyphCount;
			}
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000A7828 File Offset: 0x000A6C28
		[SecuritySafeCritical]
		internal bool HasCharacter(uint unicodeValue)
		{
			return this.FontDWrite.HasCharacter(unicodeValue);
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000A7844 File Offset: 0x000A6C44
		internal Font FontDWrite
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return this._font;
			}
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000A7860 File Offset: 0x000A6C60
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal double GetAdvanceWidth(ushort glyph, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			this.CheckInitialized();
			GlyphMetrics glyphMetrics = this.GlyphMetrics(glyph, (double)this.DesignEmHeight, pixelsPerDip, textFormattingMode, isSideways);
			return glyphMetrics.AdvanceWidth / (double)this.DesignEmHeight;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000A7898 File Offset: 0x000A6C98
		[SecuritySafeCritical]
		internal void DemandPermissionsForFontInformation()
		{
			if (this._fileIOPermObj.Value != null)
			{
				this._fileIOPermObj.Value.Demand();
			}
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000A78C4 File Offset: 0x000A6CC4
		private double GetAdvanceHeight(ushort glyph, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			double num;
			double result;
			double num2;
			double num3;
			double num4;
			double num5;
			double num6;
			this.GetGlyphMetrics(glyph, 1.0, 1.0, pixelsPerDip, textFormattingMode, isSideways, out num, out result, out num2, out num3, out num4, out num5, out num6);
			return result;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000A7900 File Offset: 0x000A6D00
		[SecurityCritical]
		private unsafe GlyphMetrics GlyphMetrics(ushort glyphIndex, double emSize, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			FontFace fontFace = this._font.GetFontFace();
			GlyphMetrics result;
			try
			{
				if (glyphIndex >= fontFace.GlyphCount)
				{
					throw new ArgumentOutOfRangeException("glyphIndex", SR.Get("GlyphIndexOutOfRange", new object[]
					{
						glyphIndex
					}));
				}
				result = default(GlyphMetrics);
				if (textFormattingMode == TextFormattingMode.Ideal)
				{
					fontFace.GetDesignGlyphMetrics((ushort*)(&glyphIndex), 1U, &result);
				}
				else
				{
					fontFace.GetDisplayGlyphMetrics((ushort*)(&glyphIndex), 1U, &result, (float)emSize, textFormattingMode != TextFormattingMode.Display, isSideways, pixelsPerDip);
				}
			}
			finally
			{
				fontFace.Release();
			}
			return result;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000A79A4 File Offset: 0x000A6DA4
		[SecurityCritical]
		private unsafe void GlyphMetrics(ushort* pGlyphIndices, int characterCount, GlyphMetrics* pGlyphMetrics, double emSize, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			FontFace fontFace = this._font.GetFontFace();
			checked
			{
				try
				{
					if (textFormattingMode == TextFormattingMode.Ideal)
					{
						fontFace.GetDesignGlyphMetrics((ushort*)pGlyphIndices, (uint)characterCount, pGlyphMetrics);
					}
					else
					{
						fontFace.GetDisplayGlyphMetrics((ushort*)pGlyphIndices, (uint)characterCount, pGlyphMetrics, (float)emSize, textFormattingMode != TextFormattingMode.Display, isSideways, pixelsPerDip);
					}
				}
				finally
				{
					fontFace.Release();
				}
			}
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000A7A0C File Offset: 0x000A6E0C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private double GetLeftSidebearing(ushort glyph, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			return (double)this.GlyphMetrics(glyph, (double)this.DesignEmHeight, pixelsPerDip, textFormattingMode, isSideways).LeftSideBearing / (double)this.DesignEmHeight;
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000A7A3C File Offset: 0x000A6E3C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private double GetRightSidebearing(ushort glyph, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			return (double)this.GlyphMetrics(glyph, (double)this.DesignEmHeight, pixelsPerDip, textFormattingMode, isSideways).RightSideBearing / (double)this.DesignEmHeight;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000A7A6C File Offset: 0x000A6E6C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private double GetTopSidebearing(ushort glyph, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			return (double)this.GlyphMetrics(glyph, (double)this.DesignEmHeight, pixelsPerDip, textFormattingMode, isSideways).TopSideBearing / (double)this.DesignEmHeight;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000A7A9C File Offset: 0x000A6E9C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private double GetBottomSidebearing(ushort glyph, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			return (double)this.GlyphMetrics(glyph, (double)this.DesignEmHeight, pixelsPerDip, textFormattingMode, isSideways).BottomSideBearing / (double)this.DesignEmHeight;
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000A7ACC File Offset: 0x000A6ECC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private double GetBaseline(ushort glyph, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways)
		{
			GlyphMetrics metrics = this.GlyphMetrics(glyph, (double)this.DesignEmHeight, pixelsPerDip, textFormattingMode, isSideways);
			return GlyphTypeface.BaselineHelper(metrics) / (double)this.DesignEmHeight;
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000A7AFC File Offset: 0x000A6EFC
		internal static double BaselineHelper(GlyphMetrics metrics)
		{
			return -1.0 * ((double)metrics.BottomSideBearing + (double)metrics.VerticalOriginY - metrics.AdvanceHeight);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000A7B2C File Offset: 0x000A6F2C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void GetGlyphMetrics(ushort glyph, double renderingEmSize, double scalingFactor, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways, out double aw, out double ah, out double lsb, out double rsb, out double tsb, out double bsb, out double baseline)
		{
			this.CheckInitialized();
			GlyphMetrics glyphMetrics = this.GlyphMetrics(glyph, renderingEmSize, pixelsPerDip, textFormattingMode, isSideways);
			double num = renderingEmSize / (double)this.DesignEmHeight;
			if (TextFormattingMode.Display == textFormattingMode)
			{
				aw = TextFormatterImp.RoundDipForDisplayMode(num * glyphMetrics.AdvanceWidth, (double)pixelsPerDip) * scalingFactor;
				ah = TextFormatterImp.RoundDipForDisplayMode(num * glyphMetrics.AdvanceHeight, (double)pixelsPerDip) * scalingFactor;
				lsb = TextFormatterImp.RoundDipForDisplayMode(num * (double)glyphMetrics.LeftSideBearing, (double)pixelsPerDip) * scalingFactor;
				rsb = TextFormatterImp.RoundDipForDisplayMode(num * (double)glyphMetrics.RightSideBearing, (double)pixelsPerDip) * scalingFactor;
				tsb = TextFormatterImp.RoundDipForDisplayMode(num * (double)glyphMetrics.TopSideBearing, (double)pixelsPerDip) * scalingFactor;
				bsb = TextFormatterImp.RoundDipForDisplayMode(num * (double)glyphMetrics.BottomSideBearing, (double)pixelsPerDip) * scalingFactor;
				baseline = TextFormatterImp.RoundDipForDisplayMode(num * GlyphTypeface.BaselineHelper(glyphMetrics), (double)pixelsPerDip) * scalingFactor;
				return;
			}
			aw = num * glyphMetrics.AdvanceWidth * scalingFactor;
			ah = num * glyphMetrics.AdvanceHeight * scalingFactor;
			lsb = num * (double)glyphMetrics.LeftSideBearing * scalingFactor;
			rsb = num * (double)glyphMetrics.RightSideBearing * scalingFactor;
			tsb = num * (double)glyphMetrics.TopSideBearing * scalingFactor;
			bsb = num * (double)glyphMetrics.BottomSideBearing * scalingFactor;
			baseline = num * GlyphTypeface.BaselineHelper(glyphMetrics) * scalingFactor;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000A7C60 File Offset: 0x000A7060
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe void GetGlyphMetrics(ushort[] glyphs, int glyphsLength, double renderingEmSize, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways, GlyphMetrics[] glyphMetrics)
		{
			this.CheckInitialized();
			Invariant.Assert(glyphsLength <= glyphs.Length);
			fixed (GlyphMetrics[] array = glyphMetrics)
			{
				GlyphMetrics* pGlyphMetrics;
				if (glyphMetrics == null || array.Length == 0)
				{
					pGlyphMetrics = null;
				}
				else
				{
					pGlyphMetrics = &array[0];
				}
				fixed (ushort* ptr = &glyphs[0])
				{
					ushort* pGlyphIndices = ptr;
					this.GlyphMetrics(pGlyphIndices, glyphsLength, pGlyphMetrics, renderingEmSize, pixelsPerDip, textFormattingMode, isSideways);
				}
			}
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000A7CBC File Offset: 0x000A70BC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe Geometry ComputeGlyphOutline(ushort glyphIndex, bool sideways, double renderingEmSize)
		{
			this.CheckInitialized();
			FontFace fontFace = this._font.GetFontFace();
			byte* ptr;
			uint num;
			FillRule fillRule;
			try
			{
				HRESULT.Check(MS.Win32.PresentationCore.UnsafeNativeMethods.MilCoreApi.MilGlyphRun_GetGlyphOutline(fontFace.DWriteFontFaceAddRef, glyphIndex, sideways, renderingEmSize, out ptr, out num, out fillRule));
			}
			finally
			{
				fontFace.Release();
			}
			Geometry.PathGeometryData pathData = default(Geometry.PathGeometryData);
			byte[] array = new byte[num];
			Marshal.Copy(new IntPtr((void*)ptr), array, 0, checked((int)num));
			HRESULT.Check(MS.Win32.PresentationCore.UnsafeNativeMethods.MilCoreApi.MilGlyphRun_ReleasePathGeometryData(ptr));
			pathData.SerializedData = array;
			pathData.FillRule = fillRule;
			pathData.Matrix = CompositionResourceManager.MatrixToMilMatrix3x2D(Matrix.Identity);
			PathStreamGeometryContext pathStreamGeometryContext = new PathStreamGeometryContext(fillRule, null);
			PathGeometry.ParsePathGeometryData(pathData, pathStreamGeometryContext);
			return pathStreamGeometryContext.GetPathGeometry();
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000A7D80 File Offset: 0x000A7180
		[SecurityCritical]
		internal unsafe void GetAdvanceWidthsUnshaped(char* unsafeCharString, int stringLength, double emSize, float pixelsPerDip, double scalingFactor, int* advanceWidthsUnshaped, bool nullFont, TextFormattingMode textFormattingMode, bool isSideways)
		{
			this.CheckInitialized();
			Invariant.Assert(stringLength > 0);
			if (!nullFont)
			{
				CharacterBufferRange characters = new CharacterBufferRange(unsafeCharString, stringLength);
				GlyphMetrics[] glyphMetrics = BufferCache.GetGlyphMetrics(stringLength);
				this.GetGlyphMetricsOptimized(characters, emSize, pixelsPerDip, textFormattingMode, isSideways, glyphMetrics);
				if (TextFormattingMode.Display == textFormattingMode)
				{
					double num = emSize / (double)this.DesignEmHeight;
					for (int i = 0; i < stringLength; i++)
					{
						advanceWidthsUnshaped[i] = (int)Math.Round(TextFormatterImp.RoundDipForDisplayMode(glyphMetrics[i].AdvanceWidth * num, (double)pixelsPerDip) * scalingFactor);
					}
				}
				else
				{
					double num2 = emSize * scalingFactor / (double)this.DesignEmHeight;
					for (int j = 0; j < stringLength; j++)
					{
						advanceWidthsUnshaped[j] = (int)Math.Round(glyphMetrics[j].AdvanceWidth * num2);
					}
				}
				BufferCache.ReleaseGlyphMetrics(glyphMetrics);
				return;
			}
			int num3 = (int)Math.Round(TextFormatterImp.RoundDip(emSize * this.GetAdvanceWidth(0, pixelsPerDip, textFormattingMode, isSideways), (double)pixelsPerDip, textFormattingMode) * scalingFactor);
			for (int k = 0; k < stringLength; k++)
			{
				advanceWidthsUnshaped[k] = num3;
			}
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000A7E88 File Offset: 0x000A7288
		internal GlyphRun ComputeUnshapedGlyphRun(Point origin, CharacterBufferRange charBufferRange, IList<double> charWidths, double emSize, float pixelsPerDip, double emHintingSize, bool nullGlyph, CultureInfo cultureInfo, string deviceFontName, TextFormattingMode textFormattingMode)
		{
			this.CheckInitialized();
			ushort[] array = new ushort[charBufferRange.Length];
			if (nullGlyph)
			{
				for (int i = 0; i < charBufferRange.Length; i++)
				{
					array[i] = 0;
				}
			}
			else
			{
				this.GetGlyphIndicesOptimized(charBufferRange, array, pixelsPerDip);
			}
			return GlyphRun.TryCreate(this, 0, false, emSize, pixelsPerDip, array, origin, charWidths, null, new PartialList<char>(charBufferRange.CharacterBuffer, charBufferRange.OffsetToFirstChar, charBufferRange.Length), deviceFontName, null, null, XmlLanguage.GetLanguage(cultureInfo.IetfLanguageTag), textFormattingMode);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000A7F0C File Offset: 0x000A730C
		internal void GetGlyphIndicesOptimized(CharacterBufferRange characters, ushort[] glyphIndices, float pixelsPerDip)
		{
			this.GetGlyphMetricsOptimized(characters, 0.0, pixelsPerDip, glyphIndices, null, TextFormattingMode.Ideal, false);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000A7F30 File Offset: 0x000A7330
		internal void GetGlyphMetricsOptimized(CharacterBufferRange characters, double emSize, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways, GlyphMetrics[] glyphMetrics)
		{
			this.GetGlyphMetricsOptimized(characters, emSize, pixelsPerDip, null, glyphMetrics, textFormattingMode, isSideways);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000A7F50 File Offset: 0x000A7350
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe void GetGlyphMetricsOptimized(CharacterBufferRange characters, double emSize, float pixelsPerDip, ushort[] glyphIndices, GlyphMetrics[] glyphMetrics, TextFormattingMode textFormattingMode, bool isSideways)
		{
			if (characters.Length * 4 < 1024)
			{
				uint* ptr = stackalloc uint[checked(unchecked((UIntPtr)characters.Length) * 4)];
				for (int i = 0; i < characters.Length; i++)
				{
					ptr[i] = (uint)characters[i];
				}
				this.GetGlyphMetricsAndIndicesOptimized(ptr, characters.Length, emSize, pixelsPerDip, glyphIndices, glyphMetrics, textFormattingMode, isSideways);
				return;
			}
			uint[] array = new uint[characters.Length];
			for (int j = 0; j < characters.Length; j++)
			{
				array[j] = (uint)characters[j];
			}
			fixed (uint* ptr2 = &array[0])
			{
				uint* pCodepoints = ptr2;
				this.GetGlyphMetricsAndIndicesOptimized(pCodepoints, characters.Length, emSize, pixelsPerDip, glyphIndices, glyphMetrics, textFormattingMode, isSideways);
			}
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000A800C File Offset: 0x000A740C
		[SecurityCritical]
		private unsafe void GetGlyphMetricsAndIndicesOptimized(uint* pCodepoints, int characterCount, double emSize, float pixelsPerDip, ushort[] glyphIndices, GlyphMetrics[] glyphMetrics, TextFormattingMode textFormattingMode, bool isSideways)
		{
			bool flag = false;
			if (glyphIndices == null)
			{
				glyphIndices = BufferCache.GetUShorts(characterCount);
				flag = true;
			}
			fixed (ushort* ptr = &glyphIndices[0])
			{
				ushort* ptr2 = ptr;
				this._fontFace.CharacterMap.TryGetValues(pCodepoints, checked((uint)characterCount), ptr2);
				if (glyphMetrics != null)
				{
					fixed (GlyphMetrics* ptr3 = &glyphMetrics[0])
					{
						GlyphMetrics* pGlyphMetrics = ptr3;
						this.GlyphMetrics(ptr2, characterCount, pGlyphMetrics, emSize, pixelsPerDip, textFormattingMode, isSideways);
					}
				}
			}
			if (flag)
			{
				BufferCache.ReleaseUShorts(glyphIndices);
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060029C2 RID: 10690 RVA: 0x000A807C File Offset: 0x000A747C
		internal FontSource FontSource
		{
			get
			{
				this.CheckInitialized();
				return this._fontSource;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000A8098 File Offset: 0x000A7498
		internal int FaceIndex
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				FontFace fontFace = this._font.GetFontFace();
				int result;
				try
				{
					result = checked((int)fontFace.Index);
				}
				finally
				{
					fontFace.Release();
				}
				return result;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000A80E8 File Offset: 0x000A74E8
		internal FontFaceLayoutInfo FontFaceLayoutInfo
		{
			get
			{
				this.CheckInitialized();
				return this._fontFace;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000A8104 File Offset: 0x000A7504
		internal ushort BlankGlyphIndex
		{
			get
			{
				this.CheckInitialized();
				return this._fontFace.BlankGlyph;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000A8124 File Offset: 0x000A7524
		internal FontTechnology FontTechnology
		{
			get
			{
				this.CheckInitialized();
				return this._fontFace.FontTechnology;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000A8144 File Offset: 0x000A7544
		internal ushort DesignEmHeight
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.CheckInitialized();
				return this._font.Metrics.DesignUnitsPerEm;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x000A8168 File Offset: 0x000A7568
		internal IntPtr GetDWriteFontAddRef
		{
			[SecurityCritical]
			get
			{
				this.CheckInitialized();
				return this._font.DWriteFontAddRef;
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000A8188 File Offset: 0x000A7588
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private IDictionary<CultureInfo, string> GetFontInfo(InformationalStringID informationalStringID)
		{
			LocalizedStrings result;
			if (this._font.GetInformationalStrings(informationalStringID, out result))
			{
				return result;
			}
			return new LocalizedStrings();
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.ComponentModel.ISupportInitialize.BeginInit" />.</summary>
		// Token: 0x060029CA RID: 10698 RVA: 0x000A81AC File Offset: 0x000A75AC
		void ISupportInitialize.BeginInit()
		{
			if (this._initializationState == GlyphTypeface.InitializationState.IsInitialized)
			{
				throw new InvalidOperationException(SR.Get("OnlyOneInitialization"));
			}
			if (this._initializationState == GlyphTypeface.InitializationState.IsInitializing)
			{
				throw new InvalidOperationException(SR.Get("InInitialization"));
			}
			this._initializationState = GlyphTypeface.InitializationState.IsInitializing;
		}

		/// <summary>Para obter uma descrição desse membro, consulte <see cref="M:System.ComponentModel.ISupportInitialize.EndInit" />.</summary>
		// Token: 0x060029CB RID: 10699 RVA: 0x000A81F4 File Offset: 0x000A75F4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		void ISupportInitialize.EndInit()
		{
			if (this._initializationState != GlyphTypeface.InitializationState.IsInitializing)
			{
				throw new InvalidOperationException(SR.Get("NotInInitialization"));
			}
			this.Initialize((this._originalUri == null) ? null : this._originalUri.Value, this._styleSimulations);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000A823C File Offset: 0x000A763C
		private void CheckInitialized()
		{
			if (this._initializationState != GlyphTypeface.InitializationState.IsInitialized)
			{
				throw new InvalidOperationException(SR.Get("InitializationIncomplete"));
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000A8264 File Offset: 0x000A7664
		private void CheckInitializing()
		{
			if (this._initializationState != GlyphTypeface.InitializationState.IsInitializing)
			{
				throw new InvalidOperationException(SR.Get("NotInInitialization"));
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000A828C File Offset: 0x000A768C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private GlyphTypeface.GlyphIndexer CreateGlyphIndexer(GlyphTypeface.GlyphAccessor accessor)
		{
			FontFace fontFace = this._font.GetFontFace();
			GlyphTypeface.GlyphIndexer result;
			try
			{
				result = new GlyphTypeface.GlyphIndexer(accessor, fontFace.GlyphCount);
			}
			finally
			{
				fontFace.Release();
			}
			return result;
		}

		// Token: 0x040012DE RID: 4830
		private FontFaceLayoutInfo _fontFace;

		// Token: 0x040012DF RID: 4831
		private StyleSimulations _styleSimulations;

		// Token: 0x040012E0 RID: 4832
		private Font _font;

		// Token: 0x040012E1 RID: 4833
		private FontSource _fontSource;

		// Token: 0x040012E2 RID: 4834
		private SecurityCriticalDataClass<Uri> _originalUri;

		// Token: 0x040012E3 RID: 4835
		private SecurityCriticalDataForSet<CodeAccessPermission> _fileIOPermObj;

		// Token: 0x040012E4 RID: 4836
		private const double CFFConversionFactor = 1.52587890625E-05;

		// Token: 0x040012E5 RID: 4837
		private GlyphTypeface.InitializationState _initializationState;

		// Token: 0x02000889 RID: 2185
		// (Invoke) Token: 0x060057F2 RID: 22514
		private delegate double GlyphAccessor(ushort glyphIndex, float pixelsPerDip, TextFormattingMode textFormattingMode, bool isSideways);

		// Token: 0x0200088A RID: 2186
		private class GlyphIndexer : IDictionary<ushort, double>, ICollection<KeyValuePair<ushort, double>>, IEnumerable<KeyValuePair<ushort, double>>, IEnumerable
		{
			// Token: 0x060057F5 RID: 22517 RVA: 0x00167200 File Offset: 0x00166600
			internal GlyphIndexer(GlyphTypeface.GlyphAccessor accessor, ushort numberOfGlyphs)
			{
				this._accessor = accessor;
				this._numberOfGlyphs = numberOfGlyphs;
			}

			// Token: 0x060057F6 RID: 22518 RVA: 0x00167224 File Offset: 0x00166624
			public void Add(ushort key, double value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057F7 RID: 22519 RVA: 0x00167238 File Offset: 0x00166638
			public bool ContainsKey(ushort key)
			{
				return key < this._numberOfGlyphs;
			}

			// Token: 0x17001229 RID: 4649
			// (get) Token: 0x060057F8 RID: 22520 RVA: 0x00167250 File Offset: 0x00166650
			public ICollection<ushort> Keys
			{
				get
				{
					return new SequentialUshortCollection(this._numberOfGlyphs);
				}
			}

			// Token: 0x060057F9 RID: 22521 RVA: 0x00167268 File Offset: 0x00166668
			public bool Remove(ushort key)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057FA RID: 22522 RVA: 0x0016727C File Offset: 0x0016667C
			public bool TryGetValue(ushort key, out double value)
			{
				if (this.ContainsKey(key))
				{
					value = this[key];
					return true;
				}
				value = 0.0;
				return false;
			}

			// Token: 0x1700122A RID: 4650
			// (get) Token: 0x060057FB RID: 22523 RVA: 0x001672AC File Offset: 0x001666AC
			public ICollection<double> Values
			{
				get
				{
					return new GlyphTypeface.GlyphIndexer.ValueCollection(this);
				}
			}

			// Token: 0x1700122B RID: 4651
			public double this[ushort key]
			{
				get
				{
					return this._accessor(key, 1f, TextFormattingMode.Ideal, false);
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x060057FE RID: 22526 RVA: 0x001672F4 File Offset: 0x001666F4
			public void Add(KeyValuePair<ushort, double> item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060057FF RID: 22527 RVA: 0x00167308 File Offset: 0x00166708
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005800 RID: 22528 RVA: 0x0016731C File Offset: 0x0016671C
			public bool Contains(KeyValuePair<ushort, double> item)
			{
				return this.ContainsKey(item.Key);
			}

			// Token: 0x06005801 RID: 22529 RVA: 0x00167338 File Offset: 0x00166738
			public void CopyTo(KeyValuePair<ushort, double>[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.Get("Collection_BadRank"));
				}
				if (arrayIndex < 0 || arrayIndex >= array.Length || arrayIndex + this.Count > array.Length)
				{
					throw new ArgumentOutOfRangeException("arrayIndex");
				}
				ushort num = 0;
				while ((int)num < this.Count)
				{
					array[arrayIndex + (int)num] = new KeyValuePair<ushort, double>(num, this[num]);
					num += 1;
				}
			}

			// Token: 0x1700122C RID: 4652
			// (get) Token: 0x06005802 RID: 22530 RVA: 0x001673B8 File Offset: 0x001667B8
			public int Count
			{
				get
				{
					return (int)this._numberOfGlyphs;
				}
			}

			// Token: 0x1700122D RID: 4653
			// (get) Token: 0x06005803 RID: 22531 RVA: 0x001673CC File Offset: 0x001667CC
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06005804 RID: 22532 RVA: 0x001673DC File Offset: 0x001667DC
			public bool Remove(KeyValuePair<ushort, double> item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005805 RID: 22533 RVA: 0x001673F0 File Offset: 0x001667F0
			public IEnumerator<KeyValuePair<ushort, double>> GetEnumerator()
			{
				ushort i = 0;
				while ((int)i < this.Count)
				{
					yield return new KeyValuePair<ushort, double>(i, this[i]);
					ushort num = i + 1;
					i = num;
				}
				yield break;
			}

			// Token: 0x06005806 RID: 22534 RVA: 0x0016740C File Offset: 0x0016680C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<KeyValuePair<ushort, double>>)this).GetEnumerator();
			}

			// Token: 0x040028CF RID: 10447
			private GlyphTypeface.GlyphAccessor _accessor;

			// Token: 0x040028D0 RID: 10448
			private ushort _numberOfGlyphs;

			// Token: 0x02000A24 RID: 2596
			private class ValueCollection : ICollection<double>, IEnumerable<double>, IEnumerable
			{
				// Token: 0x06005C25 RID: 23589 RVA: 0x001722A4 File Offset: 0x001716A4
				public ValueCollection(GlyphTypeface.GlyphIndexer glyphIndexer)
				{
					this._glyphIndexer = glyphIndexer;
				}

				// Token: 0x06005C26 RID: 23590 RVA: 0x001722C0 File Offset: 0x001716C0
				public void Add(double item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06005C27 RID: 23591 RVA: 0x001722D4 File Offset: 0x001716D4
				public void Clear()
				{
					throw new NotSupportedException();
				}

				// Token: 0x06005C28 RID: 23592 RVA: 0x001722E8 File Offset: 0x001716E8
				public bool Contains(double item)
				{
					foreach (double num in this)
					{
						if (num == item)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06005C29 RID: 23593 RVA: 0x00172340 File Offset: 0x00171740
				public void CopyTo(double[] array, int arrayIndex)
				{
					if (array == null)
					{
						throw new ArgumentNullException("array");
					}
					if (array.Rank != 1)
					{
						throw new ArgumentException(SR.Get("Collection_BadRank"));
					}
					if (arrayIndex < 0 || arrayIndex >= array.Length || arrayIndex + this.Count > array.Length)
					{
						throw new ArgumentOutOfRangeException("arrayIndex");
					}
					ushort num = 0;
					while ((int)num < this.Count)
					{
						array[arrayIndex + (int)num] = this._glyphIndexer[num];
						num += 1;
					}
				}

				// Token: 0x170012DE RID: 4830
				// (get) Token: 0x06005C2A RID: 23594 RVA: 0x001723BC File Offset: 0x001717BC
				public int Count
				{
					get
					{
						return (int)this._glyphIndexer._numberOfGlyphs;
					}
				}

				// Token: 0x170012DF RID: 4831
				// (get) Token: 0x06005C2B RID: 23595 RVA: 0x001723D4 File Offset: 0x001717D4
				public bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06005C2C RID: 23596 RVA: 0x001723E4 File Offset: 0x001717E4
				public bool Remove(double item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06005C2D RID: 23597 RVA: 0x001723F8 File Offset: 0x001717F8
				public IEnumerator<double> GetEnumerator()
				{
					ushort i = 0;
					while ((int)i < this.Count)
					{
						yield return this._glyphIndexer[i];
						ushort num = i + 1;
						i = num;
					}
					yield break;
				}

				// Token: 0x06005C2E RID: 23598 RVA: 0x00172414 File Offset: 0x00171814
				IEnumerator IEnumerable.GetEnumerator()
				{
					return ((IEnumerable<double>)this).GetEnumerator();
				}

				// Token: 0x04002F99 RID: 12185
				private GlyphTypeface.GlyphIndexer _glyphIndexer;
			}
		}

		// Token: 0x0200088B RID: 2187
		private enum InitializationState
		{
			// Token: 0x040028D2 RID: 10450
			Uninitialized,
			// Token: 0x040028D3 RID: 10451
			IsInitializing,
			// Token: 0x040028D4 RID: 10452
			IsInitialized
		}
	}
}
