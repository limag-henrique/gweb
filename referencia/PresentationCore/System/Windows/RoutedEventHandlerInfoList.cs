using System;

namespace System.Windows
{
	// Token: 0x0200018D RID: 397
	internal class RoutedEventHandlerInfoList
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x00015D80 File Offset: 0x00015180
		internal bool Contains(RoutedEventHandlerInfoList handlers)
		{
			for (RoutedEventHandlerInfoList routedEventHandlerInfoList = this; routedEventHandlerInfoList != null; routedEventHandlerInfoList = routedEventHandlerInfoList.Next)
			{
				if (routedEventHandlerInfoList == handlers)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040004C1 RID: 1217
		internal RoutedEventHandlerInfo[] Handlers;

		// Token: 0x040004C2 RID: 1218
		internal RoutedEventHandlerInfoList Next;
	}
}
