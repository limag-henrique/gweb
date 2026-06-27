using System;

namespace System.Windows
{
	/// <summary>Especifica como e se uma operação do tipo "arrastar e soltar" deve continuar.</summary>
	// Token: 0x020001A5 RID: 421
	public enum DragAction
	{
		/// <summary>A operação continuará.</summary>
		// Token: 0x0400057E RID: 1406
		Continue,
		/// <summary>A operação será interrompida ao soltar.</summary>
		// Token: 0x0400057F RID: 1407
		Drop,
		/// <summary>A operação é cancelada sem mensagem de soltar.</summary>
		// Token: 0x04000580 RID: 1408
		Cancel
	}
}
