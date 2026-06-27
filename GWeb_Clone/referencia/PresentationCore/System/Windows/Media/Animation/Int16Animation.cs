using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Int16" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x020004ED RID: 1261
	public class Int16Animation : Int16AnimationBase
	{
		// Token: 0x060038AC RID: 14508 RVA: 0x000E08C8 File Offset: 0x000DFCC8
		static Int16Animation()
		{
			Type typeFromHandle = typeof(short?);
			Type typeFromHandle2 = typeof(Int16Animation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(Int16Animation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(Int16Animation.ValidateFromToOrByValue);
			Int16Animation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int16Animation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int16Animation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Int16Animation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16Animation" />.</summary>
		// Token: 0x060038AD RID: 14509 RVA: 0x000E096C File Offset: 0x000DFD6C
		public Int16Animation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16Animation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060038AE RID: 14510 RVA: 0x000E0980 File Offset: 0x000DFD80
		public Int16Animation(short toValue, Duration duration) : this()
		{
			this.To = new short?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16Animation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060038AF RID: 14511 RVA: 0x000E09A8 File Offset: 0x000DFDA8
		public Int16Animation(short toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new short?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16Animation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x060038B0 RID: 14512 RVA: 0x000E09D8 File Offset: 0x000DFDD8
		public Int16Animation(short fromValue, short toValue, Duration duration) : this()
		{
			this.From = new short?(fromValue);
			this.To = new short?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Int16Animation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x060038B1 RID: 14513 RVA: 0x000E0A0C File Offset: 0x000DFE0C
		public Int16Animation(short fromValue, short toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new short?(fromValue);
			this.To = new short?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Int16Animation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060038B2 RID: 14514 RVA: 0x000E0A48 File Offset: 0x000DFE48
		public new Int16Animation Clone()
		{
			return (Int16Animation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.Int16Animation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060038B3 RID: 14515 RVA: 0x000E0A60 File Offset: 0x000DFE60
		protected override Freezable CreateInstanceCore()
		{
			return new Int16Animation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.Int16Animation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x060038B4 RID: 14516 RVA: 0x000E0A74 File Offset: 0x000DFE74
		protected override short GetCurrentValueCore(short defaultOriginValue, short defaultDestinationValue, AnimationClock animationClock)
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
			short num2 = 0;
			short num3 = 0;
			short value = 0;
			short value2 = 0;
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
				num3 = AnimatedTypeHelpers.AddInt16(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueInt16(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueInt16(defaultDestinationValue))
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
					short value3 = AnimatedTypeHelpers.SubtractInt16(num3, num2);
					value = AnimatedTypeHelpers.ScaleInt16(value3, num4);
				}
			}
			return AnimatedTypeHelpers.AddInt16(value2, AnimatedTypeHelpers.AddInt16(value, AnimatedTypeHelpers.InterpolateInt16(num2, num3, num)));
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000E0C74 File Offset: 0x000E0074
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new short[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new short[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new short[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new short[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new short[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000E0DF4 File Offset: 0x000E01F4
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Int16Animation int16Animation = (Int16Animation)d;
			int16Animation._isAnimationFunctionValid = false;
			int16Animation.PropertyChanged(e.Property);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000E0E1C File Offset: 0x000E021C
		private static bool ValidateFromToOrByValue(object value)
		{
			short? num = (short?)value;
			return num == null || AnimatedTypeHelpers.IsValidAnimationValueInt16(num.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é nulo.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060038B8 RID: 14520 RVA: 0x000E0E48 File Offset: 0x000E0248
		// (set) Token: 0x060038B9 RID: 14521 RVA: 0x000E0E68 File Offset: 0x000E0268
		public short? From
		{
			get
			{
				return (short?)base.GetValue(Int16Animation.FromProperty);
			}
			set
			{
				base.SetValueInternal(Int16Animation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x000E0E88 File Offset: 0x000E0288
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x000E0EA8 File Offset: 0x000E02A8
		public short? To
		{
			get
			{
				return (short?)base.GetValue(Int16Animation.ToProperty);
			}
			set
			{
				base.SetValueInternal(Int16Animation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000E0EC8 File Offset: 0x000E02C8
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x000E0EE8 File Offset: 0x000E02E8
		public short? By
		{
			get
			{
				return (short?)base.GetValue(Int16Animation.ByProperty);
			}
			set
			{
				base.SetValueInternal(Int16Animation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000E0F08 File Offset: 0x000E0308
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x000E0F28 File Offset: 0x000E0328
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(Int16Animation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(Int16Animation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000E0F44 File Offset: 0x000E0344
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x000E0F64 File Offset: 0x000E0364
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
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x000E0F84 File Offset: 0x000E0384
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x000E0FA4 File Offset: 0x000E03A4
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

		// Token: 0x04001699 RID: 5785
		private short[] _keyValues;

		// Token: 0x0400169A RID: 5786
		private AnimationType _animationType;

		// Token: 0x0400169B RID: 5787
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int16Animation.From" />.</summary>
		// Token: 0x0400169C RID: 5788
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int16Animation.To" />.</summary>
		// Token: 0x0400169D RID: 5789
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int16Animation.By" />.</summary>
		// Token: 0x0400169E RID: 5790
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Int16Animation.EasingFunction" />.</summary>
		// Token: 0x0400169F RID: 5791
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
