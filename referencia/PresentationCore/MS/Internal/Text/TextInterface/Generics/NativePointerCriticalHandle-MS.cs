using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000030 RID: 48
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFont> : CriticalHandle
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFont>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000C364 File Offset: 0x0000B764
		public override bool IsInvalid
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteFont* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteFont*)this.handle.ToPointer();
			}
		}
	}
}
