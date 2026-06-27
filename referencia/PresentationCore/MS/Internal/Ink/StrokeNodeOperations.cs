using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Ink;

namespace MS.Internal.Ink
{
	// Token: 0x020007C3 RID: 1987
	internal class StrokeNodeOperations
	{
		// Token: 0x060053BF RID: 21439 RVA: 0x00151504 File Offset: 0x00150904
		internal static StrokeNodeOperations CreateInstance(StylusShape nodeShape)
		{
			if (nodeShape == null)
			{
				throw new ArgumentNullException("nodeShape");
			}
			if (nodeShape.IsEllipse)
			{
				return new EllipticalNodeOperations(nodeShape);
			}
			return new StrokeNodeOperations(nodeShape);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x00151534 File Offset: 0x00150934
		internal StrokeNodeOperations(StylusShape nodeShape)
		{
			this._vertices = nodeShape.GetVerticesAsVectors();
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060053C1 RID: 21441 RVA: 0x00151560 File Offset: 0x00150960
		internal virtual bool IsNodeShapeEllipse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00151570 File Offset: 0x00150970
		internal Rect GetNodeBounds(StrokeNodeData node)
		{
			if (this._shapeBounds.IsEmpty)
			{
				int num = 0;
				while (num + 1 < this._vertices.Length)
				{
					this._shapeBounds.Union(new Rect((Point)this._vertices[num], (Point)this._vertices[num + 1]));
					num += 2;
				}
				if (num < this._vertices.Length)
				{
					this._shapeBounds.Union((Point)this._vertices[num]);
				}
			}
			Rect shapeBounds = this._shapeBounds;
			double num2 = (double)node.PressureFactor;
			if (!DoubleUtil.AreClose(num2, 1.0))
			{
				shapeBounds = new Rect(this._shapeBounds.X * num2, this._shapeBounds.Y * num2, this._shapeBounds.Width * num2, this._shapeBounds.Height * num2);
			}
			shapeBounds.Location += (Vector)node.Position;
			return shapeBounds;
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00151678 File Offset: 0x00150A78
		internal void GetNodeContourPoints(StrokeNodeData node, List<Point> pointBuffer)
		{
			double num = (double)node.PressureFactor;
			if (DoubleUtil.AreClose(num, 1.0))
			{
				for (int i = 0; i < this._vertices.Length; i++)
				{
					pointBuffer.Add(node.Position + this._vertices[i]);
				}
				return;
			}
			for (int j = 0; j < this._vertices.Length; j++)
			{
				pointBuffer.Add(node.Position + this._vertices[j] * num);
			}
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00151708 File Offset: 0x00150B08
		internal virtual IEnumerable<ContourSegment> GetContourSegments(StrokeNodeData node, Quad quad)
		{
			if (quad.IsEmpty)
			{
				Point begin = node.Position + this._vertices[this._vertices.Length - 1] * (double)node.PressureFactor;
				int num;
				for (int i = 0; i < this._vertices.Length; i = num + 1)
				{
					Point nextVertex = node.Position + this._vertices[i] * (double)node.PressureFactor;
					yield return new ContourSegment(begin, nextVertex);
					begin = nextVertex;
					nextVertex = default(Point);
					num = i;
				}
			}
			else
			{
				yield return new ContourSegment(quad.A, quad.B);
				int i = 0;
				int count = this._vertices.Length;
				while (i < count)
				{
					Point point = node.Position + this._vertices[i] * (double)node.PressureFactor;
					int num;
					if (point == quad.B)
					{
						for (int j = 0; j < count; j = num + 1)
						{
							if (!(point != quad.C))
							{
								break;
							}
							i = (i + 1) % count;
							Point nextVertex = node.Position + this._vertices[i] * (double)node.PressureFactor;
							yield return new ContourSegment(point, nextVertex);
							point = nextVertex;
							nextVertex = default(Point);
							num = j;
						}
						break;
					}
					num = i;
					i = num + 1;
				}
				yield return new ContourSegment(quad.C, quad.D);
				yield return new ContourSegment(quad.D, quad.A);
			}
			yield break;
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x00151734 File Offset: 0x00150B34
		internal virtual IEnumerable<ContourSegment> GetNonBezierContourSegments(StrokeNodeData beginNode, StrokeNodeData endNode)
		{
			Quad quad = beginNode.IsEmpty ? Quad.Empty : this.GetConnectingQuad(beginNode, endNode);
			return this.GetContourSegments(endNode, quad);
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00151764 File Offset: 0x00150B64
		internal virtual Quad GetConnectingQuad(StrokeNodeData beginNode, StrokeNodeData endNode)
		{
			if (beginNode.IsEmpty || endNode.IsEmpty || DoubleUtil.AreClose(beginNode.Position, endNode.Position))
			{
				return Quad.Empty;
			}
			Quad empty = Quad.Empty;
			bool flag = false;
			bool flag2 = false;
			Vector vector = endNode.Position - beginNode.Position;
			double num = (double)(endNode.PressureFactor - beginNode.PressureFactor);
			int num2 = this._vertices.Length;
			int i = 0;
			int num3 = num2 - 1;
			while (i < num2)
			{
				Vector vector2 = vector + this._vertices[i] * num;
				if (num != 0.0 && vector2.X == 0.0 && vector2.Y == 0.0)
				{
					return Quad.Empty;
				}
				StrokeNodeOperations.HitResult hitResult = StrokeNodeOperations.WhereIsVectorAboutVector(vector2, this._vertices[(i + 1) % num2] - this._vertices[i]);
				if (hitResult == StrokeNodeOperations.HitResult.Left)
				{
					if (!flag)
					{
						StrokeNodeOperations.HitResult hitResult2 = StrokeNodeOperations.WhereIsVectorAboutVector(this._vertices[i] - this._vertices[num3], vector2);
						if (StrokeNodeOperations.HitResult.Right != hitResult2)
						{
							flag = true;
							empty.A = beginNode.Position + this._vertices[i] * (double)beginNode.PressureFactor;
							empty.B = endNode.Position + this._vertices[i] * (double)endNode.PressureFactor;
							if (flag2)
							{
								break;
							}
						}
					}
				}
				else if (!flag2)
				{
					StrokeNodeOperations.HitResult hitResult3 = StrokeNodeOperations.WhereIsVectorAboutVector(this._vertices[i] - this._vertices[num3], vector2);
					if (StrokeNodeOperations.HitResult.Right == hitResult3)
					{
						flag2 = true;
						empty.C = endNode.Position + this._vertices[i] * (double)endNode.PressureFactor;
						empty.D = beginNode.Position + this._vertices[i] * (double)beginNode.PressureFactor;
						if (flag)
						{
							break;
						}
					}
				}
				i++;
				num3 = (num3 + 1) % num2;
			}
			if (!flag || !flag2 || (num != 0.0 && Vector.Determinant(empty.B - empty.A, empty.D - empty.A) == 0.0))
			{
				return Quad.Empty;
			}
			return empty;
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x001519F8 File Offset: 0x00150DF8
		internal virtual bool HitTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, Point hitBeginPoint, Point hitEndPoint)
		{
			if (quad.IsEmpty)
			{
				Point position;
				double num;
				if (beginNode.IsEmpty || endNode.PressureFactor > beginNode.PressureFactor)
				{
					position = endNode.Position;
					num = (double)endNode.PressureFactor;
				}
				else
				{
					position = beginNode.Position;
					num = (double)beginNode.PressureFactor;
				}
				Vector vector = hitBeginPoint - position;
				Vector vector2 = hitEndPoint - position;
				if (num != 1.0)
				{
					vector /= num;
					vector2 /= num;
				}
				return StrokeNodeOperations.HitTestPolygonSegment(this._vertices, vector, vector2);
			}
			Vector vector3 = hitBeginPoint - beginNode.Position;
			Vector vector4 = hitEndPoint - beginNode.Position;
			StrokeNodeOperations.HitResult hitResult = StrokeNodeOperations.WhereIsSegmentAboutSegment(vector3, vector4, quad.C - beginNode.Position, quad.D - beginNode.Position);
			if (StrokeNodeOperations.HitResult.Left == hitResult)
			{
				return false;
			}
			StrokeNodeOperations.HitResult hitResult2 = hitResult;
			StrokeNodeOperations.HitResult prevHitResult = hitResult;
			double num2 = (double)beginNode.PressureFactor;
			int num3 = this._vertices.Length;
			Vector vector5 = default(Vector);
			int i;
			for (i = 0; i < num3; i++)
			{
				vector5 = this._vertices[i] * num2;
				if (beginNode.Position + vector5 == quad.D)
				{
					break;
				}
			}
			for (int j = 0; j < 2; j++)
			{
				Point point = (j == 0) ? beginNode.Position : endNode.Position;
				Point point2 = (j == 0) ? quad.A : quad.C;
				num3 = this._vertices.Length;
				while (point + vector5 != point2 && num3 != 0)
				{
					i = (i + 1) % this._vertices.Length;
					Vector vector6 = (num2 == 1.0) ? this._vertices[i] : (this._vertices[i] * num2);
					hitResult = StrokeNodeOperations.WhereIsSegmentAboutSegment(vector3, vector4, vector5, vector6);
					if (hitResult == StrokeNodeOperations.HitResult.Hit)
					{
						return true;
					}
					if (StrokeNodeOperations.IsOutside(hitResult, prevHitResult))
					{
						return false;
					}
					prevHitResult = hitResult;
					vector5 = vector6;
					num3--;
				}
				if (j == 0)
				{
					num2 = (double)endNode.PressureFactor;
					Vector vector7 = endNode.Position - beginNode.Position;
					vector5 -= vector7;
					vector3 -= vector7;
					vector4 -= vector7;
					num3 = this._vertices.Length;
					while (endNode.Position + this._vertices[i] * num2 != quad.B && num3 != 0)
					{
						i = (i + 1) % this._vertices.Length;
						num3--;
					}
					i--;
				}
			}
			return !StrokeNodeOperations.IsOutside(hitResult2, hitResult);
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x00151CB8 File Offset: 0x001510B8
		internal virtual bool HitTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, IEnumerable<ContourSegment> hitContour)
		{
			if (quad.IsEmpty)
			{
				return this.HitTestPolygonContourSegments(hitContour, beginNode, endNode);
			}
			return this.HitTestInkContour(hitContour, quad, beginNode, endNode);
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00151CE4 File Offset: 0x001510E4
		internal virtual StrokeFIndices CutTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, Point hitBeginPoint, Point hitEndPoint)
		{
			StrokeFIndices empty = StrokeFIndices.Empty;
			for (int i = beginNode.IsEmpty ? 1 : 0; i < 2; i++)
			{
				Point point = (i == 0) ? beginNode.Position : endNode.Position;
				double num = (double)((i == 0) ? beginNode.PressureFactor : endNode.PressureFactor);
				Vector vector = hitBeginPoint - point;
				Vector vector2 = hitEndPoint - point;
				if (num != 1.0)
				{
					vector /= num;
					vector2 /= num;
				}
				if (StrokeNodeOperations.HitTestPolygonSegment(this._vertices, vector, vector2))
				{
					if (i == 0)
					{
						empty.BeginFIndex = StrokeFIndices.BeforeFirst;
						empty.EndFIndex = 0.0;
					}
					else
					{
						empty.EndFIndex = StrokeFIndices.AfterLast;
						if (beginNode.IsEmpty)
						{
							empty.BeginFIndex = StrokeFIndices.BeforeFirst;
						}
						else if (empty.BeginFIndex != StrokeFIndices.BeforeFirst)
						{
							empty.BeginFIndex = 1.0;
						}
					}
				}
			}
			if (empty.IsFull)
			{
				return empty;
			}
			if (empty.IsEmpty && (quad.IsEmpty || !StrokeNodeOperations.HitTestQuadSegment(quad, hitBeginPoint, hitEndPoint)))
			{
				return empty;
			}
			if (empty.BeginFIndex != StrokeFIndices.BeforeFirst)
			{
				empty.BeginFIndex = this.ClipTest((endNode.Position - beginNode.Position) / (double)beginNode.PressureFactor, (double)(endNode.PressureFactor / beginNode.PressureFactor - 1f), (hitBeginPoint - beginNode.Position) / (double)beginNode.PressureFactor, (hitEndPoint - beginNode.Position) / (double)beginNode.PressureFactor);
			}
			if (empty.EndFIndex != StrokeFIndices.AfterLast)
			{
				empty.EndFIndex = 1.0 - this.ClipTest((beginNode.Position - endNode.Position) / (double)endNode.PressureFactor, (double)(beginNode.PressureFactor / endNode.PressureFactor - 1f), (hitBeginPoint - endNode.Position) / (double)endNode.PressureFactor, (hitEndPoint - endNode.Position) / (double)endNode.PressureFactor);
			}
			if (this.IsInvalidCutTestResult(empty))
			{
				return StrokeFIndices.Empty;
			}
			return empty;
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x00151F40 File Offset: 0x00151340
		internal virtual StrokeFIndices CutTest(StrokeNodeData beginNode, StrokeNodeData endNode, Quad quad, IEnumerable<ContourSegment> hitContour)
		{
			if (!beginNode.IsEmpty)
			{
				StrokeFIndices empty = StrokeFIndices.Empty;
				bool flag = true;
				Vector spineVector = (endNode.Position - beginNode.Position) / (double)beginNode.PressureFactor;
				Vector spineVector2 = (beginNode.Position - endNode.Position) / (double)endNode.PressureFactor;
				double pressureDelta = (double)(endNode.PressureFactor / beginNode.PressureFactor - 1f);
				double pressureDelta2 = (double)(beginNode.PressureFactor / endNode.PressureFactor - 1f);
				foreach (ContourSegment hitSegment in hitContour)
				{
					bool flag2 = this.HitTestStrokeNodes(hitSegment, beginNode, endNode, ref empty);
					if (empty.IsFull)
					{
						return empty;
					}
					if (!flag2)
					{
						if (!quad.IsEmpty)
						{
							flag2 = (hitSegment.IsArc ? StrokeNodeOperations.HitTestQuadCircle(quad, hitSegment.Begin + hitSegment.Radius, hitSegment.Radius) : StrokeNodeOperations.HitTestQuadSegment(quad, hitSegment.Begin, hitSegment.End));
						}
						if (!flag2)
						{
							if (flag)
							{
								flag = (hitSegment.IsArc ? (StrokeNodeOperations.WhereIsVectorAboutArc(endNode.Position - hitSegment.Begin - hitSegment.Radius, -hitSegment.Radius, hitSegment.Vector - hitSegment.Radius) > StrokeNodeOperations.HitResult.Hit) : (StrokeNodeOperations.WhereIsVectorAboutVector(endNode.Position - hitSegment.Begin, hitSegment.Vector) == StrokeNodeOperations.HitResult.Right));
								continue;
							}
							continue;
						}
					}
					flag = false;
					if (!DoubleUtil.AreClose(empty.BeginFIndex, StrokeFIndices.BeforeFirst))
					{
						double num = this.CalculateClipLocation(hitSegment, beginNode, spineVector, pressureDelta);
						if (num != StrokeFIndices.BeforeFirst && empty.BeginFIndex > num)
						{
							empty.BeginFIndex = num;
						}
					}
					if (!DoubleUtil.AreClose(empty.EndFIndex, StrokeFIndices.AfterLast))
					{
						double num2 = this.CalculateClipLocation(hitSegment, endNode, spineVector2, pressureDelta2);
						if (num2 != StrokeFIndices.BeforeFirst)
						{
							num2 = 1.0 - num2;
							if (empty.EndFIndex < num2)
							{
								empty.EndFIndex = num2;
							}
						}
					}
				}
				if (DoubleUtil.AreClose(empty.BeginFIndex, StrokeFIndices.AfterLast))
				{
					if (!DoubleUtil.AreClose(empty.EndFIndex, StrokeFIndices.BeforeFirst))
					{
						empty.BeginFIndex = StrokeFIndices.BeforeFirst;
					}
				}
				else if (DoubleUtil.AreClose(empty.EndFIndex, StrokeFIndices.BeforeFirst))
				{
					empty.EndFIndex = StrokeFIndices.AfterLast;
				}
				if (this.IsInvalidCutTestResult(empty))
				{
					return StrokeFIndices.Empty;
				}
				if (!empty.IsEmpty || !flag)
				{
					return empty;
				}
				return StrokeFIndices.Full;
			}
			if (this.HitTest(beginNode, endNode, quad, hitContour))
			{
				return StrokeFIndices.Full;
			}
			return StrokeFIndices.Empty;
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x00152228 File Offset: 0x00151628
		private double ClipTest(Vector spineVector, double pressureDelta, Vector hitBegin, Vector hitEnd)
		{
			double num = StrokeFIndices.AfterLast;
			Vector vector = hitEnd - hitBegin;
			Vector vector2 = this._vertices[this._vertices.Length - 1];
			Vector vector3 = spineVector + vector2 * pressureDelta;
			bool flag = false;
			int num2 = 0;
			int num3 = this._vertices.Length;
			while (num2 < num3 || (num2 == num3 && flag))
			{
				Vector vector4 = this._vertices[num2 % num3];
				Vector vector5 = vector4 - vector2;
				Vector vector6 = spineVector + vector4 * pressureDelta;
				if ((DoubleUtil.IsZero(vector3.X) && DoubleUtil.IsZero(vector3.Y)) || (!flag && StrokeNodeOperations.HitResult.Left != StrokeNodeOperations.WhereIsVectorAboutVector(vector3, vector5)))
				{
					vector2 = vector4;
					vector3 = vector6;
				}
				else
				{
					flag = false;
					StrokeNodeOperations.HitResult hitResult = StrokeNodeOperations.HitResult.Left;
					int num4 = 0;
					for (int i = 0; i < 2; i++)
					{
						Vector vector7 = ((i == 0) ? hitBegin : hitEnd) - vector2;
						hitResult = StrokeNodeOperations.WhereIsVectorAboutVector(vector7, vector3);
						if (hitResult == StrokeNodeOperations.HitResult.Hit)
						{
							double num5 = (Math.Abs(vector3.X) < Math.Abs(vector3.Y)) ? (vector7.Y / vector3.Y) : (vector7.X / vector3.X);
							if (num > num5 && DoubleUtil.IsBetweenZeroAndOne(num5))
							{
								num = num5;
							}
						}
						else if (hitResult == StrokeNodeOperations.HitResult.Right)
						{
							num4++;
							if (StrokeNodeOperations.HitResult.Left == StrokeNodeOperations.WhereIsVectorAboutVector(vector7 - vector5, vector6))
							{
								double positionBetweenLines = StrokeNodeOperations.GetPositionBetweenLines(vector5, vector3, vector7);
								if (num > positionBetweenLines && DoubleUtil.IsBetweenZeroAndOne(positionBetweenLines))
								{
									num = positionBetweenLines;
								}
							}
							else
							{
								flag = true;
							}
						}
						else
						{
							num4--;
						}
					}
					if (num4 == 0)
					{
						if (hitResult == StrokeNodeOperations.HitResult.Hit)
						{
							break;
						}
						double num6 = -Vector.Determinant(vector3, vector);
						if (!DoubleUtil.IsZero(num6))
						{
							double num7 = Vector.Determinant(vector, hitBegin - vector2) / num6;
							if (num > num7 && DoubleUtil.IsBetweenZeroAndOne(num7))
							{
								num = num7;
							}
						}
					}
					vector2 = vector4;
					vector3 = vector6;
				}
				num2++;
			}
			return StrokeNodeOperations.AdjustFIndex(num);
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x00152418 File Offset: 0x00151818
		private double ClipTestArc(Vector spineVector, double pressureDelta, Vector hitCenter, Vector hitRadius)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x0015242C File Offset: 0x0015182C
		internal Vector[] GetVertices()
		{
			return this._vertices;
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00152440 File Offset: 0x00151840
		private bool HitTestPolygonContourSegments(IEnumerable<ContourSegment> hitContour, StrokeNodeData beginNode, StrokeNodeData endNode)
		{
			bool flag = false;
			bool flag2 = true;
			Point position;
			double num;
			if (beginNode.IsEmpty || endNode.PressureFactor > beginNode.PressureFactor)
			{
				position = endNode.Position;
				num = (double)endNode.PressureFactor;
			}
			else
			{
				position = beginNode.Position;
				num = (double)beginNode.PressureFactor;
			}
			foreach (ContourSegment contourSegment in hitContour)
			{
				if (contourSegment.IsArc)
				{
					Vector vector = contourSegment.Begin + contourSegment.Radius - position;
					Vector vector2 = contourSegment.Radius;
					if (!DoubleUtil.AreClose(num, 1.0))
					{
						vector /= num;
						vector2 /= num;
					}
					if (StrokeNodeOperations.HitTestPolygonCircle(this._vertices, vector, vector2))
					{
						flag = true;
						break;
					}
					if (flag2 && StrokeNodeOperations.WhereIsVectorAboutArc(position - contourSegment.Begin - contourSegment.Radius, -contourSegment.Radius, contourSegment.Vector - contourSegment.Radius) == StrokeNodeOperations.HitResult.Hit)
					{
						flag2 = false;
					}
				}
				else
				{
					Vector vector3 = contourSegment.Begin - position;
					Vector vector4 = vector3 + contourSegment.Vector;
					if (!DoubleUtil.AreClose(num, 1.0))
					{
						vector3 /= num;
						vector4 /= num;
					}
					if (StrokeNodeOperations.HitTestPolygonSegment(this._vertices, vector3, vector4))
					{
						flag = true;
						break;
					}
					if (flag2 && StrokeNodeOperations.WhereIsVectorAboutVector(position - contourSegment.Begin, contourSegment.Vector) != StrokeNodeOperations.HitResult.Right)
					{
						flag2 = false;
					}
				}
			}
			return flag2 || flag;
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x00152614 File Offset: 0x00151A14
		private bool HitTestInkContour(IEnumerable<ContourSegment> hitContour, Quad quad, StrokeNodeData beginNode, StrokeNodeData endNode)
		{
			bool flag = false;
			bool flag2 = true;
			foreach (ContourSegment contourSegment in hitContour)
			{
				Vector vector;
				Vector vector2;
				StrokeNodeOperations.HitResult hitResult;
				if (contourSegment.IsArc)
				{
					vector = contourSegment.Begin + contourSegment.Radius - beginNode.Position;
					vector2 = contourSegment.Radius;
					hitResult = StrokeNodeOperations.WhereIsCircleAboutSegment(vector, vector2, quad.C - beginNode.Position, quad.D - beginNode.Position);
				}
				else
				{
					vector = contourSegment.Begin - beginNode.Position;
					vector2 = vector + contourSegment.Vector;
					hitResult = StrokeNodeOperations.WhereIsSegmentAboutSegment(vector, vector2, quad.C - beginNode.Position, quad.D - beginNode.Position);
				}
				if (StrokeNodeOperations.HitResult.Left == hitResult)
				{
					if (flag2)
					{
						flag2 = (contourSegment.IsArc ? (StrokeNodeOperations.WhereIsVectorAboutArc(-vector, -contourSegment.Radius, contourSegment.Vector - contourSegment.Radius) > StrokeNodeOperations.HitResult.Hit) : (StrokeNodeOperations.WhereIsVectorAboutVector(-vector, contourSegment.Vector) == StrokeNodeOperations.HitResult.Right));
					}
				}
				else
				{
					StrokeNodeOperations.HitResult hitResult2 = hitResult;
					StrokeNodeOperations.HitResult prevHitResult = hitResult;
					double scalar = (double)beginNode.PressureFactor;
					int num = this._vertices.Length;
					Vector vector3 = default(Vector);
					int i;
					for (i = 0; i < num; i++)
					{
						vector3 = this._vertices[i] * scalar;
						if (DoubleUtil.AreClose(beginNode.Position + vector3, quad.D))
						{
							break;
						}
					}
					int j;
					for (j = 0; j < 2; j++)
					{
						num = this._vertices.Length;
						Point point = (j == 0) ? beginNode.Position : endNode.Position;
						Point point2 = (j == 0) ? quad.A : quad.C;
						while (point + vector3 != point2 && num != 0)
						{
							i = (i + 1) % this._vertices.Length;
							Vector vector4 = this._vertices[i] * scalar;
							hitResult = (contourSegment.IsArc ? StrokeNodeOperations.WhereIsCircleAboutSegment(vector, vector2, vector3, vector4) : StrokeNodeOperations.WhereIsSegmentAboutSegment(vector, vector2, vector3, vector4));
							if (hitResult == StrokeNodeOperations.HitResult.Hit)
							{
								return true;
							}
							if (StrokeNodeOperations.IsOutside(hitResult, prevHitResult))
							{
								j = 3;
								break;
							}
							prevHitResult = hitResult;
							vector3 = vector4;
							num--;
						}
						if (j == 0)
						{
							scalar = (double)endNode.PressureFactor;
							Vector vector5 = endNode.Position - beginNode.Position;
							vector3 -= vector5;
							vector -= vector5;
							if (!contourSegment.IsArc)
							{
								vector2 -= vector5;
							}
							num = this._vertices.Length;
							while (!DoubleUtil.AreClose(endNode.Position + this._vertices[i] * scalar, quad.B) && num != 0)
							{
								i = (i + 1) % this._vertices.Length;
								num--;
							}
							i--;
						}
					}
					if (j == 2 && !StrokeNodeOperations.IsOutside(hitResult2, hitResult))
					{
						flag = true;
						break;
					}
					if (flag2)
					{
						flag2 = (contourSegment.IsArc ? (StrokeNodeOperations.WhereIsVectorAboutArc(-vector, -contourSegment.Radius, contourSegment.Vector - contourSegment.Radius) > StrokeNodeOperations.HitResult.Hit) : (StrokeNodeOperations.WhereIsVectorAboutVector(-vector, contourSegment.Vector) == StrokeNodeOperations.HitResult.Right));
					}
				}
			}
			return flag || flag2;
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x001529C4 File Offset: 0x00151DC4
		private bool HitTestStrokeNodes(ContourSegment hitSegment, StrokeNodeData beginNode, StrokeNodeData endNode, ref StrokeFIndices result)
		{
			bool flag = false;
			int i = 0;
			while (i < 2)
			{
				double num;
				if (i == 0)
				{
					if (!flag || !DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.BeforeFirst))
					{
						Point position = beginNode.Position;
						num = (double)beginNode.PressureFactor;
						goto IL_67;
					}
				}
				else if (!flag || !DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.AfterLast))
				{
					Point position = endNode.Position;
					num = (double)endNode.PressureFactor;
					goto IL_67;
				}
				IL_152:
				i++;
				continue;
				IL_67:
				Vector vector;
				Vector vector2;
				if (hitSegment.IsArc)
				{
					Point position;
					vector = hitSegment.Begin - position + hitSegment.Radius;
					vector2 = hitSegment.Radius;
				}
				else
				{
					Point position;
					vector = hitSegment.Begin - position;
					vector2 = vector + hitSegment.Vector;
				}
				if (num != 1.0)
				{
					vector /= num;
					vector2 /= num;
				}
				if (!(hitSegment.IsArc ? StrokeNodeOperations.HitTestPolygonCircle(this._vertices, vector, vector2) : StrokeNodeOperations.HitTestPolygonSegment(this._vertices, vector, vector2)))
				{
					goto IL_152;
				}
				flag = true;
				if (i == 0)
				{
					result.BeginFIndex = StrokeFIndices.BeforeFirst;
					if (DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.AfterLast))
					{
						break;
					}
					goto IL_152;
				}
				else
				{
					result.EndFIndex = StrokeFIndices.AfterLast;
					if (beginNode.IsEmpty)
					{
						result.BeginFIndex = StrokeFIndices.BeforeFirst;
						break;
					}
					if (!DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.BeforeFirst))
					{
						goto IL_152;
					}
					break;
				}
			}
			return flag;
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x00152B30 File Offset: 0x00151F30
		private double CalculateClipLocation(ContourSegment hitSegment, StrokeNodeData beginNode, Vector spineVector, double pressureDelta)
		{
			double num = StrokeFIndices.BeforeFirst;
			bool flag = hitSegment.IsArc || StrokeNodeOperations.WhereIsVectorAboutVector(beginNode.Position - hitSegment.Begin, hitSegment.Vector) == StrokeNodeOperations.HitResult.Left;
			if (flag)
			{
				num = (hitSegment.IsArc ? this.ClipTestArc(spineVector, pressureDelta, (hitSegment.Begin + hitSegment.Radius - beginNode.Position) / (double)beginNode.PressureFactor, hitSegment.Radius / (double)beginNode.PressureFactor) : this.ClipTest(spineVector, pressureDelta, (hitSegment.Begin - beginNode.Position) / (double)beginNode.PressureFactor, (hitSegment.End - beginNode.Position) / (double)beginNode.PressureFactor));
				if (num == StrokeFIndices.AfterLast)
				{
					num = StrokeFIndices.BeforeFirst;
				}
			}
			return num;
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x00152C28 File Offset: 0x00152028
		protected bool IsInvalidCutTestResult(StrokeFIndices result)
		{
			return DoubleUtil.AreClose(result.BeginFIndex, result.EndFIndex) || (DoubleUtil.AreClose(result.BeginFIndex, StrokeFIndices.BeforeFirst) && result.EndFIndex < 0.0) || (result.BeginFIndex > 1.0 && DoubleUtil.AreClose(result.EndFIndex, StrokeFIndices.AfterLast));
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x00152C98 File Offset: 0x00152098
		internal static bool HitTestPolygonSegment(Vector[] vertices, Vector hitBegin, Vector hitEnd)
		{
			StrokeNodeOperations.HitResult hitResult = StrokeNodeOperations.HitResult.Right;
			StrokeNodeOperations.HitResult hitResult2 = StrokeNodeOperations.HitResult.Right;
			StrokeNodeOperations.HitResult prevHitResult = StrokeNodeOperations.HitResult.Right;
			int num = vertices.Length;
			Vector orgBegin = vertices[num - 1];
			for (int i = 0; i < num; i++)
			{
				Vector vector = vertices[i];
				hitResult = StrokeNodeOperations.WhereIsSegmentAboutSegment(hitBegin, hitEnd, orgBegin, vector);
				if (hitResult == StrokeNodeOperations.HitResult.Hit)
				{
					return true;
				}
				if (StrokeNodeOperations.IsOutside(hitResult, prevHitResult))
				{
					return false;
				}
				if (i == 0)
				{
					hitResult2 = hitResult;
				}
				prevHitResult = hitResult;
				orgBegin = vector;
			}
			return !StrokeNodeOperations.IsOutside(hitResult2, hitResult);
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x00152D04 File Offset: 0x00152104
		internal static bool HitTestQuadSegment(Quad quad, Point hitBegin, Point hitEnd)
		{
			StrokeNodeOperations.HitResult hitResult = StrokeNodeOperations.HitResult.Right;
			StrokeNodeOperations.HitResult hitResult2 = StrokeNodeOperations.HitResult.Right;
			StrokeNodeOperations.HitResult prevHitResult = StrokeNodeOperations.HitResult.Right;
			int num = 4;
			Vector hitBegin2 = new Vector(0.0, 0.0);
			Vector hitEnd2 = hitEnd - hitBegin;
			Vector orgBegin = quad[num - 1] - hitBegin;
			for (int i = 0; i < num; i++)
			{
				Vector vector = quad[i] - hitBegin;
				hitResult = StrokeNodeOperations.WhereIsSegmentAboutSegment(hitBegin2, hitEnd2, orgBegin, vector);
				if (hitResult == StrokeNodeOperations.HitResult.Hit)
				{
					return true;
				}
				if (StrokeNodeOperations.IsOutside(hitResult, prevHitResult))
				{
					return false;
				}
				if (i == 0)
				{
					hitResult2 = hitResult;
				}
				prevHitResult = hitResult;
				orgBegin = vector;
			}
			return !StrokeNodeOperations.IsOutside(hitResult2, hitResult);
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x00152DA0 File Offset: 0x001521A0
		internal static bool HitTestPolygonCircle(Vector[] vertices, Vector center, Vector radius)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x00152DB4 File Offset: 0x001521B4
		internal static bool HitTestQuadCircle(Quad quad, Point center, Vector radius)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x00152DC8 File Offset: 0x001521C8
		internal static StrokeNodeOperations.HitResult WhereIsSegmentAboutSegment(Vector hitBegin, Vector hitEnd, Vector orgBegin, Vector orgEnd)
		{
			if (hitEnd == hitBegin)
			{
				return StrokeNodeOperations.WhereIsCircleAboutSegment(hitBegin, new Vector(0.0, 0.0), orgBegin, orgEnd);
			}
			StrokeNodeOperations.HitResult result = StrokeNodeOperations.HitResult.Right;
			Vector vector = orgEnd - orgBegin;
			Vector vector2 = orgBegin - hitBegin;
			Vector vector3 = hitEnd - hitBegin;
			double num = Vector.Determinant(vector, vector3);
			if (DoubleUtil.IsZero(num))
			{
				if (DoubleUtil.IsZero(Vector.Determinant(vector3, vector2)) || DoubleUtil.GreaterThan(Vector.Determinant(vector, vector2), 0.0))
				{
					result = StrokeNodeOperations.HitResult.Left;
				}
			}
			else
			{
				double num2 = StrokeNodeOperations.AdjustFIndex(Vector.Determinant(vector, vector2) / num);
				if (num2 > 0.0 && num2 < 1.0)
				{
					double num3 = StrokeNodeOperations.AdjustFIndex(Vector.Determinant(vector3, vector2) / num);
					if (num3 > 0.0 && num3 < 1.0)
					{
						result = StrokeNodeOperations.HitResult.Hit;
					}
					else
					{
						result = ((0.0 < num3) ? StrokeNodeOperations.HitResult.InFront : StrokeNodeOperations.HitResult.Behind);
					}
				}
				else if (StrokeNodeOperations.WhereIsVectorAboutVector(hitBegin - orgBegin, vector) == StrokeNodeOperations.HitResult.Left || StrokeNodeOperations.WhereIsVectorAboutVector(hitEnd - orgBegin, vector) == StrokeNodeOperations.HitResult.Left)
				{
					result = StrokeNodeOperations.HitResult.Left;
				}
			}
			return result;
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x00152EEC File Offset: 0x001522EC
		internal static StrokeNodeOperations.HitResult WhereIsCircleAboutSegment(Vector center, Vector radius, Vector segBegin, Vector segEnd)
		{
			segBegin -= center;
			segEnd -= center;
			double lengthSquared = radius.LengthSquared;
			double lengthSquared2 = StrokeNodeOperations.GetNearest(segBegin, segEnd).LengthSquared;
			if (lengthSquared > lengthSquared2)
			{
				return StrokeNodeOperations.HitResult.Hit;
			}
			Vector vector = segEnd - segBegin;
			StrokeNodeOperations.HitResult hitResult = StrokeNodeOperations.WhereIsVectorAboutVector(-segBegin, vector);
			StrokeNodeOperations.HitResult result;
			if (hitResult == StrokeNodeOperations.HitResult.Hit)
			{
				result = (DoubleUtil.LessThan(segBegin.LengthSquared, segEnd.LengthSquared) ? StrokeNodeOperations.HitResult.InFront : StrokeNodeOperations.HitResult.Behind);
			}
			else
			{
				double projectionFIndex = StrokeNodeOperations.GetProjectionFIndex(segBegin, segEnd);
				lengthSquared2 = (segBegin + vector * projectionFIndex).LengthSquared;
				if (lengthSquared <= lengthSquared2)
				{
					result = hitResult;
				}
				else
				{
					result = ((projectionFIndex > 0.0) ? StrokeNodeOperations.HitResult.InFront : StrokeNodeOperations.HitResult.Behind);
				}
			}
			return result;
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x00152FA0 File Offset: 0x001523A0
		internal static StrokeNodeOperations.HitResult WhereIsVectorAboutVector(Vector vector1, Vector vector2)
		{
			double num = Vector.Determinant(vector1, vector2);
			if (DoubleUtil.IsZero(num))
			{
				return StrokeNodeOperations.HitResult.Hit;
			}
			if (0.0 >= num)
			{
				return StrokeNodeOperations.HitResult.Right;
			}
			return StrokeNodeOperations.HitResult.Left;
		}

		// Token: 0x060053DA RID: 21466 RVA: 0x00152FD0 File Offset: 0x001523D0
		internal static StrokeNodeOperations.HitResult WhereIsVectorAboutArc(Vector hitVector, Vector arcBegin, Vector arcEnd)
		{
			if (arcBegin == arcEnd)
			{
				return StrokeNodeOperations.HitResult.Hit;
			}
			if (StrokeNodeOperations.HitResult.Right == StrokeNodeOperations.WhereIsVectorAboutVector(arcEnd, arcBegin))
			{
				if (StrokeNodeOperations.HitResult.Left != StrokeNodeOperations.WhereIsVectorAboutVector(hitVector, arcBegin) && StrokeNodeOperations.HitResult.Right != StrokeNodeOperations.WhereIsVectorAboutVector(hitVector, arcEnd))
				{
					return StrokeNodeOperations.HitResult.Hit;
				}
			}
			else if (StrokeNodeOperations.HitResult.Left != StrokeNodeOperations.WhereIsVectorAboutVector(hitVector, arcBegin) || StrokeNodeOperations.HitResult.Right != StrokeNodeOperations.WhereIsVectorAboutVector(hitVector, arcEnd))
			{
				return StrokeNodeOperations.HitResult.Hit;
			}
			if (StrokeNodeOperations.WhereIsVectorAboutVector(hitVector - arcBegin, StrokeNodeOperations.TurnLeft(arcBegin)) != StrokeNodeOperations.HitResult.Left || StrokeNodeOperations.WhereIsVectorAboutVector(hitVector - arcEnd, StrokeNodeOperations.TurnRight(arcEnd)) != StrokeNodeOperations.HitResult.Right)
			{
				return StrokeNodeOperations.HitResult.Left;
			}
			return StrokeNodeOperations.HitResult.Right;
		}

		// Token: 0x060053DB RID: 21467 RVA: 0x0015304C File Offset: 0x0015244C
		internal static Vector TurnLeft(Vector vector)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00153060 File Offset: 0x00152460
		internal static Vector TurnRight(Vector vector)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x00153074 File Offset: 0x00152474
		internal static bool IsOutside(StrokeNodeOperations.HitResult hitResult, StrokeNodeOperations.HitResult prevHitResult)
		{
			return StrokeNodeOperations.HitResult.Left == hitResult || (StrokeNodeOperations.HitResult.Behind == hitResult && StrokeNodeOperations.HitResult.InFront == prevHitResult);
		}

		// Token: 0x060053DE RID: 21470 RVA: 0x00153094 File Offset: 0x00152494
		internal static double GetPositionBetweenLines(Vector linesVector, Vector nextLine, Vector hitPoint)
		{
			Vector projection = StrokeNodeOperations.GetProjection(-hitPoint, linesVector - hitPoint);
			hitPoint = nextLine - hitPoint;
			Vector projection2 = StrokeNodeOperations.GetProjection(hitPoint, hitPoint + linesVector);
			Vector vector = projection - projection2;
			return Math.Sqrt(projection.LengthSquared / vector.LengthSquared);
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x001530E8 File Offset: 0x001524E8
		internal static double GetProjectionFIndex(Vector begin, Vector end)
		{
			Vector vector = end - begin;
			double lengthSquared = vector.LengthSquared;
			if (DoubleUtil.IsZero(lengthSquared))
			{
				return 0.0;
			}
			double num = -(begin * vector);
			return StrokeNodeOperations.AdjustFIndex(num / lengthSquared);
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x00153128 File Offset: 0x00152528
		internal static Vector GetProjection(Vector begin, Vector end)
		{
			double projectionFIndex = StrokeNodeOperations.GetProjectionFIndex(begin, end);
			return begin + (end - begin) * projectionFIndex;
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x00153150 File Offset: 0x00152550
		internal static Vector GetNearest(Vector begin, Vector end)
		{
			double projectionFIndex = StrokeNodeOperations.GetProjectionFIndex(begin, end);
			if (projectionFIndex <= 0.0)
			{
				return begin;
			}
			if (projectionFIndex >= 1.0)
			{
				return end;
			}
			return begin + (end - begin) * projectionFIndex;
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x00153194 File Offset: 0x00152594
		internal static double AdjustFIndex(double findex)
		{
			if (DoubleUtil.IsZero(findex))
			{
				return 0.0;
			}
			if (!DoubleUtil.IsOne(findex))
			{
				return findex;
			}
			return 1.0;
		}

		// Token: 0x040025CE RID: 9678
		private Rect _shapeBounds = Rect.Empty;

		// Token: 0x040025CF RID: 9679
		protected Vector[] _vertices;

		// Token: 0x02000A04 RID: 2564
		internal enum HitResult
		{
			// Token: 0x04002F35 RID: 12085
			Hit,
			// Token: 0x04002F36 RID: 12086
			Left,
			// Token: 0x04002F37 RID: 12087
			Right,
			// Token: 0x04002F38 RID: 12088
			InFront,
			// Token: 0x04002F39 RID: 12089
			Behind
		}
	}
}
