using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000720 RID: 1824
	// (Invoke) Token: 0x06004DF2 RID: 19954
	[SecurityCritical]
	internal unsafe delegate LsErr EnumTab(IntPtr pols, Plsrun plsrun, int cpFirst, char* pwchText, char tabLeader, LsTFlow lstFlow, int fReverseOrder, int fGeometryProvided, ref LSPOINT pptStart, ref LsHeights heights, int dupRun);
}
