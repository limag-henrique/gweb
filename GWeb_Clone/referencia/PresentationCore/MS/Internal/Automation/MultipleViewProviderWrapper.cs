using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000793 RID: 1939
	internal class MultipleViewProviderWrapper : MarshalByRefObject, IMultipleViewProvider
	{
		// Token: 0x0600516A RID: 20842 RVA: 0x00145D60 File Offset: 0x00145160
		private MultipleViewProviderWrapper(AutomationPeer peer, IMultipleViewProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x00145D84 File Offset: 0x00145184
		public string GetViewName(int viewID)
		{
			return (string)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetViewName), viewID);
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x00145DB4 File Offset: 0x001451B4
		public void SetCurrentView(int viewID)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.SetCurrentView), viewID);
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600516D RID: 20845 RVA: 0x00145DE0 File Offset: 0x001451E0
		public int CurrentView
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetCurrentView), null);
			}
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x00145E0C File Offset: 0x0014520C
		public int[] GetSupportedViews()
		{
			return (int[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetSupportedViews), null);
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x00145E38 File Offset: 0x00145238
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new MultipleViewProviderWrapper(peer, (IMultipleViewProvider)iface);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x00145E54 File Offset: 0x00145254
		private object GetViewName(object arg)
		{
			return this._iface.GetViewName((int)arg);
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00145E74 File Offset: 0x00145274
		private object SetCurrentView(object arg)
		{
			this._iface.SetCurrentView((int)arg);
			return null;
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x00145E94 File Offset: 0x00145294
		private object GetCurrentView(object unused)
		{
			return this._iface.CurrentView;
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x00145EB4 File Offset: 0x001452B4
		private object GetSupportedViews(object unused)
		{
			return this._iface.GetSupportedViews();
		}

		// Token: 0x040024F9 RID: 9465
		private AutomationPeer _peer;

		// Token: 0x040024FA RID: 9466
		private IMultipleViewProvider _iface;
	}
}
