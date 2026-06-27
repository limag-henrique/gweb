using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000033 RID: 51
	internal class NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontCollection> : NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontCollection>
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000C2FC File Offset: 0x0000B6FC
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

		// Token: 0x0600033C RID: 828 RVA: 0x0000FDE8 File Offset: 0x0000F1E8
		[SecurityCritical]
		public unsafe NativeIUnknownWrapper<MS::Internal::Text::TextInterface::Native::IDWriteFontCollection>(IUnknown* pNativePointer) : base((void*)pNativePointer)
		{
		}
	}
}
