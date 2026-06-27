using System;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x02000434 RID: 1076
	internal sealed class RenderContext
	{
		// Token: 0x06002C1B RID: 11291 RVA: 0x000B02A8 File Offset: 0x000AF6A8
		internal RenderContext()
		{
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000B02BC File Offset: 0x000AF6BC
		internal DUCE.Channel Channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000B02D0 File Offset: 0x000AF6D0
		internal DUCE.ResourceHandle Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000B02E4 File Offset: 0x000AF6E4
		internal void Initialize(DUCE.Channel channel, DUCE.ResourceHandle root)
		{
			this._channel = channel;
			this._root = root;
		}

		// Token: 0x0400141D RID: 5149
		private DUCE.Channel _channel;

		// Token: 0x0400141E RID: 5150
		private DUCE.ResourceHandle _root;
	}
}
