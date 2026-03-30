using System;

namespace System.Windows.Media
{
	// Token: 0x0200040D RID: 1037
	internal static class HTFBInterpreter
	{
		// Token: 0x060029FF RID: 10751 RVA: 0x000A8AE4 File Offset: 0x000A7EE4
		internal static bool DoHitTest(HitTestFilterBehavior behavior)
		{
			return (behavior & HitTestFilterBehavior.ContinueSkipChildren) == HitTestFilterBehavior.ContinueSkipChildren;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000A8AF8 File Offset: 0x000A7EF8
		internal static bool IncludeChildren(HitTestFilterBehavior behavior)
		{
			return (behavior & HitTestFilterBehavior.ContinueSkipSelf) == HitTestFilterBehavior.ContinueSkipSelf;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000A8B0C File Offset: 0x000A7F0C
		internal static bool Stop(HitTestFilterBehavior behavior)
		{
			return (behavior & HitTestFilterBehavior.Stop) == HitTestFilterBehavior.Stop;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000A8B20 File Offset: 0x000A7F20
		internal static bool SkipSubgraph(HitTestFilterBehavior behavior)
		{
			return behavior == HitTestFilterBehavior.ContinueSkipSelfAndChildren;
		}

		// Token: 0x040012F6 RID: 4854
		internal const int c_DoHitTest = 2;

		// Token: 0x040012F7 RID: 4855
		internal const int c_IncludeChidren = 4;

		// Token: 0x040012F8 RID: 4856
		internal const int c_Stop = 8;
	}
}
