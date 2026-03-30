using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000789 RID: 1929
	internal class DockProviderWrapper : MarshalByRefObject, IDockProvider
	{
		// Token: 0x060050F9 RID: 20729 RVA: 0x001443DC File Offset: 0x001437DC
		private DockProviderWrapper(AutomationPeer peer, IDockProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x00144400 File Offset: 0x00143800
		public void SetDockPosition(DockPosition dockPosition)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.SetDockPosition), dockPosition);
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x0014442C File Offset: 0x0014382C
		public DockPosition DockPosition
		{
			get
			{
				return (DockPosition)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetDockPosition), null);
			}
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x00144458 File Offset: 0x00143858
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new DockProviderWrapper(peer, (IDockProvider)iface);
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x00144474 File Offset: 0x00143874
		private object SetDockPosition(object arg)
		{
			this._iface.SetDockPosition((DockPosition)arg);
			return null;
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x00144494 File Offset: 0x00143894
		private object GetDockPosition(object unused)
		{
			return this._iface.DockPosition;
		}

		// Token: 0x040024E6 RID: 9446
		private AutomationPeer _peer;

		// Token: 0x040024E7 RID: 9447
		private IDockProvider _iface;
	}
}
