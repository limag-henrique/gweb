using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal;

namespace MS.Win32.Recognizer
{
	// Token: 0x02000651 RID: 1617
	[SecurityCritical]
	internal class ContextSafeHandle : SafeHandle
	{
		// Token: 0x06004888 RID: 18568 RVA: 0x0011AE60 File Offset: 0x0011A260
		[SecurityCritical]
		private ContextSafeHandle() : this(true)
		{
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x0011AE74 File Offset: 0x0011A274
		[SecurityCritical]
		private ContextSafeHandle(bool ownHandle) : base(IntPtr.Zero, ownHandle)
		{
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x0011AE90 File Offset: 0x0011A290
		public override bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SecurityCritical]
			get
			{
				return base.IsClosed || this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x0011AEB8 File Offset: 0x0011A2B8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			int hr = UnsafeNativeMethods.DestroyContext(this.handle);
			this._recognizerHandle = null;
			return HRESULT.Succeeded(hr);
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x0011AEE0 File Offset: 0x0011A2E0
		[SecurityCritical]
		internal void AddReferenceOnRecognizer(RecognizerSafeHandle handle)
		{
			this._recognizerHandle = handle;
		}

		// Token: 0x04001BD7 RID: 7127
		[SecurityCritical]
		private RecognizerSafeHandle _recognizerHandle;
	}
}
