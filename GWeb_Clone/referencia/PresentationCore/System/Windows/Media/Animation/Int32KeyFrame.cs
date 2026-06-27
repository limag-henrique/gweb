using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000500 RID: 1280
	public abstract class Int32KeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" />.</summary>
		// Token: 0x06003A1A RID: 14874 RVA: 0x000E5F94 File Offset: 0x000E5394
		protected Int32KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" />.</param>
		// Token: 0x06003A1B RID: 14875 RVA: 0x000E5FA8 File Offset: 0x000E53A8
		protected Int32KeyFrame(int value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.Int32KeyFrame" />.</param>
		// Token: 0x06003A1C RID: 14876 RVA: 0x000E5FC4 File Offset: 0x000E53C4
		protected Int32KeyFrame(int value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000E5FE8 File Offset: 0x000E53E8
		// (set) Token: 0x06003A1E RID: 14878 RVA: 0x000E6008 File Offset: 0x000E5408
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(Int32KeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(Int32KeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Consulte <see cref="P:System.Windows.Media.Animation.IKeyFrame.Value" />.</summary>
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000E6028 File Offset: 0x000E5428
		// (set) Token: 0x06003A20 RID: 14880 RVA: 0x000E6040 File Offset: 0x000E5440
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (int)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000E605C File Offset: 0x000E545C
		// (set) Token: 0x06003A22 RID: 14882 RVA: 0x000E607C File Offset: 0x000E547C
		public int Value
		{
			get
			{
				return (int)base.GetValue(Int32KeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(Int32KeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A23 RID: 14883 RVA: 0x000E609C File Offset: 0x000E549C
		public int InterpolateValue(int baseValue, double keyFrameProgress)
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
		// Token: 0x06003A24 RID: 14884
		protected abstract int InterpolateValueCore(int baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.KeyTime" />.</summary>
		// Token: 0x040016CB RID: 5835
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(Int32KeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" />.</summary>
		// Token: 0x040016CC RID: 5836
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(Int32KeyFrame), new PropertyMetadata());
	}
}
