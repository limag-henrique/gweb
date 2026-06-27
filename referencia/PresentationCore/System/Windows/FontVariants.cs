using System;

namespace System.Windows
{
	/// <summary>Renderiza variantes de formas de glifo tipográficas.</summary>
	// Token: 0x020001E8 RID: 488
	public enum FontVariants
	{
		/// <summary>Comportamento de fonte padrão. A escala e o posicionamento da fonte estão normais.</summary>
		// Token: 0x04000783 RID: 1923
		Normal,
		/// <summary>Substitui um glifo padrão por um glifo sobrescrito. Sobrescrito normalmente é usado para notas de rodapé.</summary>
		// Token: 0x04000784 RID: 1924
		Superscript,
		/// <summary>Substitui um glifo padrão por um glifo subscrito.</summary>
		// Token: 0x04000785 RID: 1925
		Subscript,
		/// <summary>Substitui um glifo padrão por um glifo ordinal ou pode combinar a substituição de glifo com ajustes de posicionamento para um posicionamento correto. Formas ordinais são normalmente associados a notação numérica de uma palavra ordinal, como "1º" para "primeiro".</summary>
		// Token: 0x04000786 RID: 1926
		Ordinal,
		/// <summary>Substitui um glifo padrão com um glifo inferior ou pode combinar a substituição de glifo com ajustes de posicionamento para um posicionamento correto. Formas inferiores normalmente são usadas em fórmulas químicas ou notação matemática.</summary>
		// Token: 0x04000787 RID: 1927
		Inferior,
		/// <summary>Substitui um glifo padrão por um glifo de Kana japonês menor. Isso é usado para esclarecer o significado de Kanji, que pode não ser familiar para o leitor.</summary>
		// Token: 0x04000788 RID: 1928
		Ruby
	}
}
