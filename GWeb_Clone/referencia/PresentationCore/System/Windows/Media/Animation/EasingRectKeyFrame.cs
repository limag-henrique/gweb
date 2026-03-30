using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.RectAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E8 RID: 1256
	public class EasingRectKeyFrame : RectKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRectKeyFrame" />.</summary>
		// Token: 0x0600387F RID: 14463 RVA: 0x000E024C File Offset: 0x000DF64C
		public EasingRectKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRectKeyFrame" /> com o valor <see cref="T:System.Windows.Rect" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Rect" /> inicial.</param>
		// Token: 0x06003880 RID: 14464 RVA: 0x000E0260 File Offset: 0x000DF660
		public EasingRectKeyFrame(Rect value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRectKeyFrame" /> com o valor <see cref="T:System.Windows.Rect" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Rect" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003881 RID: 14465 RVA: 0x000E027C File Offset: 0x000DF67C
		public EasingRectKeyFrame(Rect value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingRectKeyFrame" /> com o valor <see cref="T:System.Windows.Rect" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Rect" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003882 RID: 14466 RVA: 0x000E02A0 File Offset: 0x000DF6A0
		public EasingRectKeyFrame(Rect value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003883 RID: 14467 RVA: 0x000E02C8 File Offset: 0x000DF6C8
		protected override Freezable CreateInstanceCore()
		{
			return new EasingRectKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003884 RID: 14468 RVA: 0x000E02DC File Offset: 0x000DF6DC
		protected override Rect InterpolateValueCore(Rect baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateRect(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x000E032C File Offset: 0x000DF72C
		// (set) Token: 0x06003886 RID: 14470 RVA: 0x000E034C File Offset: 0x000DF74C
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingRectKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingRectKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingRectKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001694 RID: 5780
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingRectKeyFrame));
	}
}
