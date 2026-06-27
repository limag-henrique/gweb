using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000182 RID: 386
	internal static class LocalAppContext
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00014618 File Offset: 0x00013A18
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0001462C File Offset: 0x00013A2C
		private static bool DisableCaching { get; set; }

		// Token: 0x0600037F RID: 895 RVA: 0x00014640 File Offset: 0x00013A40
		static LocalAppContext()
		{
			LocalAppContext.s_canForwardCalls = LocalAppContext.SetupDelegate();
			AppContextDefaultValues.PopulateDefaultValues();
			LocalAppContext.DisableCaching = LocalAppContext.IsSwitchEnabled("TestSwitch.LocalAppContext.DisableCaching");
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00014680 File Offset: 0x00013A80
		public static bool IsSwitchEnabled(string switchName)
		{
			bool result;
			if (LocalAppContext.s_canForwardCalls && LocalAppContext.TryGetSwitchFromCentralAppContext(switchName, out result))
			{
				return result;
			}
			return LocalAppContext.IsSwitchEnabledLocal(switchName);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000146AC File Offset: 0x00013AAC
		private static bool IsSwitchEnabledLocal(string switchName)
		{
			Dictionary<string, bool> obj = LocalAppContext.s_switchMap;
			bool flag3;
			bool flag2;
			lock (obj)
			{
				flag2 = LocalAppContext.s_switchMap.TryGetValue(switchName, out flag3);
			}
			return flag2 && flag3;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00014708 File Offset: 0x00013B08
		private static bool SetupDelegate()
		{
			Type type = typeof(object).Assembly.GetType("System.AppContext");
			if (type == null)
			{
				return false;
			}
			MethodInfo method = type.GetMethod("TryGetSwitch", BindingFlags.Static | BindingFlags.Public, null, new Type[]
			{
				typeof(string),
				typeof(bool).MakeByRefType()
			}, null);
			if (method == null)
			{
				return false;
			}
			LocalAppContext.TryGetSwitchFromCentralAppContext = (LocalAppContext.TryGetSwitchDelegate)Delegate.CreateDelegate(typeof(LocalAppContext.TryGetSwitchDelegate), method);
			return true;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00014798 File Offset: 0x00013B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || LocalAppContext.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000147BC File Offset: 0x00013BBC
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			if (LocalAppContext.DisableCaching)
			{
				return LocalAppContext.IsSwitchEnabled(switchName);
			}
			bool flag = LocalAppContext.IsSwitchEnabled(switchName);
			switchValue = (flag ? 1 : -1);
			return flag;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000147E8 File Offset: 0x00013BE8
		internal static void DefineSwitchDefault(string switchName, bool initialValue)
		{
			LocalAppContext.s_switchMap[switchName] = initialValue;
		}

		// Token: 0x04000498 RID: 1176
		private static LocalAppContext.TryGetSwitchDelegate TryGetSwitchFromCentralAppContext;

		// Token: 0x04000499 RID: 1177
		private static bool s_canForwardCalls;

		// Token: 0x0400049A RID: 1178
		private static Dictionary<string, bool> s_switchMap = new Dictionary<string, bool>();

		// Token: 0x0400049B RID: 1179
		private static readonly object s_syncLock = new object();

		// Token: 0x020007F0 RID: 2032
		// (Invoke) Token: 0x0600557C RID: 21884
		private delegate bool TryGetSwitchDelegate(string switchName, out bool value);
	}
}
