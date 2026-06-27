using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Media.Color" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x020004B9 RID: 1209
	public class ColorAnimation : ColorAnimationBase
	{
		// Token: 0x0600368C RID: 13964 RVA: 0x000D9BDC File Offset: 0x000D8FDC
		static ColorAnimation()
		{
			Type typeFromHandle = typeof(Color?);
			Type typeFromHandle2 = typeof(ColorAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(ColorAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(ColorAnimation.ValidateFromToOrByValue);
			ColorAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			ColorAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			ColorAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			ColorAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorAnimation" />.</summary>
		// Token: 0x0600368D RID: 13965 RVA: 0x000D9C80 File Offset: 0x000D9080
		public ColorAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x0600368E RID: 13966 RVA: 0x000D9C94 File Offset: 0x000D9094
		public ColorAnimation(Color toValue, Duration duration) : this()
		{
			this.To = new Color?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorAnimation" /> que é animada até o valor especificado durante o período especificado e que tem o comportamento de preenchimento especificado. O valor inicial para a animação é o valor base da propriedade que está sendo animada ou a saída de uma outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x0600368F RID: 13967 RVA: 0x000D9CBC File Offset: 0x000D90BC
		public ColorAnimation(Color toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Color?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003690 RID: 13968 RVA: 0x000D9CEC File Offset: 0x000D90EC
		public ColorAnimation(Color fromValue, Color toValue, Duration duration) : this()
		{
			this.From = new Color?(fromValue);
			this.To = new Color?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ColorAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003691 RID: 13969 RVA: 0x000D9D20 File Offset: 0x000D9120
		public ColorAnimation(Color fromValue, Color toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Color?(fromValue);
			this.To = new Color?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.ColorAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003692 RID: 13970 RVA: 0x000D9D5C File Offset: 0x000D915C
		public new ColorAnimation Clone()
		{
			return (ColorAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.ColorAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003693 RID: 13971 RVA: 0x000D9D74 File Offset: 0x000D9174
		protected override Freezable CreateInstanceCore()
		{
			return new ColorAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.ColorAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003694 RID: 13972 RVA: 0x000D9D88 File Offset: 0x000D9188
		protected override Color GetCurrentValueCore(Color defaultOriginValue, Color defaultDestinationValue, AnimationClock animationClock)
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
			Color color = default(Color);
			Color color2 = default(Color);
			Color value = default(Color);
			Color value2 = default(Color);
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				color = defaultOriginValue;
				color2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				color = this._keyValues[0];
				color2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				color = defaultOriginValue;
				color2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				color2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				color = this._keyValues[0];
				color2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				color = this._keyValues[0];
				color2 = AnimatedTypeHelpers.AddColor(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueColor(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueColor(defaultDestinationValue))
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
					Color value3 = AnimatedTypeHelpers.SubtractColor(color2, color);
					value = AnimatedTypeHelpers.ScaleColor(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddColor(value2, AnimatedTypeHelpers.AddColor(value, AnimatedTypeHelpers.InterpolateColor(color, color2, num)));
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000D9FC0 File Offset: 0x000D93C0
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Color[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Color[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Color[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Color[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Color[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000DA15C File Offset: 0x000D955C
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ColorAnimation colorAnimation = (ColorAnimation)d;
			colorAnimation._isAnimationFunctionValid = false;
			colorAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000DA184 File Offset: 0x000D9584
		private static bool ValidateFromToOrByValue(object value)
		{
			Color? color = (Color?)value;
			return color == null || AnimatedTypeHelpers.IsValidAnimationValueColor(color.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000DA1B0 File Offset: 0x000D95B0
		// (set) Token: 0x06003699 RID: 13977 RVA: 0x000DA1D0 File Offset: 0x000D95D0
		public Color? From
		{
			get
			{
				return (Color?)base.GetValue(ColorAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(ColorAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000DA1F0 File Offset: 0x000D95F0
		// (set) Token: 0x0600369B RID: 13979 RVA: 0x000DA210 File Offset: 0x000D9610
		public Color? To
		{
			get
			{
				return (Color?)base.GetValue(ColorAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(ColorAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000DA230 File Offset: 0x000D9630
		// (set) Token: 0x0600369D RID: 13981 RVA: 0x000DA250 File Offset: 0x000D9650
		public Color? By
		{
			get
			{
				return (Color?)base.GetValue(ColorAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(ColorAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000DA270 File Offset: 0x000D9670
		// (set) Token: 0x0600369F RID: 13983 RVA: 0x000DA290 File Offset: 0x000D9690
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(ColorAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(ColorAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000DA2AC File Offset: 0x000D96AC
		// (set) Token: 0x060036A1 RID: 13985 RVA: 0x000DA2CC File Offset: 0x000D96CC
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
		///   <see langword="true" /> Se a animação acumula seus valores quando seu <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> propriedade faz com que ele Repita sua duração simples. Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000DA2EC File Offset: 0x000D96EC
		// (set) Token: 0x060036A3 RID: 13987 RVA: 0x000DA30C File Offset: 0x000D970C
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

		// Token: 0x04001663 RID: 5731
		private Color[] _keyValues;

		// Token: 0x04001664 RID: 5732
		private AnimationType _animationType;

		// Token: 0x04001665 RID: 5733
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ColorAnimation.From" />.</summary>
		// Token: 0x04001666 RID: 5734
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ColorAnimation.To" />.</summary>
		// Token: 0x04001667 RID: 5735
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ColorAnimation.By" />.</summary>
		// Token: 0x04001668 RID: 5736
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ColorAnimation.EasingFunction" />.</summary>
		// Token: 0x04001669 RID: 5737
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
