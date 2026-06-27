using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias de outros tipos de e para um <see cref="T:System.Windows.Media.PointCollection" />.</summary>
	// Token: 0x020003CA RID: 970
	public sealed class PointCollectionConverter : TypeConverter
	{
		/// <summary>Determina se um objeto pode ser convertido de um tipo especificado em uma instância de um <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo puder ser convertido para um <see cref="T:System.Windows.Media.PointCollection" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002651 RID: 9809 RVA: 0x00098E0C File Offset: 0x0009820C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de um <see cref="T:System.Windows.Media.PointCollection" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo necessário para o qual você está avaliando este <see cref="T:System.Windows.Media.PointCollection" /> para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.PointCollection" /> puder ser convertido para <paramref name="destinationType" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002652 RID: 9810 RVA: 0x00098E38 File Offset: 0x00098238
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Media.PointCollection" />.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que deseja converter.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.PointCollection" /> criado na conversão de <paramref name="value" />.</returns>
		/// <exception cref="T:System.NotSupportedException">O objeto especificado é nulo ou é um tipo que não pode ser convertido em um <see cref="T:System.Windows.Media.PointCollection" />.</exception>
		// Token: 0x06002653 RID: 9811 RVA: 0x00098E64 File Offset: 0x00098264
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return PointCollection.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma <see cref="T:System.Windows.Media.PointCollection" /> em um tipo especificado.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que deseja converter.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.PointCollection" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo para o qual converter este <see cref="T:System.Windows.Media.PointCollection" />.</param>
		/// <returns>O objeto criado da conversão deste <see cref="T:System.Windows.Media.PointCollection" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é nulo ou não é um <see cref="T:System.Windows.Media.PointCollection" />; ou o <paramref name="destinationType" /> não é um dos tipos válidos para conversão.</exception>
		// Token: 0x06002654 RID: 9812 RVA: 0x00098E98 File Offset: 0x00098298
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is PointCollection)
			{
				PointCollection pointCollection = (PointCollection)value;
				if (destinationType == typeof(string))
				{
					return pointCollection.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
