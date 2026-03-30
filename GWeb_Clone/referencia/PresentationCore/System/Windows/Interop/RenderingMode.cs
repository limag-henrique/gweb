using System;

namespace System.Windows.Interop
{
	// Token: 0x02000324 RID: 804
	internal enum RenderingMode
	{
		// Token: 0x04000E60 RID: 3680
		Default,
		// Token: 0x04000E61 RID: 3681
		Software,
		// Token: 0x04000E62 RID: 3682
		Hardware,
		// Token: 0x04000E63 RID: 3683
		HardwareReference = 16777218,
		// Token: 0x04000E64 RID: 3684
		DisableMultimonDisplayClipping = 16384,
		// Token: 0x04000E65 RID: 3685
		IsDisableMultimonDisplayClippingValid = 32768,
		// Token: 0x04000E66 RID: 3686
		DisableDirtyRectangles = 65536
	}
}
