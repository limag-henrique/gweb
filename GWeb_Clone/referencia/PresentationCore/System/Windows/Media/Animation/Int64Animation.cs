using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Int64" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x020004F5 RID: 1269
	public class Int64Animation : Int64AnimationBase
	{
		// Token: 0x06003968 RID: 14696 RVA: 0x000E3AE8 File Offset: 0x000E2EE8
		static Int64Animation()
		{
			Type typeFromHandle = typeof(long?);
			Type typeFromHandle2 = typeof(Int64Animation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(Int64Animation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(Int64Animation.ValidateFromToOrByValue);
			Int64Animation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int64Animation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int64Animation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int64Animation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64Animation" />.</summary>
		// Token: 0x06003969 RID: 14697 RVA: 0x000E3B8C File Offset: 0x000E2F8C
		public Int64Animation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64Animation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x0600396A RID: 14698 RVA: 0x000E3BA0 File Offset: 0x000E2FA0
		public Int64Animation(long toValue, Duration duration) : this()
		{
			this.To = new long?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64Animation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x0600396B RID: 14699 RVA: 0x000E3BC8 File Offset: 0x000E2FC8
		public Int64Animation(long toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new long?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64Animation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x0600396C RID: 14700 RVA: 0x000E3BF8 File Offset: 0x000E2FF8
		public Int64Animation(long fromValue, long toValue, Duration duration) : this()
		{
			this.From = new long?(fromValue);
			this.To = new long?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int64Animation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x0600396D RID: 14701 RVA: 0x000E3C2C File Offset: 0x000E302C
		public Int64Animation(long fromValue, long toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new long?(fromValue);
			this.To = new long?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int64Animation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600396E RID: 14702 RVA: 0x000E3C68 File Offset: 0x000E3068
		public new Int64Animation Clone()
		{
			return (Int64Animation)base.Clone();
		}

		/// <summary>Implementação de <see cref="M:System.Windows.Freezable.CreateInstanceCore" />.</summary>
		/// <returns>O novo <see cref="T:System.Windows.Freezable" />.</returns>
		// Token: 0x0600396F RID: 14703 RVA: 0x000E3C80 File Offset: 0x000E3080
		protected override Freezable CreateInstanceCore()
		{
			return new Int64Animation();
		}

		/// <summary>Calcula o valor que essa animação acredita que deve ser o valor atual da propriedade.</summary>
		/// <param name="defaultOriginValue">Esse valor é o valor de origem sugeridas fornecido para a animação a ser usado se a animação não tiver seu próprio conceito de um valor inicial. Se essa animação for a primeira em uma cadeia de composição, esse valor será o valor de instantâneo, se houver um disponível ou o valor da propriedade base, se ele ainda não estiver; caso contrário, esse valor será o valor retornado pela animação anterior na cadeia com um animationClock que não esteja Parado.</param>
		/// <param name="defaultDestinationValue">Esse valor é o valor de destino sugeridas fornecido para a animação a ser usado se a animação não tiver seu próprio conceito de um valor final. Esse valor será o valor de base se a animação estiver na primeira camada de composição de animações em uma propriedade; caso contrário, será o valor de saída da camada de composição anterior de animações para a propriedade.</param>
		/// <param name="animationClock">Este é o animationClock que pode gerar o valor de CurrentTime ou CurrentProgress a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor que essa animação acredita que deve ser o valor atual da propriedade.</returns>
		// Token: 0x06003970 RID: 14704 RVA: 0x000E3C94 File Offset: 0x000E3094
		protected override long GetCurrentValueCore(long defaultOriginValue, long defaultDestinationValue, AnimationClock animationClock)
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
			long num2 = 0L;
			long num3 = 0L;
			long value = 0L;
			long value2 = 0L;
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
				num3 = AnimatedTypeHelpers.AddInt64(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueInt64(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueInt64(defaultDestinationValue))
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
					long value3 = AnimatedTypeHelpers.SubtractInt64(num3, num2);
					value = AnimatedTypeHelpers.ScaleInt64(value3, num4);
				}
			}
			return AnimatedTypeHelpers.AddInt64(value2, AnimatedTypeHelpers.AddInt64(value, AnimatedTypeHelpers.InterpolateInt64(num2, num3, num)));
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000E3E98 File Offset: 0x000E3298
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new long[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new long[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new long[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new long[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new long[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000E4018 File Offset: 0x000E3418
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Int64Animation int64Animation = (Int64Animation)d;
			int64Animation._isAnimationFunctionValid = false;
			int64Animation.PropertyChanged(e.Property);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000E4040 File Offset: 0x000E3440
		private static bool ValidateFromToOrByValue(object value)
		{
			long? num = (long?)value;
			return num == null || AnimatedTypeHelpers.IsValidAnimationValueInt64(num.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003974 RID: 14708 RVA: 0x000E406C File Offset: 0x000E346C
		// (set) Token: 0x06003975 RID: 14709 RVA: 0x000E408C File Offset: 0x000E348C
		public long? From
		{
			get
			{
				return (long?)base.GetValue(Int64Animation.FromProperty);
			}
			set
			{
				base.SetValueInternal(Int64Animation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O padrão é <see langword="null" />.</returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000E40AC File Offset: 0x000E34AC
		// (set) Token: 0x06003977 RID: 14711 RVA: 0x000E40CC File Offset: 0x000E34CC
		public long? To
		{
			get
			{
				return (long?)base.GetValue(Int64Animation.ToProperty);
			}
			set
			{
				base.SetValueInternal(Int64Animation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O padrão é <see langword="null" />.</returns>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x000E40EC File Offset: 0x000E34EC
		// (set) Token: 0x06003979 RID: 14713 RVA: 0x000E410C File Offset: 0x000E350C
		public long? By
		{
			get
			{
				return (long?)base.GetValue(Int64Animation.ByProperty);
			}
			set
			{
				base.SetValueInternal(Int64Animation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000E412C File Offset: 0x000E352C
		// (set) Token: 0x0600397B RID: 14715 RVA: 0x000E414C File Offset: 0x000E354C
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(Int64Animation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(Int64Animation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000E4168 File Offset: 0x000E3568
		// (set) Token: 0x0600397D RID: 14717 RVA: 0x000E4188 File Offset: 0x000E3588
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
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x000E41A8 File Offset: 0x000E35A8
		// (set) Token: 0x0600397F RID: 14719 RVA: 0x000E41C8 File Offset: 0x000E35C8
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

		// Token: 0x040016B1 RID: 5809
		private long[] _keyValues;

		// Token: 0x040016B2 RID: 5810
		private AnimationType _animationType;

		// Token: 0x040016B3 RID: 5811
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int64Animation.From" />.</summary>
		// Token: 0x040016B4 RID: 5812
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int64Animation.To" />.</summary>
		// Token: 0x040016B5 RID: 5813
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int64Animation.By" />.</summary>
		// Token: 0x040016B6 RID: 5814
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int64Animation.EasingFunction" />.</summary>
		// Token: 0x040016B7 RID: 5815
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
