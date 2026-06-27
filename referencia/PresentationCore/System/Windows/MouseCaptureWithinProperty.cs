using System;
using MS.Internal.KnownBoxes;

namespace System.Windows
{
	// Token: 0x020001CC RID: 460
	internal class MouseCaptureWithinProperty : ReverseInheritProperty
	{
		// Token: 0x06000C37 RID: 3127 RVA: 0x0002ECA4 File Offset: 0x0002E0A4
		internal MouseCaptureWithinProperty() : base(UIElement.IsMouseCaptureWithinPropertyKey, CoreFlags.IsMouseCaptureWithinCache, CoreFlags.IsMouseCaptureWithinChanged)
		{
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002ECC8 File Offset: 0x0002E0C8
		internal override void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue)
		{
			DependencyPropertyChangedEventArgs args = new DependencyPropertyChangedEventArgs(UIElement.IsMouseCaptureWithinProperty, BooleanBoxes.Box(oldValue), BooleanBoxes.Box(!oldValue));
			if (uie != null)
			{
				uie.RaiseIsMouseCaptureWithinChanged(args);
				return;
			}
			if (ce != null)
			{
				ce.RaiseIsMouseCaptureWithinChanged(args);
				return;
			}
			if (uie3D != null)
			{
				uie3D.RaiseIsMouseCaptureWithinChanged(args);
			}
		}
	}
}
