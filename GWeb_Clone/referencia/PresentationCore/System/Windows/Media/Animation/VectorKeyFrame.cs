using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</summary>
	// Token: 0x0200050C RID: 1292
	public abstract class VectorKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" />.</summary>
		// Token: 0x06003AAA RID: 15018 RVA: 0x000E734C File Offset: 0x000E674C
		protected VectorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" />.</param>
		// Token: 0x06003AAB RID: 15019 RVA: 0x000E7360 File Offset: 0x000E6760
		protected VectorKeyFrame(Vector value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.VectorKeyFrame" />.</param>
		// Token: 0x06003AAC RID: 15020 RVA: 0x000E737C File Offset: 0x000E677C
		protected VectorKeyFrame(Vector value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000E73A0 File Offset: 0x000E67A0
		// (set) Token: 0x06003AAE RID: 15022 RVA: 0x000E73C0 File Offset: 0x000E67C0
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(VectorKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(VectorKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x000E73E0 File Offset: 0x000E67E0
		// (set) Token: 0x06003AB0 RID: 15024 RVA: 0x000E73F8 File Offset: 0x000E67F8
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Vector)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000E7414 File Offset: 0x000E6814
		// (set) Token: 0x06003AB2 RID: 15026 RVA: 0x000E7434 File Offset: 0x000E6834
		public Vector Value
		{
			get
			{
				return (Vector)base.GetValue(VectorKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(VectorKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003AB3 RID: 15027 RVA: 0x000E7454 File Offset: 0x000E6854
		public Vector InterpolateValue(Vector baseValue, double keyFrameProgress)
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
		// Token: 0x06003AB4 RID: 15028
		protected abstract Vector InterpolateValueCore(Vector baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016E3 RID: 5859
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(VectorKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" />.</summary>
		// Token: 0x040016E4 RID: 5860
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Vector), typeof(VectorKeyFrame), new PropertyMetadata());
	}
}
