using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Windows.Interop;

namespace MS.Internal.WindowsRuntime.Windows.UI.ViewManagement
{
	// Token: 0x020006A5 RID: 1701
	internal class InputPane : IDisposable
	{
		// Token: 0x06004A8C RID: 19084 RVA: 0x001229C8 File Offset: 0x00121DC8
		[SecuritySafeCritical]
		static InputPane()
		{
			try
			{
				InputPane.s_WinRTType = Type.GetType(InputPane.s_TypeName);
				if (InputPane.GetWinRtActivationFactory(true) == null)
				{
					InputPane.s_WinRTType = null;
				}
			}
			catch
			{
				InputPane.s_WinRTType = null;
			}
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x00122A24 File Offset: 0x00121E24
		[SecurityCritical]
		private InputPane(IntPtr? hwnd)
		{
			if (InputPane.s_WinRTType == null)
			{
				throw new PlatformNotSupportedException();
			}
			try
			{
				if (hwnd != null)
				{
					InputPaneRcw.IInputPaneInterop inputPaneInterop;
					try
					{
						inputPaneInterop = (InputPane.GetWinRtActivationFactory(false) as InputPaneRcw.IInputPaneInterop);
					}
					catch (COMException)
					{
						inputPaneInterop = (InputPane.GetWinRtActivationFactory(true) as InputPaneRcw.IInputPaneInterop);
					}
					InputPaneRcw.IInputPane2 inputPane;
					if (inputPaneInterop == null)
					{
						inputPane = null;
					}
					else
					{
						InputPaneRcw.IInputPaneInterop inputPaneInterop2 = inputPaneInterop;
						IntPtr value = hwnd.Value;
						Guid guid = typeof(InputPaneRcw.IInputPane2).GUID;
						inputPane = inputPaneInterop2.GetForWindow(value, ref guid);
					}
					this._inputPane = inputPane;
				}
			}
			catch (COMException)
			{
			}
			if (this._inputPane == null)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x00122AE4 File Offset: 0x00121EE4
		[SecurityCritical]
		internal static InputPane GetForWindow(HwndSource source)
		{
			return new InputPane((source != null) ? new IntPtr?(source.CriticalHandle) : null);
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x00122B10 File Offset: 0x00121F10
		[SecurityCritical]
		internal bool TryShow()
		{
			bool result = false;
			try
			{
				InputPaneRcw.IInputPane2 inputPane = this._inputPane;
				result = (inputPane != null && inputPane.TryShow());
			}
			catch (COMException)
			{
			}
			return result;
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00122B54 File Offset: 0x00121F54
		[SecurityCritical]
		internal bool TryHide()
		{
			bool result = false;
			try
			{
				InputPaneRcw.IInputPane2 inputPane = this._inputPane;
				result = (inputPane != null && inputPane.TryHide());
			}
			catch (COMException)
			{
			}
			return result;
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00122B98 File Offset: 0x00121F98
		[SecurityCritical]
		private static IActivationFactory GetWinRtActivationFactory(bool forceInitialization = false)
		{
			if (forceInitialization || InputPane._winRtActivationFactory == null)
			{
				try
				{
					InputPane._winRtActivationFactory = WindowsRuntimeMarshal.GetActivationFactory(InputPane.s_WinRTType);
				}
				catch (Exception ex) when (ex is TypeLoadException || ex is FileNotFoundException)
				{
					InputPane._winRtActivationFactory = null;
				}
			}
			return InputPane._winRtActivationFactory;
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x00122C14 File Offset: 0x00122014
		~InputPane()
		{
			this.Dispose(false);
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x00122C50 File Offset: 0x00122050
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x00122C6C File Offset: 0x0012206C
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (this._inputPane != null)
				{
					try
					{
						Marshal.ReleaseComObject(this._inputPane);
					}
					catch
					{
					}
					this._inputPane = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x04001F8D RID: 8077
		private static readonly string s_TypeName = "Windows.UI.ViewManagement.InputPane, Windows, ContentType=WindowsRuntime";

		// Token: 0x04001F8E RID: 8078
		private static Type s_WinRTType;

		// Token: 0x04001F8F RID: 8079
		[SecurityCritical]
		private static IActivationFactory _winRtActivationFactory;

		// Token: 0x04001F90 RID: 8080
		[SecurityCritical]
		private InputPaneRcw.IInputPane2 _inputPane;

		// Token: 0x04001F91 RID: 8081
		private bool _disposed;
	}
}
