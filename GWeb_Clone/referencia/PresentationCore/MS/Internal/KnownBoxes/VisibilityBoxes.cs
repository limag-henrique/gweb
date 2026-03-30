using System;
using System.Windows;
using MS.Internal.PresentationCore;

namespace MS.Internal.KnownBoxes
{
	// Token: 0x020006FB RID: 1787
	[FriendAccessAllowed]
	internal static class VisibilityBoxes
	{
		// Token: 0x06004D09 RID: 19721 RVA: 0x0012FBD0 File Offset: 0x0012EFD0
		internal static object Box(Visibility value)
		{
			if (value == Visibility.Visible)
			{
				return VisibilityBoxes.VisibleBox;
			}
			if (value == Visibility.Hidden)
			{
				return VisibilityBoxes.HiddenBox;
			}
			return VisibilityBoxes.CollapsedBox;
		}

		// Token: 0x0400216C RID: 8556
		internal static object VisibleBox = Visibility.Visible;

		// Token: 0x0400216D RID: 8557
		internal static object HiddenBox = Visibility.Hidden;

		// Token: 0x0400216E RID: 8558
		internal static object CollapsedBox = Visibility.Collapsed;
	}
}
