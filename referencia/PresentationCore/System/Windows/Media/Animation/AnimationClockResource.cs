using System;
using System.Windows.Media.Composition;
using System.Windows.Threading;

namespace System.Windows.Media.Animation
{
	// Token: 0x020004A1 RID: 1185
	internal abstract class AnimationClockResource : DUCE.IResource
	{
		// Token: 0x06003487 RID: 13447 RVA: 0x000D01EC File Offset: 0x000CF5EC
		protected AnimationClockResource(AnimationClock animationClock)
		{
			this._animationClock = animationClock;
			if (this._animationClock != null)
			{
				this._animationClock.CurrentTimeInvalidated += this.OnChanged;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000D0228 File Offset: 0x000CF628
		public AnimationClock AnimationClock
		{
			get
			{
				return this._animationClock;
			}
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000D023C File Offset: 0x000CF63C
		protected void OnChanged(object sender, EventArgs args)
		{
			Dispatcher dispatcher = ((DispatcherObject)sender).Dispatcher;
			MediaContext mediaContext = MediaContext.From(dispatcher);
			DUCE.Channel channel = mediaContext.Channel;
			if (!this.IsResourceInvalid && this._duceResource.IsOnAnyChannel)
			{
				mediaContext.ResourcesUpdated += this.UpdateResourceFromMediaContext;
				this.IsResourceInvalid = true;
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000D0294 File Offset: 0x000CF694
		internal virtual void PropagateChangedHandlersCore(EventHandler handler, bool adding)
		{
			if (this._animationClock != null)
			{
				if (adding)
				{
					this._animationClock.CurrentTimeInvalidated += handler;
					return;
				}
				this._animationClock.CurrentTimeInvalidated -= handler;
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000D02C8 File Offset: 0x000CF6C8
		private void UpdateResourceFromMediaContext(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (this.IsResourceInvalid && (skipOnChannelCheck || this._duceResource.IsOnChannel(channel)))
			{
				this.UpdateResource(this._duceResource.GetHandle(channel), channel);
				this.IsResourceInvalid = false;
			}
		}

		// Token: 0x0600348C RID: 13452
		protected abstract void UpdateResource(DUCE.ResourceHandle handle, DUCE.Channel channel);

		// Token: 0x0600348D RID: 13453 RVA: 0x000D0308 File Offset: 0x000CF708
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.CreateOrAddRefOnChannel(this, channel, this.ResourceType))
				{
					this.UpdateResource(this._duceResource.GetHandle(channel), channel);
				}
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000D0380 File Offset: 0x000CF780
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this._duceResource.ReleaseOnChannel(channel);
			}
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000D03CC File Offset: 0x000CF7CC
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000D041C File Offset: 0x000CF81C
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000D0434 File Offset: 0x000CF834
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000D0450 File Offset: 0x000CF850
		void DUCE.IResource.RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000D0464 File Offset: 0x000CF864
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000D0478 File Offset: 0x000CF878
		// (set) Token: 0x06003495 RID: 13461 RVA: 0x000D048C File Offset: 0x000CF88C
		protected bool IsResourceInvalid
		{
			get
			{
				return this._isResourceInvalid;
			}
			set
			{
				this._isResourceInvalid = value;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003496 RID: 13462
		protected abstract DUCE.ResourceType ResourceType { get; }

		// Token: 0x04001601 RID: 5633
		private DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001602 RID: 5634
		private bool _isResourceInvalid;

		// Token: 0x04001603 RID: 5635
		protected AnimationClock _animationClock;
	}
}
