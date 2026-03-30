using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200073F RID: 1855
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct LscbkRedefined
	{
		// Token: 0x04002294 RID: 8852
		[SecurityCritical]
		public FetchRunRedefined pfnFetchRunRedefined;

		// Token: 0x04002295 RID: 8853
		[SecurityCritical]
		public GetGlyphsRedefined pfnGetGlyphsRedefined;

		// Token: 0x04002296 RID: 8854
		[SecurityCritical]
		public FetchLineProps pfnFetchLineProps;
	}
}
