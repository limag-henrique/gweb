using System;

namespace System.Windows.Media
{
	/// <summary>Descreve as permissões de incorporação de fonte especificadas em um arquivo de fonte OpenType.</summary>
	// Token: 0x02000395 RID: 917
	public enum FontEmbeddingRight
	{
		/// <summary>Fontes com essa configuração indicam que podem ser inseridas e instaladas permanentemente no sistema remoto por um aplicativo. O usuário do sistema remoto adquire os direitos, obrigações e licenças idênticos para essa fonte como comprador original da fonte e está sujeito ao mesmos termos de licença, direitos autorais, patentes de design e/ou de marca que o comprador original.</summary>
		// Token: 0x040010F4 RID: 4340
		Installable,
		/// <summary>Fontes com essa configuração indicam que podem ser inseridas e instaladas permanentemente no sistema remoto por um aplicativo. Elas não podem ser subdivididas antes da inserção.</summary>
		// Token: 0x040010F5 RID: 4341
		InstallableButNoSubsetting,
		/// <summary>Fontes com essa configuração indicam que podem ser inseridas e instaladas permanentemente no sistema remoto por um aplicativo. Somente os bitmaps contidos nas fontes podem ser inseridos. Nenhum dado de estrutura de tópicos pode ser inserido.</summary>
		// Token: 0x040010F6 RID: 4342
		InstallableButWithBitmapsOnly,
		/// <summary>Fontes com essa configuração indicam que podem ser inseridas e instaladas permanentemente no sistema remoto por um aplicativo. Elas não podem ser subdivididas antes da inserção. Somente os bitmaps contidos nas fontes podem ser inseridos. Nenhum dado de estrutura de tópicos pode ser inserido.</summary>
		// Token: 0x040010F7 RID: 4343
		InstallableButNoSubsettingAndWithBitmapsOnly,
		/// <summary>Fontes com essa configuração não devem ser modificadas, inseridas ou trocadas de forma alguma sem primeiro obter permissão do proprietário legal.</summary>
		// Token: 0x040010F8 RID: 4344
		RestrictedLicense,
		/// <summary>A fonte pode ser inserida e temporariamente carregada no sistema remoto. Documentos que contêm a fonte devem ser abertos em um modo somente leitura.</summary>
		// Token: 0x040010F9 RID: 4345
		PreviewAndPrint,
		/// <summary>A fonte pode ser inserida e temporariamente carregada no sistema remoto. Documentos que contêm a fonte devem ser abertos em um modo somente leitura. A fonte não pode ser subdividida antes da inserção.</summary>
		// Token: 0x040010FA RID: 4346
		PreviewAndPrintButNoSubsetting,
		/// <summary>A fonte pode ser inserida e temporariamente carregada no sistema remoto. Documentos que contêm a fonte devem ser abertos em um modo somente leitura. Somente os bitmaps contidos na fonte podem ser inseridos. Nenhum dado de estrutura de tópicos pode ser inserido.</summary>
		// Token: 0x040010FB RID: 4347
		PreviewAndPrintButWithBitmapsOnly,
		/// <summary>A fonte pode ser inserida e temporariamente carregada no sistema remoto. Documentos que contêm a fonte devem ser abertos em um modo somente leitura. A fonte não pode ser subdividida antes da inserção. Somente os bitmaps contidos na fonte podem ser inseridos. Nenhum dado de estrutura de tópicos pode ser inserido.</summary>
		// Token: 0x040010FC RID: 4348
		PreviewAndPrintButNoSubsettingAndWithBitmapsOnly,
		/// <summary>A fonte pode ser inserida, mas só deve ser instalada temporariamente em outros sistemas. Em contraste com a configuração <see cref="F:System.Windows.Media.FontEmbeddingRight.PreviewAndPrint" />, os documentos que contêm fontes Editáveis podem ser abertos para leitura, a edição é permitida e as alterações podem ser salvas.</summary>
		// Token: 0x040010FD RID: 4349
		Editable,
		/// <summary>A fonte pode ser inserida, mas só deve ser instalada temporariamente em outros sistemas. Documentos que contêm a fonte podem ser abertos para leitura, a edição é permitida e as alterações podem ser salvas. A fonte não pode ser subdividida antes da inserção.</summary>
		// Token: 0x040010FE RID: 4350
		EditableButNoSubsetting,
		/// <summary>A fonte pode ser inserida, mas só deve ser instalada temporariamente em outros sistemas. Documentos que contêm a fonte podem ser abertos para leitura, a edição é permitida e as alterações podem ser salvas. Somente os bitmaps contidos na fonte podem ser inseridos. Nenhum dado de estrutura de tópicos pode ser inserido.</summary>
		// Token: 0x040010FF RID: 4351
		EditableButWithBitmapsOnly,
		/// <summary>A fonte pode ser inserida, mas só deve ser instalada temporariamente em outros sistemas. Documentos que contêm a fonte podem ser abertos para leitura, a edição é permitida e as alterações podem ser salvas. A fonte não pode ser subdividida antes da inserção. Somente os bitmaps contidos na fonte podem ser inseridos. Nenhum dado de estrutura de tópicos pode ser inserido.</summary>
		// Token: 0x04001100 RID: 4352
		EditableButNoSubsettingAndWithBitmapsOnly
	}
}
