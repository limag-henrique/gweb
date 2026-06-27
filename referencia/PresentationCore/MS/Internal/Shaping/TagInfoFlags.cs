using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C9 RID: 1737
	[Flags]
	internal enum TagInfoFlags : uint
	{
		// Token: 0x04002083 RID: 8323
		Substitution = 1U,
		// Token: 0x04002084 RID: 8324
		Positioning = 2U,
		// Token: 0x04002085 RID: 8325
		Both = 3U,
		// Token: 0x04002086 RID: 8326
		None = 0U
	}
}
