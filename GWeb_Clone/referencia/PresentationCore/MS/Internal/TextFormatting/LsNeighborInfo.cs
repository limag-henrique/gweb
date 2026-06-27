using System;
using System.Runtime.InteropServices;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200074C RID: 1868
	internal struct LsNeighborInfo
	{
		// Token: 0x04002373 RID: 9075
		public uint fNeighborIsPresent;

		// Token: 0x04002374 RID: 9076
		public uint fNeighborIsText;

		// Token: 0x04002375 RID: 9077
		public Plsrun plsrun;

		// Token: 0x04002376 RID: 9078
		[MarshalAs(UnmanagedType.U2)]
		public char wch;

		// Token: 0x04002377 RID: 9079
		public uint fGlyphBased;

		// Token: 0x04002378 RID: 9080
		public ushort chprop;

		// Token: 0x04002379 RID: 9081
		public ushort gindex;

		// Token: 0x0400237A RID: 9082
		public uint gprop;
	}
}
