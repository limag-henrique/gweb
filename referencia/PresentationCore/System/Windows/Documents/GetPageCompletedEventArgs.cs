using System;
using System.ComponentModel;

namespace System.Windows.Documents
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Documents.DocumentPaginator.GetPageCompleted" /> .</summary>
	// Token: 0x02000303 RID: 771
	public class GetPageCompletedEventArgs : AsyncCompletedEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Documents.GetPageCompletedEventArgs" />.</summary>
		/// <param name="page">A <see cref="T:System.Windows.Documents.DocumentPage" /> para a <paramref name="pageNumber" /> solicitada.</param>
		/// <param name="pageNumber">O parâmetro <paramref name="pageNumber" /> passado para <see cref="M:System.Windows.Documents.DocumentPaginator.GetPageAsync(System.Int32,System.Object)" />.</param>
		/// <param name="error">A exceção que ocorreu durante a operação assíncrona ou NULO se nenhum erro ocorreu.</param>
		/// <param name="cancelled">
		///   <see langword="true" /> se a operação assíncrona foi cancelada; caso contrário, <see langword="false" />.</param>
		/// <param name="userState">O parâmetro <paramref name="userState" /> exclusivo passado para <see cref="M:System.Windows.Documents.DocumentPaginator.GetPageAsync(System.Int32,System.Object)" />.</param>
		// Token: 0x060018C6 RID: 6342 RVA: 0x00062CB0 File Offset: 0x000620B0
		public GetPageCompletedEventArgs(DocumentPage page, int pageNumber, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this._page = page;
			this._pageNumber = pageNumber;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Documents.DocumentPage" /> para a página especificada na chamada a <see cref="M:System.Windows.Documents.DocumentPaginator.GetPageAsync(System.Int32,System.Object)" />.</summary>
		/// <returns>A página de documento para a página especificada na chamada para <see cref="M:System.Windows.Documents.DocumentPaginator.GetPageAsync(System.Int32,System.Object)" />.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00062CD8 File Offset: 0x000620D8
		public DocumentPage DocumentPage
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._page;
			}
		}

		/// <summary>Obtém o número da página passado para <see cref="M:System.Windows.Documents.DocumentPaginator.GetPageAsync(System.Int32,System.Object)" />.</summary>
		/// <returns>O número da página passado para <see cref="M:System.Windows.Documents.DocumentPaginator.GetPageAsync(System.Int32,System.Object)" />.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00062CF4 File Offset: 0x000620F4
		public int PageNumber
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._pageNumber;
			}
		}

		// Token: 0x04000D56 RID: 3414
		private readonly DocumentPage _page;

		// Token: 0x04000D57 RID: 3415
		private readonly int _pageNumber;
	}
}
