using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using MS.Internal;
using MS.Internal.Automation;
using MS.Internal.PresentationCore;
using MS.Win32;

namespace System.Windows.Automation.Peers
{
	/// <summary>Fornece uma classe base que expõe um elemento para Automação de interface do usuário.</summary>
	// Token: 0x02000314 RID: 788
	public abstract class AutomationPeer : DispatcherObject
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x0006379C File Offset: 0x00062B9C
		static AutomationPeer()
		{
			using (Dispatcher.CurrentDispatcher.DisableProcessing())
			{
				AutomationPeer.Initialize();
			}
		}

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetChildren" />.</summary>
		/// <returns>A coleção de elementos filho.</returns>
		// Token: 0x06001908 RID: 6408
		protected abstract List<AutomationPeer> GetChildrenCore();

		/// <summary>Quando substituído em uma classe derivada, obtém o padrão de controle associado ao <see cref="T:System.Windows.Automation.Peers.PatternInterface" /> especificado.</summary>
		/// <param name="patternInterface">Um valor da enumeração <see cref="T:System.Windows.Automation.Peers.PatternInterface" />.</param>
		/// <returns>O objeto que implementa a interface padrão; <see langword="null" /> se esse par não for compatível com essa interface.</returns>
		// Token: 0x06001909 RID: 6409
		public abstract object GetPattern(PatternInterface patternInterface);

		/// <summary>Dispara o recálculo das propriedades principais do <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> e gerará a notificação <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> para o Cliente de Automação se as propriedades mudarem.</summary>
		// Token: 0x0600190A RID: 6410 RVA: 0x000637F8 File Offset: 0x00062BF8
		public void InvalidatePeer()
		{
			if (this._invalidated)
			{
				return;
			}
			base.Dispatcher.BeginInvoke(DispatcherPriority.Background, AutomationPeer._updatePeer, this);
			this._invalidated = true;
		}

		/// <summary>Obtém um valor que indica se Automação da interface do usuário está escutando o evento especificado.</summary>
		/// <param name="eventId">Um dos valores de enumeração.</param>
		/// <returns>Um <see langword="boolean" /> que indica se Automação da interface do usuário está escutando o evento.</returns>
		// Token: 0x0600190B RID: 6411 RVA: 0x00063828 File Offset: 0x00062C28
		public static bool ListenerExists(AutomationEvents eventId)
		{
			return EventMap.HasRegisteredEvent(eventId);
		}

		/// <summary>Gera um evento de automação.</summary>
		/// <param name="eventId">O identificador de evento.</param>
		// Token: 0x0600190C RID: 6412 RVA: 0x0006383C File Offset: 0x00062C3C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void RaiseAutomationEvent(AutomationEvents eventId)
		{
			AutomationEvent registeredEvent = EventMap.GetRegisteredEvent(eventId);
			if (registeredEvent == null)
			{
				return;
			}
			IRawElementProviderSimple rawElementProviderSimple = this.ProviderFromPeer(this);
			if (rawElementProviderSimple != null)
			{
				AutomationInteropProvider.RaiseAutomationEvent(registeredEvent, rawElementProviderSimple, new AutomationEventArgs(registeredEvent));
			}
		}

		/// <summary>Gera um evento para notificar o cliente de automação de um valor da propriedade alterado.</summary>
		/// <param name="property">A propriedade que foi alterada.</param>
		/// <param name="oldValue">O valor anterior da propriedade.</param>
		/// <param name="newValue">O novo valor da propriedade.</param>
		// Token: 0x0600190D RID: 6413 RVA: 0x0006386C File Offset: 0x00062C6C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void RaisePropertyChangedEvent(AutomationProperty property, object oldValue, object newValue)
		{
			if (AutomationInteropProvider.ClientsAreListening)
			{
				this.RaisePropertyChangedInternal(this.ProviderFromPeer(this), property, oldValue, newValue);
			}
		}

