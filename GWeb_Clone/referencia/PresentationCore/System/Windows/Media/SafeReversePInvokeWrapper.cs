using System;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Internal;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x0200043C RID: 1084
	internal class SafeReversePInvokeWrapper : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002C42 RID: 11330 RVA: 0x000B0C74 File Offset: 0x000B0074
		[SecurityCritical]
		internal SafeReversePInvokeWrapper() : base(true)
		{
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000B0C88 File Offset: 0x000B0088
		[SecurityCritical]
		internal SafeReversePInvokeWrapper(IntPtr delegatePtr) : base(true)
		{
			IntPtr handle;
			HRESULT.Check(UnsafeNativeMethods.MilCoreApi.MilCreateReversePInvokeWrapper(delegatePtr, out handle));
			base.SetHandle(handle);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000B0CB0 File Offset: 0x000B00B0
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.MilCoreApi.MilReleasePInvokePtrBlocking(this.handle);
			}
			UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref this.handle);
			return true;
		}
	}
}
