using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte as instâncias de outros tipos de e para uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
	// Token: 0x02000492 RID: 1170
	public sealed class Size3DConverter : TypeConverter
	{
		/// <summary>Determina se uma classe pode ser convertida de um determinado tipo em uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>Indica se o tipo pode ser convertido em uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não pode converter do tipo fornecido.  
		///
		///   true 
		///   O conversor pode converter o tipo fornecido em um <see cref="T:System.Windows.Media.Media3D.Size3D" />.</returns>
		// Token: 0x060033F4 RID: 13300 RVA: 0x000CE7DC File Offset: 0x000CDBDC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> está sendo avaliada.</param>
		/// <returns>Indica se esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> pode ser convertida em <paramref name="destinationType" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não pode converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> no tipo especificado.  
		///
		///   true 
		///   O conversor pode converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> no tipo especificado.</returns>
		// Token: 0x060033F5 RID: 13301 RVA: 0x000CE808 File Offset: 0x000CDC08
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> criada do <paramref name="value" /> convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">Ocorrerá se o objeto especificado for nulo ou for um tipo que não possa ser convertido em uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</exception>
		// Token: 0x060033F6 RID: 13302 RVA: 0x000CE834 File Offset: 0x000CDC34
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Size3D.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> em um tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">A estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo no qual esta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" /> será convertida.</param>
		/// <returns>O objeto criado da conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Size3D" />.</returns>
		// Token: 0x060033F7 RID: 13303 RVA: 0x000CE86C File Offset: 0x000CDC6C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Size3D)
			{
				Size3D size3D = (Size3D)value;
				if (destinationType == typeof(string))
				{
					return size3D.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
