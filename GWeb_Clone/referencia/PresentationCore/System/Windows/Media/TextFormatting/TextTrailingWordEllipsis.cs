using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Define as propriedades de texto recolhido para recolher toda a linha até o fim na granularidade de palavra e com reticências sendo o símbolo de texto recolhido.</summary>
	// Token: 0x020005BC RID: 1468
	public class TextTrailingWordEllipsis : TextCollapsingProperties
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextTrailingCharacterEllipsis" /> especificando propriedades de texto recolhido.</summary>
		/// <param name="width">Um <see cref="T:System.Double" /> que representa a largura para o qual o intervalo de texto recolhido especificado é restrito a.</param>
		/// <param name="textRunProperties">Um <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> compartilhado de valor que representa o conjunto de propriedades de texto.</param>
		// Token: 0x06004306 RID: 17158 RVA: 0x00104338 File Offset: 0x00103738
		public TextTrailingWordEllipsis(double width, TextRunProperties textRunProperties)
		{
			this._width = width;
			this._ellipsis = new TextCharacters("…", textRunProperties);
		}

		/// <summary>Obtém a largura para o qual o intervalo de texto recolhido especificado é restrito a.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a largura.</returns>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x00104364 File Offset: 0x00103764
		public sealed override double Width
		{
			get
			{
				return this._width;
			}
		}

		/// <summary>Obtém a sequência de texto usada como o símbolo de texto recolhido.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> valor que representa o símbolo de texto recolhido.</returns>
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06004308 RID: 17160 RVA: 0x00104378 File Offset: 0x00103778
		public sealed override TextRun Symbol
		{
			get
			{
				return this._ellipsis;
			}
		}

		/// <summary>Obtém o estilo do texto recolhido.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.TextFormatting.TextCollapsingStyle" />.</returns>
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x0010438C File Offset: 0x0010378C
		public sealed override TextCollapsingStyle Style
		{
			get
			{
				return TextCollapsingStyle.TrailingWord;
			}
		}

		// Token: 0x04001853 RID: 6227
		private double _width;

		// Token: 0x04001854 RID: 6228
		private TextRun _ellipsis;

		// Token: 0x04001855 RID: 6229
		private const string StringHorizontalEllipsis = "…";
	}
}
