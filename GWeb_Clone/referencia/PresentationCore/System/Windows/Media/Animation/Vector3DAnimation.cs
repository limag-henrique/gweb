using System;
using System.Globalization;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima o valor de uma propriedade Vector3D usando interpolação linear entre dois valores.</summary>
	// Token: 0x02000560 RID: 1376
	public class Vector3DAnimation : Vector3DAnimationBase
	{
		// Token: 0x06003FA5 RID: 16293 RVA: 0x000F9934 File Offset: 0x000F8D34
		static Vector3DAnimation()
		{
			Type typeFromHandle = typeof(Vector3D?);
			Type typeFromHandle2 = typeof(Vector3DAnimation);
			PropertyChangedCallback propertyChangedCallback = new PropertyChangedCallback(Vector3DAnimation.AnimationFunction_Changed);
			ValidateValueCallback validateValueCallback = new ValidateValueCallback(Vector3DAnimation.ValidateFromToOrByValue);
			Vector3DAnimation.FromProperty = DependencyProperty.Register("From", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Vector3DAnimation.ToProperty = DependencyProperty.Register("To", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Vector3DAnimation.ByProperty = DependencyProperty.Register("By", typeFromHandle, typeFromHandle2, new PropertyMetadata(null, propertyChangedCallback), validateValueCallback);
			Vector3DAnimation.EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeFromHandle2);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DAnimation" />.</summary>
		// Token: 0x06003FA6 RID: 16294 RVA: 0x000F99D8 File Offset: 0x000F8DD8
		public Vector3DAnimation()
		{
		}

		/// <summary>Inicializa uma nova instância da classe Vector3DAnimation com o valor de destino e a <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> especificados.</summary>
		/// <param name="toValue">O valor final dessa animação.</param>
		/// <param name="duration">A nova <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> da animação.</param>
		// Token: 0x06003FA7 RID: 16295 RVA: 0x000F99EC File Offset: 0x000F8DEC
		public Vector3DAnimation(Vector3D toValue, Duration duration) : this()
		{
			this.To = new Vector3D?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DAnimation" /> que é animada até o valor especificado pela duração especificada e que tem o comportamento de preenchimento especificado. O valor inicial da animação é o valor base da propriedade que está sendo animada ou a saída de outra animação.</summary>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003FA8 RID: 16296 RVA: 0x000F9A14 File Offset: 0x000F8E14
		public Vector3DAnimation(Vector3D toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.To = new Vector3D?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DAnimation" /> que é animada do valor inicial especificado para o valor de destino especificado durante o período especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		// Token: 0x06003FA9 RID: 16297 RVA: 0x000F9A44 File Offset: 0x000F8E44
		public Vector3DAnimation(Vector3D fromValue, Vector3D toValue, Duration duration) : this()
		{
			this.From = new Vector3D?(fromValue);
			this.To = new Vector3D?(toValue);
			base.Duration = duration;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.Vector3DAnimation" /> que é animada do valor inicial especificado até o valor de destino especificado durante a duração especificada e que tem o comportamento de preenchimento especificado.</summary>
		/// <param name="fromValue">O valor inicial da animação.</param>
		/// <param name="toValue">O valor de destino da animação.</param>
		/// <param name="duration">O período de tempo que a animação leva para ser reproduzida do início ao fim, uma única vez. Consulte a propriedade <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> para obter mais informações.</param>
		/// <param name="fillBehavior">Especifica como a animação se comporta quando ela não estiver ativa.</param>
		// Token: 0x06003FAA RID: 16298 RVA: 0x000F9A78 File Offset: 0x000F8E78
		public Vector3DAnimation(Vector3D fromValue, Vector3D toValue, Duration duration, FillBehavior fillBehavior) : this()
		{
			this.From = new Vector3D?(fromValue);
			this.To = new Vector3D?(toValue);
			base.Duration = duration;
			base.FillBehavior = fillBehavior;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Vector3DAnimation" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003FAB RID: 16299 RVA: 0x000F9AB4 File Offset: 0x000F8EB4
		public new Vector3DAnimation Clone()
		{
			return (Vector3DAnimation)base.Clone();
		}

		/// <summary>Cria uma nova instância do <see cref="T:System.Windows.Media.Animation.Vector3DAnimation" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003FAC RID: 16300 RVA: 0x000F9ACC File Offset: 0x000F8ECC
		protected override Freezable CreateInstanceCore()
		{
			return new Vector3DAnimation();
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.Vector3DAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003FAD RID: 16301 RVA: 0x000F9AE0 File Offset: 0x000F8EE0
		protected override Vector3D GetCurrentValueCore(Vector3D defaultOriginValue, Vector3D defaultDestinationValue, AnimationClock animationClock)
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
			Vector3D vector3D = default(Vector3D);
			Vector3D vector3D2 = default(Vector3D);
			Vector3D value = default(Vector3D);
			Vector3D value2 = default(Vector3D);
			bool flag = false;
			bool flag2 = false;
			switch (this._animationType)
			{
			case AnimationType.Automatic:
				vector3D = defaultOriginValue;
				vector3D2 = defaultDestinationValue;
				flag = true;
				flag2 = true;
				break;
			case AnimationType.From:
				vector3D = this._keyValues[0];
				vector3D2 = defaultDestinationValue;
				flag2 = true;
				break;
			case AnimationType.To:
				vector3D = defaultOriginValue;
				vector3D2 = this._keyValues[0];
				flag = true;
				break;
			case AnimationType.By:
				vector3D2 = this._keyValues[0];
				value2 = defaultOriginValue;
				flag = true;
				break;
			case AnimationType.FromTo:
				vector3D = this._keyValues[0];
				vector3D2 = this._keyValues[1];
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			case AnimationType.FromBy:
				vector3D = this._keyValues[0];
				vector3D2 = AnimatedTypeHelpers.AddVector3D(this._keyValues[0], this._keyValues[1]);
				if (this.IsAdditive)
				{
					value2 = defaultOriginValue;
					flag = true;
				}
				break;
			}
			if (flag && !AnimatedTypeHelpers.IsValidAnimationValueVector3D(defaultOriginValue))
			{
				throw new InvalidOperationException(SR.Get("Animation_Invalid_DefaultValue", new object[]
				{
					base.GetType(),
					"origin",
					defaultOriginValue.ToString(CultureInfo.InvariantCulture)
				}));
			}
			if (flag2 && !AnimatedTypeHelpers.IsValidAnimationValueVector3D(defaultDestinationValue))
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
					Vector3D value3 = AnimatedTypeHelpers.SubtractVector3D(vector3D2, vector3D);
					value = AnimatedTypeHelpers.ScaleVector3D(value3, num2);
				}
			}
			return AnimatedTypeHelpers.AddVector3D(value2, AnimatedTypeHelpers.AddVector3D(value, AnimatedTypeHelpers.InterpolateVector3D(vector3D, vector3D2, num)));
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x000F9D18 File Offset: 0x000F9118
		private void ValidateAnimationFunction()
		{
			this._animationType = AnimationType.Automatic;
			this._keyValues = null;
			if (this.From != null)
			{
				if (this.To != null)
				{
					this._animationType = AnimationType.FromTo;
					this._keyValues = new Vector3D[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.To.Value;
				}
				else if (this.By != null)
				{
					this._animationType = AnimationType.FromBy;
					this._keyValues = new Vector3D[2];
					this._keyValues[0] = this.From.Value;
					this._keyValues[1] = this.By.Value;
				}
				else
				{
					this._animationType = AnimationType.From;
					this._keyValues = new Vector3D[1];
					this._keyValues[0] = this.From.Value;
				}
			}
			else if (this.To != null)
			{
				this._animationType = AnimationType.To;
				this._keyValues = new Vector3D[1];
				this._keyValues[0] = this.To.Value;
			}
			else if (this.By != null)
			{
				this._animationType = AnimationType.By;
				this._keyValues = new Vector3D[1];
				this._keyValues[0] = this.By.Value;
			}
			this._isAnimationFunctionValid = true;
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x000F9EB4 File Offset: 0x000F92B4
		private static void AnimationFunction_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Vector3DAnimation vector3DAnimation = (Vector3DAnimation)d;
			vector3DAnimation._isAnimationFunctionValid = false;
			vector3DAnimation.PropertyChanged(e.Property);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x000F9EDC File Offset: 0x000F92DC
		private static bool ValidateFromToOrByValue(object value)
		{
			Vector3D? vector3D = (Vector3D?)value;
			return vector3D == null || AnimatedTypeHelpers.IsValidAnimationValueVector3D(vector3D.Value);
		}

		/// <summary>Obtém ou define o valor inicial da animação.</summary>
		/// <returns>O valor inicial da animação. O valor padrão é null.</returns>
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06003FB1 RID: 16305 RVA: 0x000F9F08 File Offset: 0x000F9308
		// (set) Token: 0x06003FB2 RID: 16306 RVA: 0x000F9F28 File Offset: 0x000F9328
		public Vector3D? From
		{
			get
			{
				return (Vector3D?)base.GetValue(Vector3DAnimation.FromProperty);
			}
			set
			{
				base.SetValueInternal(Vector3DAnimation.FromProperty, value);
			}
		}

		/// <summary>Obtém ou define o valor final da animação.</summary>
		/// <returns>O valor final da animação. O valor padrão é null.</returns>
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06003FB3 RID: 16307 RVA: 0x000F9F48 File Offset: 0x000F9348
		// (set) Token: 0x06003FB4 RID: 16308 RVA: 0x000F9F68 File Offset: 0x000F9368
		public Vector3D? To
		{
			get
			{
				return (Vector3D?)base.GetValue(Vector3DAnimation.ToProperty);
			}
			set
			{
				base.SetValueInternal(Vector3DAnimation.ToProperty, value);
			}
		}

		/// <summary>Obtém ou define a quantidade total pela qual a animação altera seu valor inicial.</summary>
		/// <returns>A quantidade total pela qual a animação altera seu valor inicial.     O valor padrão é null.</returns>
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x000F9F88 File Offset: 0x000F9388
		// (set) Token: 0x06003FB6 RID: 16310 RVA: 0x000F9FA8 File Offset: 0x000F93A8
		public Vector3D? By
		{
			get
			{
				return (Vector3D?)base.GetValue(Vector3DAnimation.ByProperty);
			}
			set
			{
				base.SetValueInternal(Vector3DAnimation.ByProperty, value);
			}
		}

		/// <summary>Obtém ou define a função de easing aplicada a essa animação.</summary>
		/// <returns>A função de easing aplicada a essa animação.</returns>
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x000F9FC8 File Offset: 0x000F93C8
		// (set) Token: 0x06003FB8 RID: 16312 RVA: 0x000F9FE8 File Offset: 0x000F93E8
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(Vector3DAnimation.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(Vector3DAnimation.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o valor atual da propriedade de destino deve ser adicionado ao valor inicial dessa animação.</summary>
		/// <returns>
		///   <see langword="true" /> Se a propriedade de destino atual do valor deve ser adicionado ao valor inicial desta animação; Caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x000FA004 File Offset: 0x000F9404
		// (set) Token: 0x06003FBA RID: 16314 RVA: 0x000FA024 File Offset: 0x000F9424
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
		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x000FA044 File Offset: 0x000F9444
		// (set) Token: 0x06003FBC RID: 16316 RVA: 0x000FA064 File Offset: 0x000F9464
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

		// Token: 0x04001776 RID: 6006
		private Vector3D[] _keyValues;

		// Token: 0x04001777 RID: 6007
		private AnimationType _animationType;

		// Token: 0x04001778 RID: 6008
		private bool _isAnimationFunctionValid;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Vector3DAnimation.From" />.</summary>
		// Token: 0x04001779 RID: 6009
		public static readonly DependencyProperty FromProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Vector3DAnimation.To" />.</summary>
		// Token: 0x0400177A RID: 6010
		public static readonly DependencyProperty ToProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Vector3DAnimation.By" />.</summary>
		// Token: 0x0400177B RID: 6011
		public static readonly DependencyProperty ByProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.Vector3DAnimation.EasingFunction" />.</summary>
		// Token: 0x0400177C RID: 6012
		public static readonly DependencyProperty EasingFunctionProperty;
	}
}
