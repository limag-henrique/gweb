using System;
using System.ComponentModel;

namespace System.Windows.Documents
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberCompleted" /> .</summary>
	// Token: 0x02000305 RID: 773
	public class GetPageNumberCompletedEventArgs : AsyncCompletedEventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Documents.GetPageNumberCompletedEventArgs" />.</summary>
		/// <param name="contentPosition">O parâmetro <paramref name="contentPosition" /> passado para <see cref="M:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync(System.Windows.Documents.ContentPosition)" />.</param>
		/// <param name="pageNumber">O número da página em que a <paramref name="contentPosition" /> é exibida.</param>
		/// <param name="error">A exceção que ocorreu durante a operação assíncrona ou NULO se nenhum erro ocorreu.</param>
		/// <param name="cancelled">
		///   <see langword="true" /> se a operação assíncrona foi cancelada; caso contrário, <see langword="false" />.</param>
		/// <param name="userState">O parâmetro <paramref name="userState" /> exclusivo passado para <see cref="M:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync(System.Windows.Documents.ContentPosition)" />.</param>
		// Token: 0x060018CD RID: 6349 RVA: 0x00062D10 File Offset: 0x00062110
		public GetPageNumberCompletedEventArgs(ContentPosition contentPosition, int pageNumber, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
		{
			this._contentPosition = contentPosition;
			this._pageNumber = pageNumber;
		}

		/// <summary>Obtém o <see cref="T:System.Windows.Documents.ContentPosition" /> passado para <see cref="M:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync(System.Windows.Documents.ContentPosition)" />.</summary>
		/// <returns>A posição do conteúdo passado para <see cref="M:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync(System.Windows.Documents.ContentPosition)" />.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00062D38 File Offset: 0x00062138
		public ContentPosition ContentPosition
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._contentPosition;
			}
		}

		/// <summary>Obtém o número da página para o <see cref="T:System.Windows.Documents.ContentPosition" /> passado para <see cref="M:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync(System.Windows.Documents.ContentPosition)" />.</summary>
		/// <returns>O número da página (com base em zero) para o <see cref="T:System.Windows.Documents.ContentPosition" /> passado para <see cref="M:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync(System.Windows.Documents.ContentPosition)" />.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00062D54 File Offset: 0x00062154
		public int PageNumber
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this._pageNumber;
			}
		}

		// Token: 0x04000D58 RID: 3416
		private readonly ContentPosition _contentPosition;

		// Token: 0x04000D59 RID: 3417
		private readonly int _pageNumber;
	}
}
