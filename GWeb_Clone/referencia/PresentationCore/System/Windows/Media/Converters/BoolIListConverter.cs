using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using MS.Internal;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte os membros de uma coleção de <see cref="T:System.Collections.IList" /> de valores boolianos de e para instâncias de <see cref="T:System.String" />.</summary>
	// Token: 0x020005C0 RID: 1472
	public sealed class BoolIListConverter : BaseIListConverter
	{
		// Token: 0x06004317 RID: 17175 RVA: 0x00104630 File Offset: 0x00103A30
		internal override object ConvertFromCore(ITypeDescriptorContext td, CultureInfo ci, string value)
		{
			this._tokenizer = new TokenizerHelper(value, '\0', ' ');
			List<bool> list = new List<bool>(Math.Min(256, value.Length / 2 + 1));
			while (this._tokenizer.NextToken())
			{
				list.Add(Convert.ToInt32(this._tokenizer.GetCurrentToken(), ci) != 0);
			}
			return list;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x00104690 File Offset: 0x00103A90
		internal override object ConvertToCore(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			IList<bool> list = value as IList<bool>;
			if (list == null)
			{
				throw base.GetConvertToException(value, destinationType);
			}
			StringBuilder stringBuilder = new StringBuilder(2 * list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(list[i] ? 1 : 0);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400185A RID: 6234
		private const int EstimatedCharCountPerItem = 2;
	}
}
