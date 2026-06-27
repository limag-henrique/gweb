using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.Rotation3DAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E7 RID: 1255
	public class EasingRotation3DKeyFrame : Rotation3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRotation3DKeyFrame" />.</summary>
		// Token: 0x06003876 RID: 14454 RVA: 0x000E0100 File Offset: 0x000DF500
		public EasingRotation3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRotation3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> inicial.</param>
		// Token: 0x06003877 RID: 14455 RVA: 0x000E0114 File Offset: 0x000DF514
		public EasingRotation3DKeyFrame(Rotation3D value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRotation3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003878 RID: 14456 RVA: 0x000E0130 File Offset: 0x000DF530
		public EasingRotation3DKeyFrame(Rotation3D value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRotation3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003879 RID: 14457 RVA: 0x000E0154 File Offset: 0x000DF554
		public EasingRotation3DKeyFrame(Rotation3D value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600387A RID: 14458 RVA: 0x000E017C File Offset: 0x000DF57C
		protected override Freezable CreateInstanceCore()
		{
			return new EasingRotation3DKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600387B RID: 14459 RVA: 0x000E0190 File Offset: 0x000DF590
		protected override Rotation3D InterpolateValueCore(Rotation3D baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateRotation3D(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000E01E0 File Offset: 0x000DF5E0
		// (set) Token: 0x0600387D RID: 14461 RVA: 0x000E0200 File Offset: 0x000DF600
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingRotation3DKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingRotation3DKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingRotation3DKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001693 RID: 5779
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingRotation3DKeyFrame));
	}
}
