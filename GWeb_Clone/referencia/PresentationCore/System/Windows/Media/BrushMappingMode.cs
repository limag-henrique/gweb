using System;

namespace System.Windows.Media
{
	/// <summary>Especifica o sistema de coordenadas usado por um <see cref="T:System.Windows.Media.Brush" />.</summary>
	// Token: 0x020003A0 RID: 928
	public enum BrushMappingMode
	{
		/// <summary>O sistema de coordenadas não é relativo a uma caixa delimitadora. Os valores são interpretados diretamente no espaço local.</summary>
		// Token: 0x04001123 RID: 4387
		Absolute,
		/// <summary>O sistema de coordenadas é relativo a uma caixa delimitadora: 0 indica 0% da caixa delimitadora e 1 indica 100% da caixa delimitadora. Por exemplo, (0,5, 0,5) descreve um ponto no meio da caixa delimitadora e (1, 1) descreve um ponto na parte inferior direita da caixa delimitadora.</summary>
		// Token: 0x04001124 RID: 4388
		RelativeToBoundingBox
	}
}
