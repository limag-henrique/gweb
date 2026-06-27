using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte um <see cref="T:System.Windows.Media.CacheMode" /> de um tipo de dados em outro.</summary>
	// Token: 0x020003A1 RID: 929
	public sealed class CacheModeConverter : TypeConverter
	{
		/// <summary>Determina se este <see cref="T:System.Windows.Media.CacheModeConverter" /> pode converter uma instância do tipo especificado para um <see cref="T:System.Windows.Media.CacheMode" />, usando o contexto especificado.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="sourceType">Um <see cref="T:System.Type" /> que especifica o tipo do qual você deseja converter.</param>
		/// <returns>
		///   <see langword="true" /> se esse <see cref="T:System.Windows.Media.CacheModeConverter" /> puder realizar a conversão; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022E0 RID: 8928 RVA: 0x0008CFF0 File Offset: 0x0008C3F0
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se este <see cref="T:System.Windows.Media.CacheModeConverter" /> pode converter um <see cref="T:System.Windows.Media.CacheMode" /> para uma instância de um tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="destinationType">Um tipo que representa o tipo para o qual converter.</param>
		/// <returns>
		///   <see langword="true" /> se esse <see cref="T:System.Windows.Media.CacheModeConverter" /> puder realizar a conversão; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022E1 RID: 8929 RVA: 0x0008D01C File Offset: 0x0008C41C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.CanConvertTo(context, destinationType);
			}
			if (context == null || context.Instance == null)
			{
				return true;
			}
			if (!(context.Instance is CacheMode))
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"CacheMode"
				}), "context");
			}
			CacheMode cacheMode = (CacheMode)context.Instance;
			return cacheMode.CanSerializeToString();
		}

		/// <summary>Converte um objeto especificado em um <see cref="T:System.Windows.Media.CacheMode" />.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="culture">Um <see cref="T:System.Globalization.CultureInfo" /> que contém informações sobre uma cultura específica.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.CacheMode" /> que é criado pela conversão de <paramref name="value" />; caso contrário, será gerada uma exceção.</returns>
		/// <exception cref="T:System.NotSupportedException">O <paramref name="value" /> é <see langword="null" /> ou não é um tipo que pode ser convertido em <see cref="T:System.Windows.Media.CacheMode" />.</exception>
		// Token: 0x060022E2 RID: 8930 RVA: 0x0008D094 File Offset: 0x0008C494
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return CacheMode.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converte um <see cref="T:System.Windows.Media.CacheMode" /> no tipo especificado.</summary>
		/// <param name="context">Um <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que fornece um contexto de formato.</param>
		/// <param name="culture">Um <see cref="T:System.Globalization.CultureInfo" /> que representa informações sobre uma cultura, como o idioma e sistema de calendário. Pode ser <see langword="null" />.</param>
		/// <param name="value">O <see cref="T:System.Object" /> a ser convertido.</param>
		/// <param name="destinationType">O <see cref="T:System.Type" /> para o qual converter.</param>
		/// <returns>O objeto convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" />, <paramref name="value" /> não é um <see cref="T:System.Windows.Media.CacheMode" /> ou <paramref name="destinationType" /> não é uma cadeia de caracteres.</exception>
		// Token: 0x060022E3 RID: 8931 RVA: 0x0008D0C8 File Offset: 0x0008C4C8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is CacheMode)
			{
				CacheMode cacheMode = (CacheMode)value;
				if (destinationType == typeof(string))
				{
					if (context != null && context.Instance != null && !cacheMode.CanSerializeToString())
					{
						throw new NotSupportedException(SR.Get("Converter_ConvertToNotSupported"));
					}
					return cacheMode.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
