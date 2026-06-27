using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Interop;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace MS.Win32.Penimc
{
	// Token: 0x0200064B RID: 1611
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06004846 RID: 18502 RVA: 0x0011AB90 File Offset: 0x00119F90
		[SecurityCritical]
		static UnsafeNativeMethods()
		{
			WpfLibraryLoader.EnsureLoaded("penimc2_v0400.dll");
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06004847 RID: 18503 RVA: 0x0011ABA8 File Offset: 0x00119FA8
		internal static IPimcManager2 PimcManager
		{
			[SecurityCritical]
			get
			{
				if (UnsafeNativeMethods._pimcManagerThreadStatic == null)
				{
					UnsafeNativeMethods._pimcManagerThreadStatic = UnsafeNativeMethods.CreatePimcManager();
				}
				return UnsafeNativeMethods._pimcManagerThreadStatic;
			}
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0011ABCC File Offset: 0x00119FCC
		[SecurityCritical]
		private static IPimcManager2 CreatePimcManager()
		{
			Guid guid = Guid.Parse("5C8422A6-F64B-4AA6-8D3C-78A3E988AA9E");
			Guid guid2 = Guid.Parse("215B68E5-0E78-4505-BE40-962EE3A0C379");
			object obj = UnsafeNativeMethods.CoCreateInstance(ref guid, null, 1, ref guid2);
			return (IPimcManager2)obj;
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0011AC04 File Offset: 0x0011A004
		[SecurityCritical]
		internal static void CheckedLockWispObjectFromGit(uint gitKey)
		{
			if (OSVersionHelper.IsOsWindows8OrGreater && !UnsafeNativeMethods.LockWispObjectFromGit(gitKey))
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x0011AC28 File Offset: 0x0011A028
		[SecurityCritical]
		internal static void CheckedUnlockWispObjectFromGit(uint gitKey)
		{
			if (OSVersionHelper.IsOsWindows8OrGreater && !UnsafeNativeMethods.UnlockWispObjectFromGit(gitKey))
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0011AC4C File Offset: 0x0011A04C
		[SecurityCritical]
		private static void ReleaseManagerExternalLockImpl(IPimcManager2 manager)
		{
			IPimcTablet2 pimcTablet = null;
			manager.GetTablet(4294958765U, out pimcTablet);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0011AC68 File Offset: 0x0011A068
		[SecurityCritical]
		internal static void ReleaseManagerExternalLock()
		{
			if (UnsafeNativeMethods._pimcManagerThreadStatic != null)
			{
				UnsafeNativeMethods.ReleaseManagerExternalLockImpl(UnsafeNativeMethods._pimcManagerThreadStatic);
			}
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x0011AC88 File Offset: 0x0011A088
		[SecurityCritical]
		internal static void SetWispManagerKey(IPimcTablet2 tablet)
		{
			uint num = UnsafeNativeMethods.QueryWispKeyFromTablet(-3, tablet);
			Invariant.Assert(UnsafeNativeMethods._wispManagerKey == null || UnsafeNativeMethods._wispManagerKey.Value == num);
			UnsafeNativeMethods._wispManagerKey = new uint?(num);
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0011ACCC File Offset: 0x0011A0CC
		[SecurityCritical]
		internal static void LockWispManager()
		{
			if (!UnsafeNativeMethods._wispManagerLocked && UnsafeNativeMethods._wispManagerKey != null)
			{
				UnsafeNativeMethods.CheckedLockWispObjectFromGit(UnsafeNativeMethods._wispManagerKey.Value);
				UnsafeNativeMethods._wispManagerLocked = true;
			}
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x0011AD04 File Offset: 0x0011A104
		[SecurityCritical]
		internal static void UnlockWispManager()
		{
			if (UnsafeNativeMethods._wispManagerLocked && UnsafeNativeMethods._wispManagerKey != null)
			{
				UnsafeNativeMethods.CheckedUnlockWispObjectFromGit(UnsafeNativeMethods._wispManagerKey.Value);
				UnsafeNativeMethods._wispManagerLocked = false;
			}
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x0011AD3C File Offset: 0x0011A13C
		[SecurityCritical]
		internal static void AcquireTabletExternalLock(IPimcTablet2 tablet)
		{
			int num = 0;
			tablet.GetCursorButtonCount(-4, out num);
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x0011AD58 File Offset: 0x0011A158
		[SecurityCritical]
		internal static void ReleaseTabletExternalLock(IPimcTablet2 tablet)
		{
			int num = 0;
			tablet.GetCursorButtonCount(-1, out num);
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x0011AD70 File Offset: 0x0011A170
		[SecurityCritical]
		private static uint QueryWispKeyFromTablet(int keyType, IPimcTablet2 tablet)
		{
			int num = 0;
			tablet.GetCursorButtonCount(keyType, out num);
			if (num == 0)
			{
				throw new InvalidOperationException();
			}
			return (uint)num;
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x0011AD94 File Offset: 0x0011A194
		[SecurityCritical]
		internal static uint QueryWispTabletKey(IPimcTablet2 tablet)
		{
			return UnsafeNativeMethods.QueryWispKeyFromTablet(-2, tablet);
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x0011ADAC File Offset: 0x0011A1AC
		[SecurityCritical]
		internal static uint QueryWispContextKey(IPimcContext2 context)
		{
			int num = 0;
			Guid empty = Guid.Empty;
			int num2 = 0;
			int num3 = 0;
			float num4 = 0f;
			context.GetPacketPropertyInfo(-1, out empty, out num, out num2, out num3, out num4);
			if (num == 0)
			{
				throw new InvalidOperationException();
			}
			return (uint)num;
		}

		// Token: 0x06004855 RID: 18517
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetPenEvent(IntPtr commHandle, IntPtr handleReset, out int evt, out int stylusPointerId, out int cPackets, out int cbPacket, out IntPtr pPackets);

		// Token: 0x06004856 RID: 18518
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetPenEventMultiple(int cCommHandles, IntPtr[] commHandles, IntPtr handleReset, out int iHandle, out int evt, out int stylusPointerId, out int cPackets, out int cbPacket, out IntPtr pPackets);

		// Token: 0x06004857 RID: 18519
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetLastSystemEventData(IntPtr commHandle, out int evt, out int modifier, out int key, out int x, out int y, out int cursorMode, out int buttonState);

		// Token: 0x06004858 RID: 18520
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CreateResetEvent(out IntPtr handle);

		// Token: 0x06004859 RID: 18521
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DestroyResetEvent(IntPtr handle);

		// Token: 0x0600485A RID: 18522
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RaiseResetEvent(IntPtr handle);

		// Token: 0x0600485B RID: 18523
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool LockWispObjectFromGit(uint gitKey);

		// Token: 0x0600485C RID: 18524
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("penimc2_v0400.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnlockWispObjectFromGit(uint gitKey);

		// Token: 0x0600485D RID: 18525
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		private static extern object CoCreateInstance([In] ref Guid clsid, [MarshalAs(UnmanagedType.Interface)] object punkOuter, int context, [In] ref Guid iid);

		// Token: 0x04001BCE RID: 7118
		private const uint ReleaseManagerExt = 4294958765U;

		// Token: 0x04001BCF RID: 7119
		private const int ReleaseTabletExt = -1;

		// Token: 0x04001BD0 RID: 7120
		private const int GetWispTabletKey = -2;

		// Token: 0x04001BD1 RID: 7121
		private const int GetWispManagerKey = -3;

		// Token: 0x04001BD2 RID: 7122
		private const int LockTabletExt = -4;

		// Token: 0x04001BD3 RID: 7123
		private const int GetWispContextKey = -1;

		// Token: 0x04001BD4 RID: 7124
		[ThreadStatic]
		[SecurityCritical]
		private static uint? _wispManagerKey;

		// Token: 0x04001BD5 RID: 7125
		[ThreadStatic]
		private static bool _wispManagerLocked;

		// Token: 0x04001BD6 RID: 7126
		[SecurityCritical]
		[ThreadStatic]
		private static IPimcManager2 _pimcManagerThreadStatic;
	}
}
