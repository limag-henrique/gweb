using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace MS.Internal.FontFace
{
	// Token: 0x02000765 RID: 1893
	internal static class FontDifferentiator
	{
		// Token: 0x06004FEE RID: 20462 RVA: 0x0013FDD0 File Offset: 0x0013F1D0
		internal static IDictionary<XmlLanguage, string> ConstructFaceNamesByStyleWeightStretch(FontStyle style, FontWeight weight, FontStretch stretch)
		{
			string value = FontDifferentiator.BuildFaceName(style, weight, stretch);
			return new Dictionary<XmlLanguage, string>(1)
			{
				{
					XmlLanguage.GetLanguage("en-us"),
					value
				}
			};
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0013FE00 File Offset: 0x0013F200
		private static string BuildFaceName(FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string value = "Regular";
			if (fontWeight != FontWeights.Normal)
			{
				text2 = ((IFormattable)fontWeight).ToString(null, CultureInfo.InvariantCulture);
			}
			if (fontStretch != FontStretches.Normal)
			{
				text3 = ((IFormattable)fontStretch).ToString(null, CultureInfo.InvariantCulture);
			}
			if (fontStyle != FontStyles.Normal)
			{
				text = ((IFormattable)fontStyle).ToString(null, CultureInfo.InvariantCulture);
			}
			StringBuilder stringBuilder = new StringBuilder(7);
			if (text3 != null)
			{
				stringBuilder.Append(text3);
			}
			if (text2 != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(text2);
			}
			if (text != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(text);
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}
	}
}
