using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.Rotation3DAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000507 RID: 1287
	public abstract class Rotation3DKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" />.</summary>
		// Token: 0x06003A6E RID: 14958 RVA: 0x000E6B14 File Offset: 0x000E5F14
		protected Rotation3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" />.</param>
		// Token: 0x06003A6F RID: 14959 RVA: 0x000E6B28 File Offset: 0x000E5F28
		protected Rotation3DKeyFrame(Rotation3D value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.Rotation3DKeyFrame" />.</param>
		// Token: 0x06003A70 RID: 14960 RVA: 0x000E6B44 File Offset: 0x000E5F44
		protected Rotation3DKeyFrame(Rotation3D value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000E6B68 File Offset: 0x000E5F68
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x000E6B88 File Offset: 0x000E5F88
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(Rotation3DKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(Rotation3DKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x000E6BA8 File Offset: 0x000E5FA8
		// (set) Token: 0x06003A74 RID: 14964 RVA: 0x000E6BBC File Offset: 0x000E5FBC
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Rotation3D)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000E6BD8 File Offset: 0x000E5FD8
		// (set) Token: 0x06003A76 RID: 14966 RVA: 0x000E6BF8 File Offset: 0x000E5FF8
		public Rotation3D Value
		{
			get
			{
				return (Rotation3D)base.GetValue(Rotation3DKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(Rotation3DKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A77 RID: 14967 RVA: 0x000E6C14 File Offset: 0x000E6014
		public Rotation3D InterpolateValue(Rotation3D baseValue, double keyFrameProgress)
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
		// Token: 0x06003A78 RID: 14968
		protected abstract Rotation3D InterpolateValueCore(Rotation3D baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016D9 RID: 5849
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(Rotation3DKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" />.</summary>
		// Token: 0x040016DA RID: 5850
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Rotation3D), typeof(Rotation3DKeyFrame), new PropertyMetadata());
	}
}
