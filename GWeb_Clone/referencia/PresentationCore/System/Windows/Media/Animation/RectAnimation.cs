using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Rect" /> entre dois valores de destino usando uma interpolação linear.</summary>
	// Token: 0x02000536 RID: 1334
	public class RectAnimation : RectAnimationBase
	{
		// Token: 0x06003CD2 RID: 15570 RVA: 0x000EF38C File Offset: 0x000EE78C
		static RectAnimation()
		{
			Type typeFromHandle = typeof(Rect?);
			Type typeFromHandle2 = typeof(RectAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(RectAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(RectAnimation.ValidateFromToOrByValue);
			RectAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			RectAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			RectAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			RectAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectAnimation" />.</summary>
		// Token: 0x06003CD3 RID: 15571 RVA: 0x000EF430 File Offset: 0x000EE830
		public RectAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003CD4 RID: 15572 RVA: 0x000EF444 File Offset: 0x000EE844
		public RectAnimation(Rect toValue, Duration duration) : this()
		{
			this.To = new Rect?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003CD5 RID: 15573 RVA: 0x000EF46C File Offset: 0x000EE86C
		public RectAnimation(Rect toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Rect?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003CD6 RID: 15574 RVA: 0x000EF49C File Offset: 0x000EE89C
		public RectAnimation(Rect fromValue, Rect toValue, Duration duration) : this()
		{
			this.From = new Rect?(fromValue);
			this.To = new Rect?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.RectAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003CD7 RID: 15575 RVA: 0x000EF4D0 File Offset: 0x000EE8D0
		public RectAnimation(Rect fromValue, Rect toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Rect?(fromValue);
			this.To = new Rect?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.RectAnimation" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06003CD8 RID: 15576 RVA: 0x000EF50C File Offset: 0x000EE90C
		public new RectAnimation Clone()
		{
			return (RectAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.RectAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003CD9 RID: 15577 RVA: 0x000EF524 File Offset: 0x000EE924
		protected override Freezable CreateInstanceCore()
		{
			return new RectAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.RectAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003CDA RID: 15578 RVA: 0x000EF538 File Offset: 0x000EE938
		protected override Rect GetCurrentValueCore(Rect defaultOriginValue, Rect defaultDestinationValue, AnimationClock animationClock)
		{
			if (!this._isAnimationFunctionValid)
			{
				this.ValidateAnimationFunction();
			}
			double num = animationClock.CurrentProgress.Value;
			IEasingFunction easingFunction = this.EasingFunction;
			if (easingFunction != null)
			{
				num = easingFunction.Ease(num);
			}
			Rect rect = default(Rect);
			Rect rect2 = default(Rect);
			Rect value = default(Rect);
			Rect value2 = default(Rect);
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				rect = defaultOriginValue;
				rect2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				rect = this._keyValues[0];
				rect2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				rect = defaultOriginValue;
				rect2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				rect2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				rect = this._keyValues[0];
				rect2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				rect = this._keyValues[0];
				rect2 = AnimatedTypeHelpers.AddRect(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueRect(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueRect(defaultDestinationValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"destination",
					defaultDestinationValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (this.IsCumulative)
			{
				double num2 = (double)(animationClock.CurrentIteration - 1).Value;
				if (num2 > 0.0)
				{
					Rect value3 = AnimatedTypeHelpers.SubtractRect(rect2, rect);
					value = AnimatedTypeHelpers.ScaleRect(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddRect(value2, AnimatedTypeHelpers.AddRect(value, AnimatedTypeHelpers.InterpolateRect(rect, rect2, num)));
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x000EF770 File Offset: 0x000EEB70
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Rect[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Rect[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Rect[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Rect[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Rect[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000EF90C File Offset: 0x000EED0C
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RectAnimation rectAnimation = (RectAnimation)d;
			rectAnimation._isAnimationFunctionValid = false;
			rectAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000EF934 File Offset: 0x000EED34
		private static bool ValidateFromToOrByValue(object value)
		{
			Rect? rect = (Rect?)value;
			return rect == null || AnimatedTypeHelpers.IsValidAnimationValueRect(rect.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x000EF960 File Offset: 0x000EED60
		// (set) Token: 0x06003CDF RID: 15583 RVA: 0x000EF980 File Offset: 0x000EED80
		public Rect? From
		{
			get
			{
				return (Rect?)base.GetValue(RectAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(RectAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x000EF9A0 File Offset: 0x000EEDA0
		// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x000EF9C0 File Offset: 0x000EEDC0
		public Rect? To
		{
			get
			{
				return (Rect?)base.GetValue(RectAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(RectAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x000EF9E0 File Offset: 0x000EEDE0
		// (set) Token: 0x06003CE3 RID: 15587 RVA: 0x000EFA00 File Offset: 0x000EEE00
		public Rect? By
		{
			get
			{
				return (Rect?)base.GetValue(RectAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(RectAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000EFA20 File Offset: 0x000EEE20
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x000EFA40 File Offset: 0x000EEE40
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(RectAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(RectAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000EFA5C File Offset: 0x000EEE5C
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x000EFA7C File Offset: 0x000EEE7C
		public bool IsAdditive
		{
			get
			{
				return (bool)base.GetValue(AnimationTimeline.IsAdditiveProperty);
			}
			set
			{
				base.SetValueInternal(AnimationTimeline.IsAdditiveProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define um valor que especifica se o valor da animação acumula quando ela se repete.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação acumula seus valores ao seu <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> propriedade faz com que ele Repita sua duração simples; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x000EFA9C File Offset: 0x000EEE9C
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x000EFABC File Offset: 0x000EEEBC
		public bool IsCumulative
		{
			get
			{
				return (bool)base.GetValue(AnimationTimeline.IsCumulativeProperty);
			}
			set
			{
				base.SetValueInternal(AnimationTimeline.IsCumulativeProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x04001719 RID: 5913
		private Rect[] _keyValues;

		// Token: 0x0400171A RID: 5914
		private AnimationType _animationType;

		// Token: 0x0400171B RID: 5915
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.RectAnimation.From" />.</summary>
		// Token: 0x0400171C RID: 5916
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.RectAnimation.To" />.</summary>
		// Token: 0x0400171D RID: 5917
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.RectAnimation.By" />.</summary>
		// Token: 0x0400171E RID: 5918
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.RectAnimation.EasingFunction" />.</summary>
		// Token: 0x0400171F RID: 5919
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
