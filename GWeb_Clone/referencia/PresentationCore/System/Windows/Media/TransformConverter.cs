using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte um objeto <see cref="T:System.Windows.Media.Transform" /> em outro tipo de objeto ou dele.</summary>
	// Token: 0x020003FA RID: 1018
	public sealed class TransformConverter : TypeConverter
	{
		/// <summary>Determina se essa classe pode converter um objeto de um tipo específico para um tipo <see cref="T:System.Windows.Media.Transform" />.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="sourceType">O tipo do qual converter.</param>
		/// <returns>
		///   <see langword="true" /> se a conversão for possível; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002872 RID: 10354 RVA: 0x000A2324 File Offset: 0x000A1724
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se essa classe pode converter um objeto de um tipo especificado para o tipo de destino especificado.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="destinationType">O tipo de destino.</param>
		/// <returns>
		///   <see langword="true" /> se a conversão for possível; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002873 RID: 10355 RVA: 0x000A2350 File Offset: 0x000A1750
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
			if (!(context.Instance is Transform))
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"Transform"
				}), "context");
			}
			Transform transform = (Transform)context.Instance;
			return transform.CanSerializeToString();
		}

		/// <summary>Converte de um objeto de um tipo especificado para um objeto <see cref="T:System.Windows.Media.Transform" />.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="culture">As informações de cultura que se aplicam à conversão.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <returns>Um novo objeto <see cref="T:System.Windows.Media.Transform" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não poderá ser convertido em um <see cref="T:System.Windows.Media.Transform" />.</exception>
		// Token: 0x06002874 RID: 10356 RVA: 0x000A23C8 File Offset: 0x000A17C8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Transform.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converte o <see cref="T:System.Windows.Media.Transform" /> especificado para o tipo especificado usando as informações de contexto e cultura especificadas.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="culture">As informações de cultura que se aplicam à conversão.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.Transform" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo de destino para o qual o objeto de <paramref name="value" /> é convertido.</param>
		/// <returns>Um objeto que representa o <paramref name="value" /> convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um <see cref="T:System.Windows.Media.Transform" />.  
		///
		/// ou - 
		/// <paramref name="destinationType" /> não é um tipo de destino válido.</exception>
		// Token: 0x06002875 RID: 10357 RVA: 0x000A23FC File Offset: 0x000A17FC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Transform)
			{
				Transform transform = (Transform)value;
				if (destinationType == typeof(string))
				{
					if (context != null && context.Instance != null && !transform.CanSerializeToString())
					{
						throw new NotSupportedException(SR.Get("Converter_ConvertToNotSupported"));
					}
					return transform.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
