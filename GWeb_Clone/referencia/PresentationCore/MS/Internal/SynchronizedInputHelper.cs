using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MS.Internal
{
	// Token: 0x0200069E RID: 1694
	internal static class SynchronizedInputHelper
	{
		// Token: 0x06004A39 RID: 19001 RVA: 0x00120B44 File Offset: 0x0011FF44
		internal static DependencyObject GetUIParentCore(DependencyObject o)
		{
			UIElement uielement = o as UIElement;
			if (uielement != null)
			{
				return uielement.GetUIParentCore();
			}
			ContentElement contentElement = o as ContentElement;
			if (contentElement != null)
			{
				return contentElement.GetUIParentCore();
			}
			UIElement3D uielement3D = o as UIElement3D;
			if (uielement3D != null)
			{
				return uielement3D.GetUIParentCore();
			}
			return null;
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x00120B88 File Offset: 0x0011FF88
		internal static bool IsMappedEvent(RoutedEventArgs args)
		{
			RoutedEvent routedEvent = args.RoutedEvent;
			return routedEvent == Keyboard.KeyUpEvent || routedEvent == Keyboard.KeyDownEvent || routedEvent == TextCompositionManager.TextInputEvent || routedEvent == Mouse.MouseDownEvent || routedEvent == Mouse.MouseUpEvent;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x00120BC8 File Offset: 0x0011FFC8
		internal static SynchronizedInputType GetPairedInputType(SynchronizedInputType inputType)
		{
			SynchronizedInputType result = SynchronizedInputType.KeyDown;
			if (inputType <= SynchronizedInputType.MouseLeftButtonDown)
			{
				switch (inputType)
				{
				case SynchronizedInputType.KeyUp:
					result = SynchronizedInputType.KeyDown;
					break;
				case SynchronizedInputType.KeyDown:
					result = SynchronizedInputType.KeyUp;
					break;
				case (SynchronizedInputType)3:
					break;
				case SynchronizedInputType.MouseLeftButtonUp:
					result = SynchronizedInputType.MouseLeftButtonDown;
					break;
				default:
					if (inputType == SynchronizedInputType.MouseLeftButtonDown)
					{
						result = SynchronizedInputType.MouseLeftButtonUp;
					}
					break;
				}
			}
			else if (inputType != SynchronizedInputType.MouseRightButtonUp)
			{
				if (inputType == SynchronizedInputType.MouseRightButtonDown)
				{
					result = SynchronizedInputType.MouseRightButtonUp;
				}
			}
			else
			{
				result = SynchronizedInputType.MouseRightButtonDown;
			}
			return result;
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x00120C20 File Offset: 0x00120020
		internal static bool IsListening(RoutedEventArgs args)
		{
			return Array.IndexOf<RoutedEvent>(InputManager.SynchronizedInputEvents, args.RoutedEvent) >= 0;
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x00120C44 File Offset: 0x00120044
		internal static bool IsListening(DependencyObject o, RoutedEventArgs args)
		{
			return InputManager.ListeningElement == o && Array.IndexOf<RoutedEvent>(InputManager.SynchronizedInputEvents, args.RoutedEvent) >= 0;
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x00120C70 File Offset: 0x00120070
		internal static bool ShouldContinueListening(RoutedEventArgs args)
		{
			return args.RoutedEvent == Keyboard.KeyDownEvent;
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x00120C8C File Offset: 0x0012008C
		internal static void AddParentPreOpportunityHandler(DependencyObject o, EventRoute route, RoutedEventArgs args)
		{
			DependencyObject dependencyObject = null;
			if (o is Visual || o is Visual3D)
			{
				dependencyObject = UIElementHelper.GetUIParent(o);
			}
			DependencyObject uiparentCore = SynchronizedInputHelper.GetUIParentCore(o);
			if (uiparentCore != null && uiparentCore != dependencyObject)
			{
				UIElement uielement = uiparentCore as UIElement;
				if (uielement != null)
				{
					uielement.AddSynchronizedInputPreOpportunityHandler(route, args);
					return;
				}
				ContentElement contentElement = uiparentCore as ContentElement;
				if (contentElement != null)
				{
					contentElement.AddSynchronizedInputPreOpportunityHandler(route, args);
					return;
				}
				UIElement3D uielement3D = uiparentCore as UIElement3D;
				if (uielement3D != null)
				{
					uielement3D.AddSynchronizedInputPreOpportunityHandler(route, args);
				}
			}
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x00120CFC File Offset: 0x001200FC
		internal static void AddHandlerToRoute(DependencyObject o, EventRoute route, RoutedEventHandler eventHandler, bool handledToo)
		{
			route.Add(o, eventHandler, handledToo);
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x00120D14 File Offset: 0x00120114
		internal static void PreOpportunityHandler(object sender, RoutedEventArgs args)
		{
			KeyboardEventArgs keyboardEventArgs = args as KeyboardEventArgs;
			if (keyboardEventArgs != null)
			{
				InputManager.SynchronizedInputState = SynchronizedInputStates.HadOpportunity;
				return;
			}
			TextCompositionEventArgs textCompositionEventArgs = args as TextCompositionEventArgs;
			if (textCompositionEventArgs != null)
			{
				InputManager.SynchronizedInputState = SynchronizedInputStates.HadOpportunity;
				return;
			}
			MouseButtonEventArgs mouseButtonEventArgs = args as MouseButtonEventArgs;
			if (mouseButtonEventArgs != null)
			{
				MouseButton changedButton = mouseButtonEventArgs.ChangedButton;
				if (changedButton != MouseButton.Left)
				{
					if (changedButton != MouseButton.Right)
					{
						return;
					}
					if (InputManager.SynchronizeInputType == SynchronizedInputType.MouseRightButtonDown || InputManager.SynchronizeInputType == SynchronizedInputType.MouseRightButtonUp)
					{
						InputManager.SynchronizedInputState = SynchronizedInputStates.HadOpportunity;
					}
				}
				else if (InputManager.SynchronizeInputType == SynchronizedInputType.MouseLeftButtonDown || InputManager.SynchronizeInputType == SynchronizedInputType.MouseLeftButtonUp)
				{
					InputManager.SynchronizedInputState = SynchronizedInputStates.HadOpportunity;
					return;
				}
			}
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x00120D8C File Offset: 0x0012018C
		internal static void PostOpportunityHandler(object sender, RoutedEventArgs args)
		{
			KeyboardEventArgs keyboardEventArgs = args as KeyboardEventArgs;
			if (keyboardEventArgs != null)
			{
				InputManager.SynchronizedInputState = SynchronizedInputStates.Handled;
				return;
			}
			TextCompositionEventArgs textCompositionEventArgs = args as TextCompositionEventArgs;
			if (textCompositionEventArgs != null)
			{
				InputManager.SynchronizedInputState = SynchronizedInputStates.Handled;
				return;
			}
			MouseButtonEventArgs mouseButtonEventArgs = args as MouseButtonEventArgs;
			if (mouseButtonEventArgs != null)
			{
				MouseButton changedButton = mouseButtonEventArgs.ChangedButton;
				if (changedButton != MouseButton.Left)
				{
					if (changedButton != MouseButton.Right)
					{
						return;
					}
					if (InputManager.SynchronizeInputType == SynchronizedInputType.MouseRightButtonDown || InputManager.SynchronizeInputType == SynchronizedInputType.MouseRightButtonUp)
					{
						InputManager.SynchronizedInputState = SynchronizedInputStates.Handled;
					}
				}
				else if (InputManager.SynchronizeInputType == SynchronizedInputType.MouseLeftButtonDown || InputManager.SynchronizeInputType == SynchronizedInputType.MouseLeftButtonUp)
				{
					InputManager.SynchronizedInputState = SynchronizedInputStates.Handled;
					return;
				}
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x00120E04 File Offset: 0x00120204
		internal static RoutedEvent[] MapInputTypeToRoutedEvents(SynchronizedInputType inputType)
		{
			if (inputType <= SynchronizedInputType.MouseLeftButtonDown)
			{
				switch (inputType)
				{
				case SynchronizedInputType.KeyUp:
					return new RoutedEvent[]
					{
						Keyboard.KeyUpEvent
					};
				case SynchronizedInputType.KeyDown:
					return new RoutedEvent[]
					{
						Keyboard.KeyDownEvent,
						TextCompositionManager.TextInputEvent
					};
				case (SynchronizedInputType)3:
					goto IL_7C;
				case SynchronizedInputType.MouseLeftButtonUp:
					goto IL_6B;
				default:
					if (inputType != SynchronizedInputType.MouseLeftButtonDown)
					{
						goto IL_7C;
					}
					break;
				}
			}
			else
			{
				if (inputType == SynchronizedInputType.MouseRightButtonUp)
				{
					goto IL_6B;
				}
				if (inputType != SynchronizedInputType.MouseRightButtonDown)
				{
					goto IL_7C;
				}
			}
			return new RoutedEvent[]
			{
				Mouse.MouseDownEvent
			};
			IL_6B:
			return new RoutedEvent[]
			{
				Mouse.MouseUpEvent
			};
			IL_7C:
			return null;
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00120E90 File Offset: 0x00120290
		internal static void RaiseAutomationEvents()
		{
			if (InputElement.IsUIElement(InputManager.ListeningElement))
			{
				UIElement uielement = (UIElement)InputManager.ListeningElement;
				SynchronizedInputHelper.RaiseAutomationEvent(uielement.GetAutomationPeer());
				return;
			}
			if (InputElement.IsContentElement(InputManager.ListeningElement))
			{
				ContentElement contentElement = (ContentElement)InputManager.ListeningElement;
				SynchronizedInputHelper.RaiseAutomationEvent(contentElement.GetAutomationPeer());
				return;
			}
			if (InputElement.IsUIElement3D(InputManager.ListeningElement))
			{
				UIElement3D uielement3D = (UIElement3D)InputManager.ListeningElement;
				SynchronizedInputHelper.RaiseAutomationEvent(uielement3D.GetAutomationPeer());
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00120F08 File Offset: 0x00120308
		internal static void RaiseAutomationEvent(AutomationPeer peer)
		{
			if (peer != null)
			{
				SynchronizedInputStates synchronizedInputState = InputManager.SynchronizedInputState;
				if (synchronizedInputState == SynchronizedInputStates.Handled)
				{
					peer.RaiseAutomationEvent(AutomationEvents.InputReachedTarget);
					return;
				}
				if (synchronizedInputState == SynchronizedInputStates.Discarded)
				{
					peer.RaiseAutomationEvent(AutomationEvents.InputDiscarded);
					return;
				}
				peer.RaiseAutomationEvent(AutomationEvents.InputReachedOtherElement);
			}
		}
	}
}
