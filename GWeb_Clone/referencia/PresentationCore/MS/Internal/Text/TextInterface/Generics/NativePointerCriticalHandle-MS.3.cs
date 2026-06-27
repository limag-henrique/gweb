using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000034 RID: 52
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontFace> : CriticalHandle
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontFace>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteFontFace* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteFontFace*)this.handle.ToPointer();
			}
		}
	}
}
