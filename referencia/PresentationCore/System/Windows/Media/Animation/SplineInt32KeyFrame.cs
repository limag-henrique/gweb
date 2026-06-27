using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int32" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x0200054F RID: 1359
	public class SplineInt32KeyFrame : Int32KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt32KeyFrame" />.</summary>
		// Token: 0x06003E87 RID: 16007 RVA: 0x000F61A0 File Offset: 0x000F55A0
		public SplineInt32KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt32KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E88 RID: 16008 RVA: 0x000F61B4 File Offset: 0x000F55B4
		public SplineInt32KeyFrame(int value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt32KeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E89 RID: 16009 RVA: 0x000F61D0 File Offset: 0x000F55D0
		public SplineInt32KeyFrame(int value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt32KeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E8A RID: 16010 RVA: 0x000F61F4 File Offset: 0x000F55F4
		public SplineInt32KeyFrame(int value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineInt32KeyFrame" />.</summary>
		/// <returns>Uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineInt32KeyFrame" />.</returns>
		// Token: 0x06003E8B RID: 16011 RVA: 0x000F622C File Offset: 0x000F562C
		protected override Freezable CreateInstanceCore()
		{
			return new SplineInt32KeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E8C RID: 16012 RVA: 0x000F6240 File Offset: 0x000F5640
		protected override int InterpolateValueCore(int baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateInt32(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000F6288 File Offset: 0x000F5688
		// (set) Token: 0x06003E8E RID: 16014 RVA: 0x000F62A8 File Offset: 0x000F56A8
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineInt32KeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineInt32KeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineInt32KeyFrame.KeySpline" />.</summary>
		// Token: 0x04001750 RID: 5968
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineInt32KeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
