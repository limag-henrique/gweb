using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Win32.PresentationCore
{
	// Token: 0x02000648 RID: 1608
	internal static class SafeNativeMethods
	{
		// Token: 0x06004831 RID: 18481 RVA: 0x0011AB40 File Offset: 0x00119F40
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static int MilCompositionEngine_InitializePartitionManager(int nPriority)
		{
			return SafeNativeMethods.SafeNativeMethodsPrivate.MilCompositionEngine_InitializePartitionManager(nPriority);
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x0011AB54 File Offset: 0x00119F54
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static int MilCompositionEngine_DeinitializePartitionManager()
		{
			return SafeNativeMethods.SafeNativeMethodsPrivate.MilCompositionEngine_DeinitializePartitionManager();
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x0011AB68 File Offset: 0x00119F68
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static long GetNextPerfElementId()
		{
			return SafeNativeMethods.SafeNativeMethodsPrivate.GetNextPerfElementId();
		}

		// Token: 0x02000960 RID: 2400
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		private static class SafeNativeMethodsPrivate
		{
			// Token: 0x0600598E RID: 22926
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilCompositionEngine_InitializePartitionManager(int nPriority);

			// Token: 0x0600598F RID: 22927
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern int MilCompositionEngine_DeinitializePartitionManager();

			// Token: 0x06005990 RID: 22928
			[DllImport("wpfgfx_v0400.dll")]
			internal static extern long GetNextPerfElementId();
		}
	}
}
