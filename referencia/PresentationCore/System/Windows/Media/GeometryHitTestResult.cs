using System;

namespace System.Windows.Media
{
	/// <summary>Retorna os resultados de um teste de clique que usa um <see cref="T:System.Windows.Media.Geometry" /> como um parâmetro de teste de clique.</summary>
	// Token: 0x02000405 RID: 1029
	public class GeometryHitTestResult : HitTestResult
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeometryHitTestResult" /> usando um objeto visual especificado e o valor <see cref="T:System.Windows.Media.IntersectionDetail" />.</summary>
		/// <param name="visualHit">O objeto visual que é acessado durante um teste de clique.</param>
		/// <param name="intersectionDetail">Descreve a interseção entre um <see cref="T:System.Windows.Media.Geometry" /> e <paramref name="visualHit" />.</param>
		// Token: 0x06002925 RID: 10533 RVA: 0x000A4C98 File Offset: 0x000A4098
		public GeometryHitTestResult(Visual visualHit, IntersectionDetail intersectionDetail) : base(visualHit)
		{
			this._intersectionDetail = intersectionDetail;
		}

		/// <summary>Obtém o valor <see cref="T:System.Windows.Media.IntersectionDetail" /> do teste de clique.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.IntersectionDetail" /> valor do teste de clique.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x000A4CB4 File Offset: 0x000A40B4
		public IntersectionDetail IntersectionDetail
		{
			get
			{
				return this._intersectionDetail;
			}
		}

		/// <summary>Obtém o objeto visual retornado de um resultado do teste de clique.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Visual" /> objeto que representa o resultado de teste de clique.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000A4CC8 File Offset: 0x000A40C8
		public new Visual VisualHit
		{
			get
			{
				return (Visual)base.VisualHit;
			}
		}

		// Token: 0x040012C6 RID: 4806
		private IntersectionDetail _intersectionDetail;
	}
}
