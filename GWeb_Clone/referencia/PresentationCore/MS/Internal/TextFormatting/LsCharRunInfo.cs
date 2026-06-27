using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200074E RID: 1870
	internal struct LsCharRunInfo
	{
		// Token: 0x04002386 RID: 9094
		public Plsrun plsrun;

		// Token: 0x04002387 RID: 9095
		[SecurityCritical]
		public unsafe char* pwch;

		// Token: 0x04002388 RID: 9096
		[SecurityCritical]
		public unsafe int* rgduNominalWidth;

		// Token: 0x04002389 RID: 9097
		[SecurityCritical]
		public unsafe int* rgduChangeLeft;

		// Token: 0x0400238A RID: 9098
		[SecurityCritical]
		public unsafe int* rgduChangeRight;

		// Token: 0x0400238B RID: 9099
		public int cwch;
	}
}
