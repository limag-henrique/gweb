using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x0200003A RID: 58
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteLocalizedStrings> : CriticalHandle
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteLocalizedStrings>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteLocalizedStrings* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteLocalizedStrings*)this.handle.ToPointer();
			}
		}
	}
}
