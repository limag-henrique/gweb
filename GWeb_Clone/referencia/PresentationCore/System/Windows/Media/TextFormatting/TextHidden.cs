using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Define uma execução de texto especializado usada para marcar um intervalo de caracteres ocultos.</summary>
	// Token: 0x020005A7 RID: 1447
	public class TextHidden : TextRun
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextHidden" /> usando um comprimento de caracteres especificado.</summary>
		/// <param name="length">O número de caracteres no buffer <see cref="T:System.Windows.Media.TextFormatting.TextHidden" />.</param>
		// Token: 0x0600424C RID: 16972 RVA: 0x00103848 File Offset: 0x00102C48
		public TextHidden(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Get("ParameterMustBeGreaterThanZero"));
			}
			this._length = length;
		}

		/// <summary>Obtém uma referência ao buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextHidden" />.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" />.</returns>
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x0010387C File Offset: 0x00102C7C
		public sealed override CharacterBufferReference CharacterBufferReference
		{
			get
			{
				return default(CharacterBufferReference);
			}
		}

		/// <summary>Obtém o comprimento de caracteres do buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextHidden" />.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> objeto que representa o tamanho do buffer de caracteres.</returns>
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x0600424E RID: 16974 RVA: 0x00103894 File Offset: 0x00102C94
		public sealed override int Length
		{
			get
			{
				return this._length;
			}
		}

		/// <summary>Obtém o conjunto de propriedades compartilhadas por todos os caracteres de texto do buffer de caracteres <see cref="T:System.Windows.Media.TextFormatting.TextHidden" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> valor que representa as propriedades compartilhadas por todos os caracteres de texto.</returns>
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x001038A8 File Offset: 0x00102CA8
		public sealed override TextRunProperties Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400182E RID: 6190
		private int _length;
	}
}
