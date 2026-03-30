using System;
using System.Collections.Generic;
using System.Security;
using MS.Win32;
using MS.Win32.Pointer;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002F0 RID: 752
	internal class PointerTabletDevice : TabletDeviceBase
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0005F5A8 File Offset: 0x0005E9A8
		internal PointerTabletDeviceInfo DeviceInfo
		{
			get
			{
				return this._deviceInfo;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0005F5BC File Offset: 0x0005E9BC
		internal IntPtr Device
		{
			get
			{
				return this._deviceInfo.Device;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0005F5D4 File Offset: 0x0005E9D4
		internal int DoubleTapDelta
		{
			[SecuritySafeCritical]
			get
			{
				if (this._tabletInfo.DeviceType != TabletDeviceType.Touch)
				{
					return StylusLogic.CurrentStylusLogic.StylusDoubleTapDelta;
				}
				return StylusLogic.CurrentStylusLogic.TouchDoubleTapDelta;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x0005F604 File Offset: 0x0005EA04
		internal int DoubleTapDeltaTime
		{
			[SecuritySafeCritical]
			get
			{
				if (this._tabletInfo.DeviceType != TabletDeviceType.Touch)
				{
					return StylusLogic.CurrentStylusLogic.StylusDoubleTapDeltaTime;
				}
				return StylusLogic.CurrentStylusLogic.TouchDoubleTapDeltaTime;
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0005F634 File Offset: 0x0005EA34
		[SecurityCritical]
		internal PointerTabletDevice(PointerTabletDeviceInfo deviceInfo) : base(deviceInfo)
		{
			this._deviceInfo = deviceInfo;
			this._tabletInfo = deviceInfo;
			this.UpdateSizeDeltas();
			this.BuildStylusDevices();
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0005F670 File Offset: 0x0005EA70
		[SecuritySafeCritical]
		private void BuildStylusDevices()
		{
			uint num = 0U;
			List<PointerStylusDevice> list = new List<PointerStylusDevice>();
			if (UnsafeNativeMethods.GetPointerDeviceCursors(this._deviceInfo.Device, ref num, null))
			{
				UnsafeNativeMethods.POINTER_DEVICE_CURSOR_INFO[] array = new UnsafeNativeMethods.POINTER_DEVICE_CURSOR_INFO[num];
				if (UnsafeNativeMethods.GetPointerDeviceCursors(this._deviceInfo.Device, ref num, array))
				{
					foreach (UnsafeNativeMethods.POINTER_DEVICE_CURSOR_INFO cursorInfo in array)
					{
						PointerStylusDevice pointerStylusDevice = new PointerStylusDevice(this, cursorInfo);
						this._stylusDeviceMap.Add(pointerStylusDevice.CursorId, pointerStylusDevice);
						list.Add(pointerStylusDevice);
					}
				}
			}
			this._stylusDevices = new StylusDeviceCollection(list.ToArray());
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0005F708 File Offset: 0x0005EB08
		[SecuritySafeCritical]
		internal void UpdateSizeDeltas()
		{
			Size size = new Size((double)Math.Max(1, SafeSystemMetrics.DragDeltaX / 2), (double)Math.Max(1, SafeSystemMetrics.DragDeltaY / 2));
			Size doubleTapSize = new Size((double)Math.Max(1, SafeSystemMetrics.DoubleClickDeltaX / 2), (double)Math.Max(1, SafeSystemMetrics.DoubleClickDeltaY / 2));
			StylusPointPropertyInfo propertyInfo = base.StylusPointDescription.GetPropertyInfo(StylusPointProperties.X);
			StylusPointPropertyInfo propertyInfo2 = base.StylusPointDescription.GetPropertyInfo(StylusPointProperties.Y);
			uint propertyValue = TabletDeviceBase.GetPropertyValue(propertyInfo);
			uint propertyValue2 = TabletDeviceBase.GetPropertyValue(propertyInfo2);
			if (propertyValue != 0U && propertyValue2 != 0U)
			{
				this._doubleTapSize = new Size((double)((int)Math.Round(base.ScreenSize.Width * (double)this.DoubleTapDelta / propertyValue)), (double)((int)Math.Round(base.ScreenSize.Height * (double)this.DoubleTapDelta / propertyValue2)));
				this._doubleTapSize.Width = Math.Max(doubleTapSize.Width, this._doubleTapSize.Width);
				this._doubleTapSize.Height = Math.Max(doubleTapSize.Height, this._doubleTapSize.Height);
			}
			else
			{
				this._doubleTapSize = doubleTapSize;
			}
			this._forceUpdateSizeDeltas = false;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0005F83C File Offset: 0x0005EC3C
		internal override Size DoubleTapSize
		{
			get
			{
				return this._doubleTapSize;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0005F850 File Offset: 0x0005EC50
		internal override StylusDeviceCollection StylusDevices
		{
			get
			{
				return this._stylusDevices;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0005F864 File Offset: 0x0005EC64
		internal override IInputElement Target
		{
			get
			{
				StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
				if (currentStylusDevice == null)
				{
					return null;
				}
				return currentStylusDevice.Target;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0005F884 File Offset: 0x0005EC84
		internal override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				StylusDevice currentStylusDevice = Stylus.CurrentStylusDevice;
				if (currentStylusDevice == null)
				{
					return null;
				}
				return currentStylusDevice.ActiveSource;
			}
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0005F8A4 File Offset: 0x0005ECA4
		internal PointerStylusDevice GetStylusByCursorId(uint cursorId)
		{
			PointerStylusDevice result = null;
			this._stylusDeviceMap.TryGetValue(cursorId, out result);
			return result;
		}

		// Token: 0x04000D09 RID: 3337
		private PointerTabletDeviceInfo _deviceInfo;

		// Token: 0x04000D0A RID: 3338
		private StylusDeviceCollection _stylusDevices;

		// Token: 0x04000D0B RID: 3339
		private Dictionary<uint, PointerStylusDevice> _stylusDeviceMap = new Dictionary<uint, PointerStylusDevice>();
	}
}
