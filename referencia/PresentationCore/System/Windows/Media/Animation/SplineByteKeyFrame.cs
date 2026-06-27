using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Byte" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> com interpolação spline.</summary>
	// Token: 0x0200054A RID: 1354
	public class SplineByteKeyFrame : ByteKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineByteKeyFrame" />.</summary>
		// Token: 0x06003E5A RID: 15962 RVA: 0x000F5A84 File Offset: 0x000F4E84
		public SplineByteKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineByteKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003E5B RID: 15963 RVA: 0x000F5A98 File Offset: 0x000F4E98
		public SplineByteKeyFrame(byte value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineByteKeyFrame" /> com o key time e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003E5C RID: 15964 RVA: 0x000F5AB4 File Offset: 0x000F4EB4
		public SplineByteKeyFrame(byte value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SplineByteKeyFrame" /> com o valor de término, o key time e o <see cref="T:System.Windows.Media.Animation.KeySpline" /> especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		/// <param name="keySpline">
		///   <see cref="T:System.Windows.Media.Animation.KeySpline" /> para o quadro-chave. O <see cref="T:System.Windows.Media.Animation.KeySpline" /> representa uma curva de Bézier que define o progresso de animação de quadro-chave.</param>
		// Token: 0x06003E5D RID: 15965 RVA: 0x000F5AD8 File Offset: 0x000F4ED8
		public SplineByteKeyFrame(byte value, KeyTime keyTime, KeySpline keySpline) : this()
		{
			if (keySpline == null)
			{
				throw new ArgumentNullException("keySpline");
			}
			base.Value = value;
			base.KeyTime = keyTime;
			this.KeySpline = keySpline;
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.SplineByteKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003E5E RID: 15966 RVA: 0x000F5B10 File Offset: 0x000F4F10
		protected override Freezable CreateInstanceCore()
		{
			return new SplineByteKeyFrame();
		}

		/// <summary>Usa a interpolação spline para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003E5F RID: 15967 RVA: 0x000F5B24 File Offset: 0x000F4F24
		protected override byte InterpolateValueCore(byte baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateByte(baseValue, base.Value, splineProgress);
		}

		/// <summary>Obtém ou define os dois pontos de controle que definem o andamento da animação desse quadro-chave.</summary>
		/// <returns>Os dois pontos de controle que especificam o Bézier cúbicas curva que define o progresso do quadro chave.</returns>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000F5B6C File Offset: 0x000F4F6C
		// (set) Token: 0x06003E61 RID: 15969 RVA: 0x000F5B8C File Offset: 0x000F4F8C
		public KeySpline KeySpline
		{
			get
			{
				return (KeySpline)base.GetValue(SplineByteKeyFrame.KeySplineProperty);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.SetValue(SplineByteKeyFrame.KeySplineProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SplineByteKeyFrame.KeySpline" />.</summary>
		// Token: 0x0400174B RID: 5963
		public static readonly DependencyProperty KeySplineProperty = DependencyProperty.Register("KeySpline", typeof(KeySpline), typeof(SplineByteKeyFrame), new PropertyMetadata(new KeySpline()));
	}
}
