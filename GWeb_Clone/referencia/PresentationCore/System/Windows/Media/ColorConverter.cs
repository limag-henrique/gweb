using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte as instâncias de outros tipos de e em uma instância de <see cref="T:System.Windows.Media.Color" />.</summary>
	// Token: 0x02000372 RID: 882
	public sealed class ColorConverter : TypeConverter
	{
		/// <summary>Determina se um objeto pode ser convertido de um determinado tipo em uma instância de um <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="td">Descreve as informações de contexto de um tipo.</param>
		/// <param name="t">O tipo da origem que está sendo avaliada para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo puder ser convertido para um <see cref="T:System.Windows.Media.Color" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F8C RID: 8076 RVA: 0x000811A4 File Offset: 0x000805A4
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se uma instância de um <see cref="T:System.Windows.Media.Color" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual se está avaliando converter este <see cref="T:System.Windows.Media.Color" />.</param>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.Color" /> puder ser convertido para <paramref name="destinationType" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001F8D RID: 8077 RVA: 0x000811C8 File Offset: 0x000805C8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tentativas de converter uma cadeia em um <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="value">A cadeia de caracteres a ser convertida para um <see cref="T:System.Windows.Media.Color" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.Color" /> que representa o texto convertido.</returns>
		// Token: 0x06001F8E RID: 8078 RVA: 0x000811F4 File Offset: 0x000805F4
		public static object ConvertFromString(string value)
		{
			if (value == null)
			{
				return null;
			}
			return Parsers.ParseColor(value, null);
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Media.Color" />.</summary>
		/// <param name="td">Descreve as informações de contexto de um tipo.</param>
		/// <param name="ci">Informações culturais a serem respeitadas durante a conversão.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Color" /> criado da conversão de <paramref name="value" />.</returns>
		// Token: 0x06001F8F RID: 8079 RVA: 0x00081214 File Offset: 0x00080614
		public override object ConvertFrom(ITypeDescriptorContext td, CultureInfo ci, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			if (!(value is string))
			{
				throw new ArgumentException(SR.Get("General_BadType", new object[]
				{
					"ConvertFrom"
				}), "value");
			}
			return Parsers.ParseColor(value as string, ci, td);
		}

		/// <summary>Tenta converter um <see cref="T:System.Windows.Media.Color" /> para um tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.Color" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo para o qual converter este <see cref="T:System.Windows.Media.Color" />.</param>
		/// <returns>O objeto criado da conversão deste <see cref="T:System.Windows.Media.Color" />.</returns>
		// Token: 0x06001F90 RID: 8080 RVA: 0x0008126C File Offset: 0x0008066C
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null && value is Color)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					MethodInfo method = typeof(Color).GetMethod("FromArgb", new Type[]
					{
						typeof(byte),
						typeof(byte),
						typeof(byte),
						typeof(byte)
					});
					Color color = (Color)value;
					return new InstanceDescriptor(method, new object[]
					{
						color.A,
						color.R,
						color.G,
						color.B
					});
				}
				if (destinationType == typeof(string))
				{
					return ((Color)value).ToString(culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
