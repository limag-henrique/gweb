using System;

namespace System.Windows.Media
{
	// Token: 0x02000417 RID: 1047
	internal interface IFreezeFreezables
	{
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002A28 RID: 10792
		bool FreezeFreezables { get; }

		// Token: 0x06002A29 RID: 10793
		bool TryFreeze(string value, Freezable freezable);

		// Token: 0x06002A2A RID: 10794
		Freezable TryGetFreezable(string value);
	}
}
