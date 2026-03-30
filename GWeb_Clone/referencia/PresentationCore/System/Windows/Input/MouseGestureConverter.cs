using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Converte um objeto <see cref="T:System.Windows.Input.MouseGesture" /> de e em outros tipos.</summary>
	// Token: 0x0200021F RID: 543
	public class MouseGestureConverter : TypeConverter
	{
		/// <summary>Determina se um objeto do tipo especificado pode ser convertido em uma instância do <see cref="T:System.Windows.Input.MouseGesture" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="sourceType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="sourceType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000376FC File Offset: 0x00036AFC
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Tenta converter o objeto especificado em um <see cref="T:System.Windows.Input.MouseGesture" />, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Informações específicas à cultura.</param>
		/// <param name="source">O objeto a ser convertido.</param>
		/// <returns>O objeto convertido.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="source" /> não pode ser convertido.</exception>
		// Token: 0x06000EA5 RID: 3749 RVA: 0x00037720 File Offset: 0x00036B20
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object source)
		{
			if (source is string && source != null)
			{
				string text = ((string)source).Trim();
				if (text == string.Empty)
				{
					return new MouseGesture(MouseAction.None, ModifierKeys.None);
				}
				int num = text.LastIndexOf('+');
				string text2;
				string value;
				if (num >= 0)
				{
					text2 = text.Substring(0, num);
					value = text.Substring(num + 1);
				}
				else
				{
					text2 = string.Empty;
					value = text;
				}
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(MouseAction));
				if (converter != null)
				{
					object obj = converter.ConvertFrom(context, culture, value);
					if (obj != null)
					{
						if (!(text2 != string.Empty))
						{
							return new MouseGesture((MouseAction)obj);
						}
						TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(ModifierKeys));
						if (converter2 != null)
						{
							object obj2 = converter2.ConvertFrom(context, culture, text2);
							if (obj2 != null && obj2 is ModifierKeys)
							{
								return new MouseGesture((MouseAction)obj, (ModifierKeys)obj2);
							}
						}
					}
				}
			}
			throw base.GetConvertFromException(source);
		}

		/// <summary>Determina se uma instância do <see cref="T:System.Windows.Input.MouseGesture" /> pode ser convertida no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="destinationType">O tipo que está sendo avaliado para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="destinationType" /> for do tipo <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00037810 File Offset: 0x00036C10
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string) && context != null && context.Instance != null)
			{
				MouseGesture mouseGesture = context.Instance as MouseGesture;
				if (mouseGesture != null)
				{
					return ModifierKeysConverter.IsDefinedModifierKeys(mouseGesture.Modifiers) && MouseActionConverter.IsDefinedMouseAction(mouseGesture.MouseAction);
				}
			}
			return false;
		}

		/// <summary>Tenta converter um <see cref="T:System.Windows.Input.MouseGesture" /> no tipo especificado, usando o contexto especificado.</summary>
		/// <param name="context">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="culture">Um contexto de formato que fornece informações sobre o ambiente do qual este conversor está sendo invocado.</param>
		/// <param name="value">O objeto a ser convertido.</param>
		/// <param name="destinationType">O tipo no qual converter o objeto.</param>
		/// <returns>O objeto convertido.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> é <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> não pode ser convertido.</exception>
		// Token: 0x06000EA7 RID: 3751 RVA: 0x00037868 File Offset: 0x00036C68
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
				MouseGesture mouseGesture = value as MouseGesture;
				if (mouseGesture != null)
				{
					string text = "";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(ModifierKeys));
					if (converter != null)
					{
						text += (converter.ConvertTo(context, culture, mouseGesture.Modifiers, destinationType) as string);
						if (text != string.Empty)
						{
							text += "+";
						}
					}
					TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(MouseAction));
					if (converter2 != null)
					{
						text += (converter2.ConvertTo(context, culture, mouseGesture.MouseAction, destinationType) as string);
					}
					return text;
				}
			}
			throw base.GetConvertToException(value, destinationType);
		}

		// Token: 0x04000852 RID: 2130
		private const char MODIFIERS_DELIMITER = '+';
	}
}
