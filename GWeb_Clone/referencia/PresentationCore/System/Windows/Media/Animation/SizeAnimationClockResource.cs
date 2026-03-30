using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x02000546 RID: 1350
	internal class SizeAnimationClockResource : AnimationClockResource, DUCE.IResource
	{
		// Token: 0x06003E12 RID: 15890 RVA: 0x000F47DC File Offset: 0x000F3BDC
		public SizeAnimationClockResource(Size baseValue, AnimationClock animationClock) : base(animationClock)
		{
			this._baseValue = baseValue;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06003E13 RID: 15891 RVA: 0x000F47F8 File Offset: 0x000F3BF8
		public Size BaseValue
		{
			get
			{
				return this._baseValue;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000F480C File Offset: 0x000F3C0C
		public Size CurrentValue
		{
			get
			{
				if (this._animationClock != null)
				{
					return ((SizeAnimationBase)this._animationClock.Timeline).GetCurrentValue(this._baseValue, this._baseValue, this._animationClock);
				}
				return this._baseValue;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x000F4850 File Offset: 0x000F3C50
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_SIZERESOURCE;
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000F4860 File Offset: 0x000F3C60
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResource(DUCE.ResourceHandle handle, DUCE.Channel channel)
		{
			DUCE.MILCMD_SIZERESOURCE milcmd_SIZERESOURCE = default(DUCE.MILCMD_SIZERESOURCE);
			milcmd_SIZERESOURCE.Type = MILCMD.MilCmdSizeResource;
			milcmd_SIZERESOURCE.Handle = handle;
			milcmd_SIZERESOURCE.Value = this.CurrentValue;
			channel.SendCommand((byte*)(&milcmd_SIZERESOURCE), sizeof(DUCE.MILCMD_SIZERESOURCE));
			base.IsResourceInvalid = false;
		}

		// Token: 0x04001745 RID: 5957
		private Size _baseValue;
	}
}
