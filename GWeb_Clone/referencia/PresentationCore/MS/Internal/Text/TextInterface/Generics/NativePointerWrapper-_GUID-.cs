using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x0200002F RID: 47
	internal class NativePointerWrapper<_GUID> : NativePointerCriticalHandle<_GUID>
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000C334 File Offset: 0x0000B734
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			<Module>.delete(this.handle.ToPointer());
			this.handle = IntPtr.Zero;
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000FD84 File Offset: 0x0000F184
		[SecurityCritical]
		public unsafe NativePointerWrapper<_GUID>(_GUID* pNativePointer) : base((void*)pNativePointer)
		{
		}
	}
}
