using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200056A RID: 1386
	internal static class HandoffBehaviorEnum
	{
		// Token: 0x06004064 RID: 16484 RVA: 0x000FCC8C File Offset: 0x000FC08C
		[FriendAccessAllowed]
		internal static bool IsDefined(HandoffBehavior handoffBehavior)
		{
			return handoffBehavior >= HandoffBehavior.SnapshotAndReplace && handoffBehavior <= HandoffBehavior.Compose;
		}
	}
}
