using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Utility;
using MS.Win32;

namespace System.Windows.Interop
{
	// Token: 0x02000329 RID: 809
	internal sealed class HwndMouseInputProvider : DispatcherObject, IMouseInputProvider, IInputProvider, IDisposable
	{
		// Token: 0x06001B3A RID: 6970 RVA: 0x0006C2D8 File Offset: 0x0006B6D8
		[SecurityCritical]
		internal HwndMouseInputProvider(HwndSource source)
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
			this._setCursorState = HwndMouseInputProvider.SetCursorState.SetCursorNotReceived;
			this._haveCapture = false;
			this._queryCursorOperation = null;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0006C35C File Offset: 0x0006B75C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void Dispose()
		{
			if (this._site != null)
			{
				this.StopTracking(this._source.Value.CriticalHandle);
				try
				{
					if (this._source.Value.HasCapture)
					{
						SafeNativeMethods.ReleaseCapture();
					}
				}
				catch (Win32Exception)
				{
				}
				try
				{
					IntPtr capture = SafeNativeMethods.GetCapture();
					this.PossiblyDeactivate(capture, false);
				}
				catch (Win32Exception)
				{
				}
				this._site.Value.Dispose();
				this._site = null;
			}
			this._source = null;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0006C40C File Offset: 0x0006B80C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		bool IInputProvider.ProvidesInputForRootVisual(Visual v)
		{
			return this._source.Value.RootVisual == v;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0006C42C File Offset: 0x0006B82C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		void IInputProvider.NotifyDeactivate()
		{
			if (this._active)
			{
				this.StopTracking(this._source.Value.CriticalHandle);
				this._active = false;
			}
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0006C460 File Offset: 0x0006B860
		[SecurityTreatAsSafe]
		[SecurityCritical]
		bool IMouseInputProvider.SetCursor(Cursor cursor)
		{
			bool result = false;
			if (this._setCursorState != HwndMouseInputProvider.SetCursorState.SetCursorDisabled)
			{
				try
				{
					SafeNativeMethods.SetCursor(cursor.Handle);
					result = true;
				}
				catch (Win32Exception)
				{
				}
			}
			return result;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0006C4A8 File Offset: 0x0006B8A8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		bool IMouseInputProvider.CaptureMouse()
		{
			if (this._isDwmProcess)
			{
				return true;
			}
			bool flag = true;
			try
			{
				SafeNativeMethods.SetCapture(new HandleRef(this, this._source.Value.CriticalHandle));
				IntPtr capture = SafeNativeMethods.GetCapture();
				if (capture != this._source.Value.CriticalHandle)
				{
					flag = false;
				}
			}
			catch (Win32Exception)
			{
				flag = false;
			}
			if (flag)
			{
				this._haveCapture = true;
			}
			if (flag && !this._active)
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				flag = UnsafeNativeMethods.TryGetCursorPos(point);
				if (flag)
				{
					try
					{
						SafeNativeMethods.ScreenToClient(new HandleRef(this, this._source.Value.CriticalHandle), point);
					}
					catch (Win32Exception)
					{
						flag = false;
					}
					if (flag)
					{
						this.ReportInput(this._source.Value.CriticalHandle, InputMode.Foreground, this._msgTime, RawMouseActions.AbsoluteMove, point.x, point.y, 0);
					}
				}
			}
			return flag;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0006C5B4 File Offset: 0x0006B9B4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		void IMouseInputProvider.ReleaseMouseCapture()
		{
			this._haveCapture = false;
			if (this._isDwmProcess)
			{
				return;
			}
			try
			{
				SafeNativeMethods.ReleaseCapture();
			}
			catch (Win32Exception)
			{
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0006C5FC File Offset: 0x0006B9FC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		int IMouseInputProvider.GetIntermediatePoints(IInputElement relativeTo, Point[] points)
		{
			SecurityHelper.DemandUnmanagedCode();
			int num = -1;
			try
			{
				if (points != null && relativeTo != null)
				{
					DependencyObject containingVisual = InputElement.GetContainingVisual(relativeTo as DependencyObject);
					HwndSource hwndSource = PresentationSource.FromDependencyObject(containingVisual) as HwndSource;
					if (hwndSource != null)
					{
						int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(SM.CXVIRTUALSCREEN);
						int systemMetrics2 = UnsafeNativeMethods.GetSystemMetrics(SM.CYVIRTUALSCREEN);
						int systemMetrics3 = UnsafeNativeMethods.GetSystemMetrics(SM.XVIRTUALSCREEN);
						int systemMetrics4 = UnsafeNativeMethods.GetSystemMetrics(SM.YVIRTUALSCREEN);
						uint num2 = 1U;
						NativeMethods.MOUSEMOVEPOINT mousemovepoint = default(NativeMethods.MOUSEMOVEPOINT);
						NativeMethods.MOUSEMOVEPOINT[] array = new NativeMethods.MOUSEMOVEPOINT[64];
						mousemovepoint.x = this._latestMovePoint.x;
						mousemovepoint.y = this._latestMovePoint.y;
						mousemovepoint.time = 0;
						int mouseMovePointsEx = UnsafeNativeMethods.GetMouseMovePointsEx((uint)Marshal.SizeOf(mousemovepoint), ref mousemovepoint, array, 64, num2);
						if (mouseMovePointsEx == -1)
						{
							throw new Win32Exception();
						}
						num = 0;
						bool flag = true;
						int num3 = 0;
						while (num3 < mouseMovePointsEx && num < points.Length)
						{
							if (!flag)
							{
								goto IL_13D;
							}
							if (array[num3].time < this._latestMovePoint.time || (array[num3].time == this._latestMovePoint.time && array[num3].x == this._latestMovePoint.x && array[num3].y == this._latestMovePoint.y))
							{
								flag = false;
								goto IL_13D;
							}
							IL_2B1:
							num3++;
							continue;
							IL_13D:
							if (array[num3].time >= this._previousMovePoint.time && (array[num3].time != this._previousMovePoint.time || array[num3].x != this._previousMovePoint.x || array[num3].y != this._previousMovePoint.y))
							{
								Point point = new Point((double)array[num3].x, (double)array[num3].y);
								if (num2 != 1U)
								{
									if (num2 == 2U)
									{
										point.X = (point.X * (double)(systemMetrics - 1) - (double)(systemMetrics3 * 65536)) / (double)systemMetrics;
										point.Y = (point.Y * (double)(systemMetrics2 - 1) - (double)(systemMetrics4 * 65536)) / (double)systemMetrics2;
									}
								}
								else
								{
									if (point.X > 32767.0)
									{
										point.X -= 65536.0;
									}
									if (point.Y > 32767.0)
									{
										point.Y -= 65536.0;
									}
								}
								point = PointUtil.ScreenToClient(point, hwndSource);
								point = PointUtil.ClientToRoot(point, hwndSource);
								GeneralTransform generalTransform = hwndSource.RootVisual.TransformToDescendant(VisualTreeHelper.GetContainingVisual2D(containingVisual));
								if (generalTransform != null)
								{
									generalTransform.TryTransform(point, out point);
								}
								points[num++] = point;
								goto IL_2B1;
							}
							break;
						}
					}
				}
			}
			catch (Win32Exception)
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0006C8F0 File Offset: 0x0006BCF0
		[SecurityCritical]
		internal unsafe IntPtr FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			IntPtr zero = IntPtr.Zero;
			if (this._source == null || this._source.Value == null)
			{
				return zero;
			}
			this._msgTime = 0;
			try
			{
				this._msgTime = SafeNativeMethods.GetMessageTime();
			}
			catch (Win32Exception)
			{
			}
			if (msg == WindowMessage.WM_MOUSEQUERY)
			{
				if (!this._isDwmProcess)
				{
					this._isDwmProcess = true;
				}
				UnsafeNativeMethods.MOUSEQUERY* ptr = (UnsafeNativeMethods.MOUSEQUERY*)((void*)lParam);
				if (ptr->uMsg == 512U)
				{
					msg = (WindowMessage)ptr->uMsg;
					wParam = ptr->wParam;
					lParam = this.MakeLPARAM(ptr->ptX, ptr->ptY);
				}
			}
			if (msg <= WindowMessage.WM_SETCURSOR)
			{
				if (msg != WindowMessage.WM_CANCELMODE)
				{
					if (msg != WindowMessage.WM_SETCURSOR)
					{
						goto IL_4F3;
					}
				}
				else
				{
					try
					{
						if (this._source.Value.HasCapture)
						{
							SafeNativeMethods.ReleaseCapture();
						}
						goto IL_4F3;
					}
					catch (Win32Exception)
					{
						goto IL_4F3;
					}
				}
				if (this._queryCursorOperation == null)
				{
					this._queryCursorOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(delegate(object sender)
					{
						if (this._active)
						{
							Mouse.UpdateCursor();
						}
						this._queryCursorOperation = null;
						return null;
					}), null);
				}
				this._setCursorState = HwndMouseInputProvider.SetCursorState.SetCursorReceived;
				int num = NativeMethods.SignedLOWORD((int)lParam);
				if (num == 1)
				{
					handled = true;
				}
			}
			else if (msg != WindowMessage.WM_NCDESTROY)
			{
				switch (msg)
				{
				case WindowMessage.WM_MOUSEMOVE:
				{
					int x = NativeMethods.SignedLOWORD(lParam);
					int y = NativeMethods.SignedHIWORD(lParam);
					if (this._queryCursorOperation != null)
					{
						this._queryCursorOperation.Abort();
						this._queryCursorOperation = null;
					}
					if (this._haveCapture)
					{
						this._setCursorState = HwndMouseInputProvider.SetCursorState.SetCursorReceived;
					}
					else if (this._setCursorState == HwndMouseInputProvider.SetCursorState.SetCursorNotReceived)
					{
						this._setCursorState = HwndMouseInputProvider.SetCursorState.SetCursorDisabled;
					}
					else if (this._setCursorState == HwndMouseInputProvider.SetCursorState.SetCursorReceived)
					{
						this._setCursorState = HwndMouseInputProvider.SetCursorState.SetCursorNotReceived;
					}
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.AbsoluteMove, x, y, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_LBUTTONDOWN:
				case WindowMessage.WM_LBUTTONDBLCLK:
					break;
				case WindowMessage.WM_LBUTTONUP:
				{
					int x2 = NativeMethods.SignedLOWORD(lParam);
					int y2 = NativeMethods.SignedHIWORD(lParam);
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.Button1Release, x2, y2, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_RBUTTONDOWN:
				case WindowMessage.WM_RBUTTONDBLCLK:
				{
					int x3 = NativeMethods.SignedLOWORD(lParam);
					int y3 = NativeMethods.SignedHIWORD(lParam);
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.Button2Press, x3, y3, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_RBUTTONUP:
				{
					int x4 = NativeMethods.SignedLOWORD(lParam);
					int y4 = NativeMethods.SignedHIWORD(lParam);
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.Button2Release, x4, y4, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_MBUTTONDOWN:
				case WindowMessage.WM_MBUTTONDBLCLK:
				{
					int x5 = NativeMethods.SignedLOWORD(lParam);
					int y5 = NativeMethods.SignedHIWORD(lParam);
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.Button3Press, x5, y5, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_MBUTTONUP:
				{
					int x6 = NativeMethods.SignedLOWORD(lParam);
					int y6 = NativeMethods.SignedHIWORD(lParam);
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.Button3Release, x6, y6, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_MOUSEWHEEL:
				{
					int wheel = NativeMethods.SignedHIWORD(wParam);
					int x7 = NativeMethods.SignedLOWORD(lParam);
					int y7 = NativeMethods.SignedHIWORD(lParam);
					NativeMethods.POINT point = new NativeMethods.POINT(x7, y7);
					try
					{
						SafeNativeMethods.ScreenToClient(new HandleRef(this, hwnd), point);
						x7 = point.x;
						y7 = point.y;
						handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.VerticalWheelRotate, x7, y7, wheel);
						goto IL_4F3;
					}
					catch (Win32Exception)
					{
						goto IL_4F3;
					}
					break;
				}
				case WindowMessage.WM_XBUTTONDOWN:
				case WindowMessage.WM_XBUTTONDBLCLK:
				{
					int num2 = NativeMethods.SignedHIWORD(wParam);
					int x8 = NativeMethods.SignedLOWORD(lParam);
					int y8 = NativeMethods.SignedHIWORD(lParam);
					RawMouseActions actions = RawMouseActions.None;
					if (num2 == 1)
					{
						actions = RawMouseActions.Button4Press;
					}
					else if (num2 == 2)
					{
						actions = RawMouseActions.Button5Press;
					}
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, actions, x8, y8, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_XBUTTONUP:
				{
					int num3 = NativeMethods.SignedHIWORD(wParam);
					int x9 = NativeMethods.SignedLOWORD(lParam);
					int y9 = NativeMethods.SignedHIWORD(lParam);
					RawMouseActions actions2 = RawMouseActions.None;
					if (num3 == 1)
					{
						actions2 = RawMouseActions.Button4Release;
					}
					else if (num3 == 2)
					{
						actions2 = RawMouseActions.Button5Release;
					}
					handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, actions2, x9, y9, 0);
					goto IL_4F3;
				}
				case WindowMessage.WM_MOUSEHWHEEL:
				case (WindowMessage)527:
				case WindowMessage.WM_PARENTNOTIFY:
				case WindowMessage.WM_ENTERMENULOOP:
				case WindowMessage.WM_EXITMENULOOP:
				case WindowMessage.WM_NEXTMENU:
				case WindowMessage.WM_SIZING:
					goto IL_4F3;
				case WindowMessage.WM_CAPTURECHANGED:
					goto IL_413;
				default:
					if (msg != WindowMessage.WM_MOUSELEAVE)
					{
						goto IL_4F3;
					}
					this.StopTracking(hwnd);
					try
					{
						IntPtr capture = SafeNativeMethods.GetCapture();
						IntPtr criticalHandle = this._source.Value.CriticalHandle;
						if (capture != criticalHandle)
						{
							this.PossiblyDeactivate(capture, false);
						}
						goto IL_4F3;
					}
					catch (Win32Exception)
					{
						goto IL_4F3;
					}
					goto IL_413;
				}
				int x10 = NativeMethods.SignedLOWORD(lParam);
				int y10 = NativeMethods.SignedHIWORD(lParam);
				handled = this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.Button1Press, x10, y10, 0);
				goto IL_4F3;
				IL_413:
				if (lParam != this._source.Value.CriticalHandle)
				{
					this._haveCapture = false;
					if (this._setCursorState == HwndMouseInputProvider.SetCursorState.SetCursorReceived)
					{
						this._setCursorState = HwndMouseInputProvider.SetCursorState.SetCursorNotReceived;
					}
					if (!this.IsOurWindow(lParam) && this._active)
					{
						this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawMouseActions.CancelCapture, 0, 0, 0);
					}
					if (lParam != IntPtr.Zero || !this._tracking)
					{
						this.PossiblyDeactivate(lParam, true);
					}
				}
			}
			else
			{
				this.Dispose();
			}
			IL_4F3:
			if (handled && EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info))
			{
				int num4 = 0;
				if (this._source != null && !this._source.Value.IsDisposed && this._source.Value.CompositionTarget != null)
				{
					num4 = this._source.Value.CompositionTarget.Dispatcher.GetHashCode();
				}
				int num5 = (int)((long)wParam);
				int num6 = (int)((long)lParam);
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientInputMessage, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, new object[]
				{
					num4,
					hwnd.ToInt64(),
					msg,
					num5,
					num6
				});
			}
			return zero;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0006CF18 File Offset: 0x0006C318
		[SecurityCritical]
		private void PossiblyDeactivate(IntPtr hwndCapture, bool stillActiveIfOverSelf)
		{
			if (this._source == null || this._source.Value == null)
			{
				return;
			}
			if (this._isDwmProcess)
			{
				return;
			}
			IntPtr intPtr = hwndCapture;
			if (intPtr == IntPtr.Zero)
			{
				NativeMethods.POINT point = new NativeMethods.POINT();
				int n = 0;
				try
				{
					n = SafeNativeMethods.GetMessagePos();
				}
				catch (Win32Exception)
				{
				}
				point.x = NativeMethods.SignedLOWORD(n);
				point.y = NativeMethods.SignedHIWORD(n);
				try
				{
					intPtr = UnsafeNativeMethods.WindowFromPoint(point.x, point.y);
				}
				catch (Win32Exception)
				{
				}
				if (!stillActiveIfOverSelf && intPtr == this._source.Value.CriticalHandle)
				{
					intPtr = IntPtr.Zero;
				}
				if (intPtr != IntPtr.Zero)
				{
					try
					{
						NativeMethods.RECT effectiveClientRect = this.GetEffectiveClientRect(intPtr);
						SafeNativeMethods.ScreenToClient(new HandleRef(this, intPtr), point);
						if (point.x < effectiveClientRect.left || point.x >= effectiveClientRect.right || point.y < effectiveClientRect.top || point.y >= effectiveClientRect.bottom)
						{
							intPtr = IntPtr.Zero;
						}
					}
					catch (Win32Exception)
					{
					}
				}
			}
			bool flag = !this.IsOurWindow(intPtr);
			if (flag)
			{
				this.ReportInput(this._source.Value.CriticalHandle, InputMode.Foreground, this._msgTime, RawMouseActions.Deactivate, 0, 0, 0);
				return;
			}
			this._active = false;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0006D0A4 File Offset: 0x0006C4A4
		[SecurityCritical]
		private NativeMethods.RECT GetEffectiveClientRect(IntPtr hwnd)
		{
			NativeMethods.RECT result = default(NativeMethods.RECT);
			if (CoreAppContextSwitches.OptOutOfMoveToChromedWindowFix && !CoreAppContextSwitches.DoNotOptOutOfMoveToChromedWindowFix)
			{
				SafeNativeMethods.GetClientRect(new HandleRef(this, hwnd), ref result);
				return result;
			}
			HwndSource hwndSource;
			if (!this.IsOurWindowImpl(hwnd, out hwndSource))
			{
				return result;
			}
			if (this.HasCustomChrome(hwndSource, ref result))
			{
				return result;
			}
			SafeNativeMethods.GetClientRect(new HandleRef(this, hwnd), ref result);
			return result;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0006D100 File Offset: 0x0006C500
		[SecurityCritical]
		private bool HasCustomChrome(HwndSource hwndSource, ref NativeMethods.RECT rcClient)
		{
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			if (!this.EnsureFrameworkAccessors(hwndSource))
			{
				return false;
			}
			DependencyObject rootVisual = hwndSource.RootVisual;
			DependencyObject dependencyObject = ((rootVisual != null) ? rootVisual.GetValue(HwndMouseInputProvider.WindowChromeWorkerProperty) : null) as DependencyObject;
			if (dependencyObject == null)
			{
				return false;
			}
			object[] array = new object[]
			{
				rcClient
			};
			if ((bool)HwndMouseInputProvider.GetEffectiveClientAreaMI.Invoke(dependencyObject, array))
			{
				rcClient = (NativeMethods.RECT)array[0];
				return true;
			}
			return false;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0006D180 File Offset: 0x0006C580
		[SecurityCritical]
		private bool EnsureFrameworkAccessors(HwndSource hwndSource)
		{
			if (HwndMouseInputProvider.WindowChromeWorkerProperty != null)
			{
				return true;
			}
			Assembly presentationFrameworkFromHwndSource = this.GetPresentationFrameworkFromHwndSource(this._source.Value);
			if (presentationFrameworkFromHwndSource == null)
			{
				presentationFrameworkFromHwndSource = this.GetPresentationFrameworkFromHwndSource(hwndSource);
			}
			if (presentationFrameworkFromHwndSource == null)
			{
				return false;
			}
			Type type = presentationFrameworkFromHwndSource.GetType("System.Windows.Shell.WindowChromeWorker");
			FieldInfo fieldInfo = (type != null) ? type.GetField("WindowChromeWorkerProperty", BindingFlags.Static | BindingFlags.Public) : null;
			DependencyProperty dependencyProperty = ((fieldInfo != null) ? fieldInfo.GetValue(null) : null) as DependencyProperty;
			HwndMouseInputProvider.GetEffectiveClientAreaMI = ((type != null) ? type.GetMethod("GetEffectiveClientArea", BindingFlags.Instance | BindingFlags.NonPublic) : null);
			if (dependencyProperty != null && HwndMouseInputProvider.GetEffectiveClientAreaMI != null)
			{
				HwndMouseInputProvider.WindowChromeWorkerProperty = dependencyProperty;
			}
			return HwndMouseInputProvider.WindowChromeWorkerProperty != null;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0006D230 File Offset: 0x0006C630
		private Assembly GetPresentationFrameworkFromHwndSource(HwndSource hwndSource)
		{
			DependencyObject dependencyObject = (hwndSource != null) ? hwndSource.RootVisual : null;
			Type type = (dependencyObject != null) ? dependencyObject.GetType() : null;
			while (type != null && type.Assembly.FullName != "PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")
			{
				type = type.BaseType;
			}
			if (type == null)
			{
				return null;
			}
			return type.Assembly;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0006D28C File Offset: 0x0006C68C
		[SecurityCritical]
		private void StartTracking(IntPtr hwnd)
		{
			if (!this._tracking && !this._isDwmProcess)
			{
				this._tme.hwndTrack = hwnd;
				this._tme.dwFlags = 2;
				try
				{
					SafeNativeMethods.TrackMouseEvent(this._tme);
					this._tracking = true;
				}
				catch (Win32Exception)
				{
				}
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0006D2F8 File Offset: 0x0006C6F8
		[SecurityCritical]
		private void StopTracking(IntPtr hwnd)
		{
			if (this._tracking && !this._isDwmProcess)
			{
				this._tme.hwndTrack = hwnd;
				this._tme.dwFlags = -2147483646;
				try
				{
					SafeNativeMethods.TrackMouseEvent(this._tme);
					this._tracking = false;
				}
				catch (Win32Exception)
				{
				}
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0006D368 File Offset: 0x0006C768
		private IntPtr MakeLPARAM(int high, int low)
		{
			return (IntPtr)(high << 16 | (low & 65535));
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0006D388 File Offset: 0x0006C788
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool IsOurWindow(IntPtr hwnd)
		{
			HwndSource hwndSource;
			return this.IsOurWindowImpl(hwnd, out hwndSource);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0006D3A0 File Offset: 0x0006C7A0
		[SecurityCritical]
		private bool IsOurWindowImpl(IntPtr hwnd, out HwndSource hwndSource)
		{
			hwndSource = null;
			bool result;
			if (hwnd != IntPtr.Zero)
			{
				hwndSource = HwndSource.CriticalFromHwnd(hwnd);
				result = (hwndSource != null && hwndSource.Dispatcher == this._source.Value.Dispatcher);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006D3F4 File Offset: 0x0006C7F4
		[SecurityCritical]
		private bool ReportInput(IntPtr hwnd, InputMode mode, int timestamp, RawMouseActions actions, int x, int y, int wheel)
		{
			if (this._source == null || this._source.Value == null)
			{
				return false;
			}
			PresentationSource presentationSource = this._source.Value;
			CompositionTarget compositionTarget = presentationSource.CompositionTarget;
			if (this._site == null || presentationSource.IsDisposed || compositionTarget == null)
			{
				if (!this._active)
				{
					return false;
				}
				actions = RawMouseActions.Deactivate;
			}
			if ((actions & RawMouseActions.Deactivate) == RawMouseActions.Deactivate)
			{
				this.StopTracking(hwnd);
				this._active = false;
			}
			else if ((actions & RawMouseActions.CancelCapture) != RawMouseActions.CancelCapture)
			{
				if (!this._active && (actions & RawMouseActions.VerticalWheelRotate) == RawMouseActions.VerticalWheelRotate)
				{
					MouseDevice primaryMouseDevice = this._site.Value.CriticalInputManager.PrimaryMouseDevice;
					if (primaryMouseDevice != null && primaryMouseDevice.CriticalActiveSource != null)
					{
						presentationSource = primaryMouseDevice.CriticalActiveSource;
					}
				}
				else
				{
					if (!this._active)
					{
						IntPtr value = SafeNativeMethods.GetCapture();
						if (hwnd != value)
						{
							NativeMethods.POINT point = new NativeMethods.POINT();
							try
							{
								UnsafeNativeMethods.GetCursorPos(point);
							}
							catch (Win32Exception)
							{
							}
							try
							{
								value = UnsafeNativeMethods.WindowFromPoint(point.x, point.y);
							}
							catch (Win32Exception)
							{
							}
							if (hwnd != value)
							{
								return false;
							}
						}
						actions |= RawMouseActions.Activate;
						this._active = true;
						this._lastX = x;
						this._lastY = y;
					}
					this.StartTracking(hwnd);
					if ((actions & RawMouseActions.AbsoluteMove) == RawMouseActions.None)
					{
						if (x != this._lastX || y != this._lastY)
						{
							actions |= RawMouseActions.AbsoluteMove;
						}
					}
					else
					{
						this._lastX = x;
						this._lastY = y;
					}
					if ((actions & RawMouseActions.AbsoluteMove) != RawMouseActions.None)
					{
						this.RecordMouseMove(x, y, this._msgTime);
					}
					if ((actions & (RawMouseActions.Activate | RawMouseActions.AbsoluteMove)) != RawMouseActions.None)
					{
						try
						{
							int windowStyle = SafeNativeMethods.GetWindowStyle(new HandleRef(this, this._source.Value.CriticalHandle), true);
							if ((windowStyle & 4194304) == 4194304)
							{
								NativeMethods.RECT rect = default(NativeMethods.RECT);
								SafeNativeMethods.GetClientRect(new HandleRef(this, this._source.Value.Handle), ref rect);
								x = rect.right - x;
							}
						}
						catch (Win32Exception)
						{
						}
					}
				}
			}
			IntPtr extraInformation = IntPtr.Zero;
			try
			{
				extraInformation = UnsafeNativeMethods.GetMessageExtraInfo();
			}
			catch (Win32Exception)
			{
			}
			RawMouseInputReport inputReport = new RawMouseInputReport(mode, timestamp, presentationSource, actions, x, y, wheel, extraInformation);
			return this._site.Value.ReportInput(inputReport);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0006D68C File Offset: 0x0006CA8C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void RecordMouseMove(int x, int y, int timestamp)
		{
			Point pointClient = new Point((double)x, (double)y);
			pointClient = PointUtil.ClientToScreen(pointClient, this._source.Value);
			this._previousMovePoint = this._latestMovePoint;
			this._latestMovePoint.x = ((int)pointClient.X & 65535);
			this._latestMovePoint.y = ((int)pointClient.Y & 65535);
			this._latestMovePoint.time = timestamp;
		}

		// Token: 0x04000E96 RID: 3734
		private SecurityCriticalDataClass<HwndSource> _source;

		// Token: 0x04000E97 RID: 3735
		private SecurityCriticalDataClass<InputProviderSite> _site;

		// Token: 0x04000E98 RID: 3736
		private int _msgTime;

		// Token: 0x04000E99 RID: 3737
		private NativeMethods.MOUSEMOVEPOINT _latestMovePoint;

		// Token: 0x04000E9A RID: 3738
		private NativeMethods.MOUSEMOVEPOINT _previousMovePoint;

		// Token: 0x04000E9B RID: 3739
		private int _lastX;

		// Token: 0x04000E9C RID: 3740
		private int _lastY;

		// Token: 0x04000E9D RID: 3741
		private bool _tracking;

		// Token: 0x04000E9E RID: 3742
		private bool _active;

		// Token: 0x04000E9F RID: 3743
		private HwndMouseInputProvider.SetCursorState _setCursorState;

		// Token: 0x04000EA0 RID: 3744
		private bool _haveCapture;

		// Token: 0x04000EA1 RID: 3745
		private DispatcherOperation _queryCursorOperation;

		// Token: 0x04000EA2 RID: 3746
		private bool _isDwmProcess;

		// Token: 0x04000EA3 RID: 3747
		[SecurityCritical]
		private NativeMethods.TRACKMOUSEEVENT _tme = new NativeMethods.TRACKMOUSEEVENT();

		// Token: 0x04000EA4 RID: 3748
		private const string PresentationFrameworkAssemblyFullName = "PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04000EA5 RID: 3749
		private static DependencyProperty WindowChromeWorkerProperty;

		// Token: 0x04000EA6 RID: 3750
		private static MethodInfo GetEffectiveClientAreaMI;

		// Token: 0x0200084C RID: 2124
		private enum SetCursorState
		{
			// Token: 0x04002803 RID: 10243
			SetCursorNotReceived,
			// Token: 0x04002804 RID: 10244
			SetCursorReceived,
			// Token: 0x04002805 RID: 10245
			SetCursorDisabled
		}
	}
}
