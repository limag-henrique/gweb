using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa as características do texto recolhido.</summary>
	// Token: 0x0200059C RID: 1436
	public abstract class TextCollapsingProperties
	{
		/// <summary>Obtém a largura do intervalo de texto recolhido.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a largura do intervalo de texto recolhido.</returns>
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004208 RID: 16904
		public abstract double Width { get; }

		/// <summary>Obtém a sequência de texto usada como o símbolo de texto recolhido.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> valor que representa o símbolo de texto recolhido.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004209 RID: 16905
		public abstract TextRun Symbol { get; }

		/// <summary>Obtém o estilo do texto recolhido.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.TextFormatting.TextCollapsingStyle" />.</returns>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600420A RID: 16906
		public abstract TextCollapsingStyle Style { get; }
	}
}
