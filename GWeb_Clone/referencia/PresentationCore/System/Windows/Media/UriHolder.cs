using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x02000360 RID: 864
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct UriHolder
	{
		// Token: 0x04000FD7 RID: 4055
		internal Uri BaseUri;

		// Token: 0x04000FD8 RID: 4056
		internal Uri OriginalUri;
	}
}
