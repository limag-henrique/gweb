using System;
using System.Diagnostics.Tracing;

namespace MS.Internal.Telemetry.PresentationCore
{
	// Token: 0x020007AA RID: 1962
	internal class TelemetryEventSource : EventSource
	{
		// Token: 0x06005277 RID: 21111 RVA: 0x00148BD8 File Offset: 0x00147FD8
		internal TelemetryEventSource(string eventSourceName) : base(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat, TelemetryEventSource.telemetryTraits)
		{
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00148BF4 File Offset: 0x00147FF4
		protected TelemetryEventSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat, TelemetryEventSource.telemetryTraits)
		{
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x00148C10 File Offset: 0x00148010
		internal static EventSourceOptions TelemetryOptions()
		{
			return new EventSourceOptions
			{
				Keywords = (EventKeywords)35184372088832L
			};
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00148C38 File Offset: 0x00148038
		internal static EventSourceOptions MeasuresOptions()
		{
			return new EventSourceOptions
			{
				Keywords = (EventKeywords)70368744177664L
			};
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00148C60 File Offset: 0x00148060
		internal static EventSourceOptions CriticalDataOptions()
		{
			return new EventSourceOptions
			{
				Keywords = (EventKeywords)140737488355328L
			};
		}

		// Token: 0x0400252D RID: 9517
		internal const EventKeywords Reserved44Keyword = (EventKeywords)17592186044416L;

		// Token: 0x0400252E RID: 9518
		internal const EventKeywords TelemetryKeyword = (EventKeywords)35184372088832L;

		// Token: 0x0400252F RID: 9519
		internal const EventKeywords MeasuresKeyword = (EventKeywords)70368744177664L;

		// Token: 0x04002530 RID: 9520
		internal const EventKeywords CriticalDataKeyword = (EventKeywords)140737488355328L;

		// Token: 0x04002531 RID: 9521
		internal const EventTags CoreData = (EventTags)524288;

		// Token: 0x04002532 RID: 9522
		internal const EventTags InjectXToken = (EventTags)1048576;

		// Token: 0x04002533 RID: 9523
		internal const EventTags RealtimeLatency = (EventTags)2097152;

		// Token: 0x04002534 RID: 9524
		internal const EventTags NormalLatency = (EventTags)4194304;

		// Token: 0x04002535 RID: 9525
		internal const EventTags CriticalPersistence = (EventTags)8388608;

		// Token: 0x04002536 RID: 9526
		internal const EventTags NormalPersistence = (EventTags)16777216;

		// Token: 0x04002537 RID: 9527
		internal const EventTags DropPii = (EventTags)33554432;

		// Token: 0x04002538 RID: 9528
		internal const EventTags HashPii = (EventTags)67108864;

		// Token: 0x04002539 RID: 9529
		internal const EventTags MarkPii = (EventTags)134217728;

		// Token: 0x0400253A RID: 9530
		internal const EventFieldTags DropPiiField = (EventFieldTags)67108864;

		// Token: 0x0400253B RID: 9531
		internal const EventFieldTags HashPiiField = (EventFieldTags)134217728;

		// Token: 0x0400253C RID: 9532
		private static readonly string[] telemetryTraits = new string[]
		{
			"ETW_GROUP",
			"{4f50731a-89cf-4782-b3e0-dce8c90476ba}"
		};
	}
}
