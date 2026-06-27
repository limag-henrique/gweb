using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200071B RID: 1819
	// (Invoke) Token: 0x06004DDE RID: 19934
	internal delegate LsErr DrawStrikethrough(IntPtr pols, Plsrun plsrun, uint stType, ref LSPOINT ptOrigin, int stLength, int stThickness, LsTFlow textFlow, uint displayMode, ref LSRECT clipRect);
}
