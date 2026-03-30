using System;
using System.Collections;
using System.Globalization;

namespace System.Windows.Input
{
	/// <summary>Define os recursos necessários para um objeto que pretende se comportar como uma origem de idioma de entrada.</summary>
	// Token: 0x0200023A RID: 570
	public interface IInputLanguageSource
	{
		/// <summary>Inicializa um objeto de origem do idioma de entrada.</summary>
		// Token: 0x06000FDF RID: 4063
		void Initialize();

		/// <summary>Cancela a inicialização de um objeto de origem do idioma de entrada.</summary>
		// Token: 0x06000FE0 RID: 4064
		void Uninitialize();

		/// <summary>Obtém ou define o idioma de entrada atual para este objeto de origem do idioma de entrada.</summary>
		/// <returns>Um <see cref="T:System.Globalization.CultureInfo" /> objeto que representa o idioma de entrada atual para este objeto de origem do idioma de entrada.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000FE1 RID: 4065
		// (set) Token: 0x06000FE2 RID: 4066
		CultureInfo CurrentInputLanguage { get; set; }

		/// <summary>Obtém uma lista de idiomas de entrada compatíveis com este objeto de origem do idioma de entrada.</summary>
		/// <returns>Um objeto enumerável que representa a lista de idiomas de entrada com suporte por este objeto de origem do idioma de entrada.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000FE3 RID: 4067
		IEnumerable InputLanguageList { get; }
	}
}
