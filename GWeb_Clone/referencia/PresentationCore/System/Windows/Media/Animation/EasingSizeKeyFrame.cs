using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.SizeAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004EA RID: 1258
	public class EasingSizeKeyFrame : SizeKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSizeKeyFrame" />.</summary>
		// Token: 0x06003891 RID: 14481 RVA: 0x000E04E4 File Offset: 0x000DF8E4
		public EasingSizeKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSizeKeyFrame" /> com o valor <see cref="T:System.Windows.Size" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Size" /> inicial.</param>
		// Token: 0x06003892 RID: 14482 RVA: 0x000E04F8 File Offset: 0x000DF8F8
		public EasingSizeKeyFrame(Size value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSizeKeyFrame" /> com o valor <see cref="T:System.Windows.Size" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Size" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003893 RID: 14483 RVA: 0x000E0514 File Offset: 0x000DF914
		public EasingSizeKeyFrame(Size value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSizeKeyFrame" /> com o valor <see cref="T:System.Windows.Size" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Size" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003894 RID: 14484 RVA: 0x000E0538 File Offset: 0x000DF938
		public EasingSizeKeyFrame(Size value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003895 RID: 14485 RVA: 0x000E0560 File Offset: 0x000DF960
		protected override Freezable CreateInstanceCore()
		{
			return new EasingSizeKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003896 RID: 14486 RVA: 0x000E0574 File Offset: 0x000DF974
		protected override Size InterpolateValueCore(Size baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateSize(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003897 RID: 14487 RVA: 0x000E05C4 File Offset: 0x000DF9C4
		// (set) Token: 0x06003898 RID: 14488 RVA: 0x000E05E4 File Offset: 0x000DF9E4
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingSizeKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingSizeKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingSizeKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001696 RID: 5782
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingSizeKeyFrame));
	}
}
