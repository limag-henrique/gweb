using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Media.Converters
{
	/// <summary>Converte os membros de uma coleção <see cref="T:System.Collections.IList" /> de caracteres Unicode de e para instâncias de <see cref="T:System.String" />.</summary>
	// Token: 0x020005C2 RID: 1474
	public sealed class CharIListConverter : BaseIListConverter
	{
		// Token: 0x0600431D RID: 17181 RVA: 0x0010481C File Offset: 0x00103C1C
		internal override object ConvertFromCore(ITypeDescriptorContext td, CultureInfo ci, string value)
		{
			return value.ToCharArray();
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x00104830 File Offset: 0x00103C30
		internal override object ConvertToCore(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			IList<char> list = value as IList<char>;
			if (list == null)
			{
				throw base.GetConvertToException(value, destinationType);
			}
			char[] array = new char[list.Count];
			list.CopyTo(array, 0);
			return new string(array);
		}
	}
}
