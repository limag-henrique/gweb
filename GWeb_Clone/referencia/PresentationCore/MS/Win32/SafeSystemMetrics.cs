using System;
using System.Security;
using MS.Internal.Interop;
using MS.Internal.PresentationCore;

namespace MS.Win32
{
	// Token: 0x02000645 RID: 1605
	[FriendAccessAllowed]
	internal sealed class SafeSystemMetrics
	{
		// Token: 0x06004828 RID: 18472 RVA: 0x0011A99C File Offset: 0x00119D9C
		private SafeSystemMetrics()
		{
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x0011A9B0 File Offset: 0x00119DB0
		internal static int DoubleClickDeltaX
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(SM.CXDOUBLECLK);
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x0600482A RID: 18474 RVA: 0x0011A9C4 File Offset: 0x00119DC4
		internal static int DoubleClickDeltaY
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(SM.CYDOUBLECLK);
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x0011A9D8 File Offset: 0x00119DD8
		internal static int DragDeltaX
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(SM.CXDRAG);
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x0600482C RID: 18476 RVA: 0x0011A9EC File Offset: 0x00119DEC
		internal static int DragDeltaY
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(SM.CYDRAG);
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x0011AA00 File Offset: 0x00119E00
		internal static bool IsImmEnabled
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(SM.IMMENABLED) != 0;
			}
		}
	}
}
