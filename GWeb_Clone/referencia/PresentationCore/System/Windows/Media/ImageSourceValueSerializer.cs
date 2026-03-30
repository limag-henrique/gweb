using System;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.ImageSource" />.</summary>
	// Token: 0x02000419 RID: 1049
	public class ImageSourceValueSerializer : ValueSerializer
	{
		/// <summary>Determina se um <see cref="T:System.String" /> pode ser convertido em uma instância de <see cref="T:System.Windows.Media.ImageSource" />.</summary>
		/// <param name="value">
		///   <see cref="T:System.String" /> a ser avaliado para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002A31 RID: 10801 RVA: 0x000A952C File Offset: 0x000A892C
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.ImageSource" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.ImageSource" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002A32 RID: 10802 RVA: 0x000A953C File Offset: 0x000A893C
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			ImageSource imageSource = value as ImageSource;
			return imageSource != null && imageSource.CanSerializeToString();
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.ImageSource" />.</summary>
		/// <param name="value">Um valor de cadeia de caracteres a ser convertido em um <see cref="T:System.Windows.Media.ImageSource" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.ImageSource" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06002A33 RID: 10803 RVA: 0x000A955C File Offset: 0x000A895C
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (!string.IsNullOrEmpty(value))
			{
				UriHolder uriFromUriContext = TypeConverterHelper.GetUriFromUriContext(context, value);
				return BitmapFrame.CreateFromUriOrStream(uriFromUriContext.BaseUri, uriFromUriContext.OriginalUri, null, BitmapCreateOptions.None, BitmapCacheOption.Default, null);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.ImageSource" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.ImageSource" /> a ser avaliado para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.ImageSource" /> fornecido.</returns>
		// Token: 0x06002A34 RID: 10804 RVA: 0x000A9598 File Offset: 0x000A8998
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			ImageSource imageSource = value as ImageSource;
			if (imageSource != null)
			{
				return imageSource.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
			}
			return base.ConvertToString(value, context);
		}
	}
}
