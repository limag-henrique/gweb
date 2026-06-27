using System;

namespace System.Windows
{
	/// <summary>Fornece uma identificação exclusiva para eventos cujos manipuladores são armazenados em uma tabela de hash interna.</summary>
	// Token: 0x020001B0 RID: 432
	public class EventPrivateKey
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.EventPrivateKey" />.</summary>
		// Token: 0x060006AF RID: 1711 RVA: 0x0001E948 File Offset: 0x0001DD48
		public EventPrivateKey()
		{
			this._globalIndex = GlobalEventManager.GetNextAvailableGlobalIndex(this);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001E968 File Offset: 0x0001DD68
		internal int GlobalIndex
		{
			get
			{
				return this._globalIndex;
			}
		}

		// Token: 0x040005A5 RID: 1445
		private int _globalIndex;
	}
}
