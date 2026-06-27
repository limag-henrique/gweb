using System;
using System.Collections;

namespace System.Windows.Media
{
	// Token: 0x02000448 RID: 1096
	internal class UniqueEventHelper
	{
		// Token: 0x06002CB4 RID: 11444 RVA: 0x000B26B4 File Offset: 0x000B1AB4
		internal void AddEvent(EventHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.EnsureEventTable();
			if (this._htDelegates[handler] == null)
			{
				this._htDelegates.Add(handler, 1);
				return;
			}
			int num = (int)this._htDelegates[handler] + 1;
			this._htDelegates[handler] = num;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000B271C File Offset: 0x000B1B1C
		internal void RemoveEvent(EventHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.EnsureEventTable();
			if (this._htDelegates[handler] != null)
			{
				int num = (int)this._htDelegates[handler];
				if (num == 1)
				{
					this._htDelegates.Remove(handler);
					return;
				}
				this._htDelegates[handler] = num - 1;
			}
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000B2784 File Offset: 0x000B1B84
		internal void InvokeEvents(object sender, EventArgs args)
		{
			if (this._htDelegates != null)
			{
				Hashtable hashtable = (Hashtable)this._htDelegates.Clone();
				foreach (object obj in hashtable.Keys)
				{
					EventHandler eventHandler = (EventHandler)obj;
					eventHandler(sender, args);
				}
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000B2804 File Offset: 0x000B1C04
		internal UniqueEventHelper Clone()
		{
			UniqueEventHelper uniqueEventHelper = new UniqueEventHelper();
			if (this._htDelegates != null)
			{
				uniqueEventHelper._htDelegates = (Hashtable)this._htDelegates.Clone();
			}
			return uniqueEventHelper;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000B2838 File Offset: 0x000B1C38
		private void EnsureEventTable()
		{
			if (this._htDelegates == null)
			{
				this._htDelegates = new Hashtable();
			}
		}

		// Token: 0x04001469 RID: 5225
		private Hashtable _htDelegates;
	}
}
