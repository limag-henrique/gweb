using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x0200003C RID: 60
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteTextAnalyzer> : CriticalHandle
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteTextAnalyzer>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteTextAnalyzer* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteTextAnalyzer*)this.handle.ToPointer();
			}
		}
	}
}
