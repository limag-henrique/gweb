using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Win32
{
	// Token: 0x02000647 RID: 1607
	internal static class LoadLibraryHelper
	{
		// Token: 0x0600482F RID: 18479 RVA: 0x0011AA84 File Offset: 0x00119E84
		[SecuritySafeCritical]
		private static bool IsKnowledgeBase2533623OrGreater()
		{
			bool result = false;
			IntPtr zero = IntPtr.Zero;
			if (UnsafeNativeMethods.GetModuleHandleEx(UnsafeNativeMethods.GetModuleHandleFlags.None, "kernel32.dll", out zero) && zero != IntPtr.Zero)
			{
				try
				{
					result = (UnsafeNativeMethods.GetProcAddressNoThrow(new HandleRef(null, zero), "AddDllDirectoryName") != IntPtr.Zero);
				}
				finally
				{
					UnsafeNativeMethods.FreeLibrary(zero);
				}
			}
			return result;
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x0011AAF8 File Offset: 0x00119EF8
		[SecurityCritical]
		internal static IntPtr SecureLoadLibraryEx(string lpFileName, IntPtr hFile, UnsafeNativeMethods.LoadLibraryFlags dwFlags)
		{
			if (!LoadLibraryHelper.IsKnowledgeBase2533623OrGreater() && (dwFlags & UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_APPLICATION_DIR & UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS & UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR & UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_SYSTEM32 & UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_USER_DIRS) != UnsafeNativeMethods.LoadLibraryFlags.None)
			{
				dwFlags &= ~(UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_APPLICATION_DIR | UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS | UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_SYSTEM32 | UnsafeNativeMethods.LoadLibraryFlags.LOAD_LIBRARY_SEARCH_USER_DIRS);
			}
			return UnsafeNativeMethods.LoadLibraryEx(lpFileName, hFile, dwFlags);
		}
	}
}
