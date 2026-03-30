using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E0 RID: 1248
	public class EasingDoubleKeyFrame : DoubleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDoubleKeyFrame" />.</summary>
		// Token: 0x06003835 RID: 14389 RVA: 0x000DF77C File Offset: 0x000DEB7C
		public EasingDoubleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDoubleKeyFrame" /> com o valor <see cref="T:System.Double" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Double" /> inicial.</param>
		// Token: 0x06003836 RID: 14390 RVA: 0x000DF790 File Offset: 0x000DEB90
		public EasingDoubleKeyFrame(double value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDoubleKeyFrame" /> com o valor <see cref="T:System.Double" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Double" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003837 RID: 14391 RVA: 0x000DF7AC File Offset: 0x000DEBAC
		public EasingDoubleKeyFrame(double value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDoubleKeyFrame" /> com o valor <see cref="T:System.Double" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Double" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003838 RID: 14392 RVA: 0x000DF7D0 File Offset: 0x000DEBD0
		public EasingDoubleKeyFrame(double value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003839 RID: 14393 RVA: 0x000DF7F8 File Offset: 0x000DEBF8
		protected override Freezable CreateInstanceCore()
		{
			return new EasingDoubleKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600383A RID: 14394 RVA: 0x000DF80C File Offset: 0x000DEC0C
		protected override double InterpolateValueCore(double baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateDouble(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000DF85C File Offset: 0x000DEC5C
		// (set) Token: 0x0600383C RID: 14396 RVA: 0x000DF87C File Offset: 0x000DEC7C
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingDoubleKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingDoubleKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingDoubleKeyFrame.EasingFunction" />.</summary>
		// Token: 0x0400168B RID: 5771
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingDoubleKeyFrame));
	}
}
