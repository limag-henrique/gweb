using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using MS.Internal.Media;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace MS.Internal.Automation
{
	// Token: 0x0200078B RID: 1931
	internal class ElementUtil
	{
		// Token: 0x0600511E RID: 20766 RVA: 0x00144AE0 File Offset: 0x00143EE0
		private ElementUtil()
		{
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x00144AF4 File Offset: 0x00143EF4
		internal static Visual GetParent(Visual el)
		{
			return VisualTreeHelper.GetParent(el) as Visual;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x00144B0C File Offset: 0x00143F0C
		internal static Visual GetFirstChild(Visual el)
		{
			if (el == null)
			{
				return null;
			}
			return ElementUtil.FindVisibleSibling(el, 0, true);
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x00144B28 File Offset: 0x00143F28
		internal static Visual GetLastChild(Visual el)
		{
			if (el == null)
			{
				return null;
			}
			return ElementUtil.FindVisibleSibling(el, el.InternalVisualChildrenCount - 1, false);
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x00144B4C File Offset: 0x00143F4C
		internal static Visual GetNextSibling(Visual el)
		{
			Visual visual = VisualTreeHelper.GetParent(el) as Visual;
			if (visual == null)
			{
				return null;
			}
			return ElementUtil.FindVisibleSibling(visual, el, true);
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x00144B74 File Offset: 0x00143F74
		internal static Visual GetPreviousSibling(Visual el)
		{
			Visual visual = VisualTreeHelper.GetParent(el) as Visual;
			if (visual == null)
			{
				return null;
			}
			return ElementUtil.FindVisibleSibling(visual, el, false);
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x00144B9C File Offset: 0x00143F9C
		internal static Visual GetRoot(Visual el)
		{
			Visual visual = el;
			for (;;)
			{
				Visual visual2 = VisualTreeHelper.GetParent(visual) as Visual;
				if (visual2 == null)
				{
					break;
				}
				visual = visual2;
			}
			return visual;
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00144BC0 File Offset: 0x00143FC0
		internal static Rect GetLocalRect(UIElement element)
		{
			Visual root = ElementUtil.GetRoot(element);
			double height = element.RenderSize.Height;
			double width = element.RenderSize.Width;
			Rect rect = new Rect(0.0, 0.0, width, height);
			GeneralTransform generalTransform = element.TransformToAncestor(root);
			return generalTransform.TransformBounds(rect);
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x00144C20 File Offset: 0x00144020
		internal static Rect GetScreenRect(IntPtr hwnd, UIElement el)
		{
			Rect localRect = ElementUtil.GetLocalRect(el);
			NativeMethods.RECT rect = new NativeMethods.RECT((int)localRect.Left, (int)localRect.Top, (int)localRect.Right, (int)localRect.Bottom);
			try
			{
				SafeSecurityHelper.TransformLocalRectToScreen(new HandleRef(null, hwnd), ref rect);
			}
			catch (Win32Exception)
			{
				return Rect.Empty;
			}
			localRect = new Rect((double)rect.left, (double)rect.top, (double)(rect.right - rect.left), (double)(rect.bottom - rect.top));
			return localRect;
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x00144CC4 File Offset: 0x001440C4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static Visual GetElementFromPoint(IntPtr hwnd, Visual root, Point pointScreen)
		{
			HwndSource hwndSource = HwndSource.CriticalFromHwnd(hwnd);
			if (hwndSource == null)
			{
				return null;
			}
			Point point = PointUtil.ScreenToClient(pointScreen, hwndSource);
			Point point2 = PointUtil.ClientToRoot(point, hwndSource);
			PointHitTestResult pointHitTestResult = VisualTreeUtils.AsNearestPointHitTestResult(VisualTreeHelper.HitTest(root, point2));
			return (pointHitTestResult != null) ? pointHitTestResult.VisualHit : null;
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x00144D0C File Offset: 0x0014410C
		internal static void CheckEnabled(Visual visual)
		{
			UIElement uielement = visual as UIElement;
			if (uielement != null && !uielement.IsEnabled)
			{
				throw new ElementNotEnabledException();
			}
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x00144D34 File Offset: 0x00144134
		internal static object Invoke(AutomationPeer peer, DispatcherOperationCallback work, object arg)
		{
			Dispatcher dispatcher = peer.Dispatcher;
			if (dispatcher == null)
			{
				throw new ElementNotAvailableException();
			}
			Exception remoteException = null;
			bool completed = false;
			object result = dispatcher.Invoke(DispatcherPriority.Send, TimeSpan.FromMinutes(3.0), new DispatcherOperationCallback(delegate(object unused)
			{
				object result2;
				try
				{
					result2 = work(arg);
				}
				catch (Exception remoteException)
				{
					Exception remoteException = remoteException;
					result2 = null;
				}
				catch
				{
					Exception remoteException = null;
					result2 = null;
				}
				finally
				{
					completed = true;
				}
				return result2;
			}), null);
			if (completed)
			{
				if (remoteException != null)
				{
					throw remoteException;
				}
				return result;
			}
			else
			{
				bool hasShutdownStarted = dispatcher.HasShutdownStarted;
				if (hasShutdownStarted)
				{
					throw new InvalidOperationException(SR.Get("AutomationDispatcherShutdown"));
				}
				throw new TimeoutException(SR.Get("AutomationTimeout"));
			}
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x00144DDC File Offset: 0x001441DC
		private static Visual FindVisibleSibling(Visual parent, int start, bool searchForwards)
		{
			int num = start;
			int internalVisualChildrenCount = parent.InternalVisualChildrenCount;
			while (num >= 0 && num < internalVisualChildrenCount)
			{
				Visual visual = parent.InternalGetVisualChild(num);
				if (!(visual is UIElement) || ((UIElement)visual).Visibility == Visibility.Visible)
				{
					return visual;
				}
				num += (searchForwards ? 1 : -1);
			}
			return null;
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x00144E28 File Offset: 0x00144228
		private static Visual FindVisibleSibling(Visual parent, Visual child, bool searchForwards)
		{
			int internalVisualChildrenCount = parent.InternalVisualChildrenCount;
			int i;
			for (i = 0; i < internalVisualChildrenCount; i++)
			{
				Visual visual = parent.InternalGetVisualChild(i);
				if (visual == child)
				{
					break;
				}
			}
			if (searchForwards)
			{
				return ElementUtil.FindVisibleSibling(parent, i + 1, searchForwards);
			}
			return ElementUtil.FindVisibleSibling(parent, i - 1, searchForwards);
		}
	}
}
