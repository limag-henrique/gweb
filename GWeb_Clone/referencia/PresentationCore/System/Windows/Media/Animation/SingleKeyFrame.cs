using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000509 RID: 1289
	public abstract class SingleKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" />.</summary>
		// Token: 0x06003A86 RID: 14982 RVA: 0x000E6E5C File Offset: 0x000E625C
		protected SingleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" />.</param>
		// Token: 0x06003A87 RID: 14983 RVA: 0x000E6E70 File Offset: 0x000E6270
		protected SingleKeyFrame(float value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.SingleKeyFrame" />.</param>
		// Token: 0x06003A88 RID: 14984 RVA: 0x000E6E8C File Offset: 0x000E628C
		protected SingleKeyFrame(float value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003A89 RID: 14985 RVA: 0x000E6EB0 File Offset: 0x000E62B0
		// (set) Token: 0x06003A8A RID: 14986 RVA: 0x000E6ED0 File Offset: 0x000E62D0
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(SingleKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(SingleKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003A8B RID: 14987 RVA: 0x000E6EF0 File Offset: 0x000E62F0
		// (set) Token: 0x06003A8C RID: 14988 RVA: 0x000E6F08 File Offset: 0x000E6308
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (float)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003A8D RID: 14989 RVA: 0x000E6F24 File Offset: 0x000E6324
		// (set) Token: 0x06003A8E RID: 14990 RVA: 0x000E6F44 File Offset: 0x000E6344
		public float Value
		{
			get
			{
				return (float)base.GetValue(SingleKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(SingleKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A8F RID: 14991 RVA: 0x000E6F64 File Offset: 0x000E6364
		public float InterpolateValue(float baseValue, double keyFrameProgress)
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
		// Token: 0x06003A90 RID: 14992
		protected abstract float InterpolateValueCore(float baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016DD RID: 5853
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(SingleKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" />.</summary>
		// Token: 0x040016DE RID: 5854
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(float), typeof(SingleKeyFrame), new PropertyMetadata());
	}
}
