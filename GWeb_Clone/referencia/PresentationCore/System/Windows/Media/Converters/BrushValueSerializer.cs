using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Brush" />.</summary>
	// Token: 0x020005C3 RID: 1475
	public class BrushValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Brush" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004320 RID: 17184 RVA: 0x00104880 File Offset: 0x00103C80
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Brush" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Brush" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Brush" />.</exception>
		// Token: 0x06004321 RID: 17185 RVA: 0x00104890 File Offset: 0x00103C90
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is Brush))
			{
				return false;
			}
			Brush brush = (Brush)value;
			return brush.CanSerializeToString();
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Brush" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Brush" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Brush" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06004322 RID: 17186 RVA: 0x001048B4 File Offset: 0x00103CB4
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Brush.Parse(value, context);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Brush" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Brush" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Brush" /> fornecido.</returns>
		// Token: 0x06004323 RID: 17187 RVA: 0x001048D4 File Offset: 0x00103CD4
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is Brush))
			{
				return base.ConvertToString(value, context);
			}
			Brush brush = (Brush)value;
			if (!brush.CanSerializeToString())
			{
				return base.ConvertToString(value, context);
			}
			return brush.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
		}
	}
}
