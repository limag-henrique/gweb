using System;

namespace System.Windows.Ink
{
	/// <summary>Representa o método que manipulará o evento de <see cref="E:System.Windows.Ink.IncrementalStrokeHitTester.StrokeHit" /> de <see cref="T:System.Windows.Ink.IncrementalStrokeHitTester" />.</summary>
	// Token: 0x02000358 RID: 856
	public class StrokeHitEventArgs : EventArgs
	{
		// Token: 0x06001CDA RID: 7386 RVA: 0x000755D4 File Offset: 0x000749D4
		internal StrokeHitEventArgs(Stroke stroke, StrokeIntersection[] hitFragments)
		{
			this._stroke = stroke;
			this._hitFragments = hitFragments;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Ink.Stroke" /> interseccionado pelo caminho de borracha.</summary>
		/// <returns>O <see cref="T:System.Windows.Ink.Stroke" /> interseccionado pelo caminho de borracha.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x000755F8 File Offset: 0x000749F8
		public Stroke HitStroke
		{
			get
			{
				return this._stroke;
			}
		}

		/// <summary>Retorna os traços que são um resultado do caminho da borracha que apaga um traço.</summary>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços criados depois que o caminho de borracha apaga a parte de <see cref="P:System.Windows.Ink.StrokeHitEventArgs.HitStroke" />.</returns>
		// Token: 0x06001CDC RID: 7388 RVA: 0x0007560C File Offset: 0x00074A0C
		public StrokeCollection GetPointEraseResults()
		{
			return this._stroke.Erase(this._hitFragments);
		}

		// Token: 0x04000FA3 RID: 4003
		private Stroke _stroke;

		// Token: 0x04000FA4 RID: 4004
		private StrokeIntersection[] _hitFragments;
	}
}
