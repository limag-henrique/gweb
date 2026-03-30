using System;
using System.Windows.Markup;

namespace System.Windows.Media.Media3D.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
	// Token: 0x02000499 RID: 1177
	public class Point4DValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Media3D.Point4D" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003446 RID: 13382 RVA: 0x000CF858 File Offset: 0x000CEC58
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Media3D.Point4D" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Media3D.Point4D" />.</exception>
		// Token: 0x06003447 RID: 13383 RVA: 0x000CF868 File Offset: 0x000CEC68
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is Point4D;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Media3D.Point4D" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Media3D.Point4D" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Media3D.Point4D" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06003448 RID: 13384 RVA: 0x000CF880 File Offset: 0x000CEC80
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Point4D.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Media3D.Point4D" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Point4D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Media3D.Point4D" /> fornecido.</returns>
		// Token: 0x06003449 RID: 13385 RVA: 0x000CF8A4 File Offset: 0x000CECA4
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is Point4D)
			{
				return ((Point4D)value).ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
