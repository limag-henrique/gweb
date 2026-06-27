using System;

namespace System.Windows.Media
{
	/// <summary>Representa um retorno de chamada usado para personalizar o teste de clique. O WPF invoca o <see cref="T:System.Windows.Media.HitTestResultCallback" /> para relatar interseções de teste de clique para o usuário.</summary>
	/// <param name="result">O valor <see cref="T:System.Windows.Media.HitTestResult" /> que representa um objeto visual retornado de um teste de clique.</param>
	/// <returns>Um <see cref="T:System.Windows.Media.HitTestFilterBehavior" /> que representa a ação resultante do teste de clique.</returns>
	// Token: 0x02000410 RID: 1040
	// (Invoke) Token: 0x06002A06 RID: 10758
	public delegate HitTestResultBehavior HitTestResultCallback(HitTestResult result);
}
