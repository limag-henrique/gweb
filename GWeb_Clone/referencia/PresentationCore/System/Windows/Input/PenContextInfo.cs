using System;
using System.Security;
using MS.Internal;
using MS.Win32.Penimc;

namespace System.Windows.Input
{
	// Token: 0x020002D1 RID: 721
	internal struct PenContextInfo
	{
		// Token: 0x04000BD5 RID: 3029
		[SecurityCritical]
		public SecurityCriticalDataClass<IPimcContext2> PimcContext;

		// Token: 0x04000BD6 RID: 3030
		[SecurityCritical]
		public SecurityCriticalDataClass<IntPtr> CommHandle;

		// Token: 0x04000BD7 RID: 3031
		public int ContextId;

		// Token: 0x04000BD8 RID: 3032
		public uint WispContextKey;
	}
}
