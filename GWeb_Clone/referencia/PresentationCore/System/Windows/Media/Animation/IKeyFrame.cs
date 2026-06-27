using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma implementação de interface <see cref="T:System.Windows.Media.Animation.IKeyFrame" /> fornece acesso não tipado a propriedades de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
	// Token: 0x0200056D RID: 1389
	public interface IKeyFrame
	{
		/// <summary>Obtém ou define valores <see cref="P:System.Windows.Media.Animation.IKeyFrame.KeyTime" /> associados a um objeto de KeyFrame.</summary>
		/// <returns>Uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</returns>
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600406C RID: 16492
		// (set) Token: 0x0600406D RID: 16493
		KeyTime KeyTime { get; set; }

		/// <summary>Obtém ou define o valor associado a uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
		/// <returns>O valor atual para essa propriedade.</returns>
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x0600406E RID: 16494
		// (set) Token: 0x0600406F RID: 16495
		object Value { get; set; }
	}
}
