using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000724 RID: 1828
	// (Invoke) Token: 0x06004E02 RID: 19970
	[SecurityCritical]
	internal unsafe delegate LsErr GetGlyphExpansionInfoFullMixed(IntPtr pols, LsDevice device, LsTFlow textFlow, LsGlyphRunInfo* plsglyphrunInfo, LsNeighborInfo* plsneighborInfoLeft, LsNeighborInfo* plsneighborInfoRight, int maxPriorityLevel, int** pplsexpansionLeft, int** pplsexpansionRight, LsExpType* plsexptype, int* pduMinInk);
}
