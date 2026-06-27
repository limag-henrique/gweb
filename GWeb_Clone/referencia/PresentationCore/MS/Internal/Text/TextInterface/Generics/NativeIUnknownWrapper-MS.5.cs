using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000039 RID: 57
	internal class NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFile> : NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontFile>
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000C2FC File Offset: 0x0000B6FC
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

		// Token: 0x0600034B RID: 843 RVA: 0x0000FD70 File Offset: 0x0000F170
		[SecurityCritical]
		public unsafe NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontFile>(IUnknown* pNativePointer) : base((void*)pNativePointer)
		{
		}
	}
}
