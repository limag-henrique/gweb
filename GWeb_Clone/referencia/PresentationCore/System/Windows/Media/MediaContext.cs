using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Utility;
using MS.Win32;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	// Token: 0x0200036D RID: 877
	internal class MediaContext : DispatcherObject, IDisposable, IClock
	{
		// Token: 0x06001ED8 RID: 7896 RVA: 0x0007C9B8 File Offset: 0x0007BDB8
		static MediaContext()
		{
			SafeNativeMethods.QueryPerformanceFrequency(out MediaContext._perfCounterFreq);
			long num;
			if (MediaContext.IsClockSupported)
			{
				SafeNativeMethods.QueryPerformanceCounter(out num);
			}
			else
			{
				num = 0L;
			}
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordGraphics, EventTrace.Event.WClientQPCFrequency, MediaContext._perfCounterFreq, num);
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0007CA24 File Offset: 0x0007BE24
		internal static bool IsClockSupported
		{
			get
			{
				return MediaContext._perfCounterFreq != 0L;
			}
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0007CA3C File Offset: 0x0007BE3C
		private static long CountsToTicks(long counts)
		{
			return 10000000L * (counts / MediaContext._perfCounterFreq) + 10000000L * (counts % MediaContext._perfCounterFreq) / MediaContext._perfCounterFreq;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x0007CA6C File Offset: 0x0007BE6C
		private static long TicksToCounts(long ticks)
		{
			return MediaContext._perfCounterFreq * (ticks / 10000000L) + MediaContext._perfCounterFreq * (ticks % 10000000L) / 10000000L;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x0007CAA0 File Offset: 0x0007BEA0
		private static bool IsPrime(int number)
		{
			if ((number & 1) == 0)
			{
				return false;
			}
			int num = (int)Math.Sqrt((double)number);
			for (int i = 3; i <= num; i += 2)
			{
				if (number % i == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0007CAD4 File Offset: 0x0007BED4
		private static int FindNextPrime(int number)
		{
			while (!MediaContext.IsPrime(++number))
			{
			}
			return number;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0007CAF0 File Offset: 0x0007BEF0
		internal static bool ShouldRenderEvenWhenNoDisplayDevicesAreAvailable { get; } = (!Environment.UserInteractive) ? (!CoreAppContextSwitches.ShouldNotRenderInNonInteractiveWindowStation) : CoreAppContextSwitches.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable;

		// Token: 0x06001EDF RID: 7903 RVA: 0x0007CB04 File Offset: 0x0007BF04
		internal MediaContext(Dispatcher dispatcher)
		{
			if (MediaContext.IsClockSupported)
			{
				SafeNativeMethods.QueryPerformanceCounter(out this._lastPresentationTime);
				this._estimatedNextPresentationTime = TimeSpan.FromTicks(MediaContext.CountsToTicks(this._lastPresentationTime));
			}
			this._contextGuid = Guid.NewGuid();
			this._registeredICompositionTargets = new Dictionary<ICompositionTarget, object>();
			this._renderModeMessage = new DispatcherOperationCallback(this.InvalidateRenderMode);
			this._notificationWindow = new MediaContextNotificationWindow(this);
			if (MediaSystem.Startup(this))
			{
				this._isConnected = MediaSystem.ConnectChannels(this);
			}
			this._destroyHandler = new EventHandler(this.OnDestroyContext);
			base.Dispatcher.ShutdownFinished += this._destroyHandler;
			this._renderMessage = new DispatcherOperationCallback(this.RenderMessageHandler);
			this._animRenderMessage = new DispatcherOperationCallback(this.AnimatedRenderMessageHandler);
			this._inputMarkerMessage = new DispatcherOperationCallback(this.InputMarkerMessageHandler);
			dispatcher.Reserved0 = this;
			this._timeManager = new TimeManager();
			this._timeManager.Start();
			this._timeManager.NeedTickSooner += this.OnNeedTickSooner;
			this._promoteRenderOpToInput = new DispatcherTimer(DispatcherPriority.Render);
			this._promoteRenderOpToInput.Tick += this.PromoteRenderOpToInput;
			this._promoteRenderOpToRender = new DispatcherTimer(DispatcherPriority.Render);
			this._promoteRenderOpToRender.Tick += this.PromoteRenderOpToRender;
			this._estimatedNextVSyncTimer = new DispatcherTimer(DispatcherPriority.Render);
			this._estimatedNextVSyncTimer.Tick += this.EstimatedNextVSyncTimeExpired;
			this._commitPendingAfterRender = false;
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0007CCAC File Offset: 0x0007C0AC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void NotifySyncChannelMessage(DUCE.Channel channel)
		{
			DUCE.MilMessage.Message message;
			while (channel.PeekNextMessage(out message))
			{
				switch (message.Type)
				{
				case DUCE.MilMessage.Type.Caps:
				case DUCE.MilMessage.Type.SyncModeStatus:
				case DUCE.MilMessage.Type.Presented:
					continue;
				case DUCE.MilMessage.Type.PartitionIsZombie:
					this._channelManager.RemoveSyncChannels();
					this.NotifyPartitionIsZombie(message.HRESULTFailure.HRESULTFailureCode);
					continue;
				}
				this.HandleInvalidPacketNotification();
			}
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0007CD18 File Offset: 0x0007C118
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void NotifyChannelMessage()
		{
			if (this.Channel != null)
			{
				DUCE.MilMessage.Message message;
				while (this.Channel.PeekNextMessage(out message))
				{
					DUCE.MilMessage.Type type = message.Type;
					switch (type)
					{
					case DUCE.MilMessage.Type.Caps:
						this.NotifySetCaps(message.Caps.Caps);
						continue;
					case (DUCE.MilMessage.Type)5:
					case (DUCE.MilMessage.Type)7:
					case (DUCE.MilMessage.Type)8:
						break;
					case DUCE.MilMessage.Type.PartitionIsZombie:
						this.NotifyPartitionIsZombie(message.HRESULTFailure.HRESULTFailureCode);
						continue;
					case DUCE.MilMessage.Type.SyncModeStatus:
						this.NotifySyncModeStatus(message.SyncModeStatus.Enabled);
						continue;
					case DUCE.MilMessage.Type.Presented:
						this.NotifyPresented(message.Presented.PresentationResults, message.Presented.PresentationTime, message.Presented.RefreshRate);
						continue;
					default:
						if (type == DUCE.MilMessage.Type.BadPixelShader)
						{
							this.NotifyBadPixelShader();
							continue;
						}
						break;
					}
					this.HandleInvalidPacketNotification();
				}
			}
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0007CDEC File Offset: 0x0007C1EC
		internal void PostInvalidateRenderMode()
		{
			base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, this._renderModeMessage, null);
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0007CE10 File Offset: 0x0007C210
		private object InvalidateRenderMode(object dontCare)
		{
			foreach (ICompositionTarget compositionTarget in this._registeredICompositionTargets.Keys)
			{
				HwndTarget hwndTarget = compositionTarget as HwndTarget;
				if (hwndTarget != null)
				{
					hwndTarget.InvalidateRenderMode();
				}
			}
			return null;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0007CE80 File Offset: 0x0007C280
		private void NotifySetCaps(MilGraphicsAccelerationCaps caps)
		{
			this.PixelShaderVersion = caps.PixelShaderVersion;
			this.MaxPixelShader30InstructionSlots = caps.MaxPixelShader30InstructionSlots;
			this.HasSSE2Support = Convert.ToBoolean(caps.HasSSE2Support);
			this.MaxTextureSize = new Size(caps.MaxTextureWidth, caps.MaxTextureHeight);
			int tierValue = caps.TierValue;
			if (this._tier != tierValue)
			{
				this._tier = tierValue;
				if (this.TierChanged != null)
				{
					this.TierChanged(null, null);
				}
			}
		}

		// Token: 0x1400018B RID: 395
		// (add) Token: 0x06001EE5 RID: 7909 RVA: 0x0007CF00 File Offset: 0x0007C300
		// (remove) Token: 0x06001EE6 RID: 7910 RVA: 0x0007CF38 File Offset: 0x0007C338
		internal event EventHandler InvalidPixelShaderEncountered;

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0007CF70 File Offset: 0x0007C370
		private void NotifyBadPixelShader()
		{
			if (this.InvalidPixelShaderEncountered != null)
			{
				this.InvalidPixelShaderEncountered(null, null);
				return;
			}
			throw new InvalidOperationException(SR.Get("MediaContext_NoBadShaderHandler"));
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0007CFA4 File Offset: 0x0007C3A4
		private void NotifyPartitionIsZombie(int failureCode)
		{
			if (failureCode == -2147024882)
			{
				throw new OutOfMemoryException();
			}
			if (failureCode != -2005532292)
			{
				throw new InvalidOperationException(SR.Get("MediaContext_RenderThreadError"));
			}
			throw new OutOfMemoryException(SR.Get("MediaContext_OutOfVideoMemory"));
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0007CFE8 File Offset: 0x0007C3E8
		private void HandleInvalidPacketNotification()
		{
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0007CFF8 File Offset: 0x0007C3F8
		internal int Tier
		{
			get
			{
				return this._tier;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0007D00C File Offset: 0x0007C40C
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x0007D020 File Offset: 0x0007C420
		internal uint PixelShaderVersion { get; private set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0007D034 File Offset: 0x0007C434
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x0007D048 File Offset: 0x0007C448
		internal uint MaxPixelShader30InstructionSlots { get; private set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0007D05C File Offset: 0x0007C45C
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x0007D070 File Offset: 0x0007C470
		internal bool HasSSE2Support { get; private set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0007D084 File Offset: 0x0007C484
		// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x0007D098 File Offset: 0x0007C498
		internal Size MaxTextureSize { get; private set; }

		// Token: 0x1400018C RID: 396
		// (add) Token: 0x06001EF3 RID: 7923 RVA: 0x0007D0AC File Offset: 0x0007C4AC
		// (remove) Token: 0x06001EF4 RID: 7924 RVA: 0x0007D0E4 File Offset: 0x0007C4E4
		internal event EventHandler TierChanged;

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0007D11C File Offset: 0x0007C51C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void RequestTier(DUCE.Channel channel)
		{
			DUCE.MILCMD_CHANNEL_REQUESTTIER milcmd_CHANNEL_REQUESTTIER;
			milcmd_CHANNEL_REQUESTTIER.Type = MILCMD.MilCmdChannelRequestTier;
			milcmd_CHANNEL_REQUESTTIER.ReturnCommonMinimum = 0U;
			channel.SendCommand((byte*)(&milcmd_CHANNEL_REQUESTTIER), sizeof(DUCE.MILCMD_CHANNEL_REQUESTTIER));
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0007D148 File Offset: 0x0007C548
		private void ScheduleNextRenderOp(TimeSpan minimumDelay)
		{
			if (!this._isDisconnecting && !this._needToCommitChannel)
			{
				TimeSpan timeSpan = TimeSpan.Zero;
				if (this.Rendering == null)
				{
					timeSpan = this._timeManager.GetNextTickNeeded();
				}
				if (timeSpan >= TimeSpan.Zero)
				{
					timeSpan = TimeSpan.FromTicks(Math.Max(timeSpan.Ticks, minimumDelay.Ticks));
					this.EnterInterlockedPresentation();
				}
				else
				{
					this.LeaveInterlockedPresentation();
				}
				if (timeSpan > TimeSpan.FromSeconds(1.0))
				{
					if (this._currentRenderOp == null)
					{
						this._currentRenderOp = base.Dispatcher.BeginInvoke(DispatcherPriority.Inactive, this._animRenderMessage, null);
						this._promoteRenderOpToRender.Interval = timeSpan;
						this._promoteRenderOpToRender.Start();
					}
				}
				else if (timeSpan > TimeSpan.Zero)
				{
					if (this._currentRenderOp == null)
					{
						this._currentRenderOp = base.Dispatcher.BeginInvoke(DispatcherPriority.Inactive, this._animRenderMessage, null);
						this._promoteRenderOpToInput.Interval = timeSpan;
						this._promoteRenderOpToInput.Start();
						this._promoteRenderOpToRender.Interval = TimeSpan.FromSeconds(1.0);
						this._promoteRenderOpToRender.Start();
					}
				}
				else if (timeSpan == TimeSpan.Zero)
				{
					DispatcherPriority priority = DispatcherPriority.Render;
					if (this._inputMarkerOp == null)
					{
						this._inputMarkerOp = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._inputMarkerMessage, null);
						this._lastInputMarkerTime = MediaContext.CurrentTicks;
					}
					else if (MediaContext.CurrentTicks - this._lastInputMarkerTime > 5000000L)
					{
						priority = DispatcherPriority.Input;
					}
					if (this._currentRenderOp == null)
					{
						this._currentRenderOp = base.Dispatcher.BeginInvoke(priority, this._animRenderMessage, null);
					}
					else
					{
						this._currentRenderOp.Priority = priority;
					}
					this._promoteRenderOpToInput.Stop();
					this._promoteRenderOpToRender.Stop();
				}
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordGraphics, EventTrace.Event.WClientScheduleRender, timeSpan.TotalMilliseconds);
			}
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0007D338 File Offset: 0x0007C738
		private void CommitChannelAfterNextVSync()
		{
			if (this._animationRenderRate != 0)
			{
				long currentTicks = MediaContext.CurrentTicks;
				long num = currentTicks + this.TicksUntilNextVsync(currentTicks) + 10000L;
				this._estimatedNextVSyncTimer.Interval = TimeSpan.FromTicks(num - currentTicks);
				this._estimatedNextVSyncTimer.Tag = num;
			}
			else
			{
				this._estimatedNextVSyncTimer.Interval = TimeSpan.FromMilliseconds(17.0);
			}
			this._estimatedNextVSyncTimer.Start();
			this._interlockState = MediaContext.InterlockState.WaitingForNextFrame;
			this._lastPresentationResults = MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_NOPRESENT;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0007D3BC File Offset: 0x0007C7BC
		private void NotifyPresented(MIL_PRESENTATION_RESULTS presentationResults, long presentationTime, int displayRefreshRate)
		{
			if (this.InterlockIsEnabled)
			{
				TimeSpan minimumDelay = TimeSpan.Zero;
				this._lastPresentationResults = presentationResults;
				this._interlockState = MediaContext.InterlockState.Idle;
				switch (presentationResults)
				{
				case MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_VSYNC:
					if (displayRefreshRate != this._displayRefreshRate)
					{
						this._displayRefreshRate = displayRefreshRate;
						this._adjustedRefreshRate = MediaContext.FindNextPrime(displayRefreshRate + 5);
					}
					this._animationRenderRate = Math.Max(this._adjustedRefreshRate, this._timeManager.GetMaxDesiredFrameRate());
					this._lastPresentationTime = presentationTime;
					break;
				case MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_NOPRESENT:
					this.CommitChannelAfterNextVSync();
					break;
				case MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_VSYNC_UNSUPPORTED:
					minimumDelay = this._timeDelay;
					break;
				case MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_DWM:
					this._animationRenderRate = displayRefreshRate;
					this._lastPresentationTime = presentationTime;
					break;
				}
				this._animationRenderRate = Math.Min(this._animationRenderRate, 1000);
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Event.WClientUceNotifyPresent, this._lastPresentationTime, (long)presentationResults);
				if (presentationResults == MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_NOPRESENT)
				{
					return;
				}
				this._estimatedNextVSyncTimer.Stop();
				if (!this.InterlockIsWaiting && this._needToCommitChannel)
				{
					if (this.HasCommittedThisVBlankInterval)
					{
						this.CommitChannelAfterNextVSync();
						return;
					}
					this.CommitChannel();
				}
				this.ScheduleNextRenderOp(minimumDelay);
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x0007D4D8 File Offset: 0x0007C8D8
		private bool HasCommittedThisVBlankInterval
		{
			get
			{
				return this._animationRenderRate != 0 && (MediaContext.CurrentTicks - this._lastCommitTime < this.RefreshPeriod && this._lastCommitTime > MediaContext.CountsToTicks(this._lastPresentationTime));
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0007D51C File Offset: 0x0007C91C
		internal static long CurrentTicks
		{
			get
			{
				long counts;
				SafeNativeMethods.QueryPerformanceCounter(out counts);
				return MediaContext.CountsToTicks(counts);
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x0007D538 File Offset: 0x0007C938
		private long RefreshPeriod
		{
			get
			{
				return 10000000L / (long)this._animationRenderRate;
			}
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0007D554 File Offset: 0x0007C954
		private long TicksSinceLastPresent(long currentTime)
		{
			return currentTime - MediaContext.CountsToTicks(this._lastPresentationTime);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0007D570 File Offset: 0x0007C970
		private long TicksSinceLastVsync(long currentTime)
		{
			return this.TicksSinceLastPresent(currentTime) % this.RefreshPeriod;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0007D58C File Offset: 0x0007C98C
		private long TicksUntilNextVsync(long currentTime)
		{
			return this.RefreshPeriod - this.TicksSinceLastVsync(currentTime);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0007D5A8 File Offset: 0x0007C9A8
		private void NotifySyncModeStatus(int enabledResult)
		{
			if (this._interlockState == MediaContext.InterlockState.RequestedStart)
			{
				if (enabledResult >= 0)
				{
					this._interlockState = MediaContext.InterlockState.Idle;
					if (this.Channel != null)
					{
						if (this.CommittingBatch != null)
						{
							this.CommittingBatch(this.Channel, new EventArgs());
						}
						this.Channel.SyncFlush();
						return;
					}
				}
				else
				{
					this._interlockState = MediaContext.InterlockState.Disabled;
				}
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0007D604 File Offset: 0x0007CA04
		TimeSpan IClock.CurrentTime
		{
			get
			{
				long counts;
				SafeNativeMethods.QueryPerformanceCounter(out counts);
				long num = MediaContext.CountsToTicks(counts);
				if (this._interlockState != MediaContext.InterlockState.Disabled)
				{
					if (this._animationRenderRate != 0 && this._lastPresentationResults != MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_VSYNC_UNSUPPORTED)
					{
						this._averagePresentationInterval = this.RefreshPeriod;
						long num2 = 0L;
						if (this.InterlockIsWaiting)
						{
							num2 = 1L;
						}
						long num3 = num + this.TicksUntilNextVsync(num);
						long num4 = num3 + num2 * this.RefreshPeriod;
						if ((num4 - this._estimatedNextPresentationTime.Ticks) * (long)this._animationRenderRate > TimeSpan.FromMilliseconds(500.0).Ticks)
						{
							this._estimatedNextPresentationTime = TimeSpan.FromTicks(num4);
						}
					}
					else
					{
						this._estimatedNextPresentationTime = TimeSpan.FromTicks(num);
					}
				}
				else
				{
					this._estimatedNextPresentationTime = TimeSpan.FromTicks(num);
				}
				return this._estimatedNextPresentationTime;
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0007D6CC File Offset: 0x0007CACC
		internal void CreateChannels()
		{
			this._channelManager.CreateChannels();
			DUCE.NotifyPolicyChangeForNonInteractiveMode(MediaContext.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable, this.Channel);
			this.HookNotifications();
			this._uceEtwEvent.CreateOrAddRefOnChannel(this, this.Channel, DUCE.ResourceType.TYPE_ETWEVENTRESOURCE);
			this.RequestTier(this.Channel);
			this.Channel.CloseBatch();
			this.Channel.Commit();
			this.CompleteRender();
			this.NotifyChannelMessage();
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0007D740 File Offset: 0x0007CB40
		private void RemoveChannels()
		{
			if (this.Channel != null)
			{
				this._uceEtwEvent.ReleaseOnChannel(this.Channel);
				this.LeaveInterlockedPresentation();
			}
			this._channelManager.RemoveChannels();
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0007D778 File Offset: 0x0007CB78
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void EnterInterlockedPresentation()
		{
			if (!this.InterlockIsEnabled && MediaSystem.AnimationSmoothing && this.Channel.MarshalType == ChannelMarshalType.ChannelMarshalTypeCrossThread && MediaContext.IsClockSupported)
			{
				DUCE.MILCMD_PARTITION_SETVBLANKSYNCMODE milcmd_PARTITION_SETVBLANKSYNCMODE;
				milcmd_PARTITION_SETVBLANKSYNCMODE.Type = MILCMD.MilCmdPartitionSetVBlankSyncMode;
				milcmd_PARTITION_SETVBLANKSYNCMODE.Enable = 1U;
				this.Channel.SendCommand((byte*)(&milcmd_PARTITION_SETVBLANKSYNCMODE), sizeof(DUCE.MILCMD_PARTITION_SETVBLANKSYNCMODE), true);
				this._interlockState = MediaContext.InterlockState.RequestedStart;
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0007D7D8 File Offset: 0x0007CBD8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void LeaveInterlockedPresentation()
		{
			bool flag = this._interlockState == MediaContext.InterlockState.Disabled;
			if (this._interlockState == MediaContext.InterlockState.WaitingForResponse)
			{
				this.CompleteRender();
			}
			this._estimatedNextVSyncTimer.Stop();
			if (!flag)
			{
				this._interlockState = MediaContext.InterlockState.Disabled;
				DUCE.MILCMD_PARTITION_SETVBLANKSYNCMODE milcmd_PARTITION_SETVBLANKSYNCMODE;
				milcmd_PARTITION_SETVBLANKSYNCMODE.Type = MILCMD.MilCmdPartitionSetVBlankSyncMode;
				milcmd_PARTITION_SETVBLANKSYNCMODE.Enable = 0U;
				this.Channel.SendCommand((byte*)(&milcmd_PARTITION_SETVBLANKSYNCMODE), sizeof(DUCE.MILCMD_PARTITION_SETVBLANKSYNCMODE), true);
				this._needToCommitChannel = true;
				this.CommitChannel();
			}
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0007D848 File Offset: 0x0007CC48
		private void HookNotifications()
		{
			this._notificationWindow.SetAsChannelNotificationWindow();
			this.RegisterForNotifications(this.Channel);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0007D86C File Offset: 0x0007CC6C
		internal static MediaContext From(Dispatcher dispatcher)
		{
			MediaContext mediaContext = (MediaContext)dispatcher.Reserved0;
			if (mediaContext == null)
			{
				mediaContext = new MediaContext(dispatcher);
			}
			return mediaContext;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0007D890 File Offset: 0x0007CC90
		internal static MediaContext CurrentMediaContext
		{
			get
			{
				return MediaContext.From(Dispatcher.CurrentDispatcher);
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0007D8A8 File Offset: 0x0007CCA8
		private void OnDestroyContext(object sender, EventArgs e)
		{
			this.Dispose();
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0007D8BC File Offset: 0x0007CCBC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public virtual void Dispose()
		{
			if (!this._isDisposed)
			{
				ICompositionTarget[] array = new ICompositionTarget[this._registeredICompositionTargets.Count];
				this._registeredICompositionTargets.Keys.CopyTo(array, 0);
				foreach (ICompositionTarget compositionTarget in array)
				{
					compositionTarget.Dispose();
				}
				this._registeredICompositionTargets = null;
				this._notificationWindow.Dispose();
				base.Dispatcher.ShutdownFinished -= this._destroyHandler;
				this._destroyHandler = null;
				this._timeManager.NeedTickSooner -= this.OnNeedTickSooner;
				this._timeManager.Stop();
				this._isDisposed = true;
				this.RemoveChannels();
				MediaSystem.Shutdown(this);
				this._timeManager = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0007D980 File Offset: 0x0007CD80
		internal static void RegisterICompositionTarget(Dispatcher dispatcher, ICompositionTarget iv)
		{
			MediaContext mediaContext = MediaContext.From(dispatcher);
			mediaContext.RegisterICompositionTargetInternal(iv);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0007D99C File Offset: 0x0007CD9C
		private void RegisterICompositionTargetInternal(ICompositionTarget iv)
		{
			if (this.Channel != null)
			{
				DUCE.ChannelSet channelSet = (this._currentRenderingChannel == null) ? this.GetChannels() : this._currentRenderingChannel.Value;
				iv.AddRefOnChannel(channelSet.Channel, channelSet.OutOfBandChannel);
			}
			this._registeredICompositionTargets.Add(iv, null);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0007D9F4 File Offset: 0x0007CDF4
		internal static void UnregisterICompositionTarget(Dispatcher dispatcher, ICompositionTarget iv)
		{
			MediaContext.From(dispatcher).UnregisterICompositionTargetInternal(iv);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0007DA10 File Offset: 0x0007CE10
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void UnregisterICompositionTargetInternal(ICompositionTarget iv)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (this.Channel != null)
			{
				DUCE.ChannelSet channelSet = (this._currentRenderingChannel == null) ? this.GetChannels() : this._currentRenderingChannel.Value;
				iv.ReleaseOnChannel(channelSet.Channel, channelSet.OutOfBandChannel);
			}
			this._registeredICompositionTargets.Remove(iv);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0007DA70 File Offset: 0x0007CE70
		internal void BeginInvokeOnRender(DispatcherOperationCallback callback, object arg)
		{
			if (this._invokeOnRenderCallbacks == null)
			{
				this._invokeOnRenderCallbacks = new FrugalObjectList<MediaContext.InvokeOnRenderCallback>();
			}
			this._invokeOnRenderCallbacks.Add(new MediaContext.InvokeOnRenderCallback(callback, arg));
			if (!this._isRendering)
			{
				this.PostRender();
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0007DAB4 File Offset: 0x0007CEB4
		[FriendAccessAllowed]
		internal LoadedOrUnloadedOperation AddLoadedOrUnloadedCallback(DispatcherOperationCallback callback, DependencyObject target)
		{
			LoadedOrUnloadedOperation loadedOrUnloadedOperation = new LoadedOrUnloadedOperation(callback, target);
			if (this._loadedOrUnloadedPendingOperations == null)
			{
				this._loadedOrUnloadedPendingOperations = new FrugalObjectList<LoadedOrUnloadedOperation>(1);
			}
			this._loadedOrUnloadedPendingOperations.Add(loadedOrUnloadedOperation);
			return loadedOrUnloadedOperation;
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0007DAEC File Offset: 0x0007CEEC
		[FriendAccessAllowed]
		internal void RemoveLoadedOrUnloadedCallback(LoadedOrUnloadedOperation op)
		{
			op.Cancel();
			if (this._loadedOrUnloadedPendingOperations != null)
			{
				for (int i = 0; i < this._loadedOrUnloadedPendingOperations.Count; i++)
				{
					LoadedOrUnloadedOperation loadedOrUnloadedOperation = this._loadedOrUnloadedPendingOperations[i];
					if (loadedOrUnloadedOperation == op)
					{
						this._loadedOrUnloadedPendingOperations.RemoveAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0007DB3C File Offset: 0x0007CF3C
		internal void PostRender()
		{
			if (this._isDisposed)
			{
				return;
			}
			if (!this._isRendering)
			{
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Event.WClientPostRender);
				if (this._currentRenderOp != null)
				{
					this._currentRenderOp.Priority = DispatcherPriority.Render;
				}
				else
				{
					this._currentRenderOp = base.Dispatcher.BeginInvoke(DispatcherPriority.Render, this._renderMessage, null);
				}
				this._promoteRenderOpToInput.Stop();
				this._promoteRenderOpToRender.Stop();
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0007DBB0 File Offset: 0x0007CFB0
		internal void Resize(ICompositionTarget resizedCompositionTarget)
		{
			if (this._currentRenderOp != null)
			{
				this._currentRenderOp.Abort();
				this._currentRenderOp = null;
			}
			this._promoteRenderOpToInput.Stop();
			this._promoteRenderOpToRender.Stop();
			this.RenderMessageHandler(resizedCompositionTarget);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0007DBF8 File Offset: 0x0007CFF8
		private object RenderMessageHandler(object resizedCompositionTarget)
		{
			if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info))
			{
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientRenderHandlerBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info, PerfService.GetPerfElementID(this));
			}
			this.RenderMessageHandlerCore(resizedCompositionTarget);
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Event.WClientRenderHandlerEnd);
			return null;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0007DC4C File Offset: 0x0007D04C
		private object AnimatedRenderMessageHandler(object resizedCompositionTarget)
		{
			if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info))
			{
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientAnimRenderHandlerBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info, PerfService.GetPerfElementID(this));
			}
			this.RenderMessageHandlerCore(resizedCompositionTarget);
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Event.WClientAnimRenderHandlerEnd);
			return null;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0007DCA0 File Offset: 0x0007D0A0
		private object InputMarkerMessageHandler(object arg)
		{
			this._inputMarkerOp = null;
			return null;
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0007DCB8 File Offset: 0x0007D0B8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void RenderMessageHandlerCore(object resizedCompositionTarget)
		{
			if (this.Channel == null)
			{
				return;
			}
			this._isRendering = true;
			this._promoteRenderOpToInput.Stop();
			this._promoteRenderOpToRender.Stop();
			bool flag = true;
			try
			{
				int num = 0;
				for (;;)
				{
					num++;
					if (num > 153)
					{
						break;
					}
					this._timeManager.Tick();
					this._timeManager.LockTickTime();
					this.FireInvokeOnRenderCallbacks();
					if (this.Rendering != null && num == 1)
					{
						this.Rendering(base.Dispatcher, new RenderingEventArgs(this._timeManager.LastTickTime));
						this.FireInvokeOnRenderCallbacks();
					}
					if (!this._timeManager.IsDirty)
					{
						goto Block_7;
					}
				}
				throw new InvalidOperationException(SR.Get("MediaContext_InfiniteTickLoop"));
				Block_7:
				this._timeManager.UnlockTickTime();
				MediaSystem.PropagateDirtyRectangleSettings();
				InputManager.UnsecureCurrent.InvalidateInputDevices();
				bool flag2 = !this.InterlockIsWaiting;
				this.Render((ICompositionTarget)resizedCompositionTarget);
				if (this._currentRenderOp != null)
				{
					this._currentRenderOp.Abort();
					this._currentRenderOp = null;
				}
				if (!this.InterlockIsEnabled)
				{
					this.ScheduleNextRenderOp(this._timeDelay);
				}
				else if (flag2)
				{
					this.ScheduleNextRenderOp(TimeSpan.Zero);
				}
				flag = false;
			}
			finally
			{
				if (flag && this._currentRenderOp != null)
				{
					this._currentRenderOp.Abort();
					this._currentRenderOp = null;
				}
				this._isRendering = false;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0007DE1C File Offset: 0x0007D21C
		private int InvokeOnRenderCallbacksCount
		{
			get
			{
				if (this._invokeOnRenderCallbacks == null)
				{
					return 0;
				}
				return this._invokeOnRenderCallbacks.Count;
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0007DE40 File Offset: 0x0007D240
		private void FireInvokeOnRenderCallbacks()
		{
			int num = 0;
			int invokeOnRenderCallbacksCount = this.InvokeOnRenderCallbacksCount;
			for (;;)
			{
				if (invokeOnRenderCallbacksCount <= 0)
				{
					this.FireLoadedPendingCallbacks();
					invokeOnRenderCallbacksCount = this.InvokeOnRenderCallbacksCount;
					if (invokeOnRenderCallbacksCount <= 0)
					{
						return;
					}
				}
				else
				{
					num++;
					if (num > 153)
					{
						break;
					}
					FrugalObjectList<MediaContext.InvokeOnRenderCallback> invokeOnRenderCallbacks = this._invokeOnRenderCallbacks;
					this._invokeOnRenderCallbacks = null;
					for (int i = 0; i < invokeOnRenderCallbacksCount; i++)
					{
						invokeOnRenderCallbacks[i].DoWork();
					}
					invokeOnRenderCallbacksCount = this.InvokeOnRenderCallbacksCount;
				}
			}
			throw new InvalidOperationException(SR.Get("MediaContext_InfiniteLayoutLoop"));
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x0007DEB8 File Offset: 0x0007D2B8
		private void FireLoadedPendingCallbacks()
		{
			if (this._loadedOrUnloadedPendingOperations != null)
			{
				int count = this._loadedOrUnloadedPendingOperations.Count;
				if (count == 0)
				{
					return;
				}
				FrugalObjectList<LoadedOrUnloadedOperation> loadedOrUnloadedPendingOperations = this._loadedOrUnloadedPendingOperations;
				this._loadedOrUnloadedPendingOperations = null;
				for (int i = 0; i < count; i++)
				{
					loadedOrUnloadedPendingOperations[i].DoWork();
				}
			}
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x0007DF04 File Offset: 0x0007D304
		private void Render(ICompositionTarget resizedCompositionTarget)
		{
			using (base.Dispatcher.DisableProcessing())
			{
				bool flag = false;
				uint num = (uint)Interlocked.Increment(ref MediaContext._contextRenderID);
				if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info))
				{
					flag = true;
					DUCE.ETWEvent.RaiseEvent(this._uceEtwEvent.Handle, num, this.Channel);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMediaRenderBegin, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info, new object[]
					{
						num,
						MediaContext.TicksToCounts(this._estimatedNextPresentationTime.Ticks)
					});
				}
				foreach (ICompositionTarget compositionTarget in this._registeredICompositionTargets.Keys)
				{
					DUCE.ChannelSet channelSet;
					channelSet.Channel = this._channelManager.Channel;
					channelSet.OutOfBandChannel = this._channelManager.OutOfBandChannel;
					this._currentRenderingChannel = new DUCE.ChannelSet?(channelSet);
					compositionTarget.Render(compositionTarget == resizedCompositionTarget, channelSet.Channel);
					this._currentRenderingChannel = null;
				}
				this.RaiseResourcesUpdated();
				if (this.Channel != null)
				{
					this.Channel.CloseBatch();
				}
				this._needToCommitChannel = true;
				this._commitPendingAfterRender = true;
				if (!this.InterlockIsWaiting)
				{
					if (this.HasCommittedThisVBlankInterval)
					{
						this.CommitChannelAfterNextVSync();
					}
					else
					{
						this.CommitChannel();
					}
				}
				if (flag)
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientMediaRenderEnd, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info);
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientUIResponse, EventTrace.Keyword.KeywordGraphics, EventTrace.Level.Info, new object[]
					{
						this.GetHashCode(),
						num
					});
				}
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0007E0E4 File Offset: 0x0007D4E4
		private void CommitChannel()
		{
			if (this.Channel != null)
			{
				if (this.InterlockIsEnabled)
				{
					long currentTicks = MediaContext.CurrentTicks;
					long num = this._estimatedNextPresentationTime.Ticks;
					if (this._animationRenderRate > 0)
					{
						long num2 = currentTicks + this.TicksUntilNextVsync(currentTicks);
						if (num2 > num)
						{
							num = num2;
						}
					}
					this.RequestPresentedNotification(this.Channel, MediaContext.TicksToCounts(num));
					this._interlockState = MediaContext.InterlockState.WaitingForResponse;
					this._lastCommitTime = currentTicks;
				}
				if (this.CommittingBatch != null)
				{
					this.CommittingBatch(this.Channel, new EventArgs());
				}
				this.Channel.Commit();
				if (this._commitPendingAfterRender)
				{
					if (this._renderCompleteHandlers != null)
					{
						this._renderCompleteHandlers(this, null);
					}
					this._commitPendingAfterRender = false;
				}
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordGraphics, EventTrace.Event.WClientUICommitChannel, MediaContext._contextRenderID);
			}
			this._needToCommitChannel = false;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0007E1BC File Offset: 0x0007D5BC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private unsafe void RequestPresentedNotification(DUCE.Channel channel, long estimatedFrameTime)
		{
			DUCE.MILCMD_PARTITION_NOTIFYPRESENT milcmd_PARTITION_NOTIFYPRESENT;
			milcmd_PARTITION_NOTIFYPRESENT.Type = MILCMD.MilCmdPartitionNotifyPresent;
			milcmd_PARTITION_NOTIFYPRESENT.FrameTime = (ulong)estimatedFrameTime;
			channel.SendCommand((byte*)(&milcmd_PARTITION_NOTIFYPRESENT), sizeof(DUCE.MILCMD_PARTITION_NOTIFYPRESENT), true);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0007E1EC File Offset: 0x0007D5EC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal void CompleteRender()
		{
			if (this.Channel != null)
			{
				if (this.InterlockIsEnabled)
				{
					if (this._interlockState == MediaContext.InterlockState.WaitingForResponse)
					{
						do
						{
							if (this.CommittingBatch != null)
							{
								this.CommittingBatch(this.Channel, new EventArgs());
							}
							this.Channel.WaitForNextMessage();
							this.NotifyChannelMessage();
						}
						while (this._interlockState == MediaContext.InterlockState.WaitingForResponse);
					}
					this._estimatedNextVSyncTimer.Stop();
					this._interlockState = MediaContext.InterlockState.Idle;
					if (this._needToCommitChannel)
					{
						this.CommitChannel();
						if (this._interlockState == MediaContext.InterlockState.WaitingForResponse)
						{
							do
							{
								this.Channel.WaitForNextMessage();
								this.NotifyChannelMessage();
							}
							while (this._interlockState == MediaContext.InterlockState.WaitingForResponse);
							this._estimatedNextVSyncTimer.Stop();
							this._interlockState = MediaContext.InterlockState.Idle;
							return;
						}
					}
				}
				else
				{
					if (this.CommittingBatch != null)
					{
						this.CommittingBatch(this.Channel, new EventArgs());
					}
					this.Channel.SyncFlush();
				}
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0007E2D0 File Offset: 0x0007D6D0
		private void OnNeedTickSooner(object sender, EventArgs e)
		{
			this.PostRender();
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0007E2E4 File Offset: 0x0007D6E4
		internal void VerifyWriteAccess()
		{
			if (!this.WriteAccessEnabled)
			{
				throw new InvalidOperationException(SR.Get("MediaContext_APINotAllowed"));
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0007E30C File Offset: 0x0007D70C
		internal bool WriteAccessEnabled
		{
			get
			{
				return this._readOnlyAccessCounter <= 0;
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0007E328 File Offset: 0x0007D728
		internal void PushReadOnlyAccess()
		{
			this._readOnlyAccessCounter++;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0007E344 File Offset: 0x0007D744
		internal void PopReadOnlyAccess()
		{
			this._readOnlyAccessCounter--;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0007E360 File Offset: 0x0007D760
		internal TimeManager TimeManager
		{
			get
			{
				return this._timeManager;
			}
		}

		// Token: 0x1400018D RID: 397
		// (add) Token: 0x06001F24 RID: 7972 RVA: 0x0007E374 File Offset: 0x0007D774
		// (remove) Token: 0x06001F25 RID: 7973 RVA: 0x0007E398 File Offset: 0x0007D798
		internal event EventHandler RenderComplete
		{
			add
			{
				if (this._commitPendingAfterRender)
				{
					this._commitPendingAfterRender = false;
				}
				this._renderCompleteHandlers += value;
			}
			remove
			{
				this._renderCompleteHandlers -= value;
			}
		}

		// Token: 0x1400018E RID: 398
		// (add) Token: 0x06001F26 RID: 7974 RVA: 0x0007E3AC File Offset: 0x0007D7AC
		// (remove) Token: 0x06001F27 RID: 7975 RVA: 0x0007E3C0 File Offset: 0x0007D7C0
		internal event MediaContext.ResourcesUpdatedHandler ResourcesUpdated
		{
			add
			{
				this._resourcesUpdatedHandlers += value;
			}
			remove
			{
				this._resourcesUpdatedHandlers -= value;
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0007E3D4 File Offset: 0x0007D7D4
		private void RaiseResourcesUpdated()
		{
			if (this._resourcesUpdatedHandlers != null)
			{
				DUCE.ChannelSet channels = this.GetChannels();
				this._resourcesUpdatedHandlers(channels.Channel, false);
				this._resourcesUpdatedHandlers = null;
			}
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0007E40C File Offset: 0x0007D80C
		internal DUCE.Channel AllocateSyncChannel()
		{
			return this._channelManager.AllocateSyncChannel();
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0007E424 File Offset: 0x0007D824
		internal void ReleaseSyncChannel(DUCE.Channel channel)
		{
			this._channelManager.ReleaseSyncChannel(channel);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001F2B RID: 7979 RVA: 0x0007E440 File Offset: 0x0007D840
		internal DUCE.Channel Channel
		{
			get
			{
				return this._channelManager.Channel;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x0007E458 File Offset: 0x0007D858
		internal DUCE.Channel OutOfBandChannel
		{
			get
			{
				return this._channelManager.OutOfBandChannel;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0007E470 File Offset: 0x0007D870
		internal bool IsConnected
		{
			get
			{
				return this._isConnected;
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0007E484 File Offset: 0x0007D884
		internal BoundsDrawingContextWalker AcquireBoundsDrawingContextWalker()
		{
			if (this._cachedBoundsDrawingContextWalker == null)
			{
				return new BoundsDrawingContextWalker();
			}
			BoundsDrawingContextWalker cachedBoundsDrawingContextWalker = this._cachedBoundsDrawingContextWalker;
			this._cachedBoundsDrawingContextWalker = null;
			cachedBoundsDrawingContextWalker.ClearState();
			return cachedBoundsDrawingContextWalker;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0007E4B4 File Offset: 0x0007D8B4
		internal void ReleaseBoundsDrawingContextWalker(BoundsDrawingContextWalker ctx)
		{
			this._cachedBoundsDrawingContextWalker = ctx;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0007E4C8 File Offset: 0x0007D8C8
		private void PromoteRenderOpToInput(object sender, EventArgs e)
		{
			if (this._currentRenderOp != null)
			{
				this._currentRenderOp.Priority = DispatcherPriority.Input;
			}
			((DispatcherTimer)sender).Stop();
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0007E4F4 File Offset: 0x0007D8F4
		private void PromoteRenderOpToRender(object sender, EventArgs e)
		{
			if (this._currentRenderOp != null)
			{
				this._currentRenderOp.Priority = DispatcherPriority.Render;
			}
			((DispatcherTimer)sender).Stop();
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0007E520 File Offset: 0x0007D920
		private void EstimatedNextVSyncTimeExpired(object sender, EventArgs e)
		{
			long currentTicks = MediaContext.CurrentTicks;
			DispatcherTimer dispatcherTimer = (DispatcherTimer)sender;
			long num = 0L;
			if (dispatcherTimer.Tag != null)
			{
				num = (long)dispatcherTimer.Tag;
			}
			if (num > currentTicks)
			{
				dispatcherTimer.Stop();
				dispatcherTimer.Interval = TimeSpan.FromTicks(num - currentTicks);
				dispatcherTimer.Start();
				return;
			}
			this._interlockState = MediaContext.InterlockState.Idle;
			if (this._needToCommitChannel)
			{
				this.CommitChannel();
				this.ScheduleNextRenderOp(TimeSpan.Zero);
			}
			dispatcherTimer.Stop();
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0007E598 File Offset: 0x0007D998
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private unsafe void RegisterForNotifications(DUCE.Channel channel)
		{
			DUCE.MILCMD_PARTITION_REGISTERFORNOTIFICATIONS milcmd_PARTITION_REGISTERFORNOTIFICATIONS;
			milcmd_PARTITION_REGISTERFORNOTIFICATIONS.Type = MILCMD.MilCmdPartitionRegisterForNotifications;
			milcmd_PARTITION_REGISTERFORNOTIFICATIONS.Enable = 1U;
			channel.SendCommand((byte*)(&milcmd_PARTITION_REGISTERFORNOTIFICATIONS), sizeof(DUCE.MILCMD_PARTITION_REGISTERFORNOTIFICATIONS));
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x0007E5C4 File Offset: 0x0007D9C4
		internal DUCE.ChannelSet GetChannels()
		{
			DUCE.ChannelSet result;
			result.Channel = this._channelManager.Channel;
			result.OutOfBandChannel = this._channelManager.OutOfBandChannel;
			return result;
		}

		// Token: 0x1400018F RID: 399
		// (add) Token: 0x06001F35 RID: 7989 RVA: 0x0007E5F8 File Offset: 0x0007D9F8
		// (remove) Token: 0x06001F36 RID: 7990 RVA: 0x0007E630 File Offset: 0x0007DA30
		private event EventHandler _renderCompleteHandlers;

		// Token: 0x14000190 RID: 400
		// (add) Token: 0x06001F37 RID: 7991 RVA: 0x0007E668 File Offset: 0x0007DA68
		// (remove) Token: 0x06001F38 RID: 7992 RVA: 0x0007E6A0 File Offset: 0x0007DAA0
		private event MediaContext.ResourcesUpdatedHandler _resourcesUpdatedHandlers;

		// Token: 0x14000191 RID: 401
		// (add) Token: 0x06001F39 RID: 7993 RVA: 0x0007E6D8 File Offset: 0x0007DAD8
		// (remove) Token: 0x06001F3A RID: 7994 RVA: 0x0007E710 File Offset: 0x0007DB10
		internal event EventHandler Rendering;

		// Token: 0x14000192 RID: 402
		// (add) Token: 0x06001F3B RID: 7995 RVA: 0x0007E748 File Offset: 0x0007DB48
		// (remove) Token: 0x06001F3C RID: 7996 RVA: 0x0007E780 File Offset: 0x0007DB80
		internal event EventHandler CommittingBatch;

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0007E7B8 File Offset: 0x0007DBB8
		private bool InterlockIsWaiting
		{
			get
			{
				return this._interlockState == MediaContext.InterlockState.WaitingForNextFrame || this._interlockState == MediaContext.InterlockState.WaitingForResponse;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0007E7DC File Offset: 0x0007DBDC
		private bool InterlockIsEnabled
		{
			get
			{
				return this._interlockState != MediaContext.InterlockState.Disabled && this._interlockState != MediaContext.InterlockState.RequestedStart;
			}
		}

		// Token: 0x04001021 RID: 4129
		private TimeManager _timeManager;

		// Token: 0x04001022 RID: 4130
		private bool _isDisposed;

		// Token: 0x04001023 RID: 4131
		private EventHandler _destroyHandler;

		// Token: 0x04001026 RID: 4134
		private Guid _contextGuid;

		// Token: 0x04001027 RID: 4135
		private DispatcherOperation _currentRenderOp;

		// Token: 0x04001028 RID: 4136
		private DispatcherOperation _inputMarkerOp;

		// Token: 0x04001029 RID: 4137
		private DispatcherOperationCallback _renderMessage;

		// Token: 0x0400102A RID: 4138
		private DispatcherOperationCallback _animRenderMessage;

		// Token: 0x0400102B RID: 4139
		private DispatcherOperationCallback _inputMarkerMessage;

		// Token: 0x0400102C RID: 4140
		private DispatcherOperationCallback _renderModeMessage;

		// Token: 0x0400102D RID: 4141
		private DispatcherTimer _promoteRenderOpToInput;

		// Token: 0x0400102E RID: 4142
		private DispatcherTimer _promoteRenderOpToRender;

		// Token: 0x0400102F RID: 4143
		private DispatcherTimer _estimatedNextVSyncTimer;

		// Token: 0x04001030 RID: 4144
		private MediaContext.ChannelManager _channelManager;

		// Token: 0x04001031 RID: 4145
		private DUCE.Resource _uceEtwEvent;

		// Token: 0x04001032 RID: 4146
		private bool _isRendering;

		// Token: 0x04001033 RID: 4147
		private bool _isDisconnecting;

		// Token: 0x04001034 RID: 4148
		private bool _isConnected;

		// Token: 0x04001035 RID: 4149
		private FrugalObjectList<MediaContext.InvokeOnRenderCallback> _invokeOnRenderCallbacks;

		// Token: 0x04001036 RID: 4150
		private Dictionary<ICompositionTarget, object> _registeredICompositionTargets;

		// Token: 0x04001037 RID: 4151
		private int _readOnlyAccessCounter;

		// Token: 0x04001038 RID: 4152
		private BoundsDrawingContextWalker _cachedBoundsDrawingContextWalker = new BoundsDrawingContextWalker();

		// Token: 0x04001039 RID: 4153
		private static int _contextRenderID = 0;

		// Token: 0x0400103A RID: 4154
		private int _tier;

		// Token: 0x0400103D RID: 4157
		private FrugalObjectList<LoadedOrUnloadedOperation> _loadedOrUnloadedPendingOperations;

		// Token: 0x0400103E RID: 4158
		private TimeSpan _timeDelay = TimeSpan.FromMilliseconds(10.0);

		// Token: 0x0400103F RID: 4159
		private bool _commitPendingAfterRender;

		// Token: 0x04001040 RID: 4160
		private MediaContextNotificationWindow _notificationWindow;

		// Token: 0x04001041 RID: 4161
		private DUCE.ChannelSet? _currentRenderingChannel;

		// Token: 0x04001042 RID: 4162
		private MediaContext.InterlockState _interlockState;

		// Token: 0x04001043 RID: 4163
		private bool _needToCommitChannel;

		// Token: 0x04001044 RID: 4164
		private long _lastPresentationTime;

		// Token: 0x04001045 RID: 4165
		private long _lastCommitTime;

		// Token: 0x04001046 RID: 4166
		private long _lastInputMarkerTime;

		// Token: 0x04001047 RID: 4167
		private long _averagePresentationInterval;

		// Token: 0x04001048 RID: 4168
		private TimeSpan _estimatedNextPresentationTime;

		// Token: 0x04001049 RID: 4169
		private int _displayRefreshRate;

		// Token: 0x0400104A RID: 4170
		private int _adjustedRefreshRate;

		// Token: 0x0400104B RID: 4171
		private int _animationRenderRate;

		// Token: 0x0400104C RID: 4172
		private MIL_PRESENTATION_RESULTS _lastPresentationResults = MIL_PRESENTATION_RESULTS.MIL_PRESENTATION_VSYNC_UNSUPPORTED;

		// Token: 0x0400104D RID: 4173
		private static long _perfCounterFreq;

		// Token: 0x0400104E RID: 4174
		private const long MaxTicksWithoutInput = 5000000L;

		// Token: 0x02000856 RID: 2134
		private struct ChannelManager
		{
			// Token: 0x0600570C RID: 22284 RVA: 0x00164504 File Offset: 0x00163904
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal void CreateChannels()
			{
				Invariant.Assert(this._asyncChannel == null);
				Invariant.Assert(this._asyncOutOfBandChannel == null);
				this._asyncChannel = new DUCE.Channel(MediaSystem.ServiceChannel, false, MediaSystem.Connection, false);
				this._asyncOutOfBandChannel = new DUCE.Channel(MediaSystem.ServiceChannel, true, MediaSystem.Connection, false);
			}

			// Token: 0x0600570D RID: 22285 RVA: 0x0016455C File Offset: 0x0016395C
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal void RemoveSyncChannels()
			{
				if (this._freeSyncChannels != null)
				{
					while (this._freeSyncChannels.Count > 0)
					{
						this._freeSyncChannels.Dequeue().Close();
					}
					this._freeSyncChannels = null;
				}
				if (this._syncServiceChannel != null)
				{
					this._syncServiceChannel.Close();
					this._syncServiceChannel = null;
				}
			}

			// Token: 0x0600570E RID: 22286 RVA: 0x001645B4 File Offset: 0x001639B4
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal void RemoveChannels()
			{
				if (this._asyncChannel != null)
				{
					this._asyncChannel.Close();
					this._asyncChannel = null;
				}
				if (this._asyncOutOfBandChannel != null)
				{
					this._asyncOutOfBandChannel.Close();
					this._asyncOutOfBandChannel = null;
				}
				this.RemoveSyncChannels();
				if (this._pSyncConnection != IntPtr.Zero)
				{
					HRESULT.Check(UnsafeNativeMethods.MilCoreApi.WgxConnection_Disconnect(this._pSyncConnection));
					this._pSyncConnection = IntPtr.Zero;
				}
			}

			// Token: 0x0600570F RID: 22287 RVA: 0x00164628 File Offset: 0x00163A28
			[SecurityTreatAsSafe]
			[SecurityCritical]
			internal DUCE.Channel AllocateSyncChannel()
			{
				if (this._pSyncConnection == IntPtr.Zero)
				{
					HRESULT.Check(UnsafeNativeMethods.MilCoreApi.WgxConnection_Create(true, out this._pSyncConnection));
				}
				if (this._freeSyncChannels == null)
				{
					this._freeSyncChannels = new Queue<DUCE.Channel>(3);
				}
				if (this._freeSyncChannels.Count > 0)
				{
					return this._freeSyncChannels.Dequeue();
				}
				if (this._syncServiceChannel == null)
				{
					this._syncServiceChannel = new DUCE.Channel(null, false, this._pSyncConnection, true);
				}
				return new DUCE.Channel(this._syncServiceChannel, false, this._pSyncConnection, true);
			}

			// Token: 0x06005710 RID: 22288 RVA: 0x001646B8 File Offset: 0x00163AB8
			[SecurityCritical]
			[SecurityTreatAsSafe]
			internal void ReleaseSyncChannel(DUCE.Channel channel)
			{
				Invariant.Assert(this._freeSyncChannels != null);
				if (this._freeSyncChannels.Count <= 3)
				{
					this._freeSyncChannels.Enqueue(channel);
					return;
				}
				channel.Close();
			}

			// Token: 0x170011EB RID: 4587
			// (get) Token: 0x06005711 RID: 22289 RVA: 0x001646F4 File Offset: 0x00163AF4
			internal DUCE.Channel Channel
			{
				[SecurityCritical]
				[SecurityTreatAsSafe]
				get
				{
					return this._asyncChannel;
				}
			}

			// Token: 0x170011EC RID: 4588
			// (get) Token: 0x06005712 RID: 22290 RVA: 0x00164708 File Offset: 0x00163B08
			internal DUCE.Channel OutOfBandChannel
			{
				[SecurityTreatAsSafe]
				[SecurityCritical]
				get
				{
					return this._asyncOutOfBandChannel;
				}
			}

			// Token: 0x0400281D RID: 10269
			[SecurityCritical]
			private DUCE.Channel _asyncChannel;

			// Token: 0x0400281E RID: 10270
			[SecurityCritical]
			private DUCE.Channel _asyncOutOfBandChannel;

			// Token: 0x0400281F RID: 10271
			[SecurityCritical]
			private Queue<DUCE.Channel> _freeSyncChannels;

			// Token: 0x04002820 RID: 10272
			[SecurityCritical]
			private DUCE.Channel _syncServiceChannel;

			// Token: 0x04002821 RID: 10273
			[SecurityCritical]
			private IntPtr _pSyncConnection;
		}

		// Token: 0x02000857 RID: 2135
		private class InvokeOnRenderCallback
		{
			// Token: 0x06005713 RID: 22291 RVA: 0x0016471C File Offset: 0x00163B1C
			public InvokeOnRenderCallback(DispatcherOperationCallback callback, object arg)
			{
				this._callback = callback;
				this._arg = arg;
			}

			// Token: 0x06005714 RID: 22292 RVA: 0x00164740 File Offset: 0x00163B40
			public void DoWork()
			{
				this._callback(this._arg);
			}

			// Token: 0x04002822 RID: 10274
			private DispatcherOperationCallback _callback;

			// Token: 0x04002823 RID: 10275
			private object _arg;
		}

		// Token: 0x02000858 RID: 2136
		// (Invoke) Token: 0x06005716 RID: 22294
		internal delegate void ResourcesUpdatedHandler(DUCE.Channel channel, bool skipOnChannelCheck);

		// Token: 0x02000859 RID: 2137
		private enum InterlockState
		{
			// Token: 0x04002825 RID: 10277
			Disabled,
			// Token: 0x04002826 RID: 10278
			RequestedStart,
			// Token: 0x04002827 RID: 10279
			Idle,
			// Token: 0x04002828 RID: 10280
			WaitingForResponse,
			// Token: 0x04002829 RID: 10281
			WaitingForNextFrame
		}
	}
}
