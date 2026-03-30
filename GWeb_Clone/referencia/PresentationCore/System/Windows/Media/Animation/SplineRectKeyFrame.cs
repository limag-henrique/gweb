using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Rect" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000555 RID: 1365
	public class SplineRectKeyFrame : RectKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRectKeyFrame" />.</summary>
		// Token: 0x06003EBF RID: 16063 RVA: 0x000F6A98 File Offset: 0x000F5E98
		public SplineRectKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRectKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003EC0 RID: 16064 RVA: 0x000F6AAC File Offset: 0x000F5EAC
		public SplineRectKeyFrame(Rect value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRectKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003EC1 RID: 16065 RVA: 0x000F6AC8 File Offset: 0x000F5EC8
		public SplineRectKeyFrame(Rect value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRectKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003EC2 RID: 16066 RVA: 0x000F6AEC File Offset: 0x000F5EEC
		public SplineRectKeyFrame(Rect value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineRectKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003EC3 RID: 16067 RVA: 0x000F6B24 File Offset: 0x000F5F24
		protected override Freezable CreateInstanceCore()
		{
			return new SplineRectKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003EC4 RID: 16068 RVA: 0x000F6B38 File Offset: 0x000F5F38
		protected override Rect InterpolateValueCore(Rect baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateRect(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000F6B80 File Offset: 0x000F5F80
		// (set) Token: 0x06003EC6 RID: 16070 RVA: 0x000F6BA0 File Offset: 0x000F5FA0
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineRectKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineRectKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineRectKeyFrame.KeySpline" />.</summary>
		// Token: 0x04001757 RID: 5975
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineRectKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
