using System;
using System.Windows.Markup;

namespace System.Windows.Media.Media3D.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
	// Token: 0x02000497 RID: 1175
	public class Point3DCollectionValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600343C RID: 13372 RVA: 0x000CF734 File Offset: 0x000CEB34
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</exception>
		// Token: 0x0600343D RID: 13373 RVA: 0x000CF744 File Offset: 0x000CEB44
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is Point3DCollection;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Media3D.Point3DCollection" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x0600343E RID: 13374 RVA: 0x000CF75C File Offset: 0x000CEB5C
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Point3DCollection.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Media3D.Point3DCollection" /> fornecido.</returns>
		// Token: 0x0600343F RID: 13375 RVA: 0x000CF77C File Offset: 0x000CEB7C
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is Point3DCollection)
			{
				Point3DCollection point3DCollection = (Point3DCollection)value;
				return point3DCollection.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
