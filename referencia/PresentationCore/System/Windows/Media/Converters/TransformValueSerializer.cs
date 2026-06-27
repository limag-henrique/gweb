using System;
using System.Windows.Markup;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Media.Transform" />.</summary>
	// Token: 0x020005CA RID: 1482
	public class TransformValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a conversão de um determinado <see cref="T:System.String" /> em uma instância de <see cref="T:System.Windows.Media.Transform" /> é possível.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o valor puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06004343 RID: 17219 RVA: 0x00104CE0 File Offset: 0x001040E0
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.Transform" /> pode ser convertida em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Transform" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorre quando <paramref name="value" /> não é um <see cref="T:System.Windows.Media.Transform" />.</exception>
		// Token: 0x06004344 RID: 17220 RVA: 0x00104CF0 File Offset: 0x001040F0
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is Transform))
			{
				return false;
			}
			Transform transform = (Transform)value;
			return transform.CanSerializeToString();
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Media.Transform" />.</summary>
		/// <param name="value">O valor de <see cref="T:System.String" /> a ser convertido em um <see cref="T:System.Windows.Media.Transform" />.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Transform" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06004345 RID: 17221 RVA: 0x00104D14 File Offset: 0x00104114
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			if (value != null)
			{
				return Transform.Parse(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Media.Transform" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">Instância de <see cref="T:System.Windows.Media.Transform" /> a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <returns>Uma representação de <see cref="T:System.String" /> do objeto <see cref="T:System.Windows.Media.Transform" /> fornecido.</returns>
		// Token: 0x06004346 RID: 17222 RVA: 0x00104D34 File Offset: 0x00104134
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			if (!(value is Transform))
			{
				return base.ConvertToString(value, context);
			}
			Transform transform = (Transform)value;
			if (!transform.CanSerializeToString())
			{
				return base.ConvertToString(value, context);
			}
			return transform.ConvertToString(null, TypeConverterHelper.InvariantEnglishUS);
		}
	}
}