		/// <summary>Chamado pelo <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para gerar o evento <see cref="F:System.Windows.Automation.AutomationElement.AsyncContentLoadedEvent" />.</summary>
		/// <param name="args">Os dados do evento.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="args" /> é <see langword="null" />.</exception>
		// Token: 0x0600190E RID: 6414 RVA: 0x00063890 File Offset: 0x00062C90
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void RaiseAsyncContentLoadedEvent(AsyncContentLoadedEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (EventMap.HasRegisteredEvent(AutomationEvents.AsyncContentLoaded))
			{
				IRawElementProviderSimple rawElementProviderSimple = this.ProviderFromPeer(this);
				if (rawElementProviderSimple != null)
				{
					AutomationInteropProvider.RaiseAutomationEvent(AutomationElementIdentifiers.AsyncContentLoadedEvent, rawElementProviderSimple, args);
				}
			}
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000638CC File Offset: 0x00062CCC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public void RaiseNotificationEvent(AutomationNotificationKind notificationKind, AutomationNotificationProcessing notificationProcessing, string displayString, string activityId)
		{
			if (EventMap.HasRegisteredEvent(AutomationEvents.Notification))
			{
				IRawElementProviderSimple rawElementProviderSimple = this.ProviderFromPeer(this);
				if (rawElementProviderSimple != null)
				{
					AutomationInteropProvider.RaiseAutomationEvent(AutomationElementIdentifiers.NotificationEvent, rawElementProviderSimple, new NotificationEventArgs(notificationKind, notificationProcessing, displayString, activityId));
				}
			}
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00063904 File Offset: 0x00062D04
		internal static void RaiseFocusChangedEventHelper(IInputElement newFocus)
		{
			if (EventMap.HasRegisteredEvent(AutomationEvents.AutomationFocusChanged))
			{
				AutomationPeer automationPeer = AutomationPeer.AutomationPeerFromInputElement(newFocus);
				if (automationPeer != null)
				{
					automationPeer.RaiseAutomationEvent(AutomationEvents.AutomationFocusChanged);
				}
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0006392C File Offset: 0x00062D2C
		internal static AutomationPeer AutomationPeerFromInputElement(IInputElement focusedElement)
		{
			AutomationPeer automationPeer = null;
			UIElement uielement = focusedElement as UIElement;
			if (uielement != null)
			{
				automationPeer = UIElementAutomationPeer.CreatePeerForElement(uielement);
			}
			else
			{
				ContentElement contentElement = focusedElement as ContentElement;
				if (contentElement != null)
				{
					automationPeer = ContentElementAutomationPeer.CreatePeerForElement(contentElement);
				}
				else
				{
					UIElement3D uielement3D = focusedElement as UIElement3D;
					if (uielement3D != null)
					{
						automationPeer = UIElement3DAutomationPeer.CreatePeerForElement(uielement3D);
					}
				}
			}
			if (automationPeer != null)
			{
				automationPeer.ValidateConnected(automationPeer);
				if (automationPeer.EventsSource != null)
				{
					automationPeer = automationPeer.EventsSource;
				}
			}
			return automationPeer;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00063990 File Offset: 0x00062D90
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal AutomationPeer ValidateConnected(AutomationPeer connectedPeer)
		{
			if (connectedPeer == null)
			{
				throw new ArgumentNullException("connectedPeer");
			}
			if (this._parent != null && this._hwnd != IntPtr.Zero)
			{
				return this;
			}
			if (connectedPeer._hwnd != IntPtr.Zero)
			{
				while (connectedPeer._parent != null)
				{
					connectedPeer = connectedPeer._parent;
				}
				if (connectedPeer == this || this.isDescendantOf(connectedPeer))
				{
					return this;
				}
			}
			ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(base.Dispatcher);
			if (contextLayoutManager != null && contextLayoutManager.AutomationSyncUpdateCounter == 0)
			{
				foreach (AutomationPeer automationPeer in contextLayoutManager.GetAutomationRoots())
				{
					if (automationPeer != null && (automationPeer == this || this.isDescendantOf(automationPeer)))
					{
						return this;
					}
				}
			}
			return null;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00063A40 File Offset: 0x00062E40
		[SecurityCritical]
		internal bool TrySetParentInfo(AutomationPeer peer)
		{
			Invariant.Assert(peer != null);
			if (peer._hwnd == IntPtr.Zero)
			{
				return false;
			}
			this._hwnd = peer._hwnd;
			this._parent = peer;
			return true;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00063A80 File Offset: 0x00062E80
		internal virtual bool IsDataItemAutomationPeer()
		{
			return false;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00063A90 File Offset: 0x00062E90
		internal virtual bool IgnoreUpdatePeer()
		{
			return false;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00063AA0 File Offset: 0x00062EA0
		internal virtual void AddToParentProxyWeakRefCache()
		{
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00063AB0 File Offset: 0x00062EB0
		private bool isDescendantOf(AutomationPeer parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			List<AutomationPeer> children = parent.GetChildren();
			if (children == null)
			{
				return false;
			}
			int count = children.Count;
			for (int i = 0; i < count; i++)
			{
				AutomationPeer automationPeer = children[i];
				if (automationPeer == this || this.isDescendantOf(automationPeer))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Informa a Automação da interface do usuário em que ponto na árvore Automação da interface do usuário colocar o <see langword="hwnd" /> que está sendo hospedado por um elemento Windows Presentation Foundation (WPF).</summary>
		/// <returns>Este método retorna o <see langword="hwnd" /> hospedado para Automação da interface do usuário para controles que hospedam objetos <see langword="hwnd" />.</returns>
		// Token: 0x06001918 RID: 6424 RVA: 0x00063B04 File Offset: 0x00062F04
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected virtual HostedWindowWrapper GetHostRawElementProviderCore()
		{
			HostedWindowWrapper result = null;
			if (this.GetParent() == null)
			{
				result = HostedWindowWrapper.CreateInternal(this.Hwnd);
			}
			return result;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00063B28 File Offset: 0x00062F28
		internal HostedWindowWrapper GetHostRawElementProvider()
		{
			return this.GetHostRawElementProviderCore();
		}

		/// <summary>Obtém um valor que indica se o elemento associado a este <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> hospeda <see langword="hwnds" /> em Windows Presentation Foundation (WPF).</summary>
		/// <returns>
		///   <see langword="true" /> Se o elemento que é associado a este <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> hosts <see langword="hwnds" /> na Windows Presentation Foundation (WPF); caso contrário, <see langword="false" />.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x00063B3C File Offset: 0x00062F3C
		protected internal virtual bool IsHwndHost
		{
			get
			{
				return false;
			}
		}

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetBoundingRectangle" />.</summary>
		/// <returns>O retângulo delimitador.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600191B RID: 6427
		protected abstract Rect GetBoundingRectangleCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento não estiver na tela; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600191C RID: 6428
		protected abstract bool IsOffscreenCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetOrientation" />.</summary>
		/// <returns>A orientação do controle.</returns>
		// Token: 0x0600191D RID: 6429
		protected abstract AutomationOrientation GetOrientationCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemType" />.</summary>
		/// <returns>O tipo de item.</returns>
		// Token: 0x0600191E RID: 6430
		protected abstract string GetItemTypeCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClassName" />.</summary>
		/// <returns>O nome de classe.</returns>
		// Token: 0x0600191F RID: 6431
		protected abstract string GetClassNameCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemStatus" />.</summary>
		/// <returns>O status.</returns>
		// Token: 0x06001920 RID: 6432
		protected abstract string GetItemStatusCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsRequiredForForm" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento precisar ser preenchido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001921 RID: 6433
		protected abstract bool IsRequiredForFormCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsKeyboardFocusable" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento puder aceitar o foco do teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001922 RID: 6434
		protected abstract bool IsKeyboardFocusableCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.HasKeyboardFocus" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver o foco do teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001923 RID: 6435
		protected abstract bool HasKeyboardFocusCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsEnabled" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o par de automação puder receber e enviar eventos; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001924 RID: 6436
		protected abstract bool IsEnabledCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsPassword" />.</summary>
		/// <returns>
		///   <see langword="true" /> se houver conteúdo confidencial no elemento; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001925 RID: 6437
		protected abstract bool IsPasswordCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationId" />.</summary>
		/// <returns>A cadeia de caracteres que contém o identificador.</returns>
		// Token: 0x06001926 RID: 6438
		protected abstract string GetAutomationIdCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetName" />.</summary>
		/// <returns>A cadeia de caracteres que contém o rótulo.</returns>
		// Token: 0x06001927 RID: 6439
		protected abstract string GetNameCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationControlType" />.</summary>
		/// <returns>O tipo de controle.</returns>
		// Token: 0x06001928 RID: 6440
		protected abstract AutomationControlType GetAutomationControlTypeCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLocalizedControlType" />.</summary>
		/// <returns>O tipo do controle.</returns>
		// Token: 0x06001929 RID: 6441 RVA: 0x00063B4C File Offset: 0x00062F4C
		protected virtual string GetLocalizedControlTypeCore()
		{
			ControlType controlType = this.GetControlType();
			return controlType.LocalizedControlType;
		}

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsContentElement" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for um elemento de conteúdo; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600192A RID: 6442
		protected abstract bool IsContentElementCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsControlElement" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for um controle; caso contrário, <see langword="false" />.</returns>
		// Token: 0x0600192B RID: 6443
		protected abstract bool IsControlElementCore();

		// Token: 0x0600192C RID: 6444 RVA: 0x00063B68 File Offset: 0x00062F68
		protected virtual bool IsDialogCore()
		{
			return false;
		}

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLabeledBy" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.LabelAutomationPeer" /> para o elemento que é o destino de <see cref="T:System.Windows.Controls.Label" />.</returns>
		// Token: 0x0600192D RID: 6445
		protected abstract AutomationPeer GetLabeledByCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetHelpText" />.</summary>
		/// <returns>O texto de ajuda.</returns>
		// Token: 0x0600192E RID: 6446
		protected abstract string GetHelpTextCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAcceleratorKey" />.</summary>
		/// <returns>A tecla de atalho.</returns>
		// Token: 0x0600192F RID: 6447
		protected abstract string GetAcceleratorKeyCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAccessKey" />.</summary>
		/// <returns>A cadeia de caracteres que contém a chave de acesso.</returns>
		// Token: 0x06001930 RID: 6448
		protected abstract string GetAccessKeyCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClickablePoint" />.</summary>
		/// <returns>Um ponto na área clicável do elemento.</returns>
		// Token: 0x06001931 RID: 6449
		protected abstract Point GetClickablePointCore();

		/// <summary>Quando substituído em uma classe derivada, é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.SetFocus" />.</summary>
		// Token: 0x06001932 RID: 6450
		protected abstract void SetFocusCore();

		/// <summary>Quando substituído em uma classe derivada, retorna as características de notificação de uma região dinâmica. Chamado pelo método <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLiveSetting" />.</summary>
		/// <returns>As características de notificação de uma região dinâmica.</returns>
		// Token: 0x06001933 RID: 6451 RVA: 0x00063B78 File Offset: 0x00062F78
		protected virtual AutomationLiveSetting GetLiveSettingCore()
		{
			return AutomationLiveSetting.Off;
		}

		/// <summary>Quando substituído em uma classe derivada, fornece à Automação da Interface do Usuário uma lista de elementos afetados ou controlados por este <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</summary>
		/// <returns>Uma lista de pares de automação para os elementos controlados.</returns>
		// Token: 0x06001934 RID: 6452 RVA: 0x00063B88 File Offset: 0x00062F88
		protected virtual List<AutomationPeer> GetControlledPeersCore()
		{
			return null;
		}

		/// <summary>Quando substituído em uma classe derivada, fornece à Automação da Interface do Usuário o tamanho do grupo ou do conjunto ao qual esse elemento pertence.</summary>
		/// <returns>Um valor inteiro que descreve o tamanho do grupo ou define aquele ao qual seu elemento pertence.</returns>
		// Token: 0x06001935 RID: 6453 RVA: 0x00063B98 File Offset: 0x00062F98
		protected virtual int GetSizeOfSetCore()
		{
			return -1;
		}

		/// <summary>Quando substituído em uma classe derivada, fornece à Automação da Interface do Usuário um valor inteiro baseado em um que descreve a posição que esse elemento ocupa em um grupo ou um conjunto.</summary>
		/// <returns>Um valor inteiro baseado em um que descreve a posição que esse elemento ocupa em um grupo ou um conjunto.</returns>
		// Token: 0x06001936 RID: 6454 RVA: 0x00063BA8 File Offset: 0x00062FA8
		protected virtual int GetPositionInSetCore()
		{
			return -1;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00063BB8 File Offset: 0x00062FB8
		protected virtual AutomationHeadingLevel GetHeadingLevelCore()
		{
			return AutomationHeadingLevel.None;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00063BC8 File Offset: 0x00062FC8
		internal virtual Rect GetVisibleBoundingRectCore()
		{
			return this.GetBoundingRectangle();
		}

		/// <summary>Obtém o objeto <see cref="T:System.Windows.Rect" /> que representa as coordenadas de tela do elemento associado ao par de automação.</summary>
		/// <returns>O retângulo delimitador.</returns>
		// Token: 0x06001939 RID: 6457 RVA: 0x00063BDC File Offset: 0x00062FDC
		public Rect GetBoundingRectangle()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				this._boundingRectangle = this.GetBoundingRectangleCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return this._boundingRectangle;
		}

		/// <summary>Obtém um valor que indica se um elemento está fora da tela.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento não estiver na tela; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600193A RID: 6458 RVA: 0x00063C44 File Offset: 0x00063044
		public bool IsOffscreen()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				this._isOffscreen = this.IsOffscreenCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return this._isOffscreen;
		}

		/// <summary>Obtém um valor que indica a orientação explícita do controle, se houver.</summary>
		/// <returns>A orientação do controle.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600193B RID: 6459 RVA: 0x00063CAC File Offset: 0x000630AC
		public AutomationOrientation GetOrientation()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			AutomationOrientation orientationCore;
			try
			{
				this._publicCallInProgress = true;
				orientationCore = this.GetOrientationCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return orientationCore;
		}

		/// <summary>Obtém uma cadeia de caracteres que descreve que tipo de item um objeto representa.</summary>
		/// <returns>O tipo de item.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600193C RID: 6460 RVA: 0x00063D08 File Offset: 0x00063108
		public string GetItemType()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			string itemTypeCore;
			try
			{
				this._publicCallInProgress = true;
				itemTypeCore = this.GetItemTypeCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return itemTypeCore;
		}

		/// <summary>Obtém um nome que é usado com <see cref="T:System.Windows.Automation.Peers.AutomationControlType" /> para diferenciar o controle representado por esse <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</summary>
		/// <returns>O nome de classe.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600193D RID: 6461 RVA: 0x00063D64 File Offset: 0x00063164
		public string GetClassName()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			string classNameCore;
			try
			{
				this._publicCallInProgress = true;
				classNameCore = this.GetClassNameCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return classNameCore;
		}

		/// <summary>Obtém o texto que transmite o status visual do elemento associado a esse par de automação.</summary>
		/// <returns>O status.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600193E RID: 6462 RVA: 0x00063DC0 File Offset: 0x000631C0
		public string GetItemStatus()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				this._itemStatus = this.GetItemStatusCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return this._itemStatus;
		}

		/// <summary>Obtém um valor que indica se o elemento associado a esse par deve ser preenchido em um formulário.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento precisar ser preenchido; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600193F RID: 6463 RVA: 0x00063E28 File Offset: 0x00063228
		public bool IsRequiredForForm()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			bool result;
			try
			{
				this._publicCallInProgress = true;
				result = this.IsRequiredForFormCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Obtém um valor que indica se o elemento pode aceitar o foco do teclado.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento puder aceitar o foco do teclado; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001940 RID: 6464 RVA: 0x00063E84 File Offset: 0x00063284
		public bool IsKeyboardFocusable()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			bool result;
			try
			{
				this._publicCallInProgress = true;
				result = this.IsKeyboardFocusableCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Obtém um valor que indica se o elemento associado a esse par de automação atualmente tem o foco do teclado.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver o foco do teclado; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001941 RID: 6465 RVA: 0x00063EE0 File Offset: 0x000632E0
		public bool HasKeyboardFocus()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			bool result;
			try
			{
				this._publicCallInProgress = true;
				result = this.HasKeyboardFocusCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Obtém um valor que indica se o elemento associado a esse par de automação é compatível com interação.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for compatível com interação; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001942 RID: 6466 RVA: 0x00063F3C File Offset: 0x0006333C
		public bool IsEnabled()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				this._isEnabled = this.IsEnabledCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return this._isEnabled;
		}

		/// <summary>Obtém um valor que indica se há conteúdo confidencial no elemento.</summary>
		/// <returns>
		///   <see langword="true" /> se houver conteúdo confidencial no elemento, como uma senha; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001943 RID: 6467 RVA: 0x00063FA4 File Offset: 0x000633A4
		public bool IsPassword()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			bool result;
			try
			{
				this._publicCallInProgress = true;
				result = this.IsPasswordCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Obtém o <see cref="P:System.Windows.Automation.AutomationProperties.AutomationId" /> do elemento associado ao par de automação.</summary>
		/// <returns>O identificador.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001944 RID: 6468 RVA: 0x00064000 File Offset: 0x00063400
		public string GetAutomationId()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			string automationIdCore;
			try
			{
				this._publicCallInProgress = true;
				automationIdCore = this.GetAutomationIdCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return automationIdCore;
		}

		/// <summary>Obtém o texto que descreve o elemento associado a esse par de automação.</summary>
		/// <returns>O nome.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001945 RID: 6469 RVA: 0x0006405C File Offset: 0x0006345C
		public string GetName()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				this._name = this.GetNameCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return this._name;
		}

		/// <summary>Obtém o tipo de controle para o elemento associado ao par de Automação da interface do usuário.</summary>
		/// <returns>O tipo de controle.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001946 RID: 6470 RVA: 0x000640C4 File Offset: 0x000634C4
		public AutomationControlType GetAutomationControlType()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			AutomationControlType automationControlTypeCore;
			try
			{
				this._publicCallInProgress = true;
				automationControlTypeCore = this.GetAutomationControlTypeCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return automationControlTypeCore;
		}

		/// <summary>Obtém uma cadeia de caracteres localizada legível por humanos que representa o valor <see cref="T:System.Windows.Automation.Peers.AutomationControlType" /> para o controle associado a esse par de automação.</summary>
		/// <returns>O tipo do controle.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001947 RID: 6471 RVA: 0x00064120 File Offset: 0x00063520
		public string GetLocalizedControlType()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			string localizedControlTypeCore;
			try
			{
				this._publicCallInProgress = true;
				localizedControlTypeCore = this.GetLocalizedControlTypeCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return localizedControlTypeCore;
		}

		/// <summary>Obtém um valor que indica se o elemento associado a esse par de automação contém dados que são apresentados ao usuário.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for um elemento de conteúdo; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001948 RID: 6472 RVA: 0x0006417C File Offset: 0x0006357C
		public bool IsContentElement()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			bool result;
			try
			{
				this._publicCallInProgress = true;
				result = (this.IsControlElementPrivate() && this.IsContentElementCore());
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Obtém um valor que indica se o elemento é compreendido pelo usuário como interativo ou como contribuindo para a estrutura lógica do controle em GUI.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento for um controle; caso contrário, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001949 RID: 6473 RVA: 0x000641E4 File Offset: 0x000635E4
		public bool IsControlElement()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			bool result;
			try
			{
				this._publicCallInProgress = true;
				result = this.IsControlElementPrivate();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00064240 File Offset: 0x00063640
		private bool IsControlElementPrivate()
		{
			return this.IsControlElementCore();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o <see cref="T:System.Windows.Controls.Label" /> que é o destino do elemento.</summary>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.LabelAutomationPeer" /> para o elemento que é o destino de <see cref="T:System.Windows.Controls.Label" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600194B RID: 6475 RVA: 0x00064254 File Offset: 0x00063654
		public AutomationPeer GetLabeledBy()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			AutomationPeer labeledByCore;
			try
			{
				this._publicCallInProgress = true;
				labeledByCore = this.GetLabeledByCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return labeledByCore;
		}

		/// <summary>Obtém o texto que descreve a funcionalidade do controle associado ao par de automação.</summary>
		/// <returns>O texto de ajuda.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600194C RID: 6476 RVA: 0x000642B0 File Offset: 0x000636B0
		public string GetHelpText()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			string helpTextCore;
			try
			{
				this._publicCallInProgress = true;
				helpTextCore = this.GetHelpTextCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return helpTextCore;
		}

		/// <summary>Obtém as combinações de tecla de aceleração para o elemento associado ao par de Automação da interface do usuário.</summary>
		/// <returns>A tecla de atalho.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600194D RID: 6477 RVA: 0x0006430C File Offset: 0x0006370C
		public string GetAcceleratorKey()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			string acceleratorKeyCore;
			try
			{
				this._publicCallInProgress = true;
				acceleratorKeyCore = this.GetAcceleratorKeyCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return acceleratorKeyCore;
		}

		/// <summary>Obtém a chave de acesso para o elemento associado ao par de automação.</summary>
		/// <returns>A cadeia de caracteres que contém a chave de acesso.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600194E RID: 6478 RVA: 0x00064368 File Offset: 0x00063768
		public string GetAccessKey()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			string accessKeyCore;
			try
			{
				this._publicCallInProgress = true;
				accessKeyCore = this.GetAccessKeyCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return accessKeyCore;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Point" /> no elemento associado ao par de automação que responde a um clique com o mouse.</summary>
		/// <returns>Um ponto na área clicável do elemento.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600194F RID: 6479 RVA: 0x000643C4 File Offset: 0x000637C4
		public Point GetClickablePoint()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			Point result;
			try
			{
				this._publicCallInProgress = true;
				if (this.IsOffscreenCore())
				{
					result = new Point(double.NaN, double.NaN);
				}
				else
				{
					result = this.GetClickablePointCore();
				}
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Define o foco do teclado no elemento associado a esse par de automação.</summary>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001950 RID: 6480 RVA: 0x00064444 File Offset: 0x00063844
		public void SetFocus()
		{
			if (this._publicSetFocusInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicSetFocusInProgress = true;
				this.SetFocusCore();
			}
			finally
			{
				this._publicSetFocusInProgress = false;
			}
		}

		/// <summary>Obtém as características de notificação de uma região dinâmica associada a esse par de automação.</summary>
		/// <returns>As características de notificação.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001951 RID: 6481 RVA: 0x000644A0 File Offset: 0x000638A0
		public AutomationLiveSetting GetLiveSetting()
		{
			AutomationLiveSetting result = AutomationLiveSetting.Off;
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				result = this.GetLiveSettingCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Fornece à Automação da Interface do Usuário uma lista de elementos afetados ou controlados por este <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</summary>
		/// <returns>Uma lista de pares de automação para os elementos controlados.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001952 RID: 6482 RVA: 0x00064500 File Offset: 0x00063900
		public List<AutomationPeer> GetControlledPeers()
		{
			List<AutomationPeer> result = null;
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				result = this.GetControlledPeersCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00064560 File Offset: 0x00063960
		private IRawElementProviderSimple[] GetControllerForProviderArray()
		{
			List<AutomationPeer> controlledPeers = this.GetControlledPeers();
			IRawElementProviderSimple[] array = null;
			if (controlledPeers != null)
			{
				array = new IRawElementProviderSimple[controlledPeers.Count];
				for (int i = 0; i < controlledPeers.Count; i++)
				{
					array[i] = this.ProviderFromPeer(controlledPeers[i]);
				}
			}
			return array;
		}

		/// <summary>Tenta obter o valor da propriedade <see cref="P:System.Windows.Automation.AutomationProperties.SizeOfSet" />.</summary>
		/// <returns>O valor da propriedade <see cref="P:System.Windows.Automation.AutomationProperties.SizeOfSet" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001954 RID: 6484 RVA: 0x000645A8 File Offset: 0x000639A8
		public int GetSizeOfSet()
		{
			int result = -1;
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				result = this.GetSizeOfSetCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00064608 File Offset: 0x00063A08
		public AutomationHeadingLevel GetHeadingLevel()
		{
			AutomationHeadingLevel result = AutomationHeadingLevel.None;
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				result = this.GetHeadingLevelCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00064668 File Offset: 0x00063A68
		private static AutomationPeer.HeadingLevel ConvertHeadingLevelToId(AutomationHeadingLevel value)
		{
			switch (value)
			{
			case AutomationHeadingLevel.None:
				return AutomationPeer.HeadingLevel.None;
			case AutomationHeadingLevel.Level1:
				return AutomationPeer.HeadingLevel.Level1;
			case AutomationHeadingLevel.Level2:
				return AutomationPeer.HeadingLevel.Level2;
			case AutomationHeadingLevel.Level3:
				return AutomationPeer.HeadingLevel.Level3;
			case AutomationHeadingLevel.Level4:
				return AutomationPeer.HeadingLevel.Level4;
			case AutomationHeadingLevel.Level5:
				return AutomationPeer.HeadingLevel.Level5;
			case AutomationHeadingLevel.Level6:
				return AutomationPeer.HeadingLevel.Level6;
			case AutomationHeadingLevel.Level7:
				return AutomationPeer.HeadingLevel.Level7;
			case AutomationHeadingLevel.Level8:
				return AutomationPeer.HeadingLevel.Level8;
			case AutomationHeadingLevel.Level9:
				return AutomationPeer.HeadingLevel.Level9;
			default:
				return AutomationPeer.HeadingLevel.None;
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000646E8 File Offset: 0x00063AE8
		public bool IsDialog()
		{
			bool result = false;
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				result = this.IsDialogCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Tenta obter o valor da propriedade <see cref="P:System.Windows.Automation.AutomationProperties.PositionInSet" />.</summary>
		/// <returns>O valor da propriedade <see cref="P:System.Windows.Automation.AutomationProperties.PositionInSet" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x06001958 RID: 6488 RVA: 0x00064748 File Offset: 0x00063B48
		public int GetPositionInSet()
		{
			int result = -1;
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				result = this.GetPositionInSetCore();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return result;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> que é o pai deste <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</summary>
		/// <returns>O par de automação do pai.</returns>
		// Token: 0x06001959 RID: 6489 RVA: 0x000647A8 File Offset: 0x00063BA8
		public AutomationPeer GetParent()
		{
			return this._parent;
		}

		/// <summary>Obtém a coleção de elementos <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetChildren" /> representados na árvore de Automação da Interface do Usuário como elementos filho imediatos do par de automação.</summary>
		/// <returns>A coleção de elementos filho.</returns>
		/// <exception cref="T:System.InvalidOperationException">Uma chamada pública a esse método está atualmente em andamento.</exception>
		// Token: 0x0600195A RID: 6490 RVA: 0x000647BC File Offset: 0x00063BBC
		public List<AutomationPeer> GetChildren()
		{
			if (this._publicCallInProgress)
			{
				throw new InvalidOperationException(SR.Get("Automation_RecursivePublicCall"));
			}
			try
			{
				this._publicCallInProgress = true;
				this.EnsureChildren();
			}
			finally
			{
				this._publicCallInProgress = false;
			}
			return this._children;
		}

		/// <summary>Redefine de maneira síncrona a árvore de elementos filho chamando <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetChildrenCore" />.</summary>
		// Token: 0x0600195B RID: 6491 RVA: 0x0006481C File Offset: 0x00063C1C
		public void ResetChildrenCache()
		{
			using (this.UpdateChildren())
			{
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00064858 File Offset: 0x00063C58
		internal int[] GetRuntimeId()
		{
			return new int[]
			{
				7,
				SafeNativeMethods.GetCurrentProcessId(),
				this.GetHashCode()
			};
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00064880 File Offset: 0x00063C80
		internal string GetFrameworkId()
		{
			return "WPF";
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00064894 File Offset: 0x00063C94
		internal AutomationPeer GetFirstChild()
		{
			AutomationPeer automationPeer = null;
			this.EnsureChildren();
			if (this._children != null && this._children.Count > 0)
			{
				automationPeer = this._children[0];
				automationPeer.ChooseIterationParent(this);
			}
			return automationPeer;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000648D4 File Offset: 0x00063CD4
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private void EnsureChildren()
		{
			if (!this._childrenValid || this._ancestorsInvalid)
			{
				this._children = this.GetChildrenCore();
				if (this._children != null)
				{
					int count = this._children.Count;
					for (int i = 0; i < count; i++)
					{
						this._children[i]._parent = this;
						this._children[i]._index = i;
						this._children[i]._hwnd = this._hwnd;
					}
				}
				this._childrenValid = true;
			}
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x00064960 File Offset: 0x00063D60
		internal void ForceEnsureChildren()
		{
			this._childrenValid = false;
			this.EnsureChildren();
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0006497C File Offset: 0x00063D7C
		internal AutomationPeer GetLastChild()
		{
			AutomationPeer automationPeer = null;
			this.EnsureChildren();
			if (this._children != null && this._children.Count > 0)
			{
				automationPeer = this._children[this._children.Count - 1];
				automationPeer.ChooseIterationParent(this);
			}
			return automationPeer;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000649C8 File Offset: 0x00063DC8
		[FriendAccessAllowed]
		internal virtual InteropAutomationProvider GetInteropChild()
		{
			return null;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000649D8 File Offset: 0x00063DD8
		internal AutomationPeer GetNextSibling()
		{
			AutomationPeer automationPeer = null;
			AutomationPeer iterationParent = this.IterationParent;
			if (iterationParent != null)
			{
				iterationParent.EnsureChildren();
				if (iterationParent._children != null && this._index >= 0 && this._index + 1 < iterationParent._children.Count && iterationParent._children[this._index] == this)
				{
					automationPeer = iterationParent._children[this._index + 1];
					automationPeer.IterationParent = iterationParent;
				}
			}
			return automationPeer;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00064A50 File Offset: 0x00063E50
		internal AutomationPeer GetPreviousSibling()
		{
			AutomationPeer automationPeer = null;
			AutomationPeer iterationParent = this.IterationParent;
			if (iterationParent != null)
			{
				iterationParent.EnsureChildren();
				if (iterationParent._children != null && this._index - 1 >= 0 && this._index < iterationParent._children.Count && iterationParent._children[this._index] == this)
				{
					automationPeer = iterationParent._children[this._index - 1];
					automationPeer.IterationParent = iterationParent;
				}
			}
			return automationPeer;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00064AC8 File Offset: 0x00063EC8
		private void ChooseIterationParent(AutomationPeer caller)
		{
			AutomationPeer iterationParent;
			if (this._parent == caller || this._parent == null)
			{
				iterationParent = this._parent;
			}
			else
			{
				this._parent.EnsureChildren();
				iterationParent = ((this._parent._children == null || this._parent._children.Count == caller._children.Count) ? this._parent : caller);
			}
			this.IterationParent = iterationParent;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00064B38 File Offset: 0x00063F38
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x00064B64 File Offset: 0x00063F64
		private AutomationPeer IterationParent
		{
			get
			{
				if (this._hasIterationParent)
				{
					return ((AutomationPeer.PeerRecord)this._eventsSourceOrPeerRecord).IterationParent;
				}
				return this._parent;
			}
			set
			{
				if (value == this._parent)
				{
					if (this._hasIterationParent)
					{
						this._eventsSourceOrPeerRecord = this.EventsSource;
						this._hasIterationParent = false;
						return;
					}
				}
				else
				{
					if (!this._hasIterationParent)
					{
						AutomationPeer.PeerRecord eventsSourceOrPeerRecord = new AutomationPeer.PeerRecord
						{
							EventsSource = this.EventsSource,
							IterationParent = value
						};
						this._eventsSourceOrPeerRecord = eventsSourceOrPeerRecord;
						this._hasIterationParent = true;
						return;
					}
					((AutomationPeer.PeerRecord)this._eventsSourceOrPeerRecord).IterationParent = value;
				}
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00064BD8 File Offset: 0x00063FD8
		internal ControlType GetControlType()
		{
			ControlType result = null;
			switch (this.GetAutomationControlTypeCore())
			{
			case AutomationControlType.Button:
				result = ControlType.Button;
				break;
			case AutomationControlType.Calendar:
				result = ControlType.Calendar;
				break;
			case AutomationControlType.CheckBox:
				result = ControlType.CheckBox;
				break;
			case AutomationControlType.ComboBox:
				result = ControlType.ComboBox;
				break;
			case AutomationControlType.Edit:
				result = ControlType.Edit;
				break;
			case AutomationControlType.Hyperlink:
				result = ControlType.Hyperlink;
				break;
			case AutomationControlType.Image:
				result = ControlType.Image;
				break;
			case AutomationControlType.ListItem:
				result = ControlType.ListItem;
				break;
			case AutomationControlType.List:
				result = ControlType.List;
				break;
			case AutomationControlType.Menu:
				result = ControlType.Menu;
				break;
			case AutomationControlType.MenuBar:
				result = ControlType.MenuBar;
				break;
			case AutomationControlType.MenuItem:
				result = ControlType.MenuItem;
				break;
			case AutomationControlType.ProgressBar:
				result = ControlType.ProgressBar;
				break;
			case AutomationControlType.RadioButton:
				result = ControlType.RadioButton;
				break;
			case AutomationControlType.ScrollBar:
				result = ControlType.ScrollBar;
				break;
			case AutomationControlType.Slider:
				result = ControlType.Slider;
				break;
			case AutomationControlType.Spinner:
				result = ControlType.Spinner;
				break;
			case AutomationControlType.StatusBar:
				result = ControlType.StatusBar;
				break;
			case AutomationControlType.Tab:
				result = ControlType.Tab;
				break;
			case AutomationControlType.TabItem:
				result = ControlType.TabItem;
				break;
			case AutomationControlType.Text:
				result = ControlType.Text;
				break;
			case AutomationControlType.ToolBar:
				result = ControlType.ToolBar;
				break;
			case AutomationControlType.ToolTip:
				result = ControlType.ToolTip;
				break;
			case AutomationControlType.Tree:
				result = ControlType.Tree;
				break;
			case AutomationControlType.TreeItem:
				result = ControlType.TreeItem;
				break;
			case AutomationControlType.Custom:
				result = ControlType.Custom;
				break;
			case AutomationControlType.Group:
				result = ControlType.Group;
				break;
			case AutomationControlType.Thumb:
				result = ControlType.Thumb;
				break;
			case AutomationControlType.DataGrid:
				result = ControlType.DataGrid;
				break;
			case AutomationControlType.DataItem:
				result = ControlType.DataItem;
				break;
			case AutomationControlType.Document:
				result = ControlType.Document;
				break;
			case AutomationControlType.SplitButton:
				result = ControlType.SplitButton;
				break;
			case AutomationControlType.Window:
				result = ControlType.Window;
				break;
			case AutomationControlType.Pane:
				result = ControlType.Pane;
				break;
			case AutomationControlType.Header:
				result = ControlType.Header;
				break;
			case AutomationControlType.HeaderItem:
				result = ControlType.HeaderItem;
				break;
			case AutomationControlType.Table:
				result = ControlType.Table;
				break;
			case AutomationControlType.TitleBar:
				result = ControlType.TitleBar;
				break;
			case AutomationControlType.Separator:
				result = ControlType.Separator;
				break;
			}
			return result;
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> do ponto especificado.</summary>
		/// <param name="point">A posição na tela da qual obter o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</param>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> no ponto especificado.</returns>
		// Token: 0x06001969 RID: 6505 RVA: 0x00064E14 File Offset: 0x00064214
		public AutomationPeer GetPeerFromPoint(Point point)
		{
			return this.GetPeerFromPointCore(point);
		}

		/// <summary>Quando substituído em uma classe derivada, é chamado de <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetPeerFromPoint(System.Windows.Point)" />.</summary>
		/// <param name="point">A posição na tela da qual obter o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</param>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> no ponto especificado.</returns>
		// Token: 0x0600196A RID: 6506 RVA: 0x00064E28 File Offset: 0x00064228
		protected virtual AutomationPeer GetPeerFromPointCore(Point point)
		{
			AutomationPeer automationPeer = null;
			if (!this.IsOffscreen())
			{
				List<AutomationPeer> children = this.GetChildren();
				if (children != null)
				{
					int count = children.Count;
					int num = count - 1;
					while (num >= 0 && automationPeer == null)
					{
						automationPeer = children[num].GetPeerFromPoint(point);
						num--;
					}
				}
				if (automationPeer == null && this.GetVisibleBoundingRect().Contains(point))
				{
					automationPeer = this;
				}
			}
			return automationPeer;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00064E88 File Offset: 0x00064288
		internal Rect GetVisibleBoundingRect()
		{
			return this.GetVisibleBoundingRectCore();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Automation.Provider.IRawElementProviderSimple" /> para o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> especificado.</summary>
		/// <param name="peer">O par de automação.</param>
		/// <returns>O proxy.</returns>
		// Token: 0x0600196C RID: 6508 RVA: 0x00064E9C File Offset: 0x0006429C
		protected internal IRawElementProviderSimple ProviderFromPeer(AutomationPeer peer)
		{
			AutomationPeer referencePeer = this;
			AutomationPeer eventsSource;
			if (peer == this && (eventsSource = this.EventsSource) != null)
			{
				peer = (referencePeer = eventsSource);
			}
			return ElementProxy.StaticWrap(peer, referencePeer);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00064EC8 File Offset: 0x000642C8
		private IRawElementProviderSimple ProviderFromPeerNoDelegation(AutomationPeer peer)
		{
			return ElementProxy.StaticWrap(peer, this);
		}

		/// <summary>Obtém ou define um <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> que é relatado ao cliente de automação, como uma origem para todos os eventos que vêm deste <see cref="T:System.Windows.Automation.Peers.AutomationPeer" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> que é a origem de eventos.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00064EE0 File Offset: 0x000642E0
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x00064F14 File Offset: 0x00064314
		public AutomationPeer EventsSource
		{
			get
			{
				if (this._hasIterationParent)
				{
					return ((AutomationPeer.PeerRecord)this._eventsSourceOrPeerRecord).EventsSource;
				}
				return (AutomationPeer)this._eventsSourceOrPeerRecord;
			}
			set
			{
				if (!this._hasIterationParent)
				{
					this._eventsSourceOrPeerRecord = value;
					return;
				}
				((AutomationPeer.PeerRecord)this._eventsSourceOrPeerRecord).EventsSource = value;
			}
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o proxy <see cref="T:System.Windows.Automation.Provider.IRawElementProviderSimple" /> especificado.</summary>
		/// <param name="provider">A classe que implementa <see cref="T:System.Windows.Automation.Provider.IRawElementProviderSimple" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o proxy <see cref="T:System.Windows.Automation.Provider.IRawElementProviderSimple" /> especificado.</returns>
		// Token: 0x06001970 RID: 6512 RVA: 0x00064F44 File Offset: 0x00064344
		protected AutomationPeer PeerFromProvider(IRawElementProviderSimple provider)
		{
			ElementProxy elementProxy = provider as ElementProxy;
			if (elementProxy != null)
			{
				return elementProxy.Peer;
			}
			return null;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00064F64 File Offset: 0x00064364
		internal void FireAutomationEvents()
		{
			this.UpdateSubtree();
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00064F78 File Offset: 0x00064378
		private void RaisePropertyChangedInternal(IRawElementProviderSimple provider, AutomationProperty propertyId, object oldValue, object newValue)
		{
			if (provider != null && EventMap.HasRegisteredEvent(AutomationEvents.PropertyChanged))
			{
				AutomationPropertyChangedEventArgs e = new AutomationPropertyChangedEventArgs(propertyId, oldValue, newValue);
				AutomationInteropProvider.RaiseAutomationPropertyChangedEvent(provider, e);
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00064FA4 File Offset: 0x000643A4
		internal void UpdateChildrenInternal(int invalidateLimit)
		{
			List<AutomationPeer> children = this._children;
			List<AutomationPeer> list = null;
			Hashtable hashtable = null;
			this._childrenValid = false;
			this.EnsureChildren();
			if (!EventMap.HasRegisteredEvent(AutomationEvents.StructureChanged))
			{
				return;
			}
			if (children != null)
			{
				hashtable = new Hashtable();
				int count = children.Count;
				for (int i = 0; i < count; i++)
				{
					if (!hashtable.Contains(children[i]))
					{
						hashtable.Add(children[i], null);
					}
				}
			}
			int num = 0;
			if (this._children != null)
			{
				int count2 = this._children.Count;
				for (int j = 0; j < count2; j++)
				{
					AutomationPeer automationPeer = this._children[j];
					if (hashtable != null && hashtable.ContainsKey(automationPeer))
					{
						hashtable.Remove(automationPeer);
					}
					else
					{
						if (list == null)
						{
							list = new List<AutomationPeer>();
						}
						num++;
						if (num <= invalidateLimit)
						{
							list.Add(automationPeer);
						}
					}
				}
			}
			int num2 = (hashtable == null) ? 0 : hashtable.Count;
			if (num2 + num > invalidateLimit)
			{
				StructureChangeType structureChangeType;
				if (num == 0)
				{
					structureChangeType = StructureChangeType.ChildrenBulkRemoved;
				}
				else if (num2 == 0)
				{
					structureChangeType = StructureChangeType.ChildrenBulkAdded;
				}
				else
				{
					structureChangeType = StructureChangeType.ChildrenInvalidated;
				}
				IRawElementProviderSimple rawElementProviderSimple = this.ProviderFromPeerNoDelegation(this);
				if (rawElementProviderSimple != null)
				{
					int[] runtimeId = this.GetRuntimeId();
					AutomationInteropProvider.RaiseStructureChangedEvent(rawElementProviderSimple, new StructureChangedEventArgs(structureChangeType, runtimeId));
					return;
				}
			}
			else
			{
				if (num2 > 0)
				{
					IRawElementProviderSimple rawElementProviderSimple2 = this.ProviderFromPeerNoDelegation(this);
					if (rawElementProviderSimple2 != null)
					{
						foreach (object obj in hashtable.Keys)
						{
							AutomationPeer automationPeer2 = (AutomationPeer)obj;
							int[] runtimeId2 = automationPeer2.GetRuntimeId();
							AutomationInteropProvider.RaiseStructureChangedEvent(rawElementProviderSimple2, new StructureChangedEventArgs(StructureChangeType.ChildRemoved, runtimeId2));
						}
					}
				}
				if (num > 0)
				{
					foreach (AutomationPeer automationPeer3 in list)
					{
						IRawElementProviderSimple rawElementProviderSimple3 = this.ProviderFromPeerNoDelegation(automationPeer3);
						if (rawElementProviderSimple3 != null)
						{
							int[] runtimeId3 = automationPeer3.GetRuntimeId();
							AutomationInteropProvider.RaiseStructureChangedEvent(rawElementProviderSimple3, new StructureChangedEventArgs(StructureChangeType.ChildAdded, runtimeId3));
						}
					}
				}
			}
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000651C4 File Offset: 0x000645C4
		internal virtual IDisposable UpdateChildren()
		{
			this.UpdateChildrenInternal(20);
			return null;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000651DC File Offset: 0x000645DC
		[FriendAccessAllowed]
		internal void UpdateSubtree()
		{
			ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(base.Dispatcher);
			if (contextLayoutManager != null)
			{
				contextLayoutManager.AutomationSyncUpdateCounter++;
				try
				{
					IRawElementProviderSimple rawElementProviderSimple = null;
					bool flag = EventMap.HasRegisteredEvent(AutomationEvents.PropertyChanged);
					bool flag2 = EventMap.HasRegisteredEvent(AutomationEvents.StructureChanged);
					if (flag)
					{
						string itemStatusCore = this.GetItemStatusCore();
						if (itemStatusCore != this._itemStatus)
						{
							if (rawElementProviderSimple == null)
							{
								rawElementProviderSimple = this.ProviderFromPeerNoDelegation(this);
							}
							this.RaisePropertyChangedInternal(rawElementProviderSimple, AutomationElementIdentifiers.ItemStatusProperty, this._itemStatus, itemStatusCore);
							this._itemStatus = itemStatusCore;
						}
						string nameCore = this.GetNameCore();
						if (nameCore != this._name)
						{
							if (rawElementProviderSimple == null)
							{
								rawElementProviderSimple = this.ProviderFromPeerNoDelegation(this);
							}
							this.RaisePropertyChangedInternal(rawElementProviderSimple, AutomationElementIdentifiers.NameProperty, this._name, nameCore);
							this._name = nameCore;
						}
						bool flag3 = this.IsOffscreenCore();
						if (flag3 != this._isOffscreen)
						{
							if (rawElementProviderSimple == null)
							{
								rawElementProviderSimple = this.ProviderFromPeerNoDelegation(this);
							}
							this.RaisePropertyChangedInternal(rawElementProviderSimple, AutomationElementIdentifiers.IsOffscreenProperty, this._isOffscreen, flag3);
							this._isOffscreen = flag3;
						}
						bool flag4 = this.IsEnabledCore();
						if (flag4 != this._isEnabled)
						{
							if (rawElementProviderSimple == null)
							{
								rawElementProviderSimple = this.ProviderFromPeerNoDelegation(this);
							}
							this.RaisePropertyChangedInternal(rawElementProviderSimple, AutomationElementIdentifiers.IsEnabledProperty, this._isEnabled, flag4);
							this._isEnabled = flag4;
						}
					}
					if (this._childrenValid ? (this.AncestorsInvalid || ControlType.Custom == this.GetControlType()) : (flag2 || flag))
					{
						using (this.UpdateChildren())
						{
							this.AncestorsInvalid = false;
							for (AutomationPeer automationPeer = this.GetFirstChild(); automationPeer != null; automationPeer = automationPeer.GetNextSibling())
							{
								automationPeer.UpdateSubtree();
							}
						}
					}
					this.AncestorsInvalid = false;
					this._invalidated = false;
				}
				finally
				{
					contextLayoutManager.AutomationSyncUpdateCounter--;
				}
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000653D4 File Offset: 0x000647D4
		internal void InvalidateAncestorsRecursive()
		{
			if (!this.AncestorsInvalid)
			{
				this.AncestorsInvalid = true;
				if (this.EventsSource != null)
				{
					this.EventsSource.InvalidateAncestorsRecursive();
				}
				if (this._parent != null)
				{
					this._parent.InvalidateAncestorsRecursive();
				}
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00065418 File Offset: 0x00064818
		private static object UpdatePeer(object arg)
		{
			AutomationPeer automationPeer = (AutomationPeer)arg;
			if (!automationPeer.IgnoreUpdatePeer())
			{
				automationPeer.UpdateSubtree();
			}
			return null;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0006543C File Offset: 0x0006483C
		internal void AddToAutomationEventList()
		{
			if (!this._addedToEventList)
			{
				ContextLayoutManager contextLayoutManager = ContextLayoutManager.From(base.Dispatcher);
				contextLayoutManager.AutomationEvents.Add(this);
				this._addedToEventList = true;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001979 RID: 6521 RVA: 0x00065474 File Offset: 0x00064874
		// (set) Token: 0x0600197A RID: 6522 RVA: 0x00065488 File Offset: 0x00064888
		internal IntPtr Hwnd
		{
			[SecurityCritical]
			get
			{
				return this._hwnd;
			}
			[SecurityCritical]
			set
			{
				this._hwnd = value;
			}
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0006549C File Offset: 0x0006489C
		internal object GetWrappedPattern(int patternId)
		{
			object result = null;
			AutomationPeer.PatternInfo patternInfo = (AutomationPeer.PatternInfo)AutomationPeer.s_patternInfo[patternId];
			if (patternInfo != null)
			{
				object pattern = this.GetPattern(patternInfo.PatternInterface);
				if (pattern != null)
				{
					result = patternInfo.WrapObject(this, pattern);
				}
			}
			return result;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000654E4 File Offset: 0x000648E4
		internal object GetPropertyValue(int propertyId)
		{
			object obj = null;
			AutomationPeer.GetProperty getProperty = (AutomationPeer.GetProperty)AutomationPeer.s_propertyInfo[propertyId];
			if (getProperty != null)
			{
				obj = getProperty(this);
				if (AutomationElementIdentifiers.HeadingLevelProperty != null && propertyId == AutomationElementIdentifiers.HeadingLevelProperty.Id)
				{
					obj = AutomationPeer.ConvertHeadingLevelToId((AutomationHeadingLevel)obj);
				}
			}
			return obj;
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0006553C File Offset: 0x0006493C
		// (set) Token: 0x0600197E RID: 6526 RVA: 0x00065550 File Offset: 0x00064950
		internal virtual bool AncestorsInvalid
		{
			get
			{
				return this._ancestorsInvalid;
			}
			set
			{
				this._ancestorsInvalid = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x00065564 File Offset: 0x00064964
		// (set) Token: 0x06001980 RID: 6528 RVA: 0x00065578 File Offset: 0x00064978
		internal bool ChildrenValid
		{
			get
			{
				return this._childrenValid;
			}
			set
			{
				this._childrenValid = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x0006558C File Offset: 0x0006498C
		// (set) Token: 0x06001982 RID: 6530 RVA: 0x000655A0 File Offset: 0x000649A0
		internal bool IsInteropPeer
		{
			get
			{
				return this._isInteropPeer;
			}
			set
			{
				this._isInteropPeer = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x000655B4 File Offset: 0x000649B4
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x000655C8 File Offset: 0x000649C8
		internal List<AutomationPeer> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x000655DC File Offset: 0x000649DC
		// (set) Token: 0x06001986 RID: 6534 RVA: 0x000655F0 File Offset: 0x000649F0
		internal WeakReference ElementProxyWeakReference
		{
			get
			{
				return this._elementProxyWeakReference;
			}
			set
			{
				if (value.Target is ElementProxy)
				{
					this._elementProxyWeakReference = value;
				}
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x00065614 File Offset: 0x00064A14
		internal bool IncludeInvisibleElementsInControlView
		{
			get
			{
				return AccessibilitySwitches.UseNetFx472CompatibleAccessibilityFeatures;
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00065628 File Offset: 0x00064A28
		private static void Initialize()
		{
			AutomationPeer.s_patternInfo = new Hashtable();
			AutomationPeer.s_patternInfo[InvokePatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(InvokePatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(InvokeProviderWrapper.Wrap), PatternInterface.Invoke);
			AutomationPeer.s_patternInfo[SelectionPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(SelectionPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(SelectionProviderWrapper.Wrap), PatternInterface.Selection);
			AutomationPeer.s_patternInfo[ValuePatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(ValuePatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(ValueProviderWrapper.Wrap), PatternInterface.Value);
			AutomationPeer.s_patternInfo[RangeValuePatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(RangeValuePatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(RangeValueProviderWrapper.Wrap), PatternInterface.RangeValue);
			AutomationPeer.s_patternInfo[ScrollPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(ScrollPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(ScrollProviderWrapper.Wrap), PatternInterface.Scroll);
			AutomationPeer.s_patternInfo[ScrollItemPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(ScrollItemPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(ScrollItemProviderWrapper.Wrap), PatternInterface.ScrollItem);
			AutomationPeer.s_patternInfo[ExpandCollapsePatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(ExpandCollapsePatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(ExpandCollapseProviderWrapper.Wrap), PatternInterface.ExpandCollapse);
			AutomationPeer.s_patternInfo[GridPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(GridPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(GridProviderWrapper.Wrap), PatternInterface.Grid);
			AutomationPeer.s_patternInfo[GridItemPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(GridItemPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(GridItemProviderWrapper.Wrap), PatternInterface.GridItem);
			AutomationPeer.s_patternInfo[MultipleViewPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(MultipleViewPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(MultipleViewProviderWrapper.Wrap), PatternInterface.MultipleView);
			AutomationPeer.s_patternInfo[WindowPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(WindowPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(WindowProviderWrapper.Wrap), PatternInterface.Window);
			AutomationPeer.s_patternInfo[SelectionItemPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(SelectionItemPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(SelectionItemProviderWrapper.Wrap), PatternInterface.SelectionItem);
			AutomationPeer.s_patternInfo[DockPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(DockPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(DockProviderWrapper.Wrap), PatternInterface.Dock);
			AutomationPeer.s_patternInfo[TablePatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(TablePatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(TableProviderWrapper.Wrap), PatternInterface.Table);
			AutomationPeer.s_patternInfo[TableItemPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(TableItemPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(TableItemProviderWrapper.Wrap), PatternInterface.TableItem);
			AutomationPeer.s_patternInfo[TogglePatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(TogglePatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(ToggleProviderWrapper.Wrap), PatternInterface.Toggle);
			AutomationPeer.s_patternInfo[TransformPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(TransformPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(TransformProviderWrapper.Wrap), PatternInterface.Transform);
			AutomationPeer.s_patternInfo[TextPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(TextPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(TextProviderWrapper.Wrap), PatternInterface.Text);
			if (VirtualizedItemPatternIdentifiers.Pattern != null)
			{
				AutomationPeer.s_patternInfo[VirtualizedItemPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(VirtualizedItemPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(VirtualizedItemProviderWrapper.Wrap), PatternInterface.VirtualizedItem);
			}
			if (ItemContainerPatternIdentifiers.Pattern != null)
			{
				AutomationPeer.s_patternInfo[ItemContainerPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(ItemContainerPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(ItemContainerProviderWrapper.Wrap), PatternInterface.ItemContainer);
			}
			if (SynchronizedInputPatternIdentifiers.Pattern != null)
			{
				AutomationPeer.s_patternInfo[SynchronizedInputPatternIdentifiers.Pattern.Id] = new AutomationPeer.PatternInfo(SynchronizedInputPatternIdentifiers.Pattern.Id, new AutomationPeer.WrapObject(SynchronizedInputProviderWrapper.Wrap), PatternInterface.SynchronizedInput);
			}
			AutomationPeer.s_propertyInfo = new Hashtable();
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsControlElementProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsControlElement);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.ControlTypeProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetControlType);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsContentElementProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsContentElement);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.LabeledByProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetLabeledBy);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.NativeWindowHandleProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetNativeWindowHandle);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.AutomationIdProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetAutomationId);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.ItemTypeProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetItemType);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsPasswordProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsPassword);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.LocalizedControlTypeProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetLocalizedControlType);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.NameProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetName);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.AcceleratorKeyProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetAcceleratorKey);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.AccessKeyProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetAccessKey);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.HasKeyboardFocusProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.HasKeyboardFocus);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsKeyboardFocusableProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsKeyboardFocusable);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsEnabledProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsEnabled);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.BoundingRectangleProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetBoundingRectangle);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.ProcessIdProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetCurrentProcessId);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.RuntimeIdProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetRuntimeId);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.ClassNameProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetClassName);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.HelpTextProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetHelpText);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.ClickablePointProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetClickablePoint);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.CultureProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetCultureInfo);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsOffscreenProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsOffscreen);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.OrientationProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetOrientation);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.FrameworkIdProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetFrameworkId);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsRequiredForFormProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsRequiredForForm);
			AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.ItemStatusProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetItemStatus);
			if (!AccessibilitySwitches.UseNetFx47CompatibleAccessibilityFeatures && AutomationElementIdentifiers.LiveSettingProperty != null)
			{
				AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.LiveSettingProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetLiveSetting);
			}
			if (!AccessibilitySwitches.UseNetFx472CompatibleAccessibilityFeatures && AutomationElementIdentifiers.ControllerForProperty != null)
			{
				AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.ControllerForProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetControllerFor);
			}
			if (!AccessibilitySwitches.UseNetFx472CompatibleAccessibilityFeatures && AutomationElementIdentifiers.SizeOfSetProperty != null)
			{
				AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.SizeOfSetProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetSizeOfSet);
			}
			if (!AccessibilitySwitches.UseNetFx472CompatibleAccessibilityFeatures && AutomationElementIdentifiers.PositionInSetProperty != null)
			{
				AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.PositionInSetProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetPositionInSet);
			}
			if (AutomationElementIdentifiers.HeadingLevelProperty != null)
			{
				AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.HeadingLevelProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.GetHeadingLevel);
			}
			if (AutomationElementIdentifiers.IsDialogProperty != null)
			{
				AutomationPeer.s_propertyInfo[AutomationElementIdentifiers.IsDialogProperty.Id] = new AutomationPeer.GetProperty(AutomationPeer.IsDialog);
			}
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00065FD0 File Offset: 0x000653D0
		private static object IsControlElement(AutomationPeer peer)
		{
			return peer.IsControlElement();
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00065FE8 File Offset: 0x000653E8
		private static object GetControlType(AutomationPeer peer)
		{
			ControlType controlType = peer.GetControlType();
			return controlType.Id;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00066008 File Offset: 0x00065408
		private static object IsContentElement(AutomationPeer peer)
		{
			return peer.IsContentElement();
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00066020 File Offset: 0x00065420
		private static object GetLabeledBy(AutomationPeer peer)
		{
			AutomationPeer labeledBy = peer.GetLabeledBy();
			return ElementProxy.StaticWrap(labeledBy, peer);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0006603C File Offset: 0x0006543C
		private static object GetNativeWindowHandle(AutomationPeer peer)
		{
			return null;
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0006604C File Offset: 0x0006544C
		private static object GetAutomationId(AutomationPeer peer)
		{
			return peer.GetAutomationId();
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00066060 File Offset: 0x00065460
		private static object GetItemType(AutomationPeer peer)
		{
			return peer.GetItemType();
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00066074 File Offset: 0x00065474
		private static object IsPassword(AutomationPeer peer)
		{
			return peer.IsPassword();
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0006608C File Offset: 0x0006548C
		private static object GetLocalizedControlType(AutomationPeer peer)
		{
			return peer.GetLocalizedControlType();
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000660A0 File Offset: 0x000654A0
		private static object GetName(AutomationPeer peer)
		{
			return peer.GetName();
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x000660B4 File Offset: 0x000654B4
		private static object GetAcceleratorKey(AutomationPeer peer)
		{
			return peer.GetAcceleratorKey();
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000660C8 File Offset: 0x000654C8
		private static object GetAccessKey(AutomationPeer peer)
		{
			return peer.GetAccessKey();
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x000660DC File Offset: 0x000654DC
		private static object HasKeyboardFocus(AutomationPeer peer)
		{
			return peer.HasKeyboardFocus();
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x000660F4 File Offset: 0x000654F4
		private static object IsKeyboardFocusable(AutomationPeer peer)
		{
			return peer.IsKeyboardFocusable();
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0006610C File Offset: 0x0006550C
		private static object IsEnabled(AutomationPeer peer)
		{
			return peer.IsEnabled();
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00066124 File Offset: 0x00065524
		private static object GetBoundingRectangle(AutomationPeer peer)
		{
			return peer.GetBoundingRectangle();
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0006613C File Offset: 0x0006553C
		private static object GetCurrentProcessId(AutomationPeer peer)
		{
			return SafeNativeMethods.GetCurrentProcessId();
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00066154 File Offset: 0x00065554
		private static object GetRuntimeId(AutomationPeer peer)
		{
			return peer.GetRuntimeId();
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00066168 File Offset: 0x00065568
		private static object GetClassName(AutomationPeer peer)
		{
			return peer.GetClassName();
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0006617C File Offset: 0x0006557C
		private static object GetHelpText(AutomationPeer peer)
		{
			return peer.GetHelpText();
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00066190 File Offset: 0x00065590
		private static object GetClickablePoint(AutomationPeer peer)
		{
			Point clickablePoint = peer.GetClickablePoint();
			return new double[]
			{
				clickablePoint.X,
				clickablePoint.Y
			};
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000661C0 File Offset: 0x000655C0
		private static object GetCultureInfo(AutomationPeer peer)
		{
			return null;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000661D0 File Offset: 0x000655D0
		private static object IsOffscreen(AutomationPeer peer)
		{
			return peer.IsOffscreen();
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000661E8 File Offset: 0x000655E8
		private static object GetOrientation(AutomationPeer peer)
		{
			return peer.GetOrientation();
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00066200 File Offset: 0x00065600
		private static object GetFrameworkId(AutomationPeer peer)
		{
			return peer.GetFrameworkId();
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00066214 File Offset: 0x00065614
		private static object IsRequiredForForm(AutomationPeer peer)
		{
			return peer.IsRequiredForForm();
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0006622C File Offset: 0x0006562C
		private static object GetItemStatus(AutomationPeer peer)
		{
			return peer.GetItemStatus();
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00066240 File Offset: 0x00065640
		private static object GetLiveSetting(AutomationPeer peer)
		{
			return peer.GetLiveSetting();
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00066258 File Offset: 0x00065658
		private static object GetControllerFor(AutomationPeer peer)
		{
			return peer.GetControllerForProviderArray();
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0006626C File Offset: 0x0006566C
		private static object GetSizeOfSet(AutomationPeer peer)
		{
			return peer.GetSizeOfSet();
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00066284 File Offset: 0x00065684
		private static object GetPositionInSet(AutomationPeer peer)
		{
			return peer.GetPositionInSet();
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0006629C File Offset: 0x0006569C
		private static object GetHeadingLevel(AutomationPeer peer)
		{
			return peer.GetHeadingLevel();
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000662B4 File Offset: 0x000656B4
		private static object IsDialog(AutomationPeer peer)
		{
			return peer.IsDialog();
		}

		// Token: 0x04000DDE RID: 3550
		private static Hashtable s_patternInfo;

		// Token: 0x04000DDF RID: 3551
		private static Hashtable s_propertyInfo;

		// Token: 0x04000DE0 RID: 3552
		private int _index = -1;

		// Token: 0x04000DE1 RID: 3553
		[SecurityCritical]
		private IntPtr _hwnd;

		// Token: 0x04000DE2 RID: 3554
		private List<AutomationPeer> _children;

		// Token: 0x04000DE3 RID: 3555
		private AutomationPeer _parent;

		// Token: 0x04000DE4 RID: 3556
		private object _eventsSourceOrPeerRecord;

		// Token: 0x04000DE5 RID: 3557
		private Rect _boundingRectangle;

		// Token: 0x04000DE6 RID: 3558
		private string _itemStatus;

		// Token: 0x04000DE7 RID: 3559
		private string _name;

		// Token: 0x04000DE8 RID: 3560
		private bool _isOffscreen;

		// Token: 0x04000DE9 RID: 3561
		private bool _isEnabled;

		// Token: 0x04000DEA RID: 3562
		private bool _invalidated;

		// Token: 0x04000DEB RID: 3563
		private bool _ancestorsInvalid;

		// Token: 0x04000DEC RID: 3564
		private bool _childrenValid;

		// Token: 0x04000DED RID: 3565
		private bool _addedToEventList;

		// Token: 0x04000DEE RID: 3566
		private bool _publicCallInProgress;

		// Token: 0x04000DEF RID: 3567
		private bool _publicSetFocusInProgress;

		// Token: 0x04000DF0 RID: 3568
		private bool _isInteropPeer;

		// Token: 0x04000DF1 RID: 3569
		private bool _hasIterationParent;

		// Token: 0x04000DF2 RID: 3570
		private WeakReference _elementProxyWeakReference;

		// Token: 0x04000DF3 RID: 3571
		private static DispatcherOperationCallback _updatePeer = new DispatcherOperationCallback(AutomationPeer.UpdatePeer);

		// Token: 0x0200083F RID: 2111
		private enum HeadingLevel
		{
			// Token: 0x040027E1 RID: 10209
			None = 80050,
			// Token: 0x040027E2 RID: 10210
			Level1,
			// Token: 0x040027E3 RID: 10211
			Level2,
			// Token: 0x040027E4 RID: 10212
			Level3,
			// Token: 0x040027E5 RID: 10213
			Level4,
			// Token: 0x040027E6 RID: 10214
			Level5,
			// Token: 0x040027E7 RID: 10215
			Level6,
			// Token: 0x040027E8 RID: 10216
			Level7,
			// Token: 0x040027E9 RID: 10217
			Level8,
			// Token: 0x040027EA RID: 10218
			Level9
		}

		// Token: 0x02000840 RID: 2112
		// (Invoke) Token: 0x060056BE RID: 22206
		private delegate object WrapObject(AutomationPeer peer, object iface);

		// Token: 0x02000841 RID: 2113
		private class PatternInfo
		{
			// Token: 0x060056C1 RID: 22209 RVA: 0x0016391C File Offset: 0x00162D1C
			internal PatternInfo(int id, AutomationPeer.WrapObject wrapObject, PatternInterface patternInterface)
			{
				this.Id = id;
				this.WrapObject = wrapObject;
				this.PatternInterface = patternInterface;
			}

			// Token: 0x040027EB RID: 10219
			internal int Id;

			// Token: 0x040027EC RID: 10220
			internal AutomationPeer.WrapObject WrapObject;

			// Token: 0x040027ED RID: 10221
			internal PatternInterface PatternInterface;
		}

		// Token: 0x02000842 RID: 2114
		// (Invoke) Token: 0x060056C3 RID: 22211
		private delegate object GetProperty(AutomationPeer peer);

		// Token: 0x02000843 RID: 2115
		private class PeerRecord
		{
			// Token: 0x170011DD RID: 4573
			// (get) Token: 0x060056C6 RID: 22214 RVA: 0x00163944 File Offset: 0x00162D44
			// (set) Token: 0x060056C7 RID: 22215 RVA: 0x00163958 File Offset: 0x00162D58
			public AutomationPeer EventsSource
			{
				get
				{
					return this._eventsSource;
				}
				set
				{
					this._eventsSource = value;
				}
			}

			// Token: 0x170011DE RID: 4574
			// (get) Token: 0x060056C8 RID: 22216 RVA: 0x0016396C File Offset: 0x00162D6C
			// (set) Token: 0x060056C9 RID: 22217 RVA: 0x00163980 File Offset: 0x00162D80
			public AutomationPeer IterationParent
			{
				get
				{
					return this._iterationParent;
				}
				set
				{
					this._iterationParent = value;
				}
			}

			// Token: 0x040027EE RID: 10222
			private AutomationPeer _eventsSource;

			// Token: 0x040027EF RID: 10223
			private AutomationPeer _iterationParent;
		}
	}
}
