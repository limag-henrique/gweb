using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Threading;
using MS.Win32;

namespace System.Windows.Interop
{
	// Token: 0x0200032A RID: 810
	internal class HwndPanningFeedback
	{
		// Token: 0x06001B50 RID: 6992 RVA: 0x0006D724 File Offset: 0x0006CB24
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public HwndPanningFeedback(HwndSource hwndSource)
		{
			if (hwndSource == null)
			{
				throw new ArgumentNullException("hwndSource");
			}
			this._hwndSource = hwndSource;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0006D74C File Offset: 0x0006CB4C
		private static bool IsSupported
		{
			get
			{
				return OperatingSystemVersionCheck.IsVersionOrLater(OperatingSystemVersion.Windows7);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0006D760 File Offset: 0x0006CB60
		private HandleRef Handle
		{
			[SecurityCritical]
			get
			{
				if (this._hwndSource != null)
				{
					IntPtr criticalHandle = this._hwndSource.CriticalHandle;
					if (criticalHandle != IntPtr.Zero)
					{
						return new HandleRef(this._hwndSource, criticalHandle);
					}
				}
				return default(HandleRef);
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0006D7A4 File Offset: 0x0006CBA4
		[SecurityCritical]
		public void UpdatePanningFeedback(Vector totalOverpanOffset, bool inInertia)
		{
			if (this._hwndSource != null && HwndPanningFeedback.IsSupported)
			{
				if (!this._isProvidingPanningFeedback)
				{
					this._isProvidingPanningFeedback = UnsafeNativeMethods.BeginPanningFeedback(this.Handle);
				}
				if (this._isProvidingPanningFeedback)
				{
					Point point = this._hwndSource.TransformToDevice((Point)totalOverpanOffset);
					this._deviceOffsetX = (int)point.X;
					this._deviceOffsetY = (int)point.Y;
					this._inInertia = inInertia;
					if (this._updatePanningOperation == null)
					{
						this._updatePanningOperation = this._hwndSource.Dispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(this.OnUpdatePanningFeedback), this);
					}
				}
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0006D848 File Offset: 0x0006CC48
		[SecurityCritical]
		private object OnUpdatePanningFeedback(object args)
		{
			HwndPanningFeedback hwndPanningFeedback = (HwndPanningFeedback)args;
			this._updatePanningOperation = null;
			UnsafeNativeMethods.UpdatePanningFeedback(hwndPanningFeedback.Handle, hwndPanningFeedback._deviceOffsetX, hwndPanningFeedback._deviceOffsetY, hwndPanningFeedback._inInertia);
			return null;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0006D884 File Offset: 0x0006CC84
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public void EndPanningFeedback(bool animateBack)
		{
			if (this._hwndSource != null && this._isProvidingPanningFeedback)
			{
				this._isProvidingPanningFeedback = false;
				if (this._updatePanningOperation != null)
				{
					this._updatePanningOperation.Abort();
					this._updatePanningOperation = null;
				}
				UnsafeNativeMethods.EndPanningFeedback(this.Handle, animateBack);
			}
		}

		// Token: 0x04000EA7 RID: 3751
		private int _deviceOffsetX;

		// Token: 0x04000EA8 RID: 3752
		private int _deviceOffsetY;

		// Token: 0x04000EA9 RID: 3753
		private bool _inInertia;

		// Token: 0x04000EAA RID: 3754
		private DispatcherOperation _updatePanningOperation;

		// Token: 0x04000EAB RID: 3755
		private bool _isProvidingPanningFeedback;

		// Token: 0x04000EAC RID: 3756
		[SecurityCritical]
		private HwndSource _hwndSource;
	}
}
