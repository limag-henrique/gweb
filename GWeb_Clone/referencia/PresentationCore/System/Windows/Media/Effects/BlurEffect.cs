using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Effects
{
	/// <summary>Um efeito de bitmap que desfoca a textura de destino.</summary>
	// Token: 0x02000606 RID: 1542
	public sealed class BlurEffect : Effect
	{
		// Token: 0x060046C7 RID: 18119 RVA: 0x001161B0 File Offset: 0x001155B0
		internal override Rect GetRenderBounds(Rect contentBounds)
		{
			Point point = default(Point);
			Point point2 = default(Point);
			double radius = this.Radius;
			point.X = contentBounds.TopLeft.X - radius;
			point.Y = contentBounds.TopLeft.Y - radius;
			point2.X = contentBounds.BottomRight.X + radius;
			point2.Y = contentBounds.BottomRight.Y + radius;
			return new Rect(point, point2);
		}

		/// <summary>Cria um clone modificável deste objeto <see cref="T:System.Windows.Media.Effects.Effect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência desse objeto, esse método copia associações de dados e referências de recurso (que não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável dessa instância. O clone retornado é efetivamente uma cópia profunda do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do clone é <see langword="false" />.</returns>
		// Token: 0x060046C8 RID: 18120 RVA: 0x0011623C File Offset: 0x0011563C
		public new BlurEffect Clone()
		{
			return (BlurEffect)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.Effect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências de recursos, associações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060046C9 RID: 18121 RVA: 0x00116254 File Offset: 0x00115654
		public new BlurEffect CloneCurrentValue()
		{
			return (BlurEffect)base.CloneCurrentValue();
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x0011626C File Offset: 0x0011566C
		private static void RadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BlurEffect blurEffect = (BlurEffect)d;
			blurEffect.PropertyChanged(BlurEffect.RadiusProperty);
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x0011628C File Offset: 0x0011568C
		private static void KernelTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BlurEffect blurEffect = (BlurEffect)d;
			blurEffect.PropertyChanged(BlurEffect.KernelTypeProperty);
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x001162AC File Offset: 0x001156AC
		private static void RenderingBiasPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BlurEffect blurEffect = (BlurEffect)d;
			blurEffect.PropertyChanged(BlurEffect.RenderingBiasProperty);
		}

		/// <summary>Obtém ou define um valor que indica o raio da curva do efeito de desfoque.</summary>
		/// <returns>O raio da curva do efeito de desfoque. O padrão é 5.</returns>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060046CD RID: 18125 RVA: 0x001162CC File Offset: 0x001156CC
		// (set) Token: 0x060046CE RID: 18126 RVA: 0x001162EC File Offset: 0x001156EC
		public double Radius
		{
			get
			{
				return (double)base.GetValue(BlurEffect.RadiusProperty);
			}
			set
			{
				base.SetValueInternal(BlurEffect.RadiusProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que representa a curva que é usada para calcular o desfoque.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.KernelType" /> que representa a curva que é usada para calcular o desfoque. O padrão é <see cref="F:System.Windows.Media.Effects.KernelType.Gaussian" />.</returns>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060046CF RID: 18127 RVA: 0x0011630C File Offset: 0x0011570C
		// (set) Token: 0x060046D0 RID: 18128 RVA: 0x0011632C File Offset: 0x0011572C
		public KernelType KernelType
		{
			get
			{
				return (KernelType)base.GetValue(BlurEffect.KernelTypeProperty);
			}
			set
			{
				base.SetValueInternal(BlurEffect.KernelTypeProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o sistema renderiza um efeito com ênfase na velocidade ou na qualidade.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Effects.RenderingBias" /> valor que indica se o sistema renderiza um efeito com ênfase na velocidade ou na qualidade. O padrão é <see cref="F:System.Windows.Media.Effects.RenderingBias.Performance" />.</returns>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060046D1 RID: 18129 RVA: 0x0011634C File Offset: 0x0011574C
		// (set) Token: 0x060046D2 RID: 18130 RVA: 0x0011636C File Offset: 0x0011576C
		public RenderingBias RenderingBias
		{
			get
			{
				return (RenderingBias)base.GetValue(BlurEffect.RenderingBiasProperty);
			}
			set
			{
				base.SetValueInternal(BlurEffect.RenderingBiasProperty, value);
			}
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x0011638C File Offset: 0x0011578C
		protected override Freezable CreateInstanceCore()
		{
			return new BlurEffect();
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x001163A0 File Offset: 0x001157A0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(BlurEffect.RadiusProperty, channel);
				DUCE.MILCMD_BLUREFFECT milcmd_BLUREFFECT;
				milcmd_BLUREFFECT.Type = MILCMD.MilCmdBlurEffect;
				milcmd_BLUREFFECT.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_BLUREFFECT.Radius = this.Radius;
				}
				milcmd_BLUREFFECT.hRadiusAnimations = animationResourceHandle;
				milcmd_BLUREFFECT.KernelType = this.KernelType;
				milcmd_BLUREFFECT.RenderingBias = this.RenderingBias;
				channel.SendCommand((byte*)(&milcmd_BLUREFFECT), sizeof(DUCE.MILCMD_BLUREFFECT));
			}
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x0011643C File Offset: 0x0011583C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_BLUREFFECT))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x00116478 File Offset: 0x00115878
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x0011649C File Offset: 0x0011589C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x001164B8 File Offset: 0x001158B8
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x001164D0 File Offset: 0x001158D0
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x001164EC File Offset: 0x001158EC
		static BlurEffect()
		{
			Type typeFromHandle = typeof(BlurEffect);
			BlurEffect.RadiusProperty = Animatable.RegisterProperty("Radius", typeof(double), typeFromHandle, 5.0, new PropertyChangedCallback(BlurEffect.RadiusPropertyChanged), null, true, null);
			BlurEffect.KernelTypeProperty = Animatable.RegisterProperty("KernelType", typeof(KernelType), typeFromHandle, KernelType.Gaussian, new PropertyChangedCallback(BlurEffect.KernelTypePropertyChanged), new ValidateValueCallback(ValidateEnums.IsKernelTypeValid), false, null);
			BlurEffect.RenderingBiasProperty = Animatable.RegisterProperty("RenderingBias", typeof(RenderingBias), typeFromHandle, RenderingBias.Performance, new PropertyChangedCallback(BlurEffect.RenderingBiasPropertyChanged), new ValidateValueCallback(ValidateEnums.IsRenderingBiasValid), false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BlurEffect.Radius" />.</summary>
		// Token: 0x040019C9 RID: 6601
		public static readonly DependencyProperty RadiusProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BlurEffect.KernelType" />.</summary>
		// Token: 0x040019CA RID: 6602
		public static readonly DependencyProperty KernelTypeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BlurEffect.RenderingBias" />.</summary>
		// Token: 0x040019CB RID: 6603
		public static readonly DependencyProperty RenderingBiasProperty;

		// Token: 0x040019CC RID: 6604
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040019CD RID: 6605
		internal const double c_Radius = 5.0;

		// Token: 0x040019CE RID: 6606
		internal const KernelType c_KernelType = KernelType.Gaussian;

		// Token: 0x040019CF RID: 6607
		internal const RenderingBias c_RenderingBias = RenderingBias.Performance;
	}
}
