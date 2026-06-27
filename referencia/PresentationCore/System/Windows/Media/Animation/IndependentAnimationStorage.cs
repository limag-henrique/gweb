using System;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200056F RID: 1391
	internal abstract class IndependentAnimationStorage : AnimationStorage, DUCE.IResource
	{
		// Token: 0x06004073 RID: 16499
		protected abstract void UpdateResourceCore(DUCE.Channel channel);

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004074 RID: 16500
		protected abstract DUCE.ResourceType ResourceType { get; }

		// Token: 0x06004075 RID: 16501 RVA: 0x000FCCC0 File Offset: 0x000FC0C0
		DUCE.ResourceHandle DUCE.IResource.AddRefOnChannel(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				if (this._duceResource.CreateOrAddRefOnChannel(this, channel, this.ResourceType))
				{
					this._updateResourceHandler = new MediaContext.ResourcesUpdatedHandler(this.UpdateResource);
					this.UpdateResourceCore(channel);
				}
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x000FCD3C File Offset: 0x000FC13C
		void DUCE.IResource.ReleaseOnChannel(DUCE.Channel channel)
		{
			using (CompositionEngineLock.Acquire())
			{
				this._duceResource.ReleaseOnChannel(channel);
				if (!this._duceResource.IsOnAnyChannel)
				{
					DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
					if (!this._isValid)
					{
						MediaContext mediaContext = MediaContext.From(dependencyObject.Dispatcher);
						mediaContext.ResourcesUpdated -= this._updateResourceHandler;
						this._isValid = true;
					}
					this._updateResourceHandler = null;
				}
			}
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000FCDD4 File Offset: 0x000FC1D4
		DUCE.ResourceHandle DUCE.IResource.GetHandle(DUCE.Channel channel)
		{
			DUCE.ResourceHandle handle;
			using (CompositionEngineLock.Acquire())
			{
				handle = this._duceResource.GetHandle(channel);
			}
			return handle;
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000FCE24 File Offset: 0x000FC224
		int DUCE.IResource.GetChannelCount()
		{
			return this._duceResource.GetChannelCount();
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000FCE3C File Offset: 0x000FC23C
		DUCE.Channel DUCE.IResource.GetChannel(int index)
		{
			return this._duceResource.GetChannel(index);
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x000FCE58 File Offset: 0x000FC258
		void DUCE.IResource.RemoveChildFromParent(DUCE.IResource parent, DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000FCE6C File Offset: 0x000FC26C
		DUCE.ResourceHandle DUCE.IResource.Get3DHandle(DUCE.Channel channel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x000FCE80 File Offset: 0x000FC280
		private void UpdateResource(DUCE.Channel channel, bool skipOnChannelCheck)
		{
			if (skipOnChannelCheck || this._duceResource.IsOnChannel(channel))
			{
				this.UpdateResourceCore(channel);
				this._isValid = true;
			}
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000FCEAC File Offset: 0x000FC2AC
		internal void InvalidateResource()
		{
			if (this._isValid && this._updateResourceHandler != null)
			{
				DependencyObject dependencyObject = (DependencyObject)this._dependencyObject.Target;
				this._isValid = false;
				MediaContext.CurrentMediaContext.ResourcesUpdated += this._updateResourceHandler;
			}
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000FCEF4 File Offset: 0x000FC2F4
		internal static DUCE.ResourceHandle GetResourceHandle(DependencyObject d, DependencyProperty dp, DUCE.Channel channel)
		{
			IndependentAnimationStorage independentAnimationStorage = AnimationStorage.GetStorage(d, dp) as IndependentAnimationStorage;
			if (independentAnimationStorage == null)
			{
				return DUCE.ResourceHandle.Null;
			}
			return ((DUCE.IResource)independentAnimationStorage).GetHandle(channel);
		}

		// Token: 0x04001791 RID: 6033
		protected MediaContext.ResourcesUpdatedHandler _updateResourceHandler;

		// Token: 0x04001792 RID: 6034
		protected DUCE.MultiChannelResource _duceResource;

		// Token: 0x04001793 RID: 6035
		private bool _isValid = true;
	}
}
