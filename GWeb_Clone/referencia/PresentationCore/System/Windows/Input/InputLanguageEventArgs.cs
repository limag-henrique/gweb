using System;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Fornece uma classe base para argumentos para eventos que lidam com uma alteração no idioma de entrada.</summary>
	// Token: 0x02000246 RID: 582
	public abstract class InputLanguageEventArgs : EventArgs
	{
		/// <summary>Inicializa valores de classe base para uma nova instância de uma classe derivada.</summary>
		/// <param name="newLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa um novo idioma de entrada atual.</param>
		/// <param name="previousLanguageId">Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada atual anterior.</param>
		// Token: 0x0600102B RID: 4139 RVA: 0x0003D044 File Offset: 0x0003C444
		protected InputLanguageEventArgs(CultureInfo newLanguageId, CultureInfo previousLanguageId)
		{
			this._newLanguageId = newLanguageId;
			this._previousLanguageId = previousLanguageId;
		}

		/// <summary>Obtém um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o novo idioma de entrada atual.</summary>
		/// <returns>Um <see cref="T:System.Globalization.CultureInfo" /> objeto que representa o novo idioma de entrada atual.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0003D068 File Offset: 0x0003C468
		public virtual CultureInfo NewLanguage
		{
			get
			{
				return this._newLanguageId;
			}
		}

		/// <summary>Obtém um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada atual anterior.</summary>
		/// <returns>Um objeto <see cref="T:System.Globalization.CultureInfo" /> que representa o idioma de entrada atual anterior.</returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0003D07C File Offset: 0x0003C47C
		public virtual CultureInfo PreviousLanguage
		{
			get
			{
				return this._previousLanguageId;
			}
		}

		// Token: 0x040008BB RID: 2235
		private CultureInfo _newLanguageId;

		// Token: 0x040008BC RID: 2236
		private CultureInfo _previousLanguageId;
	}
}
