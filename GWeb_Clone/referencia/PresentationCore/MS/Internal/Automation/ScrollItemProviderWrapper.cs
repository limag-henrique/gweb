using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000796 RID: 1942
	internal class ScrollItemProviderWrapper : MarshalByRefObject, IScrollItemProvider
	{
		// Token: 0x06005196 RID: 20886 RVA: 0x001463E0 File Offset: 0x001457E0
		private ScrollItemProviderWrapper(AutomationPeer peer, IScrollItemProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x00146404 File Offset: 0x00145804
		public void ScrollIntoView()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.ScrollIntoView), null);
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0014642C File Offset: 0x0014582C
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new ScrollItemProviderWrapper(peer, (IScrollItemProvider)iface);
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x00146448 File Offset: 0x00145848
		private object ScrollIntoView(object unused)
		{
			this._iface.ScrollIntoView();
			return null;
		}

		// Token: 0x040024FF RID: 9471
		private AutomationPeer _peer;

		// Token: 0x04002500 RID: 9472
		private IScrollItemProvider _iface;
	}
}
