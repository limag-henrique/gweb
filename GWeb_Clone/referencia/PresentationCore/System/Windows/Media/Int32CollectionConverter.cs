using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media
{
	/// <summary>Converte uma <see cref="T:System.Windows.Media.Int32Collection" /> de e para outros tipos de dados.</summary>
	// Token: 0x020003BA RID: 954
	public sealed class Int32CollectionConverter : TypeConverter
	{
		/// <summary>Determina se o conversor pode converter um objeto do tipo determinado em uma instância de <see cref="T:System.Windows.Media.Int32Collection" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter o tipo fornecido em uma instância de <see cref="T:System.Windows.Media.Int32Collection" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060024C4 RID: 9412 RVA: 0x000937A8 File Offset: 0x00092BA8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se o conversor pode converter um <see cref="T:System.Windows.Media.Int32Collection" /> em um determinado tipo de dados.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual avaliar a conversão.</param>
		/// <returns>
		///   <see langword="true" /> se um <see cref="T:System.Windows.Media.Int32Collection" /> puder ser convertido em <paramref name="destinationType" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060024C5 RID: 9413 RVA: 0x000937D4 File Offset: 0x00092BD4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter um objeto especificado em uma instância <see cref="T:System.Windows.Media.Int32Collection" />.</summary>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <param name="culture">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Int32Collection" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou o tipo inválido.</exception>
		// Token: 0x060024C6 RID: 9414 RVA: 0x00093800 File Offset: 0x00092C00
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return Int32Collection.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.Int32Collection" /> em um tipo especificado.</summary>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <param name="culture">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">
		///   <see cref="T:System.Windows.Media.Int32Collection" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>Uma nova instância do <paramref name="destinationType" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou o tipo inválido.</exception>
		// Token: 0x060024C7 RID: 9415 RVA: 0x00093834 File Offset: 0x00092C34
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Int32Collection)
			{
				Int32Collection int32Collection = (Int32Collection)value;
				if (destinationType == typeof(string))
				{
					return int32Collection.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
