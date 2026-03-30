using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte instâncias de outros tipos de e para um <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
	// Token: 0x020003C2 RID: 962
	public sealed class PathFigureCollectionConverter : TypeConverter
	{
		/// <summary>Indica se um objeto pode ser convertido de um determinado tipo em uma instância de um <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O <see cref="T:System.Type" /> de origem que está sendo consultado quanto a suporte para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se for possível converter o tipo especificado em um <see cref="T:System.Windows.Media.PathFigureCollection" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600258C RID: 9612 RVA: 0x00095EA0 File Offset: 0x000952A0
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Determina se é possível converter instâncias de <see cref="T:System.Windows.Media.PathFigureCollection" /> para o tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual se está avaliando converter este <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <returns>
		///   <see langword="true" /> se este instâncias de <see cref="T:System.Windows.Media.PathFigureCollection" /> puderem ser convertidas para <paramref name="destinationType" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600258D RID: 9613 RVA: 0x00095ECC File Offset: 0x000952CC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.CanConvertTo(context, destinationType);
			}
			if (context == null || context.Instance == null)
			{
				return true;
			}
			if (!(context.Instance is PathFigureCollection))
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"PathFigureCollection"
				}), "context");
			}
			PathFigureCollection pathFigureCollection = (PathFigureCollection)context.Instance;
			return pathFigureCollection.CanSerializeToString();
		}

		/// <summary>Converte o objeto especificado em um <see cref="T:System.Windows.Media.PathFigureCollection" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.PathFigureCollection" /> criado da conversão de <paramref name="value" />.</returns>
		// Token: 0x0600258E RID: 9614 RVA: 0x00095F44 File Offset: 0x00095344
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text != null)
			{
				return PathFigureCollection.Parse(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converte a <see cref="T:System.Windows.Media.PathFigureCollection" /> especificada no tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.PathFigureCollection" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo no qual converter <see cref="T:System.Windows.Media.PathFigureCollection" />.</param>
		/// <returns>Um <see cref="T:System.Object" /> que representa o valor convertido.</returns>
		// Token: 0x0600258F RID: 9615 RVA: 0x00095F78 File Offset: 0x00095378
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is PathFigureCollection)
			{
				PathFigureCollection pathFigureCollection = (PathFigureCollection)value;
				if (destinationType == typeof(string))
				{
					if (context != null && context.Instance != null && !pathFigureCollection.CanSerializeToString())
					{
						throw new NotSupportedException(SR.Get("Converter_ConvertToNotSupported"));
					}
					return pathFigureCollection.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
