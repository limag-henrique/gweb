using System;

namespace System.Windows.Media
{
	// Token: 0x02000386 RID: 902
	internal class DrawingGroupDrawingContext : DrawingDrawingContext
	{
		// Token: 0x0600216D RID: 8557 RVA: 0x000874F8 File Offset: 0x000868F8
		internal DrawingGroupDrawingContext(DrawingGroup drawingGroup)
		{
			this._drawingGroup = drawingGroup;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x00087514 File Offset: 0x00086914
		protected override void CloseCore(DrawingCollection rootDrawingGroupChildren)
		{
			this._drawingGroup.Close(rootDrawingGroupChildren);
		}

		// Token: 0x040010B9 RID: 4281
		private DrawingGroup _drawingGroup;
	}
}
