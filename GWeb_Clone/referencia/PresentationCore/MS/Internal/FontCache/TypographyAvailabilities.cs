using System;

namespace MS.Internal.FontCache
{
	// Token: 0x02000778 RID: 1912
	[Flags]
	internal enum TypographyAvailabilities
	{
		// Token: 0x040024B1 RID: 9393
		None = 0,
		// Token: 0x040024B2 RID: 9394
		Available = 1,
		// Token: 0x040024B3 RID: 9395
		IdeoTypographyAvailable = 2,
		// Token: 0x040024B4 RID: 9396
		FastTextTypographyAvailable = 4,
		// Token: 0x040024B5 RID: 9397
		FastTextMajorLanguageLocalizedFormAvailable = 8,
		// Token: 0x040024B6 RID: 9398
		FastTextExtraLanguageLocalizedFormAvailable = 16
	}
}
