using System;
using System.Collections;

namespace System.Windows.Media
{
	// Token: 0x02000447 RID: 1095
	internal class UniqueEventHelper<TEventArgs> where TEventArgs : EventArgs
	{
		// Token: 0x06002CAE RID: 11438 RVA: 0x000B24FC File Offset: 0x000B18FC
		internal void AddEvent(EventHandler<TEventArgs> handler)
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

		// Token: 0x06002CAF RID: 11439 RVA: 0x000B2564 File Offset: 0x000B1964
		internal void RemoveEvent(EventHandler<TEventArgs> handler)
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

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000B25CC File Offset: 0x000B19CC
		internal void InvokeEvents(object sender, TEventArgs args)
		{
			if (this._htDelegates != null)
			{
				Hashtable hashtable = (Hashtable)this._htDelegates.Clone();
				foreach (object obj in hashtable.Keys)
				{
					EventHandler<TEventArgs> eventHandler = (EventHandler<TEventArgs>)obj;
					eventHandler(sender, args);
				}
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000B264C File Offset: 0x000B1A4C
		internal UniqueEventHelper<TEventArgs> Clone()
		{
			UniqueEventHelper<TEventArgs> uniqueEventHelper = new UniqueEventHelper<TEventArgs>();
			if (this._htDelegates != null)
			{
				uniqueEventHelper._htDelegates = (Hashtable)this._htDelegates.Clone();
			}
			return uniqueEventHelper;
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000B2680 File Offset: 0x000B1A80
		private void EnsureEventTable()
		{
			if (this._htDelegates == null)
			{
				this._htDelegates = new Hashtable();
			}
		}

		// Token: 0x04001468 RID: 5224
		private Hashtable _htDelegates;
	}
}
