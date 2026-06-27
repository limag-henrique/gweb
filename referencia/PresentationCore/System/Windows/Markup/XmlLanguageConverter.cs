using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Windows.Markup
{
	/// <summary>Fornece a conversão de tipo para a classe <see cref="T:System.Windows.Markup.XmlLanguage" />.</summary>
	// Token: 0x02000203 RID: 515
	public class XmlLanguageConverter : TypeConverter
	{
		/// <summary>Retorna se este conversor pode converter um objeto de um tipo no tipo <see cref="T:System.Windows.Markup.XmlLanguage" /> compatível com esse conversor.</summary>
		/// <param name="typeDescriptorContext">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="sourceType">Um tipo que representa o tipo do qual você deseja converter.</param>
		/// <returns>
		///   <see langword="true" /> se esse conversor puder realizar a conversão; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D60 RID: 3424 RVA: 0x00032BFC File Offset: 0x00031FFC
		public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptorContext, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Retorna se esse conversor pode converter o objeto para o tipo especificado.</summary>
		/// <param name="typeDescriptorContext">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="destinationType">O tipo no qual você deseja converter.</param>
		/// <returns>
		///   <see langword="true" /> se esse conversor puder realizar a conversão; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D61 RID: 3425 RVA: 0x00032C1C File Offset: 0x0003201C
		public override bool CanConvertTo(ITypeDescriptorContext typeDescriptorContext, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string);
		}

		/// <summary>Converte o valor de cadeia de caracteres especificado no tipo <see cref="T:System.Windows.Markup.XmlLanguage" /> compatível com esse conversor.</summary>
		/// <param name="typeDescriptorContext">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="cultureInfo">O <see cref="T:System.Globalization.CultureInfo" /> a ser usado como a cultura atual.</param>
		/// <param name="source">A cadeia de caracteres a ser convertida.</param>
		/// <returns>Um objeto que representa o valor convertido.</returns>
		/// <exception cref="T:System.InvalidOperationException">Não foi possível executar a conversão.</exception>
		// Token: 0x06000D62 RID: 3426 RVA: 0x00032C50 File Offset: 0x00032050
		public override object ConvertFrom(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object source)
		{
			string text = source as string;
			if (text != null)
			{
				return XmlLanguage.GetLanguage(text);
			}
			throw base.GetConvertFromException(source);
		}

		/// <summary>Converte a <see cref="T:System.Windows.Markup.XmlLanguage" /> especificada no tipo especificado.</summary>
		/// <param name="typeDescriptorContext">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="cultureInfo">O <see cref="T:System.Globalization.CultureInfo" /> a ser usado como a cultura atual.</param>
		/// <param name="value">O objeto a ser convertido. Espera-se que seja do tipo <see cref="T:System.Windows.Markup.XmlLanguage" />.</param>
		/// <param name="destinationType">Um tipo que representa o tipo no qual você deseja converter.</param>
		/// <returns>Um objeto que representa o valor convertido.</returns>
		/// <exception cref="T:System.InvalidOperationException">Não foi possível executar a conversão.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> é <see langword="null" />.</exception>
		// Token: 0x06000D63 RID: 3427 RVA: 0x00032C78 File Offset: 0x00032078
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext typeDescriptorContext, CultureInfo cultureInfo, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			XmlLanguage xmlLanguage = value as XmlLanguage;
			if (xmlLanguage != null)
			{
				if (destinationType == typeof(string))
				{
					return xmlLanguage.IetfLanguageTag;
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					MethodInfo method = typeof(XmlLanguage).GetMethod("GetLanguage", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, new Type[]
					{
						typeof(string)
					}, null);
					return new InstanceDescriptor(method, new object[]
					{
						xmlLanguage.IetfLanguageTag
					});
				}
			}
			throw base.GetConvertToException(value, destinationType);
		}
	}
}
