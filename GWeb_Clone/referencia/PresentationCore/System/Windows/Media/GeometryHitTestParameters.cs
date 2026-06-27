using System;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Especifica um <see cref="T:System.Windows.Media.Geometry" /> como o parâmetro a ser usado para teste de clique de uma árvore visual.</summary>
	// Token: 0x02000404 RID: 1028
	public class GeometryHitTestParameters : HitTestParameters
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GeometryHitTestParameters" />, usando o <see cref="T:System.Windows.Media.Geometry" /> especificado.</summary>
		/// <param name="geometry">O valor <see cref="T:System.Windows.Media.Geometry" /> a ser usado para a geometria de teste de clique.</param>
		// Token: 0x0600291D RID: 10525 RVA: 0x000A4A64 File Offset: 0x000A3E64
		public GeometryHitTestParameters(Geometry geometry)
		{
			if (geometry == null)
			{
				throw new ArgumentNullException("geometry");
			}
			this._hitGeometryInternal = geometry.GetAsPathGeometry();
			if (this._hitGeometryInternal == geometry)
			{
				this._hitGeometryInternal = this._hitGeometryInternal.Clone();
			}
			Transform transform = this._hitGeometryInternal.Transform;
			MatrixTransform matrixTransform = new MatrixTransform();
			this._hitGeometryInternal.Transform = matrixTransform;
			this._origBounds = this._hitGeometryInternal.Bounds;
			if (transform != null && !transform.IsIdentity)
			{
				matrixTransform.Matrix = transform.Value;
			}
			this._bounds = this._hitGeometryInternal.Bounds;
			this._matrixStack = new MatrixStack();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.Geometry" /> que define a geometria de teste de clique para esta instância de <see cref="T:System.Windows.Media.GeometryHitTestParameters" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Geometry" /> que define a região de teste de clique.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000A4B10 File Offset: 0x000A3F10
		public Geometry HitGeometry
		{
			get
			{
				if (this._hitGeometryCache == null)
				{
					this._hitGeometryCache = (Geometry)this._hitGeometryInternal.GetAsFrozen();
				}
				return this._hitGeometryCache;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x000A4B44 File Offset: 0x000A3F44
		internal PathGeometry InternalHitGeometry
		{
			get
			{
				return this._hitGeometryInternal;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000A4B58 File Offset: 0x000A3F58
		internal Rect Bounds
		{
			get
			{
				return this._bounds;
			}
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000A4B6C File Offset: 0x000A3F6C
		internal void PushMatrix(ref Matrix newMatrix)
		{
			MatrixTransform matrixTransform = (MatrixTransform)this._hitGeometryInternal.Transform;
			Matrix value = matrixTransform.Value;
			this._matrixStack.Push(ref value, false);
			MatrixUtil.MultiplyMatrix(ref value, ref newMatrix);
			matrixTransform.Matrix = value;
			this._bounds = Rect.Transform(this._origBounds, value);
			this.ClearHitGeometryCache();
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000A4BC8 File Offset: 0x000A3FC8
		internal void PopMatrix()
		{
			Matrix matrix = this._matrixStack.Peek();
			((MatrixTransform)this._hitGeometryInternal.Transform).Matrix = matrix;
			this._bounds = Rect.Transform(this._origBounds, matrix);
			this._matrixStack.Pop();
			this.ClearHitGeometryCache();
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000A4C1C File Offset: 0x000A401C
		internal void EmergencyRestoreOriginalTransform()
		{
			Matrix matrix = ((MatrixTransform)this._hitGeometryInternal.Transform).Matrix;
			while (!this._matrixStack.IsEmpty)
			{
				matrix = this._matrixStack.Peek();
				this._matrixStack.Pop();
			}
			((MatrixTransform)this._hitGeometryInternal.Transform).Matrix = matrix;
			this.ClearHitGeometryCache();
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000A4C84 File Offset: 0x000A4084
		private void ClearHitGeometryCache()
		{
			this._hitGeometryCache = null;
		}

		// Token: 0x040012C1 RID: 4801
		private PathGeometry _hitGeometryInternal;

		// Token: 0x040012C2 RID: 4802
		private Geometry _hitGeometryCache;

		// Token: 0x040012C3 RID: 4803
		private Rect _origBounds;

		// Token: 0x040012C4 RID: 4804
		private Rect _bounds;

		// Token: 0x040012C5 RID: 4805
		private MatrixStack _matrixStack;
	}
}
