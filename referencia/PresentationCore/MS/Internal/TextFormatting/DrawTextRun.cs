using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000712 RID: 1810
	// (Invoke) Token: 0x06004DBA RID: 19898
	[SecurityCritical]
	internal unsafe delegate LsErr DrawTextRun(IntPtr pols, Plsrun plsrun, ref LSPOINT ptText, char* runText, int* charWidths, int cchText, LsTFlow textFlow, uint displayMode, ref LSPOINT ptRun, ref LsHeights lsHeights, int dupRun, ref LSRECT clipRect);
}
