using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Point" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x0200052B RID: 1323
	public class PointAnimation : PointAnimationBase
	{
		// Token: 0x06003C09 RID: 15369 RVA: 0x000EBE70 File Offset: 0x000EB270
		static PointAnimation()
		{
			Type typeFromHandle = typeof(Point?);
			Type typeFromHandle2 = typeof(PointAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(PointAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(PointAnimation.ValidateFromToOrByValue);
			PointAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			PointAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			PointAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			PointAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointAnimation" />.</summary>
		// Token: 0x06003C0A RID: 15370 RVA: 0x000EBF14 File Offset: 0x000EB314
		public PointAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003C0B RID: 15371 RVA: 0x000EBF28 File Offset: 0x000EB328
		public PointAnimation(Point toValue, Duration duration) : this()
		{
			this.To = new Point?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003C0C RID: 15372 RVA: 0x000EBF50 File Offset: 0x000EB350
		public PointAnimation(Point toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Point?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003C0D RID: 15373 RVA: 0x000EBF80 File Offset: 0x000EB380
		public PointAnimation(Point fromValue, Point toValue, Duration duration) : this()
		{
			this.From = new Point?(fromValue);
			this.To = new Point?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.PointAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003C0E RID: 15374 RVA: 0x000EBFB4 File Offset: 0x000EB3B4
		public PointAnimation(Point fromValue, Point toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Point?(fromValue);
			this.To = new Point?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.PointAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003C0F RID: 15375 RVA: 0x000EBFF0 File Offset: 0x000EB3F0
		public new PointAnimation Clone()
		{
			return (PointAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.PointAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003C10 RID: 15376 RVA: 0x000EC008 File Offset: 0x000EB408
		protected override Freezable CreateInstanceCore()
		{
			return new PointAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.PointAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003C11 RID: 15377 RVA: 0x000EC01C File Offset: 0x000EB41C
		protected override Point GetCurrentValueCore(Point defaultOriginValue, Point defaultDestinationValue, AnimationClock animationClock)
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
			Point point = default(Point);
			Point point2 = default(Point);
			Point value = default(Point);
			Point value2 = default(Point);
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				point = defaultOriginValue;
				point2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				point = this._keyValues[0];
				point2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				point = defaultOriginValue;
				point2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				point2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				point = this._keyValues[0];
				point2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				point = this._keyValues[0];
				point2 = AnimatedTypeHelpers.AddPoint(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValuePoint(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValuePoint(defaultDestinationValue))
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
					Point value3 = AnimatedTypeHelpers.SubtractPoint(point2, point);
					value = AnimatedTypeHelpers.ScalePoint(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddPoint(value2, AnimatedTypeHelpers.AddPoint(value, AnimatedTypeHelpers.InterpolatePoint(point, point2, num)));
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x000EC254 File Offset: 0x000EB654
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Point[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Point[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Point[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Point[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Point[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000EC3F0 File Offset: 0x000EB7F0
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PointAnimation pointAnimation = (PointAnimation)d;
			pointAnimation._isAnimationFunctionValid = false;
			pointAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000EC418 File Offset: 0x000EB818
		private static bool ValidateFromToOrByValue(object value)
		{
			Point? point = (Point?)value;
			return point == null || AnimatedTypeHelpers.IsValidAnimationValuePoint(point.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000EC444 File Offset: 0x000EB844
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x000EC464 File Offset: 0x000EB864
		public Point? From
		{
			get
			{
				return (Point?)base.GetValue(PointAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(PointAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000EC484 File Offset: 0x000EB884
		// (set) Token: 0x06003C18 RID: 15384 RVA: 0x000EC4A4 File Offset: 0x000EB8A4
		public Point? To
		{
			get
			{
				return (Point?)base.GetValue(PointAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(PointAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000EC4C4 File Offset: 0x000EB8C4
		// (set) Token: 0x06003C1A RID: 15386 RVA: 0x000EC4E4 File Offset: 0x000EB8E4
		public Point? By
		{
			get
			{
				return (Point?)base.GetValue(PointAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(PointAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000EC504 File Offset: 0x000EB904
		// (set) Token: 0x06003C1C RID: 15388 RVA: 0x000EC524 File Offset: 0x000EB924
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(PointAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(PointAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000EC540 File Offset: 0x000EB940
		// (set) Token: 0x06003C1E RID: 15390 RVA: 0x000EC560 File Offset: 0x000EB960
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
		///   <see langword="true" /> Se a animação acumula seus valores ao seu <see cref="P:System.Windows.Media.Animation.Timeline.RepeatBehavior" /> propriedade faz com que ele Repita sua duração simples; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x000EC580 File Offset: 0x000EB980
		// (set) Token: 0x06003C20 RID: 15392 RVA: 0x000EC5A0 File Offset: 0x000EB9A0
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

		// Token: 0x040016FF RID: 5887
		private Point[] _keyValues;

		// Token: 0x04001700 RID: 5888
		private AnimationType _animationType;

		// Token: 0x04001701 RID: 5889
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PointAnimation.From" />.</summary>
		// Token: 0x04001702 RID: 5890
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PointAnimation.To" />.</summary>
		// Token: 0x04001703 RID: 5891
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PointAnimation.By" />.</summary>
		// Token: 0x04001704 RID: 5892
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.PointAnimation.EasingFunction" />.</summary>
		// Token: 0x04001705 RID: 5893
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
