using System;

namespace MS.Internal
{
	// Token: 0x02000664 RID: 1636
	internal enum DitherType
	{
		// Token: 0x04001C34 RID: 7220
		DitherTypeNone,
		// Token: 0x04001C35 RID: 7221
		DitherTypeSolid = 0,
		// Token: 0x04001C36 RID: 7222
		DitherTypeOrdered4x4,
		// Token: 0x04001C37 RID: 7223
		DitherTypeOrdered8x8,
		// Token: 0x04001C38 RID: 7224
		DitherTypeOrdered16x16,
		// Token: 0x04001C39 RID: 7225
		DitherTypeSpiral4x4,
		// Token: 0x04001C3A RID: 7226
		DitherTypeSpiral8x8,
		// Token: 0x04001C3B RID: 7227
		DitherTypeDualSpiral4x4,
		// Token: 0x04001C3C RID: 7228
		DitherTypeDualSpiral8x8,
		// Token: 0x04001C3D RID: 7229
		DitherTypeErrorDiffusion
	}
}
