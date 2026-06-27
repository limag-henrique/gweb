using System;

namespace System.Windows.Media.Imaging
{
	/// <summary>Especifica como uma imagem de bitmap se beneficia do cache de memória.</summary>
	// Token: 0x020005CE RID: 1486
	public enum BitmapCacheOption
	{
		/// <summary>Armazena em cache toda a imagem na memória. Este é o valor padrão.</summary>
		// Token: 0x04001863 RID: 6243
		Default,
		/// <summary>Cria um repositório em memória apenas para os dados solicitados. A primeira solicitação carrega a imagem diretamente; as solicitações subsequentes são preenchidas no cache.</summary>
		// Token: 0x04001864 RID: 6244
		OnDemand = 0,
		/// <summary>Armazena a imagem inteira na memória no tempo de carregamento. Todas as solicitações de dados de imagem são preenchidas no repositório em memória.</summary>
		// Token: 0x04001865 RID: 6245
		OnLoad,
		/// <summary>Não crie um repositório em memória. Todas as solicitações de imagem são preenchidas diretamente pelo arquivo de imagem.</summary>
		// Token: 0x04001866 RID: 6246
		None
	}
}
