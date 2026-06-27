using System;

namespace System.Windows.Ink
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Ink.IncrementalLassoHitTester.SelectionChanged" /> .</summary>
	// Token: 0x02000357 RID: 855
	public class LassoSelectionChangedEventArgs : EventArgs
	{
		// Token: 0x06001CD7 RID: 7383 RVA: 0x00075550 File Offset: 0x00074950
		internal LassoSelectionChangedEventArgs(StrokeCollection selectedStrokes, StrokeCollection deselectedStrokes)
		{
			this._selectedStrokes = selectedStrokes;
			this._deselectedStrokes = deselectedStrokes;
		}

		/// <summary>Obtém os traços que foram cercados pelo caminho do Laço desde a última vez que o evento <see cref="E:System.Windows.Ink.IncrementalLassoHitTester.SelectionChanged" /> foi acionado.</summary>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços que foram cercados pelo caminho do Laço desde a última vez o <see cref="E:System.Windows.Ink.IncrementalLassoHitTester.SelectionChanged" /> evento foi gerado.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00075574 File Offset: 0x00074974
		public StrokeCollection SelectedStrokes
		{
			get
			{
				if (this._selectedStrokes != null)
				{
					return new StrokeCollection
					{
						this._selectedStrokes
					};
				}
				return new StrokeCollection();
			}
		}

		/// <summary>Obtém os traços que foram removidos do caminho do Laço desde a última vez que o evento <see cref="E:System.Windows.Ink.IncrementalLassoHitTester.SelectionChanged" /> foi acionado.</summary>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços que foram removidos do caminho do Laço desde a última vez o <see cref="E:System.Windows.Ink.IncrementalLassoHitTester.SelectionChanged" /> evento foi gerado.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x000755A4 File Offset: 0x000749A4
		public StrokeCollection DeselectedStrokes
		{
			get
			{
				if (this._deselectedStrokes != null)
				{
					return new StrokeCollection
					{
						this._deselectedStrokes
					};
				}
				return new StrokeCollection();
			}
		}

		// Token: 0x04000FA1 RID: 4001
		private StrokeCollection _selectedStrokes;

		// Token: 0x04000FA2 RID: 4002
		private StrokeCollection _deselectedStrokes;
	}
}
