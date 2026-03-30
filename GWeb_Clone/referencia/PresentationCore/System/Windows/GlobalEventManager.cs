using System;
using System.Collections;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows
{
	// Token: 0x020001C2 RID: 450
	internal static class GlobalEventManager
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0002D67C File Offset: 0x0002CA7C
		internal static RoutedEvent RegisterRoutedEvent(string name, RoutingStrategy routingStrategy, Type handlerType, Type ownerType)
		{
			object synchronized = GlobalEventManager.Synchronized;
			RoutedEvent result;
			lock (synchronized)
			{
				RoutedEvent routedEvent = new RoutedEvent(name, routingStrategy, handlerType, ownerType);
				GlobalEventManager._countRoutedEvents++;
				GlobalEventManager.AddOwner(routedEvent, ownerType);
				result = routedEvent;
			}
			return result;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002D6E4 File Offset: 0x0002CAE4
		internal static void RegisterClassHandler(Type classType, RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
		{
			DependencyObjectType dependencyObjectType = DependencyObjectType.FromSystemTypeInternal(classType);
			ClassHandlersStore classHandlersStore;
			int index;
			GlobalEventManager.GetDTypedClassListeners(dependencyObjectType, routedEvent, out classHandlersStore, out index);
			object synchronized = GlobalEventManager.Synchronized;
			lock (synchronized)
			{
				RoutedEventHandlerInfoList baseClassListeners = classHandlersStore.AddToExistingHandlers(index, handler, handledEventsToo);
				ItemStructList<DependencyObjectType> activeDTypes = GlobalEventManager._dTypedClassListeners.ActiveDTypes;
				for (int i = 0; i < activeDTypes.Count; i++)
				{
					if (activeDTypes.List[i].IsSubclassOf(dependencyObjectType))
					{
						classHandlersStore = (ClassHandlersStore)GlobalEventManager._dTypedClassListeners[activeDTypes.List[i]];
						classHandlersStore.UpdateSubClassHandlers(routedEvent, baseClassListeners);
					}
				}
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002D79C File Offset: 0x0002CB9C
		internal static RoutedEvent[] GetRoutedEvents()
		{
			object synchronized = GlobalEventManager.Synchronized;
			RoutedEvent[] array;
			lock (synchronized)
			{
				array = new RoutedEvent[GlobalEventManager._countRoutedEvents];
				ItemStructList<DependencyObjectType> activeDTypes = GlobalEventManager._dTypedRoutedEventList.ActiveDTypes;
				int num = 0;
				for (int i = 0; i < activeDTypes.Count; i++)
				{
					FrugalObjectList<RoutedEvent> frugalObjectList = (FrugalObjectList<RoutedEvent>)GlobalEventManager._dTypedRoutedEventList[activeDTypes.List[i]];
					for (int j = 0; j < frugalObjectList.Count; j++)
					{
						RoutedEvent routedEvent = frugalObjectList[j];
						if (Array.IndexOf<RoutedEvent>(array, routedEvent) < 0)
						{
							array[num++] = routedEvent;
						}
					}
				}
				IDictionaryEnumerator enumerator = GlobalEventManager._ownerTypedRoutedEventList.GetEnumerator();
				while (enumerator.MoveNext())
				{
					FrugalObjectList<RoutedEvent> frugalObjectList2 = (FrugalObjectList<RoutedEvent>)enumerator.Value;
					for (int k = 0; k < frugalObjectList2.Count; k++)
					{
						RoutedEvent routedEvent2 = frugalObjectList2[k];
						if (Array.IndexOf<RoutedEvent>(array, routedEvent2) < 0)
						{
							array[num++] = routedEvent2;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002D8B8 File Offset: 0x0002CCB8
		internal static void AddOwner(RoutedEvent routedEvent, Type ownerType)
		{
			if (ownerType == typeof(DependencyObject) || ownerType.IsSubclassOf(typeof(DependencyObject)))
			{
				DependencyObjectType dType = DependencyObjectType.FromSystemTypeInternal(ownerType);
				object obj = GlobalEventManager._dTypedRoutedEventList[dType];
				FrugalObjectList<RoutedEvent> frugalObjectList;
				if (obj == null)
				{
					frugalObjectList = new FrugalObjectList<RoutedEvent>(1);
					GlobalEventManager._dTypedRoutedEventList[dType] = frugalObjectList;
				}
				else
				{
					frugalObjectList = (FrugalObjectList<RoutedEvent>)obj;
				}
				if (!frugalObjectList.Contains(routedEvent))
				{
					frugalObjectList.Add(routedEvent);
					return;
				}
			}
			else
			{
				object obj2 = GlobalEventManager._ownerTypedRoutedEventList[ownerType];
				FrugalObjectList<RoutedEvent> frugalObjectList2;
				if (obj2 == null)
				{
					frugalObjectList2 = new FrugalObjectList<RoutedEvent>(1);
					GlobalEventManager._ownerTypedRoutedEventList[ownerType] = frugalObjectList2;
				}
				else
				{
					frugalObjectList2 = (FrugalObjectList<RoutedEvent>)obj2;
				}
				if (!frugalObjectList2.Contains(routedEvent))
				{
					frugalObjectList2.Add(routedEvent);
				}
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D96C File Offset: 0x0002CD6C
		internal static RoutedEvent[] GetRoutedEventsForOwner(Type ownerType)
		{
			if (ownerType == typeof(DependencyObject) || ownerType.IsSubclassOf(typeof(DependencyObject)))
			{
				DependencyObjectType dType = DependencyObjectType.FromSystemTypeInternal(ownerType);
				FrugalObjectList<RoutedEvent> frugalObjectList = (FrugalObjectList<RoutedEvent>)GlobalEventManager._dTypedRoutedEventList[dType];
				if (frugalObjectList != null)
				{
					return frugalObjectList.ToArray();
				}
			}
			else
			{
				FrugalObjectList<RoutedEvent> frugalObjectList2 = (FrugalObjectList<RoutedEvent>)GlobalEventManager._ownerTypedRoutedEventList[ownerType];
				if (frugalObjectList2 != null)
				{
					return frugalObjectList2.ToArray();
				}
			}
			return null;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D9DC File Offset: 0x0002CDDC
		internal static RoutedEvent GetRoutedEventFromName(string name, Type ownerType, bool includeSupers)
		{
			if (ownerType == typeof(DependencyObject) || ownerType.IsSubclassOf(typeof(DependencyObject)))
			{
				for (DependencyObjectType dependencyObjectType = DependencyObjectType.FromSystemTypeInternal(ownerType); dependencyObjectType != null; dependencyObjectType = (includeSupers ? dependencyObjectType.BaseType : null))
				{
					FrugalObjectList<RoutedEvent> frugalObjectList = (FrugalObjectList<RoutedEvent>)GlobalEventManager._dTypedRoutedEventList[dependencyObjectType];
					if (frugalObjectList != null)
					{
						for (int i = 0; i < frugalObjectList.Count; i++)
						{
							RoutedEvent routedEvent = frugalObjectList[i];
							if (routedEvent.Name.Equals(name))
							{
								return routedEvent;
							}
						}
					}
				}
			}
			else
			{
				while (ownerType != null)
				{
					FrugalObjectList<RoutedEvent> frugalObjectList2 = (FrugalObjectList<RoutedEvent>)GlobalEventManager._ownerTypedRoutedEventList[ownerType];
					if (frugalObjectList2 != null)
					{
						for (int j = 0; j < frugalObjectList2.Count; j++)
						{
							RoutedEvent routedEvent2 = frugalObjectList2[j];
							if (routedEvent2.Name.Equals(name))
							{
								return routedEvent2;
							}
						}
					}
					ownerType = (includeSupers ? ownerType.BaseType : null);
				}
			}
			return null;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002DAC8 File Offset: 0x0002CEC8
		internal static RoutedEventHandlerInfoList GetDTypedClassListeners(DependencyObjectType dType, RoutedEvent routedEvent)
		{
			ClassHandlersStore classHandlersStore;
			int num;
			return GlobalEventManager.GetDTypedClassListeners(dType, routedEvent, out classHandlersStore, out num);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002DAE0 File Offset: 0x0002CEE0
		internal static RoutedEventHandlerInfoList GetDTypedClassListeners(DependencyObjectType dType, RoutedEvent routedEvent, out ClassHandlersStore classListenersLists, out int index)
		{
			classListenersLists = (ClassHandlersStore)GlobalEventManager._dTypedClassListeners[dType];
			RoutedEventHandlerInfoList result;
			if (classListenersLists != null)
			{
				index = classListenersLists.GetHandlersIndex(routedEvent);
				if (index != -1)
				{
					result = classListenersLists.GetExistingHandlers(index);
					return result;
				}
			}
			object synchronized = GlobalEventManager.Synchronized;
			lock (synchronized)
			{
				result = GlobalEventManager.GetUpdatedDTypedClassListeners(dType, routedEvent, out classListenersLists, out index);
			}
			return result;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002DB64 File Offset: 0x0002CF64
		private static RoutedEventHandlerInfoList GetUpdatedDTypedClassListeners(DependencyObjectType dType, RoutedEvent routedEvent, out ClassHandlersStore classListenersLists, out int index)
		{
			classListenersLists = (ClassHandlersStore)GlobalEventManager._dTypedClassListeners[dType];
			if (classListenersLists != null)
			{
				index = classListenersLists.GetHandlersIndex(routedEvent);
				if (index != -1)
				{
					return classListenersLists.GetExistingHandlers(index);
				}
			}
			DependencyObjectType dependencyObjectType = dType;
			RoutedEventHandlerInfoList routedEventHandlerInfoList = null;
			int num = -1;
			while (num == -1 && dependencyObjectType.Id != GlobalEventManager._dependencyObjectType.Id)
			{
				dependencyObjectType = dependencyObjectType.BaseType;
				ClassHandlersStore classHandlersStore = (ClassHandlersStore)GlobalEventManager._dTypedClassListeners[dependencyObjectType];
				if (classHandlersStore != null)
				{
					num = classHandlersStore.GetHandlersIndex(routedEvent);
					if (num != -1)
					{
						routedEventHandlerInfoList = classHandlersStore.GetExistingHandlers(num);
					}
				}
			}
			if (classListenersLists == null)
			{
				if (dType.SystemType == typeof(UIElement) || dType.SystemType == typeof(ContentElement))
				{
					classListenersLists = new ClassHandlersStore(80);
				}
				else
				{
					classListenersLists = new ClassHandlersStore(1);
				}
				GlobalEventManager._dTypedClassListeners[dType] = classListenersLists;
			}
			index = classListenersLists.CreateHandlersLink(routedEvent, routedEventHandlerInfoList);
			return routedEventHandlerInfoList;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002DC54 File Offset: 0x0002D054
		internal static int GetNextAvailableGlobalIndex(object value)
		{
			object synchronized = GlobalEventManager.Synchronized;
			int result;
			lock (synchronized)
			{
				if (GlobalEventManager._globalIndexToEventMap.Count >= 2147483647)
				{
					throw new InvalidOperationException(SR.Get("TooManyRoutedEvents"));
				}
				result = GlobalEventManager._globalIndexToEventMap.Add(value);
			}
			return result;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002DCC8 File Offset: 0x0002D0C8
		internal static object EventFromGlobalIndex(int globalIndex)
		{
			return GlobalEventManager._globalIndexToEventMap[globalIndex];
		}

		// Token: 0x040006DA RID: 1754
		private static ArrayList _globalIndexToEventMap = new ArrayList(100);

		// Token: 0x040006DB RID: 1755
		private static DTypeMap _dTypedRoutedEventList = new DTypeMap(10);

		// Token: 0x040006DC RID: 1756
		private static Hashtable _ownerTypedRoutedEventList = new Hashtable(10);

		// Token: 0x040006DD RID: 1757
		private static int _countRoutedEvents = 0;

		// Token: 0x040006DE RID: 1758
		private static DTypeMap _dTypedClassListeners = new DTypeMap(100);

		// Token: 0x040006DF RID: 1759
		private static DependencyObjectType _dependencyObjectType = DependencyObjectType.FromSystemTypeInternal(typeof(DependencyObject));

		// Token: 0x040006E0 RID: 1760
		internal static object Synchronized = new object();
	}
}
