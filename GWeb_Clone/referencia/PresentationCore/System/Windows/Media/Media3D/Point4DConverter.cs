using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte as instâncias de outros tipos de e para uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
	// Token: 0x0200048F RID: 1167
	public sealed class Point4DConverter : TypeConverter
	{
		/// <summary>Determina se uma classe pode ser convertida de um determinado tipo em uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>Indica se o tipo pode ser convertido em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não pode converter do tipo fornecido.  
		///
		///   true 
		///   O conversor pode converter o tipo fornecido em um <see cref="T:System.Windows.Media.Media3D.Point4D" />.</returns>
		// Token: 0x060033E5 RID: 13285 RVA: 0x000CE500 File Offset: 0x000CD900
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> está sendo avaliada.</param>
		/// <returns>Indica se esta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> pode ser convertida em <paramref name="destinationType" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não pode converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> no tipo especificado.  
		///
		///   true 
		///   O conversor pode converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> no tipo especificado.</returns>
		// Token: 0x060033E6 RID: 13286 RVA: 0x000CE52C File Offset: 0x000CD92C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> criada do <paramref name="value" /> convertido.</returns>
		// Token: 0x060033E7 RID: 13287 RVA: 0x000CE558 File Offset: 0x000CD958
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Point4D.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> em um tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">A estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo no qual esta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" /> será convertida.</param>
		/// <returns>O objeto criado da conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Point4D" />.</returns>
		// Token: 0x060033E8 RID: 13288 RVA: 0x000CE590 File Offset: 0x000CD990
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Point4D)
			{
				Point4D point4D = (Point4D)value;
				if (destinationType == typeof(string))
				{
					return point4D.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
