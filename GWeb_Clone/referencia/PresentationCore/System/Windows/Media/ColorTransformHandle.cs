using System;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000374 RID: 884
	internal class ColorTransformHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001F98 RID: 8088 RVA: 0x00081738 File Offset: 0x00080B38
		[SecurityCritical]
		internal ColorTransformHandle() : base(true)
		{
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0008174C File Offset: 0x00080B4C
		[SecurityCritical]
		internal ColorTransformHandle(IntPtr profile) : base(true)
		{
			base.SetHandle(profile);
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00081768 File Offset: 0x00080B68
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.Mscms.DeleteColorTransform(this.handle);
		}
	}
}
