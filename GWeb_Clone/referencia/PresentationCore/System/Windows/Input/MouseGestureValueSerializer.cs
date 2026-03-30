using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace System.Windows.Input
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Input.ModifierKeys" />.</summary>
	// Token: 0x02000220 RID: 544
	public class MouseGestureValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a <see cref="T:System.String" /> especificada pode ser convertida em uma instância de <see cref="T:System.Windows.Input.ModifierKeys" />.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Sempre retorna <see langword="true" />.</returns>
		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003795C File Offset: 0x00036D5C
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.ModifierKeys" /> especificado pode ser convertido em um <see cref="T:System.String" />.</summary>
		/// <param name="value">A teclas modificadoras a serem avaliadas para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000EAA RID: 3754 RVA: 0x0003796C File Offset: 0x00036D6C
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			bool result = false;
			MouseGesture mouseGesture = value as MouseGesture;
			if (mouseGesture != null && ModifierKeysConverter.IsDefinedModifierKeys(mouseGesture.Modifiers) && MouseActionConverter.IsDefinedMouseAction(mouseGesture.MouseAction))
			{
				result = true;
			}
			return result;
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Input.ModifierKeys" />.</summary>
		/// <param name="value">A cadeia de caracteres a ser convertida em um <see cref="T:System.Windows.Input.ModifierKeys" />.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Input.ModifierKeys" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06000EAB RID: 3755 RVA: 0x000379A4 File Offset: 0x00036DA4
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(MouseGesture));
			if (converter != null)
			{
				return converter.ConvertFromString(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Input.ModifierKeys" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">A chave a ser convertida em uma cadeia de caracteres.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma representação de cadeia de caracteres do <see cref="T:System.Windows.Input.ModifierKeys" /> especificado.</returns>
		// Token: 0x06000EAC RID: 3756 RVA: 0x000379D4 File Offset: 0x00036DD4
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(MouseGesture));
			if (converter != null)
			{
				return converter.ConvertToInvariantString(value);
			}
			return base.ConvertToString(value, context);
		}
	}
}
