using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Converte um objeto <see cref="T:System.Windows.Input.KeyGesture" /> de e em outros tipos.</summary>
	// Token: 0x02000218 RID: 536
	public class KeyGestureConverter : TypeConverter
	{
		/// <summary>Determina se um objeto do tipo especificado pode ser convertido em uma instância do <see cref="T:System.Windows.Input.KeyGesture" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="sourceType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="sourceType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E6E RID: 3694 RVA: 0x000369FC File Offset: 0x00035DFC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Determina se uma instância do <see cref="T:System.Windows.Input.KeyGesture" /> pode ser convertida no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="destinationType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E6F RID: 3695 RVA: 0x00036A20 File Offset: 0x00035E20
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string) && context != null && context.Instance != null)
			{
				KeyGesture keyGesture = context.Instance as KeyGesture;
				if (keyGesture != null)
				{
					return ModifierKeysConverter.IsDefinedModifierKeys(keyGesture.Modifiers) && KeyGestureConverter.IsDefinedKey(keyGesture.Key);
				}
			}
			return false;
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Input.KeyGesture" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Informações específicas à cultura.</param>
		/// <param name="source">O objeto a ser convertido.</param>
		/// <returns>O objeto convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="source" /> não pode ser convertido.</exception>
		// Token: 0x06000E70 RID: 3696 RVA: 0x00036A78 File Offset: 0x00035E78
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object source)
		{
			if (source != null && source is string)
			{
				string text = ((string)source).Trim();
				if (text == string.Empty)
				{
					return new KeyGesture(Key.None);
				}
				int num = text.IndexOf(',');
				string displayString;
				if (num >= 0)
				{
					displayString = text.Substring(num + 1).Trim();
					text = text.Substring(0, num).Trim();
				}
				else
				{
					displayString = string.Empty;
				}
				num = text.LastIndexOf('+');
				string value;
				string value2;
				if (num >= 0)
				{
					value = text.Substring(0, num);
					value2 = text.Substring(num + 1);
				}
				else
				{
					value = string.Empty;
					value2 = text;
				}
				ModifierKeys modifiers = ModifierKeys.None;
				object obj = KeyGestureConverter.keyConverter.ConvertFrom(context, culture, value2);
				if (obj != null)
				{
					object obj2 = KeyGestureConverter.modifierKeysConverter.ConvertFrom(context, culture, value);
					if (obj2 != null)
					{
						modifiers = (ModifierKeys)obj2;
					}
					return new KeyGesture((Key)obj, modifiers, displayString);
				}
			}
			throw base.GetConvertFromException(source);
		}

		/// <summary>Tenta converter um <see cref="T:System.Windows.Input.KeyGesture" /> no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Informações específicas à cultura.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <param name="destinationType">O tipo no qual converter o objeto.</param>
		/// <returns>O objeto convertido ou uma cadeia de caracteres vazia, se <paramref name="value" /> for <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> não pode ser convertido.</exception>
		// Token: 0x06000E71 RID: 3697 RVA: 0x00036B60 File Offset: 0x00035F60
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return string.Empty;
				}
				KeyGesture keyGesture = value as KeyGesture;
				if (keyGesture != null)
				{
					if (keyGesture.Key == Key.None)
					{
						return string.Empty;
					}
					string text = "";
					string text2 = (string)KeyGestureConverter.keyConverter.ConvertTo(context, culture, keyGesture.Key, destinationType);
					if (text2 != string.Empty)
					{
						text += (KeyGestureConverter.modifierKeysConverter.ConvertTo(context, culture, keyGesture.Modifiers, destinationType) as string);
						if (text != string.Empty)
						{
							text += "+";
						}
						text += text2;
						if (!string.IsNullOrEmpty(keyGesture.DisplayString))
						{
							text = text + "," + keyGesture.DisplayString;
						}
					}
					return text;
				}
			}
			throw base.GetConvertToException(value, destinationType);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00036C64 File Offset: 0x00036064
		internal static bool IsDefinedKey(Key key)
		{
			return key >= Key.None && key <= Key.OemClear;
		}

		// Token: 0x04000840 RID: 2112
		private const char MODIFIERS_DELIMITER = '+';

		// Token: 0x04000841 RID: 2113
		internal const char DISPLAYSTRING_SEPARATOR = ',';

		// Token: 0x04000842 RID: 2114
		private static KeyConverter keyConverter = new KeyConverter();

		// Token: 0x04000843 RID: 2115
		private static ModifierKeysConverter modifierKeysConverter = new ModifierKeysConverter();
	}
}
