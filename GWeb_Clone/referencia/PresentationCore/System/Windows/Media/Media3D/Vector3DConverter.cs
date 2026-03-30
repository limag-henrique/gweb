using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte as instâncias de outros tipos de e para uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
	// Token: 0x02000494 RID: 1172
	public sealed class Vector3DConverter : TypeConverter
	{
		/// <summary>Determina se uma classe pode ser convertida de um determinado tipo em uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>Indica se o tipo pode ser convertido em uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não pode converter do tipo fornecido.  
		///
		///   true 
		///   O conversor pode converter o tipo fornecido em um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</returns>
		// Token: 0x0600342D RID: 13357 RVA: 0x000CF4BC File Offset: 0x000CE8BC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> está sendo avaliada.</param>
		/// <returns>Indica se esta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> pode ser convertida em <paramref name="destinationType" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não pode converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> no tipo especificado.  
		///
		///   true 
		///   O conversor pode converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> no tipo especificado.</returns>
		// Token: 0x0600342E RID: 13358 RVA: 0x000CF4E8 File Offset: 0x000CE8E8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> criada do <paramref name="value" /> convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">Ocorrerá se o objeto especificado for nulo ou for um tipo que não possa ser convertido em uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</exception>
		// Token: 0x0600342F RID: 13359 RVA: 0x000CF514 File Offset: 0x000CE914
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Vector3D.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> em um tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">A estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo no qual esta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" /> será convertida.</param>
		/// <returns>O objeto criado da conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</returns>
		// Token: 0x06003430 RID: 13360 RVA: 0x000CF54C File Offset: 0x000CE94C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Vector3D)
			{
				Vector3D vector3D = (Vector3D)value;
				if (destinationType == typeof(string))
				{
					return vector3D.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
