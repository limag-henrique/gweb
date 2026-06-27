using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Define um segmento de animação com seus próprios valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.StringAnimationUsingKeyFrames" />.</summary>
	// Token: 0x0200050B RID: 1291
	public abstract class StringKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.StringKeyFrame" />.</summary>
		// Token: 0x06003A9E RID: 15006 RVA: 0x000E71AC File Offset: 0x000E65AC
		protected StringKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.StringKeyFrame" />.</param>
		// Token: 0x06003A9F RID: 15007 RVA: 0x000E71C0 File Offset: 0x000E65C0
		protected StringKeyFrame(string value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.StringKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.StringKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.StringKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.StringKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.StringKeyFrame" />.</param>
		// Token: 0x06003AA0 RID: 15008 RVA: 0x000E71DC File Offset: 0x000E65DC
		protected StringKeyFrame(string value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" /> propriedade. O padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000E7200 File Offset: 0x000E6600
		// (set) Token: 0x06003AA2 RID: 15010 RVA: 0x000E7220 File Offset: 0x000E6620
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(StringKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(StringKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x000E7240 File Offset: 0x000E6640
		// (set) Token: 0x06003AA4 RID: 15012 RVA: 0x000E7254 File Offset: 0x000E6654
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (string)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.StringKeyFrame.KeyTime" /> especificado. O padrão é 0.</returns>
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x000E7270 File Offset: 0x000E6670
		// (set) Token: 0x06003AA6 RID: 15014 RVA: 0x000E7290 File Offset: 0x000E6690
		public string Value
		{
			get
			{
				return (string)base.GetValue(StringKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(StringKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003AA7 RID: 15015 RVA: 0x000E72AC File Offset: 0x000E66AC
		public string InterpolateValue(string baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 0.0 || keyFrameProgress > 1.0)
			{
				throw new ArgumentOutOfRangeException("keyFrameProgress");
			}
			return this.InterpolateValueCore(baseValue, keyFrameProgress);
		}

		/// <summary>Quando substituído em uma classe derivada, calcula o valor de um quadro-chave no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor do qual animar; normalmente, o valor do quadro-chave anterior.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AA8 RID: 15016
		protected abstract string InterpolateValueCore(string baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.StringKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016E1 RID: 5857
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(StringKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" />.</summary>
		// Token: 0x040016E2 RID: 5858
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(StringKeyFrame), new PropertyMetadata());
	}
}
