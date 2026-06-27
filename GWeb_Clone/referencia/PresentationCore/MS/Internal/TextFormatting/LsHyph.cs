using System;
using System.Runtime.InteropServices;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200073E RID: 1854
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct LsHyph
	{
		// Token: 0x04002290 RID: 8848
		public LsKysr kysr;

		// Token: 0x04002291 RID: 8849
		public char wchYsr;

		// Token: 0x04002292 RID: 8850
		public char wchYsr2;

		// Token: 0x04002293 RID: 8851
		public LsHyphenQuality lshq;
	}
}
