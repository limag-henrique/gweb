using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200074B RID: 1867
	internal struct LsBreaks
	{
		// Token: 0x0400236F RID: 9071
		public int cBreaks;

		// Token: 0x04002370 RID: 9072
		[SecurityCritical]
		public unsafe LsLInfo* plslinfoArray;

		// Token: 0x04002371 RID: 9073
		[SecurityCritical]
		public unsafe IntPtr* plinepenaltyArray;

		// Token: 0x04002372 RID: 9074
		[SecurityCritical]
		public unsafe IntPtr* pplolineArray;
	}
}
