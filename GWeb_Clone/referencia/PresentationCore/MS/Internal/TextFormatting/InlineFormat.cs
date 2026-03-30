using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000726 RID: 1830
	// (Invoke) Token: 0x06004E0A RID: 19978
	internal delegate LsErr InlineFormat(IntPtr pols, Plsrun plsrun, int lscpInline, int currentPosition, int rightMargin, ref ObjDim pobjDim, out int fFirstRealOnLine, out int fPenPositionUsed, out LsBrkCond breakBefore, out LsBrkCond breakAfter);
}
