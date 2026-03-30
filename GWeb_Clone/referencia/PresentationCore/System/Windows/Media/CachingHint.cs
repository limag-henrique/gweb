using System;

namespace System.Windows.Media
{
	/// <summary>Especifica se deve armazenar em cache objetos de pincel lado a lado.</summary>
	// Token: 0x020003A2 RID: 930
	public enum CachingHint
	{
		/// <summary>Nenhuma dica de cache foi especificada.</summary>
		// Token: 0x04001126 RID: 4390
		Unspecified,
		/// <summary>Armazena em cache os objetos de pincel lado a lado em um buffer fora da tela, usando as dicas de cache especificadas pelas configurações de <see cref="T:System.Windows.Media.RenderOptions" />.</summary>
		// Token: 0x04001127 RID: 4391
		Cache
	}
}
