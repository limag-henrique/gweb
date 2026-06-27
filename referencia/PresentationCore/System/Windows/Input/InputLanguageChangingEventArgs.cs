using System;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Contém argumentos associados ao evento <see cref="E:System.Windows.Input.InputLanguageManager.InputLanguageChanging" />.</summary>
	// Token: 0x02000248 RID: 584
	public class InputLanguageChangingEventArgs : InputLanguageEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Input.InputLanguageChangingEventArgs" />.</summary>
		/// <param name="newLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa um novo idioma de entrada atual.</param>
		/// <param name="previousLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada atual anterior.</param>
		// Token: 0x0600102F RID: 4143 RVA: 0x0003D0A8 File Offset: 0x0003C4A8
		public InputLanguageChangingEventArgs(CultureInfo newLanguageId, CultureInfo previousLanguageId) : base(newLanguageId, previousLanguageId)
		{
			this._rejected = false;
		}

		/// <summary>Obtém ou define um valor que indica se a alteração do idioma de entrada iniciada deve ser aceita ou rejeitada.</summary>
		/// <returns>
		///   <see langword="true" /> para rejeitar as alterações iniciadas do idioma de entrada; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003D0C4 File Offset: 0x0003C4C4
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0003D0D8 File Offset: 0x0003C4D8
		public bool Rejected
		{
			get
			{
				return this._rejected;
			}
			set
			{
				this._rejected = value;
			}
		}

		// Token: 0x040008BD RID: 2237
		private bool _rejected;
	}
}
