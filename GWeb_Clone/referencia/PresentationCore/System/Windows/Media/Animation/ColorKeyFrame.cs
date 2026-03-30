using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.ColorAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004FC RID: 1276
	public abstract class ColorKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" />.</summary>
		// Token: 0x060039EA RID: 14826 RVA: 0x000E58F4 File Offset: 0x000E4CF4
		protected ColorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" />.</param>
		// Token: 0x060039EB RID: 14827 RVA: 0x000E5908 File Offset: 0x000E4D08
		protected ColorKeyFrame(Color value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.ColorKeyFrame" />.</param>
		// Token: 0x060039EC RID: 14828 RVA: 0x000E5924 File Offset: 0x000E4D24
		protected ColorKeyFrame(Color value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000E5948 File Offset: 0x000E4D48
		// (set) Token: 0x060039EE RID: 14830 RVA: 0x000E5968 File Offset: 0x000E4D68
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(ColorKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(ColorKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000E5988 File Offset: 0x000E4D88
		// (set) Token: 0x060039F0 RID: 14832 RVA: 0x000E59A0 File Offset: 0x000E4DA0
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Color)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.KeyTime" /> especificado. O valor padrão é um <see cref="T:System.Windows.Media.Color" /> com um valor hexadecimal de #00000000.</returns>
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000E59BC File Offset: 0x000E4DBC
		// (set) Token: 0x060039F2 RID: 14834 RVA: 0x000E59DC File Offset: 0x000E4DDC
		public Color Value
		{
			get
			{
				return (Color)base.GetValue(ColorKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(ColorKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor do qual animar; normalmente, o valor do quadro-chave anterior.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x060039F3 RID: 14835 RVA: 0x000E59FC File Offset: 0x000E4DFC
		public Color InterpolateValue(Color baseValue, double keyFrameProgress)
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
		// Token: 0x060039F4 RID: 14836
		protected abstract Color InterpolateValueCore(Color baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016C3 RID: 5827
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(ColorKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" />.</summary>
		// Token: 0x040016C4 RID: 5828
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Color), typeof(ColorKeyFrame), new PropertyMetadata());
	}
}
