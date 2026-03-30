using System;
using System.Windows.Markup;

namespace System.Windows.Media.Media3D.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
	// Token: 0x0200049B RID: 1179
	public class Rect3DValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06003450 RID: 13392 RVA: 0x000CF980 File Offset: 0x000CED80
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</exception>
		// Token: 0x06003451 RID: 13393 RVA: 0x000CF990 File Offset: 0x000CED90
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is Rect3D;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Media3D.Rect3D" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06003452 RID: 13394 RVA: 0x000CF9A8 File Offset: 0x000CEDA8
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Rect3D.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Media3D.Rect3D" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Media3D.Rect3D" /> fornecido.</returns>
		// Token: 0x06003453 RID: 13395 RVA: 0x000CF9CC File Offset: 0x000CEDCC
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is Rect3D)
			{
				return ((Rect3D)value).ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
