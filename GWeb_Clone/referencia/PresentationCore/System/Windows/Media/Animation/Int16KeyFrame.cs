using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.Int16AnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004FF RID: 1279
	public abstract class Int16KeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" />.</summary>
		// Token: 0x06003A0E RID: 14862 RVA: 0x000E5DEC File Offset: 0x000E51EC
		protected Int16KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" />.</param>
		// Token: 0x06003A0F RID: 14863 RVA: 0x000E5E00 File Offset: 0x000E5200
		protected Int16KeyFrame(short value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int16KeyFrame" />.</param>
		// Token: 0x06003A10 RID: 14864 RVA: 0x000E5E1C File Offset: 0x000E521C
		protected Int16KeyFrame(short value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000E5E40 File Offset: 0x000E5240
		// (set) Token: 0x06003A12 RID: 14866 RVA: 0x000E5E60 File Offset: 0x000E5260
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(Int16KeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(Int16KeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06003A13 RID: 14867 RVA: 0x000E5E80 File Offset: 0x000E5280
		// (set) Token: 0x06003A14 RID: 14868 RVA: 0x000E5E98 File Offset: 0x000E5298
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (short)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000E5EB4 File Offset: 0x000E52B4
		// (set) Token: 0x06003A16 RID: 14870 RVA: 0x000E5ED4 File Offset: 0x000E52D4
		public short Value
		{
			get
			{
				return (short)base.GetValue(Int16KeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(Int16KeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A17 RID: 14871 RVA: 0x000E5EF4 File Offset: 0x000E52F4
		public short InterpolateValue(short baseValue, double keyFrameProgress)
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
		// Token: 0x06003A18 RID: 14872
		protected abstract short InterpolateValueCore(short baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.KeyTime" />.</summary>
		// Token: 0x040016C9 RID: 5833
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(Int16KeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" />.</summary>
		// Token: 0x040016CA RID: 5834
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(short), typeof(Int16KeyFrame), new PropertyMetadata());
	}
}
