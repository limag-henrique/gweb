using System;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Um assemblage de tipos <see cref="T:System.Windows.Media.Animation.Clock" /> com comportamento baseado fora de um <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</summary>
	// Token: 0x020004A9 RID: 1193
	public class ClockGroup : Clock
	{
		/// <summary>Cria uma nova instância da classe <see cref="T:System.Windows.Media.Animation.ClockGroup" />.</summary>
		/// <param name="timelineGroup">O objeto que define as características da nova classe.</param>
		// Token: 0x06003581 RID: 13697 RVA: 0x000D53A4 File Offset: 0x000D47A4
		protected internal ClockGroup(TimelineGroup timelineGroup) : base(timelineGroup)
		{
		}

		/// <summary>Obtém o objeto <see cref="T:System.Windows.Media.Animation.TimelineGroup" /> que determina o comportamento desta instância de <see cref="T:System.Windows.Media.Animation.ClockGroup" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.Animation.TimelineGroup" />.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x000D53B8 File Offset: 0x000D47B8
		public new TimelineGroup Timeline
		{
			get
			{
				return (TimelineGroup)base.Timeline;
			}
		}

		/// <summary>Obtém a coleção filho deste <see cref="T:System.Windows.Media.Animation.ClockGroup" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Animation.ClockCollection" /> que representa os filhos deste <see cref="T:System.Windows.Media.Animation.ClockGroup" />.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000D53D0 File Offset: 0x000D47D0
		public ClockCollection Children
		{
			get
			{
				return new ClockCollection(this);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000D53E4 File Offset: 0x000D47E4
		internal List<Clock> InternalChildren
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000D53F8 File Offset: 0x000D47F8
		internal List<WeakReference> InternalRootChildren
		{
			get
			{
				return this._rootChildren;
			}
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000D540C File Offset: 0x000D480C
		internal override void BuildClockSubTreeFromTimeline(Timeline timeline, bool hasControllableRoot)
		{
			TimelineGroup timelineGroup = timeline as TimelineGroup;
			TimelineCollection children = timelineGroup.Children;
			if (children != null && children.Count > 0)
			{
				this._children = new List<Clock>();
				for (int i = 0; i < children.Count; i++)
				{
					Clock clock = Clock.AllocateClock(children[i], hasControllableRoot);
					clock._parent = this;
					clock.BuildClockSubTreeFromTimeline(children[i], hasControllableRoot);
					this._children.Add(clock);
					clock._childIndex = i;
				}
				if (this._timeline is ParallelTimeline && ((ParallelTimeline)this._timeline).SlipBehavior == SlipBehavior.Slip)
				{
					if (!base.IsRoot || this._timeline.RepeatBehavior.HasDuration || this._timeline.AutoReverse || this._timeline.AccelerationRatio > 0.0 || this._timeline.DecelerationRatio > 0.0)
					{
						throw new NotSupportedException(SR.Get("Timing_SlipBehavior_SlipOnlyOnSimpleTimelines"));
					}
					int j = 0;
					while (j < this._children.Count)
					{
						Clock clock2 = this._children[j];
						if (clock2.CanSlip)
						{
							Duration resolvedDuration = clock2.ResolvedDuration;
							if ((!resolvedDuration.HasTimeSpan || resolvedDuration.TimeSpan > TimeSpan.Zero) && clock2._timeline.BeginTime != null)
							{
								this._syncData = new Clock.SyncData(clock2);
								clock2._syncData = null;
								return;
							}
							break;
						}
						else
						{
							j++;
						}
					}
				}
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000D5598 File Offset: 0x000D4998
		internal override Clock FirstChild
		{
			get
			{
				Clock result = null;
				List<Clock> children = this._children;
				if (children != null)
				{
					result = children[0];
				}
				return result;
			}
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000D55BC File Offset: 0x000D49BC
		internal int GetMaxDesiredFrameRate()
		{
			int num = 0;
			WeakRefEnumerator<Clock> weakRefEnumerator = new WeakRefEnumerator<Clock>(this._rootChildren);
			while (weakRefEnumerator.MoveNext())
			{
				Clock clock = weakRefEnumerator.Current;
				if (clock != null && clock.CurrentState == ClockState.Active)
				{
					int? desiredFrameRate = clock.DesiredFrameRate;
					if (desiredFrameRate != null)
					{
						num = Math.Max(num, desiredFrameRate.Value);
					}
				}
			}
			return num;
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000D5618 File Offset: 0x000D4A18
		internal void ComputeTreeState()
		{
			WeakRefEnumerator<Clock> weakRefEnumerator = new WeakRefEnumerator<Clock>(this._rootChildren);
			while (weakRefEnumerator.MoveNext())
			{
				Clock root = weakRefEnumerator.Current;
				PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(root, true);
				while (prefixSubtreeEnumerator.MoveNext())
				{
					Clock clock = prefixSubtreeEnumerator.Current;
					if (base.CurrentGlobalTime >= clock.InternalNextTickNeededTime)
					{
						clock.ApplyDesiredFrameRateToGlobalTime();
						clock.ComputeLocalState();
						clock.ClipNextTickByParent();
						clock.NeedsPostfixTraversal = (clock is ClockGroup || clock.IsRoot);
					}
					else
					{
						prefixSubtreeEnumerator.SkipSubtree();
					}
				}
			}
			this.ComputeTreeStateRoot();
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000D56C8 File Offset: 0x000D4AC8
		internal void ComputeTreeStateRoot()
		{
			TimeSpan? internalNextTickNeededTime = base.InternalNextTickNeededTime;
			base.InternalNextTickNeededTime = null;
			WeakRefEnumerator<Clock> weakRefEnumerator = new WeakRefEnumerator<Clock>(this._rootChildren);
			while (weakRefEnumerator.MoveNext())
			{
				Clock clock = weakRefEnumerator.Current;
				if (clock.NeedsPostfixTraversal)
				{
					if (clock is ClockGroup)
					{
						((ClockGroup)clock).ComputeTreeStatePostfix();
					}
					clock.ApplyDesiredFrameRateToNextTick();
					clock.NeedsPostfixTraversal = false;
				}
				if (base.InternalNextTickNeededTime == null || (weakRefEnumerator.Current.InternalNextTickNeededTime != null && weakRefEnumerator.Current.InternalNextTickNeededTime < base.InternalNextTickNeededTime))
				{
					base.InternalNextTickNeededTime = weakRefEnumerator.Current.InternalNextTickNeededTime;
				}
			}
			if (base.InternalNextTickNeededTime != null && (internalNextTickNeededTime == null || internalNextTickNeededTime > base.InternalNextTickNeededTime))
			{
				this._timeManager.NotifyNewEarliestFutureActivity();
			}
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000D5804 File Offset: 0x000D4C04
		private void ComputeTreeStatePostfix()
		{
			if (this._children != null)
			{
				for (int i = 0; i < this._children.Count; i++)
				{
					if (this._children[i].NeedsPostfixTraversal)
					{
						ClockGroup clockGroup = this._children[i] as ClockGroup;
						clockGroup.ComputeTreeStatePostfix();
					}
				}
				this.ClipNextTickByChildren();
			}
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000D5860 File Offset: 0x000D4C60
		private void ClipNextTickByChildren()
		{
			for (int i = 0; i < this._children.Count; i++)
			{
				if (base.InternalNextTickNeededTime == null || (this._children[i].InternalNextTickNeededTime != null && this._children[i].InternalNextTickNeededTime < base.InternalNextTickNeededTime))
				{
					base.InternalNextTickNeededTime = this._children[i].InternalNextTickNeededTime;
				}
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000D5910 File Offset: 0x000D4D10
		internal override Duration CurrentDuration
		{
			get
			{
				Duration duration = this._timeline.Duration;
				if (duration != Duration.Automatic)
				{
					return duration;
				}
				Duration duration2 = TimeSpan.Zero;
				if (this._children != null)
				{
					bool flag = false;
					bool flag2 = this._syncData != null && this._syncData.IsInSyncPeriod && !this._syncData.SyncClockHasReachedEffectiveDuration;
					for (int i = 0; i < this._children.Count; i++)
					{
						Clock clock = this._children[i];
						Duration duration3 = clock.EndOfActivePeriod;
						if (duration3 == Duration.Forever)
						{
							return Duration.Forever;
						}
						if (duration3 == Duration.Automatic)
						{
							flag = true;
						}
						else
						{
							if (flag2 && this._syncData.SyncClock == this)
							{
								duration3 += TimeSpan.FromMilliseconds(50.0);
								flag2 = false;
							}
							if (duration3 > duration2)
							{
								duration2 = duration3;
							}
						}
					}
					if (flag)
					{
						return Duration.Automatic;
					}
				}
				return duration2;
			}
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x000D5A18 File Offset: 0x000D4E18
		internal void MakeRoot(TimeManager timeManager)
		{
			base.IsTimeManager = true;
			this._rootChildren = new List<WeakReference>();
			this._timeManager = timeManager;
			this._depth = 0;
			base.InternalCurrentIteration = new int?(1);
			base.InternalCurrentProgress = new double?(0.0);
			base.InternalCurrentGlobalSpeed = new double?((double)1);
			base.InternalCurrentClockState = ClockState.Active;
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000D5A7C File Offset: 0x000D4E7C
		internal override void ResetNodesWithSlip()
		{
			if (this._children != null)
			{
				for (int i = 0; i < this._children.Count; i++)
				{
					Clock clock = this._children[i];
					if (clock._syncData != null)
					{
						clock._beginTime = clock._timeline.BeginTime;
						clock._syncData.IsInSyncPeriod = false;
						clock._syncData.UpdateClockBeginTime();
					}
				}
			}
			base.ResetNodesWithSlip();
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x000D5AEC File Offset: 0x000D4EEC
		internal void RootActivate()
		{
			TimeIntervalCollection internalCurrentIntervals = TimeIntervalCollection.CreatePoint(this._timeManager.InternalCurrentGlobalTime);
			internalCurrentIntervals.AddNullPoint();
			this._timeManager.InternalCurrentIntervals = internalCurrentIntervals;
			this.ComputeTreeState();
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000D5B24 File Offset: 0x000D4F24
		internal void RootCleanChildren()
		{
			WeakRefEnumerator<Clock> weakRefEnumerator = new WeakRefEnumerator<Clock>(this._rootChildren);
			while (weakRefEnumerator.MoveNext())
			{
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000D5B48 File Offset: 0x000D4F48
		internal bool RootHasChildren
		{
			get
			{
				return this._rootChildren.Count > 0;
			}
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000D5B64 File Offset: 0x000D4F64
		internal void RootDisable()
		{
			WeakRefEnumerator<Clock> weakRefEnumerator = new WeakRefEnumerator<Clock>(this._rootChildren);
			while (weakRefEnumerator.MoveNext())
			{
				Clock root = weakRefEnumerator.Current;
				PrefixSubtreeEnumerator prefixSubtreeEnumerator = new PrefixSubtreeEnumerator(root, true);
				while (prefixSubtreeEnumerator.MoveNext())
				{
					if (prefixSubtreeEnumerator.Current.InternalCurrentClockState != ClockState.Stopped)
					{
						prefixSubtreeEnumerator.Current.ResetCachedStateToStopped();
						prefixSubtreeEnumerator.Current.RaiseCurrentStateInvalidated();
						prefixSubtreeEnumerator.Current.RaiseCurrentTimeInvalidated();
						prefixSubtreeEnumerator.Current.RaiseCurrentGlobalSpeedInvalidated();
					}
					else
					{
						prefixSubtreeEnumerator.SkipSubtree();
					}
				}
			}
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000D5BEC File Offset: 0x000D4FEC
		internal override void UpdateDescendantsWithUnresolvedDuration()
		{
			if (!base.HasDescendantsWithUnresolvedDuration || !base.HasResolvedDuration)
			{
				return;
			}
			if (this._children != null)
			{
				for (int i = 0; i < this._children.Count; i++)
				{
					this._children[i].UpdateDescendantsWithUnresolvedDuration();
					if (this._children[i].HasDescendantsWithUnresolvedDuration)
					{
						return;
					}
				}
			}
			base.HasDescendantsWithUnresolvedDuration = false;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000D5C54 File Offset: 0x000D5054
		internal override void ClearCurrentIntervalsToNull()
		{
			this._currentIntervals.Clear();
			this._currentIntervals.AddNullPoint();
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000D5C78 File Offset: 0x000D5078
		internal override void AddNullPointToCurrentIntervals()
		{
			this._currentIntervals.AddNullPoint();
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000D5C90 File Offset: 0x000D5090
		internal override void ComputeCurrentIntervals(TimeIntervalCollection parentIntervalCollection, TimeSpan beginTime, TimeSpan? endTime, Duration fillDuration, Duration period, double appliedSpeedRatio, double accelRatio, double decelRatio, bool isAutoReversed)
		{
			this._currentIntervals.Clear();
			parentIntervalCollection.ProjectOntoPeriodicFunction(ref this._currentIntervals, beginTime, endTime, fillDuration, period, appliedSpeedRatio, accelRatio, decelRatio, isAutoReversed);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000D5CC4 File Offset: 0x000D50C4
		internal override void ComputeCurrentFillInterval(TimeIntervalCollection parentIntervalCollection, TimeSpan beginTime, TimeSpan endTime, Duration period, double appliedSpeedRatio, double accelRatio, double decelRatio, bool isAutoReversed)
		{
			this._currentIntervals.Clear();
			parentIntervalCollection.ProjectPostFillZone(ref this._currentIntervals, beginTime, endTime, period, appliedSpeedRatio, accelRatio, decelRatio, isAutoReversed);
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000D5CF8 File Offset: 0x000D50F8
		internal TimeIntervalCollection CurrentIntervals
		{
			get
			{
				return this._currentIntervals;
			}
		}

		// Token: 0x04001638 RID: 5688
		private List<Clock> _children;

		// Token: 0x04001639 RID: 5689
		private List<WeakReference> _rootChildren;

		// Token: 0x0400163A RID: 5690
		private TimeIntervalCollection _currentIntervals;
	}
}
