using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int64" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000550 RID: 1360
	public class SplineInt64KeyFrame : Int64KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt64KeyFrame" />.</summary>
		// Token: 0x06003E90 RID: 16016 RVA: 0x000F630C File Offset: 0x000F570C
		public SplineInt64KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt64KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E91 RID: 16017 RVA: 0x000F6320 File Offset: 0x000F5720
		public SplineInt64KeyFrame(long value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt64KeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E92 RID: 16018 RVA: 0x000F633C File Offset: 0x000F573C
		public SplineInt64KeyFrame(long value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt64KeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E93 RID: 16019 RVA: 0x000F6360 File Offset: 0x000F5760
		public SplineInt64KeyFrame(long value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineInt64KeyFrame" />.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Animation.SplineInt64KeyFrame" /> recém-criada.</returns>
		// Token: 0x06003E94 RID: 16020 RVA: 0x000F6398 File Offset: 0x000F5798
		protected override Freezable CreateInstanceCore()
		{
			return new SplineInt64KeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor do qual animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E95 RID: 16021 RVA: 0x000F63AC File Offset: 0x000F57AC
		protected override long InterpolateValueCore(long baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateInt64(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x000F63F4 File Offset: 0x000F57F4
		// (set) Token: 0x06003E97 RID: 16023 RVA: 0x000F6414 File Offset: 0x000F5814
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineInt64KeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineInt64KeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineInt64KeyFrame.KeySpline" />.</summary>
		// Token: 0x04001751 RID: 5969
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineInt64KeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
