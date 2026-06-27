using System;

namespace MS.Internal
{
	// Token: 0x0200069F RID: 1695
	internal enum SynchronizedInputStates
	{
		// Token: 0x04001F62 RID: 8034
		NoOpportunity = 1,
		// Token: 0x04001F63 RID: 8035
		HadOpportunity,
		// Token: 0x04001F64 RID: 8036
		Handled = 4,
		// Token: 0x04001F65 RID: 8037
		Discarded = 8
	}
}
