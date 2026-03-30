using System;
using System.Collections.Generic;
using MS.Internal.Media3D;

namespace System.Windows.Media.Media3D
{
	/// <summary>Especifica os parâmetros de um teste de clique ao longo de um raio.</summary>
	// Token: 0x02000479 RID: 1145
	public sealed class RayHitTestParameters : HitTestParameters3D
	{
		/// <summary>Cria uma instância do objeto que especifica a origem e a direção do raio ao longo do qual realizar o teste de clique.</summary>
		/// <param name="origin">Point3D na qual o raio se origina.</param>
		/// <param name="direction">Vector3D que indica a direção do raio.</param>
		// Token: 0x06003108 RID: 12552 RVA: 0x000C4190 File Offset: 0x000C3590
		public RayHitTestParameters(Point3D origin, Vector3D direction)
		{
			this._origin = origin;
			this._direction = direction;
			this._isRay = true;
		}

		/// <summary>Obtém a origem do raio ao longo do qual realizar o teste de clique.</summary>
		/// <returns>Teste de origem do raio ao longo do qual atingido.</returns>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000C41C4 File Offset: 0x000C35C4
		public Point3D Origin
		{
			get
			{
				return this._origin;
			}
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Media.Media3D.Vector3D" /> que indica a direção (de sua origem) do raio ao longo do qual realizar o teste de clique.</summary>
		/// <returns>Vector3D que indica a direção do raio ao longo do qual realizar teste de clique.</returns>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000C41D8 File Offset: 0x000C35D8
		public Vector3D Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000C41EC File Offset: 0x000C35EC
		internal void ReportResult(MeshGeometry3D meshHit, Point3D pointHit, double distanceToRayOrigin, int vertexIndex1, int vertexIndex2, int vertexIndex3, Point barycentric)
		{
			this.results.Add(new RayMeshGeometry3DHitTestResult(this.CurrentVisual, this.CurrentModel, meshHit, pointHit, distanceToRayOrigin, vertexIndex1, vertexIndex2, vertexIndex3, barycentric));
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000C4220 File Offset: 0x000C3620
		internal HitTestResultBehavior RaiseCallback(HitTestResultCallback resultCallback, HitTestFilterCallback filterCallback, HitTestResultBehavior lastResult)
		{
			return this.RaiseCallback(resultCallback, filterCallback, lastResult, 0.0);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000C4240 File Offset: 0x000C3640
		internal HitTestResultBehavior RaiseCallback(HitTestResultCallback resultCallback, HitTestFilterCallback filterCallback, HitTestResultBehavior lastResult, double distanceAdjustment)
		{
			this.results.Sort(new Comparison<RayHitTestResult>(RayHitTestResult.CompareByDistanceToRayOrigin));
			int i = 0;
			int count = this.results.Count;
			while (i < count)
			{
				RayHitTestResult rayHitTestResult = this.results[i];
				rayHitTestResult.SetDistanceToRayOrigin(rayHitTestResult.DistanceToRayOrigin + distanceAdjustment);
				Viewport2DVisual3D viewport2DVisual3D = rayHitTestResult.VisualHit as Viewport2DVisual3D;
				if (viewport2DVisual3D != null)
				{
					Visual visual = viewport2DVisual3D.Visual;
					Point uv;
					if (visual != null && Viewport2DVisual3D.GetIntersectionInfo(rayHitTestResult, out uv))
					{
						Point inPoint = Viewport2DVisual3D.TextureCoordsToVisualCoords(uv, visual);
						GeneralTransform inverse = visual.TransformToOuterSpace().Inverse;
						Point point;
						if (inverse != null && inverse.TryTransform(inPoint, out point) && visual.HitTestPoint(filterCallback, resultCallback, new PointHitTestParameters(point)) == HitTestResultBehavior.Stop)
						{
							return HitTestResultBehavior.Stop;
						}
					}
				}
				if (resultCallback(this.results[i]) == HitTestResultBehavior.Stop)
				{
					return HitTestResultBehavior.Stop;
				}
				i++;
			}
			return lastResult;
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000C4320 File Offset: 0x000C3720
		internal void GetLocalLine(out Point3D origin, out Vector3D direction)
		{
			origin = this._origin;
			direction = this._direction;
			bool flag = true;
			if (base.HasWorldTransformMatrix)
			{
				LineUtil.Transform(base.WorldTransformMatrix, ref origin, ref direction, out flag);
			}
			this._isRay = (this._isRay && flag);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000C436C File Offset: 0x000C376C
		internal void ClearResults()
		{
			if (this.results != null)
			{
				this.results.Clear();
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000C438C File Offset: 0x000C378C
		internal bool IsRay
		{
			get
			{
				return this._isRay;
			}
		}

		// Token: 0x04001573 RID: 5491
		private readonly Point3D _origin;

		// Token: 0x04001574 RID: 5492
		private readonly Vector3D _direction;

		// Token: 0x04001575 RID: 5493
		private readonly List<RayHitTestResult> results = new List<RayHitTestResult>();

		// Token: 0x04001576 RID: 5494
		private bool _isRay;
	}
}
