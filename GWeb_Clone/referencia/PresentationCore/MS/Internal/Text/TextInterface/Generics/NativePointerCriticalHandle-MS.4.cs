using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000036 RID: 54
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontList> : CriticalHandle
	{
		// Token: 0x06000342 RID: 834 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontList>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteFontList* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteFontList*)this.handle.ToPointer();
			}
		}
	}
}
