using System;

namespace System.Windows.Input
{
	/// <summary>Especifica as políticas de captura do mouse.</summary>
	// Token: 0x0200022D RID: 557
	public enum CaptureMode
	{
		/// <summary>Sem captura do mouse.  A entrada do mouse vai para o elemento sob o mouse.</summary>
		// Token: 0x04000869 RID: 2153
		None,
		/// <summary>A captura do mouse é aplicada a um único elemento.  A entrada do mouse vai para o elemento capturado.</summary>
		// Token: 0x0400086A RID: 2154
		Element,
		/// <summary>A captura do mouse é aplicada a uma subárvore de elementos.  Se o mouse estiver sobre um filho do elemento com a captura, a entrada do mouse será enviada para o elemento filho.  Caso contrário, a entrada do mouse é enviada para o elemento com a captura do mouse.</summary>
		// Token: 0x0400086B RID: 2155
		SubTree
	}
}
