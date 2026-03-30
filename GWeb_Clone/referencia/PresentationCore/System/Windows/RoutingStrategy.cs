using System;

namespace System.Windows
{
	/// <summary>Indica a estratégia de roteamento de um evento roteado.</summary>
	// Token: 0x020001D9 RID: 473
	public enum RoutingStrategy
	{
		/// <summary>O evento roteado usa uma estratégia de túnel, em que a instância do evento roteia para baixo por meio da árvore, da raiz até o elemento de origem.</summary>
		// Token: 0x04000746 RID: 1862
		Tunnel,
		/// <summary>O evento roteado usa uma estratégia de propagação, em que a instância do evento roteia para baixo por meio da árvore, do elemento de origem até a raiz.</summary>
		// Token: 0x04000747 RID: 1863
		Bubble,
		/// <summary>O evento roteado não roteia por meio de uma árvore de elementos, mas dá suporte a outros recursos de evento roteado como a manipulação de classe, <see cref="T:System.Windows.EventTrigger" /> ou <see cref="T:System.Windows.EventSetter" />.</summary>
		// Token: 0x04000748 RID: 1864
		Direct
	}
}
