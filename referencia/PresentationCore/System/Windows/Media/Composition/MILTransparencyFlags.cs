using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000625 RID: 1573
	[Flags]
	internal enum MILTransparencyFlags
	{
		// Token: 0x04001A87 RID: 6791
		Opaque = 0,
		// Token: 0x04001A88 RID: 6792
		ConstantAlpha = 1,
		// Token: 0x04001A89 RID: 6793
		PerPixelAlpha = 2,
		// Token: 0x04001A8A RID: 6794
		ColorKey = 4,
		// Token: 0x04001A8B RID: 6795
		FORCE_DWORD = -1
	}
}
