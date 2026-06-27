using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias de outros tipos de e para instâncias de <see cref="T:System.Windows.Media.Geometry" />.</summary>
	// Token: 0x020003AE RID: 942
	public sealed class GeometryConverter : TypeConverter
	{
		/// <summary>Indica se um objeto pode ser convertido de um determinado tipo em uma instância de um <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="context">Informações de contexto necessárias para conversão.</param>
		/// <param name="sourceType">O <see cref="T:System.Type" /> de origem que está sendo consultado quanto a suporte para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se for possível converter o tipo especificado em um <see cref="T:System.Windows.Media.Geometry" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060023BF RID: 9151 RVA: 0x0008FEF4 File Offset: 0x0008F2F4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se é possível converter instâncias de <see cref="T:System.Windows.Media.Geometry" /> para o tipo especificado.</summary>
		/// <param name="context">Informações de contexto necessárias para conversão.</param>
		/// <param name="destinationType">O tipo desejado para o qual se está avaliando converter este <see cref="T:System.Windows.Media.Geometry" />.</param>
		/// <returns>
		///   <see langword="true" /> se este instâncias de <see cref="T:System.Windows.Media.Geometry" /> puderem ser convertidas para <paramref name="destinationType" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060023C0 RID: 9152 RVA: 0x0008FF20 File Offset: 0x0008F320
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
			if (!(context.Instance is Geometry))
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"Geometry"
				}), "context");
			}
			Geometry geometry = (Geometry)context.Instance;
			return geometry.CanSerializeToString();
		}

		/// <summary>Converte o objeto especificado em um <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="context">Informações de contexto necessárias para conversão.</param>
		/// <param name="culture">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Geometry" /> criado da conversão de <paramref name="value" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Gerada se <paramref name="value" /> é <see langword="null" /> ou não é um tipo válido que pode ser convertido em um <see cref="T:System.Windows.Media.Geometry" />.</exception>
		// Token: 0x060023C1 RID: 9153 RVA: 0x0008FF98 File Offset: 0x0008F398
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Geometry.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converte o <see cref="T:System.Windows.Media.Geometry" /> especificado no tipo especificado.</summary>
		/// <param name="context">Informações de contexto necessárias para conversão.</param>
		/// <param name="culture">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.Geometry" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo no qual converter <see cref="T:System.Windows.Media.Geometry" />.</param>
		/// <returns>O objeto criado da conversão deste <see cref="T:System.Windows.Media.Geometry" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Gerada se <paramref name="value" /> é <see langword="null" /> ou não é um <see cref="T:System.Windows.Media.Geometry" />, ou então se o <paramref name="destinationType" /> não pode ser convertido em um <see cref="T:System.Windows.Media.Geometry" />.</exception>
		// Token: 0x060023C2 RID: 9154 RVA: 0x0008FFCC File Offset: 0x0008F3CC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Geometry)
			{
				Geometry geometry = (Geometry)value;
				if (destinationType == typeof(string))
				{
					if (context != null && context.Instance != null && !geometry.CanSerializeToString())
					{
						throw new NotSupportedException(SR.Get("Converter_ConvertToNotSupported"));
					}
					return geometry.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
