using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
	// Token: 0x020005C5 RID: 1477
	public class DoubleCollectionValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.DoubleCollection" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600432A RID: 17194 RVA: 0x001049D8 File Offset: 0x00103DD8
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.DoubleCollection" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.DoubleCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.DoubleCollection" />.</exception>
		// Token: 0x0600432B RID: 17195 RVA: 0x001049E8 File Offset: 0x00103DE8
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is DoubleCollection;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.DoubleCollection" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x0600432C RID: 17196 RVA: 0x00104A00 File Offset: 0x00103E00
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return DoubleCollection.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.DoubleCollection" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.DoubleCollection" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.DoubleCollection" /> fornecido.</returns>
		// Token: 0x0600432D RID: 17197 RVA: 0x00104A20 File Offset: 0x00103E20
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (value is DoubleCollection)
			{
				DoubleCollection doubleCollection = (DoubleCollection)value;
				return doubleCollection.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
