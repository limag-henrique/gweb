using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x0200052D RID: 1325
	internal class PointAnimationClockResource : AnimationClockResource, DUCE.IResource
	{
		// Token: 0x06003C27 RID: 15399 RVA: 0x000EC688 File Offset: 0x000EBA88
		public PointAnimationClockResource(Point baseValue, AnimationClock animationClock) : base(animationClock)
		{
			this._baseValue = baseValue;
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x000EC6A4 File Offset: 0x000EBAA4
		public Point BaseValue
		{
			get
			{
				return this._baseValue;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000EC6B8 File Offset: 0x000EBAB8
		public Point CurrentValue
		{
			get
			{
				if (this._animationClock != null)
				{
					return ((PointAnimationBase)this._animationClock.Timeline).GetCurrentValue(this._baseValue, this._baseValue, this._animationClock);
				}
				return this._baseValue;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000EC6FC File Offset: 0x000EBAFC
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_POINTRESOURCE;
			}
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x000EC70C File Offset: 0x000EBB0C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResource(DUCE.ResourceHandle handle, DUCE.Channel channel)
		{
			DUCE.MILCMD_POINTRESOURCE milcmd_POINTRESOURCE = default(DUCE.MILCMD_POINTRESOURCE);
			milcmd_POINTRESOURCE.Type = MILCMD.MilCmdPointResource;
			milcmd_POINTRESOURCE.Handle = handle;
			milcmd_POINTRESOURCE.Value = this.CurrentValue;
			channel.SendCommand((byte*)(&milcmd_POINTRESOURCE), sizeof(DUCE.MILCMD_POINTRESOURCE));
			base.IsResourceInvalid = false;
		}

		// Token: 0x04001706 RID: 5894
		private Point _baseValue;
	}
}
