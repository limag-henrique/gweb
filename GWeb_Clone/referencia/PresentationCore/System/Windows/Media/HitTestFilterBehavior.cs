using System;

namespace System.Windows.Media
{
	/// <summary>Especifica o comportamento de retorno de um teste de clique em um método de retorno de chamada de filtro de teste de clique.</summary>
	// Token: 0x0200040B RID: 1035
	public enum HitTestFilterBehavior
	{
		/// <summary>Realiza o teste de clique com relação ao <see cref="T:System.Windows.Media.Visual" /> atual, mas não seus descendentes.</summary>
		// Token: 0x040012F1 RID: 4849
		ContinueSkipChildren = 2,
		/// <summary>Não faz teste de clique com relação ao <see cref="T:System.Windows.Media.Visual" /> atual ou seus descendentes.</summary>
		// Token: 0x040012F2 RID: 4850
		ContinueSkipSelfAndChildren = 0,
		/// <summary>Não faz teste de clique com relação ao <see cref="T:System.Windows.Media.Visual" /> atual, mas faz teste de clique em relação aos seus descendentes.</summary>
		// Token: 0x040012F3 RID: 4851
		ContinueSkipSelf = 4,
		/// <summary>Realiza o teste de clique com relação ao <see cref="T:System.Windows.Media.Visual" /> atual e seus descendentes.</summary>
		// Token: 0x040012F4 RID: 4852
		Continue = 6,
		/// <summary>Interromper teste de clique no <see cref="T:System.Windows.Media.Visual" /> atual.</summary>
		// Token: 0x040012F5 RID: 4853
		Stop = 8
	}
}
