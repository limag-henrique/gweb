using System;

namespace System.Windows
{
	// Token: 0x020001B2 RID: 434
	internal static class EventRouteFactory
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x0001F010 File Offset: 0x0001E410
		internal static EventRoute FetchObject(RoutedEvent routedEvent)
		{
			EventRoute eventRoute = EventRouteFactory.Pop();
			if (eventRoute == null)
			{
				eventRoute = new EventRoute(routedEvent);
			}
			else
			{
				eventRoute.RoutedEvent = routedEvent;
			}
			return eventRoute;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001F038 File Offset: 0x0001E438
		internal static void RecycleObject(EventRoute eventRoute)
		{
			eventRoute.Clear();
			EventRouteFactory.Push(eventRoute);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001F054 File Offset: 0x0001E454
		private static void Push(EventRoute eventRoute)
		{
			object synchronized = EventRouteFactory._synchronized;
			lock (synchronized)
			{
				if (EventRouteFactory._eventRouteStack == null)
				{
					EventRouteFactory._eventRouteStack = new EventRoute[2];
					EventRouteFactory._stackTop = 0;
				}
				if (EventRouteFactory._stackTop < 2)
				{
					EventRouteFactory._eventRouteStack[EventRouteFactory._stackTop++] = eventRoute;
				}
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001F0D0 File Offset: 0x0001E4D0
		private static EventRoute Pop()
		{
			object synchronized = EventRouteFactory._synchronized;
			lock (synchronized)
			{
				if (EventRouteFactory._stackTop > 0)
				{
					EventRoute result = EventRouteFactory._eventRouteStack[--EventRouteFactory._stackTop];
					EventRouteFactory._eventRouteStack[EventRouteFactory._stackTop] = null;
					return result;
				}
			}
			return null;
		}

		// Token: 0x040005AA RID: 1450
		private static EventRoute[] _eventRouteStack;

		// Token: 0x040005AB RID: 1451
		private static int _stackTop;

		// Token: 0x040005AC RID: 1452
		private static object _synchronized = new object();
	}
}
