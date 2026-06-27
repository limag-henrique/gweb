using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000710 RID: 1808
	// (Invoke) Token: 0x06004DB2 RID: 19890
	[SecurityCritical]
	internal unsafe delegate LsErr GetRunCharWidths(IntPtr pols, Plsrun plsrun, LsDevice device, char* runText, int cchRun, int maxWidth, LsTFlow textFlow, int* charWidths, ref int totalWidth, ref int cchProcessed);
}
