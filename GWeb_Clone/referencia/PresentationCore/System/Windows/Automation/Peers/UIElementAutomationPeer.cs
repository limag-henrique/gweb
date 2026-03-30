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
	/// <summary>Expõe tipos <see cref="T:System.Windows.UIElement" /> à Automação de Interface do Usuário.</summary>
	// Token: 0x02000318 RID: 792
	public class UIElementAutomationPeer : AutomationPeer
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</summary>
		/// <param name="owner">O <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</param>
		// Token: 0x060019F7 RID: 6647 RVA: 0x00066D74 File Offset: 0x00066174
		public UIElementAutomationPeer(UIElement owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</summary>
		/// <returns>O <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00066D9C File Offset: 0x0006619C
		public UIElement Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Cria um <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> para o <see cref="T:System.Windows.UIElement" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</param>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> criado para o <see cref="T:System.Windows.UIElement" /> especificado.</returns>
		// Token: 0x060019F9 RID: 6649 RVA: 0x00066DB0 File Offset: 0x000661B0
		public static AutomationPeer CreatePeerForElement(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element.CreateAutomationPeer();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> para o <see cref="T:System.Windows.UIElement" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</param>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />; ou <see langword="null" />, se o <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> não tiver sido criado pelo método <see cref="M:System.Windows.Automation.Peers.UIElementAutomationPeer.CreatePeerForElement(System.Windows.UIElement)" />.</returns>
		// Token: 0x060019FA RID: 6650 RVA: 0x00066DD4 File Offset: 0x000661D4
		public static AutomationPeer FromElement(UIElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element.GetAutomationPeer();
		}

		/// <summary>Obtém a coleção de elementos filho do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetChildren" />.</summary>
		/// <returns>Uma lista de elementos <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> filho.</returns>
		// Token: 0x060019FB RID: 6651 RVA: 0x00066DF8 File Offset: 0x000661F8
		protected override List<AutomationPeer> GetChildrenCore()
		{
			List<AutomationPeer> children = null;
			UIElementAutomationPeer.iterate(this._owner, delegate(AutomationPeer peer)
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

		// Token: 0x060019FC RID: 6652 RVA: 0x00066E30 File Offset: 0x00066230
		[SecurityCritical]
		internal static AutomationPeer GetRootAutomationPeer(Visual rootVisual, IntPtr hwnd)
		{
			AutomationPeer root = null;
			UIElementAutomationPeer.iterate(rootVisual, delegate(AutomationPeer peer)
			{
				root = peer;
				return true;
			});
			if (root != null)
			{
				root.Hwnd = hwnd;
			}
			return root;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00066E78 File Offset: 0x00066278
		private static bool iterate(DependencyObject parent, UIElementAutomationPeer.IteratorCallback callback)
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
						flag = UIElementAutomationPeer.iterate(child, callback);
					}
					num++;
				}
			}
			return flag;
		}

		/// <summary>Obtém o padrão de controle do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</summary>
		/// <param name="patternInterface">Um valor da enumeração.</param>
		/// <returns>Um objeto que implementará a interface <see cref="T:System.Windows.Automation.Provider.ISynchronizedInputProvider" /> se <paramref name="patternInterface" /> for <see cref="F:System.Windows.Automation.Peers.PatternInterface.SynchronizedInput" />; caso contrário, <see langword="null" />.</returns>
		// Token: 0x060019FE RID: 6654 RVA: 0x00066F00 File Offset: 0x00066300
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

		/// <summary>Obtém o tipo de controle para o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationControlType" />.</summary>
		/// <returns>O valor de enumeração <see cref="F:System.Windows.Automation.Peers.AutomationControlType.Custom" />.</returns>
		// Token: 0x060019FF RID: 6655 RVA: 0x00066F34 File Offset: 0x00066334
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Custom;
		}

		/// <summary>Obtém a cadeia de caracteres que identifica exclusivamente o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationId" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Automation.AutomationProperties.AutomationId" /> que é retornado por <see cref="M:System.Windows.Automation.AutomationProperties.GetAutomationId(System.Windows.DependencyObject)" />.</returns>
		// Token: 0x06001A00 RID: 6656 RVA: 0x00066F44 File Offset: 0x00066344
		protected override string GetAutomationIdCore()
		{
			return AutomationProperties.GetAutomationId(this._owner);
		}

		/// <summary>Obtém o rótulo de texto do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetName" />.</summary>
		/// <returns>A cadeia de caracteres que contém o <see cref="P:System.Windows.Automation.AutomationProperties.Name" /> que é retornado por <see cref="M:System.Windows.Automation.AutomationProperties.GetName(System.Windows.DependencyObject)" />.</returns>
		// Token: 0x06001A01 RID: 6657 RVA: 0x00066F5C File Offset: 0x0006635C
		protected override string GetNameCore()
		{
			return AutomationProperties.GetName(this._owner);
		}

		/// <summary>Obtém a cadeia de caracteres que descreve a funcionalidade do <see cref="T:System.Windows.UIElement" /> associada a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetHelpText" />.</summary>
		/// <returns>A cadeia de caracteres que contém o <see cref="P:System.Windows.Automation.AutomationProperties.HelpText" /> que é retornado por <see cref="M:System.Windows.Automation.AutomationProperties.GetHelpText(System.Windows.DependencyObject)" />.</returns>
		// Token: 0x06001A02 RID: 6658 RVA: 0x00066F74 File Offset: 0x00066374
		protected override string GetHelpTextCore()
		{
			return AutomationProperties.GetHelpText(this._owner);
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Rect" /> que representa o retângulo delimitador do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetBoundingRectangle" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Rect" /> que contém as coordenadas do elemento. Opcionalmente, se o elemento não for <see cref="T:System.Windows.Interop.HwndSource" /> nem <see cref="T:System.Windows.PresentationSource" />, esse método retornará <see cref="P:System.Windows.Rect.Empty" />.</returns>
		// Token: 0x06001A03 RID: 6659 RVA: 0x00066F8C File Offset: 0x0006638C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		protected override Rect GetBoundingRectangleCore()
		{
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(this._owner);
			if (presentationSource == null)
			{
				return Rect.Empty;
			}
			HwndSource hwndSource = presentationSource as HwndSource;
			if (hwndSource == null)
			{
				return Rect.Empty;
			}
			Rect rectElement = new Rect(new Point(0.0, 0.0), this._owner.RenderSize);
			Rect rectRoot = PointUtil.ElementToRoot(rectElement, this._owner, presentationSource);
			Rect rectClient = PointUtil.RootToClient(rectRoot, presentationSource);
			return PointUtil.ClientToScreen(rectClient, hwndSource);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0006700C File Offset: 0x0006640C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal override Rect GetVisibleBoundingRectCore()
		{
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(this._owner);
			if (presentationSource == null)
			{
				return Rect.Empty;
			}
			HwndSource hwndSource = presentationSource as HwndSource;
			if (hwndSource == null)
			{
				return Rect.Empty;
			}
			Rect rectElement = UIElementAutomationPeer.CalculateVisibleBoundingRect(this._owner);
			Rect rectRoot = PointUtil.ElementToRoot(rectElement, this._owner, presentationSource);
			Rect rectClient = PointUtil.RootToClient(rectRoot, presentationSource);
			return PointUtil.ClientToScreen(rectClient, hwndSource);
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> está fora da tela. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento não estiver na tela; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001A05 RID: 6661 RVA: 0x00067070 File Offset: 0x00066470
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
					Rect rect = UIElementAutomationPeer.CalculateVisibleBoundingRect(this._owner);
					flag = (DoubleUtil.AreClose(rect, Rect.Empty) || DoubleUtil.AreClose(rect.Height, 0.0) || DoubleUtil.AreClose(rect.Width, 0.0));
				}
				return flag;
			}
			default:
				return !this._owner.IsVisible;
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00067110 File Offset: 0x00066510
		internal static Rect CalculateVisibleBoundingRect(UIElement owner)
		{
			Rect empty = new Rect(owner.RenderSize);
			DependencyObject parent = VisualTreeHelper.GetParent(owner);
			while (parent != null && !DoubleUtil.AreClose(empty, Rect.Empty) && !DoubleUtil.AreClose(empty.Height, 0.0) && !DoubleUtil.AreClose(empty.Width, 0.0))
			{
				Visual visual = parent as Visual;
				if (visual != null)
				{
					Geometry clip = VisualTreeHelper.GetClip(visual);
					if (clip != null)
					{
						GeneralTransform inverse = owner.TransformToAncestor(visual).Inverse;
						if (inverse != null)
						{
							Rect rect = clip.Bounds;
							rect = inverse.TransformBounds(rect);
							empty.Intersect(rect);
						}
						else
						{
							empty = Rect.Empty;
						}
					}
				}
				parent = VisualTreeHelper.GetParent(parent);
			}
			return empty;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> está disposto em uma direção específica. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetOrientation" />.</summary>
		/// <returns>O valor de enumeração <see cref="F:System.Windows.Automation.Peers.AutomationOrientation.None" />.</returns>
		// Token: 0x06001A07 RID: 6663 RVA: 0x000671C8 File Offset: 0x000665C8
		protected override AutomationOrientation GetOrientationCore()
		{
			return AutomationOrientation.None;
		}

		/// <summary>Obtém uma cadeia de caracteres legível por humanos que contém o tipo de item que o <see cref="T:System.Windows.UIElement" /> para este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> representa. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemType" />.</summary>
		/// <returns>A cadeia de caracteres que contém o <see cref="P:System.Windows.Automation.AutomationProperties.ItemType" /> que é retornado por <see cref="M:System.Windows.Automation.AutomationProperties.GetItemType(System.Windows.DependencyObject)" />.</returns>
		// Token: 0x06001A08 RID: 6664 RVA: 0x000671D8 File Offset: 0x000665D8
		protected override string GetItemTypeCore()
		{
			return AutomationProperties.GetItemType(this._owner);
		}

		/// <summary>Obtém o nome do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClassName" />.</summary>
		/// <returns>Uma cadeia de caracteres <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x06001A09 RID: 6665 RVA: 0x000671F0 File Offset: 0x000665F0
		protected override string GetClassNameCore()
		{
			return string.Empty;
		}

		/// <summary>Obtém uma cadeia de caracteres que comunica o status visual do <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemStatus" />.</summary>
		/// <returns>A cadeia de caracteres que contém o <see cref="P:System.Windows.Automation.AutomationProperties.ItemStatus" /> que é retornado por <see cref="M:System.Windows.Automation.AutomationProperties.GetItemStatus(System.Windows.DependencyObject)" />.</returns>
		// Token: 0x06001A0A RID: 6666 RVA: 0x00067204 File Offset: 0x00066604
		protected override string GetItemStatusCore()
		{
			return AutomationProperties.GetItemStatus(this._owner);
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> deve ser preenchido em um formulário. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsRequiredForForm" />.</summary>
		/// <returns>Um <see langword="boolean" /> que contém o valor retornado por <see cref="M:System.Windows.Automation.AutomationProperties.GetIsRequiredForForm(System.Windows.DependencyObject)" />, se ele estiver definido; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001A0B RID: 6667 RVA: 0x0006721C File Offset: 0x0006661C
		protected override bool IsRequiredForFormCore()
		{
			return AutomationProperties.GetIsRequiredForForm(this._owner);
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> pode aceitar o foco do teclado. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsKeyboardFocusable" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento puder ser focado pelo teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001A0C RID: 6668 RVA: 0x00067234 File Offset: 0x00066634
		protected override bool IsKeyboardFocusableCore()
		{
			return Keyboard.IsFocusable(this._owner);
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> tem foco de entrada do teclado no momento. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.HasKeyboardFocus" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver o foco de entrada do teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x06001A0D RID: 6669 RVA: 0x0006724C File Offset: 0x0006664C
		protected override bool HasKeyboardFocusCore()
		{
			return this._owner.IsKeyboardFocused;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> pode aceitar o foco do teclado. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsKeyboardFocusable" />.</summary>
		/// <returns>Um <see langword="boolean" /> que contém o valor de <see cref="P:System.Windows.UIElement.IsEnabled" />.</returns>
		// Token: 0x06001A0E RID: 6670 RVA: 0x00067264 File Offset: 0x00066664
		protected override bool IsEnabledCore()
		{
			return this._owner.IsEnabled;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0006727C File Offset: 0x0006667C
		protected override bool IsDialogCore()
		{
			return AutomationProperties.GetIsDialog(this._owner);
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> contém conteúdo protegido. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsPassword" />.</summary>
		/// <returns>
		///   <see langword="false" />.</returns>
		// Token: 0x06001A10 RID: 6672 RVA: 0x00067294 File Offset: 0x00066694
		protected override bool IsPasswordCore()
		{
			return false;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> é um elemento que contém os dados apresentados ao usuário. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsContentElement" />.</summary>
		/// <returns>
		///   <see langword="true" />.</returns>
		// Token: 0x06001A11 RID: 6673 RVA: 0x000672A4 File Offset: 0x000666A4
		protected override bool IsContentElementCore()
		{
			return true;
		}

		/// <summary>Obtém ou define um valor que indica se o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> é compreendido pelo usuário final como interativo. Opcionalmente, o usuário pode entender o <see cref="T:System.Windows.UIElement" /> como contribuindo para a estrutura lógica do controle no GUI. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsControlElement" />.</summary>
		/// <returns>
		///   <see langword="true" />.</returns>
		// Token: 0x06001A12 RID: 6674 RVA: 0x000672B4 File Offset: 0x000666B4
		protected override bool IsControlElementCore()
		{
			return base.IncludeInvisibleElementsInControlView || this._owner.IsVisible;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o elemento que é o destino de <see cref="T:System.Windows.UIElement" /> para este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLabeledBy" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o elemento que é o destino de <see cref="T:System.Windows.UIElement" /> para este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</returns>
		// Token: 0x06001A13 RID: 6675 RVA: 0x000672D8 File Offset: 0x000666D8
		protected override AutomationPeer GetLabeledByCore()
		{
			UIElement labeledBy = AutomationProperties.GetLabeledBy(this._owner);
			if (labeledBy != null)
			{
				return labeledBy.GetAutomationPeer();
			}
			return null;
		}

		/// <summary>Obtém a tecla de atalho para o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAcceleratorKey" />.</summary>
		/// <returns>O <see cref="P:System.Windows.Automation.AutomationProperties.AcceleratorKey" /> que é retornado por <see cref="M:System.Windows.Automation.AutomationProperties.GetAcceleratorKey(System.Windows.DependencyObject)" />.</returns>
		// Token: 0x06001A14 RID: 6676 RVA: 0x000672FC File Offset: 0x000666FC
		protected override string GetAcceleratorKeyCore()
		{
			return AutomationProperties.GetAcceleratorKey(this._owner);
		}

		/// <summary>Obtém a chave de acesso para o <see cref="T:System.Windows.UIElement" /> que está associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Esse método é chamado pelo <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAccessKey" />.</summary>
		/// <returns>A chave de acesso para o <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />.</returns>
		// Token: 0x06001A15 RID: 6677 RVA: 0x00067314 File Offset: 0x00066714
		protected override string GetAccessKeyCore()
		{
			string accessKey = AutomationProperties.GetAccessKey(this._owner);
			if (string.IsNullOrEmpty(accessKey))
			{
				return AccessKeyManager.InternalGetAccessKeyCharacter(this._owner);
			}
			return string.Empty;
		}

		/// <summary>Obtém as características de notificação da região dinâmica do objeto <see cref="T:System.Windows.UIElement" /> associado a esse <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLiveSetting" />.</summary>
		/// <returns>As características de notificação da região dinâmica.</returns>
		// Token: 0x06001A16 RID: 6678 RVA: 0x00067348 File Offset: 0x00066748
		protected override AutomationLiveSetting GetLiveSettingCore()
		{
			return AutomationProperties.GetLiveSetting(this._owner);
		}

		/// <summary>Lê <see cref="F:System.Windows.Automation.AutomationProperties.PositionInSetProperty" /> e retorna o valor da propriedade anexada PositionInSet.</summary>
		/// <returns>O valor da propriedade anexada PositionInSet da Automação da Interface do Usuário.</returns>
		// Token: 0x06001A17 RID: 6679 RVA: 0x00067360 File Offset: 0x00066760
		protected override int GetPositionInSetCore()
		{
			int num = AutomationProperties.GetPositionInSet(this._owner);
			if (num == -1)
			{
				UIElement positionAndSizeOfSetController = this._owner.PositionAndSizeOfSetController;
				if (positionAndSizeOfSetController != null)
				{
					AutomationPeer automationPeer = UIElementAutomationPeer.FromElement(positionAndSizeOfSetController);
					automationPeer = (automationPeer.EventsSource ?? automationPeer);
					if (automationPeer != null)
					{
						try
						{
							num = automationPeer.GetPositionInSet();
						}
						catch (ElementNotAvailableException)
						{
							num = -1;
						}
					}
				}
			}
			return num;
		}

		/// <summary>Fornece um valor para a propriedade SizeOfSet da Automação da Interface do Usuário.</summary>
		/// <returns>Lê <see cref="P:System.Windows.Automation.AutomationProperties.SizeOfSet" /> e retorna o valor.</returns>
		// Token: 0x06001A18 RID: 6680 RVA: 0x000673D0 File Offset: 0x000667D0
		protected override int GetSizeOfSetCore()
		{
			int num = AutomationProperties.GetSizeOfSet(this._owner);
			if (num == -1)
			{
				UIElement positionAndSizeOfSetController = this._owner.PositionAndSizeOfSetController;
				if (positionAndSizeOfSetController != null)
				{
					AutomationPeer automationPeer = UIElementAutomationPeer.FromElement(positionAndSizeOfSetController);
					automationPeer = (automationPeer.EventsSource ?? automationPeer);
					if (automationPeer != null)
					{
						try
						{
							num = automationPeer.GetSizeOfSet();
						}
						catch (ElementNotAvailableException)
						{
							num = -1;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00067440 File Offset: 0x00066840
		protected override AutomationHeadingLevel GetHeadingLevelCore()
		{
			return AutomationProperties.GetHeadingLevel(this._owner);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Point" /> que representa o espaço clicável no <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClickablePoint" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Point" /> no elemento que permite um clique. Os valores de ponto (<see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" />) se o elemento não for tanto um <see cref="T:System.Windows.Interop.HwndSource" /> quanto um <see cref="T:System.Windows.PresentationSource" />.</returns>
		// Token: 0x06001A1A RID: 6682 RVA: 0x00067458 File Offset: 0x00066858
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override Point GetClickablePointCore()
		{
			Point result = new Point(double.NaN, double.NaN);
			PresentationSource presentationSource = PresentationSource.CriticalFromVisual(this._owner);
			if (presentationSource == null)
			{
				return result;
			}
			HwndSource hwndSource = presentationSource as HwndSource;
			if (hwndSource == null)
			{
				return result;
			}
			Rect rectElement = new Rect(new Point(0.0, 0.0), this._owner.RenderSize);
			Rect rectRoot = PointUtil.ElementToRoot(rectElement, this._owner, presentationSource);
			Rect rectClient = PointUtil.RootToClient(rectRoot, presentationSource);
			Rect rect = PointUtil.ClientToScreen(rectClient, hwndSource);
			result = new Point(rect.Left + rect.Width * 0.5, rect.Top + rect.Height * 0.5);
			return result;
		}

		/// <summary>Define o foco de entrada do teclado no <see cref="T:System.Windows.UIElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" />. Este método é chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.SetFocus" />.</summary>
		// Token: 0x06001A1B RID: 6683 RVA: 0x00067520 File Offset: 0x00066920
		protected override void SetFocusCore()
		{
			if (!this._owner.Focus())
			{
				throw new InvalidOperationException(SR.Get("SetFocusFailed"));
			}
		}

		// Token: 0x04000DF8 RID: 3576
		private UIElement _owner;

		// Token: 0x04000DF9 RID: 3577
		private SynchronizedInputAdaptor _synchronizedInputPattern;

		// Token: 0x02000846 RID: 2118
		// (Invoke) Token: 0x060056D2 RID: 22226
		private delegate bool IteratorCallback(AutomationPeer peer);
	}
}
