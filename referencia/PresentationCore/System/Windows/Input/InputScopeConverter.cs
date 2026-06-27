using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Converte uma <see cref="T:System.Windows.Input.InputScope" /> em/de outros tipos.</summary>
	// Token: 0x02000262 RID: 610
	public class InputScopeConverter : TypeConverter
	{
		/// <summary>Determina se um objeto <see cref="T:System.Windows.Input.InputScope" /> pode ser convertido de um objeto de um tipo especificado.</summary>
		/// <param name="context">Um objeto que descreve qualquer contexto de descritor de tipo.  Este objeto deve implementar a interface <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.  Esse parâmetro pode ser <see langword="null" />.</param>
		/// <param name="sourceType">Um <see cref="T:System.Type" /> para verificar a compatibilidade de conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="sourceType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001127 RID: 4391 RVA: 0x00040860 File Offset: 0x0003FC60
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Determina se um objeto <see cref="T:System.Windows.Input.InputScope" /> pode ser convertido em um objeto de um tipo especificado.</summary>
		/// <param name="context">Um objeto que descreve qualquer contexto de descritor de tipo.  Este objeto deve implementar a interface <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.  Esse parâmetro pode ser <see langword="null" />.</param>
		/// <param name="destinationType">Um <see cref="T:System.Type" /> para verificar a compatibilidade de conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="destinationType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001128 RID: 4392 RVA: 0x00040884 File Offset: 0x0003FC84
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return false;
		}

		/// <summary>Converte um objeto de origem (cadeia de caracteres) em um objeto <see cref="T:System.Windows.Input.InputScope" />.</summary>
		/// <param name="context">Um objeto que descreve qualquer contexto de descritor de tipo.  Este objeto deve implementar a interface <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.  Esse parâmetro pode ser <see langword="null" />.</param>
		/// <param name="culture">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que contém qualquer contexto cultural para a conversão.  Esse parâmetro pode ser <see langword="null" />.</param>
		/// <param name="source">Um objeto de origem do qual converter.  Esse objeto deve ser uma cadeia de caracteres.</param>
		/// <returns>Um objeto <see cref="T:System.Windows.Input.InputScope" /> convertido do objeto de origem especificado.</returns>
		// Token: 0x06001129 RID: 4393 RVA: 0x00040894 File Offset: 0x0003FC94
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
			return new InputScope
			{
				Names = 
				{
					new InputScopeName(nameValue)
				}
			};
		}

		/// <summary>Converte um objeto <see cref="T:System.Windows.Input.InputScope" /> em um tipo de objeto especificado (cadeia de caracteres).</summary>
		/// <param name="context">Um objeto que descreve qualquer contexto de descritor de tipo.  Este objeto deve implementar a interface <see cref="T:System.ComponentModel.ITypeDescriptorContext" />.  Esse parâmetro pode ser <see langword="null" />.</param>
		/// <param name="culture">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que contém qualquer contexto cultural para a conversão.  Esse parâmetro pode ser <see langword="null" />.</param>
		/// <param name="value">Um objeto do qual converter.  Esse objeto deve ser do tipo <see cref="T:System.Windows.Input.InputScope" />.</param>
		/// <param name="destinationType">Um tipo de destino para converter o tipo.  Esse tipo deve ser uma cadeia de caracteres.</param>
		/// <returns>Um novo objeto do tipo especificado (cadeia de caracteres) convertido do objeto <see cref="T:System.Windows.Input.InputScope" /> determinado.</returns>
		// Token: 0x0600112A RID: 4394 RVA: 0x0004090C File Offset: 0x0003FD0C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			InputScope inputScope = value as InputScope;
			if (null != destinationType && inputScope != null && destinationType == typeof(string))
			{
				return Enum.GetName(typeof(InputScopeNameValue), ((InputScopeName)inputScope.Names[0]).NameValue);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
