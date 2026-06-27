using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000799 RID: 1945
	internal class SynchronizedInputProviderWrapper : MarshalByRefObject, ISynchronizedInputProvider
	{
		// Token: 0x060051AE RID: 20910 RVA: 0x0014671C File Offset: 0x00145B1C
		private SynchronizedInputProviderWrapper(AutomationPeer peer, ISynchronizedInputProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x00146740 File Offset: 0x00145B40
		public void StartListening(SynchronizedInputType inputType)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.StartListening), inputType);
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x0014676C File Offset: 0x00145B6C
		public void Cancel()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Cancel), null);
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x00146794 File Offset: 0x00145B94
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new SynchronizedInputProviderWrapper(peer, (ISynchronizedInputProvider)iface);
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x001467B0 File Offset: 0x00145BB0
		private object StartListening(object arg)
		{
			this._iface.StartListening((SynchronizedInputType)arg);
			return null;
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x001467D0 File Offset: 0x00145BD0
		private object Cancel(object unused)
		{
			this._iface.Cancel();
			return null;
		}

		// Token: 0x04002505 RID: 9477
		private AutomationPeer _peer;

		// Token: 0x04002506 RID: 9478
		private ISynchronizedInputProvider _iface;
	}
}
