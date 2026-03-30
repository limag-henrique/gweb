using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input;
using System.Windows.Input.StylusWisp;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Interop;
using MS.Utility;
using MS.Win32;

namespace System.Windows.Interop
{
	// Token: 0x0200032E RID: 814
	internal sealed class HwndStylusInputProvider : DispatcherObject, IStylusInputProvider, IInputProvider, IDisposable
	{
		// Token: 0x06001BC4 RID: 7108 RVA: 0x000702A0 File Offset: 0x0006F6A0
		[SecurityCritical]
		internal HwndStylusInputProvider(HwndSource source)
		{
			InputManager inputManager = InputManager.Current;
			this._stylusLogic = new SecurityCriticalDataClass<WispLogic>(StylusLogic.GetCurrentStylusLogicAs<WispLogic>());
			new UIPermission(PermissionState.Unrestricted).Assert();
			IntPtr handle;
			try
			{
				this._site = new SecurityCriticalDataClass<InputProviderSite>(inputManager.RegisterInputProvider(this));
				handle = source.Handle;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			this._stylusLogic.Value.RegisterHwndForInput(inputManager, source);
			this._source = new SecurityCriticalDataClass<HwndSource>(source);
			UnsafeNativeMethods.SetProp(new HandleRef(this, handle), "MicrosoftTabletPenServiceProperty", new HandleRef(null, new IntPtr(16777216)));
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00070354 File Offset: 0x0006F754
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void Dispose()
		{
			if (this._site != null)
			{
				this._site.Value.Dispose();
				this._site = null;
				this._stylusLogic.Value.UnRegisterHwndForInput(this._source.Value);
				this._stylusLogic = null;
				this._source = null;
			}
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x000703AC File Offset: 0x0006F7AC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		bool IInputProvider.ProvidesInputForRootVisual(Visual v)
		{
			return this._source.Value.RootVisual == v;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000703CC File Offset: 0x0006F7CC
		void IInputProvider.NotifyDeactivate()
		{
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000703DC File Offset: 0x0006F7DC
		[SecurityCritical]
		IntPtr IStylusInputProvider.FilterMessage(IntPtr hwnd, WindowMessage msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			IntPtr zero = IntPtr.Zero;
			if (this._source == null || this._source.Value == null)
			{
				return zero;
			}
			if (msg != WindowMessage.WM_ENABLE)
			{
				if (msg != WindowMessage.WM_TABLET_FLICK)
				{
					if (msg == WindowMessage.WM_TABLET_QUERYSYSTEMGESTURESTATUS)
					{
						handled = true;
						NativeMethods.POINT point = new NativeMethods.POINT(NativeMethods.SignedLOWORD(lParam), NativeMethods.SignedHIWORD(lParam));
						SafeNativeMethods.ScreenToClient(new HandleRef(this, hwnd), point);
						Point pt = new Point((double)point.x, (double)point.y);
						IInputElement inputElement = StylusDevice.LocalHitTest(this._source.Value, pt);
						if (inputElement != null)
						{
							DependencyObject element = (DependencyObject)inputElement;
							bool isPressAndHoldEnabled = Stylus.GetIsPressAndHoldEnabled(element);
							bool isFlicksEnabled = Stylus.GetIsFlicksEnabled(element);
							bool isTapFeedbackEnabled = Stylus.GetIsTapFeedbackEnabled(element);
							bool isTouchFeedbackEnabled = Stylus.GetIsTouchFeedbackEnabled(element);
							uint num = 0U;
							if (!isPressAndHoldEnabled)
							{
								num |= 1U;
							}
							if (!isTapFeedbackEnabled)
							{
								num |= 8U;
							}
							if (isTouchFeedbackEnabled)
							{
								num |= 256U;
							}
							else
							{
								num |= 512U;
							}
							if (!isFlicksEnabled)
							{
								num |= 65536U;
							}
							zero = new IntPtr((long)((ulong)num));
						}
					}
				}
				else
				{
					handled = true;
					int flickData = NativeMethods.IntPtrToInt32(wParam);
					if (this._stylusLogic != null && this._stylusLogic.Value.Enabled && StylusLogic.GetFlickAction(flickData) == StylusLogic.FlickAction.Scroll)
					{
						zero = new IntPtr(1);
					}
				}
			}
			else
			{
				this._stylusLogic.Value.OnWindowEnableChanged(hwnd, NativeMethods.IntPtrToInt32(wParam) == 0);
			}
			if (handled && EventTrace.IsEnabled(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info))
			{
				EventTrace.EventProvider.TraceEvent(EventTrace.Event.WClientInputMessage, EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, new object[]
				{
					(this._source.Value.CompositionTarget != null) ? this._source.Value.CompositionTarget.Dispatcher.GetHashCode() : 0,
					hwnd.ToInt64(),
					msg,
					(int)wParam,
					(int)lParam
				});
			}
			return zero;
		}

		// Token: 0x04000ED7 RID: 3799
		private const uint TABLET_PRESSANDHOLD_DISABLED = 1U;

		// Token: 0x04000ED8 RID: 3800
		private const uint TABLET_TAPFEEDBACK_DISABLED = 8U;

		// Token: 0x04000ED9 RID: 3801
		private const uint TABLET_TOUCHUI_FORCEON = 256U;

		// Token: 0x04000EDA RID: 3802
		private const uint TABLET_TOUCHUI_FORCEOFF = 512U;

		// Token: 0x04000EDB RID: 3803
		private const uint TABLET_FLICKS_DISABLED = 65536U;

		// Token: 0x04000EDC RID: 3804
		private const int MultiTouchEnabledFlag = 16777216;

		// Token: 0x04000EDD RID: 3805
		private SecurityCriticalDataClass<WispLogic> _stylusLogic;

		// Token: 0x04000EDE RID: 3806
		private SecurityCriticalDataClass<HwndSource> _source;

		// Token: 0x04000EDF RID: 3807
		private SecurityCriticalDataClass<InputProviderSite> _site;
	}
}
