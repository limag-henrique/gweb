using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.ObjectAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000503 RID: 1283
	public abstract class ObjectKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" />.</summary>
		// Token: 0x06003A3E RID: 14910 RVA: 0x000E648C File Offset: 0x000E588C
		protected ObjectKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" />.</param>
		// Token: 0x06003A3F RID: 14911 RVA: 0x000E64A0 File Offset: 0x000E58A0
		protected ObjectKeyFrame(object value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.ObjectKeyFrame" />.</param>
		// Token: 0x06003A40 RID: 14912 RVA: 0x000E64BC File Offset: 0x000E58BC
		protected ObjectKeyFrame(object value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003A41 RID: 14913 RVA: 0x000E64E0 File Offset: 0x000E58E0
		// (set) Token: 0x06003A42 RID: 14914 RVA: 0x000E6500 File Offset: 0x000E5900
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(ObjectKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(ObjectKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x000E6520 File Offset: 0x000E5920
		// (set) Token: 0x06003A44 RID: 14916 RVA: 0x000E6534 File Offset: 0x000E5934
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x000E6548 File Offset: 0x000E5948
		// (set) Token: 0x06003A46 RID: 14918 RVA: 0x000E6560 File Offset: 0x000E5960
		public object Value
		{
			get
			{
				return base.GetValue(ObjectKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(ObjectKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A47 RID: 14919 RVA: 0x000E657C File Offset: 0x000E597C
		public object InterpolateValue(object baseValue, double keyFrameProgress)
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
		// Token: 0x06003A48 RID: 14920
		protected abstract object InterpolateValueCore(object baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016D1 RID: 5841
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(ObjectKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" />.</summary>
		// Token: 0x040016D2 RID: 5842
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(ObjectKeyFrame), new PropertyMetadata());
	}
}
