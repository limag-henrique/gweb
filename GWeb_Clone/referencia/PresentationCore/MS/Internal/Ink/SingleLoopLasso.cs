using System;
using System.Collections.Generic;
using System.Windows;

namespace MS.Internal.Ink
{
	// Token: 0x020007BD RID: 1981
	internal class SingleLoopLasso : Lasso
	{
		// Token: 0x06005369 RID: 21353 RVA: 0x0014F804 File Offset: 0x0014EC04
		internal SingleLoopLasso()
		{
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x0014F824 File Offset: 0x0014EC24
		protected override bool Filter(Point point)
		{
			List<Point> pointsList = base.PointsList;
			if (pointsList.Count == 0)
			{
				return false;
			}
			if (this._hasLoop || base.Filter(point))
			{
				return true;
			}
			double num = 0.0;
			if (!this.GetIntersectionWithExistingLasso(point, ref num))
			{
				return false;
			}
			if (num == (double)(pointsList.Count - 2))
			{
				return true;
			}
			int num2 = (int)num;
			if (!DoubleUtil.AreClose((double)num2, num))
			{
				pointsList[num2] = new Point(0.0, 0.0)
				{
					X = pointsList[num2].X + (num - (double)num2) * (pointsList[num2 + 1].X - pointsList[num2].X),
					Y = pointsList[num2].Y + (num - (double)num2) * (pointsList[num2 + 1].Y - pointsList[num2].Y)
				};
				base.IsIncrementalLassoDirty = true;
			}
			if (num2 > 0)
			{
				pointsList.RemoveRange(0, num2);
				base.IsIncrementalLassoDirty = true;
			}
			if (base.IsIncrementalLassoDirty)
			{
				Rect empty = Rect.Empty;
				for (int i = 0; i < pointsList.Count; i++)
				{
					empty.Union(pointsList[i]);
				}
				base.Bounds = empty;
			}
			this._hasLoop = true;
			return true;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0014F988 File Offset: 0x0014ED88
		protected override void AddPointImpl(Point point)
		{
			this._prevBounds = base.Bounds;
			base.AddPointImpl(point);
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0014F9A8 File Offset: 0x0014EDA8
		private bool GetIntersectionWithExistingLasso(Point point, ref double bIndex)
		{
			List<Point> pointsList = base.PointsList;
			int count = pointsList.Count;
			Rect rect = new Rect(pointsList[count - 1], point);
			if (!this._prevBounds.IntersectsWith(rect))
			{
				return false;
			}
			for (int i = 0; i < count - 2; i++)
			{
				Rect rect2 = new Rect(pointsList[i], pointsList[i + 1]);
				if (rect2.IntersectsWith(rect))
				{
					double num = SingleLoopLasso.FindIntersection(pointsList[count - 1] - pointsList[i], point - pointsList[i], new Vector(0.0, 0.0), pointsList[i + 1] - pointsList[i]);
					if (num >= 0.0 && num <= 1.0)
					{
						bIndex = (double)i + num;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0014FA94 File Offset: 0x0014EE94
		private static double FindIntersection(Vector hitBegin, Vector hitEnd, Vector orgBegin, Vector orgEnd)
		{
			Vector vector = orgEnd - orgBegin;
			Vector vector2 = orgBegin - hitBegin;
			Vector vector3 = hitEnd - hitBegin;
			double num = Vector.Determinant(vector, vector3);
			if (DoubleUtil.IsZero(num))
			{
				return SingleLoopLasso.NoIntersection;
			}
			double num2 = SingleLoopLasso.AdjustFIndex(Vector.Determinant(vector, vector2) / num);
			if (num2 >= 0.0 && num2 <= 1.0)
			{
				double num3 = SingleLoopLasso.AdjustFIndex(Vector.Determinant(vector3, vector2) / num);
				if (num3 >= 0.0 && num3 <= 1.0)
				{
					return num3;
				}
			}
			return SingleLoopLasso.NoIntersection;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0014FB2C File Offset: 0x0014EF2C
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

		// Token: 0x040025B5 RID: 9653
		private bool _hasLoop;

		// Token: 0x040025B6 RID: 9654
		private Rect _prevBounds = Rect.Empty;

		// Token: 0x040025B7 RID: 9655
		private static readonly double NoIntersection = StrokeFIndices.BeforeFirst;
	}
}
