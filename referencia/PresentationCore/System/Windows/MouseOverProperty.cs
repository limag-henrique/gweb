using System;
using System.Windows.Input;

namespace System.Windows
{
	// Token: 0x020001CD RID: 461
	internal class MouseOverProperty : ReverseInheritProperty
	{
		// Token: 0x06000C39 RID: 3129 RVA: 0x0002ED14 File Offset: 0x0002E114
		internal MouseOverProperty() : base(UIElement.IsMouseOverPropertyKey, CoreFlags.IsMouseOverCache, CoreFlags.IsMouseOverChanged)
		{
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002ED38 File Offset: 0x0002E138
		internal override void FireNotifications(UIElement uie, ContentElement ce, UIElement3D uie3D, bool oldValue)
		{
			bool flag = false;
			if (uie != null)
			{
				flag = ((!oldValue && uie.IsMouseOver) || (oldValue && !uie.IsMouseOver));
			}
			else if (ce != null)
			{
				flag = ((!oldValue && ce.IsMouseOver) || (oldValue && !ce.IsMouseOver));
			}
			else if (uie3D != null)
			{
				flag = ((!oldValue && uie3D.IsMouseOver) || (oldValue && !uie3D.IsMouseOver));
			}
			if (flag)
			{
				MouseEventArgs mouseEventArgs = new MouseEventArgs(Mouse.PrimaryDevice, Environment.TickCount, Mouse.PrimaryDevice.StylusDevice);
				mouseEventArgs.RoutedEvent = (oldValue ? Mouse.MouseLeaveEvent : Mouse.MouseEnterEvent);
				if (uie != null)
				{
					uie.RaiseEvent(mouseEventArgs);
					return;
				}
				if (ce != null)
				{
					ce.RaiseEvent(mouseEventArgs);
					return;
				}
				if (uie3D != null)
				{
					uie3D.RaiseEvent(mouseEventArgs);
				}
			}
		}
	}
}
