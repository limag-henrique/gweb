using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int16" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x0200054E RID: 1358
	public class SplineInt16KeyFrame : Int16KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt16KeyFrame" />.</summary>
		// Token: 0x06003E7E RID: 15998 RVA: 0x000F6034 File Offset: 0x000F5434
		public SplineInt16KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt16KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E7F RID: 15999 RVA: 0x000F6048 File Offset: 0x000F5448
		public SplineInt16KeyFrame(short value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt16KeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E80 RID: 16000 RVA: 0x000F6064 File Offset: 0x000F5464
		public SplineInt16KeyFrame(short value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt16KeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E81 RID: 16001 RVA: 0x000F6088 File Offset: 0x000F5488
		public SplineInt16KeyFrame(short value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineInt16KeyFrame" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.SplineInt16KeyFrame" /> recém-criado.</returns>
		// Token: 0x06003E82 RID: 16002 RVA: 0x000F60C0 File Offset: 0x000F54C0
		protected override Freezable CreateInstanceCore()
		{
			return new SplineInt16KeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E83 RID: 16003 RVA: 0x000F60D4 File Offset: 0x000F54D4
		protected override short InterpolateValueCore(short baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateInt16(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000F611C File Offset: 0x000F551C
		// (set) Token: 0x06003E85 RID: 16005 RVA: 0x000F613C File Offset: 0x000F553C
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineInt16KeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineInt16KeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineInt16KeyFrame.KeySpline" />.</summary>
		// Token: 0x0400174F RID: 5967
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineInt16KeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
