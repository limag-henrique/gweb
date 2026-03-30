using System;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000795 RID: 1941
	internal class ScrollProviderWrapper : MarshalByRefObject, IScrollProvider
	{
		// Token: 0x06005184 RID: 20868 RVA: 0x00146120 File Offset: 0x00145520
		private ScrollProviderWrapper(AutomationPeer peer, IScrollProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x00146144 File Offset: 0x00145544
		public void Scroll(ScrollAmount horizontalAmount, ScrollAmount verticalAmount)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.Scroll), new ScrollAmount[]
			{
				horizontalAmount,
				verticalAmount
			});
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x00146178 File Offset: 0x00145578
		public void SetScrollPercent(double horizontalPercent, double verticalPercent)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.SetScrollPercent), new double[]
			{
				horizontalPercent,
				verticalPercent
			});
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x001461AC File Offset: 0x001455AC
		public double HorizontalScrollPercent
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetHorizontalScrollPercent), null);
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x001461D8 File Offset: 0x001455D8
		public double VerticalScrollPercent
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetVerticalScrollPercent), null);
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06005189 RID: 20873 RVA: 0x00146204 File Offset: 0x00145604
		public double HorizontalViewSize
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetHorizontalViewSize), null);
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x0600518A RID: 20874 RVA: 0x00146230 File Offset: 0x00145630
		public double VerticalViewSize
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetVerticalViewSize), null);
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x0600518B RID: 20875 RVA: 0x0014625C File Offset: 0x0014565C
		public bool HorizontallyScrollable
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetHorizontallyScrollable), null);
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x0600518C RID: 20876 RVA: 0x00146288 File Offset: 0x00145688
		public bool VerticallyScrollable
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetVerticallyScrollable), null);
			}
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x001462B4 File Offset: 0x001456B4
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new ScrollProviderWrapper(peer, (IScrollProvider)iface);
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x001462D0 File Offset: 0x001456D0
		private object Scroll(object arg)
		{
			ScrollAmount[] array = (ScrollAmount[])arg;
			this._iface.Scroll(array[0], array[1]);
			return null;
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x001462F8 File Offset: 0x001456F8
		private object SetScrollPercent(object arg)
		{
			double[] array = (double[])arg;
			this._iface.SetScrollPercent(array[0], array[1]);
			return null;
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00146320 File Offset: 0x00145720
		private object GetHorizontalScrollPercent(object unused)
		{
			return this._iface.HorizontalScrollPercent;
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00146340 File Offset: 0x00145740
		private object GetVerticalScrollPercent(object unused)
		{
			return this._iface.VerticalScrollPercent;
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x00146360 File Offset: 0x00145760
		private object GetHorizontalViewSize(object unused)
		{
			return this._iface.HorizontalViewSize;
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x00146380 File Offset: 0x00145780
		private object GetVerticalViewSize(object unused)
		{
			return this._iface.VerticalViewSize;
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x001463A0 File Offset: 0x001457A0
		private object GetHorizontallyScrollable(object unused)
		{
			return this._iface.HorizontallyScrollable;
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x001463C0 File Offset: 0x001457C0
		private object GetVerticallyScrollable(object unused)
		{
			return this._iface.VerticallyScrollable;
		}

		// Token: 0x040024FD RID: 9469
		private AutomationPeer _peer;

		// Token: 0x040024FE RID: 9470
		private IScrollProvider _iface;
	}
}
