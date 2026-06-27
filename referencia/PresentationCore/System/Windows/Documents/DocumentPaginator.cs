using System;
using System.ComponentModel;
using MS.Internal.PresentationCore;

namespace System.Windows.Documents
{
	/// <summary>Fornece uma classe base abstrata que dá suporte à criação de elementos de várias páginas com base em um único documento.</summary>
	// Token: 0x02000300 RID: 768
	public abstract class DocumentPaginator
	{
		/// <summary>Quando substituído em uma classe derivada, obtém o <see cref="T:System.Windows.Documents.DocumentPage" /> para o número de páginas especificado.</summary>
		/// <param name="pageNumber">O número de páginas com base em zero da página do documento que é necessária.</param>
		/// <returns>O <see cref="T:System.Windows.Documents.DocumentPage" /> para o <paramref name="pageNumber" /> especificado ou <see cref="F:System.Windows.Documents.DocumentPage.Missing" /> se a página não existe.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pageNumber" /> é negativo.</exception>
		// Token: 0x0600189B RID: 6299
		public abstract DocumentPage GetPage(int pageNumber);

		/// <summary>Retorna de maneira assíncrona (por meio do evento <see cref="E:System.Windows.Documents.DocumentPaginator.GetPageCompleted" />) a <see cref="T:System.Windows.Documents.DocumentPage" /> para o número de página especificado.</summary>
		/// <param name="pageNumber">O número de páginas com base em zero da página do documento que é necessária.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pageNumber" /> é negativo.</exception>
		// Token: 0x0600189C RID: 6300 RVA: 0x000627CC File Offset: 0x00061BCC
		public virtual void GetPageAsync(int pageNumber)
		{
			this.GetPageAsync(pageNumber, null);
		}

		/// <summary>Retorna de maneira assíncrona (por meio do evento <see cref="E:System.Windows.Documents.DocumentPaginator.GetPageCompleted" />) a <see cref="T:System.Windows.Documents.DocumentPage" /> para o número de página especificado e atribui a ID especificada à tarefa assíncrona.</summary>
		/// <param name="pageNumber">O número da página com base em zero do <see cref="T:System.Windows.Documents.DocumentPage" /> a ser obtido.</param>
		/// <param name="userState">Um identificador exclusivo para a tarefa assíncrona.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pageNumber" /> é negativo.</exception>
		// Token: 0x0600189D RID: 6301 RVA: 0x000627E4 File Offset: 0x00061BE4
		public virtual void GetPageAsync(int pageNumber, object userState)
		{
			if (pageNumber < 0)
			{
				throw new ArgumentOutOfRangeException("pageNumber", SR.Get("PaginatorNegativePageNumber"));
			}
			DocumentPage page = this.GetPage(pageNumber);
			this.OnGetPageCompleted(new GetPageCompletedEventArgs(page, pageNumber, null, false, userState));
		}

		/// <summary>Força uma paginação do conteúdo, atualiza <see cref="P:System.Windows.Documents.DocumentPaginator.PageCount" /> com o novo total e define <see cref="P:System.Windows.Documents.DocumentPaginator.IsPageCountValid" /> como <see langword="true" />.</summary>
		// Token: 0x0600189E RID: 6302 RVA: 0x00062824 File Offset: 0x00061C24
		public virtual void ComputePageCount()
		{
			this.GetPage(int.MaxValue);
		}

		/// <summary>De forma assíncrona, força uma paginação do conteúdo, atualiza <see cref="P:System.Windows.Documents.DocumentPaginator.PageCount" /> com o novo total e define <see cref="P:System.Windows.Documents.DocumentPaginator.IsPageCountValid" /> como <see langword="true" />.</summary>
		// Token: 0x0600189F RID: 6303 RVA: 0x00062840 File Offset: 0x00061C40
		public virtual void ComputePageCountAsync()
		{
			this.ComputePageCountAsync(null);
		}

		/// <summary>De forma assíncrona, força uma paginação do conteúdo, atualiza <see cref="P:System.Windows.Documents.DocumentPaginator.PageCount" /> com o novo total, define <see cref="P:System.Windows.Documents.DocumentPaginator.IsPageCountValid" /> como <see langword="true" />.</summary>
		/// <param name="userState">Um identificador exclusivo para a tarefa assíncrona.</param>
		// Token: 0x060018A0 RID: 6304 RVA: 0x00062854 File Offset: 0x00061C54
		public virtual void ComputePageCountAsync(object userState)
		{
			this.ComputePageCount();
			this.OnComputePageCountCompleted(new AsyncCompletedEventArgs(null, false, userState));
		}

		/// <summary>Cancela uma operação <see cref="Overload:System.Windows.Documents.DocumentPaginator.GetPageAsync" /> ou <see cref="Overload:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync" /> anterior.</summary>
		/// <param name="userState">O <paramref name="userState" /> original passado para <see cref="Overload:System.Windows.Documents.DocumentPaginator.GetPageAsync" />, <see cref="Overload:System.Windows.Documents.DynamicDocumentPaginator.GetPageNumberAsync" /> ou <see cref="Overload:System.Windows.Documents.DocumentPaginator.ComputePageCountAsync" /> que identifica a tarefa assíncrona a ser cancelada.</param>
		// Token: 0x060018A1 RID: 6305 RVA: 0x00062878 File Offset: 0x00061C78
		public virtual void CancelAsync(object userState)
		{
		}

