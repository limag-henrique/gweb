using System;

namespace System.Windows.Documents
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Documents.DocumentPaginator.PagesChanged" /> .</summary>
	// Token: 0x02000308 RID: 776
	public class PagesChangedEventArgs : EventArgs
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Documents.PagesChangedEventArgs" />.</summary>
		/// <param name="start">O número (com base em zero) da primeira página que foi alterada.</param>
		/// <param name="count">O número de páginas contínuas que foram alteradas.</param>
		// Token: 0x060018D5 RID: 6357 RVA: 0x00062D70 File Offset: 0x00062170
		public PagesChangedEventArgs(int start, int count)
		{
			this._start = start;
			this._count = count;
		}

		/// <summary>Obtém o número da primeira página que foi alterada.</summary>
		/// <returns>O número (com base em zero) da primeira página que foi alterada.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00062D94 File Offset: 0x00062194
		public int Start
		{
			get
			{
				return this._start;
			}
		}

		/// <summary>Obtém o número de páginas contínuas que foram alteradas.</summary>
		/// <returns>O número de páginas contínuas que foram alteradas.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00062DA8 File Offset: 0x000621A8
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x04000D5A RID: 3418
		private readonly int _start;

		// Token: 0x04000D5B RID: 3419
		private readonly int _count;
	}
}
