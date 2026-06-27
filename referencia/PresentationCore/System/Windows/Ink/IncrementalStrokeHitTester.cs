using System;
using System.Collections.Generic;
using MS.Internal.Ink;

namespace System.Windows.Ink
{
	/// <summary>Realiza teste de clique dinâmico em um traço com um caminho de borracha.</summary>
	// Token: 0x02000354 RID: 852
	public class IncrementalStrokeHitTester : IncrementalHitTester
	{
		/// <summary>Ocorre quando o <see cref="T:System.Windows.Ink.IncrementalStrokeHitTester" /> intersecciona um <see cref="T:System.Windows.Ink.Stroke" /> de tinta.</summary>
		// Token: 0x1400017E RID: 382
		// (add) Token: 0x06001CCA RID: 7370 RVA: 0x000753B4 File Offset: 0x000747B4
		// (remove) Token: 0x06001CCB RID: 7371 RVA: 0x000753EC File Offset: 0x000747EC
		public event StrokeHitEventHandler StrokeHit;

		// Token: 0x06001CCC RID: 7372 RVA: 0x00075424 File Offset: 0x00074824
		internal IncrementalStrokeHitTester(StrokeCollection strokes, StylusShape eraserShape) : base(strokes)
		{
			this._erasingStroke = new ErasingStroke(eraserShape);
		}

		/// <summary>Adiciona pontos ao <see cref="T:System.Windows.Ink.IncrementalHitTester" />.</summary>
		/// <param name="points">Os pontos a adicionar</param>
		// Token: 0x06001CCD RID: 7373 RVA: 0x00075444 File Offset: 0x00074844
		protected override void AddPointsCore(IEnumerable<Point> points)
		{
			this._erasingStroke.MoveTo(points);
			Rect bounds = this._erasingStroke.Bounds;
			if (bounds.IsEmpty)
			{
				return;
			}
			List<StrokeHitEventArgs> list = null;
			if (this.StrokeHit != null)
			{
				List<StrokeIntersection> list2 = new List<StrokeIntersection>();
				for (int i = 0; i < base.StrokeInfos.Count; i++)
				{
					StrokeInfo strokeInfo = base.StrokeInfos[i];
					if (bounds.IntersectsWith(strokeInfo.StrokeBounds) && this._erasingStroke.EraseTest(StrokeNodeIterator.GetIterator(strokeInfo.Stroke, strokeInfo.Stroke.DrawingAttributes), list2))
					{
						if (list == null)
						{
							list = new List<StrokeHitEventArgs>();
						}
						list.Add(new StrokeHitEventArgs(strokeInfo.Stroke, list2.ToArray()));
						list2.Clear();
					}
				}
			}
			if (list != null)
			{
				for (int j = 0; j < list.Count; j++)
				{
					StrokeHitEventArgs eventArgs = list[j];
					this.OnStrokeHit(eventArgs);
				}
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Ink.IncrementalStrokeHitTester.StrokeHit" />.</summary>
		/// <param name="eventArgs">Dados do evento.</param>
		// Token: 0x06001CCE RID: 7374 RVA: 0x0007552C File Offset: 0x0007492C
		protected void OnStrokeHit(StrokeHitEventArgs eventArgs)
		{
			if (this.StrokeHit != null)
			{
				this.StrokeHit(this, eventArgs);
			}
		}

		// Token: 0x04000FA0 RID: 4000
		private ErasingStroke _erasingStroke;
	}
}
