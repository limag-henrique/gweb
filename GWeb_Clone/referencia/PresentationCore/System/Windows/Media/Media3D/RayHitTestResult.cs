using System;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa o resultado de um teste de clique ao longo de um raio.</summary>
	// Token: 0x02000478 RID: 1144
	public abstract class RayHitTestResult : HitTestResult
	{
		// Token: 0x06003101 RID: 12545 RVA: 0x000C4128 File Offset: 0x000C3528
		internal RayHitTestResult(Visual3D visualHit, Model3D modelHit) : base(visualHit)
		{
			this._modelHit = modelHit;
		}

		/// <summary>Obtém o Visual interseccionado pelo raio ao longo do qual o teste de clique foi executado.</summary>
		/// <returns>Visual3D interseccionado pelo raio.</returns>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000C4144 File Offset: 0x000C3544
		public new Visual3D VisualHit
		{
			get
			{
				return (Visual3D)base.VisualHit;
			}
		}

		/// <summary>Obtém o Model3D interseccionado pelo raio ao longo do qual o teste de clique foi executado.</summary>
		/// <returns>Model3D interseccionado pelo raio.</returns>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x000C415C File Offset: 0x000C355C
		public Model3D ModelHit
		{
			get
			{
				return this._modelHit;
			}
		}

		/// <summary>Obtém o Point3D na interseção entre o raio ao longo do qual o teste de clique foi executado e o objeto de clique.</summary>
		/// <returns>Point3D na qual o objeto de clique foi interseccionado pelo raio.</returns>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003104 RID: 12548
		public abstract Point3D PointHit { get; }

		/// <summary>Obtém a distância entre a interseção de clique e o espaço de coordenada interno do <see cref="T:System.Windows.Media.Media3D.Visual3D" /> que iniciou o teste de clique.</summary>
		/// <returns>Duplo que indica a distância entre a interseção de clique e o espaço de coordenada interno do <see cref="T:System.Windows.Media.Media3D.Visual3D" /> que iniciou o teste de clique.</returns>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003105 RID: 12549
		public abstract double DistanceToRayOrigin { get; }

		// Token: 0x06003106 RID: 12550
		internal abstract void SetDistanceToRayOrigin(double distance);

		// Token: 0x06003107 RID: 12551 RVA: 0x000C4170 File Offset: 0x000C3570
		internal static int CompareByDistanceToRayOrigin(RayHitTestResult x, RayHitTestResult y)
		{
			return Math.Sign(x.DistanceToRayOrigin - y.DistanceToRayOrigin);
		}

		// Token: 0x04001572 RID: 5490
		private readonly Model3D _modelHit;
	}
}
