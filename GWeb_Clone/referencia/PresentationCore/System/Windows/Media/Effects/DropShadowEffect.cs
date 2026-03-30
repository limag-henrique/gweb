using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Effects
{
	/// <summary>Um efeito de bitmap que pinta uma sombra em torno da textura de destino.</summary>
	// Token: 0x02000607 RID: 1543
	public sealed class DropShadowEffect : Effect
	{
		// Token: 0x060046DC RID: 18140 RVA: 0x001165C4 File Offset: 0x001159C4
		internal override Rect GetRenderBounds(Rect contentBounds)
		{
			Point point = default(Point);
			Point point2 = default(Point);
			double blurRadius = this.BlurRadius;
			point.X = contentBounds.TopLeft.X - blurRadius;
			point.Y = contentBounds.TopLeft.Y - blurRadius;
			point2.X = contentBounds.BottomRight.X + blurRadius;
			point2.Y = contentBounds.BottomRight.Y + blurRadius;
			double shadowDepth = this.ShadowDepth;
			double num = 0.017453292519943295 * this.Direction;
			double num2 = shadowDepth * Math.Cos(num);
			double num3 = shadowDepth * Math.Sin(num);
			if (num2 >= 0.0)
			{
				point2.X += num2;
			}
			else
			{
				point.X += num2;
			}
			if (num3 >= 0.0)
			{
				point.Y -= num3;
			}
			else
			{
				point2.Y -= num3;
			}
			return new Rect(point, point2);
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Effects.Effect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência desse objeto, esse método copia associações de dados e referências de recurso (que não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável dessa instância. O clone retornado é efetivamente uma cópia profunda do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do clone é <see langword="false" />.</returns>
		// Token: 0x060046DD RID: 18141 RVA: 0x001166DC File Offset: 0x00115ADC
		public new DropShadowEffect Clone()
		{
			return (DropShadowEffect)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.Effect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências de recursos, associações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060046DE RID: 18142 RVA: 0x001166F4 File Offset: 0x00115AF4
		public new DropShadowEffect CloneCurrentValue()
		{
			return (DropShadowEffect)base.CloneCurrentValue();
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x0011670C File Offset: 0x00115B0C
		private static void ShadowDepthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowEffect dropShadowEffect = (DropShadowEffect)d;
			dropShadowEffect.PropertyChanged(DropShadowEffect.ShadowDepthProperty);
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x0011672C File Offset: 0x00115B2C
		private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowEffect dropShadowEffect = (DropShadowEffect)d;
			dropShadowEffect.PropertyChanged(DropShadowEffect.ColorProperty);
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x0011674C File Offset: 0x00115B4C
		private static void DirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowEffect dropShadowEffect = (DropShadowEffect)d;
			dropShadowEffect.PropertyChanged(DropShadowEffect.DirectionProperty);
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x0011676C File Offset: 0x00115B6C
		private static void OpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowEffect dropShadowEffect = (DropShadowEffect)d;
			dropShadowEffect.PropertyChanged(DropShadowEffect.OpacityProperty);
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x0011678C File Offset: 0x00115B8C
		private static void BlurRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowEffect dropShadowEffect = (DropShadowEffect)d;
			dropShadowEffect.PropertyChanged(DropShadowEffect.BlurRadiusProperty);
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x001167AC File Offset: 0x00115BAC
		private static void RenderingBiasPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowEffect dropShadowEffect = (DropShadowEffect)d;
			dropShadowEffect.PropertyChanged(DropShadowEffect.RenderingBiasProperty);
		}

		/// <summary>Obtém ou define a distância da sombra abaixo da textura.</summary>
		/// <returns>A distância da sombra abaixo da textura. O padrão é 5.</returns>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x001167CC File Offset: 0x00115BCC
		// (set) Token: 0x060046E6 RID: 18150 RVA: 0x001167EC File Offset: 0x00115BEC
		public double ShadowDepth
		{
			get
			{
				return (double)base.GetValue(DropShadowEffect.ShadowDepthProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowEffect.ShadowDepthProperty, value);
			}
		}

		/// <summary>Obtém ou define a cor da sombra.</summary>
		/// <returns>A cor da sombra. O padrão é <see cref="P:System.Windows.Media.Colors.Black" />.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x0011680C File Offset: 0x00115C0C
		// (set) Token: 0x060046E8 RID: 18152 RVA: 0x0011682C File Offset: 0x00115C2C
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(DropShadowEffect.ColorProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowEffect.ColorProperty, value);
			}
		}

		/// <summary>Obtém ou define a direção da sombra.</summary>
		/// <returns>A direção da sombra, em graus. O padrão é 315.</returns>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x0011684C File Offset: 0x00115C4C
		// (set) Token: 0x060046EA RID: 18154 RVA: 0x0011686C File Offset: 0x00115C6C
		public double Direction
		{
			get
			{
				return (double)base.GetValue(DropShadowEffect.DirectionProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowEffect.DirectionProperty, value);
			}
		}

		/// <summary>Obtém ou define a opacidade da sombra.</summary>
		/// <returns>A opacidade da sombra. O padrão é 1.</returns>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x0011688C File Offset: 0x00115C8C
		// (set) Token: 0x060046EC RID: 18156 RVA: 0x001168AC File Offset: 0x00115CAC
		public double Opacity
		{
			get
			{
				return (double)base.GetValue(DropShadowEffect.OpacityProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowEffect.OpacityProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica o raio do efeito de desfoque da sombra.</summary>
		/// <returns>Um valor que indica o raio da sombra do efeito de desfoque. O padrão é 5.</returns>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x001168CC File Offset: 0x00115CCC
		// (set) Token: 0x060046EE RID: 18158 RVA: 0x001168EC File Offset: 0x00115CEC
		public double BlurRadius
		{
			get
			{
				return (double)base.GetValue(DropShadowEffect.BlurRadiusProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowEffect.BlurRadiusProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o sistema renderiza a sombra com ênfase na velocidade ou na qualidade.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.RenderingBias" /> valor que indica se o sistema renderiza a sombra com ênfase na velocidade ou na qualidade. O padrão é <see cref="F:System.Windows.Media.Effects.RenderingBias.Performance" />.</returns>
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x0011690C File Offset: 0x00115D0C
		// (set) Token: 0x060046F0 RID: 18160 RVA: 0x0011692C File Offset: 0x00115D2C
		public RenderingBias RenderingBias
		{
			get
			{
				return (RenderingBias)base.GetValue(DropShadowEffect.RenderingBiasProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowEffect.RenderingBiasProperty, value);
			}
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x0011694C File Offset: 0x00115D4C
		protected override Freezable CreateInstanceCore()
		{
			return new DropShadowEffect();
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x00116960 File Offset: 0x00115D60
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(DropShadowEffect.ShadowDepthProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(DropShadowEffect.ColorProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(DropShadowEffect.DirectionProperty, channel);
				DUCE.ResourceHandle animationResourceHandle4 = base.GetAnimationResourceHandle(DropShadowEffect.OpacityProperty, channel);
				DUCE.ResourceHandle animationResourceHandle5 = base.GetAnimationResourceHandle(DropShadowEffect.BlurRadiusProperty, channel);
				DUCE.MILCMD_DROPSHADOWEFFECT milcmd_DROPSHADOWEFFECT;
				milcmd_DROPSHADOWEFFECT.Type = MILCMD.MilCmdDropShadowEffect;
				milcmd_DROPSHADOWEFFECT.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_DROPSHADOWEFFECT.ShadowDepth = this.ShadowDepth;
				}
				milcmd_DROPSHADOWEFFECT.hShadowDepthAnimations = animationResourceHandle;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_DROPSHADOWEFFECT.Color = CompositionResourceManager.ColorToMilColorF(this.Color);
				}
				milcmd_DROPSHADOWEFFECT.hColorAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_DROPSHADOWEFFECT.Direction = this.Direction;
				}
				milcmd_DROPSHADOWEFFECT.hDirectionAnimations = animationResourceHandle3;
				if (animationResourceHandle4.IsNull)
				{
					milcmd_DROPSHADOWEFFECT.Opacity = this.Opacity;
				}
				milcmd_DROPSHADOWEFFECT.hOpacityAnimations = animationResourceHandle4;
				if (animationResourceHandle5.IsNull)
				{
					milcmd_DROPSHADOWEFFECT.BlurRadius = this.BlurRadius;
				}
				milcmd_DROPSHADOWEFFECT.hBlurRadiusAnimations = animationResourceHandle5;
				milcmd_DROPSHADOWEFFECT.RenderingBias = this.RenderingBias;
				channel.SendCommand((byte*)(&milcmd_DROPSHADOWEFFECT), sizeof(DUCE.MILCMD_DROPSHADOWEFFECT));
			}
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x00116AA0 File Offset: 0x00115EA0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DROPSHADOWEFFECT))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x00116ADC File Offset: 0x00115EDC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x00116B00 File Offset: 0x00115F00
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x00116B1C File Offset: 0x00115F1C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x00116B34 File Offset: 0x00115F34
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x00116B50 File Offset: 0x00115F50
		static DropShadowEffect()
		{
			Type typeFromHandle = typeof(DropShadowEffect);
			DropShadowEffect.ShadowDepthProperty = Animatable.RegisterProperty("ShadowDepth", typeof(double), typeFromHandle, 5.0, new PropertyChangedCallback(DropShadowEffect.ShadowDepthPropertyChanged), null, true, null);
			DropShadowEffect.ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeFromHandle, Colors.Black, new PropertyChangedCallback(DropShadowEffect.ColorPropertyChanged), null, true, null);
			DropShadowEffect.DirectionProperty = Animatable.RegisterProperty("Direction", typeof(double), typeFromHandle, 315.0, new PropertyChangedCallback(DropShadowEffect.DirectionPropertyChanged), null, true, null);
			DropShadowEffect.OpacityProperty = Animatable.RegisterProperty("Opacity", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(DropShadowEffect.OpacityPropertyChanged), null, true, null);
			DropShadowEffect.BlurRadiusProperty = Animatable.RegisterProperty("BlurRadius", typeof(double), typeFromHandle, 5.0, new PropertyChangedCallback(DropShadowEffect.BlurRadiusPropertyChanged), null, true, null);
			DropShadowEffect.RenderingBiasProperty = Animatable.RegisterProperty("RenderingBias", typeof(RenderingBias), typeFromHandle, RenderingBias.Performance, new PropertyChangedCallback(DropShadowEffect.RenderingBiasPropertyChanged), new ValidateValueCallback(ValidateEnums.IsRenderingBiasValid), false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowEffect.ShadowDepth" />.</summary>
		// Token: 0x040019D0 RID: 6608
		public static readonly DependencyProperty ShadowDepthProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowEffect.Color" />.</summary>
		// Token: 0x040019D1 RID: 6609
		public static readonly DependencyProperty ColorProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowEffect.Direction" />.</summary>
		// Token: 0x040019D2 RID: 6610
		public static readonly DependencyProperty DirectionProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowEffect.Opacity" />.</summary>
		// Token: 0x040019D3 RID: 6611
		public static readonly DependencyProperty OpacityProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowEffect.BlurRadius" />.</summary>
		// Token: 0x040019D4 RID: 6612
		public static readonly DependencyProperty BlurRadiusProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowEffect.RenderingBias" />.</summary>
		// Token: 0x040019D5 RID: 6613
		public static readonly DependencyProperty RenderingBiasProperty;

		// Token: 0x040019D6 RID: 6614
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040019D7 RID: 6615
		internal const double c_ShadowDepth = 5.0;

		// Token: 0x040019D8 RID: 6616
		internal static Color s_Color = Colors.Black;

		// Token: 0x040019D9 RID: 6617
		internal const double c_Direction = 315.0;

		// Token: 0x040019DA RID: 6618
		internal const double c_Opacity = 1.0;

		// Token: 0x040019DB RID: 6619
		internal const double c_BlurRadius = 5.0;

		// Token: 0x040019DC RID: 6620
		internal const RenderingBias c_RenderingBias = RenderingBias.Performance;
	}
}
