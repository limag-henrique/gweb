using System;
using System.Runtime.InteropServices;
using System.Security;
using MS.Internal.Text.TextInterface.Native;

namespace MS.Internal.Text.TextInterface.Interfaces
{
	// Token: 0x0200001B RID: 27
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("72755049-5ff7-435d-8348-4be97cfa6c7c")]
	[ComImport]
	internal interface IDWriteFontFileEnumeratorMirror
	{
		// Token: 0x060002D6 RID: 726
		[PreserveSig]
		int MoveNext([MarshalAs(UnmanagedType.Bool)] out bool hasCurrentFile);

		// Token: 0x060002D7 RID: 727
		[SecurityCritical]
		[PreserveSig]
		unsafe int GetCurrentFontFile(IDWriteFontFile** fontFile);
	}
}
