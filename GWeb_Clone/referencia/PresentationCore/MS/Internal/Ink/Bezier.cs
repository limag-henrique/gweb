using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MS.Internal.Ink.InkSerializedFormat;

namespace MS.Internal.Ink
{
	// Token: 0x020007B7 RID: 1975
	internal class Bezier
	{
		// Token: 0x0600531C RID: 21276 RVA: 0x0014C1AC File Offset: 0x0014B5AC
		internal bool ConstructBezierState(StylusPointCollection stylusPoints, double fitError)
		{
			if (stylusPoints == null || stylusPoints.Count == 0)
			{
				return false;
			}
			CuspData cuspData = new CuspData();
			cuspData.Analyze(stylusPoints, fitError);
			return this.ConstructFromData(cuspData, fitError);
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x0014C1DC File Offset: 0x0014B5DC
		internal List<Point> Flatten(double tolerance)
		{
			List<Point> list = new List<Point>();
			Vector bezierPoint = this.GetBezierPoint(0);
			list.Add(new Point(bezierPoint.X, bezierPoint.Y));
			int num = this.BezierPointCount - 4;
			if (0 <= num)
			{
				if (tolerance < 2.2204460492503131E-16)
				{
					tolerance = 2.2204460492503131E-16;
				}
				for (int i = 0; i <= num; i += 3)
				{
					this.FlattenSegment(i, tolerance, list);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				Point value = list[j];
				value.X *= StrokeCollectionSerializer.HimetricToAvalonMultiplier;
				value.Y *= StrokeCollectionSerializer.HimetricToAvalonMultiplier;
				list[j] = value;
			}
			return list;
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x0014C294 File Offset: 0x0014B694
		private bool ExtendingRange(double error, CuspData data, int from, int next_cusp, ref int to, ref bool cusp, ref bool done)
		{
			to++;
			cusp = true;
			done = (to >= data.Count - 1);
			if (done)
			{
				to = data.Count - 1;
				cusp = true;
				return false;
			}
			cusp = (to >= next_cusp);
			if (cusp)
			{
				to = next_cusp;
				return false;
			}
			int num = (to - from) / 4;
			int[] i = new int[]
			{
				from,
				from + num,
				(to + from) / 2,
				to - num,
				to
			};
			return Bezier.CoCubic(data, i, error);
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x0014C328 File Offset: 0x0014B728
		private bool AddBezierSegment(CuspData data, int from, ref Vector tanStart, int to, ref Vector tanEnd)
		{
			int num = to - from;
			if (num == 1)
			{
				this.AddLine(data, from, to);
				return true;
			}
			if (num != 2)
			{
				return this.AddLeastSquares(data, from, ref tanStart, to, ref tanEnd);
			}
			this.AddParabola(data, from);
			return true;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x0014C368 File Offset: 0x0014B768
		private bool ConstructFromData(CuspData data, double fitError)
		{
			if (data.Count < 2)
			{
				return false;
			}
			this.AddBezierPoint(data.XY(0));
			if (data.Count == 3)
			{
				this.AddParabola(data, 0);
				return true;
			}
			if (data.Count == 2)
			{
				this.AddLine(data, 0, 1);
				return true;
			}
			if (2.2204460492503131E-16 > fitError)
			{
				fitError = 0.029999999329447746 * (data.Distance() * StrokeCollectionSerializer.HimetricToAvalonMultiplier);
			}
			data.SetTanLinks(0.5 * fitError);
			fitError *= fitError;
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int nPrevCusp = 0;
			bool flag2 = true;
			Vector vector = new Vector(0.0, 0.0);
			Vector vector2 = new Vector(0.0, 0.0);
			int num3 = 0;
			while (!flag)
			{
				if (flag2)
				{
					nPrevCusp = num2;
					num2 = data.GetNextCusp(num3);
					if (!data.Tangent(ref vector2, num3, nPrevCusp, num2, false, true))
					{
						return false;
					}
				}
				else
				{
					vector2.X = -vector.X;
					vector2.Y = -vector.Y;
				}
				num = num3 + 3;
				while (this.ExtendingRange(fitError, data, num3, num2, ref num, ref flag2, ref flag))
				{
				}
				if (!data.Tangent(ref vector, num, nPrevCusp, num2, true, flag2))
				{
					return false;
				}
				if (!this.AddBezierSegment(data, num3, ref vector2, num, ref vector))
				{
					return false;
				}
				num3 = num;
			}
			return true;
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x0014C4B0 File Offset: 0x0014B8B0
		private void AddParabola(CuspData data, int from)
		{
			double num = (data.Node(from + 1) - data.Node(from)) / (data.Node(from + 2) - data.Node(from));
			double num2 = 1.0 - num;
			if (num < 0.001 || num2 < 0.001)
			{
				this.AddLine(data, from, from + 2);
				return;
			}
			double num3 = 1.0 / num;
			double num4 = 1.0 / num2;
			Vector vector = num3 * num4 * data.XY(from + 1);
			Vector point = 0.33333333333333331 * (vector + (1.0 - num2 * num3) * data.XY(from) - num * num4 * data.XY(from + 1));
			this.AddBezierPoint(point);
			point = 0.33333333333333331 * (vector - num2 * num3 * data.XY(from) + (1.0 - num * num4) * data.XY(from + 2));
			this.AddBezierPoint(point);
			this.AddSegmentPoint(data, from + 2);
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x0014C5E4 File Offset: 0x0014B9E4
		private void AddLine(CuspData data, int from, int to)
		{
			this.AddBezierPoint((2.0 * data.XY(from) + data.XY(to)) * 0.33333333333333331);
			this.AddBezierPoint((data.XY(from) + 2.0 * data.XY(to)) * 0.33333333333333331);
			this.AddSegmentPoint(data, to);
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x0014C664 File Offset: 0x0014BA64
		private bool AddLeastSquares(CuspData data, int from, ref Vector V, int to, ref Vector W)
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			double num6 = 0.0;
			double num7 = 0.0;
			double num8 = 0.0;
			double num9 = 0.0;
			for (int i = checked(from + 1); i < to; i++)
			{
				double num10 = (data.Node(i) - data.Node(from)) / (data.Node(to) - data.Node(from));
				double num11 = num10 * num10;
				double num12 = 1.0 - num10;
				double num13 = num12 * num12;
				double num14 = num13 * num12;
				double num15 = 3.0 * num13 * num10;
				double num16 = 3.0 * num12 * num11;
				double num17 = num11 * num10;
				num += num15 * num15;
				num3 += num16 * num16;
				num2 += num15 * num16;
				num6 -= (num14 + num15) * num15;
				num7 -= (num16 + num17) * num15;
				num4 += num15 * (data.XY(i) * V);
				num8 -= (num14 + num15) * num16;
				num9 -= (num16 + num17) * num16;
				num5 += num16 * (data.XY(i) * W);
			}
			num2 *= V * W;
			num4 += V * data.XY(from) * num6 + V * data.XY(to) * num7;
			num5 += W * data.XY(from) * num8 + W * data.XY(to) * num9;
			double num18 = num4 * num3 - num5 * num2;
			double num19 = num5 * num - num4 * num2;
			double num20 = num * num3 - num2 * num2;
			bool flag = Math.Abs(num20) > Math.Abs(num18) * 2.2204460492503131E-16 && Math.Abs(num20) > Math.Abs(num19) * 2.2204460492503131E-16;
			if (flag)
			{
				num18 /= num20;
				num19 /= num20;
				flag = (num18 > 1E-06 && num19 > 1E-06);
			}
			if (!flag)
			{
				num19 = (num18 = (data.Node(to) - data.Node(from)) / 3.0);
			}
			this.AddBezierPoint(data.XY(from) + num18 * V);
			this.AddBezierPoint(data.XY(to) + num19 * W);
			this.AddSegmentPoint(data, to);
			return true;
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x0014C93C File Offset: 0x0014BD3C
		private static bool CoCubic(CuspData data, int[] i, double fitError)
		{
			double num = data.Node(i[4]) - data.Node(i[0]);
			double num2 = num / (data.Node(i[1]) - data.Node(i[0]));
			double num3 = num / (data.Node(i[2]) - data.Node(i[0]));
			double num4 = num / (data.Node(i[3]) - data.Node(i[0]));
			double num5 = num / (data.Node(i[2]) - data.Node(i[1]));
			double num6 = num / (data.Node(i[3]) - data.Node(i[1]));
			double num7 = num / (data.Node(i[4]) - data.Node(i[1]));
			double num8 = num / (data.Node(i[3]) - data.Node(i[2]));
			double num9 = num / (data.Node(i[4]) - data.Node(i[2]));
			double num10 = num / (data.Node(i[4]) - data.Node(i[3]));
			Vector vector = num2 * num3 * num4 * data.XY(i[0]) - num2 * num5 * num6 * num7 * data.XY(i[1]) + num3 * num5 * num8 * num9 * data.XY(i[2]) - num4 * num6 * num8 * num10 * data.XY(i[3]) + num7 * num9 * num10 * data.XY(i[4]);
			return vector * vector < fitError;
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x0014CABC File Offset: 0x0014BEBC
		private void AddBezierPoint(Vector point)
		{
			this._bezierControlPoints.Add((Point)point);
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x0014CADC File Offset: 0x0014BEDC
		private void AddSegmentPoint(CuspData data, int index)
		{
			this._bezierControlPoints.Add((Point)data.XY(index));
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x0014CB00 File Offset: 0x0014BF00
		private Vector DeCasteljau(int iFirst, double t)
		{
			double scalar = 1.0 - t;
			Vector vector = scalar * this.GetBezierPoint(iFirst) + t * this.GetBezierPoint(iFirst + 1);
			Vector vector2 = scalar * this.GetBezierPoint(iFirst + 1) + t * this.GetBezierPoint(iFirst + 2);
			Vector vector3 = scalar * this.GetBezierPoint(iFirst + 2) + t * this.GetBezierPoint(iFirst + 3);
			vector = scalar * vector + t * vector2;
			vector2 = scalar * vector2 + t * vector3;
			return scalar * vector + t * vector2;
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x0014CBC0 File Offset: 0x0014BFC0
		private void FlattenSegment(int iFirst, double tolerance, List<Point> points)
		{
			Vector[] array = new Vector[4];
			double num = 0.0;
			for (int i = checked(iFirst + 1); i <= checked(iFirst + 2); i++)
			{
				array[0] = (this.GetBezierPoint(i - 1) + this.GetBezierPoint(i + 1)) * 0.5 - this.GetBezierPoint(i);
				double length = array[0].Length;
				if (length > num)
				{
					num = length;
				}
			}
			if (num <= 0.5 * tolerance)
			{
				Vector bezierPoint = this.GetBezierPoint(iFirst + 3);
				points.Add(new Point(bezierPoint.X, bezierPoint.Y));
				return;
			}
			int num2 = (int)Math.Sqrt(num / tolerance) + 3;
			if (num2 > 1000)
			{
				num2 = 1000;
			}
			double num3 = 1.0 / (double)num2;
			array[0] = this.GetBezierPoint(iFirst);
			for (int i = 1; i <= 3; i++)
			{
				array[i] = this.DeCasteljau(iFirst, (double)i * num3);
				points.Add(new Point(array[i].X, array[i].Y));
			}
			for (int i = 1; i <= 3; i++)
			{
				for (int j = 0; j <= 3 - i; j++)
				{
					array[j] = array[j + 1] - array[j];
				}
			}
			for (int i = 4; i <= num2; i++)
			{
				for (int j = 1; j <= 3; j++)
				{
					array[j] += array[j - 1];
				}
				points.Add(new Point(array[3].X, array[3].Y));
			}
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0014CD80 File Offset: 0x0014C180
		private Vector GetBezierPoint(int index)
		{
			return (Vector)this._bezierControlPoints[index];
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x0014CDA0 File Offset: 0x0014C1A0
		private int BezierPointCount
		{
			get
			{
				return this._bezierControlPoints.Count;
			}
		}

		// Token: 0x0400259F RID: 9631
		private List<Point> _bezierControlPoints = new List<Point>();
	}
}
