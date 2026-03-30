using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000722 RID: 1826
	// (Invoke) Token: 0x06004DFA RID: 19962
	[SecurityCritical]
	internal unsafe delegate LsErr GetCharExpansionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsCharRunInfo* plscharrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplsexpansionLeft, int** pplsexpansionRight);
}
