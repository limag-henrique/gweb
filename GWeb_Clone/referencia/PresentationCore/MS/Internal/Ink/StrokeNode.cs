using System;
using System.Collections.Generic;
using System.Windows;

namespace MS.Internal.Ink
{
	// Token: 0x020007C0 RID: 1984
	internal struct StrokeNode
	{
		// Token: 0x06005393 RID: 21395 RVA: 0x001500B4 File Offset: 0x0014F4B4
		internal StrokeNode(StrokeNodeOperations operations, int index, StrokeNodeData nodeData, StrokeNodeData lastNodeData, bool isLastNode)
		{
			this._operations = operations;
			this._index = index;
			this._thisNode = nodeData;
			this._lastNode = lastNodeData;
			this._isQuadCached = lastNodeData.IsEmpty;
			this._connectingQuad = Quad.Empty;
			this._isLastNode = isLastNode;
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06005394 RID: 21396 RVA: 0x00150100 File Offset: 0x0014F500
		internal Point Position
		{
			get
			{
				return this._thisNode.Position;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06005395 RID: 21397 RVA: 0x00150118 File Offset: 0x0014F518
		internal Point PreviousPosition
		{
			get
			{
				return this._lastNode.Position;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06005396 RID: 21398 RVA: 0x00150130 File Offset: 0x0014F530
		internal float PressureFactor
		{
			get
			{
				return this._thisNode.PressureFactor;
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06005397 RID: 21399 RVA: 0x00150148 File Offset: 0x0014F548
		internal float PreviousPressureFactor
		{
			get
			{
				return this._lastNode.PressureFactor;
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x00150160 File Offset: 0x0014F560
		internal bool IsEllipse
		{
			get
			{
				return this.IsValid && this._operations.IsNodeShapeEllipse;
			}
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06005399 RID: 21401 RVA: 0x00150184 File Offset: 0x0014F584
		internal bool IsLastNode
		{
			get
			{
				return this._isLastNode;
			}
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00150198 File Offset: 0x0014F598
		internal Rect GetBounds()
		{
			if (!this.IsValid)
			{
				return Rect.Empty;
			}
			return this._operations.GetNodeBounds(this._thisNode);
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x001501C4 File Offset: 0x0014F5C4
		internal Rect GetBoundsConnected()
		{
			if (!this.IsValid)
			{
				return Rect.Empty;
			}
			return Rect.Union(this._operations.GetNodeBounds(this._thisNode), this.ConnectingQuad.Bounds);
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x00150204 File Offset: 0x0014F604
		internal void GetContourPoints(List<Point> pointBuffer)
		{
			if (this.IsValid)
			{
				this._operations.GetNodeContourPoints(this._thisNode, pointBuffer);
			}
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0015022C File Offset: 0x0014F62C
		internal void GetPreviousContourPoints(List<Point> pointBuffer)
		{
			if (this.IsValid)
			{
				this._operations.GetNodeContourPoints(this._lastNode, pointBuffer);
			}
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x00150254 File Offset: 0x0014F654
		internal Quad GetConnectingQuad()
		{
			if (this.IsValid)
			{
				return this.ConnectingQuad;
			}
			return Quad.Empty;
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x00150278 File Offset: 0x0014F678
		internal void GetPointsAtStartOfSegment(List<Point> abPoints, List<Point> dcPoints)
		{
			if (this.IsValid)
			{
				Quad connectingQuad = this.ConnectingQuad;
				if (this.IsEllipse)
				{
					Rect nodeBounds = this._operations.GetNodeBounds(this._lastNode);
					abPoints.Add(connectingQuad.D);
					abPoints.Add(StrokeRenderer.ArcToMarker);
					abPoints.Add(new Point(nodeBounds.Width, nodeBounds.Height));
					abPoints.Add(connectingQuad.A);
					dcPoints.Add(connectingQuad.D);
					return;
				}
				Rect nodeBounds2 = this._operations.GetNodeBounds(this._thisNode);
				Vector[] vertices = this._operations.GetVertices();
				double scalar = (double)this._lastNode.PressureFactor;
				int num = vertices.Length * 2;
				int i = 0;
				bool flag = true;
				while (i < num)
				{
					Point point = this._lastNode.Position + vertices[i % vertices.Length] * scalar;
					if (point == connectingQuad.D)
					{
						if (!nodeBounds2.Contains(connectingQuad.D))
						{
							flag = false;
							abPoints.Add(connectingQuad.D);
							dcPoints.Add(connectingQuad.D);
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (i == num)
				{
					return;
				}
				i++;
				int num2 = 0;
				while (i < num && num2 < vertices.Length)
				{
					Point point2 = this._lastNode.Position + vertices[i % vertices.Length] * scalar;
					if (!nodeBounds2.Contains(point2))
					{
						abPoints.Add(point2);
					}
					if (flag)
					{
						flag = false;
						dcPoints.Add(point2);
					}
					if (point2 == connectingQuad.A)
					{
						break;
					}
					i++;
					num2++;
				}
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00150418 File Offset: 0x0014F818
		internal void GetPointsAtEndOfSegment(List<Point> abPoints, List<Point> dcPoints)
		{
			if (this.IsValid)
			{
				Quad connectingQuad = this.ConnectingQuad;
				if (this.IsEllipse)
				{
					Rect bounds = this.GetBounds();
					abPoints.Add(connectingQuad.B);
					abPoints.Add(StrokeRenderer.ArcToMarker);
					abPoints.Add(new Point(bounds.Width, bounds.Height));
					abPoints.Add(connectingQuad.C);
					return;
				}
				double scalar = (double)this._thisNode.PressureFactor;
				Vector[] vertices = this._operations.GetVertices();
				int num = vertices.Length * 2;
				int i;
				for (i = 0; i < num; i++)
				{
					Point point = this._thisNode.Position + vertices[i % vertices.Length] * scalar;
					if (point == connectingQuad.B)
					{
						abPoints.Add(connectingQuad.B);
						break;
					}
				}
				if (i == num)
				{
					return;
				}
				i++;
				int num2 = 0;
				while (i < num && num2 < vertices.Length)
				{
					Point point2 = this._thisNode.Position + vertices[i % vertices.Length] * scalar;
					if (point2 == connectingQuad.C)
					{
						break;
					}
					abPoints.Add(point2);
					i++;
					num2++;
				}
				dcPoints.Add(connectingQuad.C);
			}
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x00150560 File Offset: 0x0014F960
		internal void GetPointsAtMiddleSegment(StrokeNode previous, double angleBetweenNodes, List<Point> abPoints, List<Point> dcPoints, out bool missingIntersection)
		{
			missingIntersection = false;
			if (this.IsValid && previous.IsValid)
			{
				Quad connectingQuad = previous.ConnectingQuad;
				if (!connectingQuad.IsEmpty)
				{
					Quad connectingQuad2 = this.ConnectingQuad;
					if (!connectingQuad2.IsEmpty)
					{
						if (this.IsEllipse)
						{
							Rect nodeBounds = this._operations.GetNodeBounds(previous._lastNode);
							Rect nodeBounds2 = this._operations.GetNodeBounds(this._lastNode);
							Rect nodeBounds3 = this._operations.GetNodeBounds(this._thisNode);
							if (angleBetweenNodes == 0.0 || (connectingQuad.B == connectingQuad2.A && connectingQuad.C == connectingQuad2.D))
							{
								abPoints.Add(connectingQuad.B);
								dcPoints.Add(connectingQuad.C);
								return;
							}
							if (angleBetweenNodes > 0.0)
							{
								if (connectingQuad.B == connectingQuad2.A)
								{
									abPoints.Add(connectingQuad.B);
								}
								else
								{
									Point intersection = StrokeNode.GetIntersection(connectingQuad.A, connectingQuad.B, connectingQuad2.A, connectingQuad2.B);
									Rect rect = Rect.Union(nodeBounds, nodeBounds2);
									rect.Inflate(1.0, 1.0);
									if (!rect.Contains(intersection))
									{
										missingIntersection = true;
										return;
									}
									abPoints.Add(intersection);
								}
								if (connectingQuad.C == connectingQuad2.D)
								{
									dcPoints.Add(connectingQuad.C);
									return;
								}
								dcPoints.Add(connectingQuad.C);
								dcPoints.Add(new Point(nodeBounds2.Width, nodeBounds2.Height));
								dcPoints.Add(StrokeRenderer.ArcToMarker);
								dcPoints.Add(connectingQuad2.D);
								return;
							}
							else
							{
								if (connectingQuad.C == connectingQuad2.D)
								{
									dcPoints.Add(connectingQuad.C);
								}
								else
								{
									Point intersection2 = StrokeNode.GetIntersection(connectingQuad.D, connectingQuad.C, connectingQuad2.D, connectingQuad2.C);
									Rect rect2 = Rect.Union(nodeBounds, nodeBounds2);
									rect2.Inflate(1.0, 1.0);
									if (!rect2.Contains(intersection2))
									{
										missingIntersection = true;
										return;
									}
									dcPoints.Add(intersection2);
								}
								if (connectingQuad.B == connectingQuad2.A)
								{
									abPoints.Add(connectingQuad.B);
									return;
								}
								abPoints.Add(connectingQuad.B);
								abPoints.Add(StrokeRenderer.ArcToMarker);
								abPoints.Add(new Point(nodeBounds2.Width, nodeBounds2.Height));
								abPoints.Add(connectingQuad2.A);
								return;
							}
						}
						else
						{
							int num = -1;
							int num2 = -1;
							int num3 = -1;
							int num4 = -1;
							Vector[] vertices = this._operations.GetVertices();
							double scalar = (double)this._lastNode.PressureFactor;
							for (int i = 0; i < vertices.Length; i++)
							{
								Point point = this._lastNode.Position + vertices[i % vertices.Length] * scalar;
								if (point == connectingQuad2.A)
								{
									num = i;
								}
								if (point == connectingQuad.B)
								{
									num2 = i;
								}
								if (point == connectingQuad.C)
								{
									num3 = i;
								}
								if (point == connectingQuad2.D)
								{
									num4 = i;
								}
							}
							if (num == -1 || num2 == -1 || num3 == -1 || num4 == -1)
							{
								return;
							}
							Rect nodeBounds4 = this._operations.GetNodeBounds(this._thisNode);
							if (num == num2)
							{
								if (!nodeBounds4.Contains(connectingQuad.B))
								{
									abPoints.Add(connectingQuad.B);
								}
							}
							else if ((num == 0 && num2 == 3) || ((num != 3 || num2 != 0) && num > num2))
							{
								if (!nodeBounds4.Contains(connectingQuad.B))
								{
									abPoints.Add(connectingQuad.B);
								}
								if (!nodeBounds4.Contains(connectingQuad2.A))
								{
									abPoints.Add(connectingQuad2.A);
								}
							}
							else
							{
								Point intersection3 = StrokeNode.GetIntersection(connectingQuad.A, connectingQuad.B, connectingQuad2.A, connectingQuad2.B);
								Rect rect3 = Rect.Union(this._operations.GetNodeBounds(previous._lastNode), this._operations.GetNodeBounds(this._lastNode));
								rect3.Inflate(1.0, 1.0);
								if (!rect3.Contains(intersection3))
								{
									missingIntersection = true;
									return;
								}
								abPoints.Add(intersection3);
							}
							if (num3 == num4)
							{
								if (!nodeBounds4.Contains(connectingQuad.C))
								{
									dcPoints.Add(connectingQuad.C);
									return;
								}
							}
							else if ((num3 == 0 && num4 == 3) || ((num3 != 3 || num4 != 0) && num3 > num4))
							{
								if (!nodeBounds4.Contains(connectingQuad.C))
								{
									dcPoints.Add(connectingQuad.C);
								}
								if (!nodeBounds4.Contains(connectingQuad2.D))
								{
									dcPoints.Add(connectingQuad2.D);
									return;
								}
							}
							else
							{
								Point intersection4 = StrokeNode.GetIntersection(connectingQuad.D, connectingQuad.C, connectingQuad2.D, connectingQuad2.C);
								Rect rect4 = Rect.Union(this._operations.GetNodeBounds(previous._lastNode), this._operations.GetNodeBounds(this._lastNode));
								rect4.Inflate(1.0, 1.0);
								if (rect4.Contains(intersection4))
								{
									dcPoints.Add(intersection4);
									return;
								}
								missingIntersection = true;
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00150AEC File Offset: 0x0014FEEC
		internal static Point GetIntersection(Point line1Start, Point line1End, Point line2Start, Point line2End)
		{
			double num = line1End.Y - line1Start.Y;
			double num2 = line1Start.X - line1End.X;
			double num3 = line1End.X * line1Start.Y - line1Start.X * line1End.Y;
			double num4 = line2End.Y - line2Start.Y;
			double num5 = line2Start.X - line2End.X;
			double num6 = line2End.X * line2Start.Y - line2Start.X * line2End.Y;
			double num7 = num * num5 - num4 * num2;
			if (num7 != 0.0)
			{
				double num8 = (num2 * num6 - num5 * num3) / num7;
				double num9 = (num4 * num3 - num * num6) / num7;
				double num10;
				double num11;
				if (line1Start.X < line1End.X)
				{
					num10 = Math.Floor(line1Start.X);
					num11 = Math.Ceiling(line1End.X);
				}
				else
				{
					num10 = Math.Floor(line1End.X);
					num11 = Math.Ceiling(line1Start.X);
				}
				double num12;
				double num13;
				if (line2Start.X < line2End.X)
				{
					num12 = Math.Floor(line2Start.X);
					num13 = Math.Ceiling(line2End.X);
				}
				else
				{
					num12 = Math.Floor(line2End.X);
					num13 = Math.Ceiling(line2Start.X);
				}
				double num14;
				double num15;
				if (line1Start.Y < line1End.Y)
				{
					num14 = Math.Floor(line1Start.Y);
					num15 = Math.Ceiling(line1End.Y);
				}
				else
				{
					num14 = Math.Floor(line1End.Y);
					num15 = Math.Ceiling(line1Start.Y);
				}
				double num16;
				double num17;
				if (line2Start.Y < line2End.Y)
				{
					num16 = Math.Floor(line2Start.Y);
					num17 = Math.Ceiling(line2End.Y);
				}
				else
				{
					num16 = Math.Floor(line2End.Y);
					num17 = Math.Ceiling(line2Start.Y);
				}
				if (num10 <= num8 && num8 <= num11 && num14 <= num9 && num9 <= num15 && num12 <= num8 && num8 <= num13 && num16 <= num9 && num9 <= num17)
				{
					return new Point(num8, num9);
				}
			}
			if ((long)line1End.X == (long)line2Start.X && (long)line1End.Y == (long)line2Start.Y)
			{
				return new Point(line1End.X, line1End.Y);
			}
			return new Point(double.NaN, double.NaN);
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x00150D5C File Offset: 0x0015015C
		internal bool HitTest(StrokeNode hitNode)
		{
			if (!this.IsValid || !hitNode.IsValid)
			{
				return false;
			}
			IEnumerable<ContourSegment> contourSegments = hitNode.GetContourSegments();
			return this._operations.HitTest(this._lastNode, this._thisNode, this.ConnectingQuad, contourSegments);
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x00150DA4 File Offset: 0x001501A4
		internal StrokeFIndices CutTest(StrokeNode hitNode)
		{
			if (!this.IsValid || !hitNode.IsValid)
			{
				return StrokeFIndices.Empty;
			}
			IEnumerable<ContourSegment> contourSegments = hitNode.GetContourSegments();
			StrokeFIndices strokeFIndices = this._operations.CutTest(this._lastNode, this._thisNode, this.ConnectingQuad, contourSegments);
			if (this._index != 0)
			{
				return this.BindFIndices(strokeFIndices);
			}
			return strokeFIndices;
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00150E00 File Offset: 0x00150200
		internal StrokeFIndices CutTest(Point begin, Point end)
		{
			if (!this.IsValid)
			{
				return StrokeFIndices.Empty;
			}
			StrokeFIndices fragment = this._operations.CutTest(this._lastNode, this._thisNode, this.ConnectingQuad, begin, end);
			return this.BindFIndicesForLassoHitTest(fragment);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00150E44 File Offset: 0x00150244
		private StrokeFIndices BindFIndices(StrokeFIndices fragment)
		{
			if (!fragment.IsEmpty)
			{
				if (!DoubleUtil.AreClose(fragment.BeginFIndex, StrokeFIndices.BeforeFirst))
				{
					fragment.BeginFIndex += (double)(this._index - 1);
				}
				if (!DoubleUtil.AreClose(fragment.EndFIndex, StrokeFIndices.AfterLast))
				{
					fragment.EndFIndex += (double)(this._index - 1);
				}
			}
			return fragment;
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060053A7 RID: 21415 RVA: 0x00150EB0 File Offset: 0x001502B0
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00150EC4 File Offset: 0x001502C4
		private StrokeFIndices BindFIndicesForLassoHitTest(StrokeFIndices fragment)
		{
			if (!fragment.IsEmpty)
			{
				if (DoubleUtil.AreClose(fragment.BeginFIndex, StrokeFIndices.BeforeFirst))
				{
					fragment.BeginFIndex = ((this._index == 0) ? StrokeFIndices.BeforeFirst : ((double)(this._index - 1)));
				}
				else
				{
					fragment.BeginFIndex += (double)(this._index - 1);
				}
				if (DoubleUtil.AreClose(fragment.EndFIndex, StrokeFIndices.AfterLast))
				{
					fragment.EndFIndex = (this._isLastNode ? StrokeFIndices.AfterLast : ((double)this._index));
				}
				else
				{
					fragment.EndFIndex += (double)(this._index - 1);
				}
			}
			return fragment;
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x00150F74 File Offset: 0x00150374
		internal bool IsValid
		{
			get
			{
				return this._operations != null;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x00150F8C File Offset: 0x0015038C
		private Quad ConnectingQuad
		{
			get
			{
				if (!this._isQuadCached)
				{
					this._connectingQuad = this._operations.GetConnectingQuad(this._lastNode, this._thisNode);
					this._isQuadCached = true;
				}
				return this._connectingQuad;
			}
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x00150FCC File Offset: 0x001503CC
		private IEnumerable<ContourSegment> GetContourSegments()
		{
			if (this.IsEllipse)
			{
				return this._operations.GetNonBezierContourSegments(this._lastNode, this._thisNode);
			}
			return this._operations.GetContourSegments(this._thisNode, this.ConnectingQuad);
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x00151010 File Offset: 0x00150410
		internal Point GetPointAt(double findex)
		{
			if (this._lastNode.IsEmpty)
			{
				return this._thisNode.Position;
			}
			if (DoubleUtil.AreClose(findex, (double)this._index))
			{
				return this._thisNode.Position;
			}
			double num = Math.Floor(findex);
			findex -= num;
			double num2 = (this._thisNode.Position.X - this._lastNode.Position.X) * findex;
			double num3 = (this._thisNode.Position.Y - this._lastNode.Position.Y) * findex;
			return new Point(this._lastNode.Position.X + num2, this._lastNode.Position.Y + num3);
		}

		// Token: 0x040025C1 RID: 9665
		private StrokeNodeOperations _operations;

		// Token: 0x040025C2 RID: 9666
		private int _index;

		// Token: 0x040025C3 RID: 9667
		private StrokeNodeData _thisNode;

		// Token: 0x040025C4 RID: 9668
		private StrokeNodeData _lastNode;

		// Token: 0x040025C5 RID: 9669
		private bool _isQuadCached;

		// Token: 0x040025C6 RID: 9670
		private Quad _connectingQuad;

		// Token: 0x040025C7 RID: 9671
		private bool _isLastNode;
	}
}
