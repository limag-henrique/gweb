using System;
using System.Security;

namespace System.Windows.Input
{
	/// <summary>Representa o dispositivo de teclado.</summary>
	// Token: 0x02000266 RID: 614
	public static class Keyboard
	{
		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001131 RID: 4401 RVA: 0x00040ACC File Offset: 0x0003FECC
		public static void AddPreviewKeyDownHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.PreviewKeyDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001132 RID: 4402 RVA: 0x00040AE8 File Offset: 0x0003FEE8
		public static void RemovePreviewKeyDownHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.PreviewKeyDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001133 RID: 4403 RVA: 0x00040B04 File Offset: 0x0003FF04
		public static void AddKeyDownHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.KeyDownEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyDown" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001134 RID: 4404 RVA: 0x00040B20 File Offset: 0x0003FF20
		public static void RemoveKeyDownHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.KeyDownEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001135 RID: 4405 RVA: 0x00040B3C File Offset: 0x0003FF3C
		public static void AddPreviewKeyUpHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.PreviewKeyUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001136 RID: 4406 RVA: 0x00040B58 File Offset: 0x0003FF58
		public static void RemovePreviewKeyUpHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.PreviewKeyUpEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001137 RID: 4407 RVA: 0x00040B74 File Offset: 0x0003FF74
		public static void AddKeyUpHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.KeyUpEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyUp" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001138 RID: 4408 RVA: 0x00040B90 File Offset: 0x0003FF90
		public static void RemoveKeyUpHandler(DependencyObject element, KeyEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.KeyUpEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewGotKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001139 RID: 4409 RVA: 0x00040BAC File Offset: 0x0003FFAC
		public static void AddPreviewGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.PreviewGotKeyboardFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewGotKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600113A RID: 4410 RVA: 0x00040BC8 File Offset: 0x0003FFC8
		public static void RemovePreviewGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.PreviewGotKeyboardFocusEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyboardInputProviderAcquireFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600113B RID: 4411 RVA: 0x00040BE4 File Offset: 0x0003FFE4
		public static void AddPreviewKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.PreviewKeyboardInputProviderAcquireFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyboardInputProviderAcquireFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600113C RID: 4412 RVA: 0x00040C00 File Offset: 0x00040000
		public static void RemovePreviewKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.PreviewKeyboardInputProviderAcquireFocusEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyboardInputProviderAcquireFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600113D RID: 4413 RVA: 0x00040C1C File Offset: 0x0004001C
		public static void AddKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.KeyboardInputProviderAcquireFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyboardInputProviderAcquireFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x0600113E RID: 4414 RVA: 0x00040C38 File Offset: 0x00040038
		public static void RemoveKeyboardInputProviderAcquireFocusHandler(DependencyObject element, KeyboardInputProviderAcquireFocusEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.KeyboardInputProviderAcquireFocusEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.GotKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x0600113F RID: 4415 RVA: 0x00040C54 File Offset: 0x00040054
		public static void AddGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.GotKeyboardFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.GotKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001140 RID: 4416 RVA: 0x00040C70 File Offset: 0x00040070
		public static void RemoveGotKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.GotKeyboardFocusEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewLostKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001141 RID: 4417 RVA: 0x00040C8C File Offset: 0x0004008C
		public static void AddPreviewLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.PreviewLostKeyboardFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewLostKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001142 RID: 4418 RVA: 0x00040CA8 File Offset: 0x000400A8
		public static void RemovePreviewLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.PreviewLostKeyboardFocusEvent, handler);
		}

		/// <summary>Adiciona um manipulador ao evento anexado <see cref="E:System.Windows.Input.Keyboard.LostKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser adicionado.</param>
		// Token: 0x06001143 RID: 4419 RVA: 0x00040CC4 File Offset: 0x000400C4
		public static void AddLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.AddHandler(element, Keyboard.LostKeyboardFocusEvent, handler);
		}

		/// <summary>Remove um manipulador para o evento anexado <see cref="E:System.Windows.Input.Keyboard.LostKeyboardFocus" />.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" /> que escuta esse evento.</param>
		/// <param name="handler">O manipulador de eventos a ser removido.</param>
		// Token: 0x06001144 RID: 4420 RVA: 0x00040CE0 File Offset: 0x000400E0
		public static void RemoveLostKeyboardFocusHandler(DependencyObject element, KeyboardFocusChangedEventHandler handler)
		{
			UIElement.RemoveHandler(element, Keyboard.LostKeyboardFocusEvent, handler);
		}

