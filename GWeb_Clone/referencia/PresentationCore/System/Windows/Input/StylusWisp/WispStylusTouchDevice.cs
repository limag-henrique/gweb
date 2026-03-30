using System;
using System.Security;
using System.Security.Permissions;
using MS.Internal;

namespace System.Windows.Input.StylusWisp
{
	// Token: 0x020002E6 RID: 742
	internal sealed class WispStylusTouchDevice : StylusTouchDeviceBase
	{
		// Token: 0x06001714 RID: 5908 RVA: 0x00059F3C File Offset: 0x0005933C
		[SecuritySafeCritical]
		internal WispStylusTouchDevice(StylusDeviceBase stylusDevice) : base(stylusDevice)
		{
			this._stylusLogic = StylusLogic.GetCurrentStylusLogicAs<WispLogic>();
			base.PromotingToOther = true;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00059F64 File Offset: 0x00059364
		protected override double GetStylusPointWidthOrHeight(StylusPoint stylusPoint, bool isWidth)
		{
			double num = 96.0;
			StylusPointProperty stylusPointProperty = isWidth ? StylusPointProperties.Width : StylusPointProperties.Height;
			double num2 = 0.0;
			if (stylusPoint.HasProperty(stylusPointProperty))
			{
				num2 = (double)stylusPoint.GetPropertyValue(stylusPointProperty);
				StylusPointPropertyInfo propertyInfo = stylusPoint.Description.GetPropertyInfo(stylusPointProperty);
				if (!DoubleUtil.AreClose((double)propertyInfo.Resolution, 0.0))
				{
					num2 /= (double)propertyInfo.Resolution;
				}
				else
				{
					num2 = 0.0;
				}
				if (propertyInfo.Unit == StylusPointPropertyUnit.Centimeters)
				{
					num2 /= 2.54;
				}
				num2 *= num;
			}
			return num2;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005A000 File Offset: 0x00059400
		protected override void OnManipulationStarted()
		{
			base.OnManipulationStarted();
			base.PromotingToOther = false;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0005A01C File Offset: 0x0005941C
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected override void OnManipulationEnded(bool cancel)
		{
			base.OnManipulationEnded(cancel);
			if (cancel)
			{
				base.PromotingToOther = true;
				this._stylusLogic.PromoteStoredItemsToMouse(this);
			}
			else
			{
				base.PromotingToOther = false;
			}
			if (this._storedStagingAreaItems != null)
			{
				this._storedStagingAreaItems.Clear();
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x0005A064 File Offset: 0x00059464
		internal WispLogic.StagingAreaInputItemList StoredStagingAreaItems
		{
			[SecurityCritical]
			get
			{
				if (this._storedStagingAreaItems == null)
				{
					this._storedStagingAreaItems = new WispLogic.StagingAreaInputItemList();
				}
				return this._storedStagingAreaItems;
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0005A08C File Offset: 0x0005948C
		[SecurityCritical]
		protected override void OnActivateImpl()
		{
			if (StylusTouchDeviceBase.ActiveDeviceCount == 1)
			{
				this._stylusLogic.CurrentMousePromotionStylusDevice = base.StylusDevice;
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0005A0B4 File Offset: 0x000594B4
		[SecurityCritical]
		protected override void OnDeactivateImpl()
		{
			if (this._storedStagingAreaItems != null)
			{
				this._storedStagingAreaItems.Clear();
			}
			if (StylusTouchDeviceBase.ActiveDeviceCount == 0)
			{
				this._stylusLogic.CurrentMousePromotionStylusDevice = null;
				return;
			}
			if (base.IsPrimary)
			{
				this._stylusLogic.CurrentMousePromotionStylusDevice = WispStylusTouchDevice.NoMousePromotionStylusDevice;
			}
		}

		// Token: 0x04000C8C RID: 3212
		[SecurityCritical]
		private WispLogic _stylusLogic;

		// Token: 0x04000C8D RID: 3213
		[SecurityCritical]
		private WispLogic.StagingAreaInputItemList _storedStagingAreaItems;

		// Token: 0x04000C8E RID: 3214
		private static object NoMousePromotionStylusDevice = new object();
	}
}
