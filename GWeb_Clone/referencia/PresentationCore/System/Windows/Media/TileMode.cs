using System;

namespace System.Windows.Media
{
	/// <summary>Descreve como um <see cref="T:System.Windows.Media.TileBrush" /> pinta lado a lado em uma área de saída.</summary>
	// Token: 0x020003F6 RID: 1014
	public enum TileMode
	{
		/// <summary>O bloco de base é obtido, mas não é repetido. A área restante permanece transparente</summary>
		// Token: 0x0400128A RID: 4746
		None,
		/// <summary>O bloco base é desenhado e a área restante é preenchida repetindo-se o bloco base. A borda direita de um bloco atinge a borda esquerda do próximo e o mesmo acontece com as bordas inferior e superior.</summary>
		// Token: 0x0400128B RID: 4747
		Tile = 4,
		/// <summary>O mesmo que <see cref="F:System.Windows.Media.TileMode.Tile" />, exceto pelas colunas alternativas de imagens serem invertidas horizontalmente. A imagem base propriamente dita não é invertida.</summary>
		// Token: 0x0400128C RID: 4748
		FlipX = 1,
		/// <summary>O mesmo que <see cref="F:System.Windows.Media.TileMode.Tile" />, exceto pelas linhas alternativas de imagens serem invertidas verticalmente. A imagem base propriamente dita não é invertida.</summary>
		// Token: 0x0400128D RID: 4749
		FlipY,
		/// <summary>A combinação de <see cref="F:System.Windows.Media.TileMode.FlipX" /> e <see cref="F:System.Windows.Media.TileMode.FlipY" />. A imagem base propriamente dita não é invertida.</summary>
		// Token: 0x0400128E RID: 4750
		FlipXY
	}
}
