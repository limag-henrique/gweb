using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000038 RID: 56
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontFile> : CriticalHandle
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontFile>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteFontFile* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteFontFile*)this.handle.ToPointer();
			}
		}
	}
}
