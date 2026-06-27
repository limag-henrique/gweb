using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Converte instâncias de <see cref="T:System.Windows.Input.InputScopeName" /> de e para outros tipos de dados.</summary>
	// Token: 0x02000264 RID: 612
	public class InputScopeNameConverter : TypeConverter
	{
		/// <summary>Indica se um objeto pode ser convertido de um determinado tipo em uma instância de um <see cref="T:System.Windows.Input.InputScopeName" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="sourceType">O <see cref="T:System.Type" /> de origem que está sendo consultado quanto a suporte para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="sourceType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600112C RID: 4396 RVA: 0x0004098C File Offset: 0x0003FD8C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Determina se é possível converter instâncias de <see cref="T:System.Windows.Input.InputScopeName" /> para o tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="destinationType">O tipo desejado para o qual se está avaliando converter este <see cref="T:System.Windows.Input.InputScopeName" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="destinationType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600112D RID: 4397 RVA: 0x000409B0 File Offset: 0x0003FDB0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return typeof(string) == destinationType && context != null && context.Instance != null && context.Instance is InputScopeName;
		}

		/// <summary>Converte o objeto especificado em um <see cref="T:System.Windows.Input.InputScopeName" />.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="source">O objeto sendo convertido.</param>
		/// <returns>O <see cref="T:System.Windows.Input.InputScopeName" /> criado da conversão de <paramref name="source" />.</returns>
		// Token: 0x0600112E RID: 4398 RVA: 0x000409EC File Offset: 0x0003FDEC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object source)
		{
			string text = source as string;
			InputScopeNameValue nameValue = InputScopeNameValue.Default;
			if (text != null)
			{
				text = text.Trim();
				if (-1 != text.LastIndexOf('.'))
				{
					text = text.Substring(text.LastIndexOf('.') + 1);
				}
				if (!text.Equals(string.Empty))
				{
					nameValue = (InputScopeNameValue)Enum.Parse(typeof(InputScopeNameValue), text);
				}
			}
			return new InputScopeName
			{
				NameValue = nameValue
			};
		}

		/// <summary>Converte o <see cref="T:System.Windows.Input.InputScopeName" /> especificado no tipo especificado.</summary>
		/// <param name="context">Descreve as informações de contexto de um tipo.</param>
		/// <param name="culture">Descreve o <see cref="T:System.Globalization.CultureInfo" /> do tipo que está sendo convertido.</param>
		/// <param name="value">O <see cref="T:System.Windows.Input.InputScopeName" /> a ser convertido.</param>
		/// <param name="destinationType">O tipo no qual converter <see cref="T:System.Windows.Input.InputScopeName" />.</param>
		/// <returns>O objeto criado da conversão deste <see cref="T:System.Windows.Input.InputScopeName" />.</returns>
		// Token: 0x0600112F RID: 4399 RVA: 0x00040A5C File Offset: 0x0003FE5C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			InputScopeName inputScopeName = value as InputScopeName;
			if (null != destinationType && inputScopeName != null && destinationType == typeof(string))
			{
				return Enum.GetName(typeof(InputScopeNameValue), inputScopeName.NameValue);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
