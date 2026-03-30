using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.SizeAnimationUsingKeyFrames" />.</summary>
	// Token: 0x0200050A RID: 1290
	public abstract class SizeKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" />.</summary>
		// Token: 0x06003A92 RID: 14994 RVA: 0x000E7004 File Offset: 0x000E6404
		protected SizeKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" />.</param>
		// Token: 0x06003A93 RID: 14995 RVA: 0x000E7018 File Offset: 0x000E6418
		protected SizeKeyFrame(Size value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.SizeKeyFrame" />.</param>
		// Token: 0x06003A94 RID: 14996 RVA: 0x000E7034 File Offset: 0x000E6434
		protected SizeKeyFrame(Size value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x000E7058 File Offset: 0x000E6458
		// (set) Token: 0x06003A96 RID: 14998 RVA: 0x000E7078 File Offset: 0x000E6478
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(SizeKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(SizeKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x000E7098 File Offset: 0x000E6498
		// (set) Token: 0x06003A98 RID: 15000 RVA: 0x000E70B0 File Offset: 0x000E64B0
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Size)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x000E70CC File Offset: 0x000E64CC
		// (set) Token: 0x06003A9A RID: 15002 RVA: 0x000E70EC File Offset: 0x000E64EC
		public Size Value
		{
			get
			{
				return (Size)base.GetValue(SizeKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(SizeKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A9B RID: 15003 RVA: 0x000E710C File Offset: 0x000E650C
		public Size InterpolateValue(Size baseValue, double keyFrameProgress)
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
		// Token: 0x06003A9C RID: 15004
		protected abstract Size InterpolateValueCore(Size baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016DF RID: 5855
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(SizeKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" />.</summary>
		// Token: 0x040016E0 RID: 5856
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Size), typeof(SizeKeyFrame), new PropertyMetadata());
	}
}
