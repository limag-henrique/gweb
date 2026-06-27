using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;
using System.Windows.Input.StylusPointer;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Win32;
using MS.Win32.Pointer;

namespace System.Windows.Interop
{
	// Token: 0x0200032F RID: 815
	internal sealed class HwndPointerInputProvider : DispatcherObject, IStylusInputProvider, IInputProvider, IDisposable
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000705D4 File Offset: 0x0006F9D4
		// (set) Token: 0x06001BCA RID: 7114 RVA: 0x000705E8 File Offset: 0x0006F9E8
		internal bool IsWindowEnabled { get; private set; }

		// Token: 0x06001BCB RID: 7115 RVA: 0x000705FC File Offset: 0x0006F9FC
		[SecurityCritical]
		internal HwndPointerInputProvider(HwndSource source)
		{
			new UIPermission(PermissionState.Unrestricted).Assert();
			try
			{
				this._site = new SecurityCriticalDataClass<InputProviderSite>(InputManager.Current.RegisterInputProvider(this));
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			this._source = new SecurityCriticalDataClass<HwndSource>(source);
			this._pointerLogic = new SecurityCriticalDataClass<PointerLogic>(StylusLogic.GetCurrentStylusLogicAs<PointerLogic>());
			this._pointerLogic.Value.PlugInManagers[this._source.Value] = new PointerStylusPlugInManager(this._source.Value);
			int windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, source.CriticalHandle), -16);
			this.IsWindowEnabled = ((windowLong & 134217728) == 0);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x000706C4 File Offset: 0x0006FAC4
		[SecurityCritical]
		~HwndPointerInputProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00070700 File Offset: 0x0006FB00
		[SecurityCritical]
		private void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				if (this._site != null)
				{
					this._site.Value.Dispose();
					this._site = null;
				}
				this._pointerLogic.Value.PlugInManagers.Remove(this._source.Value);
			}
			this._disposed = true;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00070760 File Offset: 0x0006FB60
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0007077C File Offset: 0x0006FB7C
		private uint GetPointerId(IntPtr wParam)
		{
			return (uint)NativeMethods.SignedLOWORD(wParam);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00070790 File Offset: 0x0006FB90
		[SecurityCritical]
		private int[] GenerateRawStylusData(PointerData pointerData, PointerTabletDevice tabletDevice)
		{
			int num = tabletDevice.DeviceInfo.SupportedPointerProperties.Length;
			int[] array = new int[(long)num * (long)((ulong)pointerData.Info.historyCount)];
			int[] array2 = new int[0];
			if (UnsafeNativeMethods.GetRawPointerDeviceData(pointerData.Info.pointerId, pointerData.Info.historyCount, (uint)num, tabletDevice.DeviceInfo.SupportedPointerProperties, array))
			{
				int num2;
				int num3;
				this.GetOriginOffsetsLogical(out num2, out num3);
				int num4 = tabletDevice.DeviceInfo.SupportedPointerProperties.Length - tabletDevice.DeviceInfo.SupportedButtonPropertyIndex;
				int num5 = (num4 > 0) ? (num - num4 + 1) : num;
				array2 = new int[(long)num5 * (long)((ulong)pointerData.Info.historyCount)];
				int i = 0;
				int num6 = array.Length - num;
				while (i < array2.Length)
				{
					Array.Copy(array, num6, array2, i, num5);
					array2[i + StylusPointDescription.RequiredXIndex] -= num2;
					array2[i + StylusPointDescription.RequiredYIndex] -= num3;
					if (num4 > 0)
					{
						int num7 = i + num5 - 1;
						array2[num7] = 0;
						for (int j = tabletDevice.DeviceInfo.SupportedButtonPropertyIndex; j < num; j++)
						{
							int num8 = array[num6 + j] << j - tabletDevice.DeviceInfo.SupportedButtonPropertyIndex;
							array2[num7] |= num8;
						}
					}
					i += num5;
					num6 -= num;
				}
			}
			return array2;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x000708EC File Offset: 0x0006FCEC
		[SecurityCritical]
		private bool ProcessMessage(uint pointerId, RawStylusActions action, int timestamp)
		{
			bool result = false;
			PointerData pointerData = new PointerData(pointerId);
			if (pointerData.IsValid && (pointerData.Info.pointerType == UnsafeNativeMethods.POINTER_INPUT_TYPE.PT_TOUCH || pointerData.Info.pointerType == UnsafeNativeMethods.POINTER_INPUT_TYPE.PT_PEN))
			{
				uint cursorId = 0U;
				if (UnsafeNativeMethods.GetPointerCursorId(pointerId, ref cursorId))
				{
					IntPtr sourceDevice = pointerData.Info.sourceDevice;
					if (!this.UpdateCurrentTabletAndStylus(sourceDevice, cursorId))
					{
						return false;
					}
					if (action == RawStylusActions.Move && !pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_INCONTACT) && pointerData.Info.pointerFlags.HasFlag(UnsafeNativeMethods.POINTER_FLAGS.POINTER_FLAG_INRANGE))
					{
						action = RawStylusActions.InAirMove;
					}
					RawStylusInputReport rawStylusInputReport = new RawStylusInputReport(InputMode.Foreground, timestamp, this._source.Value, action, () => this._currentTabletDevice.StylusPointDescription, this._currentTabletDevice.Id, this._currentStylusDevice.Id, this.GenerateRawStylusData(pointerData, this._currentTabletDevice))
					{
						StylusDevice = this._currentStylusDevice.StylusDevice
					};
					PointerStylusPlugInManager pointerStylusPlugInManager;
					if (!this._pointerLogic.Value.InDragDrop && this.IsWindowEnabled && this._pointerLogic.Value.PlugInManagers.TryGetValue(this._source.Value, out pointerStylusPlugInManager))
					{
						pointerStylusPlugInManager.InvokeStylusPluginCollection(rawStylusInputReport);
					}
					this._currentStylusDevice.Update(this, this._source.Value, pointerData, rawStylusInputReport);
					this._currentStylusDevice.UpdateInteractions(rawStylusInputReport);
					InputReportEventArgs input = new InputReportEventArgs(this._currentStylusDevice.StylusDevice, rawStylusInputReport)
					{
						RoutedEvent = InputManager.PreviewInputReportEvent
					};
					InputManager.UnsecureCurrent.ProcessInput(input);
					result = !this._currentStylusDevice.IsPrimary;
				}
			}
			return result;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00070A94 File Offset: 0x0006FE94
		[SecuritySafeCritical]
		[UIPermission(SecurityAction.Assert, Window = UIPermissionWindow.AllWindows)]
		private void GetOriginOffsetsLogical(out int originOffsetX, out int originOffsetY)
		{
			Point point = this._source.Value.RootVisual.PointToScreen(new Point(0.0, 0.0));
			MatrixTransform matrixTransform = new MatrixTransform(this._currentTabletDevice.TabletToScreen);
			matrixTransform = (MatrixTransform)matrixTransform.Inverse;
			Point point2 = point * matrixTransform.Matrix;
			originOffsetX = (int)Math.Round(point2.X);
			originOffsetY = (int)Math.Round(point2.Y);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00070B18 File Offset: 0x0006FF18
		private bool UpdateCurrentTabletAndStylus(IntPtr deviceId, uint cursorId)
		{
			TabletDeviceCollection tabletDevices = Tablet.TabletDevices;
			PointerTabletDeviceCollection pointerTabletDeviceCollection = (tabletDevices != null) ? tabletDevices.As<PointerTabletDeviceCollection>() : null;
			if (!pointerTabletDeviceCollection.IsValid)
			{
				pointerTabletDeviceCollection.Refresh();
				if (!pointerTabletDeviceCollection.IsValid)
				{
					return false;
				}
			}
			this._currentTabletDevice = ((pointerTabletDeviceCollection != null) ? pointerTabletDeviceCollection.GetByDeviceId(deviceId) : null);
			PointerTabletDevice currentTabletDevice = this._currentTabletDevice;
			this._currentStylusDevice = ((currentTabletDevice != null) ? currentTabletDevice.GetStylusByCursorId(cursorId) : null);
			if (this._currentTabletDevice == null || this._currentStylusDevice == null)
			{
				pointerTabletDeviceCollection.Refresh();
				this._currentTabletDevice = ((pointerTabletDeviceCollection != null) ? pointerTabletDeviceCollection.GetByDeviceId(deviceId) : null);
				PointerTabletDevice currentTabletDevice2 = this._currentTabletDevice;
				this._currentStylusDevice = ((currentTabletDevice2 != null) ? currentTabletDevice2.GetStylusByCursorId(cursorId) : null);
				if (this._currentTabletDevice == null || this._currentStylusDevice == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00070BD0 File Offset: 0x0006FFD0
		[SecurityCritical]
		IntPtr IStylusInputProvider.FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			handled = false;
			if (PointerLogic.IsEnabled)
			{
				if (msg != WindowMessage.WM_ENABLE)
				{
					switch (msg)
					{
					case WindowMessage.WM_POINTERUPDATE:
						handled = this.ProcessMessage(this.GetPointerId(wParam), RawStylusActions.Move, Environment.TickCount);
						break;
					case WindowMessage.WM_POINTERDOWN:
						handled = this.ProcessMessage(this.GetPointerId(wParam), RawStylusActions.Down, Environment.TickCount);
						break;
					case WindowMessage.WM_POINTERUP:
						handled = this.ProcessMessage(this.GetPointerId(wParam), RawStylusActions.Up, Environment.TickCount);
						break;
					case WindowMessage.WM_POINTERENTER:
						handled = this.ProcessMessage(this.GetPointerId(wParam), RawStylusActions.InRange, Environment.TickCount);
						break;
					case WindowMessage.WM_POINTERLEAVE:
						handled = this.ProcessMessage(this.GetPointerId(wParam), RawStylusActions.OutOfRange, Environment.TickCount);
						break;
					}
				}
				else
				{
					this.IsWindowEnabled = (NativeMethods.IntPtrToInt32(wParam) == 1);
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00070CB0 File Offset: 0x000700B0
		public bool ProvidesInputForRootVisual(Visual v)
		{
			return false;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00070CC0 File Offset: 0x000700C0
		public void NotifyDeactivate()
		{
		}

		// Token: 0x04000EE0 RID: 3808
		private bool _disposed;

		// Token: 0x04000EE1 RID: 3809
		private SecurityCriticalDataClass<HwndSource> _source;

		// Token: 0x04000EE2 RID: 3810
		private SecurityCriticalDataClass<InputProviderSite> _site;

		// Token: 0x04000EE3 RID: 3811
		private SecurityCriticalDataClass<PointerLogic> _pointerLogic;

		// Token: 0x04000EE4 RID: 3812
		private PointerStylusDevice _currentStylusDevice;

		// Token: 0x04000EE5 RID: 3813
		private PointerTabletDevice _currentTabletDevice;
	}
}
