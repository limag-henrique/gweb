using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000792 RID: 1938
	internal class InvokeProviderWrapper : MarshalByRefObject, IInvokeProvider
	{
		// Token: 0x06005166 RID: 20838 RVA: 0x00145CDC File Offset: 0x001450DC
		private InvokeProviderWrapper(AutomationPeer peer, IInvokeProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00145D00 File Offset: 0x00145100
		public void Invoke()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Invoke), null);
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x00145D28 File Offset: 0x00145128
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new InvokeProviderWrapper(peer, (IInvokeProvider)iface);
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x00145D44 File Offset: 0x00145144
		private object Invoke(object unused)
		{
			this._iface.Invoke();
			return null;
		}

		// Token: 0x040024F7 RID: 9463
		private AutomationPeer _peer;

		// Token: 0x040024F8 RID: 9464
		private IInvokeProvider _iface;
	}
}
