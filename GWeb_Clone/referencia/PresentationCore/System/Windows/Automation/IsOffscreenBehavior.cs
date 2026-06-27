using System;

namespace System.Windows.Automation
{
	/// <summary>Especifica como a propriedade <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" /> é determinada.</summary>
	// Token: 0x0200030C RID: 780
	public enum IsOffscreenBehavior
	{
		/// <summary>
		///   <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" /> é calculado com base na propriedade <see cref="P:System.Windows.UIElement.IsVisible" />.</summary>
		// Token: 0x04000D72 RID: 3442
		Default,
		/// <summary>
		///   <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" /> é <see langword="false" />.</summary>
		// Token: 0x04000D73 RID: 3443
		Onscreen,
		/// <summary>
		///   <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" /> é <see langword="true" />.</summary>
		// Token: 0x04000D74 RID: 3444
		Offscreen,
		/// <summary>
		///   <see cref="M:System.Windows.Automation.Peers.AutomationPeer.IsOffscreen" /> é calculado com base em regiões de recorte.</summary>
		// Token: 0x04000D75 RID: 3445
		FromClip
	}
}
