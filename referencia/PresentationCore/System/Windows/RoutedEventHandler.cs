using System;

namespace System.Windows
{
	/// <summary>Representa o método que manipulará vários eventos roteados que não têm dados de evento específicos além dos dados comuns a todos os eventos roteados.</summary>
	/// <param name="sender">O objeto em que o manipulador de eventos está anexado.</param>
	/// <param name="e">Os dados do evento.</param>
	// Token: 0x020001D5 RID: 469
	// (Invoke) Token: 0x06000C97 RID: 3223
	public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);
}
