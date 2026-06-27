using System;
using System.Security;
using MS.Win32;

namespace System.Windows.Input
{
	// Token: 0x0200025A RID: 602
	internal static class InputProcessorProfilesLoader
	{
		// Token: 0x060010FE RID: 4350 RVA: 0x000402C8 File Offset: 0x0003F6C8
		[SecurityCritical]
		internal static UnsafeNativeMethods.ITfInputProcessorProfiles Load()
		{
			UnsafeNativeMethods.ITfInputProcessorProfiles result;
			if (UnsafeNativeMethods.TF_CreateInputProcessorProfiles(out result) == 0)
			{
				return result;
			}
			return null;
		}
	}
}
