using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Navigation;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias do tipo <see cref="T:System.String" /> de e em instâncias <see cref="T:System.Windows.Media.FontFamily" />.</summary>
	// Token: 0x02000397 RID: 919
	public class FontFamilyConverter : TypeConverter
	{
		/// <summary>Determina se uma classe pode ser convertida de um determinado tipo em uma instância de um <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <param name="td">Descreve as informações de contexto de um tipo.</param>
		/// <param name="t">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor pode converter do tipo especificado em uma instância de <see cref="T:System.Windows.Media.FontFamily" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002267 RID: 8807 RVA: 0x0008ABA4 File Offset: 0x00089FA4
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.FontFamily" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão da instância de <see cref="T:System.Windows.Media.FontFamily" /> está sendo avaliada.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor pode converter essa instância de <see cref="T:System.Windows.Media.FontFamily" /> para o tipo especificado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002268 RID: 8808 RVA: 0x0008ABC4 File Offset: 0x00089FC4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return destinationType == typeof(FontFamily) || base.CanConvertTo(context, destinationType);
			}
			if (context != null)
			{
				FontFamily fontFamily = context.Instance as FontFamily;
				return fontFamily != null && fontFamily.Source != null && fontFamily.Source.Length != 0;
			}
			return true;
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="cultureInfo">Informações específicas de cultura que devem ser respeitadas durante a conversão.</param>
		/// <param name="o">O objeto que está sendo convertido.</param>
		/// <returns>A instância de <see cref="T:System.Windows.Media.FontFamily" /> que é criado com base no parâmetro <paramref name="o" /> convertido.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> não é <see langword="null" /> e não é um tipo válido que pode ser convertido em um <see cref="T:System.Windows.Media.FontFamily" />.</exception>
		// Token: 0x06002269 RID: 8809 RVA: 0x0008AC2C File Offset: 0x0008A02C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo cultureInfo, object o)
		{
			if (o == null || !(o.GetType() == typeof(string)))
			{
				return base.ConvertFrom(context, cultureInfo, o);
			}
			string text = o as string;
			if (text == null || text.Length == 0)
			{
				throw base.GetConvertFromException(text);
			}
			Uri uri = null;
			if (context != null)
			{
				IUriContext uriContext = (IUriContext)context.GetService(typeof(IUriContext));
				if (uriContext != null)
				{
					if (uriContext.BaseUri != null)
					{
						uri = uriContext.BaseUri;
						if (!uri.IsAbsoluteUri)
						{
							uri = new Uri(BaseUriHelper.BaseUri, uri);
						}
					}
					else
					{
						uri = BaseUriHelper.BaseUri;
					}
				}
			}
			return new FontFamily(uri, text);
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Informações específicas de cultura que devem ser respeitadas durante a conversão.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <param name="destinationType">O tipo para o qual esta instância de <see cref="T:System.Windows.Media.FontFamily" /> é convertida.</param>
		/// <returns>O objeto que é criado com base na instância convertida de <see cref="T:System.Windows.Media.FontFamily" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre se <paramref name="value" /> ou <paramref name="destinationType" /> não for um tipo válido para conversão.</exception>
		/// <exception cref="T:System.ArgumentNullException">Ocorre se <paramref name="value" /> ou <paramref name="destinationType" /> é <see langword="null" />.</exception>
		// Token: 0x0600226A RID: 8810 RVA: 0x0008ACD0 File Offset: 0x0008A0D0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FontFamily fontFamily = value as FontFamily;
			if (fontFamily == null)
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"FontFamily"
				}), "value");
			}
			if (null == destinationType)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (fontFamily.Source != null)
			{
				return fontFamily.Source;
			}
			string text = null;
			CultureInfo cultureInfo = null;
			if (culture != null)
			{
				if (culture.Equals(CultureInfo.InvariantCulture))
				{
					culture = null;
				}
				else
				{
					cultureInfo = culture.Parent;
					if (cultureInfo != null && (cultureInfo.Equals(CultureInfo.InvariantCulture) || cultureInfo == culture))
					{
						cultureInfo = null;
					}
				}
			}
			LanguageSpecificStringDictionary familyNames = fontFamily.FamilyNames;
			if ((culture == null || !familyNames.TryGetValue(XmlLanguage.GetLanguage(culture.IetfLanguageTag), out text)) && (cultureInfo == null || !familyNames.TryGetValue(XmlLanguage.GetLanguage(cultureInfo.IetfLanguageTag), out text)) && !familyNames.TryGetValue(XmlLanguage.Empty, out text))
			{
				foreach (FontFamilyMap fontFamilyMap in fontFamily.FamilyMaps)
				{
					if (FontFamilyMap.MatchCulture(fontFamilyMap.Language, culture))
					{
						text = fontFamilyMap.Target;
						break;
					}
				}
				if (text == null)
				{
					text = "#GLOBAL USER INTERFACE";
				}
			}
			return text;
		}
	}
}
