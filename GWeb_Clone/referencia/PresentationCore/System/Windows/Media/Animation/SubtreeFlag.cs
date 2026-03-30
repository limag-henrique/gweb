using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200057B RID: 1403
	[Flags]
	[FriendAccessAllowed]
	internal enum SubtreeFlag
	{
		// Token: 0x040017C0 RID: 6080
		Reset = 1,
		// Token: 0x040017C1 RID: 6081
		ProcessRoot = 2,
		// Token: 0x040017C2 RID: 6082
		SkipSubtree = 4
	}
}
