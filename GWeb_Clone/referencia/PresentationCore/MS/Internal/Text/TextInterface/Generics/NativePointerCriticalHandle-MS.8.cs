using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x0200003E RID: 62
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS> : CriticalHandle
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::DWRITE_SCRIPT_ANALYSIS>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe DWRITE_SCRIPT_ANALYSIS* Value
		{
			[SecurityCritical]
			get
			{
				return (DWRITE_SCRIPT_ANALYSIS*)this.handle.ToPointer();
			}
		}
	}
}
