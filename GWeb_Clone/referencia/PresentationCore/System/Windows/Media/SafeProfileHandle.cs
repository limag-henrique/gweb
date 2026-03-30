using System;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000370 RID: 880
	internal class SafeProfileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001F84 RID: 8068 RVA: 0x0008107C File Offset: 0x0008047C
		[SecurityCritical]
		internal SafeProfileHandle() : base(true)
		{
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00081090 File Offset: 0x00080490
		[SecurityCritical]
		internal SafeProfileHandle(IntPtr profile) : base(true)
		{
			base.SetHandle(profile);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000810AC File Offset: 0x000804AC
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.Mscms.CloseColorProfile(this.handle);
		}
	}
}
