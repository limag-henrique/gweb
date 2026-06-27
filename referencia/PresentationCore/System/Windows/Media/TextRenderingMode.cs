using System;

namespace System.Windows.Media
{
	/// <summary>Define os modos de renderização com suporte para texto.</summary>
	// Token: 0x02000402 RID: 1026
	public enum TextRenderingMode
	{
		/// <summary>O texto é renderizado com o algoritmo de renderização mais apropriado com base no modo de layout usado para formatar o texto.</summary>
		// Token: 0x040012B9 RID: 4793
		Auto,
		/// <summary>O texto é renderizado com suavização de dois níveis.</summary>
		// Token: 0x040012BA RID: 4794
		Aliased,
		/// <summary>O texto é renderizado com suavização de escala de cinza.</summary>
		// Token: 0x040012BB RID: 4795
		Grayscale,
		/// <summary>O texto é renderizado com o algoritmo de renderização ClearType mais apropriado com base no modo de layout usado para formatar o texto.</summary>
		// Token: 0x040012BC RID: 4796
		ClearType
	}
}
