using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.ByteAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004DD RID: 1245
	public class EasingByteKeyFrame : ByteKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingByteKeyFrame" />.</summary>
		// Token: 0x0600381A RID: 14362 RVA: 0x000DF398 File Offset: 0x000DE798
		public EasingByteKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingByteKeyFrame" /> com o valor <see cref="T:System.Byte" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Byte" /> inicial.</param>
		// Token: 0x0600381B RID: 14363 RVA: 0x000DF3AC File Offset: 0x000DE7AC
		public EasingByteKeyFrame(byte value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingByteKeyFrame" /> com o valor <see cref="T:System.Byte" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Byte" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x0600381C RID: 14364 RVA: 0x000DF3C8 File Offset: 0x000DE7C8
		public EasingByteKeyFrame(byte value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingByteKeyFrame" /> com o valor <see cref="T:System.Byte" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Byte" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x0600381D RID: 14365 RVA: 0x000DF3EC File Offset: 0x000DE7EC
		public EasingByteKeyFrame(byte value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600381E RID: 14366 RVA: 0x000DF414 File Offset: 0x000DE814
		protected override Freezable CreateInstanceCore()
		{
			return new EasingByteKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600381F RID: 14367 RVA: 0x000DF428 File Offset: 0x000DE828
		protected override byte InterpolateValueCore(byte baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateByte(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x000DF478 File Offset: 0x000DE878
		// (set) Token: 0x06003821 RID: 14369 RVA: 0x000DF498 File Offset: 0x000DE898
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingByteKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingByteKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingByteKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001688 RID: 5768
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingByteKeyFrame));
	}
}
