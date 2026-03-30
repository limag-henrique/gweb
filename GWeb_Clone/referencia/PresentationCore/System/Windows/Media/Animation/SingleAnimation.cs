using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Single" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x02000540 RID: 1344
	public class SingleAnimation : SingleAnimationBase
	{
		// Token: 0x06003D96 RID: 15766 RVA: 0x000F26A4 File Offset: 0x000F1AA4
		static SingleAnimation()
		{
			Type typeFromHandle = typeof(float?);
			Type typeFromHandle2 = typeof(SingleAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(SingleAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(SingleAnimation.ValidateFromToOrByValue);
			SingleAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			SingleAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			SingleAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			SingleAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleAnimation" />.</summary>
		// Token: 0x06003D97 RID: 15767 RVA: 0x000F2748 File Offset: 0x000F1B48
		public SingleAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003D98 RID: 15768 RVA: 0x000F275C File Offset: 0x000F1B5C
		public SingleAnimation(float toValue, Duration duration) : this()
		{
			this.To = new float?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003D99 RID: 15769 RVA: 0x000F2784 File Offset: 0x000F1B84
		public SingleAnimation(float toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new float?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003D9A RID: 15770 RVA: 0x000F27B4 File Offset: 0x000F1BB4
		public SingleAnimation(float fromValue, float toValue, Duration duration) : this()
		{
			this.From = new float?(fromValue);
			this.To = new float?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.SingleAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003D9B RID: 15771 RVA: 0x000F27E8 File Offset: 0x000F1BE8
		public SingleAnimation(float fromValue, float toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new float?(fromValue);
			this.To = new float?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.SingleAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003D9C RID: 15772 RVA: 0x000F2824 File Offset: 0x000F1C24
		public new SingleAnimation Clone()
		{
			return (SingleAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.PointAnimationUsingKeyFrames" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003D9D RID: 15773 RVA: 0x000F283C File Offset: 0x000F1C3C
		protected override Freezable CreateInstanceCore()
		{
			return new SingleAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.SingleAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003D9E RID: 15774 RVA: 0x000F2850 File Offset: 0x000F1C50
		protected override float GetCurrentValueCore(float defaultOriginValue, float defaultDestinationValue, AnimationClock animationClock)
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
			float num2 = 0f;
			float num3 = 0f;
			float value = 0f;
			float value2 = 0f;
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
				num3 = AnimatedTypeHelpers.AddSingle(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueSingle(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueSingle(defaultDestinationValue))
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
					float value3 = AnimatedTypeHelpers.SubtractSingle(num3, num2);
					value = AnimatedTypeHelpers.ScaleSingle(value3, num4);
				}
			}
			return AnimatedTypeHelpers.AddSingle(value2, AnimatedTypeHelpers.AddSingle(value, AnimatedTypeHelpers.InterpolateSingle(num2, num3, num)));
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000F2A60 File Offset: 0x000F1E60
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new float[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new float[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new float[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new float[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new float[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000F2BE0 File Offset: 0x000F1FE0
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SingleAnimation singleAnimation = (SingleAnimation)d;
			singleAnimation._isAnimationFunctionValid = false;
			singleAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000F2C08 File Offset: 0x000F2008
		private static bool ValidateFromToOrByValue(object value)
		{
			float? num = (float?)value;
			return num == null || AnimatedTypeHelpers.IsValidAnimationValueSingle(num.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é nulo.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x000F2C34 File Offset: 0x000F2034
		// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x000F2C54 File Offset: 0x000F2054
		public float? From
		{
			get
			{
				return (float?)base.GetValue(SingleAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(SingleAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000F2C74 File Offset: 0x000F2074
		// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x000F2C94 File Offset: 0x000F2094
		public float? To
		{
			get
			{
				return (float?)base.GetValue(SingleAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(SingleAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x000F2CB4 File Offset: 0x000F20B4
		// (set) Token: 0x06003DA7 RID: 15783 RVA: 0x000F2CD4 File Offset: 0x000F20D4
		public float? By
		{
			get
			{
				return (float?)base.GetValue(SingleAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(SingleAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x000F2CF4 File Offset: 0x000F20F4
		// (set) Token: 0x06003DA9 RID: 15785 RVA: 0x000F2D14 File Offset: 0x000F2114
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(SingleAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(SingleAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x000F2D30 File Offset: 0x000F2130
		// (set) Token: 0x06003DAB RID: 15787 RVA: 0x000F2D50 File Offset: 0x000F2150
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
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000F2D70 File Offset: 0x000F2170
		// (set) Token: 0x06003DAD RID: 15789 RVA: 0x000F2D90 File Offset: 0x000F2190
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

		// Token: 0x04001732 RID: 5938
		private float[] _keyValues;

		// Token: 0x04001733 RID: 5939
		private AnimationType _animationType;

		// Token: 0x04001734 RID: 5940
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SingleAnimation.From" />.</summary>
		// Token: 0x04001735 RID: 5941
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SingleAnimation.To" />.</summary>
		// Token: 0x04001736 RID: 5942
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SingleAnimation.By" />.</summary>
		// Token: 0x04001737 RID: 5943
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.SingleAnimation.EasingFunction" />.</summary>
		// Token: 0x04001738 RID: 5944
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
