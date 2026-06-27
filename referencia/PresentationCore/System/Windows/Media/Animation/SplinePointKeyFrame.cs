using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Point" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000551 RID: 1361
	public class SplinePointKeyFrame : PointKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePointKeyFrame" />.</summary>
		// Token: 0x06003E99 RID: 16025 RVA: 0x000F6478 File Offset: 0x000F5878
		public SplinePointKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePointKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E9A RID: 16026 RVA: 0x000F648C File Offset: 0x000F588C
		public SplinePointKeyFrame(Point value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePointKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Key time para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E9B RID: 16027 RVA: 0x000F64A8 File Offset: 0x000F58A8
		public SplinePointKeyFrame(Point value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePointKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Key time para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E9C RID: 16028 RVA: 0x000F64CC File Offset: 0x000F58CC
		public SplinePointKeyFrame(Point value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplinePointKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003E9D RID: 16029 RVA: 0x000F6504 File Offset: 0x000F5904
		protected override Freezable CreateInstanceCore()
		{
			return new SplinePointKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor de 0,0 a 1,0 que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E9E RID: 16030 RVA: 0x000F6518 File Offset: 0x000F5918
		protected override Point InterpolateValueCore(Point baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			double splineProgress = this.KeySpline.GetSplineProgress(keyFrameProgress);
			return AnimatedTypeHelpers.InterpolatePoint(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos que especificam a curva de Bézier cúbica que define o progresso de quadro-chave de controle.</returns>
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000F6560 File Offset: 0x000F5960
		// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x000F6580 File Offset: 0x000F5980
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplinePointKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplinePointKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplinePointKeyFrame.KeySpline" />.</summary>
		// Token: 0x04001752 RID: 5970
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplinePointKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
