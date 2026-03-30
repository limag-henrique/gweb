using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Decimal" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x0200054C RID: 1356
	public class SplineDecimalKeyFrame : DecimalKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDecimalKeyFrame" />.</summary>
		// Token: 0x06003E6C RID: 15980 RVA: 0x000F5D5C File Offset: 0x000F515C
		public SplineDecimalKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDecimalKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E6D RID: 15981 RVA: 0x000F5D70 File Offset: 0x000F5170
		public SplineDecimalKeyFrame(decimal value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDecimalKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E6E RID: 15982 RVA: 0x000F5D8C File Offset: 0x000F518C
		public SplineDecimalKeyFrame(decimal value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDecimalKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E6F RID: 15983 RVA: 0x000F5DB0 File Offset: 0x000F51B0
		public SplineDecimalKeyFrame(decimal value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineDecimalKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003E70 RID: 15984 RVA: 0x000F5DE8 File Offset: 0x000F51E8
		protected override Freezable CreateInstanceCore()
		{
			return new SplineDecimalKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E71 RID: 15985 RVA: 0x000F5DFC File Offset: 0x000F51FC
		protected override decimal InterpolateValueCore(decimal baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateDecimal(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x000F5E44 File Offset: 0x000F5244
		// (set) Token: 0x06003E73 RID: 15987 RVA: 0x000F5E64 File Offset: 0x000F5264
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineDecimalKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineDecimalKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineDecimalKeyFrame.KeySpline" />.</summary>
		// Token: 0x0400174D RID: 5965
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineDecimalKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
