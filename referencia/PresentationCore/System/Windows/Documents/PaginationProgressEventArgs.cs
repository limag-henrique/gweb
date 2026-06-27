using System;

namespace System.Windows.Documents
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Documents.DynamicDocumentPaginator.PaginationProgress" /> .</summary>
	// Token: 0x0200030A RID: 778
	public class PaginationProgressEventArgs : EventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Documents.PaginationProgressEventArgs" />.</summary>
		/// <param name="start">O número (com base em zero) da primeira página que foi paginada.</param>
		/// <param name="count">O número de páginas contínuas que foram paginadas.</param>
		// Token: 0x060018DC RID: 6364 RVA: 0x00062DBC File Offset: 0x000621BC
		public PaginationProgressEventArgs(int start, int count)
		{
			this._start = start;
			this._count = count;
		}

		/// <summary>Obtém o número da primeira página que foi paginada.</summary>
		/// <returns>O número de página (baseado em zero) da primeira página que foi paginada.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00062DE0 File Offset: 0x000621E0
		public int Start
		{
			get
			{
				return this._start;
			}
		}

		/// <summary>Obtém o número de páginas contínuas que foram paginadas.</summary>
		/// <returns>O número de páginas contínuas que foram paginadas.</returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x00062DF4 File Offset: 0x000621F4
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x04000D5C RID: 3420
		private readonly int _start;

		// Token: 0x04000D5D RID: 3421
		private readonly int _count;
	}
}
