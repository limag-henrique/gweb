using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000723 RID: 1827
	// (Invoke) Token: 0x06004DFE RID: 19966
	[SecurityCritical]
	internal unsafe delegate LsErr GetGlyphCompressionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsGlyphRunInfo* plsglyphrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplscompressionLeft, int** pplscompressionRight);
}
