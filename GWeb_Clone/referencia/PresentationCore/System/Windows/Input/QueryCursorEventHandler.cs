using System;

namespace System.Windows.Input
{
	/// <summary>Representa o método que manipulará os eventos <see cref="E:System.Windows.UIElement.QueryCursor" /> e <see cref="E:System.Windows.ContentElement.QueryCursor" /> roteados, bem como o evento anexado <see cref="E:System.Windows.Input.Mouse.QueryCursor" />.</summary>
	/// <param name="sender">O objeto em que o manipulador de eventos está anexado.</param>
	/// <param name="e">Os dados do evento.</param>
	// Token: 0x0200028E RID: 654
	// (Invoke) Token: 0x06001337 RID: 4919
	public delegate void QueryCursorEventHandler(object sender, QueryCursorEventArgs e);
}
