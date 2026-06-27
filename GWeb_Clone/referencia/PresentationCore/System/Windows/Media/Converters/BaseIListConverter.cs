using System;
using System.ComponentModel;
using System.Globalization;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Converters
{
	/// <summary>Define métodos usados para converter os membros da coleção <see cref="T:System.Collections.IList" /> em instâncias de <see cref="T:System.String" /> e delas.</summary>
	// Token: 0x020005BD RID: 1469
	[FriendAccessAllowed]
	public abstract class BaseIListConverter : TypeConverter
	{
		/// <summary>Determina se um tipo determinado pode ser convertido.</summary>
		/// <param name="td">Fornece informações contextuais necessárias para conversão.</param>
		/// <param name="t">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se este tipo puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600430A RID: 17162 RVA: 0x0010439C File Offset: 0x0010379C
		public override bool CanConvertFrom(ITypeDescriptorContext td, Type t)
		{
			return t == typeof(string);
		}

		/// <summary>Determina se um tipo determinado pode ser convertido em um <see cref="T:System.String" />.</summary>
		/// <param name="context">Fornece informações contextuais necessárias para conversão.</param>
		/// <param name="destinationType">O valor que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se este tipo puder ser convertido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600430B RID: 17163 RVA: 0x001043BC File Offset: 0x001037BC
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		/// <summary>Converte um <see cref="T:System.String" /> em uma instância com suporte de <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="td">Fornece informações contextuais necessárias para conversão.</param>
		/// <param name="ci">Informações culturais a serem respeitadas durante a conversão.</param>
		/// <param name="value">Cadeia de caracteres usada para conversão.</param>
		/// <returns>Um <see cref="T:System.Object" /> que representa o resultado da conversão.</returns>
		/// <exception cref="T:System.ArgumentException">Ocorrerá se o valor for <see langword="null" /> e não um <see cref="T:System.String" />.</exception>
		// Token: 0x0600430C RID: 17164 RVA: 0x001043DC File Offset: 0x001037DC
		public override object ConvertFrom(ITypeDescriptorContext td, CultureInfo ci, object value)
		{
			if (value == null)
			{
				throw base.GetConvertFromException(value);
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException(SR.Get("General_BadType", new object[]
				{
					"ConvertFrom"
				}), "value");
			}
			return this.ConvertFromCore(td, ci, text);
		}

		/// <summary>Converte uma instância com suporte de <see cref="T:System.Collections.IList" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="context">Fornece informações contextuais necessárias para conversão.</param>
		/// <param name="culture">Informações culturais a serem respeitadas durante a conversão.</param>
		/// <param name="value">Objeto sendo avaliado para conversão.</param>
		/// <param name="destinationType">Tipo de destino sendo avaliado para conversão.</param>
		/// <returns>A representação de cadeia de caracteres da coleção <see cref="T:System.Collections.IList" />.</returns>
		// Token: 0x0600430D RID: 17165 RVA: 0x0010442C File Offset: 0x0010382C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != null)
			{
				return this.ConvertToCore(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600430E RID: 17166
		internal abstract object ConvertFromCore(ITypeDescriptorContext td, CultureInfo ci, string value);

		// Token: 0x0600430F RID: 17167
		internal abstract object ConvertToCore(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);

		// Token: 0x04001856 RID: 6230
		internal TokenizerHelper _tokenizer;

		// Token: 0x04001857 RID: 6231
		internal const char DelimiterChar = ' ';
	}
}
