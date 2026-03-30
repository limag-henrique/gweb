using System;

namespace System.Windows.Media
{
	// Token: 0x02000388 RID: 904
	internal static class DrawingServices
	{
		// Token: 0x06002183 RID: 8579 RVA: 0x000878E0 File Offset: 0x00086CE0
		internal static bool HitTestPoint(Drawing drawing, Point point)
		{
			if (drawing != null)
			{
				HitTestDrawingContextWalker hitTestDrawingContextWalker = new HitTestWithPointDrawingContextWalker(point);
				drawing.WalkCurrentValue(hitTestDrawingContextWalker);
				return hitTestDrawingContextWalker.IsHit;
			}
			return false;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x00087908 File Offset: 0x00086D08
		internal static IntersectionDetail HitTestGeometry(Drawing drawing, PathGeometry geometry)
		{
			if (drawing != null)
			{
				HitTestDrawingContextWalker hitTestDrawingContextWalker = new HitTestWithGeometryDrawingContextWalker(geometry);
				drawing.WalkCurrentValue(hitTestDrawingContextWalker);
				return hitTestDrawingContextWalker.IntersectionDetail;
			}
			return IntersectionDetail.Empty;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x00087930 File Offset: 0x00086D30
		internal static DrawingGroup DrawingGroupFromRenderData(RenderData renderData)
		{
			DrawingGroup drawingGroup = new DrawingGroup();
			DrawingContext drawingContext = drawingGroup.Open();
			DrawingDrawingContext drawingDrawingContext = drawingContext as DrawingDrawingContext;
			if (drawingDrawingContext != null)
			{
				drawingDrawingContext.CanBeInheritanceContext = false;
			}
			DrawingContextDrawingContextWalker ctx = new DrawingContextDrawingContextWalker(drawingContext);
			renderData.BaseValueDrawingContextWalk(ctx);
			drawingContext.Close();
			return drawingGroup;
		}
	}
}
