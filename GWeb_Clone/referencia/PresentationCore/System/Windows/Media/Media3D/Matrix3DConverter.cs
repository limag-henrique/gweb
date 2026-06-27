using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte instâncias de outros tipos de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
	// Token: 0x0200048B RID: 1163
	public sealed class Matrix3DConverter : TypeConverter
	{
		/// <summary>Retorna um valor que indica se este conversor de tipo pode converter de um tipo especificado.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="sourceType">O tipo que está sendo consultado para suporte.</param>
		/// <returns>True se este conversor puder converter do tipo especificado; caso contrário, false.</returns>
		// Token: 0x060033A2 RID: 13218 RVA: 0x000CD63C File Offset: 0x000CCA3C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Obtém um valor que indica se este conversor de tipo pode converter no tipo fornecido.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="destinationType">O Tipo que está sendo consultado quanto ao suporte.</param>
		/// <returns>True se este conversor puder converter para o tipo fornecido; false se não puder.</returns>
		// Token: 0x060033A3 RID: 13219 RVA: 0x000CD668 File Offset: 0x000CCA68
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter em uma Matrix3D do objeto fornecido.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de Matrix3D.</param>
		/// <returns>A Matrix3D que foi construída.</returns>
		/// <exception cref="T:System.NotSupportedException">Uma NotSupportedException será gerada se o objeto de exemplo for nulo ou não for um tipo válido que possa ser convertido em uma Matrix3D.</exception>
		// Token: 0x060033A4 RID: 13220 RVA: 0x000CD694 File Offset: 0x000CCA94
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Matrix3D.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> para o tipo fornecido.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de <paramref name="destinationType" />.</param>
		/// <param name="destinationType">O tipo para o qual a instância de Matrix3D será convertida.</param>
		/// <returns>Objeto que foi construído.</returns>
		/// <exception cref="T:System.NotSupportedException">Gerará a NotSupportedException se o objeto de exemplo for nulo ou não for uma Matrix3D, ou se o destinationType não for um dos tipos de destino válidos.</exception>
		// Token: 0x060033A5 RID: 13221 RVA: 0x000CD6CC File Offset: 0x000CCACC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Matrix3D)
			{
				Matrix3D matrix3D = (Matrix3D)value;
				if (destinationType == typeof(string))
				{
					return matrix3D.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
