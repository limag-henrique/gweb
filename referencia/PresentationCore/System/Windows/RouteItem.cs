using System;

namespace System.Windows
{
	// Token: 0x020001D8 RID: 472
	internal struct RouteItem
	{
		// Token: 0x06000CAC RID: 3244 RVA: 0x000303B8 File Offset: 0x0002F7B8
		internal RouteItem(object target, RoutedEventHandlerInfo routedEventHandlerInfo)
		{
			this._target = target;
			this._routedEventHandlerInfo = routedEventHandlerInfo;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x000303D4 File Offset: 0x0002F7D4
		internal object Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000303E8 File Offset: 0x0002F7E8
		internal void InvokeHandler(RoutedEventArgs routedEventArgs)
		{
			this._routedEventHandlerInfo.InvokeHandler(this._target, routedEventArgs);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00030408 File Offset: 0x0002F808
		public override bool Equals(object o)
		{
			return this.Equals((RouteItem)o);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00030424 File Offset: 0x0002F824
		public bool Equals(RouteItem routeItem)
		{
			return routeItem._target == this._target && routeItem._routedEventHandlerInfo == this._routedEventHandlerInfo;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00030454 File Offset: 0x0002F854
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00030474 File Offset: 0x0002F874
		public static bool operator ==(RouteItem routeItem1, RouteItem routeItem2)
		{
			return routeItem1.Equals(routeItem2);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0003048C File Offset: 0x0002F88C
		public static bool operator !=(RouteItem routeItem1, RouteItem routeItem2)
		{
			return !routeItem1.Equals(routeItem2);
		}

		// Token: 0x04000743 RID: 1859
		private object _target;

		// Token: 0x04000744 RID: 1860
		private RoutedEventHandlerInfo _routedEventHandlerInfo;
	}
}
