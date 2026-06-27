using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
	// Token: 0x020005CB RID: 1483
	public class VectorCollectionValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.VectorCollection" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004348 RID: 17224 RVA: 0x00104D8C File Offset: 0x0010418C
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.VectorCollection" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.VectorCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.VectorCollection" />.</exception>
		// Token: 0x06004349 RID: 17225 RVA: 0x00104D9C File Offset: 0x0010419C
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is VectorCollection;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.VectorCollection" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x0600434A RID: 17226 RVA: 0x00104DB4 File Offset: 0x001041B4
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return VectorCollection.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.VectorCollection" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.VectorCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.VectorCollection" /> fornecido.</returns>
		// Token: 0x0600434B RID: 17227 RVA: 0x00104DD4 File Offset: 0x001041D4
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is VectorCollection)
			{
				VectorCollection vectorCollection = (VectorCollection)value;
				return vectorCollection.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