		/// <summary>Quando substituído em uma classe derivada, obtém um valor que indica se <see cref="P:System.Windows.Documents.DocumentPaginator.PageCount" /> é o número total de páginas.</summary>
		/// <returns>
		///   <see langword="true" /> Se a paginação estiver concluída e <see cref="P:System.Windows.Documents.DocumentPaginator.PageCount" /> é o total de número de páginas; caso contrário, <see langword="false" />, se a paginação está em processo e <see cref="P:System.Windows.Documents.DocumentPaginator.PageCount" /> é o número de páginas atualmente formatadas (não o total).  
		/// Esse valor pode ser revertida <see langword="false" />, depois de ficar <see langword="true" />, se <see cref="P:System.Windows.Documents.DocumentPaginator.PageSize" /> ou alterações; de conteúdo, pois esses eventos forçaria uma repaginação.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060018A2 RID: 6306
		public abstract bool IsPageCountValid { get; }

		/// <summary>Quando substituído em uma classe derivada, obtém uma contagem do número de páginas atualmente formatadas</summary>
		/// <returns>Uma contagem do número de páginas que foram formatados.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060018A3 RID: 6307
		public abstract int PageCount { get; }

		/// <summary>Quando substituído em uma classe derivada, obtém ou define a largura e altura sugeridas de cada página.</summary>
		/// <returns>Um <see cref="T:System.Windows.Size" /> que representa a largura e altura de cada página.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060018A4 RID: 6308
		// (set) Token: 0x060018A5 RID: 6309
		public abstract Size PageSize { get; set; }

		/// <summary>Quando substituído em uma classe derivada, retorna o elemento sendo paginado.</summary>
		/// <returns>Um <see cref="T:System.Windows.Documents.IDocumentPaginatorSource" /> que representa o elemento que está sendo paginado.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060018A6 RID: 6310
		public abstract IDocumentPaginatorSource Source { get; }

		/// <summary>Ocorre quando <see cref="Overload:System.Windows.Documents.DocumentPaginator.GetPageAsync" /> é concluído.</summary>
		// Token: 0x1400016B RID: 363
		// (add) Token: 0x060018A7 RID: 6311 RVA: 0x00062888 File Offset: 0x00061C88
		// (remove) Token: 0x060018A8 RID: 6312 RVA: 0x000628C0 File Offset: 0x00061CC0
		public event GetPageCompletedEventHandler GetPageCompleted;

		/// <summary>Ocorre quando uma operação <see cref="Overload:System.Windows.Documents.DocumentPaginator.ComputePageCountAsync" /> foi concluída.</summary>
		// Token: 0x1400016C RID: 364
		// (add) Token: 0x060018A9 RID: 6313 RVA: 0x000628F8 File Offset: 0x00061CF8
		// (remove) Token: 0x060018AA RID: 6314 RVA: 0x00062930 File Offset: 0x00061D30
		public event AsyncCompletedEventHandler ComputePageCountCompleted;

		/// <summary>Ocorre quando o conteúdo do documento é alterado.</summary>
		// Token: 0x1400016D RID: 365
		// (add) Token: 0x060018AB RID: 6315 RVA: 0x00062968 File Offset: 0x00061D68
		// (remove) Token: 0x060018AC RID: 6316 RVA: 0x000629A0 File Offset: 0x00061DA0
		public event PagesChangedEventHandler PagesChanged;

		/// <summary>Aciona o evento <see cref="E:System.Windows.Documents.DocumentPaginator.GetPageCompleted" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Documents.GetPageCompletedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060018AD RID: 6317 RVA: 0x000629D8 File Offset: 0x00061DD8
		protected virtual void OnGetPageCompleted(GetPageCompletedEventArgs e)
		{
			if (this.GetPageCompleted != null)
			{
				this.GetPageCompleted(this, e);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Documents.DocumentPaginator.ComputePageCountCompleted" />.</summary>
		/// <param name="e">Um <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060018AE RID: 6318 RVA: 0x000629FC File Offset: 0x00061DFC
		protected virtual void OnComputePageCountCompleted(AsyncCompletedEventArgs e)
		{
			if (this.ComputePageCountCompleted != null)
			{
				this.ComputePageCountCompleted(this, e);
			}
		}

		/// <summary>Aciona o evento <see cref="E:System.Windows.Documents.DocumentPaginator.PagesChanged" />.</summary>
		/// <param name="e">Um <see cref="T:System.Windows.Documents.PagesChangedEventArgs" /> que contém os dados do evento.</param>
		// Token: 0x060018AF RID: 6319 RVA: 0x00062A20 File Offset: 0x00061E20
		protected virtual void OnPagesChanged(PagesChangedEventArgs e)
		{
			if (this.PagesChanged != null)
			{
				this.PagesChanged(this, e);
			}
		}
	}
}
