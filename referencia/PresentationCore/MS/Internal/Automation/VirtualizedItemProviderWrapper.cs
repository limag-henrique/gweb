using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x020007A3 RID: 1955
	internal class VirtualizedItemProviderWrapper : MarshalByRefObject, IVirtualizedItemProvider
	{
		// Token: 0x0600523B RID: 21051 RVA: 0x00147ED0 File Offset: 0x001472D0
		private VirtualizedItemProviderWrapper(AutomationPeer peer, IVirtualizedItemProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00147EF4 File Offset: 0x001472F4
		public void Realize()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Realize), null);
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x00147F1C File Offset: 0x0014731C
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new VirtualizedItemProviderWrapper(peer, (IVirtualizedItemProvider)iface);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00147F38 File Offset: 0x00147338
		private object Realize(object unused)
		{
			this._iface.Realize();
			return null;
		}

		// Token: 0x04002518 RID: 9496
		private AutomationPeer _peer;

		// Token: 0x04002519 RID: 9497
		private IVirtualizedItemProvider _iface;
	}
}
