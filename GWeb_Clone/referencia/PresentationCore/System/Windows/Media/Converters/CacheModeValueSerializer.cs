using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.CacheMode" />.</summary>
	// Token: 0x020005C4 RID: 1476
	public class CacheModeValueSerializer : ValueSerializer
	{
		/// <summary>Determina se o <see cref="T:System.String" /> especificado pode ser convertido em uma instância de <see cref="T:System.Windows.Media.CacheMode" />.</summary>
		/// <param name="value">Um <see cref="T:System.String" /> a ser avaliado para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004325 RID: 17189 RVA: 0x0010492C File Offset: 0x00103D2C
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se a instância de <see cref="T:System.Windows.Media.CacheMode" /> especificada pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Uma instância de <see cref="T:System.Windows.Media.CacheMode" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004326 RID: 17190 RVA: 0x0010493C File Offset: 0x00103D3C
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is CacheMode))
			{
				return false;
			}
			CacheMode cacheMode = (CacheMode)value;
			return cacheMode.CanSerializeToString();
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.CacheMode" />.</summary>
		/// <param name="value">Um valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.CacheMode" />.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.CacheMode" /> com base no <paramref name="value" /> especificado.</returns>
		// Token: 0x06004327 RID: 17191 RVA: 0x00104960 File Offset: 0x00103D60
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return CacheMode.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.CacheMode" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Uma instância de <see cref="T:System.Windows.Media.CacheMode" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto de <see cref="T:System.Windows.Media.CacheMode" /> especificado.</returns>
		// Token: 0x06004328 RID: 17192 RVA: 0x00104980 File Offset: 0x00103D80
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is CacheMode))
			{
				return base.ConvertToString(value, context);
			}
			CacheMode cacheMode = (CacheMode)value;
			if (!cacheMode.CanSerializeToString())
			{
				return base.ConvertToString(value, context);
			}
			return cacheMode.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
		}
	}
}
