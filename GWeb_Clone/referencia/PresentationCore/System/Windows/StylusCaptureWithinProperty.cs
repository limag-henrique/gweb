using System;
using MS.Internal.KnownBoxes;

namespace System.Windows
{
	// Token: 0x020001DF RID: 479
	internal class StylusCaptureWithinProperty : ReverseInheritProperty
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x0003071C File Offset: 0x0002FB1C
		internal StylusCaptureWithinProperty() : base(UIElement.IsStylusCaptureWithinPropertyKey, CoreFlags.IsStylusCaptureWithinCache, CoreFlags.IsStylusCaptureWithinChanged)
		{
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00030740 File Offset: 0x0002FB40
		internal override void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue)
		{
			DependencyPropertyChangedEventArgs args = new DependencyPropertyChangedEventArgs(UIElement.IsStylusCaptureWithinProperty, BooleanBoxes.Box(oldValue), BooleanBoxes.Box(!oldValue));
			if (uie != null)
			{
				uie.RaiseIsStylusCaptureWithinChanged(args);
				return;
			}
			if (ce != null)
			{
				ce.RaiseIsStylusCaptureWithinChanged(args);
				return;
			}
			if (uie3D != null)
			{
				uie3D.RaiseIsStylusCaptureWithinChanged(args);
			}
		}
	}
}
