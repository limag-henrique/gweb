using System;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x02000553 RID: 1363
	public class SplineQuaternionKeyFrame : QuaternionKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineQuaternionKeyFrame" />.</summary>
		// Token: 0x06003EAB RID: 16043 RVA: 0x000F6750 File Offset: 0x000F5B50
		public SplineQuaternionKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineQuaternionKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		// Token: 0x06003EAC RID: 16044 RVA: 0x000F6764 File Offset: 0x000F5B64
		public SplineQuaternionKeyFrame(Quaternion value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineQuaternionKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		/// <param name="keyTime">O tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003EAD RID: 16045 RVA: 0x000F6780 File Offset: 0x000F5B80
		public SplineQuaternionKeyFrame(Quaternion value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineQuaternionKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		/// <param name="keyTime">O tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">Um <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003EAE RID: 16046 RVA: 0x000F67A4 File Offset: 0x000F5BA4
		public SplineQuaternionKeyFrame(Quaternion value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineQuaternionKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003EAF RID: 16047 RVA: 0x000F67DC File Offset: 0x000F5BDC
		protected override Freezable CreateInstanceCore()
		{
			return new SplineQuaternionKeyFrame();
		}

		/// <summary>Realiza a interpolação spline entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor de 0,0 a 1,0 que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003EB0 RID: 16048 RVA: 0x000F67F0 File Offset: 0x000F5BF0
		protected override Quaternion InterpolateValueCore(Quaternion baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateQuaternion(baseValue, base.Value, splineProgress, this.UseShortestPath);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos que especificam a curva de Bézier cúbica que define o progresso de quadro-chave de controle.</returns>
		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003EB1 RID: 16049 RVA: 0x000F6840 File Offset: 0x000F5C40
		// (set) Token: 0x06003EB2 RID: 16050 RVA: 0x000F6860 File Offset: 0x000F5C60
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineQuaternionKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineQuaternionKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se a animação usa interpolação linear esférica para calcular o arco mais curto entre posições.</summary>
		/// <returns>Valor booliano que indica se a animação usa interpolação linear esférica para calcular o arco mais curto entre posições.</returns>
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003EB3 RID: 16051 RVA: 0x000F6888 File Offset: 0x000F5C88
		// (set) Token: 0x06003EB4 RID: 16052 RVA: 0x000F68A8 File Offset: 0x000F5CA8
		public bool UseShortestPath
		{
			get
			{
				return (bool)base.GetValue(SplineQuaternionKeyFrame.UseShortestPathProperty);
			}
			set
			{
				base.SetValue(SplineQuaternionKeyFrame.UseShortestPathProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineQuaternionKeyFrame.KeySpline" />.</summary>
		// Token: 0x04001754 RID: 5972
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineQuaternionKeyFrame), new PropertyMetadata(new KeySpline()));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineQuaternionKeyFrame.UseShortestPath" />.</summary>
		// Token: 0x04001755 RID: 5973
		public static readonly DependencyProperty UseShortestPathProperty = DependencyProperty.Register("UseShortestPath", typeof(bool), typeof(SplineQuaternionKeyFrame), new PropertyMetadata(BooleanBoxes.TrueBox));
	}
}
