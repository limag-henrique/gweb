using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Windows.Input.Tracing;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows.Input
{
	/// <summary>Representa uma única entrada de toque produzida por um dedo na tela de toque.</summary>
	// Token: 0x0200029C RID: 668
	[UIPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract class TouchDevice : InputDevice, IManipulator
	{
		/// <summary>Chamado de construtores em classes derivadas para inicializar a classe <see cref="T:System.Windows.Input.TouchDevice" />.</summary>
		/// <param name="deviceId">Um identificador exclusivo para o dispositivo de toque.</param>
		// Token: 0x06001360 RID: 4960 RVA: 0x000486F8 File Offset: 0x00047AF8
		[SecurityCritical]
		protected TouchDevice(int deviceId)
		{
			this._deviceId = deviceId;
			this._inputManager = InputManager.UnsecureCurrent;
			StylusLogic currentStylusLogic = StylusLogic.CurrentStylusLogic;
			if (currentStylusLogic != null && !(this is StylusTouchDeviceBase))
			{
				currentStylusLogic.Statistics.FeaturesUsed |= StylusTraceLogger.FeatureFlags.CustomTouchDeviceUsed;
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00048744 File Offset: 0x00047B44
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void AttachTouchDevice()
		{
			this._inputManager.PostProcessInput += this.PostProcessInput;
			this._inputManager.HitTestInvalidatedAsync += this.OnHitTestInvalidatedAsync;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00048780 File Offset: 0x00047B80
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void DetachTouchDevice()
		{
			this._inputManager.PostProcessInput -= this.PostProcessInput;
			this._inputManager.HitTestInvalidatedAsync -= this.OnHitTestInvalidatedAsync;
		}

		/// <summary>Obtém o identificador exclusivo do <see cref="T:System.Windows.Input.TouchDevice" /> conforme fornecido pelo sistema operacional.</summary>
		/// <returns>O identificador exclusivo do <see cref="T:System.Windows.Input.TouchDevice" />.</returns>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x000487BC File Offset: 0x00047BBC
		public int Id
		{
			get
			{
				return this._deviceId;
			}
		}

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.TouchDevice" /> é adicionado ao sistema de mensagens de entrada.</summary>
		// Token: 0x14000164 RID: 356
		// (add) Token: 0x06001364 RID: 4964 RVA: 0x000487D0 File Offset: 0x00047BD0
		// (remove) Token: 0x06001365 RID: 4965 RVA: 0x00048808 File Offset: 0x00047C08
		public event EventHandler Activated;

		/// <summary>Ocorre quando o <see cref="T:System.Windows.Input.TouchDevice" /> é removido do sistema de mensagens de entrada.</summary>
		// Token: 0x14000165 RID: 357
		// (add) Token: 0x06001366 RID: 4966 RVA: 0x00048840 File Offset: 0x00047C40
		// (remove) Token: 0x06001367 RID: 4967 RVA: 0x00048878 File Offset: 0x00047C78
		public event EventHandler Deactivated;

		/// <summary>Obtém um valor que indica se o dispositivo está ativo.</summary>
		/// <returns>
		///   <see langword="true" /> Se o dispositivo estiver ativo; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x000488B0 File Offset: 0x00047CB0
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		/// <summary>Obtém o elemento que recebe entrada do <see cref="T:System.Windows.Input.TouchDevice" />.</summary>
		/// <returns>O elemento que recebe entrada do <see cref="T:System.Windows.Input.TouchDevice" />.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x000488C4 File Offset: 0x00047CC4
		public sealed override IInputElement Target
		{
			get
			{
				return this._directlyOver;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.PresentationSource" /> que está relatando a entrada para este dispositivo.</summary>
		/// <returns>A fonte que está relatando a entrada para este dispositivo.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x000488D8 File Offset: 0x00047CD8
		public sealed override PresentationSource ActiveSource
		{
			[SecurityCritical]
			get
			{
				SecurityHelper.DemandUIWindowPermission();
				return this._activeSource;
			}
		}

		/// <summary>Define o <see cref="T:System.Windows.PresentationSource" /> que está relatando a entrada para este dispositivo.</summary>
		/// <param name="activeSource">A origem que relata a entrada para este dispositivo.</param>
		// Token: 0x0600136B RID: 4971 RVA: 0x000488F0 File Offset: 0x00047CF0
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected void SetActiveSource(PresentationSource activeSource)
		{
			this._activeSource = activeSource;
		}

		/// <summary>Obtém o elemento sobre o qual o ponto de contato de toque é direcionado.</summary>
		/// <returns>O elemento que o ponto de contato de toque está diretamente acima.</returns>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00048904 File Offset: 0x00047D04
		public IInputElement DirectlyOver
		{
			get
			{
				return this._directlyOver;
			}
		}

		/// <summary>Retorna a posição atual do dispositivo de toque em relação ao elemento especificado.</summary>
		/// <param name="relativeTo">O elemento que define o espaço de coordenadas.</param>
		/// <returns>A posição atual do dispositivo de toque em relação ao elemento especificado.</returns>
		// Token: 0x0600136D RID: 4973
		public abstract TouchPoint GetTouchPoint(IInputElement relativeTo);

		/// <summary>Quando substituído em uma classe derivada, retorna todos os pontos de toque coletados entre os eventos de toque mais recentes e anteriores.</summary>
		/// <param name="relativeTo">O elemento que define o espaço de coordenadas.</param>
		/// <returns>Todos os pontos de toque que foram coletados entre os eventos de toque mais recentes e anteriores.</returns>
		// Token: 0x0600136E RID: 4974
		public abstract TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo);

		// Token: 0x0600136F RID: 4975 RVA: 0x00048918 File Offset: 0x00047D18
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private IInputElement CriticalHitTest(Point point, bool isSynchronize)
		{
			IInputElement inputElement = null;
			if (this._activeSource != null)
			{
				switch (this._captureMode)
				{
				case CaptureMode.None:
					if (this._isDown)
					{
						if (isSynchronize)
						{
							inputElement = TouchDevice.GlobalHitTest(point, this._activeSource);
						}
						else
						{
							inputElement = TouchDevice.LocalHitTest(point, this._activeSource);
						}
						TouchDevice.EnsureValid(ref inputElement);
					}
					break;
				case CaptureMode.Element:
					inputElement = this._captured;
					break;
				case CaptureMode.SubTree:
				{
					IInputElement containingInputElement = InputElement.GetContainingInputElement(this._captured as DependencyObject);
					if (containingInputElement != null)
					{
						inputElement = TouchDevice.GlobalHitTest(point, this._activeSource);
					}
					TouchDevice.EnsureValid(ref inputElement);
					if (inputElement != null)
					{
						IInputElement inputElement2 = inputElement;
						while (inputElement2 != null && inputElement2 != this._captured)
						{
							UIElement uielement = inputElement2 as UIElement;
							if (uielement != null)
							{
								inputElement2 = InputElement.GetContainingInputElement(uielement.GetUIParent(true));
							}
							else
							{
								ContentElement contentElement = inputElement2 as ContentElement;
								if (contentElement != null)
								{
									inputElement2 = InputElement.GetContainingInputElement(contentElement.GetUIParent(true));
								}
								else
								{
									UIElement3D uielement3D = (UIElement3D)inputElement2;
									inputElement2 = InputElement.GetContainingInputElement(uielement3D.GetUIParent(true));
								}
							}
						}
						if (inputElement2 != this._captured)
						{
							inputElement = this._captured;
						}
					}
					else
					{
						inputElement = this._captured;
					}
					break;
				}
				}
			}
			return inputElement;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00048A34 File Offset: 0x00047E34
		private static void EnsureValid(ref IInputElement element)
		{
			if (element != null && !InputElement.IsValid(element))
			{
				element = InputElement.GetContainingInputElement(element as DependencyObject);
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00048A5C File Offset: 0x00047E5C
		private static IInputElement GlobalHitTest(Point pt, PresentationSource inputSource)
		{
			return MouseDevice.GlobalHitTest(false, pt, inputSource);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00048A74 File Offset: 0x00047E74
		private static IInputElement LocalHitTest(Point pt, PresentationSource inputSource)
		{
			return MouseDevice.LocalHitTest(false, pt, inputSource);
		}

		/// <summary>Obtém o elemento que capturou o <see cref="T:System.Windows.Input.TouchDevice" />.</summary>
		/// <returns>O elemento que capturou o <see cref="T:System.Windows.Input.TouchDevice" />.</returns>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00048A8C File Offset: 0x00047E8C
		public IInputElement Captured
		{
			get
			{
				return this._captured;
			}
		}

		/// <summary>Obtém a política de captura do <see cref="T:System.Windows.Input.TouchDevice" />.</summary>
		/// <returns>A política de captura do <see cref="T:System.Windows.Input.TouchDevice" />.</returns>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00048AA0 File Offset: 0x00047EA0
		public CaptureMode CaptureMode
		{
			get
			{
				return this._captureMode;
			}
		}

		/// <summary>Captura um toque para o elemento especificado usando o modo de captura <see cref="F:System.Windows.Input.CaptureMode.Element" />.</summary>
		/// <param name="element">O elemento que captura o entrada por toque.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver conseguido capturar o toque; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="element" /> não é um <see cref="T:System.Windows.UIElement" />, <see cref="T:System.Windows.UIElement3D" /> nem <see cref="T:System.Windows.ContentElement" />.</exception>
		// Token: 0x06001375 RID: 4981 RVA: 0x00048AB4 File Offset: 0x00047EB4
		public bool Capture(IInputElement element)
		{
			return this.Capture(element, CaptureMode.Element);
		}

		/// <summary>Captura um toque para o elemento especificado usando o <see cref="T:System.Windows.Input.CaptureMode" /> especificado.</summary>
		/// <param name="element">O elemento que captura o toque.</param>
		/// <param name="captureMode">A política de captura a ser usada.</param>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver conseguido capturar o toque; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="element" /> não é um <see cref="T:System.Windows.UIElement" />, <see cref="T:System.Windows.UIElement3D" /> nem <see cref="T:System.Windows.ContentElement" />.</exception>
		// Token: 0x06001376 RID: 4982 RVA: 0x00048ACC File Offset: 0x00047ECC
		public bool Capture(IInputElement element, CaptureMode captureMode)
		{
			base.VerifyAccess();
			if (element == null || captureMode == CaptureMode.None)
			{
				element = null;
				captureMode = CaptureMode.None;
			}
			UIElement uielement;
			ContentElement contentElement;
			UIElement3D uielement3D;
			TouchDevice.CastInputElement(element, out uielement, out contentElement, out uielement3D);
			if (element != null && uielement == null && contentElement == null && uielement3D == null)
			{
				throw new ArgumentException(SR.Get("Invalid_IInputElement", new object[]
				{
					element.GetType()
				}), "element");
			}
			if (this._captured == element)
			{
				return true;
			}
			if (element == null || (uielement != null && uielement.IsVisible && uielement.IsEnabled) || (contentElement != null && contentElement.IsEnabled) || (uielement3D != null && uielement3D.IsVisible && uielement3D.IsEnabled))
			{
				IInputElement captured = this._captured;
				this._captured = element;
				this._captureMode = captureMode;
				UIElement uielement2;
				ContentElement contentElement2;
				UIElement3D uielement3D2;
				TouchDevice.CastInputElement(captured, out uielement2, out contentElement2, out uielement3D2);
				if (uielement2 != null)
				{
					uielement2.IsEnabledChanged -= this.OnReevaluateCapture;
					uielement2.IsVisibleChanged -= this.OnReevaluateCapture;
					uielement2.IsHitTestVisibleChanged -= this.OnReevaluateCapture;
				}
				else if (contentElement2 != null)
				{
					contentElement2.IsEnabledChanged -= this.OnReevaluateCapture;
				}
				else if (uielement3D2 != null)
				{
					uielement3D2.IsEnabledChanged -= this.OnReevaluateCapture;
					uielement3D2.IsVisibleChanged -= this.OnReevaluateCapture;
					uielement3D2.IsHitTestVisibleChanged -= this.OnReevaluateCapture;
				}
				if (uielement != null)
				{
					uielement.IsEnabledChanged += this.OnReevaluateCapture;
					uielement.IsVisibleChanged += this.OnReevaluateCapture;
					uielement.IsHitTestVisibleChanged += this.OnReevaluateCapture;
				}
				else if (contentElement != null)
				{
					contentElement.IsEnabledChanged += this.OnReevaluateCapture;
				}
				else if (uielement3D != null)
				{
					uielement3D.IsEnabledChanged += this.OnReevaluateCapture;
					uielement3D.IsVisibleChanged += this.OnReevaluateCapture;
					uielement3D.IsHitTestVisibleChanged += this.OnReevaluateCapture;
				}
				this.UpdateReverseInheritedProperty(true, captured, this._captured);
				if (captured != null)
				{
					DependencyObject dependencyObject = captured as DependencyObject;
					dependencyObject.SetValue(UIElement.AreAnyTouchesCapturedPropertyKey, BooleanBoxes.Box(TouchDevice.AreAnyTouchesCapturedOrDirectlyOver(captured, true)));
				}
				if (this._captured != null)
				{
					DependencyObject dependencyObject2 = this._captured as DependencyObject;
					dependencyObject2.SetValue(UIElement.AreAnyTouchesCapturedPropertyKey, BooleanBoxes.TrueBox);
				}
				if (captured != null)
				{
					this.RaiseLostCapture(captured);
				}
				if (this._captured != null)
				{
					this.RaiseGotCapture(this._captured);
				}
				this.OnCapture(element, captureMode);
				this.Synchronize();
				return true;
			}
			return false;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00048D3C File Offset: 0x0004813C
		private void UpdateReverseInheritedProperty(bool capture, IInputElement oldElement, IInputElement newElement)
		{
			List<DependencyObject> list = null;
			int num = (TouchDevice._activeDevices != null) ? TouchDevice._activeDevices.Count : 0;
			if (num > 0)
			{
				list = new List<DependencyObject>(num);
			}
			for (int i = 0; i < num; i++)
			{
				TouchDevice touchDevice = TouchDevice._activeDevices[i];
				if (touchDevice != this)
				{
					DependencyObject dependencyObject = capture ? (touchDevice._captured as DependencyObject) : (touchDevice._directlyOver as DependencyObject);
					if (dependencyObject != null)
					{
						list.Add(dependencyObject);
					}
				}
			}
			ReverseInheritProperty reverseInheritProperty = capture ? UIElement.TouchesCapturedWithinProperty : UIElement.TouchesOverProperty;
			DeferredElementTreeState deferredElementTreeState = capture ? this._capturedWithinTreeState : this._directlyOverTreeState;
			Action<DependencyObject, bool> originChangedAction = capture ? null : this.RaiseTouchEnterOrLeaveAction;
			reverseInheritProperty.OnOriginValueChanged(oldElement as DependencyObject, newElement as DependencyObject, list, ref deferredElementTreeState, originChangedAction);
			if (capture)
			{
				this._capturedWithinTreeState = deferredElementTreeState;
				return;
			}
			this._directlyOverTreeState = deferredElementTreeState;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00048E10 File Offset: 0x00048210
		internal static void ReevaluateCapturedWithin(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			int num = (TouchDevice._activeDevices != null) ? TouchDevice._activeDevices.Count : 0;
			for (int i = 0; i < num; i++)
			{
				TouchDevice touchDevice = TouchDevice._activeDevices[i];
				touchDevice.ReevaluateCapturedWithinAsync(element, oldParent, isCoreParent);
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00048E54 File Offset: 0x00048254
		private void ReevaluateCapturedWithinAsync(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (this._capturedWithinTreeState == null)
				{
					this._capturedWithinTreeState = new DeferredElementTreeState();
				}
				if (isCoreParent)
				{
					this._capturedWithinTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this._capturedWithinTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateCapture == null)
			{
				this._reevaluateCapture = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(delegate(object args)
				{
					this._reevaluateCapture = null;
					this.OnReevaluateCapturedWithinAsync();
					return null;
				}), null);
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00048EC0 File Offset: 0x000482C0
		private void OnReevaluateCapturedWithinAsync()
		{
			if (this._captured == null)
			{
				return;
			}
			UIElement uielement;
			ContentElement contentElement;
			UIElement3D uielement3D;
			TouchDevice.CastInputElement(this._captured, out uielement, out contentElement, out uielement3D);
			bool flag;
			if (uielement != null)
			{
				flag = (!uielement.IsEnabled || !uielement.IsVisible || !uielement.IsHitTestVisible);
			}
			else if (contentElement != null)
			{
				flag = !contentElement.IsEnabled;
			}
			else
			{
				flag = (uielement3D == null || !uielement3D.IsEnabled || !uielement3D.IsVisible || !uielement3D.IsHitTestVisible);
			}
			if (!flag)
			{
				DependencyObject containingVisual = InputElement.GetContainingVisual(this._captured as DependencyObject);
				flag = !this.ValidateVisualForCapture(containingVisual);
			}
			if (flag)
			{
				this.Capture(null);
			}
			if (this._capturedWithinTreeState != null && !this._capturedWithinTreeState.IsEmpty)
			{
				this.UpdateReverseInheritedProperty(true, this._captured, this._captured);
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00048F94 File Offset: 0x00048394
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool ValidateVisualForCapture(DependencyObject visual)
		{
			if (visual == null)
			{
				return false;
			}
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(visual);
			return presentationSource != null && presentationSource == this._activeSource;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00048FBC File Offset: 0x000483BC
		private void OnReevaluateCapture(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(bool)e.NewValue && this._reevaluateCapture == null)
			{
				this._reevaluateCapture = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(delegate(object args)
				{
					this._reevaluateCapture = null;
					this.Capture(null);
					return null;
				}), null);
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00049000 File Offset: 0x00048400
		private static void CastInputElement(IInputElement element, out UIElement uiElement, out ContentElement contentElement, out UIElement3D uiElement3D)
		{
			uiElement = (element as UIElement);
			contentElement = ((uiElement == null) ? (element as ContentElement) : null);
			uiElement3D = ((uiElement == null && contentElement == null) ? (element as UIElement3D) : null);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00049038 File Offset: 0x00048438
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void RaiseLostCapture(IInputElement oldCapture)
		{
			TouchEventArgs touchEventArgs = this.CreateEventArgs(Touch.LostTouchCaptureEvent);
			touchEventArgs.Source = oldCapture;
			this._inputManager.ProcessInput(touchEventArgs);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00049068 File Offset: 0x00048468
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void RaiseGotCapture(IInputElement captured)
		{
			TouchEventArgs touchEventArgs = this.CreateEventArgs(Touch.GotTouchCaptureEvent);
			touchEventArgs.Source = captured;
			this._inputManager.ProcessInput(touchEventArgs);
		}

		/// <summary>Chamado quando um toque é capturado para um elemento.</summary>
		/// <param name="element">O elemento que captura o entrada por toque.</param>
		/// <param name="captureMode">A política de captura.</param>
		// Token: 0x06001380 RID: 4992 RVA: 0x00049098 File Offset: 0x00048498
		protected virtual void OnCapture(IInputElement element, CaptureMode captureMode)
		{
		}

		/// <summary>Relata que um toque é pressionado em um elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o evento <see cref="E:System.Windows.UIElement.TouchDown" /> tiver sido manipulado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001381 RID: 4993 RVA: 0x000490A8 File Offset: 0x000484A8
		protected bool ReportDown()
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, EventTrace.Event.TouchDownReported, this._deviceId);
			this._isDown = true;
			this.UpdateDirectlyOver(false);
			bool result = this.RaiseTouchDown();
			this.OnUpdated();
			Touch.ReportFrame();
			return result;
		}

		/// <summary>Relata um toque está se movendo em um elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o evento <see cref="E:System.Windows.UIElement.TouchMove" /> tiver sido manipulado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001382 RID: 4994 RVA: 0x000490F0 File Offset: 0x000484F0
		protected bool ReportMove()
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, EventTrace.Event.TouchMoveReported, this._deviceId);
			this.UpdateDirectlyOver(false);
			bool result = this.RaiseTouchMove();
			this.OnUpdated();
			Touch.ReportFrame();
			return result;
		}

		/// <summary>Relata que um toque foi removido de um elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o evento <see cref="E:System.Windows.UIElement.TouchUp" /> tiver sido manipulado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001383 RID: 4995 RVA: 0x00049130 File Offset: 0x00048530
		protected bool ReportUp()
		{
			EventTrace.EasyTraceEvent(EventTrace.Keyword.KeywordPerf | EventTrace.Keyword.KeywordInput, EventTrace.Level.Info, EventTrace.Event.TouchUpReported, this._deviceId);
			if (this._reevaluateOver != null)
			{
				this._reevaluateOver = null;
				this.OnHitTestInvalidatedAsync(this, EventArgs.Empty);
			}
			bool result = this.RaiseTouchUp();
			this._isDown = false;
			this.UpdateDirectlyOver(false);
			this.OnUpdated();
			Touch.ReportFrame();
			return result;
		}

		/// <summary>Adiciona o <see cref="T:System.Windows.Input.TouchDevice" /> ao sistema de mensagens de entrada.</summary>
		/// <exception cref="T:System.InvalidOperationException">O dispositivo já está ativado.</exception>
		// Token: 0x06001384 RID: 4996 RVA: 0x00049194 File Offset: 0x00048594
		protected void Activate()
		{
			if (this._isActive)
			{
				throw new InvalidOperationException(SR.Get("Touch_DeviceAlreadyActivated"));
			}
			this.PromotingToManipulation = false;
			TouchDevice.AddActiveDevice(this);
			this.AttachTouchDevice();
			this.Synchronize();
			if (TouchDevice._activeDevices.Count == 1)
			{
				this._isPrimary = true;
			}
			this._isActive = true;
			if (this.Activated != null)
			{
				this.Activated(this, EventArgs.Empty);
			}
		}

		/// <summary>Remove o <see cref="T:System.Windows.Input.TouchDevice" /> do sistema de mensagens de entrada.</summary>
		/// <exception cref="T:System.InvalidOperationException">O dispositivo não está ativado.</exception>
		// Token: 0x06001385 RID: 4997 RVA: 0x00049208 File Offset: 0x00048608
		protected void Deactivate()
		{
			if (!this._isActive)
			{
				throw new InvalidOperationException(SR.Get("Touch_DeviceNotActivated"));
			}
			this.Capture(null);
			this.DetachTouchDevice();
			TouchDevice.RemoveActiveDevice(this);
			this._isActive = false;
			this._manipulatingElement = null;
			if (this.Deactivated != null)
			{
				this.Deactivated(this, EventArgs.Empty);
			}
		}

		/// <summary>Força o <see cref="T:System.Windows.Input.TouchDevice" /> a sincronizar a interface do usuário com os pontos de contato subjacentes.</summary>
		// Token: 0x06001386 RID: 4998 RVA: 0x00049268 File Offset: 0x00048668
		[SecurityCritical]
		public void Synchronize()
		{
			if (this._activeSource != null && this._activeSource.CompositionTarget != null && !this._activeSource.CompositionTarget.IsDisposed && this.UpdateDirectlyOver(true))
			{
				this.OnUpdated();
				Touch.ReportFrame();
			}
		}

		/// <summary>Chamado quando uma manipulação terminou.</summary>
		/// <param name="cancel">
		///   <see langword="true" /> para cancelar a ação; caso contrário, <see langword="false" />.</param>
		// Token: 0x06001387 RID: 4999 RVA: 0x000492B0 File Offset: 0x000486B0
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected virtual void OnManipulationEnded(bool cancel)
		{
			UIElement manipulatableElement = this.GetManipulatableElement();
			if (manipulatableElement != null && this.PromotingToManipulation)
			{
				this.Capture(null);
			}
		}

		/// <summary>Chamado quando uma manipulação é iniciada.</summary>
		// Token: 0x06001388 RID: 5000 RVA: 0x000492D8 File Offset: 0x000486D8
		protected virtual void OnManipulationStarted()
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000492E8 File Offset: 0x000486E8
		private void OnHitTestInvalidatedAsync(object sender, EventArgs e)
		{
			this.Synchronize();
			if (this._directlyOverTreeState != null && !this._directlyOverTreeState.IsEmpty)
			{
				this.UpdateReverseInheritedProperty(false, this._directlyOver, this._directlyOver);
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00049324 File Offset: 0x00048724
		private bool UpdateDirectlyOver(bool isSynchronize)
		{
			IInputElement inputElement = null;
			TouchPoint touchPoint = this.GetTouchPoint(null);
			if (touchPoint != null)
			{
				Point position = touchPoint.Position;
				inputElement = this.CriticalHitTest(position, isSynchronize);
			}
			if (inputElement != this._directlyOver)
			{
				this.ChangeDirectlyOver(inputElement);
				return true;
			}
			return false;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00049364 File Offset: 0x00048764
		private void OnReevaluateDirectlyOver(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.ReevaluateDirectlyOverAsync(null, null, true);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004937C File Offset: 0x0004877C
		internal static void ReevaluateDirectlyOver(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			int num = (TouchDevice._activeDevices != null) ? TouchDevice._activeDevices.Count : 0;
			for (int i = 0; i < num; i++)
			{
				TouchDevice touchDevice = TouchDevice._activeDevices[i];
				touchDevice.ReevaluateDirectlyOverAsync(element, oldParent, isCoreParent);
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000493C0 File Offset: 0x000487C0
		private void ReevaluateDirectlyOverAsync(DependencyObject element, DependencyObject oldParent, bool isCoreParent)
		{
			if (element != null)
			{
				if (this._directlyOverTreeState == null)
				{
					this._directlyOverTreeState = new DeferredElementTreeState();
				}
				if (isCoreParent)
				{
					this._directlyOverTreeState.SetCoreParent(element, oldParent);
				}
				else
				{
					this._directlyOverTreeState.SetLogicalParent(element, oldParent);
				}
			}
			if (this._reevaluateOver == null)
			{
				this._reevaluateOver = base.Dispatcher.BeginInvoke(DispatcherPriority.Input, new DispatcherOperationCallback(delegate(object args)
				{
					this._reevaluateOver = null;
					this.OnHitTestInvalidatedAsync(this, EventArgs.Empty);
					return null;
				}), null);
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0004942C File Offset: 0x0004882C
		private void ChangeDirectlyOver(IInputElement newDirectlyOver)
		{
			IInputElement directlyOver = this._directlyOver;
			this._directlyOver = newDirectlyOver;
			UIElement uielement;
			ContentElement contentElement;
			UIElement3D uielement3D;
			TouchDevice.CastInputElement(directlyOver, out uielement, out contentElement, out uielement3D);
			UIElement uielement2;
			ContentElement contentElement2;
			UIElement3D uielement3D2;
			TouchDevice.CastInputElement(newDirectlyOver, out uielement2, out contentElement2, out uielement3D2);
			if (uielement != null)
			{
				uielement.IsEnabledChanged -= this.OnReevaluateDirectlyOver;
				uielement.IsVisibleChanged -= this.OnReevaluateDirectlyOver;
				uielement.IsHitTestVisibleChanged -= this.OnReevaluateDirectlyOver;
			}
			else if (contentElement != null)
			{
				contentElement.IsEnabledChanged -= this.OnReevaluateDirectlyOver;
			}
			else if (uielement3D != null)
			{
				uielement3D.IsEnabledChanged -= this.OnReevaluateDirectlyOver;
				uielement3D.IsVisibleChanged -= this.OnReevaluateDirectlyOver;
				uielement3D.IsHitTestVisibleChanged -= this.OnReevaluateDirectlyOver;
			}
			if (uielement2 != null)
			{
				uielement2.IsEnabledChanged += this.OnReevaluateDirectlyOver;
				uielement2.IsVisibleChanged += this.OnReevaluateDirectlyOver;
				uielement2.IsHitTestVisibleChanged += this.OnReevaluateDirectlyOver;
			}
			else if (contentElement2 != null)
			{
				contentElement2.IsEnabledChanged += this.OnReevaluateDirectlyOver;
			}
			else if (uielement3D2 != null)
			{
				uielement3D2.IsEnabledChanged += this.OnReevaluateDirectlyOver;
				uielement3D2.IsVisibleChanged += this.OnReevaluateDirectlyOver;
				uielement3D2.IsHitTestVisibleChanged += this.OnReevaluateDirectlyOver;
			}
			this.UpdateReverseInheritedProperty(false, directlyOver, newDirectlyOver);
			if (directlyOver != null)
			{
				DependencyObject dependencyObject = directlyOver as DependencyObject;
				dependencyObject.SetValue(UIElement.AreAnyTouchesDirectlyOverPropertyKey, BooleanBoxes.Box(TouchDevice.AreAnyTouchesCapturedOrDirectlyOver(directlyOver, false)));
			}
			if (newDirectlyOver != null)
			{
				DependencyObject dependencyObject2 = newDirectlyOver as DependencyObject;
				dependencyObject2.SetValue(UIElement.AreAnyTouchesDirectlyOverPropertyKey, BooleanBoxes.TrueBox);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x000495C8 File Offset: 0x000489C8
		private Action<DependencyObject, bool> RaiseTouchEnterOrLeaveAction
		{
			get
			{
				if (this._raiseTouchEnterOrLeaveAction == null)
				{
					this._raiseTouchEnterOrLeaveAction = new Action<DependencyObject, bool>(this.RaiseTouchEnterOrLeave);
				}
				return this._raiseTouchEnterOrLeaveAction;
			}
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000495F8 File Offset: 0x000489F8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private void RaiseTouchEnterOrLeave(DependencyObject element, bool isLeave)
		{
			TouchEventArgs touchEventArgs = this.CreateEventArgs(isLeave ? Touch.TouchLeaveEvent : Touch.TouchEnterEvent);
			touchEventArgs.Source = element;
			this._inputManager.ProcessInput(touchEventArgs);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00049630 File Offset: 0x00048A30
		private TouchEventArgs CreateEventArgs(RoutedEvent routedEvent)
		{
			return new TouchEventArgs(this, Environment.TickCount)
			{
				RoutedEvent = routedEvent
			};
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00049654 File Offset: 0x00048A54
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool RaiseTouchDown()
		{
			TouchEventArgs input = this.CreateEventArgs(Touch.PreviewTouchDownEvent);
			this._lastDownHandled = false;
			this._inputManager.ProcessInput(input);
			return this._lastDownHandled;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00049688 File Offset: 0x00048A88
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool RaiseTouchMove()
		{
			TouchEventArgs input = this.CreateEventArgs(Touch.PreviewTouchMoveEvent);
			this._lastMoveHandled = false;
			this._inputManager.ProcessInput(input);
			return this._lastMoveHandled;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000496BC File Offset: 0x00048ABC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		private bool RaiseTouchUp()
		{
			TouchEventArgs input = this.CreateEventArgs(Touch.PreviewTouchUpEvent);
			this._lastUpHandled = false;
			this._inputManager.ProcessInput(input);
			return this._lastUpHandled;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000496F0 File Offset: 0x00048AF0
		[SecurityCritical]
		private void PostProcessInput(object sender, ProcessInputEventArgs e)
		{
			InputEventArgs input = e.StagingItem.Input;
			if (input != null && input.Device == this)
			{
				if (input.Handled)
				{
					RoutedEvent routedEvent = input.RoutedEvent;
					if (routedEvent == Touch.PreviewTouchMoveEvent || routedEvent == Touch.TouchMoveEvent)
					{
						this._lastMoveHandled = true;
						return;
					}
					if (routedEvent == Touch.PreviewTouchDownEvent || routedEvent == Touch.TouchDownEvent)
					{
						this._lastDownHandled = true;
						return;
					}
					if (routedEvent == Touch.PreviewTouchUpEvent || routedEvent == Touch.TouchUpEvent)
					{
						this._lastUpHandled = true;
						return;
					}
				}
				else
				{
					bool flag;
					RoutedEvent routedEvent2 = this.PromotePreviewToMain(input.RoutedEvent, out flag);
					if (routedEvent2 != null)
					{
						TouchEventArgs input2 = this.CreateEventArgs(routedEvent2);
						e.PushInput(input2, e.StagingItem);
						return;
					}
					if (flag)
					{
						UIElement manipulatableElement = this.GetManipulatableElement();
						if (manipulatableElement != null)
						{
							this.PromoteMainToManipulation(manipulatableElement, (TouchEventArgs)input);
						}
					}
				}
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000497B8 File Offset: 0x00048BB8
		private RoutedEvent PromotePreviewToMain(RoutedEvent routedEvent, out bool forManipulation)
		{
			forManipulation = false;
			if (routedEvent == Touch.PreviewTouchMoveEvent)
			{
				return Touch.TouchMoveEvent;
			}
			if (routedEvent == Touch.PreviewTouchDownEvent)
			{
				return Touch.TouchDownEvent;
			}
			if (routedEvent == Touch.PreviewTouchUpEvent)
			{
				return Touch.TouchUpEvent;
			}
			forManipulation = (routedEvent == Touch.TouchMoveEvent || routedEvent == Touch.TouchDownEvent || routedEvent == Touch.TouchUpEvent || routedEvent == Touch.GotTouchCaptureEvent || routedEvent == Touch.LostTouchCaptureEvent);
			return null;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00049820 File Offset: 0x00048C20
		private UIElement GetManipulatableElement()
		{
			UIElement uielement = InputElement.GetContainingUIElement(this._directlyOver as DependencyObject) as UIElement;
			if (uielement != null)
			{
				uielement = Manipulation.FindManipulationParent(uielement);
			}
			return uielement;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00049850 File Offset: 0x00048C50
		private void PromoteMainToManipulation(UIElement manipulatableElement, TouchEventArgs touchEventArgs)
		{
			RoutedEvent routedEvent = touchEventArgs.RoutedEvent;
			if (routedEvent == Touch.TouchDownEvent)
			{
				this.Capture(manipulatableElement);
				return;
			}
			if (routedEvent == Touch.TouchUpEvent && this.PromotingToManipulation)
			{
				this.Capture(null);
				return;
			}
			if (routedEvent == Touch.GotTouchCaptureEvent && !this.PromotingToManipulation)
			{
				UIElement uielement = this._captured as UIElement;
				if (uielement != null && uielement.IsManipulationEnabled)
				{
					this._manipulatingElement = new WeakReference(uielement);
					Manipulation.AddManipulator(uielement, this);
					this.PromotingToManipulation = true;
					this.OnManipulationStarted();
					return;
				}
			}
			else if (routedEvent == Touch.LostTouchCaptureEvent && this.PromotingToManipulation && this._manipulatingElement != null)
			{
				UIElement uielement2 = this._manipulatingElement.Target as UIElement;
				this._manipulatingElement = null;
				if (uielement2 != null)
				{
					Manipulation.TryRemoveManipulator(uielement2, this);
					this.PromotingToManipulation = false;
				}
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00049918 File Offset: 0x00048D18
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0004992C File Offset: 0x00048D2C
		internal bool PromotingToManipulation { get; private set; }

		// Token: 0x0600139B RID: 5019 RVA: 0x00049940 File Offset: 0x00048D40
		private static void AddActiveDevice(TouchDevice device)
		{
			if (TouchDevice._activeDevices == null)
			{
				TouchDevice._activeDevices = new List<TouchDevice>(2);
			}
			TouchDevice._activeDevices.Add(device);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004996C File Offset: 0x00048D6C
		private static void RemoveActiveDevice(TouchDevice device)
		{
			if (TouchDevice._activeDevices != null)
			{
				TouchDevice._activeDevices.Remove(device);
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004998C File Offset: 0x00048D8C
		internal static TouchPointCollection GetTouchPoints(IInputElement relativeTo)
		{
			TouchPointCollection touchPointCollection = new TouchPointCollection();
			if (TouchDevice._activeDevices != null)
			{
				int count = TouchDevice._activeDevices.Count;
				for (int i = 0; i < count; i++)
				{
					TouchDevice touchDevice = TouchDevice._activeDevices[i];
					touchPointCollection.Add(touchDevice.GetTouchPoint(relativeTo));
				}
			}
			return touchPointCollection;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000499D8 File Offset: 0x00048DD8
		internal static TouchPoint GetPrimaryTouchPoint(IInputElement relativeTo)
		{
			if (TouchDevice._activeDevices != null && TouchDevice._activeDevices.Count > 0)
			{
				TouchDevice touchDevice = TouchDevice._activeDevices[0];
				if (touchDevice._isPrimary)
				{
					return touchDevice.GetTouchPoint(relativeTo);
				}
			}
			return null;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00049A18 File Offset: 0x00048E18
		internal static void ReleaseAllCaptures(IInputElement element)
		{
			if (TouchDevice._activeDevices != null)
			{
				int count = TouchDevice._activeDevices.Count;
				for (int i = 0; i < count; i++)
				{
					TouchDevice touchDevice = TouchDevice._activeDevices[i];
					if (touchDevice.Captured == element)
					{
						touchDevice.Capture(null);
					}
				}
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00049A60 File Offset: 0x00048E60
		internal static IEnumerable<TouchDevice> GetCapturedTouches(IInputElement element, bool includeWithin)
		{
			return TouchDevice.GetCapturedOrOverTouches(element, includeWithin, true);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00049A78 File Offset: 0x00048E78
		internal static IEnumerable<TouchDevice> GetTouchesOver(IInputElement element, bool includeWithin)
		{
			return TouchDevice.GetCapturedOrOverTouches(element, includeWithin, false);
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00049A90 File Offset: 0x00048E90
		private static bool IsWithin(IInputElement parent, IInputElement child)
		{
			DependencyObject dependencyObject = child as DependencyObject;
			while (dependencyObject != null && dependencyObject != parent)
			{
				if (dependencyObject is Visual || dependencyObject is Visual3D)
				{
					dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
				}
				else
				{
					dependencyObject = ((ContentElement)dependencyObject).Parent;
				}
			}
			return dependencyObject == parent;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00049AD8 File Offset: 0x00048ED8
		private static IEnumerable<TouchDevice> GetCapturedOrOverTouches(IInputElement element, bool includeWithin, bool isCapture)
		{
			List<TouchDevice> list = new List<TouchDevice>();
			if (TouchDevice._activeDevices != null)
			{
				int count = TouchDevice._activeDevices.Count;
				for (int i = 0; i < count; i++)
				{
					TouchDevice touchDevice = TouchDevice._activeDevices[i];
					IInputElement inputElement = isCapture ? touchDevice.Captured : touchDevice.DirectlyOver;
					if (inputElement != null && (inputElement == element || (includeWithin && TouchDevice.IsWithin(element, inputElement))))
					{
						list.Add(touchDevice);
					}
				}
			}
			return list;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00049B44 File Offset: 0x00048F44
		private static bool AreAnyTouchesCapturedOrDirectlyOver(IInputElement element, bool isCapture)
		{
			if (TouchDevice._activeDevices != null)
			{
				int count = TouchDevice._activeDevices.Count;
				for (int i = 0; i < count; i++)
				{
					TouchDevice touchDevice = TouchDevice._activeDevices[i];
					IInputElement inputElement = isCapture ? touchDevice.Captured : touchDevice.DirectlyOver;
					if (inputElement != null && inputElement == element)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Obtém o identificador exclusivo do <see cref="T:System.Windows.Input.TouchDevice" /> conforme fornecido pelo sistema operacional.</summary>
		/// <returns>O identificador exclusivo do <see cref="T:System.Windows.Input.TouchDevice" />.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00049B98 File Offset: 0x00048F98
		int IManipulator.Id
		{
			get
			{
				return this.Id;
			}
		}

		/// <summary>Retorna a posição do objeto <see cref="T:System.Windows.Input.IManipulator" />.</summary>
		/// <param name="relativeTo">O elemento a ser usado como o quadro de referência para calcular a posição do <see cref="T:System.Windows.Input.IManipulator" />.</param>
		/// <returns>A posição do objeto <see cref="T:System.Windows.Input.IManipulator" />.</returns>
		// Token: 0x060013A6 RID: 5030 RVA: 0x00049BAC File Offset: 0x00048FAC
		Point IManipulator.GetPosition(IInputElement relativeTo)
		{
			return this.GetTouchPoint(relativeTo).Position;
		}

		/// <summary>Ocorre quando uma mensagem de toque é enviada.</summary>
		// Token: 0x14000166 RID: 358
		// (add) Token: 0x060013A7 RID: 5031 RVA: 0x00049BC8 File Offset: 0x00048FC8
		// (remove) Token: 0x060013A8 RID: 5032 RVA: 0x00049C00 File Offset: 0x00049000
		public event EventHandler Updated;

		// Token: 0x060013A9 RID: 5033 RVA: 0x00049C38 File Offset: 0x00049038
		private void OnUpdated()
		{
			if (this.Updated != null)
			{
				this.Updated(this, EventArgs.Empty);
			}
		}

		/// <summary>Ocorre quando uma manipulação terminou.</summary>
		/// <param name="cancel">
		///   <see langword="true" /> para cancelar a ação; caso contrário, <see langword="false" />.</param>
		// Token: 0x060013AA RID: 5034 RVA: 0x00049C60 File Offset: 0x00049060
		[SecurityCritical]
		[UIPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		void IManipulator.ManipulationEnded(bool cancel)
		{
			this.OnManipulationEnded(cancel);
		}

		// Token: 0x04000AA1 RID: 2721
		private int _deviceId;

		// Token: 0x04000AA2 RID: 2722
		private IInputElement _directlyOver;

		// Token: 0x04000AA3 RID: 2723
		private IInputElement _captured;

		// Token: 0x04000AA4 RID: 2724
		private CaptureMode _captureMode;

		// Token: 0x04000AA5 RID: 2725
		private bool _isDown;

		// Token: 0x04000AA6 RID: 2726
		private DispatcherOperation _reevaluateCapture;

		// Token: 0x04000AA7 RID: 2727
		private DispatcherOperation _reevaluateOver;

		// Token: 0x04000AA8 RID: 2728
		private DeferredElementTreeState _directlyOverTreeState;

		// Token: 0x04000AA9 RID: 2729
		private DeferredElementTreeState _capturedWithinTreeState;

		// Token: 0x04000AAA RID: 2730
		private bool _isPrimary;

		// Token: 0x04000AAB RID: 2731
		private bool _isActive;

		// Token: 0x04000AAC RID: 2732
		private Action<DependencyObject, bool> _raiseTouchEnterOrLeaveAction;

		// Token: 0x04000AAD RID: 2733
		private bool _lastDownHandled;

		// Token: 0x04000AAE RID: 2734
		private bool _lastUpHandled;

		// Token: 0x04000AAF RID: 2735
		private bool _lastMoveHandled;

		// Token: 0x04000AB0 RID: 2736
		[SecurityCritical]
		private PresentationSource _activeSource;

		// Token: 0x04000AB1 RID: 2737
		[SecurityCritical]
		private InputManager _inputManager;

		// Token: 0x04000AB2 RID: 2738
		private WeakReference _manipulatingElement;

		// Token: 0x04000AB3 RID: 2739
		[ThreadStatic]
		private static List<TouchDevice> _activeDevices;
	}
}
