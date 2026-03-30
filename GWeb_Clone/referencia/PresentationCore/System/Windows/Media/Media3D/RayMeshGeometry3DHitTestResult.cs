using System;

namespace System.Windows.Media.Media3D
{
	/// <summary>Representa uma interseção entre um teste de clique de raio e um <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" />.</summary>
	// Token: 0x0200047A RID: 1146
	public sealed class RayMeshGeometry3DHitTestResult : RayHitTestResult
	{
		// Token: 0x06003111 RID: 12561 RVA: 0x000C43A0 File Offset: 0x000C37A0
		internal RayMeshGeometry3DHitTestResult(Visual3D visualHit, Model3D modelHit, MeshGeometry3D meshHit, Point3D pointHit, double distanceToRayOrigin, int vertexIndex1, int vertexIndex2, int vertexIndex3, Point barycentricCoordinate) : base(visualHit, modelHit)
		{
			this._meshHit = meshHit;
			this._pointHit = pointHit;
			this._distanceToRayOrigin = distanceToRayOrigin;
			this._vertexIndex1 = vertexIndex1;
			this._vertexIndex2 = vertexIndex2;
			this._vertexIndex3 = vertexIndex3;
			this._barycentricCoordinate = barycentricCoordinate;
		}

		/// <summary>Obtém o ponto no qual a malha foi interseccionada pelo raio ao longo do qual o teste de clique foi executado.</summary>
		/// <returns>Point3D na qual a malha foi interseccionada.</returns>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000C43EC File Offset: 0x000C37EC
		public override Point3D PointHit
		{
			get
			{
				return this._pointHit;
			}
		}

		/// <summary>Obtém a distância entre o ponto de interseção e a origem do raio no espaço de coordenada do <see cref="T:System.Windows.Media.Media3D.Visual3D" /> que iniciou o teste de clique.</summary>
		/// <returns>Duplo que indica a distância entre o ponto de interseção e a origem do raio no espaço de coordenadas de <see cref="T:System.Windows.Media.Media3D.Visual3D" /> que iniciou o teste de clique.</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000C4400 File Offset: 0x000C3800
		public override double DistanceToRayOrigin
		{
			get
			{
				return this._distanceToRayOrigin;
			}
		}

		/// <summary>Primeiro vértice do triângulo de malha interseccionado pelo raio.</summary>
		/// <returns>O índice do primeiro vértice.</returns>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000C4414 File Offset: 0x000C3814
		public int VertexIndex1
		{
			get
			{
				return this._vertexIndex1;
			}
		}

		/// <summary>Segundo vértice do triângulo de malha interseccionado pelo raio.</summary>
		/// <returns>O índice do segundo vértice.</returns>
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000C4428 File Offset: 0x000C3828
		public int VertexIndex2
		{
			get
			{
				return this._vertexIndex2;
			}
		}

		/// <summary>Terceiro vértice do triângulo de malha interseccionado pelo raio.</summary>
		/// <returns>O índice do terceiro vértice.</returns>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000C443C File Offset: 0x000C383C
		public int VertexIndex3
		{
			get
			{
				return this._vertexIndex3;
			}
		}

		/// <summary>Contribuição relativa do primeiro vértice de um triângulo de malha para o ponto no qual esse triângulo foi interseccionado pelo teste de clique, expresso como um valor entre zero e 1.</summary>
		/// <returns>A importância do primeiro vértice.</returns>
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000C4450 File Offset: 0x000C3850
		public double VertexWeight1
		{
			get
			{
				return 1.0 - this.VertexWeight2 - this.VertexWeight3;
			}
		}

		/// <summary>Contribuição relativa do segundo vértice de um triângulo de malha para o ponto no qual esse triângulo foi interseccionado pelo teste de clique, expresso como um valor entre zero e 1.</summary>
		/// <returns>A importância do segundo vértice.</returns>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000C4474 File Offset: 0x000C3874
		public double VertexWeight2
		{
			get
			{
				return this._barycentricCoordinate.X;
			}
		}

		/// <summary>Contribuição relativa do terceiro vértice de um triângulo de malha para o ponto no qual esse triângulo foi interseccionado pelo teste de clique, expresso como um valor entre zero e 1.</summary>
		/// <returns>A importância do terceiro vértice.</returns>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000C4490 File Offset: 0x000C3890
		public double VertexWeight3
		{
			get
			{
				return this._barycentricCoordinate.Y;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" /> interseccionado por este teste de clique.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Media3D.MeshGeometry3D" /> interseccionado pelo raio.</returns>
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000C44AC File Offset: 0x000C38AC
		public MeshGeometry3D MeshHit
		{
			get
			{
				return this._meshHit;
			}
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000C44C0 File Offset: 0x000C38C0
		internal override void SetDistanceToRayOrigin(double distance)
		{
			this._distanceToRayOrigin = distance;
		}

		// Token: 0x04001577 RID: 5495
		private double _distanceToRayOrigin;

		// Token: 0x04001578 RID: 5496
		private readonly int _vertexIndex1;

		// Token: 0x04001579 RID: 5497
		private readonly int _vertexIndex2;

		// Token: 0x0400157A RID: 5498
		private readonly int _vertexIndex3;

		// Token: 0x0400157B RID: 5499
		private readonly Point _barycentricCoordinate;

		// Token: 0x0400157C RID: 5500
		private readonly MeshGeometry3D _meshHit;

		// Token: 0x0400157D RID: 5501
		private readonly Point3D _pointHit;
	}
}
