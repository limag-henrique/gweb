using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MS.Internal.Media3D
{
	// Token: 0x020006F0 RID: 1776
	internal class HitTestEdge
	{
		// Token: 0x06004C86 RID: 19590 RVA: 0x0012C080 File Offset: 0x0012B480
		public HitTestEdge(Point3D p1, Point3D p2, Point uv1, Point uv2)
		{
			this._p1 = p1;
			this._p2 = p2;
			this._uv1 = uv1;
			this._uv2 = uv2;
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x0012C0B0 File Offset: 0x0012B4B0
		public void Project(GeneralTransform3DTo2D objectToViewportTransform)
		{
			Point point = objectToViewportTransform.Transform(this._p1);
			Point point2 = objectToViewportTransform.Transform(this._p2);
			this._p1Transformed = new Point(point.X, point.Y);
			this._p2Transformed = new Point(point2.X, point2.Y);
		}

		// Token: 0x04002140 RID: 8512
		internal Point3D _p1;

		// Token: 0x04002141 RID: 8513
		internal Point3D _p2;

		// Token: 0x04002142 RID: 8514
		internal Point _uv1;

		// Token: 0x04002143 RID: 8515
		internal Point _uv2;

		// Token: 0x04002144 RID: 8516
		internal Point _p1Transformed;

		// Token: 0x04002145 RID: 8517
		internal Point _p2Transformed;
	}
}
