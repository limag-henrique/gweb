using System;

namespace System.Windows
{
	/// <summary>Especifica o estado de exibição de um elemento.</summary>
	// Token: 0x020001F4 RID: 500
	public enum Visibility : byte
	{
		/// <summary>Exibe o elemento.</summary>
		// Token: 0x040007CB RID: 1995
		Visible,
		/// <summary>Não exibe o elemento, mas reserva espaço para o elemento no layout.</summary>
		// Token: 0x040007CC RID: 1996
		Hidden,
		/// <summary>Não exibe o elemento e não reserva espaço para ele no layout.</summary>
		// Token: 0x040007CD RID: 1997
		Collapsed
	}
}
