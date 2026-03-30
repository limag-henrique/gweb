using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Media.Matrix" /> usando um <see cref="T:System.Windows.Media.PathGeometry" /> para gerar os valores animados. Essa animação pode ser usada para mover um objeto visual ao longo de um caminho.</summary>
	// Token: 0x02000574 RID: 1396
	public class MatrixAnimationUsingPath : MatrixAnimationBase
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.MatrixAnimationUsingPath" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060040AA RID: 16554 RVA: 0x000FD910 File Offset: 0x000FCD10
		public new MatrixAnimationUsingPath Clone()
		{
			return (MatrixAnimationUsingPath)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.MatrixAnimationUsingPath" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060040AB RID: 16555 RVA: 0x000FD928 File Offset: 0x000FCD28
		protected override Freezable CreateInstanceCore()
		{
			return new MatrixAnimationUsingPath();
		}

		/// <summary>Chamado quando este <see cref="T:System.Windows.Media.Animation.MatrixAnimationUsingPath" /> é modificado.</summary>
		// Token: 0x060040AC RID: 16556 RVA: 0x000FD93C File Offset: 0x000FCD3C
		protected override void OnChanged()
		{
			this._isValid = false;
			base.OnChanged();
		}

		/// <summary>Obtém ou define um valor que indica se o objeto gira ao longo da tangente do caminho.</summary>
		/// <returns>
		///   <see langword="true" /> Se o objeto gira junto da tangente do caminho; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x000FD958 File Offset: 0x000FCD58
		// (set) Token: 0x060040AE RID: 16558 RVA: 0x000FD978 File Offset: 0x000FCD78
		public bool DoesRotateWithTangent
		{
			get
			{
				return (bool)base.GetValue(MatrixAnimationUsingPath.DoesRotateWithTangentProperty);
			}
			set
			{
				base.SetValue(MatrixAnimationUsingPath.DoesRotateWithTangentProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x000FD994 File Offset: 0x000FCD94
		// (set) Token: 0x060040B0 RID: 16560 RVA: 0x000FD9B4 File Offset: 0x000FCDB4
		public bool IsAdditive
		{
			get
			{
				return (bool)base.GetValue(AnimationTimeline.IsAdditiveProperty);
			}
			set
			{
				base.SetValue(AnimationTimeline.IsAdditiveProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica se o ângulo de rotação da matriz animada deve se acumular entre repetições.</summary>
		/// <returns>
		///   <see langword="true" /> Se o ângulo de rotação da animação deve se acumular entre repetições; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x000FD9D0 File Offset: 0x000FCDD0
		// (set) Token: 0x060040B2 RID: 16562 RVA: 0x000FD9F0 File Offset: 0x000FCDF0
		public bool IsAngleCumulative
		{
			get
			{
				return (bool)base.GetValue(MatrixAnimationUsingPath.IsAngleCumulativeProperty);
			}
			set
			{
				base.SetValue(MatrixAnimationUsingPath.IsAngleCumulativeProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o deslocamento produzido pela matriz animada acumula-se entre repetições.</summary>
		/// <returns>
		///   <see langword="true" /> Se o objeto se acumularão ao longo se repete da animação; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x000FDA0C File Offset: 0x000FCE0C
		// (set) Token: 0x060040B4 RID: 16564 RVA: 0x000FDA2C File Offset: 0x000FCE2C
		public bool IsOffsetCumulative
		{
			get
			{
				return (bool)base.GetValue(MatrixAnimationUsingPath.IsOffsetCumulativeProperty);
			}
			set
			{
				base.SetValue(MatrixAnimationUsingPath.IsOffsetCumulativeProperty, value);
			}
		}

		/// <summary>Obtém ou define a geometria usada para gerar valores de saída desta animação.</summary>
		/// <returns>Valores de saída da geometria usada para gerar essa animação. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x000FDA48 File Offset: 0x000FCE48
		// (set) Token: 0x060040B6 RID: 16566 RVA: 0x000FDA68 File Offset: 0x000FCE68
		public PathGeometry PathGeometry
		{
			get
			{
				return (PathGeometry)base.GetValue(MatrixAnimationUsingPath.PathGeometryProperty);
			}
			set
			{
				base.SetValue(MatrixAnimationUsingPath.PathGeometryProperty, value);
			}
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.MatrixAnimationUsingPath" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x060040B7 RID: 16567 RVA: 0x000FDA84 File Offset: 0x000FCE84
		protected override Matrix GetCurrentValueCore(Matrix defaultOriginValue, Matrix defaultDestinationValue, AnimationClock animationClock)
		{
			PathGeometry pathGeometry = this.PathGeometry;
			if (pathGeometry == null)
			{
				return defaultDestinationValue;
			}
			if (!this._isValid)
			{
				this.Validate();
			}
			Point point;
			Point point2;
			pathGeometry.GetPointAtFractionLength(animationClock.CurrentProgress.Value, out point, out point2);
			double num = 0.0;
			if (this.DoesRotateWithTangent)
			{
				num = DoubleAnimationUsingPath.CalculateAngleFromTangentVector(point2.X, point2.Y);
			}
			Matrix matrix = default(Matrix);
			double num2 = (double)(animationClock.CurrentIteration - 1).Value;
			if (num2 > 0.0)
			{
				if (this.IsOffsetCumulative)
				{
					point += this._accumulatingOffset * num2;
				}
				if (this.DoesRotateWithTangent && this.IsAngleCumulative)
				{
					num += this._accumulatingAngle * num2;
				}
			}
			matrix.Rotate(num);
			matrix.Translate(point.X, point.Y);
			if (this.IsAdditive)
			{
				return Matrix.Multiply(matrix, defaultOriginValue);
			}
			return matrix;
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x000FDB9C File Offset: 0x000FCF9C
		private void Validate()
		{
			if (this.IsOffsetCumulative || this.IsAngleCumulative)
			{
				PathGeometry pathGeometry = this.PathGeometry;
				Point point;
				Point point2;
				pathGeometry.GetPointAtFractionLength(0.0, out point, out point2);
				Point point3;
				Point point4;
				pathGeometry.GetPointAtFractionLength(1.0, out point3, out point4);
				this._accumulatingAngle = DoubleAnimationUsingPath.CalculateAngleFromTangentVector(point4.X, point4.Y) - DoubleAnimationUsingPath.CalculateAngleFromTangentVector(point2.X, point2.Y);
				this._accumulatingOffset.X = point3.X - point.X;
				this._accumulatingOffset.Y = point3.Y - point.Y;
			}
			this._isValid = true;
		}

		// Token: 0x040017A8 RID: 6056
		private bool _isValid;

		// Token: 0x040017A9 RID: 6057
		private Vector _accumulatingOffset;

		// Token: 0x040017AA RID: 6058
		private double _accumulatingAngle;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.MatrixAnimationUsingPath.DoesRotateWithTangent" />.</summary>
		// Token: 0x040017AB RID: 6059
		public static readonly DependencyProperty DoesRotateWithTangentProperty = DependencyProperty.Register("DoesRotateWithTangent", typeof(bool), typeof(MatrixAnimationUsingPath), new PropertyMetadata(false));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.MatrixAnimationUsingPath.IsAngleCumulative" />.</summary>
		// Token: 0x040017AC RID: 6060
		public static readonly DependencyProperty IsAngleCumulativeProperty = DependencyProperty.Register("IsAngleCumulative", typeof(bool), typeof(MatrixAnimationUsingPath), new PropertyMetadata(false));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.MatrixAnimationUsingPath.IsOffsetCumulative" />.</summary>
		// Token: 0x040017AD RID: 6061
		public static readonly DependencyProperty IsOffsetCumulativeProperty = DependencyProperty.Register("IsOffsetCumulative", typeof(bool), typeof(MatrixAnimationUsingPath), new PropertyMetadata(false));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.MatrixAnimationUsingPath.PathGeometry" />.</summary>
		// Token: 0x040017AE RID: 6062
		public static readonly DependencyProperty PathGeometryProperty = DependencyProperty.Register("PathGeometry", typeof(PathGeometry), typeof(MatrixAnimationUsingPath), new PropertyMetadata(null));
	}
}
