using System;
using System.Globalization;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Media.Media3D.Quaternion" /> entre dois valores de destino usando uma interpolação linear em um <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificado.</summary>
	// Token: 0x02000531 RID: 1329
	public class QuaternionAnimation : QuaternionAnimationBase
	{
		// Token: 0x06003C6F RID: 15471 RVA: 0x000ED930 File Offset: 0x000ECD30
		static QuaternionAnimation()
		{
			Type typeFromHandle = typeof(Quaternion?);
			Type typeFromHandle2 = typeof(QuaternionAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(QuaternionAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(QuaternionAnimation.ValidateFromToOrByValue);
			QuaternionAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			QuaternionAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			QuaternionAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			QuaternionAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Animation.QuaternionAnimation" />.</summary>
		// Token: 0x06003C70 RID: 15472 RVA: 0x000EDA00 File Offset: 0x000ECE00
		public QuaternionAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Animation.QuaternionAnimation" /> usando o <see cref="T:System.Windows.Media.Media3D.Quaternion" /> e o <see cref="T:System.Windows.Duration" /> especificados.</summary>
		/// <param name="toValue">O quaternião para o qual animar.</param>
		/// <param name="duration">Duração da QuaternionAnimation.</param>
		// Token: 0x06003C71 RID: 15473 RVA: 0x000EDA14 File Offset: 0x000ECE14
		public QuaternionAnimation(Quaternion toValue, Duration duration) : this()
		{
			this.To = new Quaternion?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Animation.QuaternionAnimation" /> usando o <see cref="T:System.Windows.Media.Media3D.Quaternion" />, o <see cref="T:System.Windows.Duration" /> e o <see cref="T:System.Windows.Media.Animation.FillBehavior" /> especificados.</summary>
		/// <param name="toValue">O quaternião para o qual animar.</param>
		/// <param name="duration">Duração da QuaternionAnimation.</param>
		/// <param name="fillBehavior">O comportamento da linha do tempo fora de seu período ativo.</param>
		// Token: 0x06003C72 RID: 15474 RVA: 0x000EDA3C File Offset: 0x000ECE3C
		public QuaternionAnimation(Quaternion toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Quaternion?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Animation.QuaternionAnimation" /> usando o <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificado para outro <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificado pelo <see cref="T:System.Windows.Duration" /> especificado.</summary>
		/// <param name="fromValue">O quaternião do qual animar.</param>
		/// <param name="toValue">O quaternião para o qual animar.</param>
		/// <param name="duration">Duração da QuaternionAnimation.</param>
		// Token: 0x06003C73 RID: 15475 RVA: 0x000EDA6C File Offset: 0x000ECE6C
		public QuaternionAnimation(Quaternion fromValue, Quaternion toValue, Duration duration) : this()
		{
			this.From = new Quaternion?(fromValue);
			this.To = new Quaternion?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância de <see cref="T:System.Windows.Media.Animation.QuaternionAnimation" /> usando o <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificado para outro <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificado pelo <see cref="T:System.Windows.Duration" /> especificado, com o comportamento especificado no final da linha do tempo.</summary>
		/// <param name="fromValue">O quaternião do qual animar.</param>
		/// <param name="toValue">O quaternião para o qual animar.</param>
		/// <param name="duration">Duração da QuaternionAnimation.</param>
		/// <param name="fillBehavior">O comportamento da linha do tempo fora de seu período ativo.</param>
		// Token: 0x06003C74 RID: 15476 RVA: 0x000EDAA0 File Offset: 0x000ECEA0
		public QuaternionAnimation(Quaternion fromValue, Quaternion toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Quaternion?(fromValue);
			this.To = new Quaternion?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.Quaternion" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003C75 RID: 15477 RVA: 0x000EDADC File Offset: 0x000ECEDC
		public new QuaternionAnimation Clone()
		{
			return (QuaternionAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.QuaternionAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003C76 RID: 15478 RVA: 0x000EDAF4 File Offset: 0x000ECEF4
		protected override Freezable CreateInstanceCore()
		{
			return new QuaternionAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.QuaternionAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003C77 RID: 15479 RVA: 0x000EDB08 File Offset: 0x000ECF08
		protected override Quaternion GetCurrentValueCore(Quaternion defaultOriginValue, Quaternion defaultDestinationValue, AnimationClock animationClock)
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
			Quaternion quaternion = Quaternion.Identity;
			Quaternion quaternion2 = Quaternion.Identity;
			Quaternion value = Quaternion.Identity;
			Quaternion value2 = Quaternion.Identity;
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				quaternion = defaultOriginValue;
				quaternion2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				quaternion = this._keyValues[0];
				quaternion2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				quaternion = defaultOriginValue;
				quaternion2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				quaternion2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				quaternion = this._keyValues[0];
				quaternion2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				quaternion = this._keyValues[0];
				quaternion2 = AnimatedTypeHelpers.AddQuaternion(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueQuaternion(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueQuaternion(defaultDestinationValue))
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
					Quaternion value3 = AnimatedTypeHelpers.SubtractQuaternion(quaternion2, quaternion);
					value = AnimatedTypeHelpers.ScaleQuaternion(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddQuaternion(value2, AnimatedTypeHelpers.AddQuaternion(value, AnimatedTypeHelpers.InterpolateQuaternion(quaternion, quaternion2, num, this.UseShortestPath)));
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000EDD40 File Offset: 0x000ED140
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Quaternion[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Quaternion[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Quaternion[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Quaternion[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Quaternion[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000EDEDC File Offset: 0x000ED2DC
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			QuaternionAnimation quaternionAnimation = (QuaternionAnimation)d;
			quaternionAnimation._isAnimationFunctionValid = false;
			quaternionAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000EDF04 File Offset: 0x000ED304
		private static bool ValidateFromToOrByValue(object value)
		{
			Quaternion? quaternion = (Quaternion?)value;
			return quaternion == null || AnimatedTypeHelpers.IsValidAnimationValueQuaternion(quaternion.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x000EDF30 File Offset: 0x000ED330
		// (set) Token: 0x06003C7C RID: 15484 RVA: 0x000EDF50 File Offset: 0x000ED350
		public Quaternion? From
		{
			get
			{
				return (Quaternion?)base.GetValue(QuaternionAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(QuaternionAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000EDF70 File Offset: 0x000ED370
		// (set) Token: 0x06003C7E RID: 15486 RVA: 0x000EDF90 File Offset: 0x000ED390
		public Quaternion? To
		{
			get
			{
				return (Quaternion?)base.GetValue(QuaternionAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(QuaternionAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000EDFB0 File Offset: 0x000ED3B0
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x000EDFD0 File Offset: 0x000ED3D0
		public Quaternion? By
		{
			get
			{
				return (Quaternion?)base.GetValue(QuaternionAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(QuaternionAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x000EDFF0 File Offset: 0x000ED3F0
		// (set) Token: 0x06003C82 RID: 15490 RVA: 0x000EE010 File Offset: 0x000ED410
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(QuaternionAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(QuaternionAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x000EE02C File Offset: 0x000ED42C
		// (set) Token: 0x06003C84 RID: 15492 RVA: 0x000EE04C File Offset: 0x000ED44C
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
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000EE06C File Offset: 0x000ED46C
		// (set) Token: 0x06003C86 RID: 15494 RVA: 0x000EE08C File Offset: 0x000ED48C
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

		/// <summary>Obtém ou define um valor booliano que indica se a animação usa interpolação linear esférica para calcular o arco mais curto entre posições.</summary>
		/// <returns>Valor booliano que indica se a animação usa interpolação linear esférica para calcular o arco mais curto entre posições.</returns>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000EE0AC File Offset: 0x000ED4AC
		// (set) Token: 0x06003C88 RID: 15496 RVA: 0x000EE0CC File Offset: 0x000ED4CC
		public bool UseShortestPath
		{
			get
			{
				return (bool)base.GetValue(QuaternionAnimation.UseShortestPathProperty);
			}
			set
			{
				base.SetValue(QuaternionAnimation.UseShortestPathProperty, value);
			}
		}

		// Token: 0x0400170C RID: 5900
		private Quaternion[] _keyValues;

		// Token: 0x0400170D RID: 5901
		private AnimationType _animationType;

		// Token: 0x0400170E RID: 5902
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.QuaternionAnimation.From" />.</summary>
		// Token: 0x0400170F RID: 5903
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.QuaternionAnimation.To" />.</summary>
		// Token: 0x04001710 RID: 5904
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.QuaternionAnimation.By" />.</summary>
		// Token: 0x04001711 RID: 5905
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.QuaternionAnimation.EasingFunction" />.</summary>
		// Token: 0x04001712 RID: 5906
		public static readonly DependencyProperty EasingFunctionProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.QuaternionAnimation.UseShortestPath" />.</summary>
		// Token: 0x04001713 RID: 5907
		public static readonly DependencyProperty UseShortestPathProperty = DependencyProperty.Register("UseShortestPath", typeof(bool), typeof(QuaternionAnimation), new PropertyMetadata(BooleanBoxes.TrueBox));
	}
}
