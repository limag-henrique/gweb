using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.ByteAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004FA RID: 1274
	public abstract class ByteKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" />.</summary>
		// Token: 0x060039D2 RID: 14802 RVA: 0x000E55A4 File Offset: 0x000E49A4
		protected ByteKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" />.</param>
		// Token: 0x060039D3 RID: 14803 RVA: 0x000E55B8 File Offset: 0x000E49B8
		protected ByteKeyFrame(byte value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.ByteKeyFrame" />.</param>
		// Token: 0x060039D4 RID: 14804 RVA: 0x000E55D4 File Offset: 0x000E49D4
		protected ByteKeyFrame(byte value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000E55F8 File Offset: 0x000E49F8
		// (set) Token: 0x060039D6 RID: 14806 RVA: 0x000E5618 File Offset: 0x000E4A18
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(ByteKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(ByteKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000E5638 File Offset: 0x000E4A38
		// (set) Token: 0x060039D8 RID: 14808 RVA: 0x000E5650 File Offset: 0x000E4A50
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (byte)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000E566C File Offset: 0x000E4A6C
		// (set) Token: 0x060039DA RID: 14810 RVA: 0x000E568C File Offset: 0x000E4A8C
		public byte Value
		{
			get
			{
				return (byte)base.GetValue(ByteKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(ByteKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x060039DB RID: 14811 RVA: 0x000E56AC File Offset: 0x000E4AAC
		public byte InterpolateValue(byte baseValue, double keyFrameProgress)
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
		// Token: 0x060039DC RID: 14812
		protected abstract byte InterpolateValueCore(byte baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016BF RID: 5823
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(ByteKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" />.</summary>
		// Token: 0x040016C0 RID: 5824
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(byte), typeof(ByteKeyFrame), new PropertyMetadata());
	}
}
