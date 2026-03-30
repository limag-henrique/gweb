using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias de outros tipos de e para um <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
	// Token: 0x020003FE RID: 1022
	public sealed class VectorCollectionConverter : TypeConverter
	{
		/// <summary>Determina se um objeto pode ser convertido de um tipo especificado em uma instância de um <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="sourceType">O tipo de origem a ser avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo puder ser convertido em uma <see cref="T:System.Windows.Media.VectorCollection" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060028D1 RID: 10449 RVA: 0x000A37EC File Offset: 0x000A2BEC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se uma instância de um <see cref="T:System.Windows.Media.VectorCollection" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo necessário para o qual você está avaliando este <see cref="T:System.Windows.Media.VectorCollection" /> para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.VectorCollection" /> puder ser convertido para <paramref name="destinationType" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x060028D2 RID: 10450 RVA: 0x000A3818 File Offset: 0x000A2C18
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Media.VectorCollection" />.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que deseja converter.</param>
		/// <param name="value">O objeto que está sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.VectorCollection" /> criado na conversão de <paramref name="value" />.</returns>
		// Token: 0x060028D3 RID: 10451 RVA: 0x000A3844 File Offset: 0x000A2C44
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return VectorCollection.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Tenta converter uma <see cref="T:System.Windows.Media.VectorCollection" /> em um tipo especificado.</summary>
		/// <param name="context">As informações de contexto de um tipo.</param>
		/// <param name="culture">O <see cref="T:System.Globalization.CultureInfo" /> do tipo que deseja converter.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.VectorCollection" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo para o qual converter este <see cref="T:System.Windows.Media.VectorCollection" />.</param>
		/// <returns>O objeto criado da conversão deste <see cref="T:System.Windows.Media.VectorCollection" />.</returns>
		// Token: 0x060028D4 RID: 10452 RVA: 0x000A3878 File Offset: 0x000A2C78
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is VectorCollection)
			{
				VectorCollection vectorCollection = (VectorCollection)value;
				if (destinationType == typeof(string))
				{
					return vectorCollection.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
