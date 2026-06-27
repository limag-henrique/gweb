using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;

namespace MS.Internal.Ink
{
	// Token: 0x020007BA RID: 1978
	internal class EllipticalNodeOperations : StrokeNodeOperations
	{
		// Token: 0x06005341 RID: 21313 RVA: 0x0014D898 File Offset: 0x0014CC98
		internal EllipticalNodeOperations(StylusShape nodeShape) : base(nodeShape)
		{
			this._radii = new Size(nodeShape.Width * 0.5, nodeShape.Height * 0.5);
			this._radius = Math.Max(this._radii.Width, this._radii.Height);
			this._transform = nodeShape.Transform;
			this._nodeShapeToCircle = this._transform;
			this._nodeShapeToCircle.Invert();
			if (DoubleUtil.AreClose(this._radii.Width, this._radii.Height))
			{
				this._circleToNodeShape = this._transform;
				return;
			}
			if (!DoubleUtil.IsZero(nodeShape.Rotation))
			{
				this._nodeShapeToCircle.Rotate(-nodeShape.Rotation);
			}
			double scaleX;
			double scaleY;
			if (this._radii.Width > this._radii.Height)
			{
				scaleX = 1.0;
				scaleY = this._radii.Width / this._radii.Height;
			}
			else
			{
				scaleX = this._radii.Height / this._radii.Width;
				scaleY = 1.0;
			}
			this._nodeShapeToCircle.Scale(scaleX, scaleY);
			this._circleToNodeShape = this._nodeShapeToCircle;
			this._circleToNodeShape.Invert();
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06005342 RID: 21314 RVA: 0x0014D9E8 File Offset: 0x0014CDE8
		internal override bool IsNodeShapeEllipse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0014D9F8 File Offset: 0x0014CDF8
		internal override Quad GetConnectingQuad(StrokeNodeData beginNode, StrokeNodeData endNode)
		{
			if (beginNode.IsEmpty || endNode.IsEmpty || DoubleUtil.AreClose(beginNode.Position, endNode.Position))
			{
				return Quad.Empty;
			}
			Vector vector = endNode.Position - beginNode.Position;
			if (!this._nodeShapeToCircle.IsIdentity)
			{
				vector = this._nodeShapeToCircle.Transform(vector);
			}
			double num = this._radius * (double)beginNode.PressureFactor;
			double num2 = this._radius * (double)endNode.PressureFactor;
			double lengthSquared = vector.LengthSquared;
			double num3 = num2 - num;
			double num4 = DoubleUtil.IsZero(num3) ? 0.0 : (num3 * num3);
			if (DoubleUtil.LessThanOrClose(lengthSquared, num4))
			{
				return Quad.Empty;
			}
			double scalar = Math.Sqrt(lengthSquared);
			vector /= scalar;
			Vector vector2 = vector;
			double y = vector2.Y;
			vector2.Y = -vector2.X;
			vector2.X = y;
			double num5 = num4 / lengthSquared;
			Vector vector3;
			Vector vector4;
			if (DoubleUtil.IsZero(num5))
			{
				vector3 = vector2;
				vector4 = -vector2;
			}
			else
			{
				vector2 *= Math.Sqrt(1.0 - num5);
				vector *= Math.Sqrt(num5);
				if (beginNode.PressureFactor < endNode.PressureFactor)
				{
					vector = -vector;
				}
				vector3 = vector + vector2;
				vector4 = vector - vector2;
			}
			if (!this._circleToNodeShape.IsIdentity)
			{
				vector3 = this._circleToNodeShape.Transform(vector3);
				vector4 = this._circleToNodeShape.Transform(vector4);
			}
			return new Quad(beginNode.Position + vector3 * num, endNode.Position + vector3 * num2, endNode.Position + vector4 * num2, beginNode.Position + vector4 * num);
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0014DBD8 File Offset: 0x0014CFD8
		internal override IEnumerable<ContourSegment> GetContourSegments(StrokeNodeData node, Quad quad)
		{
			if (quad.IsEmpty)
			{
				Point position = node.Position;
				position.X += this._radius;
				yield return new ContourSegment(position, position, node.Position);
			}
			else if (this._nodeShapeToCircle.IsIdentity)
			{
				yield return new ContourSegment(quad.A, quad.B);
				yield return new ContourSegment(quad.B, quad.C, node.Position);
				yield return new ContourSegment(quad.C, quad.D);
				yield return new ContourSegment(quad.D, quad.A);
			}
			yield break;
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x0014DC04 File Offset: 0x0014D004
		internal override IEnumerable<ContourSegment> GetNonBezierContourSegments(StrokeNodeData beginNode, StrokeNodeData endNode)
		{
			Quad quad = beginNode.IsEmpty ? Quad.Empty : base.GetConnectingQuad(beginNode, endNode);
			return base.GetContourSegments(endNode, quad);
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0014DC34 File Offset: 0x0014D034
		internal override bool HitTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, Point hitBeginPoint, Point hitEndPoint)
		{
			StrokeNodeData strokeNodeData;
			StrokeNodeData strokeNodeData2;
			if (beginNode.IsEmpty || (quad.IsEmpty && endNode.PressureFactor > beginNode.PressureFactor))
			{
				strokeNodeData = endNode;
				strokeNodeData2 = StrokeNodeData.Empty;
			}
			else
			{
				strokeNodeData = beginNode;
				strokeNodeData2 = endNode;
			}
			Vector vector = hitBeginPoint - strokeNodeData.Position;
			Vector vector2 = hitEndPoint - strokeNodeData.Position;
			if (!this._nodeShapeToCircle.IsIdentity)
			{
				vector = this._nodeShapeToCircle.Transform(vector);
				vector2 = this._nodeShapeToCircle.Transform(vector2);
			}
			bool result = false;
			double num = this._radius * (double)strokeNodeData.PressureFactor;
			if (StrokeNodeOperations.GetNearest(vector, vector2).LengthSquared <= num * num)
			{
				result = true;
			}
			else if (!quad.IsEmpty)
			{
				Vector vector3 = strokeNodeData2.Position - strokeNodeData.Position;
				if (!this._nodeShapeToCircle.IsIdentity)
				{
					vector3 = this._nodeShapeToCircle.Transform(vector3);
				}
				double num2 = this._radius * (double)strokeNodeData2.PressureFactor;
				if (StrokeNodeOperations.GetNearest(vector - vector3, vector2 - vector3).LengthSquared <= num2 * num2 || StrokeNodeOperations.HitTestQuadSegment(quad, hitBeginPoint, hitEndPoint))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0014DD68 File Offset: 0x0014D168
		internal override bool HitTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, IEnumerable<ContourSegment> hitContour)
		{
			double num = 0.0;
			StrokeNodeData strokeNodeData;
			Vector vector;
			if (beginNode.IsEmpty || (quad.IsEmpty && endNode.PressureFactor > beginNode.PressureFactor))
			{
				strokeNodeData = endNode;
				StrokeNodeData strokeNodeData2 = StrokeNodeData.Empty;
				vector = default(Vector);
			}
			else
			{
				strokeNodeData = beginNode;
				StrokeNodeData strokeNodeData2 = endNode;
				num = this._radius * (double)strokeNodeData2.PressureFactor;
				num *= num;
				vector = strokeNodeData2.Position - strokeNodeData.Position;
				if (!this._nodeShapeToCircle.IsIdentity)
				{
					vector = this._nodeShapeToCircle.Transform(vector);
				}
			}
			double num2 = this._radius * (double)strokeNodeData.PressureFactor;
			num2 *= num2;
			bool flag = false;
			bool flag2 = true;
			foreach (ContourSegment contourSegment in hitContour)
			{
				if (!contourSegment.IsArc)
				{
					Vector vector2 = contourSegment.Begin - strokeNodeData.Position;
					Vector vector3 = vector2 + contourSegment.Vector;
					if (!this._nodeShapeToCircle.IsIdentity)
					{
						vector2 = this._nodeShapeToCircle.Transform(vector2);
						vector3 = this._nodeShapeToCircle.Transform(vector3);
					}
					if (StrokeNodeOperations.GetNearest(vector2, vector3).LengthSquared <= num2)
					{
						flag = true;
						break;
					}
					if (!quad.IsEmpty && (StrokeNodeOperations.GetNearest(vector2 - vector, vector3 - vector).LengthSquared <= num || StrokeNodeOperations.HitTestQuadSegment(quad, contourSegment.Begin, contourSegment.End)))
					{
						flag = true;
						break;
					}
					if (flag2 && StrokeNodeOperations.WhereIsVectorAboutVector(endNode.Position - contourSegment.Begin, contourSegment.Vector) != StrokeNodeOperations.HitResult.Right)
					{
						flag2 = false;
					}
				}
			}
			return flag || flag2;
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x0014DF4C File Offset: 0x0014D34C
		internal override StrokeFIndices CutTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, Point hitBeginPoint, Point hitEndPoint)
		{
			Vector vector = beginNode.IsEmpty ? new Vector(0.0, 0.0) : (beginNode.Position - endNode.Position);
			Vector vector2 = hitBeginPoint - endNode.Position;
			Vector vector3 = hitEndPoint - endNode.Position;
			if (!this._nodeShapeToCircle.IsIdentity)
			{
				vector = this._nodeShapeToCircle.Transform(vector);
				vector2 = this._nodeShapeToCircle.Transform(vector2);
				vector3 = this._nodeShapeToCircle.Transform(vector3);
			}
			StrokeFIndices empty = StrokeFIndices.Empty;
			double num = 0.0;
			double num2 = this._radius * (double)endNode.PressureFactor;
			if (StrokeNodeOperations.GetNearest(vector2, vector3).LengthSquared <= num2 * num2)
			{
				empty.EndFIndex = StrokeFIndices.AfterLast;
				empty.BeginFIndex = (beginNode.IsEmpty ? StrokeFIndices.BeforeFirst : 1.0);
			}
			if (!beginNode.IsEmpty)
			{
				num = this._radius * (double)beginNode.PressureFactor;
				if (StrokeNodeOperations.GetNearest(vector2 - vector, vector3 - vector).LengthSquared <= num * num)
				{
					empty.BeginFIndex = StrokeFIndices.BeforeFirst;
					if (!DoubleUtil.AreClose(empty.EndFIndex, StrokeFIndices.AfterLast))
					{
						empty.EndFIndex = 0.0;
					}
				}
			}
			if (empty.IsFull || quad.IsEmpty || (empty.IsEmpty && !StrokeNodeOperations.HitTestQuadSegment(quad, hitBeginPoint, hitEndPoint)))
			{
				return empty;
			}
			if (!DoubleUtil.AreClose(empty.BeginFIndex, StrokeFIndices.BeforeFirst))
			{
				empty.BeginFIndex = EllipticalNodeOperations.ClipTest(-vector, num, num2, vector2 - vector, vector3 - vector);
			}
			if (!DoubleUtil.AreClose(empty.EndFIndex, StrokeFIndices.AfterLast))
			{
				empty.EndFIndex = 1.0 - EllipticalNodeOperations.ClipTest(vector, num2, num, vector2, vector3);
			}
			if (base.IsInvalidCutTestResult(empty))
			{
				return StrokeFIndices.Empty;
			}
			return empty;
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x0014E150 File Offset: 0x0014D550
		internal override StrokeFIndices CutTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, IEnumerable<ContourSegment> hitContour)
		{
			Vector vector = beginNode.IsEmpty ? new Vector(0.0, 0.0) : (beginNode.Position - endNode.Position);
			if (!this._nodeShapeToCircle.IsIdentity)
			{
				vector = this._nodeShapeToCircle.Transform(vector);
			}
			double num = 0.0;
			double num2 = 0.0;
			double num3 = this._radius * (double)endNode.PressureFactor;
			double num4 = num3 * num3;
			if (!beginNode.IsEmpty)
			{
				num = this._radius * (double)beginNode.PressureFactor;
				num2 = num * num;
			}
			bool flag = true;
			StrokeFIndices result = StrokeFIndices.Empty;
			foreach (ContourSegment contourSegment in hitContour)
			{
				if (!contourSegment.IsArc)
				{
					Vector vector2 = contourSegment.Begin - endNode.Position;
					Vector vector3 = vector2 + contourSegment.Vector;
					if (!this._nodeShapeToCircle.IsIdentity)
					{
						vector2 = this._nodeShapeToCircle.Transform(vector2);
						vector3 = this._nodeShapeToCircle.Transform(vector3);
					}
					bool flag2 = false;
					if (StrokeNodeOperations.GetNearest(vector2, vector3).LengthSquared < num4)
					{
						flag2 = true;
						if (!DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.AfterLast))
						{
							result.EndFIndex = StrokeFIndices.AfterLast;
							if (beginNode.IsEmpty)
							{
								result.BeginFIndex = StrokeFIndices.BeforeFirst;
								break;
							}
							if (DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.BeforeFirst))
							{
								break;
							}
						}
					}
					if (!beginNode.IsEmpty && (!flag2 || !DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.BeforeFirst)) && StrokeNodeOperations.GetNearest(vector2 - vector, vector3 - vector).LengthSquared < num2)
					{
						flag2 = true;
						if (!DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.BeforeFirst))
						{
							result.BeginFIndex = StrokeFIndices.BeforeFirst;
							if (DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.AfterLast))
							{
								break;
							}
						}
					}
					if (beginNode.IsEmpty || (!flag2 && (quad.IsEmpty || !StrokeNodeOperations.HitTestQuadSegment(quad, contourSegment.Begin, contourSegment.End))))
					{
						if (flag && StrokeNodeOperations.WhereIsVectorAboutVector(endNode.Position - contourSegment.Begin, contourSegment.Vector) != StrokeNodeOperations.HitResult.Right)
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
						this.CalculateCutLocations(vector, vector2, vector3, num3, num, ref result);
						if (result.IsFull)
						{
							break;
						}
					}
				}
			}
			if (!result.IsFull)
			{
				if (flag)
				{
					result = StrokeFIndices.Full;
				}
				else if (DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.BeforeFirst) && !DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.AfterLast))
				{
					result.EndFIndex = StrokeFIndices.AfterLast;
				}
				else if (DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.AfterLast) && !DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.BeforeFirst))
				{
					result.BeginFIndex = StrokeFIndices.BeforeFirst;
				}
			}
			if (base.IsInvalidCutTestResult(result))
			{
				return StrokeFIndices.Empty;
			}
			return result;
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0014E48C File Offset: 0x0014D88C
		private static double ClipTest(Vector spineVector, double beginRadius, double endRadius, Vector hitBegin, Vector hitEnd)
		{
			if (DoubleUtil.IsZero(spineVector.X) && DoubleUtil.IsZero(spineVector.Y))
			{
				Vector nearest = StrokeNodeOperations.GetNearest(hitBegin, hitEnd);
				double num;
				if (nearest.X == 0.0)
				{
					num = Math.Abs(nearest.Y);
				}
				else if (nearest.Y == 0.0)
				{
					num = Math.Abs(nearest.X);
				}
				else
				{
					num = nearest.Length;
				}
				return StrokeNodeOperations.AdjustFIndex((num - beginRadius) / (endRadius - beginRadius));
			}
			if (DoubleUtil.AreClose(hitBegin, hitEnd))
			{
				return EllipticalNodeOperations.ClipTest(spineVector, beginRadius, endRadius, hitBegin);
			}
			Vector vector = hitEnd - hitBegin;
			double num2;
			if (DoubleUtil.IsZero(Vector.Determinant(spineVector, vector)))
			{
				num2 = EllipticalNodeOperations.ClipTest(spineVector, beginRadius, endRadius, StrokeNodeOperations.GetNearest(hitBegin, hitEnd));
			}
			else
			{
				double projectionFIndex = StrokeNodeOperations.GetProjectionFIndex(hitBegin, hitEnd);
				Vector vector2 = hitBegin + vector * projectionFIndex;
				if (vector2.LengthSquared < beginRadius * beginRadius)
				{
					num2 = EllipticalNodeOperations.ClipTest(spineVector, beginRadius, endRadius, (0.0 > projectionFIndex) ? hitBegin : hitEnd);
				}
				else
				{
					Vector vector3 = spineVector + StrokeNodeOperations.GetProjection(-spineVector, vector2 - spineVector);
					if (DoubleUtil.IsZero(vector3.LengthSquared) || DoubleUtil.IsZero(endRadius - beginRadius + vector3.Length))
					{
						return 1.0;
					}
					num2 = (vector2.Length - beginRadius) / (endRadius - beginRadius + vector3.Length);
					Vector vector4 = spineVector * num2;
					double projectionFIndex2 = StrokeNodeOperations.GetProjectionFIndex(hitBegin - vector4, hitEnd - vector4);
					if (!DoubleUtil.IsBetweenZeroAndOne(projectionFIndex2))
					{
						num2 = EllipticalNodeOperations.ClipTest(spineVector, beginRadius, endRadius, (0.0 > projectionFIndex2) ? hitBegin : hitEnd);
					}
				}
			}
			return StrokeNodeOperations.AdjustFIndex(num2);
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0014E640 File Offset: 0x0014DA40
		private static double ClipTest(Vector spine, double beginRadius, double endRadius, Vector hit)
		{
			double num = endRadius - beginRadius;
			double num2 = spine.X * spine.X + spine.Y * spine.Y - num * num;
			double num3 = -2.0 * (hit.X * spine.X + hit.Y * spine.Y + beginRadius * num);
			double num4 = hit.X * hit.X + hit.Y * hit.Y - beginRadius * beginRadius;
			if (DoubleUtil.IsZero(num2) || !DoubleUtil.GreaterThanOrClose(num3 * num3, 4.0 * num2 * num4))
			{
				return 1.0;
			}
			double num5 = Math.Sqrt(num3 * num3 - 4.0 * num2 * num4);
			double num6 = (-num3 + num5) / (2.0 * num2);
			double num7 = (-num3 - num5) / (2.0 * num2);
			double findex;
			if (DoubleUtil.IsBetweenZeroAndOne(num6) && DoubleUtil.IsBetweenZeroAndOne(num6))
			{
				findex = Math.Min(num6, num7);
			}
			else if (DoubleUtil.IsBetweenZeroAndOne(num6))
			{
				findex = num6;
			}
			else if (DoubleUtil.IsBetweenZeroAndOne(num7))
			{
				findex = num7;
			}
			else if (num6 > 1.0 && num7 > 1.0)
			{
				findex = 1.0;
			}
			else if (num6 < 0.0 && num7 < 0.0)
			{
				findex = 0.0;
			}
			else
			{
				findex = ((Math.Abs(Math.Min(num6, num7) - 0.0) < Math.Abs(Math.Max(num6, num7) - 1.0)) ? 0.0 : 1.0);
			}
			return StrokeNodeOperations.AdjustFIndex(findex);
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x0014E808 File Offset: 0x0014DC08
		private static StrokeNodeOperations.HitResult WhereIsNodeAboutSegment(Vector spine, Vector segBegin, Vector segEnd)
		{
			StrokeNodeOperations.HitResult result = StrokeNodeOperations.HitResult.Right;
			Vector vector = segEnd - segBegin;
			if (StrokeNodeOperations.WhereIsVectorAboutVector(-segBegin, vector) == StrokeNodeOperations.HitResult.Left && !DoubleUtil.IsZero(Vector.Determinant(spine, vector)))
			{
				result = StrokeNodeOperations.HitResult.Left;
			}
			return result;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0014E840 File Offset: 0x0014DC40
		private void CalculateCutLocations(Vector spineVector, Vector hitBegin, Vector hitEnd, double endRadius, double beginRadius, ref StrokeFIndices result)
		{
			if (!DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.AfterLast) && EllipticalNodeOperations.WhereIsNodeAboutSegment(spineVector, hitBegin, hitEnd) == StrokeNodeOperations.HitResult.Left)
			{
				double num = 1.0 - EllipticalNodeOperations.ClipTest(spineVector, endRadius, beginRadius, hitBegin, hitEnd);
				if (num > result.EndFIndex)
				{
					result.EndFIndex = num;
				}
			}
			if (!DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.BeforeFirst))
			{
				hitBegin -= spineVector;
				hitEnd -= spineVector;
				if (EllipticalNodeOperations.WhereIsNodeAboutSegment(-spineVector, hitBegin, hitEnd) == StrokeNodeOperations.HitResult.Left)
				{
					double num2 = EllipticalNodeOperations.ClipTest(-spineVector, beginRadius, endRadius, hitBegin, hitEnd);
					if (num2 < result.BeginFIndex)
					{
						result.BeginFIndex = num2;
					}
				}
			}
		}

		// Token: 0x040025A8 RID: 9640
		private double _radius;

		// Token: 0x040025A9 RID: 9641
		private Size _radii;

		// Token: 0x040025AA RID: 9642
		private Matrix _transform;

		// Token: 0x040025AB RID: 9643
		private Matrix _nodeShapeToCircle;

		// Token: 0x040025AC RID: 9644
		private Matrix _circleToNodeShape;
	}
}
