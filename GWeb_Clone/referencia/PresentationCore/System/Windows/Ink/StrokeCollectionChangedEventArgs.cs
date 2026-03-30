using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Ink
{
	/// <summary>Fornece dados para o evento de <see cref="E:System.Windows.Ink.StrokeCollection.StrokesChanged" /> .</summary>
	// Token: 0x02000344 RID: 836
	public class StrokeCollectionChangedEventArgs : EventArgs
	{
		// Token: 0x06001C4E RID: 7246 RVA: 0x000737FC File Offset: 0x00072BFC
		internal StrokeCollectionChangedEventArgs(StrokeCollection added, StrokeCollection removed, int index) : this(added, removed)
		{
			this._index = index;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.StrokeCollectionChangedEventArgs" />.</summary>
		/// <param name="added">Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços adicionados.</param>
		/// <param name="removed">Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços removidos.</param>
		// Token: 0x06001C4F RID: 7247 RVA: 0x00073818 File Offset: 0x00072C18
		public StrokeCollectionChangedEventArgs(StrokeCollection added, StrokeCollection removed)
		{
			if (added == null && removed == null)
			{
				throw new ArgumentException(SR.Get("CannotBothBeNull", new object[]
				{
					"added",
					"removed"
				}));
			}
			this._added = ((added == null) ? null : new StrokeCollection.ReadOnlyStrokeCollection(added));
			this._removed = ((removed == null) ? null : new StrokeCollection.ReadOnlyStrokeCollection(removed));
		}

		/// <summary>Obtém os traços que foram adicionados ao <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços adicionados.</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00073884 File Offset: 0x00072C84
		public StrokeCollection Added
		{
			get
			{
				if (this._added == null)
				{
					this._added = new StrokeCollection.ReadOnlyStrokeCollection(new StrokeCollection());
				}
				return this._added;
			}
		}

		/// <summary>Obtém os traços que foram removidos do <see cref="T:System.Windows.Ink.StrokeCollection" />.</summary>
		/// <returns>Um <see cref="T:System.Windows.Ink.StrokeCollection" /> que contém os traços removidos.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x000738B0 File Offset: 0x00072CB0
		public StrokeCollection Removed
		{
			get
			{
				if (this._removed == null)
				{
					this._removed = new StrokeCollection.ReadOnlyStrokeCollection(new StrokeCollection());
				}
				return this._removed;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x000738DC File Offset: 0x00072CDC
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04000F78 RID: 3960
		private StrokeCollection.ReadOnlyStrokeCollection _added;

		// Token: 0x04000F79 RID: 3961
		private StrokeCollection.ReadOnlyStrokeCollection _removed;

		// Token: 0x04000F7A RID: 3962
		private int _index = -1;
	}
}
