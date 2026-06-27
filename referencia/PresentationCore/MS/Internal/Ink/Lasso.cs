using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Ink;

namespace MS.Internal.Ink
{
	// Token: 0x020007BC RID: 1980
	internal class Lasso
	{
		// Token: 0x06005356 RID: 21334 RVA: 0x0014EF18 File Offset: 0x0014E318
		internal Lasso()
		{
			this._points = new List<Point>();
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06005357 RID: 21335 RVA: 0x0014EF44 File Offset: 0x0014E344
		// (set) Token: 0x06005358 RID: 21336 RVA: 0x0014EF58 File Offset: 0x0014E358
		internal Rect Bounds
		{
			get
			{
				return this._bounds;
			}
			set
			{
				this._bounds = value;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x0014EF6C File Offset: 0x0014E36C
		internal bool IsEmpty
		{
			get
			{
				return this._points.Count < 3;
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x0600535A RID: 21338 RVA: 0x0014EF88 File Offset: 0x0014E388
		internal int PointCount
		{
			get
			{
				return this._points.Count;
			}
		}

		// Token: 0x1700114E RID: 4430
		internal Point this[int index]
		{
			get
			{
				return this._points[index];
			}
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0014EFBC File Offset: 0x0014E3BC
		internal void AddPoints(IEnumerable<Point> points)
		{
			foreach (Point point in points)
			{
				this.AddPoint(point);
			}
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0014F010 File Offset: 0x0014E410
		internal void AddPoint(Point point)
		{
			if (!this.Filter(point))
			{
				this.AddPointImpl(point);
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0014F030 File Offset: 0x0014E430
		internal bool Contains(Point point)
		{
			if (!this._bounds.Contains(point))
			{
				return false;
			}
			bool flag = false;
			int num = this._points.Count;
			while (--num >= 0)
			{
				if (!DoubleUtil.AreClose(this._points[num].Y, point.Y))
				{
					flag = (point.Y < this._points[num].Y);
					break;
				}
			}
			bool flag2 = false;
			Point point2 = this._points[this._points.Count - 1];
			for (int i = 0; i < this._points.Count; i++)
			{
				Point point3 = this._points[i];
				if (DoubleUtil.AreClose(point3.Y, point.Y))
				{
					if (DoubleUtil.AreClose(point3.X, point.X))
					{
						flag2 = true;
						break;
					}
					if (i != 0 && DoubleUtil.AreClose(point2.Y, point.Y) && DoubleUtil.GreaterThanOrClose(point.X, Math.Min(point2.X, point3.X)) && DoubleUtil.LessThanOrClose(point.X, Math.Max(point2.X, point3.X)))
					{
						flag2 = true;
						break;
					}
				}
				else if (flag != point.Y < point3.Y)
				{
					flag = !flag;
					if (DoubleUtil.GreaterThanOrClose(point.X, Math.Max(point2.X, point3.X)))
					{
						flag2 = !flag2;
					}
					else if (DoubleUtil.GreaterThanOrClose(point.X, Math.Min(point2.X, point3.X)))
					{
						Vector vector = point3 - point2;
						double value = point2.X + vector.X / vector.Y * (point.Y - point2.Y);
						if (DoubleUtil.GreaterThanOrClose(point.X, value))
						{
							flag2 = !flag2;
						}
					}
				}
				point2 = point3;
			}
			return flag2;
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0014F244 File Offset: 0x0014E644
		internal StrokeIntersection[] HitTest(StrokeNodeIterator iterator)
		{
			if (this._points.Count < 3)
			{
				return new StrokeIntersection[0];
			}
			Point point = default(Point);
			Point point2 = this._points[this._points.Count - 1];
			Rect rect = Rect.Empty;
			Lasso.LassoCrossing item = Lasso.LassoCrossing.EmptyCrossing;
			List<Lasso.LassoCrossing> list = new List<Lasso.LassoCrossing>();
			for (int i = 0; i < iterator.Count; i++)
			{
				StrokeNode strokeNode = iterator[i];
				Rect bounds = strokeNode.GetBounds();
				rect.Union(bounds);
				if (rect.IntersectsWith(this._bounds))
				{
					Point point3 = point2;
					foreach (Point point4 in this._points)
					{
						Rect rect2 = new Rect(point3, point4);
						if (!rect.IntersectsWith(rect2))
						{
							point3 = point4;
						}
						else
						{
							StrokeFIndices newFIndices = strokeNode.CutTest(point3, point4);
							point3 = point4;
							if (!newFIndices.IsEmpty)
							{
								Lasso.LassoCrossing lassoCrossing = new Lasso.LassoCrossing(newFIndices, strokeNode);
								if (!item.Merge(lassoCrossing))
								{
									list.Add(item);
									item = lassoCrossing;
								}
							}
						}
					}
				}
				rect = bounds;
				point = strokeNode.Position;
			}
			if (!item.IsEmpty)
			{
				list.Add(item);
			}
			if (list.Count != 0)
			{
				Lasso.SortAndMerge(ref list);
				List<StrokeIntersection> list2 = new List<StrokeIntersection>();
				this.ProduceHitTestResults(list, list2);
				return list2.ToArray();
			}
			if (this.Contains(point))
			{
				return new StrokeIntersection[]
				{
					StrokeIntersection.Full
				};
			}
			return new StrokeIntersection[0];
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0014F3EC File Offset: 0x0014E7EC
		private static void SortAndMerge(ref List<Lasso.LassoCrossing> crossingList)
		{
			crossingList.Sort();
			List<Lasso.LassoCrossing> list = new List<Lasso.LassoCrossing>();
			Lasso.LassoCrossing item = Lasso.LassoCrossing.EmptyCrossing;
			foreach (Lasso.LassoCrossing lassoCrossing in crossingList)
			{
				if (!item.Merge(lassoCrossing))
				{
					list.Add(item);
					item = lassoCrossing;
				}
			}
			if (!item.IsEmpty)
			{
				list.Add(item);
			}
			crossingList = list;
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0014F47C File Offset: 0x0014E87C
		private bool SegmentWithinLasso(StrokeNode strokeNode, double fIndex)
		{
			bool result;
			if (DoubleUtil.AreClose(fIndex, StrokeFIndices.BeforeFirst))
			{
				result = this.Contains(strokeNode.GetPointAt(0.0));
			}
			else if (DoubleUtil.AreClose(fIndex, StrokeFIndices.AfterLast))
			{
				result = this.Contains(strokeNode.Position);
			}
			else
			{
				result = this.Contains(strokeNode.GetPointAt(fIndex));
			}
			return result;
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0014F4DC File Offset: 0x0014E8DC
		private void ProduceHitTestResults(List<Lasso.LassoCrossing> crossingList, List<StrokeIntersection> strokeIntersections)
		{
			bool flag = false;
			for (int i = 0; i <= crossingList.Count; i++)
			{
				bool flag2 = false;
				bool flag3 = true;
				StrokeIntersection item = default(StrokeIntersection);
				if (i == 0)
				{
					item.HitBegin = StrokeFIndices.BeforeFirst;
					item.InBegin = StrokeFIndices.BeforeFirst;
				}
				else
				{
					Lasso.LassoCrossing lassoCrossing = crossingList[i - 1];
					item.InBegin = lassoCrossing.FIndices.EndFIndex;
					lassoCrossing = crossingList[i - 1];
					item.HitBegin = lassoCrossing.FIndices.BeginFIndex;
					flag2 = this.SegmentWithinLasso(crossingList[i - 1].EndNode, item.InBegin);
				}
				if (i == crossingList.Count)
				{
					if (DoubleUtil.AreClose(item.InBegin, StrokeFIndices.AfterLast))
					{
						item.InEnd = StrokeFIndices.BeforeFirst;
					}
					else
					{
						item.InEnd = StrokeFIndices.AfterLast;
					}
					item.HitEnd = StrokeFIndices.AfterLast;
				}
				else
				{
					Lasso.LassoCrossing lassoCrossing = crossingList[i];
					item.InEnd = lassoCrossing.FIndices.BeginFIndex;
					if (DoubleUtil.AreClose(item.InEnd, StrokeFIndices.BeforeFirst))
					{
						item.InBegin = StrokeFIndices.AfterLast;
					}
					lassoCrossing = crossingList[i];
					item.HitEnd = lassoCrossing.FIndices.EndFIndex;
					flag2 = this.SegmentWithinLasso(crossingList[i].StartNode, item.InEnd);
					if (!flag2 && !this.SegmentWithinLasso(crossingList[i].EndNode, item.HitEnd))
					{
						flag2 = true;
						lassoCrossing = crossingList[i];
						item.HitBegin = lassoCrossing.FIndices.BeginFIndex;
						item.InBegin = StrokeFIndices.AfterLast;
						item.InEnd = StrokeFIndices.BeforeFirst;
						flag3 = false;
					}
				}
				if (flag2)
				{
					if (i > 0 && flag && flag3)
					{
						StrokeIntersection value = strokeIntersections[strokeIntersections.Count - 1];
						if (value.InSegment.IsEmpty)
						{
							value.InBegin = item.InBegin;
						}
						value.InEnd = item.InEnd;
						value.HitEnd = item.HitEnd;
						strokeIntersections[strokeIntersections.Count - 1] = value;
					}
					else
					{
						strokeIntersections.Add(item);
					}
					if (DoubleUtil.AreClose(item.HitEnd, StrokeFIndices.AfterLast))
					{
						return;
					}
				}
				flag = flag2;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06005363 RID: 21347 RVA: 0x0014F71C File Offset: 0x0014EB1C
		// (set) Token: 0x06005364 RID: 21348 RVA: 0x0014F730 File Offset: 0x0014EB30
		internal bool IsIncrementalLassoDirty
		{
			get
			{
				return this._incrementalLassoDirty;
			}
			set
			{
				this._incrementalLassoDirty = value;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06005365 RID: 21349 RVA: 0x0014F744 File Offset: 0x0014EB44
		protected List<Point> PointsList
		{
			get
			{
				return this._points;
			}
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0014F758 File Offset: 0x0014EB58
		protected virtual bool Filter(Point point)
		{
			if (this._points.Count == 0)
			{
				return false;
			}
			Point point2 = this._points[this._points.Count - 1];
			Vector vector = point - point2;
			return Math.Abs(vector.X) < Lasso.MinDistance && Math.Abs(vector.Y) < Lasso.MinDistance;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0014F7C0 File Offset: 0x0014EBC0
		protected virtual void AddPointImpl(Point point)
		{
			this._points.Add(point);
			this._bounds.Union(point);
		}

		// Token: 0x040025B1 RID: 9649
		private List<Point> _points;

		// Token: 0x040025B2 RID: 9650
		private Rect _bounds = Rect.Empty;

		// Token: 0x040025B3 RID: 9651
		private bool _incrementalLassoDirty;

		// Token: 0x040025B4 RID: 9652
		private static readonly double MinDistance = 1.0;

		// Token: 0x02000A03 RID: 2563
		private struct LassoCrossing : IComparable
		{
			// Token: 0x06005BF9 RID: 23545 RVA: 0x0017176C File Offset: 0x00170B6C
			public LassoCrossing(StrokeFIndices newFIndices, StrokeNode strokeNode)
			{
				this.FIndices = newFIndices;
				this.EndNode = strokeNode;
				this.StartNode = strokeNode;
			}

			// Token: 0x06005BFA RID: 23546 RVA: 0x00171790 File Offset: 0x00170B90
			public override string ToString()
			{
				return this.FIndices.ToString();
			}

			// Token: 0x170012D2 RID: 4818
			// (get) Token: 0x06005BFB RID: 23547 RVA: 0x001717B0 File Offset: 0x00170BB0
			public static Lasso.LassoCrossing EmptyCrossing
			{
				get
				{
					return new Lasso.LassoCrossing
					{
						FIndices = StrokeFIndices.Empty
					};
				}
			}

			// Token: 0x170012D3 RID: 4819
			// (get) Token: 0x06005BFC RID: 23548 RVA: 0x001717D4 File Offset: 0x00170BD4
			public bool IsEmpty
			{
				get
				{
					return this.FIndices.IsEmpty;
				}
			}

			// Token: 0x06005BFD RID: 23549 RVA: 0x001717EC File Offset: 0x00170BEC
			public int CompareTo(object obj)
			{
				Lasso.LassoCrossing lassoCrossing = (Lasso.LassoCrossing)obj;
				if (lassoCrossing.IsEmpty && this.IsEmpty)
				{
					return 0;
				}
				if (lassoCrossing.IsEmpty)
				{
					return 1;
				}
				if (this.IsEmpty)
				{
					return -1;
				}
				return this.FIndices.CompareTo(lassoCrossing.FIndices);
			}

			// Token: 0x06005BFE RID: 23550 RVA: 0x0017183C File Offset: 0x00170C3C
			public bool Merge(Lasso.LassoCrossing crossing)
			{
				if (crossing.IsEmpty)
				{
					return false;
				}
				if (this.FIndices.IsEmpty && !crossing.IsEmpty)
				{
					this.FIndices = crossing.FIndices;
					this.StartNode = crossing.StartNode;
					this.EndNode = crossing.EndNode;
					return true;
				}
				if (DoubleUtil.GreaterThanOrClose(crossing.FIndices.EndFIndex, this.FIndices.BeginFIndex) && DoubleUtil.GreaterThanOrClose(this.FIndices.EndFIndex, crossing.FIndices.BeginFIndex))
				{
					if (DoubleUtil.LessThan(crossing.FIndices.BeginFIndex, this.FIndices.BeginFIndex))
					{
						this.FIndices.BeginFIndex = crossing.FIndices.BeginFIndex;
						this.StartNode = crossing.StartNode;
					}
					if (DoubleUtil.GreaterThan(crossing.FIndices.EndFIndex, this.FIndices.EndFIndex))
					{
						this.FIndices.EndFIndex = crossing.FIndices.EndFIndex;
						this.EndNode = crossing.EndNode;
					}
					return true;
				}
				return false;
			}

			// Token: 0x04002F31 RID: 12081
			internal StrokeFIndices FIndices;

			// Token: 0x04002F32 RID: 12082
			internal StrokeNode StartNode;

			// Token: 0x04002F33 RID: 12083
			internal StrokeNode EndNode;
		}
	}
}
