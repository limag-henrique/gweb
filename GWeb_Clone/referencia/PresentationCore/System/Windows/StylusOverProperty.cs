using System;
using System.Windows.Input;

namespace System.Windows
{
	// Token: 0x020001E0 RID: 480
	internal class StylusOverProperty : ReverseInheritProperty
	{
		// Token: 0x06000CD0 RID: 3280 RVA: 0x0003078C File Offset: 0x0002FB8C
		internal StylusOverProperty() : base(UIElement.IsStylusOverPropertyKey, CoreFlags.IsStylusOverCache, CoreFlags.IsStylusOverChanged)
		{
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000307B0 File Offset: 0x0002FBB0
		internal override void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue)
		{
			if (Stylus.CurrentStylusDevice == null)
			{
				return;
			}
			StylusEventArgs stylusEventArgs = new StylusEventArgs(Stylus.CurrentStylusDevice, Environment.TickCount);
			stylusEventArgs.RoutedEvent = (oldValue ? Stylus.StylusLeaveEvent : Stylus.StylusEnterEvent);
			if (uie != null)
			{
				uie.RaiseEvent(stylusEventArgs);
				return;
			}
			if (ce != null)
			{
				ce.RaiseEvent(stylusEventArgs);
				return;
			}
			if (uie3D != null)
			{
				uie3D.RaiseEvent(stylusEventArgs);
			}
		}
	}
}
