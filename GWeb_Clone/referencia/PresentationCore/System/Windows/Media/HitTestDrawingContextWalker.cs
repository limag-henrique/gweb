using System;

namespace System.Windows.Media
{
	// Token: 0x02000408 RID: 1032
	internal abstract class HitTestDrawingContextWalker : DrawingContextWalker
	{
		// Token: 0x060029CF RID: 10703 RVA: 0x000A82D8 File Offset: 0x000A76D8
		internal HitTestDrawingContextWalker()
		{
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060029D0 RID: 10704
		internal abstract bool IsHit { get; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060029D1 RID: 10705
		internal abstract IntersectionDetail IntersectionDetail { get; }

		// Token: 0x060029D2 RID: 10706 RVA: 0x000A82EC File Offset: 0x000A76EC
		public override void DrawLine(Pen pen, Point point0, Point point1)
		{
			this.DrawGeometry(null, pen, new LineGeometry(point0, point1));
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000A8308 File Offset: 0x000A7708
		public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
		{
			this.DrawGeometry(brush, pen, new RectangleGeometry(rectangle));
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000A8324 File Offset: 0x000A7724
		public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
		{
			this.DrawGeometry(brush, pen, new RectangleGeometry(rectangle, radiusX, radiusY));
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000A8344 File Offset: 0x000A7744
		public override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
		{
			this.DrawGeometry(brush, pen, new EllipseGeometry(center, radiusX, radiusY));
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000A8364 File Offset: 0x000A7764
		public override void DrawImage(ImageSource imageSource, Rect rectangle)
		{
			this.DrawGeometry(new ImageBrush
			{
				CanBeInheritanceContext = false,
				ImageSource = imageSource
			}, null, new RectangleGeometry(rectangle));
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000A8394 File Offset: 0x000A7794
		public override void DrawVideo(MediaPlayer video, Rect rectangle)
		{
			this.DrawGeometry(Brushes.Black, null, new RectangleGeometry(rectangle));
		}

		// Token: 0x040012E6 RID: 4838
		protected bool _contains;
	}
}
