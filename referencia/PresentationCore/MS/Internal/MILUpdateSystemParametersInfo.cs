using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal
{
	// Token: 0x0200066B RID: 1643
	internal static class MILUpdateSystemParametersInfo
	{
		// Token: 0x060048AF RID: 18607
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MILUpdateSystemParametersInfo")]
		internal static extern int Update();
	}
}
