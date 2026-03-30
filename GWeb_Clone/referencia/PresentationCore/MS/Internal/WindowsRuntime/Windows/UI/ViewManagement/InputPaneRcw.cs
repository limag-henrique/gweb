using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.WindowsRuntime.Windows.UI.ViewManagement
{
	// Token: 0x020006A6 RID: 1702
	internal static class InputPaneRcw
	{
		// Token: 0x020009B3 RID: 2483
		internal enum TrustLevel
		{
			// Token: 0x04002DA8 RID: 11688
			BaseTrust,
			// Token: 0x04002DA9 RID: 11689
			PartialTrust,
			// Token: 0x04002DAA RID: 11690
			FullTrust
		}

		// Token: 0x020009B4 RID: 2484
		[Guid("75CF2C57-9195-4931-8332-F0B409E916AF")]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IInputPaneInterop
		{
			// Token: 0x06005A9D RID: 23197
			[MethodImpl(MethodImplOptions.InternalCall)]
			void GetIids(out uint iidCount, [MarshalAs(UnmanagedType.LPStruct)] out Guid iids);

			// Token: 0x06005A9E RID: 23198
			[MethodImpl(MethodImplOptions.InternalCall)]
			void GetRuntimeClassName([MarshalAs(UnmanagedType.BStr)] out string className);

			// Token: 0x06005A9F RID: 23199
			[MethodImpl(MethodImplOptions.InternalCall)]
			void GetTrustLevel(out InputPaneRcw.TrustLevel TrustLevel);

			// Token: 0x06005AA0 RID: 23200
			[MethodImpl(MethodImplOptions.InternalCall)]
			InputPaneRcw.IInputPane2 GetForWindow([In] IntPtr appWindow, [In] ref Guid riid);
		}

		// Token: 0x020009B5 RID: 2485
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		[Guid("8A6B3F26-7090-4793-944C-C3F2CDE26276")]
		[ComImport]
		internal interface IInputPane2
		{
			// Token: 0x06005AA1 RID: 23201
			[MethodImpl(MethodImplOptions.InternalCall)]
			void GetIids(out uint iidCount, [MarshalAs(UnmanagedType.LPStruct)] out Guid iids);

			// Token: 0x06005AA2 RID: 23202
			[MethodImpl(MethodImplOptions.InternalCall)]
			void GetRuntimeClassName([MarshalAs(UnmanagedType.BStr)] out string className);

			// Token: 0x06005AA3 RID: 23203
			[MethodImpl(MethodImplOptions.InternalCall)]
			void GetTrustLevel(out InputPaneRcw.TrustLevel TrustLevel);

			// Token: 0x06005AA4 RID: 23204
			[MethodImpl(MethodImplOptions.InternalCall)]
			bool TryShow();

			// Token: 0x06005AA5 RID: 23205
			[MethodImpl(MethodImplOptions.InternalCall)]
			bool TryHide();
		}
	}
}
