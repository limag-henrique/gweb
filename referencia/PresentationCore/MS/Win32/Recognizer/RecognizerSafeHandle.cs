using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;

namespace MS.Win32.Recognizer
{
	// Token: 0x02000650 RID: 1616
	[SecurityCritical]
	internal class RecognizerSafeHandle : SafeHandle
	{
		// Token: 0x06004884 RID: 18564 RVA: 0x0011ADE8 File Offset: 0x0011A1E8
		[SecurityCritical]
		private RecognizerSafeHandle() : this(true)
		{
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x0011ADFC File Offset: 0x0011A1FC
		[SecurityCritical]
		private RecognizerSafeHandle(bool ownHandle) : base(IntPtr.Zero, ownHandle)
		{
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06004886 RID: 18566 RVA: 0x0011AE18 File Offset: 0x0011A218
		public override bool IsInvalid
		{
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return base.IsClosed || this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x0011AE40 File Offset: 0x0011A240
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return HRESULT.Succeeded(UnsafeNativeMethods.DestroyRecognizer(this.handle));
		}
	}
}
