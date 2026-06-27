using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000717 RID: 1815
	// (Invoke) Token: 0x06004DCE RID: 19918
	internal delegate LsErr GetNextHyphenOpp(IntPtr pols, int lscpStartSearch, int lsdcpSearch, ref int fHyphenFound, ref int lscpHyphen, ref LsHyph lsHyph);
}
