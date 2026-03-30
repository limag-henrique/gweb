using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x0200078F RID: 1935
	internal class GridItemProviderWrapper : MarshalByRefObject, IGridItemProvider
	{
		// Token: 0x06005145 RID: 20805 RVA: 0x001458C0 File Offset: 0x00144CC0
		private GridItemProviderWrapper(AutomationPeer peer, IGridItemProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x001458E4 File Offset: 0x00144CE4
		public int Row
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRow), null);
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06005147 RID: 20807 RVA: 0x00145910 File Offset: 0x00144D10
		public int Column
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumn), null);
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0014593C File Offset: 0x00144D3C
		public int RowSpan
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRowSpan), null);
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x00145968 File Offset: 0x00144D68
		public int ColumnSpan
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumnSpan), null);
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x00145994 File Offset: 0x00144D94
		public IRawElementProviderSimple ContainingGrid
		{
			get
			{
				return (IRawElementProviderSimple)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetContainingGrid), null);
			}
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x001459C0 File Offset: 0x00144DC0
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new GridItemProviderWrapper(peer, (IGridItemProvider)iface);
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x001459DC File Offset: 0x00144DDC
		private object GetRow(object unused)
		{
			return this._iface.Row;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x001459FC File Offset: 0x00144DFC
		private object GetColumn(object unused)
		{
			return this._iface.Column;
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00145A1C File Offset: 0x00144E1C
		private object GetRowSpan(object unused)
		{
			return this._iface.RowSpan;
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x00145A3C File Offset: 0x00144E3C
		private object GetColumnSpan(object unused)
		{
			return this._iface.ColumnSpan;
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x00145A5C File Offset: 0x00144E5C
		private object GetContainingGrid(object unused)
		{
			return this._iface.ContainingGrid;
		}

		// Token: 0x040024F1 RID: 9457
		private AutomationPeer _peer;

		// Token: 0x040024F2 RID: 9458
		private IGridItemProvider _iface;
	}
}
