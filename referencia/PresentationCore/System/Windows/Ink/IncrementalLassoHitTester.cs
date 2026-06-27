using System;
using System.Collections.Generic;
using System.Windows.Input;
using MS.Internal;
using MS.Internal.Ink;

namespace System.Windows.Ink
{
	/// <summary>Realiza testes de clique dinamicamente em um <see cref="T:System.Windows.Ink.Stroke" /> com um Laço.</summary>
	// Token: 0x02000353 RID: 851
	public class IncrementalLassoHitTester : IncrementalHitTester
	{
		/// <summary>Ocorre quando o caminho de Laço marca ou desmarca um <see cref="T:System.Windows.Ink.Stroke" /> de tinta.</summary>
		// Token: 0x1400017D RID: 381
		// (add) Token: 0x06001CC5 RID: 7365 RVA: 0x00075064 File Offset: 0x00074464
		// (remove) Token: 0x06001CC6 RID: 7366 RVA: 0x0007509C File Offset: 0x0007449C
		public event LassoSelectionChangedEventHandler SelectionChanged;

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000750D4 File Offset: 0x000744D4
		internal IncrementalLassoHitTester(StrokeCollection strokes, int percentageWithinLasso) : base(strokes)
		{
			this._lasso = new SingleLoopLasso();
			this._percentIntersect = percentageWithinLasso;
		}

		/// <summary>Adiciona pontos ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</summary>
		/// <param name="points">Os pontos a adicionar</param>
		// Token: 0x06001CC8 RID: 7368 RVA: 0x000750FC File Offset: 0x000744FC
		protected override void AddPointsCore(IEnumerable<Point> points)
		{
			int i = (this._lasso.PointCount != 0) ? (this._lasso.PointCount - 1) : 0;
			this._lasso.AddPoints(points);
			if (this._lasso.IsEmpty || (i == this._lasso.PointCount - 1 && !this._lasso.IsIncrementalLassoDirty) || this.SelectionChanged == null)
			{
				return;
			}
			StrokeCollection strokeCollection = null;
			StrokeCollection strokeCollection2 = null;
			Lasso lasso = new Lasso();
			if (!this._lasso.IsIncrementalLassoDirty)
			{
				if (0 < i)
				{
					lasso.AddPoint(this._lasso[0]);
				}
				while (i < this._lasso.PointCount)
				{
					lasso.AddPoint(this._lasso[i]);
					i++;
				}
			}
			foreach (StrokeInfo strokeInfo in base.StrokeInfos)
			{
				Lasso lasso2;
				if (strokeInfo.IsDirty || this._lasso.IsIncrementalLassoDirty)
				{
					lasso2 = this._lasso;
					strokeInfo.IsDirty = false;
				}
				else
				{
					lasso2 = lasso;
				}
				double num = 0.0;
				if (lasso2.Bounds.IntersectsWith(strokeInfo.StrokeBounds))
				{
					StylusPointCollection stylusPoints = strokeInfo.StylusPoints;
					for (int j = 0; j < stylusPoints.Count; j++)
					{
						if (lasso2.Contains((Point)stylusPoints[j]))
						{
							double pointWeight = strokeInfo.GetPointWeight(j);
							if (lasso2 == this._lasso || this._lasso.Contains((Point)stylusPoints[j]))
							{
								num += pointWeight;
							}
							else
							{
								num -= pointWeight;
							}
						}
					}
				}
				if (num != 0.0 || lasso2 == this._lasso)
				{
					strokeInfo.HitWeight = ((lasso2 == this._lasso) ? num : (strokeInfo.HitWeight + num));
					bool flag = DoubleUtil.GreaterThanOrClose(strokeInfo.HitWeight, strokeInfo.TotalWeight * (double)this._percentIntersect / 100.0 - Stroke.PercentageTolerance);
					if (strokeInfo.IsHit != flag)
					{
						strokeInfo.IsHit = flag;
						if (flag)
						{
							if (strokeCollection == null)
							{
								strokeCollection = new StrokeCollection();
							}
							strokeCollection.Add(strokeInfo.Stroke);
						}
						else
						{
							if (strokeCollection2 == null)
							{
								strokeCollection2 = new StrokeCollection();
							}
							strokeCollection2.Add(strokeInfo.Stroke);
						}
					}
				}
			}
			this._lasso.IsIncrementalLassoDirty = false;
			if (strokeCollection != null || strokeCollection2 != null)
			{
				this.OnSelectionChanged(new LassoSelectionChangedEventArgs(strokeCollection, strokeCollection2));
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.IncrementalLassoHitTester.SelectionChanged" />.</summary>
		/// <param name="eventArgs">Dados do evento.</param>
		// Token: 0x06001CC9 RID: 7369 RVA: 0x00075390 File Offset: 0x00074790
		protected void OnSelectionChanged(LassoSelectionChangedEventArgs eventArgs)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, eventArgs);
			}
		}

		// Token: 0x04000F9D RID: 3997
		private Lasso _lasso;

		// Token: 0x04000F9E RID: 3998
		private int _percentIntersect;
	}
}
