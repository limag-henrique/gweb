using System;

namespace System.Windows.Media
{
	// Token: 0x0200042C RID: 1068
	[Flags]
	internal enum PixelFormatFlags
	{
		// Token: 0x040013F9 RID: 5113
		BitsPerPixelMask = 255,
		// Token: 0x040013FA RID: 5114
		BitsPerPixelUndefined = 0,
		// Token: 0x040013FB RID: 5115
		BitsPerPixel1 = 1,
		// Token: 0x040013FC RID: 5116
		BitsPerPixel2 = 2,
		// Token: 0x040013FD RID: 5117
		BitsPerPixel4 = 4,
		// Token: 0x040013FE RID: 5118
		BitsPerPixel8 = 8,
		// Token: 0x040013FF RID: 5119
		BitsPerPixel16 = 16,
		// Token: 0x04001400 RID: 5120
		BitsPerPixel24 = 24,
		// Token: 0x04001401 RID: 5121
		BitsPerPixel32 = 32,
		// Token: 0x04001402 RID: 5122
		BitsPerPixel48 = 48,
		// Token: 0x04001403 RID: 5123
		BitsPerPixel64 = 64,
		// Token: 0x04001404 RID: 5124
		BitsPerPixel96 = 96,
		// Token: 0x04001405 RID: 5125
		BitsPerPixel128 = 128,
		// Token: 0x04001406 RID: 5126
		IsGray = 256,
		// Token: 0x04001407 RID: 5127
		IsCMYK = 512,
		// Token: 0x04001408 RID: 5128
		IsSRGB = 1024,
		// Token: 0x04001409 RID: 5129
		IsScRGB = 2048,
		// Token: 0x0400140A RID: 5130
		Premultiplied = 4096,
		// Token: 0x0400140B RID: 5131
		ChannelOrderMask = 122880,
		// Token: 0x0400140C RID: 5132
		ChannelOrderRGB = 8192,
		// Token: 0x0400140D RID: 5133
		ChannelOrderBGR = 16384,
		// Token: 0x0400140E RID: 5134
		ChannelOrderARGB = 32768,
		// Token: 0x0400140F RID: 5135
		ChannelOrderABGR = 65536,
		// Token: 0x04001410 RID: 5136
		Palettized = 131072,
		// Token: 0x04001411 RID: 5137
		NChannelAlpha = 262144,
		// Token: 0x04001412 RID: 5138
		IsNChannel = 524288
	}
}
