using System;

namespace System.Windows.Automation
{
	/// <summary>Descreve as características de notificação de uma região dinâmica particular.</summary>
	// Token: 0x0200030E RID: 782
	public enum AutomationLiveSetting
	{
		/// <summary>O elemento não enviará notificações se o conteúdo da região dinâmica tiver sido alterado.</summary>
		// Token: 0x04000D82 RID: 3458
		Off,
		/// <summary>O elemento enviará notificações não interruptivas se o conteúdo da região dinâmica tiver sido alterado. Com essa configuração, os clientes de Automação da Interface do Usuário e tecnologias adaptativas não devem interromper o usuário para informá-lo sobre alterações na região dinâmica.</summary>
		// Token: 0x04000D83 RID: 3459
		Polite,
		/// <summary>O elemento enviará notificações interruptivas se o conteúdo da região dinâmica tiver sido alterado. Com essa configuração, os clientes de Automação da Interface do Usuário e tecnologias adaptativas devem interromper o usuário para informá-lo sobre alterações na região dinâmica.</summary>
		// Token: 0x04000D84 RID: 3460
		Assertive
	}
}
