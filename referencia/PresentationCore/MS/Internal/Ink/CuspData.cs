using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MS.Internal.Ink.InkSerializedFormat;

namespace MS.Internal.Ink
{
	// Token: 0x020007B9 RID: 1977
	internal class CuspData
	{
		// Token: 0x06005332 RID: 21298 RVA: 0x0014CEE4 File Offset: 0x0014C2E4
		internal CuspData()
		{
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0014CF14 File Offset: 0x0014C314
		internal void Analyze(StylusPointCollection stylusPoints, double rSpan)
		{
			if (stylusPoints == null || stylusPoints.Count == 0)
			{
				return;
			}
			this._points = new List<CuspData.CDataPoint>(stylusPoints.Count);
			this._nodes = new List<double>(stylusPoints.Count);
			this._nodes.Add(0.0);
			CuspData.CDataPoint item = default(CuspData.CDataPoint);
			item.Index = 0;
			Point point = (Point)stylusPoints[0];
			point.X *= StrokeCollectionSerializer.AvalonToHimetricMultiplier;
			point.Y *= StrokeCollectionSerializer.AvalonToHimetricMultiplier;
			item.Point = point;
			this._points.Add(item);
			int num = 0;
			for (int i = 1; i < stylusPoints.Count; i++)
			{
				if (!DoubleUtil.AreClose(stylusPoints[i].X, stylusPoints[i - 1].X) || !DoubleUtil.AreClose(stylusPoints[i].Y, stylusPoints[i - 1].Y))
				{
					num++;
					CuspData.CDataPoint item2 = default(CuspData.CDataPoint);
					item2.Index = num;
					Point point2 = (Point)stylusPoints[i];
					point2.X *= StrokeCollectionSerializer.AvalonToHimetricMultiplier;
					point2.Y *= StrokeCollectionSerializer.AvalonToHimetricMultiplier;
					item2.Point = point2;
					this._points.Insert(num, item2);
					this._nodes.Insert(num, this._nodes[num - 1] + (this.XY(num) - this.XY(num - 1)).Length);
				}
			}
			this.SetLinks(rSpan);
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0014D0C4 File Offset: 0x0014C4C4
		internal void SetTanLinks(double rError)
		{
			int count = this.Count;
			if (rError < 1.0)
			{
				rError = 1.0;
			}
			for (int i = 0; i < count; i++)
			{
				for (int j = i + 1; j < count; j++)
				{
					if (this._nodes[j] - this._nodes[i] >= rError)
					{
						CuspData.CDataPoint value = this._points[i];
						value.TanNext = j;
						this._points[i] = value;
						CuspData.CDataPoint value2 = this._points[j];
						value2.TanPrev = i;
						this._points[j] = value2;
						break;
					}
				}
				if (0 > this._points[i].TanPrev)
				{
					int num = i - 1;
					while (0 <= num)
					{
						if (this._nodes[i] - this._nodes[num] >= rError)
						{
							CuspData.CDataPoint value3 = this._points[i];
							value3.TanPrev = num;
							this._points[i] = value3;
							break;
						}
						num--;
					}
				}
				if (0 > this._points[i].TanNext)
				{
					CuspData.CDataPoint value4 = this._points[i];
					value4.TanNext = count - 1;
					this._points[i] = value4;
				}
				if (0 > this._points[i].TanPrev)
				{
					CuspData.CDataPoint value5 = this._points[i];
					value5.TanPrev = 0;
					this._points[i] = value5;
				}
			}
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x0014D24C File Offset: 0x0014C64C
		internal int GetNextCusp(int iCurrent)
		{
			int num = this.Count - 1;
			if (iCurrent < 0)
			{
				return 0;
			}
			if (iCurrent >= num)
			{
				return num;
			}
			int i = 0;
			int num2 = this._cusps.Count;
			int num3 = (i + num2) / 2;
			while (i < num3)
			{
				if (this._cusps[num3] <= iCurrent)
				{
					i = num3;
				}
				else
				{
					num2 = num3;
				}
				num3 = (i + num2) / 2;
			}
			return this._cusps[num3 + 1];
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0014D2B4 File Offset: 0x0014C6B4
		internal Vector XY(int i)
		{
			CuspData.CDataPoint cdataPoint = this._points[i];
			double x = cdataPoint.Point.X;
			cdataPoint = this._points[i];
			return new Vector(x, cdataPoint.Point.Y);
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06005337 RID: 21303 RVA: 0x0014D2F8 File Offset: 0x0014C6F8
		internal int Count
		{
			get
			{
				return this._points.Count;
			}
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0014D310 File Offset: 0x0014C710
		internal double Node(int i)
		{
			return this._nodes[i];
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x0014D32C File Offset: 0x0014C72C
		internal int GetPointIndex(int nodeIndex)
		{
			return this._points[nodeIndex].Index;
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x0014D34C File Offset: 0x0014C74C
		internal double Distance()
		{
			return this._dist;
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0014D360 File Offset: 0x0014C760
		internal bool Tangent(ref Vector ptT, int nAt, int nPrevCusp, int nNextCusp, bool bReverse, bool bIsCusp)
		{
			if (bIsCusp)
			{
				int num;
				int num2;
				if (bReverse)
				{
					num = this._points[nAt].TanPrev;
					if (num < nPrevCusp || 0 > num)
					{
						num2 = nPrevCusp;
						num = (num2 + nAt) / 2;
					}
					else
					{
						num2 = this._points[num].TanPrev;
						if (num2 < nPrevCusp)
						{
							num2 = nPrevCusp;
						}
					}
				}
				else
				{
					num = this._points[nAt].TanNext;
					if (num > nNextCusp || 0 > num)
					{
						num2 = nNextCusp;
						num = (num2 + nAt) / 2;
					}
					else
					{
						num2 = this._points[num].TanNext;
						if (num2 > nNextCusp)
						{
							num2 = nNextCusp;
						}
					}
				}
				ptT = this.XY(num) + 0.5 * this.XY(num2) - 1.5 * this.XY(nAt);
			}
			else
			{
				int num = nAt;
				int num2 = this._points[nAt].TanPrev;
				int num3;
				if (num2 < nPrevCusp)
				{
					num3 = nPrevCusp;
					num2 = (num3 + num) / 2;
				}
				else
				{
					num3 = this._points[num2].TanPrev;
					if (num3 < nPrevCusp)
					{
						num3 = nPrevCusp;
					}
				}
				nAt = this._points[nAt].TanNext;
				if (nAt > nNextCusp)
				{
					nAt = nNextCusp;
				}
				ptT = this.XY(num) + this.XY(num2) + 0.5 * this.XY(num3) - 2.5 * this.XY(nAt);
			}
			if (DoubleUtil.IsZero(ptT.LengthSquared))
			{
				return false;
			}
			ptT.Normalize();
			return true;
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0014D4F8 File Offset: 0x0014C8F8
		private double GetCurvature(int iPrev, int iCurrent, int iNext)
		{
			Vector vector = this.XY(iCurrent) - this.XY(iPrev);
			Vector vector2 = this.XY(iNext) - this.XY(iCurrent);
			double num = vector.Length * vector2.Length;
			if (DoubleUtil.IsZero(num))
			{
				return 0.0;
			}
			return 1.0 - vector * vector2 / num;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0014D564 File Offset: 0x0014C964
		private void FindAllCusps()
		{
			this._cusps.Clear();
			if (1 > this.Count)
			{
				return;
			}
			this._cusps.Add(0);
			int num = 0;
			int num2 = 0;
			int iPrevCusp = 0;
			if (this.FindNextAndPrev(0, iPrevCusp, ref num, ref num2))
			{
				int num3 = num2;
				while (this.FindNextAndPrev(num3, iPrevCusp, ref num, ref num2))
				{
					double curvature = this.GetCurvature(num, num3, num2);
					if (0.8 < curvature)
					{
						double num4 = curvature;
						int num5 = num3;
						int num6 = 0;
						int num7 = 0;
						if (!this.FindNextAndPrev(num2, iPrevCusp, ref num7, ref num6))
						{
							break;
						}
						int num8 = num + 1;
						while (num8 <= num6 && this.FindNextAndPrev(num8, iPrevCusp, ref num, ref num2))
						{
							curvature = this.GetCurvature(num, num8, num2);
							if (curvature > num4)
							{
								num4 = curvature;
								num5 = num8;
							}
							num8++;
						}
						this._cusps.Add(num5);
						num3 = num6 + 1;
						iPrevCusp = num5;
					}
					else if (0.035 > curvature)
					{
						num3 = num2;
					}
					else
					{
						num3++;
					}
				}
				this._cusps.Add(this.Count - 1);
				return;
			}
			if (this.Count == 0)
			{
				this._cusps.Clear();
				return;
			}
			if (1 < this.Count)
			{
				this._cusps.Add(num2);
			}
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0014D69C File Offset: 0x0014CA9C
		private bool FindNextAndPrev(int iPoint, int iPrevCusp, ref int iPrev, ref int iNext)
		{
			bool result = true;
			if (iPoint >= this.Count)
			{
				result = false;
				iPoint = this.Count - 1;
			}
			iNext = checked(iPoint + 1);
			while (iNext < this.Count && this._nodes[iNext] - this._nodes[iPoint] < this._span)
			{
				iNext++;
			}
			if (iNext >= this.Count)
			{
				result = false;
				iNext = this.Count - 1;
			}
			iPrev = checked(iPoint - 1);
			while (iPrevCusp <= iPrev && this._nodes[iPoint] - this._nodes[iPrev] < this._span)
			{
				iPrev--;
			}
			if (iPrev < 0)
			{
				iPrev = 0;
			}
			return result;
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x0014D754 File Offset: 0x0014CB54
		private static void UpdateMinMax(double a, ref double rMin, ref double rMax)
		{
			rMin = Math.Min(rMin, a);
			rMax = Math.Max(a, rMax);
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x0014D778 File Offset: 0x0014CB78
		private void SetLinks(double rSpan)
		{
			int count = this.Count;
			if (2 > count)
			{
				return;
			}
			double x = this.XY(0).X;
			double y = this.XY(0).Y;
			double num = x;
			double num2 = y;
			for (int i = 0; i < count; i++)
			{
				CuspData.UpdateMinMax(this.XY(i).X, ref x, ref num);
				CuspData.UpdateMinMax(this.XY(i).Y, ref y, ref num2);
			}
			num -= x;
			num2 -= y;
			this._dist = Math.Abs(num) + Math.Abs(num2);
			if (!DoubleUtil.IsZero(rSpan))
			{
				this._span = rSpan;
			}
			else if (0.0 < this._dist)
			{
				this._span = 0.75 * (this._nodes[count - 1] * this._nodes[count - 1]) / ((double)count * this._dist);
			}
			if (this._span < 1.0)
			{
				this._span = 1.0;
			}
			this.FindAllCusps();
		}

		// Token: 0x040025A3 RID: 9635
		private List<CuspData.CDataPoint> _points;

		// Token: 0x040025A4 RID: 9636
		private List<double> _nodes;

		// Token: 0x040025A5 RID: 9637
		private double _dist;

		// Token: 0x040025A6 RID: 9638
		private List<int> _cusps = new List<int>();

		// Token: 0x040025A7 RID: 9639
		private double _span = 3.0;

		// Token: 0x02000A01 RID: 2561
		private struct CDataPoint
		{
			// Token: 0x04002F25 RID: 12069
			public Point Point;

			// Token: 0x04002F26 RID: 12070
			public int Index;

			// Token: 0x04002F27 RID: 12071
			public int TanPrev;

			// Token: 0x04002F28 RID: 12072
			public int TanNext;
		}
	}
}
