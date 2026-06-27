using System;
using System.IO.Packaging;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Windows.Media.Composition;
using System.Windows.Navigation;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x02000427 RID: 1063
	internal class MediaPlayerState
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x000AD0D4 File Offset: 0x000AC4D4
		internal MediaPlayerState(MediaPlayer mediaPlayer)
		{
			this._dispatcher = mediaPlayer.Dispatcher;
			this.Init();
			this.CreateMedia(mediaPlayer);
			this._mediaEventsHelper.NewFrame += this.OnNewFrame;
			this._mediaEventsHelper.MediaPrerolled += this.OnMediaOpened;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000AD144 File Offset: 0x000AC544
		private void Init()
		{
			this._volume = 0.5;
			this._balance = 0.0;
			this._speedRatio = 1.0;
			this._paused = false;
			this._muted = false;
			this._sourceUri = null;
			this._scrubbingEnabled = false;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000AD19C File Offset: 0x000AC59C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		~MediaPlayerState()
		{
			if (this._helper != null)
			{
				AppDomain.CurrentDomain.ProcessExit -= this._helper.ProcessExitHandler;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000AD1F4 File Offset: 0x000AC5F4
		internal bool IsBuffering
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				bool result = false;
				HRESULT.Check(MILMedia.IsBuffering(this._nativeMedia, ref result));
				return result;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000AD21C File Offset: 0x000AC61C
		internal bool CanPause
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				bool result = false;
				HRESULT.Check(MILMedia.CanPause(this._nativeMedia, ref result));
				return result;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000AD244 File Offset: 0x000AC644
		internal double DownloadProgress
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				double result = 0.0;
				HRESULT.Check(MILMedia.GetDownloadProgress(this._nativeMedia, ref result));
				return result;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000AD274 File Offset: 0x000AC674
		internal double BufferingProgress
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				double result = 0.0;
				HRESULT.Check(MILMedia.GetBufferingProgress(this._nativeMedia, ref result));
				return result;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000AD2A4 File Offset: 0x000AC6A4
		internal int NaturalVideoHeight
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				uint result = 0U;
				HRESULT.Check(MILMedia.GetNaturalHeight(this._nativeMedia, ref result));
				return (int)result;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000AD2CC File Offset: 0x000AC6CC
		internal int NaturalVideoWidth
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				uint result = 0U;
				HRESULT.Check(MILMedia.GetNaturalWidth(this._nativeMedia, ref result));
				return (int)result;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000AD2F4 File Offset: 0x000AC6F4
		internal bool HasAudio
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.VerifyAPI();
				bool result = true;
				HRESULT.Check(MILMedia.HasAudio(this._nativeMedia, ref result));
				return result;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000AD31C File Offset: 0x000AC71C
		internal bool HasVideo
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				bool result = false;
				HRESULT.Check(MILMedia.HasVideo(this._nativeMedia, ref result));
				return result;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000AD344 File Offset: 0x000AC744
		internal Uri Source
		{
			get
			{
				this.VerifyAPI();
				return this._sourceUri;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000AD360 File Offset: 0x000AC760
		// (set) Token: 0x06002B6E RID: 11118 RVA: 0x000AD37C File Offset: 0x000AC77C
		internal double Volume
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				return this._volume;
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				this.VerifyAPI();
				if (double.IsNaN(value))
				{
					throw new ArgumentException(SR.Get("ParameterValueCannotBeNaN"), "value");
				}
				if (DoubleUtil.GreaterThanOrClose(value, 1.0))
				{
					value = 1.0;
				}
				else if (DoubleUtil.LessThanOrClose(value, 0.0))
				{
					value = 0.0;
				}
				if (!DoubleUtil.AreClose(this._volume, value))
				{
					if (!this._muted)
					{
						int hr = MILMedia.SetVolume(this._nativeMedia, value);
						HRESULT.Check(hr);
						this._volume = value;
						return;
					}
					this._volume = value;
				}
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000AD420 File Offset: 0x000AC820
		// (set) Token: 0x06002B70 RID: 11120 RVA: 0x000AD43C File Offset: 0x000AC83C
		internal double Balance
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				this.VerifyAPI();
				return this._balance;
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				this.VerifyAPI();
				if (double.IsNaN(value))
				{
					throw new ArgumentException(SR.Get("ParameterValueCannotBeNaN"), "value");
				}
				if (DoubleUtil.GreaterThanOrClose(value, 1.0))
				{
					value = 1.0;
				}
				else if (DoubleUtil.LessThanOrClose(value, -1.0))
				{
					value = -1.0;
				}
				if (!DoubleUtil.AreClose(this._balance, value))
				{
					int hr = MILMedia.SetBalance(this._nativeMedia, value);
					HRESULT.Check(hr);
					this._balance = value;
				}
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x000AD4D0 File Offset: 0x000AC8D0
		// (set) Token: 0x06002B72 RID: 11122 RVA: 0x000AD4EC File Offset: 0x000AC8EC
		internal bool ScrubbingEnabled
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				return this._scrubbingEnabled;
			}
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				this.VerifyAPI();
				if (value != this._scrubbingEnabled)
				{
					HRESULT.Check(MILMedia.SetIsScrubbingEnabled(this._nativeMedia, value));
					this._scrubbingEnabled = value;
				}
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x000AD520 File Offset: 0x000AC920
		// (set) Token: 0x06002B74 RID: 11124 RVA: 0x000AD53C File Offset: 0x000AC93C
		internal bool IsMuted
		{
			get
			{
				this.VerifyAPI();
				return this._muted;
			}
			set
			{
				this.VerifyAPI();
				double volume = this._volume;
				if (value && !this._muted)
				{
					this.Volume = 0.0;
					this._muted = true;
					this._volume = volume;
					return;
				}
				if (!value && this._muted)
				{
					this._muted = false;
					this._volume = 0.0;
					this.Volume = volume;
				}
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x000AD5A8 File Offset: 0x000AC9A8
		internal Duration NaturalDuration
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				this.VerifyAPI();
				long num = 0L;
				HRESULT.Check(MILMedia.GetMediaLength(this._nativeMedia, ref num));
				if (num == 0L)
				{
					return Duration.Automatic;
				}
				return new Duration(TimeSpan.FromTicks(num));
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000AD604 File Offset: 0x000ACA04
		// (set) Token: 0x06002B76 RID: 11126 RVA: 0x000AD5E4 File Offset: 0x000AC9E4
		internal TimeSpan Position
		{
			get
			{
				this.VerifyAPI();
				return this.GetPosition();
			}
			set
			{
				this.VerifyAPI();
				this.VerifyNotControlledByClock();
				this.SetPosition(value);
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002B78 RID: 11128 RVA: 0x000AD620 File Offset: 0x000ACA20
		// (set) Token: 0x06002B79 RID: 11129 RVA: 0x000AD63C File Offset: 0x000ACA3C
		internal double SpeedRatio
		{
			get
			{
				this.VerifyAPI();
				return this._speedRatio;
			}
			set
			{
				this.VerifyAPI();
				this.VerifyNotControlledByClock();
				if (value < 0.0)
				{
					value = 0.0;
				}
				this.SetSpeedRatio(value);
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x000AD674 File Offset: 0x000ACA74
		internal Dispatcher Dispatcher
		{
			get
			{
				return this._dispatcher;
			}
		}

		// Token: 0x140001B0 RID: 432
		// (add) Token: 0x06002B7B RID: 11131 RVA: 0x000AD688 File Offset: 0x000ACA88
		// (remove) Token: 0x06002B7C RID: 11132 RVA: 0x000AD6A8 File Offset: 0x000ACAA8
		internal event EventHandler<ExceptionEventArgs> MediaFailed
		{
			add
			{
				this.VerifyAPI();
				this._mediaEventsHelper.MediaFailed += value;
			}
			remove
			{
				this.VerifyAPI();
				this._mediaEventsHelper.MediaFailed -= value;
			}
		}

		// Token: 0x140001B1 RID: 433
		// (add) Token: 0x06002B7D RID: 11133 RVA: 0x000AD6C8 File Offset: 0x000ACAC8
		// (remove) Token: 0x06002B7E RID: 11134 RVA: 0x000AD6E8 File Offset: 0x000ACAE8
		internal event EventHandler MediaOpened
		{
			add
			{
				this.VerifyAPI();
				this._mediaOpenedHelper.AddEvent(value);
			}
			remove
			{
				this.VerifyAPI();
				this._mediaOpenedHelper.RemoveEvent(value);
			}
		}

		// Token: 0x140001B2 RID: 434
		// (add) Token: 0x06002B7F RID: 11135 RVA: 0x000AD708 File Offset: 0x000ACB08
		// (remove) Token: 0x06002B80 RID: 11136 RVA: 0x000AD728 File Offset: 0x000ACB28
		internal event EventHandler MediaEnded
		{
			add
			{
				this.VerifyAPI();
				this._mediaEventsHelper.MediaEnded += value;
			}
			remove
			{
				this.VerifyAPI();
				this._mediaEventsHelper.MediaEnded -= value;
			}
		}

		// Token: 0x140001B3 RID: 435
		// (add) Token: 0x06002B81 RID: 11137 RVA: 0x000AD748 File Offset: 0x000ACB48
		// (remove) Token: 0x06002B82 RID: 11138 RVA: 0x000AD768 File Offset: 0x000ACB68
		internal event EventHandler BufferingStarted
		{
			add
			{
				this.VerifyAPI();
				this._mediaEventsHelper.BufferingStarted += value;
			}
			remove
			{
				this.VerifyAPI();
				this._mediaEventsHelper.BufferingStarted -= value;
			}
		}

		// Token: 0x140001B4 RID: 436
		// (add) Token: 0x06002B83 RID: 11139 RVA: 0x000AD788 File Offset: 0x000ACB88
		// (remove) Token: 0x06002B84 RID: 11140 RVA: 0x000AD7A8 File Offset: 0x000ACBA8
		internal event EventHandler BufferingEnded
		{
			add
			{
				this.VerifyAPI();
				this._mediaEventsHelper.BufferingEnded += value;
			}
			remove
			{
				this.VerifyAPI();
				this._mediaEventsHelper.BufferingEnded -= value;
			}
		}

		// Token: 0x140001B5 RID: 437
		// (add) Token: 0x06002B85 RID: 11141 RVA: 0x000AD7C8 File Offset: 0x000ACBC8
		// (remove) Token: 0x06002B86 RID: 11142 RVA: 0x000AD7E8 File Offset: 0x000ACBE8
		internal event EventHandler<MediaScriptCommandEventArgs> ScriptCommand
		{
			add
			{
				this.VerifyAPI();
				this._mediaEventsHelper.ScriptCommand += value;
			}
			remove
			{
				this.VerifyAPI();
				this._mediaEventsHelper.ScriptCommand -= value;
			}
		}

		// Token: 0x140001B6 RID: 438
		// (add) Token: 0x06002B87 RID: 11143 RVA: 0x000AD808 File Offset: 0x000ACC08
		// (remove) Token: 0x06002B88 RID: 11144 RVA: 0x000AD828 File Offset: 0x000ACC28
		internal event EventHandler NewFrame
		{
			add
			{
				this.VerifyAPI();
				this._newFrameHelper.AddEvent(value);
			}
			remove
			{
				this.VerifyAPI();
				this._newFrameHelper.RemoveEvent(value);
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000AD848 File Offset: 0x000ACC48
		internal MediaClock Clock
		{
			get
			{
				this.VerifyAPI();
				return this._mediaClock;
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000AD864 File Offset: 0x000ACC64
		internal void SetClock(MediaClock clock, MediaPlayer player)
		{
			this.VerifyAPI();
			MediaClock mediaClock = this._mediaClock;
			if (mediaClock != clock)
			{
				this._mediaClock = clock;
				if (mediaClock != null)
				{
					mediaClock.Player = null;
				}
				if (clock != null)
				{
					clock.Player = player;
				}
				if (clock == null)
				{
					this.Open(null);
				}
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000AD8AC File Offset: 0x000ACCAC
		internal void Open(Uri source)
		{
			this.VerifyAPI();
			this.VerifyNotControlledByClock();
			this.SetSource(source);
			this.SetPosition(TimeSpan.Zero);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000AD8D8 File Offset: 0x000ACCD8
		internal void Play()
		{
			this.VerifyAPI();
			this.VerifyNotControlledByClock();
			this._paused = false;
			this.PrivateSpeedRatio = this.SpeedRatio;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000AD904 File Offset: 0x000ACD04
		internal void Pause()
		{
			this.VerifyAPI();
			this.VerifyNotControlledByClock();
			this._paused = true;
			this.PrivateSpeedRatio = 0.0;
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000AD934 File Offset: 0x000ACD34
		internal void Stop()
		{
			this.VerifyAPI();
			this.VerifyNotControlledByClock();
			this.Pause();
			this.Position = TimeSpan.FromTicks(0L);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000AD960 File Offset: 0x000ACD60
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void Close()
		{
			this.VerifyAPI();
			this.VerifyNotControlledByClock();
			HRESULT.Check(MILMedia.Close(this._nativeMedia));
			this.SetClock(null, null);
			this.Init();
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000AD998 File Offset: 0x000ACD98
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void SendCommandMedia(DUCE.Channel channel, DUCE.ResourceHandle handle, bool notifyUceDirectly)
		{
			this.SendMediaPlayerCommand(channel, handle, notifyUceDirectly);
			if (!notifyUceDirectly)
			{
				this.NeedUIFrameUpdate();
			}
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000AD9B8 File Offset: 0x000ACDB8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void NeedUIFrameUpdate()
		{
			this.VerifyAPI();
			HRESULT.Check(MILMedia.NeedUIFrameUpdate(this._nativeMedia));
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000AD9DC File Offset: 0x000ACDDC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void CreateMedia(MediaPlayer mediaPlayer)
		{
			this.CheckMediaDisabledFlags();
			SafeMILHandle pEventProxy = null;
			MediaEventsHelper.CreateMediaEventsHelper(mediaPlayer, out this._mediaEventsHelper, out pEventProxy);
			try
			{
				using (FactoryMaker factoryMaker = new FactoryMaker())
				{
					HRESULT.Check(UnsafeNativeMethods.MILFactory2.CreateMediaPlayer(factoryMaker.FactoryPtr, pEventProxy, SecurityHelper.CallerHasMediaPermission(MediaPermissionAudio.AllAudio, MediaPermissionVideo.AllVideo, MediaPermissionImage.NoImage), out this._nativeMedia));
				}
			}
			catch
			{
				if (this._nativeMedia != null && !this._nativeMedia.IsInvalid)
				{
					this._nativeMedia.Close();
				}
				throw;
			}
			this._helper = new MediaPlayerState.Helper(this._nativeMedia);
			AppDomain.CurrentDomain.ProcessExit += this._helper.ProcessExitHandler;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000ADAB8 File Offset: 0x000ACEB8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void OpenMedia(Uri source)
		{
			string src = null;
			if (source != null && source.IsAbsoluteUri && source.Scheme == PackUriHelper.UriSchemePack)
			{
				try
				{
					source = BaseUriHelper.ConvertPackUriToAbsoluteExternallyVisibleUri(source);
				}
				catch (InvalidOperationException)
				{
					source = null;
					this._mediaEventsHelper.RaiseMediaFailed(new NotSupportedException(SR.Get("Media_PackURIsAreNotSupported", null)));
				}
			}
			if (source != null)
			{
				bool flag = false;
				Uri baseDirectory = SecurityHelper.GetBaseDirectory(AppDomain.CurrentDomain);
				Uri uri = this.ResolveUri(source, baseDirectory);
				if (SecurityHelper.AreStringTypesEqual(uri.Scheme, Uri.UriSchemeHttps))
				{
					Uri uri2 = SecurityHelper.ExtractUriForClickOnceDeployedApp();
					if (!SecurityHelper.AreStringTypesEqual(uri2.Scheme, Uri.UriSchemeHttps))
					{
						new WebPermission(NetworkAccess.Connect, BindUriHelper.UriToString(uri)).Assert();
						flag = true;
					}
				}
				else
				{
					new FileIOPermission(FileIOPermissionAccess.Read, baseDirectory.LocalPath).Assert();
					flag = true;
				}
				try
				{
					src = this.DemandPermissions(uri);
					goto IL_DD;
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			src = null;
			IL_DD:
			HRESULT.Check(MILMedia.Open(this._nativeMedia, src));
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000ADBE8 File Offset: 0x000ACFE8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void CheckMediaDisabledFlags()
		{
			if (SafeSecurityHelper.IsFeatureDisabled(SafeSecurityHelper.KeyToRead.MediaAudioOrVideoDisable))
			{
				SecurityHelper.DemandMediaPermission(MediaPermissionAudio.AllAudio, MediaPermissionVideo.AllVideo, MediaPermissionImage.NoImage);
			}
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000ADC08 File Offset: 0x000AD008
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private Uri ResolveUri(Uri uri, Uri appBase)
		{
			if (uri.IsAbsoluteUri)
			{
				return uri;
			}
			return new Uri(appBase, uri);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000ADC28 File Offset: 0x000AD028
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private string DemandPermissions(Uri absoluteUri)
		{
			string text = BindUriHelper.UriToString(absoluteUri);
			if (SecurityHelper.MapUrlToZoneWrapper(absoluteUri) == 0)
			{
				if (absoluteUri.IsFile)
				{
					text = absoluteUri.LocalPath;
					new FileIOPermission(FileIOPermissionAccess.Read, text).Demand();
				}
			}
			else if (absoluteUri.IsFile && absoluteUri.IsUnc)
			{
				SecurityHelper.EnforceUncContentAccessRules(absoluteUri);
				if (!SecurityHelper.CallerHasMediaPermission(MediaPermissionAudio.SafeAudio, MediaPermissionVideo.SafeVideo, MediaPermissionImage.NoImage))
				{
					new FileIOPermission(FileIOPermissionAccess.Read, text).Demand();
				}
			}
			else if (absoluteUri.Scheme != Uri.UriSchemeHttps)
			{
				SecurityHelper.BlockCrossDomainForHttpsApps(absoluteUri);
				if (!SecurityHelper.CallerHasMediaPermission(MediaPermissionAudio.SafeAudio, MediaPermissionVideo.SafeVideo, MediaPermissionImage.NoImage))
				{
					new WebPermission(NetworkAccess.Connect, text).Demand();
				}
			}
			else
			{
				new WebPermission(NetworkAccess.Connect, text).Demand();
			}
			return text;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000ADCD4 File Offset: 0x000AD0D4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void SetPosition(TimeSpan value)
		{
			this.VerifyAPI();
			HRESULT.Check(MILMedia.SetPosition(this._nativeMedia, value.Ticks));
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000ADD00 File Offset: 0x000AD100
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private TimeSpan GetPosition()
		{
			this.VerifyAPI();
			long value = 0L;
			HRESULT.Check(MILMedia.GetPosition(this._nativeMedia, ref value));
			return TimeSpan.FromTicks(value);
		}

		// Token: 0x170008F1 RID: 2289
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x000ADD30 File Offset: 0x000AD130
		private double PrivateSpeedRatio
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			set
			{
				this.VerifyAPI();
				if (double.IsNaN(value))
				{
					throw new ArgumentException(SR.Get("ParameterValueCannotBeNaN"), "value");
				}
				HRESULT.Check(MILMedia.SetRate(this._nativeMedia, value));
			}
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000ADD74 File Offset: 0x000AD174
		internal void SetSpeedRatio(double value)
		{
			this._speedRatio = value;
			if (!this._paused || this._mediaClock != null)
			{
				this.PrivateSpeedRatio = this._speedRatio;
			}
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000ADDA4 File Offset: 0x000AD1A4
		internal void SetSource(Uri source)
		{
			if (source != this._sourceUri)
			{
				this.OpenMedia(source);
				this._sourceUri = source;
			}
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000ADDD0 File Offset: 0x000AD1D0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void VerifyAPI()
		{
			this._dispatcher.VerifyAccess();
			if (this._nativeMedia == null || this._nativeMedia.IsInvalid)
			{
				throw new NotSupportedException(SR.Get("Image_BadVersion"));
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000ADE10 File Offset: 0x000AD210
		private void VerifyNotControlledByClock()
		{
			if (this.Clock != null)
			{
				throw new InvalidOperationException(SR.Get("Media_NotAllowedWhileTimingEngineInControl"));
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000ADE38 File Offset: 0x000AD238
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void SendMediaPlayerCommand(DUCE.Channel channel, DUCE.ResourceHandle handle, bool notifyUceDirectly)
		{
			UnsafeNativeMethods.MILUnknown.AddRef(this._nativeMedia);
			channel.SendCommandMedia(handle, this._nativeMedia, notifyUceDirectly);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000ADE60 File Offset: 0x000AD260
		private void OnNewFrame(object sender, EventArgs args)
		{
			this._newFrameHelper.InvokeEvents(sender, args);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000ADE7C File Offset: 0x000AD27C
		private void OnMediaOpened(object sender, EventArgs args)
		{
			this._mediaOpenedHelper.InvokeEvents(sender, args);
		}

		// Token: 0x040013C7 RID: 5063
		private double _volume;

		// Token: 0x040013C8 RID: 5064
		private double _balance;

		// Token: 0x040013C9 RID: 5065
		private bool _muted;

		// Token: 0x040013CA RID: 5066
		private bool _scrubbingEnabled;

		// Token: 0x040013CB RID: 5067
		[SecurityCritical]
		private SafeMediaHandle _nativeMedia;

		// Token: 0x040013CC RID: 5068
		private MediaEventsHelper _mediaEventsHelper;

		// Token: 0x040013CD RID: 5069
		private const double DEFAULT_VOLUME = 0.5;

		// Token: 0x040013CE RID: 5070
		private const double DEFAULT_BALANCE = 0.0;

		// Token: 0x040013CF RID: 5071
		private double _speedRatio;

		// Token: 0x040013D0 RID: 5072
		private bool _paused;

		// Token: 0x040013D1 RID: 5073
		private Uri _sourceUri;

		// Token: 0x040013D2 RID: 5074
		private MediaClock _mediaClock;

		// Token: 0x040013D3 RID: 5075
		private Dispatcher _dispatcher;

		// Token: 0x040013D4 RID: 5076
		private UniqueEventHelper _newFrameHelper = new UniqueEventHelper();

		// Token: 0x040013D5 RID: 5077
		private UniqueEventHelper _mediaOpenedHelper = new UniqueEventHelper();

		// Token: 0x040013D6 RID: 5078
		private const float _defaultDevicePixelsPerInch = 96f;

		// Token: 0x040013D7 RID: 5079
		private MediaPlayerState.Helper _helper;

		// Token: 0x02000892 RID: 2194
		private class Helper
		{
			// Token: 0x0600581F RID: 22559 RVA: 0x001675FC File Offset: 0x001669FC
			[SecurityCritical]
			internal Helper(SafeMediaHandle nativeMedia)
			{
				this._nativeMedia = new WeakReference(nativeMedia);
			}

			// Token: 0x06005820 RID: 22560 RVA: 0x0016761C File Offset: 0x00166A1C
			[SecurityCritical]
			internal void ProcessExitHandler(object sender, EventArgs args)
			{
				SafeMediaHandle safeMediaHandle = (SafeMediaHandle)this._nativeMedia.Target;
				if (safeMediaHandle != null)
				{
					MILMedia.ProcessExitHandler(safeMediaHandle);
				}
			}

			// Token: 0x040028E9 RID: 10473
			[SecurityCritical]
			private WeakReference _nativeMedia;
		}
	}
}
