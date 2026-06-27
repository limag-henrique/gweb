using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000040 RID: 64
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution> : CriticalHandle
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteNumberSubstitution>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteNumberSubstitution* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteNumberSubstitution*)this.handle.ToPointer();
			}
		}
	}
}
