using System;
using System.Collections.Generic;

namespace System.Windows.Media.Animation
{
	// Token: 0x020004A3 RID: 1187
	internal class AnimationLayer
	{
		// Token: 0x0600349C RID: 13468 RVA: 0x000D0520 File Offset: 0x000CF920
		internal AnimationLayer(AnimationStorage ownerStorage)
		{
			this._ownerStorage = ownerStorage;
			this._removeRequestedHandler = new EventHandler(this.OnRemoveRequested);
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000D0558 File Offset: 0x000CF958
		internal void ApplyAnimationClocks(IList<AnimationClock> newAnimationClocks, HandoffBehavior handoffBehavior, object defaultDestinationValue)
		{
			if (handoffBehavior == HandoffBehavior.SnapshotAndReplace)
			{
				EventHandler value = new EventHandler(this.OnCurrentStateInvalidated);
				if (this._hasStickySnapshotValue)
				{
					this._animationClocks[0].CurrentStateInvalidated -= value;
					this.DetachAnimationClocks();
				}
				else if (this._animationClocks != null)
				{
					this._snapshotValue = this.GetCurrentValue(defaultDestinationValue);
					this.DetachAnimationClocks();
				}
				else
				{
					this._snapshotValue = defaultDestinationValue;
				}
				if (newAnimationClocks != null && newAnimationClocks[0].CurrentState == ClockState.Stopped)
				{
					this._hasStickySnapshotValue = true;
					newAnimationClocks[0].CurrentStateInvalidated += value;
				}
				else
				{
					this._hasStickySnapshotValue = false;
				}
				this.SetAnimationClocks(newAnimationClocks);
				return;
			}
			if (newAnimationClocks == null)
			{
				return;
			}
			if (this._animationClocks == null)
			{
				this.SetAnimationClocks(newAnimationClocks);
				return;
			}
			this.AppendAnimationClocks(newAnimationClocks);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000D0610 File Offset: 0x000CFA10
		private void DetachAnimationClocks()
		{
			int count = this._animationClocks.Count;
			for (int i = 0; i < count; i++)
			{
				this._ownerStorage.DetachAnimationClock(this._animationClocks[i], this._removeRequestedHandler);
			}
			this._animationClocks = null;
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000D065C File Offset: 0x000CFA5C
		private void SetAnimationClocks(IList<AnimationClock> animationClocks)
		{
			this._animationClocks = animationClocks;
			int count = animationClocks.Count;
			for (int i = 0; i < count; i++)
			{
				this._ownerStorage.AttachAnimationClock(animationClocks[i], this._removeRequestedHandler);
			}
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000D069C File Offset: 0x000CFA9C
		private void OnCurrentStateInvalidated(object sender, EventArgs args)
		{
			this._hasStickySnapshotValue = false;
			((AnimationClock)sender).CurrentStateInvalidated -= this.OnCurrentStateInvalidated;
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000D06C8 File Offset: 0x000CFAC8
		private void OnRemoveRequested(object sender, EventArgs args)
		{
			AnimationClock animationClock = (AnimationClock)sender;
			int num = this._animationClocks.IndexOf(animationClock);
			if (this._hasStickySnapshotValue && num == 0)
			{
				this._animationClocks[0].CurrentStateInvalidated -= this.OnCurrentStateInvalidated;
				this._hasStickySnapshotValue = false;
			}
			this._animationClocks.RemoveAt(num);
			this._ownerStorage.DetachAnimationClock(animationClock, this._removeRequestedHandler);
			AnimationStorage ownerStorage = this._ownerStorage;
			if (this._animationClocks.Count == 0)
			{
				this._animationClocks = null;
				this._snapshotValue = DependencyProperty.UnsetValue;
				this._ownerStorage.RemoveLayer(this);
				this._ownerStorage = null;
			}
			ownerStorage.WritePostscript();
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000D0778 File Offset: 0x000CFB78
		private void AppendAnimationClocks(IList<AnimationClock> newAnimationClocks)
		{
			int count = newAnimationClocks.Count;
			List<AnimationClock> list = this._animationClocks as List<AnimationClock>;
			if (list == null)
			{
				int num = (this._animationClocks == null) ? 0 : this._animationClocks.Count;
				list = new List<AnimationClock>(num + count);
				for (int i = 0; i < num; i++)
				{
					list.Add(this._animationClocks[i]);
				}
				this._animationClocks = list;
			}
			for (int j = 0; j < count; j++)
			{
				AnimationClock animationClock = newAnimationClocks[j];
				list.Add(animationClock);
				this._ownerStorage.AttachAnimationClock(animationClock, this._removeRequestedHandler);
			}
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000D0814 File Offset: 0x000CFC14
		internal object GetCurrentValue(object defaultDestinationValue)
		{
			if (this._hasStickySnapshotValue && this._animationClocks[0].CurrentState == ClockState.Stopped)
			{
				return this._snapshotValue;
			}
			if (this._animationClocks == null)
			{
				return this._snapshotValue;
			}
			object obj = this._snapshotValue;
			bool flag = false;
			if (obj == DependencyProperty.UnsetValue)
			{
				obj = defaultDestinationValue;
			}
			int count = this._animationClocks.Count;
			for (int i = 0; i < count; i++)
			{
				AnimationClock animationClock = this._animationClocks[i];
				if (animationClock.CurrentState != ClockState.Stopped)
				{
					flag = true;
					obj = animationClock.GetCurrentValue(obj, defaultDestinationValue);
				}
			}
			if (flag)
			{
				return obj;
			}
			return defaultDestinationValue;
		}

		// Token: 0x04001607 RID: 5639
		private object _snapshotValue = DependencyProperty.UnsetValue;

		// Token: 0x04001608 RID: 5640
		private IList<AnimationClock> _animationClocks;

		// Token: 0x04001609 RID: 5641
		private AnimationStorage _ownerStorage;

		// Token: 0x0400160A RID: 5642
		private EventHandler _removeRequestedHandler;

		// Token: 0x0400160B RID: 5643
		private bool _hasStickySnapshotValue;
	}
}
