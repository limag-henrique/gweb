using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000797 RID: 1943
	internal class SelectionItemProviderWrapper : MarshalByRefObject, ISelectionItemProvider
	{
		// Token: 0x0600519A RID: 20890 RVA: 0x00146464 File Offset: 0x00145864
		private SelectionItemProviderWrapper(AutomationPeer peer, ISelectionItemProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00146488 File Offset: 0x00145888
		public void Select()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Select), null);
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x001464B0 File Offset: 0x001458B0
		public void AddToSelection()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.AddToSelection), null);
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x001464D8 File Offset: 0x001458D8
		public void RemoveFromSelection()
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.RemoveFromSelection), null);
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x0600519E RID: 20894 RVA: 0x00146500 File Offset: 0x00145900
		public bool IsSelected
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetIsSelected), null);
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x0600519F RID: 20895 RVA: 0x0014652C File Offset: 0x0014592C
		public IRawElementProviderSimple SelectionContainer
		{
			get
			{
				return (IRawElementProviderSimple)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetSelectionContainer), null);
			}
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x00146558 File Offset: 0x00145958
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new SelectionItemProviderWrapper(peer, (ISelectionItemProvider)iface);
		}

		// Token: 0x060051A1 RID: 20897 RVA: 0x00146574 File Offset: 0x00145974
		private object Select(object unused)
		{
			this._iface.Select();
			return null;
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x00146590 File Offset: 0x00145990
		private object AddToSelection(object unused)
		{
			this._iface.AddToSelection();
			return null;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x001465AC File Offset: 0x001459AC
		private object RemoveFromSelection(object unused)
		{
			this._iface.RemoveFromSelection();
			return null;
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x001465C8 File Offset: 0x001459C8
		private object GetIsSelected(object unused)
		{
			return this._iface.IsSelected;
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x001465E8 File Offset: 0x001459E8
		private object GetSelectionContainer(object unused)
		{
			return this._iface.SelectionContainer;
		}

		// Token: 0x04002501 RID: 9473
		private AutomationPeer _peer;

		// Token: 0x04002502 RID: 9474
		private ISelectionItemProvider _iface;
	}
}
