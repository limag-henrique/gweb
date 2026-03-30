using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000041 RID: 65
	internal class NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution> : NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution>
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000C2FC File Offset: 0x0000B6FC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe override bool ReleaseHandle()
		{
			void* ptr = this.handle.ToPointer();
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
			this.handle = IntPtr.Zero;
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000FDFC File Offset: 0x0000F1FC
		[SecurityCritical]
		public unsafe NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution>(IUnknown* pNativePointer) : base((void*)pNativePointer)
		{
		}
	}
}
