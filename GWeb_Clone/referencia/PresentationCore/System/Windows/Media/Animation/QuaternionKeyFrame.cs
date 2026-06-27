using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000506 RID: 1286
	public abstract class QuaternionKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" />.</summary>
		// Token: 0x06003A62 RID: 14946 RVA: 0x000E696C File Offset: 0x000E5D6C
		protected QuaternionKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" />.</param>
		// Token: 0x06003A63 RID: 14947 RVA: 0x000E6980 File Offset: 0x000E5D80
		protected QuaternionKeyFrame(Quaternion value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.QuaternionKeyFrame" />.</param>
		// Token: 0x06003A64 RID: 14948 RVA: 0x000E699C File Offset: 0x000E5D9C
		protected QuaternionKeyFrame(Quaternion value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000E69C0 File Offset: 0x000E5DC0
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x000E69E0 File Offset: 0x000E5DE0
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(QuaternionKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(QuaternionKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x000E6A00 File Offset: 0x000E5E00
		// (set) Token: 0x06003A68 RID: 14952 RVA: 0x000E6A18 File Offset: 0x000E5E18
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Quaternion)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000E6A34 File Offset: 0x000E5E34
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000E6A54 File Offset: 0x000E5E54
		public Quaternion Value
		{
			get
			{
				return (Quaternion)base.GetValue(QuaternionKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(QuaternionKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A6B RID: 14955 RVA: 0x000E6A74 File Offset: 0x000E5E74
		public Quaternion InterpolateValue(Quaternion baseValue, double keyFrameProgress)
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
		// Token: 0x06003A6C RID: 14956
		protected abstract Quaternion InterpolateValueCore(Quaternion baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016D7 RID: 5847
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(QuaternionKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" />.</summary>
		// Token: 0x040016D8 RID: 5848
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Quaternion), typeof(QuaternionKeyFrame), new PropertyMetadata());
	}
}
