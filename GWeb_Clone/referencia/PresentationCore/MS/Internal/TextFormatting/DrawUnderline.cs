using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200071A RID: 1818
	// (Invoke) Token: 0x06004DDA RID: 19930
	internal delegate LsErr DrawUnderline(IntPtr pols, Plsrun plsrun, uint ulType, ref LSPOINT ptOrigin, int ulLength, int ulThickness, LsTFlow textFlow, uint displayMode, ref LSRECT clipRect);
}
