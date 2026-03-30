using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Win32
{
	// Token: 0x02000646 RID: 1606
	internal static class WinInet
	{
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600482E RID: 18478 RVA: 0x0011AA18 File Offset: 0x00119E18
		internal static Uri InternetCacheFolder
		{
			[SecurityCritical]
			get
			{
				NativeMethods.InternetCacheConfigInfo internetCacheConfigInfo = new NativeMethods.InternetCacheConfigInfo
				{
					CachePath = new string(new char[260])
				};
				uint dwStructSize = (uint)Marshal.SizeOf(internetCacheConfigInfo);
				internetCacheConfigInfo.dwStructSize = dwStructSize;
				if (!UnsafeNativeMethods.GetUrlCacheConfigInfo(ref internetCacheConfigInfo, ref dwStructSize, 260U))
				{
					int hrforLastWin32Error = Marshal.GetHRForLastWin32Error();
					if (hrforLastWin32Error != 0)
					{
						Marshal.ThrowExceptionForHR(hrforLastWin32Error);
					}
				}
				return new Uri(internetCacheConfigInfo.CachePath);
			}
		}
	}
}
