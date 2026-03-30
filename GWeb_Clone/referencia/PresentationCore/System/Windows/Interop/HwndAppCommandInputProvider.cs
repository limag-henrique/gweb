using System;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Win32;

namespace System.Windows.Interop
{
	// Token: 0x02000327 RID: 807
	internal sealed class HwndAppCommandInputProvider : DispatcherObject, IInputProvider, IDisposable
	{
		// Token: 0x06001B20 RID: 6944 RVA: 0x0006B5A8 File Offset: 0x0006A9A8
		[SecurityCritical]
		internal HwndAppCommandInputProvider(HwndSource source)
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

		// Token: 0x06001B21 RID: 6945 RVA: 0x0006B60C File Offset: 0x0006AA0C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void Dispose()
		{
			if (this._site != null)
			{
				this._site.Value.Dispose();
				this._site = null;
			}
			this._source = null;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0006B640 File Offset: 0x0006AA40
		[SecurityCritical]
		[SecurityTreatAsSafe]
		bool IInputProvider.ProvidesInputForRootVisual(Visual v)
		{
			return this._source.Value.RootVisual == v;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0006B660 File Offset: 0x0006AA60
		void IInputProvider.NotifyDeactivate()
		{
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0006B670 File Offset: 0x0006AA70
		[SecurityCritical]
		internal IntPtr FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (this._source == null || this._source.Value == null)
			{
				return IntPtr.Zero;
			}
			if (msg == WindowMessage.WM_APPCOMMAND)
			{
				RawAppCommandInputReport inputReport = new RawAppCommandInputReport(this._source.Value, InputMode.Foreground, SafeNativeMethods.GetMessageTime(), HwndAppCommandInputProvider.GetAppCommand(lParam), HwndAppCommandInputProvider.GetDevice(lParam), InputType.Command);
				handled = this._site.Value.ReportInput(inputReport);
			}
			if (!handled)
			{
				return IntPtr.Zero;
			}
			return new IntPtr(1);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0006B6EC File Offset: 0x0006AAEC
		private static int GetAppCommand(IntPtr lParam)
		{
			return (int)((short)(NativeMethods.SignedHIWORD(NativeMethods.IntPtrToInt32(lParam)) & -61441));
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0006B70C File Offset: 0x0006AB0C
		private static InputType GetDevice(IntPtr lParam)
		{
			ushort num = (ushort)(NativeMethods.SignedHIWORD(NativeMethods.IntPtrToInt32(lParam)) & 61440);
			InputType result;
			if (num != 0)
			{
				if (num != 4096 && num == 32768)
				{
					result = InputType.Mouse;
				}
				else
				{
					result = InputType.Hid;
				}
			}
			else
			{
				result = InputType.Keyboard;
			}
			return result;
		}

		// Token: 0x04000E8C RID: 3724
		private SecurityCriticalDataClass<HwndSource> _source;

		// Token: 0x04000E8D RID: 3725
		private SecurityCriticalDataClass<InputProviderSite> _site;
	}
}
