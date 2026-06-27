using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x0200079B RID: 1947
	internal class TableItemProviderWrapper : MarshalByRefObject, ITableItemProvider, IGridItemProvider
	{
		// Token: 0x060051B7 RID: 20919 RVA: 0x00146918 File Offset: 0x00145D18
		private TableItemProviderWrapper(AutomationPeer peer, ITableItemProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060051B8 RID: 20920 RVA: 0x0014693C File Offset: 0x00145D3C
		public int Row
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRow), null);
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060051B9 RID: 20921 RVA: 0x00146968 File Offset: 0x00145D68
		public int Column
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumn), null);
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060051BA RID: 20922 RVA: 0x00146994 File Offset: 0x00145D94
		public int RowSpan
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRowSpan), null);
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060051BB RID: 20923 RVA: 0x001469C0 File Offset: 0x00145DC0
		public int ColumnSpan
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumnSpan), null);
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060051BC RID: 20924 RVA: 0x001469EC File Offset: 0x00145DEC
		public IRawElementProviderSimple ContainingGrid
		{
			get
			{
				return (IRawElementProviderSimple)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetContainingGrid), null);
			}
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x00146A18 File Offset: 0x00145E18
		public IRawElementProviderSimple[] GetRowHeaderItems()
		{
			return (IRawElementProviderSimple[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRowHeaderItems), null);
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x00146A44 File Offset: 0x00145E44
		public IRawElementProviderSimple[] GetColumnHeaderItems()
		{
			return (IRawElementProviderSimple[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumnHeaderItems), null);
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x00146A70 File Offset: 0x00145E70
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new TableItemProviderWrapper(peer, (ITableItemProvider)iface);
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x00146A8C File Offset: 0x00145E8C
		private object GetRow(object unused)
		{
			return this._iface.Row;
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x00146AAC File Offset: 0x00145EAC
		private object GetColumn(object unused)
		{
			return this._iface.Column;
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x00146ACC File Offset: 0x00145ECC
		private object GetRowSpan(object unused)
		{
			return this._iface.RowSpan;
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x00146AEC File Offset: 0x00145EEC
		private object GetColumnSpan(object unused)
		{
			return this._iface.ColumnSpan;
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00146B0C File Offset: 0x00145F0C
		private object GetContainingGrid(object unused)
		{
			return this._iface.ContainingGrid;
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x00146B24 File Offset: 0x00145F24
		private object GetRowHeaderItems(object unused)
		{
			return this._iface.GetRowHeaderItems();
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x00146B3C File Offset: 0x00145F3C
		private object GetColumnHeaderItems(object unused)
		{
			return this._iface.GetColumnHeaderItems();
		}

		// Token: 0x04002508 RID: 9480
		private AutomationPeer _peer;

		// Token: 0x04002509 RID: 9481
		private ITableItemProvider _iface;
	}
}
