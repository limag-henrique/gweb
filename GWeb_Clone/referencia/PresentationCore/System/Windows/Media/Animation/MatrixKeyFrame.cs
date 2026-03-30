using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.MatrixAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000502 RID: 1282
	public abstract class MatrixKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" />.</summary>
		// Token: 0x06003A32 RID: 14898 RVA: 0x000E62E4 File Offset: 0x000E56E4
		protected MatrixKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" />.</param>
		// Token: 0x06003A33 RID: 14899 RVA: 0x000E62F8 File Offset: 0x000E56F8
		protected MatrixKeyFrame(Matrix value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.MatrixKeyFrame" />.</param>
		// Token: 0x06003A34 RID: 14900 RVA: 0x000E6314 File Offset: 0x000E5714
		protected MatrixKeyFrame(Matrix value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003A35 RID: 14901 RVA: 0x000E6338 File Offset: 0x000E5738
		// (set) Token: 0x06003A36 RID: 14902 RVA: 0x000E6358 File Offset: 0x000E5758
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(MatrixKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(MatrixKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000E6378 File Offset: 0x000E5778
		// (set) Token: 0x06003A38 RID: 14904 RVA: 0x000E6390 File Offset: 0x000E5790
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Matrix)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003A39 RID: 14905 RVA: 0x000E63AC File Offset: 0x000E57AC
		// (set) Token: 0x06003A3A RID: 14906 RVA: 0x000E63CC File Offset: 0x000E57CC
		public Matrix Value
		{
			get
			{
				return (Matrix)base.GetValue(MatrixKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(MatrixKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A3B RID: 14907 RVA: 0x000E63EC File Offset: 0x000E57EC
		public Matrix InterpolateValue(Matrix baseValue, double keyFrameProgress)
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
		// Token: 0x06003A3C RID: 14908
		protected abstract Matrix InterpolateValueCore(Matrix baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016CF RID: 5839
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(MatrixKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" />.</summary>
		// Token: 0x040016D0 RID: 5840
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Matrix), typeof(MatrixKeyFrame), new PropertyMetadata());
	}
}
