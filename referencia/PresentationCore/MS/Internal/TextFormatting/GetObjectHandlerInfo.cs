using System;
using System.Security;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000725 RID: 1829
	// (Invoke) Token: 0x06004E06 RID: 19974
	[SecurityCritical]
	internal unsafe delegate LsErr GetObjectHandlerInfo(IntPtr pols, uint objectId, void* objectInfo);
}
