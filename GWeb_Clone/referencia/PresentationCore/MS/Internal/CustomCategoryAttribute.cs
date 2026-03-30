using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x0200068A RID: 1674
	internal sealed class CustomCategoryAttribute : CategoryAttribute
	{
		// Token: 0x060049B6 RID: 18870 RVA: 0x0011F298 File Offset: 0x0011E698
		internal CustomCategoryAttribute()
		{
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0011F2AC File Offset: 0x0011E6AC
		internal CustomCategoryAttribute(string category) : base(category)
		{
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0011F2C0 File Offset: 0x0011E6C0
		protected override string GetLocalizedString(string value)
		{
			string text = SR.Get(value);
			if (text != null)
			{
				return text;
			}
			return value;
		}
	}
}
