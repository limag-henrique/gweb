using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004FE RID: 1278
	public abstract class DoubleKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" />.</summary>
		// Token: 0x06003A02 RID: 14850 RVA: 0x000E5C44 File Offset: 0x000E5044
		protected DoubleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" />.</param>
		// Token: 0x06003A03 RID: 14851 RVA: 0x000E5C58 File Offset: 0x000E5058
		protected DoubleKeyFrame(double value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.DoubleKeyFrame" />.</param>
		// Token: 0x06003A04 RID: 14852 RVA: 0x000E5C74 File Offset: 0x000E5074
		protected DoubleKeyFrame(double value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000E5C98 File Offset: 0x000E5098
		// (set) Token: 0x06003A06 RID: 14854 RVA: 0x000E5CB8 File Offset: 0x000E50B8
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(DoubleKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(DoubleKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000E5CD8 File Offset: 0x000E50D8
		// (set) Token: 0x06003A08 RID: 14856 RVA: 0x000E5CF0 File Offset: 0x000E50F0
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (double)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000E5D0C File Offset: 0x000E510C
		// (set) Token: 0x06003A0A RID: 14858 RVA: 0x000E5D2C File Offset: 0x000E512C
		public double Value
		{
			get
			{
				return (double)base.GetValue(DoubleKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(DoubleKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A0B RID: 14859 RVA: 0x000E5D4C File Offset: 0x000E514C
		public double InterpolateValue(double baseValue, double keyFrameProgress)
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
		// Token: 0x06003A0C RID: 14860
		protected abstract double InterpolateValueCore(double baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016C7 RID: 5831
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(DoubleKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" />.</summary>
		// Token: 0x040016C8 RID: 5832
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(DoubleKeyFrame), new PropertyMetadata());
	}
}
