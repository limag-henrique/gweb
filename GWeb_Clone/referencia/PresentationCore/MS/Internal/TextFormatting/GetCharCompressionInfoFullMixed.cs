using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000721 RID: 1825
	// (Invoke) Token: 0x06004DF6 RID: 19958
	[SecurityCritical]
	internal unsafe delegate LsErr GetCharCompressionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsCharRunInfo* plscharrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplscompressionLeft, int** pplscompressionRight);
}
