using System;
using System.Diagnostics;

namespace MS.Internal
{
	// Token: 0x020006A3 RID: 1699
	internal static class TraceRoutedEvent
	{
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x001220E0 File Offset: 0x001214E0
		public static AvTraceDetails RaiseEvent
		{
			get
			{
				if (TraceRoutedEvent._RaiseEvent == null)
				{
					TraceRoutedEvent._RaiseEvent = new AvTraceDetails(1, new string[]
					{
						"Raise RoutedEvent",
						"RoutedEvent",
						"Element",
						"RoutedEventArgs",
						"Handled"
					});
				}
				return TraceRoutedEvent._RaiseEvent;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x00122134 File Offset: 0x00121534
		public static AvTraceDetails ReRaiseEventAs
		{
			get
			{
				if (TraceRoutedEvent._ReRaiseEventAs == null)
				{
					TraceRoutedEvent._ReRaiseEventAs = new AvTraceDetails(2, new string[]
					{
						"Raise RoutedEvent",
						"RoutedEvent",
						"Element",
						"RoutedEventArgs",
						"Handled"
					});
				}
				return TraceRoutedEvent._ReRaiseEventAs;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x00122188 File Offset: 0x00121588
		public static AvTraceDetails HandleEvent
		{
			get
			{
				if (TraceRoutedEvent._HandleEvent == null)
				{
					TraceRoutedEvent._HandleEvent = new AvTraceDetails(3, new string[]
					{
						"RoutedEvent has set Handled",
						"Handled",
						"EventOwnerType",
						"EventName",
						"RoutedEventArgs"
					});
				}
				return TraceRoutedEvent._HandleEvent;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x001221DC File Offset: 0x001215DC
		public static AvTraceDetails InvokeHandlers
		{
			get
			{
				if (TraceRoutedEvent._InvokeHandlers == null)
				{
					TraceRoutedEvent._InvokeHandlers = new AvTraceDetails(4, new string[]
					{
						"InvokeHandlers",
						"Element",
						"RoutedEventArgs",
						"Handled"
					});
				}
				return TraceRoutedEvent._InvokeHandlers;
			}
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x00122228 File Offset: 0x00121628
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, params object[] parameters)
		{
			TraceRoutedEvent._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, parameters);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x00122254 File Offset: 0x00121654
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails)
		{
			TraceRoutedEvent._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[0]);
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x00122284 File Offset: 0x00121684
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, object p1)
		{
			TraceRoutedEvent._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1
			});
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x001222B8 File Offset: 0x001216B8
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, object p1, object p2)
		{
			TraceRoutedEvent._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2
			});
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x001222F0 File Offset: 0x001216F0
		public static void Trace(TraceEventType type, AvTraceDetails traceDetails, object p1, object p2, object p3)
		{
			TraceRoutedEvent._avTrace.Trace(type, traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2,
				p3
			});
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x00122330 File Offset: 0x00121730
		public static void TraceActivityItem(AvTraceDetails traceDetails, params object[] parameters)
		{
			TraceRoutedEvent._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, parameters);
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x0012235C File Offset: 0x0012175C
		public static void TraceActivityItem(AvTraceDetails traceDetails)
		{
			TraceRoutedEvent._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[0]);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x0012238C File Offset: 0x0012178C
		public static void TraceActivityItem(AvTraceDetails traceDetails, object p1)
		{
			TraceRoutedEvent._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1
			});
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x001223C0 File Offset: 0x001217C0
		public static void TraceActivityItem(AvTraceDetails traceDetails, object p1, object p2)
		{
			TraceRoutedEvent._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2
			});
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x001223F8 File Offset: 0x001217F8
		public static void TraceActivityItem(AvTraceDetails traceDetails, object p1, object p2, object p3)
		{
			TraceRoutedEvent._avTrace.TraceStartStop(traceDetails.Id, traceDetails.Message, traceDetails.Labels, new object[]
			{
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06004A72 RID: 19058 RVA: 0x00122434 File Offset: 0x00121834
		public static bool IsEnabled
		{
			get
			{
				return TraceRoutedEvent._avTrace != null && TraceRoutedEvent._avTrace.IsEnabled;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x00122454 File Offset: 0x00121854
		public static bool IsEnabledOverride
		{
			get
			{
				return TraceRoutedEvent._avTrace.IsEnabledOverride;
			}
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x0012246C File Offset: 0x0012186C
		public static void Refresh()
		{
			TraceRoutedEvent._avTrace.Refresh();
		}

		// Token: 0x04001F7F RID: 8063
		private static AvTrace _avTrace = new AvTrace(() => PresentationTraceSources.RoutedEventSource, delegate()
		{
			PresentationTraceSources._RoutedEventSource = null;
		});

		// Token: 0x04001F80 RID: 8064
		private static AvTraceDetails _RaiseEvent;

		// Token: 0x04001F81 RID: 8065
		private static AvTraceDetails _ReRaiseEventAs;

		// Token: 0x04001F82 RID: 8066
		private static AvTraceDetails _HandleEvent;

		// Token: 0x04001F83 RID: 8067
		private static AvTraceDetails _InvokeHandlers;
	}
}
