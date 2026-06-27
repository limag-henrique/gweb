using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000552 RID: 1362
	public class SplinePoint3DKeyFrame : Point3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePoint3DKeyFrame" />.</summary>
		// Token: 0x06003EA2 RID: 16034 RVA: 0x000F65E4 File Offset: 0x000F59E4
		public SplinePoint3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePoint3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003EA3 RID: 16035 RVA: 0x000F65F8 File Offset: 0x000F59F8
		public SplinePoint3DKeyFrame(Point3D value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePoint3DKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003EA4 RID: 16036 RVA: 0x000F6614 File Offset: 0x000F5A14
		public SplinePoint3DKeyFrame(Point3D value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplinePoint3DKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003EA5 RID: 16037 RVA: 0x000F6638 File Offset: 0x000F5A38
		public SplinePoint3DKeyFrame(Point3D value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplinePoint3DKeyFrame" />.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Animation.SplinePoint3DKeyFrame" /> recém-criada.</returns>
		// Token: 0x06003EA6 RID: 16038 RVA: 0x000F6670 File Offset: 0x000F5A70
		protected override Freezable CreateInstanceCore()
		{
			return new SplinePoint3DKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor do qual animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003EA7 RID: 16039 RVA: 0x000F6684 File Offset: 0x000F5A84
		protected override Point3D InterpolateValueCore(Point3D baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolatePoint3D(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x000F66CC File Offset: 0x000F5ACC
		// (set) Token: 0x06003EA9 RID: 16041 RVA: 0x000F66EC File Offset: 0x000F5AEC
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplinePoint3DKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplinePoint3DKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplinePoint3DKeyFrame.KeySpline" />.</summary>
		// Token: 0x04001753 RID: 5971
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplinePoint3DKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
