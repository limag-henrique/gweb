using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using MS.Internal;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte membros de uma coleção <see cref="T:System.Collections.IList" /> que contém números <see cref="T:System.Double" /> de e para instâncias de <see cref="T:System.String" />.</summary>
	// Token: 0x020005BE RID: 1470
	public sealed class DoubleIListConverter : BaseIListConverter
	{
		// Token: 0x06004311 RID: 17169 RVA: 0x00104470 File Offset: 0x00103870
		internal sealed override object ConvertFromCore(ITypeDescriptorContext td, CultureInfo ci, string value)
		{
			this._tokenizer = new TokenizerHelper(value, '\0', ' ');
			List<double> list = new List<double>(Math.Min(256, value.Length / 6 + 1));
			while (this._tokenizer.NextToken())
			{
				list.Add(Convert.ToDouble(this._tokenizer.GetCurrentToken(), ci));
			}
			return list;
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x001044D0 File Offset: 0x001038D0
		internal override object ConvertToCore(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			IList<double> list = value as IList<double>;
			if (list == null)
			{
				throw base.GetConvertToException(value, destinationType);
			}
			StringBuilder stringBuilder = new StringBuilder(6 * list.Count);
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

		// Token: 0x04001858 RID: 6232
		private const int EstimatedCharCountPerItem = 6;
	}
}
