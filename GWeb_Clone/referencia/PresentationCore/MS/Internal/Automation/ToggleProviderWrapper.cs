using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x0200079F RID: 1951
	internal class ToggleProviderWrapper : MarshalByRefObject, IToggleProvider
	{
		// Token: 0x0600520B RID: 21003 RVA: 0x001477D4 File Offset: 0x00146BD4
		private ToggleProviderWrapper(AutomationPeer peer, IToggleProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x001477F8 File Offset: 0x00146BF8
		public void Toggle()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.ToggleInternal), null);
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x0600520D RID: 21005 RVA: 0x00147820 File Offset: 0x00146C20
		public ToggleState ToggleState
		{
			get
			{
				return (ToggleState)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetToggleState), null);
			}
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0014784C File Offset: 0x00146C4C
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new ToggleProviderWrapper(peer, (IToggleProvider)iface);
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00147868 File Offset: 0x00146C68
		private object ToggleInternal(object unused)
		{
			this._iface.Toggle();
			return null;
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x00147884 File Offset: 0x00146C84
		private object GetToggleState(object unused)
		{
			return this._iface.ToggleState;
		}

		// Token: 0x04002510 RID: 9488
		private AutomationPeer _peer;

		// Token: 0x04002511 RID: 9489
		private IToggleProvider _iface;
	}
}
