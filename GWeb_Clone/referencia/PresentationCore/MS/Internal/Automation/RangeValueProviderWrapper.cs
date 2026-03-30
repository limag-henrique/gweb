using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x02000794 RID: 1940
	internal class RangeValueProviderWrapper : MarshalByRefObject, IRangeValueProvider
	{
		// Token: 0x06005174 RID: 20852 RVA: 0x00145ECC File Offset: 0x001452CC
		private RangeValueProviderWrapper(AutomationPeer peer, IRangeValueProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005175 RID: 20853 RVA: 0x00145EF0 File Offset: 0x001452F0
		public void SetValue(double val)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.SetValueInternal), val);
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x00145F1C File Offset: 0x0014531C
		public double Value
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetValue), null);
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x00145F48 File Offset: 0x00145348
		public bool IsReadOnly
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetIsReadOnly), null);
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x00145F74 File Offset: 0x00145374
		public double Maximum
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetMaximum), null);
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06005179 RID: 20857 RVA: 0x00145FA0 File Offset: 0x001453A0
		public double Minimum
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetMinimum), null);
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x00145FCC File Offset: 0x001453CC
		public double LargeChange
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetLargeChange), null);
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x0600517B RID: 20859 RVA: 0x00145FF8 File Offset: 0x001453F8
		public double SmallChange
		{
			get
			{
				return (double)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetSmallChange), null);
			}
		}

		// Token: 0x0600517C RID: 20860 RVA: 0x00146024 File Offset: 0x00145424
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new RangeValueProviderWrapper(peer, (IRangeValueProvider)iface);
		}

		// Token: 0x0600517D RID: 20861 RVA: 0x00146040 File Offset: 0x00145440
		private object SetValueInternal(object arg)
		{
			this._iface.SetValue((double)arg);
			return null;
		}

		// Token: 0x0600517E RID: 20862 RVA: 0x00146060 File Offset: 0x00145460
		private object GetValue(object unused)
		{
			return this._iface.Value;
		}

		// Token: 0x0600517F RID: 20863 RVA: 0x00146080 File Offset: 0x00145480
		private object GetIsReadOnly(object unused)
		{
			return this._iface.IsReadOnly;
		}

		// Token: 0x06005180 RID: 20864 RVA: 0x001460A0 File Offset: 0x001454A0
		private object GetMaximum(object unused)
		{
			return this._iface.Maximum;
		}

		// Token: 0x06005181 RID: 20865 RVA: 0x001460C0 File Offset: 0x001454C0
		private object GetMinimum(object unused)
		{
			return this._iface.Minimum;
		}

		// Token: 0x06005182 RID: 20866 RVA: 0x001460E0 File Offset: 0x001454E0
		private object GetLargeChange(object unused)
		{
			return this._iface.LargeChange;
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00146100 File Offset: 0x00145500
		private object GetSmallChange(object unused)
		{
			return this._iface.SmallChange;
		}

		// Token: 0x040024FB RID: 9467
		private AutomationPeer _peer;

		// Token: 0x040024FC RID: 9468
		private IRangeValueProvider _iface;
	}
}
