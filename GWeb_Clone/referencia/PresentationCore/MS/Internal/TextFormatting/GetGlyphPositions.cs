using System;
using System.Security;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200071D RID: 1821
	// (Invoke) Token: 0x06004DE6 RID: 19942
	[SecurityCritical]
	internal unsafe delegate LsErr GetGlyphPositions(IntPtr pols, IntPtr* plsplsruns, int* pcchPlsrun, int plsrunCount, LsDevice device, char* pwchText, ushort* puClusterMap, ushort* puCharProperties, int cchText, ushort* puGlyphs, uint* piGlyphProperties, int glyphCount, LsTFlow textFlow, int* piGlyphAdvances, GlyphOffset* piiGlyphOffsets);
}
