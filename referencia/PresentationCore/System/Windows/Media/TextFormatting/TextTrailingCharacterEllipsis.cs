using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Define as propriedades de texto recolhido para recolher toda a linha final na granularidade de caractere e com reticências sendo o símbolo de texto recolhido.</summary>
	// Token: 0x020005BB RID: 1467
	public class TextTrailingCharacterEllipsis : TextCollapsingProperties
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextTrailingCharacterEllipsis" /> especificando propriedades de texto recolhido.</summary>
		/// <param name="width">Um <see cref="T:System.Double" /> que representa a largura para o qual o intervalo de texto recolhido especificado é restrito a.</param>
		/// <param name="textRunProperties">Um <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> compartilhado de valor que representa o conjunto de propriedades de texto.</param>
		// Token: 0x06004302 RID: 17154 RVA: 0x001042D4 File Offset: 0x001036D4
		public TextTrailingCharacterEllipsis(double width, TextRunProperties textRunProperties)
		{
			this._width = width;
			this._ellipsis = new TextCharacters("…", textRunProperties);
		}

		/// <summary>Obtém a largura para o qual o intervalo de texto recolhido especificado é restrito a.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a largura.</returns>
		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x00104300 File Offset: 0x00103700
		public sealed override double Width
		{
			get
			{
				return this._width;
			}
		}

		/// <summary>Obtém a sequência de texto usada como o símbolo de texto recolhido.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> valor que representa o símbolo de texto recolhido.</returns>
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06004304 RID: 17156 RVA: 0x00104314 File Offset: 0x00103714
		public sealed override TextRun Symbol
		{
			get
			{
				return this._ellipsis;
			}
		}

		/// <summary>Obtém o estilo do texto recolhido.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.Media.TextFormatting.TextCollapsingStyle" />.</returns>
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x00104328 File Offset: 0x00103728
		public sealed override TextCollapsingStyle Style
		{
			get
			{
				return TextCollapsingStyle.TrailingCharacter;
			}
		}

		// Token: 0x04001850 RID: 6224
		private double _width;

		// Token: 0x04001851 RID: 6225
		private TextRun _ellipsis;

		// Token: 0x04001852 RID: 6226
		private const string StringHorizontalEllipsis = "…";
	}
}
