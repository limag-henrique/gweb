using System;
using System.Windows.Markup;

namespace System.Windows.Media.Media3D.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
	// Token: 0x0200049E RID: 1182
	public class Vector3DValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Media3D.Vector3D" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600345F RID: 13407 RVA: 0x000CFB38 File Offset: 0x000CEF38
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Media3D.Vector3D" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</exception>
		// Token: 0x06003460 RID: 13408 RVA: 0x000CFB48 File Offset: 0x000CEF48
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is Vector3D;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Media3D.Vector3D" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Media3D.Vector3D" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06003461 RID: 13409 RVA: 0x000CFB60 File Offset: 0x000CEF60
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Vector3D.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Media3D.Vector3D" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Vector3D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Media3D.Vector3D" /> fornecido.</returns>
		// Token: 0x06003462 RID: 13410 RVA: 0x000CFB84 File Offset: 0x000CEF84
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is Vector3D)
			{
				return ((Vector3D)value).ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
