using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal.KnownBoxes;

namespace System.Windows.Media
{
	/// <summary>Cria e armazena em cache uma representação de um <see cref="T:System.Windows.UIElement" />.</summary>
	// Token: 0x02000365 RID: 869
	public sealed class BitmapCache : CacheMode
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.BitmapCache" />.</summary>
		// Token: 0x06001DA7 RID: 7591 RVA: 0x00079524 File Offset: 0x00078924
		public BitmapCache()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.BitmapCache" /> com a escala especificada.</summary>
		/// <param name="renderAtScale">Um duplo que dimensiona o bitmap.</param>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x00079538 File Offset: 0x00078938
		public BitmapCache(double renderAtScale)
		{
			this.RenderAtScale = renderAtScale;
		}

		/// <summary>Cria um clone modificável do <see cref="T:System.Windows.Media.BitmapCache" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência do objeto, esse método copia as expressões (que talvez não possam mais ser resolvidas), mas não as animações nem seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06001DA9 RID: 7593 RVA: 0x00079554 File Offset: 0x00078954
		public new BitmapCache Clone()
		{
			return (BitmapCache)base.Clone();
		}

		/// <summary>Cria um clone modificável (cópia profunda) do <see cref="T:System.Windows.Media.BitmapCache" /> usando seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06001DAA RID: 7594 RVA: 0x0007956C File Offset: 0x0007896C
		public new BitmapCache CloneCurrentValue()
		{
			return (BitmapCache)base.CloneCurrentValue();
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00079584 File Offset: 0x00078984
		private static void RenderAtScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapCache bitmapCache = (BitmapCache)d;
			bitmapCache.PropertyChanged(BitmapCache.RenderAtScaleProperty);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000795A4 File Offset: 0x000789A4
		private static void SnapsToDevicePixelsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapCache bitmapCache = (BitmapCache)d;
			bitmapCache.PropertyChanged(BitmapCache.SnapsToDevicePixelsProperty);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000795C4 File Offset: 0x000789C4
		private static void EnableClearTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BitmapCache bitmapCache = (BitmapCache)d;
			bitmapCache.PropertyChanged(BitmapCache.EnableClearTypeProperty);
		}

		/// <summary>Obtém ou define um valor que indica a escala aplicada ao bitmap.</summary>
		/// <returns>A escala é aplicada ao bitmap. O padrão é 1.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x000795E4 File Offset: 0x000789E4
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x00079604 File Offset: 0x00078A04
		public double RenderAtScale
		{
			get
			{
				return (double)base.GetValue(BitmapCache.RenderAtScaleProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCache.RenderAtScaleProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se o bitmap é renderizado com o ajuste de pixels.</summary>
		/// <returns>
		///   <see langword="true" /> Se o ajuste de pixels estiver ativo; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00079624 File Offset: 0x00078A24
		// (set) Token: 0x06001DB1 RID: 7601 RVA: 0x00079644 File Offset: 0x00078A44
		public bool SnapsToDevicePixels
		{
			get
			{
				return (bool)base.GetValue(BitmapCache.SnapsToDevicePixelsProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCache.SnapsToDevicePixelsProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Obtém ou define um valor que indica se o bitmap é renderizado com o ClearType ativado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o ClearType estiver ativo; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00079664 File Offset: 0x00078A64
		// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x00079684 File Offset: 0x00078A84
		public bool EnableClearType
		{
			get
			{
				return (bool)base.GetValue(BitmapCache.EnableClearTypeProperty);
			}
			set
			{
				base.SetValueInternal(BitmapCache.EnableClearTypeProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000796A4 File Offset: 0x00078AA4
		protected override Freezable CreateInstanceCore()
		{
			return new BitmapCache();
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000796B8 File Offset: 0x00078AB8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(BitmapCache.RenderAtScaleProperty, channel);
				DUCE.MILCMD_BITMAPCACHE milcmd_BITMAPCACHE;
				milcmd_BITMAPCACHE.Type = MILCMD.MilCmdBitmapCache;
				milcmd_BITMAPCACHE.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_BITMAPCACHE.RenderAtScale = this.RenderAtScale;
				}
				milcmd_BITMAPCACHE.hRenderAtScaleAnimations = animationResourceHandle;
				milcmd_BITMAPCACHE.SnapsToDevicePixels = CompositionResourceManager.BooleanToUInt32(this.SnapsToDevicePixels);
				milcmd_BITMAPCACHE.EnableClearType = CompositionResourceManager.BooleanToUInt32(this.EnableClearType);
				channel.SendCommand((byte*)(&milcmd_BITMAPCACHE), sizeof(DUCE.MILCMD_BITMAPCACHE));
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00079760 File Offset: 0x00078B60
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_BITMAPCACHE))
			{
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0007979C File Offset: 0x00078B9C
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000797C0 File Offset: 0x00078BC0
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000797DC File Offset: 0x00078BDC
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x000797F4 File Offset: 0x00078BF4
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00079810 File Offset: 0x00078C10
		static BitmapCache()
		{
			Type typeFromHandle = typeof(BitmapCache);
			BitmapCache.RenderAtScaleProperty = Animatable.RegisterProperty("RenderAtScale", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(BitmapCache.RenderAtScalePropertyChanged), null, true, null);
			BitmapCache.SnapsToDevicePixelsProperty = Animatable.RegisterProperty("SnapsToDevicePixels", typeof(bool), typeFromHandle, false, new PropertyChangedCallback(BitmapCache.SnapsToDevicePixelsPropertyChanged), null, false, null);
			BitmapCache.EnableClearTypeProperty = Animatable.RegisterProperty("EnableClearType", typeof(bool), typeFromHandle, false, new PropertyChangedCallback(BitmapCache.EnableClearTypePropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BitmapCache.RenderAtScale" />.</summary>
		// Token: 0x04000FEB RID: 4075
		public static readonly DependencyProperty RenderAtScaleProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BitmapCache.SnapsToDevicePixels" />.</summary>
		// Token: 0x04000FEC RID: 4076
		public static readonly DependencyProperty SnapsToDevicePixelsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.BitmapCache.EnableClearType" />.</summary>
		// Token: 0x04000FED RID: 4077
		public static readonly DependencyProperty EnableClearTypeProperty;

		// Token: 0x04000FEE RID: 4078
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04000FEF RID: 4079
		internal const double c_RenderAtScale = 1.0;

		// Token: 0x04000FF0 RID: 4080
		internal const bool c_SnapsToDevicePixels = false;

		// Token: 0x04000FF1 RID: 4081
		internal const bool c_EnableClearType = false;
	}
}
