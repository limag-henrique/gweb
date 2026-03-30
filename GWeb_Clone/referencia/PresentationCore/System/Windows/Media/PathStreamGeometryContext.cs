using System;
using System.Collections.Generic;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x0200042B RID: 1067
	internal class PathStreamGeometryContext : CapacityStreamGeometryContext
	{
		// Token: 0x06002BB9 RID: 11193 RVA: 0x000AE578 File Offset: 0x000AD978
		internal PathStreamGeometryContext()
		{
			this._pathGeometry = new PathGeometry();
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000AE598 File Offset: 0x000AD998
		internal PathStreamGeometryContext(FillRule fillRule, Transform transform)
		{
			this._pathGeometry = new PathGeometry();
			if (fillRule != PathStreamGeometryContext.s_defaultFillRule)
			{
				this._pathGeometry.FillRule = fillRule;
			}
			if (transform != null && !transform.IsIdentity)
			{
				this._pathGeometry.Transform = transform.Clone();
			}
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000AE5E8 File Offset: 0x000AD9E8
		internal override void SetFigureCount(int figureCount)
		{
			this._figures = new PathFigureCollection(figureCount);
			this._pathGeometry.Figures = this._figures;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000AE614 File Offset: 0x000ADA14
		internal override void SetSegmentCount(int segmentCount)
		{
			this._segments = new PathSegmentCollection(segmentCount);
			this._currentFigure.Segments = this._segments;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000AE640 File Offset: 0x000ADA40
		internal override void SetClosedState(bool isClosed)
		{
			if (isClosed != this._currentIsClosed)
			{
				this._currentFigure.IsClosed = isClosed;
				this._currentIsClosed = isClosed;
			}
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000AE66C File Offset: 0x000ADA6C
		public override void BeginFigure(Point startPoint, bool isFilled, bool isClosed)
		{
			if (this._currentFigure == null && this._figures == null)
			{
				this._figures = new PathFigureCollection();
				this._pathGeometry.Figures = this._figures;
			}
			this.FinishSegment();
			this._segments = null;
			this._currentFigure = new PathFigure();
			this._currentIsClosed = isClosed;
			if (startPoint != PathStreamGeometryContext.s_defaultValueForPathFigureStartPoint)
			{
				this._currentFigure.StartPoint = startPoint;
			}
			if (isClosed != PathStreamGeometryContext.s_defaultValueForPathFigureIsClosed)
			{
				this._currentFigure.IsClosed = isClosed;
			}
			if (isFilled != PathStreamGeometryContext.s_defaultValueForPathFigureIsFilled)
			{
				this._currentFigure.IsFilled = isFilled;
			}
			this._figures.Add(this._currentFigure);
			this._currentSegmentType = MIL_SEGMENT_TYPE.MilSegmentNone;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000AE720 File Offset: 0x000ADB20
		public override void LineTo(Point point, bool isStroked, bool isSmoothJoin)
		{
			this.PrepareToAddPoints(1, isStroked, isSmoothJoin, MIL_SEGMENT_TYPE.MilSegmentPolyLine);
			this._currentSegmentPoints.Add(point);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000AE744 File Offset: 0x000ADB44
		public override void QuadraticBezierTo(Point point1, Point point2, bool isStroked, bool isSmoothJoin)
		{
			this.PrepareToAddPoints(2, isStroked, isSmoothJoin, MIL_SEGMENT_TYPE.MilSegmentPolyQuadraticBezier);
			this._currentSegmentPoints.Add(point1);
			this._currentSegmentPoints.Add(point2);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000AE774 File Offset: 0x000ADB74
		public override void BezierTo(Point point1, Point point2, Point point3, bool isStroked, bool isSmoothJoin)
		{
			this.PrepareToAddPoints(3, isStroked, isSmoothJoin, MIL_SEGMENT_TYPE.MilSegmentPolyBezier);
			this._currentSegmentPoints.Add(point1);
			this._currentSegmentPoints.Add(point2);
			this._currentSegmentPoints.Add(point3);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000AE7B4 File Offset: 0x000ADBB4
		public override void PolyLineTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.GenericPolyTo(points, isStroked, isSmoothJoin, MIL_SEGMENT_TYPE.MilSegmentPolyLine);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000AE7CC File Offset: 0x000ADBCC
		public override void PolyQuadraticBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.GenericPolyTo(points, isStroked, isSmoothJoin, MIL_SEGMENT_TYPE.MilSegmentPolyQuadraticBezier);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000AE7E4 File Offset: 0x000ADBE4
		public override void PolyBezierTo(IList<Point> points, bool isStroked, bool isSmoothJoin)
		{
			this.GenericPolyTo(points, isStroked, isSmoothJoin, MIL_SEGMENT_TYPE.MilSegmentPolyBezier);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000AE7FC File Offset: 0x000ADBFC
		public override void ArcTo(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection, bool isStroked, bool isSmoothJoin)
		{
			this.FinishSegment();
			if (this._segments == null)
			{
				this._segments = new PathSegmentCollection();
				this._currentFigure.Segments = this._segments;
			}
			ArcSegment arcSegment = new ArcSegment();
			arcSegment.Point = point;
			arcSegment.Size = size;
			if (isLargeArc != PathStreamGeometryContext.s_defaultValueForArcSegmentIsLargeArc)
			{
				arcSegment.IsLargeArc = isLargeArc;
			}
			if (sweepDirection != PathStreamGeometryContext.s_defaultValueForArcSegmentSweepDirection)
			{
				arcSegment.SweepDirection = sweepDirection;
			}
			if (rotationAngle != PathStreamGeometryContext.s_defaultValueForArcSegmentRotationAngle)
			{
				arcSegment.RotationAngle = rotationAngle;
			}
			if (isStroked != PathStreamGeometryContext.s_defaultValueForPathSegmentIsStroked)
			{
				arcSegment.IsStroked = isStroked;
			}
			if (isSmoothJoin != PathStreamGeometryContext.s_defaultValueForPathSegmentIsSmoothJoin)
			{
				arcSegment.IsSmoothJoin = isSmoothJoin;
			}
			this._segments.Add(arcSegment);
			this._currentSegmentType = MIL_SEGMENT_TYPE.MilSegmentArc;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000AE8B0 File Offset: 0x000ADCB0
		public override void Close()
		{
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000AE8C0 File Offset: 0x000ADCC0
		internal PathGeometry GetPathGeometry()
		{
			this.FinishSegment();
			return this._pathGeometry;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000AE8DC File Offset: 0x000ADCDC
		private void GenericPolyTo(IList<Point> points, bool isStroked, bool isSmoothJoin, MIL_SEGMENT_TYPE segmentType)
		{
			int count = points.Count;
			this.PrepareToAddPoints(count, isStroked, isSmoothJoin, segmentType);
			for (int i = 0; i < count; i++)
			{
				this._currentSegmentPoints.Add(points[i]);
			}
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000AE91C File Offset: 0x000ADD1C
		private void PrepareToAddPoints(int count, bool isStroked, bool isSmoothJoin, MIL_SEGMENT_TYPE segmentType)
		{
			if (this._currentSegmentType != segmentType || this._currentSegmentIsStroked != isStroked || this._currentSegmentIsSmoothJoin != isSmoothJoin)
			{
				this.FinishSegment();
				this._currentSegmentType = segmentType;
				this._currentSegmentIsStroked = isStroked;
				this._currentSegmentIsSmoothJoin = isSmoothJoin;
			}
			if (this._currentSegmentPoints == null)
			{
				this._currentSegmentPoints = new PointCollection();
			}
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000AE974 File Offset: 0x000ADD74
		private void FinishSegment()
		{
			if (this._currentSegmentPoints != null)
			{
				int count = this._currentSegmentPoints.Count;
				if (this._segments == null)
				{
					this._segments = new PathSegmentCollection();
					this._currentFigure.Segments = this._segments;
				}
				PathSegment pathSegment;
				switch (this._currentSegmentType)
				{
				case MIL_SEGMENT_TYPE.MilSegmentPolyLine:
					if (count == 1)
					{
						pathSegment = new LineSegment
						{
							Point = this._currentSegmentPoints[0]
						};
					}
					else
					{
						pathSegment = new PolyLineSegment
						{
							Points = this._currentSegmentPoints
						};
					}
					break;
				case MIL_SEGMENT_TYPE.MilSegmentPolyBezier:
					if (count == 3)
					{
						pathSegment = new BezierSegment
						{
							Point1 = this._currentSegmentPoints[0],
							Point2 = this._currentSegmentPoints[1],
							Point3 = this._currentSegmentPoints[2]
						};
					}
					else
					{
						pathSegment = new PolyBezierSegment
						{
							Points = this._currentSegmentPoints
						};
					}
					break;
				case MIL_SEGMENT_TYPE.MilSegmentPolyQuadraticBezier:
					if (count == 2)
					{
						pathSegment = new QuadraticBezierSegment
						{
							Point1 = this._currentSegmentPoints[0],
							Point2 = this._currentSegmentPoints[1]
						};
					}
					else
					{
						pathSegment = new PolyQuadraticBezierSegment
						{
							Points = this._currentSegmentPoints
						};
					}
					break;
				default:
					pathSegment = null;
					break;
				}
				if (this._currentSegmentIsStroked != PathStreamGeometryContext.s_defaultValueForPathSegmentIsStroked)
				{
					pathSegment.IsStroked = this._currentSegmentIsStroked;
				}
				if (this._currentSegmentIsSmoothJoin != PathStreamGeometryContext.s_defaultValueForPathSegmentIsSmoothJoin)
				{
					pathSegment.IsSmoothJoin = this._currentSegmentIsSmoothJoin;
				}
				this._segments.Add(pathSegment);
				this._currentSegmentPoints = null;
				this._currentSegmentType = MIL_SEGMENT_TYPE.MilSegmentNone;
			}
		}

		// Token: 0x040013E6 RID: 5094
		private PathGeometry _pathGeometry;

		// Token: 0x040013E7 RID: 5095
		private PathFigureCollection _figures;

		// Token: 0x040013E8 RID: 5096
		private PathFigure _currentFigure;

		// Token: 0x040013E9 RID: 5097
		private PathSegmentCollection _segments;

		// Token: 0x040013EA RID: 5098
		private bool _currentIsClosed;

		// Token: 0x040013EB RID: 5099
		private MIL_SEGMENT_TYPE _currentSegmentType;

		// Token: 0x040013EC RID: 5100
		private PointCollection _currentSegmentPoints;

		// Token: 0x040013ED RID: 5101
		private bool _currentSegmentIsStroked;

		// Token: 0x040013EE RID: 5102
		private bool _currentSegmentIsSmoothJoin;

		// Token: 0x040013EF RID: 5103
		private static FillRule s_defaultFillRule = (FillRule)PathGeometry.FillRuleProperty.GetDefaultValue(typeof(PathGeometry));

		// Token: 0x040013F0 RID: 5104
		private static bool s_defaultValueForPathFigureIsClosed = (bool)PathFigure.IsClosedProperty.GetDefaultValue(typeof(PathFigure));

		// Token: 0x040013F1 RID: 5105
		private static bool s_defaultValueForPathFigureIsFilled = (bool)PathFigure.IsFilledProperty.GetDefaultValue(typeof(PathFigure));

		// Token: 0x040013F2 RID: 5106
		private static Point s_defaultValueForPathFigureStartPoint = (Point)PathFigure.StartPointProperty.GetDefaultValue(typeof(PathFigure));

		// Token: 0x040013F3 RID: 5107
		private static bool s_defaultValueForPathSegmentIsStroked = (bool)PathSegment.IsStrokedProperty.GetDefaultValue(typeof(PathSegment));

		// Token: 0x040013F4 RID: 5108
		private static bool s_defaultValueForPathSegmentIsSmoothJoin = (bool)PathSegment.IsSmoothJoinProperty.GetDefaultValue(typeof(PathSegment));

		// Token: 0x040013F5 RID: 5109
		private static bool s_defaultValueForArcSegmentIsLargeArc = (bool)ArcSegment.IsLargeArcProperty.GetDefaultValue(typeof(ArcSegment));

		// Token: 0x040013F6 RID: 5110
		private static SweepDirection s_defaultValueForArcSegmentSweepDirection = (SweepDirection)ArcSegment.SweepDirectionProperty.GetDefaultValue(typeof(ArcSegment));

		// Token: 0x040013F7 RID: 5111
		private static double s_defaultValueForArcSegmentRotationAngle = (double)ArcSegment.RotationAngleProperty.GetDefaultValue(typeof(ArcSegment));
	}
}
