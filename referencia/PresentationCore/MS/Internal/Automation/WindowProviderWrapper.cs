using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x020007A2 RID: 1954
	internal class WindowProviderWrapper : MarshalByRefObject, IWindowProvider
	{
		// Token: 0x06005227 RID: 21031 RVA: 0x00147BE4 File Offset: 0x00146FE4
		private WindowProviderWrapper(AutomationPeer peer, IWindowProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x00147C08 File Offset: 0x00147008
		public void SetVisualState(WindowVisualState state)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.SetVisualState), state);
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00147C34 File Offset: 0x00147034
		public void Close()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Close), null);
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00147C5C File Offset: 0x0014705C
		public bool WaitForInputIdle(int milliseconds)
		{
			return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.WaitForInputIdle), milliseconds);
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x0600522B RID: 21035 RVA: 0x00147C8C File Offset: 0x0014708C
		public bool Maximizable
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetMaximizable), null);
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x0600522C RID: 21036 RVA: 0x00147CB8 File Offset: 0x001470B8
		public bool Minimizable
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetMinimizable), null);
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x0600522D RID: 21037 RVA: 0x00147CE4 File Offset: 0x001470E4
		public bool IsModal
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetIsModal), null);
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x0600522E RID: 21038 RVA: 0x00147D10 File Offset: 0x00147110
		public WindowVisualState VisualState
		{
			get
			{
				return (WindowVisualState)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetVisualState), null);
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x0600522F RID: 21039 RVA: 0x00147D3C File Offset: 0x0014713C
		public WindowInteractionState InteractionState
		{
			get
			{
				return (WindowInteractionState)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetInteractionState), null);
			}
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06005230 RID: 21040 RVA: 0x00147D68 File Offset: 0x00147168
		public bool IsTopmost
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetIsTopmost), null);
			}
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x00147D94 File Offset: 0x00147194
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new WindowProviderWrapper(peer, (IWindowProvider)iface);
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x00147DB0 File Offset: 0x001471B0
		private object SetVisualState(object arg)
		{
			this._iface.SetVisualState((WindowVisualState)arg);
			return null;
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00147DD0 File Offset: 0x001471D0
		private object WaitForInputIdle(object arg)
		{
			return this._iface.WaitForInputIdle((int)arg);
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x00147DF4 File Offset: 0x001471F4
		private object Close(object unused)
		{
			this._iface.Close();
			return null;
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x00147E10 File Offset: 0x00147210
		private object GetMaximizable(object unused)
		{
			return this._iface.Maximizable;
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00147E30 File Offset: 0x00147230
		private object GetMinimizable(object unused)
		{
			return this._iface.Minimizable;
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x00147E50 File Offset: 0x00147250
		private object GetIsModal(object unused)
		{
			return this._iface.IsModal;
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00147E70 File Offset: 0x00147270
		private object GetVisualState(object unused)
		{
			return this._iface.VisualState;
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00147E90 File Offset: 0x00147290
		private object GetInteractionState(object unused)
		{
			return this._iface.InteractionState;
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x00147EB0 File Offset: 0x001472B0
		private object GetIsTopmost(object unused)
		{
			return this._iface.IsTopmost;
		}

		// Token: 0x04002516 RID: 9494
		private AutomationPeer _peer;

		// Token: 0x04002517 RID: 9495
		private IWindowProvider _iface;
	}
}
