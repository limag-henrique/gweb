using System;

namespace System.Windows.Ink
{
	// Token: 0x0200034E RID: 846
	internal static class StylusTipHelper
	{
		// Token: 0x06001C70 RID: 7280 RVA: 0x00073AC4 File Offset: 0x00072EC4
		internal static bool IsDefined(StylusTip stylusTip)
		{
			return stylusTip >= StylusTip.Rectangle && stylusTip <= StylusTip.Ellipse;
		}
	}
}
