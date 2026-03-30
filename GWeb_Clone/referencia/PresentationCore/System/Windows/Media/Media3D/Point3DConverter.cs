using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte as instâncias de outros tipos de e para uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
	// Token: 0x0200048D RID: 1165
	public sealed class Point3DConverter : TypeConverter
	{
		/// <summary>Determina se uma classe pode ser convertida de um determinado tipo em uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>Indica se o tipo pode ser convertido em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não consegue fazer a conversão do tipo fornecido.  
		///
		///   true 
		///   O conversor consegue fazer a conversão do tipo fornecido para <see cref="T:System.Windows.Media.Media3D.Point3D" />.</returns>
		// Token: 0x060033DB RID: 13275 RVA: 0x000CE31C File Offset: 0x000CD71C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual a conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> está sendo avaliada.</param>
		/// <returns>Indica se esta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> pode ser convertida em <paramref name="destinationType" />.  
		///   Valor 
		///   Significado 
		///   false 
		///   O conversor não consegue converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> para o tipo especificado.  
		///
		///   true 
		///   O conversor consegue converter esta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> para o tipo especificado.</returns>
		// Token: 0x060033DC RID: 13276 RVA: 0x000CE348 File Offset: 0x000CD748
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> criada do <paramref name="value" /> convertido.</returns>
		// Token: 0x060033DD RID: 13277 RVA: 0x000CE374 File Offset: 0x000CD774
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Point3D.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> em um tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">A estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> a ser convertida.</param>
		/// <param name="destinationType">O tipo no qual esta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" /> será convertida.</param>
		/// <returns>O objeto criado da conversão desta estrutura <see cref="T:System.Windows.Media.Media3D.Point3D" />.</returns>
		// Token: 0x060033DE RID: 13278 RVA: 0x000CE3AC File Offset: 0x000CD7AC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Point3D)
			{
				Point3D point3D = (Point3D)value;
				if (destinationType == typeof(string))
				{
					return point3D.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
