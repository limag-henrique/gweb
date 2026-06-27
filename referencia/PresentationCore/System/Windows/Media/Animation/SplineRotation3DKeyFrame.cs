using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000554 RID: 1364
	public class SplineRotation3DKeyFrame : Rotation3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRotation3DKeyFrame" />.</summary>
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000F692C File Offset: 0x000F5D2C
		public SplineRotation3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRotation3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003EB7 RID: 16055 RVA: 0x000F6940 File Offset: 0x000F5D40
		public SplineRotation3DKeyFrame(Rotation3D value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRotation3DKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003EB8 RID: 16056 RVA: 0x000F695C File Offset: 0x000F5D5C
		public SplineRotation3DKeyFrame(Rotation3D value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineRotation3DKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003EB9 RID: 16057 RVA: 0x000F6980 File Offset: 0x000F5D80
		public SplineRotation3DKeyFrame(Rotation3D value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineRotation3DKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineRotation3DKeyFrame" />.</returns>
		// Token: 0x06003EBA RID: 16058 RVA: 0x000F69B8 File Offset: 0x000F5DB8
		protected override Freezable CreateInstanceCore()
		{
			return new SplineRotation3DKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003EBB RID: 16059 RVA: 0x000F69CC File Offset: 0x000F5DCC
		protected override Rotation3D InterpolateValueCore(Rotation3D baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateRotation3D(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000F6A14 File Offset: 0x000F5E14
		// (set) Token: 0x06003EBD RID: 16061 RVA: 0x000F6A34 File Offset: 0x000F5E34
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineRotation3DKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineRotation3DKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineRotation3DKeyFrame.KeySpline" />.</summary>
		// Token: 0x04001756 RID: 5974
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineRotation3DKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
