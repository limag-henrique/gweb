using System;

namespace System.Windows
{
	/// <summary>Especifica como uma janela será redimensionada automaticamente para se ajustar ao tamanho de seu conteúdo. Usado pela propriedade <see cref="P:System.Windows.Window.SizeToContent" />.</summary>
	// Token: 0x020001DB RID: 475
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public enum SizeToContent
	{
		/// <summary>Especifica que uma janela não será redimensionada automaticamente para se ajustar ao tamanho de seu conteúdo. Em vez disso, o tamanho de uma janela é determinado por outras propriedades, incluindo <see cref="P:System.Windows.FrameworkElement.Width" />, <see cref="P:System.Windows.FrameworkElement.Height" />, <see cref="P:System.Windows.FrameworkElement.MaxWidth" />, <see cref="P:System.Windows.FrameworkElement.MaxHeight" />, <see cref="P:System.Windows.FrameworkElement.MinWidth" /> e <see cref="P:System.Windows.FrameworkElement.MinHeight" />. Consulte Visão geral do WPF do Windows.</summary>
		// Token: 0x0400074F RID: 1871
		Manual,
		/// <summary>Especifica que uma janela definirá automaticamente sua largura para se ajustar à largura de seu conteúdo, mas não à altura.</summary>
		// Token: 0x04000750 RID: 1872
		Width,
		/// <summary>Especifica que uma janela definirá automaticamente sua altura para se ajustar à altura de seu conteúdo, mas não à largura.</summary>
		// Token: 0x04000751 RID: 1873
		Height,
		/// <summary>Especifica que uma janela definirá automaticamente sua largura e sua altura para se ajustar à largura e à altura de seu conteúdo.</summary>
		// Token: 0x04000752 RID: 1874
		WidthAndHeight
	}
}