		/// <summary>Obtém o elemento que tem o foco do teclado.</summary>
		/// <returns>O elemento focalizado.</returns>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00040CFC File Offset: 0x000400FC
		public static IInputElement FocusedElement
		{
			get
			{
				return Keyboard.PrimaryDevice.FocusedElement;
			}
		}

		/// <summary>Limpa o foco.</summary>
		// Token: 0x06001146 RID: 4422 RVA: 0x00040D14 File Offset: 0x00040114
		public static void ClearFocus()
		{
			Keyboard.PrimaryDevice.ClearFocus();
		}

		/// <summary>Determina o foco do teclado no elemento especificado.</summary>
		/// <param name="element">O elemento no qual definir o foco do teclado.</param>
		/// <returns>O elemento com foco do teclado.</returns>
		// Token: 0x06001147 RID: 4423 RVA: 0x00040D2C File Offset: 0x0004012C
		public static IInputElement Focus(IInputElement element)
		{
			return Keyboard.PrimaryDevice.Focus(element);
		}

		/// <summary>Obtém ou define o comportamento de Windows Presentation Foundation (WPF) ao restaurar o foco.</summary>
		/// <returns>Um valor de enumeração que especifica o comportamento de WPF ao restaurar o foco. O padrão no <see cref="F:System.Windows.Input.RestoreFocusMode.Auto" />.</returns>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00040D44 File Offset: 0x00040144
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x00040D5C File Offset: 0x0004015C
		public static RestoreFocusMode DefaultRestoreFocusMode
		{
			get
			{
				return Keyboard.PrimaryDevice.DefaultRestoreFocusMode;
			}
			set
			{
				Keyboard.PrimaryDevice.DefaultRestoreFocusMode = value;
			}
		}

		/// <summary>Obtém o conjunto de <see cref="T:System.Windows.Input.ModifierKeys" /> pressionadas no momento.</summary>
		/// <returns>Uma combinação bit a bit dos valores <see cref="T:System.Windows.Input.ModifierKeys" />.</returns>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00040D74 File Offset: 0x00040174
		public static ModifierKeys Modifiers
		{
			get
			{
				return Keyboard.PrimaryDevice.Modifiers;
			}
		}

		/// <summary>Determina se a tecla especificada está pressionada.</summary>
		/// <param name="key">A tecla especificada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="key" /> estiver no estado desativado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600114B RID: 4427 RVA: 0x00040D8C File Offset: 0x0004018C
		public static bool IsKeyDown(Key key)
		{
			return Keyboard.PrimaryDevice.IsKeyDown(key);
		}

		/// <summary>Determina se a tecla especificada está solta.</summary>
		/// <param name="key">A tecla a ser verificada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="key" /> estiver no estado ativado, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600114C RID: 4428 RVA: 0x00040DA4 File Offset: 0x000401A4
		public static bool IsKeyUp(Key key)
		{
			return Keyboard.PrimaryDevice.IsKeyUp(key);
		}

		/// <summary>Determina se a tecla especificada está ativada.</summary>
		/// <param name="key">A tecla especificada.</param>
		/// <returns>
		///   <see langword="true" /> se a <paramref name="key" /> estiver no estado ativado, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600114D RID: 4429 RVA: 0x00040DBC File Offset: 0x000401BC
		public static bool IsKeyToggled(Key key)
		{
			return Keyboard.PrimaryDevice.IsKeyToggled(key);
		}

		/// <summary>Obtém o conjunto de estados principais para a chave especificada.</summary>
		/// <param name="key">A tecla especificada.</param>
		/// <returns>Uma combinação bit a bit dos valores <see cref="T:System.Windows.Input.KeyStates" />.</returns>
		// Token: 0x0600114E RID: 4430 RVA: 0x00040DD4 File Offset: 0x000401D4
		public static KeyStates GetKeyStates(Key key)
		{
			return Keyboard.PrimaryDevice.GetKeyStates(key);
		}

