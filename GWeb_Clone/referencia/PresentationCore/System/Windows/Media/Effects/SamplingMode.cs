using System;

namespace System.Windows.Media.Effects
{
	/// <summary>Indica a maneira como propriedades de dependência com valor de <see cref="T:System.Windows.Media.Brush" /> são amostradas em um efeito de sombreador personalizado.</summary>
	// Token: 0x02000613 RID: 1555
	public enum SamplingMode
	{
		/// <summary>Use amostragem de vizinho mais próximo.</summary>
		// Token: 0x04001A18 RID: 6680
		NearestNeighbor,
		/// <summary>Use amostragem bilinear.</summary>
		// Token: 0x04001A19 RID: 6681
		Bilinear,
		/// <summary>O sistema selecionará o modo de amostragem mais apropriado.</summary>
		// Token: 0x04001A1A RID: 6682
		Auto
	}
}
