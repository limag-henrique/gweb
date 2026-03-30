using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Internal.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Fornece reprodução de mídia para desenhos.</summary>
	// Token: 0x02000426 RID: 1062
	public class MediaPlayer : Animatable, DUCE.IResource
	{
		/// <summary>Obtém um valor que indica se a mídia está armazenando em buffer.</summary>
		/// <returns>True se a mídia de armazenamento em buffer; Caso contrário, false.</returns>
		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000AC7BC File Offset: 0x000ABBBC
		public bool IsBuffering
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.IsBuffering;
			}
		}

		/// <summary>Obtém um valor que indica se a mídia pode ser colocada em pausa.</summary>
		/// <returns>True se a mídia pode ser pausada; Caso contrário, false.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000AC7DC File Offset: 0x000ABBDC
		public bool CanPause
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.CanPause;
			}
		}

		/// <summary>Obtém o percentual de andamento do download para o conteúdo localizado em um servidor remoto.</summary>
		/// <returns>A porcentagem de andamento do download de conteúdo localizado em um servidor remoto representado por um valor entre 0 e 1. O padrão é 0.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000AC7FC File Offset: 0x000ABBFC
		public double DownloadProgress
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.DownloadProgress;
			}
		}

		/// <summary>Obtém o percentual de buffer concluído para o conteúdo de streaming.</summary>
		/// <returns>A porcentagem de buffer concluído para streaming de conteúdo representado em um valor entre 0 e 1.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000AC81C File Offset: 0x000ABC1C
		public double BufferingProgress
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.BufferingProgress;
			}
		}

		/// <summary>Obtém a altura de pixel do vídeo.</summary>
		/// <returns>A altura de pixel do vídeo.</returns>
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000AC83C File Offset: 0x000ABC3C
		public int NaturalVideoHeight
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.NaturalVideoHeight;
			}
		}

		/// <summary>Obtém a largura de pixel do vídeo.</summary>
		/// <returns>A largura de pixel do vídeo.</returns>
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x000AC85C File Offset: 0x000ABC5C
		public int NaturalVideoWidth
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.NaturalVideoWidth;
			}
		}

		/// <summary>Obtém um valor que indica se a mídia tem saída de áudio.</summary>
		/// <returns>True se a mídia tem saída de áudio; Caso contrário, false.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000AC87C File Offset: 0x000ABC7C
		public bool HasAudio
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.HasAudio;
			}
		}

		/// <summary>Obtém um valor que indica se a mídia tem saída de vídeo.</summary>
		/// <returns>true se a mídia tiver saída de vídeo; caso contrário, false.</returns>
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x000AC89C File Offset: 0x000ABC9C
		public bool HasVideo
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.HasVideo;
			}
		}

		/// <summary>Obtém a mídia <see cref="T:System.Uri" />.</summary>
		/// <returns>A mídia <see cref="T:System.Uri" /> atual.</returns>
		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000AC8BC File Offset: 0x000ABCBC
		public Uri Source
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.Source;
			}
		}

		/// <summary>Obtém ou define o volume de mídia.</summary>
		/// <returns>O volume de mídia representado em uma escala linear entre 0 e 1. O padrão é 0,5.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000AC8DC File Offset: 0x000ABCDC
		// (set) Token: 0x06002B2C RID: 11052 RVA: 0x000AC8FC File Offset: 0x000ABCFC
		public double Volume
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.Volume;
			}
			set
			{
				this.WritePreamble();
				this._mediaPlayerState.Volume = value;
			}
		}

		/// <summary>Obtém ou define o equilíbrio entre os volumes dos alto-falantes da esquerda e da direita.</summary>
		/// <returns>A taxa do volume entre os alto-falantes esquerdo e direito em um intervalo entre -1 e 1. O padrão é 0.</returns>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000AC91C File Offset: 0x000ABD1C
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x000AC93C File Offset: 0x000ABD3C
		public double Balance
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.Balance;
			}
			set
			{
				this.WritePreamble();
				this._mediaPlayerState.Balance = value;
			}
		}

		/// <summary>Obtém ou define um valor que indica se a remoção está habilitada.</summary>
		/// <returns>
		///   <see langword="true" /> se a anulação estiver habilitada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000AC95C File Offset: 0x000ABD5C
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x000AC97C File Offset: 0x000ABD7C
		public bool ScrubbingEnabled
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.ScrubbingEnabled;
			}
			set
			{
				this.WritePreamble();
				this._mediaPlayerState.ScrubbingEnabled = value;
			}
		}

		/// <summary>Obtém um valor que indica se a mídia é silenciada.</summary>
		/// <returns>True se a mídia é silenciada; Caso contrário, false.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000AC99C File Offset: 0x000ABD9C
		// (set) Token: 0x06002B32 RID: 11058 RVA: 0x000AC9BC File Offset: 0x000ABDBC
		public bool IsMuted
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.IsMuted;
			}
			set
			{
				this.WritePreamble();
				this._mediaPlayerState.IsMuted = value;
			}
		}

		/// <summary>Obtém a duração normal da mídia.</summary>
		/// <returns>A duração normal da mídia. O padrão é <see cref="P:System.Windows.Duration.Automatic" />.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000AC9DC File Offset: 0x000ABDDC
		public Duration NaturalDuration
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.NaturalDuration;
			}
		}

		/// <summary>Obtém ou define a posição atual da mídia.</summary>
		/// <returns>A posição atual da mídia.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000ACA1C File Offset: 0x000ABE1C
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000AC9FC File Offset: 0x000ABDFC
		public TimeSpan Position
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.Position;
			}
			set
			{
				this.WritePreamble();
				this._mediaPlayerState.Position = value;
			}
		}

		/// <summary>Obtém ou define a taxa de velocidade em que mídia é reproduzida.</summary>
		/// <returns>A taxa de velocidade que a mídia é reproduzida back representado por um valor entre 0 e o maior valor duplo. O padrão é 1.0.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000ACA3C File Offset: 0x000ABE3C
		// (set) Token: 0x06002B37 RID: 11063 RVA: 0x000ACA5C File Offset: 0x000ABE5C
		public double SpeedRatio
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.SpeedRatio;
			}
			set
			{
				this.WritePreamble();
				this._mediaPlayerState.SpeedRatio = value;
			}
		}

		/// <summary>Ocorre quando um erro é encontrado</summary>
		// Token: 0x140001AA RID: 426
		// (add) Token: 0x06002B38 RID: 11064 RVA: 0x000ACA7C File Offset: 0x000ABE7C
		// (remove) Token: 0x06002B39 RID: 11065 RVA: 0x000ACA9C File Offset: 0x000ABE9C
		public event EventHandler<ExceptionEventArgs> MediaFailed
		{
			add
			{
				this.WritePreamble();
				this._mediaPlayerState.MediaFailed += value;
			}
			remove
			{
				this.WritePreamble();
				this._mediaPlayerState.MediaFailed -= value;
			}
		}

		/// <summary>Ocorre quando a mídia é aberta.</summary>
		// Token: 0x140001AB RID: 427
		// (add) Token: 0x06002B3A RID: 11066 RVA: 0x000ACABC File Offset: 0x000ABEBC
		// (remove) Token: 0x06002B3B RID: 11067 RVA: 0x000ACADC File Offset: 0x000ABEDC
		public event EventHandler MediaOpened
		{
			add
			{
				this.WritePreamble();
				this._mediaPlayerState.MediaOpened += value;
			}
			remove
			{
				this.WritePreamble();
				this._mediaPlayerState.MediaOpened -= value;
			}
		}

		/// <summary>Ocorre quando a mídia conclui a reprodução.</summary>
		// Token: 0x140001AC RID: 428
		// (add) Token: 0x06002B3C RID: 11068 RVA: 0x000ACAFC File Offset: 0x000ABEFC
		// (remove) Token: 0x06002B3D RID: 11069 RVA: 0x000ACB1C File Offset: 0x000ABF1C
		public event EventHandler MediaEnded
		{
			add
			{
				this.WritePreamble();
				this._mediaPlayerState.MediaEnded += value;
			}
			remove
			{
				this.WritePreamble();
				this._mediaPlayerState.MediaEnded -= value;
			}
		}

		/// <summary>Ocorre quando o buffer foi iniciado.</summary>
		// Token: 0x140001AD RID: 429
		// (add) Token: 0x06002B3E RID: 11070 RVA: 0x000ACB3C File Offset: 0x000ABF3C
		// (remove) Token: 0x06002B3F RID: 11071 RVA: 0x000ACB5C File Offset: 0x000ABF5C
		public event EventHandler BufferingStarted
		{
			add
			{
				this.WritePreamble();
				this._mediaPlayerState.BufferingStarted += value;
			}
			remove
			{
				this.WritePreamble();
				this._mediaPlayerState.BufferingStarted -= value;
			}
		}

		/// <summary>Ocorre quando o buffer foi concluído.</summary>
		// Token: 0x140001AE RID: 430
		// (add) Token: 0x06002B40 RID: 11072 RVA: 0x000ACB7C File Offset: 0x000ABF7C
		// (remove) Token: 0x06002B41 RID: 11073 RVA: 0x000ACB9C File Offset: 0x000ABF9C
		public event EventHandler BufferingEnded
		{
			add
			{
				this.WritePreamble();
				this._mediaPlayerState.BufferingEnded += value;
			}
			remove
			{
				this.WritePreamble();
				this._mediaPlayerState.BufferingEnded -= value;
			}
		}

		/// <summary>Ocorre quando um comando de script foi encontrado na mídia.</summary>
		// Token: 0x140001AF RID: 431
		// (add) Token: 0x06002B42 RID: 11074 RVA: 0x000ACBBC File Offset: 0x000ABFBC
		// (remove) Token: 0x06002B43 RID: 11075 RVA: 0x000ACBDC File Offset: 0x000ABFDC
		public event EventHandler<MediaScriptCommandEventArgs> ScriptCommand
		{
			add
			{
				this.WritePreamble();
				this._mediaPlayerState.ScriptCommand += value;
			}
			remove
			{
				this.WritePreamble();
				this._mediaPlayerState.ScriptCommand -= value;
			}
		}

		/// <summary>Obtém ou define o <see cref="T:System.Windows.Media.MediaClock" /> associado ao <see cref="T:System.Windows.Media.MediaTimeline" /> a ser reproduzido.</summary>
		/// <returns>O relógio associado ao <see cref="T:System.Windows.Media.MediaTimeline" /> a ser reproduzido. O padrão é nulo.</returns>
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000ACBFC File Offset: 0x000ABFFC
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x000ACC1C File Offset: 0x000AC01C
		public MediaClock Clock
		{
			get
			{
				this.ReadPreamble();
				return this._mediaPlayerState.Clock;
			}
			set
			{
				this.WritePreamble();
				this._mediaPlayerState.SetClock(value, this);
			}
		}

		/// <summary>Abre o <see cref="T:System.Uri" /> determinado para reprodução de mídia.</summary>
		/// <param name="source">A origem de mídia <see cref="T:System.Uri" />.</param>
		// Token: 0x06002B46 RID: 11078 RVA: 0x000ACC3C File Offset: 0x000AC03C
		public void Open(Uri source)
		{
			this.WritePreamble();
			this._mediaPlayerState.Open(source);
		}

		/// <summary>Reproduz a mídia do <see cref="P:System.Windows.Media.MediaPlayer.Position" /> atual.</summary>
		// Token: 0x06002B47 RID: 11079 RVA: 0x000ACC5C File Offset: 0x000AC05C
		public void Play()
		{
			this.WritePreamble();
			this._mediaPlayerState.Play();
		}

		/// <summary>Pausa a reprodução de mídia.</summary>
		// Token: 0x06002B48 RID: 11080 RVA: 0x000ACC7C File Offset: 0x000AC07C
		public void Pause()
		{
			this.WritePreamble();
			this._mediaPlayerState.Pause();
		}

		/// <summary>Interrompe a reprodução de mídia.</summary>
		// Token: 0x06002B49 RID: 11081 RVA: 0x000ACC9C File Offset: 0x000AC09C
		public void Stop()
		{
			this.WritePreamble();
			this._mediaPlayerState.Stop();
		}

		/// <summary>Fecha a mídia subjacente.</summary>
		// Token: 0x06002B4A RID: 11082 RVA: 0x000ACCBC File Offset: 0x000AC0BC
		public void Close()
		{
			this.WritePreamble();
			this._mediaPlayerState.Close();
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000ACCDC File Offset: 0x000AC0DC
		[SecurityCritical]
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			this.EnsureState();
			DUCE.ResourceHandle result;
			using (CompositionEngineLock.Acquire())
			{
				result = this.AddRefOnChannelCore(channel);
			}
			return result;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000ACD2C File Offset: 0x000AC12C
		[SecurityCritical]
		internal DUCE.ResourceHandle AddRefOnChannelCore(DUCE.Channel channel)
		{
			if (this._duceResource._duceResource.CreateOrAddRefOnChannel(this, channel, DUCE.ResourceType.TYPE_MEDIAPLAYER))
			{
				this._needsUpdate = true;
				this.UpdateResource(channel, true);
			}
			return this._duceResource._duceResource.GetHandle(channel);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000ACD70 File Offset: 0x000AC170
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			this.EnsureState();
			using (CompositionEngineLock.Acquire())
			{
				this.ReleaseOnChannelCore(channel);
			}
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000ACDBC File Offset: 0x000AC1BC
		internal void ReleaseOnChannelCore(DUCE.Channel channel)
		{
			this._duceResource._duceResource.ReleaseOnChannel(channel);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000ACDDC File Offset: 0x000AC1DC
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			this.EnsureState();
			DUCE.ResourceHandle handleCore;
			using (CompositionEngineLock.Acquire())
			{
				handleCore = this.GetHandleCore(channel);
			}
			return handleCore;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000ACE2C File Offset: 0x000AC22C
		internal DUCE.ResourceHandle GetHandleCore(DUCE.Channel channel)
		{
			return this._duceResource._duceResource.GetHandle(channel);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000ACE4C File Offset: 0x000AC24C
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource._duceResource.GetChannelCount();
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000ACE6C File Offset: 0x000AC26C
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource._duceResource.GetChannel(index);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000ACE8C File Offset: 0x000AC28C
		internal override void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource._duceResource.IsOnChannel(channel))
			{
				base.UpdateResource(channel, true);
				if (this._needsUpdate)
				{
					this.UpdateResourceInternal(channel);
				}
			}
		}

		/// <summary>Cria uma nova instância <see cref="T:System.Windows.Media.MediaPlayer" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.MediaPlayer" />.</returns>
		// Token: 0x06002B54 RID: 11092 RVA: 0x000ACEC8 File Offset: 0x000AC2C8
		protected override Freezable CreateInstanceCore()
		{
			return new MediaPlayer();
		}

		/// <summary>Torna essa instância uma cópia profunda do <see cref="T:System.Windows.Media.MediaPlayer" /> especificado. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.MediaPlayer" /> a ser clonado.</param>
		// Token: 0x06002B55 RID: 11093 RVA: 0x000ACEDC File Offset: 0x000AC2DC
		protected override void CloneCore(Freezable sourceFreezable)
		{
			base.CloneCore(sourceFreezable);
			this.CloneCommon(sourceFreezable);
		}

		/// <summary>Torna essa instância uma cópia profunda modificável do <see cref="T:System.Windows.Media.MediaPlayer" /> especificado usando os valores de propriedade atuais. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <param name="sourceFreezable">O <see cref="T:System.Windows.Media.MediaPlayer" /> a ser clonado.</param>
		// Token: 0x06002B56 RID: 11094 RVA: 0x000ACEF8 File Offset: 0x000AC2F8
		protected override void CloneCurrentValueCore(Freezable sourceFreezable)
		{
			base.CloneCurrentValueCore(sourceFreezable);
			this.CloneCommon(sourceFreezable);
		}

		/// <summary>Torna essa instância um clone do objeto <see cref="T:System.Windows.Media.MediaPlayer" /> especificado.</summary>
		/// <param name="sourceFreezable">O objeto <see cref="T:System.Windows.Media.MediaPlayer" /> a ser clonado e congelado.</param>
		// Token: 0x06002B57 RID: 11095 RVA: 0x000ACF14 File Offset: 0x000AC314
		protected override void GetAsFrozenCore(Freezable sourceFreezable)
		{
			base.GetAsFrozenCore(sourceFreezable);
			this.CloneCommon(sourceFreezable);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000ACF30 File Offset: 0x000AC330
		private void CloneCommon(Freezable sourceFreezable)
		{
			MediaPlayer mediaPlayer = (MediaPlayer)sourceFreezable;
			this._mediaPlayerState = mediaPlayer._mediaPlayerState;
			this._duceResource = mediaPlayer._duceResource;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000ACF5C File Offset: 0x000AC35C
		private void EnsureState()
		{
			if (this._mediaPlayerState == null)
			{
				this._mediaPlayerState = new MediaPlayerState(this);
			}
		}

		/// <summary>Garante que o MediaPlayer esteja sendo acessado de um thread válido.</summary>
		// Token: 0x06002B5A RID: 11098 RVA: 0x000ACF80 File Offset: 0x000AC380
		protected new void ReadPreamble()
		{
			base.ReadPreamble();
			this.EnsureState();
		}

		/// <summary>Verifica se o MediaPlayer não está congelado e está sendo acessado de um contexto de threading válido.</summary>
		// Token: 0x06002B5B RID: 11099 RVA: 0x000ACF9C File Offset: 0x000AC39C
		protected new void WritePreamble()
		{
			base.WritePreamble();
			this.EnsureState();
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000ACFB8 File Offset: 0x000AC3B8
		private void OnNewFrame(object sender, EventArgs args)
		{
			this._needsUpdate = true;
			base.RegisterForAsyncUpdateResource();
			base.FireChanged();
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000ACFD8 File Offset: 0x000AC3D8
		private void UpdateResourceInternal(DUCE.Channel channel)
		{
			bool flag = false;
			ChannelMarshalType marshalType = channel.MarshalType;
			if (marshalType != ChannelMarshalType.ChannelMarshalTypeSameThread)
			{
				if (marshalType != ChannelMarshalType.ChannelMarshalTypeCrossThread)
				{
					throw new NotSupportedException(SR.Get("Media_UnknownChannelType"));
				}
				flag = true;
			}
			if (!flag)
			{
				if (this._newFrameHandler == null)
				{
					this._newFrameHandler = new EventHandler(this.OnNewFrame);
					this._mediaPlayerState.NewFrame += this._newFrameHandler;
				}
			}
			else if (this._newFrameHandler != null)
			{
				this._mediaPlayerState.NewFrame -= this._newFrameHandler;
				this._newFrameHandler = null;
			}
			this._mediaPlayerState.SendCommandMedia(channel, this._duceResource._duceResource.GetHandle(channel), flag);
			this._needsUpdate = false;
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000AD080 File Offset: 0x000AC480
		internal void SetSpeedRatio(double value)
		{
			this._mediaPlayerState.SetSpeedRatio(value);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000AD09C File Offset: 0x000AC49C
		internal void SetSource(Uri source)
		{
			this._mediaPlayerState.SetSource(source);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000AD0B8 File Offset: 0x000AC4B8
		internal void SetPosition(TimeSpan value)
		{
			this._mediaPlayerState.SetPosition(value);
		}

		// Token: 0x040013C3 RID: 5059
		private MediaPlayerState _mediaPlayerState;

		// Token: 0x040013C4 RID: 5060
		internal DUCE.ShareableDUCEMultiChannelResource _duceResource = new DUCE.ShareableDUCEMultiChannelResource();

		// Token: 0x040013C5 RID: 5061
		private EventHandler _newFrameHandler;

		// Token: 0x040013C6 RID: 5062
		private bool _needsUpdate;
	}
}
