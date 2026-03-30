using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;

namespace MS.Internal.Automation
{
	// Token: 0x020007A4 RID: 1956
	internal class ItemContainerProviderWrapper : MarshalByRefObject, IItemContainerProvider
	{
		// Token: 0x0600523F RID: 21055 RVA: 0x00147F54 File Offset: 0x00147354
		private ItemContainerProviderWrapper(AutomationPeer peer, IItemContainerProvider iface)
		{
			this._peer = peer;
			this._iface = iface;
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x00147F78 File Offset: 0x00147378
		public IRawElementProviderSimple FindItemByProperty(IRawElementProviderSimple startAfter, int propertyId, object value)
		{
			object[] arg = new object[]
			{
				startAfter,
				propertyId,
				value
			};
			return (IRawElementProviderSimple)ElementUtil.Invoke(this._peer, new DispatcherOperationCallback(this.FindItemByProperty), arg);
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x00147FBC File Offset: 0x001473BC
		internal static object Wrap(AutomationPeer peer, object iface)
		{
			return new ItemContainerProviderWrapper(peer, (IItemContainerProvider)iface);
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x00147FD8 File Offset: 0x001473D8
		private object FindItemByProperty(object arg)
		{
			object[] array = (object[])arg;
			IRawElementProviderSimple startAfter = (IRawElementProviderSimple)array[0];
			int propertyId = (int)array[1];
			object value = array[2];
			return this._iface.FindItemByProperty(startAfter, propertyId, value);
		}

		// Token: 0x0400251A RID: 9498
		private AutomationPeer _peer;

		// Token: 0x0400251B RID: 9499
		private IItemContainerProvider _iface;
	}
}
