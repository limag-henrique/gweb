using System;
using System.Runtime.InteropServices;
using System.Security;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000028 RID: 40
	// (Invoke) Token: 0x06000301 RID: 769
	[SecurityCritical]
	internal unsafe delegate int CreateTextAnalysisSource(ushort* text, uint length, ushort* culture, void* factory, [MarshalAs(UnmanagedType.U1)] bool isRightToLeft, ushort* numberCulture, [MarshalAs(UnmanagedType.U1)] bool ignoreUserOverride, uint numberSubstitutionMethod, void** ppTextAnalysisSource);
}
