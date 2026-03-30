using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using MS.Internal;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte uma coleção <see cref="T:System.Collections.IList" /> de valores <see cref="T:System.Windows.Point" /> de e para instâncias de <see cref="T:System.String" />.</summary>
	// Token: 0x020005C1 RID: 1473
	public sealed class PointIListConverter : BaseIListConverter
	{
		// Token: 0x0600431A RID: 17178 RVA: 0x0010470C File Offset: 0x00103B0C
		internal override object ConvertFromCore(ITypeDescriptorContext td, CultureInfo ci, string value)
		{
			this._tokenizer = new TokenizerHelper(value, '\0', ' ');
			List<Point> list = new List<Point>(Math.Min(256, value.Length / 12 + 1));
			while (this._tokenizer.NextToken())
			{
				list.Add((Point)this.converter.ConvertFrom(td, ci, this._tokenizer.GetCurrentToken()));
			}
			return list;
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x00104778 File Offset: 0x00103B78
		internal override object ConvertToCore(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			IList<Point> list = value as IList<Point>;
			if (list == null)
			{
				throw base.GetConvertToException(value, destinationType);
			}
			StringBuilder stringBuilder = new StringBuilder(12 * list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append((string)this.converter.ConvertTo(context, culture, list[i], typeof(string)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400185B RID: 6235
		private PointConverter converter = new PointConverter();

		// Token: 0x0400185C RID: 6236
		private const int EstimatedCharCountPerItem = 12;
	}
}
