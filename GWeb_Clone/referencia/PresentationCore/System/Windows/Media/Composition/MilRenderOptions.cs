using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000632 RID: 1586
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MilRenderOptions
	{
		// Token: 0x04001AE1 RID: 6881
		internal MilRenderOptionFlags Flags;

		// Token: 0x04001AE2 RID: 6882
		internal EdgeMode EdgeMode;

		// Token: 0x04001AE3 RID: 6883
		internal MilCompositingMode CompositingMode;

		// Token: 0x04001AE4 RID: 6884
		internal BitmapScalingMode BitmapScalingMode;

		// Token: 0x04001AE5 RID: 6885
		internal ClearTypeHint ClearTypeHint;

		// Token: 0x04001AE6 RID: 6886
		internal TextRenderingMode TextRenderingMode;

		// Token: 0x04001AE7 RID: 6887
		internal TextHintingMode TextHintingMode;
	}
}
