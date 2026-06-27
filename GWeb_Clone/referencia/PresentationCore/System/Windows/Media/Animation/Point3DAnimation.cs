using System;
using System.Globalization;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Media.Media3D.Point3D" /> usando interpolação linear entre dois valores.</summary>
	// Token: 0x02000526 RID: 1318
	public class Point3DAnimation : Point3DAnimationBase
	{
		// Token: 0x06003BA8 RID: 15272 RVA: 0x000EA47C File Offset: 0x000E987C
		static Point3DAnimation()
		{
			Type typeFromHandle = typeof(Point3D?);
			Type typeFromHandle2 = typeof(Point3DAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(Point3DAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(Point3DAnimation.ValidateFromToOrByValue);
			Point3DAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Point3DAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Point3DAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Point3DAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DAnimation" />.</summary>
		// Token: 0x06003BA9 RID: 15273 RVA: 0x000EA520 File Offset: 0x000E9920
		public Point3DAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003BAA RID: 15274 RVA: 0x000EA534 File Offset: 0x000E9934
		public Point3DAnimation(Point3D toValue, Duration duration) : this()
		{
			this.To = new Point3D?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003BAB RID: 15275 RVA: 0x000EA55C File Offset: 0x000E995C
		public Point3DAnimation(Point3D toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Point3D?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003BAC RID: 15276 RVA: 0x000EA58C File Offset: 0x000E998C
		public Point3DAnimation(Point3D fromValue, Point3D toValue, Duration duration) : this()
		{
			this.From = new Point3D?(fromValue);
			this.To = new Point3D?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Point3DAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003BAD RID: 15277 RVA: 0x000EA5C0 File Offset: 0x000E99C0
		public Point3DAnimation(Point3D fromValue, Point3D toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Point3D?(fromValue);
			this.To = new Point3D?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Point3DAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003BAE RID: 15278 RVA: 0x000EA5FC File Offset: 0x000E99FC
		public new Point3DAnimation Clone()
		{
			return (Point3DAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.Point3DAnimation" />.</summary>
		/// <returns>O Point3Danimation é criado com todas as propriedades definidas como seu valor padrão.</returns>
		// Token: 0x06003BAF RID: 15279 RVA: 0x000EA614 File Offset: 0x000E9A14
		protected override Freezable CreateInstanceCore()
		{
			return new Point3DAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.Point3DAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003BB0 RID: 15280 RVA: 0x000EA628 File Offset: 0x000E9A28
		protected override Point3D GetCurrentValueCore(Point3D defaultOriginValue, Point3D defaultDestinationValue, AnimationClock animationClock)
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
			Point3D point3D = default(Point3D);
			Point3D point3D2 = default(Point3D);
			Point3D value = default(Point3D);
			Point3D value2 = default(Point3D);
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				point3D = defaultOriginValue;
				point3D2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				point3D = this._keyValues[0];
				point3D2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				point3D = defaultOriginValue;
				point3D2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				point3D2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				point3D = this._keyValues[0];
				point3D2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				point3D = this._keyValues[0];
				point3D2 = AnimatedTypeHelpers.AddPoint3D(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValuePoint3D(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValuePoint3D(defaultDestinationValue))
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
					Point3D value3 = AnimatedTypeHelpers.SubtractPoint3D(point3D2, point3D);
					value = AnimatedTypeHelpers.ScalePoint3D(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddPoint3D(value2, AnimatedTypeHelpers.AddPoint3D(value, AnimatedTypeHelpers.InterpolatePoint3D(point3D, point3D2, num)));
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000EA860 File Offset: 0x000E9C60
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Point3D[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Point3D[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Point3D[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Point3D[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Point3D[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000EA9FC File Offset: 0x000E9DFC
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Point3DAnimation point3DAnimation = (Point3DAnimation)d;
			point3DAnimation._isAnimationFunctionValid = false;
			point3DAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000EAA24 File Offset: 0x000E9E24
		private static bool ValidateFromToOrByValue(object value)
		{
			Point3D? point3D = (Point3D?)value;
			return point3D == null || AnimatedTypeHelpers.IsValidAnimationValuePoint3D(point3D.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000EAA50 File Offset: 0x000E9E50
		// (set) Token: 0x06003BB5 RID: 15285 RVA: 0x000EAA70 File Offset: 0x000E9E70
		public Point3D? From
		{
			get
			{
				return (Point3D?)base.GetValue(Point3DAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(Point3DAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000EAA90 File Offset: 0x000E9E90
		// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x000EAAB0 File Offset: 0x000E9EB0
		public Point3D? To
		{
			get
			{
				return (Point3D?)base.GetValue(Point3DAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(Point3DAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000EAAD0 File Offset: 0x000E9ED0
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000EAAF0 File Offset: 0x000E9EF0
		public Point3D? By
		{
			get
			{
				return (Point3D?)base.GetValue(Point3DAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(Point3DAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000EAB10 File Offset: 0x000E9F10
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x000EAB30 File Offset: 0x000E9F30
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(Point3DAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(Point3DAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000EAB4C File Offset: 0x000E9F4C
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000EAB6C File Offset: 0x000E9F6C
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
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000EAB8C File Offset: 0x000E9F8C
		// (set) Token: 0x06003BBF RID: 15295 RVA: 0x000EABAC File Offset: 0x000E9FAC
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

		// Token: 0x040016F3 RID: 5875
		private Point3D[] _keyValues;

		// Token: 0x040016F4 RID: 5876
		private AnimationType _animationType;

		// Token: 0x040016F5 RID: 5877
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Point3DAnimation.From" />.</summary>
		// Token: 0x040016F6 RID: 5878
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Point3DAnimation.To" />.</summary>
		// Token: 0x040016F7 RID: 5879
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Point3DAnimation.By" />.</summary>
		// Token: 0x040016F8 RID: 5880
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Point3DAnimation.EasingFunction" />.</summary>
		// Token: 0x040016F9 RID: 5881
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
