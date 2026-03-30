using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Define uma execução de texto especializado usado para marcar o final de um parágrafo.</summary>
	// Token: 0x020005A2 RID: 1442
	public class TextEndOfParagraph : TextEndOfLine
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextEndOfParagraph" /> usando um comprimento de caracteres especificado.</summary>
		/// <param name="length">O número de caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextEndOfParagraph" />.</param>
		// Token: 0x06004220 RID: 16928 RVA: 0x00102CC8 File Offset: 0x001020C8
		public TextEndOfParagraph(int length) : base(length)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextEndOfParagraph" /> usando um comprimento de caracteres especificado e um valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" />.</summary>
		/// <param name="length">O número de caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextEndOfParagraph" />.</param>
		/// <param name="textRunProperties">O valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> a ser usado para os caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextEndOfParagraph" />.</param>
		// Token: 0x06004221 RID: 16929 RVA: 0x00102CDC File Offset: 0x001020DC
		public TextEndOfParagraph(int length, TextRunProperties textRunProperties) : base(length, textRunProperties)
		{
		}
	}
}
