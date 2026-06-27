using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Permite que clientes do mecanismo de texto mapeiem um índice de caracteres de origem de texto para o <see cref="T:System.Windows.Media.GlyphRun" /> correspondente.</summary>
	// Token: 0x02000596 RID: 1430
	public sealed class IndexedGlyphRun
	{
		// Token: 0x060041D7 RID: 16855 RVA: 0x00102630 File Offset: 0x00101A30
		internal IndexedGlyphRun(int textSourceCharacterIndex, int textSourceCharacterLength, GlyphRun glyphRun)
		{
			this._textSourceCharacterIndex = textSourceCharacterIndex;
			this._length = textSourceCharacterLength;
			this._glyphRun = glyphRun;
		}

		/// <summary>Obtém o índice de caracteres de origem de texto que corresponde ao início do <see cref="T:System.Windows.Media.GlyphRun" />.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o índice de caracteres de código-fonte do texto.</returns>
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x00102658 File Offset: 0x00101A58
		public int TextSourceCharacterIndex
		{
			get
			{
				return this._textSourceCharacterIndex;
			}
		}

		/// <summary>Obtém o comprimento de caracteres de origem de texto que corresponde ao objeto <see cref="T:System.Windows.Media.TextFormatting.IndexedGlyphRun" />.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o tamanho do caractere de código-fonte do texto.</returns>
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060041D9 RID: 16857 RVA: 0x0010266C File Offset: 0x00101A6C
		public int TextSourceLength
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.GlyphRun" /> que corresponde ao objeto <see cref="T:System.Windows.Media.TextFormatting.IndexedGlyphRun" />.</summary>
		/// <returns>Um objeto <see cref="T:System.Windows.Media.GlyphRun" />.</returns>
		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x00102680 File Offset: 0x00101A80
		public GlyphRun GlyphRun
		{
			get
			{
				return this._glyphRun;
			}
		}

		// Token: 0x04001808 RID: 6152
		private GlyphRun _glyphRun;

		// Token: 0x04001809 RID: 6153
		private int _textSourceCharacterIndex;

		// Token: 0x0400180A RID: 6154
		private int _length;
	}
}
