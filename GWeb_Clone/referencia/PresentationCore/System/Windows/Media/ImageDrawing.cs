using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Desenha uma imagem dentro de uma região definida por um <see cref="T:System.Windows.Rect" />.</summary>
	// Token: 0x020003B7 RID: 951
	public sealed class ImageDrawing : Drawing
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.ImageDrawing" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600246A RID: 9322 RVA: 0x00092844 File Offset: 0x00091C44
		public new ImageDrawing Clone()
		{
			return (ImageDrawing)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.ImageDrawing" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600246B RID: 9323 RVA: 0x0009285C File Offset: 0x00091C5C
		public new ImageDrawing CloneCurrentValue()
		{
			return (ImageDrawing)base.CloneCurrentValue();
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00092874 File Offset: 0x00091C74
		private static void ImageSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			ImageDrawing imageDrawing = (ImageDrawing)d;
			ImageSource resource = (ImageSource)e.OldValue;
			ImageSource resource2 = (ImageSource)e.NewValue;
			Dispatcher dispatcher = imageDrawing.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = imageDrawing;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						imageDrawing.ReleaseResource(resource, channel);
						imageDrawing.AddRefResource(resource2, channel);
					}
				}
			}
			imageDrawing.PropertyChanged(ImageDrawing.ImageSourceProperty);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0009293C File Offset: 0x00091D3C
		private static void RectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ImageDrawing imageDrawing = (ImageDrawing)d;
			imageDrawing.PropertyChanged(ImageDrawing.RectProperty);
		}

		/// <summary>Obtém ou define a origem da imagem</summary>
		/// <returns>A origem da imagem. O valor padrão é nulo.</returns>
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x0009295C File Offset: 0x00091D5C
		// (set) Token: 0x0600246F RID: 9327 RVA: 0x0009297C File Offset: 0x00091D7C
		public ImageSource ImageSource
		{
			get
			{
				return (ImageSource)base.GetValue(ImageDrawing.ImageSourceProperty);
			}
			set
			{
				base.SetValueInternal(ImageDrawing.ImageSourceProperty, value);
			}
		}

		/// <summary>Obtém ou define a região em que a imagem é desenhada.</summary>
		/// <returns>A região na qual a imagem é desenhada. O padrão é vazio.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x00092998 File Offset: 0x00091D98
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x000929B8 File Offset: 0x00091DB8
		public Rect Rect
		{
			get
			{
				return (Rect)base.GetValue(ImageDrawing.RectProperty);
			}
			set
			{
				base.SetValueInternal(ImageDrawing.RectProperty, value);
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000929D8 File Offset: 0x00091DD8
		protected override Freezable CreateInstanceCore()
		{
			return new ImageDrawing();
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000929EC File Offset: 0x00091DEC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				ImageSource imageSource = this.ImageSource;
				DUCE.ResourceHandle hImageSource = (imageSource != null) ? ((DUCE.IResource)imageSource).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(ImageDrawing.RectProperty, channel);
				DUCE.MILCMD_IMAGEDRAWING milcmd_IMAGEDRAWING;
				milcmd_IMAGEDRAWING.Type = MILCMD.MilCmdImageDrawing;
				milcmd_IMAGEDRAWING.Handle = this._duceResource.GetHandle(channel);
				milcmd_IMAGEDRAWING.hImageSource = hImageSource;
				if (animationResourceHandle.IsNull)
				{
					milcmd_IMAGEDRAWING.Rect = this.Rect;
				}
				milcmd_IMAGEDRAWING.hRectAnimations = animationResourceHandle;
				channel.SendCommand((byte*)(&milcmd_IMAGEDRAWING), sizeof(DUCE.MILCMD_IMAGEDRAWING));
			}
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x00092A90 File Offset: 0x00091E90
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_IMAGEDRAWING))
			{
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

		// Token: 0x06002475 RID: 9333 RVA: 0x00092ADC File Offset: 0x00091EDC
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				ImageSource imageSource = this.ImageSource;
				if (imageSource != null)
				{
					((DUCE.IResource)imageSource).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00092B10 File Offset: 0x00091F10
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00092B2C File Offset: 0x00091F2C
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00092B44 File Offset: 0x00091F44
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00092B60 File Offset: 0x00091F60
		static ImageDrawing()
		{
			Type typeFromHandle = typeof(ImageDrawing);
			ImageDrawing.ImageSourceProperty = Animatable.RegisterProperty("ImageSource", typeof(ImageSource), typeFromHandle, null, new PropertyChangedCallback(ImageDrawing.ImageSourcePropertyChanged), null, false, null);
			ImageDrawing.RectProperty = Animatable.RegisterProperty("Rect", typeof(Rect), typeFromHandle, Rect.Empty, new PropertyChangedCallback(ImageDrawing.RectPropertyChanged), null, true, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ImageDrawing" />.</summary>
		// Token: 0x0600247A RID: 9338 RVA: 0x00092BE0 File Offset: 0x00091FE0
		public ImageDrawing()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.ImageDrawing" /> que tem o <see cref="P:System.Windows.Media.ImageDrawing.ImageSource" /> e o <see cref="P:System.Windows.Media.ImageDrawing.Rect" /> de destino especificados.</summary>
		/// <param name="imageSource">Origem da imagem que é desenhada.</param>
		/// <param name="rect">Define a área retangular na qual a imagem é desenhada.</param>
		// Token: 0x0600247B RID: 9339 RVA: 0x00092BF4 File Offset: 0x00091FF4
		public ImageDrawing(ImageSource imageSource, Rect rect)
		{
			this.ImageSource = imageSource;
			this.Rect = rect;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00092C18 File Offset: 0x00092018
		internal override void WalkCurrentValue(DrawingContextWalker ctx)
		{
			ctx.DrawImage(this.ImageSource, this.Rect);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ImageDrawing.ImageSource" />.</summary>
		// Token: 0x04001171 RID: 4465
		public static readonly DependencyProperty ImageSourceProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.ImageDrawing.Rect" />.</summary>
		// Token: 0x04001172 RID: 4466
		public static readonly DependencyProperty RectProperty;

		// Token: 0x04001173 RID: 4467
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001174 RID: 4468
		internal static Rect s_Rect = Rect.Empty;
	}
}
