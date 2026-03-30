using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000699 RID: 1689
	internal static class UIElementHelper
	{
		// Token: 0x06004A30 RID: 18992 RVA: 0x001208B8 File Offset: 0x0011FCB8
		[FriendAccessAllowed]
		internal static bool IsHitTestVisible(DependencyObject o)
		{
			UIElement uielement = o as UIElement;
			if (uielement != null)
			{
				return uielement.IsHitTestVisible;
			}
			return ((UIElement3D)o).IsHitTestVisible;
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x001208E4 File Offset: 0x0011FCE4
		[FriendAccessAllowed]
		internal static bool IsVisible(DependencyObject o)
		{
			UIElement uielement = o as UIElement;
			if (uielement != null)
			{
				return uielement.IsVisible;
			}
			return ((UIElement3D)o).IsVisible;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x00120910 File Offset: 0x0011FD10
		[FriendAccessAllowed]
		internal static DependencyObject PredictFocus(DependencyObject o, FocusNavigationDirection direction)
		{
			UIElement uielement;
			if ((uielement = (o as UIElement)) != null)
			{
				return uielement.PredictFocus(direction);
			}
			ContentElement contentElement;
			if ((contentElement = (o as ContentElement)) != null)
			{
				return contentElement.PredictFocus(direction);
			}
			UIElement3D uielement3D;
			if ((uielement3D = (o as UIElement3D)) != null)
			{
				return uielement3D.PredictFocus(direction);
			}
			return null;
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x00120954 File Offset: 0x0011FD54
		[FriendAccessAllowed]
		internal static UIElement GetContainingUIElement2D(DependencyObject reference)
		{
			UIElement uielement = null;
			while (reference != null)
			{
				uielement = (reference as UIElement);
				if (uielement != null)
				{
					break;
				}
				reference = VisualTreeHelper.GetParent(reference);
			}
			return uielement;
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x0012097C File Offset: 0x0011FD7C
		[FriendAccessAllowed]
		internal static DependencyObject GetUIParent(DependencyObject child)
		{
			return UIElementHelper.GetUIParent(child, false);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x00120994 File Offset: 0x0011FD94
		[FriendAccessAllowed]
		internal static DependencyObject GetUIParent(DependencyObject child, bool continuePastVisualTree)
		{
			DependencyObject internalVisualParent;
			if (child is Visual)
			{
				internalVisualParent = ((Visual)child).InternalVisualParent;
			}
			else
			{
				internalVisualParent = ((Visual3D)child).InternalVisualParent;
			}
			DependencyObject dependencyObject = InputElement.GetContainingUIElement(internalVisualParent);
			if (dependencyObject == null && continuePastVisualTree)
			{
				UIElement uielement = child as UIElement;
				if (uielement != null)
				{
					dependencyObject = (InputElement.GetContainingInputElement(uielement.GetUIParentCore()) as DependencyObject);
				}
				else
				{
					UIElement3D uielement3D = child as UIElement3D;
					if (uielement3D != null)
					{
						dependencyObject = (InputElement.GetContainingInputElement(uielement3D.GetUIParentCore()) as DependencyObject);
					}
				}
			}
			return dependencyObject;
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00120A10 File Offset: 0x0011FE10
		[FriendAccessAllowed]
		internal static bool IsUIElementOrUIElement3D(DependencyObject o)
		{
			return o is UIElement || o is UIElement3D;
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x00120A30 File Offset: 0x0011FE30
		[FriendAccessAllowed]
		internal static void InvalidateAutomationAncestors(DependencyObject o)
		{
			UIElement uielement = null;
			UIElement3D uielement3D = null;
			ContentElement contentElement = null;
			Stack<DependencyObject> branchNodeStack = new Stack<DependencyObject>();
			bool flag = true;
			while (o != null && flag)
			{
				flag &= UIElementHelper.InvalidateAutomationPeer(o, out uielement, out contentElement, out uielement3D);
				bool continuePastVisualTree = false;
				if (uielement != null)
				{
					flag &= uielement.InvalidateAutomationAncestorsCore(branchNodeStack, out continuePastVisualTree);
					o = uielement.GetUIParent(continuePastVisualTree);
				}
				else if (contentElement != null)
				{
					flag &= contentElement.InvalidateAutomationAncestorsCore(branchNodeStack, out continuePastVisualTree);
					o = contentElement.GetUIParent(continuePastVisualTree);
				}
				else if (uielement3D != null)
				{
					flag &= uielement3D.InvalidateAutomationAncestorsCore(branchNodeStack, out continuePastVisualTree);
					o = uielement3D.GetUIParent(continuePastVisualTree);
				}
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x00120ABC File Offset: 0x0011FEBC
		internal static bool InvalidateAutomationPeer(DependencyObject o, out UIElement e, out ContentElement ce, out UIElement3D e3d)
		{
			e = null;
			ce = null;
			e3d = null;
			AutomationPeer automationPeer = null;
			e = (o as UIElement);
			if (e != null)
			{
				if (e.HasAutomationPeer)
				{
					automationPeer = e.GetAutomationPeer();
				}
			}
			else
			{
				ce = (o as ContentElement);
				if (ce != null)
				{
					if (ce.HasAutomationPeer)
					{
						automationPeer = ce.GetAutomationPeer();
					}
				}
				else
				{
					e3d = (o as UIElement3D);
					if (e3d != null && e3d.HasAutomationPeer)
					{
						automationPeer = e3d.GetAutomationPeer();
					}
				}
			}
			if (automationPeer != null)
			{
				automationPeer.InvalidateAncestorsRecursive();
				if (automationPeer.GetParent() != null)
				{
					return false;
				}
			}
			return true;
		}
	}
}
