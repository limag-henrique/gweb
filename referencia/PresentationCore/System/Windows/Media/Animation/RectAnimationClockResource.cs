using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000538 RID: 1336
	internal class RectAnimationClockResource : AnimationClockResource, DUCE.IResource
	{
		// Token: 0x06003CF0 RID: 15600 RVA: 0x000EFBA4 File Offset: 0x000EEFA4
		public RectAnimationClockResource(Rect baseValue, AnimationClock animationClock) : base(animationClock)
		{
			this._baseValue = baseValue;
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x000EFBC0 File Offset: 0x000EEFC0
		public Rect BaseValue
		{
			get
			{
				return this._baseValue;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000EFBD4 File Offset: 0x000EEFD4
		public Rect CurrentValue
		{
			get
			{
				if (this._animationClock != null)
				{
					return ((RectAnimationBase)this._animationClock.Timeline).GetCurrentValue(this._baseValue, this._baseValue, this._animationClock);
				}
				return this._baseValue;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06003CF3 RID: 15603 RVA: 0x000EFC18 File Offset: 0x000EF018
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_RECTRESOURCE;
			}
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000EFC28 File Offset: 0x000EF028
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResource(DUCE.ResourceHandle handle, DUCE.Channel channel)
		{
			DUCE.MILCMD_RECTRESOURCE milcmd_RECTRESOURCE = default(DUCE.MILCMD_RECTRESOURCE);
			milcmd_RECTRESOURCE.Type = MILCMD.MilCmdRectResource;
			milcmd_RECTRESOURCE.Handle = handle;
			milcmd_RECTRESOURCE.Value = this.CurrentValue;
			channel.SendCommand((byte*)(&milcmd_RECTRESOURCE), sizeof(DUCE.MILCMD_RECTRESOURCE));
			base.IsResourceInvalid = false;
		}

		// Token: 0x04001720 RID: 5920
		private Rect _baseValue;
	}
}
