using System;
using System.Diagnostics.Tracing;

namespace MS.Internal.Telemetry.PresentationCore
{
	// Token: 0x020007AB RID: 1963
	internal static class TraceLoggingProvider
	{
		// Token: 0x0600527D RID: 21117 RVA: 0x00148CB0 File Offset: 0x001480B0
		internal static EventSource GetProvider()
		{
			if (TraceLoggingProvider._logger == null)
			{
				object lockObject = TraceLoggingProvider._lockObject;
				lock (lockObject)
				{
					if (TraceLoggingProvider._logger == null)
					{
						try
						{
							TraceLoggingProvider._logger = new TelemetryEventSource(TraceLoggingProvider.ProviderName);
						}
						catch (ArgumentException)
						{
						}
					}
				}
			}
			return TraceLoggingProvider._logger;
		}

		// Token: 0x0400253D RID: 9533
		private static EventSource _logger;

		// Token: 0x0400253E RID: 9534
		private static object _lockObject = new object();

		// Token: 0x0400253F RID: 9535
		private static readonly string ProviderName = "Microsoft.DOTNET.WPF.PresentationCore";
	}
}
