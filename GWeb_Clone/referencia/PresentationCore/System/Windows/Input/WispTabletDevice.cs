using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input.StylusWisp;
using System.Windows.Input.Tracing;
using MS.Internal;
using MS.Win32;
using MS.Win32.Penimc;

namespace System.Windows.Input
{
	// Token: 0x020002D6 RID: 726
	internal class WispTabletDevice : TabletDeviceBase
	{
		// Token: 0x060015F0 RID: 5616 RVA: 0x00052030 File Offset: 0x00051430
		[SecurityCritical]
		internal WispTabletDevice(TabletDeviceInfo tabletInfo, PenThread penThread) : base(tabletInfo)
		{
			this._penThread = penThread;
			this._penThread.WorkerAcquireTabletLocks(tabletInfo.PimcTablet.Value, tabletInfo.WispTabletKey);
			int num = tabletInfo.StylusDevicesInfo.Length;
			WispStylusDevice[] array = new WispStylusDevice[num];
			for (int i = 0; i < num; i++)
			{
				StylusDeviceInfo stylusDeviceInfo = tabletInfo.StylusDevicesInfo[i];
				array[i] = new WispStylusDevice(this, stylusDeviceInfo.CursorName, stylusDeviceInfo.CursorId, stylusDeviceInfo.CursorInverted, stylusDeviceInfo.ButtonCollection);
			}
			this._stylusDeviceCollection = new StylusDeviceCollection(array);
			StylusTraceLogger.LogDeviceConnect(new StylusTraceLogger.StylusDeviceInfo(base.Id, base.Name, base.ProductId, base.TabletHardwareCapabilities, base.TabletSize, base.ScreenSize, this._tabletInfo.DeviceType, this.StylusDevices.Count));
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00052110 File Offset: 0x00051510
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					this.DisposeOrDeferDisposal();
					return;
				}
				this._disposed = true;
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00052138 File Offset: 0x00051538
		[SecurityCritical]
		internal WispStylusDevice UpdateStylusDevices(int stylusId)
		{
			StylusDeviceInfo[] array = this._penThread.WorkerRefreshCursorInfo(this._tabletInfo.PimcTablet.Value);
			int num = array.Length;
			if (num > this.StylusDevices.Count)
			{
				for (int i = 0; i < num; i++)
				{
					StylusDeviceInfo stylusDeviceInfo = array[i];
					if (stylusDeviceInfo.CursorId == stylusId)
					{
						WispStylusDevice wispStylusDevice = new WispStylusDevice(this, stylusDeviceInfo.CursorName, stylusDeviceInfo.CursorId, stylusDeviceInfo.CursorInverted, stylusDeviceInfo.ButtonCollection);
						this.StylusDevices.AddStylusDevice(i, wispStylusDevice);
						return wispStylusDevice;
					}
				}
			}
			return null;
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x000521C4 File Offset: 0x000515C4
		internal override IInputElement Target
		{
			get
			{
				base.VerifyAccess();
				StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
				if (currentStylusDevice == null)
				{
					return null;
				}
				return currentStylusDevice.Target;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000521E8 File Offset: 0x000515E8
		internal override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				base.VerifyAccess();
				StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
				if (currentStylusDevice == null)
				{
					return null;
				}
				return currentStylusDevice.ActiveSource;
			}
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00052214 File Offset: 0x00051614
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}({1})", new object[]
			{
				base.ToString(),
				base.Name
			});
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00052248 File Offset: 0x00051648
		internal override StylusDeviceCollection StylusDevices
		{
			get
			{
				base.VerifyAccess();
				return this._stylusDeviceCollection;
			}
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00052264 File Offset: 0x00051664
		[SecurityCritical]
		internal PenContext CreateContext(IntPtr hwnd, PenContexts contexts)
		{
			bool supportInRange = (base.TabletHardwareCapabilities & TabletHardwareCapabilities.HardProximity) > TabletHardwareCapabilities.None;
			bool isIntegrated = (base.TabletHardwareCapabilities & TabletHardwareCapabilities.Integrated) > TabletHardwareCapabilities.None;
			PenContextInfo penContextInfo = this._penThread.WorkerCreateContext(hwnd, this._tabletInfo.PimcTablet.Value);
			return new PenContext((penContextInfo.PimcContext != null) ? penContextInfo.PimcContext.Value : null, hwnd, contexts, supportInRange, isIntegrated, penContextInfo.ContextId, (penContextInfo.CommHandle != null) ? penContextInfo.CommHandle.Value : IntPtr.Zero, base.Id, penContextInfo.WispContextKey);
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000522F4 File Offset: 0x000516F4
		internal PenThread PenThread
		{
			[SecurityCritical]
			get
			{
				return this._penThread;
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00052308 File Offset: 0x00051708
		[SecurityCritical]
		internal void UpdateScreenMeasurements()
		{
			this._cancelSize = Size.Empty;
			this._doubleTapSize = Size.Empty;
			this._tabletInfo.SizeInfo = this._penThread.WorkerGetUpdatedSizes(this._tabletInfo.PimcTablet.Value);
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00052354 File Offset: 0x00051754
		internal override Size DoubleTapSize
		{
			get
			{
				return this._doubleTapSize;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00052368 File Offset: 0x00051768
		internal Size CancelSize
		{
			get
			{
				return this._cancelSize;
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0005237C File Offset: 0x0005177C
		internal void InvalidateSizeDeltas()
		{
			this._forceUpdateSizeDeltas = true;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00052390 File Offset: 0x00051790
		internal bool AreSizeDeltasValid()
		{
			return !this._doubleTapSize.IsEmpty && !this._cancelSize.IsEmpty;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000523BC File Offset: 0x000517BC
		[SecurityCritical]
		internal void UpdateSizeDeltas(StylusPointDescription description, WispLogic stylusLogic)
		{
			Size cancelSize = new Size((double)Math.Max(1, SafeSystemMetrics.DragDeltaX / 2), (double)Math.Max(1, SafeSystemMetrics.DragDeltaY / 2));
			Size doubleTapSize = new Size((double)Math.Max(1, SafeSystemMetrics.DoubleClickDeltaX / 2), (double)Math.Max(1, SafeSystemMetrics.DoubleClickDeltaY / 2));
			StylusPointPropertyInfo propertyInfo = description.GetPropertyInfo(StylusPointProperties.X);
			StylusPointPropertyInfo propertyInfo2 = description.GetPropertyInfo(StylusPointProperties.Y);
			uint propertyValue = TabletDeviceBase.GetPropertyValue(propertyInfo);
			uint propertyValue2 = TabletDeviceBase.GetPropertyValue(propertyInfo2);
			if (propertyValue != 0U && propertyValue2 != 0U)
			{
				this._cancelSize = new Size((double)((int)Math.Round(base.ScreenSize.Width * (double)stylusLogic.CancelDelta / propertyValue)), (double)((int)Math.Round(base.ScreenSize.Height * (double)stylusLogic.CancelDelta / propertyValue2)));
				this._cancelSize.Width = Math.Max(cancelSize.Width, this._cancelSize.Width);
				this._cancelSize.Height = Math.Max(cancelSize.Height, this._cancelSize.Height);
				this._doubleTapSize = new Size((double)((int)Math.Round(base.ScreenSize.Width * (double)stylusLogic.DoubleTapDelta / propertyValue)), (double)((int)Math.Round(base.ScreenSize.Height * (double)stylusLogic.DoubleTapDelta / propertyValue2)));
				this._doubleTapSize.Width = Math.Max(doubleTapSize.Width, this._doubleTapSize.Width);
				this._doubleTapSize.Height = Math.Max(doubleTapSize.Height, this._doubleTapSize.Height);
			}
			else
			{
				this._doubleTapSize = doubleTapSize;
				this._cancelSize = cancelSize;
			}
			this._forceUpdateSizeDeltas = false;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00052580 File Offset: 0x00051980
		[SecurityCritical]
		internal void DisposeOrDeferDisposal()
		{
			if (this.CanDispose)
			{
				if (Tablet.CurrentTabletDevice == base.TabletDevice)
				{
					StylusLogic.GetCurrentStylusLogicAs<WispLogic>().SelectStylusDevice(null, null, true);
				}
				StylusTraceLogger.LogDeviceDisconnect(this._tabletInfo.Id);
				SecurityCriticalDataClass<IPimcTablet2> pimcTablet = this._tabletInfo.PimcTablet;
				IPimcTablet2 pimcTablet2 = (pimcTablet != null) ? pimcTablet.Value : null;
				this._tabletInfo.PimcTablet = null;
				if (pimcTablet2 != null)
				{
					this.PenThread.WorkerReleaseTabletLocks(pimcTablet2, this._tabletInfo.WispTabletKey);
					Marshal.ReleaseComObject(pimcTablet2);
				}
				StylusDeviceCollection stylusDeviceCollection = this._stylusDeviceCollection;
				this._stylusDeviceCollection = null;
				if (stylusDeviceCollection != null)
				{
					stylusDeviceCollection.Dispose();
				}
				this._penThread = null;
				this._isDisposalPending = false;
				this._disposed = true;
				GC.SuppressFinalize(this);
				return;
			}
			this._isDisposalPending = true;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00052644 File Offset: 0x00051A44
		internal bool IsDisposalPending
		{
			get
			{
				return this._isDisposalPending;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00052658 File Offset: 0x00051A58
		internal bool CanDispose
		{
			get
			{
				return this._queuedEventCount == 0;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00052670 File Offset: 0x00051A70
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x00052684 File Offset: 0x00051A84
		internal int QueuedEventCount
		{
			get
			{
				return this._queuedEventCount;
			}
			set
			{
				this._queuedEventCount = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00052698 File Offset: 0x00051A98
		internal uint WispTabletKey
		{
			[SecurityCritical]
			get
			{
				return this._tabletInfo.WispTabletKey;
			}
		}

		// Token: 0x04000BFA RID: 3066
		[SecurityCritical]
		private PenThread _penThread;

		// Token: 0x04000BFB RID: 3067
		protected Size _cancelSize = Size.Empty;

		// Token: 0x04000BFC RID: 3068
		private StylusDeviceCollection _stylusDeviceCollection;

		// Token: 0x04000BFD RID: 3069
		private bool _isDisposalPending;

		// Token: 0x04000BFE RID: 3070
		private int _queuedEventCount;
	}
}
