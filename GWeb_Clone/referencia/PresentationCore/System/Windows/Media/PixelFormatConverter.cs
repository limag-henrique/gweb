using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Converte uma <see cref="T:System.Windows.Media.PixelFormat" /> em/de outros tipos de dados.</summary>
	// Token: 0x0200042F RID: 1071
	public sealed class PixelFormatConverter : TypeConverter
	{
		/// <summary>Determina se o conversor pode converter um objeto do tipo determinado em uma instância de <see cref="T:System.Windows.Media.PixelFormat" />.</summary>
		/// <param name="td">Informações de contexto de tipo usadas para avaliar a conversão.</param>
		/// <param name="t">O tipo de origem que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter o tipo fornecido em uma instância de <see cref="T:System.Windows.Media.PixelFormat" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BED RID: 11245 RVA: 0x000AFD48 File Offset: 0x000AF148
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se uma instância de <see cref="T:System.Windows.Media.PixelFormat" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Informações de contexto de tipo usadas para avaliar a conversão.</param>
		/// <param name="destinationType">O tipo desejado para o qual avaliar a conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o conversor puder converter esta instância de <see cref="T:System.Windows.Media.PixelFormat" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002BEE RID: 11246 RVA: 0x000AFD6C File Offset: 0x000AF16C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Tentativas de converter uma cadeia em um <see cref="T:System.Windows.Media.PixelFormat" />.</summary>
		/// <param name="value">A cadeia de caracteres a ser convertida para um <see cref="T:System.Windows.Media.PixelFormat" />.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.PixelFormat" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> não pôde ser convertido em um <see cref="T:System.Windows.Media.PixelFormat" /> conhecido.</exception>
		// Token: 0x06002BEF RID: 11247 RVA: 0x000AFDA8 File Offset: 0x000AF1A8
		public new object ConvertFromString(string value)
		{
			if (value == null)
			{
				return null;
			}
			return new PixelFormat(value);
		}

		/// <summary>Tenta converter um objeto especificado em uma instância de <see cref="T:System.Windows.Media.PixelFormat" />.</summary>
		/// <param name="td">Informações de contexto de tipo usadas para conversão.</param>
		/// <param name="ci">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="o">O objeto que está sendo convertido.</param>
		/// <returns>Um <see cref="T:System.Object" /> que representa o valor convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="o" /> é <see langword="null" /> ou é um tipo inválido.</exception>
		// Token: 0x06002BF0 RID: 11248 RVA: 0x000AFDC8 File Offset: 0x000AF1C8
		public override object ConvertFrom(ITypeDescriptorContext td, CultureInfo ci, object o)
		{
			if (o == null)
			{
				return null;
			}
			return new PixelFormat(o as string);
		}

		/// <summary>Tenta converter uma instância de <see cref="T:System.Windows.Media.PixelFormat" /> em um tipo especificado.</summary>
		/// <param name="context">Informações de contexto usadas para conversão.</param>
		/// <param name="culture">Informações culturais respeitadas durante a conversão.</param>
		/// <param name="value">
		///   <see cref="T:System.Windows.Media.PixelFormat" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>Uma nova instância do <paramref name="destinationType" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> é <see langword="null" /> ou não é um tipo válido.</exception>
		// Token: 0x06002BF1 RID: 11249 RVA: 0x000AFDEC File Offset: 0x000AF1EC
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (null == destinationType)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is PixelFormat))
			{
				throw new ArgumentException(SR.Get("General_Expected_Type", new object[]
				{
					"PixelFormat"
				}));
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo constructor = typeof(PixelFormat).GetConstructor(new Type[]
				{
					typeof(string)
				});
				return new InstanceDescriptor(constructor, new object[]
				{
					((PixelFormat)value).ToString()
				});
			}
			if (destinationType == typeof(string))
			{
				return ((PixelFormat)value).ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
