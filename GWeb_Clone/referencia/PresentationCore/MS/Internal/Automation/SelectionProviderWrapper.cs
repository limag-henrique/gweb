using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000798 RID: 1944
	internal class SelectionProviderWrapper : MarshalByRefObject, ISelectionProvider
	{
		// Token: 0x060051A6 RID: 20902 RVA: 0x00146600 File Offset: 0x00145A00
		private SelectionProviderWrapper(AutomationPeer peer, ISelectionProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x00146624 File Offset: 0x00145A24
		public IRawElementProviderSimple[] GetSelection()
		{
			return (IRawElementProviderSimple[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetSelection), null);
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x00146650 File Offset: 0x00145A50
		public bool CanSelectMultiple
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetCanSelectMultiple), null);
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x0014667C File Offset: 0x00145A7C
		public bool IsSelectionRequired
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetIsSelectionRequired), null);
			}
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x001466A8 File Offset: 0x00145AA8
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new SelectionProviderWrapper(peer, (ISelectionProvider)iface);
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x001466C4 File Offset: 0x00145AC4
		private object GetSelection(object unused)
		{
			return this._iface.GetSelection();
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x001466DC File Offset: 0x00145ADC
		private object GetCanSelectMultiple(object unused)
		{
			return this._iface.CanSelectMultiple;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x001466FC File Offset: 0x00145AFC
		private object GetIsSelectionRequired(object unused)
		{
			return this._iface.IsSelectionRequired;
		}

		// Token: 0x04002503 RID: 9475
		private AutomationPeer _peer;

		// Token: 0x04002504 RID: 9476
		private ISelectionProvider _iface;
	}
}
