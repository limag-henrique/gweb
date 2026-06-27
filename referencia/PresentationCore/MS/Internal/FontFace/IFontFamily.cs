using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace MS.Internal.FontFace
{
	// Token: 0x0200076A RID: 1898
	internal interface IFontFamily
	{
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06005007 RID: 20487
		IDictionary<XmlLanguage, string> Names { get; }

		// Token: 0x06005008 RID: 20488
		double Baseline(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode);

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06005009 RID: 20489
		double BaselineDesign { get; }

		// Token: 0x0600500A RID: 20490
		double LineSpacing(double emSize, double toReal, double pixelsPerDip, TextFormattingMode textFormattingMode);

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600500B RID: 20491
		double LineSpacingDesign { get; }

		// Token: 0x0600500C RID: 20492
		ITypefaceMetrics GetTypefaceMetrics(FontStyle style, FontWeight weight, FontStretch stretch);

		// Token: 0x0600500D RID: 20493
		IDeviceFont GetDeviceFont(FontStyle style, FontWeight weight, FontStretch stretch);

		// Token: 0x0600500E RID: 20494
		bool GetMapTargetFamilyNameAndScale(CharacterBufferRange unicodeString, CultureInfo culture, CultureInfo digitCulture, double defaultSizeInEm, out int cchAdvance, out string targetFamilyName, out double scaleInEm);

		// Token: 0x0600500F RID: 20495
		ICollection<Typeface> GetTypefaces(FontFamilyIdentifier familyIdentifier);
	}
}
