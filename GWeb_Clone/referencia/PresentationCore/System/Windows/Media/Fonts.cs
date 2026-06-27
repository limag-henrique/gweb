using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece suporte de enumeração para objetos <see cref="T:System.Windows.Media.FontFamily" /> e <see cref="T:System.Windows.Media.Typeface" />.</summary>
	// Token: 0x02000399 RID: 921
	public static class Fonts
	{
		/// <summary>Retorna a coleção de objetos <see cref="T:System.Windows.Media.FontFamily" /> de um valor de cadeia de caracteres que representa o local das fontes.</summary>
		/// <param name="location">O local que contém as fontes.</param>
		/// <returns>Um <see cref="T:System.Collections.Generic.ICollection`1" /> de objetos <see cref="T:System.Windows.Media.FontFamily" /> que representa as fontes em <paramref name="location" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="location" /> é <see langword="null" />. Não é possível passar <see langword="null" />, pois esse parâmetro é tratado como um caminho ou URI.</exception>
		// Token: 0x06002271 RID: 8817 RVA: 0x0008AF10 File Offset: 0x0008A310
		public static ICollection<FontFamily> GetFontFamilies(string location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			return Fonts.GetFontFamilies(null, location);
		}

		/// <summary>Retorna uma coleção de objetos <see cref="T:System.Windows.Media.FontFamily" /> de um valor URI (Uniform Resource Identifier) que representa o local das fontes.</summary>
		/// <param name="baseUri">O valor URI base do local das fontes.</param>
		/// <returns>Um <see cref="T:System.Collections.Generic.ICollection`1" /> de objetos <see cref="T:System.Windows.Media.FontFamily" /> que representa as fontes em <paramref name="baseUri" />.</returns>
		// Token: 0x06002272 RID: 8818 RVA: 0x0008AF34 File Offset: 0x0008A334
		public static ICollection<FontFamily> GetFontFamilies(Uri baseUri)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			return Fonts.GetFontFamilies(baseUri, null);
		}

		/// <summary>Retorna uma coleção de objetos <see cref="T:System.Windows.Media.FontFamily" /> usando um valor URI (Uniform Resource Identifier) base para resolver o local da fonte.</summary>
		/// <param name="baseUri">O valor URI base do local das fontes.</param>
		/// <param name="location">O local que contém as fontes.</param>
		/// <returns>Um <see cref="T:System.Collections.Generic.ICollection`1" /> de objetos <see cref="T:System.Windows.Media.FontFamily" /> que representa as fontes no local de fonte resolvido.</returns>
		// Token: 0x06002273 RID: 8819 RVA: 0x0008AF5C File Offset: 0x0008A35C
		public static ICollection<FontFamily> GetFontFamilies(Uri baseUri, string location)
		{
			if (baseUri != null && !baseUri.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.Get("UriNotAbsolute"), "baseUri");
			}
			Uri uri;
			if (!string.IsNullOrEmpty(location) && Uri.TryCreate(location, UriKind.Absolute, out uri))
			{
				if (!Util.IsSupportedSchemeForAbsoluteFontFamilyUri(uri))
				{
					throw new ArgumentException(SR.Get("InvalidAbsoluteUriInFontFamilyName"), "location");
				}
				location = uri.GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
			}
			else
			{
				if (baseUri == null)
				{
					throw new ArgumentNullException("baseUri", SR.Get("NullBaseUriParam", new object[]
					{
						"baseUri",
						"location"
					}));
				}
				if (string.IsNullOrEmpty(location))
				{
					location = "./";
				}
				else if (Util.IsReferenceToWindowsFonts(location))
				{
					location = "./" + location;
				}
				uri = new Uri(baseUri, location);
			}
			return Fonts.CreateFamilyCollection(uri, baseUri, location);
		}

		/// <summary>Retorna a coleção de objetos <see cref="T:System.Windows.Media.Typeface" /> de um valor de cadeia de caracteres que representa um local de diretório de fontes.</summary>
		/// <param name="location">O local que contém as fontes.</param>
		/// <returns>Um <see cref="T:System.Collections.Generic.ICollection`1" /> de objetos <see cref="T:System.Windows.Media.Typeface" /> que representa as fontes em <paramref name="location" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">O <paramref name="location" /> é <see langword="null" />. Não é possível passar <see langword="null" />, pois esse parâmetro é tratado como um caminho ou URI.</exception>
		// Token: 0x06002274 RID: 8820 RVA: 0x0008B038 File Offset: 0x0008A438
		public static ICollection<Typeface> GetTypefaces(string location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			return new Fonts.TypefaceCollection(Fonts.GetFontFamilies(null, location));
		}

		/// <summary>Retorna uma coleção de objetos <see cref="T:System.Windows.Media.Typeface" /> de um valor URI (Uniform Resource Identifier) que representa o local da fonte.</summary>
		/// <param name="baseUri">O valor URI base do local das fontes.</param>
		/// <returns>Um <see cref="T:System.Collections.Generic.ICollection`1" /> de objetos <see cref="T:System.Windows.Media.Typeface" /> que representa as fontes em <paramref name="baseUri" />.</returns>
		// Token: 0x06002275 RID: 8821 RVA: 0x0008B064 File Offset: 0x0008A464
		public static ICollection<Typeface> GetTypefaces(Uri baseUri)
		{
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			return new Fonts.TypefaceCollection(Fonts.GetFontFamilies(baseUri, null));
		}

		/// <summary>Retorna uma coleção de objetos <see cref="T:System.Windows.Media.Typeface" /> usando um valor URI (Uniform Resource Identifier) base para resolver o local da fonte.</summary>
		/// <param name="baseUri">O valor URI base do local das fontes.</param>
		/// <param name="location">O local que contém as fontes.</param>
		/// <returns>Um <see cref="T:System.Collections.Generic.ICollection`1" /> de objetos <see cref="T:System.Windows.Media.Typeface" /> que representa as fontes no local de fonte resolvido.</returns>
		// Token: 0x06002276 RID: 8822 RVA: 0x0008B098 File Offset: 0x0008A498
		public static ICollection<Typeface> GetTypefaces(Uri baseUri, string location)
		{
			return new Fonts.TypefaceCollection(Fonts.GetFontFamilies(baseUri, location));
		}

		/// <summary>Obtém a coleção de objetos <see cref="T:System.Windows.Media.FontFamily" /> do local de fonte de sistema padrão.</summary>
		/// <returns>Uma <see cref="T:System.Collections.Generic.ICollection`1" /> de <see cref="T:System.Windows.Media.FontFamily" /> objetos que representam as fontes na coleção de fontes do sistema.</returns>
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x0008B0B8 File Offset: 0x0008A4B8
		public static ICollection<FontFamily> SystemFontFamilies
		{
			get
			{
				return Fonts._defaultFontCollection;
			}
		}

		/// <summary>Obtém a coleção de objetos <see cref="T:System.Windows.Media.Typeface" /> do local de fonte de sistema padrão.</summary>
		/// <returns>Uma <see cref="T:System.Collections.Generic.ICollection`1" /> de <see cref="T:System.Windows.Media.Typeface" /> objetos que representam as fontes na coleção de fontes do sistema.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0008B0CC File Offset: 0x0008A4CC
		public static ICollection<Typeface> SystemTypefaces
		{
			get
			{
				return new Fonts.TypefaceCollection(Fonts._defaultFontCollection);
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0008B0E8 File Offset: 0x0008A4E8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static ICollection<FontFamily> CreateFamilyCollection(Uri fontLocation, Uri fontFamilyBaseUri, string fontFamilyLocationReference)
		{
			FamilyCollection familyCollection = (fontLocation == Util.WindowsFontsUriObject) ? FamilyCollection.FromWindowsFonts(fontLocation) : FamilyCollection.FromUri(fontLocation);
			FontFamily[] fontFamilies = familyCollection.GetFontFamilies(fontFamilyBaseUri, fontFamilyLocationReference);
			return Array.AsReadOnly<FontFamily>(fontFamilies);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0008B11C File Offset: 0x0008A51C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static ICollection<FontFamily> CreateDefaultFamilyCollection()
		{
			return Fonts.CreateFamilyCollection(Util.WindowsFontsUriObject, null, null);
		}

		// Token: 0x04001108 RID: 4360
		private static readonly ICollection<FontFamily> _defaultFontCollection = Fonts.CreateDefaultFamilyCollection();

		// Token: 0x0200086D RID: 2157
		private struct TypefaceCollection : ICollection<Typeface>, IEnumerable<Typeface>, IEnumerable
		{
			// Token: 0x0600575B RID: 22363 RVA: 0x00164F10 File Offset: 0x00164310
			public TypefaceCollection(IEnumerable<FontFamily> families)
			{
				this._families = families;
			}

			// Token: 0x0600575C RID: 22364 RVA: 0x00164F24 File Offset: 0x00164324
			public void Add(Typeface item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600575D RID: 22365 RVA: 0x00164F38 File Offset: 0x00164338
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600575E RID: 22366 RVA: 0x00164F4C File Offset: 0x0016434C
			public bool Contains(Typeface item)
			{
				foreach (Typeface typeface in this)
				{
					if (typeface.Equals(item))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600575F RID: 22367 RVA: 0x00164FAC File Offset: 0x001643AC
			public void CopyTo(Typeface[] array, int arrayIndex)
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
				foreach (Typeface typeface in this)
				{
					array[arrayIndex++] = typeface;
				}
			}

			// Token: 0x17001202 RID: 4610
			// (get) Token: 0x06005760 RID: 22368 RVA: 0x0016504C File Offset: 0x0016444C
			public int Count
			{
				get
				{
					int num = 0;
					foreach (Typeface typeface in this)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x17001203 RID: 4611
			// (get) Token: 0x06005761 RID: 22369 RVA: 0x001650A0 File Offset: 0x001644A0
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06005762 RID: 22370 RVA: 0x001650B0 File Offset: 0x001644B0
			public bool Remove(Typeface item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005763 RID: 22371 RVA: 0x001650C4 File Offset: 0x001644C4
			public IEnumerator<Typeface> GetEnumerator()
			{
				foreach (FontFamily fontFamily in this._families)
				{
					foreach (Typeface typeface in fontFamily.GetTypefaces())
					{
						yield return typeface;
					}
					IEnumerator<Typeface> enumerator2 = null;
				}
				IEnumerator<FontFamily> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06005764 RID: 22372 RVA: 0x001650E4 File Offset: 0x001644E4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<Typeface>)this).GetEnumerator();
			}

			// Token: 0x0400286E RID: 10350
			private IEnumerable<FontFamily> _families;
		}
	}
}
