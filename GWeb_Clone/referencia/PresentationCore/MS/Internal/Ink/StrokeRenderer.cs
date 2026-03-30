using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using MS.Internal.PresentationCore;

namespace MS.Internal.Ink
{
	// Token: 0x020007C4 RID: 1988
	internal static class StrokeRenderer
	{
		// Token: 0x060053E3 RID: 21475 RVA: 0x001531C8 File Offset: 0x001525C8
		internal static void CalcGeometryAndBoundsWithTransform(StrokeNodeIterator iterator, DrawingAttributes drawingAttributes, MatrixTypes stylusTipMatrixType, bool calculateBounds, out Geometry geometry, out Rect bounds)
		{
			StreamGeometry streamGeometry = new StreamGeometry();
			streamGeometry.FillRule = FillRule.Nonzero;
			StreamGeometryContext streamGeometryContext = streamGeometry.Open();
			geometry = streamGeometry;
			bounds = Rect.Empty;
			try
			{
				List<Point> list = new List<Point>(iterator.Count * 4);
				int num = iterator.Count * 2;
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					list.Add(new Point(0.0, 0.0));
				}
				List<Point> list2 = new List<Point>();
				double num3 = 0.0;
				bool flag = false;
				Rect rect = new Rect(0.0, 0.0, 0.0, 0.0);
				for (int j = 0; j < iterator.Count; j++)
				{
					StrokeNode strokeNode = iterator[j];
					Rect bounds2 = strokeNode.GetBounds();
					if (calculateBounds)
					{
						bounds.Union(bounds2);
					}
					double num4 = Math.Abs(StrokeRenderer.GetAngleDeltaFromLast(strokeNode.PreviousPosition, strokeNode.Position, ref num3));
					double num5 = 45.0;
					if (stylusTipMatrixType == MatrixTypes.TRANSFORM_IS_UNKNOWN)
					{
						num5 = 10.0;
					}
					else if (bounds2.Height > 40.0 || bounds2.Width > 40.0)
					{
						num5 = 20.0;
					}
					bool flag2 = num4 > num5 && num4 < 360.0 - num5;
					double val = rect.Height * rect.Width;
					double val2 = bounds2.Height * bounds2.Width;
					bool flag3 = false;
					if (Math.Min(val, val2) / Math.Max(val, val2) <= 0.7)
					{
						flag3 = true;
					}
					rect = bounds2;
					if (j <= 1 || j >= iterator.Count - 2 || flag2 || flag3)
					{
						if (flag2 && !flag && j > 1 && j < iterator.Count - 1)
						{
							list2.Clear();
							strokeNode.GetPreviousContourPoints(list2);
							StrokeRenderer.AddFigureToStreamGeometryContext(streamGeometryContext, list2, strokeNode.IsEllipse);
							flag = true;
						}
						list2.Clear();
						strokeNode.GetContourPoints(list2);
						StrokeRenderer.AddFigureToStreamGeometryContext(streamGeometryContext, list2, strokeNode.IsEllipse);
					}
					if (!flag2)
					{
						flag = false;
					}
					Quad connectingQuad = strokeNode.GetConnectingQuad();
					if (!connectingQuad.IsEmpty)
					{
						list[num2++] = connectingQuad.A;
						list[num2++] = connectingQuad.B;
						list.Add(connectingQuad.D);
						list.Add(connectingQuad.C);
					}
					if (strokeNode.IsLastNode && num2 > 0)
					{
						int num6 = iterator.Count * 2;
						int num7 = list.Count - 1;
						int num8 = num2;
						for (int k = num6; k <= num7; k++)
						{
							list[num8] = list[k];
							num8++;
						}
						int num9 = num6 - num2;
						list.RemoveRange(num7 - num9 + 1, num9);
						int l = num2;
						int num10 = list.Count - 1;
						while (l < num10)
						{
							Point value = list[l];
							list[l] = list[num10];
							list[num10] = value;
							l++;
							num10--;
						}
						StrokeRenderer.AddFigureToStreamGeometryContext(streamGeometryContext, list, false);
					}
				}
			}
			finally
			{
				streamGeometryContext.Close();
				geometry.Freeze();
			}
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x00153534 File Offset: 0x00152934
		[FriendAccessAllowed]
		internal static void CalcGeometryAndBounds(StrokeNodeIterator iterator, DrawingAttributes drawingAttributes, bool calculateBounds, out Geometry geometry, out Rect bounds)
		{
			Matrix stylusTipTransform = drawingAttributes.StylusTipTransform;
			if (stylusTipTransform != Matrix.Identity && stylusTipTransform._type != MatrixTypes.TRANSFORM_IS_SCALING)
			{
				StrokeRenderer.CalcGeometryAndBoundsWithTransform(iterator, drawingAttributes, stylusTipTransform._type, calculateBounds, out geometry, out bounds);
				return;
			}
			StreamGeometry streamGeometry = new StreamGeometry();
			streamGeometry.FillRule = FillRule.Nonzero;
			StreamGeometryContext streamGeometryContext = streamGeometry.Open();
			geometry = streamGeometry;
			Rect empty = Rect.Empty;
			bounds = empty;
			try
			{
				StrokeNode strokeNode = default(StrokeNode);
				StrokeNode strokeNodePrevious = default(StrokeNode);
				StrokeNode strokeNode2 = default(StrokeNode);
				StrokeNode strokeNode3 = default(StrokeNode);
				Rect rect = empty;
				Rect rect2 = empty;
				Rect rect3 = empty;
				double num = 95.0;
				double num2 = Math.Max(drawingAttributes.Height, drawingAttributes.Width);
				num += Math.Min(4.99999, num2 / 20.0 * 5.0);
				double num3 = double.MinValue;
				bool flag = true;
				bool isEllipse = drawingAttributes.StylusTip == StylusTip.Ellipse;
				bool ignorePressure = drawingAttributes.IgnorePressure;
				List<Point> list = new List<Point>();
				List<Point> list2 = new List<Point>();
				List<Point> list3 = new List<Point>(4);
				int count = iterator.Count;
				int i = 0;
				int previousIndex = -1;
				while (i < count)
				{
					if (!strokeNodePrevious.IsValid)
					{
						if (!strokeNode2.IsValid)
						{
							strokeNodePrevious = iterator[i++, previousIndex++];
							rect = strokeNodePrevious.GetBounds();
							continue;
						}
						strokeNodePrevious = strokeNode2;
						rect = rect2;
						strokeNode2 = strokeNode;
					}
					if (!strokeNode2.IsValid)
					{
						if (strokeNode3.IsValid)
						{
							strokeNode2 = strokeNode3;
							rect2 = rect3;
							strokeNode3 = strokeNode;
						}
						else
						{
							strokeNode2 = iterator[i++, previousIndex];
							rect2 = strokeNode2.GetBounds();
							StrokeRenderer.RectCompareResult rectCompareResult = StrokeRenderer.FuzzyContains(rect2, rect, flag ? 99.99999 : num);
							if (rectCompareResult == StrokeRenderer.RectCompareResult.Rect1ContainsRect2)
							{
								strokeNodePrevious = iterator[i - 1, strokeNodePrevious.Index - 1];
								rect = Rect.Union(rect2, rect);
								strokeNode2 = strokeNode;
								previousIndex = i - 1;
								continue;
							}
							if (rectCompareResult == StrokeRenderer.RectCompareResult.Rect2ContainsRect1)
							{
								strokeNode2 = strokeNode;
								continue;
							}
							previousIndex = i - 1;
							continue;
						}
					}
					if (!strokeNode3.IsValid)
					{
						strokeNode3 = iterator[i++, previousIndex];
						rect3 = strokeNode3.GetBounds();
						StrokeRenderer.RectCompareResult rectCompareResult2 = StrokeRenderer.FuzzyContains(rect3, rect2, flag ? 99.99999 : num);
						StrokeRenderer.RectCompareResult rectCompareResult3 = StrokeRenderer.FuzzyContains(rect3, rect, flag ? 99.99999 : num);
						if (flag && rectCompareResult2 == StrokeRenderer.RectCompareResult.Rect1ContainsRect2 && rectCompareResult3 == StrokeRenderer.RectCompareResult.Rect1ContainsRect2)
						{
							if (list.Count > 0)
							{
								strokeNode2.GetPointsAtEndOfSegment(list, list2);
								StrokeRenderer.ReverseDCPointsRenderAndClear(streamGeometryContext, list, list2, list3, isEllipse, true);
							}
							strokeNodePrevious = iterator[i - 1, strokeNodePrevious.Index - 1];
							rect = strokeNodePrevious.GetBounds();
							strokeNode2 = strokeNode;
							strokeNode3 = strokeNode;
							previousIndex = i - 1;
							continue;
						}
						if (rectCompareResult2 == StrokeRenderer.RectCompareResult.Rect1ContainsRect2)
						{
							strokeNode3 = iterator[i - 1, strokeNode2.Index - 1];
							if (!strokeNode3.GetConnectingQuad().IsEmpty)
							{
								strokeNode2 = strokeNode3;
								rect2 = Rect.Union(rect3, rect2);
								previousIndex = i - 1;
							}
							strokeNode3 = strokeNode;
							num3 = double.MinValue;
							continue;
						}
						if (rectCompareResult2 == StrokeRenderer.RectCompareResult.Rect2ContainsRect1)
						{
							strokeNode3 = strokeNode;
							continue;
						}
						previousIndex = i - 1;
					}
					bool flag2 = rect.IntersectsWith(rect3);
					if (calculateBounds)
					{
						bounds.Union(rect2);
					}
					if (list.Count == 0)
					{
						if (calculateBounds)
						{
							bounds.Union(rect);
						}
						if (flag && flag2)
						{
							strokeNodePrevious.GetContourPoints(list3);
							StrokeRenderer.AddFigureToStreamGeometryContext(streamGeometryContext, list3, strokeNodePrevious.IsEllipse);
							list3.Clear();
						}
						strokeNode2.GetPointsAtStartOfSegment(list, list2);
						flag = false;
					}
					if (num3 == -1.7976931348623157E+308)
					{
						num3 = StrokeRenderer.GetAngleBetween(strokeNodePrevious.Position, strokeNode2.Position);
					}
					double angleDeltaFromLast = StrokeRenderer.GetAngleDeltaFromLast(strokeNode2.Position, strokeNode3.Position, ref num3);
					bool flag3 = Math.Abs(angleDeltaFromLast) > 90.0 && Math.Abs(angleDeltaFromLast) < 270.0;
					bool flag4 = flag2 && !ignorePressure && strokeNode3.PressureFactor != 1f && Math.Abs(angleDeltaFromLast) > 30.0 && Math.Abs(angleDeltaFromLast) < 330.0;
					double num4 = rect2.Height * rect2.Width;
					double num5 = rect3.Height * rect3.Width;
					bool flag5 = num4 != num5 || num4 != rect.Height * rect.Width;
					bool flag6 = false;
					if (flag2 && flag5 && Math.Min(num4, num5) / Math.Max(num4, num5) <= 0.9)
					{
						flag6 = true;
					}
					if (flag5 || angleDeltaFromLast != 0.0 || i >= count)
					{
						if ((flag2 && (flag4 || flag6)) || flag3)
						{
							strokeNode2.GetPointsAtEndOfSegment(list, list2);
							StrokeRenderer.ReverseDCPointsRenderAndClear(streamGeometryContext, list, list2, list3, isEllipse, true);
							if (flag6)
							{
								strokeNode2.GetContourPoints(list3);
								StrokeRenderer.AddFigureToStreamGeometryContext(streamGeometryContext, list3, strokeNode2.IsEllipse);
								list3.Clear();
							}
						}
						else
						{
							bool flag7;
							strokeNode3.GetPointsAtMiddleSegment(strokeNode2, angleDeltaFromLast, list, list2, out flag7);
							if (flag7)
							{
								strokeNode2.GetPointsAtEndOfSegment(list, list2);
								StrokeRenderer.ReverseDCPointsRenderAndClear(streamGeometryContext, list, list2, list3, isEllipse, true);
							}
						}
					}
					strokeNodePrevious = strokeNode;
					rect = empty;
				}
				if (strokeNodePrevious.IsValid)
				{
					if (strokeNode2.IsValid)
					{
						if (calculateBounds)
						{
							bounds.Union(rect);
							bounds.Union(rect2);
						}
						if (list.Count > 0)
						{
							strokeNode2.GetPointsAtEndOfSegment(list, list2);
							StrokeRenderer.ReverseDCPointsRenderAndClear(streamGeometryContext, list, list2, list3, isEllipse, false);
						}
						else
						{
							StrokeRenderer.RenderTwoStrokeNodes(streamGeometryContext, strokeNodePrevious, rect, strokeNode2, rect2, list, list2, list3);
						}
					}
					else
					{
						if (calculateBounds)
						{
							bounds.Union(rect);
						}
						strokeNodePrevious.GetContourPoints(list);
						StrokeRenderer.AddFigureToStreamGeometryContext(streamGeometryContext, list, strokeNodePrevious.IsEllipse);
					}
				}
				else if (strokeNode2.IsValid && strokeNode3.IsValid)
				{
					if (calculateBounds)
					{
						bounds.Union(rect2);
						bounds.Union(rect3);
					}
					if (list.Count > 0)
					{
						strokeNode3.GetPointsAtEndOfSegment(list, list2);
						StrokeRenderer.ReverseDCPointsRenderAndClear(streamGeometryContext, list, list2, list3, isEllipse, false);
						if (StrokeRenderer.FuzzyContains(rect3, rect2, 70.0) != StrokeRenderer.RectCompareResult.NoItersection)
						{
							strokeNode3.GetContourPoints(list3);
							StrokeRenderer.AddFigureToStreamGeometryContext(streamGeometryContext, list3, strokeNode3.IsEllipse);
						}
					}
					else
					{
						StrokeRenderer.RenderTwoStrokeNodes(streamGeometryContext, strokeNode2, rect2, strokeNode3, rect3, list, list2, list3);
					}
				}
			}
			finally
			{
				streamGeometryContext.Close();
				geometry.Freeze();
			}
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x00153BA4 File Offset: 0x00152FA4
		private static void RenderTwoStrokeNodes(StreamGeometryContext context, StrokeNode strokeNodePrevious, Rect strokeNodePreviousBounds, StrokeNode strokeNodeCurrent, Rect strokeNodeCurrentBounds, List<Point> pointBuffer1, List<Point> pointBuffer2, List<Point> pointBuffer3)
		{
			if (StrokeRenderer.FuzzyContains(strokeNodePreviousBounds, strokeNodeCurrentBounds, 70.0) != StrokeRenderer.RectCompareResult.NoItersection)
			{
				strokeNodePrevious.GetContourPoints(pointBuffer1);
				StrokeRenderer.AddFigureToStreamGeometryContext(context, pointBuffer1, strokeNodePrevious.IsEllipse);
				Quad connectingQuad = strokeNodeCurrent.GetConnectingQuad();
				if (!connectingQuad.IsEmpty)
				{
					pointBuffer3.Add(connectingQuad.A);
					pointBuffer3.Add(connectingQuad.B);
					pointBuffer3.Add(connectingQuad.C);
					pointBuffer3.Add(connectingQuad.D);
					StrokeRenderer.AddFigureToStreamGeometryContext(context, pointBuffer3, false);
				}
				strokeNodeCurrent.GetContourPoints(pointBuffer2);
				StrokeRenderer.AddFigureToStreamGeometryContext(context, pointBuffer2, strokeNodeCurrent.IsEllipse);
				return;
			}
			strokeNodeCurrent.GetPointsAtStartOfSegment(pointBuffer1, pointBuffer2);
			strokeNodeCurrent.GetPointsAtEndOfSegment(pointBuffer1, pointBuffer2);
			StrokeRenderer.ReverseDCPointsRenderAndClear(context, pointBuffer1, pointBuffer2, pointBuffer3, strokeNodeCurrent.IsEllipse, false);
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x00153C78 File Offset: 0x00153078
		private static void ReverseDCPointsRenderAndClear(StreamGeometryContext context, List<Point> abPoints, List<Point> dcPoints, List<Point> polyLinePoints, bool isEllipse, bool clear)
		{
			int i = 0;
			int num = dcPoints.Count - 1;
			while (i < num)
			{
				Point value = dcPoints[i];
				dcPoints[i] = dcPoints[num];
				dcPoints[num] = value;
				i++;
				num--;
			}
			if (isEllipse)
			{
				StrokeRenderer.AddArcToFigureToStreamGeometryContext(context, abPoints, dcPoints, polyLinePoints);
			}
			else
			{
				StrokeRenderer.AddPolylineFigureToStreamGeometryContext(context, abPoints, dcPoints);
			}
			if (clear)
			{
				abPoints.Clear();
				dcPoints.Clear();
			}
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x00153CE4 File Offset: 0x001530E4
		private static StrokeRenderer.RectCompareResult FuzzyContains(Rect rect1, Rect rect2, double percentIntersect)
		{
			double num = Math.Max(rect1.Left, rect2.Left);
			double num2 = Math.Max(rect1.Top, rect2.Top);
			double num3 = Math.Max(Math.Min(rect1.Right, rect2.Right) - num, 0.0);
			double num4 = Math.Max(Math.Min(rect1.Bottom, rect2.Bottom) - num2, 0.0);
			if (num3 == 0.0 || num4 == 0.0)
			{
				return StrokeRenderer.RectCompareResult.NoItersection;
			}
			double num5 = rect1.Height * rect1.Width;
			double num6 = rect2.Height * rect2.Width;
			double num7 = Math.Min(num5, num6);
			double num8 = num3 * num4;
			double num9 = num8 / num7 * 100.0;
			if (num9 < percentIntersect)
			{
				return StrokeRenderer.RectCompareResult.NoItersection;
			}
			if (num5 >= num6)
			{
				return StrokeRenderer.RectCompareResult.Rect1ContainsRect2;
			}
			return StrokeRenderer.RectCompareResult.Rect2ContainsRect1;
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x00153DD0 File Offset: 0x001531D0
		private static void AddFigureToStreamGeometryContext(StreamGeometryContext context, List<Point> points, bool isBezierFigure)
		{
			context.BeginFigure(points[points.Count - 1], true, true);
			if (isBezierFigure)
			{
				context.PolyBezierTo(points, true, true);
				return;
			}
			context.PolyLineTo(points, true, true);
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x00153E0C File Offset: 0x0015320C
		private static void AddPolylineFigureToStreamGeometryContext(StreamGeometryContext context, List<Point> abPoints, List<Point> dcPoints)
		{
			context.BeginFigure(abPoints[0], true, true);
			context.PolyLineTo(abPoints, true, true);
			context.PolyLineTo(dcPoints, true, true);
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x00153E3C File Offset: 0x0015323C
		private static void AddArcToFigureToStreamGeometryContext(StreamGeometryContext context, List<Point> abPoints, List<Point> dcPoints, List<Point> polyLinePoints)
		{
			if (abPoints.Count == 0 || dcPoints.Count == 0)
			{
				return;
			}
			context.BeginFigure(abPoints[0], true, true);
			for (int i = 0; i < 2; i++)
			{
				List<Point> list = (i == 0) ? abPoints : dcPoints;
				int num = (i == 0) ? 1 : 0;
				int j = num;
				while (j < list.Count)
				{
					Point point = list[j];
					if (point == StrokeRenderer.ArcToMarker)
					{
						if (polyLinePoints.Count > 0)
						{
							context.PolyLineTo(polyLinePoints, true, true);
							polyLinePoints.Clear();
						}
						if (j + 2 < list.Count)
						{
							Point point2 = list[j + 1];
							Size size = new Size(point2.X / 2.0, point2.Y / 2.0);
							Point point3 = list[j + 2];
							bool isLargeArc = false;
							context.ArcTo(point3, size, 0.0, isLargeArc, SweepDirection.Clockwise, true, true);
						}
						j += 3;
					}
					else
					{
						polyLinePoints.Add(point);
						j++;
					}
				}
				if (polyLinePoints.Count > 0)
				{
					context.PolyLineTo(polyLinePoints, true, true);
					polyLinePoints.Clear();
				}
			}
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x00153F60 File Offset: 0x00153360
		private static double GetAngleDeltaFromLast(Point previousPosition, Point currentPosition, ref double lastAngle)
		{
			double result = 0.0;
			double num = currentPosition.X * 1000.0 - previousPosition.X * 1000.0;
			double num2 = currentPosition.Y * 1000.0 - previousPosition.Y * 1000.0;
			if ((long)num == 0L && (long)num2 == 0L)
			{
				return result;
			}
			double angleBetween = StrokeRenderer.GetAngleBetween(previousPosition, currentPosition);
			if (lastAngle >= 270.0 && angleBetween <= 90.0)
			{
				result = lastAngle - (360.0 + angleBetween);
			}
			else if (lastAngle <= 90.0 && angleBetween >= 270.0)
			{
				result = 360.0 + lastAngle - angleBetween;
			}
			else
			{
				result = lastAngle - angleBetween;
			}
			lastAngle = angleBetween;
			return result;
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x00154030 File Offset: 0x00153430
		private static double GetAngleBetween(Point previousPosition, Point currentPosition)
		{
			double num = 0.0;
			double num2 = currentPosition.X * 1000.0 - previousPosition.X * 1000.0;
			double num3 = currentPosition.Y * 1000.0 - previousPosition.Y * 1000.0;
			if ((long)num2 == 0L && (long)num3 == 0L)
			{
				return num;
			}
			if (num2 == 0.0)
			{
				if (num3 == 0.0)
				{
					num = 0.0;
				}
				else if (num3 > 0.0)
				{
					num = 1.5707963267948966;
				}
				else
				{
					num = 4.71238898038469;
				}
			}
			else if (num3 == 0.0)
			{
				if (num2 > 0.0)
				{
					num = 0.0;
				}
				else
				{
					num = 3.1415926535897931;
				}
			}
			else if (num2 < 0.0)
			{
				num = Math.Atan(num3 / num2) + 3.1415926535897931;
			}
			else if (num3 < 0.0)
			{
				num = Math.Atan(num3 / num2) + 6.2831853071795862;
			}
			else
			{
				num = Math.Atan(num3 / num2);
			}
			return num * 180.0 / 3.1415926535897931;
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x0015417C File Offset: 0x0015357C
		internal static DrawingAttributes GetHighlighterAttributes(Stroke stroke, DrawingAttributes da)
		{
			if (da.Color.A != StrokeRenderer.SolidStrokeAlpha)
			{
				DrawingAttributes drawingAttributes = stroke.DrawingAttributes.Clone();
				drawingAttributes.Color = StrokeRenderer.GetHighlighterColor(drawingAttributes.Color);
				return drawingAttributes;
			}
			return da;
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x001541C0 File Offset: 0x001535C0
		internal static Color GetHighlighterColor(Color color)
		{
			color.A = StrokeRenderer.SolidStrokeAlpha;
			return color;
		}

		// Token: 0x040025D0 RID: 9680
		internal static readonly double HighlighterOpacity = 0.5;

		// Token: 0x040025D1 RID: 9681
		internal static readonly byte SolidStrokeAlpha = byte.MaxValue;

		// Token: 0x040025D2 RID: 9682
		internal static readonly Point ArcToMarker = new Point(double.MinValue, double.MinValue);

		// Token: 0x02000A06 RID: 2566
		private enum RectCompareResult
		{
			// Token: 0x04002F47 RID: 12103
			Rect1ContainsRect2,
			// Token: 0x04002F48 RID: 12104
			Rect2ContainsRect1,
			// Token: 0x04002F49 RID: 12105
			NoItersection
		}
	}
}
