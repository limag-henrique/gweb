using System;
using System.Security;
using System.Windows.Media.Composition;

namespace System.Windows.Media.Animation
{
	// Token: 0x020004D9 RID: 1241
	internal class DoubleAnimationClockResource : AnimationClockResource, DUCE.IResource
	{
		// Token: 0x060037D2 RID: 14290 RVA: 0x000DE0F0 File Offset: 0x000DD4F0
		public DoubleAnimationClockResource(double baseValue, AnimationClock animationClock) : base(animationClock)
		{
			this._baseValue = baseValue;
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060037D3 RID: 14291 RVA: 0x000DE10C File Offset: 0x000DD50C
		public double BaseValue
		{
			get
			{
				return this._baseValue;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060037D4 RID: 14292 RVA: 0x000DE120 File Offset: 0x000DD520
		public double CurrentValue
		{
			get
			{
				if (this._animationClock != null)
				{
					return ((DoubleAnimationBase)this._animationClock.Timeline).GetCurrentValue(this._baseValue, this._baseValue, this._animationClock);
				}
				return this._baseValue;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000DE164 File Offset: 0x000DD564
		protected override DUCE.ResourceType ResourceType
		{
			get
			{
				return DUCE.ResourceType.TYPE_DOUBLERESOURCE;
			}
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000DE174 File Offset: 0x000DD574
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected unsafe override void UpdateResource(DUCE.ResourceHandle handle, DUCE.Channel channel)
		{
			DUCE.MILCMD_DOUBLERESOURCE milcmd_DOUBLERESOURCE = default(DUCE.MILCMD_DOUBLERESOURCE);
			milcmd_DOUBLERESOURCE.Type = MILCMD.MilCmdDoubleResource;
			milcmd_DOUBLERESOURCE.Handle = handle;
			milcmd_DOUBLERESOURCE.Value = this.CurrentValue;
			channel.SendCommand((byte*)(&milcmd_DOUBLERESOURCE), sizeof(DUCE.MILCMD_DOUBLERESOURCE));
			base.IsResourceInvalid = false;
		}

		// Token: 0x04001682 RID: 5762
		private double _baseValue;
	}
}
