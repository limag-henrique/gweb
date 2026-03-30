using System;
using MS.Internal.FontCache;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece uma classe abstrata para especificar dados de caractere e formatar propriedades a serem usadas pelo objeto <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />.</summary>
	// Token: 0x020005B7 RID: 1463
	public abstract class TextSource
	{
		/// <summary>Recupera um <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> iniciando em determinado <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> posição.</summary>
		/// <param name="textSourceCharacterIndex">Especifica a posição de índice do caractere no <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> em que o <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> é recuperado.</param>
		/// <returns>Um valor que representa um <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> ou um objeto derivado de <see cref="T:System.Windows.Media.TextFormatting.TextRun" />.</returns>
		// Token: 0x060042F4 RID: 17140
		public abstract TextRun GetTextRun(int textSourceCharacterIndex);

		/// <summary>Recupera o alcance de texto imediatamente antes especificado <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> posição.</summary>
		/// <param name="textSourceCharacterIndexLimit">A posição de índice do caractere que interrompe a recuperação de texto.</param>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.CultureSpecificCharacterBufferRange" /> valor que representa o intervalo de texto imediatamente antes <paramref name="textSourceCharacterIndexLimit" />.</returns>
		// Token: 0x060042F5 RID: 17141
		public abstract TextSpan<CultureSpecificCharacterBufferRange> GetPrecedingText(int textSourceCharacterIndexLimit);

		/// <summary>Recupera um valor que mapeia um <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> índice de caracteres para um <see cref="T:System.Windows.Media.TextEffect" /> índice de caracteres.</summary>
		/// <param name="textSourceCharacterIndex">O índice de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> a mapear.</param>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o <see cref="T:System.Windows.Media.TextEffect" /> índice de caracteres.</returns>
		// Token: 0x060042F6 RID: 17142
		public abstract int GetTextEffectCharacterIndexFromTextSourceCharacterIndex(int textSourceCharacterIndex);

		/// <summary>Obtém ou define o PixelsPerDip em que o texto deve ser renderizado.</summary>
		/// <returns>O valor <see cref="P:System.Windows.Media.TextFormatting.TextSource.PixelsPerDip" /> atual.</returns>
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x001041C0 File Offset: 0x001035C0
		// (set) Token: 0x060042F8 RID: 17144 RVA: 0x001041D4 File Offset: 0x001035D4
		public double PixelsPerDip
		{
			get
			{
				return this._pixelsPerDip;
			}
			set
			{
				this._pixelsPerDip = value;
			}
		}

		// Token: 0x04001844 RID: 6212
		private double _pixelsPerDip = (double)Util.PixelsPerDip;
	}
}
