using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000713 RID: 1811
	// (Invoke) Token: 0x06004DBE RID: 19902
	internal delegate LsErr FInterruptShaping(IntPtr pols, LsTFlow textFlow, Plsrun firstPlsrun, Plsrun secondPlsrun, ref int fIsInterruptOk);
}
