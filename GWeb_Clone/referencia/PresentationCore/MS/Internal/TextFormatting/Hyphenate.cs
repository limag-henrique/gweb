using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000716 RID: 1814
	// (Invoke) Token: 0x06004DCA RID: 19914
	internal delegate LsErr Hyphenate(IntPtr pols, int fLastHyphenationFound, int lscpLastHyphenation, ref LsHyph lastHyphenation, int lscpBeginWord, int lscpExceed, ref int fHyphenFound, ref int lscpHyphen, ref LsHyph plsHyph);
}
