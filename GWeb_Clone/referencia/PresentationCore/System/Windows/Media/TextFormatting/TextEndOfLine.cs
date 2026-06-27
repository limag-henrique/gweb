using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Define um texto especializado executar que é usado para marcar o final de uma linha.</summary>
	// Token: 0x020005A1 RID: 1441
	public class TextEndOfLine : TextRun
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" /> usando um comprimento de caracteres especificado.</summary>
		/// <param name="length">O número de caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" />.</param>
		// Token: 0x0600421B RID: 16923 RVA: 0x00102C20 File Offset: 0x00102020
		public TextEndOfLine(int length) : this(length, null)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" /> usando um comprimento de caracteres especificado e um valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" />.</summary>
		/// <param name="length">O número de caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" />.</param>
		/// <param name="textRunProperties">O valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> a ser usado para os caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" />.</param>
		// Token: 0x0600421C RID: 16924 RVA: 0x00102C38 File Offset: 0x00102038
		public TextEndOfLine(int length, TextRunProperties textRunProperties)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			if (textRunProperties != null && textRunProperties.Typeface == null)
			{
				throw new ArgumentNullException("textRunProperties.Typeface");
			}
			this._length = length;
			this._textRunProperties = textRunProperties;
		}

		/// <summary>Obtém uma referência ao buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" />.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />.</returns>
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x00102C88 File Offset: 0x00102088
		public sealed override CharacterBufferReference CharacterBufferReference
		{
			get
			{
				return default(CharacterBufferReference);
			}
		}

		/// <summary>Obtém o comprimento de caracteres do buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" />.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> objeto que representa o tamanho do buffer de caracteres.</returns>
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x00102CA0 File Offset: 0x001020A0
		public sealed override int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Obtém o conjunto de propriedades compartilhadas por todos os caracteres de texto do buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextEndOfLine" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> valor que representa as propriedades compartilhadas por todos os caracteres de texto.</returns>
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x00102CB4 File Offset: 0x001020B4
		public sealed override TextRunProperties Properties
		{
			get
			{
				return this._textRunProperties;
			}
		}

		// Token: 0x04001820 RID: 6176
		private int _length;

		// Token: 0x04001821 RID: 6177
		private TextRunProperties _textRunProperties;
	}
}
