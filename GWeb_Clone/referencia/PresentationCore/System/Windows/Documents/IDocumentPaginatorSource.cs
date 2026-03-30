using System;

namespace System.Windows.Documents
{
	/// <summary>Define o objeto de origem que executa a paginação de conteúdo.</summary>
	// Token: 0x02000306 RID: 774
	public interface IDocumentPaginatorSource
	{
		/// <summary>Quando implementado em uma classe derivada, obtém o objeto que executa a paginação de conteúdo.</summary>
		/// <returns>O objeto que executa a paginação de conteúdo real.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060018D0 RID: 6352
		DocumentPaginator DocumentPaginator { get; }
	}
}
