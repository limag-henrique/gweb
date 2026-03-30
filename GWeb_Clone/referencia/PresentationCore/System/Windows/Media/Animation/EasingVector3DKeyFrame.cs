using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.Vector3DAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004EC RID: 1260
	public class EasingVector3DKeyFrame : Vector3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVector3DKeyFrame" />.</summary>
		// Token: 0x060038A3 RID: 14499 RVA: 0x000E077C File Offset: 0x000DFB7C
		public EasingVector3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVector3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> inicial.</param>
		// Token: 0x060038A4 RID: 14500 RVA: 0x000E0790 File Offset: 0x000DFB90
		public EasingVector3DKeyFrame(Vector3D value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVector3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x060038A5 RID: 14501 RVA: 0x000E07AC File Offset: 0x000DFBAC
		public EasingVector3DKeyFrame(Vector3D value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVector3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Vector3D" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x060038A6 RID: 14502 RVA: 0x000E07D0 File Offset: 0x000DFBD0
		public EasingVector3DKeyFrame(Vector3D value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060038A7 RID: 14503 RVA: 0x000E07F8 File Offset: 0x000DFBF8
		protected override Freezable CreateInstanceCore()
		{
			return new EasingVector3DKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x060038A8 RID: 14504 RVA: 0x000E080C File Offset: 0x000DFC0C
		protected override Vector3D InterpolateValueCore(Vector3D baseValue, double keyFrameProgress)
		{
			IEasingFunction easingFunction = this.EasingFunction;
			if (easingFunction != null)
			{
				keyFrameProgress = easingFunction.Ease(keyFrameProgress);
			}
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateVector3D(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x000E085C File Offset: 0x000DFC5C
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x000E087C File Offset: 0x000DFC7C
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingVector3DKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingVector3DKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingVector3DKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001698 RID: 5784
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingVector3DKeyFrame));
	}
}
