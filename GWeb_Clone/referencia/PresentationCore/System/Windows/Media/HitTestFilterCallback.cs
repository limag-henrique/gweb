using System;

namespace System.Windows.Media
{
	/// <summary>Representa o método de retorno de chamada que especifica as partes da árvore visual a serem omitidas do processamento de teste de clique</summary>
	/// <param name="potentialHitTestTarget">O visual para o teste de clique.</param>
	/// <returns>Um <see cref="T:System.Windows.Media.HitTestFilterBehavior" /> que representa a ação resultante do teste de clique.</returns>
	// Token: 0x0200040C RID: 1036
	// (Invoke) Token: 0x060029FC RID: 10748
	public delegate HitTestFilterBehavior HitTestFilterCallback(DependencyObject potentialHitTestTarget);
}
