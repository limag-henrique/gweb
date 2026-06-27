using System;
using System.Globalization;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> usando uma interpolação linear entre dois valores determinados pela combinação das propriedades <see cref="P:System.Windows.Media.Animation.Rotation3DAnimation.From" />, <see cref="P:System.Windows.Media.Animation.Rotation3DAnimation.To" /> ou <see cref="P:System.Windows.Media.Animation.Rotation3DAnimation.By" /> que são definidas para a animação.</summary>
	// Token: 0x0200053C RID: 1340
	public class Rotation3DAnimation : Rotation3DAnimationBase
	{
		// Token: 0x06003D38 RID: 15672 RVA: 0x000F0E4C File Offset: 0x000F024C
		static Rotation3DAnimation()
		{
			Type typeFromHandle = typeof(Rotation3D);
			Type typeFromHandle2 = typeof(Rotation3DAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(Rotation3DAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(Rotation3DAnimation.ValidateFromToOrByValue);
			Rotation3DAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Rotation3DAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Rotation3DAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Rotation3DAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" />.</summary>
		// Token: 0x06003D39 RID: 15673 RVA: 0x000F0EF0 File Offset: 0x000F02F0
		public Rotation3DAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003D3A RID: 15674 RVA: 0x000F0F04 File Offset: 0x000F0304
		public Rotation3DAnimation(Rotation3D toValue, Duration duration) : this()
		{
			this.To = toValue;
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003D3B RID: 15675 RVA: 0x000F0F28 File Offset: 0x000F0328
		public Rotation3DAnimation(Rotation3D toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = toValue;
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003D3C RID: 15676 RVA: 0x000F0F50 File Offset: 0x000F0350
		public Rotation3DAnimation(Rotation3D fromValue, Rotation3D toValue, Duration duration) : this()
		{
			this.From = fromValue;
			this.To = toValue;
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003D3D RID: 15677 RVA: 0x000F0F78 File Offset: 0x000F0378
		public Rotation3DAnimation(Rotation3D fromValue, Rotation3D toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = fromValue;
			this.To = toValue;
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003D3E RID: 15678 RVA: 0x000F0FA8 File Offset: 0x000F03A8
		public new Rotation3DAnimation Clone()
		{
			return (Rotation3DAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003D3F RID: 15679 RVA: 0x000F0FC0 File Offset: 0x000F03C0
		protected override Freezable CreateInstanceCore()
		{
			return new Rotation3DAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.Rotation3DAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003D40 RID: 15680 RVA: 0x000F0FD4 File Offset: 0x000F03D4
		protected override Rotation3D GetCurrentValueCore(Rotation3D defaultOriginValue, Rotation3D defaultDestinationValue, AnimationClock animationClock)
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
			Rotation3D rotation3D = Rotation3D.Identity;
			Rotation3D rotation3D2 = Rotation3D.Identity;
			Rotation3D value = Rotation3D.Identity;
			Rotation3D value2 = Rotation3D.Identity;
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				rotation3D = defaultOriginValue;
				rotation3D2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				rotation3D = this._keyValues[0];
				rotation3D2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				rotation3D = defaultOriginValue;
				rotation3D2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				rotation3D2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				rotation3D = this._keyValues[0];
				rotation3D2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				rotation3D = this._keyValues[0];
				rotation3D2 = AnimatedTypeHelpers.AddRotation3D(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueRotation3D(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueRotation3D(defaultDestinationValue))
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
					Rotation3D value3 = AnimatedTypeHelpers.SubtractRotation3D(rotation3D2, rotation3D);
					value = AnimatedTypeHelpers.ScaleRotation3D(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddRotation3D(value2, AnimatedTypeHelpers.AddRotation3D(value, AnimatedTypeHelpers.InterpolateRotation3D(rotation3D, rotation3D2, num)));
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x000F11E0 File Offset: 0x000F05E0
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Rotation3D[2];
					this._keyValues[0] = this.From;
					this._keyValues[1] = this.To;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Rotation3D[2];
					this._keyValues[0] = this.From;
					this._keyValues[1] = this.By;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Rotation3D[1];
					this._keyValues[0] = this.From;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Rotation3D[1];
				this._keyValues[0] = this.To;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Rotation3D[1];
				this._keyValues[0] = this.By;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000F12FC File Offset: 0x000F06FC
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Rotation3DAnimation rotation3DAnimation = (Rotation3DAnimation)d;
			rotation3DAnimation._isAnimationFunctionValid = false;
			rotation3DAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000F1324 File Offset: 0x000F0724
		private static bool ValidateFromToOrByValue(object value)
		{
			Rotation3D rotation3D = (Rotation3D)value;
			return rotation3D == null || AnimatedTypeHelpers.IsValidAnimationValueRotation3D(rotation3D);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é nulo.</returns>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x000F1344 File Offset: 0x000F0744
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x000F1364 File Offset: 0x000F0764
		public Rotation3D From
		{
			get
			{
				return (Rotation3D)base.GetValue(Rotation3DAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(Rotation3DAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> que representa o valor final da animação.</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x000F1380 File Offset: 0x000F0780
		// (set) Token: 0x06003D47 RID: 15687 RVA: 0x000F13A0 File Offset: 0x000F07A0
		public Rotation3D To
		{
			get
			{
				return (Rotation3D)base.GetValue(Rotation3DAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(Rotation3DAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003D48 RID: 15688 RVA: 0x000F13BC File Offset: 0x000F07BC
		// (set) Token: 0x06003D49 RID: 15689 RVA: 0x000F13DC File Offset: 0x000F07DC
		public Rotation3D By
		{
			get
			{
				return (Rotation3D)base.GetValue(Rotation3DAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(Rotation3DAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x000F13F8 File Offset: 0x000F07F8
		// (set) Token: 0x06003D4B RID: 15691 RVA: 0x000F1418 File Offset: 0x000F0818
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(Rotation3DAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(Rotation3DAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x000F1434 File Offset: 0x000F0834
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x000F1454 File Offset: 0x000F0854
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
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000F1474 File Offset: 0x000F0874
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000F1494 File Offset: 0x000F0894
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

		// Token: 0x04001726 RID: 5926
		private Rotation3D[] _keyValues;

		// Token: 0x04001727 RID: 5927
		private AnimationType _animationType;

		// Token: 0x04001728 RID: 5928
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Rotation3DAnimation.From" />.</summary>
		// Token: 0x04001729 RID: 5929
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Rotation3DAnimation.To" />.</summary>
		// Token: 0x0400172A RID: 5930
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Rotation3DAnimation.By" />.</summary>
		// Token: 0x0400172B RID: 5931
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Rotation3DAnimation.EasingFunction" />.</summary>
		// Token: 0x0400172C RID: 5932
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
