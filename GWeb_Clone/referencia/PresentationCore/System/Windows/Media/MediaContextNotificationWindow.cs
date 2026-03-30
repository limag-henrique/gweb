using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using MS.Internal;
using MS.Internal.Interop;
using MS.Win32;

namespace System.Windows.Media
{
	// Token: 0x02000422 RID: 1058
	internal class MediaContextNotificationWindow : IDisposable
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x000ABA8C File Offset: 0x000AAE8C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal MediaContextNotificationWindow(MediaContext ownerMediaContext)
		{
			this._ownerMediaContext = ownerMediaContext;
			HwndWrapper value = new HwndWrapper(0, int.MinValue, 0, 0, 0, 0, 0, "MediaContextNotificationWindow", IntPtr.Zero, null);
			this._hwndNotificationHook = new HwndWrapperHook(this.MessageFilter);
			this._hwndNotification = new SecurityCriticalDataClass<HwndWrapper>(value);
			this._hwndNotification.Value.AddHook(this._hwndNotificationHook);
			this._isDisposed = false;
			this.ChangeWindowMessageFilter(MediaContextNotificationWindow.s_dwmRedirectionEnvironmentChanged, 1U);
			HRESULT.Check(MediaContextNotificationWindow.MilContent_AttachToHwnd(this._hwndNotification.Value.Handle));
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000ABB24 File Offset: 0x000AAF24
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void Dispose()
		{
			if (!this._isDisposed)
			{
				HRESULT.Check(MediaContextNotificationWindow.MilContent_DetachFromHwnd(this._hwndNotification.Value.Handle));
				this._hwndNotification.Value.Dispose();
				this._hwndNotificationHook = null;
				this._hwndNotification = null;
				this._ownerMediaContext = null;
				this._isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000ABB88 File Offset: 0x000AAF88
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void SetAsChannelNotificationWindow()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException("MediaContextNotificationWindow");
			}
			this._ownerMediaContext.Channel.SetNotificationWindow(this._hwndNotification.Value.Handle, MediaContextNotificationWindow.s_channelNotifyMessage);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000ABBD0 File Offset: 0x000AAFD0
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private IntPtr MessageFilter(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException("MediaContextNotificationWindow");
			}
			if (msg == 798)
			{
				HRESULT.Check(MediaContextNotificationWindow.MilContent_AttachToHwnd(this._hwndNotification.Value.Handle));
			}
			else if (msg == (int)MediaContextNotificationWindow.s_channelNotifyMessage)
			{
				this._ownerMediaContext.NotifyChannelMessage();
			}
			else if (msg == (int)MediaContextNotificationWindow.s_dwmRedirectionEnvironmentChanged)
			{
				MediaSystem.NotifyRedirectionEnvironmentChanged();
			}
			return IntPtr.Zero;
		}

		// Token: 0x06002AEA RID: 10986
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll")]
		private static extern int MilContent_AttachToHwnd(IntPtr hwnd);

		// Token: 0x06002AEB RID: 10987
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll")]
		private static extern int MilContent_DetachFromHwnd(IntPtr hwnd);

		// Token: 0x06002AEC RID: 10988 RVA: 0x000ABC40 File Offset: 0x000AB040
		[SecurityCritical]
		private void ChangeWindowMessageFilter(WindowMessage message, uint flag)
		{
			IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle("user32.dll");
			IntPtr procAddressNoThrow = UnsafeNativeMethods.GetProcAddressNoThrow(new HandleRef(null, moduleHandle), "ChangeWindowMessageFilter");
			if (procAddressNoThrow != IntPtr.Zero)
			{
				MediaContextNotificationWindow.ChangeWindowMessageFilterNative changeWindowMessageFilterNative = Marshal.GetDelegateForFunctionPointer(procAddressNoThrow, typeof(MediaContextNotificationWindow.ChangeWindowMessageFilterNative)) as MediaContextNotificationWindow.ChangeWindowMessageFilterNative;
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					changeWindowMessageFilterNative(message, flag);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x040013A0 RID: 5024
		private bool _isDisposed;

		// Token: 0x040013A1 RID: 5025
		private MediaContext _ownerMediaContext;

		// Token: 0x040013A2 RID: 5026
		private SecurityCriticalDataClass<HwndWrapper> _hwndNotification;

		// Token: 0x040013A3 RID: 5027
		[SecurityCritical]
		private HwndWrapperHook _hwndNotificationHook;

		// Token: 0x040013A4 RID: 5028
		private static WindowMessage s_channelNotifyMessage = UnsafeNativeMethods.RegisterWindowMessage("MilChannelNotify");

		// Token: 0x040013A5 RID: 5029
		private static WindowMessage s_dwmRedirectionEnvironmentChanged = UnsafeNativeMethods.RegisterWindowMessage("DwmRedirectionEnvironmentChangedHint");

		// Token: 0x02000891 RID: 2193
		// (Invoke) Token: 0x0600581C RID: 22556
		[UnmanagedFunctionPointer(CallingConvention.Winapi)]
		private delegate void ChangeWindowMessageFilterNative(WindowMessage message, uint flag);
	}
}
