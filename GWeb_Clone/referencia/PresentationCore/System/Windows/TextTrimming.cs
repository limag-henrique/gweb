using System;

namespace System.Windows
{
	/// <summary>Descreve como o texto é cortado quando ele excede a borda da sua caixa de conteúdo.</summary>
	// Token: 0x020001E7 RID: 487
	[Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
	public enum TextTrimming
	{
		/// <summary>O texto não foi cortado.</summary>
		// Token: 0x0400077F RID: 1919
		None,
		/// <summary>O texto foi cortado em um limite de caracteres. Uma reticências (...) será desenhada no lugar do texto restante.</summary>
		// Token: 0x04000780 RID: 1920
		CharacterEllipsis,
		/// <summary>O texto foi cortado em um limite de palavra. Uma reticências (...) será desenhada no lugar do texto restante.</summary>
		// Token: 0x04000781 RID: 1921
		WordEllipsis
	}
}
