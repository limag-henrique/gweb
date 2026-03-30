using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000559 RID: 1369
	public class SplineVector3DKeyFrame : Vector3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVector3DKeyFrame" />.</summary>
		// Token: 0x06003EE3 RID: 16099 RVA: 0x000F7048 File Offset: 0x000F6448
		public SplineVector3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVector3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003EE4 RID: 16100 RVA: 0x000F705C File Offset: 0x000F645C
		public SplineVector3DKeyFrame(Vector3D value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVector3DKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003EE5 RID: 16101 RVA: 0x000F7078 File Offset: 0x000F6478
		public SplineVector3DKeyFrame(Vector3D value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineVector3DKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003EE6 RID: 16102 RVA: 0x000F709C File Offset: 0x000F649C
		public SplineVector3DKeyFrame(Vector3D value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineVector3DKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineVector3DKeyFrame" />.</returns>
		// Token: 0x06003EE7 RID: 16103 RVA: 0x000F70D4 File Offset: 0x000F64D4
		protected override Freezable CreateInstanceCore()
		{
			return new SplineVector3DKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003EE8 RID: 16104 RVA: 0x000F70E8 File Offset: 0x000F64E8
		protected override Vector3D InterpolateValueCore(Vector3D baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateVector3D(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000F7130 File Offset: 0x000F6530
		// (set) Token: 0x06003EEA RID: 16106 RVA: 0x000F7150 File Offset: 0x000F6550
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineVector3DKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineVector3DKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineVector3DKeyFrame.KeySpline" />.</summary>
		// Token: 0x0400175B RID: 5979
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineVector3DKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
