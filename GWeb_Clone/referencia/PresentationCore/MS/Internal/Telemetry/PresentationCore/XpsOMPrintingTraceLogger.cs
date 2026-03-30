using System;
using System.Diagnostics.Tracing;

namespace MS.Internal.Telemetry.PresentationCore
{
	// Token: 0x020007AC RID: 1964
	internal static class XpsOMPrintingTraceLogger
	{
		// Token: 0x0600527F RID: 21119 RVA: 0x00148D58 File Offset: 0x00148158
		internal static void LogXpsOMStatus(bool enabled)
		{
			EventSource provider = TraceLoggingProvider.GetProvider();
			if (provider != null)
			{
				provider.Write<XpsOMPrintingTraceLogger.XpsOMStatus>(XpsOMPrintingTraceLogger.XpsOMEnabled, TelemetryEventSource.MeasuresOptions(), new XpsOMPrintingTraceLogger.XpsOMStatus
				{
					Enabled = enabled
				});
			}
		}

		// Token: 0x04002540 RID: 9536
		private static readonly string XpsOMEnabled = "XpsOMEnabled";

		// Token: 0x020009FD RID: 2557
		[EventData]
		internal class XpsOMStatus
		{
			// Token: 0x170012CA RID: 4810
			// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x001712F8 File Offset: 0x001706F8
			// (set) Token: 0x06005BE3 RID: 23523 RVA: 0x0017130C File Offset: 0x0017070C
			public bool Enabled { get; set; } = true;
		}
	}
}
