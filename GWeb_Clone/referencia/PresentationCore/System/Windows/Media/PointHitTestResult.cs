using System;

namespace System.Windows.Media
{
	/// <summary>Representa os resultados de um teste de clique que usa um <see cref="T:System.Windows.Point" /> como um parâmetro de teste de clique.</summary>
	// Token: 0x02000431 RID: 1073
	public class PointHitTestResult : HitTestResult
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PointHitTestResult" />.</summary>
		/// <param name="visualHit">O objeto <see cref="T:System.Windows.Media.Visual" /> que representa o resultado do teste de clique.</param>
		/// <param name="pointHit">O objeto <see cref="T:System.Windows.Point" /> que representa o resultado do teste de clique.</param>
		// Token: 0x06002C0D RID: 11277 RVA: 0x000B00EC File Offset: 0x000AF4EC
		public PointHitTestResult(Visual visualHit, Point pointHit) : base(visualHit)
		{
			this._pointHit = pointHit;
		}

		/// <summary>Obtém o valor de ponto retornado de um resultado do teste de clique.</summary>
		/// <returns>Um <see cref="T:System.Windows.Point" /> objeto que representa o resultado de teste de clique.</returns>
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000B0108 File Offset: 0x000AF508
		public Point PointHit
		{
			get
			{
				return this._pointHit;
			}
		}

		/// <summary>Obtém o objeto visual retornado de um resultado do teste de clique.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Visual" /> objeto que representa o resultado de teste de clique.</returns>
		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000B011C File Offset: 0x000AF51C
		public new Visual VisualHit
		{
			get
			{
				return (Visual)base.VisualHit;
			}
		}

		// Token: 0x0400141A RID: 5146
		private Point _pointHit;
	}
}
