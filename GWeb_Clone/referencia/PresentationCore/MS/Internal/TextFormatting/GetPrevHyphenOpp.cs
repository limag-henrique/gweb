using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000718 RID: 1816
	// (Invoke) Token: 0x06004DD2 RID: 19922
	internal delegate LsErr GetPrevHyphenOpp(IntPtr pols, int lscpStartSearch, int lsdcpSearch, ref int fHyphenFound, ref int lscpHyphen, ref LsHyph lsHyph);
}
