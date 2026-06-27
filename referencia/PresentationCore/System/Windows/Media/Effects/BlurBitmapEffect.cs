using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Simula a observação de um objeto através de uma lente fora de foco.</summary>
	// Token: 0x0200060E RID: 1550
	public sealed class BlurBitmapEffect : BitmapEffect
	{
		// Token: 0x06004739 RID: 18233 RVA: 0x00117538 File Offset: 0x00116938
		[SecuritySafeCritical]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected override SafeHandle CreateUnmanagedEffect()
		{
			return null;
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x00117548 File Offset: 0x00116948
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override void UpdateUnmanagedPropertyState(SafeHandle unmanagedEffect)
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x0011755C File Offset: 0x0011695C
		internal override bool CanBeEmulatedUsingEffectPipeline()
		{
			return this.Radius <= 100.0;
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x00117580 File Offset: 0x00116980
		internal override Effect GetEmulatingEffect()
		{
			if (this._imageEffectEmulation != null && this._imageEffectEmulation.IsFrozen)
			{
				return this._imageEffectEmulation;
			}
			if (this._imageEffectEmulation == null)
			{
				this._imageEffectEmulation = new BlurEffect();
			}
			double radius = this.Radius;
			if (this._imageEffectEmulation.Radius != radius)
			{
				this._imageEffectEmulation.Radius = radius;
			}
			KernelType kernelType = this.KernelType;
			if (this._imageEffectEmulation.KernelType != kernelType)
			{
				this._imageEffectEmulation.KernelType = kernelType;
			}
			this._imageEffectEmulation.RenderingBias = RenderingBias.Performance;
			if (base.IsFrozen)
			{
				this._imageEffectEmulation.Freeze();
			}
			return this._imageEffectEmulation;
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.BlurBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600473D RID: 18237 RVA: 0x00117624 File Offset: 0x00116A24
		public new BlurBitmapEffect Clone()
		{
			return (BlurBitmapEffect)base.Clone();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.BlurBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600473E RID: 18238 RVA: 0x0011763C File Offset: 0x00116A3C
		public new BlurBitmapEffect CloneCurrentValue()
		{
			return (BlurBitmapEffect)base.CloneCurrentValue();
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x00117654 File Offset: 0x00116A54
		private static void RadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BlurBitmapEffect blurBitmapEffect = (BlurBitmapEffect)d;
			blurBitmapEffect.PropertyChanged(BlurBitmapEffect.RadiusProperty);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Obtém ou define o raio usado no kernel do desfoque. Um raio maior implica mais desfoque.</summary>
		/// <returns>O raio usado no kernel do desfoque, DIU (1/96 de polegada). O valor padrão é 5.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x00117674 File Offset: 0x00116A74
		// (set) Token: 0x06004741 RID: 18241 RVA: 0x00117694 File Offset: 0x00116A94
		public double Radius
		{
			get
			{
				return (double)base.GetValue(BlurBitmapEffect.RadiusProperty);
			}
			set
			{
				base.SetValueInternal(BlurBitmapEffect.RadiusProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Obtém ou define o tipo de kernel de desfoque a ser usado para o <see cref="T:System.Windows.Media.Effects.BlurBitmapEffect" />.</summary>
		/// <returns>O tipo de kernel de desfoque. O valor padrão é <see cref="F:System.Windows.Media.Effects.KernelType.Gaussian" />.</returns>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x001176B4 File Offset: 0x00116AB4
		// (set) Token: 0x06004743 RID: 18243 RVA: 0x001176D4 File Offset: 0x00116AD4
		public KernelType KernelType
		{
			get
			{
				return (KernelType)base.GetValue(BlurBitmapEffect.KernelTypeProperty);
			}
			set
			{
				base.SetValueInternal(BlurBitmapEffect.KernelTypeProperty, value);
			}
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x001176F4 File Offset: 0x00116AF4
		protected override Freezable CreateInstanceCore()
		{
			return new BlurBitmapEffect();
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00117708 File Offset: 0x00116B08
		static BlurBitmapEffect()
		{
			Type typeFromHandle = typeof(BlurBitmapEffect);
			BlurBitmapEffect.RadiusProperty = Animatable.RegisterProperty("Radius", typeof(double), typeFromHandle, 5.0, new PropertyChangedCallback(BlurBitmapEffect.RadiusPropertyChanged), null, true, null);
			BlurBitmapEffect.KernelTypeProperty = Animatable.RegisterProperty("KernelType", typeof(KernelType), typeFromHandle, KernelType.Gaussian, null, new ValidateValueCallback(ValidateEnums.IsKernelTypeValid), false, null);
		}

		// Token: 0x040019F4 RID: 6644
		private BlurEffect _imageEffectEmulation;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BlurBitmapEffect.Radius" />.</summary>
		// Token: 0x040019F5 RID: 6645
		public static readonly DependencyProperty RadiusProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BlurBitmapEffect.KernelType" />.</summary>
		// Token: 0x040019F6 RID: 6646
		public static readonly DependencyProperty KernelTypeProperty;

		// Token: 0x040019F7 RID: 6647
		internal const double c_Radius = 5.0;

		// Token: 0x040019F8 RID: 6648
		internal const KernelType c_KernelType = KernelType.Gaussian;
	}
}
