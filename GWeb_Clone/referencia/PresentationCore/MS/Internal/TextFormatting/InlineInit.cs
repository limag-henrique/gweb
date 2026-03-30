using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200074F RID: 1871
	internal struct InlineInit
	{
		// Token: 0x0400238C RID: 9100
		public uint dwVersion;

		// Token: 0x0400238D RID: 9101
		[SecurityCritical]
		public InlineFormat pfnFormat;

		// Token: 0x0400238E RID: 9102
		[SecurityCritical]
		public InlineDraw pfnDraw;
	}
}
