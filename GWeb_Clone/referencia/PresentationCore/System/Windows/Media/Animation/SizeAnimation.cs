using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Size" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x02000544 RID: 1348
	public class SizeAnimation : SizeAnimationBase
	{
		// Token: 0x06003DF4 RID: 15860 RVA: 0x000F3FC4 File Offset: 0x000F33C4
		static SizeAnimation()
		{
			Type typeFromHandle = typeof(Size?);
			Type typeFromHandle2 = typeof(SizeAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(SizeAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(SizeAnimation.ValidateFromToOrByValue);
			SizeAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			SizeAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			SizeAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			SizeAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeAnimation" />.</summary>
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000F4068 File Offset: 0x000F3468
		public SizeAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003DF6 RID: 15862 RVA: 0x000F407C File Offset: 0x000F347C
		public SizeAnimation(Size toValue, Duration duration) : this()
		{
			this.To = new Size?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003DF7 RID: 15863 RVA: 0x000F40A4 File Offset: 0x000F34A4
		public SizeAnimation(Size toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Size?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003DF8 RID: 15864 RVA: 0x000F40D4 File Offset: 0x000F34D4
		public SizeAnimation(Size fromValue, Size toValue, Duration duration) : this()
		{
			this.From = new Size?(fromValue);
			this.To = new Size?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SizeAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003DF9 RID: 15865 RVA: 0x000F4108 File Offset: 0x000F3508
		public SizeAnimation(Size fromValue, Size toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Size?(fromValue);
			this.To = new Size?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.SizeAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003DFA RID: 15866 RVA: 0x000F4144 File Offset: 0x000F3544
		public new SizeAnimation Clone()
		{
			return (SizeAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.SizeAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003DFB RID: 15867 RVA: 0x000F415C File Offset: 0x000F355C
		protected override Freezable CreateInstanceCore()
		{
			return new SizeAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.SizeAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003DFC RID: 15868 RVA: 0x000F4170 File Offset: 0x000F3570
		protected override Size GetCurrentValueCore(Size defaultOriginValue, Size defaultDestinationValue, AnimationClock animationClock)
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
			Size size = default(Size);
			Size size2 = default(Size);
			Size value = default(Size);
			Size value2 = default(Size);
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				size = defaultOriginValue;
				size2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				size = this._keyValues[0];
				size2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				size = defaultOriginValue;
				size2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				size2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				size = this._keyValues[0];
				size2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				size = this._keyValues[0];
				size2 = AnimatedTypeHelpers.AddSize(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueSize(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueSize(defaultDestinationValue))
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
					Size value3 = AnimatedTypeHelpers.SubtractSize(size2, size);
					value = AnimatedTypeHelpers.ScaleSize(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddSize(value2, AnimatedTypeHelpers.AddSize(value, AnimatedTypeHelpers.InterpolateSize(size, size2, num)));
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000F43A8 File Offset: 0x000F37A8
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Size[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Size[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Size[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Size[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Size[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000F4544 File Offset: 0x000F3944
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SizeAnimation sizeAnimation = (SizeAnimation)d;
			sizeAnimation._isAnimationFunctionValid = false;
			sizeAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000F456C File Offset: 0x000F396C
		private static bool ValidateFromToOrByValue(object value)
		{
			Size? size = (Size?)value;
			return size == null || AnimatedTypeHelpers.IsValidAnimationValueSize(size.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003E00 RID: 15872 RVA: 0x000F4598 File Offset: 0x000F3998
		// (set) Token: 0x06003E01 RID: 15873 RVA: 0x000F45B8 File Offset: 0x000F39B8
		public Size? From
		{
			get
			{
				return (Size?)base.GetValue(SizeAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(SizeAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000F45D8 File Offset: 0x000F39D8
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x000F45F8 File Offset: 0x000F39F8
		public Size? To
		{
			get
			{
				return (Size?)base.GetValue(SizeAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(SizeAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000F4618 File Offset: 0x000F3A18
		// (set) Token: 0x06003E05 RID: 15877 RVA: 0x000F4638 File Offset: 0x000F3A38
		public Size? By
		{
			get
			{
				return (Size?)base.GetValue(SizeAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(SizeAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000F4658 File Offset: 0x000F3A58
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000F4678 File Offset: 0x000F3A78
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(SizeAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(SizeAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x000F4694 File Offset: 0x000F3A94
		// (set) Token: 0x06003E09 RID: 15881 RVA: 0x000F46B4 File Offset: 0x000F3AB4
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
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000F46D4 File Offset: 0x000F3AD4
		// (set) Token: 0x06003E0B RID: 15883 RVA: 0x000F46F4 File Offset: 0x000F3AF4
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

		// Token: 0x0400173E RID: 5950
		private Size[] _keyValues;

		// Token: 0x0400173F RID: 5951
		private AnimationType _animationType;

		// Token: 0x04001740 RID: 5952
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SizeAnimation.From" />.</summary>
		// Token: 0x04001741 RID: 5953
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SizeAnimation.To" />.</summary>
		// Token: 0x04001742 RID: 5954
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SizeAnimation.By" />.</summary>
		// Token: 0x04001743 RID: 5955
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SizeAnimation.EasingFunction" />.</summary>
		// Token: 0x04001744 RID: 5956
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
