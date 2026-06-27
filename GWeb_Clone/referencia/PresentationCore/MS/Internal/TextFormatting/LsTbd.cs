using System;
using System.Runtime.InteropServices;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200073B RID: 1851
	internal struct LsTbd
	{
		// Token: 0x0400227F RID: 8831
		public LsKTab lskt;

		// Token: 0x04002280 RID: 8832
		public int ur;

		// Token: 0x04002281 RID: 8833
		[MarshalAs(UnmanagedType.U2)]
		public char wchTabLeader;

		// Token: 0x04002282 RID: 8834
		[MarshalAs(UnmanagedType.U2)]
		public char wchCharTab;
	}
}
