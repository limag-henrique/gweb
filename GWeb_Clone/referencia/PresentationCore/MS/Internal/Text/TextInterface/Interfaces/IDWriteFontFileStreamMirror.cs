using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.Text.TextInterface.Interfaces
{
	// Token: 0x02000019 RID: 25
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("6d4865fe-0ab8-4d91-8f62-5dd6be34a3e0")]
	[ComImport]
	internal interface IDWriteFontFileStreamMirror
	{
		// Token: 0x060002D1 RID: 721
		[SecurityCritical]
		[PreserveSig]
		unsafe int ReadFileFragment([Out] void** fragmentStart, [MarshalAs(UnmanagedType.U8)] [In] ulong fileOffset, [MarshalAs(UnmanagedType.U8)] [In] ulong fragmentSize, [Out] void** fragmentContext);

		// Token: 0x060002D2 RID: 722
		[SecurityCritical]
		[PreserveSig]
		unsafe void ReleaseFileFragment([In] void* fragmentContext);

		// Token: 0x060002D3 RID: 723
		[SecurityCritical]
		[PreserveSig]
		unsafe int GetFileSize([Out] ulong* fileSize);

		// Token: 0x060002D4 RID: 724
		[SecurityCritical]
		[PreserveSig]
		unsafe int GetLastWriteTime([Out] ulong* lastWriteTime);
	}
}
