using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Ink;

namespace MS.Internal.Ink
{
	// Token: 0x020007BB RID: 1979
	internal class ErasingStroke
	{
		// Token: 0x0600534E RID: 21326 RVA: 0x0014E8F0 File Offset: 0x0014DCF0
		internal ErasingStroke(StylusShape erasingShape)
		{
			this._nodeIterator = new StrokeNodeIterator(erasingShape);
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0014E91C File Offset: 0x0014DD1C
		internal ErasingStroke(StylusShape erasingShape, IEnumerable<Point> path) : this(erasingShape)
		{
			this.MoveTo(path);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0014E938 File Offset: 0x0014DD38
		internal void MoveTo(IEnumerable<Point> path)
		{
			Point[] pointArray = IEnumerablePointHelper.GetPointArray(path);
			if (this._erasingStrokeNodes == null)
			{
				this._erasingStrokeNodes = new List<StrokeNode>(pointArray.Length);
			}
			else
			{
				this._erasingStrokeNodes.Clear();
			}
			this._bounds = Rect.Empty;
			this._nodeIterator = this._nodeIterator.GetIteratorForNextSegment((pointArray.Length > 1) ? this.FilterPoints(pointArray) : pointArray);
			for (int i = 0; i < this._nodeIterator.Count; i++)
			{
				StrokeNode item = this._nodeIterator[i];
				this._bounds.Union(item.GetBoundsConnected());
				this._erasingStrokeNodes.Add(item);
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06005351 RID: 21329 RVA: 0x0014E9DC File Offset: 0x0014DDDC
		internal Rect Bounds
		{
			get
			{
				return this._bounds;
			}
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0014E9F0 File Offset: 0x0014DDF0
		internal bool HitTest(StrokeNodeIterator iterator)
		{
			if (this._erasingStrokeNodes == null || this._erasingStrokeNodes.Count == 0)
			{
				return false;
			}
			Rect empty = Rect.Empty;
			for (int i = 0; i < iterator.Count; i++)
			{
				StrokeNode hitNode = iterator[i];
				Rect bounds = hitNode.GetBounds();
				empty.Union(bounds);
				if (empty.IntersectsWith(this._bounds))
				{
					foreach (StrokeNode strokeNode in this._erasingStrokeNodes)
					{
						if (empty.IntersectsWith(strokeNode.GetBoundsConnected()) && strokeNode.HitTest(hitNode))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0014EAC8 File Offset: 0x0014DEC8
		internal bool EraseTest(StrokeNodeIterator iterator, List<StrokeIntersection> intersections)
		{
			intersections.Clear();
			List<StrokeFIndices> list = new List<StrokeFIndices>();
			if (this._erasingStrokeNodes == null || this._erasingStrokeNodes.Count == 0)
			{
				return false;
			}
			Rect rect = Rect.Empty;
			for (int i = 0; i < iterator.Count; i++)
			{
				StrokeNode strokeNode = iterator[i];
				Rect bounds = strokeNode.GetBounds();
				rect.Union(bounds);
				if (rect.IntersectsWith(this._bounds))
				{
					int count = list.Count;
					foreach (StrokeNode hitNode in this._erasingStrokeNodes)
					{
						if (rect.IntersectsWith(hitNode.GetBoundsConnected()))
						{
							StrokeFIndices strokeFIndices = strokeNode.CutTest(hitNode);
							if (!strokeFIndices.IsEmpty)
							{
								bool flag = false;
								for (int j = count; j < list.Count; j++)
								{
									StrokeFIndices strokeFIndices2 = list[j];
									if (strokeFIndices.BeginFIndex < strokeFIndices2.EndFIndex)
									{
										if (strokeFIndices.EndFIndex <= strokeFIndices2.BeginFIndex)
										{
											list.Insert(j, strokeFIndices);
											flag = true;
											break;
										}
										strokeFIndices = new StrokeFIndices(Math.Min(strokeFIndices2.BeginFIndex, strokeFIndices.BeginFIndex), Math.Max(strokeFIndices2.EndFIndex, strokeFIndices.EndFIndex));
										if (strokeFIndices.EndFIndex <= strokeFIndices2.EndFIndex || j + 1 == list.Count)
										{
											flag = true;
											list[j] = strokeFIndices;
											break;
										}
										list.RemoveAt(j);
										j--;
									}
								}
								if (!flag)
								{
									list.Add(strokeFIndices);
								}
								if (list[list.Count - 1].IsFull)
								{
									break;
								}
							}
						}
					}
					if (count > 0 && count < list.Count)
					{
						StrokeFIndices value = list[count - 1];
						if (DoubleUtil.AreClose(value.EndFIndex, StrokeFIndices.AfterLast))
						{
							if (DoubleUtil.AreClose(list[count].BeginFIndex, StrokeFIndices.BeforeFirst))
							{
								value.EndFIndex = list[count].EndFIndex;
								list[count - 1] = value;
								list.RemoveAt(count);
							}
							else
							{
								value.EndFIndex = (double)strokeNode.Index;
								list[count - 1] = value;
							}
						}
					}
				}
				rect = bounds;
			}
			if (list.Count != 0)
			{
				foreach (StrokeFIndices strokeFIndices3 in list)
				{
					intersections.Add(new StrokeIntersection(strokeFIndices3.BeginFIndex, StrokeFIndices.AfterLast, StrokeFIndices.BeforeFirst, strokeFIndices3.EndFIndex));
				}
			}
			return list.Count != 0;
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0014EDB4 File Offset: 0x0014E1B4
		private Point[] FilterPoints(Point[] path)
		{
			List<Point> list = new List<Point>();
			Point point;
			Point point2;
			int i;
			if (this._nodeIterator.Count == 0)
			{
				list.Add(path[0]);
				list.Add(path[1]);
				point = path[0];
				point2 = path[1];
				i = 2;
			}
			else
			{
				list.Add(path[0]);
				point = this._nodeIterator[this._nodeIterator.Count - 1].Position;
				point2 = path[0];
				i = 1;
			}
			while (i < path.Length)
			{
				if (DoubleUtil.AreClose(point2, path[i]))
				{
					i++;
				}
				else
				{
					Vector vector = point - point2;
					Vector vector2 = path[i] - point2;
					double projectionFIndex = StrokeNodeOperations.GetProjectionFIndex(vector, vector2);
					if (DoubleUtil.IsBetweenZeroAndOne(projectionFIndex) && (vector + (vector2 - vector) * projectionFIndex).LengthSquared < ErasingStroke.CollinearTolerance)
					{
						list[list.Count - 1] = path[i];
						point2 = path[i];
						i++;
					}
					else
					{
						list.Add(path[i]);
						point = point2;
						point2 = path[i];
						i++;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x040025AD RID: 9645
		private StrokeNodeIterator _nodeIterator;

		// Token: 0x040025AE RID: 9646
		private List<StrokeNode> _erasingStrokeNodes;

		// Token: 0x040025AF RID: 9647
		private Rect _bounds = Rect.Empty;

		// Token: 0x040025B0 RID: 9648
		private static readonly double CollinearTolerance = 0.10000000149011612;
	}
}
