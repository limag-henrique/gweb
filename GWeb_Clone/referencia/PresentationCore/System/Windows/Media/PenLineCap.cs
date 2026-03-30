using System;

namespace System.Windows.Media
{
	/// <summary>Descreve o formato no final de uma linha ou um segmento.</summary>
	// Token: 0x020003C7 RID: 967
	public enum PenLineCap
	{
		/// <summary>Um limite não se estende após o último ponto da linha. Comparável a nenhum limite de linha.</summary>
		// Token: 0x040011BA RID: 4538
		Flat,
		/// <summary>Um retângulo com altura igual à espessura da linha e comprimento igual à metade da espessura da linha.</summary>
		// Token: 0x040011BB RID: 4539
		Square,
		/// <summary>Um semicírculo com diâmetro igual à espessura da linha.</summary>
		// Token: 0x040011BC RID: 4540
		Round,
		/// <summary>Um triângulo isósceles reto cujo tamanho da base é igual à espessura da linha.</summary>
		// Token: 0x040011BD RID: 4541
		Triangle
	}
}
