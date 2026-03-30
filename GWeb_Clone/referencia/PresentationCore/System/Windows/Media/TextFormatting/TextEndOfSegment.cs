using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Define uma execução de texto especializado usado para marcar o final de um segmento.</summary>
	// Token: 0x020005A3 RID: 1443
	public class TextEndOfSegment : TextRun
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextEndOfSegment" />.</summary>
		/// <param name="length">O número de caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextEndOfSegment" />.</param>
		// Token: 0x06004222 RID: 16930 RVA: 0x00102CF4 File Offset: 0x001020F4
		public TextEndOfSegment(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			this._length = length;
		}

		/// <summary>Obtém uma referência ao buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextEndOfSegment" />.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />.</returns>
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06004223 RID: 16931 RVA: 0x00102D28 File Offset: 0x00102128
		public sealed override CharacterBufferReference CharacterBufferReference
		{
			get
			{
				return default(CharacterBufferReference);
			}
		}

		/// <summary>Obtém o comprimento de caracteres do buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextEndOfSegment" />.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> objeto que representa o tamanho do buffer de caracteres.</returns>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x00102D40 File Offset: 0x00102140
		public sealed override int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Obtém o conjunto de propriedades compartilhadas por todos os caracteres de texto do buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextEndOfSegment" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> valor que representa as propriedades compartilhadas por todos os caracteres de texto.</returns>
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x00102D54 File Offset: 0x00102154
		public sealed override TextRunProperties Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001822 RID: 6178
		private int _length;
	}
}
