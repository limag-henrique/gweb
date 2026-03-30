using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Media;
using System.Windows.Media.Composition;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Automation;
using MS.Internal.Interop;
using MS.Internal.PresentationCore;
using MS.Utility;
using MS.Win32;

namespace System.Windows.Interop
{
	/// <summary>Representa uma associação a um identificador de janela que dá suporte à composição visual.</summary>
	// Token: 0x02000326 RID: 806
	public class HwndTarget : CompositionTarget
	{
		// Token: 0x06001ADE RID: 6878 RVA: 0x000692E8 File Offset: 0x000686E8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		static HwndTarget()
		{
			HwndTarget.s_updateWindowSettings = UnsafeNativeMethods.RegisterWindowMessage("UpdateWindowSettings");
			HwndTarget.s_needsRePresentOnWake = UnsafeNativeMethods.RegisterWindowMessage("NeedsRePresentOnWake");
			HwndTarget.s_DisplayDevicesAvailabilityChanged = UnsafeNativeMethods.RegisterWindowMessage("DisplayDevicesAvailabilityChanged");
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Interop.HwndTarget" /> usando o HWND especificado.</summary>
		/// <param name="hwnd">O identificador para a janela para a qual esse objeto desenha.</param>
		// Token: 0x06001ADF RID: 6879 RVA: 0x00069358 File Offset: 0x00068758
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		public HwndTarget(IntPtr hwnd)
		{
			bool flag = true;
			this._sessionId = SafeNativeMethods.GetCurrentSessionId();
			this._isSessionDisconnected = !SafeNativeMethods.IsCurrentSessionConnectStateWTSActive(this._sessionId, true);
			if (this._isSessionDisconnected)
			{
				this._needsRePresentOnWake = true;
			}
			this.AttachToHwnd(hwnd);
			try
			{
				if (EventTrace.IsEnabled(EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Info))
				{
					EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientCreateVisual, EventTrace.Keyword.KeywordGeneral, EventTrace.Level.Info, new object[]
					{
						base.Dispatcher.GetHashCode(),
						hwnd.ToInt64()
					});
				}
				this._hWnd = NativeMethods.HWND.Cast(hwnd);
				this.UpdateWindowAndClientCoordinates();
				this._lastWakeOrUnlockEvent = DateTime.MinValue;
				this.InitializeDpiAwarenessAndDpiScales();
				this._worldTransform = new MatrixTransform(new Matrix(this._currentDpiScale.DpiScaleX, 0.0, 0.0, this._currentDpiScale.DpiScaleY, 0.0, 0.0));
				MediaContext.RegisterICompositionTarget(base.Dispatcher, this);
				this._restoreDT = new DispatcherTimer();
				this._restoreDT.Tick += this.InvalidateSelf;
				this._restoreDT.Interval = TimeSpan.FromMilliseconds(100.0);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					HwndTarget.VisualTarget_DetachFromHwnd(hwnd);
				}
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000694F0 File Offset: 0x000688F0
		[SecuritySafeCritical]
		private void InitializeDpiAwarenessAndDpiScales()
		{
			object obj = HwndTarget.s_lockObject;
			lock (obj)
			{
				if (HwndTarget.AppManifestProcessDpiAwareness == null)
				{
					NativeMethods.PROCESS_DPI_AWARENESS value;
					NativeMethods.PROCESS_DPI_AWARENESS value2;
					HwndTarget.GetProcessDpiAwareness(this._hWnd, out value, out value2);
					HwndTarget.AppManifestProcessDpiAwareness = new NativeMethods.PROCESS_DPI_AWARENESS?(value);
					HwndTarget.ProcessDpiAwareness = new NativeMethods.PROCESS_DPI_AWARENESS?(value2);
					DpiUtil.UpdateUIElementCacheForSystemDpi(DpiUtil.GetSystemDpi());
				}
			}
			this.DpiAwarenessContext = (DpiAwarenessContextValue)DpiUtil.GetDpiAwarenessContext(this._hWnd);
			this._currentDpiScale = HwndTarget.GetDpiScaleForWindow(this._hWnd);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000695AC File Offset: 0x000689AC
		[SecuritySafeCritical]
		private static void GetProcessDpiAwareness(IntPtr hWnd, out NativeMethods.PROCESS_DPI_AWARENESS appManifestProcessDpiAwareness, out NativeMethods.PROCESS_DPI_AWARENESS processDpiAwareness)
		{
			appManifestProcessDpiAwareness = DpiUtil.GetProcessDpiAwareness(hWnd);
			if (HwndTarget.IsPerMonitorDpiScalingEnabled)
			{
				processDpiAwareness = appManifestProcessDpiAwareness;
				return;
			}
			processDpiAwareness = DpiUtil.GetLegacyProcessDpiAwareness();
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000695D4 File Offset: 0x000689D4
		private static DpiScale2 GetDpiScaleForWindow(IntPtr hWnd)
		{
			DpiScale2 dpiScale = null;
			if (HwndTarget.IsPerMonitorDpiScalingEnabled)
			{
				dpiScale = DpiUtil.GetWindowDpi(hWnd, false);
			}
			else if (HwndTarget.ProcessDpiAwareness != null)
			{
				bool? flag = HwndTarget.IsProcessSystemAware;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
				{
					dpiScale = DpiUtil.GetSystemDpiFromUIElementCache();
				}
				else
				{
					flag = HwndTarget.IsProcessUnaware;
					flag2 = true;
					if (flag.GetValueOrDefault() == flag2 & flag != null)
					{
						dpiScale = DpiScale2.FromPixelsPerInch(96.0, 96.0);
					}
				}
			}
			if (dpiScale == null)
			{
				switch (DpiUtil.GetLegacyProcessDpiAwareness())
				{
				case NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE:
					return DpiUtil.GetSystemDpi();
				case NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE:
					return HwndTarget.IsPerMonitorDpiScalingEnabled ? DpiUtil.GetWindowDpi(hWnd, false) : DpiUtil.GetSystemDpi();
				}
				dpiScale = DpiScale2.FromPixelsPerInch(96.0, 96.0);
			}
			return dpiScale;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000696B8 File Offset: 0x00068AB8
		[SecurityCritical]
		private static HandleRef NormalizeWindow(HandleRef hWnd, bool normalizeChildWindows, bool normalizePopups)
		{
			HandleRef handleRef = hWnd;
			object wrapper = hWnd.Wrapper;
			int num = (normalizeChildWindows ? 1073741824 : 0) | (normalizePopups ? int.MinValue : 0);
			int num2 = NativeMethods.IntPtrToInt32((IntPtr)SafeNativeMethods.GetWindowStyle(hWnd, false));
			if ((num2 & num) != 0)
			{
				IntPtr intPtr = IntPtr.Zero;
				do
				{
					try
					{
						intPtr = UnsafeNativeMethods.GetParent(handleRef);
					}
					catch (Win32Exception)
					{
						intPtr = UnsafeNativeMethods.GetWindow(handleRef, 4);
					}
					if (intPtr != IntPtr.Zero)
					{
						handleRef = new HandleRef(wrapper, intPtr);
					}
				}
				while (intPtr != IntPtr.Zero);
			}
			return handleRef;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006975C File Offset: 0x00068B5C
		[SecurityCritical]
		private void AttachToHwnd(IntPtr hwnd)
		{
			int num = 0;
			int windowThreadProcessId = UnsafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, hwnd), out num);
			if (!UnsafeNativeMethods.IsWindow(new HandleRef(this, hwnd)))
			{
				throw new ArgumentException(SR.Get("HwndTarget_InvalidWindowHandle"), "hwnd");
			}
			if (num != SafeNativeMethods.GetCurrentProcessId())
			{
				throw new ArgumentException(SR.Get("HwndTarget_InvalidWindowProcess"), "hwnd");
			}
			if (windowThreadProcessId != SafeNativeMethods.GetCurrentThreadId())
			{
				throw new ArgumentException(SR.Get("HwndTarget_InvalidWindowThread"), "hwnd");
			}
			int num2 = HwndTarget.VisualTarget_AttachToHwnd(hwnd);
			if (HRESULT.Failed(num2))
			{
				if (num2 == -2147024891)
				{
					throw new InvalidOperationException(SR.Get("HwndTarget_WindowAlreadyHasContent"));
				}
				HRESULT.Check(num2);
			}
			this.EnsureNotificationWindow();
			HwndTarget._notificationWindowHelper.AttachHwndTarget(this);
			UnsafeNativeMethods.WTSRegisterSessionNotification(hwnd, 0U);
		}

		// Token: 0x06001AE5 RID: 6885
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilVisualTarget_AttachToHwnd")]
		internal static extern int VisualTarget_AttachToHwnd(IntPtr hwnd);

		// Token: 0x06001AE6 RID: 6886
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll", EntryPoint = "MilVisualTarget_DetachFromHwnd")]
		internal static extern int VisualTarget_DetachFromHwnd(IntPtr hwnd);

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00069820 File Offset: 0x00068C20
		internal void InvalidateRenderMode()
		{
			RenderingMode renderingMode = (this.RenderMode == RenderMode.SoftwareOnly) ? RenderingMode.Software : RenderingMode.Default;
			if (MediaSystem.ForceSoftwareRendering)
			{
				if (renderingMode == RenderingMode.Hardware || renderingMode == RenderingMode.HardwareReference)
				{
					throw new InvalidOperationException(SR.Get("HwndTarget_HardwareNotSupportDueToProtocolMismatch"));
				}
				renderingMode = RenderingMode.Software;
			}
			bool? enableMultiMonitorDisplayClipping = CoreCompatibilityPreferences.EnableMultiMonitorDisplayClipping;
			if (enableMultiMonitorDisplayClipping != null)
			{
				renderingMode |= RenderingMode.IsDisableMultimonDisplayClippingValid;
				if (!enableMultiMonitorDisplayClipping.Value)
				{
					renderingMode |= RenderingMode.DisableMultimonDisplayClipping;
				}
			}
			if (MediaSystem.DisableDirtyRectangles)
			{
				renderingMode |= RenderingMode.DisableDirtyRectangles;
			}
			DUCE.ChannelSet channels = MediaContext.From(base.Dispatcher).GetChannels();
			DUCE.Channel channel = channels.Channel;
			DUCE.CompositionTarget.SetRenderingMode(this._compositionTarget.GetHandle(channel), (MILRTInitializationFlags)renderingMode, channel);
		}

		/// <summary>Obtém ou define o modo de renderização para a janela referenciada por esta <see cref="T:System.Windows.Interop.HwndTarget" />.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.Interop.RenderMode" /> que especifica o modo de renderização atual. O padrão é <see cref="F:System.Windows.Interop.RenderMode.Default" />.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000698C4 File Offset: 0x00068CC4
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x000698DC File Offset: 0x00068CDC
		public RenderMode RenderMode
		{
			get
			{
				return this._renderModePreference.Value;
			}
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				if (value != RenderMode.Default && value != RenderMode.SoftwareOnly)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(RenderMode));
				}
				this._renderModePreference.Value = value;
				this.InvalidateRenderMode();
			}
		}

