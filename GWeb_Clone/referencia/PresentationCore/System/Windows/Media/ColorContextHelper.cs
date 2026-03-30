using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000371 RID: 881
	internal struct ColorContextHelper
	{
		// Token: 0x06001F87 RID: 8071 RVA: 0x000810C4 File Offset: 0x000804C4
		[SecurityCritical]
		internal void OpenColorProfile(ref UnsafeNativeMethods.PROFILE profile)
		{
			this._profileHandle = UnsafeNativeMethods.Mscms.OpenColorProfile(ref profile, 1U, 1U, 3U);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000810E0 File Offset: 0x000804E0
		[SecurityCritical]
		internal bool GetColorProfileHeader(out UnsafeNativeMethods.PROFILEHEADER header)
		{
			if (this.IsInvalid)
			{
				throw new InvalidOperationException(SR.Get("Image_ColorContextInvalid"));
			}
			return UnsafeNativeMethods.Mscms.GetColorProfileHeader(this._profileHandle, out header);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00081114 File Offset: 0x00080514
		[SecurityCritical]
		internal void GetColorProfileFromHandle(byte[] buffer, ref uint bufferSize)
		{
			Invariant.Assert(buffer == null || (ulong)bufferSize <= (ulong)((long)buffer.Length));
			if (this.IsInvalid)
			{
				throw new InvalidOperationException(SR.Get("Image_ColorContextInvalid"));
			}
			if (!UnsafeNativeMethods.Mscms.GetColorProfileFromHandle(this._profileHandle, buffer, ref bufferSize) && buffer != null)
			{
				HRESULT.Check(Marshal.GetHRForLastWin32Error());
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0008116C File Offset: 0x0008056C
		internal bool IsInvalid
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				return this._profileHandle == null || this._profileHandle.IsInvalid;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x00081190 File Offset: 0x00080590
		internal SafeProfileHandle ProfileHandle
		{
			[SecurityCritical]
			get
			{
				return this._profileHandle;
			}
		}

		// Token: 0x04001061 RID: 4193
		[SecurityCritical]
		private SafeProfileHandle _profileHandle;
	}
}
