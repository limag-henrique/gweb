using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Vector" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000558 RID: 1368
	public class SplineVectorKeyFrame : VectorKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVectorKeyFrame" />.</summary>
		// Token: 0x06003EDA RID: 16090 RVA: 0x000F6EDC File Offset: 0x000F62DC
		public SplineVectorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVectorKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003EDB RID: 16091 RVA: 0x000F6EF0 File Offset: 0x000F62F0
		public SplineVectorKeyFrame(Vector value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVectorKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003EDC RID: 16092 RVA: 0x000F6F0C File Offset: 0x000F630C
		public SplineVectorKeyFrame(Vector value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVectorKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003EDD RID: 16093 RVA: 0x000F6F30 File Offset: 0x000F6330
		public SplineVectorKeyFrame(Vector value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineVectorKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineVectorKeyFrame" />.</returns>
		// Token: 0x06003EDE RID: 16094 RVA: 0x000F6F68 File Offset: 0x000F6368
		protected override Freezable CreateInstanceCore()
		{
			return new SplineVectorKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003EDF RID: 16095 RVA: 0x000F6F7C File Offset: 0x000F637C
		protected override Vector InterpolateValueCore(Vector baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateVector(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06003EE0 RID: 16096 RVA: 0x000F6FC4 File Offset: 0x000F63C4
		// (set) Token: 0x06003EE1 RID: 16097 RVA: 0x000F6FE4 File Offset: 0x000F63E4
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineVectorKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineVectorKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineVectorKeyFrame.KeySpline" />.</summary>
		// Token: 0x0400175A RID: 5978
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineVectorKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
