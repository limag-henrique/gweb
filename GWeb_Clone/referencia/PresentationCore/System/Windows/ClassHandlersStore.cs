using System;
using MS.Utility;

namespace System.Windows
{
	// Token: 0x0200018B RID: 395
	internal class ClassHandlersStore
	{
		// Token: 0x060003CD RID: 973 RVA: 0x00015A70 File Offset: 0x00014E70
		internal ClassHandlersStore(int size)
		{
			this._eventHandlersList = new ItemStructList<ClassHandlers>(size);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00015A90 File Offset: 0x00014E90
		internal RoutedEventHandlerInfoList AddToExistingHandlers(int index, Delegate handler, bool handledEventsToo)
		{
			RoutedEventHandlerInfo routedEventHandlerInfo = new RoutedEventHandlerInfo(handler, handledEventsToo);
			RoutedEventHandlerInfoList routedEventHandlerInfoList = this._eventHandlersList.List[index].Handlers;
			if (routedEventHandlerInfoList == null || !this._eventHandlersList.List[index].HasSelfHandlers)
			{
				routedEventHandlerInfoList = new RoutedEventHandlerInfoList();
				routedEventHandlerInfoList.Handlers = new RoutedEventHandlerInfo[1];
				routedEventHandlerInfoList.Handlers[0] = routedEventHandlerInfo;
				routedEventHandlerInfoList.Next = this._eventHandlersList.List[index].Handlers;
				this._eventHandlersList.List[index].Handlers = routedEventHandlerInfoList;
				this._eventHandlersList.List[index].HasSelfHandlers = true;
			}
			else
			{
				int num = routedEventHandlerInfoList.Handlers.Length;
				RoutedEventHandlerInfo[] array = new RoutedEventHandlerInfo[num + 1];
				Array.Copy(routedEventHandlerInfoList.Handlers, 0, array, 0, num);
				array[num] = routedEventHandlerInfo;
				routedEventHandlerInfoList.Handlers = array;
			}
			return routedEventHandlerInfoList;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00015B74 File Offset: 0x00014F74
		internal RoutedEventHandlerInfoList GetExistingHandlers(int index)
		{
			return this._eventHandlersList.List[index].Handlers;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00015B98 File Offset: 0x00014F98
		internal int CreateHandlersLink(RoutedEvent routedEvent, RoutedEventHandlerInfoList handlers)
		{
			this._eventHandlersList.Add(new ClassHandlers
			{
				RoutedEvent = routedEvent,
				Handlers = handlers,
				HasSelfHandlers = false
			});
			return this._eventHandlersList.Count - 1;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00015BE0 File Offset: 0x00014FE0
		internal void UpdateSubClassHandlers(RoutedEvent routedEvent, RoutedEventHandlerInfoList baseClassListeners)
		{
			int handlersIndex = this.GetHandlersIndex(routedEvent);
			if (handlersIndex != -1)
			{
				bool hasSelfHandlers = this._eventHandlersList.List[handlersIndex].HasSelfHandlers;
				RoutedEventHandlerInfoList routedEventHandlerInfoList = hasSelfHandlers ? this._eventHandlersList.List[handlersIndex].Handlers.Next : this._eventHandlersList.List[handlersIndex].Handlers;
				bool flag = false;
				if (routedEventHandlerInfoList != null)
				{
					if (baseClassListeners.Next != null && baseClassListeners.Next.Contains(routedEventHandlerInfoList))
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					if (hasSelfHandlers)
					{
						this._eventHandlersList.List[handlersIndex].Handlers.Next = baseClassListeners;
						return;
					}
					this._eventHandlersList.List[handlersIndex].Handlers = baseClassListeners;
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00015CA8 File Offset: 0x000150A8
		internal int GetHandlersIndex(RoutedEvent routedEvent)
		{
			for (int i = 0; i < this._eventHandlersList.Count; i++)
			{
				if (this._eventHandlersList.List[i].RoutedEvent == routedEvent)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040004BD RID: 1213
		private ItemStructList<ClassHandlers> _eventHandlersList;
	}
}
