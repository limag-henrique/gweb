using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.FontWeight" /> de e para outros tipos de dados.</summary>
	// Token: 0x020001BA RID: 442
	public sealed class FontWeightConverter : TypeConverter
	{
		/// <summary>Retorna um valor que indica se esse conversor pode converter um objeto do tipo especificado em uma instância de <see cref="T:System.Windows.FontWeight" />.</summary>
		/// <param name="td">Informações de contexto de um tipo.</param>
		/// <param name="t">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter o tipo fornecido em uma instância de <see cref="T:System.Windows.FontWeight" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600070C RID: 1804 RVA: 0x0001FD0C File Offset: 0x0001F10C
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.FontWeight" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão dessa instância de <see cref="T:System.Windows.FontWeight" /> está sendo avaliada.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter esta instância de <see cref="T:System.Windows.FontWeight" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600070D RID: 1805 RVA: 0x0001FD30 File Offset: 0x0001F130
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.FontWeight" />.</summary>
		/// <param name="td">Informações de contexto de um tipo.</param>
		/// <param name="ci">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <returns>A instância de <see cref="T:System.Windows.FontWeight" /> criada com base na <paramref name="value" /> convertida.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um tipo válido para conversão.</exception>
		// Token: 0x0600070E RID: 1806 RVA: 0x0001FD6C File Offset: 0x0001F16C
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
			FontWeight fontWeight = default(FontWeight);
			if (!FontWeights.FontWeightStringToKnownWeight(text, ci, ref fontWeight))
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			return fontWeight;
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.FontWeight" /> em um tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">A instância de <see cref="T:System.Windows.FontWeight" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo para o qual esta instância de <see cref="T:System.Windows.FontWeight" /> é convertida.</param>
		/// <returns>O objeto criado com base na instância de <see cref="T:System.Windows.FontWeight" /> convertida.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" />  
		///
		/// ou - 
		/// <paramref name="value" /> i não é uma instância de <see cref="T:System.Windows.FontWeight" />  
		///
		/// ou - 
		/// <paramref name="destinationType" /> não é um tipo de destino válido.</exception>
		// Token: 0x0600070F RID: 1807 RVA: 0x0001FDDC File Offset: 0x0001F1DC
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is FontWeight)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					MethodInfo method = typeof(FontWeight).GetMethod("FromOpenTypeWeight", new Type[]
					{
						typeof(int)
					});
					return new InstanceDescriptor(method, new object[]
					{
						((FontWeight)value).ToOpenTypeWeight()
					});
				}
				if (destinationType == typeof(string))
				{
					FontWeight fontWeight = (FontWeight)value;
					return ((IFormattable)fontWeight).ToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
