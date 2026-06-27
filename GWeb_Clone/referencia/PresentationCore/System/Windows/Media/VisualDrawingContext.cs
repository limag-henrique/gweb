using System;

namespace System.Windows.Media
{
	// Token: 0x0200038A RID: 906
	internal class VisualDrawingContext : RenderDataDrawingContext
	{
		// Token: 0x06002191 RID: 8593 RVA: 0x00087C14 File Offset: 0x00087014
		internal VisualDrawingContext(Visual ownerVisual)
		{
			this._ownerVisual = ownerVisual;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00087C30 File Offset: 0x00087030
		protected override void CloseCore(RenderData renderData)
		{
			this._ownerVisual.RenderClose(renderData);
		}

		// Token: 0x040010BD RID: 4285
		private Visual _ownerVisual;
	}
}
