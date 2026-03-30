using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Decimal" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x020004BE RID: 1214
	public class DecimalAnimation : DecimalAnimationBase
	{
		// Token: 0x060036ED RID: 14061 RVA: 0x000DB5D0 File Offset: 0x000DA9D0
		static DecimalAnimation()
		{
			Type typeFromHandle = typeof(decimal?);
			Type typeFromHandle2 = typeof(DecimalAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(DecimalAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(DecimalAnimation.ValidateFromToOrByValue);
			DecimalAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			DecimalAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			DecimalAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			DecimalAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalAnimation" />.</summary>
		// Token: 0x060036EE RID: 14062 RVA: 0x000DB674 File Offset: 0x000DAA74
		public DecimalAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060036EF RID: 14063 RVA: 0x000DB688 File Offset: 0x000DAA88
		public DecimalAnimation(decimal toValue, Duration duration) : this()
		{
			this.To = new decimal?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060036F0 RID: 14064 RVA: 0x000DB6B0 File Offset: 0x000DAAB0
		public DecimalAnimation(decimal toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new decimal?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060036F1 RID: 14065 RVA: 0x000DB6E0 File Offset: 0x000DAAE0
		public DecimalAnimation(decimal fromValue, decimal toValue, Duration duration) : this()
		{
			this.From = new decimal?(fromValue);
			this.To = new decimal?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DecimalAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060036F2 RID: 14066 RVA: 0x000DB714 File Offset: 0x000DAB14
		public DecimalAnimation(decimal fromValue, decimal toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new decimal?(fromValue);
			this.To = new decimal?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.DecimalAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060036F3 RID: 14067 RVA: 0x000DB750 File Offset: 0x000DAB50
		public new DecimalAnimation Clone()
		{
			return (DecimalAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.DecimalAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060036F4 RID: 14068 RVA: 0x000DB768 File Offset: 0x000DAB68
		protected override Freezable CreateInstanceCore()
		{
			return new DecimalAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.DecimalAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x060036F5 RID: 14069 RVA: 0x000DB77C File Offset: 0x000DAB7C
		protected override decimal GetCurrentValueCore(decimal defaultOriginValue, decimal defaultDestinationValue, AnimationClock animationClock)
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
			decimal num2 = 0m;
			decimal num3 = 0m;
			decimal value = 0m;
			decimal value2 = 0m;
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
				num3 = AnimatedTypeHelpers.AddDecimal(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueDecimal(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueDecimal(defaultDestinationValue))
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
					decimal value3 = AnimatedTypeHelpers.SubtractDecimal(num3, num2);
					value = AnimatedTypeHelpers.ScaleDecimal(value3, num4);
				}
			}
			return AnimatedTypeHelpers.AddDecimal(value2, AnimatedTypeHelpers.AddDecimal(value, AnimatedTypeHelpers.InterpolateDecimal(num2, num3, num)));
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000DB9B4 File Offset: 0x000DADB4
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new decimal[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new decimal[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new decimal[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new decimal[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new decimal[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000DBB50 File Offset: 0x000DAF50
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DecimalAnimation decimalAnimation = (DecimalAnimation)d;
			decimalAnimation._isAnimationFunctionValid = false;
			decimalAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000DBB78 File Offset: 0x000DAF78
		private static bool ValidateFromToOrByValue(object value)
		{
			decimal? num = (decimal?)value;
			return num == null || AnimatedTypeHelpers.IsValidAnimationValueDecimal(num.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000DBBA4 File Offset: 0x000DAFA4
		// (set) Token: 0x060036FA RID: 14074 RVA: 0x000DBBC4 File Offset: 0x000DAFC4
		public decimal? From
		{
			get
			{
				return (decimal?)base.GetValue(DecimalAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(DecimalAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000DBBE4 File Offset: 0x000DAFE4
		// (set) Token: 0x060036FC RID: 14076 RVA: 0x000DBC04 File Offset: 0x000DB004
		public decimal? To
		{
			get
			{
				return (decimal?)base.GetValue(DecimalAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(DecimalAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x000DBC24 File Offset: 0x000DB024
		// (set) Token: 0x060036FE RID: 14078 RVA: 0x000DBC44 File Offset: 0x000DB044
		public decimal? By
		{
			get
			{
				return (decimal?)base.GetValue(DecimalAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(DecimalAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000DBC64 File Offset: 0x000DB064
		// (set) Token: 0x06003700 RID: 14080 RVA: 0x000DBC84 File Offset: 0x000DB084
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(DecimalAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(DecimalAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000DBCA0 File Offset: 0x000DB0A0
		// (set) Token: 0x06003702 RID: 14082 RVA: 0x000DBCC0 File Offset: 0x000DB0C0
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
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000DBCE0 File Offset: 0x000DB0E0
		// (set) Token: 0x06003704 RID: 14084 RVA: 0x000DBD00 File Offset: 0x000DB100
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

		// Token: 0x0400166F RID: 5743
		private decimal[] _keyValues;

		// Token: 0x04001670 RID: 5744
		private AnimationType _animationType;

		// Token: 0x04001671 RID: 5745
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DecimalAnimation.From" />.</summary>
		// Token: 0x04001672 RID: 5746
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DecimalAnimation.To" />.</summary>
		// Token: 0x04001673 RID: 5747
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DecimalAnimation.By" />.</summary>
		// Token: 0x04001674 RID: 5748
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.DecimalAnimation.EasingFunction" />.</summary>
		// Token: 0x04001675 RID: 5749
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
