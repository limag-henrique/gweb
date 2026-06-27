using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Int32Collection" />.</summary>
	// Token: 0x020005C7 RID: 1479
	public class Int32CollectionValueSerializer : ValueSerializer
	{
		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Int32Collection" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Int32Collection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Int32Collection" /> fornecido.</returns>
		// Token: 0x06004334 RID: 17204 RVA: 0x00104B14 File Offset: 0x00103F14
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Int32Collection" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Int32Collection" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Int32Collection" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06004335 RID: 17205 RVA: 0x00104B24 File Offset: 0x00103F24
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is Int32Collection;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Int32Collection" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Int32Collection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Int32Collection" />.</exception>
		// Token: 0x06004336 RID: 17206 RVA: 0x00104B3C File Offset: 0x00103F3C
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Int32Collection.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Int32Collection" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004337 RID: 17207 RVA: 0x00104B5C File Offset: 0x00103F5C
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is Int32Collection)
			{
				Int32Collection int32Collection = (Int32Collection)value;
				return int32Collection.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
