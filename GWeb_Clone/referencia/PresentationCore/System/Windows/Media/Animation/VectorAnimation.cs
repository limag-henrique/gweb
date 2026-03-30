using System;
using System.Globalization;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade <see cref="T:System.Windows.Vector" /> entre dois valores de destino usando interpolação linear em uma <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificada.</summary>
	// Token: 0x02000565 RID: 1381
	public class VectorAnimation : VectorAnimationBase
	{
		// Token: 0x06004006 RID: 16390 RVA: 0x000FB328 File Offset: 0x000FA728
		static VectorAnimation()
		{
			Type typeFromHandle = typeof(Vector?);
			Type typeFromHandle2 = typeof(VectorAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(VectorAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(VectorAnimation.ValidateFromToOrByValue);
			VectorAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			VectorAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			VectorAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			VectorAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorAnimation" />.</summary>
		// Token: 0x06004007 RID: 16391 RVA: 0x000FB3CC File Offset: 0x000FA7CC
		public VectorAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorAnimation" /> que é animada até o valor especificado durante a duração especificada. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06004008 RID: 16392 RVA: 0x000FB3E0 File Offset: 0x000FA7E0
		public VectorAnimation(Vector toValue, Duration duration) : this()
		{
			this.To = new Vector?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06004009 RID: 16393 RVA: 0x000FB408 File Offset: 0x000FA808
		public VectorAnimation(Vector toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Vector?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x0600400A RID: 16394 RVA: 0x000FB438 File Offset: 0x000FA838
		public VectorAnimation(Vector fromValue, Vector toValue, Duration duration) : this()
		{
			this.From = new Vector?(fromValue);
			this.To = new Vector?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.VectorAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x0600400B RID: 16395 RVA: 0x000FB46C File Offset: 0x000FA86C
		public VectorAnimation(Vector fromValue, Vector toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Vector?(fromValue);
			this.To = new Vector?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.VectorAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600400C RID: 16396 RVA: 0x000FB4A8 File Offset: 0x000FA8A8
		public new VectorAnimation Clone()
		{
			return (VectorAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.VectorAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600400D RID: 16397 RVA: 0x000FB4C0 File Offset: 0x000FA8C0
		protected override Freezable CreateInstanceCore()
		{
			return new VectorAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.VectorAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x0600400E RID: 16398 RVA: 0x000FB4D4 File Offset: 0x000FA8D4
		protected override Vector GetCurrentValueCore(Vector defaultOriginValue, Vector defaultDestinationValue, AnimationClock animationClock)
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
			Vector vector = default(Vector);
			Vector vector2 = default(Vector);
			Vector value = default(Vector);
			Vector value2 = default(Vector);
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				vector = defaultOriginValue;
				vector2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				vector = this._keyValues[0];
				vector2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				vector = defaultOriginValue;
				vector2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				vector2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				vector = this._keyValues[0];
				vector2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				vector = this._keyValues[0];
				vector2 = AnimatedTypeHelpers.AddVector(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueVector(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueVector(defaultDestinationValue))
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
					Vector value3 = AnimatedTypeHelpers.SubtractVector(vector2, vector);
					value = AnimatedTypeHelpers.ScaleVector(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddVector(value2, AnimatedTypeHelpers.AddVector(value, AnimatedTypeHelpers.InterpolateVector(vector, vector2, num)));
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000FB70C File Offset: 0x000FAB0C
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Vector[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Vector[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Vector[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Vector[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Vector[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x000FB8A8 File Offset: 0x000FACA8
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			VectorAnimation vectorAnimation = (VectorAnimation)d;
			vectorAnimation._isAnimationFunctionValid = false;
			vectorAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x000FB8D0 File Offset: 0x000FACD0
		private static bool ValidateFromToOrByValue(object value)
		{
			Vector? vector = (Vector?)value;
			return vector == null || AnimatedTypeHelpers.IsValidAnimationValueVector(vector.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000FB8FC File Offset: 0x000FACFC
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x000FB91C File Offset: 0x000FAD1C
		public Vector? From
		{
			get
			{
				return (Vector?)base.GetValue(VectorAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(VectorAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000FB93C File Offset: 0x000FAD3C
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x000FB95C File Offset: 0x000FAD5C
		public Vector? To
		{
			get
			{
				return (Vector?)base.GetValue(VectorAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(VectorAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000FB97C File Offset: 0x000FAD7C
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x000FB99C File Offset: 0x000FAD9C
		public Vector? By
		{
			get
			{
				return (Vector?)base.GetValue(VectorAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(VectorAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000FB9BC File Offset: 0x000FADBC
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x000FB9DC File Offset: 0x000FADDC
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(VectorAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(VectorAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x000FB9F8 File Offset: 0x000FADF8
		// (set) Token: 0x0600401B RID: 16411 RVA: 0x000FBA18 File Offset: 0x000FAE18
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
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x000FBA38 File Offset: 0x000FAE38
		// (set) Token: 0x0600401D RID: 16413 RVA: 0x000FBA58 File Offset: 0x000FAE58
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

		// Token: 0x04001782 RID: 6018
		private Vector[] _keyValues;

		// Token: 0x04001783 RID: 6019
		private AnimationType _animationType;

		// Token: 0x04001784 RID: 6020
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.VectorAnimation.From" />.</summary>
		// Token: 0x04001785 RID: 6021
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.VectorAnimation.To" />.</summary>
		// Token: 0x04001786 RID: 6022
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.VectorAnimation.By" />.</summary>
		// Token: 0x04001787 RID: 6023
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.VectorAnimation.EasingFunction" />.</summary>
		// Token: 0x04001788 RID: 6024
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
