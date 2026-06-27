using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.Point3DAnimationUsingKeyFrames" />.</summary>
	// Token: 0x02000505 RID: 1285
	public abstract class Point3DKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" />.</summary>
		// Token: 0x06003A56 RID: 14934 RVA: 0x000E67C4 File Offset: 0x000E5BC4
		protected Point3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" />.</param>
		// Token: 0x06003A57 RID: 14935 RVA: 0x000E67D8 File Offset: 0x000E5BD8
		protected Point3DKeyFrame(Point3D value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.Point3DKeyFrame" />.</param>
		// Token: 0x06003A58 RID: 14936 RVA: 0x000E67F4 File Offset: 0x000E5BF4
		protected Point3DKeyFrame(Point3D value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000E6818 File Offset: 0x000E5C18
		// (set) Token: 0x06003A5A RID: 14938 RVA: 0x000E6838 File Offset: 0x000E5C38
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(Point3DKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(Point3DKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x000E6858 File Offset: 0x000E5C58
		// (set) Token: 0x06003A5C RID: 14940 RVA: 0x000E6870 File Offset: 0x000E5C70
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Point3D)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000E688C File Offset: 0x000E5C8C
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x000E68AC File Offset: 0x000E5CAC
		public Point3D Value
		{
			get
			{
				return (Point3D)base.GetValue(Point3DKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(Point3DKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003A5F RID: 14943 RVA: 0x000E68CC File Offset: 0x000E5CCC
		public Point3D InterpolateValue(Point3D baseValue, double keyFrameProgress)
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
		// Token: 0x06003A60 RID: 14944
		protected abstract Point3D InterpolateValueCore(Point3D baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016D5 RID: 5845
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(Point3DKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" />.</summary>
		// Token: 0x040016D6 RID: 5846
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Point3D), typeof(Point3DKeyFrame), new PropertyMetadata());
	}
}
