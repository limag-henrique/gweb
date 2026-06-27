using System;
using System.Windows;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal.Media
{
	// Token: 0x020006F6 RID: 1782
	[FriendAccessAllowed]
	internal static class TextOptionsInternal
	{
		// Token: 0x06004CC9 RID: 19657 RVA: 0x0012EDC8 File Offset: 0x0012E1C8
		[FriendAccessAllowed]
		public static void SetTextHintingMode(DependencyObject element, TextHintingMode value)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			element.SetValue(TextOptionsInternal.TextHintingModeProperty, value);
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0012EDF4 File Offset: 0x0012E1F4
		[FriendAccessAllowed]
		public static TextHintingMode GetTextHintingMode(DependencyObject element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (TextHintingMode)element.GetValue(TextOptionsInternal.TextHintingModeProperty);
		}

		// Token: 0x0400215B RID: 8539
		[FriendAccessAllowed]
		internal static readonly DependencyProperty TextHintingModeProperty = DependencyProperty.RegisterAttached("TextHintingMode", typeof(TextHintingMode), typeof(TextOptionsInternal), new UIPropertyMetadata(TextHintingMode.Auto), new ValidateValueCallback(System.Windows.Media.ValidateEnums.IsTextHintingModeValid));
	}
}
