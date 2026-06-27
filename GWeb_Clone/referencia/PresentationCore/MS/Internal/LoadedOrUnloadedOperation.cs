using System;
using System.Windows;
using System.Windows.Threading;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000690 RID: 1680
	[FriendAccessAllowed]
	internal class LoadedOrUnloadedOperation
	{
		// Token: 0x060049D9 RID: 18905 RVA: 0x0011F6EC File Offset: 0x0011EAEC
		internal LoadedOrUnloadedOperation(DispatcherOperationCallback callback, DependencyObject target)
		{
			this._callback = callback;
			this._target = target;
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x0011F710 File Offset: 0x0011EB10
		internal void DoWork()
		{
			if (!this._cancelled)
			{
				this._callback(this._target);
			}
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x0011F738 File Offset: 0x0011EB38
		internal void Cancel()
		{
			this._cancelled = true;
		}

		// Token: 0x04001EDF RID: 7903
		private DispatcherOperationCallback _callback;

		// Token: 0x04001EE0 RID: 7904
		private DependencyObject _target;

		// Token: 0x04001EE1 RID: 7905
		private bool _cancelled;
	}
}
