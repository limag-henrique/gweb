using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Input.Tracing;

namespace System.Windows.Input
{
	// Token: 0x020002D4 RID: 724
	internal class PenThreadPool
	{
		// Token: 0x060015D8 RID: 5592 RVA: 0x0005100C File Offset: 0x0005040C
		[SecurityCritical]
		internal static PenThread GetPenThreadForPenContext(PenContext penContext)
		{
			if (PenThreadPool._penThreadPool == null)
			{
				PenThreadPool._penThreadPool = new PenThreadPool();
			}
			return PenThreadPool._penThreadPool.GetPenThreadForPenContextHelper(penContext);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00051038 File Offset: 0x00050438
		[SecurityCritical]
		internal PenThreadPool()
		{
			this._penThreadWeakRefList = new List<WeakReference<PenThread>>();
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00051058 File Offset: 0x00050458
		[SecurityCritical]
		private PenThread GetPenThreadForPenContextHelper(PenContext penContext)
		{
			List<PenThread> list = new List<PenThread>();
			PenThread penThread = null;
			while (list.Count < 10)
			{
				for (int i = this._penThreadWeakRefList.Count - 1; i >= 0; i--)
				{
					PenThread penThread2 = null;
					if (this._penThreadWeakRefList[i].TryGetTarget(out penThread2) && !list.Contains(penThread2))
					{
						penThread = penThread2;
					}
					else if (penThread2 == null)
					{
						this._penThreadWeakRefList.RemoveAt(i);
					}
				}
				if (penThread == null)
				{
					penThread = new PenThread();
					this._penThreadWeakRefList.Add(new WeakReference<PenThread>(penThread));
				}
				if (penContext == null || penThread.AddPenContext(penContext))
				{
					break;
				}
				list.Add(penThread);
				penThread = null;
				StylusTraceLogger.LogReentrancy("GetPenThreadForPenContextHelper");
			}
			if (penThread == null)
			{
				StylusTraceLogger.LogReentrancyRetryLimitReached();
			}
			return penThread;
		}

		// Token: 0x04000BE1 RID: 3041
		private const int MAX_PENTHREAD_RETRIES = 10;

		// Token: 0x04000BE2 RID: 3042
		[SecurityCritical]
		[ThreadStatic]
		private static PenThreadPool _penThreadPool;

		// Token: 0x04000BE3 RID: 3043
		[SecurityCritical]
		private List<WeakReference<PenThread>> _penThreadWeakRefList;
	}
}
