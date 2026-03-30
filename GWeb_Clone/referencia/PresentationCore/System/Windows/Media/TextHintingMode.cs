using System;

namespace System.Windows.Media
{
	/// <summary>Define o comportamento de renderização de texto estático ou animado.</summary>
	// Token: 0x02000403 RID: 1027
	public enum TextHintingMode
	{
		/// <summary>O mecanismo de renderização determina automaticamente se o texto será desenhado com configurações de qualidade apropriadas para um texto animado ou estático.</summary>
		// Token: 0x040012BE RID: 4798
		Auto,
		/// <summary>O mecanismo de renderização renderiza o texto com a melhor qualidade estática.</summary>
		// Token: 0x040012BF RID: 4799
		Fixed,
		/// <summary>O mecanismo de renderização renderiza o texto com a melhor qualidade animada.</summary>
		// Token: 0x040012C0 RID: 4800
		Animated
	}
}
