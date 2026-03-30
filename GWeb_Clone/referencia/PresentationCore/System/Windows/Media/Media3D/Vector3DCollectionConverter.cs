using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Media3D
{
	/// <summary>Converte instâncias de outros tipos de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" />.</summary>
	// Token: 0x02000495 RID: 1173
	public sealed class Vector3DCollectionConverter : TypeConverter
	{
		/// <summary>Retorna um valor que indica se este conversor de tipo pode converter de um tipo especificado.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="sourceType">O tipo que está sendo consultado para suporte.</param>
		/// <returns>True se este conversor puder converter do tipo especificado; caso contrário, false.</returns>
		// Token: 0x06003432 RID: 13362 RVA: 0x000CF5B0 File Offset: 0x000CE9B0
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Obtém um valor que indica se este conversor de tipo pode converter no tipo fornecido.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="destinationType">O Tipo que está sendo consultado quanto ao suporte.</param>
		/// <returns>True se este conversor puder converter para o tipo fornecido; false se não puder.</returns>
		// Token: 0x06003433 RID: 13363 RVA: 0x000CF5DC File Offset: 0x000CE9DC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter em uma <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> do objeto especificado.</summary>
		/// <param name="context">O ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">O CultureInfo respeitado durante a conversão.</param>
		/// <param name="value">O objeto a ser convertido em uma instância de Vector3DCollection.</param>
		/// <returns>Vector3DCollection que foi construído.</returns>
		/// <exception cref="T:System.NotSupportedException">Uma NotSupportedException será gerada se o objeto de exemplo for nulo ou não for um tipo válido que possa ser convertido em uma Vector3DCollection.</exception>
		// Token: 0x06003434 RID: 13364 RVA: 0x000CF608 File Offset: 0x000CEA08
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Vector3DCollection.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.Media3D.Vector3DCollection" /> no tipo fornecido.</summary>
		/// <param name="context">ITypeDescriptorContext para esta chamada.</param>
		/// <param name="culture">O CultureInfo que é respeitado durante a conversão.</param>
		/// <param name="value">Objeto a ser convertido em uma instância de <paramref name="destinationType" />.</param>
		/// <param name="destinationType">O tipo para o qual a instância de Matrix3D será convertida.</param>
		/// <returns>O objeto que foi construído.</returns>
		/// <exception cref="T:System.NotSupportedException">Gerará a NotSupportedException se o objeto de exemplo for nulo ou não for uma Vector3DCollection ou se o destinationType não for um dos tipos de destino válidos.</exception>
		// Token: 0x06003435 RID: 13365 RVA: 0x000CF63C File Offset: 0x000CEA3C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Vector3DCollection)
			{
				Vector3DCollection vector3DCollection = (Vector3DCollection)value;
				if (destinationType == typeof(string))
				{
					return vector3DCollection.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
