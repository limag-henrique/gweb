using System;

namespace System.Windows
{
	/// <summary>Especifica se o texto é encapsulado quando atinge a borda da caixa</summary>
	// Token: 0x020001E6 RID: 486
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public enum TextWrapping
	{
		/// <summary>Uma quebra de linha ocorrerá se a linha estourar além da largura de bloco disponível. No entanto, uma linha poderá estourar além da largura do bloco se o algoritmo de quebra de linha não puder determinar uma oportunidade de quebra de linha, como no caso de uma palavra muito longa restrita em um contêiner de largura fixa sem rolagem permitida.</summary>
		// Token: 0x0400077B RID: 1915
		WrapWithOverflow,
		/// <summary>Nenhuma disposição de linha é executada.</summary>
		// Token: 0x0400077C RID: 1916
		NoWrap,
		/// <summary>Uma quebra de linha ocorrerá se a linha estourar além da largura do bloco disponível, mesmo se o algoritmo de quebra de linha padrão não puder determinar uma oportunidade de quebra de linha, como no caso de uma palavra muito longa restrita em um contêiner de largura fixa sem rolagem permitida.</summary>
		// Token: 0x0400077D RID: 1917
		Wrap
	}
}
