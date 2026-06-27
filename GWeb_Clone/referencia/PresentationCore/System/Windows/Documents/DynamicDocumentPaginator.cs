using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Documents
{
	/// <summary>Fornece uma classe base abstrata que dá suporte às posições de conteúdo de acompanhamento e paginação em segundo plano automática nas repaginações além dos métodos e propriedades de sua própria classe base.</summary>
	// Token: 0x02000301 RID: 769
	public abstract class DynamicDocumentPaginator : DocumentPaginator
	{
		/// <summary>Quando substituído em uma classe derivada, retorna o número de página de base zero do <see cref="T:System.Windows.Documents.ContentPosition" /> especificado.</summary>
		/// <param name="contentPosition">A posição do conteúdo cujo número de página é necessário.</param>
		/// <returns>Um <see cref="T:System.Int32" /> que representa o número da página de base zero em que o <paramref name="contentPosition" /> especificado aparece.</returns>
		// Token: 0x060018B1 RID: 6321
		public abstract int GetPageNumber(ContentPosition contentPosition);

		/// <summary>De forma assíncrona, retorna (por meio Este método gera um evento <see cref="E:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberCompleted" />) o número de página de base zero do <see cref="T:System.Windows.Documents.ContentPosition" /> especificado.</summary>
		/// <param name="contentPosition">A posição do conteúdo cujo número de página é necessário.</param>
		// Token: 0x060018B2 RID: 6322 RVA: 0x00062A58 File Offset: 0x00061E58
		public virtual void GetPageNumberAsync(ContentPosition contentPosition)
		{
			this.GetPageNumberAsync(contentPosition, null);
		}

		/// <summary>De forma assíncrona, retorna (por meio Este método gera um evento <see cref="E:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberCompleted" />) o número de página de base zero do <see cref="T:System.Windows.Documents.ContentPosition" /> especificado.</summary>
		/// <param name="contentPosition">O elemento da posição do conteúdo do qual retornar o número da página.</param>
		/// <param name="userState">Um identificador exclusivo para a tarefa assíncrona.</param>
		// Token: 0x060018B3 RID: 6323 RVA: 0x00062A70 File Offset: 0x00061E70
		public virtual void GetPageNumberAsync(ContentPosition contentPosition, object userState)
		{
			if (contentPosition == null)
			{
				throw new ArgumentNullException("contentPosition");
			}
			if (contentPosition == ContentPosition.Missing)
			{
				throw new ArgumentException(SR.Get("PaginatorMissingContentPosition"), "contentPosition");
			}
			int pageNumber = this.GetPageNumber(contentPosition);
			this.OnGetPageNumberCompleted(new GetPageNumberCompletedEventArgs(contentPosition, pageNumber, null, false, userState));
		}

		/// <summary>Quando substituído em uma classe derivada, obtém a posição da página especificada no conteúdo do documento.</summary>
		/// <param name="page">A página cuja posição é necessária.</param>
		/// <returns>Uma representação <see cref="T:System.Windows.Documents.ContentPosition" /> da posição de <paramref name="page" />.</returns>
		// Token: 0x060018B4 RID: 6324
		public abstract ContentPosition GetPagePosition(DocumentPage page);

		/// <summary>Quando substituído em uma classe derivada, retorna um <see cref="T:System.Windows.Documents.ContentPosition" /> para o <see cref="T:System.Object" /> especificado.</summary>
		/// <param name="value">O objeto do qual retornar o <see cref="T:System.Windows.Documents.ContentPosition" />.</param>
		/// <returns>O <see cref="T:System.Windows.Documents.ContentPosition" /> do objeto especificado.</returns>
		// Token: 0x060018B5 RID: 6325
		public abstract ContentPosition GetObjectPosition(object value);

		/// <summary>Obtém ou define um valor que indica se a paginação é executada automaticamente em segundo plano em resposta a determinados eventos, como uma alteração no tamanho da página.</summary>
		/// <returns>
		///   <see langword="true" /> Se a paginação em segundo plano estiver habilitada; Caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00062AC0 File Offset: 0x00061EC0
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x00062AD0 File Offset: 0x00061ED0
		public virtual bool IsBackgroundPaginationEnabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Ocorre quando <see cref="Overload:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync" /> é concluído.</summary>
		// Token: 0x1400016E RID: 366
		// (add) Token: 0x060018B8 RID: 6328 RVA: 0x00062AE0 File Offset: 0x00061EE0
		// (remove) Token: 0x060018B9 RID: 6329 RVA: 0x00062B18 File Offset: 0x00061F18
		public event GetPageNumberCompletedEventHandler GetPageNumberCompleted;

		/// <summary>Ocorre quando todo o conteúdo do documento foi paginado.</summary>
		// Token: 0x1400016F RID: 367
		// (add) Token: 0x060018BA RID: 6330 RVA: 0x00062B50 File Offset: 0x00061F50
		// (remove) Token: 0x060018BB RID: 6331 RVA: 0x00062B88 File Offset: 0x00061F88
		public event EventHandler PaginationCompleted;

		/// <summary>Ocorre quando uma ou mais páginas de conteúdo foram paginadas.</summary>
		// Token: 0x14000170 RID: 368
		// (add) Token: 0x060018BC RID: 6332 RVA: 0x00062BC0 File Offset: 0x00061FC0
		// (remove) Token: 0x060018BD RID: 6333 RVA: 0x00062BF8 File Offset: 0x00061FF8
		public event PaginationProgressEventHandler PaginationProgress;

		/// <summary>Aciona o evento <see cref="E:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberCompleted" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Documents.GetPageNumberCompletedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060018BE RID: 6334 RVA: 0x00062C30 File Offset: 0x00062030
		protected virtual void OnGetPageNumberCompleted(GetPageNumberCompletedEventArgs e)
		{
			if (this.GetPageNumberCompleted != null)
			{
				this.GetPageNumberCompleted(this, e);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Documents.DynamicDocumentPaginator.PaginationProgress" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Documents.PaginationProgressEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060018BF RID: 6335 RVA: 0x00062C54 File Offset: 0x00062054
		protected virtual void OnPaginationProgress(PaginationProgressEventArgs e)
		{
			if (this.PaginationProgress != null)
			{
				this.PaginationProgress(this, e);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Documents.DynamicDocumentPaginator.PaginationCompleted" />.</summary>
		/// <param name="e">Um <see cref="T:System.EventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060018C0 RID: 6336 RVA: 0x00062C78 File Offset: 0x00062078
		protected virtual void OnPaginationCompleted(EventArgs e)
		{
			if (this.PaginationCompleted != null)
			{
				this.PaginationCompleted(this, e);
			}
		}
	}
}
