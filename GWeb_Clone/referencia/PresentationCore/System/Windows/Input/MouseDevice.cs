using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input.StylusPointer;
using System.Windows.Interop;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Input
{
	/// <summary>Representa um dispositivo de mouse.</summary>
	// Token: 0x02000282 RID: 642
	public abstract class MouseDevice : InputDevice
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x000457D4 File Offset: 0x00044BD4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal MouseDevice(InputManager inputManager)
		{
			this._inputManager = new SecurityCriticalData<InputManager>(inputManager);
			this._inputManager.Value.PreProcessInput += this.PreProcessInput;
			this._inputManager.Value.PreNotifyInput += this.PreNotifyInput;
			this._inputManager.Value.PostProcessInput += this.PostProcessInput;
			this._doubleClickDeltaX = SafeSystemMetrics.DoubleClickDeltaX;
			this._doubleClickDeltaY = SafeSystemMetrics.DoubleClickDeltaY;
			this._doubleClickDeltaTime = SafeNativeMethods.GetDoubleClickTime();
			this._overIsEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsEnabledChanged);
			this._overIsVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsVisibleChanged);
			this._overIsHitTestVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnOverIsHitTestVisibleChanged);
			this._reevaluateMouseOverDelegate = new DispatcherOperationCallback(this.ReevaluateMouseOverAsync);
			this._reevaluateMouseOverOperation = null;
			this._captureIsEnabledChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsEnabledChanged);
			this._captureIsVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsVisibleChanged);
			this._captureIsHitTestVisibleChangedEventHandler = new DependencyPropertyChangedEventHandler(this.OnCaptureIsHitTestVisibleChanged);
			this._reevaluateCaptureDelegate = new DispatcherOperationCallback(this.ReevaluateCaptureAsync);
			this._reevaluateCaptureOperation = null;
			this._inputManager.Value.HitTestInvalidatedAsync += this.OnHitTestInvalidatedAsync;
		}

		/// <summary>Obtém o estado do botão do mouse especificado.</summary>
		/// <param name="mouseButton">O botão que está sendo consultado.</param>
		/// <returns>O estado do botão.</returns>
		// Token: 0x060012BE RID: 4798 RVA: 0x00045944 File Offset: 0x00044D44
		protected MouseButtonState GetButtonState(MouseButton mouseButton)
		{
			if (this._stylusDevice != null && this._stylusDevice.IsValid)
			{
				return this._stylusDevice.GetMouseButtonState(mouseButton, this);
			}
			return this.GetButtonStateFromSystem(mouseButton);
		}

		/// <summary>Calcula a posição da tela do ponteiro do mouse.</summary>
		/// <returns>A posição do ponteiro do mouse.</returns>
		// Token: 0x060012BF RID: 4799 RVA: 0x0004597C File Offset: 0x00044D7C
		protected Point GetScreenPosition()
		{
			if (this._stylusDevice != null)
			{
				return this._stylusDevice.GetMouseScreenPosition(this);
			}
			return this.GetScreenPositionFromSystem();
		}

		// Token: 0x060012C0 RID: 4800
		internal abstract MouseButtonState GetButtonStateFromSystem(MouseButton mouseButton);

		// Token: 0x060012C1 RID: 4801 RVA: 0x000459A4 File Offset: 0x00044DA4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal Point GetScreenPositionFromSystem()
		{
			Point result = new Point(0.0, 0.0);
			if (this.IsActive)
			{
				try
				{
					PresentationSource criticalActiveSource = this.CriticalActiveSource;
					if (criticalActiveSource != null)
					{
						result = PointUtil.ClientToScreen(this._lastPosition, criticalActiveSource);
					}
				}
				catch (Win32Exception)
				{
					result = new Point(0.0, 0.0);
				}
			}
			return result;
		}

		/// <summary>Calcula a posição do ponteiro do mouse nas coordenadas do cliente.</summary>
		/// <returns>A posição do ponteiro do mouse nas coordenadas do cliente.</returns>
		// Token: 0x060012C2 RID: 4802 RVA: 0x00045A24 File Offset: 0x00044E24
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected Point GetClientPosition()
		{
			Point clientPosition = new Point(0.0, 0.0);
			try
			{
				PresentationSource criticalActiveSource = this.CriticalActiveSource;
				if (criticalActiveSource != null)
				{
					clientPosition = this.GetClientPosition(criticalActiveSource);
				}
			}
			catch (Win32Exception)
			{
				clientPosition = new Point(0.0, 0.0);
			}
			return clientPosition;
		}

		/// <summary>Calcula a posição do ponteiro do mouse nas coordenadas do cliente o <see cref="T:System.Windows.PresentationSource" /> especificado.</summary>
		/// <param name="presentationSource">A origem na qual obter a posição do mouse.</param>
		/// <returns>A posição do ponteiro do mouse nas coordenadas do cliente no <see cref="T:System.Windows.PresentationSource" /> especificado.</returns>
		// Token: 0x060012C3 RID: 4803 RVA: 0x00045A98 File Offset: 0x00044E98
		protected Point GetClientPosition(PresentationSource presentationSource)
		{
			Point screenPosition = this.GetScreenPosition();
			return PointUtil.ScreenToClient(screenPosition, presentationSource);
		}

		/// <summary>Obtém o <see cref="T:System.Windows.IInputElement" /> ao qual a entrada desse dispositivo de mouse é enviada.</summary>
		/// <returns>O elemento que recebe a entrada.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00045AB8 File Offset: 0x00044EB8
		public override IInputElement Target
		{
			get
			{
				return this._mouseOver;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.PresentationSource" /> que está relatando a entrada para este dispositivo.</summary>
		/// <returns>A fonte de entrada para este dispositivo.</returns>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00045ACC File Offset: 0x00044ECC
		public override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				if (this._inputSource != null)
				{
					return this._inputSource.Value;
				}
				return null;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00045AF4 File Offset: 0x00044EF4
		internal PresentationSource CriticalActiveSource
		{
			[SecurityCritical]
			get
			{
				if (this._inputSource != null)
				{
					return this._inputSource.Value;
				}
				return null;
			}
		}

		/// <summary>Obtém o elemento sobre o qual o ponteiro do mouse está diretamente acima.</summary>
		/// <returns>O elemento sobre o qual o ponteiro do mouse está acima.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00045B18 File Offset: 0x00044F18
		public IInputElement DirectlyOver
		{
			get
			{
				return this._mouseOver;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00045B2C File Offset: 0x00044F2C
		[FriendAccessAllowed]
		internal IInputElement RawDirectlyOver
		{
			get
			{
				if (this._rawMouseOver != null)
				{
					IInputElement inputElement = (IInputElement)this._rawMouseOver.Target;
					if (inputElement != null)
					{
						return inputElement;
					}
				}
				return this.DirectlyOver;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.IInputElement" /> que é capturado pelo mouse.</summary>
		/// <returns>O elemento que é capturado pelo mouse.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00045B60 File Offset: 0x00044F60
		public IInputElement Captured
		{
			get
			{
				if (this._isCaptureMouseInProgress)
				{
					return null;
				}
				return this._mouseCapture;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00045B80 File Offset: 0x00044F80
		internal CaptureMode CapturedMode
		{
			get
			{
				return this._captureMode;
			}
		}

		/// <summary>Captura eventos do mouse para o elemento especificado.</summary>
		/// <param name="element">O elemento do qual o mouse é capturado.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento conseguiu capturar o mouse, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="element" /> não é um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />.</exception>
		// Token: 0x060012CB RID: 4811 RVA: 0x00045B94 File Offset: 0x00044F94
		public bool Capture(IInputElement element)
		{
			return this.Capture(element, CaptureMode.Element);
		}

		/// <summary>Captura a entrada do mouse para o elemento especificado usando o <see cref="T:System.Windows.Input.CaptureMode" /> especificado.</summary>
		/// <param name="element">O elemento do qual o mouse é capturado.</param>
		/// <param name="captureMode">A política de captura a ser usada.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento conseguiu capturar o mouse, caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="element" /> não é um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="captureMode" /> não é um <see cref="T:System.Windows.Input.CaptureMode" /> válido.</exception>
		// Token: 0x060012CC RID: 4812 RVA: 0x00045BAC File Offset: 0x00044FAC
		[SecurityCritical]
		public bool Capture(IInputElement element, CaptureMode captureMode)
		{
			int tickCount = Environment.TickCount;
			if (captureMode != CaptureMode.None && captureMode != CaptureMode.Element && captureMode != CaptureMode.SubTree)
			{
				throw new InvalidEnumArgumentException("captureMode", (int)captureMode, typeof(CaptureMode));
			}
			if (element == null)
			{
				captureMode = CaptureMode.None;
			}
			if (captureMode == CaptureMode.None)
			{
				element = null;
			}
			DependencyObject dependencyObject = element as DependencyObject;
			if (dependencyObject != null && !InputElement.IsValid(element))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					dependencyObject.GetType()
				}));
			}
			bool flag = false;
			if (element is UIElement)
			{
				UIElement uielement = element as UIElement;
				if (uielement.IsVisible && uielement.IsEnabled)
				{
					flag = true;
				}
			}
			else if (element is ContentElement)
			{
				ContentElement contentElement = element as ContentElement;
				if (contentElement.IsEnabled)
				{
					flag = true;
				}
			}
			else if (element is UIElement3D)
			{
				UIElement3D uielement3D = element as UIElement3D;
				if (uielement3D.IsVisible && uielement3D.IsEnabled)
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				flag = false;
				IMouseInputProvider mouseInputProvider = null;
				if (element != null)
				{
					DependencyObject containingVisual = InputElement.GetContainingVisual(dependencyObject);
					if (containingVisual != null)
					{
						PresentationSource presentationSource = PresentationSource.CriticalFromVisual(containingVisual);
						if (presentationSource != null)
						{
							mouseInputProvider = (presentationSource.GetInputProvider(typeof(MouseDevice)) as IMouseInputProvider);
						}
					}
				}
				else if (this._mouseCapture != null)
				{
					mouseInputProvider = this._providerCapture.Value;
				}
				if (mouseInputProvider != null)
				{
					if (element != null)
					{
						bool isCaptureMouseInProgress = this._isCaptureMouseInProgress;
						this._isCaptureMouseInProgress = true;
						flag = mouseInputProvider.CaptureMouse();
						this._isCaptureMouseInProgress = isCaptureMouseInProgress;
						if (flag)
						{
							this.ChangeMouseCapture(element, mouseInputProvider, captureMode, tickCount);
						}
					}
					else
					{
						mouseInputProvider.ReleaseMouseCapture();
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00045D18 File Offset: 0x00045118
		[SecurityCritical]
		private IMouseInputProvider FindMouseInputProviderForCursor()
		{
			IMouseInputProvider result = null;
			foreach (object obj in this._inputManager.Value.UnsecureInputProviders)
			{
				IMouseInputProvider mouseInputProvider = obj as IMouseInputProvider;
				if (mouseInputProvider != null)
				{
					result = mouseInputProvider;
					break;
				}
			}
			return result;
		}

		/// <summary>Obtém ou define o cursor para o aplicativo inteiro.</summary>
		/// <returns>O cursor de substituição ou <see langword="null" /> se <see cref="P:System.Windows.Input.MouseDevice.OverrideCursor" /> não está definido.</returns>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00045D5C File Offset: 0x0004515C
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00045D70 File Offset: 0x00045170
		public Cursor OverrideCursor
		{
			get
			{
				return this._overrideCursor;
			}
			set
			{
				this._overrideCursor = value;
				this.UpdateCursorPrivate();
			}
		}

		/// <summary>Define o ponteiro do mouse para o <see cref="T:System.Windows.Input.Cursor" /> especificado</summary>
		/// <param name="cursor">O cursor para o qual definir o ponteiro do mouse.</param>
		/// <returns>
		///   <see langword="true" /> se o cursor do mouse estiver definido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060012D0 RID: 4816 RVA: 0x00045D8C File Offset: 0x0004518C
		[SecurityCritical]
		public bool SetCursor(Cursor cursor)
		{
			if (this._overrideCursor != null)
			{
				cursor = this._overrideCursor;
			}
			if (cursor == null)
			{
				cursor = Cursors.None;
			}
			IMouseInputProvider mouseInputProvider = this.FindMouseInputProviderForCursor();
			return mouseInputProvider != null && mouseInputProvider.SetCursor(cursor);
		}

		/// <summary>Obtém o estado do botão esquerdo do mouse deste dispositivo de mouse.</summary>
		/// <returns>O estado do botão.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00045DC8 File Offset: 0x000451C8
		public MouseButtonState LeftButton
		{
			get
			{
				return this.GetButtonState(MouseButton.Left);
			}
		}

		/// <summary>Obtém o estado do botão direito deste dispositivo de mouse.</summary>
		/// <returns>O estado do botão.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00045DDC File Offset: 0x000451DC
		public MouseButtonState RightButton
		{
			get
			{
				return this.GetButtonState(MouseButton.Right);
			}
		}

		/// <summary>O estado do botão do meio desse dispositivo de mouse.</summary>
		/// <returns>O estado do botão.</returns>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00045DF0 File Offset: 0x000451F0
		public MouseButtonState MiddleButton
		{
			get
			{
				return this.GetButtonState(MouseButton.Middle);
			}
		}

		/// <summary>Obtém o estado do primeiro botão estendido neste dispositivo de mouse.</summary>
		/// <returns>O estado do botão.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00045E04 File Offset: 0x00045204
		public MouseButtonState XButton1
		{
			get
			{
				return this.GetButtonState(MouseButton.XButton1);
			}
		}

		/// <summary>Obtém o estado do segundo botão estendido do dispositivo de mouse.</summary>
		/// <returns>O estado do botão.</returns>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00045E18 File Offset: 0x00045218
		public MouseButtonState XButton2
		{
			get
			{
				return this.GetButtonState(MouseButton.XButton2);
			}
		}

		/// <summary>Obtém a posição do mouse em relação a um elemento especificado.</summary>
		/// <param name="relativeTo">O quadro de referência no qual calcular a posição do mouse.</param>
		/// <returns>A posição do mouse em relação ao parâmetro <paramref name="relativeTo" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="relativeTo" /> é <see langword="null" /> ou não é um <see cref="T:System.Windows.UIElement" /> ou <see cref="T:System.Windows.ContentElement" />.</exception>
		// Token: 0x060012D6 RID: 4822 RVA: 0x00045E2C File Offset: 0x0004522C
		[SecurityCritical]
		public Point GetPosition(IInputElement relativeTo)
		{
			if (relativeTo != null && !InputElement.IsValid(relativeTo))
			{
				throw new InvalidOperationException(SR.Get("Invalid_IInputElement", new object[]
				{
					relativeTo.GetType()
				}));
			}
			PresentationSource presentationSource = null;
			if (relativeTo != null)
			{
				DependencyObject o = relativeTo as DependencyObject;
				DependencyObject containingVisual = InputElement.GetContainingVisual(o);
				if (containingVisual != null)
				{
					presentationSource = PresentationSource.CriticalFromVisual(containingVisual);
				}
			}
			else if (this._inputSource != null)
			{
				presentationSource = this._inputSource.Value;
			}
			if (presentationSource == null || presentationSource.RootVisual == null)
			{
				return new Point(0.0, 0.0);
			}
			Point clientPosition = this.GetClientPosition(presentationSource);
			bool flag;
			Point pt = PointUtil.TryClientToRoot(clientPosition, presentationSource, false, out flag);
			if (!flag)
			{
				return new Point(0.0, 0.0);
			}
			return InputElement.TranslatePoint(pt, presentationSource.RootVisual, (DependencyObject)relativeTo);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00045F00 File Offset: 0x00045300
		internal void ReevaluateMouseOver(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (isCoreParent)
				{
					this.MouseOverTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this.MouseOverTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateMouseOverOperation == null)
			{
				this._reevaluateMouseOverOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._reevaluateMouseOverDelegate, null);
			}
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00045F50 File Offset: 0x00045350
		private object ReevaluateMouseOverAsync(object arg)
		{
			this._reevaluateMouseOverOperation = null;
			this.Synchronize();
			if (this._mouseOverTreeState != null && !this._mouseOverTreeState.IsEmpty)
			{
				UIElement.MouseOverProperty.OnOriginValueChanged(this._mouseOver as DependencyObject, this._mouseOver as DependencyObject, ref this._mouseOverTreeState);
			}
			return null;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00045FA8 File Offset: 0x000453A8
		internal void ReevaluateCapture(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (isCoreParent)
				{
					this.MouseCaptureWithinTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this.MouseCaptureWithinTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateCaptureOperation == null)
			{
				this._reevaluateCaptureOperation = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, this._reevaluateCaptureDelegate, null);
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00045FF8 File Offset: 0x000453F8
		private object ReevaluateCaptureAsync(object arg)
		{
			this._reevaluateCaptureOperation = null;
			if (this._mouseCapture == null)
			{
				return null;
			}
			bool flag = false;
			DependencyObject o = this._mouseCapture as DependencyObject;
			if (InputElement.IsUIElement(o))
			{
				flag = !this.ValidateUIElementForCapture((UIElement)this._mouseCapture);
			}
			else if (InputElement.IsContentElement(o))
			{
				flag = !this.ValidateContentElementForCapture((ContentElement)this._mouseCapture);
			}
			else if (InputElement.IsUIElement3D(o))
			{
				flag = !this.ValidateUIElement3DForCapture((UIElement3D)this._mouseCapture);
			}
			if (!flag)
			{
				DependencyObject containingVisual = InputElement.GetContainingVisual(o);
				flag = !this.ValidateVisualForCapture(containingVisual);
			}
			if (flag)
			{
				this.Capture(null);
			}
			if (this._mouseCaptureWithinTreeState != null && !this._mouseCaptureWithinTreeState.IsEmpty)
			{
				UIElement.MouseCaptureWithinProperty.OnOriginValueChanged(this._mouseCapture as DependencyObject, this._mouseCapture as DependencyObject, ref this._mouseCaptureWithinTreeState);
			}
			return null;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000460DC File Offset: 0x000454DC
		private bool ValidateUIElementForCapture(UIElement element)
		{
			return element.IsEnabled && element.IsVisible && element.IsHitTestVisible;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00046108 File Offset: 0x00045508
		private bool ValidateUIElement3DForCapture(UIElement3D element)
		{
			return element.IsEnabled && element.IsVisible && element.IsHitTestVisible;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00046134 File Offset: 0x00045534
		private bool ValidateContentElementForCapture(ContentElement element)
		{
			return element.IsEnabled;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004614C File Offset: 0x0004554C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private bool ValidateVisualForCapture(DependencyObject visual)
		{
			if (visual == null)
			{
				return false;
			}
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(visual);
			return presentationSource != null && presentationSource == this.CriticalActiveSource;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00046178 File Offset: 0x00045578
		private void OnOverIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateMouseOver(null, null, true);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00046190 File Offset: 0x00045590
		private void OnOverIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateMouseOver(null, null, true);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000461A8 File Offset: 0x000455A8
		private void OnOverIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateMouseOver(null, null, true);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000461C0 File Offset: 0x000455C0
		private void OnCaptureIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000461D8 File Offset: 0x000455D8
		private void OnCaptureIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000461F0 File Offset: 0x000455F0
		private void OnCaptureIsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateCapture(null, null, true);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00046208 File Offset: 0x00045608
		private void OnHitTestInvalidatedAsync(object sender, EventArgs e)
		{
			this.Synchronize();
		}

		/// <summary>Força o mouse a sincronizar novamente.</summary>
		// Token: 0x060012E6 RID: 4838 RVA: 0x0004621C File Offset: 0x0004561C
		[SecurityCritical]
		public void Synchronize()
		{
			PresentationSource criticalActiveSource = this.CriticalActiveSource;
			if (criticalActiveSource != null && criticalActiveSource.CompositionTarget != null && !criticalActiveSource.CompositionTarget.IsDisposed)
			{
				int tickCount = Environment.TickCount;
				Point clientPosition = this.GetClientPosition();
				RawMouseInputReport rawMouseInputReport = new RawMouseInputReport(InputMode.Foreground, tickCount, criticalActiveSource, RawMouseActions.AbsoluteMove, (int)clientPosition.X, (int)clientPosition.Y, 0, IntPtr.Zero);
				rawMouseInputReport._isSynchronize = true;
				InputReportEventArgs inputReportEventArgs;
				if (this._stylusDevice != null)
				{
					inputReportEventArgs = new InputReportEventArgs(this._stylusDevice, rawMouseInputReport);
				}
				else
				{
					inputReportEventArgs = new InputReportEventArgs(this, rawMouseInputReport);
				}
				inputReportEventArgs.RoutedEvent = InputManager.PreviewInputReportEvent;
				this._inputManager.Value.ProcessInput(inputReportEventArgs);
			}
		}

		/// <summary>Força a atualização do cursor do mouse.</summary>
		// Token: 0x060012E7 RID: 4839 RVA: 0x000462C0 File Offset: 0x000456C0
		public void UpdateCursor()
		{
			this.UpdateCursorPrivate();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000462D4 File Offset: 0x000456D4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool UpdateCursorPrivate()
		{
			int tickCount = Environment.TickCount;
			QueryCursorEventArgs queryCursorEventArgs = new QueryCursorEventArgs(this, tickCount);
			queryCursorEventArgs.Cursor = Cursors.Arrow;
			queryCursorEventArgs.RoutedEvent = Mouse.QueryCursorEvent;
			this._inputManager.Value.ProcessInput(queryCursorEventArgs);
			return queryCursorEventArgs.Handled;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00046320 File Offset: 0x00045720
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void ChangeMouseOver(IInputElement mouseOver, int timestamp)
		{
			if (this._mouseOver != mouseOver)
			{
				IInputElement mouseOver2 = this._mouseOver;
				this._mouseOver = mouseOver;
				using (base.Dispatcher.DisableProcessing())
				{
					if (mouseOver2 != null)
					{
						DependencyObject dependencyObject = mouseOver2 as DependencyObject;
						if (InputElement.IsUIElement(dependencyObject))
						{
							((UIElement)dependencyObject).IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
							((UIElement)dependencyObject).IsVisibleChanged -= this._overIsVisibleChangedEventHandler;
							((UIElement)dependencyObject).IsHitTestVisibleChanged -= this._overIsHitTestVisibleChangedEventHandler;
						}
						else if (InputElement.IsContentElement(dependencyObject))
						{
							((ContentElement)dependencyObject).IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
						}
						else if (InputElement.IsUIElement3D(dependencyObject))
						{
							((UIElement3D)dependencyObject).IsEnabledChanged -= this._overIsEnabledChangedEventHandler;
							((UIElement3D)dependencyObject).IsVisibleChanged -= this._overIsVisibleChangedEventHandler;
							((UIElement3D)dependencyObject).IsHitTestVisibleChanged -= this._overIsHitTestVisibleChangedEventHandler;
						}
					}
					if (this._mouseOver != null)
					{
						DependencyObject dependencyObject = this._mouseOver as DependencyObject;
						if (InputElement.IsUIElement(dependencyObject))
						{
							((UIElement)dependencyObject).IsEnabledChanged += this._overIsEnabledChangedEventHandler;
							((UIElement)dependencyObject).IsVisibleChanged += this._overIsVisibleChangedEventHandler;
							((UIElement)dependencyObject).IsHitTestVisibleChanged += this._overIsHitTestVisibleChangedEventHandler;
						}
						else if (InputElement.IsContentElement(dependencyObject))
						{
							((ContentElement)dependencyObject).IsEnabledChanged += this._overIsEnabledChangedEventHandler;
						}
						else if (InputElement.IsUIElement3D(dependencyObject))
						{
							((UIElement3D)dependencyObject).IsEnabledChanged += this._overIsEnabledChangedEventHandler;
							((UIElement3D)dependencyObject).IsVisibleChanged += this._overIsVisibleChangedEventHandler;
							((UIElement3D)dependencyObject).IsHitTestVisibleChanged += this._overIsHitTestVisibleChangedEventHandler;
						}
					}
				}
				UIElement.MouseOverProperty.OnOriginValueChanged(mouseOver2 as DependencyObject, this._mouseOver as DependencyObject, ref this._mouseOverTreeState);
				if (mouseOver2 != null)
				{
					DependencyObject dependencyObject = mouseOver2 as DependencyObject;
					dependencyObject.SetValue(UIElement.IsMouseDirectlyOverPropertyKey, false);
				}
				if (this._mouseOver != null)
				{
					DependencyObject dependencyObject = this._mouseOver as DependencyObject;
					dependencyObject.SetValue(UIElement.IsMouseDirectlyOverPropertyKey, true);
				}
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00046524 File Offset: 0x00045924
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void ChangeMouseCapture(IInputElement mouseCapture, IMouseInputProvider providerCapture, CaptureMode captureMode, int timestamp)
		{
			if (mouseCapture != this._mouseCapture)
			{
				IInputElement mouseCapture2 = this._mouseCapture;
				this._mouseCapture = mouseCapture;
				if (this._mouseCapture != null)
				{
					this._providerCapture = new SecurityCriticalDataClass<IMouseInputProvider>(providerCapture);
				}
				else
				{
					this._providerCapture = null;
				}
				this._captureMode = captureMode;
				using (base.Dispatcher.DisableProcessing())
				{
					if (mouseCapture2 != null)
					{
						DependencyObject dependencyObject = mouseCapture2 as DependencyObject;
						if (InputElement.IsUIElement(dependencyObject))
						{
							((UIElement)dependencyObject).IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
							((UIElement)dependencyObject).IsVisibleChanged -= this._captureIsVisibleChangedEventHandler;
							((UIElement)dependencyObject).IsHitTestVisibleChanged -= this._captureIsHitTestVisibleChangedEventHandler;
						}
						else if (InputElement.IsContentElement(dependencyObject))
						{
							((ContentElement)dependencyObject).IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
						}
						else if (InputElement.IsUIElement3D(dependencyObject))
						{
							((UIElement3D)dependencyObject).IsEnabledChanged -= this._captureIsEnabledChangedEventHandler;
							((UIElement3D)dependencyObject).IsVisibleChanged -= this._captureIsVisibleChangedEventHandler;
							((UIElement3D)dependencyObject).IsHitTestVisibleChanged -= this._captureIsHitTestVisibleChangedEventHandler;
						}
					}
					if (this._mouseCapture != null)
					{
						DependencyObject dependencyObject = this._mouseCapture as DependencyObject;
						if (InputElement.IsUIElement(dependencyObject))
						{
							((UIElement)dependencyObject).IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
							((UIElement)dependencyObject).IsVisibleChanged += this._captureIsVisibleChangedEventHandler;
							((UIElement)dependencyObject).IsHitTestVisibleChanged += this._captureIsHitTestVisibleChangedEventHandler;
						}
						else if (InputElement.IsContentElement(dependencyObject))
						{
							((ContentElement)dependencyObject).IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
						}
						else if (InputElement.IsUIElement3D(dependencyObject))
						{
							((UIElement3D)dependencyObject).IsEnabledChanged += this._captureIsEnabledChangedEventHandler;
							((UIElement3D)dependencyObject).IsVisibleChanged += this._captureIsVisibleChangedEventHandler;
							((UIElement3D)dependencyObject).IsHitTestVisibleChanged += this._captureIsHitTestVisibleChangedEventHandler;
						}
					}
				}
				UIElement.MouseCaptureWithinProperty.OnOriginValueChanged(mouseCapture2 as DependencyObject, this._mouseCapture as DependencyObject, ref this._mouseCaptureWithinTreeState);
				if (mouseCapture2 != null)
				{
					DependencyObject dependencyObject = mouseCapture2 as DependencyObject;
					dependencyObject.SetValue(UIElement.IsMouseCapturedPropertyKey, false);
				}
				if (this._mouseCapture != null)
				{
					DependencyObject dependencyObject = this._mouseCapture as DependencyObject;
					dependencyObject.SetValue(UIElement.IsMouseCapturedPropertyKey, true);
				}
				if (mouseCapture2 != null)
				{
					MouseEventArgs mouseEventArgs = new MouseEventArgs(this, timestamp, this._stylusDevice);
					mouseEventArgs.RoutedEvent = Mouse.LostMouseCaptureEvent;
					mouseEventArgs.Source = mouseCapture2;
					this._inputManager.Value.ProcessInput(mouseEventArgs);
				}
				if (this._mouseCapture != null)
				{
					MouseEventArgs mouseEventArgs2 = new MouseEventArgs(this, timestamp, this._stylusDevice);
					mouseEventArgs2.RoutedEvent = Mouse.GotMouseCaptureEvent;
					mouseEventArgs2.Source = this._mouseCapture;
					this._inputManager.Value.ProcessInput(mouseEventArgs2);
				}
				this.Synchronize();
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000467C8 File Offset: 0x00045BC8
		[SecurityCritical]
		private void PreProcessInput(object sender, PreProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == InputManager.PreviewInputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (!inputReportEventArgs.Handled && inputReportEventArgs.Report.Type == InputType.Mouse)
				{
					RawMouseInputReport rawMouseInputReport = (RawMouseInputReport)inputReportEventArgs.Report;
					if ((rawMouseInputReport.Actions & RawMouseActions.Activate) == RawMouseActions.Activate)
					{
						if ((rawMouseInputReport.Actions & ~RawMouseActions.Activate) != RawMouseActions.None)
						{
							e.Cancel();
							RawMouseInputReport report = new RawMouseInputReport(rawMouseInputReport.Mode, rawMouseInputReport.Timestamp, rawMouseInputReport.InputSource, rawMouseInputReport.Actions & ~RawMouseActions.Activate, rawMouseInputReport.X, rawMouseInputReport.Y, rawMouseInputReport.Wheel, rawMouseInputReport.ExtraInformation);
							e.PushInput(new InputReportEventArgs(inputReportEventArgs.Device, report)
							{
								RoutedEvent = InputManager.PreviewInputReportEvent
							}, null);
							MouseDevice.PushActivateInputReport(e, inputReportEventArgs, rawMouseInputReport, false);
							return;
						}
					}
					else if (this._inputSource != null && rawMouseInputReport.InputSource == this._inputSource.Value)
					{
						InputDevice inputDevice = e.StagingItem.GetData(this._tagStylusDevice) as StylusDevice;
						if (inputDevice == null)
						{
							if (StylusLogic.IsPointerStackEnabled && StylusLogic.IsPromotedMouseEvent(rawMouseInputReport))
							{
								uint cursorIdFromMouseEvent = StylusLogic.GetCursorIdFromMouseEvent(rawMouseInputReport);
								PointerTabletDeviceCollection pointerTabletDeviceCollection = Tablet.TabletDevices.As<PointerTabletDeviceCollection>();
								PointerStylusDevice stylusDeviceByCursorId = pointerTabletDeviceCollection.GetStylusDeviceByCursorId(cursorIdFromMouseEvent);
								inputDevice = ((stylusDeviceByCursorId != null) ? stylusDeviceByCursorId.StylusDevice : null);
							}
							else
							{
								inputDevice = (inputReportEventArgs.Device as StylusDevice);
							}
							if (inputDevice != null)
							{
								e.StagingItem.SetData(this._tagStylusDevice, inputDevice);
							}
						}
						inputReportEventArgs.Device = this;
						if ((rawMouseInputReport.Actions & RawMouseActions.Deactivate) == RawMouseActions.Deactivate && this._mouseOver != null)
						{
							e.PushInput(e.StagingItem);
							e.Cancel();
							this._isPhysicallyOver = false;
							this.ChangeMouseOver(null, e.StagingItem.Input.Timestamp);
						}
						if ((rawMouseInputReport.Actions & RawMouseActions.AbsoluteMove) == RawMouseActions.AbsoluteMove)
						{
							if ((rawMouseInputReport.Actions & ~(RawMouseActions.AbsoluteMove | RawMouseActions.QueryCursor)) != RawMouseActions.None)
							{
								e.Cancel();
								RawMouseInputReport report2 = new RawMouseInputReport(rawMouseInputReport.Mode, rawMouseInputReport.Timestamp, rawMouseInputReport.InputSource, rawMouseInputReport.Actions & ~(RawMouseActions.AbsoluteMove | RawMouseActions.QueryCursor), 0, 0, rawMouseInputReport.Wheel, rawMouseInputReport.ExtraInformation);
								e.PushInput(new InputReportEventArgs(inputDevice, report2)
								{
									RoutedEvent = InputManager.PreviewInputReportEvent
								}, null);
								RawMouseInputReport report3 = new RawMouseInputReport(rawMouseInputReport.Mode, rawMouseInputReport.Timestamp, rawMouseInputReport.InputSource, rawMouseInputReport.Actions & (RawMouseActions.AbsoluteMove | RawMouseActions.QueryCursor), rawMouseInputReport.X, rawMouseInputReport.Y, 0, IntPtr.Zero);
								e.PushInput(new InputReportEventArgs(inputDevice, report3)
								{
									RoutedEvent = InputManager.PreviewInputReportEvent
								}, null);
								return;
							}
							bool flag = true;
							Point point = new Point((double)rawMouseInputReport.X, (double)rawMouseInputReport.Y);
							Point point2 = PointUtil.TryClientToRoot(point, rawMouseInputReport.InputSource, false, out flag);
							if (flag)
							{
								e.StagingItem.SetData(this._tagRootPoint, point2);
								return;
							}
							e.Cancel();
							return;
						}
					}
				}
			}
			else if (this._inputSource != null)
			{
				if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseDownEvent)
				{
					MouseButtonEventArgs mouseButtonEventArgs = e.StagingItem.Input as MouseButtonEventArgs;
					if (this._mouseCapture != null && !this._isPhysicallyOver)
					{
						MouseButtonEventArgs mouseButtonEventArgs2 = new MouseButtonEventArgs(this, mouseButtonEventArgs.Timestamp, mouseButtonEventArgs.ChangedButton, this.GetStylusDevice(e.StagingItem));
						mouseButtonEventArgs2.RoutedEvent = Mouse.PreviewMouseDownOutsideCapturedElementEvent;
						this._inputManager.Value.ProcessInput(mouseButtonEventArgs2);
						return;
					}
				}
				else if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseUpEvent)
				{
					MouseButtonEventArgs mouseButtonEventArgs3 = e.StagingItem.Input as MouseButtonEventArgs;
					if (this._mouseCapture != null && !this._isPhysicallyOver)
					{
						MouseButtonEventArgs mouseButtonEventArgs4 = new MouseButtonEventArgs(this, mouseButtonEventArgs3.Timestamp, mouseButtonEventArgs3.ChangedButton, this.GetStylusDevice(e.StagingItem));
						mouseButtonEventArgs4.RoutedEvent = Mouse.PreviewMouseUpOutsideCapturedElementEvent;
						this._inputManager.Value.ProcessInput(mouseButtonEventArgs4);
					}
				}
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00046BB8 File Offset: 0x00045FB8
		[SecurityCritical]
		internal static void PushActivateInputReport(PreProcessInputEventArgs e, InputReportEventArgs inputReportEventArgs, RawMouseInputReport rawMouseInputReport, bool clearExtraInformation)
		{
			IntPtr extraInformation = clearExtraInformation ? IntPtr.Zero : rawMouseInputReport.ExtraInformation;
			RawMouseInputReport report = new RawMouseInputReport(rawMouseInputReport.Mode, rawMouseInputReport.Timestamp, rawMouseInputReport.InputSource, RawMouseActions.Activate, rawMouseInputReport.X, rawMouseInputReport.Y, rawMouseInputReport.Wheel, extraInformation);
			e.PushInput(new InputReportEventArgs(inputReportEventArgs.Device, report)
			{
				RoutedEvent = InputManager.PreviewInputReportEvent
			}, null);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00046C24 File Offset: 0x00046024
		[SecurityCritical]
		private void PreNotifyInput(object sender, NotifyInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == InputManager.PreviewInputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (!inputReportEventArgs.Handled && inputReportEventArgs.Report.Type == InputType.Mouse)
				{
					RawMouseInputReport rawMouseInputReport = (RawMouseInputReport)inputReportEventArgs.Report;
					RawMouseActions rawMouseActions = this.GetNonRedundantActions(e);
					RawMouseActions rawMouseActions2 = rawMouseActions;
					this._stylusDevice = this.GetStylusDevice(e.StagingItem);
					if ((rawMouseInputReport.Actions & RawMouseActions.Activate) == RawMouseActions.Activate)
					{
						rawMouseActions |= RawMouseActions.Activate;
						this._positionRelativeToOver.X = 0.0;
						this._positionRelativeToOver.Y = 0.0;
						this._lastPosition.X = (double)rawMouseInputReport.X;
						this._lastPosition.Y = (double)rawMouseInputReport.Y;
						this._forceUpdateLastPosition = true;
						this._stylusDevice = (inputReportEventArgs.Device as StylusDevice);
						if (this._inputSource == null)
						{
							this._inputSource = new SecurityCriticalDataClass<PresentationSource>(rawMouseInputReport.InputSource);
						}
						else if (this._inputSource.Value != rawMouseInputReport.InputSource)
						{
							IMouseInputProvider mouseInputProvider = this._inputSource.Value.GetInputProvider(typeof(MouseDevice)) as IMouseInputProvider;
							this._inputSource = new SecurityCriticalDataClass<PresentationSource>(rawMouseInputReport.InputSource);
							if (mouseInputProvider != null)
							{
								mouseInputProvider.NotifyDeactivate();
							}
						}
					}
					if (this._inputSource != null && rawMouseInputReport.InputSource == this._inputSource.Value)
					{
						if ((rawMouseInputReport.Actions & RawMouseActions.Deactivate) == RawMouseActions.Deactivate)
						{
							this._inputSource = null;
							this.ChangeMouseCapture(null, null, CaptureMode.None, e.StagingItem.Input.Timestamp);
						}
						if ((rawMouseInputReport.Actions & RawMouseActions.CancelCapture) == RawMouseActions.CancelCapture)
						{
							this.ChangeMouseCapture(null, null, CaptureMode.None, e.StagingItem.Input.Timestamp);
						}
						if ((rawMouseInputReport.Actions & RawMouseActions.AbsoluteMove) == RawMouseActions.AbsoluteMove)
						{
							bool flag = false;
							Point point = new Point((double)rawMouseInputReport.X, (double)rawMouseInputReport.Y);
							Point pt = (Point)e.StagingItem.GetData(this._tagRootPoint);
							Point point2 = InputElement.TranslatePoint(pt, rawMouseInputReport.InputSource.RootVisual, (DependencyObject)this._mouseOver, out flag);
							IInputElement inputElement = this._mouseOver;
							IInputElement inputElement2 = (this._rawMouseOver != null) ? ((IInputElement)this._rawMouseOver.Target) : null;
							bool flag2 = this._isPhysicallyOver;
							bool flag3 = !this.ArePointsClose(point, this._lastPosition);
							if (flag3 || rawMouseInputReport._isSynchronize || !flag)
							{
								flag2 = true;
								switch (this._captureMode)
								{
								case CaptureMode.None:
									if (rawMouseInputReport._isSynchronize)
									{
										MouseDevice.GlobalHitTest(true, point, this._inputSource.Value, out inputElement, out inputElement2);
									}
									else
									{
										MouseDevice.LocalHitTest(true, point, this._inputSource.Value, out inputElement, out inputElement2);
									}
									if (inputElement == inputElement2)
									{
										inputElement2 = null;
									}
									if (!InputElement.IsValid(inputElement))
									{
										inputElement = InputElement.GetContainingInputElement(inputElement as DependencyObject);
									}
									if (inputElement2 != null && !InputElement.IsValid(inputElement2))
									{
										inputElement2 = InputElement.GetContainingInputElement(inputElement2 as DependencyObject);
									}
									break;
								case CaptureMode.Element:
									if (rawMouseInputReport._isSynchronize)
									{
										inputElement = MouseDevice.GlobalHitTest(true, point, this._inputSource.Value);
									}
									else
									{
										inputElement = MouseDevice.LocalHitTest(true, point, this._inputSource.Value);
									}
									inputElement2 = null;
									if (inputElement != this._mouseCapture)
									{
										inputElement = this._mouseCapture;
										flag2 = false;
									}
									break;
								case CaptureMode.SubTree:
								{
									IInputElement containingInputElement = InputElement.GetContainingInputElement(this._mouseCapture as DependencyObject);
									if (containingInputElement != null)
									{
										MouseDevice.GlobalHitTest(true, point, this._inputSource.Value, out inputElement, out inputElement2);
									}
									if (inputElement != null && !InputElement.IsValid(inputElement))
									{
										inputElement = InputElement.GetContainingInputElement(inputElement as DependencyObject);
									}
									if (inputElement != null)
									{
										IInputElement inputElement3 = inputElement;
										while (inputElement3 != null && inputElement3 != containingInputElement)
										{
											UIElement uielement = inputElement3 as UIElement;
											if (uielement != null)
											{
												inputElement3 = InputElement.GetContainingInputElement(uielement.GetUIParent(true));
											}
											else
											{
												ContentElement contentElement = inputElement3 as ContentElement;
												if (contentElement != null)
												{
													inputElement3 = InputElement.GetContainingInputElement(contentElement.GetUIParent(true));
												}
												else
												{
													UIElement3D uielement3D = inputElement3 as UIElement3D;
													inputElement3 = InputElement.GetContainingInputElement(uielement3D.GetUIParent(true));
												}
											}
										}
										if (inputElement3 != containingInputElement)
										{
											inputElement = this._mouseCapture;
											flag2 = false;
											inputElement2 = null;
										}
									}
									else
									{
										inputElement = this._mouseCapture;
										flag2 = false;
										inputElement2 = null;
									}
									if (inputElement2 != null)
									{
										if (inputElement == inputElement2)
										{
											inputElement2 = null;
										}
										else if (!InputElement.IsValid(inputElement2))
										{
											inputElement2 = InputElement.GetContainingInputElement(inputElement2 as DependencyObject);
										}
									}
									break;
								}
								}
							}
							this._isPhysicallyOver = (inputElement != null && flag2);
							bool flag4 = inputElement != this._mouseOver;
							if (flag4)
							{
								point2 = InputElement.TranslatePoint(pt, rawMouseInputReport.InputSource.RootVisual, (DependencyObject)inputElement);
							}
							bool flag5 = flag4 || !this.ArePointsClose(point2, this._positionRelativeToOver);
							if (flag3 || flag5 || this._forceUpdateLastPosition)
							{
								this._forceUpdateLastPosition = false;
								this._lastPosition = point;
								this._positionRelativeToOver = point2;
								if (flag4)
								{
									this.ChangeMouseOver(inputElement, e.StagingItem.Input.Timestamp);
								}
								if (this._rawMouseOver == null && inputElement2 != null)
								{
									this._rawMouseOver = new WeakReference(inputElement2);
								}
								else if (this._rawMouseOver != null)
								{
									this._rawMouseOver.Target = inputElement2;
								}
								rawMouseActions |= RawMouseActions.AbsoluteMove;
								rawMouseActions |= RawMouseActions.QueryCursor;
							}
						}
						if ((rawMouseInputReport.Actions & RawMouseActions.VerticalWheelRotate) == RawMouseActions.VerticalWheelRotate)
						{
							rawMouseActions |= RawMouseActions.VerticalWheelRotate;
							this._inputManager.Value.MostRecentInputDevice = this;
						}
						if ((rawMouseInputReport.Actions & RawMouseActions.QueryCursor) == RawMouseActions.QueryCursor)
						{
							rawMouseActions |= RawMouseActions.QueryCursor;
						}
						RawMouseActions[] array = new RawMouseActions[]
						{
							RawMouseActions.Button1Press,
							RawMouseActions.Button2Press,
							RawMouseActions.Button3Press,
							RawMouseActions.Button4Press,
							RawMouseActions.Button5Press
						};
						RawMouseActions[] array2 = new RawMouseActions[]
						{
							RawMouseActions.Button1Release,
							RawMouseActions.Button2Release,
							RawMouseActions.Button3Release,
							RawMouseActions.Button4Release,
							RawMouseActions.Button5Release
						};
						for (int i = 0; i < 5; i++)
						{
							if ((rawMouseInputReport.Actions & array[i]) == array[i])
							{
								rawMouseActions |= array[i];
								this._inputManager.Value.MostRecentInputDevice = this;
							}
							if ((rawMouseInputReport.Actions & array2[i]) == array2[i])
							{
								rawMouseActions |= array2[i];
								this._inputManager.Value.MostRecentInputDevice = this;
							}
						}
					}
					if (rawMouseActions != rawMouseActions2)
					{
						e.StagingItem.SetData(this._tagNonRedundantActions, rawMouseActions);
						return;
					}
				}
			}
			else if (this._inputSource != null && e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseDownEvent)
			{
				MouseButtonEventArgs mouseButtonEventArgs = e.StagingItem.Input as MouseButtonEventArgs;
				StylusDevice stylusDevice = this.GetStylusDevice(e.StagingItem);
				Point clientPosition = this.GetClientPosition();
				this._clickCount = this.CalculateClickCount(mouseButtonEventArgs.ChangedButton, mouseButtonEventArgs.Timestamp, stylusDevice, clientPosition);
				if (this._clickCount == 1)
				{
					this._lastClick = clientPosition;
					this._lastButton = mouseButtonEventArgs.ChangedButton;
					this._lastClickTime = mouseButtonEventArgs.Timestamp;
				}
				mouseButtonEventArgs.ClickCount = this._clickCount;
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000472F4 File Offset: 0x000466F4
		private bool ArePointsClose(Point A, Point B)
		{
			return DoubleUtil.AreClose(A.X, B.X) && DoubleUtil.AreClose(A.Y, B.Y);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004732C File Offset: 0x0004672C
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseWheelEvent && !e.StagingItem.Input.Handled)
			{
				MouseWheelEventArgs mouseWheelEventArgs = (MouseWheelEventArgs)e.StagingItem.Input;
				e.PushInput(new MouseWheelEventArgs(this, mouseWheelEventArgs.Timestamp, mouseWheelEventArgs.Delta)
				{
					RoutedEvent = Mouse.MouseWheelEvent
				}, e.StagingItem);
			}
			if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseDownEvent && !e.StagingItem.Input.Handled)
			{
				MouseButtonEventArgs mouseButtonEventArgs = (MouseButtonEventArgs)e.StagingItem.Input;
				e.PushInput(new MouseButtonEventArgs(this, mouseButtonEventArgs.Timestamp, mouseButtonEventArgs.ChangedButton, this.GetStylusDevice(e.StagingItem))
				{
					ClickCount = mouseButtonEventArgs.ClickCount,
					RoutedEvent = Mouse.MouseDownEvent
				}, e.StagingItem);
			}
			if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseUpEvent && !e.StagingItem.Input.Handled)
			{
				MouseButtonEventArgs mouseButtonEventArgs2 = (MouseButtonEventArgs)e.StagingItem.Input;
				e.PushInput(new MouseButtonEventArgs(this, mouseButtonEventArgs2.Timestamp, mouseButtonEventArgs2.ChangedButton, this.GetStylusDevice(e.StagingItem))
				{
					RoutedEvent = Mouse.MouseUpEvent
				}, e.StagingItem);
			}
			if (e.StagingItem.Input.RoutedEvent == Mouse.PreviewMouseMoveEvent && !e.StagingItem.Input.Handled)
			{
				MouseEventArgs mouseEventArgs = (MouseEventArgs)e.StagingItem.Input;
				e.PushInput(new MouseEventArgs(this, mouseEventArgs.Timestamp, this.GetStylusDevice(e.StagingItem))
				{
					RoutedEvent = Mouse.MouseMoveEvent
				}, e.StagingItem);
			}
			if (e.StagingItem.Input.RoutedEvent == Mouse.QueryCursorEvent)
			{
				QueryCursorEventArgs queryCursorEventArgs = (QueryCursorEventArgs)e.StagingItem.Input;
				this.SetCursor(queryCursorEventArgs.Cursor);
			}
			if (e.StagingItem.Input.RoutedEvent == InputManager.InputReportEvent)
			{
				InputReportEventArgs inputReportEventArgs = e.StagingItem.Input as InputReportEventArgs;
				if (!inputReportEventArgs.Handled && inputReportEventArgs.Report.Type == InputType.Mouse)
				{
					RawMouseInputReport rawMouseInputReport = (RawMouseInputReport)inputReportEventArgs.Report;
					if (this._inputSource != null && rawMouseInputReport.InputSource == this._inputSource.Value)
					{
						RawMouseActions nonRedundantActions = this.GetNonRedundantActions(e);
						if ((nonRedundantActions & RawMouseActions.Activate) == RawMouseActions.Activate)
						{
							this.Synchronize();
						}
						if ((nonRedundantActions & RawMouseActions.VerticalWheelRotate) == RawMouseActions.VerticalWheelRotate)
						{
							e.PushInput(new MouseWheelEventArgs(this, rawMouseInputReport.Timestamp, rawMouseInputReport.Wheel)
							{
								RoutedEvent = Mouse.PreviewMouseWheelEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button1Press) == RawMouseActions.Button1Press)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.Left, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseDownEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button1Release) == RawMouseActions.Button1Release)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.Left, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseUpEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button2Press) == RawMouseActions.Button2Press)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.Right, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseDownEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button2Release) == RawMouseActions.Button2Release)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.Right, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseUpEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button3Press) == RawMouseActions.Button3Press)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.Middle, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseDownEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button3Release) == RawMouseActions.Button3Release)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.Middle, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseUpEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button4Press) == RawMouseActions.Button4Press)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.XButton1, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseDownEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button4Release) == RawMouseActions.Button4Release)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.XButton1, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseUpEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button5Press) == RawMouseActions.Button5Press)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.XButton2, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseDownEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.Button5Release) == RawMouseActions.Button5Release)
						{
							e.PushInput(new MouseButtonEventArgs(this, rawMouseInputReport.Timestamp, MouseButton.XButton2, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseUpEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.AbsoluteMove) == RawMouseActions.AbsoluteMove)
						{
							e.PushInput(new MouseEventArgs(this, rawMouseInputReport.Timestamp, this.GetStylusDevice(e.StagingItem))
							{
								RoutedEvent = Mouse.PreviewMouseMoveEvent
							}, e.StagingItem);
						}
						if ((nonRedundantActions & RawMouseActions.QueryCursor) == RawMouseActions.QueryCursor)
						{
							inputReportEventArgs.Handled = this.UpdateCursorPrivate();
						}
					}
				}
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00047908 File Offset: 0x00046D08
		private RawMouseActions GetNonRedundantActions(NotifyInputEventArgs e)
		{
			RawMouseActions result = RawMouseActions.None;
			object data = e.StagingItem.GetData(this._tagNonRedundantActions);
			if (data != null)
			{
				result = (RawMouseActions)data;
			}
			return result;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00047934 File Offset: 0x00046D34
		internal static IInputElement GlobalHitTest(bool clientUnits, Point pt, PresentationSource inputSource)
		{
			IInputElement result;
			IInputElement inputElement;
			MouseDevice.GlobalHitTest(clientUnits, pt, inputSource, out result, out inputElement);
			return result;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00047950 File Offset: 0x00046D50
		internal static IInputElement GlobalHitTest(Point ptClient, PresentationSource inputSource)
		{
			return MouseDevice.GlobalHitTest(true, ptClient, inputSource);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00047968 File Offset: 0x00046D68
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private static void GlobalHitTest(bool clientUnits, Point pt, PresentationSource inputSource, out IInputElement enabledHit, out IInputElement originalHit)
		{
			IInputElement inputElement;
			originalHit = (inputElement = null);
			enabledHit = inputElement;
			Point pointClient = clientUnits ? pt : PointUtil.RootToClient(pt, inputSource);
			HwndSource hwndSource = inputSource as HwndSource;
			if (hwndSource != null && hwndSource.CompositionTarget != null && !hwndSource.IsHandleNull)
			{
				Point pointScreen = PointUtil.ClientToScreen(pointClient, hwndSource);
				IntPtr intPtr = IntPtr.Zero;
				HwndSource hwndSource2 = null;
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
				try
				{
					intPtr = UnsafeNativeMethods.WindowFromPoint((int)pointScreen.X, (int)pointScreen.Y);
					if (!SafeNativeMethods.IsWindowEnabled(new HandleRef(null, intPtr)))
					{
						intPtr = IntPtr.Zero;
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				if (intPtr != IntPtr.Zero)
				{
					hwndSource2 = HwndSource.CriticalFromHwnd(intPtr);
				}
				if (hwndSource2 != null && hwndSource2.Dispatcher == inputSource.CompositionTarget.Dispatcher)
				{
					Point pt2 = PointUtil.ScreenToClient(pointScreen, hwndSource2);
					MouseDevice.LocalHitTest(true, pt2, hwndSource2, out enabledHit, out originalHit);
				}
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00047A5C File Offset: 0x00046E5C
		internal static IInputElement LocalHitTest(bool clientUnits, Point pt, PresentationSource inputSource)
		{
			IInputElement result;
			IInputElement inputElement;
			MouseDevice.LocalHitTest(clientUnits, pt, inputSource, out result, out inputElement);
			return result;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00047A78 File Offset: 0x00046E78
		internal static IInputElement LocalHitTest(Point ptClient, PresentationSource inputSource)
		{
			return MouseDevice.LocalHitTest(true, ptClient, inputSource);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00047A90 File Offset: 0x00046E90
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private static void LocalHitTest(bool clientUnits, Point pt, PresentationSource inputSource, out IInputElement enabledHit, out IInputElement originalHit)
		{
			IInputElement inputElement;
			originalHit = (inputElement = null);
			enabledHit = inputElement;
			if (inputSource != null)
			{
				UIElement uielement = inputSource.RootVisual as UIElement;
				if (uielement != null)
				{
					Point pt2 = clientUnits ? PointUtil.ClientToRoot(pt, inputSource) : pt;
					uielement.InputHitTest(pt2, out enabledHit, out originalHit);
				}
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00047AD0 File Offset: 0x00046ED0
		internal bool IsSameSpot(Point newPosition, StylusDevice stylusDevice)
		{
			int num = (stylusDevice != null) ? stylusDevice.DoubleTapDeltaX : this._doubleClickDeltaX;
			int num2 = (stylusDevice != null) ? stylusDevice.DoubleTapDeltaY : this._doubleClickDeltaY;
			return Math.Abs(newPosition.X - this._lastClick.X) < (double)num && Math.Abs(newPosition.Y - this._lastClick.Y) < (double)num2;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00047B3C File Offset: 0x00046F3C
		internal int CalculateClickCount(MouseButton button, int timeStamp, StylusDevice stylusDevice, Point downPt)
		{
			int num = timeStamp - this._lastClickTime;
			int num2 = (stylusDevice != null) ? stylusDevice.DoubleTapDeltaTime : this._doubleClickDeltaTime;
			bool flag = this.IsSameSpot(downPt, stylusDevice);
			bool flag2 = this._lastButton == button;
			if (num < num2 && flag && flag2)
			{
				return this._clickCount + 1;
			}
			return 1;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00047B8C File Offset: 0x00046F8C
		internal Point PositionRelativeToOver
		{
			get
			{
				return this._positionRelativeToOver;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00047BA0 File Offset: 0x00046FA0
		internal Point NonRelativePosition
		{
			get
			{
				return this._lastPosition;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00047BB4 File Offset: 0x00046FB4
		internal bool IsActive
		{
			[SecurityCritical]
			[SecurityTreatAsSafe]
			get
			{
				return this._inputSource != null && this._inputSource.Value != null;
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00047BDC File Offset: 0x00046FDC
		private StylusDevice GetStylusDevice(StagingAreaInputItem stagingItem)
		{
			return stagingItem.GetData(this._tagStylusDevice) as StylusDevice;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00047BFC File Offset: 0x00046FFC
		internal StylusDevice StylusDevice
		{
			get
			{
				return this._stylusDevice;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00047C10 File Offset: 0x00047010
		private DeferredElementTreeState MouseOverTreeState
		{
			get
			{
				if (this._mouseOverTreeState == null)
				{
					this._mouseOverTreeState = new DeferredElementTreeState();
				}
				return this._mouseOverTreeState;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00047C38 File Offset: 0x00047038
		private DeferredElementTreeState MouseCaptureWithinTreeState
		{
			get
			{
				if (this._mouseCaptureWithinTreeState == null)
				{
					this._mouseCaptureWithinTreeState = new DeferredElementTreeState();
				}
				return this._mouseCaptureWithinTreeState;
			}
		}

		// Token: 0x04000A23 RID: 2595
		private SecurityCriticalDataClass<PresentationSource> _inputSource;

		// Token: 0x04000A24 RID: 2596
		private SecurityCriticalData<InputManager> _inputManager;

		// Token: 0x04000A25 RID: 2597
		private IInputElement _mouseOver;

		// Token: 0x04000A26 RID: 2598
		private DeferredElementTreeState _mouseOverTreeState;

		// Token: 0x04000A27 RID: 2599
		private bool _isPhysicallyOver;

		// Token: 0x04000A28 RID: 2600
		private WeakReference _rawMouseOver;

		// Token: 0x04000A29 RID: 2601
		private IInputElement _mouseCapture;

		// Token: 0x04000A2A RID: 2602
		private DeferredElementTreeState _mouseCaptureWithinTreeState;

		// Token: 0x04000A2B RID: 2603
		private SecurityCriticalDataClass<IMouseInputProvider> _providerCapture;

		// Token: 0x04000A2C RID: 2604
		private CaptureMode _captureMode;

		// Token: 0x04000A2D RID: 2605
		private bool _isCaptureMouseInProgress;

		// Token: 0x04000A2E RID: 2606
		private DependencyPropertyChangedEventHandler _overIsEnabledChangedEventHandler;

		// Token: 0x04000A2F RID: 2607
		private DependencyPropertyChangedEventHandler _overIsVisibleChangedEventHandler;

		// Token: 0x04000A30 RID: 2608
		private DependencyPropertyChangedEventHandler _overIsHitTestVisibleChangedEventHandler;

		// Token: 0x04000A31 RID: 2609
		private DispatcherOperationCallback _reevaluateMouseOverDelegate;

		// Token: 0x04000A32 RID: 2610
		private DispatcherOperation _reevaluateMouseOverOperation;

		// Token: 0x04000A33 RID: 2611
		private DependencyPropertyChangedEventHandler _captureIsEnabledChangedEventHandler;

		// Token: 0x04000A34 RID: 2612
		private DependencyPropertyChangedEventHandler _captureIsVisibleChangedEventHandler;

		// Token: 0x04000A35 RID: 2613
		private DependencyPropertyChangedEventHandler _captureIsHitTestVisibleChangedEventHandler;

		// Token: 0x04000A36 RID: 2614
		private DispatcherOperationCallback _reevaluateCaptureDelegate;

		// Token: 0x04000A37 RID: 2615
		private DispatcherOperation _reevaluateCaptureOperation;

		// Token: 0x04000A38 RID: 2616
		private Point _positionRelativeToOver;

		// Token: 0x04000A39 RID: 2617
		private Point _lastPosition;

		// Token: 0x04000A3A RID: 2618
		private bool _forceUpdateLastPosition;

		// Token: 0x04000A3B RID: 2619
		private object _tagNonRedundantActions = new object();

		// Token: 0x04000A3C RID: 2620
		private object _tagStylusDevice = new object();

		// Token: 0x04000A3D RID: 2621
		private object _tagRootPoint = new object();

		// Token: 0x04000A3E RID: 2622
		private Point _lastClick;

		// Token: 0x04000A3F RID: 2623
		private MouseButton _lastButton;

		// Token: 0x04000A40 RID: 2624
		private int _clickCount;

		// Token: 0x04000A41 RID: 2625
		private int _lastClickTime;

		// Token: 0x04000A42 RID: 2626
		private int _doubleClickDeltaTime;

		// Token: 0x04000A43 RID: 2627
		private int _doubleClickDeltaX;

		// Token: 0x04000A44 RID: 2628
		private int _doubleClickDeltaY;

		// Token: 0x04000A45 RID: 2629
		private Cursor _overrideCursor;

		// Token: 0x04000A46 RID: 2630
		private StylusDevice _stylusDevice;
	}
}
