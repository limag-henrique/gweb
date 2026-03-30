using System;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x02000414 RID: 1044
	internal interface ICompositionTarget : IDisposable
	{
		// Token: 0x06002A1E RID: 10782
		void Render(bool inResize, DUCE.Channel channel);

		// Token: 0x06002A1F RID: 10783
		void AddRefOnChannel(DUCE.Channel channel, DUCE.Channel outOfBandChannel);

		// Token: 0x06002A20 RID: 10784
		void ReleaseOnChannel(DUCE.Channel channel, DUCE.Channel outOfBandChannel);
	}
}
