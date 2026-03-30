using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Define um segmento de animação com seus próprios valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.PointAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000504 RID: 1284
	public abstract class PointKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointKeyFrame" />.</summary>
		// Token: 0x06003A4A RID: 14922 RVA: 0x000E661C File Offset: 0x000E5A1C
		protected PointKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.PointKeyFrame" />.</param>
		// Token: 0x06003A4B RID: 14923 RVA: 0x000E6630 File Offset: 0x000E5A30
		protected PointKeyFrame(Point value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.PointKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.PointKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.PointKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.PointKeyFrame" />.</param>
		// Token: 0x06003A4C RID: 14924 RVA: 0x000E664C File Offset: 0x000E5A4C
		protected PointKeyFrame(Point value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> propriedade. O padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000E6670 File Offset: 0x000E5A70
		// (set) Token: 0x06003A4E RID: 14926 RVA: 0x000E6690 File Offset: 0x000E5A90
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(PointKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(PointKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000E66B0 File Offset: 0x000E5AB0
		// (set) Token: 0x06003A50 RID: 14928 RVA: 0x000E66C8 File Offset: 0x000E5AC8
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Point)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.PointKeyFrame.KeyTime" /> especificado. O padrão é 0.</returns>
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000E66E4 File Offset: 0x000E5AE4
		// (set) Token: 0x06003A52 RID: 14930 RVA: 0x000E6704 File Offset: 0x000E5B04
		public Point Value
		{
			get
			{
				return (Point)base.GetValue(PointKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(PointKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A53 RID: 14931 RVA: 0x000E6724 File Offset: 0x000E5B24
		public Point InterpolateValue(Point baseValue, double keyFrameProgress)
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
		// Token: 0x06003A54 RID: 14932
		protected abstract Point InterpolateValueCore(Point baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PointKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016D3 RID: 5843
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(PointKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" />.</summary>
		// Token: 0x040016D4 RID: 5844
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Point), typeof(PointKeyFrame), new PropertyMetadata());
	}
}
