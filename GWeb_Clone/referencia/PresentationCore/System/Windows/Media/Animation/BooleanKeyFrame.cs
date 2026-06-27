using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.BooleanAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004F9 RID: 1273
	public abstract class BooleanKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" />.</summary>
		// Token: 0x060039C6 RID: 14790 RVA: 0x000E53FC File Offset: 0x000E47FC
		protected BooleanKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" />.</param>
		// Token: 0x060039C7 RID: 14791 RVA: 0x000E5410 File Offset: 0x000E4810
		protected BooleanKeyFrame(bool value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.BooleanKeyFrame" />.</param>
		// Token: 0x060039C8 RID: 14792 RVA: 0x000E542C File Offset: 0x000E482C
		protected BooleanKeyFrame(bool value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual destino do quadro-chave <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" /> devem ser atingido</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x000E5450 File Offset: 0x000E4850
		// (set) Token: 0x060039CA RID: 14794 RVA: 0x000E5470 File Offset: 0x000E4870
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(BooleanKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(BooleanKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x000E5490 File Offset: 0x000E4890
		// (set) Token: 0x060039CC RID: 14796 RVA: 0x000E54A8 File Offset: 0x000E48A8
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (bool)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.KeyTime" /> especificado. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x000E54C4 File Offset: 0x000E48C4
		// (set) Token: 0x060039CE RID: 14798 RVA: 0x000E54E4 File Offset: 0x000E48E4
		public bool Value
		{
			get
			{
				return (bool)base.GetValue(BooleanKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(BooleanKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x060039CF RID: 14799 RVA: 0x000E5504 File Offset: 0x000E4904
		public bool InterpolateValue(bool baseValue, double keyFrameProgress)
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
		// Token: 0x060039D0 RID: 14800
		protected abstract bool InterpolateValueCore(bool baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016BD RID: 5821
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(BooleanKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" />.</summary>
		// Token: 0x040016BE RID: 5822
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(bool), typeof(BooleanKeyFrame), new PropertyMetadata());
	}
}
