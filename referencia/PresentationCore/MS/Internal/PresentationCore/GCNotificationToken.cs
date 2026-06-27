using System;
using System.Threading;

namespace MS.Internal.PresentationCore
{
	// Token: 0x020007E7 RID: 2023
	internal class GCNotificationToken
	{
		// Token: 0x060054DB RID: 21723 RVA: 0x0015E2C4 File Offset: 0x0015D6C4
		private GCNotificationToken(WaitCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x0015E2E8 File Offset: 0x0015D6E8
		~GCNotificationToken()
		{
			ThreadPool.QueueUserWorkItem(this.callback, this.state);
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x0015E32C File Offset: 0x0015D72C
		internal static void RegisterCallback(WaitCallback callback, object state)
		{
			new GCNotificationToken(callback, state);
		}

		// Token: 0x04002650 RID: 9808
		private WaitCallback callback;

		// Token: 0x04002651 RID: 9809
		private object state;
	}
}
