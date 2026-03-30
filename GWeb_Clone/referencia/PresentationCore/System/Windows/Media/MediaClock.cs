using System;
using System.IO.Packaging;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace System.Windows.Media
{
	/// <summary>Mantém o estado de tempo de mídia por meio de um <see cref="T:System.Windows.Media.MediaTimeline" />.</summary>
	// Token: 0x02000421 RID: 1057
	public class MediaClock : Clock
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.MediaClock" />.</summary>
		/// <param name="media">A linha de tempo a ser usada como um modelo para o relógio de mídia.</param>
		// Token: 0x06002ADA RID: 10970 RVA: 0x000AB83C File Offset: 0x000AAC3C
		protected internal MediaClock(MediaTimeline media) : base(media)
		{
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.MediaTimeline" /> que descreve o comportamento de controle do relógio.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.MediaTimeline" /> que descreve o comportamento de controle do relógio.</returns>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000AB850 File Offset: 0x000AAC50
		public new MediaTimeline Timeline
		{
			get
			{
				return (MediaTimeline)base.Timeline;
			}
		}

		/// <summary>Recupera um valor que indica se o relógio de mídia pode ser adiado.</summary>
		/// <returns>
		///   <see langword="true" /> se o relógio de mídia puder ser adiado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06002ADC RID: 10972 RVA: 0x000AB868 File Offset: 0x000AAC68
		protected override bool GetCanSlip()
		{
			return true;
		}

		/// <summary>Recupera um valor que identifica o tempo de mídia real. Esse valor pode ser usado para sincronização de adiamento.</summary>
		/// <returns>O tempo real da mídia.</returns>
		// Token: 0x06002ADD RID: 10973 RVA: 0x000AB878 File Offset: 0x000AAC78
		protected override TimeSpan GetCurrentTimeCore()
		{
			if (this._mediaPlayer != null)
			{
				return this._mediaPlayer.Position;
			}
			return base.GetCurrentTimeCore();
		}

		/// <summary>Invocado quando o relógio está parado.</summary>
		// Token: 0x06002ADE RID: 10974 RVA: 0x000AB8A0 File Offset: 0x000AACA0
		protected override void Stopped()
		{
			if (this._mediaPlayer != null)
			{
				this._mediaPlayer.SetSpeedRatio(0.0);
				this._mediaPlayer.SetPosition(TimeSpan.FromTicks(0L));
			}
		}

		/// <summary>Invocado quando a velocidade do relógio foi alterada.</summary>
		// Token: 0x06002ADF RID: 10975 RVA: 0x000AB8DC File Offset: 0x000AACDC
		protected override void SpeedChanged()
		{
			this.Sync();
		}

		/// <summary>Invocado quando a movimentação é descontinuada.</summary>
		// Token: 0x06002AE0 RID: 10976 RVA: 0x000AB8F0 File Offset: 0x000AACF0
		protected override void DiscontinuousTimeMovement()
		{
			this.Sync();
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000AB904 File Offset: 0x000AAD04
		private void Sync()
		{
			if (this._mediaPlayer != null)
			{
				double? currentGlobalSpeed = base.CurrentGlobalSpeed;
				double num = (currentGlobalSpeed != null) ? currentGlobalSpeed.Value : 0.0;
				TimeSpan? currentTime = base.CurrentTime;
				TimeSpan position = (currentTime != null) ? currentTime.Value : TimeSpan.Zero;
				if (num == 0.0)
				{
					this._mediaPlayer.SetSpeedRatio(num);
					this._mediaPlayer.SetPosition(position);
					return;
				}
				this._mediaPlayer.SetPosition(position);
				this._mediaPlayer.SetSpeedRatio(num);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000AB99C File Offset: 0x000AAD9C
		internal override bool NeedsTicksWhenActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000AB9AC File Offset: 0x000AADAC
		// (set) Token: 0x06002AE4 RID: 10980 RVA: 0x000AB9C0 File Offset: 0x000AADC0
		internal MediaPlayer Player
		{
			get
			{
				return this._mediaPlayer;
			}
			set
			{
				MediaPlayer mediaPlayer = this._mediaPlayer;
				if (value != mediaPlayer)
				{
					this._mediaPlayer = value;
					if (mediaPlayer != null)
					{
						mediaPlayer.Clock = null;
					}
					if (value != null)
					{
						value.Clock = this;
						Uri baseUri = ((IUriContext)this.Timeline).BaseUri;
						Uri source;
						if (baseUri != null && baseUri.Scheme != PackUriHelper.UriSchemePack && !this.Timeline.Source.IsAbsoluteUri)
						{
							source = new Uri(baseUri, this.Timeline.Source);
						}
						else
						{
							source = this.Timeline.Source;
						}
						value.SetSource(source);
						this.SpeedChanged();
					}
				}
			}
		}

		// Token: 0x0400139F RID: 5023
		private MediaPlayer _mediaPlayer;
	}
}
