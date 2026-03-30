using System;

namespace System.Windows.Media
{
	/// <summary>Especifica um <see cref="T:System.Windows.Point" /> como o parâmetro a ser usado para teste de clique de um objeto visual.</summary>
	// Token: 0x02000432 RID: 1074
	public class PointHitTestParameters : HitTestParameters
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.PointHitTestParameters" /> usando um <see cref="T:System.Windows.Point" />.</summary>
		/// <param name="point">O parâmetro que é especificado como o valor <see cref="T:System.Windows.Point" />.</param>
		// Token: 0x06002C10 RID: 11280 RVA: 0x000B0134 File Offset: 0x000AF534
		public PointHitTestParameters(Point point)
		{
			this._hitPoint = point;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Point" /> no qual realizar o teste de clique.</summary>
		/// <returns>O <see cref="T:System.Windows.Point" /> em relação ao qual o teste de clique.</returns>
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000B0150 File Offset: 0x000AF550
		public Point HitPoint
		{
			get
			{
				return this._hitPoint;
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000B0164 File Offset: 0x000AF564
		internal void SetHitPoint(Point hitPoint)
		{
			this._hitPoint = hitPoint;
		}

		// Token: 0x0400141B RID: 5147
		private Point _hitPoint;
	}
}
