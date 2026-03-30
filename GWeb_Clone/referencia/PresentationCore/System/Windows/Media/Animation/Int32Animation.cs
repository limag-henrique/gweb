using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Int32" /> entre dois valores de destino usando interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x020004F1 RID: 1265
	public class Int32Animation : Int32AnimationBase
	{
		// Token: 0x0600390A RID: 14602 RVA: 0x000E21D8 File Offset: 0x000E15D8
		static Int32Animation()
		{
			Type typeFromHandle = typeof(int?);
			Type typeFromHandle2 = typeof(Int32Animation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(Int32Animation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(Int32Animation.ValidateFromToOrByValue);
			Int32Animation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int32Animation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int32Animation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int32Animation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32Animation" />.</summary>
		// Token: 0x0600390B RID: 14603 RVA: 0x000E227C File Offset: 0x000E167C
		public Int32Animation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32Animation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x0600390C RID: 14604 RVA: 0x000E2290 File Offset: 0x000E1690
		public Int32Animation(int toValue, Duration duration) : this()
		{
			this.To = new int?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32Animation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x0600390D RID: 14605 RVA: 0x000E22B8 File Offset: 0x000E16B8
		public Int32Animation(int toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new int?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32Animation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x0600390E RID: 14606 RVA: 0x000E22E8 File Offset: 0x000E16E8
		public Int32Animation(int fromValue, int toValue, Duration duration) : this()
		{
			this.From = new int?(fromValue);
			this.To = new int?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int32Animation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x0600390F RID: 14607 RVA: 0x000E231C File Offset: 0x000E171C
		public Int32Animation(int fromValue, int toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new int?(fromValue);
			this.To = new int?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int32Animation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003910 RID: 14608 RVA: 0x000E2358 File Offset: 0x000E1758
		public new Int32Animation Clone()
		{
			return (Int32Animation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.Int32Animation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003911 RID: 14609 RVA: 0x000E2370 File Offset: 0x000E1770
		protected override Freezable CreateInstanceCore()
		{
			return new Int32Animation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.Int32Animation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003912 RID: 14610 RVA: 0x000E2384 File Offset: 0x000E1784
		protected override int GetCurrentValueCore(int defaultOriginValue, int defaultDestinationValue, AnimationClock animationClock)
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
			int num2 = 0;
			int num3 = 0;
			int value = 0;
			int value2 = 0;
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
				num3 = AnimatedTypeHelpers.AddInt32(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueInt32(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueInt32(defaultDestinationValue))
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
					int value3 = AnimatedTypeHelpers.SubtractInt32(num3, num2);
					value = AnimatedTypeHelpers.ScaleInt32(value3, num4);
				}
			}
			return AnimatedTypeHelpers.AddInt32(value2, AnimatedTypeHelpers.AddInt32(value, AnimatedTypeHelpers.InterpolateInt32(num2, num3, num)));
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000E2584 File Offset: 0x000E1984
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new int[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new int[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new int[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new int[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new int[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000E2704 File Offset: 0x000E1B04
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Int32Animation int32Animation = (Int32Animation)d;
			int32Animation._isAnimationFunctionValid = false;
			int32Animation.PropertyChanged(e.Property);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000E272C File Offset: 0x000E1B2C
		private static bool ValidateFromToOrByValue(object value)
		{
			int? num = (int?)value;
			return num == null || AnimatedTypeHelpers.IsValidAnimationValueInt32(num.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003916 RID: 14614 RVA: 0x000E2758 File Offset: 0x000E1B58
		// (set) Token: 0x06003917 RID: 14615 RVA: 0x000E2778 File Offset: 0x000E1B78
		public int? From
		{
			get
			{
				return (int?)base.GetValue(Int32Animation.FromProperty);
			}
			set
			{
				base.SetValueInternal(Int32Animation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x000E2798 File Offset: 0x000E1B98
		// (set) Token: 0x06003919 RID: 14617 RVA: 0x000E27B8 File Offset: 0x000E1BB8
		public int? To
		{
			get
			{
				return (int?)base.GetValue(Int32Animation.ToProperty);
			}
			set
			{
				base.SetValueInternal(Int32Animation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600391A RID: 14618 RVA: 0x000E27D8 File Offset: 0x000E1BD8
		// (set) Token: 0x0600391B RID: 14619 RVA: 0x000E27F8 File Offset: 0x000E1BF8
		public int? By
		{
			get
			{
				return (int?)base.GetValue(Int32Animation.ByProperty);
			}
			set
			{
				base.SetValueInternal(Int32Animation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600391C RID: 14620 RVA: 0x000E2818 File Offset: 0x000E1C18
		// (set) Token: 0x0600391D RID: 14621 RVA: 0x000E2838 File Offset: 0x000E1C38
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(Int32Animation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(Int32Animation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x0600391E RID: 14622 RVA: 0x000E2854 File Offset: 0x000E1C54
		// (set) Token: 0x0600391F RID: 14623 RVA: 0x000E2874 File Offset: 0x000E1C74
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
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x000E2894 File Offset: 0x000E1C94
		// (set) Token: 0x06003921 RID: 14625 RVA: 0x000E28B4 File Offset: 0x000E1CB4
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

		// Token: 0x040016A5 RID: 5797
		private int[] _keyValues;

		// Token: 0x040016A6 RID: 5798
		private AnimationType _animationType;

		// Token: 0x040016A7 RID: 5799
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int32Animation.From" />.</summary>
		// Token: 0x040016A8 RID: 5800
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int32Animation.To" />.</summary>
		// Token: 0x040016A9 RID: 5801
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int32Animation.By" />.</summary>
		// Token: 0x040016AA RID: 5802
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int32Animation.EasingFunction" />.</summary>
		// Token: 0x040016AB RID: 5803
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
