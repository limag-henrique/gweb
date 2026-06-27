using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Size" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000557 RID: 1367
	public class SplineSizeKeyFrame : SizeKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSizeKeyFrame" />.</summary>
		// Token: 0x06003ED1 RID: 16081 RVA: 0x000F6D70 File Offset: 0x000F6170
		public SplineSizeKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSizeKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003ED2 RID: 16082 RVA: 0x000F6D84 File Offset: 0x000F6184
		public SplineSizeKeyFrame(Size value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSizeKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003ED3 RID: 16083 RVA: 0x000F6DA0 File Offset: 0x000F61A0
		public SplineSizeKeyFrame(Size value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSizeKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003ED4 RID: 16084 RVA: 0x000F6DC4 File Offset: 0x000F61C4
		public SplineSizeKeyFrame(Size value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineSizeKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003ED5 RID: 16085 RVA: 0x000F6DFC File Offset: 0x000F61FC
		protected override Freezable CreateInstanceCore()
		{
			return new SplineSizeKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003ED6 RID: 16086 RVA: 0x000F6E10 File Offset: 0x000F6210
		protected override Size InterpolateValueCore(Size baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateSize(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000F6E58 File Offset: 0x000F6258
		// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000F6E78 File Offset: 0x000F6278
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineSizeKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineSizeKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineSizeKeyFrame.KeySpline" />.</summary>
		// Token: 0x04001759 RID: 5977
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineSizeKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
