using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x0200003F RID: 63
	internal class NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> : NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000C334 File Offset: 0x0000B734
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			<Module>.delete(this.handle.ToPointer());
			this.handle = IntPtr.Zero;
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000FE10 File Offset: 0x0000F210
		[SecurityCritical]
		public unsafe NativePointerWrapper<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>(DWRITE_SCRIPT_ANALYSIS* pNativePointer) : base((void*)pNativePointer)
		{
		}
	}
}
