using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa o intervalo de caracteres e suas medidas de largura para texto recolhido dentro de uma linha.</summary>
	// Token: 0x0200059D RID: 1437
	public sealed class TextCollapsedRange
	{
		// Token: 0x0600420C RID: 16908 RVA: 0x00102B44 File Offset: 0x00101F44
		internal TextCollapsedRange(int cp, int length, double width)
		{
			this._cp = cp;
			this._length = length;
			this._width = width;
		}

		/// <summary>Obtém o índice do primeiro caractere no <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> que representa caracteres de texto recolhidos.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o índice do primeiro caractere do texto recolhido.</returns>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x00102B6C File Offset: 0x00101F6C
		public int TextSourceCharacterIndex
		{
			get
			{
				return this._cp;
			}
		}

		/// <summary>Obtém o número de caracteres para o texto recolhido.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> recolhidos de valor que representa o número de caracteres de texto.</returns>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x00102B80 File Offset: 0x00101F80
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>A largura total dos caracteres de texto recolhidos.</summary>
		/// <returns>Um <see cref="T:System.Double" /> valor que representa a largura dos caracteres de texto recolhido.</returns>
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x00102B94 File Offset: 0x00101F94
		public double Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x04001817 RID: 6167
		private int _cp;

		// Token: 0x04001818 RID: 6168
		private int _length;

		// Token: 0x04001819 RID: 6169
		private double _width;
	}
}
