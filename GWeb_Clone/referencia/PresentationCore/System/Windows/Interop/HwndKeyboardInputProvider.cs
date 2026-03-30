using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Internal.PresentationCore;
using MS.Utility;
using MS.Win32;

namespace System.Windows.Interop
{
	// Token: 0x02000328 RID: 808
	internal sealed class HwndKeyboardInputProvider : DispatcherObject, IKeyboardInputProvider, IInputProvider, IDisposable
	{
		// Token: 0x06001B27 RID: 6951 RVA: 0x0006B74C File Offset: 0x0006AB4C
		[SecurityCritical]
		internal HwndKeyboardInputProvider(HwndSource source)
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
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0006B7B0 File Offset: 0x0006ABB0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public void Dispose()
		{
			if (this._site != null)
			{
				this._site.Value.Dispose();
				this._site = null;
			}
			this._source = null;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0006B7E4 File Offset: 0x0006ABE4
		public void OnRootChanged(Visual oldRoot, Visual newRoot)
		{
			if (this._active && newRoot != null)
			{
				Keyboard.Focus(null);
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0006B804 File Offset: 0x0006AC04
		[SecurityTreatAsSafe]
		[SecurityCritical]
		bool IInputProvider.ProvidesInputForRootVisual(Visual v)
		{
			return this._source.Value.RootVisual == v;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0006B824 File Offset: 0x0006AC24
		void IInputProvider.NotifyDeactivate()
		{
			this._active = false;
			this._partialActive = false;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0006B840 File Offset: 0x0006AC40
		[SecurityCritical]
		bool IKeyboardInputProvider.AcquireFocus(bool checkOnly)
		{
			bool result = false;
			try
			{
				if (!checkOnly)
				{
					this._acquiringFocusOurselves = true;
					this._restoreFocusWindow = IntPtr.Zero;
					this._restoreFocus = null;
				}
				HandleRef hWnd = new HandleRef(this, this._source.Value.CriticalHandle);
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				int windowLong = UnsafeNativeMethods.GetWindowLong(hWnd, -20);
				if ((windowLong & 134217728) == 134217728 || this._source.Value.IsInExclusiveMenuMode)
				{
					if (SafeNativeMethods.IsWindowEnabled(hWnd))
					{
						if (SecurityHelper.AppDomainGrantedUnrestrictedUIPermission)
						{
							result = (focus != IntPtr.Zero);
						}
						else
						{
							result = this.IsOurWindow(focus);
						}
					}
				}
				else
				{
					if (!checkOnly)
					{
						if (!this._active && focus == this._source.Value.CriticalHandle)
						{
							this.OnSetFocus(focus);
						}
						else
						{
							UnsafeNativeMethods.TrySetFocus(hWnd);
							focus = UnsafeNativeMethods.GetFocus();
						}
					}
					result = (focus == this._source.Value.CriticalHandle);
				}
			}
			catch (Win32Exception)
			{
			}
			finally
			{
				this._acquiringFocusOurselves = false;
			}
			return result;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0006B970 File Offset: 0x0006AD70
		[SecurityCritical]
		internal IntPtr FilterMessage(IntPtr hwnd, WindowMessage message, IntPtr wParam, IntPtr lParam, ref bool handled)
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
			if (message <= WindowMessage.WM_SYSDEADCHAR)
			{
				if (message != WindowMessage.WM_SETFOCUS)
				{
					if (message != WindowMessage.WM_KILLFOCUS)
					{
						switch (message)
						{
						case WindowMessage.WM_KEYFIRST:
						case WindowMessage.WM_SYSKEYDOWN:
							if (!this._source.Value.IsRepeatedKeyboardMessage(hwnd, (int)message, wParam, lParam))
							{
								try
								{
									int tickCount = SafeNativeMethods.GetTickCount();
								}
								catch (Win32Exception)
								{
								}
								HwndSource._eatCharMessages = true;
								DispatcherOperation dispatcherOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(HwndSource.RestoreCharMessages), null);
								base.Dispatcher.CriticalRequestProcessing(true);
								MSG msg = new MSG(hwnd, (int)message, wParam, lParam, this._msgTime, 0, 0);
								this.ProcessKeyAction(ref msg, ref handled);
								if (!handled)
								{
									HwndSource._eatCharMessages = false;
									dispatcherOperation.Abort();
								}
							}
							break;
						case WindowMessage.WM_KEYUP:
						case WindowMessage.WM_SYSKEYUP:
							if (!this._source.Value.IsRepeatedKeyboardMessage(hwnd, (int)message, wParam, lParam))
							{
								MSG msg2 = new MSG(hwnd, (int)message, wParam, lParam, this._msgTime, 0, 0);
								this.ProcessKeyAction(ref msg2, ref handled);
							}
							break;
						case WindowMessage.WM_CHAR:
						case WindowMessage.WM_DEADCHAR:
						case WindowMessage.WM_SYSCHAR:
						case WindowMessage.WM_SYSDEADCHAR:
							if (!this._source.Value.IsRepeatedKeyboardMessage(hwnd, (int)message, wParam, lParam) && !HwndSource._eatCharMessages)
							{
								this.ProcessTextInputAction(hwnd, message, wParam, lParam, ref handled);
							}
							break;
						}
					}
					else
					{
						if (this._active && wParam != this._source.Value.CriticalHandle)
						{
							if (this._source.Value.RestoreFocusMode == RestoreFocusMode.Auto)
							{
								this._restoreFocusWindow = this.GetImmediateChildFor(wParam, this._source.Value.CriticalHandle);
								this._restoreFocus = null;
								if (this._restoreFocusWindow == IntPtr.Zero)
								{
									DependencyObject dependencyObject = Keyboard.FocusedElement as DependencyObject;
									if (dependencyObject != null)
									{
										HwndSource hwndSource = PresentationSource.CriticalFromVisual(dependencyObject) as HwndSource;
										if (hwndSource == this._source.Value)
										{
											this._restoreFocus = (dependencyObject as IInputElement);
										}
									}
								}
							}
							this.PossiblyDeactivate(wParam);
						}
						handled = true;
					}
				}
				else
				{
					this.OnSetFocus(hwnd);
					handled = true;
				}
			}
			else if (message != WindowMessage.WM_UPDATEUISTATE)
			{
				if (message == WindowMessage.WM_EXITMENULOOP || message == WindowMessage.WM_EXITSIZEMOVE)
				{
					if (this._active)
					{
						this._partialActive = true;
						this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawKeyboardActions.Activate, 0, false, false, 0);
					}
				}
			}
			else
			{
				RawUIStateInputReport inputReport = new RawUIStateInputReport(this._source.Value, InputMode.Foreground, this._msgTime, (RawUIStateActions)NativeMethods.SignedLOWORD((int)wParam), (RawUIStateTargets)NativeMethods.SignedHIWORD((int)wParam));
				this._site.Value.ReportInput(inputReport);
				handled = true;
			}
			if (handled && EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info))
			{
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientInputMessage, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, new object[]
				{
					base.Dispatcher.GetHashCode(),
					hwnd.ToInt64(),
					message,
					(int)wParam,
					(int)lParam
				});
			}
			return zero;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0006BCF8 File Offset: 0x0006B0F8
		[SecurityCritical]
		private void OnSetFocus(IntPtr hwnd)
		{
			this._active = false;
			if (!this._active)
			{
				HwndSource value = this._source.Value;
				this.ReportInput(hwnd, InputMode.Foreground, this._msgTime, RawKeyboardActions.Activate, 0, false, false, 0);
				this._partialActive = true;
				if (!this._acquiringFocusOurselves && value.RestoreFocusMode == RestoreFocusMode.Auto)
				{
					if (this._restoreFocusWindow != IntPtr.Zero)
					{
						IntPtr restoreFocusWindow = this._restoreFocusWindow;
						this._restoreFocusWindow = IntPtr.Zero;
						UnsafeNativeMethods.TrySetFocus(new HandleRef(this, restoreFocusWindow), ref restoreFocusWindow);
						return;
					}
					DependencyObject dependencyObject = this._restoreFocus as DependencyObject;
					this._restoreFocus = null;
					if (dependencyObject != null)
					{
						HwndSource hwndSource = PresentationSource.CriticalFromVisual(dependencyObject) as HwndSource;
						if (hwndSource != value)
						{
							dependencyObject = null;
						}
					}
					Keyboard.Focus(dependencyObject as IInputElement);
					IntPtr focus = UnsafeNativeMethods.GetFocus();
					if (focus == value.CriticalHandle)
					{
						dependencyObject = (DependencyObject)Keyboard.FocusedElement;
						if (dependencyObject != null)
						{
							HwndSource hwndSource2 = PresentationSource.CriticalFromVisual(dependencyObject) as HwndSource;
							if (hwndSource2 != value)
							{
								Keyboard.ClearFocus();
							}
						}
					}
				}
			}
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0006BDF8 File Offset: 0x0006B1F8
		[SecurityCritical]
		internal void ProcessKeyAction(ref MSG msg, ref bool handled)
		{
			MSG unsecureCurrentKeyboardMessage = ComponentDispatcher.UnsecureCurrentKeyboardMessage;
			ComponentDispatcher.UnsecureCurrentKeyboardMessage = msg;
			try
			{
				int virtualKey = HwndKeyboardInputProvider.GetVirtualKey(msg.wParam, msg.lParam);
				int scanCode = HwndKeyboardInputProvider.GetScanCode(msg.wParam, msg.lParam);
				bool isExtendedKey = HwndKeyboardInputProvider.IsExtendedKey(msg.lParam);
				bool isSystemKey = msg.message == 260 || msg.message == 261;
				RawKeyboardActions keyUpKeyDown = this.GetKeyUpKeyDown((WindowMessage)msg.message);
				handled = this.ReportInput(msg.hwnd, InputMode.Foreground, this._msgTime, keyUpKeyDown, scanCode, isExtendedKey, isSystemKey, virtualKey);
			}
			finally
			{
				ComponentDispatcher.UnsecureCurrentKeyboardMessage = unsecureCurrentKeyboardMessage;
			}
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0006BEB4 File Offset: 0x0006B2B4
		[SecurityCritical]
		internal void ProcessTextInputAction(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			char c = (char)((int)wParam);
			bool isDeadCharacter = msg == WindowMessage.WM_DEADCHAR || msg == WindowMessage.WM_SYSDEADCHAR;
			bool isSystemCharacter = msg == WindowMessage.WM_SYSCHAR || msg == WindowMessage.WM_SYSDEADCHAR;
			bool isControlCharacter = false;
			try
			{
				if (((int)UnsafeNativeMethods.GetKeyState(17) & 32768) != 0 && ((int)UnsafeNativeMethods.GetKeyState(18) & 32768) == 0 && char.IsControl(c))
				{
					isControlCharacter = true;
				}
			}
			catch (Win32Exception)
			{
			}
			RawTextInputReport inputReport = new RawTextInputReport(this._source.Value, InputMode.Foreground, this._msgTime, isDeadCharacter, isSystemCharacter, isControlCharacter, c);
			handled = this._site.Value.ReportInput(inputReport);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0006BF70 File Offset: 0x0006B370
		internal static int GetVirtualKey(IntPtr wParam, IntPtr lParam)
		{
			int num = NativeMethods.IntPtrToInt32(wParam);
			int num2 = NativeMethods.IntPtrToInt32(lParam);
			if (num == 16)
			{
				int nVirtKey = (num2 & 16711680) >> 16;
				try
				{
					num = SafeNativeMethods.MapVirtualKey(nVirtKey, 3);
					if (num == 0)
					{
						num = 160;
					}
				}
				catch (Win32Exception)
				{
					num = 160;
				}
			}
			if (num == 18)
			{
				bool flag = (num2 & 16777216) >> 24 != 0;
				if (flag)
				{
					num = 165;
				}
				else
				{
					num = 164;
				}
			}
			if (num == 17)
			{
				bool flag2 = (num2 & 16777216) >> 24 != 0;
				if (flag2)
				{
					num = 163;
				}
				else
				{
					num = 162;
				}
			}
			return num;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0006C020 File Offset: 0x0006B420
		internal static int GetScanCode(IntPtr wParam, IntPtr lParam)
		{
			int num = NativeMethods.IntPtrToInt32(lParam);
			int num2 = (num & 16711680) >> 16;
			if (num2 == 0)
			{
				try
				{
					int virtualKey = HwndKeyboardInputProvider.GetVirtualKey(wParam, lParam);
					num2 = SafeNativeMethods.MapVirtualKey(virtualKey, 0);
				}
				catch (Win32Exception)
				{
				}
			}
			return num2;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0006C074 File Offset: 0x0006B474
		internal static bool IsExtendedKey(IntPtr lParam)
		{
			int num = NativeMethods.IntPtrToInt32(lParam);
			return (num & 16777216) != 0;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0006C094 File Offset: 0x0006B494
		[SecurityTreatAsSafe]
		[FriendAccessAllowed]
		[SecurityCritical]
		internal static ModifierKeys GetSystemModifierKeys()
		{
			ModifierKeys modifierKeys = ModifierKeys.None;
			short keyState = UnsafeNativeMethods.GetKeyState(16);
			if (((int)keyState & 32768) == 32768)
			{
				modifierKeys |= ModifierKeys.Shift;
			}
			keyState = UnsafeNativeMethods.GetKeyState(17);
			if (((int)keyState & 32768) == 32768)
			{
				modifierKeys |= ModifierKeys.Control;
			}
			keyState = UnsafeNativeMethods.GetKeyState(18);
			if (((int)keyState & 32768) == 32768)
			{
				modifierKeys |= ModifierKeys.Alt;
			}
			return modifierKeys;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0006C0F4 File Offset: 0x0006B4F4
		private RawKeyboardActions GetKeyUpKeyDown(WindowMessage msg)
		{
			if (msg == WindowMessage.WM_KEYFIRST || msg == WindowMessage.WM_SYSKEYDOWN)
			{
				return RawKeyboardActions.KeyDown;
			}
			if (msg == WindowMessage.WM_KEYUP || msg == WindowMessage.WM_SYSKEYUP)
			{
				return RawKeyboardActions.KeyUp;
			}
			throw new ArgumentException(SR.Get("OnlyAcceptsKeyMessages"));
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0006C138 File Offset: 0x0006B538
		[SecurityCritical]
		private void PossiblyDeactivate(IntPtr hwndFocus)
		{
			bool flag = !this.IsOurWindow(hwndFocus);
			this._active = false;
			if (flag)
			{
				this.ReportInput(this._source.Value.CriticalHandle, InputMode.Foreground, this._msgTime, RawKeyboardActions.Deactivate, 0, false, false, 0);
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0006C180 File Offset: 0x0006B580
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool IsOurWindow(IntPtr hwnd)
		{
			bool result;
			if (hwnd != IntPtr.Zero)
			{
				HwndSource hwndSource = HwndSource.CriticalFromHwnd(hwnd);
				result = (hwndSource != null && hwndSource.Dispatcher == this._source.Value.Dispatcher);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0006C1D0 File Offset: 0x0006B5D0
		[SecurityCritical]
		private IntPtr GetImmediateChildFor(IntPtr hwnd, IntPtr hwndRoot)
		{
			while (hwnd != IntPtr.Zero)
			{
				int windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, hwnd), -16);
				if ((windowLong & 1073741824) == 0)
				{
					break;
				}
				IntPtr parent = UnsafeNativeMethods.GetParent(new HandleRef(this, hwnd));
				if (parent == hwndRoot)
				{
					return hwnd;
				}
				hwnd = parent;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0006C224 File Offset: 0x0006B624
		[SecurityCritical]
		private bool ReportInput(IntPtr hwnd, InputMode mode, int timestamp, RawKeyboardActions actions, int scanCode, bool isExtendedKey, bool isSystemKey, int virtualKey)
		{
			if ((actions & RawKeyboardActions.Deactivate) == RawKeyboardActions.None && (!this._active || this._partialActive))
			{
				try
				{
					actions |= RawKeyboardActions.Activate;
					this._active = true;
					this._partialActive = false;
				}
				catch (Win32Exception)
				{
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
			RawKeyboardInputReport inputReport = new RawKeyboardInputReport(this._source.Value, mode, timestamp, actions, scanCode, isExtendedKey, isSystemKey, virtualKey, extraInformation);
			return this._site.Value.ReportInput(inputReport);
		}

		// Token: 0x04000E8E RID: 3726
		private int _msgTime;

		// Token: 0x04000E8F RID: 3727
		private SecurityCriticalDataClass<HwndSource> _source;

		// Token: 0x04000E90 RID: 3728
		private SecurityCriticalDataClass<InputProviderSite> _site;

		// Token: 0x04000E91 RID: 3729
		private IInputElement _restoreFocus;

		// Token: 0x04000E92 RID: 3730
		[SecurityCritical]
		private IntPtr _restoreFocusWindow;

		// Token: 0x04000E93 RID: 3731
		private bool _active;

		// Token: 0x04000E94 RID: 3732
		private bool _partialActive;

		// Token: 0x04000E95 RID: 3733
		private bool _acquiringFocusOurselves;
	}
}
