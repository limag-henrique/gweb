using System;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x02000416 RID: 1046
	internal interface IDrawingContent : DUCE.IResource
	{
		// Token: 0x06002A23 RID: 10787
		Rect GetContentBounds(BoundsDrawingContextWalker ctx);

		// Token: 0x06002A24 RID: 10788
		void WalkContent(DrawingContextWalker walker);

		// Token: 0x06002A25 RID: 10789
		bool HitTestPoint(Point point);

		// Token: 0x06002A26 RID: 10790
		IntersectionDetail HitTestGeometry(PathGeometry geometry);

		// Token: 0x06002A27 RID: 10791
		void PropagateChangedHandler(EventHandler handler, bool adding);
	}
}
