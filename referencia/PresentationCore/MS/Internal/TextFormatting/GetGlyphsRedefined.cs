using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200071C RID: 1820
	// (Invoke) Token: 0x06004DE2 RID: 19938
	[SecurityCritical]
	internal unsafe delegate LsErr GetGlyphsRedefined(IntPtr pols, IntPtr* plsplsruns, int* pcchPlsrun, int plsrunCount, char* pwchText, int cchText, LsTFlow textFlow, ushort* puGlyphsBuffer, uint* piGlyphPropsBuffer, int cgiGlyphBuffers, ref int fIsGlyphBuffersUsed, ushort* puClusterMap, ushort* puCharProperties, int* pfCanGlyphAlone, ref int glyphCount);
}
