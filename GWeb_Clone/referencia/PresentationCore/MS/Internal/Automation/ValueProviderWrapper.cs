using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x020007A1 RID: 1953
	internal class ValueProviderWrapper : MarshalByRefObject, IValueProvider
	{
		// Token: 0x0600521F RID: 21023 RVA: 0x00147ACC File Offset: 0x00146ECC
		private ValueProviderWrapper(AutomationPeer peer, IValueProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00147AF0 File Offset: 0x00146EF0
		public void SetValue(string val)
		{
			ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.SetValueInternal), val);
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06005221 RID: 21025 RVA: 0x00147B18 File Offset: 0x00146F18
		public string Value
		{
			get
			{
				return (string)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetValue), null);
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06005222 RID: 21026 RVA: 0x00147B44 File Offset: 0x00146F44
		public bool IsReadOnly
		{
			get
			{
				return (bool)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.GetIsReadOnly), null);
			}
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00147B70 File Offset: 0x00146F70
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new ValueProviderWrapper(peer, (IValueProvider)iface);
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x00147B8C File Offset: 0x00146F8C
		private object SetValueInternal(object arg)
		{
			this._iface.SetValue((string)arg);
			return null;
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x00147BAC File Offset: 0x00146FAC
		private object GetValue(object unused)
		{
			return this._iface.Value;
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x00147BC4 File Offset: 0x00146FC4
		private object GetIsReadOnly(object unused)
		{
			return this._iface.IsReadOnly;
		}

		// Token: 0x04002514 RID: 9492
		private AutomationPeer _peer;

		// Token: 0x04002515 RID: 9493
		private IValueProvider _iface;
	}
}
