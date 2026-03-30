using System;
using MS.Internal.KnownBoxes;

namespace System.Windows
{
	// Token: 0x020001BC RID: 444
	internal class FocusWithinProperty : ReverseInheritProperty
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00020324 File Offset: 0x0001F724
		internal FocusWithinProperty() : base(UIElement.IsKeyboardFocusWithinPropertyKey, CoreFlags.IsKeyboardFocusWithinCache, CoreFlags.IsKeyboardFocusWithinChanged)
		{
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00020348 File Offset: 0x0001F748
		internal override void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue)
		{
			DependencyPropertyChangedEventArgs args = new DependencyPropertyChangedEventArgs(UIElement.IsKeyboardFocusWithinProperty, BooleanBoxes.Box(oldValue), BooleanBoxes.Box(!oldValue));
			if (uie != null)
			{
				uie.RaiseIsKeyboardFocusWithinChanged(args);
				return;
			}
			if (ce != null)
			{
				ce.RaiseIsKeyboardFocusWithinChanged(args);
				return;
			}
			if (uie3D != null)
			{
				uie3D.RaiseIsKeyboardFocusWithinChanged(args);
			}
		}
	}
}
