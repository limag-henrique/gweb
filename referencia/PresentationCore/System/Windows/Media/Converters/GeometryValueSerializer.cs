using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Geometry" />.</summary>
	// Token: 0x020005C6 RID: 1478
	public class GeometryValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Geometry" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600432F RID: 17199 RVA: 0x00104A68 File Offset: 0x00103E68
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Geometry" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Geometry" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Geometry" />.</exception>
		// Token: 0x06004330 RID: 17200 RVA: 0x00104A78 File Offset: 0x00103E78
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is Geometry))
			{
				return false;
			}
			Geometry geometry = (Geometry)value;
			return geometry.CanSerializeToString();
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Geometry" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Geometry" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Geometry" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06004331 RID: 17201 RVA: 0x00104A9C File Offset: 0x00103E9C
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Geometry.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Geometry" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Geometry" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Geometry" /> fornecido.</returns>
		// Token: 0x06004332 RID: 17202 RVA: 0x00104ABC File Offset: 0x00103EBC
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is Geometry))
			{
				return base.ConvertToString(value, context);
			}
			Geometry geometry = (Geometry)value;
			if (!geometry.CanSerializeToString())
			{
				return base.ConvertToString(value, context);
			}
			return geometry.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
		}
	}
}
