using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Pinta uma área com uma imagem.</summary>
	// Token: 0x020003B6 RID: 950
	public sealed class ImageBrush : TileBrush
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.ImageBrush" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06002459 RID: 9305 RVA: 0x00092334 File Offset: 0x00091734
		public new ImageBrush Clone()
		{
			return (ImageBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.ImageBrush" />, fazendo cópias em profundidade dos valores do objeto atual.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x0600245A RID: 9306 RVA: 0x0009234C File Offset: 0x0009174C
		public new ImageBrush CloneCurrentValue()
		{
			return (ImageBrush)base.CloneCurrentValue();
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00092364 File Offset: 0x00091764
		private static void ImageSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			ImageBrush imageBrush = (ImageBrush)d;
			ImageSource resource = (ImageSource)e.OldValue;
			ImageSource resource2 = (ImageSource)e.NewValue;
			Dispatcher dispatcher = imageBrush.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = imageBrush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						imageBrush.ReleaseResource(resource, channel);
						imageBrush.AddRefResource(resource2, channel);
					}
				}
			}
			imageBrush.PropertyChanged(ImageBrush.ImageSourceProperty);
		}

		/// <summary>Obtém ou define a imagem exibida por este <see cref="T:System.Windows.Media.ImageBrush" />.</summary>
		/// <returns>A imagem exibida por este <see cref="T:System.Windows.Media.ImageBrush" />.</returns>
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x0009242C File Offset: 0x0009182C
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x0009244C File Offset: 0x0009184C
		public ImageSource ImageSource
		{
			get
			{
				return (ImageSource)base.GetValue(ImageBrush.ImageSourceProperty);
			}
			set
			{
				base.SetValueInternal(ImageBrush.ImageSourceProperty, value);
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00092468 File Offset: 0x00091868
		protected override Freezable CreateInstanceCore()
		{
			return new ImageBrush();
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0009247C File Offset: 0x0009187C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				Transform relativeTransform = base.RelativeTransform;
				ImageSource imageSource = this.ImageSource;
				DUCE.ResourceHandle hTransform;
				if (transform == null || transform == Transform.Identity)
				{
					hTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hTransform = ((DUCE.IResource)transform).GetHandle(channel);
				}
				DUCE.ResourceHandle hRelativeTransform;
				if (relativeTransform == null || relativeTransform == Transform.Identity)
				{
					hRelativeTransform = DUCE.ResourceHandle.Null;
				}
				else
				{
					hRelativeTransform = ((DUCE.IResource)relativeTransform).GetHandle(channel);
				}
				DUCE.ResourceHandle hImageSource = (imageSource != null) ? ((DUCE.IResource)imageSource).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Brush.OpacityProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(TileBrush.ViewportProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(TileBrush.ViewboxProperty, channel);
				DUCE.MILCMD_IMAGEBRUSH milcmd_IMAGEBRUSH;
				milcmd_IMAGEBRUSH.Type = MILCMD.MilCmdImageBrush;
				milcmd_IMAGEBRUSH.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_IMAGEBRUSH.Opacity = base.Opacity;
				}
				milcmd_IMAGEBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_IMAGEBRUSH.hTransform = hTransform;
				milcmd_IMAGEBRUSH.hRelativeTransform = hRelativeTransform;
				milcmd_IMAGEBRUSH.ViewportUnits = base.ViewportUnits;
				milcmd_IMAGEBRUSH.ViewboxUnits = base.ViewboxUnits;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_IMAGEBRUSH.Viewport = base.Viewport;
				}
				milcmd_IMAGEBRUSH.hViewportAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_IMAGEBRUSH.Viewbox = base.Viewbox;
				}
				milcmd_IMAGEBRUSH.hViewboxAnimations = animationResourceHandle3;
				milcmd_IMAGEBRUSH.Stretch = base.Stretch;
				milcmd_IMAGEBRUSH.TileMode = base.TileMode;
				milcmd_IMAGEBRUSH.AlignmentX = base.AlignmentX;
				milcmd_IMAGEBRUSH.AlignmentY = base.AlignmentY;
				milcmd_IMAGEBRUSH.CachingHint = (CachingHint)base.GetValue(RenderOptions.CachingHintProperty);
				milcmd_IMAGEBRUSH.CacheInvalidationThresholdMinimum = (double)base.GetValue(RenderOptions.CacheInvalidationThresholdMinimumProperty);
				milcmd_IMAGEBRUSH.CacheInvalidationThresholdMaximum = (double)base.GetValue(RenderOptions.CacheInvalidationThresholdMaximumProperty);
				milcmd_IMAGEBRUSH.hImageSource = hImageSource;
				channel.SendCommand((byte*)(&milcmd_IMAGEBRUSH), sizeof(DUCE.MILCMD_IMAGEBRUSH));
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00092668 File Offset: 0x00091A68
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_IMAGEBRUSH))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).AddRefOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).AddRefOnChannel(channel);
				}
				ImageSource imageSource = this.ImageSource;
				if (imageSource != null)
				{
					((DUCE.IResource)imageSource).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000926D8 File Offset: 0x00091AD8
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Transform transform = base.Transform;
				if (transform != null)
				{
					((DUCE.IResource)transform).ReleaseOnChannel(channel);
				}
				Transform relativeTransform = base.RelativeTransform;
				if (relativeTransform != null)
				{
					((DUCE.IResource)relativeTransform).ReleaseOnChannel(channel);
				}
				ImageSource imageSource = this.ImageSource;
				if (imageSource != null)
				{
					((DUCE.IResource)imageSource).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00092730 File Offset: 0x00091B30
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0009274C File Offset: 0x00091B4C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00092764 File Offset: 0x00091B64
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x00092780 File Offset: 0x00091B80
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00092790 File Offset: 0x00091B90
		static ImageBrush()
		{
			Type typeFromHandle = typeof(ImageBrush);
			ImageBrush.ImageSourceProperty = Animatable.RegisterProperty("ImageSource", typeof(ImageSource), typeFromHandle, null, new PropertyChangedCallback(ImageBrush.ImageSourcePropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ImageBrush" /> sem conteúdo.</summary>
		// Token: 0x06002467 RID: 9319 RVA: 0x000927D4 File Offset: 0x00091BD4
		public ImageBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ImageBrush" /> que pinta uma área com a imagem especificada.</summary>
		/// <param name="image">A imagem a ser exibida.</param>
		// Token: 0x06002468 RID: 9320 RVA: 0x000927E8 File Offset: 0x00091BE8
		public ImageBrush(ImageSource image)
		{
			this.ImageSource = image;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00092804 File Offset: 0x00091C04
		protected override void GetContentBounds(out Rect contentBounds)
		{
			contentBounds = Rect.Empty;
			DrawingImage drawingImage = this.ImageSource as DrawingImage;
			if (drawingImage != null)
			{
				Drawing drawing = drawingImage.Drawing;
				if (drawing != null)
				{
					contentBounds = drawing.Bounds;
				}
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ImageBrush.ImageSource" />.</summary>
		// Token: 0x0400116F RID: 4463
		public static readonly DependencyProperty ImageSourceProperty;

		// Token: 0x04001170 RID: 4464
		internal DUCE.MultiChannelResource _duceResource;
	}
}
