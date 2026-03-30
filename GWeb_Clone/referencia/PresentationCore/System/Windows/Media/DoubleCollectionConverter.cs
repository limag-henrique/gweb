using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias de outros tipos de e para um <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
	// Token: 0x020003A6 RID: 934
	public sealed class DoubleCollectionConverter : TypeConverter
	{
		/// <summary>Determina se um objeto pode ser convertido de um tipo especificado em uma instância de um <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo puder ser convertido para um <see cref="T:System.Windows.Media.DoubleCollection" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002315 RID: 8981 RVA: 0x0008DAA8 File Offset: 0x0008CEA8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de um <see cref="T:System.Windows.Media.DoubleCollection" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo necessário para o qual você está avaliando este <see cref="T:System.Windows.Media.DoubleCollection" /> para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.DoubleCollection" /> puder ser convertido para <paramref name="destinationType" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002316 RID: 8982 RVA: 0x0008DAD4 File Offset: 0x0008CED4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Media.DoubleCollection" />.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que deseja converter.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.DoubleCollection" /> criado na conversão de <paramref name="value" />.</returns>
		// Token: 0x06002317 RID: 8983 RVA: 0x0008DB00 File Offset: 0x0008CF00
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return DoubleCollection.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter um <see cref="T:System.Windows.Media.DoubleCollection" /> para um tipo especificado.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que deseja converter.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.DoubleCollection" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo para o qual converter este <see cref="T:System.Windows.Media.DoubleCollection" />.</param>
		/// <returns>O objeto criado quando você converte este <see cref="T:System.Windows.Media.DoubleCollection" />.</returns>
		// Token: 0x06002318 RID: 8984 RVA: 0x0008DB34 File Offset: 0x0008CF34
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is DoubleCollection)
			{
				DoubleCollection doubleCollection = (DoubleCollection)value;
				if (destinationType == typeof(string))
				{
					return doubleCollection.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
