using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Byte" /> entre dois valores de destino usando interpolação linear em uma <see cref="T:System.Windows.Duration" /> especificada.</summary>
	// Token: 0x020004B2 RID: 1202
	public class ByteAnimation : ByteAnimationBase
	{
		// Token: 0x060035EC RID: 13804 RVA: 0x000D71BC File Offset: 0x000D65BC
		static ByteAnimation()
		{
			Type typeFromHandle = typeof(byte?);
			Type typeFromHandle2 = typeof(ByteAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(ByteAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(ByteAnimation.ValidateFromToOrByValue);
			ByteAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			ByteAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			ByteAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			ByteAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteAnimation" />.</summary>
		// Token: 0x060035ED RID: 13805 RVA: 0x000D7260 File Offset: 0x000D6660
		public ByteAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060035EE RID: 13806 RVA: 0x000D7274 File Offset: 0x000D6674
		public ByteAnimation(byte toValue, Duration duration) : this()
		{
			this.To = new byte?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060035EF RID: 13807 RVA: 0x000D729C File Offset: 0x000D669C
		public ByteAnimation(byte toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new byte?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060035F0 RID: 13808 RVA: 0x000D72CC File Offset: 0x000D66CC
		public ByteAnimation(byte fromValue, byte toValue, Duration duration) : this()
		{
			this.From = new byte?(fromValue);
			this.To = new byte?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ByteAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060035F1 RID: 13809 RVA: 0x000D7300 File Offset: 0x000D6700
		public ByteAnimation(byte fromValue, byte toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new byte?(fromValue);
			this.To = new byte?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.ByteAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060035F2 RID: 13810 RVA: 0x000D733C File Offset: 0x000D673C
		public new ByteAnimation Clone()
		{
			return (ByteAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.ByteAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060035F3 RID: 13811 RVA: 0x000D7354 File Offset: 0x000D6754
		protected override Freezable CreateInstanceCore()
		{
			return new ByteAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.ByteAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x060035F4 RID: 13812 RVA: 0x000D7368 File Offset: 0x000D6768
		protected override byte GetCurrentValueCore(byte defaultOriginValue, byte defaultDestinationValue, AnimationClock animationClock)
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
			byte b = 0;
			byte b2 = 0;
			byte value = 0;
			byte value2 = 0;
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				b = defaultOriginValue;
				b2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				b = this._keyValues[0];
				b2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				b = defaultOriginValue;
				b2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				b2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				b = this._keyValues[0];
				b2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				b = this._keyValues[0];
				b2 = AnimatedTypeHelpers.AddByte(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueByte(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueByte(defaultDestinationValue))
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
					byte value3 = AnimatedTypeHelpers.SubtractByte(b2, b);
					value = AnimatedTypeHelpers.ScaleByte(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddByte(value2, AnimatedTypeHelpers.AddByte(value, AnimatedTypeHelpers.InterpolateByte(b, b2, num)));
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x000D7568 File Offset: 0x000D6968
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new byte[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new byte[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new byte[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new byte[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new byte[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x000D76E8 File Offset: 0x000D6AE8
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ByteAnimation byteAnimation = (ByteAnimation)d;
			byteAnimation._isAnimationFunctionValid = false;
			byteAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x060035F7 RID: 13815 RVA: 0x000D7710 File Offset: 0x000D6B10
		private static bool ValidateFromToOrByValue(object value)
		{
			byte? b = (byte?)value;
			return b == null || AnimatedTypeHelpers.IsValidAnimationValueByte(b.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x000D773C File Offset: 0x000D6B3C
		// (set) Token: 0x060035F9 RID: 13817 RVA: 0x000D775C File Offset: 0x000D6B5C
		public byte? From
		{
			get
			{
				return (byte?)base.GetValue(ByteAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(ByteAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000D777C File Offset: 0x000D6B7C
		// (set) Token: 0x060035FB RID: 13819 RVA: 0x000D779C File Offset: 0x000D6B9C
		public byte? To
		{
			get
			{
				return (byte?)base.GetValue(ByteAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(ByteAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é nulo.</returns>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x000D77BC File Offset: 0x000D6BBC
		// (set) Token: 0x060035FD RID: 13821 RVA: 0x000D77DC File Offset: 0x000D6BDC
		public byte? By
		{
			get
			{
				return (byte?)base.GetValue(ByteAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(ByteAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000D77FC File Offset: 0x000D6BFC
		// (set) Token: 0x060035FF RID: 13823 RVA: 0x000D781C File Offset: 0x000D6C1C
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(ByteAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(ByteAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000D7838 File Offset: 0x000D6C38
		// (set) Token: 0x06003601 RID: 13825 RVA: 0x000D7858 File Offset: 0x000D6C58
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
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000D7878 File Offset: 0x000D6C78
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x000D7898 File Offset: 0x000D6C98
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

		// Token: 0x04001652 RID: 5714
		private byte[] _keyValues;

		// Token: 0x04001653 RID: 5715
		private AnimationType _animationType;

		// Token: 0x04001654 RID: 5716
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ByteAnimation.From" />.</summary>
		// Token: 0x04001655 RID: 5717
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ByteAnimation.To" />.</summary>
		// Token: 0x04001656 RID: 5718
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ByteAnimation.By" />.</summary>
		// Token: 0x04001657 RID: 5719
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.ByteAnimation.EasingFunction" />.</summary>
		// Token: 0x04001658 RID: 5720
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
