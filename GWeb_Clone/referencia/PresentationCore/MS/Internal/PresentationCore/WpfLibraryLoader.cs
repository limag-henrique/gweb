using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace MS.Internal.PresentationCore
{
	// Token: 0x020007E9 RID: 2025
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SuppressUnmanagedCodeSecurity]
	internal static class WpfLibraryLoader
	{
		// Token: 0x060054DF RID: 21727
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060054E0 RID: 21728 RVA: 0x0015E358 File Offset: 0x0015D758
		private static string WpfInstallPath { get; } = WpfLibraryLoader.GetWPFInstallPath();

		// Token: 0x060054E1 RID: 21729 RVA: 0x0015E36C File Offset: 0x0015D76C
		internal static void EnsureLoaded(string dllName)
		{
			WpfLibraryLoader.LoadLibrary(Path.Combine(WpfLibraryLoader.WpfInstallPath, dllName));
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x0015E38C File Offset: 0x0015D78C
		[EnvironmentPermission(SecurityAction.Assert, Read = "COMPLUS_Version;COMPLUS_InstallRoot")]
		private static string GetWPFInstallPath()
		{
			string text = null;
			string environmentVariable = Environment.GetEnvironmentVariable("COMPLUS_Version");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				text = Environment.GetEnvironmentVariable("COMPLUS_InstallRoot");
				if (string.IsNullOrEmpty(text))
				{
					text = WpfLibraryLoader.ReadLocalMachineString("Software\\Microsoft\\.NETFramework", "InstallRoot");
				}
				if (!string.IsNullOrEmpty(text))
				{
					text = Path.Combine(text, environmentVariable);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				if (RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
				{
					text = RuntimeEnvironment.GetRuntimeDirectory();
				}
				if (string.IsNullOrEmpty(text))
				{
					text = WpfLibraryLoader.ReadLocalMachineString("Software\\Microsoft\\Net Framework Setup\\NDP\\v4\\Client\\", "InstallPath");
				}
			}
			return Path.Combine(text, "WPF");
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x0015E41C File Offset: 0x0015D81C
		private static string ReadLocalMachineString(string key, string valueName)
		{
			string text = "HKEY_LOCAL_MACHINE\\" + key;
			new RegistryPermission(RegistryPermissionAccess.Read, text).Assert();
			return Registry.GetValue(text, valueName, null) as string;
		}

		// Token: 0x04002652 RID: 9810
		private const string COMPLUS_Version = "COMPLUS_Version";

		// Token: 0x04002653 RID: 9811
		private const string COMPLUS_InstallRoot = "COMPLUS_InstallRoot";

		// Token: 0x04002654 RID: 9812
		private const string EnvironmentVariables = "COMPLUS_Version;COMPLUS_InstallRoot";

		// Token: 0x04002655 RID: 9813
		private const string FRAMEWORK_RegKey = "Software\\Microsoft\\Net Framework Setup\\NDP\\v4\\Client\\";

		// Token: 0x04002656 RID: 9814
		private const string FRAMEWORK_RegKey_FullPath = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Net Framework Setup\\NDP\\v4\\Client\\";

		// Token: 0x04002657 RID: 9815
		private const string FRAMEWORK_InstallPath_RegValue = "InstallPath";

		// Token: 0x04002658 RID: 9816
		private const string DOTNET_RegKey = "Software\\Microsoft\\.NETFramework";

		// Token: 0x04002659 RID: 9817
		private const string DOTNET_Install_RegValue = "InstallRoot";

		// Token: 0x0400265A RID: 9818
		private const string WPF_SUBDIR = "WPF";
	}
}
