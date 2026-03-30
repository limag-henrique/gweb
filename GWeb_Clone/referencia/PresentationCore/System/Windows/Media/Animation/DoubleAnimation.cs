using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Double" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x020004D7 RID: 1239
	public class DoubleAnimation : DoubleAnimationBase
	{
		// Token: 0x060037B4 RID: 14260 RVA: 0x000DD90C File Offset: 0x000DCD0C
		static DoubleAnimation()
		{
			Type typeFromHandle = typeof(double?);
			Type typeFromHandle2 = typeof(DoubleAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(DoubleAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(DoubleAnimation.ValidateFromToOrByValue);
			DoubleAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			DoubleAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			DoubleAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			DoubleAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleAnimation" />.</summary>
		// Token: 0x060037B5 RID: 14261 RVA: 0x000DD9B0 File Offset: 0x000DCDB0
		public DoubleAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060037B6 RID: 14262 RVA: 0x000DD9C4 File Offset: 0x000DCDC4
		public DoubleAnimation(double toValue, Duration duration) : this()
		{
			this.To = new double?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060037B7 RID: 14263 RVA: 0x000DD9EC File Offset: 0x000DCDEC
		public DoubleAnimation(double toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new double?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060037B8 RID: 14264 RVA: 0x000DDA1C File Offset: 0x000DCE1C
		public DoubleAnimation(double fromValue, double toValue, Duration duration) : this()
		{
			this.From = new double?(fromValue);
			this.To = new double?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DoubleAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060037B9 RID: 14265 RVA: 0x000DDA50 File Offset: 0x000DCE50
		public DoubleAnimation(double fromValue, double toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new double?(fromValue);
			this.To = new double?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.DoubleAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060037BA RID: 14266 RVA: 0x000DDA8C File Offset: 0x000DCE8C
		public new DoubleAnimation Clone()
		{
			return (DoubleAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.DoubleAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060037BB RID: 14267 RVA: 0x000DDAA4 File Offset: 0x000DCEA4
		protected override Freezable CreateInstanceCore()
		{
			return new DoubleAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.DoubleAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x060037BC RID: 14268 RVA: 0x000DDAB8 File Offset: 0x000DCEB8
		protected override double GetCurrentValueCore(double defaultOriginValue, double defaultDestinationValue, AnimationClock animationClock)
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
			double num2 = 0.0;
			double num3 = 0.0;
			double value = 0.0;
			double value2 = 0.0;
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				num2 = defaultOriginValue;
				num3 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				num2 = this._keyValues[0];
				num3 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				num2 = defaultOriginValue;
				num3 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				num3 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				num2 = this._keyValues[0];
				num3 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				num2 = this._keyValues[0];
				num3 = AnimatedTypeHelpers.AddDouble(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueDouble(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueDouble(defaultDestinationValue))
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
				double num4 = (double)(animationClock.CurrentIteration - 1).Value;
				if (num4 > 0.0)
				{
					double value3 = AnimatedTypeHelpers.SubtractDouble(num3, num2);
					value = AnimatedTypeHelpers.ScaleDouble(value3, num4);
				}
			}
			return AnimatedTypeHelpers.AddDouble(value2, AnimatedTypeHelpers.AddDouble(value, AnimatedTypeHelpers.InterpolateDouble(num2, num3, num)));
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000DDCD8 File Offset: 0x000DD0D8
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new double[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new double[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new double[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new double[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new double[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000DDE58 File Offset: 0x000DD258
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DoubleAnimation doubleAnimation = (DoubleAnimation)d;
			doubleAnimation._isAnimationFunctionValid = false;
			doubleAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000DDE80 File Offset: 0x000DD280
		private static bool ValidateFromToOrByValue(object value)
		{
			double? num = (double?)value;
			return num == null || AnimatedTypeHelpers.IsValidAnimationValueDouble(num.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000DDEAC File Offset: 0x000DD2AC
		// (set) Token: 0x060037C1 RID: 14273 RVA: 0x000DDECC File Offset: 0x000DD2CC
		public double? From
		{
			get
			{
				return (double?)base.GetValue(DoubleAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(DoubleAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000DDEEC File Offset: 0x000DD2EC
		// (set) Token: 0x060037C3 RID: 14275 RVA: 0x000DDF0C File Offset: 0x000DD30C
		public double? To
		{
			get
			{
				return (double?)base.GetValue(DoubleAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(DoubleAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000DDF2C File Offset: 0x000DD32C
		// (set) Token: 0x060037C5 RID: 14277 RVA: 0x000DDF4C File Offset: 0x000DD34C
		public double? By
		{
			get
			{
				return (double?)base.GetValue(DoubleAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(DoubleAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000DDF6C File Offset: 0x000DD36C
		// (set) Token: 0x060037C7 RID: 14279 RVA: 0x000DDF8C File Offset: 0x000DD38C
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(DoubleAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(DoubleAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x000DDFA8 File Offset: 0x000DD3A8
		// (set) Token: 0x060037C9 RID: 14281 RVA: 0x000DDFC8 File Offset: 0x000DD3C8
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
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000DDFE8 File Offset: 0x000DD3E8
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x000DE008 File Offset: 0x000DD408
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

		// Token: 0x0400167B RID: 5755
		private double[] _keyValues;

		// Token: 0x0400167C RID: 5756
		private AnimationType _animationType;

		// Token: 0x0400167D RID: 5757
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleAnimation.From" />.</summary>
		// Token: 0x0400167E RID: 5758
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleAnimation.To" />.</summary>
		// Token: 0x0400167F RID: 5759
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleAnimation.By" />.</summary>
		// Token: 0x04001680 RID: 5760
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DoubleAnimation.EasingFunction" />.</summary>
		// Token: 0x04001681 RID: 5761
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
