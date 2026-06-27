using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media.Media3D
{
	/// <summary>Aplica um <see cref="T:System.Windows.Media.Brush" /> a um modelo 3D para que ele participe dos cálculos de iluminação como se o <see cref="T:System.Windows.Media.Media3D.Material" /> estivesse emitindo luz igual à cor do <see cref="T:System.Windows.Media.Brush" />.</summary>
	// Token: 0x02000458 RID: 1112
	public sealed class EmissiveMaterial : Material
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.EmissiveMaterial" />.</summary>
		// Token: 0x06002E38 RID: 11832 RVA: 0x000B8974 File Offset: 0x000B7D74
		public EmissiveMaterial()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Media3D.EmissiveMaterial" /> com o pincel especificado.</summary>
		/// <param name="brush">O pincel do novo material.</param>
		// Token: 0x06002E39 RID: 11833 RVA: 0x000B8988 File Offset: 0x000B7D88
		public EmissiveMaterial(Brush brush)
		{
			this.Brush = brush;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.EmissiveMaterial" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002E3A RID: 11834 RVA: 0x000B89A4 File Offset: 0x000B7DA4
		public new EmissiveMaterial Clone()
		{
			return (EmissiveMaterial)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.EmissiveMaterial" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002E3B RID: 11835 RVA: 0x000B89BC File Offset: 0x000B7DBC
		public new EmissiveMaterial CloneCurrentValue()
		{
			return (EmissiveMaterial)base.CloneCurrentValue();
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000B89D4 File Offset: 0x000B7DD4
		private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			EmissiveMaterial emissiveMaterial = (EmissiveMaterial)d;
			emissiveMaterial.PropertyChanged(EmissiveMaterial.ColorProperty);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000B89F4 File Offset: 0x000B7DF4
		private static void BrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			EmissiveMaterial emissiveMaterial = (EmissiveMaterial)d;
			Brush resource = (Brush)e.OldValue;
			Brush resource2 = (Brush)e.NewValue;
			Dispatcher dispatcher = emissiveMaterial.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = emissiveMaterial;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						emissiveMaterial.ReleaseResource(resource, channel);
						emissiveMaterial.AddRefResource(resource2, channel);
					}
				}
			}
			emissiveMaterial.PropertyChanged(EmissiveMaterial.BrushProperty);
		}

		/// <summary>Obtém ou define o filtro de cores para a textura do material.</summary>
		/// <returns>O filtro de cor para o <see cref="T:System.Windows.Media.Media3D.Material" />.</returns>
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000B8ABC File Offset: 0x000B7EBC
		// (set) Token: 0x06002E3F RID: 11839 RVA: 0x000B8ADC File Offset: 0x000B7EDC
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(EmissiveMaterial.ColorProperty);
			}
			set
			{
				base.SetValueInternal(EmissiveMaterial.ColorProperty, value);
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Brush" /> aplicado pelo <see cref="T:System.Windows.Media.Media3D.EmissiveMaterial" />.</summary>
		/// <returns>O pincel aplicado pelo <see cref="T:System.Windows.Media.Media3D.EmissiveMaterial" />. O valor padrão é <see langword="null" />.</returns>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000B8AFC File Offset: 0x000B7EFC
		// (set) Token: 0x06002E41 RID: 11841 RVA: 0x000B8B1C File Offset: 0x000B7F1C
		public Brush Brush
		{
			get
			{
				return (Brush)base.GetValue(EmissiveMaterial.BrushProperty);
			}
			set
			{
				base.SetValueInternal(EmissiveMaterial.BrushProperty, value);
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000B8B38 File Offset: 0x000B7F38
		protected override Freezable CreateInstanceCore()
		{
			return new EmissiveMaterial();
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000B8B4C File Offset: 0x000B7F4C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Brush brush = this.Brush;
				DUCE.ResourceHandle hbrush = (brush != null) ? ((DUCE.IResource)brush).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_EMISSIVEMATERIAL milcmd_EMISSIVEMATERIAL;
				milcmd_EMISSIVEMATERIAL.Type = MILCMD.MilCmdEmissiveMaterial;
				milcmd_EMISSIVEMATERIAL.Handle = this._duceResource.GetHandle(channel);
				milcmd_EMISSIVEMATERIAL.color = CompositionResourceManager.ColorToMilColorF(this.Color);
				milcmd_EMISSIVEMATERIAL.hbrush = hbrush;
				channel.SendCommand((byte*)(&milcmd_EMISSIVEMATERIAL), sizeof(DUCE.MILCMD_EMISSIVEMATERIAL));
			}
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000B8BD0 File Offset: 0x000B7FD0
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_EMISSIVEMATERIAL))
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

		// Token: 0x06002E45 RID: 11845 RVA: 0x000B8C1C File Offset: 0x000B801C
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

		// Token: 0x06002E46 RID: 11846 RVA: 0x000B8C50 File Offset: 0x000B8050
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000B8C6C File Offset: 0x000B806C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000B8C84 File Offset: 0x000B8084
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000B8CA0 File Offset: 0x000B80A0
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000B8CB0 File Offset: 0x000B80B0
		static EmissiveMaterial()
		{
			Type typeFromHandle = typeof(EmissiveMaterial);
			EmissiveMaterial.ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeFromHandle, Colors.White, new PropertyChangedCallback(EmissiveMaterial.ColorPropertyChanged), null, false, null);
			EmissiveMaterial.BrushProperty = Animatable.RegisterProperty("Brush", typeof(Brush), typeFromHandle, null, new PropertyChangedCallback(EmissiveMaterial.BrushPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.EmissiveMaterial.Color" />.</summary>
		// Token: 0x040014F0 RID: 5360
		public static readonly DependencyProperty ColorProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.EmissiveMaterial.Brush" />.</summary>
		// Token: 0x040014F1 RID: 5361
		public static readonly DependencyProperty BrushProperty;

		// Token: 0x040014F2 RID: 5362
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040014F3 RID: 5363
		internal static Color s_Color = Colors.White;

		// Token: 0x040014F4 RID: 5364
		internal static Brush s_Brush = null;
	}
}
