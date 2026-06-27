using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Um <see cref="T:System.Windows.Media.ImageSource" /> que usa um <see cref="T:System.Windows.Media.Drawing" /> para conteúdo.</summary>
	// Token: 0x02000387 RID: 903
	public sealed class DrawingImage : ImageSource
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DrawingImage" />.</summary>
		// Token: 0x0600216F RID: 8559 RVA: 0x00087530 File Offset: 0x00086930
		public DrawingImage()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DrawingImage" /> que tem o <see cref="P:System.Windows.Media.DrawingImage.Drawing" /> especificado.</summary>
		/// <param name="drawing">O <see cref="P:System.Windows.Media.DrawingImage.Drawing" /> da nova instância <see cref="T:System.Windows.Media.DrawingImage" />.</param>
		// Token: 0x06002170 RID: 8560 RVA: 0x00087544 File Offset: 0x00086944
		public DrawingImage(Drawing drawing)
		{
			this.Drawing = drawing;
		}

		/// <summary>Obtém a largura da <see cref="T:System.Windows.Media.DrawingImage" />.</summary>
		/// <returns>A largura do <see cref="T:System.Windows.Media.DrawingImage" />.</returns>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002171 RID: 8561 RVA: 0x00087560 File Offset: 0x00086960
		public override double Width
		{
			get
			{
				base.ReadPreamble();
				return this.Size.Width;
			}
		}

		/// <summary>Obtém a altura da <see cref="T:System.Windows.Media.DrawingImage" />.</summary>
		/// <returns>A altura do <see cref="T:System.Windows.Media.DrawingImage" />.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x00087584 File Offset: 0x00086984
		public override double Height
		{
			get
			{
				base.ReadPreamble();
				return this.Size.Height;
			}
		}

		/// <summary>Obtém os metadados do <see cref="T:System.Windows.Media.DrawingImage" />.</summary>
		/// <returns>Os metadados do <see cref="T:System.Windows.Media.DrawingImage" />.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x000875A8 File Offset: 0x000869A8
		public override ImageMetadata Metadata
		{
			get
			{
				base.ReadPreamble();
				return null;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x000875BC File Offset: 0x000869BC
		internal override Size Size
		{
			get
			{
				Drawing drawing = this.Drawing;
				if (drawing == null)
				{
					return default(Size);
				}
				Size size = drawing.GetBounds().Size;
				if (!size.IsEmpty)
				{
					return size;
				}
				return default(Size);
			}
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.DrawingImage" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002175 RID: 8565 RVA: 0x00087600 File Offset: 0x00086A00
		public new DrawingImage Clone()
		{
			return (DrawingImage)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.DrawingImage" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002176 RID: 8566 RVA: 0x00087618 File Offset: 0x00086A18
		public new DrawingImage CloneCurrentValue()
		{
			return (DrawingImage)base.CloneCurrentValue();
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x00087630 File Offset: 0x00086A30
		private static void DrawingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DrawingImage drawingImage = (DrawingImage)d;
			Drawing resource = (Drawing)e.OldValue;
			Drawing resource2 = (Drawing)e.NewValue;
			Dispatcher dispatcher = drawingImage.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = drawingImage;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						drawingImage.ReleaseResource(resource, channel);
						drawingImage.AddRefResource(resource2, channel);
					}
				}
			}
			drawingImage.PropertyChanged(DrawingImage.DrawingProperty);
		}

		/// <summary>Obtém ou define o conteúdo de desenho para o <see cref="T:System.Windows.Media.DrawingImage" />.</summary>
		/// <returns>O conteúdo de desenho do <see cref="T:System.Windows.Media.DrawingImage" />. O valor padrão é nulo.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000876F8 File Offset: 0x00086AF8
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x00087718 File Offset: 0x00086B18
		public Drawing Drawing
		{
			get
			{
				return (Drawing)base.GetValue(DrawingImage.DrawingProperty);
			}
			set
			{
				base.SetValueInternal(DrawingImage.DrawingProperty, value);
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x00087734 File Offset: 0x00086B34
		protected override Freezable CreateInstanceCore()
		{
			return new DrawingImage();
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00087748 File Offset: 0x00086B48
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Drawing drawing = this.Drawing;
				DUCE.ResourceHandle hDrawing = (drawing != null) ? ((DUCE.IResource)drawing).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.MILCMD_DRAWINGIMAGE milcmd_DRAWINGIMAGE;
				milcmd_DRAWINGIMAGE.Type = MILCMD.MilCmdDrawingImage;
				milcmd_DRAWINGIMAGE.Handle = this._duceResource.GetHandle(channel);
				milcmd_DRAWINGIMAGE.hDrawing = hDrawing;
				channel.SendCommand((byte*)(&milcmd_DRAWINGIMAGE), sizeof(DUCE.MILCMD_DRAWINGIMAGE));
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000877BC File Offset: 0x00086BBC
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DRAWINGIMAGE))
			{
				Drawing drawing = this.Drawing;
				if (drawing != null)
				{
					((DUCE.IResource)drawing).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x00087808 File Offset: 0x00086C08
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				Drawing drawing = this.Drawing;
				if (drawing != null)
				{
					((DUCE.IResource)drawing).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0008783C File Offset: 0x00086C3C
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x00087858 File Offset: 0x00086C58
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x00087870 File Offset: 0x00086C70
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0008788C File Offset: 0x00086C8C
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0008789C File Offset: 0x00086C9C
		static DrawingImage()
		{
			Type typeFromHandle = typeof(DrawingImage);
			DrawingImage.DrawingProperty = Animatable.RegisterProperty("Drawing", typeof(Drawing), typeFromHandle, null, new PropertyChangedCallback(DrawingImage.DrawingPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingImage.Drawing" />.</summary>
		// Token: 0x040010BA RID: 4282
		public static readonly DependencyProperty DrawingProperty;

		// Token: 0x040010BB RID: 4283
		internal DUCE.MultiChannelResource _duceResource;
	}
}
