using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.FontStretch" /> para e de outras representações de tipo.</summary>
	// Token: 0x020001B7 RID: 439
	public sealed class FontStretchConverter : TypeConverter
	{
		/// <summary>Determina se é possível realizar a conversão de um tipo especificado em um valor <see cref="T:System.Windows.FontStretch" />.</summary>
		/// <param name="td">Informações de contexto de um tipo.</param>
		/// <param name="t">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="t" /> pode criar um <see cref="T:System.Windows.FontStretch" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006EA RID: 1770 RVA: 0x0001F694 File Offset: 0x0001EA94
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.FontStretch" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão dessa instância de <see cref="T:System.Windows.FontStretch" /> está sendo avaliada.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter <see cref="T:System.Windows.FontStretch" /> em <paramref name="destinationType" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001F6B8 File Offset: 0x0001EAB8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.FontStretch" />.</summary>
		/// <param name="td">Informações de contexto de um tipo.</param>
		/// <param name="ci">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <returns>A instância de <see cref="T:System.Windows.FontStretch" /> criada com base na <paramref name="value" /> convertida.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um tipo válido para conversão.</exception>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001F6F4 File Offset: 0x0001EAF4
		public override object ConvertFrom(ITypeDescriptorContext td, CultureInfo ci, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException(SR.Get("General_BadType", new object[]
				{
					"ConvertFrom"
				}), "value");
			}
			FontStretch fontStretch = default(FontStretch);
			if (!FontStretches.FontStretchStringToKnownStretch(text, ci, ref fontStretch))
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			return fontStretch;
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.FontStretch" /> em um tipo especificado.</summary>
		/// <param name="context">Informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">A instância de <see cref="T:System.Windows.FontStretch" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo para o qual esta instância de <see cref="T:System.Windows.FontStretch" /> é convertida.</param>
		/// <returns>O objeto criado com base na instância de <see cref="T:System.Windows.FontStretch" /> convertida.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" />  
		///
		/// ou - 
		/// <paramref name="value" /> não é uma instância de <see cref="T:System.Windows.FontStretch" />  
		///
		/// ou - 
		/// <paramref name="destinationType" /> não é um tipo de destino válido.</exception>
		// Token: 0x060006ED RID: 1773 RVA: 0x0001F764 File Offset: 0x0001EB64
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is FontStretch)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					MethodInfo method = typeof(FontStretch).GetMethod("FromOpenTypeStretch", new Type[]
					{
						typeof(int)
					});
					return new InstanceDescriptor(method, new object[]
					{
						((FontStretch)value).ToOpenTypeStretch()
					});
				}
				if (destinationType == typeof(string))
				{
					FontStretch fontStretch = (FontStretch)value;
					return ((IFormattable)fontStretch).ToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
