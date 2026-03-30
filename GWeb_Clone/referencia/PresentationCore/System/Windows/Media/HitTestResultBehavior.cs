using System;

namespace System.Windows.Media
{
	/// <summary>Determina se deve continuar a enumeração de todos os objetos visuais restantes durante um teste de clique.</summary>
	// Token: 0x0200040F RID: 1039
	public enum HitTestResultBehavior
	{
		/// <summary>Interrompa quaisquer testes de clique remanescentes e retorne do retorno de chamada.</summary>
		// Token: 0x040012FB RID: 4859
		Stop,
		/// <summary>Continue o teste de clique no próximo elemento visual na hierarquia da árvore visual.</summary>
		// Token: 0x040012FC RID: 4860
		Continue
	}
}
