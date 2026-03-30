using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.CharAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004FB RID: 1275
	public abstract class CharKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.CharKeyFrame" />.</summary>
		// Token: 0x060039DE RID: 14814 RVA: 0x000E574C File Offset: 0x000E4B4C
		protected CharKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.CharKeyFrame" />.</param>
		// Token: 0x060039DF RID: 14815 RVA: 0x000E5760 File Offset: 0x000E4B60
		protected CharKeyFrame(char value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.CharKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.CharKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.CharKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.CharKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.CharKeyFrame" />.</param>
		// Token: 0x060039E0 RID: 14816 RVA: 0x000E577C File Offset: 0x000E4B7C
		protected CharKeyFrame(char value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000E57A0 File Offset: 0x000E4BA0
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x000E57C0 File Offset: 0x000E4BC0
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(CharKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(CharKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000E57E0 File Offset: 0x000E4BE0
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x000E57F8 File Offset: 0x000E4BF8
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (char)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.CharKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000E5814 File Offset: 0x000E4C14
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x000E5834 File Offset: 0x000E4C34
		public char Value
		{
			get
			{
				return (char)base.GetValue(CharKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(CharKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x060039E7 RID: 14823 RVA: 0x000E5854 File Offset: 0x000E4C54
		public char InterpolateValue(char baseValue, double keyFrameProgress)
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
		// Token: 0x060039E8 RID: 14824
		protected abstract char InterpolateValueCore(char baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.CharKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016C1 RID: 5825
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(CharKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" />.</summary>
		// Token: 0x040016C2 RID: 5826
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(char), typeof(CharKeyFrame), new PropertyMetadata());
	}
}
