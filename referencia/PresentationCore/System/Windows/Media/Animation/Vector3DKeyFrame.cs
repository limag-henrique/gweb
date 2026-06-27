using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, define um segmento de animação com seu próprio valor de destino e método de interpolação para um <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</summary>
	// Token: 0x0200050D RID: 1293
	public abstract class Vector3DKeyFrame : Freezable, IKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" />.</summary>
		// Token: 0x06003AB6 RID: 15030 RVA: 0x000E74F4 File Offset: 0x000E68F4
		protected Vector3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> de destino especificado.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" />.</param>
		// Token: 0x06003AB7 RID: 15031 RVA: 0x000E7508 File Offset: 0x000E6908
		protected Vector3DKeyFrame(Vector3D value) : this()
		{
			this.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" /> que tem o <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> e o <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.KeyTime" /> de destino especificados.</summary>
		/// <param name="value">O <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> da nova instância <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" />.</param>
		/// <param name="keyTime">O <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.KeyTime" /> da nova instância <see cref="T:System.Windows.Media.Animation.Vector3DKeyFrame" />.</param>
		// Token: 0x06003AB8 RID: 15032 RVA: 0x000E7524 File Offset: 0x000E6924
		protected Vector3DKeyFrame(Vector3D value, KeyTime keyTime) : this()
		{
			this.Value = value;
			this.KeyTime = keyTime;
		}

		/// <summary>Obtém ou define a hora na qual o <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> de destino do quadro-chave deve ser atingido.</summary>
		/// <returns>A hora em que o valor atual do quadro chave deve ser igual ao seu <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> propriedade. O valor padrão é <see cref="P:System.Windows.Media.Animation.KeyTime.Uniform" />.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000E7548 File Offset: 0x000E6948
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x000E7568 File Offset: 0x000E6968
		public KeyTime KeyTime
		{
			get
			{
				return (KeyTime)base.GetValue(Vector3DKeyFrame.KeyTimeProperty);
			}
			set
			{
				base.SetValueInternal(Vector3DKeyFrame.KeyTimeProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x000E7588 File Offset: 0x000E6988
		// (set) Token: 0x06003ABC RID: 15036 RVA: 0x000E75A0 File Offset: 0x000E69A0
		object IKeyFrame.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (Vector3D)value;
			}
		}

		/// <summary>Obtém ou define o valor de destino do quadro chave.</summary>
		/// <returns>O valor de destino do quadro chave, que é o valor desse quadro chave em seu <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.KeyTime" /> especificado. O valor padrão é 0.</returns>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x000E75BC File Offset: 0x000E69BC
		// (set) Token: 0x06003ABE RID: 15038 RVA: 0x000E75DC File Offset: 0x000E69DC
		public Vector3D Value
		{
			get
			{
				return (Vector3D)base.GetValue(Vector3DKeyFrame.ValueProperty);
			}
			set
			{
				base.SetValueInternal(Vector3DKeyFrame.ValueProperty, value);
			}
		}

		/// <summary>Retorna o valor interpolado de um quadro-chave específico no incremento de andamento fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Ocorre se <paramref name="keyFrameProgress" /> não estiver entre 0,0 e 1,0, inclusive.</exception>
		// Token: 0x06003ABF RID: 15039 RVA: 0x000E75FC File Offset: 0x000E69FC
		public Vector3D InterpolateValue(Vector3D baseValue, double keyFrameProgress)
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
		// Token: 0x06003AC0 RID: 15040
		protected abstract Vector3D InterpolateValueCore(Vector3D baseValue, double keyFrameProgress);

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.KeyTime" />.</summary>
		// Token: 0x040016E5 RID: 5861
		public static readonly DependencyProperty KeyTimeProperty = DependencyProperty.Register("KeyTime", typeof(KeyTime), typeof(Vector3DKeyFrame), new PropertyMetadata(KeyTime.Uniform));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" />.</summary>
		// Token: 0x040016E6 RID: 5862
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Vector3D), typeof(Vector3DKeyFrame), new PropertyMetadata());
	}
}
