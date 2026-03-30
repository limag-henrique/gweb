using System;
using System.ComponentModel;
using System.Windows.Markup;

namespace System.Windows.Input
{
	/// <summary>Converte instâncias de <see cref="T:System.String" /> de e para instâncias de <see cref="T:System.Windows.Input.MouseAction" />.</summary>
	// Token: 0x0200021C RID: 540
	public class MouseActionValueSerializer : ValueSerializer
	{
		/// <summary>Determina se a <see cref="T:System.String" /> especificada pode ser convertida em uma instância de <see cref="T:System.Windows.Input.MouseAction" />.</summary>
		/// <param name="value">Cadeia de caracteres a ser avaliada para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Sempre retorna <see langword="true" />.</returns>
		// Token: 0x06000E80 RID: 3712 RVA: 0x00037040 File Offset: 0x00036440
		public override bool CanConvertFromString(string value, IValueSerializerContext context)
		{
			return true;
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.MouseAction" /> especificado pode ser convertido em um <see cref="T:System.String" />.</summary>
		/// <param name="value">A teclas modificadoras a serem avaliadas para conversão.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="value" /> puder ser convertido em um <see cref="T:System.String" />; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000E81 RID: 3713 RVA: 0x00037050 File Offset: 0x00036450
		public override bool CanConvertToString(object value, IValueSerializerContext context)
		{
			return value is MouseAction && MouseActionConverter.IsDefinedMouseAction((MouseAction)value);
		}

		/// <summary>Converte um <see cref="T:System.String" /> em um <see cref="T:System.Windows.Input.MouseAction" />.</summary>
		/// <param name="value">A cadeia de caracteres a ser convertida em um <see cref="T:System.Windows.Input.MouseAction" />.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Input.MouseAction" /> com base no <paramref name="value" /> fornecido.</returns>
		// Token: 0x06000E82 RID: 3714 RVA: 0x00037074 File Offset: 0x00036474
		public override object ConvertFromString(string value, IValueSerializerContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(MouseAction));
			if (converter != null)
			{
				return converter.ConvertFromString(value);
			}
			return base.ConvertFromString(value, context);
		}

		/// <summary>Converte uma instância de <see cref="T:System.Windows.Input.MouseAction" /> em um <see cref="T:System.String" />.</summary>
		/// <param name="value">A chave a ser convertida em uma cadeia de caracteres.</param>
		/// <param name="context">Informações de contexto que são usadas para conversão.</param>
		/// <returns>Uma representação de cadeia de caracteres invariável do <see cref="T:System.Windows.Input.MouseAction" /> especificado.</returns>
		// Token: 0x06000E83 RID: 3715 RVA: 0x000370A4 File Offset: 0x000364A4
		public override string ConvertToString(object value, IValueSerializerContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(MouseAction));
			if (converter != null)
			{
				return converter.ConvertToInvariantString(value);
			}
			return base.ConvertToString(value, context);
		}
	}
}
