using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Automation.Peers;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;

namespace System.Windows.Input
{
	/// <summary>Classe abstrata que representa um dispositivo de teclado.</summary>
	// Token: 0x02000267 RID: 615
	public abstract class KeyboardDevice : InputDevice
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.KeyboardDevice" />.</summary>
		/// <param name="inputManager">O gerenciador de entrada associado a este <see cref="T:System.Windows.Input.KeyboardDevice" />.</param>
		// Token: 0x06001153 RID: 4435 RVA: 0x0004102C File Offset: 0x0004042C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected KeyboardDevice(InputManager inputManager)
		{
			this._inputManager = new SecurityCriticalDataClass<InputManager>(inputManager);
			this._inputManager.Value.PreProcessInput += this.PreProcessInput;
			this._inputManager.Value.PreNotifyInput += this.PreNotifyInput;
			this._inputManager.Value.PostProcessInput += this.PostProcessInput;
			this._isEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnIsEnabledChanged);
			this._isVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
			this._focusableChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnFocusableChanged);
			this._reevaluateFocusCallback = new DispatcherOperationCallback(this.ReevaluateFocusCallback);
			this._reevaluateFocusOperation = null;
			this._TsfManager = new SecurityCriticalDataClass<TextServicesManager>(new TextServicesManager(inputManager));
			this._textcompositionManager = new SecurityCriticalData<TextCompositionManager>(new TextCompositionManager(inputManager));
		}

		/// <summary>Quando substituído em uma classe derivada, obtém o <see cref="T:System.Windows.Input.KeyStates" /> para o <see cref="T:System.Windows.Input.Key" /> especificado.</summary>
		/// <param name="key">A tecla a ser verificada.</param>
		/// <returns>O conjunto de estados principais para a chave especificada.</returns>
		// Token: 0x06001154 RID: 4436
		protected abstract KeyStates GetKeyStatesFromSystem(Key key);

		/// <summary>Obtém o <see cref="T:System.Windows.IInputElement" /> especificado para o qual a entrada deste dispositivo é enviada.</summary>
		/// <returns>O elemento que recebe entrada.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00041134 File Offset: 0x00040534
		public override IInputElement Target
		{
			get
			{
				if (this.ForceTarget != null)
				{
					return this.ForceTarget;
				}
				return this.FocusedElement;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00041158 File Offset: 0x00040558
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x00041170 File Offset: 0x00040570
		internal IInputElement ForceTarget
		{
			get
			{
				return (IInputElement)this._forceTarget;
			}
			set
			{
				this._forceTarget = (value as DependencyObject);
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.PresentationSource" /> que está relatando a entrada para este dispositivo.</summary>
		/// <returns>A fonte de entrada para este dispositivo.</returns>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0004118C File Offset: 0x0004058C
		public override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				if (this._activeSource != null)
				{
					return this._activeSource.Value;
				}
				return null;
			}
		}

		/// <summary>Obtém ou define o comportamento de Windows Presentation Foundation (WPF) ao restaurar o foco.</summary>
		/// <returns>Um valor de enumeração que especifica o comportamento de WPF ao restaurar o foco. O padrão no <see cref="F:System.Windows.Input.RestoreFocusMode.Auto" />.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x000411B4 File Offset: 0x000405B4
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x000411C8 File Offset: 0x000405C8
		public RestoreFocusMode DefaultRestoreFocusMode { get; set; }

		/// <summary>Obtém o elemento que tem o foco do teclado.</summary>
		/// <returns>O elemento com foco do teclado.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x000411DC File Offset: 0x000405DC
		public IInputElement FocusedElement
		{
			get
			{
				return (IInputElement)this._focus;
			}
		}

		/// <summary>Limpa o foco.</summary>
		// Token: 0x0600115C RID: 4444 RVA: 0x000411F4 File Offset: 0x000405F4
		public void ClearFocus()
		{
			this.Focus(null, false, false, false);
		}

		/// <summary>Determina o foco do teclado no <see cref="T:System.Windows.IInputElement" /> especificado.</summary>
		/// <param name="element">O elemento para o qual mover o foco.</param>
		/// <returns>O elemento que tem o foco do teclado.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="element" /> não é um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />.</exception>
		// Token: 0x0600115D RID: 4445 RVA: 0x0004120C File Offset: 0x0004060C
		[SecurityCritical]
		public IInputElement Focus(IInputElement element)
		{
			DependencyObject dependencyObject = null;
			bool forceToNullIfFailed = false;
			if (element != null)
			{
				if (!InputElement.IsValid(element))
				{
					throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
					{
						element.GetType()
					}));
				}
				dependencyObject = (DependencyObject)element;
			}
			if (dependencyObject == null && this._activeSource != null)
			{
				dependencyObject = this._activeSource.Value.RootVisual;
				forceToNullIfFailed = true;
			}
			this.Focus(dependencyObject, true, true, forceToNullIfFailed);
			return (IInputElement)this._focus;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00041284 File Offset: 0x00040684
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void Focus(DependencyObject focus, bool askOld, bool askNew, bool forceToNullIfFailed)
		{
			bool flag = true;
			if (focus != null)
			{
				flag = Keyboard.IsFocusable(focus);
				if (!flag && forceToNullIfFailed)
				{
					focus = null;
					flag = true;
				}
			}
			if (flag)
			{
				IKeyboardInputProvider keyboardInputProvider = null;
				DependencyObject containingVisual = InputElement.GetContainingVisual(focus);
				if (containingVisual != null)
				{
					PresentationSource presentationSource = PresentationSource.CriticalFromVisual(containingVisual);
					if (presentationSource != null)
					{
						keyboardInputProvider = (IKeyboardInputProvider)presentationSource.GetInputProvider(typeof(KeyboardDevice));
					}
				}
				this.TryChangeFocus(focus, keyboardInputProvider, askOld, askNew, forceToNullIfFailed);
			}
		}

		/// <summary>Obtém o conjunto de <see cref="T:System.Windows.Input.ModifierKeys" /> que estão pressionados no momento.</summary>
		/// <returns>O conjunto de chaves de modificador.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000412E8 File Offset: 0x000406E8
		public ModifierKeys Modifiers
		{
			get
			{
				ModifierKeys modifierKeys = ModifierKeys.None;
				if (this.IsKeyDown_private(Key.LeftAlt) || this.IsKeyDown_private(Key.RightAlt))
				{
					modifierKeys |= ModifierKeys.Alt;
				}
				if (this.IsKeyDown_private(Key.LeftCtrl) || this.IsKeyDown_private(Key.RightCtrl))
				{
					modifierKeys |= ModifierKeys.Control;
				}
				if (this.IsKeyDown_private(Key.LeftShift) || this.IsKeyDown_private(Key.RightShift))
				{
					modifierKeys |= ModifierKeys.Shift;
				}
				return modifierKeys;
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00041340 File Offset: 0x00040740
		private void Validate_Key(Key key)
		{
			if ((Key)256 <= key || key <= Key.None)
			{
				throw new InvalidEnumArgumentException("key", (int)key, typeof(Key));
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00041370 File Offset: 0x00040770
		private bool IsKeyDown_private(Key key)
		{
			return (this.GetKeyStatesFromSystem(key) & KeyStates.Down) == KeyStates.Down;
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.Key" /> especificado está no estado inativo.</summary>
		/// <param name="key">A tecla a ser verificada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="key" /> estiver no estado desativado; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="key" /> não é uma chave válida.</exception>
		// Token: 0x06001162 RID: 4450 RVA: 0x0004138C File Offset: 0x0004078C
		public bool IsKeyDown(Key key)
		{
			this.Validate_Key(key);
			return this.IsKeyDown_private(key);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.Key" /> especificado está no estado ativo.</summary>
		/// <param name="key">A tecla a ser verificada.</param>
		/// <returns>
		///   <see langword="true" /> se <paramref name="key" /> estiver no estado ativado, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="key" /> não é uma chave válida.</exception>
		// Token: 0x06001163 RID: 4451 RVA: 0x000413A8 File Offset: 0x000407A8
		public bool IsKeyUp(Key key)
		{
			this.Validate_Key(key);
			return !this.IsKeyDown_private(key);
		}

		/// <summary>Determina se o <see cref="T:System.Windows.Input.Key" /> especificado está no estado alternado.</summary>
		/// <param name="key">A tecla a ser verificada.</param>
		/// <returns>
		///   <see langword="true" /> se a <paramref name="key" /> estiver no estado ativado, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="key" /> não é uma chave válida.</exception>
		// Token: 0x06001164 RID: 4452 RVA: 0x000413C8 File Offset: 0x000407C8
		public bool IsKeyToggled(Key key)
		{
			this.Validate_Key(key);
			return (this.GetKeyStatesFromSystem(key) & KeyStates.Toggled) == KeyStates.Toggled;
		}

		/// <summary>Obtém o conjunto de estados principais para o <see cref="T:System.Windows.Input.Key" /> especificado.</summary>
		/// <param name="key">A tecla a ser verificada.</param>
		/// <returns>O conjunto de estados principais para a chave especificada.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="key" /> não é uma chave válida.</exception>
		// Token: 0x06001165 RID: 4453 RVA: 0x000413E8 File Offset: 0x000407E8
		public KeyStates GetKeyStates(Key key)
		{
			this.Validate_Key(key);
			return this.GetKeyStatesFromSystem(key);
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00041404 File Offset: 0x00040804
		internal TextServicesManager TextServicesManager
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				return this._TsfManager.Value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00041424 File Offset: 0x00040824
		internal TextCompositionManager TextCompositionManager
		{
			[SecurityTreatAsSafe]
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUnrestrictedUIPermission();
				return this._textcompositionManager.Value;
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00041444 File Offset: 0x00040844
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void TryChangeFocus(DependencyObject newFocus, IKeyboardInputProvider keyboardInputProvider, bool askOld, bool askNew, bool forceToNullIfFailed)
		{
			bool flag = true;
			int tickCount = Environment.TickCount;
			DependencyObject focus = this._focus;
			if (newFocus != this._focus)
			{
				if (askOld && this._focus != null)
				{
					KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs = new KeyboardFocusChangedEventArgs(this, tickCount, (IInputElement)this._focus, (IInputElement)newFocus);
					keyboardFocusChangedEventArgs.RoutedEvent = Keyboard.PreviewLostKeyboardFocusEvent;
					keyboardFocusChangedEventArgs.Source = this._focus;
					if (this._inputManager != null)
					{
						this._inputManager.Value.ProcessInput(keyboardFocusChangedEventArgs);
					}
					if (keyboardFocusChangedEventArgs.Handled)
					{
						flag = false;
					}
				}
				if (askNew && flag && newFocus != null)
				{
					KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs2 = new KeyboardFocusChangedEventArgs(this, tickCount, (IInputElement)this._focus, (IInputElement)newFocus);
					keyboardFocusChangedEventArgs2.RoutedEvent = Keyboard.PreviewGotKeyboardFocusEvent;
					keyboardFocusChangedEventArgs2.Source = newFocus;
					if (this._inputManager != null)
					{
						this._inputManager.Value.ProcessInput(keyboardFocusChangedEventArgs2);
					}
					if (keyboardFocusChangedEventArgs2.Handled)
					{
						flag = false;
					}
				}
				if (flag && newFocus != null)
				{
					if (keyboardInputProvider != null && Keyboard.IsFocusable(newFocus))
					{
						KeyboardInputProviderAcquireFocusEventArgs keyboardInputProviderAcquireFocusEventArgs = new KeyboardInputProviderAcquireFocusEventArgs(this, tickCount, flag);
						keyboardInputProviderAcquireFocusEventArgs.RoutedEvent = Keyboard.PreviewKeyboardInputProviderAcquireFocusEvent;
						keyboardInputProviderAcquireFocusEventArgs.Source = newFocus;
						if (this._inputManager != null)
						{
							this._inputManager.Value.ProcessInput(keyboardInputProviderAcquireFocusEventArgs);
						}
						flag = keyboardInputProvider.AcquireFocus(false);
						keyboardInputProviderAcquireFocusEventArgs = new KeyboardInputProviderAcquireFocusEventArgs(this, tickCount, flag);
						keyboardInputProviderAcquireFocusEventArgs.RoutedEvent = Keyboard.KeyboardInputProviderAcquireFocusEvent;
						keyboardInputProviderAcquireFocusEventArgs.Source = newFocus;
						if (this._inputManager != null)
						{
							this._inputManager.Value.ProcessInput(keyboardInputProviderAcquireFocusEventArgs);
						}
					}
					else
					{
						flag = false;
					}
				}
				if (!flag && forceToNullIfFailed && focus == this._focus)
				{
					IInputElement inputElement = newFocus as IInputElement;
					if (inputElement == null || !inputElement.IsKeyboardFocusWithin)
					{
						newFocus = null;
						flag = true;
					}
				}
				if (flag)
				{
					this.ChangeFocus(newFocus, tickCount);
				}
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000415F4 File Offset: 0x000409F4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void ChangeFocus(DependencyObject focus, int timestamp)
		{
			if (focus != this._focus)
			{
				DependencyObject focus2 = this._focus;
				this._focus = focus;
				this._focusRootVisual = InputElement.GetRootVisual(focus);
				using (base.Dispatcher.DisableProcessing())
				{
					if (focus2 != null)
					{
						DependencyObject dependencyObject = focus2;
						if (InputElement.IsUIElement(dependencyObject))
						{
							((UIElement)dependencyObject).IsEnabledChanged -= this._isEnabledChangedEventHandler;
							((UIElement)dependencyObject).IsVisibleChanged -= this._isVisibleChangedEventHandler;
							((UIElement)dependencyObject).FocusableChanged -= this._focusableChangedEventHandler;
						}
						else if (InputElement.IsContentElement(dependencyObject))
						{
							((ContentElement)dependencyObject).IsEnabledChanged -= this._isEnabledChangedEventHandler;
							((ContentElement)dependencyObject).FocusableChanged -= this._focusableChangedEventHandler;
						}
						else
						{
							((UIElement3D)dependencyObject).IsEnabledChanged -= this._isEnabledChangedEventHandler;
							((UIElement3D)dependencyObject).IsVisibleChanged -= this._isVisibleChangedEventHandler;
							((UIElement3D)dependencyObject).FocusableChanged -= this._focusableChangedEventHandler;
						}
					}
					if (this._focus != null)
					{
						DependencyObject dependencyObject = this._focus;
						if (InputElement.IsUIElement(dependencyObject))
						{
							((UIElement)dependencyObject).IsEnabledChanged += this._isEnabledChangedEventHandler;
							((UIElement)dependencyObject).IsVisibleChanged += this._isVisibleChangedEventHandler;
							((UIElement)dependencyObject).FocusableChanged += this._focusableChangedEventHandler;
						}
						else if (InputElement.IsContentElement(dependencyObject))
						{
							((ContentElement)dependencyObject).IsEnabledChanged += this._isEnabledChangedEventHandler;
							((ContentElement)dependencyObject).FocusableChanged += this._focusableChangedEventHandler;
						}
						else
						{
							((UIElement3D)dependencyObject).IsEnabledChanged += this._isEnabledChangedEventHandler;
							((UIElement3D)dependencyObject).IsVisibleChanged += this._isVisibleChangedEventHandler;
							((UIElement3D)dependencyObject).FocusableChanged += this._focusableChangedEventHandler;
						}
					}
				}
				UIElement.FocusWithinProperty.OnOriginValueChanged(focus2, this._focus, ref this._focusTreeState);
				if (focus2 != null)
				{
					DependencyObject dependencyObject = focus2;
					dependencyObject.SetValue(UIElement.IsKeyboardFocusedPropertyKey, false);
				}
				if (this._focus != null)
				{
					DependencyObject dependencyObject = this._focus;
					dependencyObject.SetValue(UIElement.IsKeyboardFocusedPropertyKey, true);
				}
				if (this._TsfManager != null)
				{
					this._TsfManager.Value.Focus(this._focus);
				}
				InputLanguageManager.Current.Focus(this._focus, focus2);
				if (focus2 != null)
				{
					KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs = new KeyboardFocusChangedEventArgs(this, timestamp, (IInputElement)focus2, (IInputElement)focus);
					keyboardFocusChangedEventArgs.RoutedEvent = Keyboard.LostKeyboardFocusEvent;
					keyboardFocusChangedEventArgs.Source = focus2;
					if (this._inputManager != null)
					{
						this._inputManager.Value.ProcessInput(keyboardFocusChangedEventArgs);
					}
				}
				if (this._focus != null)
				{
					KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs2 = new KeyboardFocusChangedEventArgs(this, timestamp, (IInputElement)focus2, (IInputElement)this._focus);
					keyboardFocusChangedEventArgs2.RoutedEvent = Keyboard.GotKeyboardFocusEvent;
					keyboardFocusChangedEventArgs2.Source = this._focus;
					if (this._inputManager != null)
					{
						this._inputManager.Value.ProcessInput(keyboardFocusChangedEventArgs2);
					}
				}
				InputMethod.Current.GotKeyboardFocus(this._focus);
				AutomationPeer.RaiseFocusChangedEventHelper((IInputElement)this._focus);
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000418DC File Offset: 0x00040CDC
		private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateFocusAsync(null, null, false);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000418F4 File Offset: 0x00040CF4
		private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateFocusAsync(null, null, false);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0004190C File Offset: 0x00040D0C
		private void OnFocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateFocusAsync(null, null, false);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00041924 File Offset: 0x00040D24
		internal void ReevaluateFocusAsync(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (isCoreParent)
				{
					this.FocusTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this.FocusTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateFocusOperation == null)
			{
				this._reevaluateFocusOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._reevaluateFocusCallback, null);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00041974 File Offset: 0x00040D74
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private object ReevaluateFocusCallback(object arg)
		{
			this._reevaluateFocusOperation = null;
			if (this._focus == null)
			{
				return null;
			}
			DependencyObject dependencyObject = this._focus;
			while (dependencyObject != null && !Keyboard.IsFocusable(dependencyObject))
			{
				dependencyObject = DeferredElementTreeState.GetCoreParent(dependencyObject, null);
			}
			PresentationSource presentationSource = null;
			DependencyObject containingVisual = InputElement.GetContainingVisual(dependencyObject);
			if (containingVisual != null)
			{
				presentationSource = PresentationSource.CriticalFromVisual(containingVisual);
			}
			bool flag = true;
			DependencyObject dependencyObject2 = null;
			if (presentationSource != null)
			{
				IKeyboardInputProvider keyboardInputProvider = presentationSource.GetInputProvider(typeof(KeyboardDevice)) as IKeyboardInputProvider;
				if (keyboardInputProvider != null && keyboardInputProvider.AcquireFocus(true))
				{
					if (dependencyObject == this._focus)
					{
						flag = false;
					}
					else
					{
						flag = true;
						dependencyObject2 = dependencyObject;
					}
				}
			}
			if (flag)
			{
				if (dependencyObject2 == null && this._activeSource != null)
				{
					dependencyObject2 = this._activeSource.Value.RootVisual;
				}
				this.Focus(dependencyObject2, false, true, true);
			}
			else if (this._focusTreeState != null && !this._focusTreeState.IsEmpty)
			{
				UIElement.FocusWithinProperty.OnOriginValueChanged(this._focus, this._focus, ref this._focusTreeState);
			}
			return null;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00041A60 File Offset: 0x00040E60
		[SecurityCritical]
		private void PreProcessInput(object sender, PreProcessInputEventArgs e)
		{
			RawKeyboardInputReport rawKeyboardInputReport = this.ExtractRawKeyboardInputReport(e, InputManager.PreviewInputReportEvent);
			if (rawKeyboardInputReport != null)
			{
				e.StagingItem.Input.Device = this;
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00041A90 File Offset: 0x00040E90
		[SecurityCritical]
		private void PreNotifyInput(object sender, NotifyInputEventArgs e)
		{
			RawKeyboardInputReport rawKeyboardInputReport = this.ExtractRawKeyboardInputReport(e, InputManager.PreviewInputReportEvent);
			if (rawKeyboardInputReport != null)
			{
				this.CheckForDisconnectedFocus();
				if ((rawKeyboardInputReport.Actions & RawKeyboardActions.Activate) == RawKeyboardActions.Activate)
				{
					if (this._activeSource == null)
					{
						this._activeSource = new SecurityCriticalDataClass<PresentationSource>(rawKeyboardInputReport.InputSource);
					}
					else if (this._activeSource.Value != rawKeyboardInputReport.InputSource)
					{
						IKeyboardInputProvider keyboardInputProvider = this._activeSource.Value.GetInputProvider(typeof(KeyboardDevice)) as IKeyboardInputProvider;
						this._activeSource = new SecurityCriticalDataClass<PresentationSource>(rawKeyboardInputReport.InputSource);
						if (keyboardInputProvider != null)
						{
							keyboardInputProvider.NotifyDeactivate();
						}
					}
				}
				if ((rawKeyboardInputReport.Actions & RawKeyboardActions.KeyDown) == RawKeyboardActions.KeyDown)
				{
					RawKeyboardActions rawKeyboardActions = this.GetNonRedundantActions(e);
					rawKeyboardActions |= RawKeyboardActions.KeyDown;
					e.StagingItem.SetData(this._tagNonRedundantActions, rawKeyboardActions);
					Key key = KeyInterop.KeyFromVirtualKey(rawKeyboardInputReport.VirtualKey);
					e.StagingItem.SetData(this._tagKey, key);
					e.StagingItem.SetData(this._tagScanCode, new KeyboardDevice.ScanCode(rawKeyboardInputReport.ScanCode, rawKeyboardInputReport.IsExtendedKey));
					if (this._inputManager != null)
					{
						this._inputManager.Value.MostRecentInputDevice = this;
					}
				}
				if ((rawKeyboardInputReport.Actions & RawKeyboardActions.KeyUp) == RawKeyboardActions.KeyUp)
				{
					RawKeyboardActions rawKeyboardActions2 = this.GetNonRedundantActions(e);
					rawKeyboardActions2 |= RawKeyboardActions.KeyUp;
					e.StagingItem.SetData(this._tagNonRedundantActions, rawKeyboardActions2);
					Key key2 = KeyInterop.KeyFromVirtualKey(rawKeyboardInputReport.VirtualKey);
					e.StagingItem.SetData(this._tagKey, key2);
					e.StagingItem.SetData(this._tagScanCode, new KeyboardDevice.ScanCode(rawKeyboardInputReport.ScanCode, rawKeyboardInputReport.IsExtendedKey));
					if (this._inputManager != null)
					{
						this._inputManager.Value.MostRecentInputDevice = this;
					}
				}
			}
			if (e.StagingItem.Input.RoutedEvent != Keyboard.PreviewKeyDownEvent)
			{
				if (e.StagingItem.Input.RoutedEvent == Keyboard.PreviewKeyUpEvent)
				{
					this.CheckForDisconnectedFocus();
					KeyEventArgs keyEventArgs = (KeyEventArgs)e.StagingItem.Input;
					keyEventArgs.SetRepeat(false);
					this._previousKey = Key.None;
				}
				return;
			}
			this.CheckForDisconnectedFocus();
			KeyEventArgs keyEventArgs2 = (KeyEventArgs)e.StagingItem.Input;
			if (this._previousKey == keyEventArgs2.RealKey)
			{
				keyEventArgs2.SetRepeat(true);
				return;
			}
			this._previousKey = keyEventArgs2.RealKey;
			keyEventArgs2.SetRepeat(false);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00041CEC File Offset: 0x000410EC
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == Keyboard.PreviewKeyDownEvent)
			{
				this.CheckForDisconnectedFocus();
				if (!e.StagingItem.Input.Handled)
				{
					KeyEventArgs keyEventArgs = (KeyEventArgs)e.StagingItem.Input;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					Key key = keyEventArgs.Key;
					if (key == Key.System)
					{
						flag = true;
						key = keyEventArgs.RealKey;
					}
					else if (key == Key.ImeProcessed)
					{
						flag2 = true;
						key = keyEventArgs.RealKey;
					}
					else if (key == Key.DeadCharProcessed)
					{
						flag3 = true;
						key = keyEventArgs.RealKey;
					}
					KeyEventArgs keyEventArgs2 = new KeyEventArgs(this, keyEventArgs.UnsafeInputSource, keyEventArgs.Timestamp, key);
					keyEventArgs2.SetRepeat(keyEventArgs.IsRepeat);
					if (flag)
					{
						keyEventArgs2.MarkSystem();
					}
					else if (flag2)
					{
						keyEventArgs2.MarkImeProcessed();
					}
					else if (flag3)
					{
						keyEventArgs2.MarkDeadCharProcessed();
					}
					keyEventArgs2.RoutedEvent = Keyboard.KeyDownEvent;
					keyEventArgs2.ScanCode = keyEventArgs.ScanCode;
					keyEventArgs2.IsExtendedKey = keyEventArgs.IsExtendedKey;
					e.PushInput(keyEventArgs2, e.StagingItem);
				}
			}
			if (e.StagingItem.Input.RoutedEvent == Keyboard.PreviewKeyUpEvent)
			{
				this.CheckForDisconnectedFocus();
				if (!e.StagingItem.Input.Handled)
				{
					KeyEventArgs keyEventArgs3 = (KeyEventArgs)e.StagingItem.Input;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					Key key2 = keyEventArgs3.Key;
					if (key2 == Key.System)
					{
						flag4 = true;
						key2 = keyEventArgs3.RealKey;
					}
					else if (key2 == Key.ImeProcessed)
					{
						flag5 = true;
						key2 = keyEventArgs3.RealKey;
					}
					else if (key2 == Key.DeadCharProcessed)
					{
						flag6 = true;
						key2 = keyEventArgs3.RealKey;
					}
					KeyEventArgs keyEventArgs4 = new KeyEventArgs(this, keyEventArgs3.UnsafeInputSource, keyEventArgs3.Timestamp, key2);
					if (flag4)
					{
						keyEventArgs4.MarkSystem();
					}
					else if (flag5)
					{
						keyEventArgs4.MarkImeProcessed();
					}
					else if (flag6)
					{
						keyEventArgs4.MarkDeadCharProcessed();
					}
					keyEventArgs4.RoutedEvent = Keyboard.KeyUpEvent;
					keyEventArgs4.ScanCode = keyEventArgs3.ScanCode;
					keyEventArgs4.IsExtendedKey = keyEventArgs3.IsExtendedKey;
					e.PushInput(keyEventArgs4, e.StagingItem);
				}
			}
			RawKeyboardInputReport rawKeyboardInputReport = this.ExtractRawKeyboardInputReport(e, InputManager.InputReportEvent);
			if (rawKeyboardInputReport != null)
			{
				this.CheckForDisconnectedFocus();
				if (!e.StagingItem.Input.Handled)
				{
					RawKeyboardActions nonRedundantActions = this.GetNonRedundantActions(e);
					if ((nonRedundantActions & RawKeyboardActions.KeyDown) == RawKeyboardActions.KeyDown)
					{
						Key key3 = (Key)e.StagingItem.GetData(this._tagKey);
						if (key3 != Key.None)
						{
							KeyEventArgs keyEventArgs5 = new KeyEventArgs(this, rawKeyboardInputReport.InputSource, rawKeyboardInputReport.Timestamp, key3);
							KeyboardDevice.ScanCode scanCode = (KeyboardDevice.ScanCode)e.StagingItem.GetData(this._tagScanCode);
							keyEventArgs5.ScanCode = scanCode.Code;
							keyEventArgs5.IsExtendedKey = scanCode.IsExtended;
							if (rawKeyboardInputReport.IsSystemKey)
							{
								keyEventArgs5.MarkSystem();
							}
							keyEventArgs5.RoutedEvent = Keyboard.PreviewKeyDownEvent;
							e.PushInput(keyEventArgs5, e.StagingItem);
						}
					}
					if ((nonRedundantActions & RawKeyboardActions.KeyUp) == RawKeyboardActions.KeyUp)
					{
						Key key4 = (Key)e.StagingItem.GetData(this._tagKey);
						if (key4 != Key.None)
						{
							KeyEventArgs keyEventArgs6 = new KeyEventArgs(this, rawKeyboardInputReport.InputSource, rawKeyboardInputReport.Timestamp, key4);
							KeyboardDevice.ScanCode scanCode2 = (KeyboardDevice.ScanCode)e.StagingItem.GetData(this._tagScanCode);
							keyEventArgs6.ScanCode = scanCode2.Code;
							keyEventArgs6.IsExtendedKey = scanCode2.IsExtended;
							if (rawKeyboardInputReport.IsSystemKey)
							{
								keyEventArgs6.MarkSystem();
							}
							keyEventArgs6.RoutedEvent = Keyboard.PreviewKeyUpEvent;
							e.PushInput(keyEventArgs6, e.StagingItem);
						}
					}
				}
				if ((rawKeyboardInputReport.Actions & RawKeyboardActions.Deactivate) == RawKeyboardActions.Deactivate && this.IsActive)
				{
					this._activeSource = null;
					this.ChangeFocus(null, e.StagingItem.Input.Timestamp);
				}
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000420B4 File Offset: 0x000414B4
		[SecurityCritical]
		private RawKeyboardInputReport ExtractRawKeyboardInputReport(NotifyInputEventArgs e, RoutedEvent Event)
		{
			RawKeyboardInputReport result = null;
			InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
			if (inputReportEventArgs != null && inputReportEventArgs.Report.Type == InputType.Keyboard && inputReportEventArgs.RoutedEvent == Event)
			{
				result = (inputReportEventArgs.Report as RawKeyboardInputReport);
			}
			return result;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000420FC File Offset: 0x000414FC
		private RawKeyboardActions GetNonRedundantActions(NotifyInputEventArgs e)
		{
			object data = e.StagingItem.GetData(this._tagNonRedundantActions);
			RawKeyboardActions result;
			if (data != null)
			{
				result = (RawKeyboardActions)data;
			}
			else
			{
				result = RawKeyboardActions.None;
			}
			return result;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0004212C File Offset: 0x0004152C
		private bool CheckForDisconnectedFocus()
		{
			bool result = false;
			if (InputElement.GetRootVisual(this._focus) != this._focusRootVisual)
			{
				result = true;
				this.Focus(null);
			}
			return result;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004215C File Offset: 0x0004155C
		internal bool IsActive
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._activeSource != null && this._activeSource.Value != null;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00042184 File Offset: 0x00041584
		private DeferredElementTreeState FocusTreeState
		{
			get
			{
				if (this._focusTreeState == null)
				{
					this._focusTreeState = new DeferredElementTreeState();
				}
				return this._focusTreeState;
			}
		}

		// Token: 0x04000987 RID: 2439
		private SecurityCriticalDataClass<InputManager> _inputManager;

		// Token: 0x04000988 RID: 2440
		private SecurityCriticalDataClass<PresentationSource> _activeSource;

		// Token: 0x04000989 RID: 2441
		private DependencyObject _focus;

		// Token: 0x0400098A RID: 2442
		private DeferredElementTreeState _focusTreeState;

		// Token: 0x0400098B RID: 2443
		private DependencyObject _forceTarget;

		// Token: 0x0400098C RID: 2444
		private DependencyObject _focusRootVisual;

		// Token: 0x0400098D RID: 2445
		private Key _previousKey;

		// Token: 0x0400098E RID: 2446
		private DependencyPropertyChangedEventHandler _isEnabledChangedEventHandler;

		// Token: 0x0400098F RID: 2447
		private DependencyPropertyChangedEventHandler _isVisibleChangedEventHandler;

		// Token: 0x04000990 RID: 2448
		private DependencyPropertyChangedEventHandler _focusableChangedEventHandler;

		// Token: 0x04000991 RID: 2449
		private DispatcherOperationCallback _reevaluateFocusCallback;

		// Token: 0x04000992 RID: 2450
		private DispatcherOperation _reevaluateFocusOperation;

		// Token: 0x04000993 RID: 2451
		private object _tagNonRedundantActions = new object();

		// Token: 0x04000994 RID: 2452
		private object _tagKey = new object();

		// Token: 0x04000995 RID: 2453
		private object _tagScanCode = new object();

		// Token: 0x04000996 RID: 2454
		private SecurityCriticalData<TextCompositionManager> _textcompositionManager;

		// Token: 0x04000997 RID: 2455
		private SecurityCriticalDataClass<TextServicesManager> _TsfManager;

		// Token: 0x0200080C RID: 2060
		private class ScanCode
		{
			// Token: 0x0600560A RID: 22026 RVA: 0x00161E88 File Offset: 0x00161288
			internal ScanCode(int code, bool isExtended)
			{
				this._code = code;
				this._isExtended = isExtended;
			}

			// Token: 0x170011A4 RID: 4516
			// (get) Token: 0x0600560B RID: 22027 RVA: 0x00161EAC File Offset: 0x001612AC
			internal int Code
			{
				get
				{
					return this._code;
				}
			}

			// Token: 0x170011A5 RID: 4517
			// (get) Token: 0x0600560C RID: 22028 RVA: 0x00161EC0 File Offset: 0x001612C0
			internal bool IsExtended
			{
				get
				{
					return this._isExtended;
				}
			}

			// Token: 0x0400271F RID: 10015
			private readonly int _code;

			// Token: 0x04002720 RID: 10016
			private readonly bool _isExtended;
		}
	}
}
