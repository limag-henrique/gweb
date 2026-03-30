using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000790 RID: 1936
	internal class GridProviderWrapper : MarshalByRefObject, IGridProvider
	{
		// Token: 0x06005151 RID: 20817 RVA: 0x00145A74 File Offset: 0x00144E74
		private GridProviderWrapper(AutomationPeer peer, IGridProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x00145A98 File Offset: 0x00144E98
		public IRawElementProviderSimple GetItem(int row, int column)
		{
			return (IRawElementProviderSimple)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetItem), new int[]
			{
				row,
				column
			});
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x00145AD0 File Offset: 0x00144ED0
		public int RowCount
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetRowCount), null);
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06005154 RID: 20820 RVA: 0x00145AFC File Offset: 0x00144EFC
		public int ColumnCount
		{
			get
			{
				return (int)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetColumnCount), null);
			}
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x00145B28 File Offset: 0x00144F28
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new GridProviderWrapper(peer, (IGridProvider)iface);
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x00145B44 File Offset: 0x00144F44
		private object GetItem(object arg)
		{
			int[] array = (int[])arg;
			return this._iface.GetItem(array[0], array[1]);
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x00145B6C File Offset: 0x00144F6C
		private object GetRowCount(object unused)
		{
			return this._iface.RowCount;
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x00145B8C File Offset: 0x00144F8C
		private object GetColumnCount(object unused)
		{
			return this._iface.ColumnCount;
		}

		// Token: 0x040024F3 RID: 9459
		private AutomationPeer _peer;

		// Token: 0x040024F4 RID: 9460
		private IGridProvider _iface;
	}
}
