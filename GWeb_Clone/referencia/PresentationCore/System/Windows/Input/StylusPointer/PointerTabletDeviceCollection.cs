using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Windows.Input.Tracing;
using MS.Win32;
using MS.Win32.Pointer;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002F1 RID: 753
	internal class PointerTabletDeviceCollection : TabletDeviceCollection
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0005F8C4 File Offset: 0x0005ECC4
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x0005F8D8 File Offset: 0x0005ECD8
		internal bool IsValid { get; private set; }

		// Token: 0x060017DA RID: 6106 RVA: 0x0005F8EC File Offset: 0x0005ECEC
		internal PointerTabletDevice GetByDeviceId(IntPtr deviceId)
		{
			PointerTabletDevice result = null;
			this._tabletDeviceMap.TryGetValue(deviceId, out result);
			return result;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0005F90C File Offset: 0x0005ED0C
		internal PointerStylusDevice GetStylusDeviceByCursorId(uint cursorId)
		{
			PointerStylusDevice result = null;
			foreach (PointerTabletDevice pointerTabletDevice in this._tabletDeviceMap.Values)
			{
				if ((result = pointerTabletDevice.GetStylusByCursorId(cursorId)) != null)
				{
					break;
				}
			}
			return result;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0005F97C File Offset: 0x0005ED7C
		[SecuritySafeCritical]
		internal void Refresh()
		{
			try
			{
				Dictionary<IntPtr, PointerTabletDevice> tabletDeviceMap = this._tabletDeviceMap;
				this._tabletDeviceMap = new Dictionary<IntPtr, PointerTabletDevice>();
				base.TabletDevices.Clear();
				uint num = 0U;
				this.IsValid = UnsafeNativeMethods.GetPointerDevices(ref num, null);
				if (this.IsValid)
				{
					UnsafeNativeMethods.POINTER_DEVICE_INFO[] array = new UnsafeNativeMethods.POINTER_DEVICE_INFO[num];
					this.IsValid = UnsafeNativeMethods.GetPointerDevices(ref num, array);
					if (this.IsValid)
					{
						foreach (UnsafeNativeMethods.POINTER_DEVICE_INFO pointer_DEVICE_INFO in array)
						{
							int id = NativeMethods.IntPtrToInt32(pointer_DEVICE_INFO.device);
							PointerTabletDeviceInfo pointerTabletDeviceInfo = new PointerTabletDeviceInfo(id, pointer_DEVICE_INFO);
							if (pointerTabletDeviceInfo.TryInitialize())
							{
								PointerTabletDevice pointerTabletDevice = new PointerTabletDevice(pointerTabletDeviceInfo);
								if (!tabletDeviceMap.Remove(pointerTabletDevice.Device))
								{
									StylusTraceLogger.LogDeviceConnect(new StylusTraceLogger.StylusDeviceInfo(pointerTabletDevice.Id, pointerTabletDevice.Name, pointerTabletDevice.ProductId, pointerTabletDevice.TabletHardwareCapabilities, pointerTabletDevice.TabletSize, pointerTabletDevice.ScreenSize, pointerTabletDevice.Type, pointerTabletDevice.StylusDevices.Count));
								}
								this._tabletDeviceMap[pointerTabletDevice.Device] = pointerTabletDevice;
								base.TabletDevices.Add(pointerTabletDevice.TabletDevice);
							}
						}
					}
					foreach (PointerTabletDevice pointerTabletDevice2 in tabletDeviceMap.Values)
					{
						StylusTraceLogger.LogDeviceDisconnect(pointerTabletDevice2.Id);
					}
				}
			}
			catch (Win32Exception)
			{
				this.IsValid = false;
			}
		}

		// Token: 0x04000D0C RID: 3340
		private Dictionary<IntPtr, PointerTabletDevice> _tabletDeviceMap = new Dictionary<IntPtr, PointerTabletDevice>();
	}
}
