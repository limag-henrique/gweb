using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.Text.TextInterface.Interfaces
{
	// Token: 0x0200001A RID: 26
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("727cad4e-d6af-4c9e-8a08-d695b11caa49")]
	[ComImport]
	internal interface IDWriteFontFileLoaderMirror
	{
		// Token: 0x060002D5 RID: 725
		[SecurityCritical]
		[PreserveSig]
		unsafe int CreateStreamFromKey([In] void* fontFileReferenceKey, [MarshalAs(UnmanagedType.U4)] [In] uint fontFileReferenceKeySize, [Out] IntPtr* fontFileStream);
	}
}
