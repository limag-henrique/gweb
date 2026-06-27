using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Threading;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000581 RID: 1409
	internal sealed class TimeManager : DispatcherObject
	{
		// Token: 0x06004132 RID: 16690 RVA: 0x001006EC File Offset: 0x000FFAEC
		public TimeManager() : this(null)
		{
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x00100700 File Offset: 0x000FFB00
		public TimeManager(IClock clock)
		{
			this._eventQueue = new Queue<WeakReference>();
			this.Clock = clock;
			this._timeState = TimeState.Stopped;
			this._lastTimeState = TimeState.Stopped;
			this._globalTime = new TimeSpan(-1L);
			this._lastTickTime = new TimeSpan(-1L);
			this._nextTickTimeQueried = false;
			this._isInTick = false;
			ParallelTimeline parallelTimeline = new ParallelTimeline(new TimeSpan?(new TimeSpan(0L)), Duration.Forever);
			parallelTimeline.Freeze();
			this._timeManagerClock = new ClockGroup(parallelTimeline);
			this._timeManagerClock.MakeRoot(this);
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x00100790 File Offset: 0x000FFB90
		// (set) Token: 0x06004135 RID: 16693 RVA: 0x001007A4 File Offset: 0x000FFBA4
		public IClock Clock
		{
			get
			{
				return this._systemClock;
			}
			set
			{
				if (value != null)
				{
					this._systemClock = value;
					return;
				}
				if (MediaContext.IsClockSupported)
				{
					this._systemClock = MediaContext.From(base.Dispatcher);
					return;
				}
				this._systemClock = new TimeManager.GTCClock();
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x001007E0 File Offset: 0x000FFBE0
		public TimeSpan? CurrentTime
		{
			get
			{
				if (this._timeState == TimeState.Stopped)
				{
					return null;
				}
				return new TimeSpan?(this._globalTime);
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x0010080C File Offset: 0x000FFC0C
		public bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x00100820 File Offset: 0x000FFC20
		public void Pause()
		{
			if (this._timeState == TimeState.Running)
			{
				this._pauseTime = this._systemClock.CurrentTime;
				this._timeState = TimeState.Paused;
			}
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x00100850 File Offset: 0x000FFC50
		public void Restart()
		{
			TimeState timeState = this._timeState;
			this.Stop();
			this.Start();
			this._timeState = timeState;
			if (this._timeState == TimeState.Paused)
			{
				this._pauseTime = this._startTime;
			}
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x0010088C File Offset: 0x000FFC8C
		public void Resume()
		{
			if (this._timeState == TimeState.Paused)
			{
				this._startTime = this._startTime + this._systemClock.CurrentTime - this._pauseTime;
				this._timeState = TimeState.Running;
				if (this.GetNextTickNeeded() >= TimeSpan.Zero)
				{
					this.NotifyNewEarliestFutureActivity();
				}
			}
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x001008E8 File Offset: 0x000FFCE8
		public void Seek(int offset, TimeSeekOrigin origin)
		{
			if (this._timeState >= TimeState.Paused)
			{
				if (origin != TimeSeekOrigin.BeginTime)
				{
					if (origin == TimeSeekOrigin.Duration)
					{
						return;
					}
					throw new InvalidEnumArgumentException(SR.Get("Enum_Invalid", new object[]
					{
						"TimeSeekOrigin"
					}));
				}
				else
				{
					if (offset < 0)
					{
						offset = 0;
					}
					TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)offset);
					if (timeSpan != this._globalTime)
					{
						this._globalTime = timeSpan;
						this._startTime = this._systemClock.CurrentTime - this._globalTime;
						this._timeManagerClock.ComputeTreeState();
					}
				}
			}
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x00100970 File Offset: 0x000FFD70
		public void Start()
		{
			if (this._timeState == TimeState.Stopped)
			{
				this._lastTickTime = TimeSpan.Zero;
				this._startTime = this._systemClock.CurrentTime;
				this._globalTime = TimeSpan.Zero;
				this._timeState = TimeState.Running;
				this._timeManagerClock.RootActivate();
			}
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x001009C0 File Offset: 0x000FFDC0
		public void Stop()
		{
			if (this._timeState >= TimeState.Paused)
			{
				this._timeManagerClock.RootDisable();
				this._timeState = TimeState.Stopped;
			}
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x001009E8 File Offset: 0x000FFDE8
		public void Tick()
		{
			try
			{
				this._nextTickTimeQueried = false;
				this._isDirty = false;
				if (this._timeState == TimeState.Running)
				{
					this._globalTime = this.GetCurrentGlobalTime();
					this._isInTick = true;
				}
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordAnimation, EventTrace.Event.WClientTimeManagerTickBegin, (this._startTime + this._globalTime).Ticks / 10000L);
				if (this._lastTimeState == TimeState.Stopped && this._timeState == TimeState.Stopped)
				{
					this._currentTickInterval = TimeIntervalCollection.CreateNullPoint();
				}
				else
				{
					this._currentTickInterval = TimeIntervalCollection.CreateOpenClosedInterval(this._lastTickTime, this._globalTime);
					if (this._lastTimeState == TimeState.Stopped || this._timeState == TimeState.Stopped)
					{
						this._currentTickInterval.AddNullPoint();
					}
				}
				this._timeManagerClock.ComputeTreeState();
				this._lastTimeState = this._timeState;
				this.RaiseEnqueuedEvents();
			}
			finally
			{
				this._isInTick = false;
				this._lastTickTime = this._globalTime;
				EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordAnimation, EventTrace.Event.WClientTimeManagerTickEnd);
			}
			this.CleanupClocks();
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x00100B08 File Offset: 0x000FFF08
		internal int GetMaxDesiredFrameRate()
		{
			return this._timeManagerClock.GetMaxDesiredFrameRate();
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x00100B20 File Offset: 0x000FFF20
		private void CleanupClocks()
		{
			if (this._needClockCleanup)
			{
				this._needClockCleanup = false;
				this._timeManagerClock.RootCleanChildren();
			}
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x00100B48 File Offset: 0x000FFF48
		internal void AddToEventQueue(Clock sender)
		{
			this._eventQueue.Enqueue(sender.WeakReference);
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x00100B68 File Offset: 0x000FFF68
		internal TimeSpan GetCurrentGlobalTime()
		{
			switch (this._timeState)
			{
			case TimeState.Stopped:
				return TimeSpan.Zero;
			case TimeState.Paused:
				return this._pauseTime - this._startTime;
			case TimeState.Running:
				if (this._isInTick || this._lockTickTime)
				{
					return this._globalTime;
				}
				return this._systemClock.CurrentTime - this._startTime;
			default:
				return TimeSpan.Zero;
			}
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x00100BDC File Offset: 0x000FFFDC
		internal void LockTickTime()
		{
			this._lockTickTime = true;
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x00100BF0 File Offset: 0x000FFFF0
		internal void NotifyNewEarliestFutureActivity()
		{
			if (this._nextTickTimeQueried && this._userNeedTickSooner != null)
			{
				this._nextTickTimeQueried = false;
				this._userNeedTickSooner(this, EventArgs.Empty);
			}
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x00100C28 File Offset: 0x00100028
		private void RaiseEnqueuedEvents()
		{
			while (this._eventQueue.Count > 0)
			{
				WeakReference weakReference = this._eventQueue.Dequeue();
				Clock clock = (Clock)weakReference.Target;
				if (clock != null)
				{
					clock.RaiseAccumulatedEvents();
				}
			}
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x00100C68 File Offset: 0x00100068
		internal void ScheduleClockCleanup()
		{
			this._needClockCleanup = true;
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x00100C7C File Offset: 0x0010007C
		internal void SetDirty()
		{
			this._isDirty = true;
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x00100C90 File Offset: 0x00100090
		internal void UnlockTickTime()
		{
			this._lockTickTime = false;
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x00100CA4 File Offset: 0x001000A4
		internal TimeSpan InternalCurrentGlobalTime
		{
			get
			{
				return this._globalTime;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x0600414A RID: 16714 RVA: 0x00100CB8 File Offset: 0x001000B8
		internal bool InternalIsStopped
		{
			get
			{
				return this._timeState == TimeState.Stopped;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x00100CD0 File Offset: 0x001000D0
		// (set) Token: 0x0600414C RID: 16716 RVA: 0x00100CE4 File Offset: 0x001000E4
		internal TimeIntervalCollection InternalCurrentIntervals
		{
			get
			{
				return this._currentTickInterval;
			}
			set
			{
				this._currentTickInterval = value;
			}
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x00100CF8 File Offset: 0x001000F8
		internal TimeSpan GetNextTickNeeded()
		{
			this._nextTickTimeQueried = true;
			if (this._timeState != TimeState.Running)
			{
				return TimeSpan.FromTicks(-1L);
			}
			TimeSpan? internalNextTickNeededTime = this._timeManagerClock.InternalNextTickNeededTime;
			if (internalNextTickNeededTime == null)
			{
				return TimeSpan.FromTicks(-1L);
			}
			TimeSpan t = this._systemClock.CurrentTime - this._startTime;
			TimeSpan timeSpan = internalNextTickNeededTime.Value - t;
			if (timeSpan <= TimeSpan.Zero)
			{
				return TimeSpan.Zero;
			}
			return timeSpan;
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x00100D74 File Offset: 0x00100174
		internal TimeSpan LastTickDelta
		{
			get
			{
				return this._globalTime - this._lastTickTime;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x00100D94 File Offset: 0x00100194
		internal TimeSpan LastTickTime
		{
			get
			{
				return this._lastTickTime;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x00100DA8 File Offset: 0x001001A8
		internal ClockGroup TimeManagerClock
		{
			get
			{
				return this._timeManagerClock;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x00100DBC File Offset: 0x001001BC
		internal TimeState State
		{
			get
			{
				return this._timeState;
			}
		}

		// Token: 0x140001CA RID: 458
		// (add) Token: 0x06004152 RID: 16722 RVA: 0x00100DD0 File Offset: 0x001001D0
		// (remove) Token: 0x06004153 RID: 16723 RVA: 0x00100DF4 File Offset: 0x001001F4
		internal event EventHandler NeedTickSooner
		{
			add
			{
				this._userNeedTickSooner = (EventHandler)Delegate.Combine(this._userNeedTickSooner, value);
			}
			remove
			{
				this._userNeedTickSooner = (EventHandler)Delegate.Remove(this._userNeedTickSooner, value);
			}
		}

		// Token: 0x040017D6 RID: 6102
		private TimeState _timeState;

		// Token: 0x040017D7 RID: 6103
		private TimeState _lastTimeState;

		// Token: 0x040017D8 RID: 6104
		private IClock _systemClock;

		// Token: 0x040017D9 RID: 6105
		private TimeSpan _globalTime;

		// Token: 0x040017DA RID: 6106
		private TimeSpan _startTime;

		// Token: 0x040017DB RID: 6107
		private TimeSpan _lastTickTime;

		// Token: 0x040017DC RID: 6108
		private TimeSpan _pauseTime;

		// Token: 0x040017DD RID: 6109
		private TimeIntervalCollection _currentTickInterval;

		// Token: 0x040017DE RID: 6110
		private bool _nextTickTimeQueried;

		// Token: 0x040017DF RID: 6111
		private bool _isDirty;

		// Token: 0x040017E0 RID: 6112
		private bool _isInTick;

		// Token: 0x040017E1 RID: 6113
		private bool _lockTickTime;

		// Token: 0x040017E2 RID: 6114
		private EventHandler _userNeedTickSooner;

		// Token: 0x040017E3 RID: 6115
		private ClockGroup _timeManagerClock;

		// Token: 0x040017E4 RID: 6116
		private Queue<WeakReference> _eventQueue;

		// Token: 0x040017E5 RID: 6117
		private bool _needClockCleanup;

		// Token: 0x020008CD RID: 2253
		internal class GTCClock : IClock
		{
			// Token: 0x060058C8 RID: 22728 RVA: 0x001688E0 File Offset: 0x00167CE0
			internal GTCClock()
			{
			}

			// Token: 0x1700125A RID: 4698
			// (get) Token: 0x060058C9 RID: 22729 RVA: 0x001688F4 File Offset: 0x00167CF4
			TimeSpan IClock.CurrentTime
			{
				get
				{
					return TimeSpan.FromTicks(DateTime.Now.Ticks);
				}
			}
		}

		// Token: 0x020008CE RID: 2254
		internal class TestTimingClock : IClock
		{
			// Token: 0x1700125B RID: 4699
			// (get) Token: 0x060058CA RID: 22730 RVA: 0x00168914 File Offset: 0x00167D14
			// (set) Token: 0x060058CB RID: 22731 RVA: 0x00168928 File Offset: 0x00167D28
			public TimeSpan CurrentTime
			{
				get
				{
					return this._currentTime;
				}
				set
				{
					this._currentTime = value;
				}
			}

			// Token: 0x04002977 RID: 10615
			private TimeSpan _currentTime;
		}
	}
}
