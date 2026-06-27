using System;
using System.Security;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200071F RID: 1823
	// (Invoke) Token: 0x06004DEE RID: 19950
	[SecurityCritical]
	internal unsafe delegate LsErr EnumText(IntPtr pols, Plsrun plsrun, int cpFirst, int dcp, char* pwchText, int cchText, LsTFlow lstFlow, int fReverseOrder, int fGeometryProvided, ref LSPOINT pptStart, ref LsHeights pheights, int dupRun, int glyphBaseRun, int* charWidths, ushort* pClusterMap, ushort* characterProperties, ushort* puglyphs, int* pGlyphAdvances, GlyphOffset* pGlyphOffsets, uint* pGlyphProperties, int glyphCount);
}
