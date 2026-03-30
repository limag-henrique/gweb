using System;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Media;

namespace MS.Internal.FontFace
{
	// Token: 0x0200076B RID: 1899
	internal interface ITypefaceMetrics
	{
		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06005010 RID: 20496
		double XHeight { get; }

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06005011 RID: 20497
		double CapsHeight { get; }

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06005012 RID: 20498
		double UnderlinePosition { get; }

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06005013 RID: 20499
		double UnderlineThickness { get; }

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06005014 RID: 20500
		double StrikethroughPosition { get; }

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06005015 RID: 20501
		double StrikethroughThickness { get; }

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06005016 RID: 20502
		bool Symbol { get; }

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06005017 RID: 20503
		StyleSimulations StyleSimulations { get; }

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06005018 RID: 20504
		IDictionary<XmlLanguage, string> AdjustedFaceNames { get; }
	}
}
