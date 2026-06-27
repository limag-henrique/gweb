using System;
using System.Security;
using System.Windows.Input;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000681 RID: 1665
	[FriendAccessAllowed]
	internal static class CommandLibraryHelper
	{
		// Token: 0x0600496C RID: 18796 RVA: 0x0011E708 File Offset: 0x0011DB08
		internal static RoutedUICommand CreateUICommand(string name, Type ownerType, byte commandId, PermissionSet ps)
		{
			RoutedUICommand routedUICommand;
			if (ps != null)
			{
				routedUICommand = new SecureUICommand(ps, name, ownerType, commandId);
			}
			else
			{
				routedUICommand = new RoutedUICommand(name, ownerType, commandId);
			}
			routedUICommand.AreInputGesturesDelayLoaded = true;
			return routedUICommand;
		}
	}
}
