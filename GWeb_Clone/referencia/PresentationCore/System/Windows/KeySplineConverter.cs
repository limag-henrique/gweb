using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows
{
	/// <summary>Converte instâncias de outros tipos de e para um <see cref="T:System.Windows.Media.Animation.KeySpline" />.</summary>
	// Token: 0x020001FB RID: 507
	public class KeySplineConverter : TypeConverter
	{
		/// <summary>Determina se um objeto pode ser convertido de um determinado tipo em uma instância de um <see cref="T:System.Windows.Media.Animation.KeySpline" />.</summary>
		/// <param name="typeDescriptor">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo da origem que está sendo avaliada para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se o tipo puder ser convertido para um <see cref="T:System.Windows.Media.Animation.KeySpline" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D33 RID: 3379 RVA: 0x00031E18 File Offset: 0x00031218
		public override bool CanConvertFrom(ITypeDescriptorContext typeDescriptor, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		/// <summary>Determina se uma instância de um <see cref="T:System.Windows.Media.Animation.KeySpline" /> pode ser convertida em um tipo diferente.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual se está avaliando converter este <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		/// <returns>
		///   <see langword="true" /> se este <see cref="T:System.Windows.Media.Animation.KeySpline" /> puder ser convertido para <paramref name="destinationType" />, caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000D34 RID: 3380 RVA: 0x00031E3C File Offset: 0x0003123C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string);
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Media.Animation.KeySpline" />.</summary>
		/// <param name="context">Fornece informações contextuais necessárias para conversão.</param>
		/// <param name="cultureInfo">Informações culturais a serem respeitadas durante a conversão.</param>
		/// <param name="value">O objeto sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Media.Animation.KeySpline" /> criado da conversão de <paramref name="value" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Gerada se o objeto especificado for NULL ou for um tipo que não possa ser convertido em um <see cref="T:System.Windows.Media.Animation.KeySpline" />.</exception>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00031E70 File Offset: 0x00031270
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo cultureInfo, object value)
		{
			string str = value as string;
			if (value == null)
			{
				throw new NotSupportedException(SR.Get("Converter_ConvertFromNotSupported"));
			}
			TokenizerHelper tokenizerHelper = new TokenizerHelper(str, cultureInfo);
			return new KeySpline(Convert.ToDouble(tokenizerHelper.NextTokenRequired(), cultureInfo), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), cultureInfo), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), cultureInfo), Convert.ToDouble(tokenizerHelper.NextTokenRequired(), cultureInfo));
		}

		/// <summary>Tenta converter um <see cref="T:System.Windows.Media.Animation.KeySpline" /> para um tipo especificado.</summary>
		/// <param name="context">Fornece informações contextuais necessárias para conversão.</param>
		/// <param name="cultureInfo">Informações culturais a serem respeitadas durante a conversão.</param>
		/// <param name="value">O <see cref="T:System.Windows.Media.Animation.KeySpline" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo para o qual converter este <see cref="T:System.Windows.Media.Animation.KeySpline" />.</param>
		/// <returns>O objeto criado da conversão deste <see cref="T:System.Windows.Media.Animation.KeySpline" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Gerada se <paramref name="value" /> é <see langword="null" /> ou não é um <see cref="T:System.Windows.Media.Animation.KeySpline" /> ou então se <paramref name="destinationType" /> não é um dos tipos válidos para conversão.</exception>
		// Token: 0x06000D36 RID: 3382 RVA: 0x00031ED4 File Offset: 0x000312D4
		[SecurityCritical]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo cultureInfo, object value, Type destinationType)
		{
			KeySpline keySpline = value as KeySpline;
			if (keySpline != null && destinationType != null)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(KeySpline).GetConstructor(new Type[]
					{
						typeof(double),
						typeof(double),
						typeof(double),
						typeof(double)
					});
					return new InstanceDescriptor(constructor, new object[]
					{
						keySpline.ControlPoint1.X,
						keySpline.ControlPoint1.Y,
						keySpline.ControlPoint2.X,
						keySpline.ControlPoint2.Y
					});
				}
				if (destinationType == typeof(string))
				{
					return string.Format(cultureInfo, "{0}{4}{1}{4}{2}{4}{3}", new object[]
					{
						keySpline.ControlPoint1.X,
						keySpline.ControlPoint1.Y,
						keySpline.ControlPoint2.X,
						keySpline.ControlPoint2.Y,
						(cultureInfo != null) ? cultureInfo.TextInfo.ListSeparator : CultureInfo.InvariantCulture.TextInfo.ListSeparator
					});
				}
			}
			return base.ConvertTo(context, cultureInfo, value, destinationType);
		}
	}
}
