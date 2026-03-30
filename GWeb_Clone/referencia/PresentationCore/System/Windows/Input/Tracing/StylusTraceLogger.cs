using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using MS.Internal.Telemetry.PresentationCore;

namespace System.Windows.Input.Tracing
{
	// Token: 0x020002FD RID: 765
	internal static class StylusTraceLogger
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x00062370 File Offset: 0x00061770
		internal static void LogStartup()
		{
			StylusTraceLogger.Log(StylusTraceLogger.StartupEventTag);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00062388 File Offset: 0x00061788
		internal static void LogStatistics(StylusTraceLogger.StylusStatistics stylusData)
		{
			StylusTraceLogger.Requires<ArgumentNullException>(stylusData != null);
			StylusTraceLogger.Log<StylusTraceLogger.StylusStatistics>(StylusTraceLogger.StatisticsTag, stylusData);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000623AC File Offset: 0x000617AC
		internal static void LogReentrancyRetryLimitReached()
		{
			StylusTraceLogger.Log(StylusTraceLogger.ReentrancyRetryLimitTag);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000623C4 File Offset: 0x000617C4
		internal static void LogError(string error)
		{
			StylusTraceLogger.Requires<ArgumentNullException>(error != null);
			StylusTraceLogger.Log<StylusTraceLogger.StylusErrorEventData>(StylusTraceLogger.ErrorTag, new StylusTraceLogger.StylusErrorEventData
			{
				Error = error
			});
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000623F0 File Offset: 0x000617F0
		internal static void LogDeviceConnect(StylusTraceLogger.StylusDeviceInfo deviceInfo)
		{
			StylusTraceLogger.Requires<ArgumentNullException>(deviceInfo != null);
			StylusTraceLogger.Log<StylusTraceLogger.StylusDeviceInfo>(StylusTraceLogger.DeviceConnectTag, deviceInfo);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00062414 File Offset: 0x00061814
		internal static void LogDeviceDisconnect(int deviceId)
		{
			StylusTraceLogger.Log<StylusTraceLogger.StylusDisconnectInfo>(StylusTraceLogger.DeviceDisconnectTag, new StylusTraceLogger.StylusDisconnectInfo
			{
				Id = deviceId
			});
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00062438 File Offset: 0x00061838
		internal static void LogReentrancy([CallerMemberName] string functionName = "")
		{
			StylusTraceLogger.Log<StylusTraceLogger.ReentrancyEvent>(StylusTraceLogger.ReentrancyTag, new StylusTraceLogger.ReentrancyEvent
			{
				FunctionName = functionName
			});
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0006245C File Offset: 0x0006185C
		internal static void LogShutdown()
		{
			StylusTraceLogger.Log(StylusTraceLogger.ShutdownEventTag);
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00062474 File Offset: 0x00061874
		private static void Requires<T>(bool condition) where T : Exception, new()
		{
			if (!condition)
			{
				throw Activator.CreateInstance<T>();
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00062490 File Offset: 0x00061890
		private static void Log(string tag)
		{
			EventSource provider = TraceLoggingProvider.GetProvider();
			if (provider != null)
			{
				provider.Write(tag, TelemetryEventSource.MeasuresOptions());
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000624B4 File Offset: 0x000618B4
		private static void Log<T>(string tag, T data = default(T)) where T : class
		{
			EventSource provider = TraceLoggingProvider.GetProvider();
			if (provider != null)
			{
				provider.Write<T>(tag, TelemetryEventSource.MeasuresOptions(), data);
			}
		}

		// Token: 0x04000D41 RID: 3393
		private static readonly string StartupEventTag = "StylusStartup";

		// Token: 0x04000D42 RID: 3394
		private static readonly string ShutdownEventTag = "StylusShutdown";

		// Token: 0x04000D43 RID: 3395
		private static readonly string StatisticsTag = "StylusStatistics";

		// Token: 0x04000D44 RID: 3396
		private static readonly string ErrorTag = "StylusError";

		// Token: 0x04000D45 RID: 3397
		private static readonly string DeviceConnectTag = "StylusConnect";

		// Token: 0x04000D46 RID: 3398
		private static readonly string DeviceDisconnectTag = "StylusDisconnect";

		// Token: 0x04000D47 RID: 3399
		private static readonly string ReentrancyTag = "StylusReentrancy";

		// Token: 0x04000D48 RID: 3400
		private static readonly string ReentrancyRetryLimitTag = "StylusReentrancyRetryLimitReached";

		// Token: 0x02000836 RID: 2102
		[Flags]
		internal enum FeatureFlags
		{
			// Token: 0x040027CC RID: 10188
			None = 0,
			// Token: 0x040027CD RID: 10189
			CustomTouchDeviceUsed = 1,
			// Token: 0x040027CE RID: 10190
			StylusPluginsUsed = 2,
			// Token: 0x040027CF RID: 10191
			FlickScrollingUsed = 4,
			// Token: 0x040027D0 RID: 10192
			PointerStackEnabled = 268435456,
			// Token: 0x040027D1 RID: 10193
			WispStackEnabled = 536870912
		}

		// Token: 0x02000837 RID: 2103
		[EventData]
		internal class StylusStatistics
		{
			// Token: 0x170011CF RID: 4559
			// (get) Token: 0x06005698 RID: 22168 RVA: 0x0016357C File Offset: 0x0016297C
			// (set) Token: 0x06005699 RID: 22169 RVA: 0x00163590 File Offset: 0x00162990
			public StylusTraceLogger.FeatureFlags FeaturesUsed { get; set; }
		}

		// Token: 0x02000838 RID: 2104
		[EventData]
		internal class ReentrancyEvent
		{
			// Token: 0x170011D0 RID: 4560
			// (get) Token: 0x0600569B RID: 22171 RVA: 0x001635B8 File Offset: 0x001629B8
			// (set) Token: 0x0600569C RID: 22172 RVA: 0x001635CC File Offset: 0x001629CC
			public string FunctionName { get; set; } = string.Empty;
		}

		// Token: 0x02000839 RID: 2105
		[EventData]
		internal class StylusSize
		{
			// Token: 0x0600569E RID: 22174 RVA: 0x00163600 File Offset: 0x00162A00
			public StylusSize(Size size)
			{
				this.Width = size.Width;
				this.Height = size.Height;
			}

			// Token: 0x170011D1 RID: 4561
			// (get) Token: 0x0600569F RID: 22175 RVA: 0x0016364C File Offset: 0x00162A4C
			// (set) Token: 0x060056A0 RID: 22176 RVA: 0x00163660 File Offset: 0x00162A60
			public double Width { get; set; } = double.NaN;

			// Token: 0x170011D2 RID: 4562
			// (get) Token: 0x060056A1 RID: 22177 RVA: 0x00163674 File Offset: 0x00162A74
			// (set) Token: 0x060056A2 RID: 22178 RVA: 0x00163688 File Offset: 0x00162A88
			public double Height { get; set; } = double.NaN;
		}

		// Token: 0x0200083A RID: 2106
		[EventData]
		internal class StylusDeviceInfo
		{
			// Token: 0x060056A3 RID: 22179 RVA: 0x0016369C File Offset: 0x00162A9C
			public StylusDeviceInfo(int id, string name, string pnpId, TabletHardwareCapabilities capabilities, Size tabletSize, Size screenSize, TabletDeviceType deviceType, int maxContacts)
			{
				this.Id = id;
				this.Name = name;
				this.PlugAndPlayId = pnpId;
				this.Capabilities = capabilities.ToString("F");
				this.TabletSize = new StylusTraceLogger.StylusSize(tabletSize);
				this.ScreenSize = new StylusTraceLogger.StylusSize(screenSize);
				this.DeviceType = deviceType.ToString("F");
				this.MaxContacts = maxContacts;
			}

			// Token: 0x170011D3 RID: 4563
			// (get) Token: 0x060056A4 RID: 22180 RVA: 0x00163714 File Offset: 0x00162B14
			// (set) Token: 0x060056A5 RID: 22181 RVA: 0x00163728 File Offset: 0x00162B28
			public int Id { get; set; }

			// Token: 0x170011D4 RID: 4564
			// (get) Token: 0x060056A6 RID: 22182 RVA: 0x0016373C File Offset: 0x00162B3C
			// (set) Token: 0x060056A7 RID: 22183 RVA: 0x00163750 File Offset: 0x00162B50
			public string Name { get; set; }

			// Token: 0x170011D5 RID: 4565
			// (get) Token: 0x060056A8 RID: 22184 RVA: 0x00163764 File Offset: 0x00162B64
			// (set) Token: 0x060056A9 RID: 22185 RVA: 0x00163778 File Offset: 0x00162B78
			public string PlugAndPlayId { get; set; }

			// Token: 0x170011D6 RID: 4566
			// (get) Token: 0x060056AA RID: 22186 RVA: 0x0016378C File Offset: 0x00162B8C
			// (set) Token: 0x060056AB RID: 22187 RVA: 0x001637A0 File Offset: 0x00162BA0
			public string Capabilities { get; set; }

			// Token: 0x170011D7 RID: 4567
			// (get) Token: 0x060056AC RID: 22188 RVA: 0x001637B4 File Offset: 0x00162BB4
			// (set) Token: 0x060056AD RID: 22189 RVA: 0x001637C8 File Offset: 0x00162BC8
			public StylusTraceLogger.StylusSize TabletSize { get; set; }

			// Token: 0x170011D8 RID: 4568
			// (get) Token: 0x060056AE RID: 22190 RVA: 0x001637DC File Offset: 0x00162BDC
			// (set) Token: 0x060056AF RID: 22191 RVA: 0x001637F0 File Offset: 0x00162BF0
			public StylusTraceLogger.StylusSize ScreenSize { get; set; }

			// Token: 0x170011D9 RID: 4569
			// (get) Token: 0x060056B0 RID: 22192 RVA: 0x00163804 File Offset: 0x00162C04
			// (set) Token: 0x060056B1 RID: 22193 RVA: 0x00163818 File Offset: 0x00162C18
			public string DeviceType { get; set; }

			// Token: 0x170011DA RID: 4570
			// (get) Token: 0x060056B2 RID: 22194 RVA: 0x0016382C File Offset: 0x00162C2C
			// (set) Token: 0x060056B3 RID: 22195 RVA: 0x00163840 File Offset: 0x00162C40
			public int MaxContacts { get; set; }
		}

		// Token: 0x0200083B RID: 2107
		[EventData]
		internal class StylusDisconnectInfo
		{
			// Token: 0x170011DB RID: 4571
			// (get) Token: 0x060056B4 RID: 22196 RVA: 0x00163854 File Offset: 0x00162C54
			// (set) Token: 0x060056B5 RID: 22197 RVA: 0x00163868 File Offset: 0x00162C68
			public int Id { get; set; } = -1;
		}

		// Token: 0x0200083C RID: 2108
		[EventData]
		internal class StylusErrorEventData
		{
			// Token: 0x170011DC RID: 4572
			// (get) Token: 0x060056B7 RID: 22199 RVA: 0x00163898 File Offset: 0x00162C98
			// (set) Token: 0x060056B8 RID: 22200 RVA: 0x001638AC File Offset: 0x00162CAC
			public string Error { get; set; }
		}
	}
}
