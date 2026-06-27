using System;
using System.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows
{
	// Token: 0x020001AE RID: 430
	[FriendAccessAllowed]
	internal class EventHandlersStore
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x0001E3E8 File Offset: 0x0001D7E8
		public EventHandlersStore()
		{
			this._entries = default(FrugalMap);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001E408 File Offset: 0x0001D808
		public EventHandlersStore(EventHandlersStore source)
		{
			this._entries = source._entries;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001E428 File Offset: 0x0001D828
		public void Add(EventPrivateKey key, Delegate handler)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			Delegate @delegate = this[key];
			if (@delegate == null)
			{
				this._entries[key.GlobalIndex] = handler;
				return;
			}
			this._entries[key.GlobalIndex] = Delegate.Combine(@delegate, handler);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001E488 File Offset: 0x0001D888
		public void Remove(EventPrivateKey key, Delegate handler)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			Delegate @delegate = this[key];
			if (@delegate != null)
			{
				@delegate = Delegate.Remove(@delegate, handler);
				if (@delegate == null)
				{
					this._entries[key.GlobalIndex] = DependencyProperty.UnsetValue;
					return;
				}
				this._entries[key.GlobalIndex] = @delegate;
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001E4F0 File Offset: 0x0001D8F0
		public Delegate Get(EventPrivateKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this[key];
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001E514 File Offset: 0x0001D914
		public void AddRoutedEventHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!routedEvent.IsLegalHandler(handler))
			{
				throw new ArgumentException(SR.Get("HandlerTypeIllegal"));
			}
			RoutedEventHandlerInfo value = new RoutedEventHandlerInfo(handler, handledEventsToo);
			FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = this[routedEvent];
			if (frugalObjectList == null)
			{
				frugalObjectList = (this._entries[routedEvent.GlobalIndex] = new FrugalObjectList<RoutedEventHandlerInfo>(1));
			}
			frugalObjectList.Add(value);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001E58C File Offset: 0x0001D98C
		public void RemoveRoutedEventHandler(RoutedEvent routedEvent, Delegate handler)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!routedEvent.IsLegalHandler(handler))
			{
				throw new ArgumentException(SR.Get("HandlerTypeIllegal"));
			}
			FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = this[routedEvent];
			if (frugalObjectList != null && frugalObjectList.Count > 0)
			{
				if (frugalObjectList.Count == 1 && frugalObjectList[0].Handler == handler)
				{
					this._entries[routedEvent.GlobalIndex] = DependencyProperty.UnsetValue;
					return;
				}
				for (int i = 0; i < frugalObjectList.Count; i++)
				{
					if (frugalObjectList[i].Handler == handler)
					{
						frugalObjectList.RemoveAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001E64C File Offset: 0x0001DA4C
		public bool Contains(RoutedEvent routedEvent)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = this[routedEvent];
			return frugalObjectList != null && frugalObjectList.Count != 0;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001E680 File Offset: 0x0001DA80
		private static void OnEventHandlersIterationCallback(ArrayList list, int key, object value)
		{
			RoutedEvent routedEvent = GlobalEventManager.EventFromGlobalIndex(key) as RoutedEvent;
			if (routedEvent != null && ((FrugalObjectList<RoutedEventHandlerInfo>)value).Count > 0)
			{
				list.Add(routedEvent);
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001E6B4 File Offset: 0x0001DAB4
		public RoutedEventHandlerInfo[] GetRoutedEventHandlers(RoutedEvent routedEvent)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = this[routedEvent];
			if (frugalObjectList != null)
			{
				return frugalObjectList.ToArray();
			}
			return null;
		}

		// Token: 0x170000B4 RID: 180
		internal FrugalObjectList<RoutedEventHandlerInfo> this[RoutedEvent key]
		{
			get
			{
				object obj = this._entries[key.GlobalIndex];
				if (obj == DependencyProperty.UnsetValue)
				{
					return null;
				}
				return (FrugalObjectList<RoutedEventHandlerInfo>)obj;
			}
		}

		// Token: 0x170000B5 RID: 181
		internal Delegate this[EventPrivateKey key]
		{
			get
			{
				object obj = this._entries[key.GlobalIndex];
				if (obj == DependencyProperty.UnsetValue)
				{
					return null;
				}
				return (Delegate)obj;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001E744 File Offset: 0x0001DB44
		internal int Count
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x040005A3 RID: 1443
		private FrugalMap _entries;

		// Token: 0x040005A4 RID: 1444
		private static FrugalMapIterationCallback _iterationCallback = new FrugalMapIterationCallback(EventHandlersStore.OnEventHandlersIterationCallback);
	}
}
