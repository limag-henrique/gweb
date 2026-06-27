using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte instâncias de outros tipos de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
	// Token: 0x02000491 RID: 1169
	public sealed class Rect3DConverter : TypeConverter
	{
		/// <summary>Obtém um valor que indica se este conversor de tipo pode converter de um determinado tipo.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="sourceType">O tipo que está sendo consultado para suporte.</param>
		/// <returns>True se este conversor pode converter do tipo fornecido; caso contrário, false.</returns>
		// Token: 0x060033EF RID: 13295 RVA: 0x000CE6E8 File Offset: 0x000CDAE8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Obtém um valor que indica se este conversor de tipo pode converter no tipo fornecido.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="destinationType">O tipo que está sendo consultado para suporte.</param>
		/// <returns>True se este conversor pode converter para o tipo fornecido; caso contrário, false.</returns>
		// Token: 0x060033F0 RID: 13296 RVA: 0x000CE714 File Offset: 0x000CDB14
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converte o objeto especificado em um <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">CultureInfo a ser respeitada durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de Rect3D.</param>
		/// <returns>O Rect3D que foi construído.</returns>
		// Token: 0x060033F1 RID: 13297 RVA: 0x000CE740 File Offset: 0x000CDB40
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Rect3D.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> para o tipo fornecido.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de <paramref name="destinationType" />.</param>
		/// <param name="destinationType">O tipo para o qual a instância de Rect3D será convertida.</param>
		/// <returns>Objeto que foi construído.</returns>
		// Token: 0x060033F2 RID: 13298 RVA: 0x000CE778 File Offset: 0x000CDB78
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Rect3D)
			{
				Rect3D rect3D = (Rect3D)value;
				if (destinationType == typeof(string))
				{
					return rect3D.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
