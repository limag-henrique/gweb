using System;
using System.Security;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200071E RID: 1822
	// (Invoke) Token: 0x06004DEA RID: 19946
	[SecurityCritical]
	internal unsafe delegate LsErr DrawGlyphs(IntPtr pols, Plsrun plsrun, char* pwchText, ushort* puClusterMap, ushort* puCharProperties, int cchText, ushort* puGlyphs, int* piJustifiedGlyphAdvances, int* puGlyphAdvances, GlyphOffset* piiGlyphOffsets, uint* piGlyphProperties, LsExpType* plsExpType, int glyphCount, LsTFlow textFlow, uint displayMode, ref LSPOINT origin, ref LsHeights lsHeights, int runWidth, ref LSRECT clippingRect);
}
