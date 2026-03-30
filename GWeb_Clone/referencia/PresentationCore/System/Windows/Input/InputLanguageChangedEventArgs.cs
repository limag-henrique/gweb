using System;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Contém argumentos associados ao evento <see cref="E:System.Windows.Input.InputLanguageManager.InputLanguageChanged" />.</summary>
	// Token: 0x02000247 RID: 583
	public class InputLanguageChangedEventArgs : InputLanguageEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputLanguageChangedEventArgs" />.</summary>
		/// <param name="newLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa um novo idioma de entrada atual.</param>
		/// <param name="previousLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada atual anterior.</param>
		// Token: 0x0600102E RID: 4142 RVA: 0x0003D090 File Offset: 0x0003C490
		public InputLanguageChangedEventArgs(CultureInfo newLanguageId, CultureInfo previousLanguageId) : base(newLanguageId, previousLanguageId)
		{
		}
	}
}
