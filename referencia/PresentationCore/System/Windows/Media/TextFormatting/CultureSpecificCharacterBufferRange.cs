using System;
using System.Globalization;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa um intervalo de caracteres que estão associados a uma cultura.</summary>
	// Token: 0x02000595 RID: 1429
	public class CultureSpecificCharacterBufferRange
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.CultureSpecificCharacterBufferRange" />.</summary>
		/// <param name="culture">Um valor de <see cref="T:System.Globalization.CultureInfo" /> que representa a cultura do intervalo de caracteres que a contém.</param>
		/// <param name="characterBufferRange">Um valor de <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> que representa o intervalo de caracteres.</param>
		// Token: 0x060041D4 RID: 16852 RVA: 0x001025E4 File Offset: 0x001019E4
		public CultureSpecificCharacterBufferRange(CultureInfo culture, CharacterBufferRange characterBufferRange)
		{
			this._culture = culture;
			this._characterBufferRange = characterBufferRange;
		}

		/// <summary>Obtém o <see cref="T:System.Globalization.CultureInfo" /> do <see cref="T:System.Windows.Media.TextFormatting.CultureSpecificCharacterBufferRange" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x00102608 File Offset: 0x00101A08
		public CultureInfo CultureInfo
		{
			get
			{
				return this._culture;
			}
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" /> do <see cref="T:System.Windows.Media.TextFormatting.CultureSpecificCharacterBufferRange" />.</summary>
		/// <returns>Um valor do tipo <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferRange" />.</returns>
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x0010261C File Offset: 0x00101A1C
		public CharacterBufferRange CharacterBufferRange
		{
			get
			{
				return this._characterBufferRange;
			}
		}

		// Token: 0x04001806 RID: 6150
		private CultureInfo _culture;

		// Token: 0x04001807 RID: 6151
		private CharacterBufferRange _characterBufferRange;
	}
}