		/// <summary>Obtém o dispositivo de entrada do teclado primário.</summary>
		/// <returns>O dispositivo.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00040DEC File Offset: 0x000401EC
		public static KeyboardDevice PrimaryDevice
		{
			[SecurityCritical]
			get
			{
				return InputManager.UnsecureCurrent.PrimaryKeyboardDevice;
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00040E08 File Offset: 0x00040208
		internal static bool IsValidKey(Key key)
		{
			return key >= Key.None && key <= Key.OemClear;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00040E28 File Offset: 0x00040228
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal static bool IsFocusable(DependencyObject element)
		{
			if (element == null)
			{
				return false;
			}
			UIElement uielement = element as UIElement;
			if (uielement != null && !uielement.IsVisible)
			{
				return false;
			}
			if (!(bool)element.GetValue(UIElement.IsEnabledProperty))
			{
				return false;
			}
			bool flag = false;
			BaseValueSourceInternal valueSource = element.GetValueSource(UIElement.FocusableProperty, null, out flag);
			bool flag2 = (bool)element.GetValue(UIElement.FocusableProperty);
			if (!flag2 && valueSource == BaseValueSourceInternal.Default && !flag)
			{
				if (FocusManager.GetIsFocusScope(element))
				{
					return true;
				}
				if (uielement != null && uielement.InternalVisualParent == null)
				{
					PresentationSource presentationSource = PresentationSource.CriticalFromVisual(uielement);
					if (presentationSource != null)
					{
						return true;
					}
				}
			}
			return flag2;
		}

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" /> anexado.</summary>
		// Token: 0x0400097C RID: 2428
		public static readonly RoutedEvent PreviewKeyDownEvent = EventManager.RegisterRoutedEvent("PreviewKeyDown", RoutingStrategy.Tunnel, typeof(KeyEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.KeyDown" /> anexado.</summary>
		// Token: 0x0400097D RID: 2429
		public static readonly RoutedEvent KeyDownEvent = EventManager.RegisterRoutedEvent("KeyDown", RoutingStrategy.Bubble, typeof(KeyEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.PreviewKeyUp" /> anexado.</summary>
		// Token: 0x0400097E RID: 2430
		public static readonly RoutedEvent PreviewKeyUpEvent = EventManager.RegisterRoutedEvent("PreviewKeyUp", RoutingStrategy.Tunnel, typeof(KeyEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.KeyUp" /> anexado.</summary>
		// Token: 0x0400097F RID: 2431
		public static readonly RoutedEvent KeyUpEvent = EventManager.RegisterRoutedEvent("KeyUp", RoutingStrategy.Bubble, typeof(KeyEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.PreviewGotKeyboardFocus" /> anexado.</summary>
		// Token: 0x04000980 RID: 2432
		public static readonly RoutedEvent PreviewGotKeyboardFocusEvent = EventManager.RegisterRoutedEvent("PreviewGotKeyboardFocus", RoutingStrategy.Tunnel, typeof(KeyboardFocusChangedEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.PreviewKeyboardInputProviderAcquireFocus" /> anexado.</summary>
		// Token: 0x04000981 RID: 2433
		public static readonly RoutedEvent PreviewKeyboardInputProviderAcquireFocusEvent = EventManager.RegisterRoutedEvent("PreviewKeyboardInputProviderAcquireFocus", RoutingStrategy.Tunnel, typeof(KeyboardInputProviderAcquireFocusEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.KeyboardInputProviderAcquireFocus" /> anexado.</summary>
		// Token: 0x04000982 RID: 2434
		public static readonly RoutedEvent KeyboardInputProviderAcquireFocusEvent = EventManager.RegisterRoutedEvent("KeyboardInputProviderAcquireFocus", RoutingStrategy.Bubble, typeof(KeyboardInputProviderAcquireFocusEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.GotKeyboardFocus" /> anexado.</summary>
		// Token: 0x04000983 RID: 2435
		public static readonly RoutedEvent GotKeyboardFocusEvent = EventManager.RegisterRoutedEvent("GotKeyboardFocus", RoutingStrategy.Bubble, typeof(KeyboardFocusChangedEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.PreviewLostKeyboardFocus" /> anexado.</summary>
		// Token: 0x04000984 RID: 2436
		public static readonly RoutedEvent PreviewLostKeyboardFocusEvent = EventManager.RegisterRoutedEvent("PreviewLostKeyboardFocus", RoutingStrategy.Tunnel, typeof(KeyboardFocusChangedEventHandler), typeof(Keyboard));

		/// <summary>Identifica o evento <see cref="E:System.Windows.Input.Keyboard.LostKeyboardFocus" /> anexado.</summary>
		// Token: 0x04000985 RID: 2437
		public static readonly RoutedEvent LostKeyboardFocusEvent = EventManager.RegisterRoutedEvent("LostKeyboardFocus", RoutingStrategy.Bubble, typeof(KeyboardFocusChangedEventHandler), typeof(Keyboard));
	}
}
