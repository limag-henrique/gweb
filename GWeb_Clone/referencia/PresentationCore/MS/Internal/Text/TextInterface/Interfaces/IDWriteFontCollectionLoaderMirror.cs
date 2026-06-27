using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.Text.TextInterface.Interfaces
{
	// Token: 0x0200001C RID: 28
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("cca920e4-52f0-492b-bfa8-29c72ee0a468")]
	[ComImport]
	internal interface IDWriteFontCollectionLoaderMirror
	{
		// Token: 0x060002D8 RID: 728
		[SecurityCritical]
		[PreserveSig]
		unsafe int CreateEnumeratorFromKey(IntPtr factory, [In] void* collectionKey, [MarshalAs(UnmanagedType.U4)] [In] uint collectionKeySize, IntPtr* fontFileEnumerator);
	}
}
