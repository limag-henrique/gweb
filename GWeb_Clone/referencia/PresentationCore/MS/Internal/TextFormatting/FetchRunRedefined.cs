using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200070E RID: 1806
	// (Invoke) Token: 0x06004DAA RID: 19882
	[SecurityCritical]
	internal unsafe delegate LsErr FetchRunRedefined(IntPtr pols, int lscpFetch, int fIsStyle, IntPtr pstyle, char* pwchTextBuffer, int cchTextBuffer, ref int fIsBufferUsed, out char* pwchText, ref int cchText, ref int fIsHidden, ref LsChp lschp, ref IntPtr lsplsrun);
}
