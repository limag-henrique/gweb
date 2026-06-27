using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.PresentationCore;

namespace System.Windows.Interop
{
	// Token: 0x0200031F RID: 799
	internal static class OSVersionHelper
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00067C34 File Offset: 0x00067034
		// (set) Token: 0x06001A44 RID: 6724 RVA: 0x00067C48 File Offset: 0x00067048
		internal static bool IsOsWindows10RS5OrGreater { get; set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00067C5C File Offset: 0x0006705C
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x00067C70 File Offset: 0x00067070
		internal static bool IsOsWindows10RS4OrGreater { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00067C84 File Offset: 0x00067084
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00067C98 File Offset: 0x00067098
		internal static bool IsOsWindows10RS3OrGreater { get; set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00067CAC File Offset: 0x000670AC
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x00067CC0 File Offset: 0x000670C0
		internal static bool IsOsWindows10RS2OrGreater { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00067CD4 File Offset: 0x000670D4
		// (set) Token: 0x06001A4C RID: 6732 RVA: 0x00067CE8 File Offset: 0x000670E8
		internal static bool IsOsWindows10RS1OrGreater { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00067CFC File Offset: 0x000670FC
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x00067D10 File Offset: 0x00067110
		internal static bool IsOsWindows10TH2OrGreater { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x00067D24 File Offset: 0x00067124
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x00067D38 File Offset: 0x00067138
		internal static bool IsOsWindows10TH1OrGreater { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x00067D4C File Offset: 0x0006714C
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x00067D60 File Offset: 0x00067160
		internal static bool IsOsWindows10OrGreater { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00067D74 File Offset: 0x00067174
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x00067D88 File Offset: 0x00067188
		internal static bool IsOsWindows8Point1OrGreater { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x00067D9C File Offset: 0x0006719C
		// (set) Token: 0x06001A56 RID: 6742 RVA: 0x00067DB0 File Offset: 0x000671B0
		internal static bool IsOsWindows8OrGreater { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x00067DC4 File Offset: 0x000671C4
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x00067DD8 File Offset: 0x000671D8
		internal static bool IsOsWindows7SP1OrGreater { get; set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00067DEC File Offset: 0x000671EC
		// (set) Token: 0x06001A5A RID: 6746 RVA: 0x00067E00 File Offset: 0x00067200
		internal static bool IsOsWindows7OrGreater { get; set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x00067E14 File Offset: 0x00067214
		// (set) Token: 0x06001A5C RID: 6748 RVA: 0x00067E28 File Offset: 0x00067228
		internal static bool IsOsWindowsVistaSP2OrGreater { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x00067E3C File Offset: 0x0006723C
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x00067E50 File Offset: 0x00067250
		internal static bool IsOsWindowsVistaSP1OrGreater { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x00067E64 File Offset: 0x00067264
		// (set) Token: 0x06001A60 RID: 6752 RVA: 0x00067E78 File Offset: 0x00067278
		internal static bool IsOsWindowsVistaOrGreater { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00067E8C File Offset: 0x0006728C
		// (set) Token: 0x06001A62 RID: 6754 RVA: 0x00067EA0 File Offset: 0x000672A0
		internal static bool IsOsWindowsXPSP3OrGreater { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x00067EB4 File Offset: 0x000672B4
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x00067EC8 File Offset: 0x000672C8
		internal static bool IsOsWindowsXPSP2OrGreater { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00067EDC File Offset: 0x000672DC
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x00067EF0 File Offset: 0x000672F0
		internal static bool IsOsWindowsXPSP1OrGreater { get; set; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x00067F04 File Offset: 0x00067304
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x00067F18 File Offset: 0x00067318
		internal static bool IsOsWindowsXPOrGreater { get; set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00067F2C File Offset: 0x0006732C
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00067F40 File Offset: 0x00067340
		internal static bool IsOsWindowsServer { get; set; }

		// Token: 0x06001A6B RID: 6763 RVA: 0x00067F54 File Offset: 0x00067354
		[SecurityCritical]
		static OSVersionHelper()
		{
			WpfLibraryLoader.EnsureLoaded("PresentationNative_v0400.dll");
			OSVersionHelper.IsOsWindows10RS5OrGreater = OSVersionHelper.IsWindows10RS5OrGreater();
			OSVersionHelper.IsOsWindows10RS4OrGreater = OSVersionHelper.IsWindows10RS4OrGreater();
			OSVersionHelper.IsOsWindows10RS3OrGreater = OSVersionHelper.IsWindows10RS3OrGreater();
			OSVersionHelper.IsOsWindows10RS2OrGreater = OSVersionHelper.IsWindows10RS2OrGreater();
			OSVersionHelper.IsOsWindows10RS1OrGreater = OSVersionHelper.IsWindows10RS1OrGreater();
			OSVersionHelper.IsOsWindows10TH2OrGreater = OSVersionHelper.IsWindows10TH2OrGreater();
			OSVersionHelper.IsOsWindows10TH1OrGreater = OSVersionHelper.IsWindows10TH1OrGreater();
			OSVersionHelper.IsOsWindows10OrGreater = OSVersionHelper.IsWindows10OrGreater();
			OSVersionHelper.IsOsWindows8Point1OrGreater = OSVersionHelper.IsWindows8Point1OrGreater();
			OSVersionHelper.IsOsWindows8OrGreater = OSVersionHelper.IsWindows8OrGreater();
			OSVersionHelper.IsOsWindows7SP1OrGreater = OSVersionHelper.IsWindows7SP1OrGreater();
			OSVersionHelper.IsOsWindows7OrGreater = OSVersionHelper.IsWindows7OrGreater();
			OSVersionHelper.IsOsWindowsVistaSP2OrGreater = OSVersionHelper.IsWindowsVistaSP2OrGreater();
			OSVersionHelper.IsOsWindowsVistaSP1OrGreater = OSVersionHelper.IsWindowsVistaSP1OrGreater();
			OSVersionHelper.IsOsWindowsVistaOrGreater = OSVersionHelper.IsWindowsVistaOrGreater();
			OSVersionHelper.IsOsWindowsXPSP3OrGreater = OSVersionHelper.IsWindowsXPSP3OrGreater();
			OSVersionHelper.IsOsWindowsXPSP2OrGreater = OSVersionHelper.IsWindowsXPSP2OrGreater();
			OSVersionHelper.IsOsWindowsXPSP1OrGreater = OSVersionHelper.IsWindowsXPSP1OrGreater();
			OSVersionHelper.IsOsWindowsXPOrGreater = OSVersionHelper.IsWindowsXPOrGreater();
			OSVersionHelper.IsOsWindowsServer = OSVersionHelper.IsWindowsServer();
		}

		// Token: 0x06001A6C RID: 6764
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10RS5OrGreater();

		// Token: 0x06001A6D RID: 6765
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10RS4OrGreater();

		// Token: 0x06001A6E RID: 6766
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10RS3OrGreater();

		// Token: 0x06001A6F RID: 6767
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10RS2OrGreater();

		// Token: 0x06001A70 RID: 6768
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10RS1OrGreater();

		// Token: 0x06001A71 RID: 6769
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10TH2OrGreater();

		// Token: 0x06001A72 RID: 6770
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10TH1OrGreater();

		// Token: 0x06001A73 RID: 6771
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows10OrGreater();

		// Token: 0x06001A74 RID: 6772
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows8Point1OrGreater();

		// Token: 0x06001A75 RID: 6773
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows8OrGreater();

		// Token: 0x06001A76 RID: 6774
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows7SP1OrGreater();

		// Token: 0x06001A77 RID: 6775
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindows7OrGreater();

		// Token: 0x06001A78 RID: 6776
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsVistaSP2OrGreater();

		// Token: 0x06001A79 RID: 6777
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsVistaSP1OrGreater();

		// Token: 0x06001A7A RID: 6778
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsVistaOrGreater();

		// Token: 0x06001A7B RID: 6779
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsXPSP3OrGreater();

		// Token: 0x06001A7C RID: 6780
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsXPSP2OrGreater();

		// Token: 0x06001A7D RID: 6781
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsXPSP1OrGreater();

		// Token: 0x06001A7E RID: 6782
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsXPOrGreater();

		// Token: 0x06001A7F RID: 6783
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("PresentationNative_v0400.dll", CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool IsWindowsServer();

		// Token: 0x06001A80 RID: 6784 RVA: 0x00068034 File Offset: 0x00067434
		internal static bool IsOsVersionOrGreater(OperatingSystemVersion osVer)
		{
			switch (osVer)
			{
			case OperatingSystemVersion.WindowsXPSP2:
				return OSVersionHelper.IsOsWindowsXPSP2OrGreater;
			case OperatingSystemVersion.WindowsXPSP3:
				return OSVersionHelper.IsOsWindowsXPSP3OrGreater;
			case OperatingSystemVersion.WindowsVista:
				return OSVersionHelper.IsOsWindowsVistaOrGreater;
			case OperatingSystemVersion.WindowsVistaSP1:
				return OSVersionHelper.IsOsWindowsVistaSP1OrGreater;
			case OperatingSystemVersion.WindowsVistaSP2:
				return OSVersionHelper.IsOsWindowsVistaSP2OrGreater;
			case OperatingSystemVersion.Windows7:
				return OSVersionHelper.IsOsWindows7OrGreater;
			case OperatingSystemVersion.Windows7SP1:
				return OSVersionHelper.IsOsWindows7SP1OrGreater;
			case OperatingSystemVersion.Windows8:
				return OSVersionHelper.IsOsWindows8OrGreater;
			case OperatingSystemVersion.Windows8Point1:
				return OSVersionHelper.IsOsWindows8Point1OrGreater;
			case OperatingSystemVersion.Windows10:
				return OSVersionHelper.IsOsWindows10OrGreater;
			case OperatingSystemVersion.Windows10TH2:
				return OSVersionHelper.IsOsWindows10TH2OrGreater;
			case OperatingSystemVersion.Windows10RS1:
				return OSVersionHelper.IsOsWindows10RS1OrGreater;
			case OperatingSystemVersion.Windows10RS2:
				return OSVersionHelper.IsOsWindows10RS2OrGreater;
			case OperatingSystemVersion.Windows10RS3:
				return OSVersionHelper.IsOsWindows10RS3OrGreater;
			case OperatingSystemVersion.Windows10RS4:
				return OSVersionHelper.IsOsWindows10RS4OrGreater;
			case OperatingSystemVersion.Windows10RS5:
				return OSVersionHelper.IsOsWindows10RS5OrGreater;
			default:
				throw new ArgumentException(string.Format("{0} is not a valid OS!", osVer.ToString()), "osVer");
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0006810C File Offset: 0x0006750C
		internal static OperatingSystemVersion GetOsVersion()
		{
			if (OSVersionHelper.IsOsWindows10RS5OrGreater)
			{
				return OperatingSystemVersion.Windows10RS5;
			}
			if (OSVersionHelper.IsOsWindows10RS4OrGreater)
			{
				return OperatingSystemVersion.Windows10RS4;
			}
			if (OSVersionHelper.IsOsWindows10RS3OrGreater)
			{
				return OperatingSystemVersion.Windows10RS3;
			}
			if (OSVersionHelper.IsOsWindows10RS2OrGreater)
			{
				return OperatingSystemVersion.Windows10RS2;
			}
			if (OSVersionHelper.IsOsWindows10RS1OrGreater)
			{
				return OperatingSystemVersion.Windows10RS1;
			}
			if (OSVersionHelper.IsOsWindows10TH2OrGreater)
			{
				return OperatingSystemVersion.Windows10TH2;
			}
			if (OSVersionHelper.IsOsWindows10OrGreater)
			{
				return OperatingSystemVersion.Windows10;
			}
			if (OSVersionHelper.IsOsWindows8Point1OrGreater)
			{
				return OperatingSystemVersion.Windows8Point1;
			}
			if (OSVersionHelper.IsOsWindows8OrGreater)
			{
				return OperatingSystemVersion.Windows8;
			}
			if (OSVersionHelper.IsOsWindows7SP1OrGreater)
			{
				return OperatingSystemVersion.Windows7SP1;
			}
			if (OSVersionHelper.IsOsWindows7OrGreater)
			{
				return OperatingSystemVersion.Windows7;
			}
			if (OSVersionHelper.IsOsWindowsVistaSP2OrGreater)
			{
				return OperatingSystemVersion.WindowsVistaSP2;
			}
			if (OSVersionHelper.IsOsWindowsVistaSP1OrGreater)
			{
				return OperatingSystemVersion.WindowsVistaSP1;
			}
			if (OSVersionHelper.IsOsWindowsVistaOrGreater)
			{
				return OperatingSystemVersion.WindowsVista;
			}
			if (OSVersionHelper.IsOsWindowsXPSP3OrGreater)
			{
				return OperatingSystemVersion.WindowsXPSP3;
			}
			if (OSVersionHelper.IsOsWindowsXPSP2OrGreater)
			{
				return OperatingSystemVersion.WindowsXPSP2;
			}
			throw new Exception("OSVersionHelper.GetOsVersion Could not detect OS!");
		}
	}
}
