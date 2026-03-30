using System;
using System.Security;
using Microsoft.Win32.SafeHandles;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x0200043A RID: 1082
	internal class SafeMILHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002C3A RID: 11322 RVA: 0x000B0B30 File Offset: 0x000AFF30
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal SafeMILHandle() : base(true)
		{
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000B0B44 File Offset: 0x000AFF44
		[SecurityCritical]
		internal SafeMILHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000B0B60 File Offset: 0x000AFF60
		[SecurityCritical]
		internal void UpdateEstimatedSize(long estimatedSize)
		{
			if (this._gcPressure != null)
			{
				this._gcPressure.Release();
			}
			if (estimatedSize > 0L)
			{
				this._gcPressure = new SafeMILHandleMemoryPressure(estimatedSize);
				this._gcPressure.AddRef();
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000B0B9C File Offset: 0x000AFF9C
		internal void CopyMemoryPressure(SafeMILHandle original)
		{
			this._gcPressure = original._gcPressure;
			if (this._gcPressure != null)
			{
				this._gcPressure.AddRef();
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000B0BC8 File Offset: 0x000AFFC8
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			UnsafeNativeMethods.MILUnknown.ReleaseInterface(ref this.handle);
			if (this._gcPressure != null)
			{
				this._gcPressure.Release();
				this._gcPressure = null;
			}
			return true;
		}

		// Token: 0x04001426 RID: 5158
		private SafeMILHandleMemoryPressure _gcPressure;
	}
}
