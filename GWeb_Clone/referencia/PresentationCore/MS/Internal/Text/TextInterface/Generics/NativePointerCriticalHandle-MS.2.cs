using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Generics
{
	// Token: 0x02000032 RID: 50
	internal abstract class NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontCollection> : CriticalHandle
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
		[SecurityCritical]
		public unsafe NativePointerCriticalHandle<MS::Internal::Text::TextInterface::Native::IDWriteFontCollection>(void* pNativePointer) : base(IntPtr.Zero)
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000C364 File Offset: 0x0000B764
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C388 File Offset: 0x0000B788
		public unsafe IDWriteFontCollection* Value
		{
			[SecurityCritical]
			get
			{
				return (IDWriteFontCollection*)this.handle.ToPointer();
			}
		}
	}
}
