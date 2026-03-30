using System;
using System.Security;
using System.Security.Permissions;
using System.Windows.Media;
using MS.Internal;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002F3 RID: 755
	internal sealed class PointerTouchDevice : StylusTouchDeviceBase
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x0005FFEC File Offset: 0x0005F3EC
		internal PointerTouchDevice(PointerStylusDevice stylusDevice) : base(stylusDevice)
		{
			this._stylusDevice = stylusDevice;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00060008 File Offset: 0x0005F408
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected override void OnManipulationEnded(bool cancel)
		{
			base.OnManipulationEnded(cancel);
			if (cancel)
			{
				base.PromotingToOther = true;
				return;
			}
			base.PromotingToOther = false;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00060030 File Offset: 0x0005F430
		protected override void OnManipulationStarted()
		{
			base.OnManipulationStarted();
			base.PromotingToOther = false;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0006004C File Offset: 0x0005F44C
		[SecuritySafeCritical]
		protected override double GetStylusPointWidthOrHeight(StylusPoint stylusPoint, bool isWidth)
		{
			double num = 96.0;
			PresentationSource activeSource = this.ActiveSource;
			if (((activeSource != null) ? activeSource.RootVisual : null) != null)
			{
				num = VisualTreeHelper.GetDpi(this.ActiveSource.RootVisual).PixelsPerInchX;
			}
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

		// Token: 0x060017F4 RID: 6132 RVA: 0x00060114 File Offset: 0x0005F514
		protected override void OnActivateImpl()
		{
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00060124 File Offset: 0x0005F524
		protected override void OnDeactivateImpl()
		{
		}

		// Token: 0x04000D15 RID: 3349
		private PointerStylusDevice _stylusDevice;
	}
}
