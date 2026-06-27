using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using Microsoft.Win32.SafeHandles;
using MS.Utility;
using MS.Win32;

namespace MS.Internal
{
	// Token: 0x02000689 RID: 1673
	internal static class DpiUtil
	{
		// Token: 0x060049AA RID: 18858 RVA: 0x0011F0C8 File Offset: 0x0011E4C8
		[SecurityCritical]
		internal static DpiAwarenessContextHandle GetDpiAwarenessContext(IntPtr hWnd)
		{
			return DpiUtil.DpiAwarenessContextHelper.GetDpiAwarenessContext(hWnd);
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x0011F0DC File Offset: 0x0011E4DC
		[SecuritySafeCritical]
		internal static NativeMethods.PROCESS_DPI_AWARENESS GetProcessDpiAwareness(IntPtr hWnd)
		{
			return DpiUtil.ProcessDpiAwarenessHelper.GetProcessDpiAwareness(hWnd);
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0011F0F0 File Offset: 0x0011E4F0
		internal static DpiAwarenessContextValue GetProcessDpiAwarenessContextValue(IntPtr hWnd)
		{
			NativeMethods.PROCESS_DPI_AWARENESS processDpiAwareness = DpiUtil.ProcessDpiAwarenessHelper.GetProcessDpiAwareness(hWnd);
			return (DpiAwarenessContextValue)DpiUtil.DpiAwarenessContextHelper.GetProcessDpiAwarenessContext(processDpiAwareness);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x0011F110 File Offset: 0x0011E510
		internal static NativeMethods.PROCESS_DPI_AWARENESS GetLegacyProcessDpiAwareness()
		{
			return DpiUtil.ProcessDpiAwarenessHelper.GetLegacyProcessDpiAwareness();
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x0011F124 File Offset: 0x0011E524
		internal static DpiScale2 GetSystemDpi()
		{
			return DpiUtil.SystemDpiHelper.GetSystemDpi();
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x0011F138 File Offset: 0x0011E538
		internal static DpiScale2 GetSystemDpiFromUIElementCache()
		{
			return DpiUtil.SystemDpiHelper.GetSystemDpiFromUIElementCache();
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x0011F14C File Offset: 0x0011E54C
		internal static void UpdateUIElementCacheForSystemDpi(DpiScale2 systemDpiScale)
		{
			DpiUtil.SystemDpiHelper.UpdateUIElementCacheForSystemDpi(systemDpiScale);
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x0011F160 File Offset: 0x0011E560
		internal static DpiScale2 GetWindowDpi(IntPtr hWnd, bool fallbackToNearestMonitorHeuristic)
		{
			return DpiUtil.WindowDpiScaleHelper.GetWindowDpi(hWnd, fallbackToNearestMonitorHeuristic);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0011F174 File Offset: 0x0011E574
		[SecuritySafeCritical]
		internal static DpiUtil.HwndDpiInfo GetExtendedDpiInfoForWindow(IntPtr hWnd, bool fallbackToNearestMonitorHeuristic)
		{
			return new DpiUtil.HwndDpiInfo(hWnd, fallbackToNearestMonitorHeuristic);
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x0011F188 File Offset: 0x0011E588
		[SecuritySafeCritical]
		internal static DpiUtil.HwndDpiInfo GetExtendedDpiInfoForWindow(IntPtr hWnd)
		{
			return DpiUtil.GetExtendedDpiInfoForWindow(hWnd, true);
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0011F19C File Offset: 0x0011E59C
		internal static IDisposable WithDpiAwarenessContext(DpiAwarenessContextValue dpiAwarenessContext)
		{
			return new DpiUtil.DpiAwarenessScope(dpiAwarenessContext);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0011F1B0 File Offset: 0x0011E5B0
		internal static DpiFlags UpdateDpiScalesAndGetIndex(double pixelsPerInchX, double pixelsPerInchY)
		{
			object dpiLock = UIElement.DpiLock;
			DpiFlags result;
			lock (dpiLock)
			{
				int count = UIElement.DpiScaleXValues.Count;
				int num = 0;
				while (num < count && (UIElement.DpiScaleXValues[num] != pixelsPerInchX / 96.0 || UIElement.DpiScaleYValues[num] != pixelsPerInchY / 96.0))
				{
					num++;
				}
				if (num == count)
				{
					UIElement.DpiScaleXValues.Add(pixelsPerInchX / 96.0);
					UIElement.DpiScaleYValues.Add(pixelsPerInchY / 96.0);
				}
				bool dpiScaleFlag;
				bool dpiScaleFlag2;
				if (num < 3)
				{
					dpiScaleFlag = ((num & 1) != 0);
					dpiScaleFlag2 = ((num & 2) != 0);
				}
				else
				{
					dpiScaleFlag2 = (dpiScaleFlag = true);
				}
				result = new DpiFlags(dpiScaleFlag, dpiScaleFlag2, num);
			}
			return result;
		}

		// Token: 0x04001D0D RID: 7437
		internal const double DefaultPixelsPerInch = 96.0;

		// Token: 0x020009AA RID: 2474
		private static class DpiAwarenessContextHelper
		{
			// Token: 0x17001274 RID: 4724
			// (get) Token: 0x06005A62 RID: 23138 RVA: 0x0016B524 File Offset: 0x0016A924
			// (set) Token: 0x06005A63 RID: 23139 RVA: 0x0016B538 File Offset: 0x0016A938
			private static bool IsGetWindowDpiAwarenessContextMethodSupported { get; set; } = true;

			// Token: 0x06005A64 RID: 23140 RVA: 0x0016B54C File Offset: 0x0016A94C
			[SecurityCritical]
			internal static DpiAwarenessContextHandle GetDpiAwarenessContext(IntPtr hWnd)
			{
				if (DpiUtil.DpiAwarenessContextHelper.IsGetWindowDpiAwarenessContextMethodSupported)
				{
					try
					{
						return DpiUtil.DpiAwarenessContextHelper.GetWindowDpiAwarenessContext(hWnd);
					}
					catch (Exception ex) when (ex is EntryPointNotFoundException || ex is MissingMethodException || ex is DllNotFoundException)
					{
						DpiUtil.DpiAwarenessContextHelper.IsGetWindowDpiAwarenessContextMethodSupported = false;
					}
				}
				return DpiUtil.DpiAwarenessContextHelper.GetProcessDpiAwarenessContext(hWnd);
			}

			// Token: 0x06005A65 RID: 23141 RVA: 0x0016B5C4 File Offset: 0x0016A9C4
			[SecurityCritical]
			private static DpiAwarenessContextHandle GetProcessDpiAwarenessContext(IntPtr hWnd)
			{
				NativeMethods.PROCESS_DPI_AWARENESS processDpiAwareness = DpiUtil.ProcessDpiAwarenessHelper.GetProcessDpiAwareness(hWnd);
				return DpiUtil.DpiAwarenessContextHelper.GetProcessDpiAwarenessContext(processDpiAwareness);
			}

			// Token: 0x06005A66 RID: 23142 RVA: 0x0016B5E0 File Offset: 0x0016A9E0
			internal static DpiAwarenessContextHandle GetProcessDpiAwarenessContext(NativeMethods.PROCESS_DPI_AWARENESS dpiAwareness)
			{
				switch (dpiAwareness)
				{
				case NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE:
					return NativeMethods.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE;
				case NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE:
					return NativeMethods.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE;
				}
				return NativeMethods.DPI_AWARENESS_CONTEXT_UNAWARE;
			}

			// Token: 0x06005A67 RID: 23143 RVA: 0x0016B614 File Offset: 0x0016AA14
			private static DpiAwarenessContextHandle GetWindowDpiAwarenessContext(IntPtr hWnd)
			{
				return SafeNativeMethods.GetWindowDpiAwarenessContext(hWnd);
			}
		}

		// Token: 0x020009AB RID: 2475
		internal class DpiAwarenessScope : IDisposable
		{
			// Token: 0x06005A69 RID: 23145 RVA: 0x0016B63C File Offset: 0x0016AA3C
			public DpiAwarenessScope(DpiAwarenessContextValue dpiAwarenessContextEnumValue) : this(dpiAwarenessContextEnumValue, false, false, IntPtr.Zero)
			{
			}

			// Token: 0x06005A6A RID: 23146 RVA: 0x0016B658 File Offset: 0x0016AA58
			public DpiAwarenessScope(DpiAwarenessContextValue dpiAwarenessContextEnumValue, bool updateIfThreadInMixedHostingMode) : this(dpiAwarenessContextEnumValue, updateIfThreadInMixedHostingMode, false, IntPtr.Zero)
			{
			}

			// Token: 0x06005A6B RID: 23147 RVA: 0x0016B674 File Offset: 0x0016AA74
			public DpiAwarenessScope(DpiAwarenessContextValue dpiAwarenessContextEnumValue, bool updateIfThreadInMixedHostingMode, IntPtr hWnd) : this(dpiAwarenessContextEnumValue, updateIfThreadInMixedHostingMode, true, hWnd)
			{
			}

			// Token: 0x06005A6C RID: 23148 RVA: 0x0016B68C File Offset: 0x0016AA8C
			[SecuritySafeCritical]
			private DpiAwarenessScope(DpiAwarenessContextValue dpiAwarenessContextValue, bool updateIfThreadInMixedHostingMode, bool updateIfWindowIsSystemAwareOrUnaware, IntPtr hWnd)
			{
				if (dpiAwarenessContextValue == DpiAwarenessContextValue.Invalid)
				{
					return;
				}
				if (!DpiUtil.DpiAwarenessScope.OperationSupported)
				{
					return;
				}
				if (updateIfThreadInMixedHostingMode && !this.IsThreadInMixedHostingBehavior)
				{
					return;
				}
				if (updateIfWindowIsSystemAwareOrUnaware && (hWnd == IntPtr.Zero || !this.IsWindowUnawareOrSystemAware(hWnd)))
				{
					return;
				}
				try
				{
					this.OldDpiAwarenessContext = UnsafeNativeMethods.SetThreadDpiAwarenessContext(new DpiAwarenessContextHandle(dpiAwarenessContextValue));
				}
				catch (Exception ex) when (ex is EntryPointNotFoundException || ex is MissingMethodException || ex is DllNotFoundException)
				{
					DpiUtil.DpiAwarenessScope.OperationSupported = false;
				}
			}

			// Token: 0x17001275 RID: 4725
			// (get) Token: 0x06005A6D RID: 23149 RVA: 0x0016B73C File Offset: 0x0016AB3C
			// (set) Token: 0x06005A6E RID: 23150 RVA: 0x0016B750 File Offset: 0x0016AB50
			private static bool OperationSupported { get; set; } = true;

			// Token: 0x17001276 RID: 4726
			// (get) Token: 0x06005A6F RID: 23151 RVA: 0x0016B764 File Offset: 0x0016AB64
			private bool IsThreadInMixedHostingBehavior
			{
				[SecuritySafeCritical]
				get
				{
					return SafeNativeMethods.GetThreadDpiHostingBehavior() == NativeMethods.DPI_HOSTING_BEHAVIOR.DPI_HOSTING_BEHAVIOR_MIXED;
				}
			}

			// Token: 0x17001277 RID: 4727
			// (get) Token: 0x06005A70 RID: 23152 RVA: 0x0016B77C File Offset: 0x0016AB7C
			// (set) Token: 0x06005A71 RID: 23153 RVA: 0x0016B790 File Offset: 0x0016AB90
			private DpiAwarenessContextHandle OldDpiAwarenessContext { get; set; }

			// Token: 0x06005A72 RID: 23154 RVA: 0x0016B7A4 File Offset: 0x0016ABA4
			[SecuritySafeCritical]
			public void Dispose()
			{
				if (this.OldDpiAwarenessContext != null)
				{
					UnsafeNativeMethods.SetThreadDpiAwarenessContext(this.OldDpiAwarenessContext);
					this.OldDpiAwarenessContext = null;
				}
			}

			// Token: 0x06005A73 RID: 23155 RVA: 0x0016B7CC File Offset: 0x0016ABCC
			[SecuritySafeCritical]
			private bool IsWindowUnawareOrSystemAware(IntPtr hWnd)
			{
				DpiAwarenessContextHandle dpiAwarenessContext = DpiUtil.GetDpiAwarenessContext(hWnd);
				return dpiAwarenessContext.Equals(DpiAwarenessContextValue.Unaware) || dpiAwarenessContext.Equals(DpiAwarenessContextValue.SystemAware);
			}
		}

		// Token: 0x020009AC RID: 2476
		public class HwndDpiInfo : Tuple<DpiAwarenessContextValue, DpiScale2>
		{
			// Token: 0x06005A75 RID: 23157 RVA: 0x0016B808 File Offset: 0x0016AC08
			[SecuritySafeCritical]
			internal HwndDpiInfo(IntPtr hWnd, bool fallbackToNearestMonitorHeuristic) : base((DpiAwarenessContextValue)DpiUtil.GetDpiAwarenessContext(hWnd), DpiUtil.GetWindowDpi(hWnd, fallbackToNearestMonitorHeuristic))
			{
				this.ContainingMonitorScreenRect = DpiUtil.HwndDpiInfo.NearestMonitorInfoFromWindow(hWnd).rcMonitor;
			}

			// Token: 0x06005A76 RID: 23158 RVA: 0x0016B840 File Offset: 0x0016AC40
			internal HwndDpiInfo(DpiAwarenessContextValue dpiAwarenessContextValue, DpiScale2 dpiScale) : base(dpiAwarenessContextValue, dpiScale)
			{
				this.ContainingMonitorScreenRect = DpiUtil.HwndDpiInfo.NearestMonitorInfoFromWindow(IntPtr.Zero).rcMonitor;
			}

			// Token: 0x06005A77 RID: 23159 RVA: 0x0016B86C File Offset: 0x0016AC6C
			[SecuritySafeCritical]
			private static NativeMethods.MONITORINFOEX NearestMonitorInfoFromWindow(IntPtr hwnd)
			{
				IntPtr intPtr = SafeNativeMethods.MonitorFromWindow(new HandleRef(null, hwnd), 2);
				if (intPtr == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				NativeMethods.MONITORINFOEX monitorinfoex = new NativeMethods.MONITORINFOEX();
				SafeNativeMethods.GetMonitorInfo(new HandleRef(null, intPtr), monitorinfoex);
				return monitorinfoex;
			}

			// Token: 0x17001278 RID: 4728
			// (get) Token: 0x06005A78 RID: 23160 RVA: 0x0016B8B0 File Offset: 0x0016ACB0
			internal NativeMethods.RECT ContainingMonitorScreenRect { get; }

			// Token: 0x17001279 RID: 4729
			// (get) Token: 0x06005A79 RID: 23161 RVA: 0x0016B8C4 File Offset: 0x0016ACC4
			internal DpiAwarenessContextValue DpiAwarenessContextValue
			{
				get
				{
					return base.Item1;
				}
			}

			// Token: 0x1700127A RID: 4730
			// (get) Token: 0x06005A7A RID: 23162 RVA: 0x0016B8D8 File Offset: 0x0016ACD8
			internal DpiScale2 DpiScale
			{
				get
				{
					return base.Item2;
				}
			}
		}

		// Token: 0x020009AD RID: 2477
		private static class ProcessDpiAwarenessHelper
		{
			// Token: 0x1700127B RID: 4731
			// (get) Token: 0x06005A7B RID: 23163 RVA: 0x0016B8EC File Offset: 0x0016ACEC
			// (set) Token: 0x06005A7C RID: 23164 RVA: 0x0016B900 File Offset: 0x0016AD00
			private static bool IsGetProcessDpiAwarenessFunctionSupported { get; set; } = true;

			// Token: 0x06005A7D RID: 23165 RVA: 0x0016B914 File Offset: 0x0016AD14
			[SecuritySafeCritical]
			internal static NativeMethods.PROCESS_DPI_AWARENESS GetLegacyProcessDpiAwareness()
			{
				if (!UnsafeNativeMethods.IsProcessDPIAware())
				{
					return NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
				}
				return NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE;
			}

			// Token: 0x06005A7E RID: 23166 RVA: 0x0016B92C File Offset: 0x0016AD2C
			[SecuritySafeCritical]
			internal static NativeMethods.PROCESS_DPI_AWARENESS GetProcessDpiAwareness(IntPtr hWnd)
			{
				if (DpiUtil.ProcessDpiAwarenessHelper.IsGetProcessDpiAwarenessFunctionSupported)
				{
					try
					{
						try
						{
							return DpiUtil.ProcessDpiAwarenessHelper.GetProcessDpiAwarenessFromWindow(hWnd);
						}
						catch (Exception ex) when (ex is EntryPointNotFoundException || ex is MissingMethodException || ex is DllNotFoundException)
						{
							DpiUtil.ProcessDpiAwarenessHelper.IsGetProcessDpiAwarenessFunctionSupported = false;
						}
					}
					catch (Exception ex2) when (ex2 is ArgumentException || ex2 is UnauthorizedAccessException || ex2 is COMException)
					{
					}
				}
				return DpiUtil.ProcessDpiAwarenessHelper.GetLegacyProcessDpiAwareness();
			}

			// Token: 0x06005A7F RID: 23167 RVA: 0x0016B9F0 File Offset: 0x0016ADF0
			[SecuritySafeCritical]
			private static NativeMethods.PROCESS_DPI_AWARENESS GetProcessDpiAwarenessFromWindow(IntPtr hWnd)
			{
				int dwProcessId = 0;
				if (hWnd != IntPtr.Zero)
				{
					UnsafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, hWnd), out dwProcessId);
				}
				else
				{
					dwProcessId = SafeNativeMethods.GetCurrentProcessId();
				}
				NativeMethods.PROCESS_DPI_AWARENESS processDpiAwareness;
				using (SafeProcessHandle safeProcessHandle = new SafeProcessHandle(UnsafeNativeMethods.OpenProcess(2035711, false, dwProcessId), true))
				{
					processDpiAwareness = SafeNativeMethods.GetProcessDpiAwareness(new HandleRef(null, safeProcessHandle.DangerousGetHandle()));
				}
				return processDpiAwareness;
			}
		}

		// Token: 0x020009AE RID: 2478
		private static class SystemDpiHelper
		{
			// Token: 0x1700127C RID: 4732
			// (get) Token: 0x06005A81 RID: 23169 RVA: 0x0016BA88 File Offset: 0x0016AE88
			// (set) Token: 0x06005A82 RID: 23170 RVA: 0x0016BA9C File Offset: 0x0016AE9C
			private static bool IsGetDpiForSystemFunctionAvailable { get; set; } = true;

			// Token: 0x06005A83 RID: 23171 RVA: 0x0016BAB0 File Offset: 0x0016AEB0
			internal static DpiScale2 GetSystemDpi()
			{
				if (DpiUtil.SystemDpiHelper.IsGetDpiForSystemFunctionAvailable)
				{
					try
					{
						return DpiUtil.SystemDpiHelper.GetDpiForSystem();
					}
					catch (Exception ex) when (ex is EntryPointNotFoundException || ex is MissingMethodException || ex is DllNotFoundException)
					{
						DpiUtil.SystemDpiHelper.IsGetDpiForSystemFunctionAvailable = false;
					}
				}
				return DpiUtil.SystemDpiHelper.GetSystemDpiFromDeviceCaps();
			}

			// Token: 0x06005A84 RID: 23172 RVA: 0x0016BB28 File Offset: 0x0016AF28
			internal static DpiScale2 GetSystemDpiFromUIElementCache()
			{
				object dpiLock = UIElement.DpiLock;
				DpiScale2 result;
				lock (dpiLock)
				{
					result = new DpiScale2(UIElement.DpiScaleXValues[0], UIElement.DpiScaleYValues[0]);
				}
				return result;
			}

			// Token: 0x06005A85 RID: 23173 RVA: 0x0016BB8C File Offset: 0x0016AF8C
			internal static void UpdateUIElementCacheForSystemDpi(DpiScale2 systemDpiScale)
			{
				object dpiLock = UIElement.DpiLock;
				lock (dpiLock)
				{
					UIElement.DpiScaleXValues.Insert(0, systemDpiScale.DpiScaleX);
					UIElement.DpiScaleYValues.Insert(0, systemDpiScale.DpiScaleY);
				}
			}

			// Token: 0x06005A86 RID: 23174 RVA: 0x0016BBF4 File Offset: 0x0016AFF4
			private static DpiScale2 GetDpiForSystem()
			{
				uint dpiForSystem = SafeNativeMethods.GetDpiForSystem();
				return DpiScale2.FromPixelsPerInch(dpiForSystem, dpiForSystem);
			}

			// Token: 0x06005A87 RID: 23175 RVA: 0x0016BC14 File Offset: 0x0016B014
			[SecuritySafeCritical]
			private static DpiScale2 GetSystemDpiFromDeviceCaps()
			{
				HandleRef hWnd = new HandleRef(IntPtr.Zero, IntPtr.Zero);
				HandleRef hDC = new HandleRef(IntPtr.Zero, UnsafeNativeMethods.GetDC(hWnd));
				if (hDC.Handle == IntPtr.Zero)
				{
					return null;
				}
				DpiScale2 result;
				try
				{
					int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(hDC, 88);
					int deviceCaps2 = UnsafeNativeMethods.GetDeviceCaps(hDC, 90);
					result = DpiScale2.FromPixelsPerInch((double)deviceCaps, (double)deviceCaps2);
				}
				finally
				{
					UnsafeNativeMethods.ReleaseDC(hWnd, hDC);
				}
				return result;
			}
		}

		// Token: 0x020009AF RID: 2479
		private static class WindowDpiScaleHelper
		{
			// Token: 0x1700127D RID: 4733
			// (get) Token: 0x06005A89 RID: 23177 RVA: 0x0016BCC0 File Offset: 0x0016B0C0
			// (set) Token: 0x06005A8A RID: 23178 RVA: 0x0016BCD4 File Offset: 0x0016B0D4
			private static bool IsGetDpiForWindowFunctionEnabled { get; set; } = true;

			// Token: 0x06005A8B RID: 23179 RVA: 0x0016BCE8 File Offset: 0x0016B0E8
			internal static DpiScale2 GetWindowDpi(IntPtr hWnd, bool fallbackToNearestMonitorHeuristic)
			{
				if (DpiUtil.WindowDpiScaleHelper.IsGetDpiForWindowFunctionEnabled)
				{
					try
					{
						try
						{
							return DpiUtil.WindowDpiScaleHelper.GetDpiForWindow(hWnd);
						}
						catch (Exception ex) when (ex is EntryPointNotFoundException || ex is MissingMethodException || ex is DllNotFoundException)
						{
							DpiUtil.WindowDpiScaleHelper.IsGetDpiForWindowFunctionEnabled = false;
						}
					}
					catch (Exception ex2) when (ex2 is COMException)
					{
					}
				}
				if (fallbackToNearestMonitorHeuristic)
				{
					try
					{
						return DpiUtil.WindowDpiScaleHelper.GetDpiForWindowFromNearestMonitor(hWnd);
					}
					catch (Exception ex3) when (ex3 is COMException)
					{
					}
				}
				return null;
			}

			// Token: 0x06005A8C RID: 23180 RVA: 0x0016BDD8 File Offset: 0x0016B1D8
			private static DpiScale2 GetDpiForWindow(IntPtr hWnd)
			{
				uint dpiForWindow = SafeNativeMethods.GetDpiForWindow(new HandleRef(IntPtr.Zero, hWnd));
				return DpiScale2.FromPixelsPerInch(dpiForWindow, dpiForWindow);
			}

			// Token: 0x06005A8D RID: 23181 RVA: 0x0016BE08 File Offset: 0x0016B208
			[SecuritySafeCritical]
			private static DpiScale2 GetDpiForWindowFromNearestMonitor(IntPtr hWnd)
			{
				IntPtr handle = SafeNativeMethods.MonitorFromWindow(new HandleRef(IntPtr.Zero, hWnd), 2);
				uint num;
				uint num2;
				int dpiForMonitor = (int)UnsafeNativeMethods.GetDpiForMonitor(new HandleRef(IntPtr.Zero, handle), NativeMethods.MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out num, out num2);
				Marshal.ThrowExceptionForHR(dpiForMonitor);
				return DpiScale2.FromPixelsPerInch(num, num2);
			}
		}
	}
}
