using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.ColorAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004DE RID: 1246
	public class EasingColorKeyFrame : ColorKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingColorKeyFrame" />.</summary>
		// Token: 0x06003823 RID: 14371 RVA: 0x000DF4E4 File Offset: 0x000DE8E4
		public EasingColorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingColorKeyFrame" /> com o <see cref="T:System.Windows.Media.Color" /> especificado.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Color" /> inicial.</param>
		// Token: 0x06003824 RID: 14372 RVA: 0x000DF4F8 File Offset: 0x000DE8F8
		public EasingColorKeyFrame(Color value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingColorKeyFrame" /> com o <see cref="T:System.Windows.Media.Color" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Color" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003825 RID: 14373 RVA: 0x000DF514 File Offset: 0x000DE914
		public EasingColorKeyFrame(Color value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingColorKeyFrame" /> com o <see cref="T:System.Windows.Media.Color" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O <see cref="T:System.Windows.Media.Color" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003826 RID: 14374 RVA: 0x000DF538 File Offset: 0x000DE938
		public EasingColorKeyFrame(Color value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003827 RID: 14375 RVA: 0x000DF560 File Offset: 0x000DE960
		protected override Freezable CreateInstanceCore()
		{
			return new EasingColorKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003828 RID: 14376 RVA: 0x000DF574 File Offset: 0x000DE974
		protected override Color InterpolateValueCore(Color baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateColor(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x000DF5C4 File Offset: 0x000DE9C4
		// (set) Token: 0x0600382A RID: 14378 RVA: 0x000DF5E4 File Offset: 0x000DE9E4
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingColorKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingColorKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingColorKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001689 RID: 5769
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingColorKeyFrame));
	}
}