		/// <summary>Libera todos os recursos usados pelo <see cref="T:System.Windows.Interop.HwndTarget" />.</summary>
		// Token: 0x06001AEA RID: 6890 RVA: 0x00069918 File Offset: 0x00068D18
		[SecurityCritical]
		public override void Dispose()
		{
			base.VerifyAccess();
			try
			{
				if (!base.IsDisposed)
				{
					this.RootVisual = null;
					HRESULT.Check(HwndTarget.VisualTarget_DetachFromHwnd(this._hWnd));
					MediaContext.UnregisterICompositionTarget(base.Dispatcher, this);
					if (HwndTarget._notificationWindowHelper != null && HwndTarget._notificationWindowHelper.DetachHwndTarget(this))
					{
						HwndTarget._notificationWindowHelper.Dispose();
						HwndTarget._notificationWindowHelper = null;
					}
					UnsafeNativeMethods.WTSUnRegisterSessionNotification(this._hWnd);
				}
			}
			finally
			{
				base.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x000699BC File Offset: 0x00068DBC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal override void CreateUCEResources(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			base.CreateUCEResources(channel, outOfBandChannel);
			bool flag = this._compositionTarget.CreateOrAddRefOnChannel(this, outOfBandChannel, DUCE.ResourceType.TYPE_HWNDRENDERTARGET);
			this._compositionTarget.DuplicateHandle(outOfBandChannel, channel);
			outOfBandChannel.CloseBatch();
			outOfBandChannel.Commit();
			DUCE.CompositionTarget.HwndInitialize(this._compositionTarget.GetHandle(channel), this._hWnd, this._hwndClientRectInScreenCoords.right - this._hwndClientRectInScreenCoords.left, this._hwndClientRectInScreenCoords.bottom - this._hwndClientRectInScreenCoords.top, MediaSystem.ForceSoftwareRendering, (int)this.DpiAwarenessContext, this._currentDpiScale, channel);
			DUCE.ResourceHandle hTransform = ((DUCE.IResource)this._worldTransform).AddRefOnChannel(channel);
			DUCE.CompositionNode.SetTransform(this._contentRoot.GetHandle(channel), hTransform, channel);
			DUCE.CompositionTarget.SetClearColor(this._compositionTarget.GetHandle(channel), this._backgroundColor, channel);
			Rect rect = new Rect(0.0, 0.0, (double)((float)Math.Ceiling((double)(this._hwndClientRectInScreenCoords.right - this._hwndClientRectInScreenCoords.left))), (double)((float)Math.Ceiling((double)(this._hwndClientRectInScreenCoords.bottom - this._hwndClientRectInScreenCoords.top))));
			base.StateChangedCallback(new object[]
			{
				(CompositionTarget.HostStateFlags)3U,
				this._worldTransform.Matrix,
				rect
			});
			DUCE.CompositionTarget.SetRoot(this._compositionTarget.GetHandle(channel), this._contentRoot.GetHandle(channel), channel);
			this._disableCookie = 0;
			DUCE.ChannelSet value;
			value.Channel = channel;
			value.OutOfBandChannel = outOfBandChannel;
			this.UpdateWindowSettings(this._isRenderTargetEnabled, new DUCE.ChannelSet?(value));
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00069B64 File Offset: 0x00068F64
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal override void ReleaseUCEResources(DUCE.Channel channel, DUCE.Channel outOfBandChannel)
		{
			if (this._compositionTarget.IsOnChannel(channel))
			{
				DUCE.CompositionTarget.SetRoot(this._compositionTarget.GetHandle(channel), DUCE.ResourceHandle.Null, channel);
				this._compositionTarget.ReleaseOnChannel(channel);
			}
			if (this._compositionTarget.IsOnChannel(outOfBandChannel))
			{
				this._compositionTarget.ReleaseOnChannel(outOfBandChannel);
			}
			if (!((DUCE.IResource)this._worldTransform).GetHandle(channel).IsNull)
			{
				((DUCE.IResource)this._worldTransform).ReleaseOnChannel(channel);
			}
			base.ReleaseUCEResources(channel, outOfBandChannel);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00069BE8 File Offset: 0x00068FE8
		[SecurityCritical]
		private bool HandleDpiChangedMessage(IntPtr wParam, IntPtr lParam)
		{
			bool result = false;
			if (HwndTarget.IsPerMonitorDpiScalingEnabled)
			{
				HwndSource hwndSource = HwndSource.FromHwnd(this._hWnd);
				if (hwndSource != null)
				{
					DpiScale2 currentDpiScale = this._currentDpiScale;
					DpiScale2 dpiScale = DpiScale2.FromPixelsPerInch((double)NativeMethods.SignedLOWORD(wParam), (double)NativeMethods.SignedHIWORD(wParam));
					if (currentDpiScale != dpiScale)
					{
						NativeMethods.RECT rect = UnsafeNativeMethods.PtrToStructure<NativeMethods.RECT>(lParam);
						Rect suggestedRect = new Rect((double)rect.left, (double)rect.top, (double)rect.Width, (double)rect.Height);
						hwndSource.ChangeDpi(new HwndDpiChangedEventArgs(currentDpiScale, dpiScale, suggestedRect));
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00069C84 File Offset: 0x00069084
		[SecurityCritical]
		private bool HandleDpiChangedAfterParentMessage()
		{
			bool result = false;
			if (HwndTarget.IsPerMonitorDpiScalingEnabled)
			{
				DpiScale2 currentDpiScale = this._currentDpiScale;
				DpiScale2 dpiScaleForWindow = HwndTarget.GetDpiScaleForWindow(this._hWnd);
				if (currentDpiScale != dpiScaleForWindow)
				{
					HwndSource hwndSource = HwndSource.FromHwnd(this._hWnd);
					if (hwndSource != null)
					{
						NativeMethods.RECT clientRect = SafeNativeMethods.GetClientRect(this._hWnd.MakeHandleRef(this));
						Rect suggestedRect = new Rect((double)clientRect.left, (double)clientRect.top, (double)(clientRect.right - clientRect.left), (double)(clientRect.bottom - clientRect.top));
						hwndSource.ChangeDpi(new HwndDpiChangedAfterParentEventArgs(currentDpiScale, dpiScaleForWindow, suggestedRect));
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00069D34 File Offset: 0x00069134
		[SecurityCritical]
		internal unsafe IntPtr HandleMessage(WindowMessage msg, IntPtr wparam, IntPtr lparam)
		{
			IntPtr result = HwndTarget.Unhandled;
			if (msg == HwndTarget.s_DisplayDevicesAvailabilityChanged)
			{
				this._displayDevicesAvailable = (wparam.ToInt32() != 0);
				if (this._displayDevicesAvailable && this._wasWmPaintProcessingDeferred)
				{
					UnsafeNativeMethods.InvalidateRect(this._hWnd.MakeHandleRef(this), IntPtr.Zero, true);
					this.DoPaint();
				}
			}
			else if (msg == HwndTarget.s_updateWindowSettings)
			{
				if (SafeNativeMethods.IsWindowVisible(this._hWnd.MakeHandleRef(this)))
				{
					this.UpdateWindowSettings(true);
				}
			}
			else if (msg == HwndTarget.s_needsRePresentOnWake)
			{
				bool flag = (DateTime.Now - this._lastWakeOrUnlockEvent).TotalSeconds < 10.0;
				bool flag2 = this._displayDevicesAvailable || MediaContext.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable;
				if (this._isSessionDisconnected || this._isSuspended || (this._hasRePresentedSinceWake && !flag) || !flag2)
				{
					this._needsRePresentOnWake = true;
				}
				else if (!this._hasRePresentedSinceWake || flag)
				{
					UnsafeNativeMethods.InvalidateRect(this._hWnd.MakeHandleRef(this), IntPtr.Zero, true);
					this.DoPaint();
					this._hasRePresentedSinceWake = true;
				}
				return HwndTarget.Handled;
			}
			if (base.IsDisposed)
			{
				return result;
			}
			if (msg <= WindowMessage.WM_WINDOWPOSCHANGED)
			{
				if (msg <= WindowMessage.WM_SHOWWINDOW)
				{
					if (msg <= WindowMessage.WM_PAINT)
					{
						if (msg != WindowMessage.WM_SIZE)
						{
							if (msg == WindowMessage.WM_PAINT)
							{
								if (this._displayDevicesAvailable || MediaContext.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable)
								{
									this._wasWmPaintProcessingDeferred = false;
									this.DoPaint();
									result = HwndTarget.Handled;
								}
								else
								{
									this._wasWmPaintProcessingDeferred = true;
								}
							}
						}
						else if (NativeMethods.IntPtrToInt32(wparam) != 1)
						{
							if (this._isMinimized)
							{
								this._restoreDT.Start();
							}
							this._isMinimized = false;
							this.DoPaint();
							this.OnResize();
						}
						else
						{
							this._isMinimized = true;
						}
					}
					else if (msg != WindowMessage.WM_ERASEBKGND)
					{
						if (msg == WindowMessage.WM_SHOWWINDOW)
						{
							bool flag3 = wparam != IntPtr.Zero;
							this.OnShowWindow(flag3);
							if (flag3)
							{
								this.DoPaint();
							}
						}
					}
					else
					{
						result = HwndTarget.Handled;
					}
				}
				else if (msg <= WindowMessage.WM_GETOBJECT)
				{
					if (msg != WindowMessage.WM_WININICHANGE)
					{
						if (msg == WindowMessage.WM_GETOBJECT)
						{
							result = HwndTarget.CriticalHandleWMGetobject(wparam, lparam, this.RootVisual, this._hWnd);
						}
					}
					else if (this.OnSettingChange(NativeMethods.IntPtrToInt32(wparam)))
					{
						UnsafeNativeMethods.InvalidateRect(this._hWnd.MakeHandleRef(this), IntPtr.Zero, true);
					}
				}
				else if (msg != WindowMessage.WM_WINDOWPOSCHANGING)
				{
					if (msg == WindowMessage.WM_WINDOWPOSCHANGED)
					{
						this.OnWindowPosChanged(lparam);
					}
				}
				else
				{
					this.OnWindowPosChanging(lparam);
				}
			}
			else if (msg <= WindowMessage.WM_POWERBROADCAST)
			{
				if (msg <= WindowMessage.WM_STYLECHANGED)
				{
					if (msg != WindowMessage.WM_STYLECHANGING)
					{
						if (msg == WindowMessage.WM_STYLECHANGED)
						{
							NativeMethods.STYLESTRUCT* ptr = (NativeMethods.STYLESTRUCT*)((void*)lparam);
							bool flag6;
							if ((int)wparam == -16)
							{
								bool flag4 = (ptr->styleOld & 1073741824) == 1073741824;
								bool flag5 = (ptr->styleNew & 1073741824) == 1073741824;
								flag6 = (flag4 != flag5);
							}
							else
							{
								bool flag7 = (ptr->styleOld & 4194304) == 4194304;
								bool flag8 = (ptr->styleNew & 4194304) == 4194304;
								flag6 = (flag7 != flag8);
							}
							if (flag6)
							{
								this.UpdateWindowSettings();
							}
						}
					}
					else
					{
						NativeMethods.STYLESTRUCT* ptr2 = (NativeMethods.STYLESTRUCT*)((void*)lparam);
						if ((int)wparam == -20)
						{
							if (this.UsesPerPixelOpacity)
							{
								ptr2->styleNew |= 524288;
							}
							else
							{
								ptr2->styleNew &= -524289;
							}
						}
					}
				}
				else if (msg != WindowMessage.WM_NCCREATE)
				{
					if (msg == WindowMessage.WM_POWERBROADCAST)
					{
						int num = NativeMethods.IntPtrToInt32(wparam);
						if (num != 4)
						{
							if (num - 6 <= 1 || num == 18)
							{
								this._isSuspended = false;
								if (this._needsRePresentOnWake)
								{
									UnsafeNativeMethods.InvalidateRect(this._hWnd.MakeHandleRef(this), IntPtr.Zero, true);
									this._needsRePresentOnWake = false;
								}
								this.DoPaint();
								this._lastWakeOrUnlockEvent = DateTime.Now;
							}
						}
						else
						{
							this._isSuspended = true;
							this._hasRePresentedSinceWake = false;
							this._lastWakeOrUnlockEvent = DateTime.MinValue;
						}
					}
				}
				else
				{
					bool? isProcessPerMonitorDpiAware = HwndTarget.IsProcessPerMonitorDpiAware;
					bool flag9 = true;
					if (isProcessPerMonitorDpiAware.GetValueOrDefault() == flag9 & isProcessPerMonitorDpiAware != null)
					{
						UnsafeNativeMethods.EnableNonClientDpiScaling(HwndTarget.NormalizeWindow(new HandleRef(this, this._hWnd), false, true));
					}
				}
			}
			else if (msg <= WindowMessage.WM_EXITSIZEMOVE)
			{
				if (msg != WindowMessage.WM_ENTERSIZEMOVE)
				{
					if (msg == WindowMessage.WM_EXITSIZEMOVE)
					{
						this.OnExitSizeMove();
					}
				}
				else
				{
					this.OnEnterSizeMove();
				}
			}
			else if (msg != WindowMessage.WM_WTSSESSION_CHANGE)
			{
				if (msg != WindowMessage.WM_DPICHANGED)
				{
					if (msg == WindowMessage.WM_DPICHANGED_AFTERPARENT)
					{
						result = (this.HandleDpiChangedAfterParentMessage() ? HwndTarget.Handled : HwndTarget.Unhandled);
					}
				}
				else
				{
					result = (this.HandleDpiChangedMessage(wparam, lparam) ? HwndTarget.Handled : HwndTarget.Unhandled);
				}
			}
			else if (this._sessionId == null || this._sessionId.Value == lparam.ToInt32())
			{
				switch (NativeMethods.IntPtrToInt32(wparam))
				{
				case 1:
				case 3:
				case 8:
					this._isSessionDisconnected = false;
					if (this._needsRePresentOnWake || this._wasWmPaintProcessingDeferred)
					{
						UnsafeNativeMethods.InvalidateRect(this._hWnd.MakeHandleRef(this), IntPtr.Zero, true);
						this._needsRePresentOnWake = false;
					}
					this.DoPaint();
					this._lastWakeOrUnlockEvent = DateTime.Now;
					break;
				case 2:
				case 4:
				case 7:
					this._hasRePresentedSinceWake = false;
					this._isSessionDisconnected = true;
					this._lastWakeOrUnlockEvent = DateTime.MinValue;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0006A2FC File Offset: 0x000696FC
		[SecurityCritical]
		private void OnMonitorPowerEvent(object sender, HwndTarget.MonitorPowerEventArgs eventArgs)
		{
			this.OnMonitorPowerEvent(sender, eventArgs.PowerOn, true);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0006A318 File Offset: 0x00069718
		[SecurityCritical]
		private void OnMonitorPowerEvent(object sender, bool powerOn, bool paintOnWake)
		{
			if (powerOn)
			{
				this._isSuspended = false;
				if (paintOnWake)
				{
					if (this._needsRePresentOnWake)
					{
						UnsafeNativeMethods.InvalidateRect(this._hWnd.MakeHandleRef(this), IntPtr.Zero, true);
						this._needsRePresentOnWake = false;
					}
					this.DoPaint();
				}
				this._lastWakeOrUnlockEvent = DateTime.Now;
				return;
			}
			this._isSuspended = true;
			this._hasRePresentedSinceWake = false;
			this._lastWakeOrUnlockEvent = DateTime.MinValue;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0006A384 File Offset: 0x00069784
		[SecuritySafeCritical]
		private void InvalidateSelf(object s, EventArgs args)
		{
			UnsafeNativeMethods.InvalidateRect(this._hWnd.MakeHandleRef(this), IntPtr.Zero, true);
			DispatcherTimer dispatcherTimer = (DispatcherTimer)s;
			if (dispatcherTimer != null)
			{
				dispatcherTimer.Stop();
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0006A3BC File Offset: 0x000697BC
		[SecurityCritical]
		private void DoPaint()
		{
			NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
			HandleRef handleRef = new HandleRef(this, this._hWnd);
			NativeMethods.HDC hdc;
			hdc.h = UnsafeNativeMethods.BeginPaint(handleRef, ref paintstruct);
			int windowLong = UnsafeNativeMethods.GetWindowLong(handleRef, -20);
			NativeMethods.RECT rcDirty = new NativeMethods.RECT(paintstruct.rcPaint_left, paintstruct.rcPaint_top, paintstruct.rcPaint_right, paintstruct.rcPaint_bottom);
			if (rcDirty.IsEmpty && (windowLong & 524288) != 0 && !UnsafeNativeMethods.GetLayeredWindowAttributes(this._hWnd.MakeHandleRef(this), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero) && !this._isSessionDisconnected && !this._isMinimized && (!this._isSuspended || UnsafeNativeMethods.GetSystemMetrics(SM.REMOTESESSION) != 0))
			{
				rcDirty = new NativeMethods.RECT(0, 0, this._hwndClientRectInScreenCoords.right - this._hwndClientRectInScreenCoords.left, this._hwndClientRectInScreenCoords.bottom - this._hwndClientRectInScreenCoords.top);
			}
			this.AdjustForRightToLeft(ref rcDirty, handleRef);
			if (!rcDirty.IsEmpty)
			{
				this.InvalidateRect(rcDirty);
			}
			UnsafeNativeMethods.EndPaint(this._hWnd.MakeHandleRef(this), ref paintstruct);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0006A4E0 File Offset: 0x000698E0
		[SecurityCritical]
		internal AutomationPeer EnsureAutomationPeer(Visual root)
		{
			return HwndTarget.EnsureAutomationPeer(root, this._hWnd);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0006A500 File Offset: 0x00069900
		[SecurityCritical]
		internal static AutomationPeer EnsureAutomationPeer(Visual root, IntPtr handle)
		{
			AutomationPeer automationPeer = null;
			if (root.CheckFlagsAnd(VisualFlags.IsUIElement))
			{
				UIElement uielement = (UIElement)root;
				automationPeer = UIElementAutomationPeer.CreatePeerForElement(uielement);
				if (automationPeer == null)
				{
					automationPeer = uielement.CreateGenericRootAutomationPeer();
				}
				if (automationPeer != null)
				{
					automationPeer.Hwnd = handle;
				}
			}
			if (automationPeer == null)
			{
				automationPeer = UIElementAutomationPeer.GetRootAutomationPeer(root, handle);
			}
			if (automationPeer != null)
			{
				automationPeer.AddToAutomationEventList();
			}
			return automationPeer;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0006A550 File Offset: 0x00069950
		[SecurityCritical]
		private static IntPtr CriticalHandleWMGetobject(IntPtr wparam, IntPtr lparam, Visual root, IntPtr handle)
		{
			IntPtr result;
			try
			{
				if (root == null)
				{
					result = IntPtr.Zero;
				}
				else
				{
					AutomationPeer automationPeer = HwndTarget.EnsureAutomationPeer(root, handle);
					if (automationPeer == null)
					{
						result = IntPtr.Zero;
					}
					else
					{
						IRawElementProviderSimple el = ElementProxy.StaticWrap(automationPeer, automationPeer);
						PermissionSet permissionSet = new PermissionSet(PermissionState.None);
						permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.SerializationFormatter | SecurityPermissionFlag.RemotingConfiguration));
						permissionSet.AddPermission(new DnsPermission(PermissionState.Unrestricted));
						permissionSet.AddPermission(new SocketPermission(PermissionState.Unrestricted));
						permissionSet.Assert();
						try
						{
							result = AutomationInteropProvider.ReturnRawElementProvider(handle, wparam, lparam, el);
						}
						finally
						{
							CodeAccessPermission.RevertAll();
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (CriticalExceptions.IsCriticalException(ex))
				{
					throw;
				}
				result = new IntPtr(Marshal.GetHRForException(ex));
			}
			return result;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0006A624 File Offset: 0x00069A24
		internal void AdjustForRightToLeft(ref NativeMethods.RECT rc, HandleRef handleRef)
		{
			int windowStyle = SafeNativeMethods.GetWindowStyle(handleRef, true);
			if ((windowStyle & 4194304) == 4194304)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				SafeNativeMethods.GetClientRect(handleRef, ref rect);
				int num = rc.right - rc.left;
				rc.right = rect.right - rc.left;
				rc.left = rc.right - num;
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0006A688 File Offset: 0x00069A88
		[SecurityCritical]
		private bool OnSettingChange(int firstParam)
		{
			if (firstParam == 75 || firstParam == 8203 || firstParam == 8205 || firstParam == 8211 || firstParam == 8213 || firstParam == 8215 || firstParam == 8217 || firstParam == 8219)
			{
				HRESULT.Check(MILUpdateSystemParametersInfo.Update());
				return true;
			}
			return false;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006A6E0 File Offset: 0x00069AE0
		private void InvalidateRect(NativeMethods.RECT rcDirty)
		{
			DUCE.ChannelSet channels = MediaContext.From(base.Dispatcher).GetChannels();
			DUCE.Channel channel = channels.Channel;
			DUCE.Channel outOfBandChannel = channels.OutOfBandChannel;
			if (this._compositionTarget.IsOnChannel(channel))
			{
				DUCE.CompositionTarget.Invalidate(this._compositionTarget.GetHandle(outOfBandChannel), ref rcDirty, outOfBandChannel);
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0006A730 File Offset: 0x00069B30
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void OnResize()
		{
			if (this._compositionTarget.IsOnAnyChannel)
			{
				MediaContext mediaContext = MediaContext.From(base.Dispatcher);
				this.UpdateWindowSettings();
				Rect rect = new Rect(0.0, 0.0, (double)((float)Math.Ceiling((double)(this._hwndClientRectInScreenCoords.right - this._hwndClientRectInScreenCoords.left))), (double)((float)Math.Ceiling((double)(this._hwndClientRectInScreenCoords.bottom - this._hwndClientRectInScreenCoords.top))));
				base.StateChangedCallback(new object[]
				{
					CompositionTarget.HostStateFlags.ClipBounds,
					null,
					rect
				});
				mediaContext.Resize(this);
				int windowLong = UnsafeNativeMethods.GetWindowLong(this._hWnd.MakeHandleRef(this), -16);
				if (this._userInputResize || this._usesPerPixelOpacity || ((windowLong & 1073741824) != 0 && Utilities.IsCompositionEnabled))
				{
					mediaContext.CompleteRender();
				}
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0006A818 File Offset: 0x00069C18
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void UpdateWindowAndClientCoordinates()
		{
			HandleRef hWnd = this._hWnd.MakeHandleRef(this);
			SafeNativeMethods.GetWindowRect(hWnd, ref this._hwndWindowRectInScreenCoords);
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			SafeNativeMethods.GetClientRect(hWnd, ref rect);
			NativeMethods.POINT point = new NativeMethods.POINT(rect.left, rect.top);
			UnsafeNativeMethods.ClientToScreen(hWnd, point);
			NativeMethods.POINT point2 = new NativeMethods.POINT(rect.right, rect.bottom);
			UnsafeNativeMethods.ClientToScreen(hWnd, point2);
			if (point2.x >= point.x)
			{
				this._hwndClientRectInScreenCoords.left = point.x;
				this._hwndClientRectInScreenCoords.right = point2.x;
			}
			else
			{
				this._hwndClientRectInScreenCoords.left = point2.x;
				this._hwndClientRectInScreenCoords.right = point.x;
			}
			if (point2.y >= point.y)
			{
				this._hwndClientRectInScreenCoords.top = point.y;
				this._hwndClientRectInScreenCoords.bottom = point2.y;
				return;
			}
			this._hwndClientRectInScreenCoords.top = point2.y;
			this._hwndClientRectInScreenCoords.bottom = point.y;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0006A928 File Offset: 0x00069D28
		private void UpdateWorldTransform(DpiScale2 dpiScale)
		{
			this._worldTransform = new MatrixTransform(new Matrix(dpiScale.DpiScaleX, 0.0, 0.0, dpiScale.DpiScaleY, 0.0, 0.0));
			DUCE.ChannelSet channels = MediaContext.From(base.Dispatcher).GetChannels();
			DUCE.Channel channel = channels.Channel;
			DUCE.ResourceHandle hTransform = ((DUCE.IResource)this._worldTransform).AddRefOnChannel(channel);
			DUCE.CompositionNode.SetTransform(this._contentRoot.GetHandle(channel), hTransform, channel);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0006A9B0 File Offset: 0x00069DB0
		private void PropagateDpiChangeToRootVisual(DpiScale2 oldDpi, DpiScale2 newDpi)
		{
			DpiFlags dpiFlags = DpiUtil.UpdateDpiScalesAndGetIndex(newDpi.PixelsPerInchX, newDpi.PixelsPerInchY);
			if (this.RootVisual != null)
			{
				this.RecursiveUpdateDpiFlagAndInvalidateMeasure(this.RootVisual, new DpiRecursiveChangeArgs(dpiFlags, oldDpi, newDpi));
			}
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006A9F8 File Offset: 0x00069DF8
		private void NotifyListenersOfWorldTransformAndClipBoundsChanged()
		{
			Rect rect = new Rect(0.0, 0.0, (double)(this._hwndClientRectInScreenCoords.right - this._hwndClientRectInScreenCoords.left), (double)(this._hwndClientRectInScreenCoords.bottom - this._hwndClientRectInScreenCoords.top));
			base.StateChangedCallback(new object[]
			{
				(CompositionTarget.HostStateFlags)3U,
				this._worldTransform.Matrix,
				rect
			});
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0006AA80 File Offset: 0x00069E80
		[SecurityCritical]
		internal void OnDpiChanged(HwndDpiChangedEventArgs e)
		{
			DpiScale2 currentDpiScale = this._currentDpiScale;
			DpiScale2 dpiScale = new DpiScale2(e.NewDpi);
			this._currentDpiScale = dpiScale;
			this.UpdateWorldTransform(dpiScale);
			this.PropagateDpiChangeToRootVisual(currentDpiScale, dpiScale);
			this.NotifyListenersOfWorldTransformAndClipBoundsChanged();
			this.NotifyRendererOfDpiChange(false);
			UnsafeNativeMethods.SetWindowPos(this._hWnd.MakeHandleRef(this), new HandleRef(null, IntPtr.Zero), (int)e.SuggestedRect.Left, (int)e.SuggestedRect.Top, (int)e.SuggestedRect.Width, (int)e.SuggestedRect.Height, 16388);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0006AB24 File Offset: 0x00069F24
		[SecurityCritical]
		internal void OnDpiChangedAfterParent(HwndDpiChangedAfterParentEventArgs e)
		{
			DpiScale2 currentDpiScale = this._currentDpiScale;
			DpiScale2 dpiScale = new DpiScale2(e.NewDpi);
			this._currentDpiScale = dpiScale;
			this.UpdateWorldTransform(dpiScale);
			this.PropagateDpiChangeToRootVisual(currentDpiScale, dpiScale);
			this.NotifyListenersOfWorldTransformAndClipBoundsChanged();
			this.NotifyRendererOfDpiChange(true);
			UnsafeNativeMethods.SetWindowPos(this._hWnd.MakeHandleRef(this), new HandleRef(null, IntPtr.Zero), (int)e.SuggestedRect.Left, (int)e.SuggestedRect.Top, (int)e.SuggestedRect.Width, (int)e.SuggestedRect.Height, 20);
			UnsafeNativeMethods.InvalidateRect(new HandleRef(this, this._hWnd), IntPtr.Zero, true);
			this.DoPaint();
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006ABE8 File Offset: 0x00069FE8
		private void NotifyRendererOfDpiChange(bool afterParent)
		{
			DUCE.ChannelSet channels = MediaContext.From(base.Dispatcher).GetChannels();
			DUCE.Channel channel = channels.Channel;
			DUCE.CompositionTarget.ProcessDpiChanged(this._compositionTarget.GetHandle(channel), this._currentDpiScale, afterParent, channel);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0006AC2C File Offset: 0x0006A02C
		private void RecursiveUpdateDpiFlagAndInvalidateMeasure(DependencyObject d, DpiRecursiveChangeArgs args)
		{
			int childrenCount = VisualTreeHelper.GetChildrenCount(d);
			for (int i = 0; i < childrenCount; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(d, i);
				if (child != null)
				{
					this.RecursiveUpdateDpiFlagAndInvalidateMeasure(child, args);
				}
			}
			Visual visual = d as Visual;
			if (visual != null)
			{
				visual.SetDpiScaleVisualFlags(args);
				UIElement uielement = d as UIElement;
				if (uielement != null)
				{
					uielement.InvalidateMeasure();
				}
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006AC84 File Offset: 0x0006A084
		[SecurityCritical]
		private void OnWindowPosChanging(IntPtr lParam)
		{
			this._windowPosChanging = true;
			this.UpdateWindowPos(lParam);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0006ACA0 File Offset: 0x0006A0A0
		[SecurityCritical]
		private void OnWindowPosChanged(IntPtr lParam)
		{
			this._windowPosChanging = false;
			this.UpdateWindowPos(lParam);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0006ACBC File Offset: 0x0006A0BC
		[SecurityCritical]
		private void UpdateWindowPos(IntPtr lParam)
		{
			NativeMethods.WINDOWPOS windowpos = (NativeMethods.WINDOWPOS)UnsafeNativeMethods.PtrToStructure(lParam, typeof(NativeMethods.WINDOWPOS));
			bool flag = (windowpos.flags & 2) == 0;
			bool flag2 = (windowpos.flags & 1) == 0;
			bool flag3 = flag || flag2;
			if (flag3)
			{
				if (!flag)
				{
					windowpos.x = (windowpos.y = 0);
				}
				if (!flag2)
				{
					windowpos.cx = (windowpos.cy = 0);
				}
				NativeMethods.RECT rect = new NativeMethods.RECT(windowpos.x, windowpos.y, windowpos.x + windowpos.cx, windowpos.y + windowpos.cy);
				IntPtr parent = UnsafeNativeMethods.GetParent(new HandleRef(null, windowpos.hwnd));
				if (parent != IntPtr.Zero)
				{
					SafeSecurityHelper.TransformLocalRectToScreen(new HandleRef(null, parent), ref rect);
				}
				if (!flag)
				{
					int num = rect.right - rect.left;
					int num2 = rect.bottom - rect.top;
					rect.left = this._hwndWindowRectInScreenCoords.left;
					rect.right = rect.left + num;
					rect.top = this._hwndWindowRectInScreenCoords.top;
					rect.bottom = rect.top + num2;
				}
				if (!flag2)
				{
					int num3 = this._hwndWindowRectInScreenCoords.right - this._hwndWindowRectInScreenCoords.left;
					int num4 = this._hwndWindowRectInScreenCoords.bottom - this._hwndWindowRectInScreenCoords.top;
					rect.right = rect.left + num3;
					rect.bottom = rect.top + num4;
				}
				flag3 = (this._hwndWindowRectInScreenCoords.left != rect.left || this._hwndWindowRectInScreenCoords.top != rect.top || this._hwndWindowRectInScreenCoords.right != rect.right || this._hwndWindowRectInScreenCoords.bottom != rect.bottom);
			}
			bool flag4 = SafeNativeMethods.IsWindowVisible(this._hWnd.MakeHandleRef(this));
			if (flag4 && (this._windowPosChanging && flag3))
			{
				flag4 = false;
			}
			if (flag3 || flag4 != this._isRenderTargetEnabled)
			{
				this.UpdateWindowSettings(flag4);
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0006AED8 File Offset: 0x0006A2D8
		private void OnShowWindow(bool enableRenderTarget)
		{
			if (enableRenderTarget != this._isRenderTargetEnabled)
			{
				this.UpdateWindowSettings(enableRenderTarget);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x0006AEF8 File Offset: 0x0006A2F8
		// (set) Token: 0x06001B08 RID: 6920 RVA: 0x0006AF0C File Offset: 0x0006A30C
		private static NativeMethods.PROCESS_DPI_AWARENESS? ProcessDpiAwareness { get; set; } = null;

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x0006AF20 File Offset: 0x0006A320
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0006AF34 File Offset: 0x0006A334
		private static NativeMethods.PROCESS_DPI_AWARENESS? AppManifestProcessDpiAwareness { get; set; } = null;

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x0006AF48 File Offset: 0x0006A348
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x0006AF5C File Offset: 0x0006A35C
		private DpiAwarenessContextValue DpiAwarenessContext { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0006AF70 File Offset: 0x0006A370
		internal static bool IsPerMonitorDpiScalingSupportedOnCurrentPlatform
		{
			get
			{
				return OSVersionHelper.IsOsWindows10RS1OrGreater;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x0006AF84 File Offset: 0x0006A384
		internal static bool IsPerMonitorDpiScalingEnabled
		{
			get
			{
				return !CoreAppContextSwitches.DoNotScaleForDpiChanges && HwndTarget.IsPerMonitorDpiScalingSupportedOnCurrentPlatform;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0006AFA0 File Offset: 0x0006A3A0
		internal static bool? IsProcessPerMonitorDpiAware
		{
			get
			{
				if (HwndTarget.ProcessDpiAwareness != null)
				{
					return new bool?(HwndTarget.ProcessDpiAwareness.Value == NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
				}
				return null;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x0006AFDC File Offset: 0x0006A3DC
		internal static bool? IsProcessSystemAware
		{
			get
			{
				if (HwndTarget.ProcessDpiAwareness != null)
				{
					return new bool?(HwndTarget.ProcessDpiAwareness.Value == NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE);
				}
				return null;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0006B018 File Offset: 0x0006A418
		internal static bool? IsProcessUnaware
		{
			get
			{
				if (HwndTarget.ProcessDpiAwareness != null)
				{
					return new bool?(HwndTarget.ProcessDpiAwareness.Value == NativeMethods.PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE);
				}
				return null;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0006B054 File Offset: 0x0006A454
		internal bool IsWindowPerMonitorDpiAware
		{
			get
			{
				return this.DpiAwarenessContext == DpiAwarenessContextValue.PerMonitorAware || this.DpiAwarenessContext == DpiAwarenessContextValue.PerMonitorAwareVersion2;
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0006B078 File Offset: 0x0006A478
		private void OnEnterSizeMove()
		{
			this._userInputResize = true;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0006B08C File Offset: 0x0006A48C
		private void OnExitSizeMove()
		{
			if (this._windowPosChanging)
			{
				this._windowPosChanging = false;
				this.UpdateWindowSettings(true);
			}
			this._userInputResize = false;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0006B0B8 File Offset: 0x0006A4B8
		private void UpdateWindowSettings()
		{
			this.UpdateWindowSettings(this._isRenderTargetEnabled, null);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0006B0DC File Offset: 0x0006A4DC
		private void UpdateWindowSettings(bool enableRenderTarget)
		{
			this.UpdateWindowSettings(enableRenderTarget, null);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0006B0FC File Offset: 0x0006A4FC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void UpdateWindowSettings(bool enableRenderTarget, DUCE.ChannelSet? channelSet)
		{
			MediaContext mediaContext = MediaContext.From(base.Dispatcher);
			bool flag = false;
			bool flag2 = false;
			if (this._isRenderTargetEnabled != enableRenderTarget)
			{
				this._isRenderTargetEnabled = enableRenderTarget;
				flag = !enableRenderTarget;
				flag2 = enableRenderTarget;
			}
			if (!this._compositionTarget.IsOnAnyChannel)
			{
				return;
			}
			this.UpdateWindowAndClientCoordinates();
			int windowLong = UnsafeNativeMethods.GetWindowLong(this._hWnd.MakeHandleRef(this), -16);
			int windowLong2 = UnsafeNativeMethods.GetWindowLong(this._hWnd.MakeHandleRef(this), -20);
			bool flag3 = (windowLong2 & 524288) != 0;
			bool isChild = (windowLong & 1073741824) != 0;
			bool isRTL = (windowLong2 & 4194304) != 0;
			int num = this._hwndClientRectInScreenCoords.right - this._hwndClientRectInScreenCoords.left;
			int num2 = this._hwndClientRectInScreenCoords.bottom - this._hwndClientRectInScreenCoords.top;
			MILTransparencyFlags miltransparencyFlags = MILTransparencyFlags.Opaque;
			if (this._usesPerPixelOpacity)
			{
				miltransparencyFlags |= MILTransparencyFlags.PerPixelAlpha;
			}
			if (!flag3 && miltransparencyFlags != MILTransparencyFlags.Opaque)
			{
				UnsafeNativeMethods.SetWindowLong(this._hWnd.MakeHandleRef(this), -20, new IntPtr(windowLong2 | 524288));
			}
			else if (flag3 && miltransparencyFlags == MILTransparencyFlags.Opaque)
			{
				UnsafeNativeMethods.SetWindowLong(this._hWnd.MakeHandleRef(this), -20, new IntPtr(windowLong2 & -524289));
			}
			else if (flag3 && miltransparencyFlags != MILTransparencyFlags.Opaque && this._isRenderTargetEnabled && (num == 0 || num2 == 0))
			{
				NativeMethods.BLENDFUNCTION blendfunction = default(NativeMethods.BLENDFUNCTION);
				blendfunction.BlendOp = 0;
				blendfunction.SourceConstantAlpha = 0;
				UnsafeNativeMethods.UpdateLayeredWindow(this._hWnd.h, IntPtr.Zero, null, null, IntPtr.Zero, null, 0, ref blendfunction, 2);
			}
			flag3 = (miltransparencyFlags > MILTransparencyFlags.Opaque);
			if (channelSet == null)
			{
				channelSet = new DUCE.ChannelSet?(mediaContext.GetChannels());
			}
			DUCE.Channel outOfBandChannel = channelSet.Value.OutOfBandChannel;
			if (flag2)
			{
				outOfBandChannel.Commit();
				outOfBandChannel.SyncFlush();
			}
			if (!this._isRenderTargetEnabled)
			{
				this._disableCookie++;
			}
			DUCE.Channel channel = channelSet.Value.Channel;
			DUCE.CompositionTarget.UpdateWindowSettings(this._isRenderTargetEnabled ? this._compositionTarget.GetHandle(channel) : this._compositionTarget.GetHandle(outOfBandChannel), this._hwndClientRectInScreenCoords, Colors.Transparent, 1f, flag3 ? (this._usesPerPixelOpacity ? MILWindowLayerType.ApplicationManagedLayer : MILWindowLayerType.SystemManagedLayer) : MILWindowLayerType.NotLayered, miltransparencyFlags, isChild, isRTL, this._isRenderTargetEnabled, this._disableCookie, this._isRenderTargetEnabled ? channel : outOfBandChannel);
			if (this._isRenderTargetEnabled)
			{
				mediaContext.PostRender();
				return;
			}
			if (flag)
			{
				outOfBandChannel.CloseBatch();
				outOfBandChannel.Commit();
				outOfBandChannel.SyncFlush();
			}
			UnsafeNativeMethods.PostMessage(new HandleRef(this, this._hWnd), HwndTarget.s_updateWindowSettings, IntPtr.Zero, IntPtr.Zero);
		}

		/// <summary>Obtém ou define o objeto visual raiz da página que é hospedada pela janela.</summary>
		/// <returns>O objeto visual raiz da página hospedado.</returns>
		// Token: 0x17000517 RID: 1303
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x0006B38C File Offset: 0x0006A78C
		public override Visual RootVisual
		{
			[SecurityCritical]
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			set
			{
				base.RootVisual = value;
				if (value != null)
				{
					bool? isProcessPerMonitorDpiAware = HwndTarget.IsProcessPerMonitorDpiAware;
					bool flag = true;
					if (isProcessPerMonitorDpiAware.GetValueOrDefault() == flag & isProcessPerMonitorDpiAware != null)
					{
						DpiFlags dpiFlags = DpiUtil.UpdateDpiScalesAndGetIndex(this._currentDpiScale.PixelsPerInchX, this._currentDpiScale.PixelsPerInchY);
						DpiScale newDpiScale = new DpiScale(UIElement.DpiScaleXValues[dpiFlags.Index], UIElement.DpiScaleYValues[dpiFlags.Index]);
						this.RootVisual.RecursiveSetDpiScaleVisualFlags(new DpiRecursiveChangeArgs(dpiFlags, this.RootVisual.GetDpi(), newDpiScale));
					}
					UnsafeNativeMethods.NotifyWinEvent(1879048191, this._hWnd.MakeHandleRef(this), 0, 0);
				}
			}
		}

		/// <summary>Obtém uma matriz que transforma as coordenadas desse destino para o dispositivo associado ao destino de renderização.</summary>
		/// <returns>A matriz de transformação.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0006B43C File Offset: 0x0006A83C
		public override Matrix TransformToDevice
		{
			get
			{
				base.VerifyAPIReadOnly();
				Matrix identity = Matrix.Identity;
				identity.Scale(this._currentDpiScale.DpiScaleX, this._currentDpiScale.DpiScaleY);
				return identity;
			}
		}

		/// <summary>Obtém uma matriz que transforma as coordenadas do dispositivo associado ao destino de renderização desse destino.</summary>
		/// <returns>A matriz de transformação.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0006B474 File Offset: 0x0006A874
		public override Matrix TransformFromDevice
		{
			get
			{
				base.VerifyAPIReadOnly();
				Matrix identity = Matrix.Identity;
				identity.Scale(1.0 / this._currentDpiScale.DpiScaleX, 1.0 / this._currentDpiScale.DpiScaleY);
				return identity;
			}
		}

		/// <summary>Obtém ou define a cor da tela de fundo da janela referenciada por este <see cref="T:System.Windows.Interop.HwndTarget" />.</summary>
		/// <returns>A cor do plano de fundo, como um <see cref="T:System.Windows.Media.Color" /> valor.</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0006B4C0 File Offset: 0x0006A8C0
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x0006B4DC File Offset: 0x0006A8DC
		public Color BackgroundColor
		{
			get
			{
				base.VerifyAPIReadOnly();
				return this._backgroundColor;
			}
			set
			{
				base.VerifyAPIReadWrite();
				if (this._backgroundColor != value)
				{
					this._backgroundColor = value;
					MediaContext mediaContext = MediaContext.From(base.Dispatcher);
					DUCE.ChannelSet channels = mediaContext.GetChannels();
					DUCE.Channel channel = channels.Channel;
					if (channel != null)
					{
						DUCE.CompositionTarget.SetClearColor(this._compositionTarget.GetHandle(channel), this._backgroundColor, channel);
						mediaContext.PostRender();
					}
				}
			}
		}

		/// <summary>Obtém um valor que declara se o valor de opacidade por pixel do conteúdo da janela de origem é usado para renderização.</summary>
		/// <returns>
		///   <see langword="true" /> Se usando a opacidade por pixel; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0006B540 File Offset: 0x0006A940
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x0006B55C File Offset: 0x0006A95C
		public bool UsesPerPixelOpacity
		{
			get
			{
				base.VerifyAPIReadOnly();
				return this._usesPerPixelOpacity;
			}
			internal set
			{
				base.VerifyAPIReadWrite();
				if (this._usesPerPixelOpacity != value)
				{
					this._usesPerPixelOpacity = value;
					this.UpdateWindowSettings();
				}
			}
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0006B588 File Offset: 0x0006A988
		private void EnsureNotificationWindow()
		{
			if (HwndTarget._notificationWindowHelper == null)
			{
				HwndTarget._notificationWindowHelper = new HwndTarget.NotificationWindowHelper();
			}
		}

		// Token: 0x04000E6A RID: 3690
		private static readonly object s_lockObject = new object();

		// Token: 0x04000E6B RID: 3691
		[SecurityCritical]
		private static WindowMessage s_updateWindowSettings;

		// Token: 0x04000E6C RID: 3692
		[SecurityCritical]
		private static WindowMessage s_needsRePresentOnWake;

		// Token: 0x04000E6D RID: 3693
		[SecurityCritical]
		private static WindowMessage s_DisplayDevicesAvailabilityChanged;

		// Token: 0x04000E6E RID: 3694
		private static readonly IntPtr Handled = new IntPtr(1);

		// Token: 0x04000E6F RID: 3695
		private static readonly IntPtr Unhandled = IntPtr.Zero;

		// Token: 0x04000E70 RID: 3696
		private MatrixTransform _worldTransform;

		// Token: 0x04000E71 RID: 3697
		private DpiScale2 _currentDpiScale;

		// Token: 0x04000E72 RID: 3698
		private SecurityCriticalDataForSet<RenderMode> _renderModePreference = new SecurityCriticalDataForSet<RenderMode>(RenderMode.Default);

		// Token: 0x04000E73 RID: 3699
		[SecurityCritical]
		private NativeMethods.HWND _hWnd;

		// Token: 0x04000E74 RID: 3700
		private NativeMethods.RECT _hwndClientRectInScreenCoords;

		// Token: 0x04000E75 RID: 3701
		private NativeMethods.RECT _hwndWindowRectInScreenCoords;

		// Token: 0x04000E76 RID: 3702
		private Color _backgroundColor = Color.FromRgb(0, 0, 0);

		// Token: 0x04000E77 RID: 3703
		private DUCE.MultiChannelResource _compositionTarget;

		// Token: 0x04000E78 RID: 3704
		private bool _isRenderTargetEnabled = true;

		// Token: 0x04000E79 RID: 3705
		private bool _usesPerPixelOpacity;

		// Token: 0x04000E7A RID: 3706
		private int _disableCookie;

		// Token: 0x04000E7B RID: 3707
		private bool _isMinimized;

		// Token: 0x04000E7C RID: 3708
		private bool _isSessionDisconnected;

		// Token: 0x04000E7D RID: 3709
		private bool _isSuspended;

		// Token: 0x04000E7E RID: 3710
		private bool _userInputResize;

		// Token: 0x04000E7F RID: 3711
		private bool _needsRePresentOnWake;

		// Token: 0x04000E80 RID: 3712
		private bool _hasRePresentedSinceWake;

		// Token: 0x04000E81 RID: 3713
		private bool _displayDevicesAvailable = MediaContext.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable;

		// Token: 0x04000E82 RID: 3714
		private bool _wasWmPaintProcessingDeferred;

		// Token: 0x04000E83 RID: 3715
		private int? _sessionId;

		// Token: 0x04000E84 RID: 3716
		private DateTime _lastWakeOrUnlockEvent;

		// Token: 0x04000E85 RID: 3717
		private const double _allowedPresentFailureDelay = 10.0;

		// Token: 0x04000E86 RID: 3718
		private DispatcherTimer _restoreDT;

		// Token: 0x04000E87 RID: 3719
		private bool _windowPosChanging;

		// Token: 0x04000E8B RID: 3723
		[ThreadStatic]
		private static HwndTarget.NotificationWindowHelper _notificationWindowHelper;

		// Token: 0x0200084A RID: 2122
		private class MonitorPowerEventArgs : EventArgs
		{
			// Token: 0x060056E3 RID: 22243 RVA: 0x00163CDC File Offset: 0x001630DC
			public MonitorPowerEventArgs(bool powerOn)
			{
				this.PowerOn = powerOn;
			}

			// Token: 0x170011E2 RID: 4578
			// (get) Token: 0x060056E4 RID: 22244 RVA: 0x00163CF8 File Offset: 0x001630F8
			// (set) Token: 0x060056E5 RID: 22245 RVA: 0x00163D0C File Offset: 0x0016310C
			public bool PowerOn { get; private set; }
		}

		// Token: 0x0200084B RID: 2123
		private class NotificationWindowHelper : IDisposable
		{
			// Token: 0x140001D4 RID: 468
			// (add) Token: 0x060056E6 RID: 22246 RVA: 0x00163D20 File Offset: 0x00163120
			// (remove) Token: 0x060056E7 RID: 22247 RVA: 0x00163D58 File Offset: 0x00163158
			public event EventHandler<HwndTarget.MonitorPowerEventArgs> MonitorPowerEvent;

			// Token: 0x060056E8 RID: 22248 RVA: 0x00163D90 File Offset: 0x00163190
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public unsafe NotificationWindowHelper()
			{
				if (Utilities.IsOSVistaOrNewer)
				{
					this._notificationHook = new HwndWrapperHook(this.NotificationFilterMessage);
					HwndWrapperHook[] hooks = new HwndWrapperHook[]
					{
						this._notificationHook
					};
					this._notificationHwnd = new HwndWrapper(0, 0, 0, 0, 0, 0, 0, "", IntPtr.Zero, hooks);
					Guid guid = new Guid(NativeMethods.GUID_MONITOR_POWER_ON.ToByteArray());
					this._hPowerNotify = UnsafeNativeMethods.RegisterPowerSettingNotification(this._notificationHwnd.Handle, &guid, 0);
				}
			}

			// Token: 0x060056E9 RID: 22249 RVA: 0x00163E20 File Offset: 0x00163220
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public void Dispose()
			{
				if (this._hPowerNotify != IntPtr.Zero)
				{
					UnsafeNativeMethods.UnregisterPowerSettingNotification(this._hPowerNotify);
					this._hPowerNotify = IntPtr.Zero;
				}
				this.MonitorPowerEvent = null;
				this._hwndTargetCount = 0;
				if (this._notificationHwnd != null)
				{
					this._notificationHwnd.Dispose();
					this._notificationHwnd = null;
				}
			}

			// Token: 0x060056EA RID: 22250 RVA: 0x00163E80 File Offset: 0x00163280
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public void AttachHwndTarget(HwndTarget hwndTarget)
			{
				this.MonitorPowerEvent += hwndTarget.OnMonitorPowerEvent;
				if (this._hwndTargetCount > 0)
				{
					hwndTarget.OnMonitorPowerEvent(null, this._monitorOn, false);
				}
				this._hwndTargetCount++;
			}

			// Token: 0x060056EB RID: 22251 RVA: 0x00163EC4 File Offset: 0x001632C4
			[SecurityTreatAsSafe]
			[SecurityCritical]
			public bool DetachHwndTarget(HwndTarget hwndTarget)
			{
				this.MonitorPowerEvent -= hwndTarget.OnMonitorPowerEvent;
				this._hwndTargetCount--;
				return this._hwndTargetCount == 0;
			}

			// Token: 0x060056EC RID: 22252 RVA: 0x00163EFC File Offset: 0x001632FC
			[SecurityCritical]
			private unsafe IntPtr NotificationFilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
			{
				IntPtr zero = IntPtr.Zero;
				if (msg == 536)
				{
					int num = NativeMethods.IntPtrToInt32(wParam);
					if (num == 32787)
					{
						NativeMethods.POWERBROADCAST_SETTING* ptr = (NativeMethods.POWERBROADCAST_SETTING*)((void*)lParam);
						if (ptr->PowerSetting == NativeMethods.GUID_MONITOR_POWER_ON)
						{
							if (ptr->Data == 0)
							{
								this._monitorOn = false;
							}
							else
							{
								this._monitorOn = true;
							}
							if (this.MonitorPowerEvent != null)
							{
								this.MonitorPowerEvent(null, new HwndTarget.MonitorPowerEventArgs(this._monitorOn));
							}
						}
					}
				}
				else
				{
					handled = false;
				}
				return zero;
			}

			// Token: 0x040027FC RID: 10236
			[SecurityCritical]
			private HwndWrapper _notificationHwnd;

			// Token: 0x040027FD RID: 10237
			[SecurityCritical]
			private HwndWrapperHook _notificationHook;

			// Token: 0x040027FE RID: 10238
			private int _hwndTargetCount;

			// Token: 0x04002800 RID: 10240
			private bool _monitorOn = true;

			// Token: 0x04002801 RID: 10241
			[SecurityCritical]
			private IntPtr _hPowerNotify;
		}
	}
}
