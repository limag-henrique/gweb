using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Double" /> entre dois ou mais valores de destino usando um <see cref="T:System.Windows.Media.PathGeometry" /> para especificar esses valores. Essa animação pode ser usada para mover um objeto visual ao longo de um caminho.</summary>
	// Token: 0x020004AB RID: 1195
	public class DoubleAnimationUsingPath : DoubleAnimationBase
	{
		/// <summary>Especifica a geometria usada para gerar valores de saída desta animação.</summary>
		/// <returns>Valores de saída do caminho usado para gerar essa animação. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000D5D20 File Offset: 0x000D5120
		// (set) Token: 0x0600359C RID: 13724 RVA: 0x000D5D40 File Offset: 0x000D5140
		public PathGeometry PathGeometry
		{
			get
			{
				return (PathGeometry)base.GetValue(DoubleAnimationUsingPath.PathGeometryProperty);
			}
			set
			{
				base.SetValue(DoubleAnimationUsingPath.PathGeometryProperty, value);
			}
		}

		/// <summary>Obtém ou define o aspecto do <see cref="P:System.Windows.Media.Animation.DoubleAnimationUsingPath.PathGeometry" /> desta animação que determina o valor de saída.</summary>
		/// <returns>O aspecto dessa animação <see cref="P:System.Windows.Media.Animation.DoubleAnimationUsingPath.PathGeometry" /> que determina o valor de saída. O valor padrão é <see cref="F:System.Windows.Media.Animation.PathAnimationSource.X" />.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000D5D5C File Offset: 0x000D515C
		// (set) Token: 0x0600359E RID: 13726 RVA: 0x000D5D7C File Offset: 0x000D517C
		public PathAnimationSource Source
		{
			get
			{
				return (PathAnimationSource)base.GetValue(DoubleAnimationUsingPath.SourceProperty);
			}
			set
			{
				base.SetValue(DoubleAnimationUsingPath.SourceProperty, value);
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.DoubleAnimationUsingPath" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600359F RID: 13727 RVA: 0x000D5D9C File Offset: 0x000D519C
		public new DoubleAnimationUsingPath Clone()
		{
			return (DoubleAnimationUsingPath)base.Clone();
		}

		/// <summary>Implementação de <see cref="M:System.Windows.Freezable.CreateInstanceCore" />.</summary>
		/// <returns>O novo <see cref="T:System.Windows.Freezable" /> criado.</returns>
		// Token: 0x060035A0 RID: 13728 RVA: 0x000D5DB4 File Offset: 0x000D51B4
		protected override Freezable CreateInstanceCore()
		{
			return new DoubleAnimationUsingPath();
		}

		/// <summary>Chamado quando este <see cref="T:System.Windows.Media.Animation.DoubleAnimationUsingPath" /> é modificado.</summary>
		// Token: 0x060035A1 RID: 13729 RVA: 0x000D5DC8 File Offset: 0x000D51C8
		protected override void OnChanged()
		{
			this._isValid = false;
			base.OnChanged();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.DoubleAnimationUsingPath" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x060035A2 RID: 13730 RVA: 0x000D5DE4 File Offset: 0x000D51E4
		protected override double GetCurrentValueCore(double defaultOriginValue, double defaultDestinationValue, AnimationClock animationClock)
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
			double num = 0.0;
			Point point;
			Point point2;
			pathGeometry.GetPointAtFractionLength(animationClock.CurrentProgress.Value, out point, out point2);
			switch (this.Source)
			{
			case PathAnimationSource.X:
				num = point.X;
				break;
			case PathAnimationSource.Y:
				num = point.Y;
				break;
			case PathAnimationSource.Angle:
				num = DoubleAnimationUsingPath.CalculateAngleFromTangentVector(point2.X, point2.Y);
				break;
			}
			double num2 = (double)(animationClock.CurrentIteration - 1).Value;
			if (this.IsCumulative && num2 > 0.0)
			{
				num += this._accumulatingValue * num2;
			}
			if (this.IsAdditive)
			{
				return defaultOriginValue + num;
			}
			return num;
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x000D5ED4 File Offset: 0x000D52D4
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x000D5EF4 File Offset: 0x000D52F4
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

		/// <summary>Obtém ou define um valor que especifica se o valor da animação acumula quando ela se repete.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação acumula seus valores quando seu <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> propriedade faz com que ele Repita sua duração simples. Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060035A5 RID: 13733 RVA: 0x000D5F10 File Offset: 0x000D5310
		// (set) Token: 0x060035A6 RID: 13734 RVA: 0x000D5F30 File Offset: 0x000D5330
		public bool IsCumulative
		{
			get
			{
				return (bool)base.GetValue(AnimationTimeline.IsCumulativeProperty);
			}
			set
			{
				base.SetValue(AnimationTimeline.IsCumulativeProperty, value);
			}
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000D5F4C File Offset: 0x000D534C
		private void Validate()
		{
			if (this.IsCumulative)
			{
				PathGeometry pathGeometry = this.PathGeometry;
				Point point;
				Point point2;
				pathGeometry.GetPointAtFractionLength(0.0, out point, out point2);
				Point point3;
				Point point4;
				pathGeometry.GetPointAtFractionLength(1.0, out point3, out point4);
				switch (this.Source)
				{
				case PathAnimationSource.X:
					this._accumulatingValue = point3.X - point.X;
					break;
				case PathAnimationSource.Y:
					this._accumulatingValue = point3.Y - point.Y;
					break;
				case PathAnimationSource.Angle:
					this._accumulatingValue = DoubleAnimationUsingPath.CalculateAngleFromTangentVector(point4.X, point4.Y) - DoubleAnimationUsingPath.CalculateAngleFromTangentVector(point2.X, point2.Y);
					break;
				}
			}
			this._isValid = true;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000D6010 File Offset: 0x000D5410
		internal static double CalculateAngleFromTangentVector(double x, double y)
		{
			double num = Math.Acos(x) * 57.295779513082323;
			if (y < 0.0)
			{
				num = 360.0 - num;
			}
			return num;
		}

		// Token: 0x0400163F RID: 5695
		private bool _isValid;

		// Token: 0x04001640 RID: 5696
		private double _accumulatingValue;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleAnimationUsingPath.PathGeometry" />.</summary>
		// Token: 0x04001641 RID: 5697
		public static readonly DependencyProperty PathGeometryProperty = DependencyProperty.Register("PathGeometry", typeof(PathGeometry), typeof(DoubleAnimationUsingPath), new PropertyMetadata(null));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleAnimationUsingPath.Source" />.</summary>
		// Token: 0x04001642 RID: 5698
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(PathAnimationSource), typeof(DoubleAnimationUsingPath), new PropertyMetadata(PathAnimationSource.X));
	}
}
