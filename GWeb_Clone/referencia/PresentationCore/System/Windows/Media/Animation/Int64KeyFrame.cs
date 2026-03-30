using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000501 RID: 1281
	public abstract class Int64KeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" />.</summary>
		// Token: 0x06003A26 RID: 14886 RVA: 0x000E613C File Offset: 0x000E553C
		protected Int64KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" />.</param>
		// Token: 0x06003A27 RID: 14887 RVA: 0x000E6150 File Offset: 0x000E5550
		protected Int64KeyFrame(long value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int64KeyFrame" />.</param>
		// Token: 0x06003A28 RID: 14888 RVA: 0x000E616C File Offset: 0x000E556C
		protected Int64KeyFrame(long value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000E6190 File Offset: 0x000E5590
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x000E61B0 File Offset: 0x000E55B0
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(Int64KeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(Int64KeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000E61D0 File Offset: 0x000E55D0
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x000E61E8 File Offset: 0x000E55E8
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (long)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x000E6204 File Offset: 0x000E5604
		// (set) Token: 0x06003A2E RID: 14894 RVA: 0x000E6224 File Offset: 0x000E5624
		public long Value
		{
			get
			{
				return (long)base.GetValue(Int64KeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(Int64KeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A2F RID: 14895 RVA: 0x000E6244 File Offset: 0x000E5644
		public long InterpolateValue(long baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 0.0 || keyFrameProgress > 1.0)
			{
				throw new ArgumentOutOfRangeException("keyFrameProgress");
			}
			return this.InterpolateValueCore(baseValue, keyFrameProgress);
		}

		/// <summary>Calcula o valor de um quadro-chave no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor do qual animar; normalmente, o valor do quadro-chave anterior.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003A30 RID: 14896
		protected abstract long InterpolateValueCore(long baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.KeyTime" />.</summary>
		// Token: 0x040016CD RID: 5837
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(Int64KeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" />.</summary>
		// Token: 0x040016CE RID: 5838
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(long), typeof(Int64KeyFrame), new PropertyMetadata());
	}
}
