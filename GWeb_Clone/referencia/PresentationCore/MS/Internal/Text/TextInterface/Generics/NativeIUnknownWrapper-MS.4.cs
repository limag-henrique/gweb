using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000037 RID: 55
	internal class NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontList> : NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontList>
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000C2FC File Offset: 0x0000B6FC
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

		// Token: 0x06000346 RID: 838 RVA: 0x0000FDD4 File Offset: 0x0000F1D4
		[SecurityCritical]
		public unsafe NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontList>(IUnknown* pNativePointer) : base((void*)pNativePointer)
		{
		}
	}
}
