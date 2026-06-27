using System;

namespace System.Windows.Input
{
	/// <summary>Representa o método que manipulará os eventos <see cref="E:System.Windows.Input.InputManager.PreNotifyInput" /> e <see cref="E:System.Windows.Input.InputManager.PostNotifyInput" />.</summary>
	/// <param name="sender">A fonte do evento.</param>
	/// <param name="e">Os dados do evento.</param>
	// Token: 0x02000288 RID: 648
	// (Invoke) Token: 0x0600131C RID: 4892
	public delegate void NotifyInputEventHandler(object sender, NotifyInputEventArgs e);
}
