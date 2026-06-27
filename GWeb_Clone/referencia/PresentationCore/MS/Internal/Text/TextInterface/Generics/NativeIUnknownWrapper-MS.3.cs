using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000035 RID: 53
	internal class NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFace> : NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontFace>
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000C2FC File Offset: 0x0000B6FC
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe override bool ReleaseHandle()
		{
			void* ptr = this.handle.ToPointer();
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr, *(*(long*)ptr + 16L));
			this.handle = IntPtr.Zero;
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000FD98 File Offset: 0x0000F198
		[SecurityCritical]
		public unsafe NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFace>(IUnknown* pNativePointer) : base((void*)pNativePointer)
		{
		}
	}
}
