using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Pinta uma área com um <see cref="T:System.Windows.Media.Drawing" />, que pode incluir formas, texto, vídeo, imagens ou outros desenhos.</summary>
	// Token: 0x0200037F RID: 895
	public sealed class DrawingBrush : TileBrush
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DrawingBrush" />. O pincel resultante não tem nenhum conteúdo.</summary>
		// Token: 0x0600207D RID: 8317 RVA: 0x00084598 File Offset: 0x00083998
		public DrawingBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.DrawingBrush" /> que contém o <see cref="T:System.Windows.Media.Drawing" /> especificado.</summary>
		/// <param name="drawing">O <see cref="T:System.Windows.Media.Drawing" /> que descreve o conteúdo do pincel.</param>
		// Token: 0x0600207E RID: 8318 RVA: 0x000845AC File Offset: 0x000839AC
		public DrawingBrush(Drawing drawing)
		{
			this.Drawing = drawing;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x000845C8 File Offset: 0x000839C8
		protected override void GetContentBounds(out Rect contentBounds)
		{
			contentBounds = this.Drawing.GetBounds();
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.DrawingBrush" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06002080 RID: 8320 RVA: 0x000845E8 File Offset: 0x000839E8
		public new DrawingBrush Clone()
		{
			return (DrawingBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.DrawingBrush" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06002081 RID: 8321 RVA: 0x00084600 File Offset: 0x00083A00
		public new DrawingBrush CloneCurrentValue()
		{
			return (DrawingBrush)base.CloneCurrentValue();
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00084618 File Offset: 0x00083A18
		private static void DrawingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.IsASubPropertyChange && e.OldValueSource == e.NewValueSource)
			{
				return;
			}
			DrawingBrush drawingBrush = (DrawingBrush)d;
			Drawing resource = (Drawing)e.OldValue;
			Drawing resource2 = (Drawing)e.NewValue;
			Dispatcher dispatcher = drawingBrush.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = drawingBrush;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						drawingBrush.ReleaseResource(resource, channel);
						drawingBrush.AddRefResource(resource2, channel);
					}
				}
			}
			drawingBrush.PropertyChanged(DrawingBrush.DrawingProperty);
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.Drawing" /> que descreve o conteúdo deste <see cref="T:System.Windows.Media.DrawingBrush" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.Drawing" /> que descreve o conteúdo deste <see cref="T:System.Windows.Media.DrawingBrush" />. O padrão é a referência nula (<see langword="Nothing" /> no Visual Basic).</returns>
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x000846E0 File Offset: 0x00083AE0
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x00084700 File Offset: 0x00083B00
		public Drawing Drawing
		{
			get
			{
				return (Drawing)base.GetValue(DrawingBrush.DrawingProperty);
			}
			set
			{
				base.SetValueInternal(DrawingBrush.DrawingProperty, value);
			}
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0008471C File Offset: 0x00083B1C
		protected override Freezable CreateInstanceCore()
		{
			return new DrawingBrush();
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00084730 File Offset: 0x00083B30
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				Transform transform = base.Transform;
				Transform relativeTransform = base.RelativeTransform;
				Drawing drawing = this.Drawing;
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
				DUCE.ResourceHandle hDrawing = (drawing != null) ? ((DUCE.IResource)drawing).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(Brush.OpacityProperty, channel);
				DUCE.ResourceHandle animationResourceHandle2 = base.GetAnimationResourceHandle(TileBrush.ViewportProperty, channel);
				DUCE.ResourceHandle animationResourceHandle3 = base.GetAnimationResourceHandle(TileBrush.ViewboxProperty, channel);
				DUCE.MILCMD_DRAWINGBRUSH milcmd_DRAWINGBRUSH;
				milcmd_DRAWINGBRUSH.Type = MILCMD.MilCmdDrawingBrush;
				milcmd_DRAWINGBRUSH.Handle = this._duceResource.GetHandle(channel);
				if (animationResourceHandle.IsNull)
				{
					milcmd_DRAWINGBRUSH.Opacity = base.Opacity;
				}
				milcmd_DRAWINGBRUSH.hOpacityAnimations = animationResourceHandle;
				milcmd_DRAWINGBRUSH.hTransform = hTransform;
				milcmd_DRAWINGBRUSH.hRelativeTransform = hRelativeTransform;
				milcmd_DRAWINGBRUSH.ViewportUnits = base.ViewportUnits;
				milcmd_DRAWINGBRUSH.ViewboxUnits = base.ViewboxUnits;
				if (animationResourceHandle2.IsNull)
				{
					milcmd_DRAWINGBRUSH.Viewport = base.Viewport;
				}
				milcmd_DRAWINGBRUSH.hViewportAnimations = animationResourceHandle2;
				if (animationResourceHandle3.IsNull)
				{
					milcmd_DRAWINGBRUSH.Viewbox = base.Viewbox;
				}
				milcmd_DRAWINGBRUSH.hViewboxAnimations = animationResourceHandle3;
				milcmd_DRAWINGBRUSH.Stretch = base.Stretch;
				milcmd_DRAWINGBRUSH.TileMode = base.TileMode;
				milcmd_DRAWINGBRUSH.AlignmentX = base.AlignmentX;
				milcmd_DRAWINGBRUSH.AlignmentY = base.AlignmentY;
				milcmd_DRAWINGBRUSH.CachingHint = (CachingHint)base.GetValue(RenderOptions.CachingHintProperty);
				milcmd_DRAWINGBRUSH.CacheInvalidationThresholdMinimum = (double)base.GetValue(RenderOptions.CacheInvalidationThresholdMinimumProperty);
				milcmd_DRAWINGBRUSH.CacheInvalidationThresholdMaximum = (double)base.GetValue(RenderOptions.CacheInvalidationThresholdMaximumProperty);
				milcmd_DRAWINGBRUSH.hDrawing = hDrawing;
				channel.SendCommand((byte*)(&milcmd_DRAWINGBRUSH), sizeof(DUCE.MILCMD_DRAWINGBRUSH));
			}
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0008491C File Offset: 0x00083D1C
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_DRAWINGBRUSH))
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

		// Token: 0x06002088 RID: 8328 RVA: 0x0008498C File Offset: 0x00083D8C
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
				Drawing drawing = this.Drawing;
				if (drawing != null)
				{
					((DUCE.IResource)drawing).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000849E4 File Offset: 0x00083DE4
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00084A00 File Offset: 0x00083E00
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00084A18 File Offset: 0x00083E18
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x00084A34 File Offset: 0x00083E34
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00084A44 File Offset: 0x00083E44
		static DrawingBrush()
		{
			Type typeFromHandle = typeof(DrawingBrush);
			DrawingBrush.DrawingProperty = Animatable.RegisterProperty("Drawing", typeof(Drawing), typeFromHandle, null, new PropertyChangedCallback(DrawingBrush.DrawingPropertyChanged), null, false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.DrawingBrush.Drawing" />.</summary>
		// Token: 0x0400109B RID: 4251
		public static readonly DependencyProperty DrawingProperty;

		// Token: 0x0400109C RID: 4252
		internal DUCE.MultiChannelResource _duceResource;
	}
}
