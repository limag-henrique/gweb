using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media.Media3D
{
	/// <summary>Permite a aplicação de um pincel 2D, como um <see cref="T:System.Windows.Media.SolidColorBrush" /> ou <see cref="T:System.Windows.Media.TileBrush" />, em um modelo 3D de luz difusa.</summary>
	// Token: 0x02000456 RID: 1110
	public sealed class DiffuseMaterial : Material
	{
		/// <summary>Constrói um DiffuseMaterial.</summary>
		// Token: 0x06002E13 RID: 11795 RVA: 0x000B81E4 File Offset: 0x000B75E4
		public DiffuseMaterial()
		{
		}

		/// <summary>Constrói um DiffuseMaterial com a propriedade Brush especificada.</summary>
		/// <param name="brush">O pincel do novo material.</param>
		// Token: 0x06002E14 RID: 11796 RVA: 0x000B81F8 File Offset: 0x000B75F8
		public DiffuseMaterial(Brush brush)
		{
			this.Brush = brush;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Media3D.DiffuseMaterial" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002E15 RID: 11797 RVA: 0x000B8214 File Offset: 0x000B7614
		public new DiffuseMaterial Clone()
		{
			return (DiffuseMaterial)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Media3D.DiffuseMaterial" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002E16 RID: 11798 RVA: 0x000B822C File Offset: 0x000B762C
		public new DiffuseMaterial CloneCurrentValue()
		{
			return (DiffuseMaterial)base.CloneCurrentValue();
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000B8244 File Offset: 0x000B7644
		private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DiffuseMaterial diffuseMaterial = (DiffuseMaterial)d;
			diffuseMaterial.PropertyChanged(DiffuseMaterial.ColorProperty);
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000B8264 File Offset: 0x000B7664
		private static void AmbientColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DiffuseMaterial diffuseMaterial = (DiffuseMaterial)d;
			diffuseMaterial.PropertyChanged(DiffuseMaterial.AmbientColorProperty);
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000B8284 File Offset: 0x000B7684
		private static void BrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DiffuseMaterial diffuseMaterial = (DiffuseMaterial)d;
			Brush resource = (Brush)e.OldValue;
			Brush resource2 = (Brush)e.NewValue;
			Dispatcher dispatcher = diffuseMaterial.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = diffuseMaterial;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						diffuseMaterial.ReleaseResource(resource, channel);
						diffuseMaterial.AddRefResource(resource2, channel);
					}
				}
			}
			diffuseMaterial.PropertyChanged(DiffuseMaterial.BrushProperty);
		}

		/// <summary>Obtém ou define o filtro de cores para a textura do material.</summary>
		/// <returns>O filtro de cor para o <see cref="T:System.Windows.Media.Media3D.Material" />. O valor padrão é #FFFFFF. Uma vez que todas as cores compõem em branco, todas as cores são visíveis por padrão.</returns>
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000B834C File Offset: 0x000B774C
		// (set) Token: 0x06002E1B RID: 11803 RVA: 0x000B836C File Offset: 0x000B776C
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(DiffuseMaterial.ColorProperty);
			}
			set
			{
				base.SetValueInternal(DiffuseMaterial.ColorProperty, value);
			}
		}

		/// <summary>Obtém ou define uma cor que representa como o material reflete <see cref="T:System.Windows.Media.Media3D.AmbientLight" />.</summary>
		/// <returns>A cor da luz ambiente refletida por objeto 3D. O valor padrão é #FFFFFF.</returns>
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000B838C File Offset: 0x000B778C
		// (set) Token: 0x06002E1D RID: 11805 RVA: 0x000B83AC File Offset: 0x000B77AC
		public Color AmbientColor
		{
			get
			{
				return (Color)base.GetValue(DiffuseMaterial.AmbientColorProperty);
			}
			set
			{
				base.SetValueInternal(DiffuseMaterial.AmbientColorProperty, value);
			}
		}

		/// <summary>
		///   <see cref="T:System.Windows.Media.Brush" /> a ser aplicado como um <see cref="T:System.Windows.Media.Media3D.Material" /> a um modelo 3D.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Media.Brush" /> a ser aplicado.</returns>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000B83CC File Offset: 0x000B77CC
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x000B83EC File Offset: 0x000B77EC
		public Brush Brush
		{
			get
			{
				return (Brush)base.GetValue(DiffuseMaterial.BrushProperty);
			}
			set
			{
				base.SetValueInternal(DiffuseMaterial.BrushProperty, value);
			}
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000B8408 File Offset: 0x000B7808
		protected override Freezable CreateInstanceCore()
		{
			return new DiffuseMaterial();
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000B841C File Offset: 0x000B781C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Brush brush = this.Brush;
				DUCE.ResourceHandle hbrush = (brush != null) ? ((DUCE.IResource)brush).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_DIFFUSEMATERIAL milcmd_DIFFUSEMATERIAL;
				milcmd_DIFFUSEMATERIAL.Type = MILCMD.MilCmdDiffuseMaterial;
				milcmd_DIFFUSEMATERIAL.Handle = this._duceResource.GetHandle(channel);
				milcmd_DIFFUSEMATERIAL.color = CompositionResourceManager.ColorToMilColorF(this.Color);
				milcmd_DIFFUSEMATERIAL.ambientColor = CompositionResourceManager.ColorToMilColorF(this.AmbientColor);
				milcmd_DIFFUSEMATERIAL.hbrush = hbrush;
				channel.SendCommand((byte*)(&milcmd_DIFFUSEMATERIAL), sizeof(DUCE.MILCMD_DIFFUSEMATERIAL));
			}
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000B84B8 File Offset: 0x000B78B8
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DIFFUSEMATERIAL))
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

		// Token: 0x06002E23 RID: 11811 RVA: 0x000B8504 File Offset: 0x000B7904
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

		// Token: 0x06002E24 RID: 11812 RVA: 0x000B8538 File Offset: 0x000B7938
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000B8554 File Offset: 0x000B7954
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000B856C File Offset: 0x000B796C
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000B8588 File Offset: 0x000B7988
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000B8598 File Offset: 0x000B7998
		static DiffuseMaterial()
		{
			Type typeFromHandle = typeof(DiffuseMaterial);
			DiffuseMaterial.ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeFromHandle, Colors.White, new PropertyChangedCallback(DiffuseMaterial.ColorPropertyChanged), null, false, null);
			DiffuseMaterial.AmbientColorProperty = Animatable.RegisterProperty("AmbientColor", typeof(Color), typeFromHandle, Colors.White, new PropertyChangedCallback(DiffuseMaterial.AmbientColorPropertyChanged), null, false, null);
			DiffuseMaterial.BrushProperty = Animatable.RegisterProperty("Brush", typeof(Brush), typeFromHandle, null, new PropertyChangedCallback(DiffuseMaterial.BrushPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.DiffuseMaterial.Color" />.</summary>
		// Token: 0x040014E6 RID: 5350
		public static readonly DependencyProperty ColorProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.DiffuseMaterial.AmbientColor" />.</summary>
		// Token: 0x040014E7 RID: 5351
		public static readonly DependencyProperty AmbientColorProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Media3D.DiffuseMaterial.Brush" />.</summary>
		// Token: 0x040014E8 RID: 5352
		public static readonly DependencyProperty BrushProperty;

		// Token: 0x040014E9 RID: 5353
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040014EA RID: 5354
		internal static Color s_Color = Colors.White;

		// Token: 0x040014EB RID: 5355
		internal static Color s_AmbientColor = Colors.White;

		// Token: 0x040014EC RID: 5356
		internal static Brush s_Brush = null;
	}
}
