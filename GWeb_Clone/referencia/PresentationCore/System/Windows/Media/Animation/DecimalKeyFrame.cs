using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.DecimalAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004FD RID: 1277
	public abstract class DecimalKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" />.</summary>
		// Token: 0x060039F6 RID: 14838 RVA: 0x000E5A9C File Offset: 0x000E4E9C
		protected DecimalKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" />.</param>
		// Token: 0x060039F7 RID: 14839 RVA: 0x000E5AB0 File Offset: 0x000E4EB0
		protected DecimalKeyFrame(decimal value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.DecimalKeyFrame" />.</param>
		// Token: 0x060039F8 RID: 14840 RVA: 0x000E5ACC File Offset: 0x000E4ECC
		protected DecimalKeyFrame(decimal value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x000E5AF0 File Offset: 0x000E4EF0
		// (set) Token: 0x060039FA RID: 14842 RVA: 0x000E5B10 File Offset: 0x000E4F10
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(DecimalKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(DecimalKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000E5B30 File Offset: 0x000E4F30
		// (set) Token: 0x060039FC RID: 14844 RVA: 0x000E5B48 File Offset: 0x000E4F48
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (decimal)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x000E5B64 File Offset: 0x000E4F64
		// (set) Token: 0x060039FE RID: 14846 RVA: 0x000E5B84 File Offset: 0x000E4F84
		public decimal Value
		{
			get
			{
				return (decimal)base.GetValue(DecimalKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(DecimalKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x060039FF RID: 14847 RVA: 0x000E5BA4 File Offset: 0x000E4FA4
		public decimal InterpolateValue(decimal baseValue, double keyFrameProgress)
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
		// Token: 0x06003A00 RID: 14848
		protected abstract decimal InterpolateValueCore(decimal baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016C5 RID: 5829
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(DecimalKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" />.</summary>
		// Token: 0x040016C6 RID: 5830
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(decimal), typeof(DecimalKeyFrame), new PropertyMetadata());
	}
}
