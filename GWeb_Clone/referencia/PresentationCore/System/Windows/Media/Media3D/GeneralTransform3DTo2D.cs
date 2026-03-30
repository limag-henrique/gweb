using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Media3D
{
	/// <summary>Fornece suporte a transformações de 3D em 2D.</summary>
	// Token: 0x0200045B RID: 1115
	public class GeneralTransform3DTo2D : Freezable
	{
		// Token: 0x06002E62 RID: 11874 RVA: 0x000B9058 File Offset: 0x000B8458
		internal GeneralTransform3DTo2D()
		{
			this._transformBetween2D = null;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000B9074 File Offset: 0x000B8474
		internal GeneralTransform3DTo2D(Matrix3D projectionTransform, GeneralTransform transformBetween2D)
		{
			this._projectionTransform = projectionTransform;
			this._transformBetween2D = (GeneralTransform)transformBetween2D.GetAsFrozen();
		}

		/// <summary>Transforma o ponto 3D especificado e retorna o resultado.</summary>
		/// <param name="inPoint">O ponto 3D a ser transformado.</param>
		/// <param name="result">O resultado de transformar <paramref name="inPoint" />.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="inPoint" /> foi transformado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002E64 RID: 11876 RVA: 0x000B90A0 File Offset: 0x000B84A0
		public bool TryTransform(Point3D inPoint, out Point result)
		{
			bool result2 = false;
			result = default(Point);
			Matrix3D projectionTransform = this._projectionTransform;
			Point3D point3D = this._projectionTransform.Transform(inPoint);
			if (this._transformBetween2D != null)
			{
				result = this._transformBetween2D.Transform(new Point(point3D.X, point3D.Y));
				result2 = true;
			}
			return result2;
		}

		/// <summary>Transforma o ponto 3D especificado e retorna o resultado.</summary>
		/// <param name="point">O ponto 3D a ser transformado.</param>
		/// <returns>O resultado de transformar <paramref name="point" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transformação não foi bem-sucedida.</exception>
		// Token: 0x06002E65 RID: 11877 RVA: 0x000B90FC File Offset: 0x000B84FC
		public Point Transform(Point3D point)
		{
			Point result;
			if (!this.TryTransform(point, out result))
			{
				throw new InvalidOperationException(SR.Get("GeneralTransform_TransformFailed", null));
			}
			return result;
		}

		/// <summary>Transforma a caixa delimitadora 3D especificada e retorna uma caixa delimitadora alinhada por eixo que contém todos os pontos na caixa delimitadora 3D original.</summary>
		/// <param name="rect3D">A caixa delimitadora 3D a ser transformada.</param>
		/// <returns>Uma caixa delimitadora alinhada por eixo que contém todos os pontos na caixa delimitadora 3D especificada.</returns>
		// Token: 0x06002E66 RID: 11878 RVA: 0x000B9128 File Offset: 0x000B8528
		public Rect TransformBounds(Rect3D rect3D)
		{
			if (this._transformBetween2D != null)
			{
				return this._transformBetween2D.TransformBounds(MILUtilities.ProjectBounds(ref this._projectionTransform, ref rect3D));
			}
			return Rect.Empty;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06002E67 RID: 11879 RVA: 0x000B915C File Offset: 0x000B855C
		protected override Freezable CreateInstanceCore()
		{
			return new GeneralTransform3DTo2D();
		}

		/// <summary>Faz com que a instância seja um clone (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">O objeto a ser clonado.</param>
		// Token: 0x06002E68 RID: 11880 RVA: 0x000B9170 File Offset: 0x000B8570
		protected override void CloneCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2D transform = (GeneralTransform3DTo2D)sourceFreezable;
			base.CloneCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		/// <summary>Torna a instância um clone modificável (cópia em profundidade) do <see cref="T:System.Windows.Freezable" /> especificado usando os valores de propriedade atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser clonado.</param>
		// Token: 0x06002E69 RID: 11881 RVA: 0x000B9194 File Offset: 0x000B8594
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2D transform = (GeneralTransform3DTo2D)sourceFreezable;
			base.CloneCurrentValueCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		/// <summary>Torna a instância um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado usando valores de propriedade base (não animados).</summary>
		/// <param name="sourceFreezable">A instância a ser copiada.</param>
		// Token: 0x06002E6A RID: 11882 RVA: 0x000B91B8 File Offset: 0x000B85B8
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2D transform = (GeneralTransform3DTo2D)sourceFreezable;
			base.GetAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		/// <summary>Torna a instância atual um clone congelado do <see cref="T:System.Windows.Freezable" /> especificado. Se o objeto tiver propriedades de dependência animadas, seus valores animados atuais serão copiados.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Freezable" /> a ser copiado e congelado.</param>
		// Token: 0x06002E6B RID: 11883 RVA: 0x000B91DC File Offset: 0x000B85DC
		protected override void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
		{
			GeneralTransform3DTo2D transform = (GeneralTransform3DTo2D)sourceFreezable;
			base.GetCurrentValueAsFrozenCore(sourceFreezable);
			this.CopyCommon(transform);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000B9200 File Offset: 0x000B8600
		private void CopyCommon(GeneralTransform3DTo2D transform)
		{
			this._projectionTransform = transform._projectionTransform;
			this._transformBetween2D = transform._transformBetween2D;
		}

		// Token: 0x040014F7 RID: 5367
		private Matrix3D _projectionTransform;

		// Token: 0x040014F8 RID: 5368
		private GeneralTransform _transformBetween2D;
	}
}
