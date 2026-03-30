using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media
{
	/// <summary>Reproduz um arquivo de mídia. Se a mídia for um arquivo de vídeo, o <see cref="T:System.Windows.Media.VideoDrawing" /> o desenhará no retângulo especificado.</summary>
	// Token: 0x02000400 RID: 1024
	public sealed class VideoDrawing : Drawing
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.VideoDrawing" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x060028F5 RID: 10485 RVA: 0x000A41C8 File Offset: 0x000A35C8
		public new VideoDrawing Clone()
		{
			return (VideoDrawing)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.VideoDrawing" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x060028F6 RID: 10486 RVA: 0x000A41E0 File Offset: 0x000A35E0
		public new VideoDrawing CloneCurrentValue()
		{
			return (VideoDrawing)base.CloneCurrentValue();
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x000A41F8 File Offset: 0x000A35F8
		private static void PlayerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			VideoDrawing videoDrawing = (VideoDrawing)d;
			MediaPlayer resource = (MediaPlayer)e.OldValue;
			MediaPlayer resource2 = (MediaPlayer)e.NewValue;
			Dispatcher dispatcher = videoDrawing.Dispatcher;
			if (dispatcher != null)
			{
				DUCE.IResource resource3 = videoDrawing;
				using (CompositionEngineLock.Acquire())
				{
					int channelCount = resource3.GetChannelCount();
					for (int i = 0; i < channelCount; i++)
					{
						DUCE.Channel channel = resource3.GetChannel(i);
						videoDrawing.ReleaseResource(resource, channel);
						videoDrawing.AddRefResource(resource2, channel);
					}
				}
			}
			videoDrawing.PropertyChanged(VideoDrawing.PlayerProperty);
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x000A42A4 File Offset: 0x000A36A4
		private static void RectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			VideoDrawing videoDrawing = (VideoDrawing)d;
			videoDrawing.PropertyChanged(VideoDrawing.RectProperty);
		}

		/// <summary>Obtém ou define o player de mídia associado ao desenho.</summary>
		/// <returns>O player de mídia associado ao desenho.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x000A42C4 File Offset: 0x000A36C4
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x000A42E4 File Offset: 0x000A36E4
		public MediaPlayer Player
		{
			get
			{
				return (MediaPlayer)base.GetValue(VideoDrawing.PlayerProperty);
			}
			set
			{
				base.SetValueInternal(VideoDrawing.PlayerProperty, value);
			}
		}

		/// <summary>Obtém ou define a área retangular na qual o vídeo será desenhado.</summary>
		/// <returns>O retângulo no qual o vídeo será desenhado.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x000A4300 File Offset: 0x000A3700
		// (set) Token: 0x060028FC RID: 10492 RVA: 0x000A4320 File Offset: 0x000A3720
		public Rect Rect
		{
			get
			{
				return (Rect)base.GetValue(VideoDrawing.RectProperty);
			}
			set
			{
				base.SetValueInternal(VideoDrawing.RectProperty, value);
			}
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x000A4340 File Offset: 0x000A3740
		protected override Freezable CreateInstanceCore()
		{
			return new VideoDrawing();
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x000A4354 File Offset: 0x000A3754
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal unsafe override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, skipOnChannelCheck);
				MediaPlayer player = this.Player;
				DUCE.ResourceHandle hPlayer = (player != null) ? ((DUCE.IResource)player).GetHandle(channel) : DUCE.ResourceHandle.Null;
				DUCE.ResourceHandle animationResourceHandle = base.GetAnimationResourceHandle(VideoDrawing.RectProperty, channel);
				DUCE.MILCMD_VIDEODRAWING milcmd_VIDEODRAWING;
				milcmd_VIDEODRAWING.Type = MILCMD.MilCmdVideoDrawing;
				milcmd_VIDEODRAWING.Handle = this._duceResource.GetHandle(channel);
				milcmd_VIDEODRAWING.hPlayer = hPlayer;
				if (animationResourceHandle.IsNull)
				{
					milcmd_VIDEODRAWING.Rect = this.Rect;
				}
				milcmd_VIDEODRAWING.hRectAnimations = animationResourceHandle;
				channel.SendCommand((byte*)(&milcmd_VIDEODRAWING), sizeof(DUCE.MILCMD_VIDEODRAWING));
			}
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000A43F8 File Offset: 0x000A37F8
		internal override DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_VIDEODRAWING))
			{
				MediaPlayer player = this.Player;
				if (player != null)
				{
					((DUCE.IResource)player).AddRefOnChannel(channel);
				}
				this.AddRefOnChannelAnimations(channel);
				this.UpdateResource(channel, true);
			}
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x000A4444 File Offset: 0x000A3844
		internal override void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource.ReleaseOnChannel(channel))
			{
				MediaPlayer player = this.Player;
				if (player != null)
				{
					((DUCE.IResource)player).ReleaseOnChannel(channel);
				}
				this.ReleaseOnChannelAnimations(channel);
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x000A4478 File Offset: 0x000A3878
		internal override DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource.GetHandle(channel);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000A4494 File Offset: 0x000A3894
		internal override int GetChannelCountCore()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x000A44AC File Offset: 0x000A38AC
		internal override DUCE.Channel GetChannelCore(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x000A44C8 File Offset: 0x000A38C8
		static VideoDrawing()
		{
			Type typeFromHandle = typeof(VideoDrawing);
			VideoDrawing.PlayerProperty = Animatable.RegisterProperty("Player", typeof(MediaPlayer), typeFromHandle, null, new PropertyChangedCallback(VideoDrawing.PlayerPropertyChanged), null, false, null);
			VideoDrawing.RectProperty = Animatable.RegisterProperty("Rect", typeof(Rect), typeFromHandle, Rect.Empty, new PropertyChangedCallback(VideoDrawing.RectPropertyChanged), null, true, null);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000A455C File Offset: 0x000A395C
		internal override void WalkCurrentValue(DrawingContextWalker ctx)
		{
			ctx.DrawVideo(this.Player, this.Rect);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.VideoDrawing.Player" />.</summary>
		// Token: 0x040012AD RID: 4781
		public static readonly DependencyProperty PlayerProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.VideoDrawing.Rect" />.</summary>
		// Token: 0x040012AE RID: 4782
		public static readonly DependencyProperty RectProperty;

		// Token: 0x040012AF RID: 4783
		internal DUCE.MultiChannelResource _duceResource;

		// Token: 0x040012B0 RID: 4784
		internal static Rect s_Rect = Rect.Empty;
	}
}
