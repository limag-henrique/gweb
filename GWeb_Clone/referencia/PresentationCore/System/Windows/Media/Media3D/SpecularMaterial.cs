using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media.Media3D
{
	/// <summary>Permite que um pincel 2D, como um <see cref="T:System.Windows.Media.SolidColorBrush" /> ou <see cref="T:System.Windows.Media.TileBrush" />, seja aplicado a um modelo 3D de luz especular.</summary>
	// Token: 0x0200047F RID: 1151
	public sealed class SpecularMaterial : Material
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.SpecularMaterial" />.</summary>
		// Token: 0x060031A6 RID: 12710 RVA: 0x000C67BC File Offset: 0x000C5BBC
		public SpecularMaterial()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.SpecularMaterial" /> com o pincel e o expoente especular especificados.</summary>
		/// <param name="brush">O pincel aplicado pelo novo <see cref="T:System.Windows.Media.Media3D.SpecularMaterial" />.</param>
		/// <param name="specularPower">O expoente especular.</param>
		// Token: 0x060031A7 RID: 12711 RVA: 0x000C67D0 File Offset: 0x000C5BD0
		public SpecularMaterial(Brush brush, double specularPower)
		{
			this.Brush = brush;
			this.SpecularPower = specularPower;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.SpecularMaterial" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060031A8 RID: 12712 RVA: 0x000C67F4 File Offset: 0x000C5BF4
		public new SpecularMaterial Clone()
		{
			return (SpecularMaterial)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.SpecularMaterial" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060031A9 RID: 12713 RVA: 0x000C680C File Offset: 0x000C5C0C
		public new SpecularMaterial CloneCurrentValue()
		{
			return (SpecularMaterial)base.CloneCurrentValue();
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000C6824 File Offset: 0x000C5C24
		private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SpecularMaterial specularMaterial = (SpecularMaterial)d;
			specularMaterial.PropertyChanged(SpecularMaterial.ColorProperty);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000C6844 File Offset: 0x000C5C44
		private static void BrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			SpecularMaterial specularMaterial = (SpecularMaterial)d;
			Brush resource = (Brush)e.OldValue;
			Brush resource2 = (Brush)e.NewValue;
			Dispatcher dispatcher = specularMaterial.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = specularMaterial;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						specularMaterial.ReleaseResource(resource, channel);
						specularMaterial.AddRefResource(resource2, channel);
					}
				}
			}
			specularMaterial.PropertyChanged(SpecularMaterial.BrushProperty);
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000C690C File Offset: 0x000C5D0C
		private static void SpecularPowerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SpecularMaterial specularMaterial = (SpecularMaterial)d;
			specularMaterial.PropertyChanged(SpecularMaterial.SpecularPowerProperty);
		}

		/// <summary>Obtém ou define um valor que filtra as propriedades de cor do material aplicado ao modelo.</summary>
		/// <returns>A cor com a qual o material de filtro.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000C692C File Offset: 0x000C5D2C
		// (set) Token: 0x060031AE RID: 12718 RVA: 0x000C694C File Offset: 0x000C5D4C
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(SpecularMaterial.ColorProperty);
			}
			set
			{
				base.SetValueInternal(SpecularMaterial.ColorProperty, value);
			}
		}

		/// <summary>Obtém ou define o pincel 2D a ser aplicado a um modelo 3D aceso especularmente.</summary>
		/// <returns>O pincel a ser aplicado.</returns>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000C696C File Offset: 0x000C5D6C
		// (set) Token: 0x060031B0 RID: 12720 RVA: 0x000C698C File Offset: 0x000C5D8C
		public Brush Brush
		{
			get
			{
				return (Brush)base.GetValue(SpecularMaterial.BrushProperty);
			}
			set
			{
				base.SetValueInternal(SpecularMaterial.BrushProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica o grau ao qual um material aplicado a um modelo 3D reflete o modelo de iluminação como brilho.</summary>
		/// <returns>Contribuição relativa, para um material aplicado como um 2D de pincel para um 3D modelo do componente do modelo de iluminação especular.</returns>
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000C69A8 File Offset: 0x000C5DA8
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x000C69C8 File Offset: 0x000C5DC8
		public double SpecularPower
		{
			get
			{
				return (double)base.GetValue(SpecularMaterial.SpecularPowerProperty);
			}
			set
			{
				base.SetValueInternal(SpecularMaterial.SpecularPowerProperty, value);
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000C69E8 File Offset: 0x000C5DE8
		protected override Freezable CreateInstanceCore()
		{
			return new SpecularMaterial();
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000C69FC File Offset: 0x000C5DFC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Brush brush = this.Brush;
				DUCE.ResourceHandle hbrush = (brush != null) ? ((DUCE.IResource)brush).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_SPECULARMATERIAL milcmd_SPECULARMATERIAL;
				milcmd_SPECULARMATERIAL.Type = MILCMD.MilCmdSpecularMaterial;
				milcmd_SPECULARMATERIAL.Handle = this._duceResource.GetHandle(channel);
				milcmd_SPECULARMATERIAL.color = CompositionResourceManager.ColorToMilColorF(this.Color);
				milcmd_SPECULARMATERIAL.hbrush = hbrush;
				milcmd_SPECULARMATERIAL.specularPower = this.SpecularPower;
				channel.SendCommand((byte*)(&milcmd_SPECULARMATERIAL), sizeof(DUCE.MILCMD_SPECULARMATERIAL));
			}
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000C6A90 File Offset: 0x000C5E90
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_SPECULARMATERIAL))
			{
				Brush brush = this.Brush;
				if (brush != null)
				{
					((DUCE.IResource)brush).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000C6ADC File Offset: 0x000C5EDC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Brush brush = this.Brush;
				if (brush != null)
				{
					((DUCE.IResource)brush).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000C6B10 File Offset: 0x000C5F10
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000C6B2C File Offset: 0x000C5F2C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000C6B44 File Offset: 0x000C5F44
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000C6B60 File Offset: 0x000C5F60
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000C6B70 File Offset: 0x000C5F70
		static SpecularMaterial()
		{
			Type typeFromHandle = typeof(SpecularMaterial);
			SpecularMaterial.ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeFromHandle, Colors.White, new PropertyChangedCallback(SpecularMaterial.ColorPropertyChanged), null, false, null);
			SpecularMaterial.BrushProperty = Animatable.RegisterProperty("Brush", typeof(Brush), typeFromHandle, null, new PropertyChangedCallback(SpecularMaterial.BrushPropertyChanged), null, false, null);
			SpecularMaterial.SpecularPowerProperty = Animatable.RegisterProperty("SpecularPower", typeof(double), typeFromHandle, 40.0, new PropertyChangedCallback(SpecularMaterial.SpecularPowerPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.SpecularMaterial.Color" />.</summary>
		// Token: 0x040015AA RID: 5546
		public static readonly DependencyProperty ColorProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.SpecularMaterial.Brush" />.</summary>
		// Token: 0x040015AB RID: 5547
		public static readonly DependencyProperty BrushProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.SpecularMaterial.SpecularPower" />.</summary>
		// Token: 0x040015AC RID: 5548
		public static readonly DependencyProperty SpecularPowerProperty;

		// Token: 0x040015AD RID: 5549
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040015AE RID: 5550
		internal static Color s_Color = Colors.White;

		// Token: 0x040015AF RID: 5551
		internal static Brush s_Brush = null;

		// Token: 0x040015B0 RID: 5552
		internal const double c_SpecularPower = 40.0;
	}
}
