using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.FontStyle" /> de e para outros tipos de dados.</summary>
	// Token: 0x020001B4 RID: 436
	public sealed class FontStyleConverter : TypeConverter
	{
		/// <summary>Retorna um valor que indica se esse conversor pode converter um objeto do tipo especificado em uma instância de <see cref="T:System.Windows.FontStyle" />.</summary>
		/// <param name="td">Descreve as informações de contexto de um tipo.</param>
		/// <param name="t">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter o tipo fornecido em uma instância de <see cref="T:System.Windows.FontStyle" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001F270 File Offset: 0x0001E670
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.FontStyle" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão dessa instância de <see cref="T:System.Windows.FontStyle" /> está sendo avaliada.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter esta instância de <see cref="T:System.Windows.FontStyle" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060006D1 RID: 1745 RVA: 0x0001F294 File Offset: 0x0001E694
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.FontStyle" />.</summary>
		/// <param name="td">Informações de contexto de um tipo.</param>
		/// <param name="ci">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <returns>A instância de <see cref="T:System.Windows.FontStyle" /> criada com base na <paramref name="value" /> convertida.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um tipo válido para conversão.</exception>
		// Token: 0x060006D2 RID: 1746 RVA: 0x0001F2D0 File Offset: 0x0001E6D0
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
			FontStyle fontStyle = default(FontStyle);
			if (!FontStyles.FontStyleStringToKnownStyle(text, ci, ref fontStyle))
			{
				throw new FormatException(SR.Get("Parsers_IllegalToken"));
			}
			return fontStyle;
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.FontStyle" /> em um tipo especificado.</summary>
		/// <param name="context">Informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">A instância de <see cref="T:System.Windows.FontStyle" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo para o qual esta instância de <see cref="T:System.Windows.FontStyle" /> é convertida.</param>
		/// <returns>O objeto criado com base na instância de <see cref="T:System.Windows.FontStyle" /> convertida.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" />  
		///
		/// ou - 
		/// <paramref name="value" /> não é uma instância de <see cref="T:System.Windows.FontStyle" />  
		///
		/// ou - 
		/// <paramref name="destinationType" /> não é um tipo de destino válido.</exception>
		// Token: 0x060006D3 RID: 1747 RVA: 0x0001F340 File Offset: 0x0001E740
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is FontStyle)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(FontStyle).GetConstructor(new Type[]
					{
						typeof(int)
					});
					int styleForInternalConstruction = ((FontStyle)value).GetStyleForInternalConstruction();
					return new InstanceDescriptor(constructor, new object[]
					{
						styleForInternalConstruction
					});
				}
				if (destinationType == typeof(string))
				{
					FontStyle fontStyle = (FontStyle)value;
					return ((IFormattable)fontStyle).ToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
