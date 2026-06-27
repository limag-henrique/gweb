using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa o retângulo delimitador de uma sequência de texto.</summary>
	// Token: 0x02000599 RID: 1433
	public sealed class TextRunBounds
	{
		// Token: 0x060041E7 RID: 16871 RVA: 0x001027E8 File Offset: 0x00101BE8
		internal TextRunBounds(Rect bounds, int cpFirst, int cpEnd, TextRun textRun)
		{
			this._cpFirst = cpFirst;
			this._cch = cpEnd - cpFirst;
			this._bounds = bounds;
			this._textRun = textRun;
		}

		/// <summary>Obtém o índice de caracteres do primeiro caractere na sequência de texto associada.</summary>
		/// <returns>O índice que representa o primeiro caractere da sequência de texto associado.</returns>
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x0010281C File Offset: 0x00101C1C
		public int TextSourceCharacterIndex
		{
			get
			{
				return this._cpFirst;
			}
		}

		/// <summary>Obtém o comprimento de caracteres da sequência de texto associada.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o comprimento de caracteres.</returns>
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x00102830 File Offset: 0x00101C30
		public int Length
		{
			get
			{
				return this._cch;
			}
		}

		/// <summary>Obtém o retângulo delimitador para a sequência de texto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> executar de valor que representa o retângulo delimitador do texto.</returns>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x00102844 File Offset: 0x00101C44
		public Rect Rectangle
		{
			get
			{
				return this._bounds;
			}
		}

		/// <summary>Obtém o objeto <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> que representa a sequência de texto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> valor que representa o texto é executado.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x00102858 File Offset: 0x00101C58
		public TextRun TextRun
		{
			get
			{
				return this._textRun;
			}
		}

		// Token: 0x04001810 RID: 6160
		private int _cpFirst;

		// Token: 0x04001811 RID: 6161
		private int _cch;

		// Token: 0x04001812 RID: 6162
		private Rect _bounds;

		// Token: 0x04001813 RID: 6163
		private TextRun _textRun;
	}
}
