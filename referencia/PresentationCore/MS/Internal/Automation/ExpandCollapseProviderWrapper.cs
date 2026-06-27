using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x0200078E RID: 1934
	internal class ExpandCollapseProviderWrapper : MarshalByRefObject, IExpandCollapseProvider
	{
		// Token: 0x0600513D RID: 20797 RVA: 0x001457AC File Offset: 0x00144BAC
		private ExpandCollapseProviderWrapper(AutomationPeer peer, IExpandCollapseProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x001457D0 File Offset: 0x00144BD0
		public void Expand()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Expand), null);
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x001457F8 File Offset: 0x00144BF8
		public void Collapse()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Collapse), null);
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06005140 RID: 20800 RVA: 0x00145820 File Offset: 0x00144C20
		public ExpandCollapseState ExpandCollapseState
		{
			get
			{
				return (ExpandCollapseState)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetExpandCollapseState), null);
			}
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0014584C File Offset: 0x00144C4C
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new ExpandCollapseProviderWrapper(peer, (IExpandCollapseProvider)iface);
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x00145868 File Offset: 0x00144C68
		private object Expand(object unused)
		{
			this._iface.Expand();
			return null;
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x00145884 File Offset: 0x00144C84
		private object Collapse(object unused)
		{
			this._iface.Collapse();
			return null;
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x001458A0 File Offset: 0x00144CA0
		private object GetExpandCollapseState(object unused)
		{
			return this._iface.ExpandCollapseState;
		}

		// Token: 0x040024EF RID: 9455
		private AutomationPeer _peer;

		// Token: 0x040024F0 RID: 9456
		private IExpandCollapseProvider _iface;
	}
}
