using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x0200079C RID: 1948
	internal class TableProviderWrapper : MarshalByRefObject, ITableProvider, IGridProvider
	{
		// Token: 0x060051C7 RID: 20935 RVA: 0x00146B54 File Offset: 0x00145F54
		private TableProviderWrapper(AutomationPeer peer, ITableProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00146B78 File Offset: 0x00145F78
		public IRawElementProviderSimple GetItem(int row, int column)
		{
			return (IRawElementProviderSimple)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetItem), new int[]
			{
				row,
				column
			});
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x00146BB0 File Offset: 0x00145FB0
		public int RowCount
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRowCount), null);
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060051CA RID: 20938 RVA: 0x00146BDC File Offset: 0x00145FDC
		public int ColumnCount
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumnCount), null);
			}
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00146C08 File Offset: 0x00146008
		public IRawElementProviderSimple[] GetRowHeaders()
		{
			return (IRawElementProviderSimple[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRowHeaders), null);
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00146C34 File Offset: 0x00146034
		public IRawElementProviderSimple[] GetColumnHeaders()
		{
			return (IRawElementProviderSimple[])ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumnHeaders), null);
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060051CD RID: 20941 RVA: 0x00146C60 File Offset: 0x00146060
		public RowOrColumnMajor RowOrColumnMajor
		{
			get
			{
				return (RowOrColumnMajor)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRowOrColumnMajor), null);
			}
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x00146C8C File Offset: 0x0014608C
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new TableProviderWrapper(peer, (ITableProvider)iface);
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x00146CA8 File Offset: 0x001460A8
		private object GetItem(object arg)
		{
			int[] array = (int[])arg;
			return this._iface.GetItem(array[0], array[1]);
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x00146CD0 File Offset: 0x001460D0
		private object GetRowCount(object unused)
		{
			return this._iface.RowCount;
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x00146CF0 File Offset: 0x001460F0
		private object GetColumnCount(object unused)
		{
			return this._iface.ColumnCount;
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x00146D10 File Offset: 0x00146110
		private object GetRowHeaders(object unused)
		{
			return this._iface.GetRowHeaders();
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x00146D28 File Offset: 0x00146128
		private object GetColumnHeaders(object unused)
		{
			return this._iface.GetColumnHeaders();
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x00146D40 File Offset: 0x00146140
		private object GetRowOrColumnMajor(object unused)
		{
			return this._iface.RowOrColumnMajor;
		}

		// Token: 0x0400250A RID: 9482
		private AutomationPeer _peer;

		// Token: 0x0400250B RID: 9483
		private ITableProvider _iface;
	}
}
