using System;
using System.Security;
using System.Windows.Threading;
using MS.Internal;

namespace System.Windows.Input
{
	// Token: 0x020002DF RID: 735
	internal class TextServicesManager : DispatcherObject
	{
		// Token: 0x06001655 RID: 5717 RVA: 0x00053E78 File Offset: 0x00053278
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal TextServicesManager(InputManager inputManager)
		{
			this._inputManager = inputManager;
			this._inputManager.PreProcessInput += this.PreProcessInput;
			this._inputManager.PostProcessInput += this.PostProcessInput;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00053EC0 File Offset: 0x000532C0
		internal void Focus(DependencyObject focus)
		{
			if (focus == null)
			{
				base.Dispatcher.IsTSFMessagePumpEnabled = false;
				return;
			}
			base.Dispatcher.IsTSFMessagePumpEnabled = true;
			if ((bool)focus.GetValue(InputMethod.IsInputMethodSuspendedProperty))
			{
				return;
			}
			InputMethod.Current.EnableOrDisableInputMethod((bool)focus.GetValue(InputMethod.IsInputMethodEnabledProperty));
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00053F18 File Offset: 0x00053318
		[SecurityCritical]
		private void PreProcessInput(object sender, PreProcessInputEventArgs e)
		{
			if (!TextServicesLoader.ServicesInstalled)
			{
				return;
			}
			if (e.StagingItem.Input.RoutedEvent != Keyboard.PreviewKeyDownEvent && e.StagingItem.Input.RoutedEvent != Keyboard.PreviewKeyUpEvent)
			{
				return;
			}
			if (this.IsSysKeyDown())
			{
				return;
			}
			if (InputMethod.IsImm32ImeCurrent())
			{
				return;
			}
			DependencyObject dependencyObject = Keyboard.FocusedElement as DependencyObject;
			if (dependencyObject == null || (bool)dependencyObject.GetValue(InputMethod.IsInputMethodSuspendedProperty))
			{
				return;
			}
			KeyEventArgs keyEventArgs = (KeyEventArgs)e.StagingItem.Input;
			if (!keyEventArgs.Handled)
			{
				TextServicesContext dispatcherCurrent = TextServicesContext.DispatcherCurrent;
				if (dispatcherCurrent != null && this.TextServicesKeystroke(dispatcherCurrent, keyEventArgs, true))
				{
					keyEventArgs.MarkImeProcessed();
				}
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00053FC4 File Offset: 0x000533C4
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (!TextServicesLoader.ServicesInstalled)
			{
				return;
			}
			if (InputMethod.IsImm32ImeCurrent())
			{
				return;
			}
			DependencyObject dependencyObject = Keyboard.FocusedElement as DependencyObject;
			if (dependencyObject == null || (bool)dependencyObject.GetValue(InputMethod.IsInputMethodSuspendedProperty))
			{
				return;
			}
			if (e.StagingItem.Input.RoutedEvent == Keyboard.PreviewKeyDownEvent || e.StagingItem.Input.RoutedEvent == Keyboard.PreviewKeyUpEvent)
			{
				if (this.IsSysKeyDown())
				{
					return;
				}
				KeyEventArgs keyEventArgs = (KeyEventArgs)e.StagingItem.Input;
				if (!keyEventArgs.Handled && keyEventArgs.Key == Key.ImeProcessed)
				{
					TextServicesContext dispatcherCurrent = TextServicesContext.DispatcherCurrent;
					if (dispatcherCurrent != null && this.TextServicesKeystroke(dispatcherCurrent, keyEventArgs, false))
					{
						keyEventArgs.Handled = true;
						return;
					}
				}
			}
			else if (e.StagingItem.Input.RoutedEvent == Keyboard.KeyDownEvent || e.StagingItem.Input.RoutedEvent == Keyboard.KeyUpEvent)
			{
				KeyEventArgs keyEventArgs = (KeyEventArgs)e.StagingItem.Input;
				if (!keyEventArgs.Handled && keyEventArgs.Key == Key.ImeProcessed)
				{
					keyEventArgs.Handled = true;
				}
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000540DC File Offset: 0x000534DC
		[SecurityCritical]
		private bool TextServicesKeystroke(TextServicesContext context, KeyEventArgs keyArgs, bool test)
		{
			Key realKey = keyArgs.RealKey;
			int wParam;
			int num;
			if (realKey != Key.LeftShift)
			{
				if (realKey == Key.RightShift)
				{
					wParam = 16;
					num = 54;
				}
				else
				{
					wParam = KeyInterop.VirtualKeyFromKey(keyArgs.RealKey);
					num = 0;
				}
			}
			else
			{
				wParam = 16;
				num = 42;
			}
			int num2 = num << 16 | 1;
			TextServicesContext.KeyOp op;
			if (keyArgs.RoutedEvent == Keyboard.PreviewKeyDownEvent)
			{
				op = (test ? TextServicesContext.KeyOp.TestDown : TextServicesContext.KeyOp.Down);
			}
			else
			{
				num2 |= -1073741824;
				op = (test ? TextServicesContext.KeyOp.TestUp : TextServicesContext.KeyOp.Up);
			}
			return context.Keystroke(wParam, num2, op);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00054154 File Offset: 0x00053554
		private bool IsSysKeyDown()
		{
			return Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt) || Keyboard.IsKeyDown(Key.F10);
		}

		// Token: 0x04000C2A RID: 3114
		[SecurityCritical]
		private readonly InputManager _inputManager;
	}
}
