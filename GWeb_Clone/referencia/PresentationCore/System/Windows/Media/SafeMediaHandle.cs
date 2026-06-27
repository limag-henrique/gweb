using System;
using System.Security;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000439 RID: 1081
	internal class SafeMediaHandle : SafeMILHandle
	{
		// Token: 0x06002C37 RID: 11319 RVA: 0x000B0AD4 File Offset: 0x000AFED4
		internal SafeMediaHandle()
		{
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000B0AE8 File Offset: 0x000AFEE8
		[SecurityCritical]
		internal SafeMediaHandle(IntPtr handle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000B0B04 File Offset: 0x000AFF04
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			HRESULT.Check(MILMedia.Shutdown(this.handle));
			UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref this.handle);
			return true;
		}
	}
}
