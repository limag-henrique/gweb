using System;
using System.Collections.Generic;
using System.Windows.Input;
using MS.Internal.Automation;
using MS.Internal.PresentationCore;

namespace System.Windows.Automation.Peers
{
	/// <summary>Expõe tipos <see cref="T:System.Windows.ContentElement" /> à Automação de Interface do Usuário.</summary>
	// Token: 0x02000315 RID: 789
	public class ContentElementAutomationPeer : AutomationPeer
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</summary>
		/// <param name="owner">O <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</param>
		// Token: 0x060019AB RID: 6571 RVA: 0x000662E8 File Offset: 0x000656E8
		public ContentElementAutomationPeer(ContentElement owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</summary>
		/// <returns>O <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x00066310 File Offset: 0x00065710
		public ContentElement Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Cria um <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> para o <see cref="T:System.Windows.ContentElement" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</param>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> para o <see cref="T:System.Windows.ContentElement" /> especificado.</returns>
		// Token: 0x060019AD RID: 6573 RVA: 0x00066324 File Offset: 0x00065724
		public static AutomationPeer CreatePeerForElement(ContentElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element.CreateAutomationPeer();
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> para o <see cref="T:System.Windows.ContentElement" /> especificado.</summary>
		/// <param name="element">O <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</param>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> para o <see cref="T:System.Windows.ContentElement" /> especificado ou <see langword="null" /> se o <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> não tiver sido criado pelo método <see cref="M:System.Windows.Automation.Peers.ContentElementAutomationPeer.CreatePeerForElement(System.Windows.ContentElement)" />.</returns>
		// Token: 0x060019AE RID: 6574 RVA: 0x00066348 File Offset: 0x00065748
		public static AutomationPeer FromElement(ContentElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return element.GetAutomationPeer();
		}

		/// <summary>Obtém a coleção de elementos filho do <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetChildren" />.</summary>
		/// <returns>A coleção de elementos filho.</returns>
		// Token: 0x060019AF RID: 6575 RVA: 0x0006636C File Offset: 0x0006576C
		protected override List<AutomationPeer> GetChildrenCore()
		{
			return null;
		}

		/// <summary>Obtém o padrão de controle do <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</summary>
		/// <param name="patternInterface">Um dos valores de enumeração.</param>
		/// <returns>Um objeto que implementará a interface <see cref="T:System.Windows.Automation.Provider.ISynchronizedInputProvider" /> se <paramref name="patternInterface" /> for <see cref="F:System.Windows.Automation.Peers.PatternInterface.SynchronizedInput" />; caso contrário, <see langword="null" />.</returns>
		// Token: 0x060019B0 RID: 6576 RVA: 0x0006637C File Offset: 0x0006577C
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

		/// <summary>Obtém o tipo de controle para o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationControlType" />.</summary>
		/// <returns>O valor de enumeração <see cref="F:System.Windows.Automation.Peers.AutomationControlType.Custom" />.</returns>
		// Token: 0x060019B1 RID: 6577 RVA: 0x000663B0 File Offset: 0x000657B0
		protected override AutomationControlType GetAutomationControlTypeCore()
		{
			return AutomationControlType.Custom;
		}

		/// <summary>Obtém a cadeia de caracteres que identifica exclusivamente o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationId" />.</summary>
		/// <returns>Uma cadeia de caracteres que contém o identificador de automação.</returns>
		// Token: 0x060019B2 RID: 6578 RVA: 0x000663C0 File Offset: 0x000657C0
		protected override string GetAutomationIdCore()
		{
			return AutomationProperties.GetAutomationId(this._owner);
		}

		/// <summary>Obtém o rótulo de texto do <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetName" />.</summary>
		/// <returns>A cadeia de caracteres que contém o rótulo.</returns>
		// Token: 0x060019B3 RID: 6579 RVA: 0x000663D8 File Offset: 0x000657D8
		protected override string GetNameCore()
		{
			return AutomationProperties.GetName(this._owner);
		}

		/// <summary>Obtém a cadeia de caracteres que descreve a funcionalidade do <see cref="T:System.Windows.ContentElement" /> associada a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetHelpText" />.</summary>
		/// <returns>A cadeia de caracteres que descreve a funcionalidade do <see cref="T:System.Windows.ContentElement" /> associada a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />.</returns>
		// Token: 0x060019B4 RID: 6580 RVA: 0x000663F0 File Offset: 0x000657F0
		protected override string GetHelpTextCore()
		{
			return AutomationProperties.GetHelpText(this._owner);
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Rect" /> que representa o retângulo delimitador do <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetBoundingRectangle" />.</summary>
		/// <returns>O retângulo delimitador.</returns>
		// Token: 0x060019B5 RID: 6581 RVA: 0x00066408 File Offset: 0x00065808
		protected override Rect GetBoundingRectangleCore()
		{
			return Rect.Empty;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> está fora da tela. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento não estiver na tela; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019B6 RID: 6582 RVA: 0x0006641C File Offset: 0x0006581C
		protected override bool IsOffscreenCore()
		{
			IsOffscreenBehavior isOffscreenBehavior = AutomationProperties.GetIsOffscreenBehavior(this._owner);
			return isOffscreenBehavior != IsOffscreenBehavior.Onscreen;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> está disposto em uma direção específica. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetOrientation" />.</summary>
		/// <returns>O valor de enumeração <see cref="F:System.Windows.Automation.Peers.AutomationOrientation.None" />.</returns>
		// Token: 0x060019B7 RID: 6583 RVA: 0x0006643C File Offset: 0x0006583C
		protected override AutomationOrientation GetOrientationCore()
		{
			return AutomationOrientation.None;
		}

		/// <summary>Obtém uma cadeia de caracteres legível por humanos que contém o tipo do item que o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> representa. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemType" />.</summary>
		/// <returns>A cadeia de caracteres que contém o tipo de item.</returns>
		// Token: 0x060019B8 RID: 6584 RVA: 0x0006644C File Offset: 0x0006584C
		protected override string GetItemTypeCore()
		{
			return string.Empty;
		}

		/// <summary>Obtém o nome do <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClassName" />.</summary>
		/// <returns>Uma cadeia de caracteres <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x060019B9 RID: 6585 RVA: 0x00066460 File Offset: 0x00065860
		protected override string GetClassNameCore()
		{
			return string.Empty;
		}

		/// <summary>Obtém uma cadeia de caracteres que transmite o status visual do <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetItemStatus" />.</summary>
		/// <returns>Uma cadeia de caracteres que contém o status.</returns>
		// Token: 0x060019BA RID: 6586 RVA: 0x00066474 File Offset: 0x00065874
		protected override string GetItemStatusCore()
		{
			return string.Empty;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> deve ser preenchido em um formulário. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsRequiredForForm" />.</summary>
		/// <returns>
		///   <see langword="false" />.</returns>
		// Token: 0x060019BB RID: 6587 RVA: 0x00066488 File Offset: 0x00065888
		protected override bool IsRequiredForFormCore()
		{
			return false;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> pode aceitar o foco do teclado. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsKeyboardFocusable" />.</summary>
		/// <returns>
		///   <see langword="false" />.</returns>
		// Token: 0x060019BC RID: 6588 RVA: 0x00066498 File Offset: 0x00065898
		protected override bool IsKeyboardFocusableCore()
		{
			return Keyboard.IsFocusable(this._owner);
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> tem foco de entrada do teclado no momento. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.HasKeyboardFocus" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o elemento tiver o foco de entrada do teclado; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019BD RID: 6589 RVA: 0x000664B0 File Offset: 0x000658B0
		protected override bool HasKeyboardFocusCore()
		{
			return this._owner.IsKeyboardFocused;
		}

		/// <summary>Obtém um valor que indica se este par de automação pode receber e enviar eventos para o elemento associado. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsEnabled" />.</summary>
		/// <returns>
		///   <see langword="true" /> se o par de automação puder receber e enviar eventos; caso contrário, <see langword="false" />.</returns>
		// Token: 0x060019BE RID: 6590 RVA: 0x000664C8 File Offset: 0x000658C8
		protected override bool IsEnabledCore()
		{
			return this._owner.IsEnabled;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000664E0 File Offset: 0x000658E0
		protected override bool IsDialogCore()
		{
			return AutomationProperties.GetIsDialog(this._owner);
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> contém conteúdo protegido. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsPassword" />.</summary>
		/// <returns>
		///   <see langword="false" />.</returns>
		// Token: 0x060019C0 RID: 6592 RVA: 0x000664F8 File Offset: 0x000658F8
		protected override bool IsPasswordCore()
		{
			return false;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> é um elemento que contém os dados apresentados ao usuário. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsContentElement" />.</summary>
		/// <returns>
		///   <see langword="false" />.</returns>
		// Token: 0x060019C1 RID: 6593 RVA: 0x00066508 File Offset: 0x00065908
		protected override bool IsContentElementCore()
		{
			return true;
		}

		/// <summary>Obtém um valor que indica se o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> é algo que o usuário final entenderia como sendo interativo ou como contribuindo para a estrutura lógica do controle no GUI. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsControlElement" />.</summary>
		/// <returns>
		///   <see langword="false" />.</returns>
		// Token: 0x060019C2 RID: 6594 RVA: 0x00066518 File Offset: 0x00065918
		protected override bool IsControlElementCore()
		{
			return false;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> para o <see cref="T:System.Windows.Controls.Label" /> que é o destino de <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLabeledBy" />.</summary>
		/// <returns>O <see cref="T:System.Windows.Automation.Peers.LabelAutomationPeer" /> para o elemento que é o destino de <see cref="T:System.Windows.Controls.Label" />.</returns>
		// Token: 0x060019C3 RID: 6595 RVA: 0x00066528 File Offset: 0x00065928
		protected override AutomationPeer GetLabeledByCore()
		{
			return null;
		}

		/// <summary>Obtém a tecla de aceleração para o elemento associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAcceleratorKey" />.</summary>
		/// <returns>Uma cadeia de caracteres <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x060019C4 RID: 6596 RVA: 0x00066538 File Offset: 0x00065938
		protected override string GetAcceleratorKeyCore()
		{
			return string.Empty;
		}

		/// <summary>Obtém a chave de acesso para o <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAccessKey" />.</summary>
		/// <returns>A chave de acesso para este <see cref="T:System.Windows.ContentElement" />.</returns>
		// Token: 0x060019C5 RID: 6597 RVA: 0x0006654C File Offset: 0x0006594C
		protected override string GetAccessKeyCore()
		{
			return AccessKeyManager.InternalGetAccessKeyCharacter(this._owner);
		}

		/// <summary>Obtém as características de notificação da região dinâmica do <see cref="T:System.Windows.ContentElement" /> associado a esse <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetLiveSetting" />.</summary>
		/// <returns>As características de notificação da região dinâmica.</returns>
		// Token: 0x060019C6 RID: 6598 RVA: 0x00066564 File Offset: 0x00065964
		protected override AutomationLiveSetting GetLiveSettingCore()
		{
			return AutomationProperties.GetLiveSetting(this._owner);
		}

		/// <summary>Lê <see cref="F:System.Windows.Automation.AutomationProperties.PositionInSetProperty" /> e retorna o valor da propriedade anexada PositionInSet.</summary>
		/// <returns>O valor da propriedade anexada PositionInSet da Automação da Interface do Usuário.</returns>
		// Token: 0x060019C7 RID: 6599 RVA: 0x0006657C File Offset: 0x0006597C
		protected override int GetPositionInSetCore()
		{
			return AutomationProperties.GetPositionInSet(this._owner);
		}

		/// <summary>Lê <see cref="F:System.Windows.Automation.AutomationProperties.SizeOfSetProperty" /> e retorna o valor da propriedade anexada SizeOfSet.</summary>
		/// <returns>O valor da propriedade anexada SizeOfSet da Automação da Interface do Usuário.</returns>
		// Token: 0x060019C8 RID: 6600 RVA: 0x00066594 File Offset: 0x00065994
		protected override int GetSizeOfSetCore()
		{
			return AutomationProperties.GetSizeOfSet(this._owner);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000665AC File Offset: 0x000659AC
		protected override AutomationHeadingLevel GetHeadingLevelCore()
		{
			return AutomationProperties.GetHeadingLevel(this._owner);
		}

		/// <summary>Obtém um <see cref="T:System.Windows.Point" /> que representa o espaço clicável no <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetClickablePoint" />.</summary>
		/// <returns>O ponto que representa o espaço clicável no elemento.</returns>
		// Token: 0x060019CA RID: 6602 RVA: 0x000665C4 File Offset: 0x000659C4
		protected override Point GetClickablePointCore()
		{
			return new Point(double.NaN, double.NaN);
		}

		/// <summary>Define o foco de entrada do teclado no <see cref="T:System.Windows.ContentElement" /> associado a este <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" />. Chamado por <see cref="M:System.Windows.Automation.Peers.AutomationPeer.SetFocus" />.</summary>
		// Token: 0x060019CB RID: 6603 RVA: 0x000665E8 File Offset: 0x000659E8
		protected override void SetFocusCore()
		{
			if (!this._owner.Focus())
			{
				throw new InvalidOperationException(SR.Get("SetFocusFailed"));
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00066614 File Offset: 0x00065A14
		internal override Rect GetVisibleBoundingRectCore()
		{
			return base.GetBoundingRectangle();
		}

		// Token: 0x04000DF4 RID: 3572
		private ContentElement _owner;

		// Token: 0x04000DF5 RID: 3573
		private SynchronizedInputAdaptor _synchronizedInputPattern;
	}
}
