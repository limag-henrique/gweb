using System;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200057F RID: 1407
	internal struct TimeIntervalCollection
	{
		// Token: 0x060040EB RID: 16619 RVA: 0x000FEA30 File Offset: 0x000FDE30
		private TimeIntervalCollection(bool containsNullPoint)
		{
			this._containsNullPoint = containsNullPoint;
			this._count = 0;
			this._current = 0;
			this._invertCollection = false;
			this._nodeTime = null;
			this._nodeIsPoint = null;
			this._nodeIsInterval = null;
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000FEA70 File Offset: 0x000FDE70
		private TimeIntervalCollection(TimeSpan point)
		{
			this = new TimeIntervalCollection(false);
			this.InitializePoint(point);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x000FEA8C File Offset: 0x000FDE8C
		private void InitializePoint(TimeSpan point)
		{
			this.EnsureAllocatedCapacity(4);
			this._nodeTime[0] = point;
			this._nodeIsPoint[0] = true;
			this._nodeIsInterval[0] = false;
			this._count = 1;
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x000FEAC8 File Offset: 0x000FDEC8
		private TimeIntervalCollection(TimeSpan point, bool includePoint)
		{
			this = new TimeIntervalCollection(false);
			this.InitializePoint(point);
			this._nodeIsPoint[0] = includePoint;
			this._nodeIsInterval[0] = true;
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x000FEAF8 File Offset: 0x000FDEF8
		private TimeIntervalCollection(TimeSpan from, bool includeFrom, TimeSpan to, bool includeTo)
		{
			this = new TimeIntervalCollection(false);
			this.EnsureAllocatedCapacity(4);
			this._nodeTime[0] = from;
			if (from == to)
			{
				if (includeFrom || includeTo)
				{
					this._nodeIsPoint[0] = true;
					this._count = 1;
					return;
				}
			}
			else
			{
				if (from < to)
				{
					this._nodeIsPoint[0] = includeFrom;
					this._nodeIsInterval[0] = true;
					this._nodeTime[1] = to;
					this._nodeIsPoint[1] = includeTo;
				}
				else
				{
					this._nodeTime[0] = to;
					this._nodeIsPoint[0] = includeTo;
					this._nodeIsInterval[0] = true;
					this._nodeTime[1] = from;
					this._nodeIsPoint[1] = includeFrom;
				}
				this._count = 2;
			}
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000FEBB4 File Offset: 0x000FDFB4
		internal void Clear()
		{
			if (this._nodeTime != null && this._nodeTime.Length > 4)
			{
				this._nodeTime = null;
				this._nodeIsPoint = null;
				this._nodeIsInterval = null;
			}
			this._containsNullPoint = false;
			this._count = 0;
			this._current = 0;
			this._invertCollection = false;
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x000FEC08 File Offset: 0x000FE008
		internal bool IsSingleInterval
		{
			get
			{
				return this._count < 2 || (this._count == 2 && this._nodeIsInterval[0]);
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000FEC34 File Offset: 0x000FE034
		internal TimeSpan FirstNodeTime
		{
			get
			{
				return this._nodeTime[0];
			}
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x000FEC50 File Offset: 0x000FE050
		internal TimeIntervalCollection SlipBeginningOfConnectedInterval(TimeSpan slipTime)
		{
			if (slipTime == TimeSpan.Zero)
			{
				return this;
			}
			TimeIntervalCollection empty;
			if (this._count < 2 || slipTime > this._nodeTime[1] - this._nodeTime[0])
			{
				empty = TimeIntervalCollection.Empty;
			}
			else
			{
				empty = new TimeIntervalCollection(this._nodeTime[0] + slipTime, this._nodeIsPoint[0], this._nodeTime[1], this._nodeIsPoint[1]);
			}
			if (this.ContainsNullPoint)
			{
				empty.AddNullPoint();
			}
			return empty;
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000FECEC File Offset: 0x000FE0EC
		internal TimeIntervalCollection SetBeginningOfConnectedInterval(TimeSpan beginTime)
		{
			if (this._count == 1)
			{
				return new TimeIntervalCollection(this._nodeTime[0], this._nodeIsPoint[0], beginTime, true);
			}
			return new TimeIntervalCollection(beginTime, false, this._nodeTime[1], this._nodeIsPoint[1]);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000FED3C File Offset: 0x000FE13C
		internal static TimeIntervalCollection CreatePoint(TimeSpan time)
		{
			return new TimeIntervalCollection(time);
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000FED50 File Offset: 0x000FE150
		internal static TimeIntervalCollection CreateClosedOpenInterval(TimeSpan from, TimeSpan to)
		{
			return new TimeIntervalCollection(from, true, to, false);
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x000FED68 File Offset: 0x000FE168
		internal static TimeIntervalCollection CreateOpenClosedInterval(TimeSpan from, TimeSpan to)
		{
			return new TimeIntervalCollection(from, false, to, true);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x000FED80 File Offset: 0x000FE180
		internal static TimeIntervalCollection CreateInfiniteClosedInterval(TimeSpan from)
		{
			return new TimeIntervalCollection(from, true);
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060040F9 RID: 16633 RVA: 0x000FED94 File Offset: 0x000FE194
		internal static TimeIntervalCollection Empty
		{
			get
			{
				return default(TimeIntervalCollection);
			}
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000FEDAC File Offset: 0x000FE1AC
		internal static TimeIntervalCollection CreateNullPoint()
		{
			return new TimeIntervalCollection(true);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000FEDC0 File Offset: 0x000FE1C0
		internal void AddNullPoint()
		{
			this._containsNullPoint = true;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000FEDD4 File Offset: 0x000FE1D4
		internal bool Contains(TimeSpan time)
		{
			int num = this.Locate(time);
			if (num < 0)
			{
				return false;
			}
			if (this._nodeTime[num] == time)
			{
				return this._nodeIsPoint[num];
			}
			return this._nodeIsInterval[num];
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000FEE14 File Offset: 0x000FE214
		internal bool Intersects(TimeSpan from, TimeSpan to)
		{
			if (from == to)
			{
				return false;
			}
			if (from > to)
			{
				TimeSpan timeSpan = from;
				from = to;
				to = timeSpan;
			}
			int num = this.Locate(from);
			int num2 = this.Locate(to);
			if (num == num2)
			{
				return num2 >= 0 && this._nodeIsInterval[num2];
			}
			return num + 1 != num2 || to > this._nodeTime[num2] || (num >= 0 && this._nodeIsInterval[num]);
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000FEE8C File Offset: 0x000FE28C
		internal bool Intersects(TimeIntervalCollection other)
		{
			return (this.ContainsNullPoint && other.ContainsNullPoint) || (!this.IsEmptyOfRealPoints && !other.IsEmptyOfRealPoints && this.IntersectsHelper(other));
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000FEEC8 File Offset: 0x000FE2C8
		private bool IntersectsHelper(TimeIntervalCollection other)
		{
			TimeIntervalCollection.IntersectsHelperPrepareIndexers(ref this, ref other);
			bool result = false;
			while (!(this.CurrentNodeTime < other.CurrentNodeTime) || !TimeIntervalCollection.IntersectsHelperUnequalCase(ref this, ref other, ref result))
			{
				if (this.CurrentNodeTime > other.CurrentNodeTime && TimeIntervalCollection.IntersectsHelperUnequalCase(ref other, ref this, ref result))
				{
					return result;
				}
				while (this.CurrentNodeTime == other.CurrentNodeTime)
				{
					if (TimeIntervalCollection.IntersectsHelperEqualCase(ref this, ref other, ref result))
					{
						return result;
					}
				}
			}
			return result;
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x000FEF48 File Offset: 0x000FE348
		private static void IntersectsHelperPrepareIndexers(ref TimeIntervalCollection tic1, ref TimeIntervalCollection tic2)
		{
			tic1.MoveFirst();
			tic2.MoveFirst();
			if (tic1.CurrentNodeTime < tic2.CurrentNodeTime)
			{
				while (!tic1.CurrentIsAtLastNode)
				{
					if (!(tic1.NextNodeTime <= tic2.CurrentNodeTime))
					{
						return;
					}
					tic1.MoveNext();
				}
			}
			else if (tic2.CurrentNodeTime < tic1.CurrentNodeTime)
			{
				while (!tic2.CurrentIsAtLastNode && tic2.NextNodeTime <= tic1.CurrentNodeTime)
				{
					tic2.MoveNext();
				}
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000FEFD0 File Offset: 0x000FE3D0
		private static bool IntersectsHelperUnequalCase(ref TimeIntervalCollection tic1, ref TimeIntervalCollection tic2, ref bool intersectionFound)
		{
			if (tic1.CurrentNodeIsInterval)
			{
				intersectionFound = true;
				return true;
			}
			if (tic1.CurrentIsAtLastNode)
			{
				intersectionFound = false;
				return true;
			}
			while (!tic2.CurrentIsAtLastNode && tic2.NextNodeTime <= tic1.NextNodeTime)
			{
				tic2.MoveNext();
			}
			tic1.MoveNext();
			return false;
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000FF020 File Offset: 0x000FE420
		private static bool IntersectsHelperEqualCase(ref TimeIntervalCollection tic1, ref TimeIntervalCollection tic2, ref bool intersectionFound)
		{
			if ((tic1.CurrentNodeIsPoint && tic2.CurrentNodeIsPoint) || (tic1.CurrentNodeIsInterval && tic2.CurrentNodeIsInterval))
			{
				intersectionFound = true;
				return true;
			}
			if (!tic1.CurrentIsAtLastNode && (tic2.CurrentIsAtLastNode || tic1.NextNodeTime < tic2.NextNodeTime))
			{
				tic1.MoveNext();
			}
			else if (!tic2.CurrentIsAtLastNode && (tic1.CurrentIsAtLastNode || tic2.NextNodeTime < tic1.NextNodeTime))
			{
				tic2.MoveNext();
			}
			else
			{
				if (tic1.CurrentIsAtLastNode || tic2.CurrentIsAtLastNode)
				{
					intersectionFound = false;
					return true;
				}
				tic1.MoveNext();
				tic2.MoveNext();
			}
			return false;
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x000FF0CC File Offset: 0x000FE4CC
		internal bool IntersectsInverseOf(TimeIntervalCollection other)
		{
			if (this.ContainsNullPoint && !other.ContainsNullPoint)
			{
				return true;
			}
			if (this.IsEmptyOfRealPoints)
			{
				return false;
			}
			if (other.IsEmptyOfRealPoints || this._nodeTime[0] < other._nodeTime[0])
			{
				return true;
			}
			other.SetInvertedMode(true);
			bool result = this.IntersectsHelper(other);
			other.SetInvertedMode(false);
			return result;
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x000FF13C File Offset: 0x000FE53C
		internal bool IntersectsPeriodicCollection(TimeSpan beginTime, Duration period, double appliedSpeedRatio, double accelRatio, double decelRatio, bool isAutoReversed)
		{
			if (this.IsEmptyOfRealPoints || period == TimeSpan.Zero || (accelRatio == 0.0 && decelRatio == 0.0 && !isAutoReversed) || !period.HasTimeSpan || appliedSpeedRatio > (double)period.TimeSpan.Ticks)
			{
				return false;
			}
			this.MoveFirst();
			long ticks = beginTime.Ticks;
			long num = (long)((double)period.TimeSpan.Ticks / appliedSpeedRatio);
			if (num < 0L)
			{
				num = 4611686018427387903L;
			}
			long num2 = 2L * num;
			long num3 = (long)(accelRatio * (double)num);
			long num4 = (long)((1.0 - decelRatio) * (double)num);
			while (this._current < this._count)
			{
				bool flag = false;
				long num5;
				if (isAutoReversed)
				{
					num5 = (this.CurrentNodeTime.Ticks - ticks) % num2;
					if (num5 >= num)
					{
						num5 = num2 - num5;
						flag = true;
					}
				}
				else
				{
					num5 = (this.CurrentNodeTime.Ticks - ticks) % num;
				}
				if ((0L < num5 && num5 < num3) || num4 < num5)
				{
					return true;
				}
				if ((num5 == 0L || num5 == num4) && this.CurrentNodeIsPoint)
				{
					return true;
				}
				if (this.CurrentNodeIsInterval)
				{
					if ((num5 == 0L && num3 > 0L) || (num5 == num4 && num4 < num))
					{
						return true;
					}
					long num6;
					if (flag)
					{
						num6 = num5 - num3;
					}
					else
					{
						num6 = num4 - num5;
					}
					if (this.CurrentIsAtLastNode || this.NextNodeTime.Ticks - this.CurrentNodeTime.Ticks >= num6)
					{
						return true;
					}
				}
				this.MoveNext();
			}
			return false;
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x000FF2C4 File Offset: 0x000FE6C4
		internal bool IntersectsMultiplePeriods(TimeSpan beginTime, Duration period, double appliedSpeedRatio)
		{
			if (this._count < 2 || period == TimeSpan.Zero || !period.HasTimeSpan || appliedSpeedRatio > (double)period.TimeSpan.Ticks)
			{
				return false;
			}
			long num = (long)((double)period.TimeSpan.Ticks / appliedSpeedRatio);
			if (num <= 0L)
			{
				return false;
			}
			long num2 = (this.FirstNodeTime - beginTime).Ticks / num;
			TimeSpan t = this._nodeTime[this._count - 1];
			long num3 = (t - beginTime).Ticks / num;
			return num2 != num3;
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x000FF36C File Offset: 0x000FE76C
		internal void ProjectPostFillZone(ref TimeIntervalCollection projection, TimeSpan beginTime, TimeSpan endTime, Duration period, double appliedSpeedRatio, double accelRatio, double decelRatio, bool isAutoReversed)
		{
			long num;
			if (beginTime == endTime)
			{
				num = 0L;
			}
			else
			{
				num = (long)(appliedSpeedRatio * (double)(endTime - beginTime).Ticks);
				if (period.HasTimeSpan)
				{
					long ticks = period.TimeSpan.Ticks;
					if (isAutoReversed)
					{
						long num2 = ticks << 1;
						num %= num2;
						if (num > ticks)
						{
							num = num2 - num;
						}
					}
					else
					{
						num %= ticks;
						if (num == 0L)
						{
							num = ticks;
						}
					}
					if (accelRatio + decelRatio > 0.0)
					{
						double num3 = (double)ticks;
						double num4 = 1.0 / num3;
						double num5 = 1.0 / (2.0 - accelRatio - decelRatio);
						long num6 = (long)(num3 * accelRatio);
						long num7 = ticks - (long)(num3 * decelRatio);
						if (num < num6)
						{
							double num8 = (double)num;
							num = (long)(num5 * num4 * num8 * num8 / accelRatio);
						}
						else if (num <= num7)
						{
							double num8 = (double)num;
							num = (long)(num5 * (2.0 * num8 - accelRatio));
						}
						else
						{
							double num8 = (double)(ticks - num);
							num = ticks - (long)(num5 * num4 * num8 * num8 / decelRatio);
						}
					}
				}
			}
			projection.InitializePoint(TimeSpan.FromTicks(num));
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x000FF484 File Offset: 0x000FE884
		internal void ProjectOntoPeriodicFunction(ref TimeIntervalCollection projection, TimeSpan beginTime, TimeSpan? endTime, Duration fillDuration, Duration period, double appliedSpeedRatio, double accelRatio, double decelRatio, bool isAutoReversed)
		{
			bool containsNullPoint = this._containsNullPoint || this._nodeTime[0] < beginTime || (endTime != null && fillDuration.HasTimeSpan && (this._nodeTime[this._count - 1] > endTime.Value + fillDuration.TimeSpan || (this._nodeTime[this._count - 1] == endTime.Value + fillDuration.TimeSpan && this._nodeIsPoint[this._count - 1] && (endTime > beginTime || fillDuration.TimeSpan > TimeSpan.Zero))));
			if (endTime != null && beginTime == endTime)
			{
				projection.InitializePoint(TimeSpan.Zero);
			}
			else
			{
				bool flag = !fillDuration.HasTimeSpan || fillDuration.TimeSpan > TimeSpan.Zero;
				if (period.HasTimeSpan)
				{
					TimeIntervalCollection timeIntervalCollection = default(TimeIntervalCollection);
					this.ProjectionNormalize(ref timeIntervalCollection, beginTime, endTime, flag, appliedSpeedRatio);
					long ticks = period.TimeSpan.Ticks;
					TimeSpan? activeDuration;
					bool includeMaxPoint;
					if (endTime != null)
					{
						activeDuration = new TimeSpan?(endTime.Value - beginTime);
						includeMaxPoint = (flag && activeDuration.Value.Ticks % ticks == 0L);
					}
					else
					{
						activeDuration = null;
						includeMaxPoint = false;
					}
					projection.EnsureAllocatedCapacity(4);
					timeIntervalCollection.ProjectionFold(ref projection, activeDuration, ticks, isAutoReversed, includeMaxPoint);
					if (accelRatio + decelRatio > 0.0)
					{
						projection.ProjectionWarp(ticks, accelRatio, decelRatio);
					}
				}
				else
				{
					this.ProjectionNormalize(ref projection, beginTime, endTime, flag, appliedSpeedRatio);
				}
			}
			projection._containsNullPoint = containsNullPoint;
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000FF690 File Offset: 0x000FEA90
		private void ProjectionNormalize(ref TimeIntervalCollection projection, TimeSpan beginTime, TimeSpan? endTime, bool includeFillPeriod, double speedRatio)
		{
			projection.EnsureAllocatedCapacity(this._nodeTime.Length);
			this.MoveFirst();
			projection.MoveFirst();
			while (!this.CurrentIsAtLastNode && this.NextNodeTime <= beginTime)
			{
				this.MoveNext();
			}
			if (this.CurrentNodeTime < beginTime)
			{
				if (this.CurrentNodeIsInterval)
				{
					projection._count++;
					projection.CurrentNodeTime = TimeSpan.Zero;
					projection.CurrentNodeIsPoint = true;
					projection.CurrentNodeIsInterval = true;
					projection.MoveNext();
				}
				this.MoveNext();
			}
			while (this._current < this._count && (endTime == null || this.CurrentNodeTime < endTime))
			{
				double num = (double)(this.CurrentNodeTime - beginTime).Ticks;
				projection._count++;
				projection.CurrentNodeTime = TimeSpan.FromTicks((long)(speedRatio * num));
				projection.CurrentNodeIsPoint = this.CurrentNodeIsPoint;
				projection.CurrentNodeIsInterval = this.CurrentNodeIsInterval;
				projection.MoveNext();
				this.MoveNext();
			}
			if (this._current < this._count && (this._nodeIsInterval[this._current - 1] || (this.CurrentNodeTime == endTime.Value && this.CurrentNodeIsPoint && includeFillPeriod)))
			{
				double num2 = (double)(endTime.Value - beginTime).Ticks;
				projection._count++;
				projection.CurrentNodeTime = TimeSpan.FromTicks((long)(speedRatio * num2));
				projection.CurrentNodeIsPoint = (includeFillPeriod && (this.CurrentNodeTime > endTime.Value || this.CurrentNodeIsPoint));
				projection.CurrentNodeIsInterval = false;
			}
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000FF85C File Offset: 0x000FEC5C
		private void ProjectionFold(ref TimeIntervalCollection projection, TimeSpan? activeDuration, long periodInTicks, bool isAutoReversed, bool includeMaxPoint)
		{
			this.MoveFirst();
			bool flag = false;
			do
			{
				if (this.CurrentNodeIsInterval)
				{
					flag = this.ProjectionFoldInterval(ref projection, activeDuration, periodInTicks, isAutoReversed, includeMaxPoint);
					this._current += (this.NextNodeIsInterval ? 1 : 2);
				}
				else
				{
					this.ProjectionFoldPoint(ref projection, activeDuration, periodInTicks, isAutoReversed, includeMaxPoint);
					this._current++;
				}
			}
			while (!flag && this._current < this._count);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000FF8D0 File Offset: 0x000FECD0
		private void ProjectionFoldPoint(ref TimeIntervalCollection projection, TimeSpan? activeDuration, long periodInTicks, bool isAutoReversed, bool includeMaxPoint)
		{
			long num2;
			if (isAutoReversed)
			{
				long num = periodInTicks << 1;
				num2 = this.CurrentNodeTime.Ticks % num;
				if (num2 > periodInTicks)
				{
					num2 = num - num2;
				}
			}
			else if (includeMaxPoint && activeDuration != null && this.CurrentNodeTime == activeDuration)
			{
				num2 = periodInTicks;
			}
			else
			{
				num2 = this.CurrentNodeTime.Ticks % periodInTicks;
			}
			projection.MergePoint(TimeSpan.FromTicks(num2));
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x000FF954 File Offset: 0x000FED54
		private bool ProjectionFoldInterval(ref TimeIntervalCollection projection, TimeSpan? activeDuration, long periodInTicks, bool isAutoReversed, bool includeMaxPoint)
		{
			long ticks = (this.NextNodeTime - this.CurrentNodeTime).Ticks;
			if (isAutoReversed)
			{
				long num = periodInTicks << 1;
				long num2 = this.CurrentNodeTime.Ticks % num;
				bool flag;
				long num3;
				if (num2 < periodInTicks)
				{
					flag = false;
					num3 = periodInTicks - num2;
				}
				else
				{
					flag = true;
					num2 = num - num2;
					num3 = num2;
				}
				long num4 = ticks - num3;
				if (num4 > 0L)
				{
					bool result;
					if (num3 >= num4)
					{
						bool flag2 = this.CurrentNodeIsPoint;
						if (num3 == num4)
						{
							flag2 = (flag2 || this.NextNodeIsPoint);
						}
						if (flag)
						{
							projection.MergeInterval(TimeSpan.Zero, true, TimeSpan.FromTicks(num2), flag2);
							result = (flag2 && num2 == periodInTicks);
						}
						else
						{
							projection.MergeInterval(TimeSpan.FromTicks(num2), flag2, TimeSpan.FromTicks(periodInTicks), true);
							result = (flag2 && num2 == 0L);
						}
					}
					else if (flag)
					{
						long num5 = (num4 < periodInTicks) ? num4 : periodInTicks;
						projection.MergeInterval(TimeSpan.Zero, true, TimeSpan.FromTicks(num5), this.NextNodeIsPoint);
						result = (this.NextNodeIsPoint && num5 == periodInTicks);
					}
					else
					{
						long num6 = (num4 < periodInTicks) ? (periodInTicks - num4) : 0L;
						projection.MergeInterval(TimeSpan.FromTicks(num6), this.NextNodeIsPoint, TimeSpan.FromTicks(periodInTicks), true);
						result = (this.NextNodeIsPoint && num6 == 0L);
					}
					return result;
				}
				if (flag)
				{
					projection.MergeInterval(TimeSpan.FromTicks(num2 - ticks), this.NextNodeIsPoint, TimeSpan.FromTicks(num2), this.CurrentNodeIsPoint);
				}
				else
				{
					projection.MergeInterval(TimeSpan.FromTicks(num2), this.CurrentNodeIsPoint, TimeSpan.FromTicks(num2 + ticks), this.NextNodeIsPoint);
				}
				return false;
			}
			else
			{
				long num2 = this.CurrentNodeTime.Ticks % periodInTicks;
				long num3 = periodInTicks - num2;
				if (ticks > periodInTicks)
				{
					projection._nodeTime[0] = TimeSpan.Zero;
					projection._nodeIsPoint[0] = true;
					projection._nodeIsInterval[0] = true;
					projection._nodeTime[1] = TimeSpan.FromTicks(periodInTicks);
					projection._nodeIsPoint[1] = includeMaxPoint;
					projection._nodeIsInterval[1] = false;
					this._count = 2;
					return true;
				}
				if (ticks >= num3)
				{
					projection.MergeInterval(TimeSpan.FromTicks(num2), this.CurrentNodeIsPoint, TimeSpan.FromTicks(periodInTicks), false);
					if (ticks > num3)
					{
						projection.MergeInterval(TimeSpan.Zero, true, TimeSpan.FromTicks(ticks - num3), this.NextNodeIsPoint);
					}
					else if (this.NextNodeIsPoint)
					{
						if (includeMaxPoint && activeDuration != null && this.NextNodeTime == activeDuration)
						{
							projection.MergePoint(TimeSpan.FromTicks(periodInTicks));
						}
						else
						{
							projection.MergePoint(TimeSpan.Zero);
						}
					}
					return false;
				}
				projection.MergeInterval(TimeSpan.FromTicks(num2), this.CurrentNodeIsPoint, TimeSpan.FromTicks(num2 + ticks), this.NextNodeIsPoint);
				return false;
			}
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x000FFC14 File Offset: 0x000FF014
		private void MergePoint(TimeSpan point)
		{
			int num = this.Locate(point);
			if (num >= 0 && this._nodeTime[num] == point)
			{
				if (!this._nodeIsPoint[num])
				{
					if (num == 0 || !this._nodeIsInterval[num - 1] || !this._nodeIsInterval[num])
					{
						this._nodeIsPoint[num] = true;
						return;
					}
					int num2 = num;
					while (num2 + 1 < this._count)
					{
						this._nodeTime[num2] = this._nodeTime[num2 + 1];
						this._nodeIsPoint[num2] = this._nodeIsPoint[num2 + 1];
						this._nodeIsInterval[num2] = this._nodeIsInterval[num2 + 1];
						num2++;
					}
					this._count--;
					return;
				}
			}
			else if (num == -1 || !this._nodeIsInterval[num])
			{
				this.EnsureAllocatedCapacity(this._count + 1);
				for (int i = this._count - 1; i > num; i--)
				{
					this._nodeTime[i + 1] = this._nodeTime[i];
					this._nodeIsPoint[i + 1] = this._nodeIsPoint[i];
					this._nodeIsInterval[i + 1] = this._nodeIsInterval[i];
				}
				this._nodeTime[num + 1] = point;
				this._nodeIsPoint[num + 1] = true;
				this._nodeIsInterval[num + 1] = false;
				this._count++;
			}
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x000FFD7C File Offset: 0x000FF17C
		private void MergeInterval(TimeSpan from, bool includeFrom, TimeSpan to, bool includeTo)
		{
			if (this.IsEmptyOfRealPoints)
			{
				this._nodeTime[0] = from;
				this._nodeIsPoint[0] = includeFrom;
				this._nodeIsInterval[0] = true;
				this._nodeTime[1] = to;
				this._nodeIsPoint[1] = includeTo;
				this._nodeIsInterval[1] = false;
				this._count = 2;
				return;
			}
			int num = this.Locate(from);
			int num2 = this.Locate(to);
			bool flag = false;
			bool flag2 = false;
			int num3 = num - num2;
			int num4 = num + 1;
			int num5 = num2;
			if (num == -1 || this._nodeTime[num] < from)
			{
				if (num == -1 || !this._nodeIsInterval[num])
				{
					flag = true;
					num3++;
				}
			}
			else if (num > 0 && this._nodeIsInterval[num - 1] && (includeFrom || this._nodeIsPoint[num]))
			{
				num3--;
				num4--;
			}
			else
			{
				this._nodeIsPoint[num] = (includeFrom || this._nodeIsPoint[num]);
			}
			if (num2 == -1 || this._nodeTime[num2] < to)
			{
				if (num2 == -1 || !this._nodeIsInterval[num2])
				{
					flag2 = true;
					num3++;
				}
			}
			else if (!this._nodeIsInterval[num2] || (!includeTo && !this._nodeIsPoint[num2]))
			{
				num3++;
				num5--;
				this._nodeIsPoint[num2] = (includeTo || this._nodeIsPoint[num2]);
			}
			if (num3 > 0)
			{
				this.EnsureAllocatedCapacity(this._count + num3);
				for (int i = this._count - 1; i > num5; i--)
				{
					this._nodeTime[i + num3] = this._nodeTime[i];
					this._nodeIsPoint[i + num3] = this._nodeIsPoint[i];
					this._nodeIsInterval[i + num3] = this._nodeIsInterval[i];
				}
			}
			else if (num3 < 0)
			{
				for (int j = num5 + 1; j < this._count; j++)
				{
					this._nodeTime[j + num3] = this._nodeTime[j];
					this._nodeIsPoint[j + num3] = this._nodeIsPoint[j];
					this._nodeIsInterval[j + num3] = this._nodeIsInterval[j];
				}
			}
			this._count += num3;
			if (flag)
			{
				this._nodeTime[num4] = from;
				this._nodeIsPoint[num4] = includeFrom;
				this._nodeIsInterval[num4] = true;
				num4++;
			}
			if (flag2)
			{
				this._nodeTime[num4] = to;
				this._nodeIsPoint[num4] = includeTo;
				this._nodeIsInterval[num4] = false;
			}
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x000FFFF8 File Offset: 0x000FF3F8
		private void EnsureAllocatedCapacity(int requiredCapacity)
		{
			if (this._nodeTime == null)
			{
				this._nodeTime = new TimeSpan[requiredCapacity];
				this._nodeIsPoint = new bool[requiredCapacity];
				this._nodeIsInterval = new bool[requiredCapacity];
				return;
			}
			if (this._nodeTime.Length < requiredCapacity)
			{
				int num = this._nodeTime.Length << 1;
				TimeSpan[] array = new TimeSpan[num];
				bool[] array2 = new bool[num];
				bool[] array3 = new bool[num];
				for (int i = 0; i < this._count; i++)
				{
					array[i] = this._nodeTime[i];
					array2[i] = this._nodeIsPoint[i];
					array3[i] = this._nodeIsInterval[i];
				}
				this._nodeTime = array;
				this._nodeIsPoint = array2;
				this._nodeIsInterval = array3;
			}
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x001000B0 File Offset: 0x000FF4B0
		private void ProjectionWarp(long periodInTicks, double accelRatio, double decelRatio)
		{
			double num = (double)periodInTicks;
			double num2 = 1.0 / num;
			double num3 = 1.0 / (2.0 - accelRatio - decelRatio);
			TimeSpan t = TimeSpan.FromTicks((long)(num * accelRatio));
			TimeSpan t2 = TimeSpan.FromTicks(periodInTicks - (long)(num * decelRatio));
			this.MoveFirst();
			while (this._current < this._count)
			{
				if (!(this.CurrentNodeTime < t))
				{
					break;
				}
				double num4 = (double)this._nodeTime[this._current].Ticks;
				this._nodeTime[this._current] = TimeSpan.FromTicks((long)(num3 * num2 * num4 * num4 / accelRatio));
				this.MoveNext();
			}
			while (this._current < this._count)
			{
				if (!(this.CurrentNodeTime <= t2))
				{
					break;
				}
				double num4 = (double)this._nodeTime[this._current].Ticks;
				this._nodeTime[this._current] = TimeSpan.FromTicks((long)(num3 * (2.0 * num4 - accelRatio * num)));
				this.MoveNext();
			}
			while (this._current < this._count)
			{
				double num4 = (double)(periodInTicks - this._nodeTime[this._current].Ticks);
				this._nodeTime[this._current] = TimeSpan.FromTicks(periodInTicks - (long)(num3 * num2 * num4 * num4 / decelRatio));
				this.MoveNext();
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x00100218 File Offset: 0x000FF618
		private int Locate(TimeSpan time)
		{
			if (this._count == 0 || time < this._nodeTime[0])
			{
				return -1;
			}
			int num = 0;
			int num2 = this._count - 1;
			while (num + 1 < num2)
			{
				int num3 = num + num2 >> 1;
				if (time < this._nodeTime[num3])
				{
					num2 = num3;
				}
				else
				{
					num = num3;
				}
			}
			if (time < this._nodeTime[num2])
			{
				return num;
			}
			return num2;
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x00100290 File Offset: 0x000FF690
		internal bool IsEmptyOfRealPoints
		{
			get
			{
				return this._count == 0;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x001002A8 File Offset: 0x000FF6A8
		internal bool IsEmpty
		{
			get
			{
				return this._count == 0 && !this._containsNullPoint;
			}
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x001002C8 File Offset: 0x000FF6C8
		private void MoveFirst()
		{
			this._current = 0;
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x001002DC File Offset: 0x000FF6DC
		private void MoveNext()
		{
			this._current++;
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004115 RID: 16661 RVA: 0x001002F8 File Offset: 0x000FF6F8
		private bool CurrentIsAtLastNode
		{
			get
			{
				return this._current + 1 == this._count;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x00100318 File Offset: 0x000FF718
		// (set) Token: 0x06004117 RID: 16663 RVA: 0x00100338 File Offset: 0x000FF738
		private TimeSpan CurrentNodeTime
		{
			get
			{
				return this._nodeTime[this._current];
			}
			set
			{
				this._nodeTime[this._current] = value;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x00100358 File Offset: 0x000FF758
		// (set) Token: 0x06004119 RID: 16665 RVA: 0x0010037C File Offset: 0x000FF77C
		private bool CurrentNodeIsPoint
		{
			get
			{
				return this._nodeIsPoint[this._current] ^ this._invertCollection;
			}
			set
			{
				this._nodeIsPoint[this._current] = value;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x00100398 File Offset: 0x000FF798
		// (set) Token: 0x0600411B RID: 16667 RVA: 0x001003BC File Offset: 0x000FF7BC
		private bool CurrentNodeIsInterval
		{
			get
			{
				return this._nodeIsInterval[this._current] ^ this._invertCollection;
			}
			set
			{
				this._nodeIsInterval[this._current] = value;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x001003D8 File Offset: 0x000FF7D8
		private TimeSpan NextNodeTime
		{
			get
			{
				return this._nodeTime[this._current + 1];
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x001003F8 File Offset: 0x000FF7F8
		private bool NextNodeIsPoint
		{
			get
			{
				return this._nodeIsPoint[this._current + 1] ^ this._invertCollection;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x0010041C File Offset: 0x000FF81C
		private bool NextNodeIsInterval
		{
			get
			{
				return this._nodeIsInterval[this._current + 1] ^ this._invertCollection;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x00100440 File Offset: 0x000FF840
		internal bool ContainsNullPoint
		{
			get
			{
				return this._containsNullPoint ^ this._invertCollection;
			}
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x0010045C File Offset: 0x000FF85C
		private void SetInvertedMode(bool mode)
		{
			this._invertCollection = mode;
		}

		// Token: 0x040017CD RID: 6093
		private TimeSpan[] _nodeTime;

		// Token: 0x040017CE RID: 6094
		private bool[] _nodeIsPoint;

		// Token: 0x040017CF RID: 6095
		private bool[] _nodeIsInterval;

		// Token: 0x040017D0 RID: 6096
		private bool _containsNullPoint;

		// Token: 0x040017D1 RID: 6097
		private int _count;

		// Token: 0x040017D2 RID: 6098
		private int _current;

		// Token: 0x040017D3 RID: 6099
		private bool _invertCollection;

		// Token: 0x040017D4 RID: 6100
		private const int _minimumCapacity = 4;
	}
}
