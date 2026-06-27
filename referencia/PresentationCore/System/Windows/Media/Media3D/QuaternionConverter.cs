using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte instâncias de outros tipos de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</summary>
	// Token: 0x02000490 RID: 1168
	public sealed class QuaternionConverter : TypeConverter
	{
		/// <summary>Obtém um valor que indica se este conversor de tipo pode converter de um determinado tipo.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="sourceType">O tipo que está sendo consultado para suporte.</param>
		/// <returns>True se este conversor pode converter do tipo fornecido; caso contrário, false.</returns>
		// Token: 0x060033EA RID: 13290 RVA: 0x000CE5F4 File Offset: 0x000CD9F4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Obtém um valor que indica se este conversor de tipo pode converter no tipo fornecido.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="destinationType">O tipo que está sendo consultado para suporte.</param>
		/// <returns>True se este conversor pode converter para o tipo fornecido; caso contrário, false.</returns>
		// Token: 0x060033EB RID: 13291 RVA: 0x000CE620 File Offset: 0x000CDA20
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converte o objeto especificado em um <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">CultureInfo a ser respeitada durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância do Quaternion.</param>
		/// <returns>Quaternion que foi construído.</returns>
		// Token: 0x060033EC RID: 13292 RVA: 0x000CE64C File Offset: 0x000CDA4C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Quaternion.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.Media3D.Quaternion" /> para o tipo fornecido.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de <paramref name="destinationType" />.</param>
		/// <param name="destinationType">O tipo para o qual a instância de Matrix3D será convertida.</param>
		/// <returns>Objeto que foi construído.</returns>
		// Token: 0x060033ED RID: 13293 RVA: 0x000CE684 File Offset: 0x000CDA84
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Quaternion)
			{
				Quaternion quaternion = (Quaternion)value;
				if (destinationType == typeof(string))
				{
					return quaternion.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
