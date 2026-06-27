using System;
using System.Security;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using MS.Internal.WindowsRuntime.Windows.UI.ViewManagement;

namespace MS.Internal.Interop
{
	// Token: 0x020006FC RID: 1788
	internal static class TipTsfHelper
	{
		// Token: 0x06004D0B RID: 19723 RVA: 0x0012FC28 File Offset: 0x0012F028
		private static bool CheckAndDispatchKbOperation(Action<DependencyObject> kbCall, DependencyObject focusedObject)
		{
			DispatcherOperation dispatcherOperation = TipTsfHelper.s_KbOperation;
			if (dispatcherOperation != null && dispatcherOperation.Status == DispatcherOperationStatus.Pending)
			{
				TipTsfHelper.s_KbOperation.Abort();
			}
			TipTsfHelper.s_KbOperation = null;
			if (Dispatcher.CurrentDispatcher._disableProcessingCount > 0)
			{
				TipTsfHelper.s_KbOperation = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Input, new Action(delegate()
				{
					if (Keyboard.FocusedElement == focusedObject)
					{
						kbCall(focusedObject);
					}
				}));
				return true;
			}
			return false;
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x0012FC9C File Offset: 0x0012F09C
		[SecuritySafeCritical]
		internal static void Show(DependencyObject focusedObject)
		{
			if (TipTsfHelper.s_PlatformSupported && !CoreAppContextSwitches.DisableImplicitTouchKeyboardInvocation && StylusLogic.IsStylusAndTouchSupportEnabled && !StylusLogic.IsPointerStackEnabled && !TipTsfHelper.CheckAndDispatchKbOperation(new Action<DependencyObject>(TipTsfHelper.Show), focusedObject) && TipTsfHelper.ShouldShow(focusedObject))
			{
				try
				{
					InputPane forWindow;
					InputPane inputPane = forWindow = InputPane.GetForWindow(TipTsfHelper.GetHwndSource(focusedObject));
					try
					{
						if (inputPane != null)
						{
							inputPane.TryShow();
						}
					}
					finally
					{
						if (forWindow != null)
						{
							((IDisposable)forWindow).Dispose();
						}
					}
				}
				catch (PlatformNotSupportedException)
				{
					TipTsfHelper.s_PlatformSupported = false;
				}
			}
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x0012FD44 File Offset: 0x0012F144
		[SecuritySafeCritical]
		internal static void Hide(DependencyObject focusedObject)
		{
			if (TipTsfHelper.s_PlatformSupported && StylusLogic.IsStylusAndTouchSupportEnabled && !StylusLogic.IsPointerStackEnabled && !TipTsfHelper.CheckAndDispatchKbOperation(new Action<DependencyObject>(TipTsfHelper.Hide), focusedObject))
			{
				try
				{
					InputPane forWindow;
					InputPane inputPane = forWindow = InputPane.GetForWindow(TipTsfHelper.GetHwndSource(focusedObject));
					try
					{
						if (inputPane != null)
						{
							inputPane.TryHide();
						}
					}
					finally
					{
						if (forWindow != null)
						{
							((IDisposable)forWindow).Dispose();
						}
					}
				}
				catch (PlatformNotSupportedException)
				{
					TipTsfHelper.s_PlatformSupported = false;
				}
			}
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x0012FDE0 File Offset: 0x0012F1E0
		private static bool ShouldShow(DependencyObject focusedObject)
		{
			AutomationPeer automationPeer = null;
			UIElement uielement;
			UIElement3D uielement3D;
			ContentElement contentElement;
			if ((uielement = (focusedObject as UIElement)) != null)
			{
				automationPeer = uielement.GetAutomationPeer();
			}
			else if ((uielement3D = (focusedObject as UIElement3D)) != null)
			{
				automationPeer = uielement3D.GetAutomationPeer();
			}
			else if ((contentElement = (focusedObject as ContentElement)) != null)
			{
				automationPeer = contentElement.GetAutomationPeer();
			}
			return ((automationPeer != null) ? automationPeer.GetPattern(PatternInterface.Text) : null) != null;
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x0012FE38 File Offset: 0x0012F238
		[SecurityCritical]
		private static HwndSource GetHwndSource(DependencyObject focusedObject)
		{
			return PresentationSource.CriticalFromVisual(focusedObject) as HwndSource;
		}

		// Token: 0x0400216F RID: 8559
		private static bool s_PlatformSupported = true;

		// Token: 0x04002170 RID: 8560
		[ThreadStatic]
		private static DispatcherOperation s_KbOperation = null;
	}
}
