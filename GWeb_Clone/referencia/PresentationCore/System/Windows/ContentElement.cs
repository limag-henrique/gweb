using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MS.Internal;
using MS.Internal.Interop;
using MS.Internal.KnownBoxes;
using MS.Internal.Permissions;
using MS.Internal.PresentationCore;
using MS.Utility;

namespace System.Windows
{
	/// <summary>Fornece uma classe base de nível de núcleo do WPF para elementos de conteúdo. Elementos de conteúdo são projetados para apresentação de estilo de fluxo usando um modelo de layout intuitivo orientado para marcação e um modelo de objeto deliberadamente simples.</summary>
	// Token: 0x0200018F RID: 399
	public class ContentElement : DependencyObject, IInputElement, IAnimatable
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x000166D8 File Offset: 0x00015AD8
		[SecurityTreatAsSafe]
		[SecurityCritical]
		static ContentElement()
		{
			ContentElement.GotFocusEvent = FocusManager.GotFocusEvent.AddOwner(typeof(ContentElement));
			ContentElement.LostFocusEvent = FocusManager.LostFocusEvent.AddOwner(typeof(ContentElement));
			ContentElement.IsFocusedProperty = UIElement.IsFocusedProperty.AddOwner(typeof(ContentElement));
			ContentElement.IsEnabledProperty = UIElement.IsEnabledProperty.AddOwner(typeof(ContentElement), new UIPropertyMetadata(BooleanBoxes.TrueBox, new PropertyChangedCallback(ContentElement.OnIsEnabledChanged), new CoerceValueCallback(ContentElement.CoerceIsEnabled)));
			ContentElement.FocusableProperty = UIElement.FocusableProperty.AddOwner(typeof(ContentElement), new UIPropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ContentElement.OnFocusableChanged)));
			ContentElement.AllowDropProperty = UIElement.AllowDropProperty.AddOwner(typeof(ContentElement), new PropertyMetadata(BooleanBoxes.FalseBox));
			ContentElement.EventHandlersStoreField = UIElement.EventHandlersStoreField;
			ContentElement.InputBindingCollectionField = UIElement.InputBindingCollectionField;
			ContentElement.CommandBindingCollectionField = UIElement.CommandBindingCollectionField;
			ContentElement.AutomationPeerField = new UncommonField<AutomationPeer>();
			ContentElement.ContentElementType = DependencyObjectType.FromSystemTypeInternal(typeof(ContentElement));
			ContentElement._typeofThis = typeof(ContentElement);
			ContentElement.PreviewMouseDownEvent = Mouse.PreviewMouseDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseDownEvent = Mouse.MouseDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewMouseUpEvent = Mouse.PreviewMouseUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseUpEvent = Mouse.MouseUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewMouseLeftButtonDownEvent = UIElement.PreviewMouseLeftButtonDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseLeftButtonDownEvent = UIElement.MouseLeftButtonDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewMouseLeftButtonUpEvent = UIElement.PreviewMouseLeftButtonUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseLeftButtonUpEvent = UIElement.MouseLeftButtonUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewMouseRightButtonDownEvent = UIElement.PreviewMouseRightButtonDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseRightButtonDownEvent = UIElement.MouseRightButtonDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewMouseRightButtonUpEvent = UIElement.PreviewMouseRightButtonUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseRightButtonUpEvent = UIElement.MouseRightButtonUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewMouseMoveEvent = Mouse.PreviewMouseMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseMoveEvent = Mouse.MouseMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewMouseWheelEvent = Mouse.PreviewMouseWheelEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseWheelEvent = Mouse.MouseWheelEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseEnterEvent = Mouse.MouseEnterEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.MouseLeaveEvent = Mouse.MouseLeaveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.GotMouseCaptureEvent = Mouse.GotMouseCaptureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.LostMouseCaptureEvent = Mouse.LostMouseCaptureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.QueryCursorEvent = Mouse.QueryCursorEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusDownEvent = Stylus.PreviewStylusDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusDownEvent = Stylus.StylusDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusUpEvent = Stylus.PreviewStylusUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusUpEvent = Stylus.StylusUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusMoveEvent = Stylus.PreviewStylusMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusMoveEvent = Stylus.StylusMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusInAirMoveEvent = Stylus.PreviewStylusInAirMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusInAirMoveEvent = Stylus.StylusInAirMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusEnterEvent = Stylus.StylusEnterEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusLeaveEvent = Stylus.StylusLeaveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusInRangeEvent = Stylus.PreviewStylusInRangeEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusInRangeEvent = Stylus.StylusInRangeEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusOutOfRangeEvent = Stylus.PreviewStylusOutOfRangeEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusOutOfRangeEvent = Stylus.StylusOutOfRangeEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusSystemGestureEvent = Stylus.PreviewStylusSystemGestureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusSystemGestureEvent = Stylus.StylusSystemGestureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.GotStylusCaptureEvent = Stylus.GotStylusCaptureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.LostStylusCaptureEvent = Stylus.LostStylusCaptureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusButtonDownEvent = Stylus.StylusButtonDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.StylusButtonUpEvent = Stylus.StylusButtonUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusButtonDownEvent = Stylus.PreviewStylusButtonDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewStylusButtonUpEvent = Stylus.PreviewStylusButtonUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewKeyDownEvent = Keyboard.PreviewKeyDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.KeyDownEvent = Keyboard.KeyDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewKeyUpEvent = Keyboard.PreviewKeyUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.KeyUpEvent = Keyboard.KeyUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewGotKeyboardFocusEvent = Keyboard.PreviewGotKeyboardFocusEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.GotKeyboardFocusEvent = Keyboard.GotKeyboardFocusEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewLostKeyboardFocusEvent = Keyboard.PreviewLostKeyboardFocusEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.LostKeyboardFocusEvent = Keyboard.LostKeyboardFocusEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewTextInputEvent = TextCompositionManager.PreviewTextInputEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.TextInputEvent = TextCompositionManager.TextInputEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewQueryContinueDragEvent = DragDrop.PreviewQueryContinueDragEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.QueryContinueDragEvent = DragDrop.QueryContinueDragEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewGiveFeedbackEvent = DragDrop.PreviewGiveFeedbackEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.GiveFeedbackEvent = DragDrop.GiveFeedbackEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewDragEnterEvent = DragDrop.PreviewDragEnterEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.DragEnterEvent = DragDrop.DragEnterEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewDragOverEvent = DragDrop.PreviewDragOverEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.DragOverEvent = DragDrop.DragOverEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewDragLeaveEvent = DragDrop.PreviewDragLeaveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.DragLeaveEvent = DragDrop.DragLeaveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewDropEvent = DragDrop.PreviewDropEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.DropEvent = DragDrop.DropEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewTouchDownEvent = Touch.PreviewTouchDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.TouchDownEvent = Touch.TouchDownEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewTouchMoveEvent = Touch.PreviewTouchMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.TouchMoveEvent = Touch.TouchMoveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.PreviewTouchUpEvent = Touch.PreviewTouchUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.TouchUpEvent = Touch.TouchUpEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.GotTouchCaptureEvent = Touch.GotTouchCaptureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.LostTouchCaptureEvent = Touch.LostTouchCaptureEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.TouchEnterEvent = Touch.TouchEnterEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.TouchLeaveEvent = Touch.TouchLeaveEvent.AddOwner(ContentElement._typeofThis);
			ContentElement.IsMouseDirectlyOverProperty = UIElement.IsMouseDirectlyOverProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsMouseOverProperty = UIElement.IsMouseOverProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsStylusOverProperty = UIElement.IsStylusOverProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsKeyboardFocusWithinProperty = UIElement.IsKeyboardFocusWithinProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsMouseCapturedProperty = UIElement.IsMouseCapturedProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsMouseCaptureWithinProperty = UIElement.IsMouseCaptureWithinProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsStylusDirectlyOverProperty = UIElement.IsStylusDirectlyOverProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsStylusCapturedProperty = UIElement.IsStylusCapturedProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsStylusCaptureWithinProperty = UIElement.IsStylusCaptureWithinProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.IsKeyboardFocusedProperty = UIElement.IsKeyboardFocusedProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.AreAnyTouchesDirectlyOverProperty = UIElement.AreAnyTouchesDirectlyOverProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.AreAnyTouchesOverProperty = UIElement.AreAnyTouchesOverProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.AreAnyTouchesCapturedProperty = UIElement.AreAnyTouchesCapturedProperty.AddOwner(ContentElement._typeofThis);
			ContentElement.AreAnyTouchesCapturedWithinProperty = UIElement.AreAnyTouchesCapturedWithinProperty.AddOwner(ContentElement._typeofThis);
			UIElement.RegisterEvents(typeof(ContentElement));
			ContentElement.RegisterProperties();
			UIElement.IsFocusedPropertyKey.OverrideMetadata(typeof(ContentElement), new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ContentElement.IsFocused_Changed)));
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00016F3C File Offset: 0x0001633C
		internal DependencyObject GetUIParent()
		{
			return this.GetUIParent(false);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00016F50 File Offset: 0x00016350
		internal DependencyObject GetUIParent(bool continuePastVisualTree)
		{
			DependencyObject dependencyObject = InputElement.GetContainingInputElement(this._parent) as DependencyObject;
			if (dependencyObject == null && continuePastVisualTree)
			{
				DependencyObject uiparentCore = this.GetUIParentCore();
				dependencyObject = (InputElement.GetContainingInputElement(uiparentCore) as DependencyObject);
			}
			return dependencyObject;
		}

		/// <summary>Quando substituído em uma classe derivada, retornará um pai UI (interface do usuário) alternativo para esse elemento se nenhum pai visual existir.</summary>
		/// <returns>Um objeto se a implementação de uma classe derivada tiver uma conexão alternativa pai com o relatório.</returns>
		// Token: 0x06000401 RID: 1025 RVA: 0x00016F8C File Offset: 0x0001638C
		protected internal virtual DependencyObject GetUIParentCore()
		{
			return null;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00016F9C File Offset: 0x0001639C
		internal DependencyObject Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00016FB0 File Offset: 0x000163B0
		[FriendAccessAllowed]
		internal virtual void OnContentParentChanged(DependencyObject oldParent)
		{
			this.SynchronizeReverseInheritPropertyFlags(oldParent, true);
		}

		/// <summary>Retorna implementações de <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> específicas à classe para a infra-estrutura de Windows Presentation Foundation (WPF).</summary>
		/// <returns>A implementação de <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> específica ao tipo.</returns>
		// Token: 0x06000404 RID: 1028 RVA: 0x00016FC8 File Offset: 0x000163C8
		protected virtual AutomationPeer OnCreateAutomationPeer()
		{
			return null;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00016FD8 File Offset: 0x000163D8
		internal AutomationPeer CreateAutomationPeer()
		{
			base.VerifyAccess();
			AutomationPeer automationPeer;
			if (this.HasAutomationPeer)
			{
				automationPeer = ContentElement.AutomationPeerField.GetValue(this);
			}
			else
			{
				automationPeer = this.OnCreateAutomationPeer();
				if (automationPeer != null)
				{
					ContentElement.AutomationPeerField.SetValue(this, automationPeer);
					this.HasAutomationPeer = true;
				}
			}
			return automationPeer;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00017024 File Offset: 0x00016424
		internal AutomationPeer GetAutomationPeer()
		{
			base.VerifyAccess();
			if (this.HasAutomationPeer)
			{
				return ContentElement.AutomationPeerField.GetValue(this);
			}
			return null;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001704C File Offset: 0x0001644C
		internal void AddSynchronizedInputPreOpportunityHandler(EventRoute route, RoutedEventArgs args)
		{
			if (InputManager.IsSynchronizedInput && SynchronizedInputHelper.IsListening(this, args))
			{
				RoutedEventHandler eventHandler = new RoutedEventHandler(this.SynchronizedInputPreOpportunityHandler);
				SynchronizedInputHelper.AddHandlerToRoute(this, route, eventHandler, false);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00017080 File Offset: 0x00016480
		internal void AddSynchronizedInputPostOpportunityHandler(EventRoute route, RoutedEventArgs args)
		{
			if (InputManager.IsSynchronizedInput)
			{
				if (SynchronizedInputHelper.IsListening(this, args))
				{
					RoutedEventHandler eventHandler = new RoutedEventHandler(this.SynchronizedInputPostOpportunityHandler);
					SynchronizedInputHelper.AddHandlerToRoute(this, route, eventHandler, true);
					return;
				}
				SynchronizedInputHelper.AddParentPreOpportunityHandler(this, route, args);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000170BC File Offset: 0x000164BC
		internal void SynchronizedInputPreOpportunityHandler(object sender, RoutedEventArgs args)
		{
			if (!args.Handled)
			{
				SynchronizedInputHelper.PreOpportunityHandler(sender, args);
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000170D8 File Offset: 0x000164D8
		internal void SynchronizedInputPostOpportunityHandler(object sender, RoutedEventArgs args)
		{
			if (args.Handled && InputManager.SynchronizedInputState == SynchronizedInputStates.HadOpportunity)
			{
				SynchronizedInputHelper.PostOpportunityHandler(sender, args);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000170FC File Offset: 0x000164FC
		internal bool StartListeningSynchronizedInput(SynchronizedInputType inputType)
		{
			if (InputManager.IsSynchronizedInput)
			{
				return false;
			}
			InputManager.StartListeningSynchronizedInput(this, inputType);
			return true;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001711C File Offset: 0x0001651C
		internal void CancelSynchronizedInput()
		{
			InputManager.CancelSynchronizedInput();
		}

		/// <summary>Obtém um valor que indica se a posição do ponteiro do mouse corresponde aos resultados de teste de clique, que levam em consideração a composição de elementos.</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro do mouse estiver sobre o mesmo resultado do elemento que um teste de clique; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00017130 File Offset: 0x00016530
		public bool IsMouseDirectlyOver
		{
			get
			{
				return this.IsMouseDirectlyOver_ComputeValue();
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00017144 File Offset: 0x00016544
		private bool IsMouseDirectlyOver_ComputeValue()
		{
			return Mouse.DirectlyOver == this;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001715C File Offset: 0x0001655C
		[FriendAccessAllowed]
		internal void SynchronizeReverseInheritPropertyFlags(DependencyObject oldParent, bool isCoreParent)
		{
			if (this.IsKeyboardFocusWithin)
			{
				Keyboard.PrimaryDevice.ReevaluateFocusAsync(this, oldParent, isCoreParent);
			}
			if (this.IsStylusOver)
			{
				StylusLogic.CurrentStylusLogicReevaluateStylusOver(this, oldParent, isCoreParent);
			}
			if (this.IsStylusCaptureWithin)
			{
				StylusLogic.CurrentStylusLogicReevaluateCapture(this, oldParent, isCoreParent);
			}
			if (this.IsMouseOver)
			{
				Mouse.PrimaryDevice.ReevaluateMouseOver(this, oldParent, isCoreParent);
			}
			if (this.IsMouseCaptureWithin)
			{
				Mouse.PrimaryDevice.ReevaluateCapture(this, oldParent, isCoreParent);
			}
			if (this.AreAnyTouchesOver)
			{
				TouchDevice.ReevaluateDirectlyOver(this, oldParent, isCoreParent);
			}
			if (this.AreAnyTouchesCapturedWithin)
			{
				TouchDevice.ReevaluateCapturedWithin(this, oldParent, isCoreParent);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000171E8 File Offset: 0x000165E8
		internal virtual bool BlockReverseInheritance()
		{
			return false;
		}

		/// <summary>Obtém um valor que indica se o ponteiro do mouse está localizado sobre esse elemento (incluindo os elementos filho visuais ou sua composição de controle).</summary>
		/// <returns>
		///   <see langword="true" /> se o ponteiro do mouse estiver sobre o elemento ou seus elementos filho; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000171F8 File Offset: 0x000165F8
		public bool IsMouseOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsMouseOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se a caneta está localizada sobre esse elemento (incluindo elementos filho visuais).</summary>
		/// <returns>
		///   <see langword="true" /> Se a caneta está sobre o elemento ou seus elementos filho; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00017210 File Offset: 0x00016610
		public bool IsStylusOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsStylusOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se o foco do teclado está em qualquer lugar dentro do elemento ou elementos filho.</summary>
		/// <returns>
		///   <see langword="true" /> se o foco do teclado está no elemento ou em seus elementos filho; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00017228 File Offset: 0x00016628
		public bool IsKeyboardFocusWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsKeyboardFocusWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se o mouse é capturado por esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver a captura do mouse; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00017240 File Offset: 0x00016640
		public bool IsMouseCaptured
		{
			get
			{
				return (bool)base.GetValue(ContentElement.IsMouseCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura do mouse para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o mouse for capturado com êxito; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000415 RID: 1045 RVA: 0x00017260 File Offset: 0x00016660
		public bool CaptureMouse()
		{
			return Mouse.Capture(this);
		}

		/// <summary>Libera a captura do mouse, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x06000416 RID: 1046 RVA: 0x00017274 File Offset: 0x00016674
		public void ReleaseMouseCapture()
		{
			Mouse.Capture(null);
		}

		/// <summary>Obtém um valor que determina se a captura do mouse é mantida por esse elemento ou elementos filho em sua árvore de elementos.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento ou um elemento contido tiver captura do mouse; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00017288 File Offset: 0x00016688
		public bool IsMouseCaptureWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsMouseCaptureWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se a posição da caneta corresponde aos resultados de teste de clique, que levam em consideração a composição dos elementos.</summary>
		/// <returns>
		///   <see langword="true" /> Se a caneta está sobre o mesmo elemento como um teste de clique; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000172A0 File Offset: 0x000166A0
		public bool IsStylusDirectlyOver
		{
			get
			{
				return this.IsStylusDirectlyOver_ComputeValue();
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000172B4 File Offset: 0x000166B4
		private bool IsStylusDirectlyOver_ComputeValue()
		{
			return Stylus.DirectlyOver == this;
		}

		/// <summary>Obtém um valor que indica se a caneta é capturada para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tem captura da caneta; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000172CC File Offset: 0x000166CC
		public bool IsStylusCaptured
		{
			get
			{
				return (bool)base.GetValue(ContentElement.IsStylusCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura da caneta para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se a caneta for capturada com êxito, caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600041B RID: 1051 RVA: 0x000172EC File Offset: 0x000166EC
		public bool CaptureStylus()
		{
			return Stylus.Capture(this);
		}

		/// <summary>Libera a captura do dispositivo de caneta, se esse elemento tiver mantido a captura.</summary>
		// Token: 0x0600041C RID: 1052 RVA: 0x00017300 File Offset: 0x00016700
		public void ReleaseStylusCapture()
		{
			Stylus.Capture(null);
		}

		/// <summary>Obtém um valor que determina se a captura da caneta é mantida por esse elemento, incluindo elementos filho e composição de controle.</summary>
		/// <returns>
		///   <see langword="true" /> Se a captura da caneta é mantida dentro desse elemento; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00017314 File Offset: 0x00016714
		public bool IsStylusCaptureWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.IsStylusCaptureWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se esse elemento tem o foco do teclado.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento tiver o foco do teclado; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001732C File Offset: 0x0001672C
		public bool IsKeyboardFocused
		{
			get
			{
				return this.IsKeyboardFocused_ComputeValue();
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00017340 File Offset: 0x00016740
		private bool IsKeyboardFocused_ComputeValue()
		{
			return Keyboard.FocusedElement == this;
		}

		/// <summary>Tenta definir o foco para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se o foco do teclado puder ser definido para esse elemento, <see langword="false" /> se a chamada desse método não forçou o foco.</returns>
		// Token: 0x06000420 RID: 1056 RVA: 0x00017358 File Offset: 0x00016758
		public bool Focus()
		{
			if (Keyboard.Focus(this) == this)
			{
				TipTsfHelper.Show(this);
				return true;
			}
			return false;
		}

		/// <summary>Tenta mover o foco deste para outro elemento. A direção para mover o foco é especificada por uma direção de diretrizes, que é interpretada dentro da organização do pai visual deste elemento.</summary>
		/// <param name="request">Uma solicitação de passagem, que contém uma propriedade que indica um modo para percorrer uma ordem de tabulação existente ou uma direção de movimentação visualmente.</param>
		/// <returns>
		///   <see langword="true" /> se a passagem solicitada foi executada; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000421 RID: 1057 RVA: 0x00017378 File Offset: 0x00016778
		public virtual bool MoveFocus(TraversalRequest request)
		{
			return false;
		}

		/// <summary>Quando substituído em uma classe derivada, retorna o elemento que deve receber o foco para uma direção de passagem do foco especificada, sem realmente mover o foco para esse elemento.</summary>
		/// <param name="direction">A direção da passagem do foco solicitada.</param>
		/// <returns>O elemento que teria recebido foco, se <see cref="M:System.Windows.ContentElement.MoveFocus(System.Windows.Input.TraversalRequest)" /> realmente fosse invocado.</returns>
		// Token: 0x06000422 RID: 1058 RVA: 0x00017388 File Offset: 0x00016788
		public virtual DependencyObject PredictFocus(FocusNavigationDirection direction)
		{
			return null;
		}

		/// <summary>Ocorre quando este elemento tem foco lógico.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000423 RID: 1059 RVA: 0x00017398 File Offset: 0x00016798
		// (remove) Token: 0x06000424 RID: 1060 RVA: 0x000173B4 File Offset: 0x000167B4
		public event RoutedEventHandler GotFocus
		{
			add
			{
				this.AddHandler(ContentElement.GotFocusEvent, value);
			}
			remove
			{
				this.RemoveHandler(ContentElement.GotFocusEvent, value);
			}
		}

		/// <summary>Ocorre quando este elemento perde o foco lógico.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000425 RID: 1061 RVA: 0x000173D0 File Offset: 0x000167D0
		// (remove) Token: 0x06000426 RID: 1062 RVA: 0x000173EC File Offset: 0x000167EC
		public event RoutedEventHandler LostFocus
		{
			add
			{
				this.AddHandler(ContentElement.LostFocusEvent, value);
			}
			remove
			{
				this.RemoveHandler(ContentElement.LostFocusEvent, value);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00017408 File Offset: 0x00016808
		private static void IsFocused_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ContentElement contentElement = (ContentElement)d;
			if ((bool)e.NewValue)
			{
				contentElement.OnGotFocus(new RoutedEventArgs(ContentElement.GotFocusEvent, contentElement));
				return;
			}
			contentElement.OnLostFocus(new RoutedEventArgs(ContentElement.LostFocusEvent, contentElement));
		}

		/// <summary>Gera o evento roteado <see cref="E:System.Windows.ContentElement.GotFocus" /> usando os dados de evento fornecidos.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém dados do evento. Esses dados de evento devem conter o identificador para o evento <see cref="E:System.Windows.ContentElement.GotFocus" />.</param>
		// Token: 0x06000428 RID: 1064 RVA: 0x00017450 File Offset: 0x00016850
		protected virtual void OnGotFocus(RoutedEventArgs e)
		{
			this.RaiseEvent(e);
		}

		/// <summary>Gera o evento roteado <see cref="E:System.Windows.ContentElement.LostFocus" /> usando os dados de evento fornecidos.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém dados do evento. Esses dados de evento devem conter o identificador para o evento <see cref="E:System.Windows.ContentElement.LostFocus" />.</param>
		// Token: 0x06000429 RID: 1065 RVA: 0x00017464 File Offset: 0x00016864
		protected virtual void OnLostFocus(RoutedEventArgs e)
		{
			this.RaiseEvent(e);
		}

		/// <summary>Obtém um valor que determina se esse elemento tem foco lógico.</summary>
		/// <returns>
		///   <see langword="true" /> se esse elemento tiver o foco lógico; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00017478 File Offset: 0x00016878
		public bool IsFocused
		{
			get
			{
				return (bool)base.GetValue(ContentElement.IsFocusedProperty);
			}
		}

		/// <summary>Obtém ou define um valor que indica se esse elemento está habilitado no UI (interface do usuário).</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver habilitado; caso contrário, <see langword="false" />. O valor padrão é <see langword="true" />.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00017498 File Offset: 0x00016898
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x000174B8 File Offset: 0x000168B8
		public bool IsEnabled
		{
			get
			{
				return (bool)base.GetValue(ContentElement.IsEnabledProperty);
			}
			set
			{
				base.SetValue(ContentElement.IsEnabledProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.IsEnabled" /> neste elemento é alterado.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600042D RID: 1069 RVA: 0x000174D8 File Offset: 0x000168D8
		// (remove) Token: 0x0600042E RID: 1070 RVA: 0x000174F4 File Offset: 0x000168F4
		public event DependencyPropertyChangedEventHandler IsEnabledChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsEnabledChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsEnabledChangedKey, value);
			}
		}

		/// <summary>Obtém um valor que se torna o valor retornado de <see cref="P:System.Windows.ContentElement.IsEnabled" /> em classes derivadas.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento estiver habilitado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00017510 File Offset: 0x00016910
		protected virtual bool IsEnabledCore
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00017520 File Offset: 0x00016920
		private static object CoerceIsEnabled(DependencyObject d, object value)
		{
			ContentElement contentElement = (ContentElement)d;
			if (!(bool)value)
			{
				return BooleanBoxes.FalseBox;
			}
			DependencyObject uiparentCore = contentElement.GetUIParentCore();
			if (uiparentCore == null || (bool)uiparentCore.GetValue(ContentElement.IsEnabledProperty))
			{
				return BooleanBoxes.Box(contentElement.IsEnabledCore);
			}
			return BooleanBoxes.FalseBox;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00017570 File Offset: 0x00016970
		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ContentElement contentElement = (ContentElement)d;
			contentElement.RaiseDependencyPropertyChanged(UIElement.IsEnabledChangedKey, e);
			contentElement.InvalidateForceInheritPropertyOnChildren(e.Property);
			InputManager.SafeCurrentNotifyHitTestInvalidated();
		}

		/// <summary>Obtém ou define um valor que indica se um elemento pode receber foco.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for focalizável; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000175A4 File Offset: 0x000169A4
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x000175C4 File Offset: 0x000169C4
		public bool Focusable
		{
			get
			{
				return (bool)base.GetValue(ContentElement.FocusableProperty);
			}
			set
			{
				base.SetValue(ContentElement.FocusableProperty, BooleanBoxes.Box(value));
			}
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.Focusable" /> muda.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000434 RID: 1076 RVA: 0x000175E4 File Offset: 0x000169E4
		// (remove) Token: 0x06000435 RID: 1077 RVA: 0x00017600 File Offset: 0x00016A00
		public event DependencyPropertyChangedEventHandler FocusableChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.FocusableChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.FocusableChangedKey, value);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001761C File Offset: 0x00016A1C
		private static void OnFocusableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ContentElement contentElement = (ContentElement)d;
			contentElement.RaiseDependencyPropertyChanged(UIElement.FocusableChangedKey, e);
		}

		/// <summary>Obtém um valor que indica se um sistema de método de entrada, como um Input Method Editor (IME), está habilitado para processamento de entrada para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se um método de entrada estiver ativo; caso contrário, <see langword="false" />. O valor padrão da propriedade anexada subjacente é <see langword="true" />; no entanto, esse valor é influenciado pelo estado de métodos de entrada em tempo de execução.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001763C File Offset: 0x00016A3C
		public bool IsInputMethodEnabled
		{
			get
			{
				return (bool)base.GetValue(InputMethod.IsInputMethodEnabledProperty);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001765C File Offset: 0x00016A5C
		private void RaiseMouseButtonEvent(EventPrivateKey key, MouseButtonEventArgs e)
		{
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				Delegate @delegate = eventHandlersStore.Get(key);
				if (@delegate != null)
				{
					((MouseButtonEventHandler)@delegate)(this, e);
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001768C File Offset: 0x00016A8C
		private void RaiseDependencyPropertyChanged(EventPrivateKey key, DependencyPropertyChangedEventArgs args)
		{
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				Delegate @delegate = eventHandlersStore.Get(key);
				if (@delegate != null)
				{
					((DependencyPropertyChangedEventHandler)@delegate)(this, args);
				}
			}
		}

		/// <summary>Obtém ou define um valor que indica se um elemento pode ser usado como o destino de uma operação do tipo "arrastar e soltar".</summary>
		/// <returns>
		///   <see langword="true" /> se um elemento pode ser usado como o destino de uma operação do tipo "arrastar e soltar"; caso contrário, <see langword="false" />. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x000176BC File Offset: 0x00016ABC
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x000176DC File Offset: 0x00016ADC
		public bool AllowDrop
		{
			get
			{
				return (bool)base.GetValue(ContentElement.AllowDropProperty);
			}
			set
			{
				base.SetValue(ContentElement.AllowDropProperty, BooleanBoxes.Box(value));
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000176FC File Offset: 0x00016AFC
		internal virtual void InvalidateForceInheritPropertyOnChildren(DependencyProperty property)
		{
		}

		/// <summary>Obtém um valor que indica se pelo menos um toque for pressionado sobre esse elemento ou elementos filho na sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> se pelo menos um toque for pressionado sobre esse elemento ou elementos filho na sua árvore visual; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001770C File Offset: 0x00016B0C
		public bool AreAnyTouchesOver
		{
			get
			{
				return this.ReadFlag(CoreFlags.TouchesOverCache);
			}
		}

		/// <summary>Obtém um valor que indica se pelo menos um toque é feito sobre esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se pelo menos um toque for pressionado sobre esse elemento; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00017724 File Offset: 0x00016B24
		public bool AreAnyTouchesDirectlyOver
		{
			get
			{
				return (bool)base.GetValue(ContentElement.AreAnyTouchesDirectlyOverProperty);
			}
		}

		/// <summary>Obtém um valor que indica se ao menos um toque é capturado nesse elemento ou elementos filho na sua árvore visual.</summary>
		/// <returns>
		///   <see langword="true" /> Se pelo menos um toque é capturado para esse elemento ou elementos filho na árvore visual; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00017744 File Offset: 0x00016B44
		public bool AreAnyTouchesCapturedWithin
		{
			get
			{
				return this.ReadFlag(CoreFlags.TouchesCapturedWithinCache);
			}
		}

		/// <summary>Obtém um valor que indica se pelo menos um toque é capturado para esse elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se pelo menos um toque for capturado para esse elemento; caso contrário, <see langword="false" />.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0001775C File Offset: 0x00016B5C
		public bool AreAnyTouchesCaptured
		{
			get
			{
				return (bool)base.GetValue(ContentElement.AreAnyTouchesCapturedProperty);
			}
		}

		/// <summary>Tenta forçar a captura de um toque para esse elemento.</summary>
		/// <param name="touchDevice">O dispositivo a ser capturado.</param>
		/// <returns>
		///   <see langword="true" /> se o toque especificado for capturado para esse elemento; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="touchDevice" /> é <see langword="null" />.</exception>
		// Token: 0x06000441 RID: 1089 RVA: 0x0001777C File Offset: 0x00016B7C
		public bool CaptureTouch(TouchDevice touchDevice)
		{
			if (touchDevice == null)
			{
				throw new ArgumentNullException("touchDevice");
			}
			return touchDevice.Capture(this);
		}

		/// <summary>Tenta liberar o dispositivo de toque especificado desse elemento.</summary>
		/// <param name="touchDevice">O dispositivo a ser liberado.</param>
		/// <returns>
		///   <see langword="true" /> se o dispositivo de toque estiver liberado; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="touchDevice" /> é <see langword="null" />.</exception>
		// Token: 0x06000442 RID: 1090 RVA: 0x000177A0 File Offset: 0x00016BA0
		public bool ReleaseTouchCapture(TouchDevice touchDevice)
		{
			if (touchDevice == null)
			{
				throw new ArgumentNullException("touchDevice");
			}
			if (touchDevice.Captured == this)
			{
				touchDevice.Capture(null);
				return true;
			}
			return false;
		}

		/// <summary>Libera todos os dispositivos de toque capturados desse elemento.</summary>
		// Token: 0x06000443 RID: 1091 RVA: 0x000177D0 File Offset: 0x00016BD0
		public void ReleaseAllTouchCaptures()
		{
			TouchDevice.ReleaseAllCaptures(this);
		}

		/// <summary>Obtém todos os dispositivos de toque capturados para esse elemento.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> capturados para esse elemento.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000177E4 File Offset: 0x00016BE4
		public IEnumerable<TouchDevice> TouchesCaptured
		{
			get
			{
				return TouchDevice.GetCapturedTouches(this, false);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque que são capturados para esse elemento ou os elementos filho na árvore visual.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> que são capturados para esse elemento ou elementos filho na árvore visual.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000177F8 File Offset: 0x00016BF8
		public IEnumerable<TouchDevice> TouchesCapturedWithin
		{
			get
			{
				return TouchDevice.GetCapturedTouches(this, true);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque que estão sobre esse elemento ou sobre os elementos filho na árvore visual.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> que estão acima desse elemento ou dos elementos filho na árvore visual.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001780C File Offset: 0x00016C0C
		public IEnumerable<TouchDevice> TouchesOver
		{
			get
			{
				return TouchDevice.GetTouchesOver(this, true);
			}
		}

		/// <summary>Obtém todos os dispositivos de toque nesse elemento.</summary>
		/// <returns>Uma enumeração de objetos <see cref="T:System.Windows.Input.TouchDevice" /> nesse elemento.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00017820 File Offset: 0x00016C20
		public IEnumerable<TouchDevice> TouchesDirectlyOver
		{
			get
			{
				return TouchDevice.GetTouchesOver(this, false);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00017834 File Offset: 0x00016C34
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001784C File Offset: 0x00016C4C
		internal bool HasAutomationPeer
		{
			get
			{
				return this.ReadFlag(CoreFlags.HasAutomationPeer);
			}
			set
			{
				this.WriteFlag(CoreFlags.HasAutomationPeer, value);
			}
		}

		/// <summary>Aplica uma animação a uma propriedade de dependência especificada neste elemento. Todas as animações existentes são interrompidas e substituídas pela nova animação.</summary>
		/// <param name="dp">O identificador para a propriedade a ser animada.</param>
		/// <param name="clock">O relógio de animação que controla e declara a animação.</param>
		// Token: 0x0600044A RID: 1098 RVA: 0x00017868 File Offset: 0x00016C68
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock)
		{
			this.ApplyAnimationClock(dp, clock, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Aplica uma animação a uma propriedade de dependência especificada nesse elemento, com a capacidade de especificar o que ocorrerá se a propriedade já tiver uma animação em execução.</summary>
		/// <param name="dp">A propriedade a ser animada.</param>
		/// <param name="clock">O relógio de animação que controla e declara a animação.</param>
		/// <param name="handoffBehavior">Um valor da enumeração. O padrão é <see cref="F:System.Windows.Media.Animation.HandoffBehavior.SnapshotAndReplace" />, que interromperá a animação existente, substituindo-a pela nova.</param>
		// Token: 0x0600044B RID: 1099 RVA: 0x00017880 File Offset: 0x00016C80
		public void ApplyAnimationClock(DependencyProperty dp, AnimationClock clock, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (clock != null && !AnimationStorage.IsAnimationValid(dp, clock.Timeline))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					clock.Timeline.GetType(),
					dp.Name,
					dp.PropertyType
				}), "clock");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.ApplyAnimationClock(this, dp, clock, handoffBehavior);
		}

		/// <summary>Inicia uma animação de uma propriedade animada especificada neste elemento.</summary>
		/// <param name="dp">A propriedade a ser animada, que é especificada como um identificador da propriedade de dependência.</param>
		/// <param name="animation">A linha do tempo da animação a ser iniciada.</param>
		// Token: 0x0600044C RID: 1100 RVA: 0x0001796C File Offset: 0x00016D6C
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation)
		{
			this.BeginAnimation(dp, animation, HandoffBehavior.SnapshotAndReplace);
		}

		/// <summary>Inicia uma animação específica para uma propriedade animada especificada neste elemento, com a opção de especificar o que acontece se a propriedade já tiver uma animação em execução.</summary>
		/// <param name="dp">A propriedade a ser animada, que é especificada como o identificador da propriedade de dependência.</param>
		/// <param name="animation">A linha do tempo da animação a ser aplicada.</param>
		/// <param name="handoffBehavior">Um valor de enumeração que especifica como a nova animação interage com todas as animações atuais (em execução) que já estão afetando o valor da propriedade.</param>
		// Token: 0x0600044D RID: 1101 RVA: 0x00017984 File Offset: 0x00016D84
		public void BeginAnimation(DependencyProperty dp, AnimationTimeline animation, HandoffBehavior handoffBehavior)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (!AnimationStorage.IsPropertyAnimatable(this, dp))
			{
				throw new ArgumentException(SR.Get("Animation_DependencyPropertyIsNotAnimatable", new object[]
				{
					dp.Name,
					base.GetType()
				}), "dp");
			}
			if (animation != null && !AnimationStorage.IsAnimationValid(dp, animation))
			{
				throw new ArgumentException(SR.Get("Animation_AnimationTimelineTypeMismatch", new object[]
				{
					animation.GetType(),
					dp.Name,
					dp.PropertyType
				}), "animation");
			}
			if (!HandoffBehaviorEnum.IsDefined(handoffBehavior))
			{
				throw new ArgumentException(SR.Get("Animation_UnrecognizedHandoffBehavior"));
			}
			if (base.IsSealed)
			{
				throw new InvalidOperationException(SR.Get("IAnimatable_CantAnimateSealedDO", new object[]
				{
					dp,
					base.GetType()
				}));
			}
			AnimationStorage.BeginAnimation(this, dp, animation, handoffBehavior);
		}

		/// <summary>Obtém um valor que indica se esse elemento tem propriedades animadas.</summary>
		/// <returns>
		///   <see langword="true" /> se este elemento tem animações anexadas a uma de suas propriedades; caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00017A64 File Offset: 0x00016E64
		public bool HasAnimatedProperties
		{
			get
			{
				base.VerifyAccess();
				return base.IAnimatable_HasAnimatedProperties;
			}
		}

		/// <summary>Retorna o valor da propriedade base da propriedade especificada neste elemento, desconsiderando qualquer possível valor animado de uma animação parada ou em execução.</summary>
		/// <param name="dp">A propriedade de dependência a ser verificada.</param>
		/// <returns>O valor da propriedade como se não houvesse nenhuma animação anexada à propriedade de dependência especificada.</returns>
		// Token: 0x0600044F RID: 1103 RVA: 0x00017A80 File Offset: 0x00016E80
		public object GetAnimationBaseValue(DependencyProperty dp)
		{
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			return base.GetValueEntry(base.LookupEntry(dp.GlobalIndex), dp, null, RequestFlags.AnimationBaseValue).Value;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00017AB8 File Offset: 0x00016EB8
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		internal sealed override void EvaluateAnimatedValueCore(DependencyProperty dp, PropertyMetadata metadata, ref EffectiveValueEntry entry)
		{
			if (base.IAnimatable_HasAnimatedProperties)
			{
				AnimationStorage storage = AnimationStorage.GetStorage(this, dp);
				if (storage != null)
				{
					storage.EvaluateAnimatedValue(metadata, ref entry);
				}
			}
		}

		/// <summary>Obtém a coleção de associações de entrada associadas a este elemento.</summary>
		/// <returns>A coleção de ligações de entrada.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00017AE0 File Offset: 0x00016EE0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public InputBindingCollection InputBindings
		{
			get
			{
				base.VerifyAccess();
				InputBindingCollection inputBindingCollection = ContentElement.InputBindingCollectionField.GetValue(this);
				if (inputBindingCollection == null)
				{
					inputBindingCollection = new InputBindingCollection(this);
					ContentElement.InputBindingCollectionField.SetValue(this, inputBindingCollection);
				}
				return inputBindingCollection;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00017B18 File Offset: 0x00016F18
		internal InputBindingCollection InputBindingsInternal
		{
			get
			{
				base.VerifyAccess();
				return ContentElement.InputBindingCollectionField.GetValue(this);
			}
		}

		/// <summary>Indica se os processos de serialização devem serializar o conteúdo da propriedade <see cref="P:System.Windows.ContentElement.InputBindings" /> em instâncias dessa classe.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade <see cref="P:System.Windows.ContentElement.InputBindings" /> precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000453 RID: 1107 RVA: 0x00017B38 File Offset: 0x00016F38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeInputBindings()
		{
			InputBindingCollection value = ContentElement.InputBindingCollectionField.GetValue(this);
			return value != null && value.Count > 0;
		}

		/// <summary>Obtém uma coleção de objetos <see cref="T:System.Windows.Input.CommandBinding" /> associados a esse elemento.</summary>
		/// <returns>A coleção de todos os objetos <see cref="T:System.Windows.Input.CommandBinding" />.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00017B60 File Offset: 0x00016F60
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CommandBindingCollection CommandBindings
		{
			get
			{
				base.VerifyAccess();
				CommandBindingCollection commandBindingCollection = ContentElement.CommandBindingCollectionField.GetValue(this);
				if (commandBindingCollection == null)
				{
					commandBindingCollection = new CommandBindingCollection();
					ContentElement.CommandBindingCollectionField.SetValue(this, commandBindingCollection);
				}
				return commandBindingCollection;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00017B98 File Offset: 0x00016F98
		internal CommandBindingCollection CommandBindingsInternal
		{
			get
			{
				base.VerifyAccess();
				return ContentElement.CommandBindingCollectionField.GetValue(this);
			}
		}

		/// <summary>Indica se os processos de serialização devem serializar o conteúdo da propriedade <see cref="P:System.Windows.ContentElement.CommandBindings" /> em instâncias dessa classe.</summary>
		/// <returns>
		///   <see langword="true" /> se o valor da propriedade <see cref="P:System.Windows.ContentElement.CommandBindings" /> precisar ser serializado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06000456 RID: 1110 RVA: 0x00017BB8 File Offset: 0x00016FB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeCommandBindings()
		{
			CommandBindingCollection value = ContentElement.CommandBindingCollectionField.GetValue(this);
			return value != null && value.Count > 0;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00017BE0 File Offset: 0x00016FE0
		internal virtual bool BuildRouteCore(EventRoute route, RoutedEventArgs args)
		{
			return false;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00017BF0 File Offset: 0x00016FF0
		internal void BuildRoute(EventRoute route, RoutedEventArgs args)
		{
			UIElement.BuildRouteHelper(this, route, args);
		}

		/// <summary>Aciona um evento roteado específico. O <see cref="T:System.Windows.RoutedEvent" /> a ser gerado é identificado na instância <see cref="T:System.Windows.RoutedEventArgs" /> fornecida (como a propriedade <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> desses dados de eventos).</summary>
		/// <param name="e">Um <see cref="T:System.Windows.RoutedEventArgs" /> que contém os dados do evento e também identifica o evento a ser acionado.</param>
		// Token: 0x06000459 RID: 1113 RVA: 0x00017C08 File Offset: 0x00017008
		public void RaiseEvent(RoutedEventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			e.ClearUserInitiated();
			UIElement.RaiseEventImpl(this, e);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00017C30 File Offset: 0x00017030
		[SecurityCritical]
		internal void RaiseEvent(RoutedEventArgs args, bool trusted)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (trusted)
			{
				this.RaiseTrustedEvent(args);
				return;
			}
			args.ClearUserInitiated();
			UIElement.RaiseEventImpl(this, args);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00017C64 File Offset: 0x00017064
		[SecurityCritical]
		[UserInitiatedRoutedEventPermission(SecurityAction.Assert)]
		internal void RaiseTrustedEvent(RoutedEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			args.MarkAsUserInitiated();
			try
			{
				UIElement.RaiseEventImpl(this, args);
			}
			finally
			{
				args.ClearUserInitiated();
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00017CB4 File Offset: 0x000170B4
		internal virtual object AdjustEventSource(RoutedEventArgs args)
		{
			return null;
		}

		/// <summary>Adiciona um manipulador de eventos roteados de um evento roteado especificado, adicionando o manipulador à coleção de manipuladores no elemento atual.</summary>
		/// <param name="routedEvent">Um identificador do evento roteado a ser manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador.</param>
		// Token: 0x0600045D RID: 1117 RVA: 0x00017CC4 File Offset: 0x000170C4
		public void AddHandler(RoutedEvent routedEvent, Delegate handler)
		{
			this.AddHandler(routedEvent, handler, false);
		}

		/// <summary>Adiciona um manipulador de eventos roteados de um evento roteado especificado, adicionando o manipulador à coleção de manipuladores no elemento atual. Especifique <paramref name="handledEventsToo" /> como <see langword="true" /> para que o manipulador fornecido seja invocado para eventos roteados que já tenham sido marcados como manipulados por outro elemento na rota de evento.</summary>
		/// <param name="routedEvent">Um identificador do evento roteado a ser manipulado.</param>
		/// <param name="handler">Uma referência à implementação do manipulador.</param>
		/// <param name="handledEventsToo">
		///   <see langword="true" /> para registrar o manipulador de modo que ele seja invocado mesmo quando o evento roteado estiver marcado como tratado nos dados do evento; <see langword="false" /> para registrar o manipulador com a condição padrão que não será chamado se o evento roteado já estiver marcado como tratado.  
		/// O padrão é <see langword="false" />.  
		/// Não solicite sempre para tratar novamente um evento roteado.</param>
		// Token: 0x0600045E RID: 1118 RVA: 0x00017CDC File Offset: 0x000170DC
		public void AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!routedEvent.IsLegalHandler(handler))
			{
				throw new ArgumentException(SR.Get("HandlerTypeIllegal"));
			}
			this.EnsureEventHandlersStore();
			this.EventHandlersStore.AddRoutedEventHandler(routedEvent, handler, handledEventsToo);
			this.OnAddHandler(routedEvent, handler);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00017D3C File Offset: 0x0001713C
		internal virtual void OnAddHandler(RoutedEvent routedEvent, Delegate handler)
		{
		}

		/// <summary>Remove o manipulador de eventos roteados especificado desse elemento.</summary>
		/// <param name="routedEvent">O identificador do evento roteado ao qual o manipulador está anexado.</param>
		/// <param name="handler">A implementação do manipulador específico para remover da coleção de manipuladores de eventos neste elemento.</param>
		// Token: 0x06000460 RID: 1120 RVA: 0x00017D4C File Offset: 0x0001714C
		public void RemoveHandler(RoutedEvent routedEvent, Delegate handler)
		{
			if (routedEvent == null)
			{
				throw new ArgumentNullException("routedEvent");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (!routedEvent.IsLegalHandler(handler))
			{
				throw new ArgumentException(SR.Get("HandlerTypeIllegal"));
			}
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				eventHandlersStore.RemoveRoutedEventHandler(routedEvent, handler);
				this.OnRemoveHandler(routedEvent, handler);
				if (eventHandlersStore.Count == 0)
				{
					ContentElement.EventHandlersStoreField.ClearValue(this);
					this.WriteFlag(CoreFlags.ExistsEventHandlersStore, false);
				}
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00017DC8 File Offset: 0x000171C8
		internal virtual void OnRemoveHandler(RoutedEvent routedEvent, Delegate handler)
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00017DD8 File Offset: 0x000171D8
		private void EventHandlersStoreAdd(EventPrivateKey key, Delegate handler)
		{
			this.EnsureEventHandlersStore();
			this.EventHandlersStore.Add(key, handler);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00017DF8 File Offset: 0x000171F8
		private void EventHandlersStoreRemove(EventPrivateKey key, Delegate handler)
		{
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				eventHandlersStore.Remove(key, handler);
				if (eventHandlersStore.Count == 0)
				{
					ContentElement.EventHandlersStoreField.ClearValue(this);
					this.WriteFlag(CoreFlags.ExistsEventHandlersStore, false);
				}
			}
		}

		/// <summary>Adiciona manipuladores ao <see cref="T:System.Windows.EventRoute" /> especificado para a coleção do manipulador de eventos <see cref="T:System.Windows.ContentElement" /> atual.</summary>
		/// <param name="route">A rota de eventos à qual os manipuladores são adicionados.</param>
		/// <param name="e">Os dados de evento usados para adicionar os manipuladores. Esse método usa a propriedade <see cref="P:System.Windows.RoutedEventArgs.RoutedEvent" /> dos argumentos para criar os manipuladores.</param>
		// Token: 0x06000464 RID: 1124 RVA: 0x00017E38 File Offset: 0x00017238
		public void AddToEventRoute(EventRoute route, RoutedEventArgs e)
		{
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			for (RoutedEventHandlerInfoList routedEventHandlerInfoList = GlobalEventManager.GetDTypedClassListeners(base.DependencyObjectType, e.RoutedEvent); routedEventHandlerInfoList != null; routedEventHandlerInfoList = routedEventHandlerInfoList.Next)
			{
				for (int i = 0; i < routedEventHandlerInfoList.Handlers.Length; i++)
				{
					route.Add(this, routedEventHandlerInfoList.Handlers[i].Handler, routedEventHandlerInfoList.Handlers[i].InvokeHandledEventsToo);
				}
			}
			EventHandlersStore eventHandlersStore = this.EventHandlersStore;
			if (eventHandlersStore != null)
			{
				FrugalObjectList<RoutedEventHandlerInfo> frugalObjectList = eventHandlersStore[e.RoutedEvent];
				if (frugalObjectList != null)
				{
					for (int j = 0; j < frugalObjectList.Count; j++)
					{
						route.Add(this, frugalObjectList[j].Handler, frugalObjectList[j].InvokeHandledEventsToo);
					}
				}
			}
			this.AddToEventRouteCore(route, e);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00017F1C File Offset: 0x0001731C
		internal virtual void AddToEventRouteCore(EventRoute route, RoutedEventArgs args)
		{
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00017F2C File Offset: 0x0001732C
		internal EventHandlersStore EventHandlersStore
		{
			[FriendAccessAllowed]
			get
			{
				if (!this.ReadFlag(CoreFlags.ExistsEventHandlersStore))
				{
					return null;
				}
				return ContentElement.EventHandlersStoreField.GetValue(this);
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00017F54 File Offset: 0x00017354
		[FriendAccessAllowed]
		internal void EnsureEventHandlersStore()
		{
			if (this.EventHandlersStore == null)
			{
				ContentElement.EventHandlersStoreField.SetValue(this, new EventHandlersStore());
				this.WriteFlag(CoreFlags.ExistsEventHandlersStore, true);
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00017F88 File Offset: 0x00017388
		internal virtual bool InvalidateAutomationAncestorsCore(Stack<DependencyObject> branchNodeStack, out bool continuePastVisualTree)
		{
			continuePastVisualTree = false;
			return true;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00017F9C File Offset: 0x0001739C
		private static void RegisterProperties()
		{
			UIElement.IsMouseDirectlyOverPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ContentElement.IsMouseDirectlyOver_Changed)));
			UIElement.IsMouseOverPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsStylusOverPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsKeyboardFocusWithinPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsMouseCapturedPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ContentElement.IsMouseCaptured_Changed)));
			UIElement.IsMouseCaptureWithinPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsStylusDirectlyOverPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ContentElement.IsStylusDirectlyOver_Changed)));
			UIElement.IsStylusCapturedPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ContentElement.IsStylusCaptured_Changed)));
			UIElement.IsStylusCaptureWithinPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.IsKeyboardFocusedPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox, new PropertyChangedCallback(ContentElement.IsKeyboardFocused_Changed)));
			UIElement.AreAnyTouchesDirectlyOverPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.AreAnyTouchesOverPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.AreAnyTouchesCapturedPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
			UIElement.AreAnyTouchesCapturedWithinPropertyKey.OverrideMetadata(ContentElement._typeofThis, new PropertyMetadata(BooleanBoxes.FalseBox));
		}

		/// <summary>Ocorre quando qualquer botão do mouse é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600046A RID: 1130 RVA: 0x00018144 File Offset: 0x00017544
		// (remove) Token: 0x0600046B RID: 1131 RVA: 0x00018160 File Offset: 0x00017560
		public event MouseButtonEventHandler PreviewMouseDown
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados de evento relatam que um ou mais botões do mouse foram pressionados.</param>
		// Token: 0x0600046C RID: 1132 RVA: 0x0001817C File Offset: 0x0001757C
		protected internal virtual void OnPreviewMouseDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600046D RID: 1133 RVA: 0x0001818C File Offset: 0x0001758C
		// (remove) Token: 0x0600046E RID: 1134 RVA: 0x000181A8 File Offset: 0x000175A8
		public event MouseButtonEventHandler MouseDown
		{
			add
			{
				this.AddHandler(Mouse.MouseDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Esses dados de evento relatam detalhes sobre o botão do mouse que foi pressionado e o estado tratado.</param>
		// Token: 0x0600046F RID: 1135 RVA: 0x000181C4 File Offset: 0x000175C4
		protected internal virtual void OnMouseDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000470 RID: 1136 RVA: 0x000181D4 File Offset: 0x000175D4
		// (remove) Token: 0x06000471 RID: 1137 RVA: 0x000181F0 File Offset: 0x000175F0
		public event MouseButtonEventHandler PreviewMouseUp
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados de evento informam que um ou mais botões do mouse foram soltos.</param>
		// Token: 0x06000472 RID: 1138 RVA: 0x0001820C File Offset: 0x0001760C
		protected internal virtual void OnPreviewMouseUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando qualquer botão do mouse é liberado sobre este elemento.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000473 RID: 1139 RVA: 0x0001821C File Offset: 0x0001761C
		// (remove) Token: 0x06000474 RID: 1140 RVA: 0x00018238 File Offset: 0x00017638
		public event MouseButtonEventHandler MouseUp
		{
			add
			{
				this.AddHandler(Mouse.MouseUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.Input.Mouse.MouseUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão do mouse foi liberado.</param>
		// Token: 0x06000475 RID: 1141 RVA: 0x00018254 File Offset: 0x00017654
		protected internal virtual void OnMouseUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000476 RID: 1142 RVA: 0x00018264 File Offset: 0x00017664
		// (remove) Token: 0x06000477 RID: 1143 RVA: 0x00018280 File Offset: 0x00017680
		public event MouseButtonEventHandler PreviewMouseLeftButtonDown
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.PreviewMouseLeftButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo do mouse foi pressionado.</param>
		// Token: 0x06000478 RID: 1144 RVA: 0x0001829C File Offset: 0x0001769C
		protected internal virtual void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000479 RID: 1145 RVA: 0x000182AC File Offset: 0x000176AC
		// (remove) Token: 0x0600047A RID: 1146 RVA: 0x000182C8 File Offset: 0x000176C8
		public event MouseButtonEventHandler MouseLeftButtonDown
		{
			add
			{
				this.AddHandler(UIElement.MouseLeftButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseLeftButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.MouseLeftButtonDown" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo do mouse foi pressionado.</param>
		// Token: 0x0600047B RID: 1147 RVA: 0x000182E4 File Offset: 0x000176E4
		protected internal virtual void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600047C RID: 1148 RVA: 0x000182F4 File Offset: 0x000176F4
		// (remove) Token: 0x0600047D RID: 1149 RVA: 0x00018310 File Offset: 0x00017710
		public event MouseButtonEventHandler PreviewMouseLeftButtonUp
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseLeftButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseLeftButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.PreviewMouseLeftButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo foi liberado.</param>
		// Token: 0x0600047E RID: 1150 RVA: 0x0001832C File Offset: 0x0001772C
		protected internal virtual void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão esquerdo do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600047F RID: 1151 RVA: 0x0001833C File Offset: 0x0001773C
		// (remove) Token: 0x06000480 RID: 1152 RVA: 0x00018358 File Offset: 0x00017758
		public event MouseButtonEventHandler MouseLeftButtonUp
		{
			add
			{
				this.AddHandler(UIElement.MouseLeftButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseLeftButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.MouseLeftButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão esquerdo foi liberado.</param>
		// Token: 0x06000481 RID: 1153 RVA: 0x00018374 File Offset: 0x00017774
		protected internal virtual void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000482 RID: 1154 RVA: 0x00018384 File Offset: 0x00017784
		// (remove) Token: 0x06000483 RID: 1155 RVA: 0x000183A0 File Offset: 0x000177A0
		public event MouseButtonEventHandler PreviewMouseRightButtonDown
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseRightButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseRightButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.PreviewMouseRightButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi pressionado.</param>
		// Token: 0x06000484 RID: 1156 RVA: 0x000183BC File Offset: 0x000177BC
		protected internal virtual void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é pressionado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000485 RID: 1157 RVA: 0x000183CC File Offset: 0x000177CC
		// (remove) Token: 0x06000486 RID: 1158 RVA: 0x000183E8 File Offset: 0x000177E8
		public event MouseButtonEventHandler MouseRightButtonDown
		{
			add
			{
				this.AddHandler(UIElement.MouseRightButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseRightButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.MouseRightButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi pressionado.</param>
		// Token: 0x06000487 RID: 1159 RVA: 0x00018404 File Offset: 0x00017804
		protected internal virtual void OnMouseRightButtonDown(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000488 RID: 1160 RVA: 0x00018414 File Offset: 0x00017814
		// (remove) Token: 0x06000489 RID: 1161 RVA: 0x00018430 File Offset: 0x00017830
		public event MouseButtonEventHandler PreviewMouseRightButtonUp
		{
			add
			{
				this.AddHandler(UIElement.PreviewMouseRightButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.PreviewMouseRightButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.PreviewMouseRightButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi liberado.</param>
		// Token: 0x0600048A RID: 1162 RVA: 0x0001844C File Offset: 0x0001784C
		protected internal virtual void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão direito do mouse é liberado enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600048B RID: 1163 RVA: 0x0001845C File Offset: 0x0001785C
		// (remove) Token: 0x0600048C RID: 1164 RVA: 0x00018478 File Offset: 0x00017878
		public event MouseButtonEventHandler MouseRightButtonUp
		{
			add
			{
				this.AddHandler(UIElement.MouseRightButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(UIElement.MouseRightButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento roteado <see cref="E:System.Windows.ContentElement.MouseRightButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> que contém os dados do evento. Os dados do evento relatam que o botão direito do mouse foi liberado.</param>
		// Token: 0x0600048D RID: 1165 RVA: 0x00018494 File Offset: 0x00017894
		protected internal virtual void OnMouseRightButtonUp(MouseButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre este elemento.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600048E RID: 1166 RVA: 0x000184A4 File Offset: 0x000178A4
		// (remove) Token: 0x0600048F RID: 1167 RVA: 0x000184C0 File Offset: 0x000178C0
		public event MouseEventHandler PreviewMouseMove
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000490 RID: 1168 RVA: 0x000184DC File Offset: 0x000178DC
		protected internal virtual void OnPreviewMouseMove(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse se move enquanto está sobre este elemento.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000491 RID: 1169 RVA: 0x000184EC File Offset: 0x000178EC
		// (remove) Token: 0x06000492 RID: 1170 RVA: 0x00018508 File Offset: 0x00017908
		public event MouseEventHandler MouseMove
		{
			add
			{
				this.AddHandler(Mouse.MouseMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000493 RID: 1171 RVA: 0x00018524 File Offset: 0x00017924
		protected internal virtual void OnMouseMove(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário gira a roda do mouse enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000494 RID: 1172 RVA: 0x00018534 File Offset: 0x00017934
		// (remove) Token: 0x06000495 RID: 1173 RVA: 0x00018550 File Offset: 0x00017950
		public event MouseWheelEventHandler PreviewMouseWheel
		{
			add
			{
				this.AddHandler(Mouse.PreviewMouseWheelEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.PreviewMouseWheelEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.PreviewMouseWheel" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseWheelEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000496 RID: 1174 RVA: 0x0001856C File Offset: 0x0001796C
		protected internal virtual void OnPreviewMouseWheel(MouseWheelEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário gira a roda do mouse enquanto o ponteiro do mouse está sobre este elemento.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000497 RID: 1175 RVA: 0x0001857C File Offset: 0x0001797C
		// (remove) Token: 0x06000498 RID: 1176 RVA: 0x00018598 File Offset: 0x00017998
		public event MouseWheelEventHandler MouseWheel
		{
			add
			{
				this.AddHandler(Mouse.MouseWheelEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseWheelEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseWheel" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseWheelEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000499 RID: 1177 RVA: 0x000185B4 File Offset: 0x000179B4
		protected internal virtual void OnMouseWheel(MouseWheelEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse entra nos limites deste elemento.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600049A RID: 1178 RVA: 0x000185C4 File Offset: 0x000179C4
		// (remove) Token: 0x0600049B RID: 1179 RVA: 0x000185E0 File Offset: 0x000179E0
		public event MouseEventHandler MouseEnter
		{
			add
			{
				this.AddHandler(Mouse.MouseEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseEnter" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600049C RID: 1180 RVA: 0x000185FC File Offset: 0x000179FC
		protected internal virtual void OnMouseEnter(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando o ponteiro do mouse sai dos limites deste elemento.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600049D RID: 1181 RVA: 0x0001860C File Offset: 0x00017A0C
		// (remove) Token: 0x0600049E RID: 1182 RVA: 0x00018628 File Offset: 0x00017A28
		public event MouseEventHandler MouseLeave
		{
			add
			{
				this.AddHandler(Mouse.MouseLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.MouseLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.MouseLeave" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600049F RID: 1183 RVA: 0x00018644 File Offset: 0x00017A44
		protected internal virtual void OnMouseLeave(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento captura o mouse.</summary>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060004A0 RID: 1184 RVA: 0x00018654 File Offset: 0x00017A54
		// (remove) Token: 0x060004A1 RID: 1185 RVA: 0x00018670 File Offset: 0x00017A70
		public event MouseEventHandler GotMouseCapture
		{
			add
			{
				this.AddHandler(Mouse.GotMouseCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.GotMouseCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.GotMouseCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004A2 RID: 1186 RVA: 0x0001868C File Offset: 0x00017A8C
		protected internal virtual void OnGotMouseCapture(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura do mouse.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060004A3 RID: 1187 RVA: 0x0001869C File Offset: 0x00017A9C
		// (remove) Token: 0x060004A4 RID: 1188 RVA: 0x000186B8 File Offset: 0x00017AB8
		public event MouseEventHandler LostMouseCapture
		{
			add
			{
				this.AddHandler(Mouse.LostMouseCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.LostMouseCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.LostMouseCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.MouseEventArgs" /> que contém dados do evento.</param>
		// Token: 0x060004A5 RID: 1189 RVA: 0x000186D4 File Offset: 0x00017AD4
		protected internal virtual void OnLostMouseCapture(MouseEventArgs e)
		{
		}

		/// <summary>Ocorre quando a exibição do cursor é solicitada. Este evento é gerado em um elemento toda vez que o ponteiro do mouse se move para uma nova localização, o que significa que o objeto de cursor talvez precise ser alterado de acordo com sua nova posição.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060004A6 RID: 1190 RVA: 0x000186E4 File Offset: 0x00017AE4
		// (remove) Token: 0x060004A7 RID: 1191 RVA: 0x00018700 File Offset: 0x00017B00
		public event QueryCursorEventHandler QueryCursor
		{
			add
			{
				this.AddHandler(Mouse.QueryCursorEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Mouse.QueryCursorEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Mouse.QueryCursor" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.QueryCursorEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001871C File Offset: 0x00017B1C
		protected internal virtual void OnQueryCursor(QueryCursorEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre este elemento.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060004A9 RID: 1193 RVA: 0x0001872C File Offset: 0x00017B2C
		// (remove) Token: 0x060004AA RID: 1194 RVA: 0x00018748 File Offset: 0x00017B48
		public event StylusDownEventHandler PreviewStylusDown
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusDownEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004AB RID: 1195 RVA: 0x00018764 File Offset: 0x00017B64
		protected internal virtual void OnPreviewStylusDown(StylusDownEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta toca o digitalizador enquanto está sobre este elemento.</summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060004AC RID: 1196 RVA: 0x00018774 File Offset: 0x00017B74
		// (remove) Token: 0x060004AD RID: 1197 RVA: 0x00018790 File Offset: 0x00017B90
		public event StylusDownEventHandler StylusDown
		{
			add
			{
				this.AddHandler(Stylus.StylusDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusDownEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004AE RID: 1198 RVA: 0x000187AC File Offset: 0x00017BAC
		protected internal virtual void OnStylusDown(StylusDownEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário retira a caneta do digitalizador enquanto ela está sobre esse elemento.</summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060004AF RID: 1199 RVA: 0x000187BC File Offset: 0x00017BBC
		// (remove) Token: 0x060004B0 RID: 1200 RVA: 0x000187D8 File Offset: 0x00017BD8
		public event StylusEventHandler PreviewStylusUp
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004B1 RID: 1201 RVA: 0x000187F4 File Offset: 0x00017BF4
		protected internal virtual void OnPreviewStylusUp(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário retira a caneta do digitalizador enquanto ela está sobre este elemento.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060004B2 RID: 1202 RVA: 0x00018804 File Offset: 0x00017C04
		// (remove) Token: 0x060004B3 RID: 1203 RVA: 0x00018820 File Offset: 0x00017C20
		public event StylusEventHandler StylusUp
		{
			add
			{
				this.AddHandler(Stylus.StylusUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004B4 RID: 1204 RVA: 0x0001883C File Offset: 0x00017C3C
		protected internal virtual void OnStylusUp(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move enquanto está sobre o elemento. A caneta deve se mover enquanto está sendo detectada pelo digitalizador para gerar este evento, caso contrário, <see cref="E:System.Windows.ContentElement.PreviewStylusInAirMove" /> é gerado.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060004B5 RID: 1205 RVA: 0x0001884C File Offset: 0x00017C4C
		// (remove) Token: 0x060004B6 RID: 1206 RVA: 0x00018868 File Offset: 0x00017C68
		public event StylusEventHandler PreviewStylusMove
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004B7 RID: 1207 RVA: 0x00018884 File Offset: 0x00017C84
		protected internal virtual void OnPreviewStylusMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre este elemento. A caneta deve mover-se enquanto está no digitalizador para gerar este evento. Caso contrário, <see cref="E:System.Windows.ContentElement.StylusInAirMove" /> será gerado.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060004B8 RID: 1208 RVA: 0x00018894 File Offset: 0x00017C94
		// (remove) Token: 0x060004B9 RID: 1209 RVA: 0x000188B0 File Offset: 0x00017CB0
		public event StylusEventHandler StylusMove
		{
			add
			{
				this.AddHandler(Stylus.StylusMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004BA RID: 1210 RVA: 0x000188CC File Offset: 0x00017CCC
		protected internal virtual void OnStylusMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre um elemento sem tocar de fato o digitalizador.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060004BB RID: 1211 RVA: 0x000188DC File Offset: 0x00017CDC
		// (remove) Token: 0x060004BC RID: 1212 RVA: 0x000188F8 File Offset: 0x00017CF8
		public event StylusEventHandler PreviewStylusInAirMove
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusInAirMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusInAirMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInAirMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004BD RID: 1213 RVA: 0x00018914 File Offset: 0x00017D14
		protected internal virtual void OnPreviewStylusInAirMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta se move sobre um elemento sem tocar de fato o digitalizador.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060004BE RID: 1214 RVA: 0x00018924 File Offset: 0x00017D24
		// (remove) Token: 0x060004BF RID: 1215 RVA: 0x00018940 File Offset: 0x00017D40
		public event StylusEventHandler StylusInAirMove
		{
			add
			{
				this.AddHandler(Stylus.StylusInAirMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusInAirMoveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInAirMove" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001895C File Offset: 0x00017D5C
		protected internal virtual void OnStylusInAirMove(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta entra nos limites deste elemento.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060004C1 RID: 1217 RVA: 0x0001896C File Offset: 0x00017D6C
		// (remove) Token: 0x060004C2 RID: 1218 RVA: 0x00018988 File Offset: 0x00017D88
		public event StylusEventHandler StylusEnter
		{
			add
			{
				this.AddHandler(Stylus.StylusEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusEnter" /> sem tratamento é gerado por esse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004C3 RID: 1219 RVA: 0x000189A4 File Offset: 0x00017DA4
		protected internal virtual void OnStylusEnter(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta sai dos limites do elemento.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060004C4 RID: 1220 RVA: 0x000189B4 File Offset: 0x00017DB4
		// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x000189D0 File Offset: 0x00017DD0
		public event StylusEventHandler StylusLeave
		{
			add
			{
				this.AddHandler(Stylus.StylusLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusLeave" /> sem tratamento é gerado por esse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004C6 RID: 1222 RVA: 0x000189EC File Offset: 0x00017DEC
		protected internal virtual void OnStylusLeave(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre este elemento e perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060004C7 RID: 1223 RVA: 0x000189FC File Offset: 0x00017DFC
		// (remove) Token: 0x060004C8 RID: 1224 RVA: 0x00018A18 File Offset: 0x00017E18
		public event StylusEventHandler PreviewStylusInRange
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusInRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusInRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusInRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004C9 RID: 1225 RVA: 0x00018A34 File Offset: 0x00017E34
		protected internal virtual void OnPreviewStylusInRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre este elemento e perto o suficiente do digitalizador para ser detectada.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060004CA RID: 1226 RVA: 0x00018A44 File Offset: 0x00017E44
		// (remove) Token: 0x060004CB RID: 1227 RVA: 0x00018A60 File Offset: 0x00017E60
		public event StylusEventHandler StylusInRange
		{
			add
			{
				this.AddHandler(Stylus.StylusInRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusInRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusInRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004CC RID: 1228 RVA: 0x00018A7C File Offset: 0x00017E7C
		protected internal virtual void OnStylusInRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060004CD RID: 1229 RVA: 0x00018A8C File Offset: 0x00017E8C
		// (remove) Token: 0x060004CE RID: 1230 RVA: 0x00018AA8 File Offset: 0x00017EA8
		public event StylusEventHandler PreviewStylusOutOfRange
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusOutOfRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusOutOfRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusOutOfRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004CF RID: 1231 RVA: 0x00018AC4 File Offset: 0x00017EC4
		protected internal virtual void OnPreviewStylusOutOfRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando a caneta está sobre o elemento e longe demais do digitalizador para ser detectada.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060004D0 RID: 1232 RVA: 0x00018AD4 File Offset: 0x00017ED4
		// (remove) Token: 0x060004D1 RID: 1233 RVA: 0x00018AF0 File Offset: 0x00017EF0
		public event StylusEventHandler StylusOutOfRange
		{
			add
			{
				this.AddHandler(Stylus.StylusOutOfRangeEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusOutOfRangeEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusOutOfRange" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004D2 RID: 1234 RVA: 0x00018B0C File Offset: 0x00017F0C
		protected internal virtual void OnStylusOutOfRange(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário executa um dos diversos gestos da caneta.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060004D3 RID: 1235 RVA: 0x00018B1C File Offset: 0x00017F1C
		// (remove) Token: 0x060004D4 RID: 1236 RVA: 0x00018B38 File Offset: 0x00017F38
		public event StylusSystemGestureEventHandler PreviewStylusSystemGesture
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusSystemGestureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusSystemGestureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusSystemGesture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusSystemGestureEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004D5 RID: 1237 RVA: 0x00018B54 File Offset: 0x00017F54
		protected internal virtual void OnPreviewStylusSystemGesture(StylusSystemGestureEventArgs e)
		{
		}

		/// <summary>Ocorre quando o usuário executa um dos diversos gestos da caneta.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060004D6 RID: 1238 RVA: 0x00018B64 File Offset: 0x00017F64
		// (remove) Token: 0x060004D7 RID: 1239 RVA: 0x00018B80 File Offset: 0x00017F80
		public event StylusSystemGestureEventHandler StylusSystemGesture
		{
			add
			{
				this.AddHandler(Stylus.StylusSystemGestureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusSystemGestureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusSystemGesture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusSystemGestureEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004D8 RID: 1240 RVA: 0x00018B9C File Offset: 0x00017F9C
		protected internal virtual void OnStylusSystemGesture(StylusSystemGestureEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento captura a caneta.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060004D9 RID: 1241 RVA: 0x00018BAC File Offset: 0x00017FAC
		// (remove) Token: 0x060004DA RID: 1242 RVA: 0x00018BC8 File Offset: 0x00017FC8
		public event StylusEventHandler GotStylusCapture
		{
			add
			{
				this.AddHandler(Stylus.GotStylusCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.GotStylusCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.GotStylusCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004DB RID: 1243 RVA: 0x00018BE4 File Offset: 0x00017FE4
		protected internal virtual void OnGotStylusCapture(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura da caneta.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060004DC RID: 1244 RVA: 0x00018BF4 File Offset: 0x00017FF4
		// (remove) Token: 0x060004DD RID: 1245 RVA: 0x00018C10 File Offset: 0x00018010
		public event StylusEventHandler LostStylusCapture
		{
			add
			{
				this.AddHandler(Stylus.LostStylusCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.LostStylusCaptureEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.LostStylusCapture" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusEventArgs" /> que contém dados do evento.</param>
		// Token: 0x060004DE RID: 1246 RVA: 0x00018C2C File Offset: 0x0001802C
		protected internal virtual void OnLostStylusCapture(StylusEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060004DF RID: 1247 RVA: 0x00018C3C File Offset: 0x0001803C
		// (remove) Token: 0x060004E0 RID: 1248 RVA: 0x00018C58 File Offset: 0x00018058
		public event StylusButtonEventHandler StylusButtonDown
		{
			add
			{
				this.AddHandler(Stylus.StylusButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004E1 RID: 1249 RVA: 0x00018C74 File Offset: 0x00018074
		protected internal virtual void OnStylusButtonDown(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060004E2 RID: 1250 RVA: 0x00018C84 File Offset: 0x00018084
		// (remove) Token: 0x060004E3 RID: 1251 RVA: 0x00018CA0 File Offset: 0x000180A0
		public event StylusButtonEventHandler StylusButtonUp
		{
			add
			{
				this.AddHandler(Stylus.StylusButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.StylusButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.StylusButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004E4 RID: 1252 RVA: 0x00018CBC File Offset: 0x000180BC
		protected internal virtual void OnStylusButtonUp(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é pressionado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060004E5 RID: 1253 RVA: 0x00018CCC File Offset: 0x000180CC
		// (remove) Token: 0x060004E6 RID: 1254 RVA: 0x00018CE8 File Offset: 0x000180E8
		public event StylusButtonEventHandler PreviewStylusButtonDown
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusButtonDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusButtonDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004E7 RID: 1255 RVA: 0x00018D04 File Offset: 0x00018104
		protected internal virtual void OnPreviewStylusButtonDown(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando o botão da caneta é liberado enquanto o ponteiro está sobre este elemento.</summary>
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060004E8 RID: 1256 RVA: 0x00018D14 File Offset: 0x00018114
		// (remove) Token: 0x060004E9 RID: 1257 RVA: 0x00018D30 File Offset: 0x00018130
		public event StylusButtonEventHandler PreviewStylusButtonUp
		{
			add
			{
				this.AddHandler(Stylus.PreviewStylusButtonUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Stylus.PreviewStylusButtonUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Stylus.PreviewStylusButtonUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.StylusButtonEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004EA RID: 1258 RVA: 0x00018D4C File Offset: 0x0001814C
		protected internal virtual void OnPreviewStylusButtonUp(StylusButtonEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060004EB RID: 1259 RVA: 0x00018D5C File Offset: 0x0001815C
		// (remove) Token: 0x060004EC RID: 1260 RVA: 0x00018D78 File Offset: 0x00018178
		public event KeyEventHandler PreviewKeyDown
		{
			add
			{
				this.AddHandler(Keyboard.PreviewKeyDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewKeyDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004ED RID: 1261 RVA: 0x00018D94 File Offset: 0x00018194
		protected internal virtual void OnPreviewKeyDown(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é pressionada enquanto o foco está neste elemento.</summary>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060004EE RID: 1262 RVA: 0x00018DA4 File Offset: 0x000181A4
		// (remove) Token: 0x060004EF RID: 1263 RVA: 0x00018DC0 File Offset: 0x000181C0
		public event KeyEventHandler KeyDown
		{
			add
			{
				this.AddHandler(Keyboard.KeyDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.KeyDownEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004F0 RID: 1264 RVA: 0x00018DDC File Offset: 0x000181DC
		protected internal virtual void OnKeyDown(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma tecla é liberada enquanto o teclado está focalizado neste elemento.</summary>
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060004F1 RID: 1265 RVA: 0x00018DEC File Offset: 0x000181EC
		// (remove) Token: 0x060004F2 RID: 1266 RVA: 0x00018E08 File Offset: 0x00018208
		public event KeyEventHandler PreviewKeyUp
		{
			add
			{
				this.AddHandler(Keyboard.PreviewKeyUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewKeyUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004F3 RID: 1267 RVA: 0x00018E24 File Offset: 0x00018224
		protected internal virtual void OnPreviewKeyUp(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma chave é liberada enquanto o foco está neste elemento.</summary>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060004F4 RID: 1268 RVA: 0x00018E34 File Offset: 0x00018234
		// (remove) Token: 0x060004F5 RID: 1269 RVA: 0x00018E50 File Offset: 0x00018250
		public event KeyEventHandler KeyUp
		{
			add
			{
				this.AddHandler(Keyboard.KeyUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.KeyUpEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.KeyUp" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004F6 RID: 1270 RVA: 0x00018E6C File Offset: 0x0001826C
		protected internal virtual void OnKeyUp(KeyEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060004F7 RID: 1271 RVA: 0x00018E7C File Offset: 0x0001827C
		// (remove) Token: 0x060004F8 RID: 1272 RVA: 0x00018E98 File Offset: 0x00018298
		public event KeyboardFocusChangedEventHandler PreviewGotKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.PreviewGotKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewGotKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewGotKeyboardFocus" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004F9 RID: 1273 RVA: 0x00018EB4 File Offset: 0x000182B4
		protected internal virtual void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado está focalizado neste elemento.</summary>
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060004FA RID: 1274 RVA: 0x00018EC4 File Offset: 0x000182C4
		// (remove) Token: 0x060004FB RID: 1275 RVA: 0x00018EE0 File Offset: 0x000182E0
		public event KeyboardFocusChangedEventHandler GotKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.GotKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.GotKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.GotKeyboardFocus" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004FC RID: 1276 RVA: 0x00018EFC File Offset: 0x000182FC
		protected internal virtual void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado não está mais focalizado neste elemento.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060004FD RID: 1277 RVA: 0x00018F0C File Offset: 0x0001830C
		// (remove) Token: 0x060004FE RID: 1278 RVA: 0x00018F28 File Offset: 0x00018328
		public event KeyboardFocusChangedEventHandler PreviewLostKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.PreviewLostKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.PreviewLostKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.PreviewKeyDown" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060004FF RID: 1279 RVA: 0x00018F44 File Offset: 0x00018344
		protected internal virtual void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando o teclado não está mais focalizado neste elemento.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000500 RID: 1280 RVA: 0x00018F54 File Offset: 0x00018354
		// (remove) Token: 0x06000501 RID: 1281 RVA: 0x00018F70 File Offset: 0x00018370
		public event KeyboardFocusChangedEventHandler LostKeyboardFocus
		{
			add
			{
				this.AddHandler(Keyboard.LostKeyboardFocusEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Keyboard.LostKeyboardFocusEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.Keyboard.LostKeyboardFocus" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.KeyboardFocusChangedEventArgs" /> que contém dados do evento.</param>
		// Token: 0x06000502 RID: 1282 RVA: 0x00018F8C File Offset: 0x0001838C
		protected internal virtual void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000503 RID: 1283 RVA: 0x00018F9C File Offset: 0x0001839C
		// (remove) Token: 0x06000504 RID: 1284 RVA: 0x00018FB8 File Offset: 0x000183B8
		public event TextCompositionEventHandler PreviewTextInput
		{
			add
			{
				this.AddHandler(TextCompositionManager.PreviewTextInputEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(TextCompositionManager.PreviewTextInputEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.PreviewTextInput" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.TextCompositionEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000505 RID: 1285 RVA: 0x00018FD4 File Offset: 0x000183D4
		protected internal virtual void OnPreviewTextInput(TextCompositionEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento obtém texto de forma independente de dispositivo.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000506 RID: 1286 RVA: 0x00018FE4 File Offset: 0x000183E4
		// (remove) Token: 0x06000507 RID: 1287 RVA: 0x00019000 File Offset: 0x00018400
		public event TextCompositionEventHandler TextInput
		{
			add
			{
				this.AddHandler(TextCompositionManager.TextInputEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(TextCompositionManager.TextInputEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.Input.TextCompositionManager.TextInput" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.Input.TextCompositionEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000508 RID: 1288 RVA: 0x0001901C File Offset: 0x0001841C
		protected internal virtual void OnTextInput(TextCompositionEventArgs e)
		{
		}

		/// <summary>Ocorre quando há uma alteração no estado do botão do teclado ou do mouse durante uma operação de arrastar e soltar.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000509 RID: 1289 RVA: 0x0001902C File Offset: 0x0001842C
		// (remove) Token: 0x0600050A RID: 1290 RVA: 0x00019048 File Offset: 0x00018448
		public event QueryContinueDragEventHandler PreviewQueryContinueDrag
		{
			add
			{
				this.AddHandler(DragDrop.PreviewQueryContinueDragEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewQueryContinueDragEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewQueryContinueDrag" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.QueryContinueDragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600050B RID: 1291 RVA: 0x00019064 File Offset: 0x00018464
		protected internal virtual void OnPreviewQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		/// <summary>Ocorre quando há uma alteração no estado do botão do teclado ou do mouse durante uma operação de arrastar e soltar.</summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600050C RID: 1292 RVA: 0x00019074 File Offset: 0x00018474
		// (remove) Token: 0x0600050D RID: 1293 RVA: 0x00019090 File Offset: 0x00018490
		public event QueryContinueDragEventHandler QueryContinueDrag
		{
			add
			{
				this.AddHandler(DragDrop.QueryContinueDragEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.QueryContinueDragEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.QueryContinueDrag" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.QueryContinueDragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600050E RID: 1294 RVA: 0x000190AC File Offset: 0x000184AC
		protected internal virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e)
		{
		}

		/// <summary>Ocorre quando uma operação de arrastar e soltar se inicia.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600050F RID: 1295 RVA: 0x000190BC File Offset: 0x000184BC
		// (remove) Token: 0x06000510 RID: 1296 RVA: 0x000190D8 File Offset: 0x000184D8
		public event GiveFeedbackEventHandler PreviewGiveFeedback
		{
			add
			{
				this.AddHandler(DragDrop.PreviewGiveFeedbackEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewGiveFeedbackEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewGiveFeedback" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.GiveFeedbackEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000511 RID: 1297 RVA: 0x000190F4 File Offset: 0x000184F4
		protected internal virtual void OnPreviewGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento de arrastar e soltar subjacente que envolve este elemento.</summary>
		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000512 RID: 1298 RVA: 0x00019104 File Offset: 0x00018504
		// (remove) Token: 0x06000513 RID: 1299 RVA: 0x00019120 File Offset: 0x00018520
		public event GiveFeedbackEventHandler GiveFeedback
		{
			add
			{
				this.AddHandler(DragDrop.GiveFeedbackEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.GiveFeedbackEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.GiveFeedback" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.GiveFeedbackEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000514 RID: 1300 RVA: 0x0001913C File Offset: 0x0001853C
		protected internal virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como o destino de arrastar.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000515 RID: 1301 RVA: 0x0001914C File Offset: 0x0001854C
		// (remove) Token: 0x06000516 RID: 1302 RVA: 0x00019168 File Offset: 0x00018568
		public event DragEventHandler PreviewDragEnter
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDragEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDragEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragEnter" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000517 RID: 1303 RVA: 0x00019184 File Offset: 0x00018584
		protected internal virtual void OnPreviewDragEnter(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como o destino de arrastar.</summary>
		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000518 RID: 1304 RVA: 0x00019194 File Offset: 0x00018594
		// (remove) Token: 0x06000519 RID: 1305 RVA: 0x000191B0 File Offset: 0x000185B0
		public event DragEventHandler DragEnter
		{
			add
			{
				this.AddHandler(DragDrop.DragEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DragEnterEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragEnter" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600051A RID: 1306 RVA: 0x000191CC File Offset: 0x000185CC
		protected internal virtual void OnDragEnter(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento do tipo "arrastar" subjacente com esse elemento como a reprodução automática potencial.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600051B RID: 1307 RVA: 0x000191DC File Offset: 0x000185DC
		// (remove) Token: 0x0600051C RID: 1308 RVA: 0x000191F8 File Offset: 0x000185F8
		public event DragEventHandler PreviewDragOver
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDragOverEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDragOverEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragOver" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600051D RID: 1309 RVA: 0x00019214 File Offset: 0x00018614
		protected internal virtual void OnPreviewDragOver(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento do tipo "arrastar" subjacente com esse elemento como a reprodução automática potencial.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600051E RID: 1310 RVA: 0x00019224 File Offset: 0x00018624
		// (remove) Token: 0x0600051F RID: 1311 RVA: 0x00019240 File Offset: 0x00018640
		public event DragEventHandler DragOver
		{
			add
			{
				this.AddHandler(DragDrop.DragOverEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DragOverEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragOver" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000520 RID: 1312 RVA: 0x0001925C File Offset: 0x0001865C
		protected internal virtual void OnDragOver(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como a origem de arrastar.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000521 RID: 1313 RVA: 0x0001926C File Offset: 0x0001866C
		// (remove) Token: 0x06000522 RID: 1314 RVA: 0x00019288 File Offset: 0x00018688
		public event DragEventHandler PreviewDragLeave
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDragLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDragLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDragLeave" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000523 RID: 1315 RVA: 0x000192A4 File Offset: 0x000186A4
		protected internal virtual void OnPreviewDragLeave(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento arrastar subjacente com este elemento como a origem de arrastar.</summary>
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000524 RID: 1316 RVA: 0x000192B4 File Offset: 0x000186B4
		// (remove) Token: 0x06000525 RID: 1317 RVA: 0x000192D0 File Offset: 0x000186D0
		public event DragEventHandler DragLeave
		{
			add
			{
				this.AddHandler(DragDrop.DragLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DragLeaveEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragLeave" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000526 RID: 1318 RVA: 0x000192EC File Offset: 0x000186EC
		protected internal virtual void OnDragLeave(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento soltar subjacente com esse elemento sendo uma reprodução automática.</summary>
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000527 RID: 1319 RVA: 0x000192FC File Offset: 0x000186FC
		// (remove) Token: 0x06000528 RID: 1320 RVA: 0x00019318 File Offset: 0x00018718
		public event DragEventHandler PreviewDrop
		{
			add
			{
				this.AddHandler(DragDrop.PreviewDropEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.PreviewDropEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.PreviewDrop" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000529 RID: 1321 RVA: 0x00019334 File Offset: 0x00018734
		protected internal virtual void OnPreviewDrop(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando o sistema de entrada relata um evento soltar subjacente com esse elemento sendo uma reprodução automática.</summary>
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x0600052A RID: 1322 RVA: 0x00019344 File Offset: 0x00018744
		// (remove) Token: 0x0600052B RID: 1323 RVA: 0x00019360 File Offset: 0x00018760
		public event DragEventHandler Drop
		{
			add
			{
				this.AddHandler(DragDrop.DropEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(DragDrop.DropEvent, value);
			}
		}

		/// <summary>Invocado quando um evento anexado <see cref="E:System.Windows.DragDrop.DragEnter" /> sem tratamento atinge um elemento em sua rota que deriva dessa classe. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DragEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600052C RID: 1324 RVA: 0x0001937C File Offset: 0x0001877C
		protected internal virtual void OnDrop(DragEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo toca a tela enquanto está sobre esse elemento.</summary>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600052D RID: 1325 RVA: 0x0001938C File Offset: 0x0001878C
		// (remove) Token: 0x0600052E RID: 1326 RVA: 0x000193A8 File Offset: 0x000187A8
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> PreviewTouchDown
		{
			add
			{
				this.AddHandler(Touch.PreviewTouchDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.PreviewTouchDownEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.ContentElement.PreviewTouchDown" /> que ocorrem quando um toque pressiona esse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600052F RID: 1327 RVA: 0x000193C4 File Offset: 0x000187C4
		protected internal virtual void OnPreviewTouchDown(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo toca a tela enquanto está sobre esse elemento.</summary>
		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000530 RID: 1328 RVA: 0x000193D4 File Offset: 0x000187D4
		// (remove) Token: 0x06000531 RID: 1329 RVA: 0x000193F0 File Offset: 0x000187F0
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchDown
		{
			add
			{
				this.AddHandler(Touch.TouchDownEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchDownEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.ContentElement.TouchDown" /> que ocorrem quando há um toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000532 RID: 1330 RVA: 0x0001940C File Offset: 0x0001880C
		protected internal virtual void OnTouchDown(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo se move na tela enquanto está sobre esse elemento.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000533 RID: 1331 RVA: 0x0001941C File Offset: 0x0001881C
		// (remove) Token: 0x06000534 RID: 1332 RVA: 0x00019438 File Offset: 0x00018838
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> PreviewTouchMove
		{
			add
			{
				this.AddHandler(Touch.PreviewTouchMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.PreviewTouchMoveEvent, value);
			}
		}

		/// <summary>Fornece manipulação de classes para o evento roteado <see cref="E:System.Windows.ContentElement.PreviewTouchMove" /> que ocorre quando há uma movimentação de toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000535 RID: 1333 RVA: 0x00019454 File Offset: 0x00018854
		protected internal virtual void OnPreviewTouchMove(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo se move na tela enquanto está sobre esse elemento.</summary>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06000536 RID: 1334 RVA: 0x00019464 File Offset: 0x00018864
		// (remove) Token: 0x06000537 RID: 1335 RVA: 0x00019480 File Offset: 0x00018880
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchMove
		{
			add
			{
				this.AddHandler(Touch.TouchMoveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchMoveEvent, value);
			}
		}

		/// <summary>Fornece manipulação de classes para o evento roteado <see cref="E:System.Windows.ContentElement.TouchMove" /> que ocorre quando há uma movimentação de toque nesse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000538 RID: 1336 RVA: 0x0001949C File Offset: 0x0001889C
		protected internal virtual void OnTouchMove(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo é gerado fora da tela enquanto o dedo está sobre este elemento.</summary>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06000539 RID: 1337 RVA: 0x000194AC File Offset: 0x000188AC
		// (remove) Token: 0x0600053A RID: 1338 RVA: 0x000194C8 File Offset: 0x000188C8
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> PreviewTouchUp
		{
			add
			{
				this.AddHandler(Touch.PreviewTouchUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.PreviewTouchUpEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classe para os eventos roteados <see cref="E:System.Windows.ContentElement.PreviewTouchUp" /> que ocorrem quando um toque é liberado dentro desse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600053B RID: 1339 RVA: 0x000194E4 File Offset: 0x000188E4
		protected internal virtual void OnPreviewTouchUp(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um dedo é gerado fora da tela enquanto o dedo está sobre este elemento.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600053C RID: 1340 RVA: 0x000194F4 File Offset: 0x000188F4
		// (remove) Token: 0x0600053D RID: 1341 RVA: 0x00019510 File Offset: 0x00018910
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchUp
		{
			add
			{
				this.AddHandler(Touch.TouchUpEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchUpEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classe para os eventos roteados <see cref="E:System.Windows.ContentElement.TouchUp" /> que ocorrem quando um toque é liberado dentro desse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600053E RID: 1342 RVA: 0x0001952C File Offset: 0x0001892C
		protected internal virtual void OnTouchUp(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é capturado para esse elemento.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x0600053F RID: 1343 RVA: 0x0001953C File Offset: 0x0001893C
		// (remove) Token: 0x06000540 RID: 1344 RVA: 0x00019558 File Offset: 0x00018958
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> GotTouchCapture
		{
			add
			{
				this.AddHandler(Touch.GotTouchCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.GotTouchCaptureEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados <see cref="E:System.Windows.ContentElement.GotTouchCapture" /> que ocorrem quando um toque é capturado para esse elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000541 RID: 1345 RVA: 0x00019574 File Offset: 0x00018974
		protected internal virtual void OnGotTouchCapture(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando este elemento perde a captura do toque.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000542 RID: 1346 RVA: 0x00019584 File Offset: 0x00018984
		// (remove) Token: 0x06000543 RID: 1347 RVA: 0x000195A0 File Offset: 0x000189A0
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> LostTouchCapture
		{
			add
			{
				this.AddHandler(Touch.LostTouchCaptureEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.LostTouchCaptureEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para o evento roteado <see cref="E:System.Windows.ContentElement.LostTouchCapture" /> que ocorre quando este elemento perde a captura de toque.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000544 RID: 1348 RVA: 0x000195BC File Offset: 0x000189BC
		protected internal virtual void OnLostTouchCapture(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é movido de fora para dentro dos limites deste elemento.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000545 RID: 1349 RVA: 0x000195CC File Offset: 0x000189CC
		// (remove) Token: 0x06000546 RID: 1350 RVA: 0x000195E8 File Offset: 0x000189E8
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchEnter
		{
			add
			{
				this.AddHandler(Touch.TouchEnterEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchEnterEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classes para os eventos roteados de <see cref="E:System.Windows.ContentElement.TouchEnter" /> que ocorre quando um toque é movido de fora para dentro dos limites deste elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000547 RID: 1351 RVA: 0x00019604 File Offset: 0x00018A04
		protected internal virtual void OnTouchEnter(TouchEventArgs e)
		{
		}

		/// <summary>Ocorre quando um toque é movido de dentro para fora dos limites deste elemento.</summary>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000548 RID: 1352 RVA: 0x00019614 File Offset: 0x00018A14
		// (remove) Token: 0x06000549 RID: 1353 RVA: 0x00019630 File Offset: 0x00018A30
		[CustomCategory("Touch_Category")]
		public event EventHandler<TouchEventArgs> TouchLeave
		{
			add
			{
				this.AddHandler(Touch.TouchLeaveEvent, value, false);
			}
			remove
			{
				this.RemoveHandler(Touch.TouchLeaveEvent, value);
			}
		}

		/// <summary>Fornece tratamento de classe para o evento roteado <see cref="E:System.Windows.ContentElement.TouchLeave" /> que ocorre quando um toque é movido de dentro para fora dos limites deste elemento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Input.TouchEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600054A RID: 1354 RVA: 0x0001964C File Offset: 0x00018A4C
		protected internal virtual void OnTouchLeave(TouchEventArgs e)
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001965C File Offset: 0x00018A5C
		private static void IsMouseDirectlyOver_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ContentElement)d).RaiseIsMouseDirectlyOverChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.IsMouseDirectlyOver" /> é alterado neste elemento.</summary>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600054C RID: 1356 RVA: 0x00019678 File Offset: 0x00018A78
		// (remove) Token: 0x0600054D RID: 1357 RVA: 0x00019694 File Offset: 0x00018A94
		public event DependencyPropertyChangedEventHandler IsMouseDirectlyOverChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsMouseDirectlyOverChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsMouseDirectlyOverChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.ContentElement.IsMouseDirectlyOverChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600054E RID: 1358 RVA: 0x000196B0 File Offset: 0x00018AB0
		protected virtual void OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000196C0 File Offset: 0x00018AC0
		private void RaiseIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseDirectlyOverChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseDirectlyOverChangedKey, args);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="E:System.Windows.ContentElement.IsKeyboardFocusWithinChanged" /> é alterado neste elemento.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000550 RID: 1360 RVA: 0x000196E0 File Offset: 0x00018AE0
		// (remove) Token: 0x06000551 RID: 1361 RVA: 0x000196FC File Offset: 0x00018AFC
		public event DependencyPropertyChangedEventHandler IsKeyboardFocusWithinChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsKeyboardFocusWithinChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsKeyboardFocusWithinChangedKey, value);
			}
		}

		/// <summary>Invocado pouco antes do evento <see cref="E:System.Windows.ContentElement.IsKeyboardFocusWithinChanged" /> ser gerado por este elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000552 RID: 1362 RVA: 0x00019718 File Offset: 0x00018B18
		protected virtual void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00019728 File Offset: 0x00018B28
		internal void RaiseIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsKeyboardFocusWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsKeyboardFocusWithinChangedKey, args);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00019748 File Offset: 0x00018B48
		private static void IsMouseCaptured_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ContentElement)d).RaiseIsMouseCapturedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.IsMouseCaptured" /> é alterado neste elemento.</summary>
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000555 RID: 1365 RVA: 0x00019764 File Offset: 0x00018B64
		// (remove) Token: 0x06000556 RID: 1366 RVA: 0x00019780 File Offset: 0x00018B80
		public event DependencyPropertyChangedEventHandler IsMouseCapturedChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsMouseCapturedChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsMouseCapturedChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.ContentElement.IsMouseCapturedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000557 RID: 1367 RVA: 0x0001979C File Offset: 0x00018B9C
		protected virtual void OnIsMouseCapturedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000197AC File Offset: 0x00018BAC
		private void RaiseIsMouseCapturedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseCapturedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseCapturedChangedKey, args);
		}

		/// <summary>Ocorre quando o valor do <see cref="F:System.Windows.ContentElement.IsMouseCaptureWithinProperty" /> é alterado nesse elemento.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000559 RID: 1369 RVA: 0x000197CC File Offset: 0x00018BCC
		// (remove) Token: 0x0600055A RID: 1370 RVA: 0x000197E8 File Offset: 0x00018BE8
		public event DependencyPropertyChangedEventHandler IsMouseCaptureWithinChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsMouseCaptureWithinChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsMouseCaptureWithinChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.ContentElement.IsMouseCaptureWithinChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600055B RID: 1371 RVA: 0x00019804 File Offset: 0x00018C04
		protected virtual void OnIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00019814 File Offset: 0x00018C14
		internal void RaiseIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsMouseCaptureWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsMouseCaptureWithinChangedKey, args);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00019834 File Offset: 0x00018C34
		private static void IsStylusDirectlyOver_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ContentElement)d).RaiseIsStylusDirectlyOverChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.IsStylusDirectlyOver" /> é alterado neste elemento.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x0600055E RID: 1374 RVA: 0x00019850 File Offset: 0x00018C50
		// (remove) Token: 0x0600055F RID: 1375 RVA: 0x0001986C File Offset: 0x00018C6C
		public event DependencyPropertyChangedEventHandler IsStylusDirectlyOverChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsStylusDirectlyOverChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsStylusDirectlyOverChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.ContentElement.IsStylusDirectlyOverChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000560 RID: 1376 RVA: 0x00019888 File Offset: 0x00018C88
		protected virtual void OnIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00019898 File Offset: 0x00018C98
		private void RaiseIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusDirectlyOverChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusDirectlyOverChangedKey, args);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000198B8 File Offset: 0x00018CB8
		private static void IsStylusCaptured_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ContentElement)d).RaiseIsStylusCapturedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.IsStylusCaptured" /> é alterado neste elemento.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000563 RID: 1379 RVA: 0x000198D4 File Offset: 0x00018CD4
		// (remove) Token: 0x06000564 RID: 1380 RVA: 0x000198F0 File Offset: 0x00018CF0
		public event DependencyPropertyChangedEventHandler IsStylusCapturedChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsStylusCapturedChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsStylusCapturedChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.ContentElement.IsStylusCapturedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000565 RID: 1381 RVA: 0x0001990C File Offset: 0x00018D0C
		protected virtual void OnIsStylusCapturedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001991C File Offset: 0x00018D1C
		private void RaiseIsStylusCapturedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusCapturedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusCapturedChangedKey, args);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.IsStylusCaptureWithin" /> é alterado neste elemento.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000567 RID: 1383 RVA: 0x0001993C File Offset: 0x00018D3C
		// (remove) Token: 0x06000568 RID: 1384 RVA: 0x00019958 File Offset: 0x00018D58
		public event DependencyPropertyChangedEventHandler IsStylusCaptureWithinChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsStylusCaptureWithinChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsStylusCaptureWithinChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.ContentElement.IsStylusCaptureWithinChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06000569 RID: 1385 RVA: 0x00019974 File Offset: 0x00018D74
		protected virtual void OnIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00019984 File Offset: 0x00018D84
		internal void RaiseIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsStylusCaptureWithinChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsStylusCaptureWithinChangedKey, args);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000199A4 File Offset: 0x00018DA4
		private static void IsKeyboardFocused_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ContentElement)d).RaiseIsKeyboardFocusedChanged(e);
		}

		/// <summary>Ocorre quando o valor da propriedade <see cref="P:System.Windows.ContentElement.IsKeyboardFocused" /> é alterado neste elemento.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x0600056C RID: 1388 RVA: 0x000199C0 File Offset: 0x00018DC0
		// (remove) Token: 0x0600056D RID: 1389 RVA: 0x000199DC File Offset: 0x00018DDC
		public event DependencyPropertyChangedEventHandler IsKeyboardFocusedChanged
		{
			add
			{
				this.EventHandlersStoreAdd(UIElement.IsKeyboardFocusedChangedKey, value);
			}
			remove
			{
				this.EventHandlersStoreRemove(UIElement.IsKeyboardFocusedChangedKey, value);
			}
		}

		/// <summary>Invocado quando um evento <see cref="E:System.Windows.ContentElement.IsKeyboardFocusedChanged" /> sem tratamento é gerado nesse elemento. Implemente esse método para adicionar tratamento de classe a esse evento.</summary>
		/// <param name="e">O <see cref="T:System.Windows.DependencyPropertyChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x0600056E RID: 1390 RVA: 0x000199F8 File Offset: 0x00018DF8
		protected virtual void OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs e)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00019A08 File Offset: 0x00018E08
		private void RaiseIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs args)
		{
			this.OnIsKeyboardFocusedChanged(args);
			this.RaiseDependencyPropertyChanged(UIElement.IsKeyboardFocusedChangedKey, args);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00019A28 File Offset: 0x00018E28
		internal bool ReadFlag(CoreFlags field)
		{
			return (this._flags & field) > CoreFlags.None;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00019A40 File Offset: 0x00018E40
		internal void WriteFlag(CoreFlags field, bool value)
		{
			if (value)
			{
				this._flags |= field;
				return;
			}
			this._flags &= ~field;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsFocused" />.</summary>
		// Token: 0x040004C9 RID: 1225
		public static readonly DependencyProperty IsFocusedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsEnabled" />.</summary>
		// Token: 0x040004CA RID: 1226
		public static readonly DependencyProperty IsEnabledProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.Focusable" />.</summary>
		// Token: 0x040004CB RID: 1227
		[CommonDependencyProperty]
		public static readonly DependencyProperty FocusableProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.AllowDrop" />.</summary>
		// Token: 0x040004CC RID: 1228
		public static readonly DependencyProperty AllowDropProperty;

		// Token: 0x040004CD RID: 1229
		internal DependencyObject _parent;

		// Token: 0x040004CE RID: 1230
		internal static readonly UncommonField<EventHandlersStore> EventHandlersStoreField;

		// Token: 0x040004CF RID: 1231
		internal static readonly UncommonField<InputBindingCollection> InputBindingCollectionField;

		// Token: 0x040004D0 RID: 1232
		internal static readonly UncommonField<CommandBindingCollection> CommandBindingCollectionField;

		// Token: 0x040004D1 RID: 1233
		private static readonly UncommonField<AutomationPeer> AutomationPeerField;

		// Token: 0x040004D2 RID: 1234
		private static DependencyObjectType ContentElementType;

		// Token: 0x040004D3 RID: 1235
		private static readonly Type _typeofThis;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsMouseDirectlyOver" />.</summary>
		// Token: 0x0400051F RID: 1311
		public static readonly DependencyProperty IsMouseDirectlyOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsMouseOver" />.</summary>
		// Token: 0x04000520 RID: 1312
		public static readonly DependencyProperty IsMouseOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsStylusOver" />.</summary>
		// Token: 0x04000521 RID: 1313
		public static readonly DependencyProperty IsStylusOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsKeyboardFocusWithin" />.</summary>
		// Token: 0x04000522 RID: 1314
		public static readonly DependencyProperty IsKeyboardFocusWithinProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsMouseCaptured" />.</summary>
		// Token: 0x04000523 RID: 1315
		public static readonly DependencyProperty IsMouseCapturedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsMouseCaptureWithin" />.</summary>
		// Token: 0x04000524 RID: 1316
		public static readonly DependencyProperty IsMouseCaptureWithinProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsStylusDirectlyOver" />.</summary>
		// Token: 0x04000525 RID: 1317
		public static readonly DependencyProperty IsStylusDirectlyOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsStylusCaptured" />.</summary>
		// Token: 0x04000526 RID: 1318
		public static readonly DependencyProperty IsStylusCapturedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsStylusCaptureWithin" />.</summary>
		// Token: 0x04000527 RID: 1319
		public static readonly DependencyProperty IsStylusCaptureWithinProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.IsKeyboardFocused" />.</summary>
		// Token: 0x04000528 RID: 1320
		public static readonly DependencyProperty IsKeyboardFocusedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.AreAnyTouchesDirectlyOver" />.</summary>
		// Token: 0x04000529 RID: 1321
		public static readonly DependencyProperty AreAnyTouchesDirectlyOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.AreAnyTouchesOver" />.</summary>
		// Token: 0x0400052A RID: 1322
		public static readonly DependencyProperty AreAnyTouchesOverProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.AreAnyTouchesCaptured" />.</summary>
		// Token: 0x0400052B RID: 1323
		public static readonly DependencyProperty AreAnyTouchesCapturedProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.ContentElement.AreAnyTouchesCapturedWithin" />.</summary>
		// Token: 0x0400052C RID: 1324
		public static readonly DependencyProperty AreAnyTouchesCapturedWithinProperty;

		// Token: 0x0400052D RID: 1325
		private CoreFlags _flags;
	}
}
