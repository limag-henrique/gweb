using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32;
using MS.Internal.WindowsBase;
using MS.Win32;

namespace MS.Internal.PresentationCore
{
	// Token: 0x020007E5 RID: 2021
	internal static class SafeSecurityHelper
	{
		// Token: 0x060054CC RID: 21708 RVA: 0x0015DD24 File Offset: 0x0015D124
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static void TransformLocalRectToScreen(HandleRef hwnd, ref NativeMethods.RECT rcWindowCoords)
		{
			int num = NativeMethodsSetLastError.MapWindowPoints(hwnd, new HandleRef(null, IntPtr.Zero), ref rcWindowCoords, 2);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (num == 0 && lastWin32Error != 0)
			{
				throw new Win32Exception(lastWin32Error);
			}
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0015DD58 File Offset: 0x0015D158
		internal static string GetAssemblyPartialName(Assembly assembly)
		{
			AssemblyName assemblyName = new AssemblyName(assembly.FullName);
			string name = assemblyName.Name;
			if (name == null)
			{
				return string.Empty;
			}
			return name;
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0015DD84 File Offset: 0x0015D184
		internal static Assembly GetLoadedAssembly(AssemblyName assemblyName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Version version = assemblyName.Version;
			CultureInfo cultureInfo = assemblyName.CultureInfo;
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			for (int i = assemblies.Length - 1; i >= 0; i--)
			{
				AssemblyName assemblyName2 = SafeSecurityHelper.GetAssemblyName(assemblies[i]);
				Version version2 = assemblyName2.Version;
				CultureInfo cultureInfo2 = assemblyName2.CultureInfo;
				byte[] publicKeyToken2 = assemblyName2.GetPublicKeyToken();
				if (string.Compare(assemblyName2.Name, assemblyName.Name, true, TypeConverterHelper.InvariantEnglishUS) == 0 && (version == null || version.Equals(version2)) && (cultureInfo == null || cultureInfo.Equals(cultureInfo2)) && (publicKeyToken == null || SafeSecurityHelper.IsSameKeyToken(publicKeyToken, publicKeyToken2)))
				{
					return assemblies[i];
				}
			}
			return null;
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0015DE34 File Offset: 0x0015D234
		private static AssemblyName GetAssemblyName(Assembly assembly)
		{
			object key = assembly.IsDynamic ? new WeakRefKey(assembly) : assembly;
			object obj = SafeSecurityHelper.syncObject;
			AssemblyName result;
			lock (obj)
			{
				AssemblyName assemblyName;
				if (SafeSecurityHelper._assemblies == null)
				{
					SafeSecurityHelper._assemblies = new Dictionary<object, AssemblyName>();
				}
				else if (SafeSecurityHelper._assemblies.TryGetValue(key, out assemblyName))
				{
					return assemblyName;
				}
				assemblyName = new AssemblyName(assembly.FullName);
				SafeSecurityHelper._assemblies.Add(key, assemblyName);
				if (assembly.IsDynamic && !SafeSecurityHelper._isGCCallbackPending)
				{
					GCNotificationToken.RegisterCallback(SafeSecurityHelper._cleanupCollectedAssemblies, null);
					SafeSecurityHelper._isGCCallbackPending = true;
				}
				result = assemblyName;
			}
			return result;
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x0015DEF0 File Offset: 0x0015D2F0
		private static void CleanupCollectedAssemblies(object state)
		{
			bool flag = false;
			List<object> list = null;
			object obj = SafeSecurityHelper.syncObject;
			lock (obj)
			{
				foreach (object obj2 in SafeSecurityHelper._assemblies.Keys)
				{
					WeakReference weakReference = obj2 as WeakReference;
					if (weakReference != null)
					{
						if (weakReference.IsAlive)
						{
							flag = true;
						}
						else
						{
							if (list == null)
							{
								list = new List<object>();
							}
							list.Add(obj2);
						}
					}
				}
				if (list != null)
				{
					foreach (object key in list)
					{
						SafeSecurityHelper._assemblies.Remove(key);
					}
				}
				if (flag)
				{
					GCNotificationToken.RegisterCallback(SafeSecurityHelper._cleanupCollectedAssemblies, null);
				}
				else
				{
					SafeSecurityHelper._isGCCallbackPending = false;
				}
			}
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x0015E01C File Offset: 0x0015D41C
		internal static bool IsSameKeyToken(byte[] reqKeyToken, byte[] curKeyToken)
		{
			bool result = false;
			if (reqKeyToken == null && curKeyToken == null)
			{
				result = true;
			}
			else if (reqKeyToken != null && curKeyToken != null && reqKeyToken.Length == curKeyToken.Length)
			{
				result = true;
				for (int i = 0; i < reqKeyToken.Length; i++)
				{
					if (reqKeyToken[i] != curKeyToken[i])
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x0015E060 File Offset: 0x0015D460
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static bool IsFeatureDisabled(SafeSecurityHelper.KeyToRead key)
		{
			bool flag = false;
			switch (key)
			{
			case SafeSecurityHelper.KeyToRead.WebBrowserDisable:
			{
				string name = "WebBrowserDisallow";
				goto IL_76;
			}
			case SafeSecurityHelper.KeyToRead.MediaAudioDisable:
			{
				string name = "MediaAudioDisallow";
				goto IL_76;
			}
			case (SafeSecurityHelper.KeyToRead)3:
			case (SafeSecurityHelper.KeyToRead)5:
			case (SafeSecurityHelper.KeyToRead)7:
				break;
			case SafeSecurityHelper.KeyToRead.MediaVideoDisable:
			{
				string name = "MediaVideoDisallow";
				goto IL_76;
			}
			case SafeSecurityHelper.KeyToRead.MediaAudioOrVideoDisable:
			{
				string name = "MediaAudioDisallow";
				goto IL_76;
			}
			case SafeSecurityHelper.KeyToRead.MediaImageDisable:
			{
				string name = "MediaImageDisallow";
				goto IL_76;
			}
			default:
				if (key == SafeSecurityHelper.KeyToRead.ScriptInteropDisable)
				{
					string name = "ScriptInteropDisallow";
					goto IL_76;
				}
				break;
			}
			throw new ArgumentException(key.ToString());
			IL_76:
			RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\.NETFramework\\Windows Presentation Foundation\\Features");
			registryPermission.Assert();
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\.NETFramework\\Windows Presentation Foundation\\Features");
				if (registryKey != null)
				{
					string name;
					object value = registryKey.GetValue(name);
					bool flag2 = value is int && (int)value == 1;
					if (flag2)
					{
						flag = true;
					}
					if (!flag && key == SafeSecurityHelper.KeyToRead.MediaAudioOrVideoDisable)
					{
						name = "MediaVideoDisallow";
						value = registryKey.GetValue(name);
						flag2 = (value is int && (int)value == 1);
						if (flag2)
						{
							flag = true;
						}
					}
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return flag;
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0015E188 File Offset: 0x0015D588
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static CultureInfo GetCultureInfoByIetfLanguageTag(string languageTag)
		{
			CultureInfo result = null;
			RegistryPermission registryPermission = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Nls\\IetfLanguage");
			registryPermission.Assert();
			try
			{
				result = CultureInfo.GetCultureInfoByIetfLanguageTag(languageTag);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0015E1D8 File Offset: 0x0015D5D8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static bool IsConnectedToPresentationSource(Visual visual)
		{
			return PresentationSource.CriticalFromVisual(visual) != null;
		}

		// Token: 0x0400264A RID: 9802
		private static Dictionary<object, AssemblyName> _assemblies;

		// Token: 0x0400264B RID: 9803
		private static object syncObject = new object();

		// Token: 0x0400264C RID: 9804
		private static bool _isGCCallbackPending;

		// Token: 0x0400264D RID: 9805
		private static readonly WaitCallback _cleanupCollectedAssemblies = new WaitCallback(SafeSecurityHelper.CleanupCollectedAssemblies);

		// Token: 0x0400264E RID: 9806
		internal const string IMAGE = "image";

		// Token: 0x02000A0E RID: 2574
		internal enum KeyToRead
		{
			// Token: 0x04002F87 RID: 12167
			WebBrowserDisable = 1,
			// Token: 0x04002F88 RID: 12168
			MediaAudioDisable,
			// Token: 0x04002F89 RID: 12169
			MediaVideoDisable = 4,
			// Token: 0x04002F8A RID: 12170
			MediaImageDisable = 8,
			// Token: 0x04002F8B RID: 12171
			MediaAudioOrVideoDisable = 6,
			// Token: 0x04002F8C RID: 12172
			ScriptInteropDisable = 16
		}
	}
}
