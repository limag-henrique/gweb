using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte a instâncias de outros tipos de e para instâncias <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
	// Token: 0x0200048E RID: 1166
	public sealed class Point3DCollectionConverter : TypeConverter
	{
		/// <summary>Retorna um valor que indica se o conversor de tipo pode fazer a conversão de um tipo especificado.</summary>
		/// <param name="context">O contexto do descritor de tipo para esta chamada.</param>
		/// <param name="sourceType">O tipo que está sendo consultado para obter suporte.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor pode fazer a conversão do tipo fornecido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060033E0 RID: 13280 RVA: 0x000CE410 File Offset: 0x000CD810
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Retorna um valor que indica se o conversor de tipo pode fazer a conversão no tipo especificado.</summary>
		/// <param name="context">O contexto do descritor de tipo para esta chamada.</param>
		/// <param name="destinationType">O tipo que está sendo consultado para obter suporte.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor pode fazer a conversão no tipo fornecido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060033E1 RID: 13281 RVA: 0x000CE43C File Offset: 0x000CD83C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter em uma <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> do objeto especificado.</summary>
		/// <param name="context">O contexto do descritor de tipo para esta chamada.</param>
		/// <param name="culture">As informações de cultura respeitadas durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> que foi construído.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um tipo válido que pode ser convertido em um <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</exception>
		// Token: 0x060033E2 RID: 13282 RVA: 0x000CE468 File Offset: 0x000CD868
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Point3DCollection.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> no tipo especificado</summary>
		/// <param name="context">O contexto do descritor de tipo para esta chamada.</param>
		/// <param name="culture">As informações de cultura respeitadas durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de <paramref name="destinationType" />.</param>
		/// <param name="destinationType">Tipo no qual este método converterá a instância <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		/// <returns>O objeto que foi construído.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.  
		///
		/// ou - 
		/// <paramref name="destinationType" /> não é um dos tipos de destino válidos.</exception>
		// Token: 0x060033E3 RID: 13283 RVA: 0x000CE49C File Offset: 0x000CD89C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Point3DCollection)
			{
				Point3DCollection point3DCollection = (Point3DCollection)value;
				if (destinationType == typeof(string))
				{
					return point3DCollection.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
