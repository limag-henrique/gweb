using System;
using System.Collections.Generic;
using System.Security;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using MS.Internal;
using MS.Internal.Automation;
using MS.Internal.PresentationCore;

namespace System.Windows.Automation.Peers
{
	/// <summary>Expõe tipos <see cref="T:System.Windows.UIElement3D" /> à Automação de Interface do Usuário.</summary>
	// Token: 0x02000317 RID: 791
	public class UIElement3DAutomationPeer : AutomationPeer
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</summary>
		/// <param name="owner">O <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</param>
		// Token: 0x060019D2 RID: 6610 RVA: 0x000667BC File Offset: 0x00065BBC
		public UIElement3DAutomationPeer(UIElement3D owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</summary>
		/// <returns>O <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x000667E4 File Offset: 0x00065BE4
		public UIElement3D Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Cria um <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> para o <see cref="T:System.Windows.UIElement3D" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</param>
		/// <returns>Um <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> para o <see cref="T:System.Windows.UIElement3D" /> especificado.</returns>
		// Token: 0x060019D4 RID: 6612 RVA: 0x000667F8 File Offset: 0x00065BF8
		public static AutomationPeer CreatePeerForElement(UIElement3D element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element.CreateAutomationPeer();
		}

		/// <summary>Retorna o <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> para o <see cref="T:System.Windows.UIElement3D" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</param>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> ou <see langword="null" />, se o <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> não tiver sido criado pelo método <see cref="M:System.Windows.Automation.Peers.UIElement3DAutomationPeer.CreatePeerForElement(System.Windows.UIElement3D)" />.</returns>
		// Token: 0x060019D5 RID: 6613 RVA: 0x0006681C File Offset: 0x00065C1C
		public static AutomationPeer FromElement(UIElement3D element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element.GetAutomationPeer();
		}

		/// <summary>Retorna a coleção de elementos filho do <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetChildren" />.</summary>
		/// <returns>Os elementos <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> que correspondem aos elementos filho do <see cref="T:System.Windows.UIElement3D" />.</returns>
		// Token: 0x060019D6 RID: 6614 RVA: 0x00066840 File Offset: 0x00065C40
		protected override List<AutomationPeer> GetChildrenCore()
		{
			List<AutomationPeer> children = null;
			UIElement3DAutomationPeer.iterate(this._owner, delegate(AutomationPeer peer)
			{
				if (children == null)
				{
					children = new List<AutomationPeer>();
				}
				children.Add(peer);
				return false;
			});
			return children;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00066878 File Offset: 0x00065C78
		private static bool iterate(DependencyObject parent, UIElement3DAutomationPeer.IteratorCallback callback)
		{
			bool flag = false;
			if (parent != null)
			{
				int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
				int num = 0;
				while (num < childrenCount && !flag)
				{
					DependencyObject child = VisualTreeHelper.GetChild(parent, num);
					AutomationPeer peer;
					if (child != null && child is UIElement && (peer = UIElementAutomationPeer.CreatePeerForElement((UIElement)child)) != null)
					{
						flag = callback(peer);
					}
					else if (child != null && child is UIElement3D && (peer = UIElement3DAutomationPeer.CreatePeerForElement((UIElement3D)child)) != null)
					{
						flag = callback(peer);
					}
					else
					{
						flag = UIElement3DAutomationPeer.iterate(child, callback);
					}
					num++;
				}
			}
			return flag;
		}

		/// <summary>Retorna o padrão de controle do <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</summary>
		/// <param name="patternInterface">Um dos valores de enumeração.</param>
		/// <returns>Um objeto que implementará a interface <see cref="T:System.Windows.Automation.Provider.ISynchronizedInputProvider" /> se <paramref name="patternInterface" /> for <see cref="F:System.Windows.Automation.Peers.PatternInterface.SynchronizedInput" />; caso contrário, <see langword="null" />.</returns>
		// Token: 0x060019D8 RID: 6616 RVA: 0x00066900 File Offset: 0x00065D00
		public override object GetPattern(PatternInterface patternInterface)
		{
			if (patternInterface == PatternInterface.SynchronizedInput)
			{
				if (this._synchronizedInputPattern == null)
				{
					this._synchronizedInputPattern = new SynchronizedInputAdaptor(this._owner);
				}
				return this._synchronizedInputPattern;
			}
			return null;
		}

		/// <summary>Retorna o tipo de controle para o <see cref="T:System.Windows.UIElement3D" /> que está associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationControlType" />.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Automation.Peers.AutomationControlType.Custom" /> em todos os casos.</returns>
		// Token: 0x060019D9 RID: 6617 RVA: 0x00066934 File Offset: 0x00065D34
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Custom;
		}

		/// <summary>Retorna a cadeia de caracteres que identifica exclusivamente o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationId" />.</summary>
		/// <returns>A cadeia de caracteres que identifica exclusivamente o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x060019DA RID: 6618 RVA: 0x00066944 File Offset: 0x00065D44
		protected override string GetAutomationIdCore()
		{
			return AutomationProperties.GetAutomationId(this._owner);
		}

		/// <summary>Retorna a cadeia de caracteres que representa o <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> que está associado com este <see cref="T:System.Windows.UIElement3D" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetName" />.</summary>
		/// <returns>A cadeia de caracteres que representa o <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> que está associado com este <see cref="T:System.Windows.UIElement3D" />.</returns>
		// Token: 0x060019DB RID: 6619 RVA: 0x0006695C File Offset: 0x00065D5C
		protected override string GetNameCore()
		{
			return AutomationProperties.GetName(this._owner);
		}

		/// <summary>Retorna a cadeia de caracteres que descreve a funcionalidade do <see cref="T:System.Windows.UIElement3D" /> associada a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetHelpText" />.</summary>
		/// <returns>Uma cadeia de caracteres que descreve a funcionalidade do <see cref="T:System.Windows.UIElement3D" /> associada a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x060019DC RID: 6620 RVA: 0x00066974 File Offset: 0x00065D74
		protected override string GetHelpTextCore()
		{
			return AutomationProperties.GetHelpText(this._owner);
		}

		/// <summary>Retorna o <see cref="T:System.Windows.Rect" /> que representa o retângulo delimitador do <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetBoundingRectangle" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Rect" /> que contém as coordenadas do elemento.</returns>
		// Token: 0x060019DD RID: 6621 RVA: 0x0006698C File Offset: 0x00065D8C
		protected override Rect GetBoundingRectangleCore()
		{
			Rect empty;
			if (!this.ComputeBoundingRectangle(out empty))
			{
				empty = Rect.Empty;
			}
			return empty;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000669AC File Offset: 0x00065DAC
		[SecurityTreatAsSafe]
		[SecurityCritical]
		private bool ComputeBoundingRectangle(out Rect rect)
		{
			rect = Rect.Empty;
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(this._owner);
			if (presentationSource == null)
			{
				return false;
			}
			HwndSource hwndSource = presentationSource as HwndSource;
			if (hwndSource == null)
			{
				return false;
			}
			Rect visual2DContentBounds = this._owner.Visual2DContentBounds;
			Rect rectRoot = PointUtil.ElementToRoot(visual2DContentBounds, VisualTreeHelper.GetContainingVisual2D(this._owner), presentationSource);
			Rect rectClient = PointUtil.RootToClient(rectRoot, presentationSource);
			rect = PointUtil.ClientToScreen(rectClient, hwndSource);
			return true;
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> está fora da tela. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento não estiver na tela; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019DF RID: 6623 RVA: 0x00066A18 File Offset: 0x00065E18
		protected override bool IsOffscreenCore()
		{
			switch (AutomationProperties.GetIsOffscreenBehavior(this._owner))
			{
			case IsOffscreenBehavior.Onscreen:
				return false;
			case IsOffscreenBehavior.Offscreen:
				return true;
			case IsOffscreenBehavior.FromClip:
			{
				bool flag = !this._owner.IsVisible;
				if (!flag)
				{
					UIElement containingUIElement = UIElement3DAutomationPeer.GetContainingUIElement(this._owner);
					if (containingUIElement != null)
					{
						Rect rect = UIElementAutomationPeer.CalculateVisibleBoundingRect(containingUIElement);
						flag = (DoubleUtil.AreClose(rect, Rect.Empty) || DoubleUtil.AreClose(rect.Height, 0.0) || DoubleUtil.AreClose(rect.Width, 0.0));
					}
				}
				return flag;
			}
			default:
				return !this._owner.IsVisible;
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00066AC0 File Offset: 0x00065EC0
		private static UIElement GetContainingUIElement(DependencyObject reference)
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

		/// <summary>Retorna a orientação do <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetOrientation" />.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Automation.Peers.AutomationOrientation.None" /> em todos os casos.</returns>
		// Token: 0x060019E1 RID: 6625 RVA: 0x00066AE8 File Offset: 0x00065EE8
		protected override AutomationOrientation GetOrientationCore()
		{
			return AutomationOrientation.None;
		}

		/// <summary>Retorna uma cadeia de caracteres legível por humanos que representa o tipo de item que o <see cref="T:System.Windows.UIElement3D" /> para este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemType" />.</summary>
		/// <returns>Uma cadeia de caracteres que representa o tipo de item que o <see cref="T:System.Windows.UIElement3D" /> para este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x060019E2 RID: 6626 RVA: 0x00066AF8 File Offset: 0x00065EF8
		protected override string GetItemTypeCore()
		{
			return AutomationProperties.GetItemType(this._owner);
		}

		/// <summary>Retorna o nome do <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClassName" />.</summary>
		/// <returns>
		///   <see cref="F:System.String.Empty" /> em todos os casos.</returns>
		// Token: 0x060019E3 RID: 6627 RVA: 0x00066B10 File Offset: 0x00065F10
		protected override string GetClassNameCore()
		{
			return string.Empty;
		}

		/// <summary>Retorna uma cadeia de caracteres que comunica o status do <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemStatus" />.</summary>
		/// <returns>O status do <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x060019E4 RID: 6628 RVA: 0x00066B24 File Offset: 0x00065F24
		protected override string GetItemStatusCore()
		{
			return AutomationProperties.GetItemStatus(this._owner);
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> deve ser preenchido em um formulário. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsRequiredForForm" />.</summary>
		/// <returns>
		///   <see langword="true" /> se for necessário que o <see cref="T:System.Windows.UIElement3D" /> seja preenchido em um formulário; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019E5 RID: 6629 RVA: 0x00066B3C File Offset: 0x00065F3C
		protected override bool IsRequiredForFormCore()
		{
			return AutomationProperties.GetIsRequiredForForm(this._owner);
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> pode aceitar o foco do teclado. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsKeyboardFocusable" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento puder receber o foco do teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019E6 RID: 6630 RVA: 0x00066B54 File Offset: 0x00065F54
		protected override bool IsKeyboardFocusableCore()
		{
			return Keyboard.IsFocusable(this._owner);
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> tem foco de entrada do teclado no momento. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.HasKeyboardFocus" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver o foco de entrada do teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019E7 RID: 6631 RVA: 0x00066B6C File Offset: 0x00065F6C
		protected override bool HasKeyboardFocusCore()
		{
			return this._owner.IsKeyboardFocused;
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> pode participar de testes de acerto ou aceitar o foco. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsKeyboardFocusable" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o <see cref="T:System.Windows.UIElement3D" /> associado a esse <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> puder participar de testes de acerto ou aceitar o foco; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019E8 RID: 6632 RVA: 0x00066B84 File Offset: 0x00065F84
		protected override bool IsEnabledCore()
		{
			return this._owner.IsEnabled;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00066B9C File Offset: 0x00065F9C
		protected override bool IsDialogCore()
		{
			return AutomationProperties.GetIsDialog(this._owner);
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> contém conteúdo protegido. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsPassword" />.</summary>
		/// <returns>
		///   <see langword="false" /> em todos os casos.</returns>
		// Token: 0x060019EA RID: 6634 RVA: 0x00066BB4 File Offset: 0x00065FB4
		protected override bool IsPasswordCore()
		{
			return false;
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> é um elemento que contém os dados apresentados ao usuário. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsContentElement" />.</summary>
		/// <returns>
		///   <see langword="true" /> em todos os casos.</returns>
		// Token: 0x060019EB RID: 6635 RVA: 0x00066BC4 File Offset: 0x00065FC4
		protected override bool IsContentElementCore()
		{
			return true;
		}

		/// <summary>Retorna um valor que indica se o <see cref="T:System.Windows.UIElement3D" /> associada a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" /> é compreendido pelo usuário final como interativo. Opcionalmente, o usuário pode entender o <see cref="T:System.Windows.UIElement3D" /> como contribuindo para a estrutura lógica do controle no GUI. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsControlElement" />.</summary>
		/// <returns>
		///   <see langword="true" /> em todos os casos.</returns>
		// Token: 0x060019EC RID: 6636 RVA: 0x00066BD4 File Offset: 0x00065FD4
		protected override bool IsControlElementCore()
		{
			return base.IncludeInvisibleElementsInControlView || this._owner.IsVisible;
		}

		/// <summary>Retorna o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o elemento que tem como destino o <see cref="T:System.Windows.UIElement3D" /> deste <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLabeledBy" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o elemento que tem como destino o <see cref="T:System.Windows.UIElement3D" /> deste <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x060019ED RID: 6637 RVA: 0x00066BF8 File Offset: 0x00065FF8
		protected override AutomationPeer GetLabeledByCore()
		{
			UIElement labeledBy = AutomationProperties.GetLabeledBy(this._owner);
			if (labeledBy != null)
			{
				return labeledBy.GetAutomationPeer();
			}
			return null;
		}

		/// <summary>Retorna a tecla de atalho para o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAcceleratorKey" />.</summary>
		/// <returns>A tecla de atalho para o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x060019EE RID: 6638 RVA: 0x00066C1C File Offset: 0x0006601C
		protected override string GetAcceleratorKeyCore()
		{
			return AutomationProperties.GetAcceleratorKey(this._owner);
		}

		/// <summary>Retorna a chave de acesso para o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAccessKey" />.</summary>
		/// <returns>A chave de acesso para o <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />.</returns>
		// Token: 0x060019EF RID: 6639 RVA: 0x00066C34 File Offset: 0x00066034
		protected override string GetAccessKeyCore()
		{
			string accessKey = AutomationProperties.GetAccessKey(this._owner);
			if (string.IsNullOrEmpty(accessKey))
			{
				return AccessKeyManager.InternalGetAccessKeyCharacter(this._owner);
			}
			return string.Empty;
		}

		/// <summary>Obtém as características de notificação da região dinâmica do objeto <see cref="T:System.Windows.UIElement3D" /> associado a esse <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLiveSetting" />.</summary>
		/// <returns>As características de notificação da região dinâmica.</returns>
		// Token: 0x060019F0 RID: 6640 RVA: 0x00066C68 File Offset: 0x00066068
		protected override AutomationLiveSetting GetLiveSettingCore()
		{
			return AutomationProperties.GetLiveSetting(this._owner);
		}

		/// <summary>Lê <see cref="F:System.Windows.Automation.AutomationProperties.PositionInSetProperty" /> e retorna o valor da propriedade anexada PositionInSet.</summary>
		/// <returns>O valor da propriedade anexada PositionInSet da Automação da Interface do Usuário</returns>
		// Token: 0x060019F1 RID: 6641 RVA: 0x00066C80 File Offset: 0x00066080
		protected override int GetPositionInSetCore()
		{
			return AutomationProperties.GetPositionInSet(this._owner);
		}

		/// <summary>Fornece um valor para a propriedade SizeOfSet da Automação da Interface do Usuário.</summary>
		/// <returns>Lê <see cref="F:System.Windows.Automation.AutomationProperties.SizeOfSetProperty" /> e retorna o valor.</returns>
		// Token: 0x060019F2 RID: 6642 RVA: 0x00066C98 File Offset: 0x00066098
		protected override int GetSizeOfSetCore()
		{
			return AutomationProperties.GetSizeOfSet(this._owner);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00066CB0 File Offset: 0x000660B0
		protected override AutomationHeadingLevel GetHeadingLevelCore()
		{
			return AutomationProperties.GetHeadingLevel(this._owner);
		}

		/// <summary>Retorna um <see cref="T:System.Windows.Point" /> que representa o espaço clicável no <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClickablePoint" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Point" /> no elemento que permite um clique. Os valores de ponto (<see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" />) se o elemento não for tanto um <see cref="T:System.Windows.Interop.HwndSource" /> quanto um <see cref="T:System.Windows.PresentationSource" />.</returns>
		// Token: 0x060019F4 RID: 6644 RVA: 0x00066CC8 File Offset: 0x000660C8
		protected override Point GetClickablePointCore()
		{
			Point result = new Point(double.NaN, double.NaN);
			Rect rect;
			if (this.ComputeBoundingRectangle(out rect))
			{
				result = new Point(rect.Left + rect.Width * 0.5, rect.Top + rect.Height * 0.5);
			}
			return result;
		}

		/// <summary>Define o foco de entrada do teclado no <see cref="T:System.Windows.UIElement3D" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElement3DAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.SetFocus" />.</summary>
		// Token: 0x060019F5 RID: 6645 RVA: 0x00066D34 File Offset: 0x00066134
		protected override void SetFocusCore()
		{
			if (!this._owner.Focus())
			{
				throw new InvalidOperationException(SR.Get("SetFocusFailed"));
			}
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00066D60 File Offset: 0x00066160
		internal override Rect GetVisibleBoundingRectCore()
		{
			return base.GetBoundingRectangle();
		}

		// Token: 0x04000DF6 RID: 3574
		private UIElement3D _owner;

		// Token: 0x04000DF7 RID: 3575
		private SynchronizedInputAdaptor _synchronizedInputPattern;

		// Token: 0x02000844 RID: 2116
		// (Invoke) Token: 0x060056CC RID: 22220
		private delegate bool IteratorCallback(AutomationPeer peer);
	}
}
