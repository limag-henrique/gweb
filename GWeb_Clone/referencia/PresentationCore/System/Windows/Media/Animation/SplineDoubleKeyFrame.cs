using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Double" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x0200054D RID: 1357
	public class SplineDoubleKeyFrame : DoubleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDoubleKeyFrame" />.</summary>
		// Token: 0x06003E75 RID: 15989 RVA: 0x000F5EC8 File Offset: 0x000F52C8
		public SplineDoubleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDoubleKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E76 RID: 15990 RVA: 0x000F5EDC File Offset: 0x000F52DC
		public SplineDoubleKeyFrame(double value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDoubleKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E77 RID: 15991 RVA: 0x000F5EF8 File Offset: 0x000F52F8
		public SplineDoubleKeyFrame(double value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDoubleKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E78 RID: 15992 RVA: 0x000F5F1C File Offset: 0x000F531C
		public SplineDoubleKeyFrame(double value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineDoubleKeyFrame" />.</summary>
		/// <returns>Uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineDoubleKeyFrame" />.</returns>
		// Token: 0x06003E79 RID: 15993 RVA: 0x000F5F54 File Offset: 0x000F5354
		protected override Freezable CreateInstanceCore()
		{
			return new SplineDoubleKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E7A RID: 15994 RVA: 0x000F5F68 File Offset: 0x000F5368
		protected override double InterpolateValueCore(double baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateDouble(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06003E7B RID: 15995 RVA: 0x000F5FB0 File Offset: 0x000F53B0
		// (set) Token: 0x06003E7C RID: 15996 RVA: 0x000F5FD0 File Offset: 0x000F53D0
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineDoubleKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineDoubleKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineDoubleKeyFrame.KeySpline" />.</summary>
		// Token: 0x0400174E RID: 5966
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineDoubleKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
