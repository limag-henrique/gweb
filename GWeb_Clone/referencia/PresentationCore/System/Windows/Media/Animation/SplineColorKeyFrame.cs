using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Color" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x0200054B RID: 1355
	public class SplineColorKeyFrame : ColorKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineColorKeyFrame" />.</summary>
		// Token: 0x06003E63 RID: 15971 RVA: 0x000F5BF0 File Offset: 0x000F4FF0
		public SplineColorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineColorKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E64 RID: 15972 RVA: 0x000F5C04 File Offset: 0x000F5004
		public SplineColorKeyFrame(Color value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineColorKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E65 RID: 15973 RVA: 0x000F5C20 File Offset: 0x000F5020
		public SplineColorKeyFrame(Color value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineColorKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E66 RID: 15974 RVA: 0x000F5C44 File Offset: 0x000F5044
		public SplineColorKeyFrame(Color value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineColorKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003E67 RID: 15975 RVA: 0x000F5C7C File Offset: 0x000F507C
		protected override Freezable CreateInstanceCore()
		{
			return new SplineColorKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E68 RID: 15976 RVA: 0x000F5C90 File Offset: 0x000F5090
		protected override Color InterpolateValueCore(Color baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateColor(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x000F5CD8 File Offset: 0x000F50D8
		// (set) Token: 0x06003E6A RID: 15978 RVA: 0x000F5CF8 File Offset: 0x000F50F8
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineColorKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineColorKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineColorKeyFrame.KeySpline" />.</summary>
		// Token: 0x0400174C RID: 5964
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineColorKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
