using System;
using System.Windows.Markup;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.FontFamily" />.</summary>
	// Token: 0x02000398 RID: 920
	public class FontFamilyValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.FontFamily" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600226C RID: 8812 RVA: 0x0008AE60 File Offset: 0x0008A260
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.FontFamily" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.FontFamily" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.FontFamily" /> com base no <paramref name="value" /> fornecido.</returns>
		/// <exception cref="T:System.NotSupportedException">Ocorre quando <paramref name="value" /> é <see langword="null" /> ou é igual a <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x0600226D RID: 8813 RVA: 0x0008AE70 File Offset: 0x0008A270
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw base.GetConvertFromException(value);
			}
			return new FontFamily(value);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.FontFamily" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.FontFamily" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600226E RID: 8814 RVA: 0x0008AE94 File Offset: 0x0008A294
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			FontFamily fontFamily = value as FontFamily;
			return fontFamily != null && fontFamily.Source != null && fontFamily.Source.Length != 0;
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.FontFamily" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.FontFamily" /> ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.FontFamily" /> fornecido.</returns>
		/// <exception cref="T:System.NotSupportedException">Ocorre quando <paramref name="value" /> é <see langword="null" /> ou é igual a <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x0600226F RID: 8815 RVA: 0x0008AEC4 File Offset: 0x0008A2C4
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			FontFamily fontFamily = value as FontFamily;
			if (fontFamily == null || fontFamily.Source == null)
			{
				throw base.GetConvertToException(value, typeof(string));
			}
			return fontFamily.Source;
		}
	}
}
