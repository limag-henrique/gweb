using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Fornece suporte a transformações de 2D em 3D.</summary>
	// Token: 0x0200045C RID: 1116
	public class GeneralTransform2DTo3D : Freezable
	{
		// Token: 0x06002E6D RID: 11885 RVA: 0x000B9228 File Offset: 0x000B8628
		internal GeneralTransform2DTo3D()
		{
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000B923C File Offset: 0x000B863C
		internal GeneralTransform2DTo3D(GeneralTransform transform2D, Viewport2DVisual3D containingVisual3D, GeneralTransform3D transform3D)
		{
			Visual visual = containingVisual3D.Visual;
			this._transform3D = (GeneralTransform3D)transform3D.GetCurrentValueAsFrozen();
			GeneralTransformGroup generalTransformGroup = new GeneralTransformGroup();
			generalTransformGroup.Children.Add((GeneralTransform)transform2D.GetCurrentValueAsFrozen());
			generalTransformGroup.Children.Add((GeneralTransform)visual.TransformToOuterSpace().GetCurrentValueAsFrozen());
			generalTransformGroup.Freeze();
			this._transform2D = generalTransformGroup;
			this._positions = containingVisual3D.InternalPositionsCache;
			this._textureCoords = containingVisual3D.InternalTextureCoordinatesCache;
			this._triIndices = containingVisual3D.InternalTriangleIndicesCache;
			this._childBounds = visual.CalculateSubgraphRenderBoundsOuterSpace();
		}

		/// <summary>Tenta transformar o ponto especificado e retorna um valor que indica se a transformação foi bem-sucedida.</summary>
		/// <param name="inPoint">O ponto a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002E6F RID: 11887 RVA: 0x000B92DC File Offset: 0x000B86DC
		public bool TryTransform(Point inPoint, out Point3D result)
		{
			result = default(Point3D);
			Point pt;
			if (!this._transform2D.TryTransform(inPoint, out pt))
			{
				return false;
			}
			Point point = Viewport2DVisual3D.VisualCoordsToTextureCoords(pt, this._childBounds);
			Point3D inPoint2;
			return Viewport2DVisual3D.Get3DPointFor2DCoordinate(point, out inPoint2, this._positions, this._textureCoords, this._triIndices) && this._transform3D.TryTransform(inPoint2, out result);
		}

		/// <summary>Transforma o ponto especificado e retorna o resultado.</summary>
		/// <param name="point">O ponto a ser transformado.</param>
		/// <returns>O resultado de transformar <paramref name="point" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transformação não foi bem-sucedida.</exception>
		// Token: 0x06002E70 RID: 11888 RVA: 0x000B9340 File Offset: 0x000B8740
		public Point3D Transform(Point point)
		{
			Point3D result;
			if (!this.TryTransform(point, out result))
			{
				throw new InvalidOperationException(SR.Get("GeneralTransform_TransformFailed", null));
			}
			return result;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06002E71 RID: 11889 RVA: 0x000B936C File Offset: 0x000B876C
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransform2DTo3D();
		}

		/// <summary>Faz com que a instância seja um clone (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06002E72 RID: 11890 RVA: 0x000B9380 File Offset: 0x000B8780
		protected override void CloneCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3D transform = (GeneralTransform2DTo3D)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		/// <summary>Torna a instância um clone modificável (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando os valores de propriedade atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser clonado.</param>
		// Token: 0x06002E73 RID: 11891 RVA: 0x000B93A4 File Offset: 0x000B87A4
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3D transform = (GeneralTransform2DTo3D)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		/// <summary>Torna a instância um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">A instância a ser copiada.</param>
		// Token: 0x06002E74 RID: 11892 RVA: 0x000B93C8 File Offset: 0x000B87C8
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3D transform = (GeneralTransform2DTo3D)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		/// <summary>Torna a instância atual um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado. Se o objeto tiver propriedades de dependência animadas, seus valores animados atuais serão copiados.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser copiado e congelado.</param>
		// Token: 0x06002E75 RID: 11893 RVA: 0x000B93EC File Offset: 0x000B87EC
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform2DTo3D transform = (GeneralTransform2DTo3D)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000B9410 File Offset: 0x000B8810
		private void CopyCommon(GeneralTransform2DTo3D transform)
		{
			this._transform2D = transform._transform2D;
			this._transform3D = transform._transform3D;
			this._positions = transform._positions;
			this._textureCoords = transform._textureCoords;
			this._triIndices = transform._triIndices;
			this._childBounds = transform._childBounds;
		}

		// Token: 0x040014F9 RID: 5369
		private GeneralTransform _transform2D;

		// Token: 0x040014FA RID: 5370
		private GeneralTransform3D _transform3D;

		// Token: 0x040014FB RID: 5371
		private Point3DCollection _positions;

		// Token: 0x040014FC RID: 5372
		private PointCollection _textureCoords;

		// Token: 0x040014FD RID: 5373
		private Int32Collection _triIndices;

		// Token: 0x040014FE RID: 5374
		private Rect _childBounds;
	}
}
