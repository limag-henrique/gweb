using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Usado para converter um objeto <see cref="T:System.Windows.Media.Brush" /> em ou de outro tipo de objeto.</summary>
	// Token: 0x0200039F RID: 927
	public sealed class BrushConverter : TypeConverter
	{
		/// <summary>Determina se esta classe pode converter um objeto de um tipo específico para um objeto do <see cref="T:System.Windows.Media.Brush" />.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="sourceType">O tipo do qual converter.</param>
		/// <returns>Retorna <see langword="true" /> se a conversão for possível (o objeto é o tipo de cadeia de caracteres); caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022DB RID: 8923 RVA: 0x0008CE94 File Offset: 0x0008C294
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se esta classe pode converter um objeto de um tipo específico para o tipo de destino especificado.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="destinationType">O tipo de destino.</param>
		/// <returns>Retorna <see langword="true" /> se a conversão for possível; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060022DC RID: 8924 RVA: 0x0008CEC0 File Offset: 0x0008C2C0
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
			if (!(context.Instance is Brush))
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"Brush"
				}), "context");
			}
			Brush brush = (Brush)context.Instance;
			return brush.CanSerializeToString();
		}

		/// <summary>Converte de um objeto de um tipo específico para um objeto de <see cref="T:System.Windows.Media.Brush" />.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="culture">As informações de cultura que se aplicam à conversão.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <returns>Retorna um novo objeto de <see cref="T:System.Windows.Media.Brush" /> se for bem-sucedido; caso contrário, NULL.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é NULL ou não pode ser convertido para um <see cref="T:System.Windows.Media.Brush" />.</exception>
		// Token: 0x060022DD RID: 8925 RVA: 0x0008CF38 File Offset: 0x0008C338
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Brush.Parse(text, context);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converte um objeto de <see cref="T:System.Windows.Media.Brush" /> para um tipo especificado, usando as informações de contexto e de cultura especificadas.</summary>
		/// <param name="context">O contexto de conversão.</param>
		/// <param name="culture">As informações da cultura atual.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.Brush" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo de destino para o qual o objeto de <paramref name="value" /> é convertido.</param>
		/// <returns>Um objeto que representa o <paramref name="value" /> convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é NULL ou não é um <see cref="T:System.Windows.Media.Brush" />  
		///
		/// ou - 
		/// <paramref name="destinationType" /> não é um tipo de destino válido.</exception>
		// Token: 0x060022DE RID: 8926 RVA: 0x0008CF6C File Offset: 0x0008C36C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Brush)
			{
				Brush brush = (Brush)value;
				if (destinationType == typeof(string))
				{
					if (context != null && context.Instance != null && !brush.CanSerializeToString())
					{
						throw new NotSupportedException(SR.Get("Converter_ConvertToNotSupported"));
					}
					return brush.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
