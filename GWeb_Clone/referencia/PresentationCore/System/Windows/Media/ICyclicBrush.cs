using System;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x02000415 RID: 1045
	internal interface ICyclicBrush
	{
		// Token: 0x06002A21 RID: 10785
		void FireOnChanged();

		// Token: 0x06002A22 RID: 10786
		void RenderForCyclicBrush(DUCE.Channel channel, bool skipChannelCheck);
	}
}
