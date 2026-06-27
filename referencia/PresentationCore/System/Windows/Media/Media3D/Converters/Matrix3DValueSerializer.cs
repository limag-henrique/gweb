using System;
using System.Windows.Markup;

namespace System.Windows.Media.Media3D.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
	// Token: 0x02000496 RID: 1174
	public class Matrix3DValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003437 RID: 13367 RVA: 0x000CF6A0 File Offset: 0x000CEAA0
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</exception>
		// Token: 0x06003438 RID: 13368 RVA: 0x000CF6B0 File Offset: 0x000CEAB0
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is Matrix3D;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Media3D.Matrix3D" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06003439 RID: 13369 RVA: 0x000CF6C8 File Offset: 0x000CEAC8
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Matrix3D.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Media3D.Matrix3D" /> fornecido.</returns>
		// Token: 0x0600343A RID: 13370 RVA: 0x000CF6EC File Offset: 0x000CEAEC
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is Matrix3D)
			{
				return ((Matrix3D)value).ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
