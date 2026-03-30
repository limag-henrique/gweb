using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Single" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000556 RID: 1366
	public class SplineSingleKeyFrame : SingleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSingleKeyFrame" />.</summary>
		// Token: 0x06003EC8 RID: 16072 RVA: 0x000F6C04 File Offset: 0x000F6004
		public SplineSingleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSingleKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003EC9 RID: 16073 RVA: 0x000F6C18 File Offset: 0x000F6018
		public SplineSingleKeyFrame(float value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSingleKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003ECA RID: 16074 RVA: 0x000F6C34 File Offset: 0x000F6034
		public SplineSingleKeyFrame(float value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSingleKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003ECB RID: 16075 RVA: 0x000F6C58 File Offset: 0x000F6058
		public SplineSingleKeyFrame(float value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSingleKeyFrame" />.</summary>
		/// <returns>Uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineSingleKeyFrame" />.</returns>
		// Token: 0x06003ECC RID: 16076 RVA: 0x000F6C90 File Offset: 0x000F6090
		protected override Freezable CreateInstanceCore()
		{
			return new SplineSingleKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor do qual animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003ECD RID: 16077 RVA: 0x000F6CA4 File Offset: 0x000F60A4
		protected override float InterpolateValueCore(float baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateSingle(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x000F6CEC File Offset: 0x000F60EC
		// (set) Token: 0x06003ECF RID: 16079 RVA: 0x000F6D0C File Offset: 0x000F610C
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineSingleKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineSingleKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineSingleKeyFrame.KeySpline" />.</summary>
		// Token: 0x04001758 RID: 5976
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineSingleKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
