using System;

namespace System.Windows
{
	/// <summary>Representa um método que tratará os eventos roteados que permite que uma operação do tipo "arrastar e soltar" seja cancelada pela fonte de arrastar, por exemplo, <see cref="E:System.Windows.UIElement.QueryContinueDrag" />.</summary>
	/// <param name="sender">O objeto em que o manipulador de eventos está anexado.</param>
	/// <param name="e">Os dados do evento.</param>
	// Token: 0x020001D1 RID: 465
	// (Invoke) Token: 0x06000C74 RID: 3188
	public delegate void QueryContinueDragEventHandler(object sender, QueryContinueDragEventArgs e);
}
