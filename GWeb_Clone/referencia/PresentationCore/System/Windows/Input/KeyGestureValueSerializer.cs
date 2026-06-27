using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace System.Windows.Input
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
	// Token: 0x02000219 RID: 537
	public class KeyGestureValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a <see cref="T:System.String" /> especificada pode ser convertida em uma instância de <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Sempre retorna <see langword="true" />.</returns>
		// Token: 0x06000E75 RID: 3701 RVA: 0x00036CBC File Offset: 0x000360BC
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.KeyGesture" /> especificado pode ser convertido em um <see cref="T:System.String" />.</summary>
		/// <param name="value">O gesto para avaliar quanto à conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E76 RID: 3702 RVA: 0x00036CCC File Offset: 0x000360CC
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			KeyGesture keyGesture = value as KeyGesture;
			return keyGesture != null && ModifierKeysConverter.IsDefinedModifierKeys(keyGesture.Modifiers) && KeyGestureConverter.IsDefinedKey(keyGesture.Key);
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Input.KeyGesture" />.</summary>
		/// <param name="value">A cadeia de caracteres a ser convertida em um <see cref="T:System.Windows.Input.KeyGesture" />.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Input.KeyGesture" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06000E77 RID: 3703 RVA: 0x00036D00 File Offset: 0x00036100
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(KeyGesture));
			if (converter != null)
			{
				return converter.ConvertFromString(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Input.KeyGesture" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">O gesto a converter em uma cadeia de caracteres.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma representação de cadeia de caracteres invariável do <see cref="T:System.Windows.Input.KeyGesture" /> especificado.</returns>
		// Token: 0x06000E78 RID: 3704 RVA: 0x00036D30 File Offset: 0x00036130
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(KeyGesture));
			if (converter != null)
			{
				return converter.ConvertToInvariantString(value);
			}
			return base.ConvertToString(value, context);
		}
	}
}
