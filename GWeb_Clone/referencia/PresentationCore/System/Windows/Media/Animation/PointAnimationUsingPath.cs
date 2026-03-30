using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Point" /> entre dois ou mais valores de destino usando um <see cref="T:System.Windows.Media.PathGeometry" /> para especificar esses valores. Essa animação pode ser usada para mover um objeto visual ao longo de um caminho.</summary>
	// Token: 0x02000576 RID: 1398
	public class PointAnimationUsingPath : PointAnimationBase
	{
		/// <summary>Especifica a geometria usada para gerar valores de saída desta animação.</summary>
		/// <returns>Valores de saída do caminho usado para gerar essa animação. O valor padrão é nulo.</returns>
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x000FDD28 File Offset: 0x000FD128
		// (set) Token: 0x060040BC RID: 16572 RVA: 0x000FDD48 File Offset: 0x000FD148
		public PathGeometry PathGeometry
		{
			get
			{
				return (PathGeometry)base.GetValue(PointAnimationUsingPath.PathGeometryProperty);
			}
			set
			{
				base.SetValue(PointAnimationUsingPath.PathGeometryProperty, value);
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.PointAnimationUsingPath" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060040BD RID: 16573 RVA: 0x000FDD64 File Offset: 0x000FD164
		public new PointAnimationUsingPath Clone()
		{
			return (PointAnimationUsingPath)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.PointAnimationUsingPath" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060040BE RID: 16574 RVA: 0x000FDD7C File Offset: 0x000FD17C
		protected override Freezable CreateInstanceCore()
		{
			return new PointAnimationUsingPath();
		}

		/// <summary>Chamado quando este <see cref="T:System.Windows.Media.Animation.PointAnimationUsingPath" /> é modificado.</summary>
		// Token: 0x060040BF RID: 16575 RVA: 0x000FDD90 File Offset: 0x000FD190
		protected override void OnChanged()
		{
			this._isValid = false;
			base.OnChanged();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.PointAnimationUsingPath" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x060040C0 RID: 16576 RVA: 0x000FDDAC File Offset: 0x000FD1AC
		protected override Point GetCurrentValueCore(Point defaultOriginValue, Point defaultDestinationValue, AnimationClock animationClock)
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
			double num = (double)(animationClock.CurrentIteration - 1).Value;
			if (this.IsCumulative && num > 0.0)
			{
				point += this._accumulatingVector * num;
			}
			if (this.IsAdditive)
			{
				return defaultOriginValue + (Vector)point;
			}
			return point;
		}

		/// <summary>Obtém um valor que especifica se o valor de saída da animação é adicionado ao valor base da propriedade que está sendo animada.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação adicionará o valor de saída para o valor base da propriedade sendo animada, em vez de substituí-la; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x000FDE60 File Offset: 0x000FD260
		// (set) Token: 0x060040C2 RID: 16578 RVA: 0x000FDE80 File Offset: 0x000FD280
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
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x000FDE9C File Offset: 0x000FD29C
		// (set) Token: 0x060040C4 RID: 16580 RVA: 0x000FDEBC File Offset: 0x000FD2BC
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

		// Token: 0x060040C5 RID: 16581 RVA: 0x000FDED8 File Offset: 0x000FD2D8
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
				this._accumulatingVector.X = point3.X - point.X;
				this._accumulatingVector.Y = point3.Y - point.Y;
			}
			this._isValid = true;
		}

		// Token: 0x040017B3 RID: 6067
		private bool _isValid;

		// Token: 0x040017B4 RID: 6068
		private Vector _accumulatingVector;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PointAnimationUsingPath.PathGeometry" />.</summary>
		// Token: 0x040017B5 RID: 6069
		public static readonly DependencyProperty PathGeometryProperty = DependencyProperty.Register("PathGeometry", typeof(PathGeometry), typeof(PointAnimationUsingPath), new PropertyMetadata(null));
	}
}
