using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using MS.Internal;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte uma coleção <see cref="T:System.Collections.IList" /> de valores de número UShort de instâncias de <see cref="T:System.String" /> e nelas.</summary>
	// Token: 0x020005BF RID: 1471
	public sealed class UShortIListConverter : BaseIListConverter
	{
		// Token: 0x06004314 RID: 17172 RVA: 0x00104550 File Offset: 0x00103950
		internal override object ConvertFromCore(ITypeDescriptorContext td, CultureInfo ci, string value)
		{
			this._tokenizer = new TokenizerHelper(value, '\0', ' ');
			List<ushort> list = new List<ushort>(Math.Min(256, value.Length / 3 + 1));
			while (this._tokenizer.NextToken())
			{
				list.Add(Convert.ToUInt16(this._tokenizer.GetCurrentToken(), ci));
			}
			return list;
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x001045B0 File Offset: 0x001039B0
		internal override object ConvertToCore(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			IList<ushort> list = value as IList<ushort>;
			if (list == null)
			{
				throw base.GetConvertToException(value, destinationType);
			}
			StringBuilder stringBuilder = new StringBuilder(3 * list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(list[i].ToString(culture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001859 RID: 6233
		private const int EstimatedCharCountPerItem = 3;
	}
}
