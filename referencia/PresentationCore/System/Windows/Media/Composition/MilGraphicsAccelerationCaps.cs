using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000631 RID: 1585
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MilGraphicsAccelerationCaps
	{
		// Token: 0x04001AD7 RID: 6871
		internal int TierValue;

		// Token: 0x04001AD8 RID: 6872
		internal int HasWDDMSupport;

		// Token: 0x04001AD9 RID: 6873
		internal uint PixelShaderVersion;

		// Token: 0x04001ADA RID: 6874
		internal uint VertexShaderVersion;

		// Token: 0x04001ADB RID: 6875
		internal uint MaxTextureWidth;

		// Token: 0x04001ADC RID: 6876
		internal uint MaxTextureHeight;

		// Token: 0x04001ADD RID: 6877
		internal int WindowCompatibleMode;

		// Token: 0x04001ADE RID: 6878
		internal uint BitsPerPixel;

		// Token: 0x04001ADF RID: 6879
		internal uint HasSSE2Support;

		// Token: 0x04001AE0 RID: 6880
		internal uint MaxPixelShader30InstructionSlots;
	}
}
