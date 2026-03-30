using System;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal.KnownBoxes
{
	// Token: 0x020006FA RID: 1786
	[FriendAccessAllowed]
	internal static class FillRuleBoxes
	{
		// Token: 0x06004D07 RID: 19719 RVA: 0x0012FB90 File Offset: 0x0012EF90
		internal static object Box(FillRule value)
		{
			if (value == FillRule.Nonzero)
			{
				return FillRuleBoxes.NonzeroBox;
			}
			return FillRuleBoxes.EvenOddBox;
		}

		// Token: 0x0400216A RID: 8554
		internal static object EvenOddBox = FillRule.EvenOdd;

		// Token: 0x0400216B RID: 8555
		internal static object NonzeroBox = FillRule.Nonzero;
	}
}
