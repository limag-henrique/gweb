using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.RectAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000508 RID: 1288
	public abstract class RectKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectKeyFrame" />.</summary>
		// Token: 0x06003A7A RID: 14970 RVA: 0x000E6CB4 File Offset: 0x000E60B4
		protected RectKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.RectKeyFrame" />.</param>
		// Token: 0x06003A7B RID: 14971 RVA: 0x000E6CC8 File Offset: 0x000E60C8
		protected RectKeyFrame(Rect value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.RectKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.RectKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.RectKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.RectKeyFrame" />.</param>
		// Token: 0x06003A7C RID: 14972 RVA: 0x000E6CE4 File Offset: 0x000E60E4
		protected RectKeyFrame(Rect value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003A7D RID: 14973 RVA: 0x000E6D08 File Offset: 0x000E6108
		// (set) Token: 0x06003A7E RID: 14974 RVA: 0x000E6D28 File Offset: 0x000E6128
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(RectKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(RectKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000E6D48 File Offset: 0x000E6148
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x000E6D60 File Offset: 0x000E6160
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Rect)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.RectKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000E6D7C File Offset: 0x000E617C
		// (set) Token: 0x06003A82 RID: 14978 RVA: 0x000E6D9C File Offset: 0x000E619C
		public Rect Value
		{
			get
			{
				return (Rect)base.GetValue(RectKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(RectKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A83 RID: 14979 RVA: 0x000E6DBC File Offset: 0x000E61BC
		public Rect InterpolateValue(Rect baseValue, double keyFrameProgress)
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
		// Token: 0x06003A84 RID: 14980
		protected abstract Rect InterpolateValueCore(Rect baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.RectKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016DB RID: 5851
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(RectKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" />.</summary>
		// Token: 0x040016DC RID: 5852
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Rect), typeof(RectKeyFrame), new PropertyMetadata());
	}
}
