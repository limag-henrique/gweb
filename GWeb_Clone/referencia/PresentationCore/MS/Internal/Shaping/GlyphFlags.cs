using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006B7 RID: 1719
	[Flags]
	internal enum GlyphFlags : ushort
	{
		// Token: 0x04001FEE RID: 8174
		Unassigned = 0,
		// Token: 0x04001FEF RID: 8175
		Base = 1,
		// Token: 0x04001FF0 RID: 8176
		Ligature = 2,
		// Token: 0x04001FF1 RID: 8177
		Mark = 3,
		// Token: 0x04001FF2 RID: 8178
		Component = 4,
		// Token: 0x04001FF3 RID: 8179
		Unresolved = 7,
		// Token: 0x04001FF4 RID: 8180
		GlyphTypeMask = 7,
		// Token: 0x04001FF5 RID: 8181
		Substituted = 16,
		// Token: 0x04001FF6 RID: 8182
		Positioned = 32,
		// Token: 0x04001FF7 RID: 8183
		NotChanged = 0,
		// Token: 0x04001FF8 RID: 8184
		CursiveConnected = 64,
		// Token: 0x04001FF9 RID: 8185
		ClusterStart = 256,
		// Token: 0x04001FFA RID: 8186
		Diacritic = 512,
		// Token: 0x04001FFB RID: 8187
		ZeroWidth = 1024,
		// Token: 0x04001FFC RID: 8188
		Missing = 2048,
		// Token: 0x04001FFD RID: 8189
		InvalidBase = 4096
	}
}
