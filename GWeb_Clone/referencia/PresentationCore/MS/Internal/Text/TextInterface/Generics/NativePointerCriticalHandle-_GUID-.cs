using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x0200002E RID: 46
	internal abstract class NativePointerCriticalHandle<_GUID> : CriticalHandle
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<_GUID>(void* pNativePointer) : base(IntPtr.Zero)
		{
			try
			{
				IntPtr handle = new IntPtr(pNativePointer);
				base.SetHandle(handle);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000C364 File Offset: 0x0000B764
		public override bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SecuritySafeCritical]
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe _GUID* Value
		{
			[SecurityCritical]
			get
			{
				return (_GUID*)this.handle.ToPointer();
			}
		}
	}
}
